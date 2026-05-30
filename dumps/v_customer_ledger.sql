DROP VIEW IF EXISTS `v_customer_ledger`;

CREATE VIEW `v_customer_ledger` AS
SELECT
	c.concessionaire_id AS concessionaire_id,
	c.concessionaire_code AS concessionaire_code,
	c.concessionaire_name AS concessionaire_name,
	c.address AS address,
	c.zone_id AS zone_id,
	b.billing_date AS transaction_date,
	CONCAT('BILL-', b.bill_number) AS reference_no,
	'' AS remarks,
	ROUND(COALESCE(b.total_amount, 0.00), 2) AS debit,
	0.00 AS credit,
	ROUND(COALESCE(b.water_charge, 0.00), 2) AS water_charge,
	ROUND(COALESCE(b.tax_amount, 0.00), 2) AS tax_amount,
	ROUND(COALESCE(b.penalty_amount, 0.00), 2) AS penalty_amount,
	ROUND(COALESCE(b.scf_amount, 0.00), 2) AS scf_amount,
	ROUND(COALESCE(b.arrears_amount, 0.00), 2) AS arrears_amount,
	b.status AS billing_status
FROM billing b
INNER JOIN concessionaire c
	ON c.concessionaire_id = b.concessionaire_id

UNION ALL

SELECT
	c.concessionaire_id AS concessionaire_id,
	c.concessionaire_code AS concessionaire_code,
	c.concessionaire_name AS concessionaire_name,
	c.address AS address,
	c.zone_id AS zone_id,
	col.collection_date AS transaction_date,
	CONCAT('OR-', col.or_number) AS reference_no,
	COALESCE(col.remarks, '') AS remarks,
	0.00 AS debit,
	ROUND(COALESCE(col.grand_total, 0.00), 2) AS credit,
	ROUND(COALESCE(col.total_current_bill, 0.00), 2) AS water_charge,
	ROUND(COALESCE(col.total_tax, 0.00), 2) AS tax_amount,
	ROUND(COALESCE(col.total_penalty, 0.00), 2) AS penalty_amount,
	ROUND(COALESCE(col.total_scf, 0.00), 2) AS scf_amount,
	ROUND(COALESCE(col.total_arrears, 0.00), 2) AS arrears_amount,
	NULL AS billing_status
FROM collection col
INNER JOIN concessionaire c
	ON (
		c.concessionaire_code = col.concessionaire_code
		OR FIND_IN_SET(c.concessionaire_code, REPLACE(COALESCE(col.concessionaire_code, ''), ' ', '')) > 0
	);
