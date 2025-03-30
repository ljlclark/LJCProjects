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
  /* foreignUniqueParams - targetTableName+targetUniqueColumnName */
  @dataModuleName nvarchar(60),
	/* sourceUniqueParams */
  @name nvarchar(60),
	/* Other column params. */
  @description nvarchar(80),
  @sequence int,
  @newName nvarchar(60),
  @schemaName nvarchar(20)
AS
BEGIN
/* foreignKeyVars - @targetTableName+targetPrimaryColumnName */
DECLARE @dataModuleID int = (SELECT ID FROM DataModule
 WHERE Name = @dataModuleName);
/*
SELECT
  @dataModuleID = ID,
	@dataModuleDBID = DBID
	FROM dataModule
	WHERE Name = @dataModuleName;
*/
/* foreignKeyVars */
IF @dataModuleID IS NOT NULL
IF NOT EXISTS(SELECT ID FROM DataTable
  /* sourceUniqueParams */
  WHERE Name = @name)
    INSERT INTO DataTable
      (DataModuleID, Name, Description, Sequence, NewName, SchemaName)
		  /* foreignKeyVars */
      VALUES(@dataModuleID, @name, @description, @sequence, @newName, @schemaName);
END