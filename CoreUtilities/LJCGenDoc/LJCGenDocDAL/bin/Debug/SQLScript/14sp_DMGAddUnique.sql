/* 14sp_DMGAddUnique.sql */
/* DocMethodGroup Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DMGAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DMGAddUnique;  
GO
CREATE PROCEDURE dbo.sp_DMGAddUnique
  @className nvarchar(60),
  @headingName nvarchar(60),
  @headingTextCustom nvarchar(100),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
declare @docClassID smallint
  = (select ID from DocClass where Name = @className);
declare @docMethodGroupHeadingID smallint
  = (select ID from DocMethodGroupHeading where Name = @headingName);
IF NOT EXISTS (select ID from DocMethodGroup
where DocClassID = @docClassID
  and HeadingName = @headingName)
  insert into DocMethodGroup
    (DocClassID, DocMethodGroupHeadingID, HeadingName, HeadingTextCustom
      , Sequence, ActiveFlag)
    values (@docClassID, @docMethodGroupHeadingID, @headingName
      , @headingTextCustom, @sequence, @activeFlag);
END
GO
