using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LJCDataUtility.Forms.DataTable.CustomActions
{
  internal class Class1
  {
    // DELIMITER //

    // CREATE PROCEDURE GetOfficeByCountry(
    //   IN countryName VARCHAR(255)
    // )
    // BEGIN
    //   SELECT*
    //    FROM offices
    //   WHERE country = countryName;
    // END
    // //
    //DELIMITER;

    //-- Copyright(c) Lester J.Clark and Contributors.
    //-- Licensed under the MIT License.
    //-- sp_DataTable.sql
    // DELIMITER //
    // CREATE PROCEDURE `sp_DataTable` (
    //   IN Name varchar(60)
    // )
    // BEGIN
    // CREATE TABLE IF NOT EXISTS `DataTable` (
    //   `ID` bigint NOT NULL AUTO_INCREMENT,
    //   `DataSiteID` bigint NOT NULL,
    //   `DataModuleID` bigint NOT NULL,
    //   `DataModuleSiteID` bigint NOT NULL,
    //   `Name` varchar(60) NOT NULL,
    //   `Description` varchar(80) DEFAULT NULL,
    //   `Sequence` int NOT NULL DEFAULT 0,
    //   `NewName` varchar(60) DEFAULT NULL,
    //   `SchemaName` varchar(20) DEFAULT NULL
    // ) AUTO_INCREMENT = 1;
    //
    // ALTER TABLE `DataTable`
    //  ADD CONSTRAINT `pk_DataTable` PRIMARY KEY(`ID`, `DataSiteID`);

    // ALTER TABLE `DataTable`
    //  ADD CONSTRAINT `uq_DataTable`
    //  UNIQUE(`Name`);
    //    END
    //    //
    //    DELIMITER;

    // CREATE TABLE IF NOT EXISTS `DBName`.`City` (
    //   `CityID` bigint NOT NULL AUTO_INCREMENT,
    //   `ProvinceID` bigint NOT NULL,
    //   `Name` varchar(60) NOT NULL,
    //   `Description` varchar(100) DEFAULT NULL,
    //   `CityFlag` bit(1) NOT NULL,
    //   `ZipCode` char (4) DEFAULT NULL,
    //   `District` smallint DEFAULT NULL,
    //   PRIMARY KEY (`CityID`),
    //   UNIQUE INDEX `uq_Province` (`Name` asc),
    //   CONSTRAINT `fk_City_Province`
    //    FOREIGN KEY (`ProvinceID`)
    //    REFERENCES `DBName`.`Province` (`ID`)
    //);

    //) ENGINE=InnoDB AUTO_INCREMENT = 77 DEFAULT CHARSET = utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

    // ALTER TABLE `City`
    //   ADD CONSTRAINT `fk_City_Province`
    //    FOREIGN KEY (`ProvinceID`)
    //    REFERENCES `Province` (`ID`);

    //ALTER TABLE Persons
    //ADD CONSTRAINT PK_Person PRIMARY KEY (ID, LastName);

    //ALTER TABLE table_name
    //ADD CONSTRAINT constraint_name UNIQUE(column1, column2, ... column_n);

    //ALTER TABLE employees DROP CONSTRAINT pk_employee_id;
  }
}
