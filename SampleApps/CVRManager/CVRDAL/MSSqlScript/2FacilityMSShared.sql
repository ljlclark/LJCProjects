USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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
 values('Facility', 'Facility Type Class');
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
  CONSTRAINT [FK_CodeTypeCodeTypeClass]
  FOREIGN KEY ([CodeTypeClassID])
  REFERENCES [dbo].[CodeTypeClass]([ID])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CodeType')
BEGIN
if not exists (select Code from CodeType where Code = 'FACBDODIP')
insert into CodeType
 (Code, Description, CodeTypeClassID)
 values('FACBDODIP', 'BDO Bank Dipolog', 1);
END
GO

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
    [Id] ASC),
  CONSTRAINT [FK_FacilityCodeType]
  FOREIGN KEY ([CodeTypeID])
  REFERENCES [dbo].[CodeType]([Id])
  ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Facility')
BEGIN
if not exists (select Code from Facility where Code = 'FACBDODIP1')
insert into Facility
 (Code, Description, CodeTypeID)
 values('FACBDODIP1', 'BDO Bank Dipolog Main Branch', 1);

if not exists (select Code from Facility where Code = 'FACBDODIP2')
insert into Facility
 (Code, Description, CodeTypeID)
 values('FACBDODIP2', 'BDO Bank Dipolog Branch 2', 1);

if not exists (select Code from Facility where Code = 'FACBDODIP3')
insert into Facility
 (Code, Description, CodeTypeID)
 values('FACBDODIP3', 'BDO Bank Dipolog Branch 3', 1);
END
GO
