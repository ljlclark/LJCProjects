﻿/* 3DocAssemblyGroup.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
s/*
select
  ID, Name, Heading, Sequence
from DocAssemblyGroup;
*/

exec sp_DAGAddUnique 'CommonLibraries', 'Common Libraries', 1
exec sp_DAGAddUnique 'DataLibraries', 'Data Libraries', 2
exec sp_DAGAddUnique 'CodeGen', 'Code Generator Utility', 3
exec sp_DAGAddUnique 'DocGen', 'HTML Documentation Generator', 4
exec sp_DAGAddUnique 'DataTransform', 'Data Transform Projects', 5
exec sp_DAGAddUnique 'CVRManager', 'Contact Visit Record Manager', 6
exec sp_DAGAddUnique 'LJCSales', 'Sales Manager', 7
exec sp_DAGAddUnique 'LJCUnitMeasure', 'Unit Measure', 8
exec sp_DAGAddUnique 'FacilityManager', 'Facility Manager', 9
exec sp_DAGAddUnique 'FacilityManagerSetup', 'Facility Manager Setup', 10
exec sp_DAGAddUnique 'RegionManager', 'Region Manager', 11
exec sp_DAGAddUnique 'AppManager', 'Application Manager', 12
exec sp_DAGAddUnique 'DocAppManager', 'DocApp Manager', 13
exec sp_DAGAddUnique 'DBViewDAL', 'Data View Data Access Layer', 14
exec sp_DAGAddUnique 'ViewBuilder', 'Data View Builder', 15
exec sp_DAGAddUnique 'ViewEditor', 'Data View Editor', 16
exec sp_DAGAddUnique 'CodeLine', 'Code Line Counter and Text Finder', 17
exec sp_DAGAddUnique 'TextInvasion', 'Text Invasion Typing Game', 18
exec sp_DAGAddUnique 'LJCPagination', 'Pagination Testing', 19
