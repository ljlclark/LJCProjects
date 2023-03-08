/* 8sp_DCGAddUnique.sql */
/* DocClassGroup Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DCGAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DCGAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DCGAddUnique
  @assemblyName nvarchar(60),
  @headingName nvarchar(60),
  @headingTextCustom nvarchar(100),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
declare @docAssemblyID smallint
  = (select ID from DocAssembly where Name = @assemblyName);
declare @docClassGroupHeadingID smallint
  = (select ID from DocClassGroupHeading where Name = @headingName);
IF NOT EXISTS (select ID from DocClassGroup
where DocAssemblyID = @docAssemblyID
  and HeadingName = @headingName)
  insert into DocClassGroup
    (DocAssemblyID, DocClassGroupHeadingID, HeadingName, HeadingTextCustom
      , Sequence, ActiveFlag)
    values (@docAssemblyID, @docClassGroupHeadingID, @headingName
      , @headingTextCustom, @sequence, @activeFlag);
END
GO
