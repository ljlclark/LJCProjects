
CREATE TABLE if not exists `testdata`.`CVSex` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Code` char(2) NOT NULL,
  `Name` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`ID`),
  unique index `UKCVSex`
    (`Name` asc) visible
  );
-- GO

insert ignore into CVSex (Code, Name) values('NA', 'Not Available');
insert ignore into CVSex (Code, Name) values('M', 'Male');
insert ignore into CVSex (Code, Name) values('F', 'Female');
insert ignore into CVSex (Code, Name) values('AM', 'Assumed Male');
insert ignore into CVSex (Code, Name) values('AF', 'Assumed Female');
-- GO

CREATE TABLE if not exists `testdata`.`CVPerson` (
  `ID` BIGINT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(30) NOT NULL,
  `MiddleName` varchar(30) NULL,
  `LastName` VARCHAR(30) NOT NULL,
  `CVSexID` INT NOT NULL default 1,
  `DeliveryAddressLine` VARCHAR(45) NULL,
  `LastLine` varchar(45) NULL,
  `Phone` varchar(15) NULL,
  `RegionID` int NULL,
  `ProvinceID` int NULL,
  `CityID` int NULL,
  `CitySectionID` int NULL,
  PRIMARY KEY (`ID`),
  unique index `UKCVPersonName`
    (`FirstName`, `MiddleName`, `LastName` asc) visible,
  constraint `FKCVPersonCVSex`
    foreign key (`CVSexID`)
    references `TestData`.`CVSex` (`ID`)
    on delete no action on update no action
  );
-- GO

CREATE TABLE if not exists `TestData`.`CVVisit` (
  `ID` bigint NOT NULL auto_increment,
  `FacilityID` int NOT NULL default(1),
  `CVPersonID` bigint NOT NULL default(1),
  `RegisterTime` datetime NOT NULL,
  `EnterTime` datetime NULL,
  `ExitTime` datetime NULL,
  `BaseTemperature` varchar(5) NULL,
  `BaseTemperatureUnitID` int NULL,
  `Temperature` varchar(5) NULL,
  `TemperatureUnitID` int NULL,
  PRIMARY KEY (`ID`),
  constraint `FKCVVisitFacility`
    foreign key (`FacilityID`)
    references `TestData`.`Facility` (`ID`)
    on delete no action on update no action,
  constraint `FKCVVisitCVPerson`
    foreign key (`CVPersonID`)
    references `TestData`.`CVPerson` (`ID`)
    on delete no action on update no action
  );
-- GO
