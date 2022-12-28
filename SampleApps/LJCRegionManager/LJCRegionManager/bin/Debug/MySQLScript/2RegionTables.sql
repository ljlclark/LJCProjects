CREATE TABLE if not exists `TestData`.`Region` (
  `RegionID` int NOT NULL auto_increment,
  `Number` varchar(5) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  PRIMARY KEY  (`RegionID`),
  unique index `UKRegionNumber`
    (`Number` asc) visible
);

CREATE TABLE if not exists `TestData`.`Province`(
  `ProvinceID` int NOT NULL auto_increment,
  `RegionID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `Abbreviation` char(3) NULL,
  PRIMARY KEY (`ProvinceID`),
  unique index `UKProvinceName`
    (`Name` asc) visible,
  constraint `FKProvinceRegion`
    foreign key (`RegionID`)
    references `TestData`.`Region` (`RegionID`)
    on delete no action
    on update no action
);

CREATE TABLE if not exists `TestData`.`City`(
  `CityID` int NOT NULL auto_increment,
  `ProvinceID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `CityFlag` bit NOT NULL,
  `ZipCode` char(4) NULL,
  `District` smallint NULL,
  PRIMARY KEY (`CityID`),
  unique index `UKProvinceName`
    (`Name` asc) visible,
  constraint `FKCityProvince`
    foreign key (`ProvinceID`)
    references `TestData`.`Province` (`ProvinceID`)
    on delete no action
    on update no action
);

CREATE TABLE if not exists `TestData`.`CitySection`(
  `ID` int NOT NULL auto_increment,
  `CityID` int NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Description` varchar(100) NULL,
  `ZoneType` varchar(25) NULL,
  `Contact` varchar(60) NULL,
  PRIMARY KEY (`ID`),
  unique index `UKProvinceName`
    (`CityID`, `Name` asc) visible,
  constraint `FKCitySectionCity`
    foreign key (`CityID`)
    references `TestData`.`City` (`CityID`)
    on delete no action
    on update no action
);
