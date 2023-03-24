/* 9DocClassGroup.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select
  ID, DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom
    , Sequence
from DocClassGroup;
select * from DocAssembly;
*/

declare @assemblyName nvarchar(60);

set @assemblyName = 'LJCDBDataAccessLib';
exec sp_DCGAddUnique @assemblyName, 'Request'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Result'
  , null, 2;

set @assemblyName = 'LJCDBMessage';
exec sp_DCGAddUnique @assemblyName, 'Request'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Result'
  , null, 2;

set @assemblyName = 'LJCNetCommon';
exec sp_DCGAddUnique @assemblyName, 'Static'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Collection'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'Comparer'
  , null, 3;
exec sp_DCGAddUnique @assemblyName, 'Reflection'
  , 'Reflection Classes', 4;
exec sp_DCGAddUnique @assemblyName, 'Syntax'
  , 'Syntax Classes', 5;

set @assemblyName = 'LJCWinFormControls';
exec sp_DCGAddUnique @assemblyName, 'Combobox'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'DataGrid'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'Tab'
  , null, 3;
