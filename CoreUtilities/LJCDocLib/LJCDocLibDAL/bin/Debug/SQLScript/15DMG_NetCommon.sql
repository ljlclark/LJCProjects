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

declare @className nvarchar(60) = 'NetCommon';
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
