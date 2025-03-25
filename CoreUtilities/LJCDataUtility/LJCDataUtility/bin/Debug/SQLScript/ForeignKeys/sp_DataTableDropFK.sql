/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataTableDropFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataTableDropFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataTableDropFK];
GO
CREATE PROCEDURE [dbo].[sp_DataTableDropFK]
AS
BEGIN
IF OBJECT_ID('fk_DataTableDataModule', N'f')
 IS NOT NULL
ALTER TABLE[dbo].[DataTable]
  DROP CONSTRAINT[fk_DataTableDataModule]
END