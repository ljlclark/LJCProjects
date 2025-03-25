/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataTableData.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataTableData]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataTableData];
GO
CREATE PROCEDURE [dbo].[sp_DataTableData]
AS
BEGIN
EXEC sp_DataTableAdd DataUtility
 , 'DataModule', 'The Module Definition Table'
EXEC sp_DataTableAdd DataUtility
 , 'DataTable', 'The Table Definition Table'
EXEC sp_DataTableAdd DataUtility
 , 'DataColumn', 'The Column Definition Table'
EXEC sp_DataTableAdd DataUtility
 , 'DataKey', 'The Foreign Key Definition Table'
EXEC sp_DataTableAdd Region
 , 'City', 'City'
EXEC sp_DataTableAdd Region
 , 'CitySection', 'CitySection'
EXEC sp_DataTableAdd Region
 , 'Region', 'Region'
EXEC sp_DataTableAdd Region
 , 'Province', 'Province'
EXEC sp_DataTableAdd Facility
 , 'Equipment', 'Equipment'
EXEC sp_DataTableAdd Facility
 , 'Facility', 'Facility'
EXEC sp_DataTableAdd Facility
 , 'Fixture', 'Fixture'
EXEC sp_DataTableAdd Facility
 , 'Unit', 'Unit'
EXEC sp_DataTableAdd Facility
 , 'Business', 'Business'
EXEC sp_DataTableAdd Facility
 , 'BusinessAddress', 'BusinessAddress'
EXEC sp_DataTableAdd Facility
 , 'Address', 'Address'
EXEC sp_DataTableAdd Facility
 , 'CodeType', 'CodeType'
EXEC sp_DataTableAdd Facility
 , 'CodeTypeClass', 'CodeTypeClass'
EXEC sp_DataTableAdd Facility
 , 'Person', 'Person'
EXEC sp_DataTableAdd Views
 , 'ViewColumn', 'ViewColumn'
EXEC sp_DataTableAdd Views
 , 'ViewCondition', 'ViewCondition'
EXEC sp_DataTableAdd Views
 , 'ViewConditionSet', 'ViewConditionSet'
EXEC sp_DataTableAdd Views
 , 'ViewData', 'ViewData'
EXEC sp_DataTableAdd Views
 , 'ViewFilter', 'ViewFilter'
EXEC sp_DataTableAdd Views
 , 'ViewGridColumn', 'ViewGridColumn'
EXEC sp_DataTableAdd Views
 , 'ViewJoin', 'ViewJoin'
EXEC sp_DataTableAdd Views
 , 'ViewJoinColumn', 'ViewJoinColumn'
EXEC sp_DataTableAdd Views
 , 'ViewJoinOn', 'ViewJoinOn'
EXEC sp_DataTableAdd Views
 , 'ViewOrderBy', 'ViewOrderBy'
EXEC sp_DataTableAdd Views
 , 'ViewTable', 'ViewTable'
EXEC sp_DataTableAdd Facility
 , 'UnitPerson', 'The Persons assigned to a unit.'
EXEC sp_DataTableAdd Facility
 , 'PersonRelation', 'PersonRelation'
EXEC sp_DataTableAdd Facility
 , 'Account', 'Account'
EXEC sp_DataTableAdd Facility
 , 'BusinessPerson', 'BusinessPerson'
EXEC sp_DataTableAdd Facility
 , 'PersonAddress', 'PersonAddress'
EXEC sp_DataTableAdd DataTransform
 , 'DataProcess', 'DataProcess'
EXEC sp_DataTableAdd DataTransform
 , 'DataSource', 'DataSource'
EXEC sp_DataTableAdd DataTransform
 , 'DataType', 'DataType'
EXEC sp_DataTableAdd DataTransform
 , 'LayoutColumn', 'LayoutColumn'
EXEC sp_DataTableAdd DataTransform
 , 'ProcessGroup', 'ProcessGroup'
EXEC sp_DataTableAdd DataTransform
 , 'ProcessGroupProcess', 'ProcessGroupProcess'
EXEC sp_DataTableAdd DataTransform
 , 'ProcessStatus', 'ProcessStatus'
EXEC sp_DataTableAdd DataTransform
 , 'SourceType', 'SourceType'
EXEC sp_DataTableAdd DataTransform
 , 'Step', 'Step'
EXEC sp_DataTableAdd DataTransform
 , 'StepTask', 'StepTask'
EXEC sp_DataTableAdd DataTransform
 , 'TaskSource', 'TaskSource'
EXEC sp_DataTableAdd DataTransform
 , 'TaskStatus', 'TaskStatus'
EXEC sp_DataTableAdd DataTransform
 , 'TaskTransform', 'TaskTransform'
EXEC sp_DataTableAdd DataTransform
 , 'TaskType', 'TaskType'
EXEC sp_DataTableAdd DataTransform
 , 'TransformMap', 'TransformMap'
EXEC sp_DataTableAdd DataTransform
 , 'TransformMatch', 'TransformMatch'
EXEC sp_DataTableAdd DataTransform
 , 'SourceLayout', 'SourceLayout'
EXEC sp_DataTableAdd Geneaology
 , 'DataInstance', 'DataInstance'
EXEC sp_DataTableAdd Geneaology
 , 'Individual', 'Individual'
EXEC sp_DataTableAdd Geneaology
 , 'Partner', 'Partner'
EXEC sp_DataTableAdd AppManager
 , 'AppManagerUser', 'AppManagerUser'
EXEC sp_DataTableAdd AppManager
 , 'AppProgram', 'AppProgram'
EXEC sp_DataTableAdd AppManager
 , 'AppModule', 'AppModule'
EXEC sp_DataTableAdd AppManager
 , 'UserAppProgram', 'UserAppProgram'
EXEC sp_DataTableAdd AppManager
 , 'UserAppModule', 'UserAppModule'
EXEC sp_DataTableAdd DocApp
 , 'DocApp', 'DocApp'
EXEC sp_DataTableAdd DocApp
 , 'DocAppFile', 'DocAppFile'
EXEC sp_DataTableAdd DocApp
 , 'DocAppFieldBehavior', 'DocAppFieldBehavior'
EXEC sp_DataTableAdd DocApp
 , 'DocAppField', 'DocAppField'
EXEC sp_DataTableAdd DocApp
 , 'LandTitleValue', 'LandTitleValue'
EXEC sp_DataTableAdd GenDoc
 , 'DocAssemblyGroup', 'Assembly Group for main HTML page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocAssembly', 'Doc Assembly for main HTML page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocClassGroupHeading', 'Class Group heading for Assembly page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocClassGroup', 'Class Group for Assembly page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocClass', 'Class for Assembly page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocMethodGroupHeading', 'Group heading for Class page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocMethodGroup', 'Method Group for Class page.'
EXEC sp_DataTableAdd GenDoc
 , 'DocMethod', 'DocMethod'
EXEC sp_DataTableAdd DataDetail
 , 'ControlDetail', 'ControlDetail'
EXEC sp_DataTableAdd DataDetail
 , 'ControlData', 'ControlData'
EXEC sp_DataTableAdd DataDetail
 , 'ControlTab', 'ControlTab'
EXEC sp_DataTableAdd DataDetail
 , 'ControlColumn', 'ControlColumn'
EXEC sp_DataTableAdd DataDetail
 , 'ControlRow', 'ControlRow'
EXEC sp_DataTableAdd UnitMeasure
 , 'UnitCategory', 'The Unit Category'
EXEC sp_DataTableAdd UnitMeasure
 , 'UnitSystem', 'The Unit System'
EXEC sp_DataTableAdd UnitMeasure
 , 'UnitMeasure', 'the Unit Measurement'
EXEC sp_DataTableAdd UnitMeasure
 , 'UnitConversion', 'The Unit Conversion'
EXEC sp_DataTableAdd DataTransform
 , 'SourceStatus', 'SourceStatus'
EXEC sp_DataTableAdd DataTransform
 , 'MapType', 'MapType'
END