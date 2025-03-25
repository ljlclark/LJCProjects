/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataTable.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataTable]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataTable];
GO
CREATE PROCEDURE [dbo].[sp_DataTable]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataTable', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataTable](
  [ID] [int] IDENTITY(1, 1) NOT NULL,
  [DataModuleID] [int] NOT NULL,
  [Name] [nvarchar](60) NOT NULL,
  [Description] [nvarchar](80) NULL,
  [Sequence] [int] NOT NULL DEFAULT 0,
  [NewName] [nvarchar](60) NULL
  )
END

IF OBJECT_ID('pk_DataTable', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataTable]
  ADD CONSTRAINT[pk_DataTable]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataTable', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataTable]
  ADD CONSTRAINT[uq_DataTable]
  UNIQUE(Name);
END
END