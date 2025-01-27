CREATE TABLE IF NOT EXISTS `TestData`.`Region` (
  `RegionID` int NOT NULL auto_increment,
  `Number` varchar(5) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  PRIMARY KEY (`RegionID`),
  UNIQUE INDEX `uq_Region`
    (`Number` asc)
);

CREATE TABLE IF NOT EXISTS `TestData`.`Province` (
  `ProvinceID` int NOT NULL auto_increment,
  `RegionID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `Abbreviation` char(3) NULL,
  PRIMARY KEY (`ProvinceID`),
  UNIQUE INDEX `uq_Province`
    (`Name` asc),
  CONSTRAINT `fk_Province_Region`
    FOREIGN KEY (`RegionID`)
    REFERENCES `TestData`.`Region` (`RegionID`)
);

CREATE TABLE IF NOT EXISTS `TestData`.`City` (
  `CityID` int NOT NULL auto_increment,
  `ProvinceID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `CityFlag` bit NOT NULL,
  `ZipCode` char(4) NULL,
  `District` smallint NULL,
  PRIMARY KEY (`CityID`),
  UNIQUE INDEX `uq_City`
    (`Name` asc),
  CONSTRAINT `fk_City_Province`
    FOREIGN KEY (`ProvinceID`)
    REFERENCES `TestData`.`Province` (`ProvinceID`)
);

CREATE TABLE IF NOT EXISTS `TestData`.`CitySection` (
  `ID` int NOT NULL auto_increment,
  `CityID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `ZoneType` varchar(25) NULL,
  `Contact` varchar(60) NULL,
  PRIMARY KEY (`ID`),
  unique index `uq_Province`
    (`CityID`, `Name` asc) VISIBLE,
  constraint `FK_CitySection_City`
    FOREIGN KEY (`CityID`)
    REFERENCES `TestData`.`City` (`CityID`)
    on delete no action
    on update no action
);
