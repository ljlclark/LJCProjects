/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataModule.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataModule]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataModule];
GO
CREATE PROCEDURE [dbo].[sp_DataModule]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataModule', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataModule](
  [ID] [bigint] IDENTITY(1, 1) NOT NULL,
  [Name] [nvarchar](60) NOT NULL,
  [Description] [nvarchar](80) NULL
  )
END

IF OBJECT_ID('pk_DataModule', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataModule]
  ADD CONSTRAINT[pk_DataModule]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataModule', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataModule]
  ADD CONSTRAINT[uq_DataModule]
  UNIQUE(Name);
END
END