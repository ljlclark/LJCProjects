
CREATE TABLE if not exists `TestData`.CodeTypeClass (
  `ID` int NOT NULL auto_increment,
  `Code` varchar(25) NOT NULL,
  `Description` varchar(60) NOT NULL,
  PRIMARY KEY (`ID`),
  unique index `UKCodeTypeClassCode`
    (`Code` asc) visible
  );
-- GO

insert ignore into CodeTypeClass (Code, Description)
 values('Facility', 'Facility Type Class');
-- GO

CREATE TABLE if not exists `TestData`.CodeType (
  `ID` int NOT NULL auto_increment,
  `Code` varchar(25) NOT NULL,
  `Description` varchar(60) NOT NULL,
  `CodeTypeClassID` int NOT NULL,
  PRIMARY KEY (`Id`),
  unique index `UKCodeTypeCode`
    (`Code` asc) visible,
  constraint `FKCodeTypeCodeTypeClass`
    foreign key (`CodeTypeClassID`)
    references `TestData`.`CodeTypeClass` (`ID`)
    on delete no action on update no action
  );
-- GO

insert ignore into CodeType (Code, Description, CodeTypeClassID)
 values('FACBDODIP', 'BDO Bank Dipolog', 1);
-- GO

CREATE TABLE if not exists `TestData`.`Facility` (
  `ID` int NOT NULL auto_increment,
  `Code` varchar(25) NOT NULL,
  `Description` varchar(60) NOT NULL,
  `CodeTypeID` int NOT NULL,
  PRIMARY KEY (`ID`),
  unique index `UKFacilityCode`
    (`Code` asc) visible,
  constraint `FKFacilityCodeType`
    foreign key (`CodeTypeID`)
    references `TestData`.`CodeType` (`ID`)
    on delete no action on update no action
  );
-- GO

insert ignore into Facility (Code, Description, CodeTypeID)
 values('FACBDODIP1', 'BDO Bank Dipolog Main Branch', 1);
insert ignore into Facility (Code, Description, CodeTypeID)
 values('FACBDODIP2', 'BDO Bank Dipolog Branch 2', 1);
insert ignore into Facility (Code, Description, CodeTypeID)
 values('FACBDODIP3', 'BDO Bank Dipolog Branch 3', 1);
-- GO
