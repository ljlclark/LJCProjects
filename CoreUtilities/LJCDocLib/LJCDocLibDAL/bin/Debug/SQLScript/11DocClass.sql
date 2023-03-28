/* 11DCLJCDBMessage.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select
  ID, DocAssemblyID, DocClassGroupID, Name, Description, Sequence
from DocClass;
*/

declare @assemblyName nvarchar(60);
declare @headingName nvarchar(60);

/* ------------------------------ */
set @assemblyName = 'LJCDataAccess';
set @headingName = null;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataAccess'
  , '', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'ProcedureParameters'
  , '', 2;

/* ------------------------------ */
set @assemblyName = 'LJCDBClientLib';
set @headingName = null;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataManager'
  , '', 1;

/* ------------------------------ */
set @assemblyName = 'LJCDBMessage';
set @headingName = 'Request';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbCondition'
  , 'Represents a filter condition.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditions'
  , 'Represents a collection of DbCondition objects.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditionSet'
  , 'Represents the conditions and properties. (E)', 3;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilter'
  , 'Represents a filter which is part of a where clause.', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilters'
  , 'Represents a collection of DbFilter objects. (E)', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoin'
  , 'Represents a database table join.', 5;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOn'
  , 'Represents a Join On definition.', 6;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOns'
  , 'Represents a collection of join on definitions.', 7;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoins'
  , 'Represents a collection of table joins. (E)', 8;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRequest'
  , 'Represents a database request. (E)', 9;

set @assemblyName = 'LJCDBMessage';
set @headingName = 'Result';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbResult'
  , 'Represents a Request result. (R)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRow'
  , 'Represents a result Row.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRows'
  , 'Represents a collection of LJCNetCommon.DbValues.', 3;

/* ------------------------------ */
set @assemblyName = 'LJCNetCommon';
set @headingName = 'Static';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetCommon'
  , 'Contains common static functions. (RDE)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetFile'
  , 'Contains common file related functions. (RE)', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetString'
  , 'Contains common string related functions.', 3;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Collection';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumn'
  , 'Represents a Data Column definition. (D)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumns'
  , 'Represents a collection of DbColumn objects.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbValue'
  , 'Represents a data source value.', 3;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbValues'
  , 'Represents a collection of DbValue objects.', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'KeyItem'
  , 'Represents Key item values.', 5;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'KeyItems'
  , 'Represents a collection of KeyItem objects.', 6;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Comparer';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnNameComparer'
  , 'Sort and search on column name.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnPropertyComparer'
  , 'Sort and search on PropertyName.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbColumnRenameAsComparer'
  , 'Sort and search on RenameAs value.', 3;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Reflection';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCAssemblyReflect'
  , 'Provides Assembly Reflection methods. (DE)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCReflect'
  , 'Provides object property reflection capabilities. (DE)', 2;

set @assemblyName = 'LJCNetCommon';
set @headingName = 'Syntax';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonDataTypes'
  , 'Represents a collection of Common Data Types.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonKeywords'
  , 'Represents a collection of Common Key Words.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CommonModifiers'
  , 'Represents a collection of Common Modifiers.', 3;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Keywords'
  , 'Represents a collection of Keywords.', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LibTypes'
  , 'Represents a collection of Library Types.', 5;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Modifiers'
  , 'Represents a collection of Modifiers.', 6;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PropertyDelegate'
  , 'Represents a PropertyDelegate definition.', 7;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PropertyDelegates'
  , 'Represents a collection of PropertyDelegate objects.', 8;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'RefTypes'
  , 'Represents a collection of Reference Types.', 9;

set @assemblyName = 'LJCNetCommon';
set @headingName = null;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'AppSettings'
  , 'Represents the Configuration AppSettings. (RE)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'CodeTokenizer'
  , 'A C# Code Tokenizer class. (RE)', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'Cryptography_Type'
  , 'The encryption types.', 3;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DataTypes'
  , 'Represents a collection of Data Types.', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCCryptography'
  , 'Provides methods to encrypt and decrypt data in memory.', 5;

/* ------------------------------ */
set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Combobox';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItem'
  , 'Represents an LJCItemCombo Item.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItemCombo'
  , 'Provides custom functionality for a ComboBox control. (R)', 2;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'DataGrid';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCHelper'
  , 'Provides methods for setting a complex list control when AutoScaleMode.Font is used.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCDataGrid'
  , 'Provides custom functionality for a DataGridView control. (D)', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCGridRow'
  , 'Provides custom functionality for a DataGridViewRow control.', 3;

set @assemblyName = 'LJCWinFormControls';
set @headingName = 'Tab';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabControl'
  , 'Provides custom drag and drop functionality for a TabControl.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabPanel'
  , 'A Tab control in a panel.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PanelControlsAdjust'
  , 'Contains standard panel control adjustment values.', 3;
