/* 6DocClass.sql */
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

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Static';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass
where DocClassGroupID = @DocClassGroupID
  and Name = 'NetCommon')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'NetCommon'
     , 'Contains common static functions. (RDE)', 1);
IF NOT EXISTS (select ID from DocClass
where DocClassGroupID = @DocClassGroupID
  and Name = 'NetFile')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'NetFile'
     , 'Contains common file related functions. (RE)', 2);
IF NOT EXISTS (select ID from DocClass
where DocClassGroupID = @DocClassGroupID
  and Name = 'NetString')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'NetString'
     , 'Contains common string related functions.', 3);
go

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Collection';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbColumn')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbColumn'
     , 'Represents a Data Column definition. (D)', 1);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbColumns')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbColumns'
     , 'Represents a collection of DbColumn objects.', 2);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbValue')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbValue'
     , 'Represents a data source value.', 3);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbValues')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbValues'
     , 'Represents a collection of DbValue objects.', 4);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'KeyItem')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'KeyItem'
     , 'Represents Key item values.', 5);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'KeyItems')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'KeyItems'
     , 'Represents a collection of KeyItem objects.', 6);
go

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Comparer';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbColumnNameComparer')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbColumnNameComparer'
     , 'Sort and search on column name.', 1);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbColumnPropertyComparer')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbColumnPropertyComparer'
     , 'Sort and search on PropertyName.', 2);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DbColumnRenameAsComparer')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DbColumnRenameAsComparer'
     , 'Sort and search on RenameAs value.', 3);
go

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Reflection';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'LJCAssemblyReflect')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'LJCAssemblyReflect'
     , 'Provides Assembly Reflection methods. (DE)', 1);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'LJCReflect')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'LJCReflect'
     , 'Provides object property reflection capabilities. (DE)', 2);
go

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Other';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'AppSettings')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'AppSettings'
     , 'Represents the Configuration AppSettings. (RE)', 1);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'CodeTokenizer')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'CodeTokenizer'
     , 'A C# Code Tokenizer class. (RE)', 2);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'Cryptography_Type')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'Cryptography_Type'
     , 'The encryption types.', 3);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'DataTypes')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'DataTypes'
     , 'Represents a collection of Data Types.', 4);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'LJCCryptography')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'LJCCryptography'
     , 'Provides methods to encrypt and decrypt data in memory.', 5);
go

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');
declare @HeadingName nvarchar(60) = 'Syntax';
declare @DocClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @DocAssemblyID
     and HeadingName = @HeadingName);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'CommonDataTypes')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'CommonDataTypes'
     , 'Represents a collection of Common Data Types.', 1);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'CommonKeywords')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'CommonKeywords'
     , 'Represents a collection of Common Key Words.', 2);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'CommonModifiers')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'CommonModifiers'
     , 'Represents a collection of Common Modifiers.', 3);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'Keywords')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'Keywords'
     , 'Represents a collection of Keywords.', 4);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'LibTypes')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'LibTypes'
     , 'Represents a collection of Library Types.', 5);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'Modifiers')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'Modifiers'
     , 'Represents a collection of Modifiers.', 6);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'PropertyDelegate')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'PropertyDelegate'
     , 'Represents a PropertyDelegate definition.', 7);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'PropertyDelegates')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'PropertyDelegates'
     , 'Represents a collection of PropertyDelegate objects.', 8);
IF NOT EXISTS (select ID from DocClass where DocClassGroupID = @DocClassGroupID
  and Name = 'RefTypes')
  insert into DocClass (DocClassGroupID, Name, Description, Sequence)
   values (@DocClassGroupID, 'RefTypes'
     , 'Represents a collection of Reference Types.', 9);
go
