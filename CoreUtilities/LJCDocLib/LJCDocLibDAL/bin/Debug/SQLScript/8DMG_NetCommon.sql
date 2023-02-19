/* 8DMG_NetCommon.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom
    , Sequence
from DocMethodGroup;
*/

declare @DocClassID smallint
  = (select ID from DocClass where Name = 'NetCommon');

declare @HeadingName nvarchar(60);
declare @DocMethodGroupHeadingID smallint;

set @HeadingName = 'Static';
set @DocMethodGroupHeadingID
  = (select ID from DocMethodGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName)
  insert into DocMethodGroup
   (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocClassID, @DocMethodGroupHeadingID, @HeadingName
     , null, 1);

set @HeadingName = 'TextTransform';
set @DocMethodGroupHeadingID
  = (select ID from DocMethodGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName)
  insert into DocMethodGroup
   (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocClassID, @DocMethodGroupHeadingID, @HeadingName
     , 'Text Transform Functions', 2);

set @HeadingName = 'Serialize';
set @DocMethodGroupHeadingID
  = (select ID from DocMethodGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName)
  insert into DocMethodGroup
   (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocClassID, @DocMethodGroupHeadingID, @HeadingName
     , 'Serialization Functions', 3);

set @HeadingName = 'Config';
set @DocMethodGroupHeadingID
  = (select ID from DocMethodGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName)
  insert into DocMethodGroup
   (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocClassID, @DocMethodGroupHeadingID, @HeadingName
     , 'Program Config Value Functions', 4);

set @HeadingName = 'Value';
set @DocMethodGroupHeadingID
  = (select ID from DocMethodGroupHeading where Name = @HeadingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName)
  insert into DocMethodGroup
   (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom, Sequence)
   values (@DocClassID, @DocMethodGroupHeadingID, @HeadingName
     , null, 5);
