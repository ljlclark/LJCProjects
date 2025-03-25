/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataModuleAdd.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataModuleAdd]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataModuleAdd];
GO
CREATE PROCEDURE [dbo].[sp_DataModuleAdd]
  @name nvarchar(60),
  @description nvarchar(80)
AS
BEGIN
IF NOT EXISTS(SELECT ID FROM DataModule
 WHERE Name = @name)
  INSERT INTO DataModule
    (Name, Description)
    VALUES(@name, @description);
END