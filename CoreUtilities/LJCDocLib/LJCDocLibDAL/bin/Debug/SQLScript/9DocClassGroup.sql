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
*/

declare @assemblyName nvarchar(60) = 'LJCNetCommon';
exec sp_DCGAddUnique @assemblyName, 'Static'
  , null, 1;
exec sp_DCGAddUnique @assemblyName, 'Collection'
  , null, 2;
exec sp_DCGAddUnique @assemblyName, 'Comparer'
  , null, 3;
exec sp_DCGAddUnique @assemblyName, 'Reflection'
  , 'Reflection Classes', 4;
exec sp_DCGAddUnique @assemblyName, 'Other'
  , null, 5;
exec sp_DCGAddUnique @assemblyName, 'Syntax'
  , 'Syntax Classes', 6;
