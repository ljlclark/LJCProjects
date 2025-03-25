/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataColumnAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataColumnAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataColumnAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataColumnAdd]
  @dataUtilTableName nvarchar(60),
  @name nvarchar(60),
  @description nvarchar(80),
  @sequence int,
  @typeName nvarchar(20),
  @maxLength smallint,
  @allowNull bit,
  @defaultValue nvarchar(80),
  @identityStart smallint,
  @identityIncrement smallint,
  @newName nvarchar(60),
  @newMaxLength smallint
AS
BEGIN
DECLARE @dataUtilTableID int = (SELECT ID FROM DataUtilTable
 WHERE Name = @dataUtilTableName);

IF @dataUtilTableID IS NOT NULL
IF NOT EXISTS(SELECT ID FROM DataColumn
 WHERE Name = @name)
  INSERT INTO DataColumn
    (DataTableID, Name, Description, Sequence, TypeName, MaxLength, AllowNull
     , DefaultValue, IdentityStart, IdentityIncrement, NewName, NewMaxLength)
    VALUES(@dataUtilTableID, @name, @description, @sequence, @typeName, @maxLength
     , @allowNull, @defaultValue, @identityStart, @identityIncrement, @newName
     , @newMaxLength);
END