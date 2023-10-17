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
select dc.ID 'DocClass' da.Name 'Assembly Name', dcg.HeadingName, dc.Name,
  dc.Description, dc.Sequence
from DocClass as dc
left join DocAssembly as da on DocAssemblyID = da.ID
left join DocClassGroup as dcg on DocClassGroupID = dcg.ID
order by da.Name, DocClass.Name, HeadingName, Sequence
*/

declare @assemblyName nvarchar(60);
declare @headingName nvarchar(60);
declare @seq int

/* LJCDataAccess */
/* ------------------------------ */
set @assemblyName = 'LJCDataAccess';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataAccess'
  , ''
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'ProcedureParameters'
  , ''
  , @seq;

/* LJCDBClientLib */
/* ------------------------------ */
set @assemblyName = 'LJCDBClientLib';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataManager'
  , ''
  , @seq;

/* LJCDBMessage */
/* ------------------------------ */
set @assemblyName = 'LJCDBMessage';
set @headingName = 'Request';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbCondition'
  , 'Represents a filter condition.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditions'
  , 'Represents a collection of DbCondition objects.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditionSet'
  , 'Represents the conditions and properties. (E)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilter'
  , 'Represents a filter which is part of a where clause.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilters'
  , 'Represents a collection of DbFilter objects. (E)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoin'
  , 'Represents a database table join.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOn'
  , 'Represents a Join On definition.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOns'
  , 'Represents a collection of join on definitions.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoins'
  , 'Represents a collection of table joins. (E)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRequest'
  , 'Represents a database request. (E)'
  , @seq;

set @assemblyName = 'LJCDBMessage';
set @headingName = 'Result';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbResult'
  , 'Represents a Request result. (R)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRow'
  , 'Represents a result Row.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRows'
  , 'Represents a collection of LJCNetCommon.DbValues.'
  , @seq;

/* LJCGridDataLib */
/* ------------------------------ */
set @assemblyName = 'LJCGridDataLib';
set @headingName = 'DataGrid';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'ResultGridData'
  , 'Provides DbResult helpers for an LJCDataGrid control.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'TableGridData'
  , 'Provides DataTable helpers for an LJCDataGrid control.'
  , @seq;

/* LJCNetCommon */
/* ------------------------------ */
set @assemblyName = 'LJCNetCommon';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'AppSettings'
  , 'Represents the Configuration AppSettings. (RE)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CodeTokenizer'
  , 'A C# Code Tokenizer class. (RE)'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonDataTypes'
  , 'Represents a collection of Common Data Types.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonKeywords'
  , 'Represents a collection of Common Key Words.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonModifiers'
  , 'Represents a collection of Common Modifiers.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Cryptography_Type'
  , 'The encryption types.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataTypes'
  , 'Represents a collection of Data Types.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumn'
  , 'Represents a Data Column definition. (D)'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Comparer';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnNameComparer'
  , 'Sort and search on column name.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnPropertyComparer'
  , 'Sort and search on PropertyName.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnRenameAsComparer'
  , 'Sort and search on RenameAs value.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumns'
  , 'Represents a collection of DbColumn objects.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbValue'
  , 'Represents a data source value.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbValues'
  , 'Represents a collection of DbValue objects.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'KeyItem'
  , 'Represents Key item values.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'KeyItems'
  , 'Represents a collection of KeyItem objects.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Keywords'
  , 'Represents a collection of Keywords.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LibTypes'
  , 'Represents a collection of Library Types.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Reflection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCAssemblyReflect'
  , 'Provides Assembly Reflection methods. (DE)'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = '';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCCryptography'
  , 'Provides methods to encrypt and decrypt data in memory.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Reflection';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCReflect'
  , 'Provides object property reflection capabilities. (DE)'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Modifiers'
  , 'Represents a collection of Modifiers.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Static';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetCommon'
  , 'Contains common static functions. (RDE)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetFile'
  , 'Contains common file related functions. (RE)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetString'
  , 'Contains common string related functions.'
  , @seq;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PropertyDelegate'
  , 'Represents a PropertyDelegate definition.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PropertyDelegates'
  , 'Represents a collection of PropertyDelegate objects.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'RefTypes'
  , 'Represents a collection of Reference Types.'
  , @seq;

/* LJCWinFormControls */
/* ------------------------------ */
set @assemblyName = 'LJCWinFormControls';
set @headingName = 'DataGrid';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCDataGrid'
  , 'Provides custom functionality for a DataGridView control. (D)'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCGridRow'
  , 'Provides custom functionality for a DataGridViewRow control.'
  , @seq;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Combobox';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItem'
  , 'Represents an LJCItemCombo Item.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItemCombo'
  , 'Provides custom functionality for a ComboBox control. (R)'
  , @seq;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Tab';
set @seq = 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabControl'
  , 'Provides custom drag and drop functionality for a TabControl.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabPanel'
  , 'A Tab control in a panel.'
  , @seq;
set @seq += 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PanelControlsAdjust'
  , 'Contains standard panel control adjustment values.'
  , @seq;
