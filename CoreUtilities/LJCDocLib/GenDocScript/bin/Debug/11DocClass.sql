/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 11DocClass.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select dc.ID 'DocClass' da.Name 'Assembly Name', dcg.HeadingName, dc.Name,
  dc.Description, dc.Sequence
from DocClass as dc
left join DocAssembly as da on DocAssemblyID = da.ID
left join DocClassGroup as dcg on DocClassGroupID = dcg.ID
order by da.Name, DocClass.Name, HeadingName, Sequence
*/

declare @assemblyName nvarchar(60);
declare @headingName nvarchar(60);
declare @seq int
