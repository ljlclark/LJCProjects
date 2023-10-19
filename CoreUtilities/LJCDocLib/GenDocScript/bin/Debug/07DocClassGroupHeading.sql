/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 07DocClassGroupHeading.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select ID 'DocClassGroupHeading', Name, Heading, Sequence, ActiveFlag
from DocClassGroupHeading
order by Sequence;
*/

exec sp_DCGHAddUnique 'Static', 'Static Classes'  , 1
exec sp_DCGHAddUnique 'Collection', 'Collection and Object Classes'  , 2
exec sp_DCGHAddUnique 'Comparer', 'Comparer Classes'  , 3
