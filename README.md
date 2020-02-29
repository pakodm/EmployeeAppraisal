# EmployeeAppraisal
A Net Core 2.1 API for employees evaluations

# Tech
C# .Net Core 2.1
MySQL 8.0

MySQL NuGet package can't create the DB so it needs to be created in advanced

CREATE DATABASE `appraisaldb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;

EF Core does not create the required table so it also needs to be created before the first migration
dotnet ef database update

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8 COLLATE utf8_spanish_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 COLLATE utf8_spanish_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;