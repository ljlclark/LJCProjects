/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataColumn.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataColumn]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataColumn];
GO
CREATE PROCEDURE [dbo].[sp_DataColumn]
AS
BEGIN

/* Create Table */
IF OBJECT_ID('DataColumn', N'u')
 IS NULL
BEGIN
CREATE TABLE[dbo].[DataColumn](
  [ID] [bigint] IDENTITY(1, 1) NOT NULL,
  [DataTableID] [bigint] NOT NULL,
  [Name] [nvarchar](60) NOT NULL,
  [Description] [nvarchar](80) NULL,
  [Sequence] [int] NOT NULL DEFAULT 0,
  [TypeName] [nvarchar](20) NOT NULL,
  [MaxLength] [smallint] NOT NULL DEFAULT -1,
  [AllowNull] [bit] NOT NULL DEFAULT 0,
  [DefaultValue] [nvarchar](80) NULL,
  [IdentityStart] [smallint] NOT NULL DEFAULT -1,
  [IdentityIncrement] [smallint] NOT NULL DEFAULT -1,
  [NewName] [nvarchar](60) NULL,
  [NewMaxLength] [smallint] NOT NULL DEFAULT -1
  )
END

IF OBJECT_ID('pk_DataColumn', N'pk')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataColumn]
  ADD CONSTRAINT[pk_DataColumn]
  PRIMARY KEY CLUSTERED
  (
   [ID] ASC
  )
END

IF OBJECT_ID('uq_DataColumn', N'uq')
 IS NULL
BEGIN
 ALTER TABLE[dbo].[DataColumn]
  ADD CONSTRAINT[uq_DataColumn]
  UNIQUE(Name);
END
END