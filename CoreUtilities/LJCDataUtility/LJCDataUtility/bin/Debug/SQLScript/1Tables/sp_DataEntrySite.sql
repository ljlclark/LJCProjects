/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataEntrySite.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataEntrySite]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataEntrySite];
GO
CREATE PROCEDURE [dbo].[sp_DataEntrySite]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataEntrySite', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataEntrySite](
  [DataSiteID] [bigint] NOT NULL,
  [DataEntryID] [bigint] NOT NULL
  )
END

IF OBJECT_ID('pk_DataEntrySite', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataEntrySite]
  ADD CONSTRAINT[pk_DataEntrySite]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataEntrySite', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataEntrySite]
  ADD CONSTRAINT[uq_DataEntrySite]
  UNIQUE(Name);
END
END