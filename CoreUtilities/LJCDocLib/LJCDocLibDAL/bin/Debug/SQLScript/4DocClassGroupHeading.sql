/* 4DocClassGroupHeading.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, Name, Heading, Sequence
from DocClassGroupHeading;
*/

IF NOT EXISTS (select ID from DocClassGroupHeading
where Name = 'Static')
  insert into DocClassGroupHeading
   (Name, Heading, Sequence)
   values ('Static', 'Static Classes', 1);
IF NOT EXISTS (select ID from DocClassGroupHeading
where Name = 'Collection')
  insert into DocClassGroupHeading
   (Name, Heading, Sequence)
   values ('Collection', 'Collection and Object Classes', 2);
IF NOT EXISTS (select ID from DocClassGroupHeading
where Name = 'Comparer')
  insert into DocClassGroupHeading
   (Name, Heading, Sequence)
   values ('Comparer', 'Comparer Classes', 3);
IF NOT EXISTS (select ID from DocClassGroupHeading
where Name = 'Other')
  insert into DocClassGroupHeading
   (Name, Heading, Sequence)
   values ('Other', 'Other Classes', 5);
go