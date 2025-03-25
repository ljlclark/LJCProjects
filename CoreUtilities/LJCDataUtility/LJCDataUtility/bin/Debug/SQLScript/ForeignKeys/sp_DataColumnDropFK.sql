/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataColumnDropFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataColumnDropFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataColumnDropFK];
GO
CREATE PROCEDURE [dbo].[sp_DataColumnDropFK]
AS
BEGIN
IF OBJECT_ID('fk_DataColumnDataTable', N'f')
 IS NOT NULL
ALTER TABLE[dbo].[DataColumn]
  DROP CONSTRAINT[fk_DataColumnDataTable]
END