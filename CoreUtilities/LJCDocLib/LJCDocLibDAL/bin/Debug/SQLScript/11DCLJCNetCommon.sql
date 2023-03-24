/* 11DocClass.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select
  ID, DocClassGroupID, Name, Description, Sequence
from DocClass;
*/

declare @assemblyName nvarchar(60) = 'LJCNetCommon';
declare @headingName nvarchar(60) = 'Static';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetCommon'
  , 'Contains common static functions. (RDE)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetFile'
  , 'Contains common file related functions. (RE)', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'NetString'
  , 'Contains common string related functions.', 3;

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

set @headingName = 'Reflection';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCAssemblyReflect'
  , 'Provides Assembly Reflection methods. (DE)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCReflect'
  , 'Provides object property reflection capabilities. (DE)', 2;

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

exec sp_DCAddUnique @assemblyName, null
  , 'AppSettings'
  , 'Represents the Configuration AppSettings. (RE)', 1;
exec sp_DCAddUnique @assemblyName, null
  , 'CodeTokenizer'
  , 'A C# Code Tokenizer class. (RE)', 2;
exec sp_DCAddUnique @assemblyName, null
  , 'Cryptography_Type'
  , 'The encryption types.', 3;
exec sp_DCAddUnique @assemblyName, null
  , 'DataTypes'
  , 'Represents a collection of Data Types.', 4;
exec sp_DCAddUnique @assemblyName, null
  , 'LJCCryptography'
  , 'Provides methods to encrypt and decrypt data in memory.', 5;
