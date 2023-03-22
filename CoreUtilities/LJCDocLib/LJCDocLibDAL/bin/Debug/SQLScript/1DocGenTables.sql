/* 1DocGenTables.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
drop table DocMethod;
drop table DocMethodGroup;
drop table DocMethodGroupHeading;
drop table DocClass;
drop table DocClassGroup;
drop table DocClassGroupHeading;
drop table DocAssembly;
drop table DocAssemblyGroup;
*/

/* 1 Assembly Group for Main Page */
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocAssemblyGroup')
BEGIN
CREATE TABLE [dbo].[DocAssemblyGroup](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Heading] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocAssemblyGroup]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC)
)
END
GO

/* 2 Assembly for Main Page*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocAssembly')
BEGIN
CREATE TABLE [dbo].[DocAssembly](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[DocAssemblyGroupID] [smallint] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	[FileSpec] [nvarchar](120) NULL,
	[MainImage][nvarchar](60) NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocAssembly]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_DocAssemblyGroup]
	FOREIGN KEY ([DocAssemblyGroupID])
	REFERENCES [dbo].[DocAssemblyGroup]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

/* 3 Class Group Heading for Assembly Page*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocClassGroupHeading')
BEGIN
CREATE TABLE [dbo].[DocClassGroupHeading](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Heading] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	CONSTRAINT [PK_DocClassGroupHeading]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
)
END
GO

/* 4 Class Group for Assembly Page */
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocClassGroup')
BEGIN
CREATE TABLE [dbo].[DocClassGroup](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[DocAssemblyID] [smallint] NOT NULL,
	[DocClassGroupHeadingID] [smallint] NULL,
	[HeadingName] [nvarchar](60) NOT NULL,
	[HeadingTextCustom] [nvarchar](100) NULL,
	[Sequence] [smallint] NOT NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocClassGroup]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_DocAssembly]
	FOREIGN KEY ([DocAssemblyID])
	REFERENCES [dbo].[DocAssembly]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_DocClassGroupHeading]
	FOREIGN KEY ([DocClassGroupHeadingID])
	REFERENCES [dbo].[DocClassGroupHeading]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

/* 5 Class for Assembly Page*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocClass')
BEGIN
CREATE TABLE [dbo].[DocClass](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[DocAssemblyID] [smallint] NOT NULL,
	[DocClassGroupID] [smallint] NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocClass]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_DocClassGroup]
	FOREIGN KEY ([DocClassGroupID])
	REFERENCES [dbo].[DocClassGroup]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

/* 6 Method Group Heading for Class Page*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocMethodGroupHeading')
BEGIN
CREATE TABLE [dbo].[DocMethodGroupHeading](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Heading] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	CONSTRAINT [PK_DocMethodGroupHeading]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
)
END
GO

/* 7 Method Group for Class Page */
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocMethodGroup')
BEGIN
CREATE TABLE [dbo].[DocMethodGroup](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[DocClassID] [smallint] NOT NULL,
	[DocMethodGroupHeadingID] [smallint] NULL,
	[HeadingName] [nvarchar](60) NOT NULL,
	[HeadingTextCustom] [nvarchar](100) NULL,
	[Sequence] [smallint] NOT NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocMethodGroup]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_DocClass]
	FOREIGN KEY ([DocClassID])
	REFERENCES [dbo].[DocClass]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT [FK_DocMethodGroupHeading]
	FOREIGN KEY ([DocMethodGroupHeadingID])
	REFERENCES [dbo].[DocMethodGroupHeading]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO

/* 8 Method for Class Page*/
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DocMethod')
BEGIN
CREATE TABLE [dbo].[DocMethod](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[DocClassID] [smallint] NOT NULL,
	[DocMethodGroupID] [smallint] NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Sequence] [smallint] NOT NULL,
	[ActiveFlag] [bit] NOT NULL default(1),
	CONSTRAINT [PK_DocMethod]
	PRIMARY KEY CLUSTERED (
	  [ID] ASC),
	CONSTRAINT [FK_DocMethodGroup]
	FOREIGN KEY ([DocMethodGroupID])
	REFERENCES [dbo].[DocMethodGroup]([ID])
	ON DELETE NO ACTION ON UPDATE NO ACTION
)
END
GO
