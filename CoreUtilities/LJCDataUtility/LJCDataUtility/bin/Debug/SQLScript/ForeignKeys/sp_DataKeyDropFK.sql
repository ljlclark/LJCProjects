/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataKeyDropFK.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataKeyDropFK]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataKeyDropFK];
GO
CREATE PROCEDURE [dbo].[sp_DataKeyDropFK]
AS
BEGIN
IF OBJECT_ID('fk_DataKeyDataTable', N'f')
 IS NOT NULL
ALTER TABLE[dbo].[DataKey]
  DROP CONSTRAINT[fk_DataKeyDataTable]
END