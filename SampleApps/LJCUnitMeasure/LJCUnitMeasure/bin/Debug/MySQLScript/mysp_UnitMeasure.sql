-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_UnitMeasure.sql
DELIMITER //
CREATE PROCEDURE `mysp_UnitMeasure` (
)
BEGIN
CREATE TABLE IF NOT EXISTS `TestData`.`UnitMeasure` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `UnitCategoryID` int NOT NULL,
  `UnitSystemID` int NOT NULL,
  `Code` varchar(5) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `AltName` varchar(30) NOT NULL,
  `Sequence` int NOT NULL,
  `Description` varchar(40) DEFAULT NULL, 
  PRIMARY KEY (`ID`)
) AUTO_INCREMENT = 1;
END//
DELIMITER ;