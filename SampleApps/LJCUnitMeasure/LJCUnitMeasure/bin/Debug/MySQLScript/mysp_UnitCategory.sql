-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_UnitCategory.sql
DELIMITER //
CREATE PROCEDURE `mysp_UnitCategory` (
)
BEGIN
CREATE TABLE IF NOT EXISTS `TestData`.`UnitCategory` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(5) NOT NULL,
  `Name` varchar(30) NOT NULL, 
  PRIMARY KEY (`ID`)
) AUTO_INCREMENT = 1;
END//
DELIMITER ;