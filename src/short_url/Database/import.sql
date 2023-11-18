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
  `title` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
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
INSERT INTO `short_urls` VALUES ('0311e6be1a3f8cc1150f4e9fe0a82ae08b914d41d59c8b0398f0332b5e219d94','https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641','kKrgLG-toRa','2023-10-11 10:16:25','2023-11-07 03:54:13',_binary '',3,0,_binary '\0',NULL,'kKrgLG-toRa'),('227af5e38aaad803de67ad332ca796e14cf496a22c6cff2f3a037ed0039bad62','https://www.youtube.com/watch?v=kOjfi8uUSro','WUkLQCfqA','2023-11-12 03:45:36','2023-11-13 03:47:45',_binary '',2,0,_binary '\0',NULL,'WUkLQCfqA'),('296f90b82b9494641e5ff6b7b8b3263672f13fa6c0b554486f938d1020c3307c','https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641','bGeKmcxr_','2023-10-06 05:52:29',NULL,_binary '',1,0,_binary '\0',NULL,'bGeKmcxr_'),('455e92b9ba0aa2c81172fbc0cf1dd110d91f21010f5c9637d71dc226bae93e47','https://www.pluralsight.com/guides/how-to-use-react-to-set-the-value-of-an-input','SzhmHMvECspnH','2023-11-08 09:05:47','2023-11-13 03:47:42',_binary '',3,0,_binary '\0',NULL,'SzhmHMvECspnH'),('5433ee23228bee77204c1f217725c2679348d0195c23f923c76ed47a181a2f30','https://www.youtube.com/watch?v=usRxeGVkWcM','GJsPDMSIa','2023-11-12 03:47:14',NULL,_binary '',2,0,_binary '\0',NULL,'GJsPDMSIa'),('7c8adb6ccea12eef4e6f7c6c2dde8b1e4b126b0c860243e509190bcc5a98614d','https://www.youtube.com/watch?v=8idicWGjYBI&t=89s','vmUmHNDIFC','2023-11-12 03:45:10',NULL,_binary '',2,0,_binary '\0',NULL,'vmUmHNDIFC'),('88aed0a3c53d37de723e82490847e4730a3512b1a0088abe2ac869adc580fa8e','https://www.youtube.com/','lFqfBpzFGwm','2023-11-12 03:45:21',NULL,_binary '\0',2,0,_binary '\0',NULL,'lFqfBpzFGwm'),('983a4c65e5e5b3a00d06627cd21ebf96fc37dd641d30fa52991593262e5793cb','https://www.facebook.com/watch/?ref=tab','sWqTgJDXjANy','2023-11-12 03:44:13',NULL,_binary '',2,0,_binary '\0',NULL,'sWqTgJDXjANy'),('9b5950dfd82b891449aa3cb037e2947e7dbbdc8040f97517ce5286edf24d8b8d','https://www.facebook.com/profile','kDMAcaj_','2023-10-23 04:14:33','2023-11-13 03:44:46',_binary '',3,6,_binary '\0',NULL,'kDMAcaj_'),('a7105124e495f33108a72e37948bd1a1b7bcbdcfb04f1b329920aeb96e4eb670','https://www.youtube.com/watch?v=ZvKfaTjjCVY','GnpsEjz_h','2023-11-12 03:47:35',NULL,_binary '',2,0,_binary '\0',NULL,'GnpsEjz_h'),('bcebf190761f6866621fa7cbe34ded4364f073687e97b9fdb4dcf6ac1f8620cd','https://www.youtube.com/watch?v=2rJ2sVJ_x0E','uuQdLSGgJWCR','2023-11-12 03:45:04',NULL,_binary '',2,0,_binary '\0',NULL,'uuQdLSGgJWCR'),('f3dcbb73900662998117d209906fe8bc2b961d6f55004d4e1f3c983e80425594','https://www.youtube.com/watch?v=W4oXOrisqpY','JWrHMCXLdkpz','2023-11-12 03:46:57',NULL,_binary '',2,0,_binary '\0',NULL,'JWrHMCXLdkpz'),('f9176bee88cd67f1fabb6f476521beabec972b441f14942b4d039169ee6c0dec','https://www.youtube.com/watch?v=mslGaWmdXN8','ryUvGffuJ','2023-11-12 03:45:47',NULL,_binary '',2,0,_binary '\0',NULL,'ryUvGffuJ'),('ffe60fb9a729f7382300a3623525acd7fbdb340c18fdaad7c3ed19c7d7aed5ff','https://6-4-0--reactstrap.netlify.app/components/form/','SkWYaqwvfwW','2023-11-12 04:18:10','2023-11-07 03:54:27',_binary '',3,1,_binary '\0',NULL,'SkWYaqwvfwW');
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
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_black_lists`
--

LOCK TABLES `token_black_lists` WRITE;
/*!40000 ALTER TABLE `token_black_lists` DISABLE KEYS */;
INSERT INTO `token_black_lists` VALUES (1,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU4OTQ0NTIsImV4cCI6MTY5NjQ5OTI1MiwiaWF0IjoxNjk1ODk0NDUyfQ.hxiZciqB4HmF8i7K8sEWjbz8JCGjiHZWSi9vKhQRIUA','2023-09-29 03:41:30'),(2,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTkxNTAsImV4cCI6MTY5NjU2Mzk1MCwiaWF0IjoxNjk1OTU5MTUwfQ.IhvGqp7ZG1ysFOcioQtTUO8Dk65iYgqXr9vYh3ZexSg','2023-09-29 03:45:57'),(3,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTkyMTUsImV4cCI6MTY5NjU2NDAxNSwiaWF0IjoxNjk1OTU5MjE1fQ.myDXO6OsidwBrD8xifTNerPkGN_76BfiCkQz2yyaq2c','2023-09-29 03:46:57'),(4,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTU5NTk1NzMsImV4cCI6MTY5NjU2NDM3MywiaWF0IjoxNjk1OTU5NTczfQ.CSEPeFY6rTTwcdx9LsneikD2riTIXqbP3p-pAnc2yFA','2023-09-29 03:53:06'),(5,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTYyMTkwNjQsImV4cCI6MTY5NjgyMzg2NCwiaWF0IjoxNjk2MjE5MDY0fQ.Q9qm3_13plN5RSvTyfTJ3glyp2h8_dJ4-2AOVDblFTQ','2023-10-02 03:57:50'),(6,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5ODMwMzMxMSwiZXhwIjoxNjk4OTA4MTExLCJpYXQiOjE2OTgzMDMzMTF9.PoKzzQ3KdX9Itt7cbzv2qP7lxBvMaLWbElXc5Jbxb6k','2023-10-31 01:45:56'),(7,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5ODkxODA2NiwiZXhwIjoxNjk5NTIyODY2LCJpYXQiOjE2OTg5MTgwNjZ9.hdp1czA6JWAXmFoikSV-4IqsDYmFlo7a0UeePvf2WuA','2023-11-03 04:24:37'),(8,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5OTMyOTAwMiwiZXhwIjoxNjk5OTMzODAyLCJpYXQiOjE2OTkzMjkwMDJ9.EgTklndyVbu3TW4i6RssK4pSIC_JRG4veL4PvGAy8lU','2023-11-08 09:12:03'),(9,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5OTQzNjE4NiwiZXhwIjoxNzAwMDQwOTg2LCJpYXQiOjE2OTk0MzYxODZ9.6qOK9yZe0gIVjsMa1SU0oOGhGR848B7PvaX_8yqIqoU','2023-11-08 10:04:08'),(10,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5OTQzODE0MCwiZXhwIjoxNzAwMDQyOTQwLCJpYXQiOjE2OTk0MzgxNDB9.qRYlYNujUBw_FBpZLj4jjP_UkoIdDSKDYO53xUiilMc','2023-11-08 10:09:24'),(11,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NTkwODAzLCJleHAiOjE3MDAxOTU2MDMsImlhdCI6MTY5OTU5MDgwM30.zNaZt-DA4Rkog7XdIobcN7HIiIyrjXnn9veMe5ZSVDE','2023-11-10 09:23:33'),(12,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6Ik1FTUJFUiIsIm5iZiI6MTY5OTYwODIyMSwiZXhwIjoxNzAwMjEzMDIxLCJpYXQiOjE2OTk2MDgyMjF9.bBK1-rkDpAFtyRCWdJqaOyeLkO8LTQg1mQl8t6FoG44','2023-11-11 14:27:10'),(13,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3MTI4NzMsImV4cCI6MTcwMDMxNzY3MywiaWF0IjoxNjk5NzEyODczfQ.cJkvjrgpWZMV62UFyNl3jZQC6CD9WydiRTMeV7QFoMo','2023-11-11 14:28:11'),(14,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3MTI5MjUsImV4cCI6MTcwMDMxNzcyNSwiaWF0IjoxNjk5NzEyOTI1fQ.tN_VCUKdl85UTH8qIReaSDWwTeDLXEcIWyt5F3C4NU0','2023-11-11 14:29:13'),(15,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3MTI5NjEsImV4cCI6MTcwMDMxNzc2MSwiaWF0IjoxNjk5NzEyOTYxfQ.1WitYToKr8Kbf18ZV9nIWwKbGaQ5gvmZTGYGgu8-m9M','2023-11-11 14:29:58'),(16,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzEzNzc4LCJleHAiOjE3MDAzMTg1NzgsImlhdCI6MTY5OTcxMzc3OH0.1EF_nfN4oGZ-6e8Yy2B7YB7bdA_iURt42uU98YNxqbY','2023-11-11 14:49:32'),(17,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzE0MjAxLCJleHAiOjE3MDAzMTkwMDEsImlhdCI6MTY5OTcxNDIwMX0.ykAOfIR0W2nZeFU8OxKuEHk_dG34uJT-XJu61pF70u0','2023-11-11 14:50:56'),(18,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzE0MjczLCJleHAiOjE3MDAzMTkwNzMsImlhdCI6MTY5OTcxNDI3M30.0Omv4-c1f4AtSnMd-N7B_autDTdujEzyiXI9r_ioFCM','2023-11-11 14:52:10'),(19,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3MTQ0NDUsImV4cCI6MTcwMDMxOTI0NSwiaWF0IjoxNjk5NzE0NDQ1fQ.e9w6Npl8UTvjJ-ng-YMHSpsvEs4l_FZCcc08KxBdonk','2023-11-12 02:24:41'),(20,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3NTYzNzUsImV4cCI6MTcwMDM2MTE3NSwiaWF0IjoxNjk5NzU2Mzc1fQ.sI29l01PUvK2lJgr0CdTCv61aXIVYSgVBzW6S2lYOx0','2023-11-12 02:35:22'),(21,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3NTY1MjksImV4cCI6MTcwMDM2MTMyOSwiaWF0IjoxNjk5NzU2NTI5fQ.7EqSDlcEtPtVxqN1mZtBGLWZrR27Tpj_QvedX5mJUC0','2023-11-12 02:42:59'),(22,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzU3MDA1LCJleHAiOjE3MDAzNjE4MDUsImlhdCI6MTY5OTc1NzAwNX0.2Iu3oyfFVNaq4_yueyuk-b5dBGopRE7hr1BmtBYY6eQ','2023-11-12 02:44:06'),(23,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3NTc5MDEsImV4cCI6MTcwMDM2MjcwMSwiaWF0IjoxNjk5NzU3OTAxfQ.b7QPrYAQJN7ZJOkEGfpbjCZtJJ66p9hJbCFxsTHvusk','2023-11-12 03:17:25'),(24,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzU5MDUxLCJleHAiOjE3MDAzNjM4NTEsImlhdCI6MTY5OTc1OTA1MX0.VZTyiDwof1aWYh4I53pWVHWOaYDlL-8IHGGU84QB0-w','2023-11-12 06:44:27'),(25,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3NzE0NzcsImV4cCI6MTcwMDM3NjI3NywiaWF0IjoxNjk5NzcxNDc3fQ.IXAzJ6dT4id6DJzELBNyqmX64fIsxW-pPI8Jh6VdOr0','2023-11-12 07:08:54'),(26,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5NzcyOTQxLCJleHAiOjE3MDAzNzc3NDEsImlhdCI6MTY5OTc3Mjk0MX0.zNZiqLdIzBt-l0rWfWDFlZNyurw8Ikev9icosn6Q-BM','2023-11-12 07:09:12'),(27,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3NzI5NzEsImV4cCI6MTcwMDM3Nzc3MSwiaWF0IjoxNjk5NzcyOTcxfQ.MClZp357wDsaljrIWqU8OvNbZPAw_P4h-iPHKeU4JVY','2023-11-12 09:28:09'),(28,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk3ODEzMDMsImV4cCI6MTcwMDM4NjEwMywiaWF0IjoxNjk5NzgxMzAzfQ.g8nvOqce4MswI7C_4xyUScMQlJaaNZ7If3OOmcruVnY','2023-11-13 04:16:36'),(29,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5ODQ5MDAyLCJleHAiOjE3MDA0NTM4MDIsImlhdCI6MTY5OTg0OTAwMn0.Dv8P3yTp_D6esxBbGPYzAlq7mws0RGVXqcTz698xA5k','2023-11-13 04:42:16'),(30,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk4NTA1NDgsImV4cCI6MTcwMDQ1NTM0OCwiaWF0IjoxNjk5ODUwNTQ4fQ.0pysoZy7ioSmMu9exUzoNfxbpfNfbN6OrOJw5v4Vj38','2023-11-13 14:44:30'),(31,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6WyJBRE1JTiIsIlZJUCJdLCJuYmYiOjE2OTk4ODg1ODEsImV4cCI6MTcwMDQ5MzM4MSwiaWF0IjoxNjk5ODg4NTgxfQ.1lIe-VY4beJVV1LYU9cs_C2PIETbdYsYaEnHKu2mSpc','2023-11-14 01:38:04'),(32,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5OTMwNDUwLCJleHAiOjE3MDA1MzUyNTAsImlhdCI6MTY5OTkzMDQ1MH0.BPiGQ-yG6Ya4J5aRmES46T6QkSgmZLx-xaLvBMjosts','2023-11-14 02:57:09'),(33,'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwicm9sZSI6WyJNT0QiLCJNRU1CRVIiXSwibmJmIjoxNjk5OTMwODk3LCJleHAiOjE3MDA1MzU2OTcsImlhdCI6MTY5OTkzMDg5N30.3_EA0HJj8KAwzkCL15jKYOcZs87nXRpYN0vrl0jgCU4','2023-11-14 03:08:50');
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
INSERT INTO `user_role` VALUES (1,1),(2,2),(1,3),(2,4),(3,4),(4,4);
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
  `email` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `address` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `create_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `update_at` datetime DEFAULT NULL,
  `update_by` int DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'nguyen van c','$2a$12$OjXG/48JwsWUohtYjdDtleUSEs1Qrdse5A2w7Eb.HcOXD0n2ZB95i','admin1@gmail.com','tp.hcm',_binary '','2023-09-26 10:52:10','2023-09-29 11:07:19',1),(2,'nguyen van ba','$2a$12$OjXG/48JwsWUohtYjdDtleUSEs1Qrdse5A2w7Eb.HcOXD0n2ZB95i','user1@gmail.com','q1, tp.hcm',_binary '','2023-09-27 09:46:35','2023-11-13 11:40:23',2),(3,'nguyen van can','$2a$11$YRWxBKQg5jVBCzkPV6lPfucwp4oLxlRwK2BBrJsI93ZsFpmkxufjS','user2@gmail.com','tp.hcm',_binary '','2023-09-28 08:55:17','2023-11-11 19:52:39',3),(4,'Pham Dang','$2a$12$azsMkCwxMsHbgk.1IybRSOF44MSzNKNsVOoB/Q18G5Jds5tj96gB6','user3@gmail.com','tp.hcm',_binary '\0','2023-11-09 15:21:15',NULL,0);
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



    SELECT COUNT(*) INTO v_Total

    FROM short_urls

    WHERE (in_Keyword is null or

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

    FROM short_urls

    WHERE (in_Keyword is null or

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

-- Dump completed on 2023-11-15 15:29:20
