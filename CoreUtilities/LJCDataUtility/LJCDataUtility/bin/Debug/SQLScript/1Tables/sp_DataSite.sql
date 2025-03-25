/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataSite.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataSite]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataSite];
GO
CREATE PROCEDURE [dbo].[sp_DataSite]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataSite', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataSite](
  [ID] [bigint] IDENTITY(1, 1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](80) NULL,
  [SiteURL] [varchar](100) NOT NULL
  )
END

IF OBJECT_ID('pk_DataSite', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataSite]
  ADD CONSTRAINT[pk_DataSite]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataSite', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataSite]
  ADD CONSTRAINT[uq_DataSite]
  UNIQUE(Name);
END
END