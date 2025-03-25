/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataKeyAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataKeyAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataKeyAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataKeyAdd]
  @dataUtilTableName nvarchar(60),
  @name nvarchar(60),
  @keyType smallint,
  @sourceColumnName nvarchar(60),
  @targetTableName nvarchar(60),
  @targetColumnName nvarchar(60),
  @isClustered bit,
  @isAscending bit
AS
BEGIN
DECLARE @dataUtilTableID int = (SELECT ID FROM DataUtilTable
 WHERE Name = @dataUtilTableName);

IF @dataUtilTableID IS NOT NULL
IF NOT EXISTS(SELECT ID FROM DataKey
 WHERE Name = @name)
  INSERT INTO DataKey
    (DataTableID, Name, KeyType, SourceColumnName, TargetTableName
     , TargetColumnName, IsClustered, IsAscending)
    VALUES(@dataUtilTableID, @name, @keyType, @sourceColumnName, @targetTableName
     , @targetColumnName, @isClustered, @isAscending);
END