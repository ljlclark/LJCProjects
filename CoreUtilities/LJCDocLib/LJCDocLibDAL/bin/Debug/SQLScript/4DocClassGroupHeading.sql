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
  where Name = 'StaticClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('StaticClasses', 'Static Classes', 1);
IF NOT EXISTS (select ID from DocClassGroupHeading
  where Name = 'CollectionClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('CollectionClasses', 'Collection and Object Classes', 2);
IF NOT EXISTS (select ID from DocClassGroupHeading
  where Name = 'ComparerClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('ComparerClasses', 'Comparer Classes', 3);
IF NOT EXISTS (select ID from DocClassGroupHeading
  where Name = 'ReflectionClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('ReflectionClasses', 'Reflection Classes', 4);
IF NOT EXISTS (select ID from DocClassGroupHeading
  where Name = 'OtherClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('OtherClasses', 'Other Classes', 5);
IF NOT EXISTS (select ID from DocClassGroupHeading
  where Name = 'SyntaxClasses')
insert into DocClassGroupHeading
 (Name, Heading, Sequence)
 values ('SyntaxClasses', 'Syntax Classes', 6);
go