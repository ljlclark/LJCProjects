USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CVSex')
BEGIN
CREATE TABLE [dbo].[CVSex](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](2) NOT NULL,
  [Name] [nvarchar](15) NOT NULL,
  CONSTRAINT [PK_CVSex]
  PRIMARY KEY CLUSTERED (
    [ID] ASC)
)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CVSex')
BEGIN
if not exists (select Code from CVSex where Code = 'NA')
insert into CVSex
 (Code, Name)
 values('NA', 'Not Available');

if not exists (select Code from CVSex where Code = 'M')
insert into CVSex
 (Code, Name)
 values('M', 'Male');

if not exists (select Code from CVSex where Code = 'F')
insert into CVSex
 (Code, Name)
 values('F', 'Female');

if not exists (select Code from CVSex where Code = 'AM')
insert into CVSex
 (Code, Name)
 values('AM', 'Assumed Male');

if not exists (select Code from CVSex where Code = 'AF')
insert into CVSex
 (Code, Name)
 values('A', 'Assumed Female');
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CVPerson')
BEGIN
CREATE TABLE [dbo].[CVPerson](
  [ID] [bigint] IDENTITY(1,1) NOT NULL,
  [FirstName] [nvarchar](30) NOT NULL,
  [MiddleName] [nvarchar](30) NULL,
  [LastName] [nvarchar](30) NOT NULL,
  [CVSexID] [int] NOT NULL,
  [DeliveryAddressLine] [nvarchar](45) NULL,
  [LastLine] [nvarchar](45) NULL,
  [Phone] [nvarchar](15) NULL,
  [RegionID] [int] NULL,
  [ProvinceID] [int] NULL,
  [CityID] [int] NULL,
  [CitySectionID] [int] NULL,
  CONSTRAINT [PK_CVPerson]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_CVPersonCVSex]
  FOREIGN KEY ([CVSexID])
  REFERENCES [dbo].[CVSex]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CVVisit')
BEGIN
CREATE TABLE [dbo].[CVVisit](
  [ID] [bigint] IDENTITY(1,1) NOT NULL,
  [FacilityID] [int] NOT NULL,
  [CVPersonID] [bigint] NOT NULL,
  [RegisterTime] [datetime] NOT NULL,
  [EnterTime] [datetime] NULL,
  [ExitTime] [datetime] NULL,
  [BaseTemperature] [nvarchar](5) NULL,
  [BaseTemperatureUnitID] [int] NULL,
  [Temperature] [nvarchar](5) NULL,
  [TemperatureUnitID] [int] NULL,
  CONSTRAINT [PK_CVVisit]
  PRIMARY KEY CLUSTERED (
    [ID] ASC),
  CONSTRAINT [FK_CVVisitFacility]
  FOREIGN KEY ([FacilityID])
  REFERENCES [dbo].[Facility]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT [FK_CVVisitCVPerson]
  FOREIGN KEY ([CVPersonID])
  REFERENCES [dbo].[CVPerson]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO
