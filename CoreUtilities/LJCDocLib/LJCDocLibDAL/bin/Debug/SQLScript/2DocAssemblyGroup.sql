/* 2DocAssemblyGroup.sql */
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, Name, Heading, Sequence
from DocAssemblyGroup;
*/

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'CommonLibraries')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('CommonLibraries', 'Common Libraries', 1);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'DataLibraries')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('DataLibraries', 'Data Libraries', 2);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'CodeGen')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('CodeGen', 'Code Generator Utility', 3);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'DocGen')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('DocGen', 'HTML Documentation Generator', 4);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'DataTransform')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('DataTransform', 'Data Transform Projects', 5);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'CVRManager')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('CVRManager', 'Contact Visit Record Manager', 6);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'LJCSales')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('LJCSales', 'Sales Manager', 7);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'LJCUnitMeasure')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('LJCUnitMeasure', 'Unit Measure', 8);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'FacilityManager')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('FacilityManager', 'Facility Manager', 9);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'FacilityManagerSetup')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('FacilityManagerSetup', 'Facility Manager Setup', 10);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'RegionManager')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('RegionManager', 'Region Manager', 11);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'AppManager')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('AppManager', 'Application Manager', 12);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'DocAppManager')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('DocAppManager', 'DocApp Manager', 13);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'DBViewDAL')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('DBViewDAL', 'Data View Data Access Layer', 14);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'ViewBuilder')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('ViewBuilder', 'Data View Builder', 15);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'ViewEditor')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('ViewEditor', 'Data View Editor', 16);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'CodeLine')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('CodeLine', 'Code Line Counter and Text Finder.', 17);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'TextInvasion')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('TextInvasion', 'Text Invasion Typing Game', 18);
go

IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = 'LJCPagination')
  insert into DocAssemblyGroup
   (Name, Heading, Sequence)
   values ('LJCPagination', 'Pagination Testing', 19);
go
