/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 15DocMethodGroup.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select DocMethodGroup.ID 'DocMethodGroup', Name 'Class Name', HeadingName,
  HeadingTextCustom, DocMethodGroup.Sequence, DocMethodGroup.ActiveFlag
from DocClassGroup
left join DocClass on DocClassID = DocClass.ID
order by DocClassID, DocMethodGroup.Sequence;
*/

declare @className nvarchar(60);

set @className= 'DataAccess';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'NonQuery',
 'Insert Update and Delete',  2
exec sp_DMGAddUnique @className, 'Select',
 'Select Methods',  3
exec sp_DMGAddUnique @className, 'Script',
 'Script Methods',  4
exec sp_DMGAddUnique @className, 'StoredProcedure',
 'Stored Procedure',  5

set @className= 'ProcedureParameters';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  2

set @className= 'DataManager';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'DataAccess',
 'Data Access Methods',  2
exec sp_DMGAddUnique @className, 'OtherData',
 '',  3

set @className= 'DbCondition';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'DbConditions';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'DbConditionSet';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'DbFilter';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'DbFilters';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'DbJoin';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'DbJoinOn';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'DbJoinOns';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'DbJoins';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'DbRequest';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Data',
 '',  3

set @className= 'DbResult';
exec sp_DMGAddUnique @className, 'Static',
 '',  7
exec sp_DMGAddUnique @className, 'Constructor',
 '',  8
exec sp_DMGAddUnique @className, 'Collection',
 '',  9

set @className= 'DbRow';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3

set @className= 'DbRows';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3

set @className= 'DbColumn';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Data',
 '',  3

set @className= 'LJCAssemblyReflect';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'SetReflectionObjects',
 'Set Reflection Objects',  2
exec sp_DMGAddUnique @className, 'GetSyntax',
 'Get Syntax Methods',  3
exec sp_DMGAddUnique @className, 'BoolCheckMethods',
 'Bool Check Methods',  4

set @className= 'LJCReflect';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  2
exec sp_DMGAddUnique @className, 'Value',
 '',  3
exec sp_DMGAddUnique @className, 'SetMethods',
 'Set Methods',  4

set @className= 'NetCommon';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'TextTransform',
 'Text Transform Functions',  2
exec sp_DMGAddUnique @className, 'Serialize',
 'Serialization Functions',  3
exec sp_DMGAddUnique @className, 'Config',
 'Program Config Value Functions',  4
exec sp_DMGAddUnique @className, 'Value',
 '',  5

set @className= 'NetString';
exec sp_DMGAddUnique @className, 'CheckValues',
 'Check Values',  1
exec sp_DMGAddUnique @className, 'Formatting',
 'Formatting',  2
exec sp_DMGAddUnique @className, 'Parsing',
 'Parsing',  3
exec sp_DMGAddUnique @className, 'Soundex',
 'Soundex',  4

set @className= 'CommonDataTypes';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'CommonKeywords';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'CommonModifiers';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'Keywords';
exec sp_DMGAddUnique @className, 'Static',
 '',  5
exec sp_DMGAddUnique @className, 'Constructor',
 '',  6
exec sp_DMGAddUnique @className, 'Collection',
 '',  7
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  8

set @className= 'LibTypes';
exec sp_DMGAddUnique @className, 'Static',
 '',  5
exec sp_DMGAddUnique @className, 'Constructor',
 '',  6
exec sp_DMGAddUnique @className, 'Collection',
 '',  7
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  8

set @className= 'Modifiers';
exec sp_DMGAddUnique @className, 'Static',
 '',  5
exec sp_DMGAddUnique @className, 'Constructor',
 '',  6
exec sp_DMGAddUnique @className, 'Collection',
 '',  7
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  8

set @className= 'PropertyDelegates';
exec sp_DMGAddUnique @className, 'Collection',
 '',  5
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  6

set @className= 'RefTypes';
exec sp_DMGAddUnique @className, 'Static',
 '',  2
exec sp_DMGAddUnique @className, 'Constructor',
 '',  3
exec sp_DMGAddUnique @className, 'Collection',
 '',  4
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  5

set @className= 'LJCItem';
exec sp_DMGAddUnique @className, 'Data',
 '',  1

set @className= 'LJCItemCombo';
exec sp_DMGAddUnique @className, 'Data',
 '',  2
exec sp_DMGAddUnique @className, 'Constructor',
 '',  3

set @className= 'LJCDataGrid';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'RowData',
 'Row Data Methods',  2
exec sp_DMGAddUnique @className, 'ColumnData',
 'Column Data Methods',  3
exec sp_DMGAddUnique @className, 'RowSet',
 'Row Set Methods',  4
exec sp_DMGAddUnique @className, 'RowSelection',
 'Row Selection Changed',  5
exec sp_DMGAddUnique @className, 'GridConfig',
 'Grid Configuration',  6

set @className= 'LJCGridRow';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Value',
 '',  2

set @className= 'LJCTabControl';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

set @className= 'LJCTabPanel';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'EventHandlers',
 'Event Handlers',  2

set @className= 'PanelControlsAdjust';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1

set @className= 'ControlValue';
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'ControlValues';
exec sp_DMGAddUnique @className, 'Value',
 '',  4
exec sp_DMGAddUnique @className, 'Constructor',
 '',  4
exec sp_DMGAddUnique @className, 'Collection',
 '',  5
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  6

set @className= 'ModuleReference';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  8
exec sp_DMGAddUnique @className, 'Collection',
 '',  9
exec sp_DMGAddUnique @className, 'GetReference',
 'Get Object Reference',  10

set @className= 'FormCommon';
exec sp_DMGAddUnique @className, 'General',
 'General Functions',  2
exec sp_DMGAddUnique @className, 'ActionState',
 'Action State Functions',  2
exec sp_DMGAddUnique @className, 'Error',
 'Error Functions',  3
exec sp_DMGAddUnique @className, 'KeyHandler',
 'Key Handler Functions',  4
exec sp_DMGAddUnique @className, 'File',
 'File Functions',  5
exec sp_DMGAddUnique @className, 'Image',
 'Image Functions',  6
exec sp_DMGAddUnique @className, 'ScreenPoint',
 'ScreenPoint Functions',  7

set @className= 'ModuleNameComparer';
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  1

set @className= 'TableData';
exec sp_DMGAddUnique @className, 'Collection',
 '',  2

set @className= 'Directionals';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'PrimaryRoads';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'RoadLookups';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Collection',
 '',  3
exec sp_DMGAddUnique @className, 'SearchSort',
 '',  4

set @className= 'StateLookups';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2

set @className= 'Suffixes';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
