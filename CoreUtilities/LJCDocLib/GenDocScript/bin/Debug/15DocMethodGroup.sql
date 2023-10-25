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
exec sp_DMGAddUnique @className, 'Data',
 '',  2

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
exec sp_DMGAddUnique @className, 'Data',
 '',  2
exec sp_DMGAddUnique @className, 'OtherData',
 '',  3

set @className= 'DbColumn';
exec sp_DMGAddUnique @className, 'Static',
 '',  1
exec sp_DMGAddUnique @className, 'Constructor',
 '',  2
exec sp_DMGAddUnique @className, 'Data',
 '',  3

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

set @className= 'LJCItem';
exec sp_DMGAddUnique @className, 'Data',
 '',  1

set @className= 'LJCItemCombo';
exec sp_DMGAddUnique @className, 'Constructor',
 '',  1
exec sp_DMGAddUnique @className, 'Data',
 '',  2

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
