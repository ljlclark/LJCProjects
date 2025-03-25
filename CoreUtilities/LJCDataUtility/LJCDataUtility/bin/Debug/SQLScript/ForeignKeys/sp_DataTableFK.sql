/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataTableFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataTableFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataTableFK];
GO
CREATE PROCEDURE [dbo].[sp_DataTableFK]
AS
BEGIN
IF OBJECT_ID('fk_DataTableDataModule', N'f')
 IS NULL
BEGIN
  ALTER TABLE[dbo].[DataTable]
  ADD CONSTRAINT[fk_DataTableDataModule]
  FOREIGN KEY([DataModuleID])
  REFERENCES[dbo].[DataModule]([ID])
   ON DELETE NO ACTION ON UPDATE NO ACTION;
END
END