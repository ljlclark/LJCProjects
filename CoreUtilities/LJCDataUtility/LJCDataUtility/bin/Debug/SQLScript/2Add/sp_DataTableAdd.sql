/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataTableAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataTableAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataTableAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataTableAdd]
  @dataModuleName nvarchar(60),
  @name nvarchar(60),
  @description nvarchar(80),
  @sequence int,
  @newName nvarchar(60),
  @schemaName nvarchar(20)
AS
BEGIN
DECLARE @dataModuleID int = (SELECT ID FROM DataModule
 WHERE Name = @dataModuleName);

IF @dataModuleID IS NOT NULL
IF NOT EXISTS(SELECT ID FROM DataTable
 WHERE Name = @name)
  INSERT INTO DataTable
    (DataModuleID, Name, Description, Sequence, NewName, SchemaName)
    VALUES(@dataModuleID, @name, @description, @sequence, @newName, @schemaName);
END