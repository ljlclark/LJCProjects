/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataModuleData.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataModuleData]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataModuleData];
GO
CREATE PROCEDURE [dbo].[sp_DataModuleData]
AS
BEGIN
EXEC sp_DataModuleAdd
 'DataUtility', 'DataUtility Module'
EXEC sp_DataModuleAdd
 'Region', 'Region Module'
EXEC sp_DataModuleAdd
 'Facility', 'Facility  Module'
EXEC sp_DataModuleAdd
 'Views', 'Views Module'
EXEC sp_DataModuleAdd
 'DataTransform', 'Data Transform'
EXEC sp_DataModuleAdd
 'Geneaology', 'Genealogy'
EXEC sp_DataModuleAdd
 'AppManager', 'AppManager'
EXEC sp_DataModuleAdd
 'DocApp', 'A Document Management Utility'
EXEC sp_DataModuleAdd
 'GenDoc', 'Generate HTML Documentation'
EXEC sp_DataModuleAdd
 'DataDetail', 'The Dynamic Data Detail dialog.'
EXEC sp_DataModuleAdd
 'UnitMeasure', 'Unit Measure App'
END