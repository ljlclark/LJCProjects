/* 10sp_DCAddUnique.sql */
/* DocClass Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DCAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DCAddUnique;  
GO
CREATE PROCEDURE dbo.sp_DCAddUnique
  @assemblyName nvarchar(60),
  @headingName nvarchar(60),
  @name nvarchar(60),
  @description nvarchar(100),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
declare @docAssemblyID smallint
  = (select ID from DocAssembly where Name = @assemblyName);
declare @docClassGroupID smallint
  = (select ID from DocClassGroup where DocAssemblyID = @docAssemblyID
     and HeadingName = @headingName);
IF NOT EXISTS (select ID from DocClass
where DocAssemblyID = @docAssemblyID
  and DocClassGroupID = @docClassGroupID
  and Name = @name)
  IF NOT EXISTS (select ID from DocClass
  where DocAssemblyID = @docAssemblyID
    and DocClassGroupID is null
    and Name = @name)
  insert into DocClass (DocAssemblyID, DocClassGroupID, Name, Description
	  , Sequence, ActiveFlag)
    values (@docAssemblyID, @docClassGroupID, @name, @description
	  , @sequence, @activeFlag);
END
GO
