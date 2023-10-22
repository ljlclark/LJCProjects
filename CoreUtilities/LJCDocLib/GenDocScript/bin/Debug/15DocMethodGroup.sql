/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 15DocMethodGroup.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select DocMethodGroup.ID 'DocMethodGroup', Name 'Class Name', HeadingName,
  HeadingTextCustom, DocMethodGroup.Sequence, DocMethodGroup.ActiveFlag
from DocClassGroup
left join DocClass on DocClassID = DocClass.ID
order by DocClassID, DocMethodGroup.Sequence;
*/


declare @className nvarchar(60);


/* DataAccess */
/* ------------------------------ */
exec sp_DMGAddUnique 'DataAccess', 'Constructor'
 ''  , 1
exec sp_DMGAddUnique 'DataAccess', 'Data'
 ''  , 2

/* ProcedureParameters */
/* ------------------------------ */
exec sp_DMGAddUnique 'ProcedureParameters', 'Constructor'
 ''  , 1
exec sp_DMGAddUnique 'ProcedureParameters', 'Collection'
 ''  , 2
exec sp_DMGAddUnique 'ProcedureParameters', 'SearchSort'
 ''  , 2

/* DataManager */
/* ------------------------------ */
exec sp_DMGAddUnique 'DataManager', 'Constructor'
 ''  , 1
exec sp_DMGAddUnique 'DataManager', 'Data'
 ''  , 2
exec sp_DMGAddUnique 'DataManager', 'OtherData'
 ''  , 3

/* NetCommon */
/* ------------------------------ */
exec sp_DMGAddUnique 'NetCommon', 'Static'
 ''  , 1
exec sp_DMGAddUnique 'NetCommon', 'TextTransform'
 'Text Transform Functions'  , 2
exec sp_DMGAddUnique 'NetCommon', 'Serialize'
 'Serialization Functions'  , 3
exec sp_DMGAddUnique 'NetCommon', 'Config'
 'Program Config Value Functions'  , 4
exec sp_DMGAddUnique 'NetCommon', 'Value'
 ''  , 5
