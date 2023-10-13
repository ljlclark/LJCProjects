/* 7DocClassGroupHeading.sql */
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

exec sp_DCGHAddUnique 'Static', 'Static Classes', 1;
exec sp_DCGHAddUnique 'Collection', 'Collection and Object Classes', 2;
exec sp_DCGHAddUnique 'Comparer', 'Comparer Classes', 3;
