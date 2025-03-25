/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataKey.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataKey]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataKey];
GO
CREATE PROCEDURE [dbo].[sp_DataKey]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataKey', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataKey](
  [ID] [bigint] IDENTITY(1, 1) NOT NULL,
  [DataTableID] [bigint] NOT NULL,
  [Name] [nvarchar](60) NOT NULL,
  [KeyType] [smallint] NOT NULL,
  [SourceColumnName] [nvarchar](60) NULL,
  [TargetTableName] [nvarchar](60) NULL,
  [TargetColumnName] [nvarchar](60) NULL,
  [IsClustered] [bit] NOT NULL DEFAULT 0,
  [IsAscending] [bit] NOT NULL DEFAULT 0
  )
END

IF OBJECT_ID('pk_DataKey', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataKey]
  ADD CONSTRAINT[pk_DataKey]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataKey', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataKey]
  ADD CONSTRAINT[uq_DataKey]
  UNIQUE(Name);
END
END