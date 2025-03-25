/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataEntry.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataEntry]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataEntry];
GO
CREATE PROCEDURE [dbo].[sp_DataEntry]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataEntry', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataEntry](
  [ID] [bigint] IDENTITY(1, 1) NOT NULL,
  [SourceSiteID] [bigint] NOT NULL,
  [EntryTime] [datetime] NOT NULL,
  [ModuleID] [bigint] NOT NULL,
  [TableID] [bigint] NOT NULL,
  [EntryType] [varchar](10) NOT NULL,
  [DataConfigName] [varchar](60) NOT NULL,
  [PublishTime] [datetime] NOT NULL,
  [EntryData] [varchar](4000) NOT NULL
  )
END

IF OBJECT_ID('pk_DataEntry', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataEntry]
  ADD CONSTRAINT[pk_DataEntry]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataEntry', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataEntry]
  ADD CONSTRAINT[uq_DataEntry]
  UNIQUE(Name);
END
END