-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: short_urls
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `role_id` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `create_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `update_at` datetime DEFAULT NULL,
  `create_by` int DEFAULT NULL,
  `update_by` int DEFAULT NULL,
  `role_code` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `delete_at` datetime DEFAULT NULL,
  `is_deleted` bit(1) DEFAULT b'0',
  `description` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT '',
  `priority` int NOT NULL DEFAULT '5',
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Admin',_binary '','2023-09-26 20:25:03',NULL,1,NULL,'ADMIN',NULL,_binary '\0','',1),(2,'Quản lý',_binary '','2023-09-27 08:32:17',NULL,1,NULL,'MOD',NULL,_binary '\0','',2),(3,'VIP Member',_binary '','2023-09-27 10:02:21',NULL,1,NULL,'VIP',NULL,_binary '\0','',3),(4,'thành viên',_binary '','2023-09-27 10:02:32','2023-09-27 16:48:24',1,1,'MEMBER',NULL,_binary '\0','',4),(6,'Member',_binary '','2023-09-27 16:56:03','2023-09-27 16:56:26',1,1,'MEMBER','2023-09-27 16:56:26',_binary '','',4);
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `short_urls`
--

DROP TABLE IF EXISTS `short_urls`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `short_urls` (
  `hash_id` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `long_url` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `short_url` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `create_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_at` datetime DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `user_id` int NOT NULL,
  `click_count` bigint unsigned NOT NULL DEFAULT '0',
  `is_deleted` bit(1) NOT NULL DEFAULT b'0',
  `deleted_at` datetime DEFAULT NULL,
  PRIMARY KEY (`hash_id`),
  KEY `fk_user_short_url` (`user_id`),
  CONSTRAINT `fk_user_short_url` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `short_urls`
--

LOCK TABLES `short_urls` WRITE;
/*!40000 ALTER TABLE `short_urls` DISABLE KEYS */;
INSERT INTO `short_urls` VALUES ('0311e6be1a3f8cc1150f4e9fe0a82ae08b914d41d59c8b0398f0332b5e219d94','https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641','kKrgLG-toRa','2023-10-11 10:16:25','2023-10-12 09:43:13',_binary '',3,0,_binary '','2023-10-12 09:43:13'),('296f90b82b9494641e5ff6b7b8b3263672f13fa6c0b554486f938d1020c3307c','https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641','bGeKmcxr_','2023-10-06 05:52:29',NULL,_binary '',1,0,_binary '\0',NULL),('9b5950dfd82b891449aa3cb037e2947e7dbbdc8040f97517ce5286edf24d8b8d','https://6-4-0--reactstrap.netlify.app/components/form/23232','kDMAcaj_','2023-10-23 04:14:33','2023-10-31 03:20:41',_binary '',3,6,_binary '\0',NULL),('ffe60fb9a729f7382300a3623525acd7fbdb340c18fdaad7c3ed19c7d7aed5ff','https://6-4-0--reactstrap.netlify.app/components/form/','SkWYaqwvfwW','2023-10-23 04:18:10',NULL,_binary '',3,0,_binary '\0',NULL);
/*!40000 ALTER TABLE `short_urls` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_black_lists`
--

DROP TABLE IF EXISTS `token_black_lists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `token_black_lists` (
  `id` int NOT NULL AUTO_INCREMENT,
  `access_token` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_black_lists`
--

LOCK TABLES `token_black_lists` WRITE;
/*!40000 ALTER TABLE `token_black_lists` DISABLE KEYS */;
INSERT INTO `token_black_lists` VALUES (1,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU4OTQ0NTIsImV4cCI6MTY5NjQ5OTI1MiwiaWF0IjoxNjk1ODk0NDUyfQ.hxiZciqB4HmF8i7K8sEWjbz8JCGjiHZWSi9vKhQRIUA','2023-09-29 03:41:30'),(2,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTkxNTAsImV4cCI6MTY5NjU2Mzk1MCwiaWF0IjoxNjk1OTU5MTUwfQ.IhvGqp7ZG1ysFOcioQtTUO8Dk65iYgqXr9vYh3ZexSg','2023-09-29 03:45:57'),(3,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTkyMTUsImV4cCI6MTY5NjU2NDAxNSwiaWF0IjoxNjk1OTU5MjE1fQ.myDXO6OsidwBrD8xifTNerPkGN_76BfiCkQz2yyaq2c','2023-09-29 03:46:57'),(4,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTk1NzMsImV4cCI6MTY5NjU2NDM3MywiaWF0IjoxNjk1OTU5NTczfQ.CSEPeFY6rTTwcdx9LsneikD2riTIXqbP3p-pAnc2yFA','2023-09-29 03:53:06'),(5,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTYyMTkwNjQsImV4cCI6MTY5NjgyMzg2NCwiaWF0IjoxNjk2MjE5MDY0fQ.Q9qm3_13plN5RSvTyfTJ3glyp2h8_dJ4-2AOVDblFTQ','2023-10-02 03:57:50'),(6,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5ODMwMzMxMSwiZXhwIjoxNjk4OTA4MTExLCJpYXQiOjE2OTgzMDMzMTF9.PoKzzQ3KdX9Itt7cbzv2qP7lxBvMaLWbElXc5Jbxb6k','2023-10-31 01:45:56');
/*!40000 ALTER TABLE `token_black_lists` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_role`
--

DROP TABLE IF EXISTS `user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_role` (
  `user_id` int NOT NULL,
  `role_id` int NOT NULL,
  PRIMARY KEY (`user_id`,`role_id`),
  KEY `fk_userrole_role` (`role_id`),
  CONSTRAINT `fk_userrole_role` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`),
  CONSTRAINT `fk_userrole_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_role`
--

LOCK TABLES `user_role` WRITE;
/*!40000 ALTER TABLE `user_role` DISABLE KEYS */;
INSERT INTO `user_role` VALUES (1,1),(2,2),(1,3),(2,4),(3,4);
/*!40000 ALTER TABLE `user_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `phone` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `address` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `create_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `update_at` datetime DEFAULT NULL,
  `update_by` int DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'nguyen van an','$2a$12$OjXG/48JwsWUohtYjdDtleUSEs1Qrdse5A2w7Eb.HcOXD0n2ZB95i','0389324159','tp.hcm',_binary '','2023-09-26 10:52:10','2023-09-29 11:07:19',1),(2,'nguyen van b','$2a$12$OjXG/48JwsWUohtYjdDtleUSEs1Qrdse5A2w7Eb.HcOXD0n2ZB95i','0389324158','tp.hcm',_binary '','2023-09-27 09:46:35',NULL,0),(3,'nguyen van c','$2a$12$OjXG/48JwsWUohtYjdDtleUSEs1Qrdse5A2w7Eb.HcOXD0n2ZB95i','0389324157','tp.hcm',_binary '','2023-09-28 08:55:17',NULL,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'short_urls'
--
/*!50003 DROP PROCEDURE IF EXISTS `Proc_GetShortUrls` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_GetShortUrls`(
    IN in_Keyword NVARCHAR(255),
    IN in_Status bit(1),
    IN in_Offset INT,
    IN in_endRecord INT,
    IN in_UserId INT,
	IN in_StartDate Datetime,
	IN in_EndDate Datetime,
    OUT out_TotalRecord INT
)
begin
	DECLARE v_Total INT;
	set v_Total = 0;
	set in_Keyword = '';

    SELECT COUNT(*) INTO v_Total
    FROM Short_Urls
    WHERE (in_Keyword = '' OR 
		title LIKE CONCAT('%', in_Keyword, '%') or 
    	long_url LIKE CONCAT('%', in_Keyword, '%') or 
    	short_url LIKE CONCAT('%', in_Keyword, '%') or 
    	Hash_Id LIKE CONCAT('%', in_Keyword, '%')
    	)
		and (in_StartDate is NULL OR create_at >= in_StartDate)
		and (in_EndDate is NULL OR create_at <= in_EndDate)
    	AND (in_Status is NULL OR Status = in_Status)
		and (in_UserId is null or User_Id = in_UserId)
		and is_deleted = 0;

    SET out_TotalRecord = v_Total;

    SELECT Hash_Id, Title, Long_Url, short_url, Status, click_count, User_Id, create_at, update_at
    FROM Short_Urls
    WHERE (in_Keyword = '' OR 
		title LIKE CONCAT('%', in_Keyword, '%') or 
    	long_url LIKE CONCAT('%', in_Keyword, '%') or 
    	short_url LIKE CONCAT('%', in_Keyword, '%') or 
    	Hash_Id LIKE CONCAT('%', in_Keyword, '%')
    	)
    	and (in_StartDate is NULL OR create_at >= in_StartDate)
		and (in_EndDate is NULL OR create_at <= in_EndDate)
    	AND (in_Status is NULL OR Status = in_Status)
		and (in_UserId is null or User_Id = in_UserId)
		and is_deleted = 0
    LIMIT in_endRecord
    OFFSET in_Offset;
END $$
DELIMITER ;

-- SET @TotalRecord = 0;
-- CALL `Proc_GetShortUrls`('', 0, 0, 10, 3, '2023-10-01 0:00:00', '2023-11-29 0:00:00', @TotalRecord);

-- SELECT @TotalRecord AS TotalRecord;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-01  9:54:02
