/* sp_DocMethodShow.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DocMethodShow', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DocMethodShow;  
GO  
CREATE PROCEDURE dbo.sp_DocMethodShow
  @methodID smallint
AS
BEGIN
declare @classID smallint;
declare @methodGroupID smallint;
declare @assemblyID smallint;

select @classID=DocClassID, @methodGroupID=DocMethodGroupID
from DocMethod where ID = @methodID;
select @assemblyID=DocAssemblyID from DocClass where ID = @classID;

select ID DocAssembly, DocAssemblyGroupID, Name, Description, Sequence
 , FileSpec, MainImage, ActiveFlag
from DocAssembly where ID = @assemblyID;
select ID DocClass, DocAssemblyID, DocClassGroupID, Name, Description
 , Sequence, ActiveFlag
from DocClass where ID = @classID;
select ID DocMethodGroup, DocClassID, DocMethodGroupHeadingID
 , HeadingName, HeadingTextCustom, Sequence, ActiveFlag
from DocMethodGroup where ID = @methodGroupID;
select ID DocMethod, DocClassID, DocMethodGroupID, Name, Description
 , Sequence, ActiveFlag
from DocMethod where ID = @methodID;
END
GO
