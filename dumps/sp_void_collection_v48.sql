
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_void_collection_v48`(
	IN p_collection_id INT,
	IN p_voided_by_user_id INT,
	IN p_void_remarks TEXT
)
sp_main: BEGIN
	DECLARE v_done INT DEFAULT 0;

	DECLARE v_target_collection_id INT DEFAULT 0;
	DECLARE v_collection_status VARCHAR(50) DEFAULT '';
	DECLARE v_collection_total_scf DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_collection_remarks TEXT;
	DECLARE v_concessionaire_code VARCHAR(50) DEFAULT '';

	DECLARE v_payment_id INT;
	DECLARE v_billing_id INT;
	DECLARE v_water_paid DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_tax_paid DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_penalty_paid DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_scf_paid DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_amount_paid DECIMAL(12,2) DEFAULT 0.00;

	DECLARE v_voided_payment_count INT DEFAULT 0;
	DECLARE v_scf_concessionaire_id INT DEFAULT NULL;

	DECLARE cur_payments CURSOR FOR
		SELECT
			p.payment_id,
			p.billing_id,
			COALESCE(p.water_paid, 0.00),
			COALESCE(p.tax_paid, 0.00),
			COALESCE(p.penalty_paid, 0.00),
			COALESCE(p.scf_paid, 0.00),
			COALESCE(p.amount_paid, 0.00)
		FROM payment p
		WHERE p.collection_id = v_target_collection_id
		  AND COALESCE(p.status, 'POSTED') IN ('POSTED', 'PAID')
		FOR UPDATE;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
		DROP TEMPORARY TABLE IF EXISTS tmp_affected_billing;
		RESIGNAL;
	END;

	IF p_collection_id IS NULL OR p_collection_id <= 0 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid collection id';
	END IF;

	IF p_voided_by_user_id IS NULL OR p_voided_by_user_id <= 0 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid user id';
	END IF;

	SET p_void_remarks = COALESCE(p_void_remarks, '');
	SET v_target_collection_id = p_collection_id;

	START TRANSACTION;

	SELECT
		COALESCE(c.status, ''),
		COALESCE(c.total_scf, 0.00),
		COALESCE(c.remarks, ''),
		COALESCE(c.concessionaire_code, '')
	INTO
		v_collection_status,
		v_collection_total_scf,
		v_collection_remarks,
		v_concessionaire_code
	FROM collection c
	WHERE c.collection_id = v_target_collection_id
	FOR UPDATE;

	IF v_collection_status = '' THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Collection not found';
	END IF;

	IF UPPER(v_collection_status) IN ('VOID', 'VOIDED') THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Collection already voided';
	END IF;

	DROP TEMPORARY TABLE IF EXISTS tmp_affected_billing;
	CREATE TEMPORARY TABLE tmp_affected_billing (
		billing_id INT NOT NULL PRIMARY KEY
	) ENGINE=InnoDB;

	OPEN cur_payments;

	payment_loop: LOOP
		FETCH cur_payments INTO
			v_payment_id,
			v_billing_id,
			v_water_paid,
			v_tax_paid,
			v_penalty_paid,
			v_scf_paid,
			v_amount_paid;

		IF v_done = 1 THEN
			LEAVE payment_loop;
		END IF;

		INSERT IGNORE INTO tmp_affected_billing (billing_id)
		VALUES (v_billing_id);

		UPDATE billing b
		SET
			b.remaining_water_charge   = ROUND(COALESCE(b.remaining_water_charge, 0.00) + v_water_paid, 2),
			b.remaining_tax_amount     = ROUND(COALESCE(b.remaining_tax_amount, 0.00) + v_tax_paid, 2),
			b.remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0.00) + v_penalty_paid, 2),
			b.remaining_scf_amount     = ROUND(COALESCE(b.remaining_scf_amount, 0.00) + v_scf_paid, 2),
			b.remaining_balance        = ROUND(COALESCE(b.remaining_balance, 0.00) + v_amount_paid, 2),
			b.paid_at                  = NULL,
			b.updated_at               = NOW(),
			b.updated_by_user_id       = p_voided_by_user_id
		WHERE b.billing_id = v_billing_id;

		UPDATE payment p
		SET
			p.status = 'VOIDED',
			p.collection_id = NULL,
			p.remarks = CONCAT_WS(
				' | ',
				NULLIF(TRIM(COALESCE(p.remarks, '')), ''),
				CONCAT('Voided from collection #', v_target_collection_id)
			)
		WHERE p.payment_id = v_payment_id;

		SET v_voided_payment_count = v_voided_payment_count + 1;
	END LOOP;

	CLOSE cur_payments;

	UPDATE billing b
	JOIN tmp_affected_billing t
		ON t.billing_id = b.billing_id
	SET
		b.payment_count = (
			SELECT COUNT(*)
			FROM payment p2
			WHERE p2.billing_id = b.billing_id
			  AND COALESCE(p2.status, 'POSTED') IN ('POSTED', 'PAID')
		),
		b.last_payment_date = (
			SELECT MAX(p2.payment_date)
			FROM payment p2
			WHERE p2.billing_id = b.billing_id
			  AND COALESCE(p2.status, 'POSTED') IN ('POSTED', 'PAID')
		),
		b.last_collection_id = (
			SELECT p2.collection_id
			FROM payment p2
			WHERE p2.billing_id = b.billing_id
			  AND COALESCE(p2.status, 'POSTED') IN ('POSTED', 'PAID')
			ORDER BY p2.payment_date DESC, p2.payment_id DESC
			LIMIT 1
		),
		b.status = CASE
			WHEN COALESCE(b.remaining_balance, 0.00) = 0 THEN 'paid'
			WHEN b.due_date IS NOT NULL AND DATE(b.due_date) < CURDATE() THEN 'overdue'
			ELSE 'partially_paid'
		END,
		b.scf_status = CASE
			WHEN COALESCE(b.remaining_scf_amount, 0.00) = 0 THEN 'paid'
			ELSE 'unpaid'
		END,
		b.updated_at = NOW(),
		b.updated_by_user_id = p_voided_by_user_id;

	IF v_voided_payment_count = 0 AND v_collection_total_scf <= 0 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No voidable payment rows found for this collection';
	END IF;

	IF v_voided_payment_count = 0 AND v_collection_total_scf > 0 THEN
		SELECT c.concessionaire_id
		INTO v_scf_concessionaire_id
		FROM concessionaire c
		WHERE c.concessionaire_code = v_concessionaire_code
		LIMIT 1;

		IF v_scf_concessionaire_id IS NOT NULL THEN
			UPDATE scf_balance sb
			SET
				sb.balance = ROUND(COALESCE(sb.balance, 0.00) + v_collection_total_scf, 2),
				sb.updated_at = NOW()
			WHERE sb.concessionaire_id = v_scf_concessionaire_id
			ORDER BY sb.scf_id DESC
			LIMIT 1;
		END IF;
	END IF;

	DELETE FROM collection
	WHERE collection_id = v_target_collection_id;

	INSERT INTO user_logs (action, user_id)
	VALUES (
		CONCAT_WS(
			' | ',
			CONCAT('Collection deleted after void #', v_target_collection_id),
			CONCAT('Voided Payments: ', v_voided_payment_count),
			CONCAT('SCF Reversed (if SCF-only): ', ROUND(v_collection_total_scf, 2)),
			CONCAT('Concessionaire Code: ', COALESCE(v_concessionaire_code, 'N/A')),
			CONCAT('Remarks: ', COALESCE(NULLIF(TRIM(p_void_remarks), ''), 'N/A'))
		),
		p_voided_by_user_id
	);

	COMMIT;

	DROP TEMPORARY TABLE IF EXISTS tmp_affected_billing;

	SELECT
		v_target_collection_id AS collection_id,
		'DELETED' AS status,
		v_voided_payment_count AS voided_payment_count,
		ROUND(v_collection_total_scf, 2) AS scf_reversed_if_scf_only;
END$$
DELIMITER ;
