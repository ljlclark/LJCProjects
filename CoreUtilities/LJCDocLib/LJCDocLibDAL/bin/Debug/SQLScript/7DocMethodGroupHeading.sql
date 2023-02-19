/* 7DocMethodGroupHeading.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, Name, Heading, Sequence
from DocMethodGroupHeading;
*/

/* Common */
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Static')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Static', 'Static Functions', 1);
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Constructor')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Constructor', 'Constructors', 2);
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Other')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Other', 'Other Methods', 3);

/* Collection */
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Collection')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Collection', 'Collection Methods', 1);
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'SearchSort')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('SearchSort', 'Search and Sort Methods', 2);
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Value')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Value', 'Value Methods', 3);

/* Data */
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'Data')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('Data', 'Data Methods', 1);
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = 'DataProperties')
  insert into DocMethodGroupHeading
   (Name, Heading, Sequence)
   values ('DataProperties', 'Data Properties', 2);
go