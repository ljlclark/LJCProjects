/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 17DocMethod.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select dm.ID 'DocMethod', da.Name 'Assembly Name', dm.DocClassID, dc.Name,
  DocMethodGroupID, dmg.HeadingName 'Group Heading Name', dm.Name,
  dm.Description, dm.Sequence, OverloadName
from DocMethod as dm
left join DocClass as dc on DocClassID = dc.ID
left join DocAssembly as da on dc.DocAssemblyID = da.ID
left join DocMethodGroup as dmg on DocMethodGroupID = dmg.ID
order by da.Name, dc.Name, dmg.HeadingName, Sequence
*/

declare @docClassName nvarchar(60);
declare @headingName nvarchar(60);
declare @seq int
