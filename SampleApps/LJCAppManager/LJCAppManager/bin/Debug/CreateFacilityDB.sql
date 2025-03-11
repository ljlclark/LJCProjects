USE [FacilityData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CodeTypeClass')
BEGIN
CREATE TABLE [dbo].[CodeTypeClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	CONSTRAINT [PK_CodeTypeClass]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC)
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
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	[CodeTypeClass_Id] [int] NOT NULL,
	CONSTRAINT [PK_CodeType]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_CodeTypeCodeTypeClass]
	FOREIGN KEY ([CodeTypeClass_Id])
	REFERENCES [dbo].[CodeTypeClass]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Facility')
BEGIN
CREATE TABLE [dbo].[Facility](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	[CodeType_Id] [int] NOT NULL,
	CONSTRAINT [PK_Facility]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_FacilityCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Unit')
BEGIN
CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Facility_Id] [int] NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	[CodeType_Id] [int] NOT NULL,
	[Beds] [smallint] NOT NULL,
	[Baths] [smallint] NOT NULL,
	[Phone] [nvarchar](18) NULL,
	[Extension] [nchar](4) NULL,
	CONSTRAINT [PK_Unit]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_UnitCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
  ALTER TABLE Unit Add
  CONSTRAINT [FK_UnitFacility]
  FOREIGN KEY ([Facility_Id])
  REFERENCES [dbo].[Facility]([Id])
  ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Person')
BEGIN
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](45) NULL,
	[MiddleInitial] [nchar](1) NULL,
	[LastName] [nvarchar](45) NOT NULL,
	[Sex] [nchar](1) NULL,
	[CodeType_Id] [int] NOT NULL,
	[PrincipleFlag] [bit] NOT NULL,
	[PrincipleTitle] [nvarchar](30) NULL,
	[BirthDate] [datetime] NULL,
	[Phone] [nvarchar](18) NULL,
	[Extension] [nchar](4) NULL,
	[CellPhone] [nvarchar](18) NULL,
	[Fax] [nvarchar](18) NULL,
	[UserId] [nvarchar](10) NULL,
	[Password] [nvarchar](100) NULL,
	CONSTRAINT [PK_Person]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_PersonCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitPerson')
BEGIN
CREATE TABLE [dbo].[UnitPerson](
	[Unit_Id] [int] NOT NULL,
	[Person_Id] [int] NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	CONSTRAINT [PK_UnitPerson]
	PRIMARY KEY CLUSTERED (
	  [Unit_Id], [Person_Id] ASC),
	CONSTRAINT [FK_UnitPersonPerson]
	FOREIGN KEY ([Person_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Equipment')
BEGIN
CREATE TABLE [dbo].[Equipment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Unit_Id] [int] NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	[CodeType_Id] [int] NOT NULL,
	[Make] [nvarchar](25) NULL,
	[Model] [nvarchar](25) NULL,
	[SerialNumber] [nvarchar](25) NULL,
	CONSTRAINT [PK_Equipment]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_EquipmentCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
  ALTER TABLE Equipment Add
  CONSTRAINT [FK_EquipmentUnit]
  FOREIGN KEY ([Unit_Id])
  REFERENCES [dbo].[Unit]([Id])
  ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Fixture')
BEGIN
CREATE TABLE [dbo].[Fixture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Unit_Id] [int] NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
	[CodeType_Id] [int] NOT NULL,
	[Make] [nvarchar](25) NULL,
	[Model] [nvarchar](25) NULL,
	[SerialNumber] [nvarchar](25) NULL,
	CONSTRAINT [PK_Fixture]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_FixtureCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
  ALTER TABLE Fixture Add
  CONSTRAINT [FK_FixtureUnit]
  FOREIGN KEY ([Unit_Id])
  REFERENCES [dbo].[Unit]([Id])
  ON DELETE NO ACTION ON UPDATE NO ACTION
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'PersonRelation')
BEGIN
CREATE TABLE [dbo].[PersonRelation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person_Id] [int] NOT NULL,
	[Relation_Id] [int] NOT NULL,
	[RelationCodeType_Id] [int] NOT NULL,
	CONSTRAINT [PK_PersonRelation]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_PersonRelationPerson]
	FOREIGN KEY ([Person_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_PersonRelationRelation]
	FOREIGN KEY ([Relation_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_PersonRelationCodeType]
	FOREIGN KEY ([RelationCodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Address')
BEGIN
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](45) NULL,
	[City] [nvarchar](45) NULL,
	[Province_Code] [nchar](3) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[CodeType_Id] [int] NOT NULL,
	CONSTRAINT [PK_Address]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_AddressProvince]
	FOREIGN KEY ([Province_Code])
	REFERENCES [dbo].[Province]([Code])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_AddressCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'PersonAddress')
BEGIN
CREATE TABLE [dbo].[PersonAddress](
	[Person_Id] [int] NOT NULL,
	[Address_Id] [int] NOT NULL,
	CONSTRAINT [PK_PersonAddress] 
	PRIMARY KEY CLUSTERED (
	  [Person_Id], [Address_Id] ASC),
	CONSTRAINT [FK_PersonAddressPerson]
	FOREIGN KEY ([Person_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_PersonAddressAddress]
	FOREIGN KEY ([Address_Id])
	REFERENCES [dbo].[Address]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Account')
BEGIN
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](60) NULL,
	[Person_Id] [int] NULL,
	[Business_Id] [int] NULL,
	[IdNumber] [nvarchar](25) NULL,
	[GroupNumber] [nvarchar](25) NULL,
	[PlanNumber] [nvarchar](25) NULL,
	[EffectiveDate] [DateTime] NULL,
	[ExpirationDate] [DateTime] NULL,
	CONSTRAINT [PK_Account] 
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_AccountPerson]
	FOREIGN KEY ([Person_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_AccountBusiness]
	FOREIGN KEY ([Business_Id])
	REFERENCES [dbo].[Business]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Business')
BEGIN
CREATE TABLE [dbo].[Business](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NULL,
	[Description] [nvarchar](60) NULL,
	[CodeType_Id] [int] NULL,
	[EffectiveDate] [DateTime] NULL,
	[ExpirationDate] [DateTime] NULL,
	[Phone] [nvarchar](18) NULL,
	[Extension] [nchar](4) NULL,
	[Fax] [nvarchar](18) NULL,
	CONSTRAINT [PK_Business]
	PRIMARY KEY CLUSTERED (
	  [Id] ASC),
	CONSTRAINT [FK_BusinessCodeType]
	FOREIGN KEY ([CodeType_Id])
	REFERENCES [dbo].[CodeType]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Province')
BEGIN
CREATE TABLE [dbo].[Province](
	[Code] [nchar](3) NOT NULL,
	[Name] [nvarchar](30) NULL,
	CONSTRAINT [PK_Province]
	PRIMARY KEY CLUSTERED (
	  [Code] ASC)
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'BusinessPerson')
BEGIN
CREATE TABLE [dbo].[BusinessPerson](
	[Business_Id] [int] NOT NULL,
	[Person_Id] [int] NOT NULL,
	CONSTRAINT [PK_BusinessPerson]
	PRIMARY KEY CLUSTERED (
	  [Business_Id], [Person_Id] ASC),
	CONSTRAINT [FK_BusinessPersonBusiness]
	FOREIGN KEY ([Business_Id])
	REFERENCES [dbo].[Business]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_BusinessPersonPerson]
	FOREIGN KEY ([Person_Id])
	REFERENCES [dbo].[Person]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'BusinessAddress')
BEGIN
CREATE TABLE [dbo].[BusinessAddress](
	[Business_Id] [int] NOT NULL,
	[Address_Id] [int] NOT NULL,
	CONSTRAINT [PK_BusinessAddress]
	PRIMARY KEY CLUSTERED (
	  [Business_Id], [Address_Id] ASC),
	CONSTRAINT [FK_BusinessAddressBusiness]
	FOREIGN KEY ([Business_Id])
	REFERENCES [dbo].[Business]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_BusinessAddressAddress]
	FOREIGN KEY ([Business_Id])
	REFERENCES [dbo].[Address]([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO
