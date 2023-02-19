/* 5DocClassGroup.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, DocAssemblyID, DocClassGroupHeadingID, Sequence
from DocClassGroup;
*/

declare @DocAssemblyID smallint
  = (select ID from DocAssembly where Name = 'LJCNetCommon');

declare @DocClassGroupHeadingID smallint
  = (select ID from DocClassGroupHeading where Name = 'StaticClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 1);

set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = 'CollectionClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 2);

set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = 'ComparerClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 3);

set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = 'ReflectionClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 4);

set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = 'OtherClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 5);

set @DocClassGroupHeadingID
  = (select ID from DocClassGroupHeading where Name = 'SyntaxClasses');
IF NOT EXISTS (select ID from DocClassGroup
  where DocAssemblyID = @DocAssemblyID
  and DocClassGroupHeadingID = @DocClassGroupHeadingID)
insert into DocClassGroup
 (DocAssemblyID, DocClassGroupHeadingID, Sequence)
 values (@DocAssemblyID, @DocClassGroupHeadingID, 6);
go