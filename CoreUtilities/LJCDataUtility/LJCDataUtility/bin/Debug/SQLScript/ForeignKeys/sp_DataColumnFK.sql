/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataColumnFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataColumnFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataColumnFK];
GO
CREATE PROCEDURE [dbo].[sp_DataColumnFK]
AS
BEGIN
IF OBJECT_ID('fk_DataColumnDataTable', N'f')
 IS NULL
BEGIN
  ALTER TABLE[dbo].[DataColumn]
  ADD CONSTRAINT[fk_DataColumnDataTable]
  FOREIGN KEY([DataTableID])
  REFERENCES[dbo].[DataTable]([ID])
   ON DELETE NO ACTION ON UPDATE NO ACTION;
END
END