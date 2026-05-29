CREATE DATABASE  IF NOT EXISTS `water_district_billing_system_db_tubungan` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `water_district_billing_system_db_tubungan`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: water_district_billing_system_db_tubungan
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
  `due_date` date DEFAULT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billing`
--

LOCK TABLES `billing` WRITE;
/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
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
INSERT INTO `billing_invoice_print_settings` VALUES (1,'billing_date',300,103,'2025-10-15','Billing date of the invoice'),(2,'due_date',665,35,'2025-10-30','Due date for payment'),(3,'concessionaire_name',190,150,'DELA CRUZ, JUAN','Name of the concessionaire'),(4,'address',190,183,'BRGY. BANILE IGBARAS ILOILO','Address of the concessionaire'),(5,'concessionaire_code',190,200,'00-0-00-000','Concessionaire code'),(6,'from_reading_date',288,213,'2025-10-01','Start date of meter reading period'),(7,'to_reading_date',368,213,'2025-10-15','End date of meter reading period'),(8,'previous_reading',210,265,'100','Previous meter reading'),(9,'present_reading',290,265,'55','Present meter reading'),(10,'total_meter_consumed',370,265,'55','Total meter consumption'),(11,'billed_meter',600,90,'45','Billed meter'),(12,'minimum_charge',643,105,'352.00','Minimum charge'),(13,'total_water_consumption_amount',695,90,'2281.00','Total water consumption amount'),(14,'q_1_10',600,125,'10','Quantity for 1-10 cubic meters'),(15,'rate_1_10',643,125,'37.15','Rate for 1-10 cubic meters'),(16,'amount_1_10',695,125,'371.50','Amount for 1-10 cubic meters'),(17,'q_11_20',600,140,'10','Quantity for 11-20 cubic meters'),(18,'rate_11_20',643,140,'39.15','Rate for 11-20 cubic meters'),(19,'amount_11_20',695,140,'391.50','Amount for 11-20 cubic meters'),(20,'q_21_30',600,155,'10','-----'),(21,'rate_21_30',643,155,'41.15','-----'),(22,'amount_21_30',695,155,'411.50','-----'),(23,'q_31_40',600,170,'10','-----'),(24,'rate_31_40',643,170,'43.15','-----'),(25,'amount_31_40',695,170,'431.50','-----'),(26,'q_41_up',600,185,'15','-----'),(27,'rate_41_up',643,185,'45.00','-----'),(28,'amount_41_up',695,185,'675.00','-----'),(29,'discount_amount',695,203,'159.67','Discount amount'),(30,'tax_amount',695,218,'42.43','Tax amount'),(31,'arrears_amount',695,248,'212.13','Arrears amount'),(32,'scf_amount',695,263,'500.00','SCF amount'),(33,'subtotal_amount_due',695,283,'2121.33','Subtotal amount due'),(34,'penalty_amount',695,298,'212.13','Penalty amount'),(35,'total_amount_due',695,313,'2375.89','Total amount due'),(36,'billing_date_2',300,464,'2025-10-15',NULL),(37,'due_date_2',665,394,'2025-10-30',NULL),(38,'concessionaire_name_2',190,512,'ESCABA, VINCENT',NULL),(39,'address_2',190,542,'BRGY. BANILE IGBARAS ILOILO',NULL),(40,'concessionaire_code_2',190,559,'01-1-12-013J',NULL),(41,'from_reading_date_2',288,572,'2025-10-01',NULL),(42,'to_reading_date_2',368,572,'2025-10-15',NULL),(43,'previous_reading_2',210,624,'0',NULL),(44,'present_reading_2',290,624,'55',NULL),(45,'total_meter_consumed_2',370,624,'55',NULL),(46,'billed_meter_2',600,449,'55',NULL),(47,'minimum_charge_2',643,464,'352.00',NULL),(48,'total_water_consumption_amount_2',695,449,'2281.00',NULL),(49,'q_1_10_2',600,484,'10',NULL),(50,'rate_1_10_2',643,484,'37.15',NULL),(51,'amount_1_10_2',695,484,'371.50',NULL),(52,'q_11_20_2',600,499,'10',NULL),(53,'rate_11_20_2',643,499,'39.15',NULL),(54,'amount_11_20_2',695,499,'391.50',NULL),(55,'q_21_30_2',600,514,'10',NULL),(56,'rate_21_30_2',643,514,'41.15',NULL),(57,'amount_21_30_2',695,514,'411.50',NULL),(58,'q_31_40_2',600,529,'10',NULL),(59,'rate_31_40_2',643,529,'43.15',NULL),(60,'amount_31_40_2',695,529,'431.50',NULL),(61,'q_41_up_2',600,544,'15',NULL),(62,'rate_41_up_2',643,544,'45.00',NULL),(63,'amount_41_up_2',695,544,'675.00',NULL),(64,'discount_amount_2',695,562,'159.67',NULL),(65,'tax_amount_2',695,577,'42.43',NULL),(66,'arrears_amount_2',695,607,'212.13',NULL),(67,'scf_amount_2',695,622,'500.00',NULL),(68,'subtotal_amount_due_2',695,642,'2121.33',NULL),(69,'penalty_amount_2',695,657,'212.13',NULL),(70,'total_amount_due_2',695,672,'2375.89',NULL),(71,'billing_date_3',300,823,'2025-10-15',NULL),(72,'due_date_3',665,753,'2025-10-30',NULL),(73,'concessionaire_name_3',190,871,'ESCABA, VINCENT',NULL),(74,'address_3',190,901,'BRGY. BANILE IGBARAS ILOILO',NULL),(75,'concessionaire_code_3',190,918,'01-1-12-013J',NULL),(76,'from_reading_date_3',288,931,'2025-10-01',NULL),(77,'to_reading_date_3',368,931,'2025-10-15',NULL),(78,'previous_reading_3',210,983,'0',NULL),(79,'present_reading_3',290,983,'55',NULL),(80,'total_meter_consumed_3',370,983,'55',NULL),(81,'billed_meter_3',600,808,'55',NULL),(82,'minimum_charge_3',643,823,'352.00',NULL),(83,'total_water_consumption_amount_3',695,808,'2281.00',NULL),(84,'q_1_10_3',600,843,'10',NULL),(85,'rate_1_10_3',643,843,'37.15',NULL),(86,'amount_1_10_3',695,843,'371.50',NULL),(87,'q_11_20_3',600,858,'10',NULL),(88,'rate_11_20_3',643,858,'39.15',NULL),(89,'amount_11_20_3',695,858,'391.50',NULL),(90,'q_21_30_3',600,873,'10',NULL),(91,'rate_21_30_3',643,873,'41.15',NULL),(92,'amount_21_30_3',695,873,'411.50',NULL),(93,'q_31_40_3',600,888,'10',NULL),(94,'rate_31_40_3',643,888,'43.15',NULL),(95,'amount_31_40_3',695,888,'431.50',NULL),(96,'q_41_up_3',600,903,'15',NULL),(97,'rate_41_up_3',643,903,'45.00',NULL),(98,'amount_41_up_3',695,903,'675.00',NULL),(99,'discount_amount_3',695,921,'159.67',NULL),(100,'tax_amount_3',695,936,'42.43',NULL),(101,'arrears_amount_3',695,966,'212.13',NULL),(102,'scf_amount_3',695,981,'500.00',NULL),(103,'subtotal_amount_due_3',695,1001,'2121.33',NULL),(104,'penalty_amount_3',695,1016,'212.13',NULL),(105,'total_amount_due_3',695,1031,'2375.89',NULL);
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
INSERT INTO `collection_print_settings` VALUES (1,'collection_date',340,170,'2025-10-15','Date of payment'),(2,'concessionaire_name',110,200,'DELA CRUZ, JUAN','Concessionaire name'),(3,'address',110,248,'IGBARAS, ILOILO','Address '),(4,'total_current_bill',300,310,'250.00','Total current bill'),(5,'total_arrears',300,335,'100.00','Total arrears'),(6,'total_penalty',300,360,'25.00','Total penalty'),(7,'total_tax',300,385,'15.00','Tax amount'),(8,'total_scf',300,410,'10.00','SCF charge'),(9,'total_others',300,435,'5.00','Other charges'),(10,'total_paid_amount',300,455,'500.00','Total Amount Paid');
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
  `status` varchar(45) DEFAULT NULL,
  `tin_number` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`concessionaire_id`),
  KEY `zone_id` (`zone_id`),
  KEY `service_id` (`service_id`),
  KEY `idx_concessionaire_concessionaire_name` (`concessionaire_name`),
  KEY `idx_concessionaire_concessionaire_code` (`concessionaire_code`),
  KEY `idx_concessionaire_zone_id` (`zone_id`),
  CONSTRAINT `concessionaire_fk_service` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`),
  CONSTRAINT `concessionaire_fk_zone` FOREIGN KEY (`zone_id`) REFERENCES `zone` (`zone_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1800 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `concessionaire`
--

LOCK TABLES `concessionaire` WRITE;
/*!40000 ALTER TABLE `concessionaire` DISABLE KEYS */;
/*!40000 ALTER TABLE `concessionaire` ENABLE KEYS */;
UNLOCK TABLES;

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
  `payment_type` enum('cash','bank') DEFAULT 'cash',
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
  KEY `idx_payment_collection_id` (`collection_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
) ENGINE=InnoDB AUTO_INCREMENT=17605 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reading`
--

LOCK TABLES `reading` WRITE;
/*!40000 ALTER TABLE `reading` DISABLE KEYS */;
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
  `total_amount` decimal(12,2) NOT NULL,
  `balance` decimal(12,2) DEFAULT '0.00',
  `monthly` decimal(12,2) DEFAULT '0.00',
  `updated_at` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `last_payment_date` datetime DEFAULT NULL,
  PRIMARY KEY (`scf_id`),
  UNIQUE KEY `concessionaire_id_UNIQUE` (`concessionaire_id`),
  KEY `concessionaire_id` (`concessionaire_id`),
  CONSTRAINT `scf_balance_fk_concessionaire` FOREIGN KEY (`concessionaire_id`) REFERENCES `concessionaire` (`concessionaire_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scf_balance`
--

LOCK TABLES `scf_balance` WRITE;
/*!40000 ALTER TABLE `scf_balance` DISABLE KEYS */;
/*!40000 ALTER TABLE `scf_balance` ENABLE KEYS */;
UNLOCK TABLES;

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
INSERT INTO `services` VALUES (1,'RESIDENTIAL','1/2',352.00,37.15,39.15,41.15,43.15),(2,'COMMERCIAL FULL','1/2',704.00,74.30,78.30,82.30,86.30),(3,'GOVERNMENT A','1/2',352.00,37.15,39.15,41.15,43.15),(4,'COMMERCIAL A','1/2',616.00,65.00,68.50,72.00,75.50),(5,'COMMERCIAL B','1/2',528.00,55.75,58.75,61.75,64.75),(6,'GOVERNMENT B','3/4',519.75,32.50,34.20,35.90,37.60),(7,'GOVERNMENT C','3/4',563.20,37.15,39.15,41.15,43.15),(8,'RESIDENTIAL 3/4','3/4',563.20,37.15,39.15,41.15,43.15),(9,'COMMERCIAL A 3/4','3/4',985.60,65.00,68.50,72.00,75.50);
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
) ENGINE=InnoDB AUTO_INCREMENT=206 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_settings`
--

LOCK TABLES `system_settings` WRITE;
/*!40000 ALTER TABLE `system_settings` DISABLE KEYS */;
INSERT INTO `system_settings` VALUES (1,'discount_percent','7'),(2,'discount_thresh_hold','30'),(3,'penalize_after_days','15'),(4,'penalty_percent','10'),(5,'tax_percent','2'),(35,'aging_date','20260131');
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
  `action` text NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`log_id`),
  KEY `idx_created_at` (`created_at`)
) ENGINE=InnoDB AUTO_INCREMENT=2366 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_logs`
--

LOCK TABLES `user_logs` WRITE;
/*!40000 ALTER TABLE `user_logs` DISABLE KEYS */;
INSERT INTO `user_logs` VALUES (2348,'User logged in: System Administrator (ADMIN)','2026-05-21 15:24:09',1),(2349,'User logged in: System Administrator (ADMIN)','2026-05-21 16:01:13',1),(2350,'User logged in: System Administrator (ADMIN)','2026-05-21 16:01:22',1),(2351,'User logged in: System Administrator (ADMIN)','2026-05-21 16:01:26',1),(2352,'User logged in: System Administrator (ADMIN)','2026-05-21 16:01:29',1),(2353,'User logged in: System Administrator (ADMIN)','2026-05-21 16:01:32',1),(2354,'ADMIN ACCESS via secret key: System Administrator','2026-05-21 16:01:36',1),(2355,'User logged in: System Administrator (ADMIN)','2026-05-21 16:04:29',1),(2356,'ADMIN ACCESS via secret key: System Administrator','2026-05-21 16:07:35',1),(2357,'User logged in: System Administrator (ADMIN)','2026-05-21 17:10:39',1),(2358,'User logged in: System Administrator (ADMIN)','2026-05-21 17:18:40',1),(2359,'User logged in: System Administrator (ADMIN)','2026-05-21 17:28:47',1),(2360,'ADMIN ACCESS via secret key: System Administrator','2026-05-25 10:21:46',1),(2361,'ADMIN ACCESS via secret key: System Administrator','2026-05-25 11:32:36',1),(2362,'Updated user account \'a\'.','2026-05-25 11:32:41',1),(2363,'Updated user account \'a\'.','2026-05-25 11:32:43',1),(2364,'Updated user account \'a\'.','2026-05-25 11:32:43',1),(2365,'Updated user account \'cashier2\'.','2026-05-25 11:32:44',1);
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
  `role` enum('ADMIN','BILLER','CASHIER') NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`),
  KEY `idx_users_full_name` (`full_name`),
  KEY `idx_users_role` (`role`),
  KEY `idx_users_username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'a','a','System Administrator','ADMIN',1,'2026-05-21 14:59:38'),(2,'b','b','Juan Dela Cruz','BILLER',1,'2026-05-21 14:59:38'),(3,'biller2','biller123','Maria Santos','BILLER',1,'2026-05-21 14:59:38'),(4,'c','c','Pedro Reyes','CASHIER',1,'2026-05-21 14:59:38'),(5,'cashier2','cashier123','Ana Lopez','CASHIER',1,'2026-05-21 14:59:38');
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
 1 AS `Zone_Name`,
 1 AS `Service_Type`,
 1 AS `Pipe_Size`*/;
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
 1 AS `entry_type`,
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
 1 AS `discount_amount`,
 1 AS `tax_amount`,
 1 AS `scf_amount`,
 1 AS `arrears_amount`,
 1 AS `penalty_amount`,
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
 1 AS `full_name`,
 1 AS `action`,
 1 AS `created_at`*/;
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
INSERT INTO `zone` VALUES (1,'ZONE 1'),(2,'ZONE 2'),(3,'ZONE 3'),(4,'ZONE 4'),(5,'ZONE 5'),(6,'ZONE 6'),(7,'ZONE 7');
/*!40000 ALTER TABLE `zone` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'water_district_billing_system_db_tubungan'
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
-- Dumping routines for database 'water_district_billing_system_db_tubungan'
--
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
        FROM `water_district_billing_system_db_text_upgrade_aging_breakdown`.`collection`
        WHERE or_number = v_or_number
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Duplicate OR';
    END IF;

    IF NOT EXISTS (
        SELECT 1
        FROM `water_district_billing_system_db_text_upgrade_aging_breakdown`.`concessionaire`
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
    FROM `water_district_billing_system_db_text_upgrade_aging_breakdown`.`concessionaire` c
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
    FROM `water_district_billing_system_db_text_upgrade_aging_breakdown`.`scf_balance` sb
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

    UPDATE `water_district_billing_system_db_text_upgrade_aging_breakdown`.`scf_balance`
    SET
        balance = v_scf_balance_after,
        updated_at = NOW()
    WHERE scf_id = v_scf_id;

    SELECT COALESCE(SUM(b.remaining_balance), 0.00)
    INTO v_uncollected
    FROM `water_district_billing_system_db_text_upgrade_aging_breakdown`.`billing` b
    WHERE b.concessionaire_id = p_concessionaire_id
      AND b.remaining_balance > 0;

    SET v_grand_total = ROUND(v_scf_amount_used + v_total_others, 2);
    SET v_amount_received = v_grand_total;
    SET v_change_amount = 0.00;

    INSERT INTO `water_district_billing_system_db_text_upgrade_aging_breakdown`.`collection` (
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
    DECLARE v_bill_number VARCHAR(50) DEFAULT '';
    DECLARE v_payment_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    IF p_billing_id IS NULL OR p_billing_id <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid billing id';
    END IF;

    IF p_voided_by_user_id IS NULL OR p_voided_by_user_id <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid user id';
    END IF;

    SET p_void_remarks = COALESCE(p_void_remarks, '');

    START TRANSACTION;

    SELECT b.bill_number
    INTO v_bill_number
    FROM billing b
    WHERE b.billing_id = p_billing_id
    FOR UPDATE;

    IF COALESCE(v_bill_number, '') = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Billing record not found';
    END IF;

    SELECT COUNT(*)
    INTO v_payment_count
    FROM payment p
    WHERE p.billing_id = p_billing_id;

    IF v_payment_count > 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot void billing with existing payment records';
    END IF;

    DELETE FROM billing
    WHERE billing_id = p_billing_id;

    INSERT INTO user_logs (action, user_id)
    VALUES (
        CONCAT_WS(
            ' | ',
            CONCAT('Voided billing ', v_bill_number),
            CONCAT('Billing ID: ', p_billing_id),
            CONCAT('Remarks: ', COALESCE(NULLIF(TRIM(p_void_remarks), ''), 'N/A'))
        ),
        p_voided_by_user_id
    );

    COMMIT;

    SELECT
        p_billing_id AS billing_id,
        v_bill_number AS bill_number,
        'DELETED' AS status;
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
		  AND COALESCE(p.status, 'POSTED') = 'POSTED'
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

	IF UPPER(v_collection_status) = 'VOID' THEN
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
			p.status = 'VOID',
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
			  AND COALESCE(p2.status, 'POSTED') = 'POSTED'
		),
		b.last_payment_date = (
			SELECT MAX(p2.payment_date)
			FROM payment p2
			WHERE p2.billing_id = b.billing_id
			  AND COALESCE(p2.status, 'POSTED') = 'POSTED'
		),
		b.last_collection_id = (
			SELECT p2.collection_id
			FROM payment p2
			WHERE p2.billing_id = b.billing_id
			  AND COALESCE(p2.status, 'POSTED') = 'POSTED'
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
/*!50001 VIEW `v_collection_report` AS select `col`.`collection_id` AS `Collection_ID`,`col`.`or_number` AS `OR_Number`,`col`.`collection_date` AS `Date`,coalesce(`col`.`bill_numbers`,'') AS `Invoice_Number`,coalesce(`col`.`concessionaire_code`,'') AS `Concessionaire_Code`,coalesce(`col`.`concessionaire_name`,'') AS `Concessionaire_Name`,coalesce(`col`.`address`,'') AS `Address`,coalesce(`col`.`total_current_bill`,0.00) AS `Water_Charge`,coalesce(`col`.`total_arrears`,0.00) AS `Arrears`,coalesce(`col`.`total_penalty`,0.00) AS `Penalty`,coalesce(`col`.`total_tax`,0.00) AS `Tax`,coalesce(`col`.`total_scf`,0.00) AS `SCF`,coalesce(`col`.`total_water_bill_paid`,0.00) AS `Total_Water_Bill_Paid`,coalesce(`col`.`scf_paid`,0.00) AS `SCF_Paid`,coalesce(`col`.`total_others`,0.00) AS `Others`,coalesce(`col`.`grand_total`,0.00) AS `Grand_Total`,coalesce(`col`.`amount_received`,0.00) AS `Amount_Received`,coalesce(`col`.`change_amount`,0.00) AS `Change_Amount`,coalesce(`col`.`total_paid_amount`,0.00) AS `Collected`,coalesce(`col`.`total_discount`,0.00) AS `Total_Discount`,coalesce(`col`.`payment_type`,'') AS `Payment_Type`,coalesce(`col`.`payment_reference`,'') AS `Payment_Reference`,coalesce(`col`.`remarks`,'') AS `Remarks`,coalesce(`col`.`payment_ids`,'') AS `Payment_IDs`,`col`.`created_by` AS `Created_By`,coalesce(`col`.`created_by_name`,'') AS `Created_By_Name`,`col`.`created_at` AS `Created_At`,`col`.`updated_at` AS `Updated_At`,coalesce(`col`.`uncollected`,0.00) AS `Uncollected` from `collection` `col` order by `col`.`collection_id` desc */;
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
/*!50001 VIEW `v_concessionaire_details` AS select `c`.`concessionaire_id` AS `Concessionaire_ID`,`c`.`concessionaire_code` AS `Account_No`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`address` AS `Address`,`c`.`tin_number` AS `Tin`,`c`.`status` AS `Status`,`c`.`meter_no` AS `Meter_Number`,`c`.`first_reading_date` AS `FRD`,`c`.`is_tax_exempt` AS `Tax_Exempted`,`c`.`is_due_exempt` AS `Due_Exempted`,`c`.`is_discounted` AS `Discounted`,`z`.`zone_name` AS `Zone_Name`,`s`.`service_type` AS `Service_Type`,`s`.`pipe_size` AS `Pipe_Size` from ((`concessionaire` `c` left join `zone` `z` on((`c`.`zone_id` = `z`.`zone_id`))) left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) order by `c`.`concessionaire_name` */;
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
/*!50001 VIEW `v_customer_ledger` AS select 'BILL' AS `entry_type`,`c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`b`.`billing_date` AS `transaction_date`,concat('BILL-',`b`.`bill_number`) AS `reference_no`,concat('Water Bill - ',convert(date_format(`b`.`billing_date`,'%M %Y') using utf8mb4)) AS `remarks`,round(coalesce(`b`.`total_amount`,0.00),2) AS `debit`,0.00 AS `credit`,round(coalesce(`b`.`water_charge`,0.00),2) AS `water_charge`,round(coalesce(`b`.`discount_amount`,0.00),2) AS `discount_amount`,round(coalesce(`b`.`tax_amount`,0.00),2) AS `tax_amount`,round(coalesce(`b`.`scf_amount`,0.00),2) AS `scf_amount`,round(coalesce(`b`.`arrears_amount`,0.00),2) AS `arrears_amount`,round(coalesce(`b`.`penalty_amount`,0.00),2) AS `penalty_amount`,`b`.`status` AS `billing_status` from (`billing` `b` join `concessionaire` `c` on((`c`.`concessionaire_id` = `b`.`concessionaire_id`))) union all select 'PAYMENT' AS `entry_type`,`c`.`concessionaire_id` AS `concessionaire_id`,`c`.`concessionaire_code` AS `concessionaire_code`,`c`.`concessionaire_name` AS `concessionaire_name`,`c`.`address` AS `address`,`c`.`zone_id` AS `zone_id`,`col`.`collection_date` AS `transaction_date`,concat('OR-',`col`.`or_number`) AS `reference_no`,coalesce(nullif(trim(`col`.`remarks`),''),concat('Payment - OR#',`col`.`or_number`)) AS `remarks`,0.00 AS `debit`,round(coalesce(`col`.`grand_total`,0.00),2) AS `credit`,round(coalesce(`col`.`total_current_bill`,0.00),2) AS `water_charge`,round(coalesce(`col`.`total_discount`,0.00),2) AS `discount_amount`,round(coalesce(`col`.`total_tax`,0.00),2) AS `tax_amount`,round(coalesce(`col`.`scf_paid`,0.00),2) AS `scf_amount`,round(coalesce(`col`.`total_arrears`,0.00),2) AS `arrears_amount`,round(coalesce(`col`.`total_penalty`,0.00),2) AS `penalty_amount`,NULL AS `billing_status` from (`collection` `col` join `concessionaire` `c` on((`c`.`concessionaire_code` = `col`.`concessionaire_code`))) */;
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
/*!50001 VIEW `v_input_meter_reading` AS select (case when exists(select 1 from `billing` `b` where ((`b`.`concessionaire_id` = `c`.`concessionaire_id`) and (month(`b`.`billing_date`) = month(curdate())) and (year(`b`.`billing_date`) = year(curdate())))) then true else false end) AS `Is_Billed_Today`,`c`.`zone_id` AS `Zone`,`c`.`concessionaire_id` AS `Concessionaire_Id`,`c`.`concessionaire_code` AS `Concessionaire_Code`,`c`.`concessionaire_name` AS `Concessionaire_Name`,`c`.`meter_no` AS `Meter_Number`,coalesce((select date_format(`r2`.`reading_date`,'%Y-%m-%d') from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),(case when ((month(`c`.`first_reading_date`) in (month(curdate()),month((curdate() - interval 1 month)))) and (year(`c`.`first_reading_date`) in (year(curdate()),year((curdate() - interval 1 month))))) then date_format(`c`.`first_reading_date`,'%Y-%m-%d') else NULL end)) AS `Previous_Reading_Date`,coalesce((select `r2`.`present_reading` from `reading` `r2` where (`r2`.`concessionaire_id` = `c`.`concessionaire_id`) order by `r2`.`reading_date` desc limit 1),0) AS `Previous_Reading` from `concessionaire` `c` where (`c`.`status` <> 'DISCONNECTED') */;
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
/*!50001 VIEW `v_service_summary` AS select `s`.`service_id` AS `ServiceID`,`s`.`service_type` AS `ServiceType`,`s`.`pipe_size` AS `PipeSize`,count(`c`.`concessionaire_id`) AS `TotalConcessionaires`,sum(`c`.`is_tax_exempt`) AS `TaxExemptCount`,sum(`c`.`is_due_exempt`) AS `DueExemptCount`,sum(`c`.`is_discounted`) AS `DiscountedCount`,sum((case when (`c`.`status` = 'ACTIVE') then 1 else 0 end)) AS `ActiveCount`,sum((case when (`c`.`status` <> 'ACTIVE') then 1 else 0 end)) AS `InactiveCount`,sum((case when ((`c`.`meter_no` is not null) and (`c`.`meter_no` <> '')) then 1 else 0 end)) AS `MetersAssigned`,sum((case when ((`c`.`meter_no` is null) or (`c`.`meter_no` = '')) then 1 else 0 end)) AS `MetersUnassigned` from (`concessionaire` `c` left join `services` `s` on((`c`.`service_id` = `s`.`service_id`))) group by `s`.`service_id`,`s`.`service_type`,`s`.`pipe_size` order by `s`.`service_type` */;
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
/*!50001 VIEW `v_user_logs` AS select `l`.`log_id` AS `log_id`,`u`.`user_id` AS `user_id`,`u`.`full_name` AS `full_name`,`l`.`action` AS `action`,`l`.`created_at` AS `created_at` from (`users` `u` join `user_logs` `l` on((`u`.`user_id` = `l`.`user_id`))) */;
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

-- Dump completed on 2026-05-25 12:13:46
