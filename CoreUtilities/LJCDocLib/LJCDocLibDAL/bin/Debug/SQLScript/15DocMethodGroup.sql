/* 15DMG_NetCommon.sql */
/* DocMethodGroup */
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

declare @className nvarchar(60);

/* LJCDataAccess */
set @className = 'DataAccess';
exec sp_DMGAddUnique @className, 'Constructor'
  , null, 1;
exec sp_DMGAddUnique @className, 'Data'
  , null, 2;

set @className = 'ProcedureParameters';
exec sp_DMGAddUnique @className, 'Constructor'
  , null, 1;
exec sp_DMGAddUnique @className, 'Collection'
  , null, 2;
exec sp_DMGAddUnique @className, 'SearchSort'
  , null, 2;

/* LJCDBClientLib */
set @className = 'DataManager';
exec sp_DMGAddUnique @className, 'Constructor'
  , null, 1
exec sp_DMGAddUnique @className, 'Data'
  , null, 2;
exec sp_DMGAddUnique @className, 'OtherData'
  , null, 3;

/* LJCNetCommon */
set @className = 'NetCommon';
exec sp_DMGAddUnique @className, 'Static'
  , null, 1;
exec sp_DMGAddUnique @className, 'TextTransform'
  , 'Text Transform Functions', 2;
exec sp_DMGAddUnique @className, 'Serialize'
  , 'Serialization Functions', 3;
exec sp_DMGAddUnique @className, 'Config'
  , 'Program Config Value Functions', 4;
exec sp_DMGAddUnique @className, 'Value'
  , null, 5;
