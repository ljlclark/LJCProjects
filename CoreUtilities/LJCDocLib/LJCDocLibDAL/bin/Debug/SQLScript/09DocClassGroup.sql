/* 9DocClassGroup.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select Name, DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, DocClassGroup.Sequence
from DocClassGroup
left join DocAssembly on DocAssemblyID = DocAssembly.ID
order by DocAssemblyID, DocClassGroup.Sequence;
*/

declare @assemblyName nvarchar(60);

set @assemblyName = 'LJCDataAccess';
exec sp_DCGAddUnique @assemblyName, 'Ungrouped'
  , null, 1;

set @assemblyName = 'LJCDBClientLib';
exec sp_DCGAddUnique @assemblyName, 'Ungrouped'
  , null, 1;

set @assemblyName = 'LJCDBMessage';
exec sp_DCGAddUnique @assemblyName, 'Ungrouped'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Request'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'Result'
  , null, 3;

set @assemblyName = 'LJCGridDataLib';
exec sp_DCGAddUnique @assemblyName, 'DataGrid'
  , 'DataGrid', 1;

set @assemblyName = 'LJCNetCommon';
exec sp_DCGAddUnique @assemblyName, 'Ungrouped'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Static'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'Collection'
  , null, 3;
exec sp_DCGAddUnique @assemblyName, 'Comparer'
  , null, 4;
exec sp_DCGAddUnique @assemblyName, 'Reflection'
  , 'Reflection Classes', 5;
exec sp_DCGAddUnique @assemblyName, 'Syntax'
  , 'Syntax Classes', 6;

set @assemblyName = 'LJCWinFormControls';
exec sp_DCGAddUnique @assemblyName, 'Ungrouped'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Combobox'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'DataGrid'
  , null, 3;
exec sp_DCGAddUnique @assemblyName, 'Tab'
  , null, 4;
