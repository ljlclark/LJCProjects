-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_UnitSystem.sql
DELIMITER //
CREATE PROCEDURE `mysp_UnitSystem` (
)
BEGIN
CREATE TABLE IF NOT EXISTS `TestData`.`UnitSystem` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(5) NOT NULL,
  `Name` varchar(30) NOT NULL, 
  PRIMARY KEY (`ID`)
) AUTO_INCREMENT = 1;
END//
DELIMITER ;