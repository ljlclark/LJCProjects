/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 13DocMethodGroupHeading.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select ID 'DocMethodGroupHeading', Name, Heading, Sequence, ActiveFlag
from DocMethodGroupHeading
order by Sequence;
*/

exec sp_DMGHAddUnique 'Static', 'Static Functions'  , 1
exec sp_DMGHAddUnique 'Collection', 'Collection Methods'  , 1
exec sp_DMGHAddUnique 'Data', 'Data Methods'  , 1
exec sp_DMGHAddUnique 'DataProperties', 'Data Properties'  , 2
exec sp_DMGHAddUnique 'SearchSort', 'Search and Sort Methods'  , 2
exec sp_DMGHAddUnique 'Constructor', 'Constructors'  , 2
exec sp_DMGHAddUnique 'Value', 'Value Methods'  , 3
exec sp_DMGHAddUnique 'OtherData', 'Other Data Methods'  , 3
