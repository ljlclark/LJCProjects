/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 11DocClass.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select dc.ID 'DocClass', da.Name 'Assembly Name', dcg.HeadingName, dc.Name,
  dc.Description, dc.Sequence
from DocClass as dc
left join DocAssembly as da on DocAssemblyID = da.ID
left join DocClassGroup as dcg on DocClassGroupID = dcg.ID
order by da.Name, HeadingName, Sequence;
*/

declare @assemblyName nvarchar(60);
declare @headingName nvarchar(60);
declare @seq int

/* LJCAddressParserLib */
set @assemblyName = 'LJCAddressParserLib';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Directional',
  'Represents an Address Directional component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Directionals',
  'Represents a collection of Directional objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ErrorMessage',
  'Represents an Address Parsing error.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ErrorMessages',
  'Represents a collection of ErrorMessage objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'PrimaryRoad',
  'Represents a Primary Road.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'PrimaryRoads',
  'Represents a collection of PrimaryRoad objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'RoadLookup',
  'Represents a RoadLookup component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'RoadLookups',
  'Represents a collection of RoadLookup objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'StandardAddress',
  'Provides methods to parse Address information into standardized component      properties. (R)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'State',
  'Represents an Address State component',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'StateLookup',
  'Represents a StateLookup component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'StateLookups',
  'Represents a colletion of StateLookup objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'States',
  'Represents a collection of State objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Suffix',
  'Represents a Suffix component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Suffixes',
  'Represents a colletion of StateLookup objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SuffixLookup',
  'Represents a SuffixLookup component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SuffixLookups',
  'Represents a colletion of SuffixLookup objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Unit',
  'Represents a Unit component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'UnitLookup',
  'Represents a UnitLookup component.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'UnitLookups',
  'Represents a colletion of SuffixLookup objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Units',
  'Represents a colletion of Unit objects.',
  @seq;

set @assemblyName = 'LJCAddressParserLib';
set @headingName = 'Comparer';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'NameComparer',
  'Sort and search on Name value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'RoadLSoundexComparer',
  'Sort and search on Letter Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'RoadPSoundexComparer',
  'Sort and search on Phonetic Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SpanishNameComparer',
  'Sort and search on Spanish Name value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'StateLSoundexComparer',
  'Sort and search on Letter Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'StatePSoundexComparer',
  'Sort and search on Phonetic Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SuffixLSoundexComparer',
  'Sort and search on Letter Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SuffixPSoundexComparer',
  'Sort and search on Phonetic Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'UnitLSoundexComparer',
  'Sort and search on Letter Soundex value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'UnitNameComparer',
  'Sort and search on Name value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'UnitPSoundexComparer',
  'Sort and search on Phonetic Soundex value.',
  @seq;

/* LJCDataAccess */
set @assemblyName = 'LJCDataAccess';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataAccess',
  '',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ProcedureParameters',
  '',
  @seq;

/* LJCDataAccessConfig */
set @assemblyName = 'LJCDataAccessConfig';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ConnectionTemplate',
  'Represents a Connection String template.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ConnectionTemplates',
  'Represents a collection of Connection string templates.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataConfig',
  'Represents a data location configuration.      (R)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataConfigs',
  'Represents a collection of DataConfig objects.',
  @seq;

/* LJCDataDetailDAL */
set @assemblyName = 'LJCDataDetailDAL';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlDetail',
  'The DetailConfig table Data Object.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlDetails',
  'Represents a collection of DetailDialog objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlTab',
  'The ControlTab table Data Object.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlTabItems',
  'Represents a collection of ControlTab objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlColumn',
  'The ControlColumn table Data Object.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlColumns',
  'Represents a collection of ControlColumn objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlRow',
  'The ControlRow table Data Object.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlRows',
  'Represents a collection of ControlRow objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataDetailData',
  'Contains methods for using DataDetail data.',
  @seq;

set @assemblyName = 'LJCDataDetailDAL';
set @headingName = 'Manager';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlDetailManager',
  'Provides table specific data methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlTabManager',
  'Provides table specific data methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlColumnManager',
  'Provides table specific data methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlRowManager',
  'Provides table specific data methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataDetailManagers',
  'Gets the Manager objects.',
  @seq;

/* LJCDBClientLib */
set @assemblyName = 'LJCDBClientLib';
set @headingName = 'DataAccess';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbServiceClient',
  'The Proxy client object.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbServiceRef',
  'Contains the      DbDataAccess,      local DbService and      DbServiceClient      proxy referen',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'IDbService',
  'The Proxy DbService contract.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'IDbServiceChannel',
  'The Proxy ServiceChannel contract.',
  @seq;

set @assemblyName = 'LJCDBClientLib';
set @headingName = 'DataManager';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataManager',
  '',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbManager',
  'Provides DbDataAccess data manipulation methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'IDataManager',
  'Provides standard data manipulation.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ManagerCommon',
  'Contains common static manager methods.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ObjectManager`2',
  'Provides object specific data methods.      (RE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'SQLManager',
  'Provides SQL data manipulation methods.',
  @seq;

/* LJCDBMessage */
set @assemblyName = 'LJCDBMessage';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbCommon',
  'Common data message methods.',
  @seq;

set @assemblyName = 'LJCDBMessage';
set @headingName = 'Request';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbCondition',
  'Represents a filter condition.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbConditions',
  'Represents a collection of DbCondition objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbConditionSet',
  'Represents the conditions and properties. (E)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbFilter',
  'Represents a filter which is part of a where clause.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbFilters',
  'Represents a collection of DbFilter objects. (E)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbJoin',
  'Represents a database table join.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbJoinOn',
  'Represents a Join On definition.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbJoinOns',
  'Represents a collection of join on definitions.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbJoins',
  'Represents a collection of table joins. (E)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbRequest',
  'Represents a database request. (E)',
  @seq;

set @assemblyName = 'LJCDBMessage';
set @headingName = 'Result';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbResult',
  'Represents a Request result. (R)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbRow',
  'Represents a result Row.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbRows',
  'Represents a collection of LJCNetCommon.DbValues.',
  @seq;

/* LJCGridDataLib */
set @assemblyName = 'LJCGridDataLib';
set @headingName = 'DataGrid';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'TableData',
  'Provides DataTable helpers for an LJCDataGrid control.',
  @seq;

/* LJCNetCommon */
set @assemblyName = 'LJCNetCommon';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'AppSettings',
  'Represents the Configuration AppSettings. (RE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'CodeTokenizer',
  'A C# Code Tokenizer class. (RE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Cryptography_Type',
  'The encryption types.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DataTypes',
  'Represents a collection of Data Types.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCCryptography',
  'Provides methods to encrypt and decrypt data in memory.',
  @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbColumn',
  'Represents a Data Column definition. (D)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbColumns',
  'Represents a collection of DbColumn objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbValue',
  'Represents a data source value.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbValues',
  'Represents a collection of DbValue objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'KeyItem',
  'Represents Key item values.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'KeyItems',
  'Represents a collection of KeyItem objects.',
  @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Comparer';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbColumnNameComparer',
  'Sort and search on column name.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbColumnPropertyComparer',
  'Sort and search on PropertyName.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'DbColumnRenameAsComparer',
  'Sort and search on RenameAs value.',
  @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Reflection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCAssemblyReflect',
  'Provides Assembly Reflection methods. (DE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCReflect',
  'Provides object property reflection capabilities. (DE)',
  @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Static';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'NetCommon',
  'Contains common static functions. (RDE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'NetFile',
  'Contains common file related functions. (RE)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'NetString',
  'Contains common string related functions.',
  @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'CommonDataTypes',
  'Represents a collection of Common Data Types.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'CommonKeywords',
  'Represents a collection of Common Key Words.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'CommonModifiers',
  'Represents a collection of Common Modifiers.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Keywords',
  'Represents a collection of Keywords.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LibTypes',
  'Represents a collection of Library Types.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'Modifiers',
  'Represents a collection of Modifiers.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'PropertyDelegate',
  'Represents a PropertyDelegate definition.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'PropertyDelegates',
  'Represents a collection of PropertyDelegate objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'RefTypes',
  'Represents a collection of Reference Types.',
  @seq;

/* LJCWinFormCommon */
set @assemblyName = 'LJCWinFormCommon';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlValue',
  'Represents a control's position and size.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ControlValues',
  'Represents a collection of ControlValue objects.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ModuleReference',
  'Represents a module reference.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ModuleReferences',
  'Represents a collection of ModuleReference objects.',
  @seq;

set @assemblyName = 'LJCWinFormCommon';
set @headingName = 'Comparer';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'ModuleNameComparer',
  'Sort and search on file name and module name.',
  @seq;

set @assemblyName = 'LJCWinFormCommon';
set @headingName = 'Static';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'FormCommon',
  'Provides common WinForms methods.',
  @seq;

/* LJCWinFormControls */
set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Combobox';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCItem',
  'Represents an LJCItemCombo Item.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCItemCombo',
  'Provides custom functionality for a ComboBox control. (R)',
  @seq;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'DataGrid';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCDataGrid',
  'Provides custom functionality for a DataGridView control. (D)',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCGridRow',
  'Provides custom functionality for a DataGridViewRow control.',
  @seq;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Tab';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCTabControl',
  'Provides custom drag and drop functionality for a TabControl.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'LJCTabPanel',
  'A Tab control in a panel.',
  @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName,
  'PanelControlsAdjust',
  'Contains standard panel control adjustment values.',
  @seq;
