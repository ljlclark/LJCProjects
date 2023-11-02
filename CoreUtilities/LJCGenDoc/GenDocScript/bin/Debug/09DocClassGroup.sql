/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 09DocClassGroup.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select DocClassGroup.ID 'DocClassGroup', Name 'Assembly Name', HeadingName,
  HeadingTextCustom, DocClassGroup.Sequence, DocClassGroup.ActiveFlag
from DocClassGroup
left join DocAssembly on DocAssemblyID = DocAssembly.ID
order by DocAssemblyID, DocClassGroup.Sequence;
*/

declare @assemblyName nvarchar(60);

set @assemblyName= 'LJCNetCommon';
exec sp_DCGAddUnique @assemblyName,  'Ungrouped',
 '',  1
exec sp_DCGAddUnique @assemblyName,  'Static',
 '',  2
exec sp_DCGAddUnique @assemblyName,  'Collection',
 '',  3
exec sp_DCGAddUnique @assemblyName,  'Comparer',
 '',  4
exec sp_DCGAddUnique @assemblyName,  'Reflection',
 'Reflection Classes',  5
exec sp_DCGAddUnique @assemblyName,  'Syntax',
 'Syntax Classes',  6

set @assemblyName= 'LJCAddressParserLib';
exec sp_DCGAddUnique @assemblyName,  'Collection',
 '',  1
exec sp_DCGAddUnique @assemblyName,  'Comparer',
 '',  2

set @assemblyName= 'LJCWinFormCommon';
exec sp_DCGAddUnique @assemblyName,  'Static',
 '',  1
exec sp_DCGAddUnique @assemblyName,  'Collection',
 '',  2
exec sp_DCGAddUnique @assemblyName,  'Comparer',
 '',  3

set @assemblyName= 'LJCWinFormControls';
exec sp_DCGAddUnique @assemblyName,  'Ungrouped',
 '',  1
exec sp_DCGAddUnique @assemblyName,  'Combobox',
 '',  2
exec sp_DCGAddUnique @assemblyName,  'DataGrid',
 '',  3
exec sp_DCGAddUnique @assemblyName,  'Tab',
 '',  4

set @assemblyName= 'LJCGridDataLib';
exec sp_DCGAddUnique @assemblyName,  'DataGrid',
 'DataGrid',  1

set @assemblyName= 'LJCDataAccess';
exec sp_DCGAddUnique @assemblyName,  'Ungrouped',
 '',  1

set @assemblyName= 'LJCDataAccessConfig';
exec sp_DCGAddUnique @assemblyName,  'Collection',
 '',  1

set @assemblyName= 'LJCDBClientLib';
exec sp_DCGAddUnique @assemblyName,  'DataAccess',
 'Data Access',  2
exec sp_DCGAddUnique @assemblyName,  'DataManager',
 'Data Manager',  3

set @assemblyName= 'LJCDBMessage';
exec sp_DCGAddUnique @assemblyName,  'Ungrouped',
 '',  1
exec sp_DCGAddUnique @assemblyName,  'Request',
 '',  2
exec sp_DCGAddUnique @assemblyName,  'Result',
 '',  3
