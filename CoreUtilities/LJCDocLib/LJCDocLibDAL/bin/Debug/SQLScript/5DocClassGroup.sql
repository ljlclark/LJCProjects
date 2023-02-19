/* 5DocClassGroup.sql */
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

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');

declare @HeadingName nvarchar(60);
declare @DocClassGroupHeadingID smallint;

set @HeadingName = 'Static';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , null, 1);

set @HeadingName = 'Collection';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , null, 1);

set @HeadingName = 'Comparer';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , null, 3);

set @HeadingName = 'Reflection';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , 'Reflection Classes', 4);

set @HeadingName = 'Other';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , null, 5);

set @HeadingName = 'Syntax';
set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @DocAssemblyID
  and HeadingName = @HeadingName)
  insert into DocClassGroup
   (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocAssemblyID, @DocClassGroupHeadingID, @HeadingName
     , 'Syntax Classes', 6);
go
