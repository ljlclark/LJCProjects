/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataKeyData.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataKeyData]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataKeyData];
GO
CREATE PROCEDURE [dbo].[sp_DataKeyData]
AS
BEGIN
EXEC sp_DataKeyAdd DataModule
 , 'pk_DataModule', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DataTable
 , 'pk_DataTable', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DataTable
 , 'fk_DataTableDataModule', 3
 , DataModuleID, DataModule, ID, False, True
EXEC sp_DataKeyAdd DataColumn
 , 'pk_DataColumn', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DataColumn
 , 'fk_DataColumnDataTable', 3
 , DataTableID, DataTable, ID, False, False
EXEC sp_DataKeyAdd DataKey
 , 'pk_DataKey', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DataKey
 , 'fk_DataKeyDataTable', 3
 , DataTableID, DataTable, ID, False, False
EXEC sp_DataKeyAdd DataColumn
 , 'uq_DataColumn', 2
 , DataTableID, Name, , , False, False
EXEC sp_DataKeyAdd DataKey
 , 'uq_DataKey', 2
 , DataTableID, Name, , , False, False
EXEC sp_DataKeyAdd DataTable
 , 'uq_DataTable', 2
 , Name, , , False, False
EXEC sp_DataKeyAdd DataModule
 , 'uq_DataModule', 2
 , Name, , , False, False
EXEC sp_DataKeyAdd DataInstance
 , 'pk_DataInstance', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd Individual
 , 'pk_Individual', 1
 , ID, DBID, , , True, True
EXEC sp_DataKeyAdd Individual
 , 'fk_Individual1', 3
 , Parent1ID, Parent1DBID, Individual, ID, DBID, False, False
EXEC sp_DataKeyAdd Individual
 , 'fk_Individual2', 3
 , Parent2ID, Parent2DBID, Individual, ID, DBID, False, False
EXEC sp_DataKeyAdd Partner
 , 'fk_Partner1Individual', 3
 , Partner1ID, Partner1DBID, Individual, ID, DBID, False, False
EXEC sp_DataKeyAdd Partner
 , 'fk_Partner2Individual', 3
 , Partner2ID, Partner2DBID, Individual, ID, DBID, False, False
EXEC sp_DataKeyAdd AppManagerUser
 , 'PK_Person', 1
 , Id, , , True, True
EXEC sp_DataKeyAdd AppProgram
 , 'PK_AppProgram', 1
 , Id, , , True, True
EXEC sp_DataKeyAdd AppModule
 , 'PK_AppModule', 1
 , Id, , , True, True
EXEC sp_DataKeyAdd AppModule
 , 'FK_AppModuleAppProgram', 3
 , AppProgram_ID, AppProgram, Id, False, False
EXEC sp_DataKeyAdd UserAppProgram
 , 'PK_UserAppProgram', 1
 , AppManagerUser_Id, AppProgram_Id, , , True, True
EXEC sp_DataKeyAdd UserAppProgram
 , 'FK_UserAppProgramUser', 3
 , AppManagerUser_Id, AppManagerUser, Id, False, False
EXEC sp_DataKeyAdd UserAppProgram
 , 'FK_UserAppProgramProgram', 3
 , AppProgram_Id, AppProgram, Id, False, False
EXEC sp_DataKeyAdd UserAppModule
 , 'PK_UserAppModule', 1
 , AppManagerUser_Id, AppProgram_Id, AppModule_Id, , , True, True
EXEC sp_DataKeyAdd UserAppModule
 , 'FK_UserAppModuleUser', 3
 , AppManagerUser_Id, AppManagerUser, Id, False, False
EXEC sp_DataKeyAdd UserAppModule
 , 'FK_UserAppModuleProgram', 3
 , AppProgram_Id, AppProgram, Id, False, False
EXEC sp_DataKeyAdd UserAppModule
 , 'FK_UserAppModuleModule', 3
 , AppModule_Id, AppModule, Id, False, False
EXEC sp_DataKeyAdd LandTitleValue
 , 'PK_LandTitleValue', 1
 , ID, , , False, False
EXEC sp_DataKeyAdd DocApp
 , 'PK_DocApp', 1
 , ID, , , False, False
EXEC sp_DataKeyAdd DocAppFile
 , 'PK_DocAppFile', 1
 , ID, , , False, False
EXEC sp_DataKeyAdd DocAppFile
 , 'FK_DocAppFile_DocApp', 3
 , DocAppID, DocApp, ID, False, False
EXEC sp_DataKeyAdd DocAppFieldBehavior
 , 'PK_DocAppFieldBehavior', 1
 , ID, , , False, False
EXEC sp_DataKeyAdd DocAppField
 , 'PK_DocAppField', 1
 , ID, , , False, False
EXEC sp_DataKeyAdd DocAppField
 , 'FK_DocAppField_DocApp', 3
 , DocAppID, DocApp, ID, False, False
EXEC sp_DataKeyAdd DocAppField
 , 'FK_DocAppField_DocAppFieldBehavior', 3
 , DocAppFieldBehaviorID, DocAppFieldBehavior, ID, False, False
EXEC sp_DataKeyAdd LandTitleValue
 , 'FK_LandTitleValue_DocAppFile', 3
 , DocAppFileID, DocAppFile, ID, False, False
EXEC sp_DataKeyAdd DocAssemblyGroup
 , 'PK_DocAssemblyGroup', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocAssembly
 , 'PK_DocAssembly', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocAssembly
 , 'FK_DocAssemblyGroup', 3
 , DocAssemblyGroupID, DocAssemblyGroup, ID, False, False
EXEC sp_DataKeyAdd DocClassGroupHeading
 , 'PK_DocClassGroupHeading', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocClassGroup
 , 'PK_DocClassGroup', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocClassGroup
 , 'FK_DocAssembly', 3
 , AssemblyID, Assembly, ID, False, False
EXEC sp_DataKeyAdd DocClassGroup
 , 'FK_DocClassGroupHeadingID', 3
 , DocClassGroupHeadingID, DocClassGroupHeading, ID, False, False
EXEC sp_DataKeyAdd DocClass
 , 'PK_DocClass', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocClass
 , 'FK_DocClassGrouup', 3
 , DocClassGroupID, DocClassGroup, ID, False, False
EXEC sp_DataKeyAdd DocMethodGroupHeading
 , 'PK_DocMethodGroupHeading', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocMethodGroup
 , 'PK_DocMethodGroup', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocMethodGroup
 , 'FK_DocClass', 3
 , DocClassID, DocClass, ID, False, False
EXEC sp_DataKeyAdd DocMethodGroup
 , 'FK_DocMethodGroupHeading', 3
 , DocMethodGroupHeadingID, DocMethodGroupHeading, ID, False, False
EXEC sp_DataKeyAdd DocMethod
 , 'PK_DocMethod', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd DocMethod
 , 'FK_DocMethodGroup', 3
 , DocMethodGroupID, DocMethodGroup, ID, False, False
EXEC sp_DataKeyAdd ControlDetail
 , 'PKDetailDialog', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd ControlDetail
 , 'UKDetailDialog', 2
 , Name, , , False, False
EXEC sp_DataKeyAdd ControlData
 , 'PKControlData', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd ControlData
 , 'UKControlData', 2
 , ControlDetailID, PropertyName, , , False, False
EXEC sp_DataKeyAdd ControlData
 , 'FKControlData', 3
 , ControlDetailID, ControlDetail, ID, False, False
EXEC sp_DataKeyAdd ControlTab
 , 'PKControlTab', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd ControlTab
 , 'UKControlTab', 2
 , ControlDetailID, TabIndex, , , False, False
EXEC sp_DataKeyAdd ControlTab
 , 'FKControlTab', 3
 , ControlDetailID, ControlDetail, ID, False, False
EXEC sp_DataKeyAdd ControlColumn
 , 'UKControlColumn', 2
 , ControlTabID, ColumnIndex, , , False, False
EXEC sp_DataKeyAdd ControlColumn
 , 'FKControlColumn', 3
 , ControlTabID, ControlTab, ID, False, False
EXEC sp_DataKeyAdd ControlRow
 , 'PKControlRow', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd ControlRow
 , 'UKControlRow', 2
 , ControlColumnID, DataValueName, , , False, False
EXEC sp_DataKeyAdd ControlRow
 , 'FKControlRow', 3
 , ControlColumnID, ControlColumn, ID, False, False
EXEC sp_DataKeyAdd UnitCategory
 , 'PK_UnitCategry', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd UnitMeasure
 , 'PK_UnitMeasure', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd UnitConversion
 , 'PK_UnitConversion', 1
 , ID, , , True, True
EXEC sp_DataKeyAdd ProcessGroup
 , 'PK_ProcessGroup', 1
 , ProcessGroupID, , , True, True
EXEC sp_DataKeyAdd ProcessStatus
 , 'PK_ProcessStatus', 1
 , ProcessStatusID, , , True, True
EXEC sp_DataKeyAdd DataProcess
 , 'PK_Process', 1
 , DataProcessID, , , True, True
EXEC sp_DataKeyAdd DataProcess
 , 'FK_DataProcess_ProcessStatus', 3
 , ProcessStatusID, ProcessStatus, ProcessStatusID, False, False
EXEC sp_DataKeyAdd ProcessGroupProcess
 , 'FK_ProcessGroupProcess_ProcessGroup', 3
 , ProcessGroupID, ProcessGroup, ProcessGroupID, False, False
EXEC sp_DataKeyAdd ProcessGroupProcess
 , 'FK_ProcessGroupProcess_DataProcess', 3
 , DataProcessID, DataProcess, DataProcessID, False, False
EXEC sp_DataKeyAdd TaskStatus
 , 'PK_TaskStatus', 1
 , TaskStatusID, , , True, True
EXEC sp_DataKeyAdd Step
 , 'PK_Step', 1
 , StepID, , , True, True
EXEC sp_DataKeyAdd Step
 , 'FK_Step_DataProcess', 3
 , DataProcessID, DataProcess, DataProcessID, False, False
EXEC sp_DataKeyAdd Step
 , 'FK_Step_TaskStatus', 3
 , StatusID, TaskStatus, TaskStatusID, False, False
EXEC sp_DataKeyAdd StepTask
 , 'PK_Task', 1
 , StepTaskID, , , True, True
EXEC sp_DataKeyAdd StepTask
 , 'FK_StepTask_Step', 3
 , StepID, Step, StepID, False, False
EXEC sp_DataKeyAdd StepTask
 , 'FK_StepTask_TaskType', 3
 , TaskTypeID, TaskType, TaskTypeID, False, False
EXEC sp_DataKeyAdd StepTask
 , 'FK_StepTask_TaskStatus', 3
 , TaskStatusID, TaskStatus, TaskStatusID, False, False
EXEC sp_DataKeyAdd LayoutColumn
 , 'PK_LayoutColumn', 1
 , LayoutColumnID, , , True, True
EXEC sp_DataKeyAdd LayoutColumn
 , 'FK_LayoutColumn_SourceLayout', 3
 , SourceLayoutID, SourceLayout, SourceLayoutID, False, False
EXEC sp_DataKeyAdd LayoutColumn
 , 'FK_LayoutColumn_DataType', 3
 , DataTypeID, DataType, DataTypeID, False, False
EXEC sp_DataKeyAdd SourceType
 , 'PK_SourceType', 1
 , SourceTypeID, , , True, True
EXEC sp_DataKeyAdd SourceStatus
 , 'PK_SourceStatus', 1
 , SourceStatusID, , , True, True
EXEC sp_DataKeyAdd DataSource
 , 'PK_DataSource', 1
 , DataSourceID, , , True, True
EXEC sp_DataKeyAdd DataSource
 , 'FK_DataSource_SourceType', 3
 , SourceTypeID, SourceType, SourceTypeID, False, False
EXEC sp_DataKeyAdd DataSource
 , 'FK_DataSource_SourceLayout', 3
 , SourceLayoutID, SourceLayout, SourceLayoutID, False, False
EXEC sp_DataKeyAdd DataSource
 , 'FK_DataSource_SourceStatus', 3
 , SourceStatusID, SourceStatus, SourceStatusID, False, False
EXEC sp_DataKeyAdd TaskSource
 , 'FK_TaskSource_StepTask', 3
 , StepTaskID, StepTask, StepTaskID, False, False
EXEC sp_DataKeyAdd TaskSource
 , 'FK_TaskSource_DataSource', 3
 , DataSourceID, DataSource, DataSourceID, False, False
EXEC sp_DataKeyAdd TaskTransform
 , 'PK_TaskTransform', 1
 , TransformID, , , True, True
EXEC sp_DataKeyAdd TaskTransform
 , 'FK_TaskTransform_StepTask', 3
 , StepTaskID, StepTask, StepTaskID, False, False
EXEC sp_DataKeyAdd TaskTransform
 , 'FK_TaskTransform_DataSource', 3
 , DataSourceID, DataSource, DataSourceID, False, False
EXEC sp_DataKeyAdd TaskTransform
 , 'FK_TaskTransform_DataSource1', 3
 , TargetID, DataSource, DataSourceID, False, False
EXEC sp_DataKeyAdd TransformMatch
 , 'PK_TransformMatch', 1
 , TransformMatchID, , , True, True
EXEC sp_DataKeyAdd TransformMatch
 , 'FK_TransformMatch_ransform', 3
 , TransformID, TaskTransform, TransformID, False, False
EXEC sp_DataKeyAdd TransformMatch
 , 'FK_TransformMatch_LlayoutColumn', 3
 , SourceColumnID, LayoutColumn, LayoutColumnID, False, False
EXEC sp_DataKeyAdd TransformMatch
 , 'FK_TransformMatch_Layoutolumn1', 3
 , TargetColumnID, LayoutColumn, LayoutColumnID, False, False
EXEC sp_DataKeyAdd MapType
 , 'PK_MapType', 1
 , MapTypeID, , , True, True
EXEC sp_DataKeyAdd TransformMap
 , 'PK_TransformMap', 1
 , TransformMapID, , , True, True
EXEC sp_DataKeyAdd TransformMap
 , 'FK_TransformMap_Transform', 3
 , TransformID, TaskTransform, TransformID, False, False
EXEC sp_DataKeyAdd TransformMap
 , 'FK_TransformMap_Layoutolumn1', 3
 , SourceColumnID, LayoutColumn, LayoutColumnID, False, False
EXEC sp_DataKeyAdd TransformMap
 , 'FK_TransformMap_LayoutColumn1', 3
 , TargetColumnID, LayoutColumn, LayoutColumnID, False, False
EXEC sp_DataKeyAdd TransformMap
 , 'FK_TransformMap_MapType', 3
 , MapTypeID, MapType, MapTypeID, False, False
EXEC sp_DataKeyAdd TaskType
 , 'PK_TaskType', 1
 , TaskTypeID, , , True, True
EXEC sp_DataKeyAdd SourceLayout
 , 'PK_Layout', 1
 , SourceLayoutID, , , True, True
EXEC sp_DataKeyAdd DataType
 , 'PK_DataType', 1
 , DataTypeID, , , True, True
END