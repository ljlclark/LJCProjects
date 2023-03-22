/* 16sp_DMAddUnique.sql */
/* DocMethod Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DMAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DMAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DMAddUnique
  @className nvarchar(60),
  @headingName nvarchar(60),
  @name nvarchar(60),
  @description nvarchar(100),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
declare @docClassID smallint
  = (select ID from DocClass where Name = @className);
declare @docMethodGroupID smallint
  = (select ID from DocMethodGroup where DocClassID = @docClassID
     and HeadingName = @headingName);
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @docMethodGroupID
  and Name = @name)
  insert into DocMethod (DocClassID, DocMethodGroupID, Name, Description
    , Sequence, ActiveFlag)
    values (@docClassID, @docMethodGroupID, @name, @description
    , @sequence, @activeFlag);
END
GO
