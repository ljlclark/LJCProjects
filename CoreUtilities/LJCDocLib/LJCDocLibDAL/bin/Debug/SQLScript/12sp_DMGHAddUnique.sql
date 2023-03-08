/* 12sp_DMGHAddUnique.sql */
/* DocMethodGroupHeading Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DMGHAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DMGHAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DMGHAddUnique
  @name nvarchar(60),
  @heading nvarchar(100),
  @sequence smallint
AS
BEGIN
IF NOT EXISTS (select ID from DocMethodGroupHeading
where Name = @name)
  insert into DocMethodGroupHeading
    (Name, Heading, Sequence)
    values (@name, @heading, @sequence);
END
GO