USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
drop table CitySection;
drop table City;
drop table Province;
drop table Region;
*/

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
 WHERE TABLE_NAME = N'City')
BEGIN
CREATE TABLE [dbo].[City](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [ProvinceID] [int] NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [CityFlag] [bit] NOT NULL,
  [ZipCode] [char](4) NULL,
  [District] [smallint] NULL,
  CONSTRAINT [PK_City] PRIMARY KEY  CLUSTERED
  (
    [ID] ASC
  )
) ON [PRIMARY]
ALTER TABLE [dbo].[City] WITH CHECK
  ADD CONSTRAINT [FK_City_Province]
  FOREIGN KEY([ProvinceID])
  REFERENCES [dbo].[Province] ([ID])
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'CitySection')
BEGIN
CREATE TABLE [dbo].[CitySection](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [CityID] [int] NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
	[ZoneType] [varchar](25) NULL,
	[Contact] [varchar](60) NULL,
  CONSTRAINT [PK_CitySection] PRIMARY KEY  CLUSTERED
  (
    [ID] ASC
  )
) ON [PRIMARY]
ALTER TABLE [dbo].[CitySection] WITH CHECK
  ADD CONSTRAINT [FK_CitySection_City]
  FOREIGN KEY([CityID])
  REFERENCES [dbo].[City] ([ID])
END
GO
