/* 4sp_DAAddUnique.sql */
/* DocAssembly Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DAAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DAAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DAAddUnique
  @groupName nvarchar(60),
  @name nvarchar(60),
  @description nvarchar(100),
  @fileSpec nvarchar(120),
  @mainImage nvarchar(60),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
declare @DocAssemblyGroupID smallint
  = (select ID from DocAssemblyGroup where Name = @groupName);
IF NOT EXISTS (select ID from DocAssembly
where Name = @name)
  insert into DocAssembly
    (DocAssemblyGroupID, Name, Description, Sequence, FileSpec, MainImage
     , ActiveFlag)
    values (@DocAssemblyGroupID, @name, @description
      , @sequence, @fileSpec, @mainImage, @activeFlag);
END
GO
