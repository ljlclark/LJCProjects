/* sp_DocAssemblyShow.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DocAssemblyShow', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DocAssemblyShow;  
GO
CREATE PROCEDURE dbo.sp_DocAssemblyShow
  @assemblyID smallint
AS
BEGIN
declare @assemblyGroupID smallint;

select @assemblyGroupID=DocAssemblyGroupID
from DocAssembly where ID = @assemblyID;

select ID DocAssemblyGroup, Name, Heading, Sequence, ActiveFlag
from DocAssemblyGroup where ID = @assemblyGroupID;
select ID DocAssembly, DocAssemblyGroupID, Name, Description, Sequence
 , FileSpec, MainImage, ActiveFlag
from DocAssembly where ID = @assemblyID;
select ID DocClass, DocAssemblyID, DocClassGroupID, Name, Description
 , Sequence, ActiveFlag
from DocClass where DocAssemblyID = @assemblyID;
END
GO
