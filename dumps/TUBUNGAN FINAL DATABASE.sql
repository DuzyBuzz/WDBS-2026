CREATE DATABASE  IF NOT EXISTS `wdbs_tubungan_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `wdbs_tubungan_db`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: wdbs_tubungan_db
-- ------------------------------------------------------
-- Server version	8.0.44

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `billing`
--

DROP TABLE IF EXISTS `billing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `billing` (
  `billing_id` int NOT NULL AUTO_INCREMENT,
  `bill_number` int NOT NULL,
  `concessionaire_id` int NOT NULL,
  `reading_id` int NOT NULL,
  `billing_date` date NOT NULL DEFAULT (curdate()),
  `due_date` date NOT NULL,
  `consumption` int NOT NULL,
  `free_water` int NOT NULL DEFAULT '0',
  `water_charge` decimal(12,2) NOT NULL,
  `discount_amount` decimal(10,2) DEFAULT '0.00',
  `tax_amount` decimal(10,2) DEFAULT '0.00',
  `total_water_bill` decimal(12,2) DEFAULT NULL,
  `scf_amount` decimal(12,2) DEFAULT '0.00',
  `arrears_amount` decimal(12,2) DEFAULT '0.00',
  `penalty_amount` decimal(12,2) DEFAULT '0.00',
  `total_amount` decimal(12,2) NOT NULL,
  `remaining_water_charge` decimal(12,2) DEFAULT '0.00',
  `remaining_tax_amount` decimal(12,2) DEFAULT '0.00',
  `remaining_penalty_amount` decimal(12,2) DEFAULT '0.00',
  `remaining_scf_amount` decimal(12,2) DEFAULT '0.00',
  `remaining_balance` decimal(12,2) DEFAULT '0.00',
  `status` enum('unpaid','paid','overdue','partially_paid') DEFAULT 'unpaid',
  `scf_status` varchar(50) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `last_payment_date` datetime DEFAULT NULL,
  `is_initial` tinyint(1) NOT NULL DEFAULT '0',
  `payment_count` int DEFAULT '0',
  `last_collection_id` int DEFAULT NULL,
  `created_by_user_id` int DEFAULT NULL,
  `updated_by_user_id` int DEFAULT NULL,
  `tax_percent_used` decimal(12,2) DEFAULT '0.00',
  `discount_percent_used` decimal(12,2) DEFAULT '0.00',
  `penalty_percent_used` decimal(12,2) DEFAULT '0.00',
  `is_penalty_applied` tinyint DEFAULT '0',
  `penalty_applied_at` datetime DEFAULT NULL,
  `request_id` varchar(200) DEFAULT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `scf_monthly_used` decimal(12,2) DEFAULT '0.00',
  `scf_total_cap_used` decimal(12,2) DEFAULT '0.00',
  `paid_at` datetime DEFAULT NULL,
  PRIMARY KEY (`billing_id`),
  UNIQUE KEY `bill_number_UNIQUE` (`bill_number`),
  KEY `concessionaire_id` (`concessionaire_id`),
  KEY `reading_id` (`reading_id`),
  KEY `idx_billing_bill_number` (`bill_number`),
  KEY `idx_billing_concessionaire_is_initial` (`concessionaire_id`,`is_initial`),
  KEY `idx_billing_remaining` (`remaining_balance`),
  KEY `idx_billing_due_date` (`due_date`),
  KEY `idx_billing_concessionaire_balance` (`concessionaire_id`,`remaining_balance`),
  KEY `idx_billing_request_id` (`request_id`),
  CONSTRAINT `billing_fk_concessionaire` FOREIGN KEY (`concessionaire_id`) REFERENCES `concessionaire` (`concessionaire_id`),
  CONSTRAINT `billing_fk_reading` FOREIGN KEY (`reading_id`) REFERENCES `reading` (`reading_id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing`
--

LOCK TABLES `billing` WRITE;
/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
INSERT INTO `billing` VALUES (27,1,1805,17614,'2026-06-03','2026-06-17',10,0,195.00,0.00,3.90,198.90,0.00,0.00,0.00,198.90,195.00,3.90,0.00,0.00,198.90,'unpaid','paid','2026-06-03 23:26:28',NULL,1,0,NULL,3,3,2.00,7.00,10.00,0,NULL,'MR-20260603-3-1805-b7e25a44a7904aa7901855312d5e8b61','2026-06-03 23:26:28',500.00,0.00,NULL),(28,2,1806,17615,'2026-05-03','2026-05-17',10,0,181.35,13.92,3.63,184.98,500.00,0.00,18.14,684.98,181.35,3.63,18.14,500.00,703.12,'unpaid','unpaid','2026-06-03 23:28:11',NULL,1,0,NULL,3,3,2.00,7.00,10.00,1,'2026-06-03 23:26:53','MR-20260603-3-1806-93dd031fdefc4617916a7d3ef556e7b8','2026-06-03 23:26:28',500.00,1700.00,NULL),(29,3,1806,17616,'2026-06-03','2026-06-17',10,0,181.35,13.92,3.63,184.98,500.00,184.98,0.00,869.96,181.35,3.63,0.00,500.00,684.98,'unpaid','unpaid','2026-06-03 23:28:11',NULL,0,0,NULL,3,3,2.00,7.00,10.00,0,NULL,'MR-20260603-3-1806-7660dc27b0254377840f60067967766b','2026-06-03 23:26:53',500.00,1200.00,NULL);
/*!40000 ALTER TABLE `billing` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_billing_mark_initial` BEFORE INSERT ON `billing` FOR EACH ROW BEGIN
    DECLARE existing_count INT DEFAULT 0;

    -- Count how many billing records this concessionaire already has
    SELECT COUNT(*) INTO existing_count
    FROM billing
    WHERE concessionaire_id = NEW.concessionaire_id;

    -- If none, this is the first billing
    IF existing_count = 0 THEN
        SET NEW.is_initial = 1;
    ELSE
        SET NEW.is_initial = 0;
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_billing_scf_paid_on_insert` BEFORE INSERT ON `billing` FOR EACH ROW BEGIN
    -- If SCF amount is zero, mark SCF as paid
    IF NEW.scf_amount = 0.00 THEN
        SET NEW.scf_status = 'paid';
    END IF;

    -- If water bill amount is zero, mark water bill as paid
    IF NEW.total_water_bill = 0.00 THEN
        SET NEW.status = 'paid';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_billing_before_insert` BEFORE INSERT ON `billing` FOR EACH ROW BEGIN
    DECLARE v_is_not_billable TINYINT(1);

    SELECT is_not_billable
    INTO v_is_not_billable
    FROM concessionaire
    WHERE concessionaire_id = NEW.concessionaire_id;

    IF v_is_not_billable = 1 THEN

        -- Keep consumption unchanged
        -- Keep reading information unchanged

        SET NEW.water_charge = 0.00;
        SET NEW.discount_amount = 0.00;
        SET NEW.tax_amount = 0.00;
        SET NEW.total_water_bill = 0.00;

        SET NEW.scf_amount = 0.00;
        SET NEW.arrears_amount = 0.00;
        SET NEW.penalty_amount = 0.00;

        SET NEW.total_amount = 0.00;

        SET NEW.remaining_water_charge = 0.00;
        SET NEW.remaining_tax_amount = 0.00;
        SET NEW.remaining_penalty_amount = 0.00;
        SET NEW.remaining_scf_amount = 0.00;
        SET NEW.remaining_balance = 0.00;

        SET NEW.status = 'paid';
                SET NEW.scf_status = 'paid';
        SET NEW.payment_count = 0;
        SET NEW.last_payment_date = NULL;
        SET NEW.paid_at = NOW();
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_billing_scf_paid_on_update` BEFORE UPDATE ON `billing` FOR EACH ROW BEGIN
    -- Check if scf_amount becomes 0
    IF NEW.scf_amount = 0.00 THEN
        SET NEW.scf_status = 'paid';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_billing_after_delete` AFTER DELETE ON `billing` FOR EACH ROW BEGIN
  IF OLD.reading_id IS NOT NULL THEN
    DELETE FROM `reading` WHERE reading_id = OLD.reading_id;
  END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `billing_invoice_print_settings`
--

DROP TABLE IF EXISTS `billing_invoice_print_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `billing_invoice_print_settings` (
  `id` int NOT NULL AUTO_INCREMENT,
  `field_name` varchar(100) NOT NULL,
  `x_position` int NOT NULL,
  `y_position` int NOT NULL,
  `test_print` varchar(150) DEFAULT NULL,
  `note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `field_name_UNIQUE` (`field_name`)
) ENGINE=InnoDB AUTO_INCREMENT=141 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing_invoice_print_settings`
--

LOCK TABLES `billing_invoice_print_settings` WRITE;
/*!40000 ALTER TABLE `billing_invoice_print_settings` DISABLE KEYS */;
/*!40000 ALTER TABLE `billing_invoice_print_settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `collection`
--

DROP TABLE IF EXISTS `collection`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `collection` (
  `collection_id` int NOT NULL AUTO_INCREMENT,
  `or_number` varchar(50) DEFAULT NULL,
  `collection_date` date DEFAULT NULL,
  `bill_numbers` text,
  `concessionaire_code` text,
  `concessionaire_name` text,
  `address` text,
  `total_current_bill` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_arrears` decimal(12,2) DEFAULT '0.00',
  `total_penalty` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_tax` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_scf` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_water_bill_paid` decimal(12,2) DEFAULT '0.00',
  `scf_paid` decimal(12,2) DEFAULT '0.00',
  `total_others` decimal(12,2) NOT NULL DEFAULT '0.00',
  `grand_total` decimal(14,2) NOT NULL DEFAULT '0.00',
  `amount_received` decimal(12,2) NOT NULL DEFAULT '0.00',
  `change_amount` decimal(12,2) NOT NULL DEFAULT '0.00',
  `total_paid_amount` decimal(12,2) DEFAULT '0.00',
  `uncollected` decimal(12,2) NOT NULL DEFAULT '0.00',
  `created_by` int DEFAULT NULL,
  `created_by_name` varchar(191) DEFAULT NULL,
  `remarks` text,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `payment_type` text,
  `payment_reference` text,
  `payment_ids` text,
  `total_discount` decimal(12,2) DEFAULT '0.00',
  `status` varchar(50) DEFAULT 'POSTED',
  `voided_by_user_id` int DEFAULT NULL,
  `voided_at` datetime DEFAULT NULL,
  `billing_count` int DEFAULT '0',
  `payment_count` int DEFAULT '0',
  `payor_name` varchar(65) DEFAULT NULL,
  PRIMARY KEY (`collection_id`),
  UNIQUE KEY `or_number_UNIQUE` (`or_number`),
  KEY `idx_collection_date` (`collection_date`),
  KEY `idx_created_by` (`created_by`),
  KEY `idx_collection_or_number` (`or_number`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collection`
--

LOCK TABLES `collection` WRITE;
/*!40000 ALTER TABLE `collection` DISABLE KEYS */;
/*!40000 ALTER TABLE `collection` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_collection_before_insert` BEFORE INSERT ON `collection` FOR EACH ROW BEGIN
    DECLARE v_bill_no VARCHAR(50);
    DECLARE v_rest TEXT;
    DECLARE v_pos INT DEFAULT 0;
    DECLARE v_total_disc DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_disc DECIMAL(12,2) DEFAULT 0.00;

    -- Start with all bill numbers
    SET v_rest = NEW.bill_numbers;

    -- Loop while there are still values separated by commas
    WHILE v_rest IS NOT NULL AND v_rest <> '' DO
        -- Find position of next comma
        SET v_pos = LOCATE(',', v_rest);

        IF v_pos > 0 THEN
            SET v_bill_no = TRIM(SUBSTRING(v_rest, 1, v_pos - 1));
            SET v_rest = SUBSTRING(v_rest, v_pos + 1);
        ELSE
            SET v_bill_no = TRIM(v_rest);
            SET v_rest = NULL;
        END IF;

        -- Look up the discount for that bill number
        SELECT IFNULL(SUM(discount_amount), 0.00)
        INTO v_disc
        FROM billing
        WHERE bill_number = v_bill_no;

        -- Add to total
        SET v_total_disc = v_total_disc + v_disc;
    END WHILE;

    -- Store total discount in the collection record
    SET NEW.total_discount = v_total_disc;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_calculate_total_paid_amount` BEFORE INSERT ON `collection` FOR EACH ROW BEGIN
    -- Calculate the total paid amount every time a new collection row is inserted
    -- total_paid_amount = amount_received - change_amount
    SET NEW.total_paid_amount = (NEW.amount_received - NEW.change_amount);
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_collection_delete` AFTER DELETE ON `collection` FOR EACH ROW BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE payment_id INT;
    DECLARE payment_cursor CURSOR FOR
        SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(OLD.payment_ids, ',', n.n), ',', -1) AS UNSIGNED)
        FROM (
            SELECT 1 AS n UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5
            UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9 UNION ALL SELECT 10
            -- extend if you expect more than 10 payments per collection
        ) n
        WHERE n.n <= 1 + LENGTH(OLD.payment_ids) - LENGTH(REPLACE(OLD.payment_ids, ',', ''));

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    OPEN payment_cursor;

    read_loop: LOOP
        FETCH payment_cursor INTO payment_id;
        IF done THEN
            LEAVE read_loop;
        END IF;

        DELETE FROM payment WHERE payment_id = payment_id;
    END LOOP;

    CLOSE payment_cursor;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_after_delete_collection` AFTER DELETE ON `collection` FOR EACH ROW BEGIN
    DECLARE done INT DEFAULT 0;

    DECLARE v_payment_id INT;
    DECLARE v_billing_id INT;

    DECLARE v_water_paid DECIMAL(12,2);
    DECLARE v_tax_paid DECIMAL(12,2);
    DECLARE v_penalty_paid DECIMAL(12,2);
    DECLARE v_scf_paid DECIMAL(12,2);

    DECLARE cur CURSOR FOR
        SELECT
            payment_id,
            billing_id,
            COALESCE(water_paid,0),
            COALESCE(tax_paid,0),
            COALESCE(penalty_paid,0),
            COALESCE(scf_paid,0)
        FROM payment
        WHERE collection_id = OLD.collection_id;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_payment_id,
            v_billing_id,
            v_water_paid,
            v_tax_paid,
            v_penalty_paid,
            v_scf_paid;

        IF done = 1 THEN
            LEAVE read_loop;
        END IF;

        IF v_billing_id IS NOT NULL THEN

            UPDATE billing b
            SET
                b.remaining_water_charge = ROUND(b.remaining_water_charge + v_water_paid, 2),
                b.remaining_tax_amount = ROUND(b.remaining_tax_amount + v_tax_paid, 2),
                b.remaining_penalty_amount = ROUND(b.remaining_penalty_amount + v_penalty_paid, 2),
                b.remaining_scf_amount = ROUND(b.remaining_scf_amount + v_scf_paid, 2),
                b.remaining_balance = ROUND(
                    b.remaining_water_charge +
                    b.remaining_tax_amount +
                    b.remaining_penalty_amount +
                    b.remaining_scf_amount, 2
                ),
                b.status = CASE
                    WHEN (
                        b.remaining_water_charge +
                        b.remaining_tax_amount +
                        b.remaining_penalty_amount +
                        b.remaining_scf_amount
                    ) = 0 THEN 'paid'
                    ELSE 'unpaid'
                END,
                b.updated_at = NOW()
            WHERE b.billing_id = v_billing_id;

        END IF;

        -- delete payment AFTER reversing
        DELETE FROM payment WHERE payment_id = v_payment_id;

    END LOOP;

    CLOSE cur;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `collection_print_settings`
--

DROP TABLE IF EXISTS `collection_print_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `collection_print_settings` (
  `id` int NOT NULL AUTO_INCREMENT,
  `field_name` varchar(100) DEFAULT NULL,
  `x_position` int DEFAULT NULL,
  `y_position` int DEFAULT NULL,
  `test_print` varchar(150) DEFAULT NULL,
  `note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `field_name_UNIQUE` (`field_name`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collection_print_settings`
--

LOCK TABLES `collection_print_settings` WRITE;
/*!40000 ALTER TABLE `collection_print_settings` DISABLE KEYS */;
/*!40000 ALTER TABLE `collection_print_settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `concessionaire`
--

DROP TABLE IF EXISTS `concessionaire`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `concessionaire` (
  `concessionaire_id` int NOT NULL AUTO_INCREMENT,
  `concessionaire_code` varchar(45) DEFAULT NULL,
  `concessionaire_name` varchar(100) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `zone_id` int DEFAULT NULL,
  `service_id` int DEFAULT NULL,
  `meter_no` varchar(50) DEFAULT NULL,
  `first_reading_date` date DEFAULT NULL,
  `is_tax_exempt` tinyint(1) DEFAULT '0',
  `is_due_exempt` tinyint(1) DEFAULT '0',
  `is_discounted` tinyint(1) DEFAULT '0',
  `is_not_billable` tinyint(1) DEFAULT '0',
  `status` varchar(45) DEFAULT 'PENDING',
  `tin_number` varchar(45) DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`concessionaire_id`),
  KEY `zone_id` (`zone_id`),
  KEY `service_id` (`service_id`),
  KEY `idx_concessionaire_concessionaire_name` (`concessionaire_name`),
  KEY `idx_concessionaire_concessionaire_code` (`concessionaire_code`),
  KEY `idx_concessionaire_zone_id` (`zone_id`),
  CONSTRAINT `concessionaire_fk_service` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`),
  CONSTRAINT `concessionaire_fk_zone` FOREIGN KEY (`zone_id`) REFERENCES `zone` (`zone_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1807 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `concessionaire`
--

LOCK TABLES `concessionaire` WRITE;
/*!40000 ALTER TABLE `concessionaire` DISABLE KEYS */;
INSERT INTO `concessionaire` VALUES (1805,NULL,'Nbnbfgg',NULL,1,1,NULL,'2026-06-03',0,0,0,0,'ACTIVE',NULL,3),(1806,NULL,'Kuku',NULL,1,1,NULL,'2026-06-03',0,0,1,0,'ACTIVE',NULL,3);
/*!40000 ALTER TABLE `concessionaire` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_concessionaire_after_insert` AFTER INSERT ON `concessionaire` FOR EACH ROW BEGIN
    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        NEW.user_id,
        'CREATE',
        'CONCESSIONAIRE',
        'concessionaire',
        NEW.concessionaire_code,
        CONCAT(
            'Created concessionaire. ',
            'Account No: ', COALESCE(NEW.concessionaire_code, 'N/A'),
            ', Name: ', COALESCE(NEW.concessionaire_name, 'N/A'),
            ', Meter No: ', COALESCE(NEW.meter_no, 'N/A'),
            ', Status: ', COALESCE(NEW.status, 'N/A')
        )
    );
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_concessionaire_after_update` AFTER UPDATE ON `concessionaire` FOR EACH ROW BEGIN
    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        NEW.user_id,
        'UPDATE',
        'CONCESSIONAIRE',
        'concessionaire',
        NEW.concessionaire_code,
        CONCAT(
            'Updated concessionaire. ',
            'Account No: ', COALESCE(NEW.concessionaire_code, 'N/A'),
            ', Name: ', COALESCE(NEW.concessionaire_name, 'N/A')
        )
    );
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_concessionaire_before_delete` BEFORE DELETE ON `concessionaire` FOR EACH ROW BEGIN
    -- 1. log the deletion first
    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        OLD.user_id,
        'DELETE',
        'CONCESSIONAIRE',
        'concessionaire',
        OLD.concessionaire_code,
        CONCAT(
            'Deleted concessionaire. Code: ',
            COALESCE(OLD.concessionaire_code, 'N/A'),
            ', Name: ',
            COALESCE(OLD.concessionaire_name, 'N/A')
        )
    );

    -- 2. assign user_id to SCF before deletion (audit preservation)
    UPDATE scf_balance
    SET user_id = OLD.user_id
    WHERE concessionaire_id = OLD.concessionaire_id;

    -- 3. delete SCF records
    DELETE FROM scf_balance
    WHERE concessionaire_id = OLD.concessionaire_id;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment` (
  `payment_id` int NOT NULL AUTO_INCREMENT,
  `billing_id` int DEFAULT NULL,
  `amount_paid` decimal(10,2) NOT NULL,
  `balance` decimal(10,2) DEFAULT '0.00',
  `scf_paid` decimal(10,2) DEFAULT NULL,
  `scf_balance` decimal(10,2) DEFAULT NULL,
  `payment_date` date NOT NULL DEFAULT (curdate()),
  `other_payment` decimal(10,2) DEFAULT '0.00',
  `late_penalty` decimal(10,2) DEFAULT '0.00',
  `arrears_penalty` decimal(12,2) NOT NULL DEFAULT '0.00',
  `arrears_tax` decimal(12,2) NOT NULL DEFAULT '0.00',
  `payment_type` enum('cash','bank','check') DEFAULT 'cash',
  `bank_number` varchar(45) DEFAULT NULL,
  `remarks` text,
  `billing_number` varchar(50) DEFAULT NULL,
  `collection_id` int DEFAULT NULL,
  `water_paid` decimal(12,2) DEFAULT '0.00',
  `tax_paid` decimal(12,2) DEFAULT '0.00',
  `arrears_paid` decimal(12,2) DEFAULT '0.00',
  `penalty_paid` decimal(12,2) DEFAULT '0.00',
  `other_paid` decimal(12,2) DEFAULT '0.00',
  `remaining_water_after` decimal(12,2) DEFAULT '0.00',
  `remaining_balance_after` decimal(12,2) DEFAULT '0.00',
  `status` varchar(50) DEFAULT 'POSTED',
  PRIMARY KEY (`payment_id`),
  KEY `billing_id` (`billing_id`),
  KEY `idx_payment_collection_id` (`collection_id`),
  CONSTRAINT `fk_payment_billing` FOREIGN KEY (`billing_id`) REFERENCES `billing` (`billing_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_payment_after_delete` AFTER DELETE ON `payment` FOR EACH ROW BEGIN
    DECLARE v_total_paid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_paid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    -- ===============================
    -- 1. Get total payments still existing for this billing_id
    -- ===============================
    SELECT COALESCE(SUM(amount_paid), 0),
           COALESCE(SUM(scf_paid), 0)
    INTO v_total_paid, v_scf_paid
    FROM payment
    WHERE billing_id = OLD.billing_id;

    -- ===============================
    -- 2. Get billing amounts for comparison
    -- ===============================
    SELECT total_water_bill, scf_amount
    INTO v_total_water_bill, v_scf_amount
    FROM billing
    WHERE billing_id = OLD.billing_id;

    -- ===============================
    -- 3. Determine correct statuses after deletion
    -- ===============================
    IF v_total_paid = 0 THEN
        UPDATE billing
        SET status = 'unpaid'
        WHERE billing_id = OLD.billing_id;
    ELSEIF v_total_paid < v_total_water_bill THEN
        UPDATE billing
        SET status = 'partially_paid'
        WHERE billing_id = OLD.billing_id;
    ELSE
        UPDATE billing
        SET status = 'paid'
        WHERE billing_id = OLD.billing_id;
    END IF;

    IF v_scf_paid = 0 THEN
        UPDATE billing
        SET scf_status = 'unpaid'
        WHERE billing_id = OLD.billing_id;
    ELSEIF v_scf_paid < v_scf_amount THEN
        UPDATE billing
        SET scf_status = 'partially_paid'
        WHERE billing_id = OLD.billing_id;
    ELSE
        UPDATE billing
        SET scf_status = 'paid'
        WHERE billing_id = OLD.billing_id;
    END IF;

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `payor`
--

DROP TABLE IF EXISTS `payor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payor` (
  `payor_id` int NOT NULL,
  `payor_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`payor_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payor`
--

LOCK TABLES `payor` WRITE;
/*!40000 ALTER TABLE `payor` DISABLE KEYS */;
/*!40000 ALTER TABLE `payor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reading`
--

DROP TABLE IF EXISTS `reading`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reading` (
  `reading_id` int NOT NULL AUTO_INCREMENT,
  `concessionaire_id` int NOT NULL,
  `previous_reading` int DEFAULT NULL,
  `present_reading` int NOT NULL,
  `reading_date` date NOT NULL DEFAULT (curdate()),
  PRIMARY KEY (`reading_id`),
  KEY `concessionaire_id` (`concessionaire_id`),
  CONSTRAINT `reading_fk_concessionaire` FOREIGN KEY (`concessionaire_id`) REFERENCES `concessionaire` (`concessionaire_id`)
) ENGINE=InnoDB AUTO_INCREMENT=17617 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reading`
--

LOCK TABLES `reading` WRITE;
/*!40000 ALTER TABLE `reading` DISABLE KEYS */;
INSERT INTO `reading` VALUES (17614,1805,0,10,'2026-06-03'),(17615,1806,0,10,'2026-06-03'),(17616,1806,10,20,'2026-06-03');
/*!40000 ALTER TABLE `reading` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `scf_balance`
--

DROP TABLE IF EXISTS `scf_balance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scf_balance` (
  `scf_id` int NOT NULL AUTO_INCREMENT,
  `concessionaire_id` int NOT NULL,
  `total_amount` decimal(12,2) DEFAULT '0.00',
  `balance` decimal(12,2) DEFAULT '0.00',
  `monthly` decimal(12,2) DEFAULT '0.00',
  `updated_at` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `last_payment_date` datetime DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`scf_id`),
  UNIQUE KEY `concessionaire_id_UNIQUE` (`concessionaire_id`),
  KEY `concessionaire_id` (`concessionaire_id`),
  CONSTRAINT `scf_balance_fk_concessionaire` FOREIGN KEY (`concessionaire_id`) REFERENCES `concessionaire` (`concessionaire_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scf_balance`
--

LOCK TABLES `scf_balance` WRITE;
/*!40000 ALTER TABLE `scf_balance` DISABLE KEYS */;
INSERT INTO `scf_balance` VALUES (11,1806,2700.00,2700.00,500.00,'2026-06-03 23:30:04',NULL,3);
/*!40000 ALTER TABLE `scf_balance` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_scf_balance_after_insert` AFTER INSERT ON `scf_balance` FOR EACH ROW BEGIN

    DECLARE v_concessionaire_code VARCHAR(50);

    SELECT c.concessionaire_code
    INTO v_concessionaire_code
    FROM concessionaire c
    WHERE c.concessionaire_id = NEW.concessionaire_id
    LIMIT 1;

    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        NEW.user_id,
        'CREATE',
        'SCF',
        'scf_balance',
        NEW.scf_id,
        CONCAT(
            'Created SCF balance record. ',
            'Concessionaire Code: ', COALESCE(v_concessionaire_code, 'N/A'),
            ', Total Amount: ₱', FORMAT(COALESCE(NEW.total_amount, 0), 2),
            ', Balance: ₱', FORMAT(COALESCE(NEW.balance, 0), 2),
            ', Monthly: ₱', FORMAT(COALESCE(NEW.monthly, 0), 2)
        )
    );

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_scf_balance_after_update` AFTER UPDATE ON `scf_balance` FOR EACH ROW BEGIN

    DECLARE v_concessionaire_code VARCHAR(50);

    SELECT c.concessionaire_code
    INTO v_concessionaire_code
    FROM concessionaire c
    WHERE c.concessionaire_id = NEW.concessionaire_id
    LIMIT 1;

    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        NEW.user_id,
        'UPDATE',
        'SCF',
        'scf_balance',
        NEW.scf_id,
        CONCAT(
            'Updated SCF balance. ',
            'Concessionaire Code: ', COALESCE(v_concessionaire_code, 'N/A'),
            ', Old Balance: ₱', FORMAT(COALESCE(OLD.balance, 0), 2),
            ', New Balance: ₱', FORMAT(COALESCE(NEW.balance, 0), 2)
        )
    );

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_scf_balance_after_delete` AFTER DELETE ON `scf_balance` FOR EACH ROW BEGIN
    DECLARE v_concessionaire_code VARCHAR(50);

    -- Get concessionaire code before it disappears
    SELECT c.concessionaire_code
    INTO v_concessionaire_code
    FROM concessionaire c
    WHERE c.concessionaire_id = OLD.concessionaire_id
    LIMIT 1;

    INSERT INTO user_logs (
        user_id,
        action_type,
        module,
        entity_name,
        entity_id,
        description
    )
    VALUES (
        OLD.user_id,
        'DELETE',
        'SCF',
        'scf_balance',
        OLD.scf_id,
        CONCAT(
            'Deleted SCF balance record. ',
            'Concessionaire: ', COALESCE(v_concessionaire_code, 'UNKNOWN'),
            ', Total Amount: ₱', FORMAT(COALESCE(OLD.total_amount, 0), 2),
            ', Balance: ₱', FORMAT(COALESCE(OLD.balance, 0), 2),
            ', Monthly: ₱', FORMAT(COALESCE(OLD.monthly, 0), 2)
        )
    );
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `services`
--

DROP TABLE IF EXISTS `services`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `services` (
  `service_id` int NOT NULL,
  `service_type` varchar(50) NOT NULL,
  `pipe_size` varchar(10) NOT NULL,
  `min_rate` decimal(10,2) NOT NULL,
  `rate_11_20` decimal(10,2) NOT NULL,
  `rate_21_30` decimal(10,2) NOT NULL,
  `rate_31_40` decimal(10,2) NOT NULL,
  `rate_41_above` decimal(10,2) NOT NULL,
  PRIMARY KEY (`service_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `services`
--

LOCK TABLES `services` WRITE;
/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services` VALUES (1,'RESIDENTIAL','1/2',195.00,23.75,27.00,30.25,33.50),(2,'COMMERCIAL FULL','1/2',390.00,47.50,54.00,60.50,67.00),(3,'GOVERNMENT A 1/2','1/2',195.00,23.75,27.00,30.25,33.50),(4,'COMMERCIAL A','1/2',341.25,41.55,47.25,52.90,58.60),(5,'COMMERCIAL B','1/2',292.50,35.60,40.50,45.35,50.25),(6,'COMMERCIAL C','1/2',243.75,29.65,33.75,37.80,41.85),(7,'COMMERCIAL A 3/4','3/4',546.00,41.55,47.25,52.90,58.60),(8,'COMMERCIAL C 3/4','3/4',390.00,29.65,33.75,37.80,41.85),(9,'COMMERCIAL B 3/4','3/4',468.00,35.60,40.50,45.35,50.25),(10,'RESIDENTIAL 3/4','3/4',312.00,23.75,27.00,30.25,33.50),(11,'RESIDENTIAL ST','1/2',150.00,18.13,20.60,23.08,25.55),(12,'COMMERCIAL FULL ST','1/2',300.00,36.26,41.21,46.15,51.10);
/*!40000 ALTER TABLE `services` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `system_settings`
--

DROP TABLE IF EXISTS `system_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `system_settings` (
  `settings_id` int NOT NULL AUTO_INCREMENT,
  `settings_key` varchar(50) NOT NULL,
  `settings_value` varchar(50) NOT NULL,
  PRIMARY KEY (`settings_id`),
  UNIQUE KEY `settings_key_UNIQUE` (`settings_key`)
) ENGINE=InnoDB AUTO_INCREMENT=255 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_settings`
--

LOCK TABLES `system_settings` WRITE;
/*!40000 ALTER TABLE `system_settings` DISABLE KEYS */;
INSERT INTO `system_settings` VALUES (229,'discount_percent','7'),(230,'discount_thresh_hold','30'),(231,'penalize_after_days','14'),(232,'penalty_percent','10'),(233,'tax_percent','2');
/*!40000 ALTER TABLE `system_settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_logs`
--

DROP TABLE IF EXISTS `user_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_logs` (
  `log_id` bigint NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `action_type` varchar(50) NOT NULL,
  `module` varchar(50) NOT NULL,
  `entity_name` varchar(100) NOT NULL,
  `entity_id` varchar(100) DEFAULT NULL,
  `description` text NOT NULL,
  `old_value` json DEFAULT NULL,
  `new_value` json DEFAULT NULL,
  `ip_address` varchar(45) DEFAULT NULL,
  `user_agent` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`log_id`),
  KEY `idx_user_logs_user_id` (`user_id`),
  KEY `idx_user_logs_module` (`module`),
  KEY `idx_user_logs_action_type` (`action_type`),
  KEY `idx_user_logs_entity` (`entity_name`,`entity_id`),
  KEY `idx_user_logs_created_at` (`created_at`),
  CONSTRAINT `fk_user_logs_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=180 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_logs`
--

LOCK TABLES `user_logs` WRITE;
/*!40000 ALTER TABLE `user_logs` DISABLE KEYS */;
INSERT INTO `user_logs` VALUES (3,3,'UPDATE','SYSTEM_SETTINGS','system_settings','global','Updated 5 system setting value(s).',NULL,NULL,NULL,NULL,'2026-06-03 22:09:48'),(6,3,'CREATE','CONCESSIONAIRE','concessionaire',NULL,'Created concessionaire. Account No: N/A, Name: Nbnb, Meter No: N/A, Status: ACTIVE',NULL,NULL,NULL,NULL,'2026-06-03 22:13:38'),(7,3,'CREATE','SCF','scf_balance','1','Created SCF balance record. Concessionaire Code: N/A, Total Amount: ₱0.00, Balance: ₱0.00, Monthly: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 22:13:38'),(8,3,'UPDATE','CONCESSIONAIRE','concessionaire',NULL,'Updated concessionaire. Account No: N/A, Name: Nbnbfgg',NULL,NULL,NULL,NULL,'2026-06-03 22:13:45'),(9,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:13:45'),(10,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:13:57'),(11,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱195.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:13:57'),(12,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:14:03'),(13,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:14:08'),(14,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱195.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:14:08'),(15,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:14:51'),(16,3,'CREATE','CONCESSIONAIRE','concessionaire',NULL,'Created concessionaire. Account No: N/A, Name: Kuku, Meter No: N/A, Status: ACTIVE',NULL,NULL,NULL,NULL,'2026-06-03 22:15:08'),(17,3,'CREATE','SCF','scf_balance','4','Created SCF balance record. Concessionaire Code: N/A, Total Amount: ₱2,700.00, Balance: ₱2,700.00, Monthly: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 22:15:08'),(18,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:15:23'),(19,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱695.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:15:23'),(20,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱305.00. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:16:07'),(21,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:16:25'),(22,3,'COLLECT','SCF','collection','0000002','SCF collection posted. OR#: 0000002, Concessionaire: Kuku, SCF Amount: ₱2,200.00, Others: ₱0.00, Grand Total: ₱2,200.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:16:25'),(23,3,'VOID','COLLECTION','collection','','Collection OR# was voided. 1 payments reversed. Account: . Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:16:32'),(24,3,'VOID','COLLECTION','collection','','Collection OR# was voided. 0 payments reversed. Account: . Reason: jguyfhg.',NULL,NULL,NULL,NULL,'2026-06-03 22:16:54'),(25,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Nbnbfgg. Payment type: Cash. Change: ₱5.00. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:18:28'),(26,3,'VOID','COLLECTION','collection','','Collection OR# was voided. 1 payments reversed. Account: . Reason: .mnljkm.',NULL,NULL,NULL,NULL,'2026-06-03 22:18:34'),(27,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:19:22'),(28,3,'COLLECT','SCF','collection','0000002','SCF collection posted. OR#: 0000002, Concessionaire: Nbnbfgg, SCF Amount: ₱2,700.00, Others: ₱0.00, Grand Total: ₱2,700.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:19:22'),(29,3,'VOID','COLLECTION','collection','','Collection OR# was voided. 0 payments reversed. Account: . Reason: kmkmjk.',NULL,NULL,NULL,NULL,'2026-06-03 22:19:29'),(30,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: dasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:22:11'),(31,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:22:15'),(32,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:22:24'),(33,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱195.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:22:24'),(34,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:22:24'),(35,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱195.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:22:24'),(36,3,'UPDATE','CONCESSIONAIRE','concessionaire',NULL,'Updated concessionaire. Account No: N/A, Name: Kuku',NULL,NULL,NULL,NULL,'2026-06-03 22:22:48'),(37,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:22:48'),(38,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: kk.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:00'),(39,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: \'l;jkjk.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:05'),(40,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:23:13'),(41,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱195.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:13'),(42,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:23:13'),(43,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:13'),(44,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: sdas.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:36'),(45,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:23:40'),(46,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:23:52'),(47,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:24:04'),(48,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱695.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:24:04'),(49,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:24:04'),(50,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:24:04'),(51,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:24:21'),(52,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:26:16'),(53,3,'COLLECT','SCF','collection','0000002','SCF collection posted. OR#: 0000002, Concessionaire: Nbnbfgg, SCF Amount: ₱2,000.00, Others: ₱100.00, Grand Total: ₱2,100.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:26:17'),(54,3,'VOID','COLLECTION','collection','6','VOIDED Collection OR#0000002. Type: SCF-ONLY. Payments restored: 0. SCF restored: ₱2,000.00. Reason: ok. SNAPSHOT: {\"status\": \"POSTED\", \"or_number\": \"0000002\", \"total_scf\": 2000.00, \"total_tax\": 0.00, \"grand_total\": 2100.00, \"payment_ids\": \"\", \"collection_id\": 6, \"total_arrears\": 0.00, \"total_penalty\": 0.00, \"collection_date\": \"2026-06-03\", \"total_current_bill\": 0.00, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Nbnbfgg\", \"total_water_bill_paid\": 0.00}',NULL,NULL,NULL,NULL,'2026-06-03 22:26:26'),(55,3,'CREATE','COLLECTION','collection','0000003','Payment posted using OR #0000003. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Nbnbfgg. Payment type: Cash. Change: ₱305.00. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:26:39'),(56,3,'VOID','COLLECTION','collection','7','VOIDED Collection OR#0000003. Type: WATER. Payments restored: 0. SCF restored: ₱500.00. Reason: 100. SNAPSHOT: {\"status\": \"POSTED\", \"or_number\": \"0000003\", \"total_scf\": 500.00, \"total_tax\": 0.00, \"grand_total\": 695.00, \"payment_ids\": \"4\", \"collection_id\": 7, \"total_arrears\": 0.00, \"total_penalty\": 0.00, \"collection_date\": \"2026-06-03\", \"total_current_bill\": 195.00, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Nbnbfgg\", \"total_water_bill_paid\": 195.00}',NULL,NULL,NULL,NULL,'2026-06-03 22:26:47'),(57,3,'VOID','COLLECTION','collection','5','VOIDED Collection OR#0000001. Type: WATER. Payments restored: 0. SCF restored: ₱0.00. Reason: kqkas. SNAPSHOT: {\"status\": \"POSTED\", \"or_number\": \"0000001\", \"total_scf\": 0.00, \"total_tax\": 3.63, \"grand_total\": 184.98, \"payment_ids\": \"3\", \"collection_id\": 5, \"total_arrears\": 0.00, \"total_penalty\": 0.00, \"collection_date\": \"2026-06-03\", \"total_current_bill\": 181.35, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Kuku\", \"total_water_bill_paid\": 184.98}',NULL,NULL,NULL,NULL,'2026-06-03 22:26:52'),(58,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: sadsdf.',NULL,NULL,NULL,NULL,'2026-06-03 22:28:55'),(59,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:28:59'),(60,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱200.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:29:04'),(61,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱395.00. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:29:04'),(62,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:29:04'),(63,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:29:04'),(64,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:29:20'),(65,3,'VOID','COLLECTION','collection','8','VOIDED OR#0000001 | Type:WATER | Reason:asdasd','{\"or_number\": \"0000001\", \"total_scf\": 0.00, \"grand_total\": 184.98, \"payment_ids\": \"5\", \"bill_numbers\": \"2\", \"collection_id\": 8, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Kuku\", \"total_water_bill_paid\": 184.98}',NULL,NULL,NULL,'2026-06-03 22:33:05'),(66,3,'UPDATE','CONCESSIONAIRE','concessionaire',NULL,'Updated concessionaire. Account No: N/A, Name: Nbnbfgg',NULL,NULL,NULL,NULL,'2026-06-03 22:33:45'),(67,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:33:45'),(68,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: asdasd.',NULL,NULL,NULL,NULL,'2026-06-03 22:33:55'),(69,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: asdsd.',NULL,NULL,NULL,NULL,'2026-06-03 22:33:59'),(70,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:34:13'),(71,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱198.90. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:34:13'),(72,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:34:13'),(73,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:34:13'),(74,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:34:27'),(75,3,'VOID','COLLECTION','collection','9','VOIDED OR#0000001 | Type:WATER | Reason:asdasd','{\"or_number\": \"0000001\", \"total_scf\": 0.00, \"grand_total\": 184.98, \"payment_ids\": \"6\", \"bill_numbers\": \"2\", \"collection_id\": 9, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Kuku\", \"total_water_bill_paid\": 184.98}',NULL,NULL,NULL,'2026-06-03 22:34:32'),(76,3,'CREATE','COLLECTION','collection','0000002','Payment posted using OR #0000002. Total amount received: ₱100.00. Bills processed: 1. Concessionaires: Nbnbfgg. Payment type: Cash. Change: ₱0.00. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:34:56'),(77,3,'VOID','COLLECTION','collection','10','VOIDED OR#0000002 | Type:WATER | Reason:aasDASD','{\"or_number\": \"0000002\", \"total_scf\": 0.00, \"grand_total\": 100.00, \"payment_ids\": \"7\", \"bill_numbers\": \"1\", \"collection_id\": 10, \"concessionaire_code\": \"\", \"concessionaire_name\": \"Nbnbfgg\", \"total_water_bill_paid\": 100.00}',NULL,NULL,NULL,'2026-06-03 22:35:03'),(78,3,'CREATE','COLLECTION','collection','0000003','Payment posted using OR #0000003. Total amount received: ₱100.00. Bills processed: 1. Concessionaires: Nbnbfgg. Payment type: Cash. Change: ₱1.10. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:40:19'),(79,3,'VOID','COLLECTION','collection','11','Collection #11 OR#0000003 voided | Payments:1 | SCF:0.00 | ASDSAD','{\"remarks\": \"\", \"or_number\": \"0000003\", \"total_scf\": 0.00, \"collection_id\": 11, \"concessionaire_code\": \"\"}',NULL,NULL,NULL,'2026-06-03 22:40:23'),(80,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: ASDASD.',NULL,NULL,NULL,NULL,'2026-06-03 22:40:36'),(81,3,'VOID','BILLING','billing','1','Billing record #1 was voided and permanently removed from the system. Reason: ASDASD.',NULL,NULL,NULL,NULL,'2026-06-03 22:40:41'),(82,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:40:55'),(83,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱198.90. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:40:55'),(84,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:40:55'),(85,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:40:55'),(86,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:41:05'),(87,3,'CREATE','COLLECTION','collection','0000002','Payment posted using OR #0000002. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Nbnbfgg. Payment type: Cash. Change: ₱1.10. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:41:17'),(88,3,'VOID','COLLECTION','collection','13','Collection #13 OR#0000002 voided | Payments:1 | SCF:0.00 | ASDASD','{\"remarks\": \"\", \"or_number\": \"0000002\", \"total_scf\": 0.00, \"collection_id\": 13, \"concessionaire_code\": \"\"}',NULL,NULL,NULL,'2026-06-03 22:41:21'),(89,3,'VOID','COLLECTION','collection','12','Collection #12 OR#0000001 voided | Payments:0 | SCF:0.00 | ASASD','{\"remarks\": \"\", \"or_number\": \"0000001\", \"total_scf\": 0.00, \"collection_id\": 12, \"concessionaire_code\": \"\"}',NULL,NULL,NULL,'2026-06-03 22:41:25'),(90,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: ASDASD.',NULL,NULL,NULL,NULL,'2026-06-03 22:42:13'),(91,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:42:21'),(92,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱184.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:42:21'),(93,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:42:33'),(94,3,'VOID','COLLECTION','collection','14','Collection #14 OR#0000001 voided | Payments:1 | SCF:0.00 | ASDASD','{\"remarks\": \"\", \"or_number\": \"0000001\", \"total_scf\": 0.00, \"collection_id\": 14, \"concessionaire_code\": \"\"}',NULL,NULL,NULL,'2026-06-03 22:42:39'),(95,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱200.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱15.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:44:25'),(96,3,'VOID','COLLECTION','collection','15','=== COLLECTION VOID AUDIT === | Collection ID: 15 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 12 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱0.00 | Water Paid: ₱184.98 | Grand Total: ₱184.98 | Amount Received: ₱200.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱184.98 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:44:29 | Reason: SADASD | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 0.00, \"tax\": 3.63, \"grand\": 184.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 3.63, \"total\": 184.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000001\", \"collection_id\": 15, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:44:29'),(97,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:45:56'),(98,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 22:46:15'),(99,3,'COLLECT','SCF','collection','0000001','SCF collection posted. OR#: 0000001, Concessionaire: Kuku, SCF Amount: ₱2,200.00, Others: ₱100.00, Grand Total: ₱2,300.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:46:15'),(100,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱500.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:46:22'),(101,3,'VOID','COLLECTION','collection','16','=== COLLECTION VOID AUDIT === | Collection ID: 16 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected:  (0 bills) | Payment IDs Voided:  | --- ORIGINAL AMOUNTS --- | Current: ₱0.00 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱0.00 | SCF: ₱2,200.00 | Water Paid: ₱0.00 | Grand Total: ₱2,300.00 | Amount Received: ₱2,300.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱0.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱0.00 | SCF Balance Reversed: ₱2,200.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:46:22 | Reason: ASDASD | Original Remarks: NF','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"\", \"totals\": {\"scf\": 2200.00, \"tax\": 0.00, \"grand\": 2300.00, \"arrears\": 0.00, \"current\": 0.00, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 0.00, \"water\": 0.00, \"penalty\": 0.00}, \"or_number\": \"0000001\", \"collection_id\": 16, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:46:22'),(102,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: ASASD.',NULL,NULL,NULL,NULL,'2026-06-03 22:46:35'),(103,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:46:39'),(104,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:46:39'),(105,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱100.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱0.00. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:46:54'),(106,3,'VOID','COLLECTION','collection','17','=== COLLECTION VOID AUDIT === | Collection ID: 17 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 13 | --- ORIGINAL AMOUNTS --- | Current: ₱100.00 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱0.00 | SCF: ₱0.00 | Water Paid: ₱100.00 | Grand Total: ₱100.00 | Amount Received: ₱100.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱100.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱100.00 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:47:04 | Reason: ASASD | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 0.00, \"tax\": 0.00, \"grand\": 100.00, \"arrears\": 0.00, \"current\": 100.00, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 100.00, \"water\": 100.00, \"penalty\": 0.00}, \"or_number\": \"0000001\", \"collection_id\": 17, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:47:04'),(107,3,'CREATE','COLLECTION','collection','0000002','Payment posted using OR #0000002. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:47:16'),(108,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:47:27'),(109,3,'COLLECT','SCF','collection','0000003','SCF collection posted. OR#: 0000003, Concessionaire: Kuku, SCF Amount: ₱2,200.00, Others: ₱0.00, Grand Total: ₱2,200.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:47:27'),(110,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:47:45'),(111,3,'VOID','COLLECTION','collection','19','=== COLLECTION VOID AUDIT === | Collection ID: 19 | OR Number: 0000003 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected:  (0 bills) | Payment IDs Voided:  | --- ORIGINAL AMOUNTS --- | Current: ₱0.00 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱0.00 | SCF: ₱2,200.00 | Water Paid: ₱0.00 | Grand Total: ₱2,200.00 | Amount Received: ₱2,200.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱0.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱0.00 | SCF Balance Reversed: ₱2,200.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:47:45 | Reason: 2200 NI KUKU EBALIK\' | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"\", \"totals\": {\"scf\": 2200.00, \"tax\": 0.00, \"grand\": 2200.00, \"arrears\": 0.00, \"current\": 0.00, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 0.00, \"water\": 0.00, \"penalty\": 0.00}, \"or_number\": \"0000003\", \"collection_id\": 19, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:47:45'),(112,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:48:00'),(113,3,'VOID','COLLECTION','collection','18','=== COLLECTION VOID AUDIT === | Collection ID: 18 | OR Number: 0000002 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (0 bills) | Payment IDs Voided: 14 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱0.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱0.00 | SCF Balance Reversed: ₱500.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:48:00 | Reason: SCF NGA 500 EBALIK | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 0.00, \"water\": 0.00, \"penalty\": 0.00}, \"or_number\": \"0000002\", \"collection_id\": 18, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:48:00'),(114,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: ASDASD.',NULL,NULL,NULL,NULL,'2026-06-03 22:50:46'),(115,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:50:52'),(116,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:50:52'),(117,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:51:06'),(118,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:51:18'),(119,3,'COLLECT','SCF','collection','0000002','SCF collection posted. OR#: 0000002, Concessionaire: Kuku, SCF Amount: ₱2,200.00, Others: ₱0.00, Grand Total: ₱2,200.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:51:18'),(120,3,'VOID','COLLECTION','collection','20','VOIDED COLLECTION | OR#:0000001 | ID:20 | Concessionaire:[] Kuku | Bills:2 | Payments:1 | RESTORED => Water:₱181.35, Tax:₱3.63, Penalty:₱0.00, SCF(to bill):₱500.00, Total Balance:₱684.98 | SCF Balance Reversed:₱0.00 | Reason:BALIK ANG 500 NGA SCF | By:3 at 2026-06-03 22:51:30','{\"or\": \"0000001\", \"bills\": \"2\", \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}}',NULL,NULL,NULL,'2026-06-03 22:51:30'),(121,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:52:43'),(122,3,'VOID','COLLECTION','collection','21','=== COLLECTION VOID AUDIT === | Collection ID: 21 | OR Number: 0000002 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected:  (0 bills) | Payment IDs Voided:  | --- ORIGINAL AMOUNTS --- | Current: ₱0.00 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱0.00 | SCF: ₱2,200.00 | Water Paid: ₱0.00 | Grand Total: ₱2,200.00 | Amount Received: ₱2,200.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱0.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱0.00 | SCF Balance Reversed: ₱2,200.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:52:43 | Reason: ASSAD | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"\", \"totals\": {\"scf\": 2200.00, \"tax\": 0.00, \"grand\": 2200.00, \"arrears\": 0.00, \"current\": 0.00, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 0.00, \"water\": 0.00, \"penalty\": 0.00}, \"or_number\": \"0000002\", \"collection_id\": 21, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:52:43'),(123,3,'CREATE','COLLECTION','collection','0000003','Payment posted using OR #0000003. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:52:51'),(124,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:53:02'),(125,3,'VOID','COLLECTION','collection','22','=== COLLECTION VOID AUDIT === | Collection ID: 22 | OR Number: 0000003 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 16 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱500.00 | Total Balance Restored: ₱684.98 | SCF Balance Reversed: ₱500.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:53:02 | Reason: BALIK ANG BILLING AND SCF | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000003\", \"collection_id\": 22, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:53:02'),(126,3,'CREATE','COLLECTION','collection','0000004','Payment posted using OR #0000004. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:53:25'),(127,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱3,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:53:38'),(128,3,'VOID','COLLECTION','collection','23','=== COLLECTION VOID AUDIT === | Collection ID: 23 | OR Number: 0000004 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 17 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱500.00 | Total Balance Restored: ₱684.98 | SCF Balance Reversed: ₱500.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:53:38 | Reason: ASDAD | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000004\", \"collection_id\": 23, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:53:38'),(129,3,'CREATE','COLLECTION','collection','0000005','Payment posted using OR #0000005. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:54:19'),(130,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱3,200.00, New Balance: ₱3,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:55:00'),(131,3,'VOID','COLLECTION','collection','24','=== COLLECTION VOID AUDIT === | Collection ID: 24 | OR Number: 0000005 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 18 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱500.00 | Total Balance Restored: ₱684.98 | SCF Balance Reversed: ₱500.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:55:00 | Reason: ASASD | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000005\", \"collection_id\": 24, \"concessionaire\": \"Kuku\"}',NULL,NULL,NULL,'2026-06-03 22:55:00'),(132,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: ADADAS.',NULL,NULL,NULL,NULL,'2026-06-03 22:55:10'),(133,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱3,700.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 22:55:16'),(134,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:55:30'),(135,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 22:55:30'),(136,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:57:49'),(137,3,'VOID','COLLECTION','collection','25','=== COLLECTION VOID AUDIT === | Collection ID: 25 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 19 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱500.00 | Total Balance Restored: ₱684.98 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:58:02 | Reason: ND MAG DUGANG 500 SA SCF | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000001\", \"collection_id\": 25, \"concessionaire\": \"Kuku\", \"scf_balance_reversed\": 0}',NULL,NULL,NULL,'2026-06-03 22:58:02'),(138,3,'CREATE','COLLECTION','collection','0000002','Payment posted using OR #0000002. Total amount received: ₱1,000.00. Bills processed: 1. Concessionaires: Kuku. Payment type: Cash. Change: ₱315.02. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 22:58:12'),(139,3,'VOID','COLLECTION','collection','26','=== COLLECTION VOID AUDIT === | Collection ID: 26 | OR Number: 0000002 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2 (1 bills) | Payment IDs Voided: 20 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱3.63 | SCF: ₱500.00 | Water Paid: ₱184.98 | Grand Total: ₱684.98 | Amount Received: ₱1,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱181.35 | Tax Restored: ₱3.63 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱500.00 | Total Balance Restored: ₱684.98 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:58:21 | Reason: ND NA MAG DUGANG | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2\", \"totals\": {\"scf\": 500.00, \"tax\": 3.63, \"grand\": 684.98, \"arrears\": 0.00, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 500.00, \"tax\": 3.63, \"total\": 684.98, \"water\": 181.35, \"penalty\": 0.00}, \"or_number\": \"0000002\", \"collection_id\": 26, \"concessionaire\": \"Kuku\", \"scf_balance_reversed\": 0}',NULL,NULL,NULL,'2026-06-03 22:58:21'),(140,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 22:58:39'),(141,3,'COLLECT','SCF','collection','0099212','SCF collection posted. OR#: 0099212, Concessionaire: Kuku, SCF Amount: ₱2,200.00, Others: ₱0.00, Grand Total: ₱2,200.00.',NULL,NULL,NULL,NULL,'2026-06-03 22:58:39'),(142,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 22:58:50'),(143,3,'VOID','COLLECTION','collection','27','=== COLLECTION VOID AUDIT === | Collection ID: 27 | OR Number: 0099212 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected:  (0 bills) | Payment IDs Voided:  | --- ORIGINAL AMOUNTS --- | Current: ₱0.00 | Arrears: ₱0.00 | Penalty: ₱0.00 | Tax: ₱0.00 | SCF: ₱2,200.00 | Water Paid: ₱0.00 | Grand Total: ₱2,200.00 | Amount Received: ₱2,200.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱0.00 | Tax Restored: ₱0.00 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱0.00 | Total Balance Restored: ₱0.00 | SCF Balance Reversed: ₱2,200.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 22:58:50 | Reason: EBALIK AND 2200 SA SCF | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"\", \"totals\": {\"scf\": 2200.00, \"tax\": 0.00, \"grand\": 2200.00, \"arrears\": 0.00, \"current\": 0.00, \"penalty\": 0.00}, \"restored\": {\"scf\": 0.00, \"tax\": 0.00, \"total\": 0.00, \"water\": 0.00, \"penalty\": 0.00}, \"or_number\": \"0099212\", \"collection_id\": 27, \"concessionaire\": \"Kuku\", \"scf_balance_reversed\": 2200.00}',NULL,NULL,NULL,'2026-06-03 22:58:50'),(144,3,'VOID','BILLING','billing','2','Billing record #2 was voided and permanently removed from the system. Reason: OKII.',NULL,NULL,NULL,NULL,'2026-06-03 23:00:32'),(145,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱1,700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:03:46'),(146,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:03:46'),(147,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,700.00, New Balance: ₱1,200.00',NULL,NULL,NULL,NULL,'2026-06-03 23:05:01'),(148,3,'CREATE','BILLING','billing','3','New billing record #3 created successfully. Total amount: ₱869.96. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:05:01'),(149,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,200.00, New Balance: ₱2,700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:07:56'),(150,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 23:08:06'),(151,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱198.90. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:08:06'),(152,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 23:08:06'),(153,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:08:06'),(154,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱1,700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:08:58'),(155,3,'CREATE','BILLING','billing','3','New billing record #3 created successfully. Total amount: ₱869.96. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:08:58'),(156,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 23:14:45'),(157,3,'VOID','BILLING','billing','3','BILL VOIDED | #3 | Date:2026-06-03 | Consumption:10 | Water:₱181.35 | Tax:₱3.63 | SCF Monthly Restored:₱500.00 (Monthly rate was ₱500.00) | Total:₱869.96 | Reason:asdjasd','{\"bill_number\": \"3\", \"void_reason\": \"asdjasd\", \"total_amount\": 869.96, \"scf_monthly_rate\": 500.00, \"concessionaire_id\": 1806, \"scf_amount_billed\": 500.00}',NULL,NULL,NULL,'2026-06-03 23:14:45'),(158,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱1,700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:14:53'),(159,3,'CREATE','BILLING','billing','3','New billing record #3 created successfully. Total amount: ₱869.96. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:14:53'),(160,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱2,000.00. Bills processed: 2. Concessionaires: Kuku. Payment type: Cash. Change: ₱630.04. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 23:16:38'),(161,3,'VOID','COLLECTION','collection','28','=== COLLECTION VOID AUDIT === | Collection ID: 28 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2, 3 (2 bills) | Payment IDs Voided: 21, 22 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱181.35 | Penalty: ₱0.00 | Tax: ₱7.26 | SCF: ₱1,000.00 | Water Paid: ₱369.96 | Grand Total: ₱1,369.96 | Amount Received: ₱2,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱362.70 | Tax Restored: ₱7.26 | Penalty Restored: ₱0.00 | SCF Restored to Bills: ₱1,000.00 | Total Balance Restored: ₱1,369.96 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 23:17:44 | Reason: lknlknlk | Original Remarks: ','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2, 3\", \"totals\": {\"scf\": 1000.00, \"tax\": 7.26, \"grand\": 1369.96, \"arrears\": 181.35, \"current\": 181.35, \"penalty\": 0.00}, \"restored\": {\"scf\": 1000.00, \"tax\": 7.26, \"total\": 1369.96, \"water\": 362.70, \"penalty\": 0.00}, \"or_number\": \"0000001\", \"collection_id\": 28, \"concessionaire\": \"Kuku\", \"scf_balance_reversed\": 0}',NULL,NULL,NULL,'2026-06-03 23:17:44'),(162,3,'CREATE','COLLECTION','collection','0000002','Payment posted using OR #0000002. Total amount received: ₱2,000.00. Bills processed: 2. Concessionaires: Kuku. Payment type: Cash. Change: ₱630.04. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 23:19:13'),(163,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,700.00, New Balance: ₱2,200.00',NULL,NULL,NULL,NULL,'2026-06-03 23:22:32'),(164,3,'VOID','BILLING','billing','3','BILL VOIDED | #3 | Date:2026-06-03 | Consumption:10 | Water:₱181.35 | Tax:₱3.63 | SCF Monthly Restored:₱500.00 (Monthly rate was ₱500.00) | Total:₱869.96 | Reason:asdasd','{\"bill_number\": \"3\", \"void_reason\": \"asdasd\", \"total_amount\": 869.96, \"scf_monthly_rate\": 500.00, \"concessionaire_id\": 1806, \"scf_amount_billed\": 500.00}',NULL,NULL,NULL,'2026-06-03 23:22:32'),(165,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱2,200.00, New Balance: ₱1,700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:22:37'),(166,3,'CREATE','BILLING','billing','3','New billing record #3 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:22:37'),(167,3,'UPDATE','SCF','scf_balance','1','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱0.00, New Balance: ₱0.00',NULL,NULL,NULL,NULL,'2026-06-03 23:26:28'),(168,3,'CREATE','BILLING','billing','1','New billing record #1 created successfully. Total amount: ₱198.90. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:26:28'),(169,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,700.00, New Balance: ₱1,200.00',NULL,NULL,NULL,NULL,'2026-06-03 23:26:28'),(170,3,'CREATE','BILLING','billing','2','New billing record #2 created successfully. Total amount: ₱684.98. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:26:28'),(171,3,'UPDATE','SCF','scf_balance','4','Updated SCF balance. Concessionaire Code: N/A, Old Balance: ₱1,200.00, New Balance: ₱700.00',NULL,NULL,NULL,NULL,'2026-06-03 23:26:53'),(172,3,'CREATE','BILLING','billing','3','New billing record #3 created successfully. Total amount: ₱869.96. Consumption: 10 cu.m. Due date: 2026-06-17.',NULL,NULL,NULL,NULL,'2026-06-03 23:26:53'),(173,3,'CREATE','COLLECTION','collection','0000001','Payment posted using OR #0000001. Total amount received: ₱2,000.00. Bills processed: 2. Concessionaires: Kuku. Payment type: Cash. Change: ₱611.90. Status: POSTED.',NULL,NULL,NULL,NULL,'2026-06-03 23:27:13'),(176,3,'VOID','COLLECTION','collection','30','=== COLLECTION VOID AUDIT === | Collection ID: 30 | OR Number: 0000001 | Collection Date: 2026-06-03 00:00 | Concessionaire: [] Kuku | Bills Affected: 2, 3 (2 bills) | Payment IDs Voided: 25, 26 | --- ORIGINAL AMOUNTS --- | Current: ₱181.35 | Arrears: ₱181.35 | Penalty: ₱18.14 | Tax: ₱7.26 | SCF: ₱1,000.00 | Water Paid: ₱388.10 | Grand Total: ₱1,488.10 | Amount Received: ₱2,000.00 | --- AMOUNTS RESTORED TO BILLING --- | Water Restored: ₱362.70 | Tax Restored: ₱7.26 | Penalty Restored: ₱18.14 | SCF Restored to Bills: ₱1,000.00 | Total Balance Restored: ₱1,388.10 | SCF Balance Reversed: ₱0.00 | --- VOID DETAILS --- | Voided By User ID: 3 | Void Date: 2026-06-03 23:28:11 | Reason: asdasd | Original Remarks: nf','{\"date\": \"2026-06-03 00:00:00.000000\", \"bills\": \"2, 3\", \"totals\": {\"scf\": 1000.00, \"tax\": 7.26, \"grand\": 1488.10, \"arrears\": 181.35, \"current\": 181.35, \"penalty\": 18.14}, \"restored\": {\"scf\": 1000.00, \"tax\": 7.26, \"total\": 1388.10, \"water\": 362.70, \"penalty\": 18.14}, \"or_number\": \"0000001\", \"collection_id\": 30, \"concessionaire\": \"Kuku\", \"scf_balance_reversed\": 0}',NULL,NULL,NULL,'2026-06-03 23:28:11'),(177,3,'DELETE','SCF','scf_balance','1','Deleted SCF balance record. Concessionaire: UNKNOWN, Total Amount: ₱2,700.00, Balance: ₱0.00, Monthly: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 23:29:50'),(178,3,'DELETE','SCF','scf_balance','4','Deleted SCF balance record. Concessionaire: UNKNOWN, Total Amount: ₱2,700.00, Balance: ₱700.00, Monthly: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 23:29:50'),(179,3,'CREATE','SCF','scf_balance','11','Created SCF balance record. Concessionaire Code: N/A, Total Amount: ₱2,700.00, Balance: ₱2,700.00, Monthly: ₱500.00',NULL,NULL,NULL,NULL,'2026-06-03 23:30:04');
/*!40000 ALTER TABLE `user_logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `role` enum('ADMIN','BILLER','CASHIER','NULL') DEFAULT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`),
  KEY `idx_users_full_name` (`full_name`),
  KEY `idx_users_role` (`role`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'1','1','System Administrator','BILLER',1,'2026-01-25 01:25:54'),(2,'2','2','STAFF','CASHIER',1,'2026-01-25 01:25:54'),(3,'3','3','Lordz Esperida','ADMIN',1,'2026-01-16 14:51:41');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `v_aging_of_accounts`
--

DROP TABLE IF EXISTS `v_aging_of_accounts`;
/*!50001 DROP VIEW IF EXISTS `v_aging_of_accounts`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_aging_of_accounts` AS SELECT 
 1 AS `Billing_ID`,
 1 AS `Bill_No.`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Billing_Date`,
 1 AS `Due_Date`,
 1 AS `Status`,
 1 AS `Days_Overdue`,
 1 AS `Current_Due`,
 1 AS `1–30_Days`,
 1 AS `31–60 Days`,
 1 AS `61–90 Days`,
 1 AS `Over_90_Days`,
 1 AS `Total_Amount`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_aging_of_accounts_summary`
--

DROP TABLE IF EXISTS `v_aging_of_accounts_summary`;
/*!50001 DROP VIEW IF EXISTS `v_aging_of_accounts_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_aging_of_accounts_summary` AS SELECT 
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Current`,
 1 AS `1-30_Days`,
 1 AS `31-60_Days`,
 1 AS `61-90_Days`,
 1 AS `91-120_Days`,
 1 AS `Over_120_Days`,
 1 AS `Total`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_aging_of_scf_summary`
--

DROP TABLE IF EXISTS `v_aging_of_scf_summary`;
/*!50001 DROP VIEW IF EXISTS `v_aging_of_scf_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_aging_of_scf_summary` AS SELECT 
 1 AS `Concessionaire_ID`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Current`,
 1 AS `1_30_Days`,
 1 AS `31_60_Days`,
 1 AS `61_90_Days`,
 1 AS `Over_90_Days`,
 1 AS `Total`,
 1 AS `Max_Total_Allowed`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_concessionaire`
--

DROP TABLE IF EXISTS `v_billing_concessionaire`;
/*!50001 DROP VIEW IF EXISTS `v_billing_concessionaire`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_concessionaire` AS SELECT 
 1 AS `billing_id`,
 1 AS `bill_number`,
 1 AS `billing_date`,
 1 AS `concessionaire_id`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_dashboard`
--

DROP TABLE IF EXISTS `v_billing_dashboard`;
/*!50001 DROP VIEW IF EXISTS `v_billing_dashboard`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_dashboard` AS SELECT 
 1 AS `BillingID`,
 1 AS `BillNumber`,
 1 AS `ConcessionaireID`,
 1 AS `ConcessionaireName`,
 1 AS `ConcessionaireCode`,
 1 AS `ZoneID`,
 1 AS `ZoneName`,
 1 AS `ServiceID`,
 1 AS `ServiceType`,
 1 AS `ReadingID`,
 1 AS `ReadingDate`,
 1 AS `BillingDate`,
 1 AS `DueDate`,
 1 AS `CubicMeter`,
 1 AS `FreeWater`,
 1 AS `WaterCharge`,
 1 AS `DiscountAmount`,
 1 AS `TaxAmount`,
 1 AS `TotalWaterBill`,
 1 AS `SCFAmount`,
 1 AS `ArrearsAmount`,
 1 AS `PenaltyAmount`,
 1 AS `TotalAmount`,
 1 AS `BillingStatus`,
 1 AS `SCFStatus`,
 1 AS `IsInitial`,
 1 AS `UpdatedAt`,
 1 AS `LastPaymentDate`,
 1 AS `PaidAmount`,
 1 AS `UnpaidAmount`,
 1 AS `OverdueAmount`,
 1 AS `NetAmount`,
 1 AS `BillingYear`,
 1 AS `BillingMonth`,
 1 AS `BillingMonthLabel`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_details`
--

DROP TABLE IF EXISTS `v_billing_details`;
/*!50001 DROP VIEW IF EXISTS `v_billing_details`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_details` AS SELECT 
 1 AS `reading_id`,
 1 AS `concessionaire_id`,
 1 AS `billing_id`,
 1 AS `bill_number`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `is_tax_exempt`,
 1 AS `is_due_exempt`,
 1 AS `is_discounted`,
 1 AS `previous_reading`,
 1 AS `present_reading`,
 1 AS `current_reading_date`,
 1 AS `previous_reading_date`,
 1 AS `billing_date`,
 1 AS `due_date`,
 1 AS `consumption`,
 1 AS `free_water`,
 1 AS `water_charge`,
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `total_water_bill`,
 1 AS `scf_amount`,
 1 AS `arrears_amount`,
 1 AS `penalty_amount`,
 1 AS `total_amount`,
 1 AS `remaining_water_charge`,
 1 AS `remaining_tax_amount`,
 1 AS `remaining_penalty_amount`,
 1 AS `remaining_scf_amount`,
 1 AS `remaining_balance`,
 1 AS `status`,
 1 AS `scf_status`,
 1 AS `last_payment_date`,
 1 AS `payment_count`,
 1 AS `last_collection_id`,
 1 AS `paid_at`,
 1 AS `is_initial`,
 1 AS `is_penalty_applied`,
 1 AS `penalty_applied_at`,
 1 AS `tax_percent_used`,
 1 AS `discount_percent_used`,
 1 AS `penalty_percent_used`,
 1 AS `scf_monthly_used`,
 1 AS `scf_total_cap_used`,
 1 AS `created_at`,
 1 AS `updated_at`,
 1 AS `created_by_user_id`,
 1 AS `updated_by_user_id`,
 1 AS `created_by_name`,
 1 AS `updated_by_name`,
 1 AS `request_id`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_info`
--

DROP TABLE IF EXISTS `v_billing_info`;
/*!50001 DROP VIEW IF EXISTS `v_billing_info`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_info` AS SELECT 
 1 AS `billing_id`,
 1 AS `bill_number`,
 1 AS `concessionaire_id`,
 1 AS `reading_id`,
 1 AS `billing_date`,
 1 AS `due_date`,
 1 AS `consumption`,
 1 AS `free_water`,
 1 AS `water_charge`,
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `total_water_bill`,
 1 AS `scf_amount`,
 1 AS `arrears_amount`,
 1 AS `penalty_amount`,
 1 AS `total_amount`,
 1 AS `remaining_water_charge`,
 1 AS `remaining_tax_amount`,
 1 AS `remaining_penalty_amount`,
 1 AS `remaining_scf_amount`,
 1 AS `remaining_balance`,
 1 AS `status`,
 1 AS `scf_status`,
 1 AS `updated_at`,
 1 AS `last_payment_date`,
 1 AS `is_initial`,
 1 AS `payment_count`,
 1 AS `last_collection_id`,
 1 AS `created_by_user_id`,
 1 AS `updated_by_user_id`,
 1 AS `tax_percent_used`,
 1 AS `discount_percent_used`,
 1 AS `penalty_percent_used`,
 1 AS `is_penalty_applied`,
 1 AS `penalty_applied_at`,
 1 AS `request_id`,
 1 AS `created_at`,
 1 AS `scf_monthly_used`,
 1 AS `scf_total_cap_used`,
 1 AS `paid_at`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `zone_id`,
 1 AS `service_id`,
 1 AS `meter_no`,
 1 AS `first_reading_date`,
 1 AS `is_tax_exempt`,
 1 AS `is_due_exempt`,
 1 AS `is_discounted`,
 1 AS `concessionaire_status`,
 1 AS `tin_number`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_report`
--

DROP TABLE IF EXISTS `v_billing_report`;
/*!50001 DROP VIEW IF EXISTS `v_billing_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_report` AS SELECT 
 1 AS `Zone`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Invoice_Number`,
 1 AS `Cu_m³`,
 1 AS `Un_m³`,
 1 AS `Water_Bill`,
 1 AS `Arrears`,
 1 AS `Tax`,
 1 AS `Discount`,
 1 AS `Total_Water_Bill`,
 1 AS `SCF`,
 1 AS `Total_Amount_Billed`,
 1 AS `Initial`,
 1 AS `Date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_report_summary_total`
--

DROP TABLE IF EXISTS `v_billing_report_summary_total`;
/*!50001 DROP VIEW IF EXISTS `v_billing_report_summary_total`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_report_summary_total` AS SELECT 
 1 AS `Total_Accounts`,
 1 AS `Total_Cu_m3`,
 1 AS `Total_Un_m3`,
 1 AS `Total_Water_CHARGE`,
 1 AS `Total_Arrears`,
 1 AS `Total_Tax`,
 1 AS `Total_Discount`,
 1 AS `Total_Water_Bill_Final`,
 1 AS `Total_SCF`,
 1 AS `Grand_Total`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_status`
--

DROP TABLE IF EXISTS `v_billing_status`;
/*!50001 DROP VIEW IF EXISTS `v_billing_status`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_status` AS SELECT 
 1 AS `billing_month`,
 1 AS `total_bills`,
 1 AS `paid_count`,
 1 AS `partially_paid_count`,
 1 AS `overdue_count`,
 1 AS `unpaid_count`,
 1 AS `paid_percent`,
 1 AS `partially_paid_percent`,
 1 AS `overdue_percent`,
 1 AS `unpaid_percent`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_summary_jan_2026`
--

DROP TABLE IF EXISTS `v_billing_summary_jan_2026`;
/*!50001 DROP VIEW IF EXISTS `v_billing_summary_jan_2026`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_summary_jan_2026` AS SELECT 
 1 AS `Total_Accounts`,
 1 AS `Total_Cu_m3`,
 1 AS `Total_Un_m3`,
 1 AS `Total_Water_Bill`,
 1 AS `Total_Arrears`,
 1 AS `Total_Tax`,
 1 AS `Total_Discount`,
 1 AS `Total_Water_Bill_Final`,
 1 AS `Total_SCF`,
 1 AS `Grand_Total`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_billing_with_users_full`
--

DROP TABLE IF EXISTS `v_billing_with_users_full`;
/*!50001 DROP VIEW IF EXISTS `v_billing_with_users_full`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_billing_with_users_full` AS SELECT 
 1 AS `billing_id`,
 1 AS `bill_number`,
 1 AS `concessionaire_id`,
 1 AS `reading_id`,
 1 AS `billing_date`,
 1 AS `due_date`,
 1 AS `consumption`,
 1 AS `free_water`,
 1 AS `water_charge`,
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `total_water_bill`,
 1 AS `scf_amount`,
 1 AS `arrears_amount`,
 1 AS `penalty_amount`,
 1 AS `total_amount`,
 1 AS `remaining_water_charge`,
 1 AS `remaining_tax_amount`,
 1 AS `remaining_penalty_amount`,
 1 AS `remaining_scf_amount`,
 1 AS `remaining_balance`,
 1 AS `status`,
 1 AS `scf_status`,
 1 AS `updated_at`,
 1 AS `last_payment_date`,
 1 AS `is_initial`,
 1 AS `payment_count`,
 1 AS `last_collection_id`,
 1 AS `created_by_user_id`,
 1 AS `updated_by_user_id`,
 1 AS `tax_percent_used`,
 1 AS `discount_percent_used`,
 1 AS `penalty_percent_used`,
 1 AS `is_penalty_applied`,
 1 AS `penalty_applied_at`,
 1 AS `request_id`,
 1 AS `created_at`,
 1 AS `scf_monthly_used`,
 1 AS `scf_total_cap_used`,
 1 AS `paid_at`,
 1 AS `created_by_username`,
 1 AS `created_by_full_name`,
 1 AS `created_by_role`,
 1 AS `updated_by_username`,
 1 AS `updated_by_full_name`,
 1 AS `updated_by_role`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_collection_report`
--

DROP TABLE IF EXISTS `v_collection_report`;
/*!50001 DROP VIEW IF EXISTS `v_collection_report`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_collection_report` AS SELECT 
 1 AS `Collection_ID`,
 1 AS `OR_Number`,
 1 AS `Date`,
 1 AS `Invoice_Number`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Water_Charge`,
 1 AS `Arrears`,
 1 AS `Penalty`,
 1 AS `Tax`,
 1 AS `SCF`,
 1 AS `Total_Water_Bill_Paid`,
 1 AS `SCF_Paid`,
 1 AS `Others`,
 1 AS `Grand_Total`,
 1 AS `Amount_Received`,
 1 AS `Change_Amount`,
 1 AS `Collected`,
 1 AS `Total_Discount`,
 1 AS `Payment_Type`,
 1 AS `Payment_Reference`,
 1 AS `Remarks`,
 1 AS `Payment_IDs`,
 1 AS `Created_By`,
 1 AS `Created_By_Name`,
 1 AS `Created_At`,
 1 AS `Updated_At`,
 1 AS `Uncollected`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_collection_report_v47`
--

DROP TABLE IF EXISTS `v_collection_report_v47`;
/*!50001 DROP VIEW IF EXISTS `v_collection_report_v47`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_collection_report_v47` AS SELECT 
 1 AS `collection_id`,
 1 AS `OR_Number`,
 1 AS `Date`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Payor_Name`,
 1 AS `Water_Charge`,
 1 AS `Arrears`,
 1 AS `Penalty`,
 1 AS `Tax`,
 1 AS `SCF`,
 1 AS `Others`,
 1 AS `Amount_Collected`,
 1 AS `Amount_Received`,
 1 AS `Change_Amount`,
 1 AS `payment_type`,
 1 AS `payment_reference`,
 1 AS `remarks`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_collection_with_users`
--

DROP TABLE IF EXISTS `v_collection_with_users`;
/*!50001 DROP VIEW IF EXISTS `v_collection_with_users`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_collection_with_users` AS SELECT 
 1 AS `collection_id`,
 1 AS `or_number`,
 1 AS `collection_date`,
 1 AS `bill_numbers`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `total_current_bill`,
 1 AS `total_arrears`,
 1 AS `total_penalty`,
 1 AS `total_tax`,
 1 AS `total_scf`,
 1 AS `total_water_bill_paid`,
 1 AS `scf_paid`,
 1 AS `total_others`,
 1 AS `grand_total`,
 1 AS `amount_received`,
 1 AS `change_amount`,
 1 AS `total_paid_amount`,
 1 AS `uncollected`,
 1 AS `total_discount`,
 1 AS `payment_type`,
 1 AS `payment_reference`,
 1 AS `status`,
 1 AS `remarks`,
 1 AS `payor_name`,
 1 AS `created_at`,
 1 AS `updated_at`,
 1 AS `voided_at`,
 1 AS `billing_count`,
 1 AS `payment_count`,
 1 AS `created_by_user_id`,
 1 AS `created_by_username`,
 1 AS `created_by_full_name`,
 1 AS `created_by_role`,
 1 AS `created_by_is_active`,
 1 AS `voided_by_user_id`,
 1 AS `voided_by_username`,
 1 AS `voided_by_full_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire`
--

DROP TABLE IF EXISTS `v_concessionaire`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire` AS SELECT 
 1 AS `Concessionaire_ID`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Zone`,
 1 AS `Service`,
 1 AS `Meter_Number`,
 1 AS `First_Reading_Date`,
 1 AS `Tax_Exempt`,
 1 AS `Due_Exempt`,
 1 AS `Discounted`,
 1 AS `Status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire_collection_lookup`
--

DROP TABLE IF EXISTS `v_concessionaire_collection_lookup`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire_collection_lookup`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire_collection_lookup` AS SELECT 
 1 AS `concessionaire_id`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `zone_id`,
 1 AS `meter_no`,
 1 AS `is_discounted`,
 1 AS `is_tax_exempt`,
 1 AS `is_due_exempt`,
 1 AS `service_type`,
 1 AS `outstanding_balance`,
 1 AS `remaining_scf`,
 1 AS `unpaid_bill_count`,
 1 AS `last_billing_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire_details`
--

DROP TABLE IF EXISTS `v_concessionaire_details`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire_details`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire_details` AS SELECT 
 1 AS `Concessionaire_ID`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Tin`,
 1 AS `Status`,
 1 AS `Meter_Number`,
 1 AS `FRD`,
 1 AS `Tax_Exempted`,
 1 AS `Due_Exempted`,
 1 AS `Discounted`,
 1 AS `Not_Billable`,
 1 AS `Zone`,
 1 AS `Service_Type`,
 1 AS `Pipe_Size`,
 1 AS `SCF_Total_Amount`,
 1 AS `SCF_Balance`,
 1 AS `SCF_Monthly`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire_latest_reading`
--

DROP TABLE IF EXISTS `v_concessionaire_latest_reading`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire_latest_reading`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire_latest_reading` AS SELECT 
 1 AS `concessionaire_id`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `zone_id`,
 1 AS `service_id`,
 1 AS `meter_no`,
 1 AS `first_reading_date`,
 1 AS `status`,
 1 AS `reading_id`,
 1 AS `previous_reading`,
 1 AS `present_reading`,
 1 AS `reading_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire_scf`
--

DROP TABLE IF EXISTS `v_concessionaire_scf`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire_scf`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire_scf` AS SELECT 
 1 AS `scf_id`,
 1 AS `concessionaire_id`,
 1 AS `total_amount`,
 1 AS `balance`,
 1 AS `monthly`,
 1 AS `updated_at`,
 1 AS `last_payment_date`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_concessionaire_statistics_v3`
--

DROP TABLE IF EXISTS `v_concessionaire_statistics_v3`;
/*!50001 DROP VIEW IF EXISTS `v_concessionaire_statistics_v3`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_concessionaire_statistics_v3` AS SELECT 
 1 AS `Zone_ID`,
 1 AS `Zone_Name`,
 1 AS `Service_ID`,
 1 AS `Service_Type`,
 1 AS `Pipe_Size`,
 1 AS `Concessionaires`,
 1 AS `Tax_Exempted`,
 1 AS `Due_Exempted`,
 1 AS `Discounted`,
 1 AS `Active`,
 1 AS `Pending`,
 1 AS `Disconnected`,
 1 AS `Meters_Assigned`,
 1 AS `Meters_Unassigned`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_customer_ledger`
--

DROP TABLE IF EXISTS `v_customer_ledger`;
/*!50001 DROP VIEW IF EXISTS `v_customer_ledger`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_customer_ledger` AS SELECT 
 1 AS `concessionaire_id`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `zone_id`,
 1 AS `transaction_date`,
 1 AS `reference_no`,
 1 AS `remarks`,
 1 AS `debit`,
 1 AS `credit`,
 1 AS `water_charge`,
 1 AS `tax_amount`,
 1 AS `penalty_amount`,
 1 AS `scf_amount`,
 1 AS `arrears_amount`,
 1 AS `billing_status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_daily_billing`
--

DROP TABLE IF EXISTS `v_daily_billing`;
/*!50001 DROP VIEW IF EXISTS `v_daily_billing`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_daily_billing` AS SELECT 
 1 AS `Zone`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Invoice_Number`,
 1 AS `Cu_m³`,
 1 AS `Un_m³`,
 1 AS `Water_Bill`,
 1 AS `Penalty`,
 1 AS `Tax`,
 1 AS `Discount`,
 1 AS `Total_Water_Bill`,
 1 AS `SCF`,
 1 AS `Total_Amount_Billed`,
 1 AS `Initial`,
 1 AS `Date`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_daily_collection`
--

DROP TABLE IF EXISTS `v_daily_collection`;
/*!50001 DROP VIEW IF EXISTS `v_daily_collection`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_daily_collection` AS SELECT 
 1 AS `Collection_ID`,
 1 AS `OR_Number`,
 1 AS `Account_No`,
 1 AS `Concessionaire_Name`,
 1 AS `Address`,
 1 AS `Remarks`,
 1 AS `Invoice_Number`,
 1 AS `Water_Charge`,
 1 AS `Discount`,
 1 AS `Arrears`,
 1 AS `Penalty`,
 1 AS `Tax`,
 1 AS `SCF`,
 1 AS `SCF_Paid`,
 1 AS `Others`,
 1 AS `Total`,
 1 AS `Collected`,
 1 AS `Amount_Received`,
 1 AS `Change`,
 1 AS `Payment_Type`,
 1 AS `Reference_No`,
 1 AS `Payment_IDs`,
 1 AS `Date`,
 1 AS `Collected_By_ID`,
 1 AS `Collected_By`,
 1 AS `Created_At`,
 1 AS `Updated_At`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_input_meter_reading`
--

DROP TABLE IF EXISTS `v_input_meter_reading`;
/*!50001 DROP VIEW IF EXISTS `v_input_meter_reading`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_input_meter_reading` AS SELECT 
 1 AS `Is_Billed_Today`,
 1 AS `Zone`,
 1 AS `Concessionaire_Id`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Meter_Number`,
 1 AS `Previous_Reading_Date`,
 1 AS `Previous_Reading`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_map_billing_print_v2`
--

DROP TABLE IF EXISTS `v_map_billing_print_v2`;
/*!50001 DROP VIEW IF EXISTS `v_map_billing_print_v2`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_map_billing_print_v2` AS SELECT 
 1 AS `billing_id`,
 1 AS `zone_id`,
 1 AS `bill_number`,
 1 AS `billing_date`,
 1 AS `due_date`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `from_reading_date`,
 1 AS `to_reading_date`,
 1 AS `previous_reading`,
 1 AS `present_reading`,
 1 AS `total_meter_consumed`,
 1 AS `meter_unbilled`,
 1 AS `billed_meter`,
 1 AS `minimum_charge`,
 1 AS `total_water_consumption_amount`,
 1 AS `q_1_10`,
 1 AS `rate_1_10`,
 1 AS `amount_1_10`,
 1 AS `q_11_20`,
 1 AS `rate_11_20`,
 1 AS `amount_11_20`,
 1 AS `q_21_30`,
 1 AS `rate_21_30`,
 1 AS `amount_21_30`,
 1 AS `q_31_40`,
 1 AS `rate_31_40`,
 1 AS `amount_31_40`,
 1 AS `q_41_up`,
 1 AS `rate_41_up`,
 1 AS `amount_41_up`,
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `arrears_amount`,
 1 AS `scf_amount`,
 1 AS `subtotal_amount_due`,
 1 AS `penalty_amount`,
 1 AS `total_amount_due`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_map_billing_print_v3`
--

DROP TABLE IF EXISTS `v_map_billing_print_v3`;
/*!50001 DROP VIEW IF EXISTS `v_map_billing_print_v3`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_map_billing_print_v3` AS SELECT 
 1 AS `billing_id`,
 1 AS `bill_number`,
 1 AS `billing_date`,
 1 AS `due_date`,
 1 AS `concessionaire_code`,
 1 AS `concessionaire_name`,
 1 AS `address`,
 1 AS `from_reading_date`,
 1 AS `to_reading_date`,
 1 AS `previous_reading`,
 1 AS `present_reading`,
 1 AS `total_meter_consumed`,
 1 AS `meter_unbilled`,
 1 AS `billed_meter`,
 1 AS `minimum_charge`,
 1 AS `total_water_consumption_amount`,
 1 AS `q_1_10`,
 1 AS `rate_1_10`,
 1 AS `amount_1_10`,
 1 AS `q_11_20`,
 1 AS `rate_11_20`,
 1 AS `amount_11_20`,
 1 AS `q_21_30`,
 1 AS `rate_21_30`,
 1 AS `amount_21_30`,
 1 AS `q_31_40`,
 1 AS `rate_31_40`,
 1 AS `amount_31_40`,
 1 AS `q_41_up`,
 1 AS `rate_41_up`,
 1 AS `amount_41_up`,
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `arrears_amount`,
 1 AS `scf_amount`,
 1 AS `subtotal_amount_due`,
 1 AS `penalty_amount`,
 1 AS `total_amount_due`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_meter_reading_sheet`
--

DROP TABLE IF EXISTS `v_meter_reading_sheet`;
/*!50001 DROP VIEW IF EXISTS `v_meter_reading_sheet`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_meter_reading_sheet` AS SELECT 
 1 AS `Zone`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Meter_Number`,
 1 AS `Previous_Reading_Date`,
 1 AS `Previous_Reading`,
 1 AS `Present_Reading`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_meter_reading_sheet_backup`
--

DROP TABLE IF EXISTS `v_meter_reading_sheet_backup`;
/*!50001 DROP VIEW IF EXISTS `v_meter_reading_sheet_backup`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_meter_reading_sheet_backup` AS SELECT 
 1 AS `Zone`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Meter_Number`,
 1 AS `Previous_Reading_Date`,
 1 AS `Previous_Reading`,
 1 AS `Present_Reading`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_monthly_billing`
--

DROP TABLE IF EXISTS `v_monthly_billing`;
/*!50001 DROP VIEW IF EXISTS `v_monthly_billing`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_monthly_billing` AS SELECT 
 1 AS `Bill_Number`,
 1 AS `Concessionaire_Code`,
 1 AS `Concessionaire_Name`,
 1 AS `Billing_Date`,
 1 AS `Due_Date`,
 1 AS `Consumption`,
 1 AS `Water_Charge`,
 1 AS `Discount`,
 1 AS `Tax`,
 1 AS `SCF`,
 1 AS `Penalty`,
 1 AS `Total_Amount`,
 1 AS `Status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_monthly_collection`
--

DROP TABLE IF EXISTS `v_monthly_collection`;
/*!50001 DROP VIEW IF EXISTS `v_monthly_collection`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_monthly_collection` AS SELECT 
 1 AS `Year`,
 1 AS `Month_Name`,
 1 AS `Month_Period`,
 1 AS `Total_Current_Bill`,
 1 AS `Total_Penalty`,
 1 AS `Total_Tax`,
 1 AS `Total_SCF`,
 1 AS `Total_Others`,
 1 AS `Total_Arrears`,
 1 AS `Grand_Total`,
 1 AS `Amount_Received`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_service_billing_monthly`
--

DROP TABLE IF EXISTS `v_service_billing_monthly`;
/*!50001 DROP VIEW IF EXISTS `v_service_billing_monthly`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_service_billing_monthly` AS SELECT 
 1 AS `billing_month`,
 1 AS `service_id`,
 1 AS `service_type`,
 1 AS `pipe_size`,
 1 AS `total_bills`,
 1 AS `total_consumption`,
 1 AS `total_water_charge`,
 1 AS `total_tax_amount`,
 1 AS `total_penalty_amount`,
 1 AS `total_billed_amount`,
 1 AS `total_remaining_balance`,
 1 AS `billing_share_percent`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_service_summary`
--

DROP TABLE IF EXISTS `v_service_summary`;
/*!50001 DROP VIEW IF EXISTS `v_service_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_service_summary` AS SELECT 
 1 AS `ServiceID`,
 1 AS `ServiceType`,
 1 AS `PipeSize`,
 1 AS `TotalConcessionaires`,
 1 AS `TaxExemptCount`,
 1 AS `DueExemptCount`,
 1 AS `DiscountedCount`,
 1 AS `ActiveCount`,
 1 AS `InactiveCount`,
 1 AS `MetersAssigned`,
 1 AS `MetersUnassigned`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_service_summary_v2`
--

DROP TABLE IF EXISTS `v_service_summary_v2`;
/*!50001 DROP VIEW IF EXISTS `v_service_summary_v2`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_service_summary_v2` AS SELECT 
 1 AS `Service_ID`,
 1 AS `Service_Type`,
 1 AS `Pipe_Size`,
 1 AS `Concessionaires`,
 1 AS `Active`,
 1 AS `Pending`,
 1 AS `Disconnected`,
 1 AS `Tax_Exempted`,
 1 AS `Due_Exempted`,
 1 AS `Discounted`,
 1 AS `Meters_Assigned`,
 1 AS `Meters_Unassigned`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_top_zones`
--

DROP TABLE IF EXISTS `v_top_zones`;
/*!50001 DROP VIEW IF EXISTS `v_top_zones`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_top_zones` AS SELECT 
 1 AS `Zone_ID`,
 1 AS `Zone_Name`,
 1 AS `Active`,
 1 AS `Pending`,
 1 AS `Disconnected`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_user_logs`
--

DROP TABLE IF EXISTS `v_user_logs`;
/*!50001 DROP VIEW IF EXISTS `v_user_logs`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_user_logs` AS SELECT 
 1 AS `log_id`,
 1 AS `user_id`,
 1 AS `username`,
 1 AS `full_name`,
 1 AS `role`,
 1 AS `action_type`,
 1 AS `module`,
 1 AS `entity_name`,
 1 AS `entity_id`,
 1 AS `description`,
 1 AS `created_at`,
 1 AS `formatted_date`,
 1 AS `severity`,
 1 AS `activity_summary`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `v_zone_summary`
--

DROP TABLE IF EXISTS `v_zone_summary`;
/*!50001 DROP VIEW IF EXISTS `v_zone_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `v_zone_summary` AS SELECT 
 1 AS `Zone_ID`,
 1 AS `Zone_Name`,
 1 AS `Concessionaires`,
 1 AS `Active`,
 1 AS `Pending`,
 1 AS `Disconnected`,
 1 AS `Tax_Exempted`,
 1 AS `Due_Exempted`,
 1 AS `Discounted`,
 1 AS `Meters_Assigned`,
 1 AS `Meters_Unassigned`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `zone`
--

DROP TABLE IF EXISTS `zone`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `zone` (
  `zone_id` int NOT NULL,
  `zone_name` varchar(100) NOT NULL,
  PRIMARY KEY (`zone_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zone`
--

LOCK TABLES `zone` WRITE;
/*!40000 ALTER TABLE `zone` DISABLE KEYS */;
INSERT INTO `zone` VALUES (1,'1');
/*!40000 ALTER TABLE `zone` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'wdbs_tubungan_db'
--
/*!50106 SET @save_time_zone= @@TIME_ZONE */ ;
/*!50106 DROP EVENT IF EXISTS `ev_mark_billing_overdue` */;
DELIMITER ;;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;;
/*!50003 SET character_set_client  = utf8mb4 */ ;;
/*!50003 SET character_set_results = utf8mb4 */ ;;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;;
/*!50003 SET @saved_time_zone      = @@time_zone */ ;;
/*!50003 SET time_zone             = 'SYSTEM' */ ;;
/*!50106 CREATE*/ /*!50117 DEFINER=`root`@`localhost`*/ /*!50106 EVENT `ev_mark_billing_overdue` ON SCHEDULE EVERY 1 DAY STARTS '2025-10-30 00:00:00' ON COMPLETION NOT PRESERVE ENABLE DO BEGIN
    -- 1. Update billing table: only 'unpaid' bills get 'overdue'
    UPDATE billing
    SET status = 'overdue'
    WHERE status = 'unpaid'
      AND due_date IS NOT NULL
      AND due_date < CURDATE();

    -- 2. Update SCF status
    UPDATE billing
    SET scf_status = 'overdue'
    WHERE scf_status = 'unpaid'
      AND due_date IS NOT NULL
      AND due_date < CURDATE();
END */ ;;
/*!50003 SET time_zone             = @saved_time_zone */ ;;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;;
/*!50003 SET character_set_client  = @saved_cs_client */ ;;
/*!50003 SET character_set_results = @saved_cs_results */ ;;
/*!50003 SET collation_connection  = @saved_col_connection */ ;;
DELIMITER ;
/*!50106 SET TIME_ZONE= @save_time_zone */ ;

--
-- Dumping routines for database 'wdbs_tubungan_db'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT
)
BEGIN
    -- ===============================
    -- Declarations (same as v5/v6)
    -- ===============================
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_raw_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;

    DECLARE v_discount_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_discount_thresh INT DEFAULT 0;
    DECLARE v_tax_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_penalize_after_days INT DEFAULT 14;
    DECLARE v_penalty_percent DECIMAL(7,4) DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_arrears_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_total_cap DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_unpaid DECIMAL(12,2) DEFAULT 0; -- unpaid from last SCF billing
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;

    DECLARE v_billing_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE DEFAULT NULL;

    DECLARE v_msg VARCHAR(255) DEFAULT '';

    -- ===============================
    -- Validate concessionaire exists
    -- ===============================
    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SET v_msg = CONCAT('Concessionaire ID ', p_concessionaire_id, ' not found.');
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_msg;
    END IF;

    -- ===============================
    -- Load system settings
    -- ===============================
    SELECT 
      CAST(MAX(CASE WHEN settings_key='discount_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='discount_thresh_hold' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='tax_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='penalize_after_days' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='penalty_percent' THEN settings_value END) AS DECIMAL(7,4))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_after_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_after_days = COALESCE(v_penalize_after_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    -- concessionaire flags
    SELECT is_discounted, is_tax_exempt, is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    -- ===============================
    -- Load service rates for this concessionaire (unchanged)
    -- ===============================
    SELECT s.min_rate, s.rate_11_20, s.rate_21_30, s.rate_31_40, s.rate_41_above
    INTO v_min_rate, v_rate_11_20, v_rate_21_30, v_rate_31_40, v_rate_41_above
    FROM services s
    JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- ===============================
    -- Previous reading and consumption
    -- ===============================
    SELECT present_reading INTO v_prev_reading
    FROM reading
    WHERE concessionaire_id = p_concessionaire_id
    ORDER BY reading_id DESC
    LIMIT 1;

    SET v_prev_reading = COALESCE(v_prev_reading, 0);
    SET v_raw_consumption = p_present_reading - v_prev_reading;
    IF v_raw_consumption < 0 THEN SET v_raw_consumption = 0; END IF;

    SET v_billable = v_raw_consumption - GREATEST(p_free_water,0);
    IF v_billable < 0 THEN SET v_billable = 0; END IF;

    -- ===============================
    -- Compute tiered water charge (unchanged)
    -- ===============================
    SET v_water_charge = v_min_rate;
    IF v_billable <= 10 THEN
      SET v_water_charge = v_min_rate;
    ELSE
      IF v_billable > 10 THEN
        SET v_water_charge = v_min_rate + LEAST(v_billable - 10, 10) * v_rate_11_20;
      END IF;
      IF v_billable > 20 THEN
        SET v_water_charge = v_water_charge + LEAST(v_billable - 20, 10) * v_rate_21_30;
      END IF;
      IF v_billable > 30 THEN
        SET v_water_charge = v_water_charge + LEAST(v_billable - 30, 10) * v_rate_31_40;
      END IF;
      IF v_billable > 40 THEN
        SET v_water_charge = v_water_charge + (v_billable - 40) * v_rate_41_above;
      END IF;
    END IF;
    SET v_water_charge = ROUND(v_water_charge, 2);

    -- ===============================
    -- ======= FIXED SCF BLOCK =======
    -- Use only the most recent billing row's unpaid SCF,
    -- add ONE monthly installment, cap at total_amount.
    -- ===============================
    SELECT COALESCE(monthly, 0), COALESCE(total_amount, 0)
    INTO v_scf_monthly, v_scf_total_cap
    FROM scf_balance
    WHERE concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- get unpaid SCF from most recent billing with unpaid/partial/overdue scf_status
    SELECT COALESCE((
        SELECT (b.scf_amount - COALESCE((SELECT SUM(p2.scf_paid) FROM payment p2 WHERE p2.billing_id = b.billing_id),0))
        FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
          AND b.scf_status IN ('unpaid','partially_paid','overdue')
        ORDER BY b.billing_id DESC
        LIMIT 1
    ), 0)
    INTO v_scf_unpaid;

    SET v_scf_amount = LEAST(GREATEST(v_scf_unpaid, 0) + v_scf_monthly, v_scf_total_cap);

    -- ===============================
    -- Arrears (water only)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p3.amount_paid) FROM payment p3 WHERE p3.billing_id = b.billing_id),0))
    ), 0)
    INTO v_arrears_amount
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue');

    -- ===============================
    -- Penalty: apply only to overdue unpaid water amounts (due_date < billing_date)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p4.amount_paid) FROM payment p4 WHERE p4.billing_id = b.billing_id),0))
    ), 0)
    INTO @overdue_unpaid
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue')
      AND b.due_date < v_billing_date;

    SET @overdue_unpaid = COALESCE(@overdue_unpaid, 0);
    IF @overdue_unpaid > 0 AND v_is_due_exempt = 0 THEN
        SET v_penalty_amount = ROUND(@overdue_unpaid * (v_penalty_percent / 100), 2);
    ELSE
        SET v_penalty_amount = 0;
    END IF;

    -- ===============================
    -- Tax & Discount (unchanged)
    -- ===============================
    IF v_is_tax_exempt = 1 THEN
        SET v_tax_amount = 0;
    ELSE
        SET v_tax_amount = ROUND(v_water_charge * (v_tax_percent / 100), 2);
    END IF;

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount_amount = ROUND((v_water_charge + v_tax_amount) * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount_amount = 0;
    END IF;

    SET v_total_water_bill = ROUND(v_water_charge + v_tax_amount - v_discount_amount, 2);

    -- ===============================
    -- Grand total
    -- ===============================
    SET v_total_amount = ROUND(v_total_water_bill + v_arrears_amount + v_penalty_amount + v_scf_amount, 2);

    -- due date
    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_billing_date, INTERVAL v_penalize_after_days DAY);
    END IF;

    -- ===============================
    -- Persist reading & billing
    -- ===============================
    INSERT INTO reading (concessionaire_id, previous_reading, present_reading, reading_date)
    VALUES (p_concessionaire_id, v_prev_reading, p_present_reading, v_billing_date);

    INSERT INTO billing (
        bill_number, concessionaire_id, reading_id, billing_date, due_date, free_water,
        consumption, water_charge, discount_amount, tax_amount, penalty_amount,
        total_water_bill, scf_amount, arrears_amount, total_amount, status, scf_status
    )
    VALUES (
        p_bill_number, p_concessionaire_id, LAST_INSERT_ID(), v_billing_date, v_due_date, GREATEST(p_free_water,0),
        v_raw_consumption, v_water_charge, v_discount_amount, v_tax_amount, v_penalty_amount,
        v_total_water_bill, v_scf_amount, v_arrears_amount, v_total_amount, 'unpaid', 'unpaid'
    );

    -- ===============================
    -- Return summary row
    -- ===============================
    SELECT
        p_concessionaire_id AS concessionaire_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_raw_consumption AS raw_consumption,
        v_billable AS billable_consumption,
        v_water_charge AS water_charge,
        v_tax_amount AS tax_amount,
        v_discount_amount AS discount_amount,
        v_total_water_bill AS total_water_bill,
        v_scf_amount AS scf_amount,
        v_arrears_amount AS arrears_amount,
        v_penalty_amount AS penalty_amount,
        v_total_amount AS grand_total;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v4` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v4`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT
)
BEGIN
    -- ===============================
    -- Variable Declarations
    -- ===============================
    DECLARE v_exists INT DEFAULT 0;
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_raw_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;

    DECLARE v_discount_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_discount_threshold INT DEFAULT 0;
    DECLARE v_tax_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_penalize_after_days INT DEFAULT 14;
    DECLARE v_penalty_percent DECIMAL(7,4) DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_arrears_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_total_cap DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_unpaid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_billing_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE;
    DECLARE v_is_discounted BOOLEAN DEFAULT 0;
    DECLARE v_is_tax_exempt BOOLEAN DEFAULT 0;
    DECLARE v_is_due_exempt BOOLEAN DEFAULT 0;

    DECLARE q10 INT DEFAULT 0;
    DECLARE q20 INT DEFAULT 0;
    DECLARE q30 INT DEFAULT 0;
    DECLARE q40 INT DEFAULT 0;
    DECLARE q41 INT DEFAULT 0;

    DECLARE a10 DECIMAL(12,2) DEFAULT 0;
    DECLARE a20 DECIMAL(12,2) DEFAULT 0;
    DECLARE a30 DECIMAL(12,2) DEFAULT 0;
    DECLARE a40 DECIMAL(12,2) DEFAULT 0;
    DECLARE a41 DECIMAL(12,2) DEFAULT 0;

    -- ===============================
    -- Check if Concessionaire Exists
    -- ===============================
    SELECT COUNT(*)
    INTO v_exists
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    IF v_exists = 0 THEN
        SET @msg = CONCAT('Concessionaire ID ', p_concessionaire_id, ' not found.');
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = @msg;
    END IF;

    -- ===============================
    -- Load System Settings
    -- ===============================
    SELECT 
        CAST(MAX(CASE WHEN settings_key = 'discount_percent' THEN settings_value END) AS DECIMAL(7,4)),
        CAST(MAX(CASE WHEN settings_key = 'discount_thresh_hold' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'tax_percent' THEN settings_value END) AS DECIMAL(7,4)),
        CAST(MAX(CASE WHEN settings_key = 'penalize_after_days' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(7,4))
    INTO v_discount_percent, v_discount_threshold, v_tax_percent, v_penalize_after_days, v_penalty_percent
    FROM system_settings;

    -- ===============================
    -- Load Concessionaire Info
    -- ===============================
    SELECT is_discounted, is_tax_exempt, is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    -- ===============================
    -- Load Service Rates
    -- ===============================
    SELECT s.min_rate, s.rate_11_20, s.rate_21_30, s.rate_31_40, s.rate_41_above
    INTO v_min_rate, v_rate_11_20, v_rate_21_30, v_rate_31_40, v_rate_41_above
    FROM services s
    INNER JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- ===============================
    -- Load Previous Reading
    -- ===============================
    SELECT present_reading
    INTO v_prev_reading
    FROM reading
    WHERE concessionaire_id = p_concessionaire_id
    ORDER BY reading_id DESC
    LIMIT 1;

    SET v_prev_reading = COALESCE(v_prev_reading, 0);
    SET v_raw_consumption = p_present_reading - v_prev_reading;
    IF v_raw_consumption < 0 THEN
        SET v_raw_consumption = 0;
    END IF;

    SET v_billable = v_raw_consumption - p_free_water;
    IF v_billable < 0 THEN
        SET v_billable = 0;
    END IF;

    -- ===============================
    -- Compute Tiered Water Charge
    -- ===============================
    SET q10 = LEAST(v_billable, 10);
    SET q20 = LEAST(GREATEST(v_billable - 10, 0), 10);
    SET q30 = LEAST(GREATEST(v_billable - 20, 0), 10);
    SET q40 = LEAST(GREATEST(v_billable - 30, 0), 10);
    SET q41 = GREATEST(v_billable - 40, 0);

    SET a10 = IF(q10 > 0, v_min_rate, 0);
    SET a20 = q20 * v_rate_11_20;
    SET a30 = q30 * v_rate_21_30;
    SET a40 = q40 * v_rate_31_40;
    SET a41 = q41 * v_rate_41_above;

    SET v_water_charge = ROUND(a10 + a20 + a30 + a40 + a41, 2);

    -- ===============================
    -- Get SCF (Monthly, Unpaid, and Cap)
    -- ===============================
    SELECT COALESCE(monthly, 0), COALESCE(total_amount, 0)
    INTO v_scf_monthly, v_scf_total_cap
    FROM scf_balance
    WHERE concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SELECT COALESCE(SUM(b.scf_amount) - COALESCE(SUM(p.scf_paid), 0), 0)
    INTO v_scf_unpaid
    FROM billing b
    LEFT JOIN payment p ON b.billing_id = p.billing_id
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.scf_status IN ('unpaid','partially_paid','overdue');

    SET v_scf_amount = LEAST(v_scf_unpaid + v_scf_monthly, v_scf_total_cap);

    -- ===============================
    -- Compute Arrears (Water only)
    -- ===============================
    SELECT COALESCE(SUM(b.total_water_bill) - COALESCE(SUM(p.amount_paid), 0), 0)
    INTO v_arrears_amount
    FROM billing b
    LEFT JOIN payment p ON b.billing_id = p.billing_id
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue');

    -- ===============================
    -- Compute Tax, Discount, and Total
    -- ===============================
    IF v_is_tax_exempt = 1 THEN
        SET v_tax_amount = 0;
    ELSE
        SET v_tax_amount = ROUND(v_water_charge * (v_tax_percent / 100), 2);
    END IF;

    IF v_is_discounted = 1 AND v_billable <= v_discount_threshold THEN
        SET v_discount_amount = ROUND((v_water_charge + v_tax_amount) * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount_amount = 0;
    END IF;

    SET v_total_water_bill = ROUND(v_water_charge + v_tax_amount - v_discount_amount, 2);

    -- ===============================
    -- Apply Penalty to Arrears (Water Only)
    -- ===============================
    SET v_penalty_amount = ROUND(v_arrears_amount * (v_penalty_percent / 100), 2);

    -- ===============================
    -- Compute Grand Total
    -- ===============================
    SET v_total_amount = ROUND(v_total_water_bill + v_scf_amount + v_arrears_amount + v_penalty_amount, 2);

    -- ===============================
    -- Compute Due Date
    -- ===============================
    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_billing_date, INTERVAL v_penalize_after_days DAY);
    END IF;

    -- ===============================
    -- Insert into Reading & Billing Tables
    -- ===============================
    INSERT INTO reading (concessionaire_id, previous_reading, present_reading, reading_date)
    VALUES (p_concessionaire_id, v_prev_reading, p_present_reading, v_billing_date);

    INSERT INTO billing (
        bill_number, concessionaire_id, reading_id, billing_date, due_date, free_water,
        consumption, water_charge, discount_amount, tax_amount, penalty_amount,
        total_water_bill, scf_amount, arrears_amount, total_amount, status, scf_status
    )
    VALUES (
        p_bill_number, p_concessionaire_id, LAST_INSERT_ID(), v_billing_date, v_due_date, p_free_water,
        v_billable, v_water_charge, v_discount_amount, v_tax_amount, v_penalty_amount,
        v_total_water_bill, v_scf_amount, v_arrears_amount, v_total_amount, 'unpaid', 'unpaid'
    );

    -- ===============================
    -- Return Billing Summary
    -- ===============================
    SELECT
        p_concessionaire_id AS concessionaire_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_billable AS billable_consumption,
        v_water_charge AS water_charge,
        v_tax_amount AS tax_amount,
        v_discount_amount AS discount_amount,
        v_scf_amount AS scf_amount,
        v_arrears_amount AS arrears_amount,
        v_penalty_amount AS penalty_amount,
        v_total_amount AS total_amount;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v45` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v45`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT,
    IN p_request_id VARCHAR(200),
    IN p_user_id INT,
    IN p_force_initial TINYINT
)
sp_main: BEGIN
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_reading_id INT DEFAULT NULL;

    DECLARE v_bill_count INT DEFAULT 0;
    DECLARE v_is_initial TINYINT DEFAULT 0;
    DECLARE v_force_initial TINYINT DEFAULT 0;

    DECLARE v_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;
    DECLARE v_free_water INT DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge_raw DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_raw DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_gross_total DECIMAL(12,2) DEFAULT 0;

    DECLARE v_arrears_snapshot DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_applied_total DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalized_count INT DEFAULT 0;

    DECLARE v_scf_id INT DEFAULT NULL;
    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_balance_before DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_current_live_balance DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_tax_percent DECIMAL(12,2) DEFAULT 2;
    DECLARE v_discount_percent DECIMAL(12,2) DEFAULT 7;
    DECLARE v_penalty_percent DECIMAL(12,2) DEFAULT 10;
    DECLARE v_discount_thresh INT DEFAULT 30;
    DECLARE v_penalize_days INT DEFAULT 14;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;

    DECLARE v_bill_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE DEFAULT NULL;
    DECLARE v_request_id_clean VARCHAR(200) DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SET v_force_initial = COALESCE(p_force_initial, 0);
    SET v_request_id_clean = NULLIF(TRIM(p_request_id), '');

    IF v_request_id_clean IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'request_id is required';
    END IF;

    IF EXISTS (SELECT 1 FROM billing WHERE request_id = v_request_id_clean) THEN
        SELECT
            1 AS is_duplicate_request,
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            b.reading_id,
            b.billing_date,
            b.due_date,
            b.consumption,
            b.free_water,
            b.water_charge,
            b.discount_amount,
            b.tax_amount,
            b.total_water_bill,
            b.scf_amount,
            b.arrears_amount,
            b.penalty_amount,
            b.total_amount,
            b.status,
            b.scf_status,
            b.scf_monthly_used,
            b.scf_total_cap_used,
            b.remaining_water_charge,
            b.remaining_tax_amount,
            b.remaining_penalty_amount,
            b.remaining_scf_amount,
            b.remaining_balance,
            b.request_id,
            b.created_at,
            b.total_amount AS statement_total_amount,
            b.remaining_balance AS current_bill_remaining_balance
        FROM billing b
        WHERE b.request_id = v_request_id_clean
        ORDER BY b.billing_id DESC
        LIMIT 1;

        LEAVE sp_main;
    END IF;

    START TRANSACTION;

    IF p_bill_number IS NULL OR TRIM(p_bill_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bill number is required';
    END IF;

    IF EXISTS (SELECT 1 FROM billing WHERE bill_number = p_bill_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate bill number';
    END IF;

    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    SELECT c.is_discounted, c.is_tax_exempt, c.is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire c
    WHERE c.concessionaire_id = p_concessionaire_id
    FOR UPDATE;

    SET v_is_discounted = COALESCE(v_is_discounted, 0);
    SET v_is_tax_exempt = COALESCE(v_is_tax_exempt, 0);
    SET v_is_due_exempt = COALESCE(v_is_due_exempt, 0);

    SELECT
        CAST(MAX(CASE WHEN settings_key = 'discount_percent' THEN settings_value END) AS DECIMAL(12,2)),
        CAST(MAX(CASE WHEN settings_key = 'discount_thresh_hold' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'tax_percent' THEN settings_value END) AS DECIMAL(12,2)),
        CAST(MAX(CASE WHEN settings_key = 'penalize_after_days' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_days = COALESCE(v_penalize_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    SELECT
        s.min_rate,
        s.rate_11_20,
        s.rate_21_30,
        s.rate_31_40,
        s.rate_41_above
    INTO
        v_min_rate,
        v_rate_11_20,
        v_rate_21_30,
        v_rate_31_40,
        v_rate_41_above
    FROM services s
    INNER JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SET v_min_rate = COALESCE(v_min_rate, 0.00);
    SET v_rate_11_20 = COALESCE(v_rate_11_20, 0.00);
    SET v_rate_21_30 = COALESCE(v_rate_21_30, 0.00);
    SET v_rate_31_40 = COALESCE(v_rate_31_40, 0.00);
    SET v_rate_41_above = COALESCE(v_rate_41_above, 0.00);

    IF v_min_rate <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Service rate configuration is missing';
    END IF;

    SELECT COUNT(*)
    INTO v_bill_count
    FROM billing
    WHERE concessionaire_id = p_concessionaire_id;

    IF v_force_initial = 1 THEN
        SET v_is_initial = 1;
    ELSEIF v_force_initial = 3 THEN
        SET v_is_initial = 0;
    ELSE
        SET v_is_initial = CASE WHEN v_bill_count = 0 THEN 1 ELSE 0 END;
    END IF;

    SELECT COALESCE(
        (SELECT present_reading
         FROM reading
         WHERE concessionaire_id = p_concessionaire_id
         ORDER BY reading_date DESC, reading_id DESC
         LIMIT 1),
        0
    )
    INTO v_prev_reading;

    IF p_present_reading < v_prev_reading THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid reading';
    END IF;

    INSERT INTO reading (
        concessionaire_id,
        previous_reading,
        present_reading,
        reading_date
    )
    VALUES (
        p_concessionaire_id,
        v_prev_reading,
        p_present_reading,
        v_bill_date
    );

    SET v_reading_id = LAST_INSERT_ID();

    SET v_free_water = GREATEST(COALESCE(p_free_water, 0), 0);
    SET v_consumption = GREATEST(p_present_reading - v_prev_reading, 0);
    SET v_billable = GREATEST(v_consumption - v_free_water, 0);

    IF v_billable = 0 THEN
        IF v_is_initial = 1 THEN
            SET v_water_charge_raw = 0.00;
        ELSE
            SET v_water_charge_raw = v_min_rate;
        END IF;
    ELSE
        IF v_is_initial = 1 AND v_billable < 10 THEN
            SET v_water_charge_raw = ROUND(v_billable * (v_min_rate / 10), 2);
        ELSE
            SET v_water_charge_raw = v_min_rate;

            IF v_billable > 10 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 10, 10) * v_rate_11_20);
            END IF;

            IF v_billable > 20 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 20, 10) * v_rate_21_30);
            END IF;

            IF v_billable > 30 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 30, 10) * v_rate_31_40);
            END IF;

            IF v_billable > 40 THEN
                SET v_water_charge_raw = v_water_charge_raw + ((v_billable - 40) * v_rate_41_above);
            END IF;

            SET v_water_charge_raw = ROUND(v_water_charge_raw, 2);
        END IF;
    END IF;

    IF v_is_tax_exempt = 1 THEN
        SET v_tax_raw = 0.00;
    ELSE
        SET v_tax_raw = ROUND(v_water_charge_raw * (v_tax_percent / 100), 2);
    END IF;

    SET v_gross_total = ROUND(v_water_charge_raw + v_tax_raw, 2);

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount = ROUND(v_gross_total * (v_discount_percent / 100), 2);
        SET v_water_discount_amount = ROUND(v_water_charge_raw * (v_discount_percent / 100), 2);
        SET v_tax_discount_amount = ROUND(v_tax_raw * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount = 0.00;
        SET v_water_discount_amount = 0.00;
        SET v_tax_discount_amount = 0.00;
    END IF;

    SET v_water_charge_raw = ROUND(v_water_charge_raw - v_water_discount_amount, 2);
    SET v_tax_raw = ROUND(v_tax_raw - v_tax_discount_amount, 2);
    SET v_total_water_bill = ROUND(v_water_charge_raw + v_tax_raw, 2);

SELECT COALESCE(
    SUM(
        ROUND(
            GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100),
            2
        )
    ),
    0
)
INTO v_penalty_applied_total
FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0
      AND b.due_date IS NOT NULL
      AND b.due_date < v_bill_date
      AND COALESCE(b.is_penalty_applied, 0) = 0
      AND GREATEST(b.remaining_water_charge, 0) > 0;

UPDATE billing b
SET
    penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
    remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
    remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
        is_penalty_applied = 1,
        penalty_applied_at = NOW(),
        status = 'overdue',
        updated_at = NOW(),
        updated_by_user_id = p_user_id
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0
      AND b.due_date IS NOT NULL
      AND b.due_date < v_bill_date
      AND COALESCE(b.is_penalty_applied, 0) = 0
      AND GREATEST(b.remaining_water_charge, 0) > 0;

    SET v_penalized_count = ROW_COUNT();

    SELECT COALESCE(
        SUM(
            GREATEST(b.remaining_water_charge, 0) +
            GREATEST(b.remaining_tax_amount, 0)
        ),
        0
    )
    INTO v_arrears_snapshot
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0;
    SELECT
        (SELECT sb.scf_id
         FROM scf_balance sb
         WHERE sb.concessionaire_id = p_concessionaire_id
         ORDER BY sb.scf_id DESC
         LIMIT 1),
        COALESCE(
            (SELECT sb.monthly
             FROM scf_balance sb
             WHERE sb.concessionaire_id = p_concessionaire_id
             ORDER BY sb.scf_id DESC
             LIMIT 1),
            0
        ),
        COALESCE(
            (SELECT sb.balance
             FROM scf_balance sb
             WHERE sb.concessionaire_id = p_concessionaire_id
             ORDER BY sb.scf_id DESC
             LIMIT 1),
            0
        )
    INTO v_scf_id, v_scf_monthly, v_scf_balance_before;

    IF v_scf_id IS NULL THEN
        SET v_scf_monthly = 0.00;
        SET v_scf_balance_before = 0.00;
        SET v_scf_amount = 0.00;
    ELSE
        SET v_scf_amount = LEAST(GREATEST(v_scf_monthly, 0), GREATEST(v_scf_balance_before, 0));

        UPDATE scf_balance sb
        SET balance = GREATEST(sb.balance - v_scf_amount, 0),
            updated_at = NOW()
        WHERE sb.scf_id = v_scf_id;
    END IF;

    SET v_current_live_balance = ROUND(v_total_water_bill + v_scf_amount, 2);
    SET v_total_amount = ROUND(v_current_live_balance + v_arrears_snapshot, 2);

    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_bill_date, INTERVAL v_penalize_days DAY);
    END IF;

    INSERT INTO billing (
        bill_number,
        concessionaire_id,
        reading_id,
        billing_date,
        due_date,
        consumption,
        free_water,
        water_charge,
        discount_amount,
        tax_amount,
        total_water_bill,
        scf_amount,
        arrears_amount,
        penalty_amount,
        total_amount,
        status,
        scf_status,
        scf_monthly_used,
        scf_total_cap_used,
        remaining_water_charge,
        remaining_tax_amount,
        remaining_penalty_amount,
        remaining_scf_amount,
        remaining_balance,
        payment_count,
        last_collection_id,
        paid_at,
        request_id,
        created_by_user_id,
        updated_by_user_id,
        tax_percent_used,
        discount_percent_used,
        penalty_percent_used,
        is_initial,
        is_penalty_applied,
        penalty_applied_at,
        created_at,
        updated_at,
        last_payment_date
    )
    VALUES (
        p_bill_number,
        p_concessionaire_id,
        v_reading_id,
        v_bill_date,
        v_due_date,
        v_consumption,
        v_free_water,
        v_water_charge_raw,
        v_discount,
        v_tax_raw,
        v_total_water_bill,
        v_scf_amount,
        v_arrears_snapshot,
        0.00,
        v_total_amount,
        CASE WHEN v_current_live_balance > 0 THEN 'unpaid' ELSE 'paid' END,
        CASE WHEN v_scf_amount > 0 THEN 'unpaid' ELSE 'paid' END,
        v_scf_monthly,
        v_scf_balance_before,
        GREATEST(ROUND(v_water_charge_raw, 2), 0),
        GREATEST(ROUND(v_tax_raw, 2), 0),
        0.00,
        v_scf_amount,
        v_current_live_balance,
        0,
        NULL,
        NULL,
        v_request_id_clean,
        p_user_id,
        p_user_id,
        v_tax_percent,
        v_discount_percent,
        v_penalty_percent,
        v_is_initial,
        0,
        NULL,
        NOW(),
        NOW(),
        NULL
    );

    COMMIT;

    SELECT
        0 AS is_duplicate_request,
        p_bill_number AS bill_number,
        p_request_id AS request_id,
        p_concessionaire_id AS concessionaire_id,
        v_reading_id AS reading_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_consumption AS consumption,
        v_billable AS billable_consumption,
        v_water_charge_raw AS water_charge,
        v_discount AS current_discount_amount,
        v_tax_raw AS tax_amount,
        v_total_water_bill AS total_water_bill,
        v_arrears_snapshot AS arrears_amount,
        0.00 AS penalty_amount,
        v_scf_amount AS scf_amount,
        v_penalized_count AS penalized_bill_count,
        v_penalty_applied_total AS penalty_applied_total,
        v_current_live_balance AS current_bill_remaining_balance,
        v_total_amount AS statement_total_amount,
        v_bill_date AS billing_date,
        v_due_date AS due_date;
END sp_main ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v46` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v46`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT,
    IN p_request_id VARCHAR(200),
    IN p_user_id INT,
    IN p_force_initial TINYINT,
    IN p_bill_date DATE
)
sp_main: BEGIN
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_reading_id INT DEFAULT NULL;

    DECLARE v_bill_count INT DEFAULT 0;
    DECLARE v_is_initial TINYINT DEFAULT 0;
    DECLARE v_force_initial TINYINT DEFAULT 0;

    DECLARE v_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;
    DECLARE v_free_water INT DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge_raw DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_raw DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_gross_total DECIMAL(12,2) DEFAULT 0;

    DECLARE v_arrears_snapshot DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_applied_total DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalized_count INT DEFAULT 0;

    DECLARE v_scf_id INT DEFAULT NULL;
    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_balance_before DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_current_live_balance DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_tax_percent DECIMAL(12,2) DEFAULT 2;
    DECLARE v_discount_percent DECIMAL(12,2) DEFAULT 7;
    DECLARE v_penalty_percent DECIMAL(12,2) DEFAULT 10;
    DECLARE v_discount_thresh INT DEFAULT 30;
    DECLARE v_penalize_days INT DEFAULT 14;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;

    DECLARE v_bill_date DATE DEFAULT NULL;
    DECLARE v_due_date DATE DEFAULT NULL;
    DECLARE v_request_id_clean VARCHAR(200) DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SET v_force_initial = COALESCE(p_force_initial, 0);
    SET v_request_id_clean = NULLIF(TRIM(p_request_id), '');
    SET v_bill_date = p_bill_date;

    IF v_bill_date IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'bill_date is required';
    END IF;

    IF v_request_id_clean IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'request_id is required';
    END IF;

    IF EXISTS (SELECT 1 FROM billing WHERE request_id = v_request_id_clean) THEN
        SELECT
            1 AS is_duplicate_request,
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            b.reading_id,
            b.billing_date,
            b.due_date,
            b.consumption,
            b.free_water,
            b.water_charge,
            b.discount_amount,
            b.tax_amount,
            b.total_water_bill,
            b.scf_amount,
            b.arrears_amount,
            b.penalty_amount,
            b.total_amount,
            b.status,
            b.scf_status,
            b.scf_monthly_used,
            b.scf_total_cap_used,
            b.remaining_water_charge,
            b.remaining_tax_amount,
            b.remaining_penalty_amount,
            b.remaining_scf_amount,
            b.remaining_balance,
            b.request_id,
            b.created_at,
            b.total_amount AS statement_total_amount,
            b.remaining_balance AS current_bill_remaining_balance
        FROM billing b
        WHERE b.request_id = v_request_id_clean
        ORDER BY b.billing_id DESC
        LIMIT 1;

        LEAVE sp_main;
    END IF;

    START TRANSACTION;

    IF p_bill_number IS NULL OR TRIM(p_bill_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Bill number is required';
    END IF;

    IF EXISTS (SELECT 1 FROM billing WHERE bill_number = p_bill_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate bill number';
    END IF;

    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    SELECT c.is_discounted, c.is_tax_exempt, c.is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire c
    WHERE c.concessionaire_id = p_concessionaire_id
    FOR UPDATE;

    SET v_is_discounted = COALESCE(v_is_discounted, 0);
    SET v_is_tax_exempt = COALESCE(v_is_tax_exempt, 0);
    SET v_is_due_exempt = COALESCE(v_is_due_exempt, 0);

    SELECT
        CAST(MAX(CASE WHEN settings_key = 'discount_percent' THEN settings_value END) AS DECIMAL(12,2)),
        CAST(MAX(CASE WHEN settings_key = 'discount_thresh_hold' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'tax_percent' THEN settings_value END) AS DECIMAL(12,2)),
        CAST(MAX(CASE WHEN settings_key = 'penalize_after_days' THEN settings_value END) AS SIGNED),
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_days = COALESCE(v_penalize_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    SELECT
        s.min_rate,
        s.rate_11_20,
        s.rate_21_30,
        s.rate_31_40,
        s.rate_41_above
    INTO
        v_min_rate,
        v_rate_11_20,
        v_rate_21_30,
        v_rate_31_40,
        v_rate_41_above
    FROM services s
    INNER JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SET v_min_rate = COALESCE(v_min_rate, 0.00);
    SET v_rate_11_20 = COALESCE(v_rate_11_20, 0.00);
    SET v_rate_21_30 = COALESCE(v_rate_21_30, 0.00);
    SET v_rate_31_40 = COALESCE(v_rate_31_40, 0.00);
    SET v_rate_41_above = COALESCE(v_rate_41_above, 0.00);

    IF v_min_rate <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Service rate configuration is missing';
    END IF;

    SELECT COUNT(*)
    INTO v_bill_count
    FROM billing
    WHERE concessionaire_id = p_concessionaire_id;

    IF v_force_initial = 1 THEN
        SET v_is_initial = 1;
    ELSEIF v_force_initial = 3 THEN
        SET v_is_initial = 0;
    ELSE
        SET v_is_initial = CASE WHEN v_bill_count = 0 THEN 1 ELSE 0 END;
    END IF;

    SELECT COALESCE(
        (SELECT present_reading
         FROM reading
         WHERE concessionaire_id = p_concessionaire_id
         ORDER BY reading_date DESC, reading_id DESC
         LIMIT 1),
        0
    )
    INTO v_prev_reading;

    IF p_present_reading < v_prev_reading THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid reading';
    END IF;

    INSERT INTO reading (
        concessionaire_id,
        previous_reading,
        present_reading,
        reading_date
    )
    VALUES (
        p_concessionaire_id,
        v_prev_reading,
        p_present_reading,
        v_bill_date
    );

    SET v_reading_id = LAST_INSERT_ID();

    SET v_free_water = GREATEST(COALESCE(p_free_water, 0), 0);
    SET v_consumption = GREATEST(p_present_reading - v_prev_reading, 0);
    SET v_billable = GREATEST(v_consumption - v_free_water, 0);

    IF v_billable = 0 THEN
        IF v_is_initial = 1 THEN
            SET v_water_charge_raw = 0.00;
        ELSE
            SET v_water_charge_raw = v_min_rate;
        END IF;
    ELSE
        IF v_is_initial = 1 AND v_billable < 10 THEN
            SET v_water_charge_raw = ROUND(v_billable * (v_min_rate / 10), 2);
        ELSE
            SET v_water_charge_raw = v_min_rate;

            IF v_billable > 10 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 10, 10) * v_rate_11_20);
            END IF;

            IF v_billable > 20 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 20, 10) * v_rate_21_30);
            END IF;

            IF v_billable > 30 THEN
                SET v_water_charge_raw = v_water_charge_raw + (LEAST(v_billable - 30, 10) * v_rate_31_40);
            END IF;

            IF v_billable > 40 THEN
                SET v_water_charge_raw = v_water_charge_raw + ((v_billable - 40) * v_rate_41_above);
            END IF;

            SET v_water_charge_raw = ROUND(v_water_charge_raw, 2);
        END IF;
    END IF;

    IF v_is_tax_exempt = 1 THEN
        SET v_tax_raw = 0.00;
    ELSE
        SET v_tax_raw = ROUND(v_water_charge_raw * (v_tax_percent / 100), 2);
    END IF;

    SET v_gross_total = ROUND(v_water_charge_raw + v_tax_raw, 2);

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount = ROUND(v_gross_total * (v_discount_percent / 100), 2);
        SET v_water_discount_amount = ROUND(v_water_charge_raw * (v_discount_percent / 100), 2);
        SET v_tax_discount_amount = ROUND(v_tax_raw * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount = 0.00;
        SET v_water_discount_amount = 0.00;
        SET v_tax_discount_amount = 0.00;
    END IF;

    SET v_water_charge_raw = ROUND(v_water_charge_raw - v_water_discount_amount, 2);
    SET v_tax_raw = ROUND(v_tax_raw - v_tax_discount_amount, 2);
    SET v_total_water_bill = ROUND(v_water_charge_raw + v_tax_raw, 2);

    SELECT COALESCE(
        SUM(
            ROUND(
                GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100),
                2
            )
        ),
        0
    )
    INTO v_penalty_applied_total
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0
      AND b.due_date IS NOT NULL
      AND b.due_date < v_bill_date
      AND COALESCE(b.is_penalty_applied, 0) = 0
      AND GREATEST(b.remaining_water_charge, 0) > 0;

    UPDATE billing b
    SET
        penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
        remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
        remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + (GREATEST(b.remaining_water_charge, 0) * (v_penalty_percent / 100)), 2),
        is_penalty_applied = 1,
        penalty_applied_at = NOW(),
        status = 'overdue',
        updated_at = NOW(),
        updated_by_user_id = p_user_id
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0
      AND b.due_date IS NOT NULL
      AND b.due_date < v_bill_date
      AND COALESCE(b.is_penalty_applied, 0) = 0
      AND GREATEST(b.remaining_water_charge, 0) > 0;

    SET v_penalized_count = ROW_COUNT();

    SELECT COALESCE(
        SUM(
            GREATEST(b.remaining_water_charge, 0) +
            GREATEST(b.remaining_tax_amount, 0)
        ),
        0
    )
    INTO v_arrears_snapshot
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0;

    SELECT
        (SELECT sb.scf_id
         FROM scf_balance sb
         WHERE sb.concessionaire_id = p_concessionaire_id
         ORDER BY sb.scf_id DESC
         LIMIT 1),
        COALESCE(
            (SELECT sb.monthly
             FROM scf_balance sb
             WHERE sb.concessionaire_id = p_concessionaire_id
             ORDER BY sb.scf_id DESC
             LIMIT 1),
            0
        ),
        COALESCE(
            (SELECT sb.balance
             FROM scf_balance sb
             WHERE sb.concessionaire_id = p_concessionaire_id
             ORDER BY sb.scf_id DESC
             LIMIT 1),
            0
        )
    INTO v_scf_id, v_scf_monthly, v_scf_balance_before;

    IF v_scf_id IS NULL THEN
        SET v_scf_monthly = 0.00;
        SET v_scf_balance_before = 0.00;
        SET v_scf_amount = 0.00;
    ELSE
        SET v_scf_amount = LEAST(GREATEST(v_scf_monthly, 0), GREATEST(v_scf_balance_before, 0));

        UPDATE scf_balance sb
        SET balance = GREATEST(sb.balance - v_scf_amount, 0),
            updated_at = NOW()
        WHERE sb.scf_id = v_scf_id;
    END IF;

    SET v_current_live_balance = ROUND(v_total_water_bill + v_scf_amount, 2);
    SET v_total_amount = ROUND(v_current_live_balance + v_arrears_snapshot, 2);

    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_bill_date, INTERVAL v_penalize_days DAY);
    END IF;

    INSERT INTO billing (
        bill_number,
        concessionaire_id,
        reading_id,
        billing_date,
        due_date,
        consumption,
        free_water,
        water_charge,
        discount_amount,
        tax_amount,
        total_water_bill,
        scf_amount,
        arrears_amount,
        penalty_amount,
        total_amount,
        status,
        scf_status,
        scf_monthly_used,
        scf_total_cap_used,
        remaining_water_charge,
        remaining_tax_amount,
        remaining_penalty_amount,
        remaining_scf_amount,
        remaining_balance,
        payment_count,
        last_collection_id,
        paid_at,
        request_id,
        created_by_user_id,
        updated_by_user_id,
        tax_percent_used,
        discount_percent_used,
        penalty_percent_used,
        is_initial,
        is_penalty_applied,
        penalty_applied_at,
        created_at,
        updated_at,
        last_payment_date
    )
    VALUES (
        p_bill_number,
        p_concessionaire_id,
        v_reading_id,
        v_bill_date,
        v_due_date,
        v_consumption,
        v_free_water,
        v_water_charge_raw,
        v_discount,
        v_tax_raw,
        v_total_water_bill,
        v_scf_amount,
        v_arrears_snapshot,
        0.00,
        v_total_amount,
        CASE WHEN v_current_live_balance > 0 THEN 'unpaid' ELSE 'paid' END,
        CASE WHEN v_scf_amount > 0 THEN 'unpaid' ELSE 'paid' END,
        v_scf_monthly,
        v_scf_balance_before,
        GREATEST(ROUND(v_water_charge_raw, 2), 0),
        GREATEST(ROUND(v_tax_raw, 2), 0),
        0.00,
        v_scf_amount,
        v_current_live_balance,
        0,
        NULL,
        NULL,
        v_request_id_clean,
        p_user_id,
        p_user_id,
        v_tax_percent,
        v_discount_percent,
        v_penalty_percent,
        v_is_initial,
        0,
        NULL,
        NOW(),
        NOW(),
        NULL
    );

    COMMIT;
INSERT INTO user_logs (
    user_id,
    action_type,
    module,
    entity_name,
    entity_id,
    description
)
VALUES (
    p_user_id,
    'CREATE',
    'BILLING',
    'billing',
    p_bill_number,
    CONCAT(
        'New billing record #', p_bill_number,
        ' created successfully. Total amount: ₱',
        FORMAT(v_total_amount, 2),
        '. Consumption: ', v_consumption,
        ' cu.m. Due date: ',
        IFNULL(DATE_FORMAT(v_due_date, '%Y-%m-%d'), 'N/A'),
        '.'
    )
);

    SELECT
        0 AS is_duplicate_request,
        p_bill_number AS bill_number,
        p_request_id AS request_id,
        p_concessionaire_id AS concessionaire_id,
        v_reading_id AS reading_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_consumption AS consumption,
        v_billable AS billable_consumption,
        v_water_charge_raw AS water_charge,
        v_discount AS current_discount_amount,
        v_tax_raw AS tax_amount,
        v_total_water_bill AS total_water_bill,
        v_arrears_snapshot AS arrears_amount,
        0.00 AS penalty_amount,
        v_scf_amount AS scf_amount,
        v_penalized_count AS penalized_bill_count,
        v_penalty_applied_total AS penalty_applied_total,
        v_current_live_balance AS current_bill_remaining_balance,
        v_total_amount AS statement_total_amount,
        v_bill_date AS billing_date,
        v_due_date AS due_date;
END sp_main ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v6` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v6`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT
)
BEGIN
    -- ===============================
    -- Declarations (same as v5/v6)
    -- ===============================
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_raw_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;

    DECLARE v_discount_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_discount_thresh INT DEFAULT 0;
    DECLARE v_tax_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_penalize_after_days INT DEFAULT 14;
    DECLARE v_penalty_percent DECIMAL(7,4) DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_arrears_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_total_cap DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_unpaid DECIMAL(12,2) DEFAULT 0; -- unpaid from last SCF billing
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;

    DECLARE v_billing_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE DEFAULT NULL;

    DECLARE v_msg VARCHAR(255) DEFAULT '';

    -- ===============================
    -- Validate concessionaire exists
    -- ===============================
    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SET v_msg = CONCAT('Concessionaire ID ', p_concessionaire_id, ' not found.');
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_msg;
    END IF;

    -- ===============================
    -- Load system settings
    -- ===============================
    SELECT 
      CAST(MAX(CASE WHEN settings_key='discount_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='discount_thresh_hold' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='tax_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='penalize_after_days' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='penalty_percent' THEN settings_value END) AS DECIMAL(7,4))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_after_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_after_days = COALESCE(v_penalize_after_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    -- concessionaire flags
    SELECT is_discounted, is_tax_exempt, is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    -- ===============================
    -- Load service rates for this concessionaire (unchanged)
    -- ===============================
    SELECT s.min_rate, s.rate_11_20, s.rate_21_30, s.rate_31_40, s.rate_41_above
    INTO v_min_rate, v_rate_11_20, v_rate_21_30, v_rate_31_40, v_rate_41_above
    FROM services s
    JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- ===============================
    -- Previous reading and consumption
    -- ===============================
    SELECT present_reading INTO v_prev_reading
    FROM reading
    WHERE concessionaire_id = p_concessionaire_id
    ORDER BY reading_id DESC
    LIMIT 1;

    SET v_prev_reading = COALESCE(v_prev_reading, 0);
    SET v_raw_consumption = p_present_reading - v_prev_reading;
    IF v_raw_consumption < 0 THEN SET v_raw_consumption = 0; END IF;

    SET v_billable = v_raw_consumption - GREATEST(p_free_water,0);
    IF v_billable < 0 THEN SET v_billable = 0; END IF;

    -- ===============================
    -- Compute tiered water charge (unchanged)
    -- ===============================
    SET v_water_charge = v_min_rate;
    IF v_billable <= 10 THEN
      SET v_water_charge = v_min_rate;
    ELSE
      IF v_billable > 10 THEN
        SET v_water_charge = v_min_rate + LEAST(v_billable - 10, 10) * v_rate_11_20;
      END IF;
      IF v_billable > 20 THEN
        SET v_water_charge = v_water_charge + LEAST(v_billable - 20, 10) * v_rate_21_30;
      END IF;
      IF v_billable > 30 THEN
        SET v_water_charge = v_water_charge + LEAST(v_billable - 30, 10) * v_rate_31_40;
      END IF;
      IF v_billable > 40 THEN
        SET v_water_charge = v_water_charge + (v_billable - 40) * v_rate_41_above;
      END IF;
    END IF;
    SET v_water_charge = ROUND(v_water_charge, 2);

    -- ===============================
    -- ======= FIXED SCF BLOCK =======
    -- Use only the most recent billing row's unpaid SCF,
    -- add ONE monthly installment, cap at total_amount.
    -- ===============================
    SELECT COALESCE(monthly, 0), COALESCE(total_amount, 0)
    INTO v_scf_monthly, v_scf_total_cap
    FROM scf_balance
    WHERE concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- get unpaid SCF from most recent billing with unpaid/partial/overdue scf_status
    SELECT COALESCE((
        SELECT (b.scf_amount - COALESCE((SELECT SUM(p2.scf_paid) FROM payment p2 WHERE p2.billing_id = b.billing_id),0))
        FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
          AND b.scf_status IN ('unpaid','partially_paid','overdue')
        ORDER BY b.billing_id DESC
        LIMIT 1
    ), 0)
    INTO v_scf_unpaid;

    SET v_scf_amount = LEAST(GREATEST(v_scf_unpaid, 0) + v_scf_monthly, v_scf_total_cap);

    -- ===============================
    -- Arrears (water only)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p3.amount_paid) FROM payment p3 WHERE p3.billing_id = b.billing_id),0))
    ), 0)
    INTO v_arrears_amount
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue');

    -- ===============================
    -- Penalty: apply only to overdue unpaid water amounts (due_date < billing_date)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p4.amount_paid) FROM payment p4 WHERE p4.billing_id = b.billing_id),0))
    ), 0)
    INTO @overdue_unpaid
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue')
      AND b.due_date < v_billing_date;

    SET @overdue_unpaid = COALESCE(@overdue_unpaid, 0);
    IF @overdue_unpaid > 0 AND v_is_due_exempt = 0 THEN
        SET v_penalty_amount = ROUND(@overdue_unpaid * (v_penalty_percent / 100), 2);
    ELSE
        SET v_penalty_amount = 0;
    END IF;

    -- ===============================
    -- Tax & Discount (unchanged)
    -- ===============================
    IF v_is_tax_exempt = 1 THEN
        SET v_tax_amount = 0;
    ELSE
        SET v_tax_amount = ROUND(v_water_charge * (v_tax_percent / 100), 2);
    END IF;

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount_amount = ROUND((v_water_charge + v_tax_amount) * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount_amount = 0;
    END IF;

    SET v_total_water_bill = ROUND(v_water_charge + v_tax_amount - v_discount_amount, 2);

    -- ===============================
    -- Grand total
    -- ===============================
    SET v_total_amount = ROUND(v_total_water_bill + v_arrears_amount + v_penalty_amount + v_scf_amount, 2);

    -- due date
    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_billing_date, INTERVAL v_penalize_after_days DAY);
    END IF;

    -- ===============================
    -- Persist reading & billing
    -- ===============================
    INSERT INTO reading (concessionaire_id, previous_reading, present_reading, reading_date)
    VALUES (p_concessionaire_id, v_prev_reading, p_present_reading, v_billing_date);

    INSERT INTO billing (
        bill_number, concessionaire_id, reading_id, billing_date, due_date, free_water,
        consumption, water_charge, discount_amount, tax_amount, penalty_amount,
        total_water_bill, scf_amount, arrears_amount, total_amount, status, scf_status
    )
    VALUES (
        p_bill_number, p_concessionaire_id, LAST_INSERT_ID(), v_billing_date, v_due_date, GREATEST(p_free_water,0),
        v_raw_consumption, v_water_charge, v_discount_amount, v_tax_amount, v_penalty_amount,
        v_total_water_bill, v_scf_amount, v_arrears_amount, v_total_amount, 'unpaid', 'unpaid'
    );

    -- ===============================
    -- Return summary row
    -- ===============================
    SELECT
        p_concessionaire_id AS concessionaire_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_raw_consumption AS raw_consumption,
        v_billable AS billable_consumption,
        v_water_charge AS water_charge,
        v_tax_amount AS tax_amount,
        v_discount_amount AS discount_amount,
        v_total_water_bill AS total_water_bill,
        v_scf_amount AS scf_amount,
        v_arrears_amount AS arrears_amount,
        v_penalty_amount AS penalty_amount,
        v_total_amount AS grand_total;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v7` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v7`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT)
BEGIN
    -- ===============================
    -- Declarations
    -- ===============================
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_raw_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;

    DECLARE v_discount_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_discount_thresh INT DEFAULT 0;
    DECLARE v_tax_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_penalize_after_days INT DEFAULT 14;
    DECLARE v_penalty_percent DECIMAL(7,4) DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_arrears_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_total_cap DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_unpaid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;
    DECLARE v_is_initial_billing TINYINT DEFAULT 0;

    DECLARE v_billing_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE DEFAULT NULL;

    DECLARE v_msg VARCHAR(255) DEFAULT '';

    -- ===============================
    -- Validate concessionaire exists
    -- ===============================
    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SET v_msg = CONCAT('Concessionaire ID ', p_concessionaire_id, ' not found.');
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_msg;
    END IF;

    -- ===============================
    -- Load system settings
    -- ===============================
    SELECT 
      CAST(MAX(CASE WHEN settings_key='discount_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='discount_thresh_hold' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='tax_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='penalize_after_days' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='penalty_percent' THEN settings_value END) AS DECIMAL(7,4))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_after_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_after_days = COALESCE(v_penalize_after_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    -- ===============================
    -- Concessionaire flags
    -- ===============================
    SELECT is_discounted, is_tax_exempt, is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    -- ===============================
    -- Load service rates
    -- ===============================
    SELECT s.min_rate, s.rate_11_20, s.rate_21_30, s.rate_31_40, s.rate_41_above
    INTO v_min_rate, v_rate_11_20, v_rate_21_30, v_rate_31_40, v_rate_41_above
    FROM services s
    JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- ===============================
    -- Previous reading and consumption
    -- ===============================
    SELECT present_reading INTO v_prev_reading
    FROM reading
    WHERE concessionaire_id = p_concessionaire_id
    ORDER BY reading_id DESC
    LIMIT 1;

    SET v_prev_reading = COALESCE(v_prev_reading, 0);
    SET v_raw_consumption = p_present_reading - v_prev_reading;
    IF v_raw_consumption < 0 THEN SET v_raw_consumption = 0; END IF;

    SET v_billable = v_raw_consumption - GREATEST(p_free_water,0);
    IF v_billable < 0 THEN SET v_billable = 0; END IF;

    -- ===============================
    -- Detect initial billing
    -- ===============================
    SELECT COUNT(*) INTO @bill_count
    FROM billing
    WHERE concessionaire_id = p_concessionaire_id;

    IF @bill_count = 0 THEN
        SET v_is_initial_billing = 1;
    END IF;

-- ===============================
-- Compute tiered water charge (with initial billing rule)
-- ===============================
IF v_billable = 0 THEN
    -- No billable consumption => no water charge.
    SET v_water_charge = 0.00;
ELSE
    IF v_is_initial_billing = 1 AND v_billable < 10 THEN
        -- Initial billing below 10 m³ → charge actual proportional rate
        SET v_water_charge = ROUND(v_billable * (v_min_rate / 10), 2);
    ELSE
        SET v_water_charge = v_min_rate;
        IF v_billable > 10 THEN
            SET v_water_charge = v_water_charge + LEAST(v_billable - 10, 10) * v_rate_11_20;
        END IF;
        IF v_billable > 20 THEN
            SET v_water_charge = v_water_charge + LEAST(v_billable - 20, 10) * v_rate_21_30;
        END IF;
        IF v_billable > 30 THEN
            SET v_water_charge = v_water_charge + LEAST(v_billable - 30, 10) * v_rate_31_40;
        END IF;
        IF v_billable > 40 THEN
            SET v_water_charge = v_water_charge + (v_billable - 40) * v_rate_41_above;
        END IF;
        SET v_water_charge = ROUND(v_water_charge, 2);
    END IF;
END IF;


    -- ===============================
    -- SCF logic
    -- ===============================
    SELECT COALESCE(monthly, 0), COALESCE(balance, 0)
    INTO v_scf_monthly, v_scf_total_cap
    FROM scf_balance
    WHERE concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SELECT COALESCE((
        SELECT (b.scf_amount - COALESCE((SELECT SUM(p2.scf_paid) FROM payment p2 WHERE p2.billing_id = b.billing_id),0))
        FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
          AND b.scf_status IN ('unpaid','partially_paid','overdue')
        ORDER BY b.billing_id DESC
        LIMIT 1
    ), 0)
    INTO v_scf_unpaid;

    SET v_scf_amount = LEAST(GREATEST(v_scf_unpaid, 0) + v_scf_monthly, v_scf_total_cap);

    -- ===============================
    -- Arrears (water only)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p3.amount_paid) FROM payment p3 WHERE p3.billing_id = b.billing_id),0))
    ), 0)
    INTO v_arrears_amount
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue');

    -- ===============================
    -- Penalty
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p4.amount_paid) FROM payment p4 WHERE p4.billing_id = b.billing_id),0))
    ), 0)
    INTO @overdue_unpaid
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue')
      AND b.due_date < v_billing_date;

    SET @overdue_unpaid = COALESCE(@overdue_unpaid, 0);
    IF @overdue_unpaid > 0 AND v_is_due_exempt = 0 THEN
        SET v_penalty_amount = ROUND(@overdue_unpaid * (v_penalty_percent / 100), 2);
    ELSE
        SET v_penalty_amount = 0;
    END IF;

    -- ===============================
    -- Tax & Discount
    -- ===============================
    IF v_is_tax_exempt = 1 THEN
        SET v_tax_amount = 0;
    ELSE
        SET v_tax_amount = ROUND(v_water_charge * (v_tax_percent / 100), 2);
    END IF;

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount_amount = ROUND((v_water_charge + v_tax_amount) * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount_amount = 0;
    END IF;

    SET v_total_water_bill = ROUND(v_water_charge + v_tax_amount - v_discount_amount, 2);

    -- ===============================
    -- Grand total
    -- ===============================
    SET v_total_amount = ROUND(v_total_water_bill + v_arrears_amount + v_penalty_amount + v_scf_amount, 2);

    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_billing_date, INTERVAL v_penalize_after_days DAY);
    END IF;

    -- ===============================
    -- Save reading & billing
    -- ===============================
    INSERT INTO reading (concessionaire_id, previous_reading, present_reading, reading_date)
    VALUES (p_concessionaire_id, v_prev_reading, p_present_reading, v_billing_date);

    INSERT INTO billing (
        bill_number, concessionaire_id, reading_id, billing_date, due_date, free_water,
        consumption, water_charge, discount_amount, tax_amount, penalty_amount,
        total_water_bill, scf_amount, arrears_amount, total_amount, status, scf_status
    )
    VALUES (
        p_bill_number, p_concessionaire_id, LAST_INSERT_ID(), v_billing_date, v_due_date, GREATEST(p_free_water,0),
        v_raw_consumption, v_water_charge, v_discount_amount, v_tax_amount, v_penalty_amount,
        v_total_water_bill, v_scf_amount, v_arrears_amount, v_total_amount, 'unpaid', 'unpaid'
    );

    -- ===============================
    -- Return summary
    -- ===============================
    SELECT
        p_concessionaire_id AS concessionaire_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_raw_consumption AS raw_consumption,
        v_billable AS billable_consumption,
        v_water_charge AS water_charge,
        v_tax_amount AS tax_amount,
        v_discount_amount AS discount_amount,
        v_total_water_bill AS total_water_bill,
        v_scf_amount AS scf_amount,
        v_arrears_amount AS arrears_amount,
        v_penalty_amount AS penalty_amount,
        v_total_amount AS grand_total;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_bill_v7_1` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_bill_v7_1`(
    IN p_concessionaire_id INT,
    IN p_present_reading INT,
    IN p_bill_number VARCHAR(50),
    IN p_free_water INT,
    IN p_force_initial TINYINT(1)  )
BEGIN
    -- ===============================
    -- Declarations
    -- ===============================
    DECLARE v_prev_reading INT DEFAULT 0;
    DECLARE v_raw_consumption INT DEFAULT 0;
    DECLARE v_billable INT DEFAULT 0;

    DECLARE v_discount_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_discount_thresh INT DEFAULT 0;
    DECLARE v_tax_percent DECIMAL(7,4) DEFAULT 0;
    DECLARE v_penalize_after_days INT DEFAULT 14;
    DECLARE v_penalty_percent DECIMAL(7,4) DEFAULT 0;

    DECLARE v_min_rate DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_11_20 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_21_30 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_31_40 DECIMAL(12,2) DEFAULT 0;
    DECLARE v_rate_41_above DECIMAL(12,2) DEFAULT 0;

    DECLARE v_water_charge DECIMAL(12,2) DEFAULT 0;
    DECLARE v_discount_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_tax_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_penalty_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_arrears_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_bill DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_total_cap DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_unpaid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_amount DECIMAL(12,2) DEFAULT 0;

    DECLARE v_is_discounted TINYINT DEFAULT 0;
    DECLARE v_is_tax_exempt TINYINT DEFAULT 0;
    DECLARE v_is_due_exempt TINYINT DEFAULT 0;
    DECLARE v_is_initial_billing TINYINT DEFAULT 0;

    DECLARE v_billing_date DATE DEFAULT CURDATE();
    DECLARE v_due_date DATE DEFAULT NULL;

    DECLARE v_msg VARCHAR(255) DEFAULT '';

    -- ===============================
    -- Validate concessionaire exists
    -- ===============================
    IF NOT EXISTS (SELECT 1 FROM concessionaire WHERE concessionaire_id = p_concessionaire_id) THEN
        SET v_msg = CONCAT('Concessionaire ID ', p_concessionaire_id, ' not found.');
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = v_msg;
    END IF;

    -- ===============================
    -- Load system settings
    -- ===============================
    SELECT 
      CAST(MAX(CASE WHEN settings_key='discount_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='discount_thresh_hold' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='tax_percent' THEN settings_value END) AS DECIMAL(7,4)),
      CAST(MAX(CASE WHEN settings_key='penalize_after_days' THEN settings_value END) AS SIGNED),
      CAST(MAX(CASE WHEN settings_key='penalty_percent' THEN settings_value END) AS DECIMAL(7,4))
    INTO v_discount_percent, v_discount_thresh, v_tax_percent, v_penalize_after_days, v_penalty_percent
    FROM system_settings;

    SET v_discount_percent = COALESCE(v_discount_percent, 7.00);
    SET v_discount_thresh = COALESCE(v_discount_thresh, 30);
    SET v_tax_percent = COALESCE(v_tax_percent, 2.00);
    SET v_penalize_after_days = COALESCE(v_penalize_after_days, 14);
    SET v_penalty_percent = COALESCE(v_penalty_percent, 10.00);

    -- ===============================
    -- Concessionaire flags
    -- ===============================
    SELECT is_discounted, is_tax_exempt, is_due_exempt
    INTO v_is_discounted, v_is_tax_exempt, v_is_due_exempt
    FROM concessionaire
    WHERE concessionaire_id = p_concessionaire_id;

    -- ===============================
    -- Load service rates
    -- ===============================
    SELECT s.min_rate, s.rate_11_20, s.rate_21_30, s.rate_31_40, s.rate_41_above
    INTO v_min_rate, v_rate_11_20, v_rate_21_30, v_rate_31_40, v_rate_41_above
    FROM services s
    JOIN concessionaire c ON c.service_id = s.service_id
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    -- ===============================
    -- Previous reading and consumption
    -- ===============================
    SELECT present_reading INTO v_prev_reading
    FROM reading
    WHERE concessionaire_id = p_concessionaire_id
    ORDER BY reading_date DESC
    LIMIT 1;

    SET v_prev_reading = COALESCE(v_prev_reading, 0);
    SET v_raw_consumption = p_present_reading - v_prev_reading;
    IF v_raw_consumption < 0 THEN SET v_raw_consumption = 0; END IF;

    SET v_billable = v_raw_consumption - GREATEST(p_free_water,0);
    IF v_billable < 0 THEN SET v_billable = 0; END IF;

    -- ===============================
    -- Detect initial billing
    -- ===============================
    SELECT COUNT(*) INTO @bill_count
    FROM billing
    WHERE concessionaire_id = p_concessionaire_id;

-- Determine initial billing intent:
-- If caller passed p_force_initial = 1 => mark this as initial and clear any existing initial flag.
-- Otherwise, if there is already an existing initial billing, do not mark this as initial.
IF p_force_initial = 1 THEN
    SET v_is_initial_billing = 1;
ELSE
    IF EXISTS (
        SELECT 1 FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
          AND COALESCE(b.is_initial, 0) = 1
        LIMIT 1
    ) THEN
        SET v_is_initial_billing = 0;
    ELSE
        SET v_is_initial_billing = 0;
    END IF;
END IF;

-- ===============================
-- Compute tiered water charge (WITH ZERO-CONSUMPTION RULE)
-- ===============================
IF v_billable = 0 THEN

    IF v_is_initial_billing = 1 THEN
        -- Initial billing + zero consumption → NO CHARGE
        SET v_water_charge = 0.00;
    ELSE
        -- NOT initial billing + zero consumption → MINIMUM CHARGE
        SET v_water_charge = v_min_rate;
    END IF;

ELSE

    IF v_is_initial_billing = 1 AND v_billable < 10 THEN
        -- Initial billing below 10 m³ → proportional minimum
        SET v_water_charge = ROUND(v_billable * (v_min_rate / 10), 2);
    ELSE
        -- Normal tiered billing
        SET v_water_charge = v_min_rate;

        IF v_billable > 10 THEN
            SET v_water_charge = v_water_charge
                + LEAST(v_billable - 10, 10) * v_rate_11_20;
        END IF;

        IF v_billable > 20 THEN
            SET v_water_charge = v_water_charge
                + LEAST(v_billable - 20, 10) * v_rate_21_30;
        END IF;

        IF v_billable > 30 THEN
            SET v_water_charge = v_water_charge
                + LEAST(v_billable - 30, 10) * v_rate_31_40;
        END IF;

        IF v_billable > 40 THEN
            SET v_water_charge = v_water_charge
                + (v_billable - 40) * v_rate_41_above;
        END IF;

        SET v_water_charge = ROUND(v_water_charge, 2);
    END IF;

END IF;



    -- ===============================
    -- SCF logic
    -- ===============================
    SELECT COALESCE(monthly, 0), COALESCE(balance, 0)
    INTO v_scf_monthly, v_scf_total_cap
    FROM scf_balance
    WHERE concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SELECT COALESCE((
        SELECT (b.scf_amount - COALESCE((SELECT SUM(p2.scf_paid) FROM payment p2 WHERE p2.billing_id = b.billing_id),0))
        FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
          AND b.scf_status IN ('unpaid','partially_paid','overdue')
        ORDER BY b.billing_id DESC
        LIMIT 1
    ), 0)
    INTO v_scf_unpaid;

    SET v_scf_amount = LEAST(GREATEST(v_scf_unpaid, 0) + v_scf_monthly, v_scf_total_cap);

    -- ===============================
    -- Arrears (water only)
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p3.amount_paid) FROM payment p3 WHERE p3.billing_id = b.billing_id),0))
    ), 0)
    INTO v_arrears_amount
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue');

    -- ===============================
    -- Penalty
    -- ===============================
    SELECT COALESCE(SUM(
        (b.total_water_bill - COALESCE((SELECT SUM(p4.amount_paid) FROM payment p4 WHERE p4.billing_id = b.billing_id),0))
    ), 0)
    INTO @overdue_unpaid
    FROM billing b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.status IN ('unpaid','partially_paid','overdue')
      AND b.due_date < v_billing_date;

    SET @overdue_unpaid = COALESCE(@overdue_unpaid, 0);
    IF @overdue_unpaid > 0 AND v_is_due_exempt = 0 THEN
        SET v_penalty_amount = ROUND(@overdue_unpaid * (v_penalty_percent / 100), 2);
    ELSE
        SET v_penalty_amount = 0;
    END IF;

    -- ===============================
    -- Tax & Discount
    -- ===============================
    IF v_is_tax_exempt = 1 THEN
        SET v_tax_amount = 0;
    ELSE
        SET v_tax_amount = ROUND(v_water_charge * (v_tax_percent / 100), 2);
    END IF;

    IF v_is_discounted = 1 AND v_billable <= v_discount_thresh THEN
        SET v_discount_amount = ROUND((v_water_charge + v_tax_amount) * (v_discount_percent / 100), 2);
    ELSE
        SET v_discount_amount = 0;
    END IF;

    SET v_total_water_bill = ROUND(v_water_charge + v_tax_amount - v_discount_amount, 2);

    -- ===============================
    -- Grand total
    -- ===============================
    SET v_total_amount = ROUND(v_total_water_bill + v_arrears_amount + v_penalty_amount + v_scf_amount, 2);

    IF v_is_due_exempt = 1 THEN
        SET v_due_date = NULL;
    ELSE
        SET v_due_date = DATE_ADD(v_billing_date, INTERVAL v_penalize_after_days DAY);
    END IF;

    -- ===============================
    -- Save reading & billing
    -- ===============================
    INSERT INTO reading (concessionaire_id, previous_reading, present_reading, reading_date)
    VALUES (p_concessionaire_id, v_prev_reading, p_present_reading, v_billing_date);
IF p_force_initial = 1 THEN
    UPDATE billing
    SET is_initial = 0
    WHERE concessionaire_id = p_concessionaire_id AND COALESCE(is_initial,0) = 1;
END IF;

    INSERT INTO billing (
        bill_number, concessionaire_id, reading_id, billing_date, due_date, free_water,
        consumption, water_charge, discount_amount, tax_amount, penalty_amount,
        total_water_bill, scf_amount, arrears_amount, total_amount, status, scf_status, is_initial
    )
    VALUES (
        p_bill_number, p_concessionaire_id, LAST_INSERT_ID(), v_billing_date, v_due_date, GREATEST(p_free_water,0),
        v_raw_consumption, v_water_charge, v_discount_amount, v_tax_amount, v_penalty_amount,
        v_total_water_bill, v_scf_amount, v_arrears_amount, v_total_amount, 'unpaid', 'unpaid', v_is_initial_billing
    );

    -- ===============================
    -- Return summary
    -- ===============================
    SELECT
        p_concessionaire_id AS concessionaire_id,
        v_prev_reading AS previous_reading,
        p_present_reading AS present_reading,
        v_raw_consumption AS raw_consumption,
        v_billable AS billable_consumption,
        v_water_charge AS water_charge,
        v_tax_amount AS tax_amount,
        v_discount_amount AS discount_amount,
        v_total_water_bill AS total_water_bill,
        v_scf_amount AS scf_amount,
        v_arrears_amount AS arrears_amount,
        v_penalty_amount AS penalty_amount,
        v_total_amount AS grand_total;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_create_payment_master` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_create_payment_master`(
    IN p_receipt_no VARCHAR(100),
    IN p_payor_id INT,
    IN p_payment_date DATE,
    IN p_notes TEXT
)
BEGIN
    INSERT INTO payment_master (receipt_no, payor_id, payment_date, notes)
    VALUES (p_receipt_no, NULLIF(p_payor_id, 0), IFNULL(p_payment_date, CURDATE()), p_notes);

    SELECT LAST_INSERT_ID() AS master_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_import_billing_requests` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_import_billing_requests`(
    IN p_from_bill VARCHAR(50),
    IN p_to_bill   VARCHAR(50)
)
BEGIN
    DECLARE done INT DEFAULT 0;

    DECLARE v_concessionaire_id INT;
    DECLARE v_present_reading INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_free_water INT;
    DECLARE v_request_id VARCHAR(200);
    DECLARE v_user_id INT;
    DECLARE v_billing_date DATE;
    DECLARE v_is_initial TINYINT;

    DECLARE v_force_initial TINYINT;

    DECLARE v_success_count INT DEFAULT 0;
    DECLARE v_failed_count INT DEFAULT 0;
    DECLARE v_skipped_count INT DEFAULT 0;
    DECLARE v_error INT DEFAULT 0;

    DECLARE cur CURSOR FOR
        SELECT 
            concessionaire_id,
            present_reading,
            bill_number,
            free_water,
            request_id,
            user_id,
            billing_date,
            is_initial
        FROM `water_district_billing_system_db_text_march_billing`.`v_billing_request`
        WHERE bill_number BETWEEN p_from_bill AND p_to_bill
        ORDER BY bill_number;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO 
            v_concessionaire_id,
            v_present_reading,
            v_bill_number,
            v_free_water,
            v_request_id,
            v_user_id,
            v_billing_date,
            v_is_initial;

        IF done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_force_initial = CASE 
            WHEN COALESCE(v_is_initial, 0) = 1 THEN 1
            ELSE 3
        END;

        IF v_bill_number IS NULL OR TRIM(v_bill_number) = '' THEN
            SET v_failed_count = v_failed_count + 1;

        ELSEIF EXISTS (
            SELECT 1
            FROM `wdbs_tubungan_db`.`billing`
            WHERE bill_number = v_bill_number
               OR request_id = v_request_id
        ) THEN
            SET v_skipped_count = v_skipped_count + 1;

        ELSE
            SET v_error = 0;

            BEGIN
                DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                BEGIN
                    SET v_error = 1;
                END;

                CALL `wdbs_tubungan_db`.`sp_create_bill_v46`(
                    v_concessionaire_id,
                    v_present_reading,
                    v_bill_number,
                    v_free_water,
                    v_request_id,
                    v_user_id,
                    v_force_initial,
                    v_billing_date
                );
            END;

            IF v_error = 1 THEN
                SET v_failed_count = v_failed_count + 1;
            ELSE
                SET v_success_count = v_success_count + 1;
            END IF;
        END IF;

    END LOOP;

    CLOSE cur;

    SELECT 
        v_success_count AS success_count,
        v_failed_count AS failed_count,
        v_skipped_count AS skipped_count,
        CASE 
            WHEN v_failed_count = 0 THEN 'ALL SUCCESS'
            WHEN v_success_count = 0 THEN 'ALL FAILED'
            ELSE 'PARTIAL SUCCESS'
        END AS status;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_import_collection_requests` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_import_collection_requests`(
    IN p_from_payment_id INT,
    IN p_to_payment_id INT
)
BEGIN
    DECLARE done INT DEFAULT 0;

    DECLARE v_source_payment_id INT;
    DECLARE v_billing_id INT;
    DECLARE v_concessionaire_id INT;
    DECLARE v_concessionaire_code VARCHAR(50);
    DECLARE v_concessionaire_name VARCHAR(255);
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_payment_date DATETIME;
    DECLARE v_amount_paid DECIMAL(12,2);
    DECLARE v_scf_paid DECIMAL(12,2);
    DECLARE v_other_payment DECIMAL(12,2);
    DECLARE v_amount_received DECIMAL(12,2);
    DECLARE v_payment_type VARCHAR(50);
    DECLARE v_reference_no VARCHAR(100);
    DECLARE v_remarks TEXT;
    DECLARE v_payor_name VARCHAR(255);
    DECLARE v_or_number VARCHAR(50);

    DECLARE v_success_count INT DEFAULT 0;
    DECLARE v_failed_count INT DEFAULT 0;
    DECLARE v_skipped_count INT DEFAULT 0;
    DECLARE v_error INT DEFAULT 0;

    DECLARE cur CURSOR FOR
        SELECT
            source_payment_id,
            billing_id,
            concessionaire_id,
            concessionaire_code,
            concessionaire_name,
            bill_number,
            payment_date,
            amount_paid,
            scf_paid,
            other_payment,
            amount_received,
            payment_type,
            reference_no,
            remarks,
            payor_name,
            or_number
        FROM `water_district_billing_system_db_text`.`v_collection_request`
        WHERE source_payment_id BETWEEN p_from_payment_id AND p_to_payment_id
        ORDER BY source_payment_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    IF p_from_payment_id IS NULL OR p_to_payment_id IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'from and to payment ids are required';
    END IF;

    IF p_from_payment_id > p_to_payment_id THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'from payment id cannot be greater than to payment id';
    END IF;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_source_payment_id,
            v_billing_id,
            v_concessionaire_id,
            v_concessionaire_code,
            v_concessionaire_name,
            v_bill_number,
            v_payment_date,
            v_amount_paid,
            v_scf_paid,
            v_other_payment,
            v_amount_received,
            v_payment_type,
            v_reference_no,
            v_remarks,
            v_payor_name,
            v_or_number;

        IF done = 1 THEN
            LEAVE read_loop;
        END IF;

        IF v_concessionaire_id IS NULL OR v_bill_number IS NULL OR TRIM(v_bill_number) = '' THEN
            SET v_failed_count = v_failed_count + 1;

        ELSEIF EXISTS (
            SELECT 1
            FROM `wdbs_tubungan_db`.`collection`
            WHERE or_number = v_or_number
        ) THEN
            SET v_skipped_count = v_skipped_count + 1;

        ELSE
            SET v_error = 0;

            BEGIN
                DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                BEGIN
                    SET v_error = 1;
                END;

                CALL `wdbs_tubungan_db`.`sp_make_collection_v47`(
                    v_or_number,
                    v_payment_date,
                    v_payment_type,
                    v_reference_no,
                    CONCAT(
                        COALESCE(v_remarks, ''),
                        CASE
                            WHEN COALESCE(v_remarks, '') = '' THEN ''
                            ELSE ' | '
                        END,
                        'Migrated from old payment_id ',
                        v_source_payment_id
                    ),
                    v_payor_name,
                    v_amount_received,
                    v_other_payment,
                    3,
                    JSON_ARRAY(v_concessionaire_id)
                );
            END;

            IF v_error = 1 THEN
                SET v_failed_count = v_failed_count + 1;
            ELSE
                SET v_success_count = v_success_count + 1;
            END IF;
        END IF;

    END LOOP;

    CLOSE cur;

    SELECT
        v_success_count AS success_count,
        v_failed_count AS failed_count,
        v_skipped_count AS skipped_count,
        CASE
            WHEN v_failed_count = 0 AND v_success_count > 0 THEN 'ALL SUCCESS'
            WHEN v_success_count = 0 AND v_failed_count > 0 THEN 'ALL FAILED'
            ELSE 'PARTIAL SUCCESS'
        END AS status;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_import_collection_requests_clean_only` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_import_collection_requests_clean_only`(
    IN p_from_or VARCHAR(50),
    IN p_to_or VARCHAR(50)
)
sp_main: BEGIN
    DECLARE done INT DEFAULT 0;

    DECLARE v_from_or_num BIGINT UNSIGNED DEFAULT 0;
    DECLARE v_to_or_num BIGINT UNSIGNED DEFAULT 0;

    DECLARE v_source_collection_id INT;
    DECLARE v_or_number VARCHAR(50);
    DECLARE v_collection_date DATETIME;
    DECLARE v_payment_type VARCHAR(100);
    DECLARE v_reference_no VARCHAR(100);
    DECLARE v_remarks TEXT;
    DECLARE v_payor_name VARCHAR(255);
    DECLARE v_amount_received DECIMAL(12,2);
    DECLARE v_csv LONGTEXT;
    DECLARE v_json LONGTEXT;
    DECLARE v_concessionaire_name VARCHAR(255);

    DECLARE v_success INT DEFAULT 0;
    DECLARE v_failed INT DEFAULT 0;
    DECLARE v_skipped INT DEFAULT 0;
    DECLARE v_error INT DEFAULT 0;

    DECLARE cur CURSOR FOR
        SELECT
            source_collection_id,
            or_number,
            collection_date,
            payment_type,
            reference_no,
            remarks,
            payor_name,
            amount_received,
            concessionaire_ids_csv,
            concessionaire_name
        FROM `water_district_billing_system_db_text`.`v_collection_request_clean`
        WHERE or_number REGEXP '^[0-9]+$'
          AND CAST(or_number AS UNSIGNED) BETWEEN v_from_or_num AND v_to_or_num
        ORDER BY CAST(or_number AS UNSIGNED) ASC, source_collection_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    IF p_from_or IS NULL OR TRIM(p_from_or) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'from OR required';
    END IF;

    IF p_to_or IS NULL OR TRIM(p_to_or) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'to OR required';
    END IF;

    IF p_from_or NOT REGEXP '^[0-9]+$' OR p_to_or NOT REGEXP '^[0-9]+$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR must be numeric';
    END IF;

    SET v_from_or_num = CAST(p_from_or AS UNSIGNED);
    SET v_to_or_num = CAST(p_to_or AS UNSIGNED);

    IF v_from_or_num > v_to_or_num THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid OR range';
    END IF;

    START TRANSACTION;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_source_collection_id,
            v_or_number,
            v_collection_date,
            v_payment_type,
            v_reference_no,
            v_remarks,
            v_payor_name,
            v_amount_received,
            v_csv,
            v_concessionaire_name;

        IF done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_error = 0;

        IF v_or_number IS NULL OR TRIM(v_or_number) = '' THEN
            SET v_failed = v_failed + 1;

        ELSE
            SET v_or_number = LPAD(TRIM(v_or_number), 7, '0');

            IF EXISTS (
                SELECT 1
                FROM `wdbs_tubungan_db`.`collection`
                WHERE or_number = v_or_number
            ) THEN
                SET v_skipped = v_skipped + 1;

            ELSEIF v_csv IS NULL OR TRIM(v_csv) = '' THEN
                SET v_skipped = v_skipped + 1;

            ELSE
                SET v_json = CONCAT('[', v_csv, ']');

                IF JSON_VALID(v_json) = 0 THEN
                    SET v_failed = v_failed + 1;

                ELSE
                    BEGIN
                        DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                        BEGIN
                            SET v_error = 1;
                        END;

                        CALL `wdbs_tubungan_db`.`sp_make_collection_v48`(
                            v_or_number,
                            COALESCE(v_collection_date, NOW()),
                            COALESCE(NULLIF(TRIM(v_payment_type), ''), 'UNKNOWN'),
                            COALESCE(NULLIF(TRIM(v_reference_no), ''), ''),
                            COALESCE(v_remarks, ''),
                            COALESCE(NULLIF(TRIM(v_payor_name), ''), COALESCE(v_concessionaire_name, 'MIGRATED')),
                            COALESCE(v_amount_received, 0.00),
                            0.00,
                            3,
                            CAST(v_json AS JSON)
                        );
                    END;

                    IF v_error = 1 THEN
                        SET v_failed = v_failed + 1;
                    ELSE
                        SET v_success = v_success + 1;
                    END IF;
                END IF;
            END IF;
        END IF;
    END LOOP;

    CLOSE cur;

    COMMIT;

    SELECT
        v_success AS success_count,
        v_failed AS failed_count,
        v_skipped AS skipped_count,
        CASE
            WHEN v_failed = 0 AND v_success > 0 THEN 'ALL SUCCESS'
            WHEN v_success = 0 AND v_failed > 0 THEN 'ALL FAILED'
            ELSE 'PARTIAL SUCCESS'
        END AS status;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_import_collection_requests_clean_only_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_import_collection_requests_clean_only_v48`(
    IN p_from_or VARCHAR(50),
    IN p_to_or VARCHAR(50)
)
sp_main: BEGIN
    DECLARE done INT DEFAULT 0;

    DECLARE v_from_or_num BIGINT UNSIGNED DEFAULT 0;
    DECLARE v_to_or_num BIGINT UNSIGNED DEFAULT 0;

    DECLARE v_source_collection_id INT;
    DECLARE v_or_number VARCHAR(50);
    DECLARE v_collection_date DATETIME;
    DECLARE v_payment_type VARCHAR(100);
    DECLARE v_reference_no VARCHAR(100);
    DECLARE v_remarks TEXT;
    DECLARE v_payor_name VARCHAR(255);
    DECLARE v_amount_received DECIMAL(12,2);
    DECLARE v_total_others DECIMAL(12,2);
    DECLARE v_csv LONGTEXT;
    DECLARE v_json LONGTEXT;
    DECLARE v_concessionaire_name VARCHAR(255);

    DECLARE v_success INT DEFAULT 0;
    DECLARE v_failed INT DEFAULT 0;
    DECLARE v_skipped INT DEFAULT 0;
    DECLARE v_error INT DEFAULT 0;

    DECLARE cur CURSOR FOR
        SELECT
            source_collection_id,
            or_number,
            collection_date,
            payment_type,
            reference_no,
            remarks,
            payor_name,
            amount_received,
            total_others,
            concessionaire_ids_csv,
            concessionaire_name
        FROM `water_district_billing_system_db_text`.`v_collection_request_clean`
        WHERE or_number REGEXP '^[0-9]+$'
          AND CAST(or_number AS UNSIGNED) BETWEEN v_from_or_num AND v_to_or_num
        ORDER BY CAST(or_number AS UNSIGNED) ASC, source_collection_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    IF p_from_or IS NULL OR TRIM(p_from_or) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'from OR required';
    END IF;

    IF p_to_or IS NULL OR TRIM(p_to_or) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'to OR required';
    END IF;

    IF p_from_or NOT REGEXP '^[0-9]+$' OR p_to_or NOT REGEXP '^[0-9]+$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR must be numeric';
    END IF;

    SET v_from_or_num = CAST(p_from_or AS UNSIGNED);
    SET v_to_or_num = CAST(p_to_or AS UNSIGNED);

    IF v_from_or_num > v_to_or_num THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid OR range';
    END IF;

    START TRANSACTION;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_source_collection_id,
            v_or_number,
            v_collection_date,
            v_payment_type,
            v_reference_no,
            v_remarks,
            v_payor_name,
            v_amount_received,
            v_total_others,
            v_csv,
            v_concessionaire_name;

        IF done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_error = 0;

        IF v_or_number IS NULL OR TRIM(v_or_number) = '' THEN
            SET v_failed = v_failed + 1;

        ELSE
            SET v_or_number = LPAD(TRIM(v_or_number), 7, '0');

            IF EXISTS (
                SELECT 1
                FROM `wdbs_tubungan_db`.`collection`
                WHERE or_number = v_or_number
            ) THEN
                SET v_skipped = v_skipped + 1;

            ELSE
                SET v_csv = REPLACE(COALESCE(TRIM(v_csv), ''), ' ', '');

                IF v_csv IS NULL OR v_csv = '' THEN
                    SET v_skipped = v_skipped + 1;

                ELSE
                    SET v_json = CONCAT('[', v_csv, ']');

                    IF JSON_VALID(v_json) = 0 THEN
                        SET v_failed = v_failed + 1;

                    ELSE
                        BEGIN
                            DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                            BEGIN
                                SET v_error = 1;
                            END;

                            CALL `wdbs_tubungan_db`.`sp_make_collection_v48`(
                                v_or_number,
                                COALESCE(v_collection_date, NOW()),
                                COALESCE(NULLIF(TRIM(v_payment_type), ''), 'UNKNOWN'),
                                COALESCE(NULLIF(TRIM(v_reference_no), ''), ''),
                                COALESCE(v_remarks, ''),
                                COALESCE(NULLIF(TRIM(v_payor_name), ''), COALESCE(v_concessionaire_name, 'MIGRATED')),
                                COALESCE(v_amount_received, 0.00),
                                COALESCE(v_total_others, 0.00),
                                3,
                                CAST(v_json AS JSON)
                            );
                        END;

                        IF v_error = 1 THEN
                            SET v_failed = v_failed + 1;
                        ELSE
                            SET v_success = v_success + 1;
                        END IF;
                    END IF;
                END IF;
            END IF;
        END IF;
    END LOOP;

    CLOSE cur;

    COMMIT;

    SELECT
        v_success AS success_count,
        v_failed AS failed_count,
        v_skipped AS skipped_count,
        CASE
            WHEN v_failed = 0 AND v_success > 0 THEN 'ALL SUCCESS'
            WHEN v_success = 0 AND v_failed > 0 THEN 'ALL FAILED'
            ELSE 'PARTIAL SUCCESS'
        END AS status;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_import_collection_requests_v2` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_import_collection_requests_v2`(
    IN p_from_collection_id INT,
    IN p_to_collection_id INT
)
sp_main: BEGIN
    DECLARE done_bill INT DEFAULT 0;
    DECLARE done_scf INT DEFAULT 0;

    DECLARE v_source_collection_id INT;
    DECLARE v_or_number VARCHAR(50);
    DECLARE v_collection_date DATETIME;
    DECLARE v_payment_type VARCHAR(50);
    DECLARE v_reference_no VARCHAR(100);
    DECLARE v_remarks TEXT;
    DECLARE v_payor_name VARCHAR(255);
    DECLARE v_amount_received DECIMAL(12,2);
    DECLARE v_total_others DECIMAL(12,2);
    DECLARE v_bill_numbers LONGTEXT;
    DECLARE v_concessionaire_ids_json JSON;

    DECLARE v_concessionaire_id INT;
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_others_amount DECIMAL(12,2);

    DECLARE v_success_count INT DEFAULT 0;
    DECLARE v_failed_count INT DEFAULT 0;
    DECLARE v_skipped_count INT DEFAULT 0;

    DECLARE v_error INT DEFAULT 0;

    DECLARE cur_bill CURSOR FOR
        SELECT
            source_collection_id,
            or_number,
            collection_date,
            payment_type,
            reference_no,
            remarks,
            payor_name,
            amount_received,
            total_others,
            bill_numbers,
            concessionaire_ids_json
        FROM `water_district_billing_system_db_text`.`v_collection_request_clean`
        WHERE source_collection_id BETWEEN p_from_collection_id AND p_to_collection_id
        ORDER BY source_collection_id ASC;

    DECLARE cur_scf CURSOR FOR
        SELECT
            source_collection_id,
            or_number,
            collection_date,
            payment_type,
            reference_no,
            remarks,
            payor_name,
            concessionaire_id,
            scf_amount,
            others_amount,
            amount_received
        FROM `water_district_billing_system_db_text`.`v_collection_request_scf`
        WHERE source_collection_id BETWEEN p_from_collection_id AND p_to_collection_id
        ORDER BY source_collection_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND
    BEGIN
        IF done_bill = 0 THEN
            SET done_bill = 1;
        ELSE
            SET done_scf = 1;
        END IF;
    END;

    IF p_from_collection_id IS NULL OR p_to_collection_id IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'from and to collection ids are required';
    END IF;

    IF p_from_collection_id > p_to_collection_id THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'from collection id cannot be greater than to collection id';
    END IF;

    /* =========================
       BILL-LINKED COLLECTIONS
       ========================= */
    OPEN cur_bill;

    bill_loop: LOOP
        FETCH cur_bill INTO
            v_source_collection_id,
            v_or_number,
            v_collection_date,
            v_payment_type,
            v_reference_no,
            v_remarks,
            v_payor_name,
            v_amount_received,
            v_total_others,
            v_bill_numbers,
            v_concessionaire_ids_json;

        IF done_bill = 1 THEN
            LEAVE bill_loop;
        END IF;

        IF v_or_number IS NULL OR TRIM(v_or_number) = '' THEN
            SET v_failed_count = v_failed_count + 1;

        ELSEIF EXISTS (
            SELECT 1
            FROM `wdbs_tubungan_db`.`collection`
            WHERE or_number = v_or_number
        ) THEN
            SET v_skipped_count = v_skipped_count + 1;

        ELSEIF v_concessionaire_ids_json IS NULL OR JSON_LENGTH(v_concessionaire_ids_json) = 0 THEN
            SET v_failed_count = v_failed_count + 1;

        ELSE
            SET v_error = 0;

            BEGIN
                DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                BEGIN
                    SET v_error = 1;
                END;

                CALL `wdbs_tubungan_db`.`sp_make_collection_v47`(
                    v_or_number,
                    v_collection_date,
                    v_payment_type,
                    v_reference_no,
                    CONCAT(
                        COALESCE(v_remarks, ''),
                        CASE
                            WHEN COALESCE(v_remarks, '') = '' THEN ''
                            ELSE ' | '
                        END,
                        'Migrated from old collection_id ',
                        v_source_collection_id
                    ),
                    COALESCE(NULLIF(TRIM(v_payor_name), ''), 'MIGRATED PAYOR'),
                    COALESCE(v_amount_received, 0.00),
                    COALESCE(v_total_others, 0.00),
                    3,
                    v_concessionaire_ids_json
                );
            END;

            IF v_error = 1 THEN
                SET v_failed_count = v_failed_count + 1;
            ELSE
                SET v_success_count = v_success_count + 1;
            END IF;
        END IF;
    END LOOP;

    CLOSE cur_bill;

    /* =========================
       SCF-ONLY COLLECTIONS
       ========================= */
    OPEN cur_scf;

    scf_loop: LOOP
        FETCH cur_scf INTO
            v_source_collection_id,
            v_or_number,
            v_collection_date,
            v_payment_type,
            v_reference_no,
            v_remarks,
            v_payor_name,
            v_concessionaire_id,
            v_scf_amount,
            v_others_amount,
            v_amount_received;

        IF done_scf = 1 THEN
            LEAVE scf_loop;
        END IF;

        IF v_or_number IS NULL OR TRIM(v_or_number) = '' THEN
            SET v_failed_count = v_failed_count + 1;

        ELSEIF EXISTS (
            SELECT 1
            FROM `wdbs_tubungan_db`.`collection`
            WHERE or_number = v_or_number
        ) THEN
            SET v_skipped_count = v_skipped_count + 1;

        ELSEIF v_concessionaire_id IS NULL OR v_concessionaire_id <= 0 THEN
            SET v_failed_count = v_failed_count + 1;

        ELSE
            SET v_error = 0;

            BEGIN
                DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
                BEGIN
                    SET v_error = 1;
                END;

                CALL `wdbs_tubungan_db`.`sp_make_scf_collection_v46`(
                    v_or_number,
                    v_collection_date,
                    v_payment_type,
                    v_reference_no,
                    CONCAT(
                        COALESCE(v_remarks, ''),
                        CASE
                            WHEN COALESCE(v_remarks, '') = '' THEN ''
                            ELSE ' | '
                        END,
                        'Migrated from old collection_id ',
                        v_source_collection_id
                    ),
                    COALESCE(v_scf_amount, 0.00),
                    COALESCE(v_others_amount, 0.00),
                    3,
                    v_concessionaire_id,
                    COALESCE(NULLIF(TRIM(v_payor_name), ''), 'MIGRATED PAYOR')
                );
            END;

            IF v_error = 1 THEN
                SET v_failed_count = v_failed_count + 1;
            ELSE
                SET v_success_count = v_success_count + 1;
            END IF;
        END IF;
    END LOOP;

    CLOSE cur_scf;

    SELECT
        v_success_count AS success_count,
        v_failed_count AS failed_count,
        v_skipped_count AS skipped_count,
        CASE
            WHEN v_failed_count = 0 AND v_success_count > 0 THEN 'ALL SUCCESS'
            WHEN v_success_count = 0 AND v_failed_count > 0 THEN 'ALL FAILED'
            ELSE 'PARTIAL SUCCESS'
        END AS status;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_collection_v46` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_collection_v46`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_pay_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_scf DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_cash DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_paid_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_generated_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count_loaded INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_payment_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;
    DECLARE v_json_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_payment_ids LONGTEXT DEFAULT '';

    DECLARE v_selected_code VARCHAR(50) DEFAULT '';
    DECLARE v_selected_name VARCHAR(255) DEFAULT '';
    DECLARE v_selected_address VARCHAR(255) DEFAULT '';

    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_row_paid DECIMAL(12,2) DEFAULT 0.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC
        FOR UPDATE;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_remarks = COALESCE(p_remarks, '');

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR required';
    END IF;

    IF EXISTS (SELECT 1 FROM collection WHERE or_number = p_or_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF p_amount_received <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid amount received';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_others > p_amount_received THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others cannot be greater than amount received';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    IF UPPER(TRIM(COALESCE(p_payment_type, ''))) <> 'CASH'
       AND (p_reference_no IS NULL OR TRIM(p_reference_no) = '') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Reference number is required for non-cash payments';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    START TRANSACTION;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*), JSON_LENGTH(p_json_concessionaires)
    INTO v_selected_count, v_json_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    IF v_selected_count <> v_json_count THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'One or more concessionaires are invalid or duplicated';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count_loaded
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    IF v_selected_count = 1 THEN
        SELECT concessionaire_code, concessionaire_name, address
        INTO v_selected_code, v_selected_name, v_selected_address
        FROM tmp_selected
        LIMIT 1;
    ELSE
        SET v_selected_code = 'MULTIPLE';
        SET v_selected_name = 'MULTIPLE';
        SET v_selected_address = 'MULTIPLE';
    END IF;

    IF v_bill_count_loaded = 0 AND p_others <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No unpaid billing found for the selected concessionaires';
    END IF;

    INSERT INTO collection (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        created_by,
        created_by_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount,
        status,
        voided_by_user_id,
        voided_at,
        billing_count,
        payment_count
    )
    VALUES (
        p_or_number,
        p_payment_date,
        '',
        '',
        '',
        '',
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        p_others,
        0.00,
        p_amount_received,
        0.00,
        0.00,
        p_user_id,
        '',
        p_remarks,
        NOW(),
        NOW(),
        p_payment_type,
        COALESCE(p_reference_no, ''),
        '',
        0.00,
        'POSTED',
        NULL,
        NULL,
        0,
        0
    );

    SET v_collection_id = LAST_INSERT_ID();
    SET v_cash = ROUND(p_amount_received - p_others, 2);

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND GREATEST(v_rem_water, 0) > 0 THEN

            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
            SET v_penalty_generated_total = ROUND(v_penalty_generated_total + v_generated_penalty, 2);

            SET v_rem_penalty = ROUND(v_rem_penalty + v_generated_penalty, 2);
            SET v_rem_balance = ROUND(v_rem_balance + v_generated_penalty, 2);

            UPDATE billing b
            SET
                penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + v_generated_penalty, 2),
                remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + v_generated_penalty, 2),
                remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + v_generated_penalty, 2),
                is_penalty_applied = 1,
                penalty_applied_at = NOW(),
                status = 'overdue',
                updated_at = NOW(),
                updated_by_user_id = p_user_id
            WHERE b.billing_id = v_bill_id;
        END IF;

        SET v_pay_penalty = 0.00;
        SET v_pay_water = 0.00;
        SET v_pay_tax = 0.00;
        SET v_pay_scf = 0.00;

        IF v_cash > 0 THEN
            SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
            SET v_cash = ROUND(v_cash - v_pay_penalty, 2);

            SET v_pay_water = LEAST(v_cash, v_rem_water);
            SET v_cash = ROUND(v_cash - v_pay_water, 2);

            SET v_pay_tax = LEAST(v_cash, v_rem_tax);
            SET v_cash = ROUND(v_cash - v_pay_tax, 2);

            SET v_pay_scf = LEAST(v_cash, v_rem_scf);
            SET v_cash = ROUND(v_cash - v_pay_scf, 2);
        END IF;

        SET v_new_penalty = ROUND(GREATEST(v_rem_penalty - v_pay_penalty, 0), 2);
        SET v_new_water   = ROUND(GREATEST(v_rem_water - v_pay_water, 0), 2);
        SET v_new_tax     = ROUND(GREATEST(v_rem_tax - v_pay_tax, 0), 2);
        SET v_new_scf     = ROUND(GREATEST(v_rem_scf - v_pay_scf, 0), 2);
        SET v_new_balance = ROUND(v_new_penalty + v_new_water + v_new_tax + v_new_scf, 2);

        SET v_row_paid = ROUND(v_pay_penalty + v_pay_water + v_pay_tax + v_pay_scf, 2);

        IF v_row_paid > 0 THEN
            UPDATE billing b
            SET
                remaining_penalty_amount = v_new_penalty,
                remaining_water_charge   = v_new_water,
                remaining_tax_amount     = v_new_tax,
                remaining_scf_amount     = v_new_scf,
                remaining_balance        = v_new_balance,
                payment_count            = payment_count + 1,
                last_collection_id       = v_collection_id,
                last_payment_date        = p_payment_date,
                paid_at                  = CASE
                                                WHEN v_new_balance = 0 THEN p_payment_date
                                                ELSE paid_at
                                            END,
                status                   = CASE
                                                WHEN v_new_balance = 0 THEN 'paid'
                                                WHEN v_bill_due_date IS NOT NULL AND DATE(v_bill_due_date) < DATE(p_payment_date) THEN 'overdue'
                                                ELSE 'partially_paid'
                                           END,
                scf_status               = CASE
                                                WHEN v_new_scf = 0 THEN 'paid'
                                                WHEN v_pay_scf > 0 THEN 'partially_paid'
                                                ELSE scf_status
                                           END,
                updated_at               = NOW(),
                updated_by_user_id       = p_user_id
            WHERE b.billing_id = v_bill_id;

            INSERT INTO payment (
                billing_id,
                billing_number,
                amount_paid,
                balance,
                scf_paid,
                scf_balance,
                payment_date,
                other_payment,
                late_penalty,
                arrears_penalty,
                arrears_tax,
                payment_type,
                bank_number,
                remarks,
                collection_id,
                water_paid,
                tax_paid,
                arrears_paid,
                penalty_paid,
                other_paid,
                remaining_water_after,
                remaining_balance_after,
                status
            )
            VALUES (
                v_bill_id,
                v_bill_number,
                ROUND(v_row_paid, 2),
                v_new_balance,
                v_pay_scf,
                v_new_scf,
                p_payment_date,
                0.00,
                v_pay_penalty,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_penalty ELSE 0.00 END,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_tax ELSE 0.00 END,
                p_payment_type,
                COALESCE(p_reference_no, ''),
                p_remarks,
                v_collection_id,
                v_pay_water,
                v_pay_tax,
                CASE WHEN v_bill_id <> v_current_bill_id THEN ROUND(v_pay_penalty + v_pay_water + v_pay_tax, 2) ELSE 0.00 END,
                v_pay_penalty,
                0.00,
                v_new_water,
                v_new_balance,
                'POSTED'
            );

            SET v_processed_bill_count = v_processed_bill_count + 1;
            SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

            IF v_bill_id = v_current_bill_id THEN
                SET v_total_current = ROUND(v_total_current + v_pay_water, 2);
            ELSE
                SET v_total_arrears = ROUND(v_total_arrears + v_pay_water, 2);
            END IF;

            SET v_total_penalty = ROUND(v_total_penalty + v_pay_penalty, 2);
            SET v_total_tax = ROUND(v_total_tax + v_pay_tax, 2);
            SET v_total_scf = ROUND(v_total_scf + v_pay_scf, 2);
            SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);

            SET v_payment_count = v_payment_count + 1;
            SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        END IF;
    END LOOP;

    CLOSE cur;

    IF p_others > 0 THEN
        INSERT INTO payment (
            billing_id,
            billing_number,
            amount_paid,
            balance,
            scf_paid,
            scf_balance,
            payment_date,
            other_payment,
            late_penalty,
            arrears_penalty,
            arrears_tax,
            payment_type,
            bank_number,
            remarks,
            collection_id,
            water_paid,
            tax_paid,
            arrears_paid,
            penalty_paid,
            other_paid,
            remaining_water_after,
            remaining_balance_after,
            status
        )
        VALUES (
            NULL,
            NULL,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_date,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_type,
            COALESCE(p_reference_no, ''),
            p_remarks,
            v_collection_id,
            0.00,
            0.00,
            0.00,
            0.00,
            p_others,
            0.00,
            0.00,
            'POSTED'
        );

        SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        SET v_payment_count = v_payment_count + 1;
    END IF;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_total_paid_amount = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);
    SET v_grand_total = v_total_paid_amount;
    SET v_change_amount = ROUND(p_amount_received - v_grand_total, 2);

    UPDATE collection
    SET
        bill_numbers          = v_bill_numbers,
        concessionaire_code   = v_selected_code,
        concessionaire_name   = v_selected_name,
        address               = v_selected_address,
        total_current_bill    = ROUND(v_total_current, 2),
        total_arrears         = ROUND(v_total_arrears, 2),
        total_penalty         = ROUND(v_total_penalty, 2),
        total_tax             = ROUND(v_total_tax, 2),
        total_scf             = ROUND(v_total_scf, 2),
        total_water_bill_paid = ROUND(v_total_water_bill_paid, 2),
        scf_paid              = ROUND(v_total_scf, 2),
        total_others          = ROUND(p_others, 2),
        grand_total           = ROUND(v_grand_total, 2),
        amount_received       = ROUND(p_amount_received, 2),
        change_amount         = ROUND(v_change_amount, 2),
        total_paid_amount     = ROUND(v_total_paid_amount, 2),
        created_by_name       = '',
        remarks               = p_remarks,
        updated_at            = NOW(),
        payment_type          = p_payment_type,
        payment_reference     = COALESCE(p_reference_no, ''),
        payment_ids           = v_payment_ids,
        total_discount        = ROUND(v_total_discount, 2),
        status                = 'POSTED',
        billing_count         = v_processed_bill_count,
        payment_count         = v_payment_count
    WHERE collection_id = v_collection_id;

    COMMIT;

    SELECT
        v_collection_id AS collection_id,
        p_or_number AS or_number,
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(v_total_scf, 2) AS scf_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_grand_total, 2) AS grand_total,
        ROUND(p_amount_received, 2) AS amount_received,
        ROUND(v_change_amount, 2) AS change_amount,
        ROUND(v_total_paid_amount, 2) AS total_paid_amount,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        v_payment_count AS payment_count,
        v_payment_ids AS payment_ids,
        ROUND(v_penalty_generated_total, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_collection_v47` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_collection_v47`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_pay_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_scf DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_cash DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_paid_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_generated_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count_loaded INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_payment_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;
    DECLARE v_json_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_payment_ids LONGTEXT DEFAULT '';

    DECLARE v_selected_code VARCHAR(50) DEFAULT '';
    DECLARE v_selected_name VARCHAR(255) DEFAULT '';
    DECLARE v_selected_address VARCHAR(255) DEFAULT '';
    DECLARE v_payor_name VARCHAR(255) DEFAULT '';

    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_row_paid DECIMAL(12,2) DEFAULT 0.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC
        FOR UPDATE;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR required';
    END IF;

    IF EXISTS (SELECT 1 FROM collection WHERE or_number = p_or_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF p_amount_received <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid amount received';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_others > p_amount_received THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others cannot be greater than amount received';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    IF UPPER(TRIM(COALESCE(p_payment_type, ''))) <> 'CASH'
       AND (p_reference_no IS NULL OR TRIM(p_reference_no) = '') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Reference number is required for non-cash payments';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    START TRANSACTION;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*), JSON_LENGTH(p_json_concessionaires)
    INTO v_selected_count, v_json_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    IF v_selected_count <> v_json_count THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'One or more concessionaires are invalid or duplicated';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count_loaded
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    IF v_selected_count = 1 THEN
        SELECT concessionaire_code, concessionaire_name, address
        INTO v_selected_code, v_selected_name, v_selected_address
        FROM tmp_selected
        LIMIT 1;
    ELSE
        SET v_selected_code = 'MULTIPLE';
        SET v_selected_name = 'MULTIPLE';
        SET v_selected_address = 'MULTIPLE';
    END IF;

    IF v_bill_count_loaded = 0 AND p_others <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No unpaid billing found for the selected concessionaires';
    END IF;

    INSERT INTO collection (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        created_by,
        created_by_name,
        payor_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount,
        status,
        voided_by_user_id,
        voided_at,
        billing_count,
        payment_count
    )
    VALUES (
        p_or_number,
        p_payment_date,
        '',
        '',
        '',
        '',
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        p_others,
        0.00,
        p_amount_received,
        0.00,
        0.00,
        p_user_id,
        '',
        p_payor_name,
        p_remarks,
        NOW(),
        NOW(),
        p_payment_type,
        COALESCE(p_reference_no, ''),
        '',
        0.00,
        'POSTED',
        NULL,
        NULL,
        0,
        0
    );

    SET v_collection_id = LAST_INSERT_ID();
    SET v_cash = ROUND(p_amount_received - p_others, 2);

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND GREATEST(v_rem_water, 0) > 0 THEN

            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
            SET v_penalty_generated_total = ROUND(v_penalty_generated_total + v_generated_penalty, 2);

            SET v_rem_penalty = ROUND(v_rem_penalty + v_generated_penalty, 2);
            SET v_rem_balance = ROUND(v_rem_balance + v_generated_penalty, 2);

            UPDATE billing b
            SET
                penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + v_generated_penalty, 2),
                remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + v_generated_penalty, 2),
                remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + v_generated_penalty, 2),
                is_penalty_applied = 1,
                penalty_applied_at = NOW(),
                status = 'overdue',
                updated_at = NOW(),
                updated_by_user_id = p_user_id
            WHERE b.billing_id = v_bill_id;
        END IF;

        SET v_pay_penalty = 0.00;
        SET v_pay_water = 0.00;
        SET v_pay_tax = 0.00;
        SET v_pay_scf = 0.00;

        IF v_cash > 0 THEN
            SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
            SET v_cash = ROUND(v_cash - v_pay_penalty, 2);

            SET v_pay_water = LEAST(v_cash, v_rem_water);
            SET v_cash = ROUND(v_cash - v_pay_water, 2);

            SET v_pay_tax = LEAST(v_cash, v_rem_tax);
            SET v_cash = ROUND(v_cash - v_pay_tax, 2);

            SET v_pay_scf = LEAST(v_cash, v_rem_scf);
            SET v_cash = ROUND(v_cash - v_pay_scf, 2);
        END IF;

        SET v_new_penalty = ROUND(GREATEST(v_rem_penalty - v_pay_penalty, 0), 2);
        SET v_new_water   = ROUND(GREATEST(v_rem_water - v_pay_water, 0), 2);
        SET v_new_tax     = ROUND(GREATEST(v_rem_tax - v_pay_tax, 0), 2);
        SET v_new_scf     = ROUND(GREATEST(v_rem_scf - v_pay_scf, 0), 2);
        SET v_new_balance = ROUND(v_new_penalty + v_new_water + v_new_tax + v_new_scf, 2);

        SET v_row_paid = ROUND(v_pay_penalty + v_pay_water + v_pay_tax + v_pay_scf, 2);

        IF v_row_paid > 0 THEN
            UPDATE billing b
            SET
                remaining_penalty_amount = v_new_penalty,
                remaining_water_charge   = v_new_water,
                remaining_tax_amount     = v_new_tax,
                remaining_scf_amount     = v_new_scf,
                remaining_balance        = v_new_balance,
                payment_count            = payment_count + 1,
                last_collection_id       = v_collection_id,
                last_payment_date        = p_payment_date,
                paid_at                  = CASE
                                                WHEN v_new_balance = 0 THEN p_payment_date
                                                ELSE paid_at
                                            END,
                status                   = CASE
                                                WHEN v_new_balance = 0 THEN 'paid'
                                                WHEN v_bill_due_date IS NOT NULL AND DATE(v_bill_due_date) < DATE(p_payment_date) THEN 'overdue'
                                                ELSE 'partially_paid'
                                           END,
                scf_status               = CASE
                                                WHEN v_new_scf = 0 THEN 'paid'
                                                WHEN v_pay_scf > 0 THEN 'partially_paid'
                                                ELSE scf_status
                                           END,
                updated_at               = NOW(),
                updated_by_user_id       = p_user_id
            WHERE b.billing_id = v_bill_id;

            INSERT INTO payment (
                billing_id,
                billing_number,
                amount_paid,
                balance,
                scf_paid,
                scf_balance,
                payment_date,
                other_payment,
                late_penalty,
                arrears_penalty,
                arrears_tax,
                payment_type,
                bank_number,
                remarks,
                collection_id,
                water_paid,
                tax_paid,
                arrears_paid,
                penalty_paid,
                other_paid,
                remaining_water_after,
                remaining_balance_after,
                status
            )
            VALUES (
                v_bill_id,
                v_bill_number,
                ROUND(v_row_paid, 2),
                v_new_balance,
                v_pay_scf,
                v_new_scf,
                p_payment_date,
                0.00,
                v_pay_penalty,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_penalty ELSE 0.00 END,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_tax ELSE 0.00 END,
                p_payment_type,
                COALESCE(p_reference_no, ''),
                p_remarks,
                v_collection_id,
                v_pay_water,
                v_pay_tax,
                CASE WHEN v_bill_id <> v_current_bill_id THEN ROUND(v_pay_penalty + v_pay_water + v_pay_tax, 2) ELSE 0.00 END,
                v_pay_penalty,
                0.00,
                v_new_water,
                v_new_balance,
                'POSTED'
            );

            SET v_processed_bill_count = v_processed_bill_count + 1;
            SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

            IF v_bill_id = v_current_bill_id THEN
                SET v_total_current = ROUND(v_total_current + v_pay_water, 2);
            ELSE
                SET v_total_arrears = ROUND(v_total_arrears + v_pay_water, 2);
            END IF;

            SET v_total_penalty = ROUND(v_total_penalty + v_pay_penalty, 2);
            SET v_total_tax = ROUND(v_total_tax + v_pay_tax, 2);
            SET v_total_scf = ROUND(v_total_scf + v_pay_scf, 2);
            SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);

            SET v_payment_count = v_payment_count + 1;
            SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        END IF;
    END LOOP;

    CLOSE cur;

    IF p_others > 0 THEN
        INSERT INTO payment (
            billing_id,
            billing_number,
            amount_paid,
            balance,
            scf_paid,
            scf_balance,
            payment_date,
            other_payment,
            late_penalty,
            arrears_penalty,
            arrears_tax,
            payment_type,
            bank_number,
            remarks,
            collection_id,
            water_paid,
            tax_paid,
            arrears_paid,
            penalty_paid,
            other_paid,
            remaining_water_after,
            remaining_balance_after,
            status
        )
        VALUES (
            NULL,
            NULL,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_date,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_type,
            COALESCE(p_reference_no, ''),
            p_remarks,
            v_collection_id,
            0.00,
            0.00,
            0.00,
            0.00,
            p_others,
            0.00,
            0.00,
            'POSTED'
        );

        SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        SET v_payment_count = v_payment_count + 1;
    END IF;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_total_paid_amount = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);
    SET v_grand_total = v_total_paid_amount;
    SET v_change_amount = ROUND(p_amount_received - v_grand_total, 2);

    UPDATE collection
    SET
        bill_numbers          = v_bill_numbers,
        concessionaire_code   = v_selected_code,
        concessionaire_name   = v_selected_name,
        address               = v_selected_address,
        total_current_bill    = ROUND(v_total_current, 2),
        total_arrears         = ROUND(v_total_arrears, 2),
        total_penalty         = ROUND(v_total_penalty, 2),
        total_tax             = ROUND(v_total_tax, 2),
        total_scf             = ROUND(v_total_scf, 2),
        total_water_bill_paid = ROUND(v_total_water_bill_paid, 2),
        scf_paid              = ROUND(v_total_scf, 2),
        total_others          = ROUND(p_others, 2),
        grand_total           = ROUND(v_grand_total, 2),
        amount_received       = ROUND(p_amount_received, 2),
        change_amount         = ROUND(v_change_amount, 2),
        total_paid_amount     = ROUND(v_total_paid_amount, 2),
        created_by_name       = '',
        payor_name            = p_payor_name,
        remarks               = p_remarks,
        updated_at            = NOW(),
        payment_type          = p_payment_type,
        payment_reference     = COALESCE(p_reference_no, ''),
        payment_ids           = v_payment_ids,
        total_discount        = ROUND(v_total_discount, 2),
        status                = 'POSTED',
        billing_count         = v_processed_bill_count,
        payment_count         = v_payment_count
    WHERE collection_id = v_collection_id;

    COMMIT;

    SELECT
        v_collection_id AS collection_id,
        p_or_number AS or_number,
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        p_payor_name AS payor_name,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(v_total_scf, 2) AS scf_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_grand_total, 2) AS grand_total,
        ROUND(p_amount_received, 2) AS amount_received,
        ROUND(v_change_amount, 2) AS change_amount,
        ROUND(v_total_paid_amount, 2) AS total_paid_amount,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        v_payment_count AS payment_count,
        v_payment_ids AS payment_ids,
        ROUND(v_penalty_generated_total, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_collection_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_collection_v48`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_pay_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_scf DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_cash DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_paid_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_uncollected DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_generated_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count_loaded INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_payment_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;
    DECLARE v_json_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_payment_ids LONGTEXT DEFAULT '';

    DECLARE v_selected_code VARCHAR(50) DEFAULT '';
    DECLARE v_selected_name VARCHAR(255) DEFAULT '';
    DECLARE v_selected_address VARCHAR(255) DEFAULT '';

    DECLARE v_payor_name_clean VARCHAR(255) DEFAULT '';
    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_row_paid DECIMAL(12,2) DEFAULT 0.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC
        FOR UPDATE;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');
    SET p_payment_type = COALESCE(TRIM(p_payment_type), '');
    SET v_payor_name_clean = p_payor_name;

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR required';
    END IF;

    IF EXISTS (SELECT 1 FROM collection WHERE or_number = p_or_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF p_amount_received <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid amount received';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_others > p_amount_received THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others cannot be greater than amount received';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    START TRANSACTION;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*), JSON_LENGTH(p_json_concessionaires)
    INTO v_selected_count, v_json_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    IF v_selected_count <> v_json_count THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'One or more concessionaires are invalid or duplicated';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count_loaded
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    IF v_selected_count = 1 THEN
        SELECT concessionaire_code, concessionaire_name, address
        INTO v_selected_code, v_selected_name, v_selected_address
        FROM tmp_selected
        LIMIT 1;
    ELSE
        SET v_selected_code = 'MULTIPLE';
        SET v_selected_name = 'MULTIPLE';
        SET v_selected_address = 'MULTIPLE';
    END IF;

    IF v_bill_count_loaded = 0 AND p_others <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No unpaid billing found for the selected concessionaires';
    END IF;

    INSERT INTO collection (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        payor_name,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        uncollected,
        created_by,
        created_by_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount,
        status,
        voided_by_user_id,
        voided_at,
        billing_count,
        payment_count
    )
    VALUES (
        p_or_number,
        p_payment_date,
        '',
        '',
        '',
        '',
        p_payor_name,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        p_others,
        0.00,
        p_amount_received,
        0.00,
        0.00,
        0.00,
        p_user_id,
        '',
        p_remarks,
        NOW(),
        NOW(),
        p_payment_type,
        COALESCE(p_reference_no, ''),
        '',
        0.00,
        'POSTED',
        NULL,
        NULL,
        0,
        0
    );

    SET v_collection_id = LAST_INSERT_ID();
    SET v_cash = ROUND(p_amount_received - p_others, 2);

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND v_rem_balance > 0 THEN

            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
            SET v_penalty_generated_total = ROUND(v_penalty_generated_total + v_generated_penalty, 2);

            SET v_rem_penalty = ROUND(v_rem_penalty + v_generated_penalty, 2);
            SET v_rem_balance = ROUND(v_rem_balance + v_generated_penalty, 2);

            UPDATE billing b
            SET
                penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + v_generated_penalty, 2),
                remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + v_generated_penalty, 2),
                remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + v_generated_penalty, 2),
                is_penalty_applied = 1,
                penalty_applied_at = NOW(),
                status = 'overdue',
                updated_at = NOW(),
                updated_by_user_id = p_user_id
            WHERE b.billing_id = v_bill_id;
        END IF;

        SET v_pay_penalty = 0.00;
        SET v_pay_water = 0.00;
        SET v_pay_tax = 0.00;
        SET v_pay_scf = 0.00;

        IF v_cash > 0 THEN
            SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
            SET v_cash = ROUND(v_cash - v_pay_penalty, 2);

            SET v_pay_water = LEAST(v_cash, v_rem_water);
            SET v_cash = ROUND(v_cash - v_pay_water, 2);

            SET v_pay_tax = LEAST(v_cash, v_rem_tax);
            SET v_cash = ROUND(v_cash - v_pay_tax, 2);

            SET v_pay_scf = LEAST(v_cash, v_rem_scf);
            SET v_cash = ROUND(v_cash - v_pay_scf, 2);
        END IF;

        SET v_new_penalty = ROUND(GREATEST(v_rem_penalty - v_pay_penalty, 0), 2);
        SET v_new_water   = ROUND(GREATEST(v_rem_water - v_pay_water, 0), 2);
        SET v_new_tax     = ROUND(GREATEST(v_rem_tax - v_pay_tax, 0), 2);
        SET v_new_scf     = ROUND(GREATEST(v_rem_scf - v_pay_scf, 0), 2);
        SET v_new_balance = ROUND(v_new_penalty + v_new_water + v_new_tax + v_new_scf, 2);

        SET v_row_paid = ROUND(v_pay_penalty + v_pay_water + v_pay_tax + v_pay_scf, 2);

        IF v_row_paid > 0 THEN
            UPDATE billing b
            SET
                remaining_penalty_amount = v_new_penalty,
                remaining_water_charge   = v_new_water,
                remaining_tax_amount     = v_new_tax,
                remaining_scf_amount     = v_new_scf,
                remaining_balance        = v_new_balance,
                payment_count            = payment_count + 1,
                last_collection_id       = v_collection_id,
                last_payment_date        = p_payment_date,
                paid_at                  = CASE
                                                WHEN v_new_balance = 0 THEN p_payment_date
                                                ELSE paid_at
                                            END,
                status                   = CASE
                                                WHEN v_new_balance = 0 THEN 'paid'
                                                WHEN v_bill_due_date IS NOT NULL AND DATE(v_bill_due_date) < DATE(p_payment_date) THEN 'overdue'
                                                ELSE 'partially_paid'
                                           END,
                scf_status               = CASE
                                                WHEN v_new_scf = 0 THEN 'paid'
                                                WHEN v_pay_scf > 0 THEN 'partially_paid'
                                                ELSE scf_status
                                           END,
                updated_at               = NOW(),
                updated_by_user_id       = p_user_id
            WHERE b.billing_id = v_bill_id;

            INSERT INTO payment (
                billing_id,
                billing_number,
                amount_paid,
                balance,
                scf_paid,
                scf_balance,
                payment_date,
                other_payment,
                late_penalty,
                arrears_penalty,
                arrears_tax,
                payment_type,
                bank_number,
                remarks,
                collection_id,
                water_paid,
                tax_paid,
                arrears_paid,
                penalty_paid,
                other_paid,
                remaining_water_after,
                remaining_balance_after,
                status
            )
            VALUES (
                v_bill_id,
                v_bill_number,
                ROUND(v_row_paid, 2),
                v_new_balance,
                v_pay_scf,
                v_new_scf,
                p_payment_date,
                0.00,
                v_pay_penalty,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_penalty ELSE 0.00 END,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_tax ELSE 0.00 END,
                p_payment_type,
                COALESCE(p_reference_no, ''),
                p_remarks,
                v_collection_id,
                v_pay_water,
                v_pay_tax,
                CASE WHEN v_bill_id <> v_current_bill_id THEN ROUND(v_pay_penalty + v_pay_water + v_pay_tax, 2) ELSE 0.00 END,
                v_pay_penalty,
                0.00,
                v_new_water,
                v_new_balance,
                'POSTED'
            );

            SET v_processed_bill_count = v_processed_bill_count + 1;
            SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

            IF v_bill_id = v_current_bill_id THEN
                SET v_total_current = ROUND(v_total_current + v_pay_water, 2);
            ELSE
                SET v_total_arrears = ROUND(v_total_arrears + v_pay_water, 2);
            END IF;

            SET v_total_penalty = ROUND(v_total_penalty + v_pay_penalty, 2);
            SET v_total_tax = ROUND(v_total_tax + v_pay_tax, 2);
            SET v_total_scf = ROUND(v_total_scf + v_pay_scf, 2);
            SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);

            SET v_payment_count = v_payment_count + 1;
            SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        END IF;
    END LOOP;

    CLOSE cur;

    IF p_others > 0 THEN
        INSERT INTO payment (
            billing_id,
            billing_number,
            amount_paid,
            balance,
            scf_paid,
            scf_balance,
            payment_date,
            other_payment,
            late_penalty,
            arrears_penalty,
            arrears_tax,
            payment_type,
            bank_number,
            remarks,
            collection_id,
            water_paid,
            tax_paid,
            arrears_paid,
            penalty_paid,
            other_paid,
            remaining_water_after,
            remaining_balance_after,
            status
        )
        VALUES (
            NULL,
            NULL,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_date,
            p_others,
            0.00,
            0.00,
            0.00,
            p_payment_type,
            COALESCE(p_reference_no, ''),
            p_remarks,
            v_collection_id,
            0.00,
            0.00,
            0.00,
            0.00,
            p_others,
            0.00,
            0.00,
            'POSTED'
        );

        SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        SET v_payment_count = v_payment_count + 1;
    END IF;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_total_paid_amount = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);
    SET v_grand_total = v_total_paid_amount;
    SET v_change_amount = ROUND(p_amount_received - v_grand_total, 2);

    SELECT COALESCE(SUM(b.remaining_balance), 0.00)
    INTO v_uncollected
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    UPDATE collection
    SET
        bill_numbers          = v_bill_numbers,
        concessionaire_code   = v_selected_code,
        concessionaire_name   = v_selected_name,
        address               = v_selected_address,
        payor_name            = p_payor_name,
        total_current_bill    = ROUND(v_total_current, 2),
        total_arrears         = ROUND(v_total_arrears, 2),
        total_penalty         = ROUND(v_total_penalty, 2),
        total_tax             = ROUND(v_total_tax, 2),
        total_scf             = ROUND(v_total_scf, 2),
        total_water_bill_paid = ROUND(v_total_water_bill_paid, 2),
        scf_paid              = ROUND(v_total_scf, 2),
        total_others          = ROUND(p_others, 2),
        grand_total           = ROUND(v_grand_total, 2),
        amount_received       = ROUND(p_amount_received, 2),
        change_amount         = ROUND(v_change_amount, 2),
        total_paid_amount     = ROUND(v_total_paid_amount, 2),
        uncollected           = ROUND(v_uncollected, 2),
        created_by_name       = '',
        remarks               = p_remarks,
        updated_at            = NOW(),
        payment_type          = p_payment_type,
        payment_reference     = COALESCE(p_reference_no, ''),
        payment_ids           = v_payment_ids,
        total_discount        = ROUND(v_total_discount, 2),
        status                = 'POSTED',
        billing_count         = v_processed_bill_count,
        payment_count         = v_payment_count
    WHERE collection_id = v_collection_id;

    COMMIT;

    SELECT
        v_collection_id AS collection_id,
        p_or_number AS or_number,
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        p_payor_name AS payor_name,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(v_total_scf, 2) AS scf_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_grand_total, 2) AS grand_total,
        ROUND(p_amount_received, 2) AS amount_received,
        ROUND(v_change_amount, 2) AS change_amount,
        ROUND(v_total_paid_amount, 2) AS total_paid_amount,
        ROUND(v_uncollected, 2) AS uncollected,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        v_payment_count AS payment_count,
        v_payment_ids AS payment_ids,
        ROUND(v_penalty_generated_total, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_collection_v48_migration` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_collection_v48_migration`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_json_concessionaires JSON
)
sp_main: BEGIN

    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;

    DECLARE v_rem_water DECIMAL(12,2);
    DECLARE v_rem_tax DECIMAL(12,2);
    DECLARE v_rem_penalty DECIMAL(12,2);
    DECLARE v_rem_scf DECIMAL(12,2);
    DECLARE v_rem_balance DECIMAL(12,2);

    DECLARE v_pay_water DECIMAL(12,2);
    DECLARE v_pay_tax DECIMAL(12,2);
    DECLARE v_pay_penalty DECIMAL(12,2);
    DECLARE v_pay_scf DECIMAL(12,2);

    DECLARE v_new_water DECIMAL(12,2);
    DECLARE v_new_tax DECIMAL(12,2);
    DECLARE v_new_penalty DECIMAL(12,2);
    DECLARE v_new_scf DECIMAL(12,2);
    DECLARE v_new_balance DECIMAL(12,2);

    DECLARE v_cash DECIMAL(12,2);
    DECLARE v_collection_id INT;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_paid_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0;
    DECLARE v_uncollected DECIMAL(12,2) DEFAULT 0;

    DECLARE v_bill_numbers TEXT DEFAULT '';
    DECLARE v_payment_ids TEXT DEFAULT '';

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            b.billing_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, 10),
            COALESCE(b.remaining_water_charge, 0),
            COALESCE(b.remaining_tax_amount, 0),
            COALESCE(b.remaining_penalty_amount, 0),
            COALESCE(b.remaining_scf_amount, 0),
            COALESCE(b.remaining_balance, 0)
        FROM billing b
        JOIN JSON_TABLE(
            p_json_concessionaires,
            '$[*]' COLUMNS(concessionaire_id INT PATH '$')
        ) jt
        ON jt.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;


    SET p_payment_date = COALESCE(p_payment_date, NOW());

    START TRANSACTION;

    INSERT INTO collection (
        or_number,
        collection_date,
        payor_name,
        total_others,
        amount_received,
        created_by,
        created_at,
        payment_type,
        payment_reference,
        remarks
    )
    VALUES (
        p_or_number,
        p_payment_date,
        p_payor_name,
        p_others,
        p_amount_received,
        p_user_id,
        NOW(),
        p_payment_type,
        p_reference_no,
        p_remarks
    );

    SET v_collection_id = LAST_INSERT_ID();
    SET v_cash = p_amount_received - p_others;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance;

        IF v_done = 1 THEN LEAVE read_loop; END IF;

        SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
        SET v_cash = v_cash - v_pay_penalty;

        SET v_pay_water = LEAST(v_cash, v_rem_water);
        SET v_cash = v_cash - v_pay_water;

        SET v_pay_tax = LEAST(v_cash, v_rem_tax);
        SET v_cash = v_cash - v_pay_tax;

        SET v_pay_scf = LEAST(v_cash, v_rem_scf);
        SET v_cash = v_cash - v_pay_scf;

        SET v_new_penalty = v_rem_penalty - v_pay_penalty;
        SET v_new_water = v_rem_water - v_pay_water;
        SET v_new_tax = v_rem_tax - v_pay_tax;
        SET v_new_scf = v_rem_scf - v_pay_scf;
        SET v_new_balance = v_new_penalty + v_new_water + v_new_tax + v_new_scf;

        UPDATE billing
        SET
            remaining_penalty_amount = v_new_penalty,
            remaining_water_charge = v_new_water,
            remaining_tax_amount = v_new_tax,
            remaining_scf_amount = v_new_scf,
            remaining_balance = v_new_balance,
            last_payment_date = p_payment_date
        WHERE billing_id = v_bill_id;

        INSERT INTO payment (
            billing_id,
            billing_number,
            amount_paid,
            payment_date,
            collection_id,
            water_paid,
            tax_paid,
            penalty_paid
        )
        VALUES (
            v_bill_id,
            v_bill_number,
            v_pay_water + v_pay_tax + v_pay_penalty + v_pay_scf,
            p_payment_date,
            v_collection_id,
            v_pay_water,
            v_pay_tax,
            v_pay_penalty
        );

        SET v_bill_numbers = CONCAT_WS(',', v_bill_numbers, v_bill_number);

        SET v_total_current = v_total_current + v_pay_water;
        SET v_total_tax = v_total_tax + v_pay_tax;
        SET v_total_penalty = v_total_penalty + v_pay_penalty;
        SET v_total_scf = v_total_scf + v_pay_scf;

    END LOOP;

    CLOSE cur;

    SET v_total_paid_amount = v_total_current + v_total_tax + v_total_penalty + v_total_scf + p_others;
    SET v_change_amount = p_amount_received - v_total_paid_amount;

    SELECT COALESCE(SUM(remaining_balance),0)
    INTO v_uncollected
    FROM billing
    WHERE concessionaire_id IN (
        SELECT concessionaire_id
        FROM JSON_TABLE(p_json_concessionaires,'$[*]' COLUMNS(concessionaire_id INT PATH '$')) jt
    );

    UPDATE collection
    SET
        bill_numbers = v_bill_numbers,
        total_current_bill = v_total_current,
        total_penalty = v_total_penalty,
        total_tax = v_total_tax,
        total_scf = v_total_scf,
        total_paid_amount = v_total_paid_amount,
        change_amount = v_change_amount,
        uncollected = v_uncollected
    WHERE collection_id = v_collection_id;

    COMMIT;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_collection_v48_test` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_collection_v48_test`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_pay_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_scf DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_cash DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_paid_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_uncollected DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_generated_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count_loaded INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_payment_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_payment_ids LONGTEXT DEFAULT '';

    DECLARE v_selected_code TEXT;
    DECLARE v_selected_name TEXT;
    DECLARE v_selected_address TEXT;

    DECLARE v_payor_name_clean VARCHAR(255) DEFAULT '';
    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_row_paid DECIMAL(12,2) DEFAULT 0.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC
        FOR UPDATE;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');
    SET p_payment_type = COALESCE(TRIM(p_payment_type), '');
    SET v_payor_name_clean = p_payor_name;

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR required';
    END IF;

    IF EXISTS (SELECT 1 FROM collection WHERE or_number = p_or_number) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF p_amount_received <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid amount received';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    START TRANSACTION;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*)
    INTO v_selected_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count_loaded
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    IF v_selected_count = 1 THEN
        SELECT concessionaire_code, concessionaire_name, address
        INTO v_selected_code, v_selected_name, v_selected_address
        FROM tmp_selected
        LIMIT 1;
ELSE
    SELECT
        GROUP_CONCAT(DISTINCT concessionaire_code ORDER BY concessionaire_code SEPARATOR ', '),
        GROUP_CONCAT(DISTINCT concessionaire_name ORDER BY concessionaire_name SEPARATOR ', '),
        GROUP_CONCAT(DISTINCT address ORDER BY address SEPARATOR ', ')
    INTO
        v_selected_code,
        v_selected_name,
        v_selected_address
    FROM tmp_selected;
END IF;

    IF v_bill_count_loaded = 0 AND p_others <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No unpaid billing found for the selected concessionaires';
    END IF;

    INSERT INTO collection (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        payor_name,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        uncollected,
        created_by,
        created_by_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount,
        status,
        voided_by_user_id,
        voided_at,
        billing_count,
        payment_count
    )
    VALUES (
        p_or_number,
        p_payment_date,
        '',
        v_selected_code,
        v_selected_name,
        v_selected_address,
        v_payor_name_clean,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        0.00,
        ROUND(p_others, 2),
        0.00,
        ROUND(p_amount_received, 2),
        0.00,
        0.00,
        0.00,
        p_user_id,
        '',
        p_remarks,
        NOW(),
        NOW(),
        p_payment_type,
        COALESCE(p_reference_no, ''),
        '',
        0.00,
        'POSTED',
        NULL,
        NULL,
        0,
        0
    );

    SET v_collection_id = LAST_INSERT_ID();

    SET v_cash = ROUND(p_amount_received, 2);

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND v_rem_balance > 0 THEN

            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
            SET v_penalty_generated_total = ROUND(v_penalty_generated_total + v_generated_penalty, 2);

            SET v_rem_penalty = ROUND(v_rem_penalty + v_generated_penalty, 2);
            SET v_rem_balance = ROUND(v_rem_balance + v_generated_penalty, 2);

            UPDATE billing b
            SET
                penalty_amount = ROUND(COALESCE(b.penalty_amount, 0) + v_generated_penalty, 2),
                remaining_penalty_amount = ROUND(COALESCE(b.remaining_penalty_amount, 0) + v_generated_penalty, 2),
                remaining_balance = ROUND(COALESCE(b.remaining_balance, 0) + v_generated_penalty, 2),
                is_penalty_applied = 1,
                penalty_applied_at = NOW(),
                status = 'overdue',
                updated_at = NOW(),
                updated_by_user_id = p_user_id
            WHERE b.billing_id = v_bill_id;
        END IF;

        SET v_pay_penalty = 0.00;
        SET v_pay_water = 0.00;
        SET v_pay_tax = 0.00;
        SET v_pay_scf = 0.00;

        IF v_cash > 0 THEN
            SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
            SET v_cash = ROUND(v_cash - v_pay_penalty, 2);

            SET v_pay_water = LEAST(v_cash, v_rem_water);
            SET v_cash = ROUND(v_cash - v_pay_water, 2);

            SET v_pay_tax = LEAST(v_cash, v_rem_tax);
            SET v_cash = ROUND(v_cash - v_pay_tax, 2);

            SET v_pay_scf = LEAST(v_cash, v_rem_scf);
            SET v_cash = ROUND(v_cash - v_pay_scf, 2);
        END IF;

        SET v_new_penalty = ROUND(GREATEST(v_rem_penalty - v_pay_penalty, 0), 2);
        SET v_new_water   = ROUND(GREATEST(v_rem_water - v_pay_water, 0), 2);
        SET v_new_tax     = ROUND(GREATEST(v_rem_tax - v_pay_tax, 0), 2);
        SET v_new_scf     = ROUND(GREATEST(v_rem_scf - v_pay_scf, 0), 2);
        SET v_new_balance = ROUND(v_new_penalty + v_new_water + v_new_tax + v_new_scf, 2);

        SET v_row_paid = ROUND(v_pay_penalty + v_pay_water + v_pay_tax + v_pay_scf, 2);

        IF v_row_paid > 0 THEN
            UPDATE billing b
            SET
                remaining_penalty_amount = v_new_penalty,
                remaining_water_charge   = v_new_water,
                remaining_tax_amount     = v_new_tax,
                remaining_scf_amount     = v_new_scf,
                remaining_balance        = v_new_balance,
                payment_count            = payment_count + 1,
                last_collection_id       = v_collection_id,
                last_payment_date        = p_payment_date,
                paid_at                  = CASE
                                                WHEN v_new_balance = 0 THEN p_payment_date
                                                ELSE paid_at
                                            END,
                status                   = CASE
                                                WHEN v_new_balance = 0 THEN 'paid'
                                                WHEN v_bill_due_date IS NOT NULL AND DATE(v_bill_due_date) < DATE(p_payment_date) THEN 'overdue'
                                                ELSE 'partially_paid'
                                           END,
                scf_status               = CASE
                                                WHEN v_new_scf = 0 THEN 'paid'
                                                WHEN v_pay_scf > 0 THEN 'partially_paid'
                                                ELSE scf_status
                                           END,
                updated_at               = NOW(),
                updated_by_user_id       = p_user_id
            WHERE b.billing_id = v_bill_id;

            INSERT INTO payment (
                billing_id,
                billing_number,
                amount_paid,
                balance,
                scf_paid,
                scf_balance,
                payment_date,
                other_payment,
                late_penalty,
                arrears_penalty,
                arrears_tax,
                payment_type,
                bank_number,
                remarks,
                collection_id,
                water_paid,
                tax_paid,
                arrears_paid,
                penalty_paid,
                other_paid,
                remaining_water_after,
                remaining_balance_after,
                status
            )
            VALUES (
                v_bill_id,
                v_bill_number,
                ROUND(v_row_paid, 2),
                v_new_balance,
                v_pay_scf,
                v_new_scf,
                p_payment_date,
                0.00,
                v_pay_penalty,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_penalty ELSE 0.00 END,
                CASE WHEN v_bill_id <> v_current_bill_id THEN v_pay_tax ELSE 0.00 END,
                p_payment_type,
                COALESCE(p_reference_no, ''),
                p_remarks,
                v_collection_id,
                v_pay_water,
                v_pay_tax,
                CASE WHEN v_bill_id <> v_current_bill_id THEN ROUND(v_pay_penalty + v_pay_water + v_pay_tax, 2) ELSE 0.00 END,
                v_pay_penalty,
                0.00,
                v_new_water,
                v_new_balance,
                'POSTED'
            );

            SET v_processed_bill_count = v_processed_bill_count + 1;
            SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

            IF v_bill_id = v_current_bill_id THEN
                SET v_total_current = ROUND(v_total_current + v_pay_water, 2);
            ELSE
                SET v_total_arrears = ROUND(v_total_arrears + v_pay_water, 2);
            END IF;

            SET v_total_penalty = ROUND(v_total_penalty + v_pay_penalty, 2);
            SET v_total_tax = ROUND(v_total_tax + v_pay_tax, 2);
            SET v_total_scf = ROUND(v_total_scf + v_pay_scf, 2);
            SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);

            SET v_payment_count = v_payment_count + 1;
            SET v_payment_ids = CONCAT_WS(', ', NULLIF(v_payment_ids, ''), LAST_INSERT_ID());
        END IF;
    END LOOP;

    CLOSE cur;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_total_paid_amount = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);
    SET v_grand_total = v_total_paid_amount;
    SET v_change_amount = ROUND((p_amount_received + p_others) - v_grand_total, 2);

    IF v_bill_numbers IS NULL OR TRIM(v_bill_numbers) = '' THEN
        SET v_bill_numbers = '';
    END IF;

    SELECT COALESCE(SUM(b.remaining_balance), 0.00)
    INTO v_uncollected
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    UPDATE collection
    SET
        bill_numbers          = v_bill_numbers,
        concessionaire_code   = v_selected_code,
        concessionaire_name   = v_selected_name,
        address               = v_selected_address,
        payor_name            = v_payor_name_clean,
        total_current_bill    = ROUND(v_total_current, 2),
        total_arrears         = ROUND(v_total_arrears, 2),
        total_penalty         = ROUND(v_total_penalty, 2),
        total_tax             = ROUND(v_total_tax, 2),
        total_scf             = ROUND(v_total_scf, 2),
        total_water_bill_paid = ROUND(v_total_water_bill_paid, 2),
        scf_paid              = ROUND(v_total_scf, 2),
        total_others          = ROUND(p_others, 2),
        grand_total           = ROUND(v_grand_total, 2),
        amount_received       = ROUND(p_amount_received, 2),
        change_amount         = ROUND(v_change_amount, 2),
        total_paid_amount     = ROUND(v_total_paid_amount, 2),
        uncollected           = ROUND(v_uncollected, 2),
        created_by_name       = '',
        remarks               = p_remarks,
        updated_at            = NOW(),
        payment_type          = p_payment_type,
        payment_reference     = COALESCE(p_reference_no, ''),
        payment_ids           = v_payment_ids,
        total_discount        = ROUND(v_total_discount, 2),
        status                = 'POSTED',
        billing_count         = v_processed_bill_count,
        payment_count         = v_payment_count
    WHERE collection_id = v_collection_id;

    COMMIT;
INSERT INTO user_logs (
    user_id,
    action_type,
    module,
    entity_name,
    entity_id,
    description
)
VALUES (
    p_user_id,
    'CREATE',
    'COLLECTION',
    'collection',
    p_or_number,
    CONCAT(
        'Payment posted using OR #', p_or_number,
        '. Total amount received: ₱', FORMAT(p_amount_received, 2),
        '. Bills processed: ', v_processed_bill_count,
        '. Concessionaires: ', v_selected_name,
        '. Payment type: ', p_payment_type,
        '. Change: ₱', FORMAT(v_change_amount, 2),
        '. Status: POSTED.'
    )
);
    SELECT
        v_collection_id AS collection_id,
        p_or_number AS or_number,
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        p_payor_name AS payor_name,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(v_total_scf, 2) AS scf_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_grand_total, 2) AS grand_total,
        ROUND(p_amount_received, 2) AS amount_received,
        ROUND(v_change_amount, 2) AS change_amount,
        ROUND(v_total_paid_amount, 2) AS total_paid_amount,
        ROUND(v_uncollected, 2) AS uncollected,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        v_payment_count AS payment_count,
        v_payment_ids AS payment_ids,
        ROUND(v_penalty_generated_total, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v2` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v2`(
    IN p_billing_id INT,
    IN p_payor_id INT,               -- optional (can be NULL)
    IN p_amount_paid DECIMAL(10,2),
    IN p_scf_paid DECIMAL(10,2),
    IN p_other_payment DECIMAL(10,2)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');

    -- Get the total and SCF amount for the billing
    SELECT total_amount, scf_amount
    INTO v_total_amount, v_scf_amount
    FROM billing
    WHERE billing_id = p_billing_id;

    -- Compute remaining balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    -- Determine billing status
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
        SET v_balance = 0;
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- Insert payment record (payor_id can be NULL)
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        NULLIF(p_payor_id, 0),     -- if 0 is passed, convert to NULL
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- Update billing status and last payment date
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v4` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v4`(
    IN p_billing_id INT,
    IN p_payor_id INT,                   -- optional (can be NULL)
    IN p_amount_paid DECIMAL(10,2),      -- main bill payment
    IN p_scf_paid DECIMAL(10,2),         -- SCF payment
    IN p_other_payment DECIMAL(10,2)     -- optional (can be 0.00)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');

    -- ✅ 1. Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- ✅ 2. Get current billing and scf data
    SELECT total_water_bill, scf_amount
    INTO v_total_amount, v_scf_amount
    FROM billing
    WHERE billing_id = p_billing_id;

    -- ✅ 3. Compute remaining balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    -- ✅ 4. Determine billing status
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
        SET v_balance = 0;
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- ✅ 5. Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- ✅ 6. Insert payment record (always one record)
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        NULLIF(p_payor_id, 0),
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- ✅ 7. Update billing table
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v5` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v5`(
    IN p_billing_id INT,
    IN p_payor_id INT,                   -- optional (can be NULL)
    IN p_amount_paid DECIMAL(10,2),      -- main bill payment
    IN p_scf_paid DECIMAL(10,2),         -- SCF payment
    IN p_other_payment DECIMAL(10,2)     -- optional (can be 0.00)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');

    -- Step 1: Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- Step 2: Get current billing and scf data
    SELECT total_water_bill, scf_amount
    INTO v_total_amount, v_scf_amount
    FROM billing
    WHERE billing_id = p_billing_id;

    -- Step 3: Compute balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_scf_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    -- Prevent negative results
    IF v_balance < 0 THEN SET v_balance = 0; END IF;
    IF v_scf_balance < 0 THEN SET v_scf_balance = 0; END IF;

    -- Step 4: Determine billing status
    IF v_balance = 0 THEN
        SET v_status = 'paid';
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- Step 5: Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance = 0 THEN
        SET v_scf_status = 'paid';
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- Step 6: Insert payment record
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        NULLIF(p_payor_id, 0),
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- Step 7: Update billing table
    UPDATE billing
    SET 
        total_amount = v_balance, 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v6` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v6`(
    IN p_billing_id INT,
    IN p_payor_id INT,                   -- optional (can be NULL)
    IN p_amount_paid DECIMAL(10,2),      -- main bill payment
    IN p_scf_paid DECIMAL(10,2),         -- SCF payment
    IN p_other_payment DECIMAL(10,2)     -- optional (can be 0.00)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_concessionaire_id INT;

    -- Step 1: Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- Step 2: Get current billing data and concessionaire_id
    SELECT total_water_bill, scf_amount, concessionaire_id
    INTO v_total_amount, v_scf_amount, v_concessionaire_id
    FROM billing
    WHERE billing_id = p_billing_id;

    -- Step 3: Compute balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    IF v_balance < 0 THEN SET v_balance = 0; END IF;
    IF v_scf_balance < 0 THEN SET v_scf_balance = 0; END IF;

    -- Step 4: Determine billing status
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
        SET v_balance = 0;
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- Step 5: Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- Step 6: Insert payment record
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        NULLIF(p_payor_id, 0),
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- Step 7: Update billing table
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

    -- Step 8: Update SCF balance (deduct paid SCF from scf_balance.total_amount)
    IF IFNULL(p_scf_paid, 0) > 0 THEN
        UPDATE scf_balance
        SET 
            balance = GREATEST(total_amount - p_scf_paid, 0),
            last_payment_date = NOW(),
            updated_at = NOW()
        WHERE concessionaire_id = v_concessionaire_id;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v7` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v7`(
    IN p_billing_id INT,
    IN p_amount_paid DECIMAL(10,2),
    IN p_scf_paid DECIMAL(10,2),
    IN p_other_payment DECIMAL(10,2)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_concessionaire_id INT;

    -- Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- Get billing data and concessionaire_id
    SELECT total_water_bill, scf_amount, concessionaire_id
    INTO v_total_amount, v_scf_amount, v_concessionaire_id
    FROM billing
    WHERE billing_id = p_billing_id
    LIMIT 1;

    -- Compute balances (treat NULL inputs as 0)
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    IF v_balance < 0 THEN SET v_balance = 0; END IF;
    IF v_scf_balance < 0 THEN SET v_scf_balance = 0; END IF;

    -- Determine billing status
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- Insert payment record
    -- We explicitly insert NULL for payor_id and payor_name since payor is removed from the API.
    INSERT INTO payment (
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- Update billing table
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

    -- Update SCF balance table (deduct from scf_balance.balance)
    IF IFNULL(p_scf_paid, 0) > 0 THEN
        UPDATE scf_balance
        SET 
            balance = GREATEST(balance - p_scf_paid, 0),
            last_payment_date = NOW(),
            updated_at = NOW()
        WHERE concessionaire_id = v_concessionaire_id;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v7_0)` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v7_0)`(
    IN p_billing_id INT,
    IN p_payor_id INT,                   -- optional (can be NULL)
    IN p_amount_paid DECIMAL(10,2),      -- main bill payment
    IN p_scf_paid DECIMAL(10,2),         -- SCF payment
    IN p_other_payment DECIMAL(10,2)     -- optional (can be 0.00)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_concessionaire_id INT;

    -- ✅ Step 1: Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- ✅ Step 2: Get billing data and concessionaire_id
    SELECT total_water_bill, scf_amount, concessionaire_id
    INTO v_total_amount, v_scf_amount, v_concessionaire_id
    FROM billing
    WHERE billing_id = p_billing_id;

    -- ✅ Step 3: Compute balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid, 0) + IFNULL(p_other_payment, 0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid, 0);

    IF v_balance < 0 THEN SET v_balance = 0; END IF;
    IF v_scf_balance < 0 THEN SET v_scf_balance = 0; END IF;

    -- ✅ Step 4: Determine billing status
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- ✅ Step 5: Determine SCF status
    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- ✅ Step 6: Insert payment record
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty
    ) VALUES (
        NULLIF(p_payor_id, 0),
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        0.00
    );

    -- ✅ Step 7: Update billing table
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

    -- ✅ Step 8: Update SCF balance table (deduct from scf_balance.balance)
    IF IFNULL(p_scf_paid, 0) > 0 THEN
        UPDATE scf_balance
        SET 
            balance = GREATEST(balance - p_scf_paid, 0),
            last_payment_date = NOW(),
            updated_at = NOW()
        WHERE concessionaire_id = v_concessionaire_id;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_v8` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_v8`(
    IN p_billing_id INT,
    IN p_amount_paid DECIMAL(10,2),
    IN p_scf_paid DECIMAL(10,2),
    IN p_other_payment DECIMAL(10,2)
)
BEGIN
    DECLARE v_total_amount DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_balance DECIMAL(12,2);
    DECLARE v_scf_balance DECIMAL(12,2);
    DECLARE v_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_scf_status ENUM('unpaid','paid','overdue','partially_paid');
    DECLARE v_concessionaire_id INT;

    DECLARE v_due_date DATE;
    DECLARE v_arrears_amount DECIMAL(12,2);
    DECLARE v_arrears_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_arrears_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_late_penalty DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_penalty_percent DECIMAL(5,2);
    DECLARE v_tax_percent DECIMAL(5,2);
    DECLARE v_penalize_after_days INT;

    -- ✅ Validate billing_id
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid billing_id: record not found in billing table.';
    END IF;

    -- ✅ Fetch billing info
    SELECT 
        total_water_bill, 
        scf_amount, 
        concessionaire_id,
        due_date,
        arrears_amount
    INTO 
        v_total_amount, 
        v_scf_amount, 
        v_concessionaire_id,
        v_due_date,
        v_arrears_amount
    FROM billing
    WHERE billing_id = p_billing_id
    LIMIT 1;

    -- ✅ Get settings
    SELECT 
        MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END),
        MAX(CASE WHEN settings_key = 'tax_percent' THEN settings_value END),
        MAX(CASE WHEN settings_key = 'penalize_after_days' THEN settings_value END)
    INTO v_penalty_percent, v_tax_percent, v_penalize_after_days
    FROM system_settings;

    -- ✅ Compute arrears tax
    SET v_arrears_tax = IFNULL(v_arrears_amount,0) * (IFNULL(v_tax_percent,0) / 100);

    -- ✅ Check late payment
    IF DATEDIFF(CURDATE(), v_due_date) > v_penalize_after_days THEN
        SET v_late_penalty = v_total_amount * (v_penalty_percent / 100);
    ELSE
        SET v_late_penalty = 0.00;
    END IF;

    -- ✅ Compute arrears penalty if there is arrears
    IF v_arrears_amount > 0 THEN
        SET v_arrears_penalty = v_arrears_amount * (v_penalty_percent / 100);
    ELSE
        SET v_arrears_penalty = 0.00;
    END IF;

    -- ✅ Compute balances
    SET v_balance = v_total_amount - (IFNULL(p_amount_paid,0) + IFNULL(p_other_payment,0));
    SET v_scf_balance = v_scf_amount - IFNULL(p_scf_paid,0);

    IF v_balance < 0 THEN SET v_balance = 0; END IF;
    IF v_scf_balance < 0 THEN SET v_scf_balance = 0; END IF;

    -- ✅ Determine statuses
    IF v_balance <= 0 THEN
        SET v_status = 'paid';
    ELSEIF v_balance < v_total_amount THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    IF v_scf_amount = 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance <= 0 THEN
        SET v_scf_status = 'paid';
        SET v_scf_balance = 0;
    ELSEIF v_scf_balance < v_scf_amount THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- ✅ Insert payment record (now includes arrears_penalty + arrears_tax)
    INSERT INTO payment (
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty,
        arrears_penalty,
        arrears_tax,
        remarks
    ) VALUES (
        p_billing_id,
        IFNULL(p_amount_paid, 0),
        v_balance,
        IFNULL(p_scf_paid, 0),
        v_scf_balance,
        CURDATE(),
        IFNULL(p_other_payment, 0),
        v_late_penalty,
        v_arrears_penalty,
        v_arrears_tax,
        CONCAT('Auto payment inserted on ', NOW())
    );

    -- ✅ Update billing table
    UPDATE billing
    SET 
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = NOW()
    WHERE billing_id = p_billing_id;

    -- ✅ Update SCF balance
    IF IFNULL(p_scf_paid, 0) > 0 THEN
        UPDATE scf_balance
        SET 
            balance = GREATEST(balance - p_scf_paid, 0),
            last_payment_date = NOW(),
            updated_at = NOW()
        WHERE concessionaire_id = v_concessionaire_id;
    END IF;
    -- Return the inserted payment_id
SELECT LAST_INSERT_ID() AS payment_id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_payment_with_master` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_payment_with_master`(
    IN p_billing_id INT,
    IN p_payor_id INT,                        -- optional
    IN p_amount_paid DECIMAL(12,2),           -- water-side (arrears + water + penalty)
    IN p_scf_paid DECIMAL(12,2),              -- scf only
    IN p_other_payment DECIMAL(12,2),         -- recorded but NOT applied to billing
    IN p_payment_date DATE,                   -- optional (pass CURDATE())
    IN p_master_id INT                        -- optional: link to payment_master.master_id
)
BEGIN
    -- =========================
    -- DECLARE (must appear immediately after BEGIN)
    -- =========================
    DECLARE v_total_water_bill    DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_arrears_amount      DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_amount      DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_amount          DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_due_date            DATE;
    DECLARE v_concessionaire_id   INT DEFAULT NULL;

    DECLARE v_existing_paid       DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_existing_scf_paid   DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_total_paid      DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_total_scf_paid  DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_water_due           DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_remaining_water     DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_remaining_scf       DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_status              ENUM('unpaid','paid','overdue','partially_paid') DEFAULT 'unpaid';
    DECLARE v_scf_status          ENUM('unpaid','paid','overdue','partially_paid') DEFAULT 'unpaid';

    DECLARE v_payment_id          BIGINT DEFAULT 0;
    DECLARE v_master_sum          DECIMAL(14,2) DEFAULT 0.00;

    -- =========================
    -- Normalize params (after DECLARE)
    -- =========================
    SET p_amount_paid   = IFNULL(p_amount_paid, 0.00);
    SET p_scf_paid      = IFNULL(p_scf_paid, 0.00);
    SET p_other_payment = IFNULL(p_other_payment, 0.00);

    IF p_payment_date IS NULL THEN
        SET p_payment_date = CURDATE();
    END IF;

    -- =========================
    -- 0) Validate billing exists
    -- =========================
    IF (SELECT COUNT(*) FROM billing WHERE billing_id = p_billing_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid billing_id: not found';
    END IF;

    -- =========================
    -- 1) Load billing amounts
    -- =========================
    SELECT total_water_bill, arrears_amount, penalty_amount, scf_amount, due_date, concessionaire_id
    INTO v_total_water_bill, v_arrears_amount, v_penalty_amount, v_scf_amount, v_due_date, v_concessionaire_id
    FROM billing
    WHERE billing_id = p_billing_id
    LIMIT 1;

    -- =========================
    -- 2) Compute water-side due (arrears + current water + penalty)
    -- =========================
    SET v_water_due = COALESCE(v_total_water_bill,0.00) + COALESCE(v_arrears_amount,0.00) + COALESCE(v_penalty_amount,0.00);

    -- =========================
    -- 3) Sum existing payments (exclude the new one)
    -- =========================
    SELECT COALESCE(SUM(amount_paid),0.00), COALESCE(SUM(scf_paid),0.00)
    INTO v_existing_paid, v_existing_scf_paid
    FROM payment
    WHERE billing_id = p_billing_id;

    -- =========================
    -- 4) Totals after incoming payment
    -- Note: other_payment is NOT applied to billing; it is only recorded
    -- =========================
    SET v_new_total_paid = v_existing_paid + p_amount_paid;
    SET v_new_total_scf_paid = v_existing_scf_paid + p_scf_paid;

    -- =========================
    -- 5) Remaining balances (floor at 0)
    -- =========================
    SET v_remaining_water = GREATEST(v_water_due - v_new_total_paid, 0.00);
    SET v_remaining_scf   = GREATEST(v_scf_amount - v_new_total_scf_paid, 0.00);

    -- =========================
    -- 6) Determine billing.status (water-side)
    -- If fully paid => paid
    -- else if payment_date > due_date => overdue
    -- else if some payment made => partially_paid
    -- else unpaid
    -- =========================
    IF v_remaining_water = 0.00 THEN
        SET v_status = 'paid';
    ELSEIF p_payment_date > v_due_date THEN
        SET v_status = 'overdue';
    ELSEIF v_new_total_paid > 0.00 THEN
        SET v_status = 'partially_paid';
    ELSE
        SET v_status = 'unpaid';
    END IF;

    -- =========================
    -- 7) Determine scf_status
    -- =========================
    IF v_scf_amount = 0.00 THEN
        SET v_scf_status = 'paid';
    ELSEIF v_remaining_scf = 0.00 THEN
        SET v_scf_status = 'paid';
    ELSEIF v_new_total_scf_paid > 0.00 THEN
        SET v_scf_status = 'partially_paid';
    ELSE
        SET v_scf_status = 'unpaid';
    END IF;

    -- =========================
    -- 8) Insert payment row (link to master if provided)
    -- store balances AFTER applying only amount_paid & scf_paid
    -- =========================
    INSERT INTO payment (
        payor_id,
        billing_id,
        amount_paid,
        balance,
        scf_paid,
        scf_balance,
        payment_date,
        other_payment,
        late_penalty,
        payment_master_id
    ) VALUES (
        NULLIF(p_payor_id,0),
        p_billing_id,
        p_amount_paid,
        v_remaining_water,
        p_scf_paid,
        v_remaining_scf,
        p_payment_date,
        p_other_payment,
        0.00,
        NULLIF(p_master_id, 0)
    );

    SET v_payment_id = LAST_INSERT_ID();

    -- =========================
    -- 9) Update billing status & last_payment_date (only if payment toward bill/scf exists)
    -- =========================
    UPDATE billing
    SET
        status = v_status,
        scf_status = v_scf_status,
        last_payment_date = CASE WHEN (p_amount_paid > 0 OR p_scf_paid > 0) THEN NOW() ELSE last_payment_date END
    WHERE billing_id = p_billing_id;

    -- =========================
    -- 10) Update scf_balance record if scf was paid
    -- =========================
    IF p_scf_paid > 0 AND v_concessionaire_id IS NOT NULL THEN
        UPDATE scf_balance
        SET
            balance = GREATEST(COALESCE(balance,0.00) - p_scf_paid, 0.00),
            last_payment_date = NOW(),
            updated_at = NOW()
        WHERE concessionaire_id = v_concessionaire_id;
    END IF;

    -- =========================
    -- 11) If master id provided, increment its total_amount
    -- Use sum of components (amount_paid + scf_paid + other_payment)
    -- =========================
    IF p_master_id IS NOT NULL AND p_master_id <> 0 THEN
        SET v_master_sum = p_amount_paid + p_scf_paid + p_other_payment;

        UPDATE payment_master
        SET total_amount = COALESCE(total_amount, 0.00) + v_master_sum,
            updated_at = NOW()
        WHERE master_id = p_master_id;
    END IF;

    -- =========================
    -- 12) Return created payment id and remaining balances
    -- =========================
    SELECT
        v_payment_id           AS payment_id,
        v_remaining_water      AS remaining_water_balance,
        v_remaining_scf        AS remaining_scf_balance,
        v_status               AS billing_status,
        v_scf_status           AS scf_status;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_scf_collection_v46` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_scf_collection_v46`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_scf_amount DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_concessionaire_id INT,
    IN p_payor_name VARCHAR(191)
)
sp_main: BEGIN
    DECLARE v_scf_id INT DEFAULT NULL;
    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_balance_before DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_balance_after DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_concessionaire_code TEXT DEFAULT '';
    DECLARE v_concessionaire_name TEXT DEFAULT '';
    DECLARE v_address VARCHAR(200) DEFAULT '';

    DECLARE v_amount_received DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_payor_name VARCHAR(191) DEFAULT '';

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_payment_type = COALESCE(NULLIF(TRIM(p_payment_type), ''), '');
    SET p_reference_no = COALESCE(NULLIF(TRIM(p_reference_no), ''), '');
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_scf_amount = COALESCE(p_scf_amount, 0.00);
    SET p_others = COALESCE(p_others, 0.00);
    SET v_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR number is required';
    END IF;

    IF EXISTS (
        SELECT 1
        FROM collection c
        WHERE c.or_number = p_or_number
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR number';
    END IF;

    IF p_concessionaire_id IS NULL OR p_concessionaire_id <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    IF UPPER(TRIM(p_payment_type)) <> 'CASH'
       AND (p_reference_no IS NULL OR TRIM(p_reference_no) = '') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Reference number is required for non-cash payments';
    END IF;

    IF p_scf_amount < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'SCF amount cannot be negative';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others amount cannot be negative';
    END IF;

    IF p_scf_amount = 0 AND p_others = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nothing to collect';
    END IF;

    IF v_payor_name = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payor name is required';
    END IF;

    START TRANSACTION;

    IF NOT EXISTS (
        SELECT 1
        FROM concessionaire c
        WHERE c.concessionaire_id = p_concessionaire_id
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    IF EXISTS (
        SELECT 1
        FROM billing b
        WHERE b.concessionaire_id = p_concessionaire_id
        LIMIT 1
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'This collection is only allowed before the first billing';
    END IF;

    SELECT
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    INTO
        v_concessionaire_code,
        v_concessionaire_name,
        v_address
    FROM concessionaire c
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SELECT
        sb.scf_id,
        COALESCE(sb.monthly, 0.00),
        COALESCE(sb.balance, 0.00)
    INTO
        v_scf_id,
        v_scf_monthly,
        v_scf_balance_before
    FROM scf_balance sb
    WHERE sb.concessionaire_id = p_concessionaire_id
    ORDER BY sb.scf_id DESC
    LIMIT 1
    FOR UPDATE;

    IF v_scf_id IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'SCF balance record not found';
    END IF;

    SET p_scf_amount = ROUND(LEAST(p_scf_amount, GREATEST(v_scf_balance_before, 0.00)), 2);

    SET v_grand_total = ROUND(p_scf_amount + p_others, 2);
    SET v_amount_received = v_grand_total;
    SET v_change_amount = 0.00;
    SET v_scf_balance_after = ROUND(v_scf_balance_before - p_scf_amount, 2);

    UPDATE scf_balance sb
    SET
        sb.balance = v_scf_balance_after,
        sb.updated_at = NOW()
    WHERE sb.scf_id = v_scf_id;

    INSERT INTO collection (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        payor_name,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        created_by,
        created_by_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount
    )
    VALUES (
        p_or_number,
        DATE(p_payment_date),
        'NO_BILL_YET',
        v_concessionaire_code,
        v_concessionaire_name,
        v_address,
        v_payor_name,
        0.00,
        0.00,
        0.00,
        0.00,
        p_scf_amount,
        0.00,
        p_scf_amount,
        p_others,
        v_grand_total,
        v_amount_received,
        v_change_amount,
        v_grand_total,
        p_user_id,
        '',
        p_remarks,
        NOW(),
        NOW(),
        p_payment_type,
        p_reference_no,
        '',
        0.00
    );

    SET v_collection_id = LAST_INSERT_ID();

    COMMIT;

    SELECT
        0 AS is_duplicate_request,
        'SCF_AND_OTHERS_ONLY' AS collection_mode,
        v_collection_id AS collection_id,
        p_or_number AS or_number,
        p_concessionaire_id AS concessionaire_id,
        v_concessionaire_code AS concessionaire_code,
        v_concessionaire_name AS concessionaire_name,
        v_address AS address,
        v_payor_name AS payor_name,
        p_scf_amount AS scf_collected,
        p_others AS others_collected,
        v_grand_total AS grand_total,
        v_amount_received AS amount_received,
        v_change_amount AS change_amount,
        v_scf_balance_before AS scf_balance_before,
        v_scf_balance_after AS scf_balance_after,
        p_payment_date AS payment_date;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_make_scf_collection_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_make_scf_collection_v48`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(100),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_scf_amount DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_user_id INT,
    IN p_concessionaire_id INT
)
sp_main: BEGIN
    DECLARE v_collection_id INT DEFAULT NULL;

    DECLARE v_or_number VARCHAR(50) DEFAULT '';
    DECLARE v_payment_type VARCHAR(100) DEFAULT '';
    DECLARE v_reference_no VARCHAR(100) DEFAULT '';
    DECLARE v_remarks TEXT DEFAULT '';
    DECLARE v_payor_name_clean VARCHAR(255) DEFAULT '';

    DECLARE v_concessionaire_code VARCHAR(50) DEFAULT '';
    DECLARE v_concessionaire_name VARCHAR(255) DEFAULT '';
    DECLARE v_concessionaire_address VARCHAR(255) DEFAULT '';

    DECLARE v_scf_id INT DEFAULT NULL;
    DECLARE v_scf_monthly DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_balance_before DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_balance_after DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_scf_amount_used DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_total_others DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_amount_received DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_uncollected DECIMAL(12,2) DEFAULT 0.00;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_payment_type = COALESCE(NULLIF(TRIM(p_payment_type), ''), 'UNKNOWN');
    SET p_reference_no = COALESCE(NULLIF(TRIM(p_reference_no), ''), '');
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');
    SET p_scf_amount = COALESCE(p_scf_amount, 0.00);
    SET p_others = COALESCE(p_others, 0.00);
    SET v_total_others = ROUND(p_others, 2);

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR number is required';
    END IF;

    IF p_or_number NOT REGEXP '^[0-9]+$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR number must be numeric';
    END IF;

    SET v_or_number = LPAD(TRIM(p_or_number), 7, '0');
    SET v_remarks = p_remarks;
    SET v_payment_type = p_payment_type;
    SET v_reference_no = p_reference_no;

    IF p_concessionaire_id IS NULL OR p_concessionaire_id <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    IF p_scf_amount < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'SCF amount cannot be negative';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others amount cannot be negative';
    END IF;

    IF p_scf_amount = 0 AND p_others = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nothing to collect';
    END IF;

    START TRANSACTION;

    IF EXISTS (
        SELECT 1
        FROM `wdbs_tubungan_db`.`collection`
        WHERE or_number = v_or_number
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF NOT EXISTS (
        SELECT 1
        FROM `wdbs_tubungan_db`.`concessionaire`
        WHERE concessionaire_id = p_concessionaire_id
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid concessionaire';
    END IF;

    SELECT
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    INTO
        v_concessionaire_code,
        v_concessionaire_name,
        v_concessionaire_address
    FROM `wdbs_tubungan_db`.`concessionaire` c
    WHERE c.concessionaire_id = p_concessionaire_id
    LIMIT 1;

    SET v_payor_name_clean = COALESCE(NULLIF(TRIM(p_payor_name), ''), v_concessionaire_name);

    SELECT
        sb.scf_id,
        COALESCE(sb.monthly, 0.00),
        COALESCE(sb.balance, 0.00)
    INTO
        v_scf_id,
        v_scf_monthly,
        v_scf_balance_before
    FROM `wdbs_tubungan_db`.`scf_balance` sb
    WHERE sb.concessionaire_id = p_concessionaire_id
    ORDER BY sb.scf_id DESC
    LIMIT 1
    FOR UPDATE;

    IF v_scf_id IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'SCF balance record not found';
    END IF;

    IF p_scf_amount > v_scf_balance_before THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'SCF amount cannot be greater than the available SCF balance';
    END IF;

    SET v_scf_amount_used = ROUND(p_scf_amount, 2);
    SET v_scf_balance_after = ROUND(v_scf_balance_before - v_scf_amount_used, 2);

    UPDATE `wdbs_tubungan_db`.`scf_balance`
    SET
        balance = v_scf_balance_after,
        updated_at = NOW()
    WHERE scf_id = v_scf_id;

    SELECT COALESCE(SUM(b.remaining_balance), 0.00)
    INTO v_uncollected
    FROM `wdbs_tubungan_db`.`billing` b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0;

    SET v_grand_total = ROUND(v_scf_amount_used + v_total_others, 2);
    SET v_amount_received = v_grand_total;
    SET v_change_amount = 0.00;

    INSERT INTO `wdbs_tubungan_db`.`collection` (
        or_number,
        collection_date,
        bill_numbers,
        concessionaire_code,
        concessionaire_name,
        address,
        payor_name,
        total_current_bill,
        total_arrears,
        total_penalty,
        total_tax,
        total_scf,
        total_water_bill_paid,
        scf_paid,
        total_others,
        grand_total,
        amount_received,
        change_amount,
        total_paid_amount,
        uncollected,
        created_by,
        created_by_name,
        remarks,
        created_at,
        updated_at,
        payment_type,
        payment_reference,
        payment_ids,
        total_discount,
        status,
        voided_by_user_id,
        voided_at,
        billing_count,
        payment_count
    )
    VALUES (
        v_or_number,
        p_payment_date,
        '',
        v_concessionaire_code,
        v_concessionaire_name,
        v_concessionaire_address,
        v_payor_name_clean,
        0.00,
        0.00,
        0.00,
        0.00,
        v_scf_amount_used,
        0.00,
        v_scf_amount_used,
        v_total_others,
        v_grand_total,
        v_amount_received,
        v_change_amount,
        v_grand_total,
        v_uncollected,
        p_user_id,
        '',
        v_remarks,
        NOW(),
        NOW(),
        v_payment_type,
        v_reference_no,
        '',
        0.00,
        'POSTED',
        NULL,
        NULL,
        0,
        0
    );

    SET v_collection_id = LAST_INSERT_ID();

    COMMIT;
INSERT INTO user_logs (
    user_id,
    action_type,
    module,
    entity_name,
    entity_id,
    description
)
VALUES (
    p_user_id,
    'COLLECT',
    'SCF',
    'collection',
    v_or_number,
    CONCAT(
        'SCF collection posted. OR#: ', v_or_number,
        ', Concessionaire: ', v_concessionaire_name,
        ', SCF Amount: ₱', FORMAT(v_scf_amount_used, 2),
        ', Others: ₱', FORMAT(v_total_others, 2),
        ', Grand Total: ₱', FORMAT(v_grand_total, 2),
        '.'
    )
);
    SELECT
        v_collection_id AS collection_id,
        v_or_number AS or_number,
        p_concessionaire_id AS concessionaire_id,
        v_concessionaire_code AS concessionaire_code,
        v_concessionaire_name AS concessionaire_name,
        v_concessionaire_address AS address,
        v_payor_name_clean AS payor_name,
        v_scf_amount_used AS scf_collected,
        v_total_others AS others_collected,
        v_grand_total AS grand_total,
        v_amount_received AS amount_received,
        v_change_amount AS change_amount,
        v_uncollected AS uncollected,
        v_scf_balance_before AS scf_balance_before,
        v_scf_balance_after AS scf_balance_after,
        p_payment_date AS payment_date,
        v_payment_type AS payment_type,
        v_reference_no AS payment_reference,
        v_remarks AS remarks;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_preview_collection_v46` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_preview_collection_v46`(
    IN p_payment_date DATETIME,
    IN p_amount_received DECIMAL(12,2),
    IN p_others DECIMAL(12,2),
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_pay_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_pay_scf DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_new_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_new_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_cash DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_change_amount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_penalty_generated_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_payment_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;
    DECLARE v_json_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_selected_code VARCHAR(50) DEFAULT '';
    DECLARE v_selected_name VARCHAR(255) DEFAULT '';
    DECLARE v_selected_address VARCHAR(255) DEFAULT '';

    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_row_paid DECIMAL(12,2) DEFAULT 0.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        DROP TEMPORARY TABLE IF EXISTS tmp_fifo;
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_amount_received = COALESCE(p_amount_received, 0.00);
    SET p_others = COALESCE(p_others, 0.00);

    IF p_amount_received <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid amount received';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_others > p_amount_received THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Others cannot be greater than amount received';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*), JSON_LENGTH(p_json_concessionaires)
    INTO v_selected_count, v_json_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    IF v_selected_count <> v_json_count THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'One or more concessionaires are invalid or duplicated';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    IF v_selected_count = 1 THEN
        SELECT concessionaire_code, concessionaire_name, address
        INTO v_selected_code, v_selected_name, v_selected_address
        FROM tmp_selected
        LIMIT 1;
    ELSE
        SET v_selected_code = 'MULTIPLE';
        SET v_selected_name = 'MULTIPLE';
        SET v_selected_address = 'MULTIPLE';
    END IF;

    IF v_bill_count = 0 AND p_others <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No unpaid billing found for the selected concessionaires';
    END IF;

    DROP TEMPORARY TABLE IF EXISTS tmp_fifo;
    CREATE TEMPORARY TABLE tmp_fifo (
        billing_id INT NOT NULL PRIMARY KEY,
        bill_number VARCHAR(50) NOT NULL,
        concessionaire_id INT NOT NULL,
        current_bill_id INT DEFAULT NULL,
        due_date DATE DEFAULT NULL,
        is_penalty_applied TINYINT DEFAULT 0,
        penalty_percent_used DECIMAL(12,2) DEFAULT 10.00,
        discount_amount DECIMAL(12,2) DEFAULT 0.00,
        remaining_water_charge DECIMAL(12,2) DEFAULT 0.00,
        remaining_tax_amount DECIMAL(12,2) DEFAULT 0.00,
        remaining_penalty_amount DECIMAL(12,2) DEFAULT 0.00,
        remaining_scf_amount DECIMAL(12,2) DEFAULT 0.00,
        remaining_balance DECIMAL(12,2) DEFAULT 0.00,
        billing_date DATE DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_fifo (
        billing_id,
        bill_number,
        concessionaire_id,
        current_bill_id,
        due_date,
        is_penalty_applied,
        penalty_percent_used,
        discount_amount,
        remaining_water_charge,
        remaining_tax_amount,
        remaining_penalty_amount,
        remaining_scf_amount,
        remaining_balance,
        billing_date
    )
    SELECT
        b.billing_id,
        b.bill_number,
        b.concessionaire_id,
        s.current_bill_id,
        b.due_date,
        COALESCE(b.is_penalty_applied, 0),
        COALESCE(b.penalty_percent_used, v_default_penalty_percent),
        COALESCE(b.discount_amount, 0.00),
        COALESCE(b.remaining_water_charge, 0.00),
        COALESCE(b.remaining_tax_amount, 0.00),
        COALESCE(b.remaining_penalty_amount, 0.00),
        COALESCE(b.remaining_scf_amount, 0.00),
        COALESCE(b.remaining_balance, 0.00),
        b.billing_date
    FROM billing b
    JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND GREATEST(v_rem_water, 0) > 0 THEN

            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
            SET v_penalty_generated_total = ROUND(v_penalty_generated_total + v_generated_penalty, 2);

            SET v_rem_penalty = ROUND(v_rem_penalty + v_generated_penalty, 2);
            SET v_rem_balance = ROUND(v_rem_balance + v_generated_penalty, 2);
        END IF;

        SET v_pay_penalty = 0.00;
        SET v_pay_water = 0.00;
        SET v_pay_tax = 0.00;
        SET v_pay_scf = 0.00;

        IF v_cash > 0 THEN
            SET v_pay_penalty = LEAST(v_cash, v_rem_penalty);
            SET v_cash = ROUND(v_cash - v_pay_penalty, 2);

            SET v_pay_water = LEAST(v_cash, v_rem_water);
            SET v_cash = ROUND(v_cash - v_pay_water, 2);

            SET v_pay_tax = LEAST(v_cash, v_rem_tax);
            SET v_cash = ROUND(v_cash - v_pay_tax, 2);

            SET v_pay_scf = LEAST(v_cash, v_rem_scf);
            SET v_cash = ROUND(v_cash - v_pay_scf, 2);
        END IF;

        SET v_new_penalty = ROUND(GREATEST(v_rem_penalty - v_pay_penalty, 0), 2);
        SET v_new_water   = ROUND(GREATEST(v_rem_water - v_pay_water, 0), 2);
        SET v_new_tax     = ROUND(GREATEST(v_rem_tax - v_pay_tax, 0), 2);
        SET v_new_scf     = ROUND(GREATEST(v_rem_scf - v_pay_scf, 0), 2);
        SET v_new_balance = ROUND(v_new_penalty + v_new_water + v_new_tax + v_new_scf, 2);

        SET v_row_paid = ROUND(v_pay_penalty + v_pay_water + v_pay_tax + v_pay_scf, 2);

        IF v_row_paid > 0 THEN
            SET v_processed_bill_count = v_processed_bill_count + 1;
            SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

            IF v_bill_id = v_current_bill_id THEN
                SET v_total_current = ROUND(v_total_current + v_pay_water, 2);
            ELSE
                SET v_total_arrears = ROUND(v_total_arrears + v_pay_water, 2);
            END IF;

            SET v_total_penalty = ROUND(v_total_penalty + v_pay_penalty, 2);
            SET v_total_tax = ROUND(v_total_tax + v_pay_tax, 2);
            SET v_total_scf = ROUND(v_total_scf + v_pay_scf, 2);
            SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);
            SET v_payment_count = v_payment_count + 1;
        END IF;
    END LOOP;

    CLOSE cur;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_total_paid = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);
    SET v_change_amount = ROUND(p_amount_received - v_total_paid, 2);

    SELECT
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(v_total_scf, 2) AS scf_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_total_paid, 2) AS grand_total,
        ROUND(p_amount_received, 2) AS amount_received,
        ROUND(v_change_amount, 2) AS change_amount,
        ROUND(v_total_paid, 2) AS total_paid_amount,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        v_payment_count AS payment_count,
        ROUND(v_penalty_generated_total, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_fifo;
    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_preview_collection_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_preview_collection_v48`(
    IN p_or_number VARCHAR(50),
    IN p_payment_date DATETIME,
    IN p_payment_type VARCHAR(50),
    IN p_reference_no VARCHAR(100),
    IN p_remarks TEXT,
    IN p_payor_name VARCHAR(255),
    IN p_others DECIMAL(12,2),
    IN p_json_concessionaires JSON
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    DECLARE v_bill_id INT;
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_current_bill_id INT;
    DECLARE v_bill_due_date DATE DEFAULT NULL;
    DECLARE v_is_penalty_applied TINYINT DEFAULT 0;
    DECLARE v_penalty_percent_used DECIMAL(12,2) DEFAULT 10.00;
    DECLARE v_bill_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_bill_date DATE DEFAULT NULL;

    DECLARE v_rem_water DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_rem_balance DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_generated_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_line_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_discount DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_total_water_bill_paid DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0.00;

    DECLARE v_bill_count_loaded INT DEFAULT 0;
    DECLARE v_processed_bill_count INT DEFAULT 0;
    DECLARE v_selected_count INT DEFAULT 0;
    DECLARE v_json_count INT DEFAULT 0;

    DECLARE v_bill_numbers LONGTEXT DEFAULT '';
    DECLARE v_preview_text LONGTEXT DEFAULT '';
    DECLARE v_line LONGTEXT DEFAULT '';

    DECLARE v_selected_code LONGTEXT DEFAULT '';
    DECLARE v_selected_name LONGTEXT DEFAULT '';
    DECLARE v_selected_address LONGTEXT DEFAULT '';
    DECLARE v_payor_name_clean LONGTEXT DEFAULT '';
    DECLARE v_default_penalty_percent DECIMAL(12,2) DEFAULT 10.00;

    DECLARE cur CURSOR FOR
        SELECT
            b.billing_id,
            b.bill_number,
            b.concessionaire_id,
            s.current_bill_id,
            b.due_date,
            COALESCE(b.is_penalty_applied, 0),
            COALESCE(b.penalty_percent_used, v_default_penalty_percent),
            COALESCE(b.discount_amount, 0.00),
            COALESCE(b.remaining_water_charge, 0.00),
            COALESCE(b.remaining_tax_amount, 0.00),
            COALESCE(b.remaining_penalty_amount, 0.00),
            COALESCE(b.remaining_scf_amount, 0.00),
            COALESCE(b.remaining_balance, 0.00),
            b.billing_date
        FROM billing b
        JOIN tmp_selected s
            ON s.concessionaire_id = b.concessionaire_id
        WHERE b.remaining_balance > 0
        ORDER BY b.billing_date ASC, b.billing_id ASC;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        DROP TEMPORARY TABLE IF EXISTS tmp_selected;
        RESIGNAL;
    END;

    SET SESSION group_concat_max_len = 100000;

    SET p_payment_date = COALESCE(p_payment_date, NOW());
    SET p_remarks = COALESCE(p_remarks, '');
    SET p_payor_name = COALESCE(NULLIF(TRIM(p_payor_name), ''), '');
    SET p_payment_type = COALESCE(TRIM(p_payment_type), '');
    SET v_payor_name_clean = p_payor_name;

    IF p_or_number IS NULL OR TRIM(p_or_number) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'OR required';
    END IF;

    IF p_others < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid others amount';
    END IF;

    IF p_json_concessionaires IS NULL OR JSON_VALID(p_json_concessionaires) = 0 OR JSON_LENGTH(p_json_concessionaires) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No concessionaire selected';
    END IF;

    IF p_payment_type IS NULL OR TRIM(p_payment_type) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Payment type is required';
    END IF;

    SELECT COALESCE(
        CAST(MAX(CASE WHEN settings_key = 'penalty_percent' THEN settings_value END) AS DECIMAL(12,2)),
        10.00
    )
    INTO v_default_penalty_percent
    FROM system_settings;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
    CREATE TEMPORARY TABLE tmp_selected (
        concessionaire_id INT NOT NULL PRIMARY KEY,
        concessionaire_code VARCHAR(50) NOT NULL DEFAULT '',
        concessionaire_name VARCHAR(255) NOT NULL DEFAULT '',
        address VARCHAR(255) NOT NULL DEFAULT '',
        current_bill_id INT DEFAULT NULL
    ) ENGINE=InnoDB;

    INSERT INTO tmp_selected (
        concessionaire_id,
        concessionaire_code,
        concessionaire_name,
        address
    )
    SELECT DISTINCT
        c.concessionaire_id,
        COALESCE(c.concessionaire_code, ''),
        COALESCE(c.concessionaire_name, ''),
        COALESCE(c.address, '')
    FROM concessionaire c
    INNER JOIN JSON_TABLE(
        p_json_concessionaires,
        '$[*]'
        COLUMNS(
            concessionaire_id INT PATH '$'
        )
    ) jt
        ON jt.concessionaire_id = c.concessionaire_id;

    SELECT COUNT(*), JSON_LENGTH(p_json_concessionaires)
    INTO v_selected_count, v_json_count
    FROM tmp_selected;

    IF v_selected_count = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'No valid concessionaire selected';
    END IF;

    IF v_selected_count <> v_json_count THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'One or more concessionaires are invalid or duplicated';
    END IF;

    UPDATE tmp_selected t
    SET t.current_bill_id = (
        SELECT b.billing_id
        FROM billing b
        WHERE b.concessionaire_id = t.concessionaire_id
          AND b.remaining_balance > 0
        ORDER BY b.billing_date DESC, b.billing_id DESC
        LIMIT 1
    );

    SELECT COUNT(*)
    INTO v_bill_count_loaded
    FROM billing b
    INNER JOIN tmp_selected s
        ON s.concessionaire_id = b.concessionaire_id
    WHERE b.remaining_balance > 0;

    SELECT
        COALESCE(GROUP_CONCAT(DISTINCT concessionaire_code ORDER BY concessionaire_code SEPARATOR ', '), ''),
        COALESCE(GROUP_CONCAT(DISTINCT concessionaire_name ORDER BY concessionaire_name SEPARATOR ', '), ''),
        COALESCE(GROUP_CONCAT(DISTINCT address ORDER BY address SEPARATOR ', '), '')
    INTO
        v_selected_code,
        v_selected_name,
        v_selected_address
    FROM tmp_selected;

    SET v_preview_text = CONCAT(
        'Collection Preview', CHAR(10),
        'OR Number: ', p_or_number, CHAR(10),
        'Concessionaire(s): ', v_selected_name, CHAR(10),
        'Payor Name: ', v_payor_name_clean, CHAR(10),
        'Payment Type: ', p_payment_type, CHAR(10),
        'Reference No: ', COALESCE(p_reference_no, ''), CHAR(10),
        'Remarks: ', COALESCE(p_remarks, ''), CHAR(10),
        'Attached Others: ', FORMAT(p_others, 2), CHAR(10),
        REPEAT('-', 70), CHAR(10)
    );

    OPEN cur;

    read_loop: LOOP
        FETCH cur INTO
            v_bill_id,
            v_bill_number,
            v_concessionaire_id,
            v_current_bill_id,
            v_bill_due_date,
            v_is_penalty_applied,
            v_penalty_percent_used,
            v_bill_discount,
            v_rem_water,
            v_rem_tax,
            v_rem_penalty,
            v_rem_scf,
            v_rem_balance,
            v_bill_date;

        IF v_done = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_generated_penalty = 0.00;

        IF v_bill_id = v_current_bill_id
           AND v_bill_due_date IS NOT NULL
           AND DATE(p_payment_date) > DATE(v_bill_due_date)
           AND COALESCE(v_is_penalty_applied, 0) = 0
           AND GREATEST(v_rem_water, 0) > 0 THEN
            SET v_generated_penalty = ROUND(GREATEST(v_rem_water, 0) * (COALESCE(v_penalty_percent_used, v_default_penalty_percent) / 100), 2);
        END IF;

        SET v_line_total = ROUND(v_rem_balance + v_generated_penalty, 2);

        SET v_line = CONCAT(
            'Bill Number: ', v_bill_number,
            ' | Billing Date: ', DATE_FORMAT(v_bill_date, '%Y-%m-%d'),
            ' | ', CASE WHEN v_bill_id = v_current_bill_id THEN 'Current Bill' ELSE 'Arrears Bill' END,
            ' | Water Charge: ', FORMAT(v_rem_water, 2),
            ' | Tax: ', FORMAT(v_rem_tax, 2),
            ' | Penalty: ', FORMAT(v_rem_penalty + v_generated_penalty, 2),
            ' | SCF: ', FORMAT(v_rem_scf, 2),
            ' | Total Due: ', FORMAT(v_line_total, 2)
        );

        SET v_preview_text = CONCAT(v_preview_text, v_line, CHAR(10));

        SET v_processed_bill_count = v_processed_bill_count + 1;
        SET v_bill_numbers = CONCAT_WS(', ', NULLIF(v_bill_numbers, ''), v_bill_number);

        IF v_bill_id = v_current_bill_id THEN
            SET v_total_current = ROUND(v_total_current + v_rem_water, 2);
        ELSE
            SET v_total_arrears = ROUND(v_total_arrears + v_rem_water, 2);
        END IF;

        SET v_total_tax = ROUND(v_total_tax + v_rem_tax, 2);
        SET v_total_penalty = ROUND(v_total_penalty + v_rem_penalty + v_generated_penalty, 2);
        SET v_total_scf = ROUND(v_total_scf + v_rem_scf, 2);
        SET v_total_discount = ROUND(v_total_discount + v_bill_discount, 2);
    END LOOP;

    CLOSE cur;

    SET v_total_water_bill_paid = ROUND(v_total_current + v_total_arrears + v_total_penalty + v_total_tax, 2);
    SET v_grand_total = ROUND(v_total_water_bill_paid + v_total_scf + p_others, 2);

    SET v_preview_text = CONCAT(
        v_preview_text, CHAR(10),
        REPEAT('-', 70), CHAR(10),
        'Total Current Bill: ', FORMAT(v_total_current, 2), CHAR(10),
        'Total Arrears: ', FORMAT(v_total_arrears, 2), CHAR(10),
        'Total Penalty: ', FORMAT(v_total_penalty, 2), CHAR(10),
        'Total Tax: ', FORMAT(v_total_tax, 2), CHAR(10),
        'Total SCF: ', FORMAT(v_total_scf, 2), CHAR(10),
        'Total Discount (Reference): ', FORMAT(v_total_discount, 2), CHAR(10),
        'Total Water Bill Paid: ', FORMAT(v_total_water_bill_paid, 2), CHAR(10),
        'Attached Others: ', FORMAT(p_others, 2), CHAR(10),
        'Full Amount To Be Paid: ', FORMAT(v_grand_total, 2), CHAR(10)
    );

    SELECT
        v_preview_text AS preview_text,
        v_selected_code AS concessionaire_code,
        v_selected_name AS concessionaire_name,
        v_selected_address AS address,
        v_payor_name_clean AS payor_name,
        v_bill_numbers AS bill_numbers,
        ROUND(v_total_current, 2) AS total_current_bill,
        ROUND(v_total_arrears, 2) AS total_arrears,
        ROUND(v_total_penalty, 2) AS total_penalty,
        ROUND(v_total_tax, 2) AS total_tax,
        ROUND(v_total_scf, 2) AS total_scf,
        ROUND(v_total_water_bill_paid, 2) AS total_water_bill_paid,
        ROUND(p_others, 2) AS total_others,
        ROUND(v_grand_total, 2) AS grand_total,
        ROUND(v_total_discount, 2) AS total_discount,
        v_processed_bill_count AS billing_count,
        ROUND(0.00, 2) AS change_amount,
        ROUND(0.00, 2) AS amount_received,
        ROUND(0.00, 2) AS total_paid_amount,
        ROUND(0.00, 2) AS penalty_generated_total;

    DROP TEMPORARY TABLE IF EXISTS tmp_selected;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_reverse_collection_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reverse_collection_v48`(
    IN p_or_number VARCHAR(50)
)
BEGIN
    DECLARE v_collection_id INT;


    SELECT collection_id
    INTO v_collection_id
    FROM collection
    WHERE or_number = p_or_number
    LIMIT 1;

    IF v_collection_id IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Collection not found';
    END IF;

    START TRANSACTION;


    UPDATE billing b
    JOIN payment p ON p.billing_id = b.billing_id
    SET
        b.remaining_water_charge = ROUND(b.remaining_water_charge + p.water_paid, 2),
        b.remaining_tax_amount = ROUND(b.remaining_tax_amount + p.tax_paid, 2),
        b.remaining_penalty_amount = ROUND(b.remaining_penalty_amount + p.penalty_paid, 2),
        b.remaining_scf_amount = ROUND(b.remaining_scf_amount + p.scf_paid, 2),
        b.remaining_balance = ROUND(b.remaining_balance + p.amount_paid, 2),

        b.payment_count = GREATEST(b.payment_count - 1, 0),


        b.status = CASE
            WHEN (b.remaining_balance + p.amount_paid) = 0 THEN 'paid'
            ELSE 'unpaid'
        END,

        b.updated_at = NOW(),
        b.updated_by_user_id = NULL

    WHERE p.collection_id = v_collection_id
      AND p.billing_id IS NOT NULL;


    UPDATE billing
    SET last_collection_id = NULL,
        last_payment_date = NULL
    WHERE last_collection_id = v_collection_id;


    DELETE FROM payment
    WHERE collection_id = v_collection_id;

    DELETE FROM collection
    WHERE collection_id = v_collection_id;

    COMMIT;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_reverse_collection_v49` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reverse_collection_v49`(
    IN p_or_number VARCHAR(50)
)
BEGIN
    DECLARE v_collection_id INT;

    -- Get collection
    SELECT collection_id
    INTO v_collection_id
    FROM collection
    WHERE or_number = p_or_number
    LIMIT 1;

    IF v_collection_id IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Collection not found';
    END IF;

    START TRANSACTION;

    -- ===============================
    -- 1. REVERSE BILLING PAYMENTS
    -- ===============================
    UPDATE billing b
    JOIN payment p ON p.billing_id = b.billing_id
    SET
        b.remaining_water_charge = ROUND(b.remaining_water_charge + p.water_paid, 2),
        b.remaining_tax_amount = ROUND(b.remaining_tax_amount + p.tax_paid, 2),
        b.remaining_penalty_amount = ROUND(b.remaining_penalty_amount + p.penalty_paid, 2),
        b.remaining_scf_amount = ROUND(b.remaining_scf_amount + p.scf_paid, 2),
        b.remaining_balance = ROUND(b.remaining_balance + p.amount_paid, 2),

        b.payment_count = GREATEST(b.payment_count - 1, 0),

        b.status = CASE
            WHEN (b.remaining_balance + p.amount_paid) = 0 THEN 'paid'
            ELSE 'unpaid'
        END,

        b.updated_at = NOW(),
        b.updated_by_user_id = NULL

    WHERE p.collection_id = v_collection_id
      AND p.billing_id IS NOT NULL;

    -- ===============================
    -- 2. REVERSE SCF BALANCE (IMPORTANT)
    -- ===============================
    UPDATE scf_balance sb
    JOIN payment p ON p.collection_id = v_collection_id
    SET
        sb.balance = ROUND(sb.balance + p.scf_paid, 2),
        sb.updated_at = NOW()
    WHERE sb.concessionaire_id = (
        SELECT b.concessionaire_id
        FROM billing b
        WHERE b.billing_id = p.billing_id
        LIMIT 1
    )
    AND p.scf_paid > 0;

    -- ===============================
    -- 3. CLEAN BILLING LINKS
    -- ===============================
    UPDATE billing
    SET last_collection_id = NULL,
        last_payment_date = NULL
    WHERE last_collection_id = v_collection_id;

    -- ===============================
    -- 4. DELETE RECORDS
    -- ===============================
    DELETE FROM payment
    WHERE collection_id = v_collection_id;

    DELETE FROM collection
    WHERE collection_id = v_collection_id;

    COMMIT;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_void_billing_v1` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_void_billing_v1`(
    IN p_billing_id INT,
    IN p_voided_by_user_id INT,
    IN p_void_remarks TEXT
)
sp_main: BEGIN
    DECLARE v_bill_number VARCHAR(50);
    DECLARE v_concessionaire_id INT;
    DECLARE v_billing_date DATE;
    DECLARE v_due_date DATE;
    DECLARE v_consumption INT;
    DECLARE v_water_charge DECIMAL(12,2);
    DECLARE v_tax DECIMAL(12,2);
    DECLARE v_discount DECIMAL(12,2);
    DECLARE v_total_water DECIMAL(12,2);
    DECLARE v_scf_amount DECIMAL(12,2);
    DECLARE v_scf_monthly DECIMAL(12,2);  -- <-- monthly only
    DECLARE v_arrears DECIMAL(12,2);
    DECLARE v_penalty DECIMAL(12,2);
    DECLARE v_total DECIMAL(12,2);
    DECLARE v_status VARCHAR(20);
    DECLARE v_request_id VARCHAR(200);

    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;
    SET p_void_remarks = COALESCE(NULLIF(TRIM(p_void_remarks),''),'No reason');

    START TRANSACTION;

    -- 1. Get full billing + the monthly SCF used
    SELECT b.bill_number, b.concessionaire_id, b.billing_date, b.due_date,
           b.consumption, b.water_charge, b.tax_amount, b.discount_amount,
           b.total_water_bill, b.scf_amount, b.scf_monthly_used,
           b.arrears_amount, b.penalty_amount, b.total_amount,
           b.status, b.request_id
    INTO v_bill_number, v_concessionaire_id, v_billing_date, v_due_date,
         v_consumption, v_water_charge, v_tax, v_discount,
         v_total_water, v_scf_amount, v_scf_monthly,
         v_arrears, v_penalty, v_total,
         v_status, v_request_id
    FROM billing b
    WHERE b.billing_id = p_billing_id
    FOR UPDATE;

    IF v_bill_number IS NULL THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Billing not found'; END IF;

    -- 2. RESTORE SCF - MONTHLY ONLY (not whole balance)
    IF COALESCE(v_scf_amount,0) > 0 THEN
        -- v_scf_amount = LEAST(monthly, balance_before) from create bill
        -- so we add back exactly what was deducted
        UPDATE scf_balance sb
        SET sb.balance = ROUND(COALESCE(sb.balance,0) + v_scf_amount, 2),
            sb.updated_at = NOW()
        WHERE sb.concessionaire_id = v_concessionaire_id
        ORDER BY sb.scf_id DESC
        LIMIT 1;
    END IF;

    -- 3. Delete the bill
    DELETE FROM billing WHERE billing_id = p_billing_id;

    -- 4. Full audit log
    INSERT INTO user_logs (user_id, action_type, module, entity_name, entity_id, description, old_value, created_at)
    VALUES (
        p_voided_by_user_id, 'VOID', 'BILLING', 'billing', v_bill_number,
        CONCAT(
            'BILL VOIDED | #',v_bill_number,' | Date:',DATE_FORMAT(v_billing_date,'%Y-%m-%d'),
            ' | Consumption:',v_consumption,' | Water:₱',FORMAT(v_water_charge,2),
            ' | Tax:₱',FORMAT(v_tax,2),' | SCF Monthly Restored:₱',FORMAT(v_scf_amount,2),
            ' (Monthly rate was ₱',FORMAT(v_scf_monthly,2),')',
            ' | Total:₱',FORMAT(v_total,2),' | Reason:',p_void_remarks
        ),
        JSON_OBJECT('bill_number',v_bill_number,'concessionaire_id',v_concessionaire_id,
                    'scf_amount_billed',v_scf_amount,'scf_monthly_rate',v_scf_monthly,
                    'total_amount',v_total,'void_reason',p_void_remarks),
        NOW()
    );

    COMMIT;

    SELECT p_billing_id AS billing_id, v_bill_number AS bill_number,
           v_scf_amount AS scf_monthly_restored, 'VOIDED' AS status;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_void_collection_v48` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_void_collection_v48`(
    IN p_collection_id INT,
    IN p_voided_by_user_id INT,
    IN p_void_remarks TEXT
)
sp_main: BEGIN
    DECLARE v_done INT DEFAULT 0;

    -- Collection data
    DECLARE v_target_id INT DEFAULT p_collection_id;
    DECLARE v_status VARCHAR(50) DEFAULT '';
    DECLARE v_or_number VARCHAR(50) DEFAULT '';
    DECLARE v_collection_date DATETIME;
    DECLARE v_concessionaire_code VARCHAR(50) DEFAULT '';
    DECLARE v_concessionaire_name VARCHAR(255) DEFAULT '';
    DECLARE v_bill_numbers TEXT DEFAULT '';
    DECLARE v_payment_ids TEXT DEFAULT '';
    DECLARE v_total_current DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_arrears DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_penalty DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_tax DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_scf DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_water_paid DECIMAL(12,2) DEFAULT 0;
    DECLARE v_grand_total DECIMAL(12,2) DEFAULT 0;
    DECLARE v_amount_received DECIMAL(12,2) DEFAULT 0;
    DECLARE v_remarks TEXT DEFAULT '';

    -- Loop vars
    DECLARE v_payment_id INT;
    DECLARE v_billing_id INT;
    DECLARE v_water_paid DECIMAL(12,2);
    DECLARE v_tax_paid DECIMAL(12,2);
    DECLARE v_penalty_paid DECIMAL(12,2);
    DECLARE v_scf_paid DECIMAL(12,2);
    DECLARE v_amount_paid DECIMAL(12,2);
    
    -- Totals restored
    DECLARE v_voided_cnt INT DEFAULT 0;
    DECLARE v_restored_water DECIMAL(12,2) DEFAULT 0;
    DECLARE v_restored_tax DECIMAL(12,2) DEFAULT 0;
    DECLARE v_restored_penalty DECIMAL(12,2) DEFAULT 0;
    DECLARE v_restored_scf DECIMAL(12,2) DEFAULT 0;
    DECLARE v_restored_total DECIMAL(12,2) DEFAULT 0;
    DECLARE v_scf_concessionaire_id INT;

    DECLARE cur_payments CURSOR FOR
        SELECT p.payment_id, p.billing_id,
               COALESCE(p.water_paid,0), COALESCE(p.tax_paid,0),
               COALESCE(p.penalty_paid,0), COALESCE(p.scf_paid,0),
               COALESCE(p.amount_paid,0)
        FROM payment p
        WHERE p.collection_id = v_target_id AND p.status IN ('POSTED','PAID')
        FOR UPDATE;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_done = 1;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    SET p_void_remarks = COALESCE(NULLIF(TRIM(p_void_remarks),''),'No reason provided');

    START TRANSACTION;

    -- 1. Load full collection
    SELECT c.status, c.or_number, c.collection_date, c.concessionaire_code, c.concessionaire_name,
           c.bill_numbers, c.payment_ids, c.total_current_bill, c.total_arrears, c.total_penalty,
           c.total_tax, c.total_scf, c.total_water_bill_paid, c.grand_total, c.amount_received, c.remarks
    INTO v_status, v_or_number, v_collection_date, v_concessionaire_code, v_concessionaire_name,
         v_bill_numbers, v_payment_ids, v_total_current, v_total_arrears, v_total_penalty,
         v_total_tax, v_total_scf, v_total_water_paid, v_grand_total, v_amount_received, v_remarks
    FROM collection c WHERE c.collection_id = v_target_id FOR UPDATE;

    IF v_status IS NULL THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Collection not found'; END IF;

    DROP TEMPORARY TABLE IF EXISTS tmp_affected_billing;
    CREATE TEMPORARY TABLE tmp_affected_billing (billing_id INT PRIMARY KEY);

    -- 2. Restore billings
    OPEN cur_payments;
    loop_pay: LOOP
        FETCH cur_payments INTO v_payment_id, v_billing_id, v_water_paid, v_tax_paid, v_penalty_paid, v_scf_paid, v_amount_paid;
        IF v_done THEN LEAVE loop_pay; END IF;

        INSERT IGNORE INTO tmp_affected_billing VALUES (v_billing_id);

        UPDATE billing SET
            remaining_water_charge = ROUND(COALESCE(remaining_water_charge,0)+v_water_paid,2),
            remaining_tax_amount = ROUND(COALESCE(remaining_tax_amount,0)+v_tax_paid,2),
            remaining_penalty_amount = ROUND(COALESCE(remaining_penalty_amount,0)+v_penalty_paid,2),
            remaining_scf_amount = ROUND(COALESCE(remaining_scf_amount,0)+v_scf_paid,2),
            remaining_balance = ROUND(COALESCE(remaining_balance,0)+v_amount_paid,2),
            payment_count = GREATEST(COALESCE(payment_count,0)-1,0),
            status='unpaid', scf_status='unpaid', paid_at=NULL,
            last_collection_id=NULL, last_payment_date=NULL,
            updated_at=NOW(), updated_by_user_id=p_voided_by_user_id
        WHERE billing_id = v_billing_id;

        UPDATE payment SET status='VOIDED', collection_id=NULL,
            remarks=CONCAT_WS(' | ',remarks,CONCAT('Voided OR#',v_or_number))
        WHERE payment_id=v_payment_id;

        SET v_voided_cnt = v_voided_cnt + 1;
        SET v_restored_water = v_restored_water + v_water_paid;
        SET v_restored_tax = v_restored_tax + v_tax_paid;
        SET v_restored_penalty = v_restored_penalty + v_penalty_paid;
        SET v_restored_scf = v_restored_scf + v_scf_paid;
        SET v_restored_total = v_restored_total + v_amount_paid;
    END LOOP;
    CLOSE cur_payments;

    -- 3. Restore SCF balance - ONLY FOR SCF-ONLY (no payments)
    IF v_voided_cnt = 0 AND v_total_scf > 0 THEN
        SELECT concessionaire_id INTO v_scf_concessionaire_id
        FROM concessionaire 
        WHERE concessionaire_code=v_concessionaire_code OR concessionaire_name=v_concessionaire_name 
        LIMIT 1;
        IF v_scf_concessionaire_id IS NOT NULL THEN
            UPDATE scf_balance 
            SET balance=ROUND(COALESCE(balance,0)+v_total_scf,2), updated_at=NOW()
            WHERE concessionaire_id=v_scf_concessionaire_id 
            ORDER BY scf_id DESC LIMIT 1;
        END IF;
    END IF;

    DELETE FROM collection WHERE collection_id=v_target_id;

    -- 4. VERY DETAILED LOG
    INSERT INTO user_logs (user_id, action_type, module, entity_name, entity_id, description, old_value, created_at)
    VALUES (
        p_voided_by_user_id,
        'VOID',
        'COLLECTION',
        'collection',
        v_target_id,
        CONCAT(
            '=== COLLECTION VOID AUDIT === | ',
            'Collection ID: ', v_target_id, ' | ',
            'OR Number: ', v_or_number, ' | ',
            'Collection Date: ', DATE_FORMAT(v_collection_date,'%Y-%m-%d %H:%i'), ' | ',
            'Concessionaire: [', v_concessionaire_code, '] ', v_concessionaire_name, ' | ',
            'Bills Affected: ', IFNULL(v_bill_numbers,'NONE'), ' (', v_voided_cnt, ' bills) | ',
            'Payment IDs Voided: ', IFNULL(v_payment_ids,'NONE'), ' | ',
            '--- ORIGINAL AMOUNTS --- | ',
            'Current: ₱', FORMAT(v_total_current,2), ' | ',
            'Arrears: ₱', FORMAT(v_total_arrears,2), ' | ',
            'Penalty: ₱', FORMAT(v_total_penalty,2), ' | ',
            'Tax: ₱', FORMAT(v_total_tax,2), ' | ',
            'SCF: ₱', FORMAT(v_total_scf,2), ' | ',
            'Water Paid: ₱', FORMAT(v_total_water_paid,2), ' | ',
            'Grand Total: ₱', FORMAT(v_grand_total,2), ' | ',
            'Amount Received: ₱', FORMAT(v_amount_received,2), ' | ',
            '--- AMOUNTS RESTORED TO BILLING --- | ',
            'Water Restored: ₱', FORMAT(v_restored_water,2), ' | ',
            'Tax Restored: ₱', FORMAT(v_restored_tax,2), ' | ',
            'Penalty Restored: ₱', FORMAT(v_restored_penalty,2), ' | ',
            'SCF Restored to Bills: ₱', FORMAT(v_restored_scf,2), ' | ',
            'Total Balance Restored: ₱', FORMAT(v_restored_total,2), ' | ',
            'SCF Balance Reversed: ₱', FORMAT(IF(v_voided_cnt=0, v_total_scf, 0),2), ' | ',
            '--- VOID DETAILS --- | ',
            'Voided By User ID: ', p_voided_by_user_id, ' | ',
            'Void Date: ', DATE_FORMAT(NOW(),'%Y-%m-%d %H:%i:%s'), ' | ',
            'Reason: ', p_void_remarks, ' | ',
            'Original Remarks: ', IFNULL(v_remarks,'NONE')
        ),
        JSON_OBJECT(
            'collection_id',v_target_id,'or_number',v_or_number,'date',v_collection_date,
            'concessionaire',v_concessionaire_name,'bills',v_bill_numbers,
            'totals',JSON_OBJECT('current',v_total_current,'arrears',v_total_arrears,'penalty',v_total_penalty,'tax',v_total_tax,'scf',v_total_scf,'grand',v_grand_total),
            'restored',JSON_OBJECT('water',v_restored_water,'tax',v_restored_tax,'penalty',v_restored_penalty,'scf',v_restored_scf,'total',v_restored_total),
            'scf_balance_reversed', IF(v_voided_cnt=0, v_total_scf, 0)
        ),
        NOW()
    );

    COMMIT;
    DROP TEMPORARY TABLE IF EXISTS tmp_affected_billing;

    SELECT v_target_id AS collection_id, v_or_number AS or_number, 'DELETED' AS status,
           v_voided_cnt AS bills_restored, v_restored_scf AS scf_restored_to_billing;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `v_aging_of_accounts`
--

/*!50001 DROP VIEW IF EXISTS `v_aging_of_accounts`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_aging_of_accounts` AS select `b`.`billing_id` AS `Billing_ID`,`b`.`bill_number` AS `Bill_No.`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`b`.`billing_date` AS `Billing_Date`,`b`.`due_date` AS `Due_Date`,`b`.`status` AS `Status`,(to_days(curdate()) - to_days(`b`.`due_date`)) AS `Days_Overdue`,(case when ((to_days(curdate()) - to_days(`b`.`due_date`)) <= 0) then `b`.`total_amount` else 0 end) AS `Current_Due`,(case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 1 and 30) then `b`.`total_amount` else 0 end) AS `1–30_Days`,(case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 31 and 60) then `b`.`total_amount` else 0 end) AS `31–60 Days`,(case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 61 and 90) then `b`.`total_amount` else 0 end) AS `61–90 Days`,(case when ((to_days(curdate()) - to_days(`b`.`due_date`)) > 90) then `b`.`total_amount` else 0 end) AS `Over_90_Days`,`b`.`total_amount` AS `Total_Amount` from (`billing` `b` join `concessionaire` `c` on((`c`.`concessionaire_id` = `b`.`concessionaire_id`))) where (`b`.`status` in ('unpaid','partially_paid','overdue')) order by `c`.`concessionaire_name`,`b`.`due_date` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_aging_of_accounts_summary`
--

/*!50001 DROP VIEW IF EXISTS `v_aging_of_accounts_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_aging_of_accounts_summary` AS select `t`.`Account_No` AS `Account_No`,`t`.`Concessionaire_Name` AS `Concessionaire_Name`,round(`t`.`Current`,2) AS `Current`,round(`t`.`1-30_Days`,2) AS `1-30_Days`,round(`t`.`31-60_Days`,2) AS `31-60_Days`,round(`t`.`61-90_Days`,2) AS `61-90_Days`,round(`t`.`91-120_Days`,2) AS `91-120_Days`,round(`t`.`Over_120_Days`,2) AS `Over_120_Days`,round(`t`.`Total`,2) AS `Total` from (select `c`.`zone_id` AS `zone_id`,`c`.`concessionaire_code` AS `Account_No`,max(`c`.`concessionaire_name`) AS `Concessionaire_Name`,sum((case when (`d`.`days` = 0) then `d`.`amount` else 0 end)) AS `Current`,sum((case when (`d`.`days` between 1 and 30) then `d`.`amount` else 0 end)) AS `1-30_Days`,sum((case when (`d`.`days` between 31 and 60) then `d`.`amount` else 0 end)) AS `31-60_Days`,sum((case when (`d`.`days` between 61 and 90) then `d`.`amount` else 0 end)) AS `61-90_Days`,sum((case when (`d`.`days` between 91 and 120) then `d`.`amount` else 0 end)) AS `91-120_Days`,sum((case when (`d`.`days` > 120) then `d`.`amount` else 0 end)) AS `Over_120_Days`,sum(`d`.`amount`) AS `Total` from ((select `b`.`concessionaire_id` AS `concessionaire_id`,`sd`.`aging_date` AS `aging_date`,(to_days(`sd`.`aging_date`) - to_days(`b`.`due_date`)) AS `days`,(case when (abs(((coalesce(`b`.`remaining_water_charge`,0) + coalesce(`b`.`remaining_tax_amount`,0)) + (case when (coalesce(`b`.`is_penalty_applied`,0) = 1) then coalesce(`b`.`remaining_penalty_amount`,0) when ((to_days(`sd`.`aging_date`) - to_days(`b`.`due_date`)) > 0) then round((coalesce(`b`.`remaining_water_charge`,0) * 0.10),2) else 0 end))) < 0.05) then 0 else round(((coalesce(`b`.`remaining_water_charge`,0) + coalesce(`b`.`remaining_tax_amount`,0)) + (case when (coalesce(`b`.`is_penalty_applied`,0) = 1) then coalesce(`b`.`remaining_penalty_amount`,0) when ((to_days(`sd`.`aging_date`) - to_days(`b`.`due_date`)) > 0) then round((coalesce(`b`.`remaining_water_charge`,0) * 0.10),2) else 0 end)),2) end) AS `amount` from (`billing` `b` join (select str_to_date(`system_settings`.`settings_value`,'%Y%m%d') AS `aging_date` from `system_settings` where (`system_settings`.`settings_key` = 'aging_date') limit 1) `sd`) where ((`b`.`due_date` is not null) and (`b`.`due_date` <= `sd`.`aging_date`) and (round(((coalesce(`b`.`remaining_water_charge`,0) + coalesce(`b`.`remaining_tax_amount`,0)) + (case when (coalesce(`b`.`is_penalty_applied`,0) = 1) then coalesce(`b`.`remaining_penalty_amount`,0) when ((to_days(`sd`.`aging_date`) - to_days(`b`.`due_date`)) > 0) then round((coalesce(`b`.`remaining_water_charge`,0) * 0.10),2) else 0 end)),2) > 0.05))) `d` join `concessionaire` `c` on((`c`.`concessionaire_id` = `d`.`concessionaire_id`))) group by `c`.`zone_id`,`c`.`concessionaire_code` having (abs(sum(`d`.`amount`)) >= 0.05)) `t` order by `t`.`zone_id`,`t`.`Account_No` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_aging_of_scf_summary`
--

/*!50001 DROP VIEW IF EXISTS `v_aging_of_scf_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_aging_of_scf_summary` AS select `c`.`concessionaire_id` AS `Concessionaire_ID`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,sum((case when ((to_days(curdate()) - to_days(`b`.`due_date`)) <= 0) then `b`.`scf_amount` else 0 end)) AS `Current`,sum((case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 1 and 30) then `b`.`scf_amount` else 0 end)) AS `1_30_Days`,sum((case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 31 and 60) then `b`.`scf_amount` else 0 end)) AS `31_60_Days`,sum((case when ((to_days(curdate()) - to_days(`b`.`due_date`)) between 61 and 90) then `b`.`scf_amount` else 0 end)) AS `61_90_Days`,sum((case when ((to_days(curdate()) - to_days(`b`.`due_date`)) > 90) then `b`.`scf_amount` else 0 end)) AS `Over_90_Days`,least(sum(`b`.`scf_amount`),ifnull(`sb`.`total_amount`,0)) AS `Total`,ifnull(`sb`.`total_amount`,0) AS `Max_Total_Allowed` from ((`billing` `b` join `concessionaire` `c` on((`c`.`concessionaire_id` = `b`.`concessionaire_id`))) left join `scf_balance` `sb` on((`sb`.`concessionaire_id` = `c`.`concessionaire_id`))) where (`b`.`scf_status` in ('unpaid','overdue','partially_paid')) group by `c`.`concessionaire_id`,`c`.`concessionaire_code`,`c`.`concessionaire_name`,`c`.`address`,`sb`.`total_amount` order by `c`.`concessionaire_code` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_concessionaire`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_concessionaire`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_concessionaire` AS select `b`.`billing_id` AS `billing_id`,`b`.`bill_number` AS `bill_number`,`b`.`billing_date` AS `billing_date`,`c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name` from (`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_dashboard`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_dashboard`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_dashboard` AS select `b`.`billing_id` AS `BillingID`,`b`.`bill_number` AS `BillNumber`,`b`.`concessionaire_id` AS `ConcessionaireID`,`c`.`concessionaire_name` AS `ConcessionaireName`,`c`.`concessionaire_code` AS `ConcessionaireCode`,`c`.`zone_id` AS `ZoneID`,`z`.`zone_name` AS `ZoneName`,`c`.`service_id` AS `ServiceID`,`s`.`service_type` AS `ServiceType`,`b`.`reading_id` AS `ReadingID`,`r`.`reading_date` AS `ReadingDate`,`b`.`billing_date` AS `BillingDate`,`b`.`due_date` AS `DueDate`,`b`.`consumption` AS `CubicMeter`,`b`.`free_water` AS `FreeWater`,`b`.`water_charge` AS `WaterCharge`,`b`.`discount_amount` AS `DiscountAmount`,`b`.`tax_amount` AS `TaxAmount`,`b`.`total_water_bill` AS `TotalWaterBill`,`b`.`scf_amount` AS `SCFAmount`,`b`.`arrears_amount` AS `ArrearsAmount`,`b`.`penalty_amount` AS `PenaltyAmount`,`b`.`total_amount` AS `TotalAmount`,`b`.`status` AS `BillingStatus`,`b`.`scf_status` AS `SCFStatus`,`b`.`is_initial` AS `IsInitial`,`b`.`updated_at` AS `UpdatedAt`,`b`.`last_payment_date` AS `LastPaymentDate`,(case when (`b`.`status` = 'paid') then `b`.`total_amount` else 0 end) AS `PaidAmount`,(case when (`b`.`status` = 'unpaid') then `b`.`total_amount` else 0 end) AS `UnpaidAmount`,(case when (`b`.`status` = 'overdue') then `b`.`total_amount` else 0 end) AS `OverdueAmount`,((`b`.`total_amount` - ifnull(`b`.`discount_amount`,0)) - ifnull(`b`.`tax_amount`,0)) AS `NetAmount`,year(`b`.`billing_date`) AS `BillingYear`,month(`b`.`billing_date`) AS `BillingMonth`,date_format(`b`.`billing_date`,'%b') AS `BillingMonthLabel` from ((((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) left join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_details`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_details`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_details` AS select `b`.`reading_id` AS `reading_id`,`b`.`concessionaire_id` AS `concessionaire_id`,`b`.`billing_id` AS `billing_id`,`b`.`bill_number` AS `bill_number`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`is_tax_exempt` AS `is_tax_exempt`,`c`.`is_due_exempt` AS `is_due_exempt`,`c`.`is_discounted` AS `is_discounted`,coalesce(`r`.`previous_reading`,0) AS `previous_reading`,coalesce(`r`.`present_reading`,0) AS `present_reading`,`r`.`reading_date` AS `current_reading_date`,coalesce((select `r2`.`reading_date` from `reading` `r2` where ((`r2`.`concessionaire_id` = `b`.`concessionaire_id`) and (`r2`.`reading_id` < coalesce(`r`.`reading_id`,0))) order by `r2`.`reading_id` desc limit 1),`c`.`first_reading_date`) AS `previous_reading_date`,`b`.`billing_date` AS `billing_date`,`b`.`due_date` AS `due_date`,`b`.`consumption` AS `consumption`,`b`.`free_water` AS `free_water`,`b`.`water_charge` AS `water_charge`,`b`.`discount_amount` AS `discount_amount`,`b`.`tax_amount` AS `tax_amount`,`b`.`total_water_bill` AS `total_water_bill`,`b`.`scf_amount` AS `scf_amount`,`b`.`arrears_amount` AS `arrears_amount`,`b`.`penalty_amount` AS `penalty_amount`,`b`.`total_amount` AS `total_amount`,`b`.`remaining_water_charge` AS `remaining_water_charge`,`b`.`remaining_tax_amount` AS `remaining_tax_amount`,`b`.`remaining_penalty_amount` AS `remaining_penalty_amount`,`b`.`remaining_scf_amount` AS `remaining_scf_amount`,`b`.`remaining_balance` AS `remaining_balance`,`b`.`status` AS `status`,`b`.`scf_status` AS `scf_status`,`b`.`last_payment_date` AS `last_payment_date`,`b`.`payment_count` AS `payment_count`,`b`.`last_collection_id` AS `last_collection_id`,`b`.`paid_at` AS `paid_at`,`b`.`is_initial` AS `is_initial`,`b`.`is_penalty_applied` AS `is_penalty_applied`,`b`.`penalty_applied_at` AS `penalty_applied_at`,`b`.`tax_percent_used` AS `tax_percent_used`,`b`.`discount_percent_used` AS `discount_percent_used`,`b`.`penalty_percent_used` AS `penalty_percent_used`,`b`.`scf_monthly_used` AS `scf_monthly_used`,`b`.`scf_total_cap_used` AS `scf_total_cap_used`,`b`.`created_at` AS `created_at`,`b`.`updated_at` AS `updated_at`,`b`.`created_by_user_id` AS `created_by_user_id`,`b`.`updated_by_user_id` AS `updated_by_user_id`,`u1`.`full_name` AS `created_by_name`,`u2`.`full_name` AS `updated_by_name`,`b`.`request_id` AS `request_id` from ((((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) left join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) left join `users` `u1` on((`b`.`created_by_user_id` = `u1`.`user_id`))) left join `users` `u2` on((`b`.`updated_by_user_id` = `u2`.`user_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_info`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_info`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_info` AS select `b`.`billing_id` AS `billing_id`,`b`.`bill_number` AS `bill_number`,`b`.`concessionaire_id` AS `concessionaire_id`,`b`.`reading_id` AS `reading_id`,`b`.`billing_date` AS `billing_date`,`b`.`due_date` AS `due_date`,`b`.`consumption` AS `consumption`,`b`.`free_water` AS `free_water`,`b`.`water_charge` AS `water_charge`,`b`.`discount_amount` AS `discount_amount`,`b`.`tax_amount` AS `tax_amount`,`b`.`total_water_bill` AS `total_water_bill`,`b`.`scf_amount` AS `scf_amount`,`b`.`arrears_amount` AS `arrears_amount`,`b`.`penalty_amount` AS `penalty_amount`,`b`.`total_amount` AS `total_amount`,`b`.`remaining_water_charge` AS `remaining_water_charge`,`b`.`remaining_tax_amount` AS `remaining_tax_amount`,`b`.`remaining_penalty_amount` AS `remaining_penalty_amount`,`b`.`remaining_scf_amount` AS `remaining_scf_amount`,`b`.`remaining_balance` AS `remaining_balance`,`b`.`status` AS `status`,`b`.`scf_status` AS `scf_status`,`b`.`updated_at` AS `updated_at`,`b`.`last_payment_date` AS `last_payment_date`,`b`.`is_initial` AS `is_initial`,`b`.`payment_count` AS `payment_count`,`b`.`last_collection_id` AS `last_collection_id`,`b`.`created_by_user_id` AS `created_by_user_id`,`b`.`updated_by_user_id` AS `updated_by_user_id`,`b`.`tax_percent_used` AS `tax_percent_used`,`b`.`discount_percent_used` AS `discount_percent_used`,`b`.`penalty_percent_used` AS `penalty_percent_used`,`b`.`is_penalty_applied` AS `is_penalty_applied`,`b`.`penalty_applied_at` AS `penalty_applied_at`,`b`.`request_id` AS `request_id`,`b`.`created_at` AS `created_at`,`b`.`scf_monthly_used` AS `scf_monthly_used`,`b`.`scf_total_cap_used` AS `scf_total_cap_used`,`b`.`paid_at` AS `paid_at`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`c`.`service_id` AS `service_id`,`c`.`meter_no` AS `meter_no`,`c`.`first_reading_date` AS `first_reading_date`,`c`.`is_tax_exempt` AS `is_tax_exempt`,`c`.`is_due_exempt` AS `is_due_exempt`,`c`.`is_discounted` AS `is_discounted`,`c`.`status` AS `concessionaire_status`,`c`.`tin_number` AS `tin_number` from (`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_report`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_report` AS select `c`.`zone_id` AS `Zone`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`b`.`bill_number` AS `Invoice_Number`,round(`b`.`consumption`,2) AS `Cu_m³`,round(`b`.`free_water`,2) AS `Un_m³`,round(`b`.`water_charge`,2) AS `Water_Bill`,round(`b`.`arrears_amount`,2) AS `Arrears`,round(`b`.`tax_amount`,2) AS `Tax`,round(`b`.`discount_amount`,2) AS `Discount`,round(`b`.`total_water_bill`,2) AS `Total_Water_Bill`,round(`b`.`scf_amount`,2) AS `SCF`,round(`b`.`total_amount`,2) AS `Total_Amount_Billed`,`b`.`is_initial` AS `Initial`,cast(`b`.`billing_date` as date) AS `Date` from ((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) left join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_report_summary_total`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_report_summary_total`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_report_summary_total` AS select count(0) AS `Total_Accounts`,round(sum(`b`.`consumption`),2) AS `Total_Cu_m3`,round(sum(`b`.`free_water`),2) AS `Total_Un_m3`,round(sum(`b`.`water_charge`),2) AS `Total_Water_CHARGE`,round(sum(`b`.`arrears_amount`),2) AS `Total_Arrears`,round(sum(`b`.`tax_amount`),2) AS `Total_Tax`,round(sum(`b`.`discount_amount`),2) AS `Total_Discount`,round(sum(`b`.`total_water_bill`),2) AS `Total_Water_Bill_Final`,round(sum(`b`.`scf_amount`),2) AS `Total_SCF`,round(sum(`b`.`total_amount`),2) AS `Grand_Total` from `billing` `b` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_status`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_status`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_status` AS select date_format(`billing`.`billing_date`,'%Y-%m') AS `billing_month`,count(0) AS `total_bills`,sum((case when (`billing`.`remaining_balance` = 0) then 1 else 0 end)) AS `paid_count`,sum((case when ((`billing`.`remaining_balance` > 0) and (`billing`.`remaining_balance` < `billing`.`total_amount`)) then 1 else 0 end)) AS `partially_paid_count`,sum((case when ((`billing`.`remaining_balance` = `billing`.`total_amount`) and (`billing`.`due_date` < curdate())) then 1 else 0 end)) AS `overdue_count`,sum((case when ((`billing`.`remaining_balance` = `billing`.`total_amount`) and (`billing`.`due_date` >= curdate())) then 1 else 0 end)) AS `unpaid_count`,round(((100 * sum((case when (`billing`.`remaining_balance` = 0) then 1 else 0 end))) / count(0)),2) AS `paid_percent`,round(((100 * sum((case when ((`billing`.`remaining_balance` > 0) and (`billing`.`remaining_balance` < `billing`.`total_amount`)) then 1 else 0 end))) / count(0)),2) AS `partially_paid_percent`,round(((100 * sum((case when ((`billing`.`remaining_balance` = `billing`.`total_amount`) and (`billing`.`due_date` < curdate())) then 1 else 0 end))) / count(0)),2) AS `overdue_percent`,round(((100 * sum((case when ((`billing`.`remaining_balance` = `billing`.`total_amount`) and (`billing`.`due_date` >= curdate())) then 1 else 0 end))) / count(0)),2) AS `unpaid_percent` from `billing` group by date_format(`billing`.`billing_date`,'%Y-%m') order by `billing_month` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_summary_jan_2026`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_summary_jan_2026`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_summary_jan_2026` AS select count(0) AS `Total_Accounts`,round(sum(`b`.`consumption`),2) AS `Total_Cu_m3`,round(sum(`b`.`free_water`),2) AS `Total_Un_m3`,round(sum(`b`.`water_charge`),2) AS `Total_Water_Bill`,round(sum(`b`.`arrears_amount`),2) AS `Total_Arrears`,round(sum(`b`.`tax_amount`),2) AS `Total_Tax`,round(sum(`b`.`discount_amount`),2) AS `Total_Discount`,round(sum(`b`.`total_water_bill`),2) AS `Total_Water_Bill_Final`,round(sum(`b`.`scf_amount`),2) AS `Total_SCF`,round(sum(`b`.`total_amount`),2) AS `Grand_Total` from `billing` `b` where ((`b`.`billing_date` >= '2026-01-01') and (`b`.`billing_date` < '2026-02-01')) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_billing_with_users_full`
--

/*!50001 DROP VIEW IF EXISTS `v_billing_with_users_full`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_billing_with_users_full` AS select `b`.`billing_id` AS `billing_id`,`b`.`bill_number` AS `bill_number`,`b`.`concessionaire_id` AS `concessionaire_id`,`b`.`reading_id` AS `reading_id`,`b`.`billing_date` AS `billing_date`,`b`.`due_date` AS `due_date`,`b`.`consumption` AS `consumption`,`b`.`free_water` AS `free_water`,`b`.`water_charge` AS `water_charge`,`b`.`discount_amount` AS `discount_amount`,`b`.`tax_amount` AS `tax_amount`,`b`.`total_water_bill` AS `total_water_bill`,`b`.`scf_amount` AS `scf_amount`,`b`.`arrears_amount` AS `arrears_amount`,`b`.`penalty_amount` AS `penalty_amount`,`b`.`total_amount` AS `total_amount`,`b`.`remaining_water_charge` AS `remaining_water_charge`,`b`.`remaining_tax_amount` AS `remaining_tax_amount`,`b`.`remaining_penalty_amount` AS `remaining_penalty_amount`,`b`.`remaining_scf_amount` AS `remaining_scf_amount`,`b`.`remaining_balance` AS `remaining_balance`,`b`.`status` AS `status`,`b`.`scf_status` AS `scf_status`,`b`.`updated_at` AS `updated_at`,`b`.`last_payment_date` AS `last_payment_date`,`b`.`is_initial` AS `is_initial`,`b`.`payment_count` AS `payment_count`,`b`.`last_collection_id` AS `last_collection_id`,`b`.`created_by_user_id` AS `created_by_user_id`,`b`.`updated_by_user_id` AS `updated_by_user_id`,`b`.`tax_percent_used` AS `tax_percent_used`,`b`.`discount_percent_used` AS `discount_percent_used`,`b`.`penalty_percent_used` AS `penalty_percent_used`,`b`.`is_penalty_applied` AS `is_penalty_applied`,`b`.`penalty_applied_at` AS `penalty_applied_at`,`b`.`request_id` AS `request_id`,`b`.`created_at` AS `created_at`,`b`.`scf_monthly_used` AS `scf_monthly_used`,`b`.`scf_total_cap_used` AS `scf_total_cap_used`,`b`.`paid_at` AS `paid_at`,`uc`.`username` AS `created_by_username`,`uc`.`full_name` AS `created_by_full_name`,`uc`.`role` AS `created_by_role`,`uu`.`username` AS `updated_by_username`,`uu`.`full_name` AS `updated_by_full_name`,`uu`.`role` AS `updated_by_role` from ((`billing` `b` left join `users` `uc` on((`b`.`created_by_user_id` = `uc`.`user_id`))) left join `users` `uu` on((`b`.`updated_by_user_id` = `uu`.`user_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_collection_report`
--

/*!50001 DROP VIEW IF EXISTS `v_collection_report`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_collection_report` AS select `col`.`collection_id` AS `Collection_ID`,`col`.`or_number` AS `OR_Number`,`col`.`collection_date` AS `Date`,coalesce(`col`.`bill_numbers`,'') AS `Invoice_Number`,coalesce(`col`.`concessionaire_code`,'') AS `Concessionaire_Code`,coalesce(`col`.`concessionaire_name`,'') AS `Concessionaire_Name`,coalesce(`col`.`address`,'') AS `Address`,coalesce(`col`.`total_current_bill`,0.00) AS `Water_Charge`,coalesce(`col`.`total_arrears`,0.00) AS `Arrears`,coalesce(`col`.`total_penalty`,0.00) AS `Penalty`,coalesce(`col`.`total_tax`,0.00) AS `Tax`,coalesce(`col`.`total_scf`,0.00) AS `SCF`,coalesce(`col`.`total_water_bill_paid`,0.00) AS `Total_Water_Bill_Paid`,coalesce(`col`.`scf_paid`,0.00) AS `SCF_Paid`,coalesce(`col`.`total_others`,0.00) AS `Others`,coalesce(`col`.`grand_total`,0.00) AS `Grand_Total`,coalesce(`col`.`amount_received`,0.00) AS `Amount_Received`,coalesce(`col`.`change_amount`,0.00) AS `Change_Amount`,coalesce(`col`.`total_paid_amount`,0.00) AS `Collected`,coalesce(`col`.`total_discount`,0.00) AS `Total_Discount`,coalesce(`col`.`payment_type`,'') AS `Payment_Type`,coalesce(`col`.`payment_reference`,'') AS `Payment_Reference`,coalesce(`col`.`remarks`,'') AS `Remarks`,coalesce(`col`.`payment_ids`,'') AS `Payment_IDs`,`col`.`created_by` AS `Created_By`,coalesce(`col`.`created_by_name`,'') AS `Created_By_Name`,`col`.`created_at` AS `Created_At`,`col`.`updated_at` AS `Updated_At`,coalesce(`col`.`uncollected`,0.00) AS `Uncollected` from `collection` `col` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_collection_report_v47`
--

/*!50001 DROP VIEW IF EXISTS `v_collection_report_v47`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_collection_report_v47` AS select `c`.`collection_id` AS `collection_id`,`c`.`or_number` AS `OR_Number`,cast(`c`.`collection_date` as date) AS `Date`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`c`.`payor_name` AS `Payor_Name`,round((coalesce(`c`.`total_current_bill`,0) + coalesce(`c`.`total_arrears`,0)),2) AS `Water_Charge`,round(coalesce(`c`.`total_arrears`,0),2) AS `Arrears`,round(coalesce(`c`.`total_penalty`,0),2) AS `Penalty`,round(coalesce(`c`.`total_tax`,0),2) AS `Tax`,round(coalesce(`c`.`total_scf`,0),2) AS `SCF`,round(coalesce(`c`.`total_others`,0),2) AS `Others`,round(coalesce(`c`.`total_paid_amount`,0),2) AS `Amount_Collected`,round(coalesce(`c`.`amount_received`,0),2) AS `Amount_Received`,round(coalesce(`c`.`change_amount`,0),2) AS `Change_Amount`,`c`.`payment_type` AS `payment_type`,`c`.`payment_reference` AS `payment_reference`,`c`.`remarks` AS `remarks` from `collection` `c` where (`c`.`status` = 'POSTED') order by `c`.`collection_date` desc,`c`.`collection_id` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_collection_with_users`
--

/*!50001 DROP VIEW IF EXISTS `v_collection_with_users`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_collection_with_users` AS select `c`.`collection_id` AS `collection_id`,`c`.`or_number` AS `or_number`,`c`.`collection_date` AS `collection_date`,`c`.`bill_numbers` AS `bill_numbers`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`total_current_bill` AS `total_current_bill`,`c`.`total_arrears` AS `total_arrears`,`c`.`total_penalty` AS `total_penalty`,`c`.`total_tax` AS `total_tax`,`c`.`total_scf` AS `total_scf`,`c`.`total_water_bill_paid` AS `total_water_bill_paid`,`c`.`scf_paid` AS `scf_paid`,`c`.`total_others` AS `total_others`,`c`.`grand_total` AS `grand_total`,`c`.`amount_received` AS `amount_received`,`c`.`change_amount` AS `change_amount`,`c`.`total_paid_amount` AS `total_paid_amount`,`c`.`uncollected` AS `uncollected`,`c`.`total_discount` AS `total_discount`,`c`.`payment_type` AS `payment_type`,`c`.`payment_reference` AS `payment_reference`,`c`.`status` AS `status`,`c`.`remarks` AS `remarks`,`c`.`payor_name` AS `payor_name`,`c`.`created_at` AS `created_at`,`c`.`updated_at` AS `updated_at`,`c`.`voided_at` AS `voided_at`,`c`.`billing_count` AS `billing_count`,`c`.`payment_count` AS `payment_count`,`c`.`created_by` AS `created_by_user_id`,`u`.`username` AS `created_by_username`,`u`.`full_name` AS `created_by_full_name`,`u`.`role` AS `created_by_role`,`u`.`is_active` AS `created_by_is_active`,`v`.`user_id` AS `voided_by_user_id`,`v`.`username` AS `voided_by_username`,`v`.`full_name` AS `voided_by_full_name` from ((`collection` `c` left join `users` `u` on((`c`.`created_by` = `u`.`user_id`))) left join `users` `v` on((`c`.`voided_by_user_id` = `v`.`user_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire` AS select `c`.`concessionaire_id` AS `Concessionaire_ID`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`c`.`zone_id` AS `Zone`,`c`.`service_id` AS `Service`,`c`.`meter_no` AS `Meter_Number`,`c`.`first_reading_date` AS `First_Reading_Date`,`c`.`is_tax_exempt` AS `Tax_Exempt`,`c`.`is_due_exempt` AS `Due_Exempt`,`c`.`is_discounted` AS `Discounted`,`c`.`status` AS `Status` from `concessionaire` `c` order by `c`.`concessionaire_code` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire_collection_lookup`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire_collection_lookup`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire_collection_lookup` AS select `c`.`concessionaire_id` AS `concessionaire_id`,coalesce(`c`.`concessionaire_code`,'') AS `concessionaire_code`,coalesce(`c`.`concessionaire_name`,'') AS `concessionaire_name`,coalesce(`c`.`address`,'') AS `address`,coalesce(`c`.`zone_id`,'') AS `zone_id`,coalesce(`c`.`meter_no`,'') AS `meter_no`,coalesce(`c`.`is_discounted`,0) AS `is_discounted`,coalesce(`c`.`is_tax_exempt`,0) AS `is_tax_exempt`,coalesce(`c`.`is_due_exempt`,0) AS `is_due_exempt`,coalesce(`s`.`service_type`,'') AS `service_type`,coalesce(round(sum((case when (coalesce(`b`.`remaining_balance`,0) > 0) then `b`.`remaining_balance` else 0 end)),2),0.00) AS `outstanding_balance`,coalesce(`scf`.`balance`,0.00) AS `remaining_scf`,coalesce(sum((case when (coalesce(`b`.`remaining_balance`,0) > 0) then 1 else 0 end)),0) AS `unpaid_bill_count`,max(`b`.`billing_date`) AS `last_billing_date` from (((`concessionaire` `c` left join `services` `s` on((`s`.`service_id` = `c`.`service_id`))) left join `billing` `b` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) left join (select `sb`.`concessionaire_id` AS `concessionaire_id`,`sb`.`balance` AS `balance` from (`scf_balance` `sb` join (select `scf_balance`.`concessionaire_id` AS `concessionaire_id`,max(`scf_balance`.`scf_id`) AS `max_id` from `scf_balance` group by `scf_balance`.`concessionaire_id`) `latest` on(((`sb`.`concessionaire_id` = `latest`.`concessionaire_id`) and (`sb`.`scf_id` = `latest`.`max_id`))))) `scf` on((`scf`.`concessionaire_id` = `c`.`concessionaire_id`))) group by `c`.`concessionaire_id`,`c`.`concessionaire_code`,`c`.`concessionaire_name`,`c`.`zone_id`,`c`.`meter_no`,`c`.`is_discounted`,`c`.`is_tax_exempt`,`c`.`is_due_exempt`,`s`.`service_type`,`scf`.`balance` order by `c`.`concessionaire_id` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire_details`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire_details`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire_details` AS select `c`.`concessionaire_id` AS `Concessionaire_ID`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`c`.`tin_number` AS `Tin`,`c`.`status` AS `Status`,`c`.`meter_no` AS `Meter_Number`,`c`.`first_reading_date` AS `FRD`,`c`.`is_tax_exempt` AS `Tax_Exempted`,`c`.`is_due_exempt` AS `Due_Exempted`,`c`.`is_discounted` AS `Discounted`,`c`.`is_not_billable` AS `Not_Billable`,`z`.`zone_id` AS `Zone`,`s`.`service_type` AS `Service_Type`,`s`.`pipe_size` AS `Pipe_Size`,`scf`.`total_amount` AS `SCF_Total_Amount`,`scf`.`balance` AS `SCF_Balance`,`scf`.`monthly` AS `SCF_Monthly` from (((`concessionaire` `c` left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) left join `scf_balance` `scf` on((`c`.`concessionaire_id` = `scf`.`concessionaire_id`))) order by `c`.`concessionaire_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire_latest_reading`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire_latest_reading`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire_latest_reading` AS select `c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`c`.`service_id` AS `service_id`,`c`.`meter_no` AS `meter_no`,`c`.`first_reading_date` AS `first_reading_date`,`c`.`status` AS `status`,`r`.`reading_id` AS `reading_id`,`r`.`previous_reading` AS `previous_reading`,`r`.`present_reading` AS `present_reading`,`r`.`reading_date` AS `reading_date` from (`concessionaire` `c` left join `reading` `r` on(((`r`.`concessionaire_id` = `c`.`concessionaire_id`) and (`r`.`reading_date` = (select max(`r2`.`reading_date`) from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`))) and (`r`.`reading_id` = (select max(`r3`.`reading_id`) from `reading` `r3` where ((`r3`.`concessionaire_id` = `c`.`concessionaire_id`) and (`r3`.`reading_date` = (select max(`r4`.`reading_date`) from `reading` `r4` where (`r4`.`concessionaire_id` = `c`.`concessionaire_id`))))))))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire_scf`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire_scf`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire_scf` AS select `s`.`scf_id` AS `scf_id`,`s`.`concessionaire_id` AS `concessionaire_id`,`s`.`total_amount` AS `total_amount`,`s`.`balance` AS `balance`,`s`.`monthly` AS `monthly`,`s`.`updated_at` AS `updated_at`,`s`.`last_payment_date` AS `last_payment_date`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name` from (`scf_balance` `s` join `concessionaire` `c` on((`s`.`concessionaire_id` = `c`.`concessionaire_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_concessionaire_statistics_v3`
--

/*!50001 DROP VIEW IF EXISTS `v_concessionaire_statistics_v3`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_concessionaire_statistics_v3` AS select `z`.`zone_id` AS `Zone_ID`,`z`.`zone_name` AS `Zone_Name`,`s`.`service_id` AS `Service_ID`,`s`.`service_type` AS `Service_Type`,`s`.`pipe_size` AS `Pipe_Size`,count(`c`.`concessionaire_id`) AS `Concessionaires`,sum(`c`.`is_tax_exempt`) AS `Tax_Exempted`,sum(`c`.`is_due_exempt`) AS `Due_Exempted`,sum(`c`.`is_discounted`) AS `Discounted`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `Active`,sum((case when (`c`.`status` = 'PENDING') then 1 else 0 end)) AS `Pending`,sum((case when (`c`.`status` = 'DISCONNECTED') then 1 else 0 end)) AS `Disconnected`,sum((case when ((`c`.`meter_no` is not null) and (`c`.`meter_no` <> '')) then 1 else 0 end)) AS `Meters_Assigned`,sum((case when ((`c`.`meter_no` is null) or (`c`.`meter_no` = '')) then 1 else 0 end)) AS `Meters_Unassigned` from ((`concessionaire` `c` left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) group by `z`.`zone_id`,`z`.`zone_name`,`s`.`service_id`,`s`.`service_type`,`s`.`pipe_size` order by `z`.`zone_name`,`s`.`service_type` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_customer_ledger`
--

/*!50001 DROP VIEW IF EXISTS `v_customer_ledger`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_customer_ledger` AS select `c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`b`.`billing_date` AS `transaction_date`,concat('BILL-',`b`.`bill_number`) AS `reference_no`,'' AS `remarks`,round(coalesce(`b`.`total_amount`,0.00),2) AS `debit`,0.00 AS `credit`,round(coalesce(`b`.`water_charge`,0.00),2) AS `water_charge`,round(coalesce(`b`.`tax_amount`,0.00),2) AS `tax_amount`,round(coalesce(`b`.`penalty_amount`,0.00),2) AS `penalty_amount`,round(coalesce(`b`.`scf_amount`,0.00),2) AS `scf_amount`,round(coalesce(`b`.`arrears_amount`,0.00),2) AS `arrears_amount`,`b`.`status` AS `billing_status` from (`billing` `b` join `concessionaire` `c` on((`c`.`concessionaire_id` = `b`.`concessionaire_id`))) union all select `c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`col`.`collection_date` AS `transaction_date`,concat('OR-',`col`.`or_number`) AS `reference_no`,coalesce(`col`.`remarks`,'') AS `remarks`,0.00 AS `debit`,round(coalesce(`col`.`grand_total`,0.00),2) AS `credit`,round(coalesce(`col`.`total_current_bill`,0.00),2) AS `water_charge`,round(coalesce(`col`.`total_tax`,0.00),2) AS `tax_amount`,round(coalesce(`col`.`total_penalty`,0.00),2) AS `penalty_amount`,round(coalesce(`col`.`total_scf`,0.00),2) AS `scf_amount`,round(coalesce(`col`.`total_arrears`,0.00),2) AS `arrears_amount`,NULL AS `billing_status` from (`collection` `col` join `concessionaire` `c` on(((`c`.`concessionaire_code` = `col`.`concessionaire_code`) or (find_in_set(`c`.`concessionaire_code`,replace(coalesce(`col`.`concessionaire_code`,''),' ','')) > 0)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_daily_billing`
--

/*!50001 DROP VIEW IF EXISTS `v_daily_billing`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_daily_billing` AS select `c`.`zone_id` AS `Zone`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`b`.`bill_number` AS `Invoice_Number`,round((`r`.`present_reading` - `r`.`previous_reading`),2) AS `Cu_m³`,round(`b`.`free_water`,2) AS `Un_m³`,round((case when ((`c`.`is_discounted` = 1) and (((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) < (select cast(`system_settings`.`settings_value` as signed) from `system_settings` where (`system_settings`.`settings_key` = 'discount_thresh_hold') limit 1))) then (`b`.`water_charge` - ((`b`.`water_charge` * (select cast(`system_settings`.`settings_value` as decimal(7,4)) from `system_settings` where (`system_settings`.`settings_key` = 'discount_percent') limit 1)) / 100)) else `b`.`water_charge` end),2) AS `Water_Bill`,round(`b`.`penalty_amount`,2) AS `Penalty`,round((case when ((`c`.`is_discounted` = 1) and (((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) < (select cast(`system_settings`.`settings_value` as signed) from `system_settings` where (`system_settings`.`settings_key` = 'discount_thresh_hold') limit 1))) then (`b`.`tax_amount` * (1 - ((select cast(`system_settings`.`settings_value` as decimal(7,2)) from `system_settings` where (`system_settings`.`settings_key` = 'discount_percent') limit 1) / 100))) else `b`.`tax_amount` end),2) AS `Tax`,round(`b`.`discount_amount`,2) AS `Discount`,round(`b`.`total_water_bill`,2) AS `Total_Water_Bill`,round(`b`.`scf_amount`,2) AS `SCF`,round(`b`.`total_amount`,2) AS `Total_Amount_Billed`,`b`.`is_initial` AS `Initial`,cast(`b`.`billing_date` as date) AS `Date` from ((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) left join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) where (`b`.`status` <> 'CANCELLED') order by `b`.`bill_number` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_daily_collection`
--

/*!50001 DROP VIEW IF EXISTS `v_daily_collection`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_daily_collection` AS select `c`.`collection_id` AS `Collection_ID`,`c`.`or_number` AS `OR_Number`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`c`.`remarks` AS `Remarks`,`c`.`bill_numbers` AS `Invoice_Number`,`c`.`total_current_bill` AS `Water_Charge`,`c`.`total_discount` AS `Discount`,`c`.`total_arrears` AS `Arrears`,`c`.`total_penalty` AS `Penalty`,`c`.`total_tax` AS `Tax`,`c`.`total_scf` AS `SCF`,`c`.`scf_paid` AS `SCF_Paid`,`c`.`total_others` AS `Others`,`c`.`grand_total` AS `Total`,`c`.`total_paid_amount` AS `Collected`,`c`.`amount_received` AS `Amount_Received`,`c`.`change_amount` AS `Change`,`c`.`payment_type` AS `Payment_Type`,`c`.`payment_reference` AS `Reference_No`,`c`.`payment_ids` AS `Payment_IDs`,`c`.`collection_date` AS `Date`,`c`.`created_by` AS `Collected_By_ID`,`c`.`created_by_name` AS `Collected_By`,`c`.`created_at` AS `Created_At`,`c`.`updated_at` AS `Updated_At` from `collection` `c` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_input_meter_reading`
--

/*!50001 DROP VIEW IF EXISTS `v_input_meter_reading`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_input_meter_reading` AS select (case when exists(select 1 from `billing` `b` where ((`b`.`concessionaire_id` = `c`.`concessionaire_id`) and (month(`b`.`billing_date`) = month(curdate())) and (year(`b`.`billing_date`) = year(curdate())))) then true else false end) AS `Is_Billed_Today`,`c`.`zone_id` AS `Zone`,`c`.`concessionaire_id` AS `Concessionaire_Id`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`meter_no` AS `Meter_Number`,coalesce((select date_format(`r2`.`reading_date`,'%Y-%m-%d') from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),(case when ((month(`c`.`first_reading_date`) in (month(curdate()),month((curdate() - interval 1 month)))) and (year(`c`.`first_reading_date`) in (year(curdate()),year((curdate() - interval 1 month))))) then date_format(`c`.`first_reading_date`,'%Y-%m-%d') else NULL end)) AS `Previous_Reading_Date`,coalesce((select `r2`.`present_reading` from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),0) AS `Previous_Reading` from `concessionaire` `c` where (upper(trim(coalesce(`c`.`status`,''))) = 'ACTIVE') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_map_billing_print_v2`
--

/*!50001 DROP VIEW IF EXISTS `v_map_billing_print_v2`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_map_billing_print_v2` AS select `b`.`billing_id` AS `billing_id`,`c`.`zone_id` AS `zone_id`,`b`.`bill_number` AS `bill_number`,`b`.`billing_date` AS `billing_date`,`b`.`due_date` AS `due_date`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,coalesce((select `r2`.`reading_date` from `reading` `r2` where ((`r2`.`concessionaire_id` = `r`.`concessionaire_id`) and (`r2`.`reading_id` < `r`.`reading_id`)) order by `r2`.`reading_id` desc limit 1),`c`.`first_reading_date`) AS `from_reading_date`,`r`.`reading_date` AS `to_reading_date`,`r`.`previous_reading` AS `previous_reading`,`r`.`present_reading` AS `present_reading`,(`r`.`present_reading` - `r`.`previous_reading`) AS `total_meter_consumed`,`b`.`free_water` AS `meter_unbilled`,`b`.`consumption` AS `billed_meter`,`s`.`min_rate` AS `minimum_charge`,`b`.`water_charge` AS `total_water_consumption_amount`,least(greatest(((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`),0),10) AS `q_1_10`,`s`.`min_rate` AS `rate_1_10`,round((least(greatest(((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`),0),10) * `s`.`min_rate`),2) AS `amount_1_10`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 10),0),10) AS `q_11_20`,`s`.`rate_11_20` AS `rate_11_20`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 10),0),10) * `s`.`rate_11_20`),2) AS `amount_11_20`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 20),0),10) AS `q_21_30`,`s`.`rate_21_30` AS `rate_21_30`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 20),0),10) * `s`.`rate_21_30`),2) AS `amount_21_30`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 30),0),10) AS `q_31_40`,`s`.`rate_31_40` AS `rate_31_40`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 30),0),10) * `s`.`rate_31_40`),2) AS `amount_31_40`,greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 40),0) AS `q_41_up`,`s`.`rate_41_above` AS `rate_41_up`,round((greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 40),0) * `s`.`rate_41_above`),2) AS `amount_41_up`,`b`.`discount_amount` AS `discount_amount`,`b`.`tax_amount` AS `tax_amount`,`b`.`arrears_amount` AS `arrears_amount`,`b`.`scf_amount` AS `scf_amount`,`b`.`total_water_bill` AS `subtotal_amount_due`,`b`.`penalty_amount` AS `penalty_amount`,`b`.`total_amount` AS `total_amount_due` from ((((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) left join (select `p1`.`payment_id` AS `payment_id`,`p1`.`billing_id` AS `billing_id`,`p1`.`amount_paid` AS `amount_paid`,`p1`.`balance` AS `balance`,`p1`.`scf_paid` AS `scf_paid`,`p1`.`scf_balance` AS `scf_balance`,`p1`.`payment_date` AS `payment_date` from `payment` `p1` where (`p1`.`payment_id` = (select max(`p2`.`payment_id`) from `payment` `p2` where (`p2`.`billing_id` = `p1`.`billing_id`)))) `p` on((`p`.`billing_id` = `b`.`billing_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_map_billing_print_v3`
--

/*!50001 DROP VIEW IF EXISTS `v_map_billing_print_v3`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_map_billing_print_v3` AS select `b`.`billing_id` AS `billing_id`,`b`.`bill_number` AS `bill_number`,`b`.`billing_date` AS `billing_date`,`b`.`due_date` AS `due_date`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,coalesce((select `r2`.`reading_date` from `reading` `r2` where ((`r2`.`concessionaire_id` = `r`.`concessionaire_id`) and (`r2`.`reading_id` < `r`.`reading_id`)) order by `r2`.`reading_id` desc limit 1),`c`.`first_reading_date`) AS `from_reading_date`,`r`.`reading_date` AS `to_reading_date`,`r`.`previous_reading` AS `previous_reading`,`r`.`present_reading` AS `present_reading`,(`r`.`present_reading` - `r`.`previous_reading`) AS `total_meter_consumed`,`b`.`free_water` AS `meter_unbilled`,greatest(((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`),0) AS `billed_meter`,`s`.`min_rate` AS `minimum_charge`,`b`.`water_charge` AS `total_water_consumption_amount`,least(greatest(((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`),0),10) AS `q_1_10`,`s`.`min_rate` AS `rate_1_10`,round((least(greatest(((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`),0),10) * `s`.`min_rate`),2) AS `amount_1_10`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 10),0),10) AS `q_11_20`,`s`.`rate_11_20` AS `rate_11_20`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 10),0),10) * `s`.`rate_11_20`),2) AS `amount_11_20`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 20),0),10) AS `q_21_30`,`s`.`rate_21_30` AS `rate_21_30`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 20),0),10) * `s`.`rate_21_30`),2) AS `amount_21_30`,least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 30),0),10) AS `q_31_40`,`s`.`rate_31_40` AS `rate_31_40`,round((least(greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 30),0),10) * `s`.`rate_31_40`),2) AS `amount_31_40`,greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 40),0) AS `q_41_up`,`s`.`rate_41_above` AS `rate_41_up`,round((greatest((((`r`.`present_reading` - `r`.`previous_reading`) - `b`.`free_water`) - 40),0) * `s`.`rate_41_above`),2) AS `amount_41_up`,`b`.`discount_amount` AS `discount_amount`,`b`.`tax_amount` AS `tax_amount`,`b`.`arrears_amount` AS `arrears_amount`,`b`.`scf_amount` AS `scf_amount`,`b`.`total_water_bill` AS `subtotal_amount_due`,`b`.`penalty_amount` AS `penalty_amount`,`b`.`total_amount` AS `total_amount_due` from ((((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) join `reading` `r` on((`b`.`reading_id` = `r`.`reading_id`))) join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) left join (select `p1`.`payment_id` AS `payment_id`,`p1`.`billing_id` AS `billing_id`,`p1`.`amount_paid` AS `amount_paid`,`p1`.`balance` AS `balance`,`p1`.`scf_paid` AS `scf_paid`,`p1`.`scf_balance` AS `scf_balance`,`p1`.`payment_date` AS `payment_date` from `payment` `p1` where (`p1`.`payment_id` = (select max(`p2`.`payment_id`) from `payment` `p2` where (`p2`.`billing_id` = `p1`.`billing_id`)))) `p` on((`p`.`billing_id` = `b`.`billing_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_meter_reading_sheet`
--

/*!50001 DROP VIEW IF EXISTS `v_meter_reading_sheet`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_meter_reading_sheet` AS select `c`.`zone_id` AS `Zone`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`meter_no` AS `Meter_Number`,coalesce((select date_format(`r2`.`reading_date`,'%Y-%m-%d') from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),(case when ((month(`c`.`first_reading_date`) in (month(curdate()),month((curdate() - interval 1 month)))) and (year(`c`.`first_reading_date`) in (year(curdate()),year((curdate() - interval 1 month))))) then date_format(`c`.`first_reading_date`,'%Y-%m-%d') else NULL end)) AS `Previous_Reading_Date`,coalesce((select `r2`.`present_reading` from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),0) AS `Previous_Reading`,NULL AS `Present_Reading` from `concessionaire` `c` where (`c`.`status` <> 'DISCONNECTED') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_meter_reading_sheet_backup`
--

/*!50001 DROP VIEW IF EXISTS `v_meter_reading_sheet_backup`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_meter_reading_sheet_backup` AS select `c`.`zone_id` AS `Zone`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`meter_no` AS `Meter_Number`,coalesce((select `r2`.`reading_date` from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),(case when ((month(`c`.`first_reading_date`) in (month(curdate()),month((curdate() - interval 1 month)))) and (year(`c`.`first_reading_date`) in (year(curdate()),year((curdate() - interval 1 month))))) then `c`.`first_reading_date` else NULL end)) AS `Previous_Reading_Date`,coalesce((select `r2`.`present_reading` from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),0) AS `Previous_Reading`,NULL AS `Present_Reading` from `concessionaire` `c` where (`c`.`status` <> 'DISCONNECTED') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_monthly_billing`
--

/*!50001 DROP VIEW IF EXISTS `v_monthly_billing`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_monthly_billing` AS select `b`.`bill_number` AS `Bill_Number`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`b`.`billing_date` AS `Billing_Date`,`b`.`due_date` AS `Due_Date`,`b`.`consumption` AS `Consumption`,`b`.`water_charge` AS `Water_Charge`,`b`.`discount_amount` AS `Discount`,`b`.`tax_amount` AS `Tax`,`b`.`scf_amount` AS `SCF`,`b`.`penalty_amount` AS `Penalty`,`b`.`total_amount` AS `Total_Amount`,`b`.`status` AS `Status` from (`billing` `b` join `concessionaire` `c` on((`c`.`concessionaire_id` = `b`.`concessionaire_id`))) order by `b`.`billing_date` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_monthly_collection`
--

/*!50001 DROP VIEW IF EXISTS `v_monthly_collection`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_monthly_collection` AS select year(`c`.`collection_date`) AS `Year`,monthname(`c`.`collection_date`) AS `Month_Name`,date_format(`c`.`collection_date`,'%Y-%m') AS `Month_Period`,sum(`c`.`total_current_bill`) AS `Total_Current_Bill`,sum(`c`.`total_penalty`) AS `Total_Penalty`,sum(`c`.`total_tax`) AS `Total_Tax`,sum(`c`.`total_scf`) AS `Total_SCF`,sum(`c`.`total_others`) AS `Total_Others`,sum(`c`.`total_arrears`) AS `Total_Arrears`,sum(`c`.`grand_total`) AS `Grand_Total`,sum(`c`.`amount_received`) AS `Amount_Received` from `collection` `c` group by year(`c`.`collection_date`),month(`c`.`collection_date`) order by year(`c`.`collection_date`) desc,month(`c`.`collection_date`) desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_service_billing_monthly`
--

/*!50001 DROP VIEW IF EXISTS `v_service_billing_monthly`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_service_billing_monthly` AS select date_format(`b`.`billing_date`,'%Y-%m') AS `billing_month`,`s`.`service_id` AS `service_id`,`s`.`service_type` AS `service_type`,`s`.`pipe_size` AS `pipe_size`,count(`b`.`billing_id`) AS `total_bills`,sum(`b`.`consumption`) AS `total_consumption`,sum(`b`.`water_charge`) AS `total_water_charge`,sum(`b`.`tax_amount`) AS `total_tax_amount`,sum(`b`.`penalty_amount`) AS `total_penalty_amount`,sum(`b`.`total_amount`) AS `total_billed_amount`,sum(`b`.`remaining_balance`) AS `total_remaining_balance`,round(((100 * sum(`b`.`total_amount`)) / nullif(sum(sum(`b`.`total_amount`)) OVER (PARTITION BY date_format(`b`.`billing_date`,'%Y-%m') ) ,0)),2) AS `billing_share_percent` from ((`billing` `b` join `concessionaire` `c` on((`b`.`concessionaire_id` = `c`.`concessionaire_id`))) join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) group by date_format(`b`.`billing_date`,'%Y-%m'),`s`.`service_id`,`s`.`service_type`,`s`.`pipe_size` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_service_summary`
--

/*!50001 DROP VIEW IF EXISTS `v_service_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_service_summary` AS select `s`.`service_id` AS `ServiceID`,`s`.`service_type` AS `ServiceType`,`s`.`pipe_size` AS `PipeSize`,count(`c`.`concessionaire_id`) AS `TotalConcessionaires`,sum(`c`.`is_tax_exempt`) AS `TaxExemptCount`,sum(`c`.`is_due_exempt`) AS `DueExemptCount`,sum(`c`.`is_discounted`) AS `DiscountedCount`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `ActiveCount`,sum((case when (`c`.`status` <> 'ACTIVE') then 1 else 0 end)) AS `InactiveCount`,sum((case when ((`c`.`meter_no` is not null) and (`c`.`meter_no` <> '')) then 1 else 0 end)) AS `MetersAssigned`,sum((case when ((`c`.`meter_no` is null) or (`c`.`meter_no` = '')) then 1 else 0 end)) AS `MetersUnassigned` from (`concessionaire` `c` left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) group by `s`.`service_id`,`s`.`service_type`,`s`.`pipe_size` order by `s`.`service_id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_service_summary_v2`
--

/*!50001 DROP VIEW IF EXISTS `v_service_summary_v2`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_service_summary_v2` AS select `s`.`service_id` AS `Service_ID`,`s`.`service_type` AS `Service_Type`,`s`.`pipe_size` AS `Pipe_Size`,count(`c`.`concessionaire_id`) AS `Concessionaires`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `Active`,sum((case when (`c`.`status` = 'PENDING') then 1 else 0 end)) AS `Pending`,sum((case when (`c`.`status` = 'DISCONNECTED') then 1 else 0 end)) AS `Disconnected`,sum(`c`.`is_tax_exempt`) AS `Tax_Exempted`,sum(`c`.`is_due_exempt`) AS `Due_Exempted`,sum(`c`.`is_discounted`) AS `Discounted`,sum((case when ((`c`.`meter_no` is not null) and (`c`.`meter_no` <> '')) then 1 else 0 end)) AS `Meters_Assigned`,sum((case when ((`c`.`meter_no` is null) or (`c`.`meter_no` = '')) then 1 else 0 end)) AS `Meters_Unassigned` from (`concessionaire` `c` left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) group by `s`.`service_id`,`s`.`service_type`,`s`.`pipe_size` order by `s`.`service_type` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_top_zones`
--

/*!50001 DROP VIEW IF EXISTS `v_top_zones`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_top_zones` AS select `z`.`zone_id` AS `Zone_ID`,`z`.`zone_name` AS `Zone_Name`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `Active`,sum((case when (`c`.`status` = 'PENDING') then 1 else 0 end)) AS `Pending`,sum((case when (`c`.`status` = 'DISCONNECTED') then 1 else 0 end)) AS `Disconnected` from (`concessionaire` `c` left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) group by `z`.`zone_id`,`z`.`zone_name` order by `Active` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_user_logs`
--

/*!50001 DROP VIEW IF EXISTS `v_user_logs`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_user_logs` AS select `ul`.`log_id` AS `log_id`,`ul`.`user_id` AS `user_id`,`u`.`username` AS `username`,`u`.`full_name` AS `full_name`,`u`.`role` AS `role`,`ul`.`action_type` AS `action_type`,`ul`.`module` AS `module`,`ul`.`entity_name` AS `entity_name`,`ul`.`entity_id` AS `entity_id`,`ul`.`description` AS `description`,`ul`.`created_at` AS `created_at`,date_format(`ul`.`created_at`,'%Y-%m-%d %H:%i:%s') AS `formatted_date`,(case when (`ul`.`action_type` in ('DELETE','VOID','REVERSE')) then 'CRITICAL' when (`ul`.`action_type` = 'UPDATE') then 'WARNING' else 'INFO' end) AS `severity`,concat('[',upper(`ul`.`module`),'] ',`ul`.`action_type`,' on ',`ul`.`entity_name`,coalesce(concat(' #',`ul`.`entity_id`),''),' by ',coalesce(`u`.`full_name`,`u`.`username`),' (',coalesce(`u`.`role`,'UNKNOWN'),')',' at ',convert(date_format(`ul`.`created_at`,'%Y-%m-%d %H:%i') using utf8mb4)) AS `activity_summary` from (`user_logs` `ul` left join `users` `u` on((`u`.`user_id` = `ul`.`user_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `v_zone_summary`
--

/*!50001 DROP VIEW IF EXISTS `v_zone_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `v_zone_summary` AS select `z`.`zone_id` AS `Zone_ID`,`z`.`zone_name` AS `Zone_Name`,count(`c`.`concessionaire_id`) AS `Concessionaires`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `Active`,sum((case when (`c`.`status` = 'PENDING') then 1 else 0 end)) AS `Pending`,sum((case when (`c`.`status` = 'DISCONNECTED') then 1 else 0 end)) AS `Disconnected`,sum(`c`.`is_tax_exempt`) AS `Tax_Exempted`,sum(`c`.`is_due_exempt`) AS `Due_Exempted`,sum(`c`.`is_discounted`) AS `Discounted`,sum((case when ((`c`.`meter_no` is not null) and (`c`.`meter_no` <> '')) then 1 else 0 end)) AS `Meters_Assigned`,sum((case when ((`c`.`meter_no` is null) or (`c`.`meter_no` = '')) then 1 else 0 end)) AS `Meters_Unassigned` from (`concessionaire` `c` left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) group by `z`.`zone_id`,`z`.`zone_name` order by `z`.`zone_name` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-03 23:32:47
