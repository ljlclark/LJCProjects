/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataKeyFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataKeyFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataKeyFK];
GO
CREATE PROCEDURE [dbo].[sp_DataKeyFK]
AS
BEGIN
IF OBJECT_ID('fk_DataKeyDataTable', N'f')
 IS NULL
BEGIN
  ALTER TABLE[dbo].[DataKey]
  ADD CONSTRAINT[fk_DataKeyDataTable]
  FOREIGN KEY([DataTableID])
  REFERENCES[dbo].[DataTable]([ID])
   ON DELETE NO ACTION ON UPDATE NO ACTION;
END
END