/* sp_DocClassShow.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DocClassShow', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DocClassShow;  
GO  
CREATE PROCEDURE dbo.sp_DocClassShow
  @classID smallint
AS
BEGIN
declare @classGroupID smallint;
declare @assemblyID smallint;

select @classID=ID, @classGroupID=DocClassGroupID, @assemblyID=DocAssemblyID
from DocClass where ID = @classID;

select ID DocAssembly, DocAssemblyGroupID, Name, Description, Sequence
 , FileSpec, MainImage, ActiveFlag
from DocAssembly where ID = @assemblyID;
select ID DocClassGroup, DocAssemblyID, DocClassGroupHeadingID
 , HeadingName, HeadingTextCustom, Sequence, ActiveFlag
from DocClassGroup where ID = @classGroupID;
select ID DocClass, DocAssemblyID, DocClassGroupID, Name, Description
 , Sequence, ActiveFlag
from DocClass where ID = @classID;
select ID DocMethod, DocClassID, DocMethodGroupID, Name, Description
 , OverloadName, Sequence, ActiveFlag
from DocMethod where DocClassID = @classID;
END
GO
