USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
drop table BusinessAddress;
drop table BusinessPerson;
drop table Account;
drop table Business;
drop table PersonAddress;
drop table Address;
drop table PersonRelation;
drop table Fixture;
drop table Equipment;
drop table UnitPerson;
drop table Person;
drop table Unit;
drop table Facility;
drop table CodeType;
drop table CodeTypeClass;
*/

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CodeTypeClass')
BEGIN
CREATE TABLE [dbo].[CodeTypeClass](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  CONSTRAINT [PK_CodeTypeClass]
  PRIMARY KEY CLUSTERED (
    [ID] ASC)
)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CodeTypeClass')
BEGIN
if not exists (select Code from CodeTypeClass where Code = 'Facility')
insert into CodeTypeClass
 (Code, Description)
 values('Facility', 'Facility Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Unit')
insert into CodeTypeClass
 (Code, Description)
 values('Unit', 'Unit Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Fixture')
insert into CodeTypeClass
 (Code, Description)
 values('Fixture', 'Fixture Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Equipment')
insert into CodeTypeClass
 (Code, Description)
 values('Equipment', 'Equipment Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Person')
insert into CodeTypeClass
 (Code, Description)
 values('Person', 'Person Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Business')
insert into CodeTypeClass
 (Code, Description)
 values('Business', 'Business Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Address')
insert into CodeTypeClass
 (Code, Description)
 values('Address', 'Address Type Class')

if not exists (select Code from CodeTypeClass where Code = 'Relation')
insert into CodeTypeClass
 (Code, Description)
 values('Relation', 'Relation Type Class')
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CodeType')
BEGIN
CREATE TABLE [dbo].[CodeType](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  [CodeTypeClassID] [int] NOT NULL,
  CONSTRAINT [PK_CodeType]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_CodeType_CodeTypeClass]
  FOREIGN KEY ([CodeTypeClassID])
  REFERENCES [dbo].[CodeTypeClass]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

declare @CodeClassTypeID int;

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Address');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'ADDRBUS')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('ADDRBUS', 'Business Address', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'ADDRPER')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('ADDRPER', 'Personal Address', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Business');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'BUSAC')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('BUSAC', 'Air Conditioning Sales Repair', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Equipment');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'EQPRJ')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('EQPRJ', 'Audio Visual Projector', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Facility');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'FACCHR')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('FACCHR', 'Church', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'FACSCH')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('FACSCH', 'School', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Fixture');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'FIXAC')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('FIXAC', 'Air Conditioner', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Person');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'PERCONT')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('PERCONT', 'A Contact Person', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'PERPAR')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('PERPAR', 'School Student Parent', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'PERSTU')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('PERSTU', 'School Student', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'PERUSER')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('PERUSER', 'Application User', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Relation');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'RELBRO')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('RELBRO', 'Brother', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'RELFATH')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('RELFATH', 'Father', @CodeClassTypeID)

  if not exists (select Code from CodeType where Code = 'RELSIS')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('RELSIS', 'Sister', @CodeClassTypeID)
end

set @CodeClassTypeID = (select ID from CodeTypeClass where Code = 'Unit');
if @CodeClassTypeID > 0
begin
  if not exists (select Code from CodeType where Code = 'UNITCLS')
  insert into CodeType
   (Code, Description, CodeTypeClassID)
   values('UNITCLS', 'Class Room', @CodeClassTypeID)
end
go

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Facility')
BEGIN
CREATE TABLE [dbo].[Facility](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  [CodeTypeID] [int] NOT NULL,
  CONSTRAINT [PK_Facility]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Facility_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Unit')
BEGIN
CREATE TABLE [dbo].[Unit](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [FacilityID] [int] NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  [CodeTypeID] [int] NOT NULL,
  [Beds] [smallint] NOT NULL,
  [Baths] [smallint] NOT NULL,
  [Phone] [nvarchar](18) NULL,
  [Extension] [nchar](4) NULL,
  CONSTRAINT [PK_Unit]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Unit_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)

ALTER TABLE Unit Add
CONSTRAINT [FK_Unit_Facility]
FOREIGN KEY ([FacilityID])
REFERENCES [dbo].[Facility]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Person')
BEGIN
CREATE TABLE [dbo].[Person](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [FirstName] [nvarchar](45) NULL,
  [MiddleInitial] [nchar](1) NULL,
  [LastName] [nvarchar](45) NOT NULL,
  [Sex] [nchar](1) NULL,
  [CodeTypeID] [int] NOT NULL,
  [PrincipleFlag] [bit] NOT NULL,
  [PrincipleTitle] [nvarchar](30) NULL,
  [BirthDate] [datetime] NULL,
  [Phone] [nvarchar](18) NULL,
  [Extension] [nchar](4) NULL,
  [CellPhone] [nvarchar](18) NULL,
  [Fax] [nvarchar](18) NULL,
  [UserID] [nvarchar](10) NULL,
  [Password] [nvarchar](100) NULL,
  CONSTRAINT [PK_Person]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Person_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'PersonTest')
BEGIN
CREATE TABLE [dbo].[PersonTest](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](60) NULL,
  [PrincipleFlag] [bit] NOT NULL,
  CONSTRAINT [PK_PersonTest]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitPerson')
BEGIN
CREATE TABLE [dbo].[UnitPerson](
  [UnitID] [int] NOT NULL,
  [PersonID] [int] NOT NULL,
  [BeginDate] [datetime] NOT NULL,
  [EndDate] [datetime] NULL,
  CONSTRAINT [PK_UnitPerson]
  PRIMARY KEY CLUSTERED (
    [UnitID], [PersonID] ASC),
  CONSTRAINT [FK_UnitPerson_Person]
  FOREIGN KEY ([PersonID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Equipment')
BEGIN
CREATE TABLE [dbo].[Equipment](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [UnitID] [int] NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  [CodeTypeID] [int] NOT NULL,
  [Make] [nvarchar](25) NULL,
  [Model] [nvarchar](25) NULL,
  [SerialNumber] [nvarchar](25) NULL,
  CONSTRAINT [PK_Equipment]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Equipment_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)

ALTER TABLE Equipment Add
CONSTRAINT [FK_Equipment_Unit]
FOREIGN KEY ([UnitID])
REFERENCES [dbo].[Unit]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Fixture')
BEGIN
CREATE TABLE [dbo].[Fixture](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [UnitID] [int] NOT NULL,
  [Code] [nvarchar](25) NOT NULL,
  [Description] [nvarchar](60) NOT NULL,
  [CodeTypeID] [int] NOT NULL,
  [Make] [nvarchar](25) NULL,
  [Model] [nvarchar](25) NULL,
  [SerialNumber] [nvarchar](25) NULL,
  CONSTRAINT [PK_Fixture]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Fixture_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)

ALTER TABLE Fixture Add
CONSTRAINT [FK_Fixture_Unit]
FOREIGN KEY ([UnitID])
REFERENCES [dbo].[Unit]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'PersonRelation')
BEGIN
CREATE TABLE [dbo].[PersonRelation](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [PersonID] [int] NOT NULL,
  [RelationID] [int] NOT NULL,
  [RelationCodeTypeID] [int] NOT NULL,
  CONSTRAINT [PK_PersonRelation]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_PersonRelation_Person]
  FOREIGN KEY ([PersonID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_PersonRelation_Relation]
  FOREIGN KEY ([RelationID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_PersonRelation_CodeType]
  FOREIGN KEY ([RelationCodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Region')
BEGIN
CREATE TABLE [dbo].[Region](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Number] [nvarchar](5) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_Region] PRIMARY KEY  CLUSTERED
  (
    [ID] ASC
  )
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Province')
BEGIN
CREATE TABLE [dbo].[Province](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [RegionID] [int] NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [Abbreviation] [char](3) NULL,
  CONSTRAINT [PK_Province] PRIMARY KEY  CLUSTERED
  (
    [ID] ASC
  )
) ON [PRIMARY]
ALTER TABLE [dbo].[Province] WITH CHECK
  ADD CONSTRAINT [FK_Province_Region]
  FOREIGN KEY([RegionID])
  REFERENCES [dbo].[Region] ([ID])
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Address')
BEGIN
CREATE TABLE [dbo].[Address](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [RegionID] [int] NULL,
  [ProvinceID] [int] NULL,
  [CityID] [int] NULL,
  [CitySectionID] [int] NULL,
  [Street] [nvarchar](45) NULL,
  [PostalCode] [nvarchar](10) NULL,
  [CodeTypeID] [int] NOT NULL,
  CONSTRAINT [PK_Address]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Address_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)

ALTER TABLE Address Add
CONSTRAINT [FK_Address_Region]
FOREIGN KEY ([RegionID])
REFERENCES [dbo].[Region]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE Address Add
CONSTRAINT [FK_Address_Province]
FOREIGN KEY ([ProvinceID])
REFERENCES [dbo].[Province]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE Address Add
CONSTRAINT [FK_Address_City]
FOREIGN KEY ([CityID])
REFERENCES [dbo].[City]([CityID])
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE Address Add
CONSTRAINT [FK_Address_CitySection]
FOREIGN KEY ([CitySectionID])
REFERENCES [dbo].[CitySection]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION;
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'PersonAddress')
BEGIN
CREATE TABLE [dbo].[PersonAddress](
  [PersonID] [int] NOT NULL,
  [AddressID] [int] NOT NULL,
  CONSTRAINT [PK_PersonAddress] 
  PRIMARY KEY CLUSTERED (
    [PersonID], [AddressID] ASC),
  CONSTRAINT [FK_PersonAddress_Person]
  FOREIGN KEY ([PersonID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
)

ALTER TABLE PersonAddress Add
CONSTRAINT [FK_PersonAddress_Address]
FOREIGN KEY ([AddressID])
REFERENCES [dbo].[Address]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION;
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Business')
BEGIN
CREATE TABLE [dbo].[Business](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](60) NULL,
  [Description] [nvarchar](60) NULL,
  [CodeTypeID] [int] NULL,
  [EffectiveDate] [DateTime] NULL,
  [ExpirationDate] [DateTime] NULL,
  [Phone] [nvarchar](18) NULL,
  [Extension] [nchar](4) NULL,
  [Fax] [nvarchar](18) NULL,
  CONSTRAINT [PK_Business]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Business_CodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Account')
BEGIN
CREATE TABLE [dbo].[Account](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Description] [nvarchar](60) NULL,
  [PersonID] [int] NULL,
  [BusinessID] [int] NULL,
  [IDNumber] [nvarchar](25) NULL,
  [GroupNumber] [nvarchar](25) NULL,
  [PlanNumber] [nvarchar](25) NULL,
  [EffectiveDate] [DateTime] NULL,
  [ExpirationDate] [DateTime] NULL,
  CONSTRAINT [PK_Account] 
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_Account_Person]
  FOREIGN KEY ([PersonID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_Account_Business]
  FOREIGN KEY ([BusinessID])
  REFERENCES [dbo].[Business]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'BusinessPerson')
BEGIN
CREATE TABLE [dbo].[BusinessPerson](
  [BusinessID] [int] NOT NULL,
  [PersonID] [int] NOT NULL,
  CONSTRAINT [PK_BusinessPerson]
  PRIMARY KEY CLUSTERED (
    [BusinessID], [PersonID] ASC),
  CONSTRAINT [FK_BusinessPerson_Business]
  FOREIGN KEY ([BusinessID])
  REFERENCES [dbo].[Business]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_BusinessPerson_Person]
  FOREIGN KEY ([PersonID])
  REFERENCES [dbo].[Person]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'BusinessAddress')
BEGIN
CREATE TABLE [dbo].[BusinessAddress](
  [BusinessID] [int] NOT NULL,
  [AddressID] [int] NOT NULL,
  CONSTRAINT [PK_BusinessAddress]
  PRIMARY KEY CLUSTERED (
    [BusinessID], [AddressID] ASC),
  CONSTRAINT [FK_BusinessAddress_Business]
  FOREIGN KEY ([BusinessID])
  REFERENCES [dbo].[Business]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)

ALTER TABLE BusinessAddress Add
CONSTRAINT [FK_BusinessAddress_Address]
FOREIGN KEY ([AddressID])
REFERENCES [dbo].[Address]([ID])
ON DELETE NO ACTION ON UPDATE NO ACTION;
END
GO
