/* 6sp_DCGHAddUnique.sql */
/* DocClassGroupHeading Add Unique */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DCGHAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DCGHAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DCGHAddUnique
  @name nvarchar(60),
  @heading nvarchar(100),
  @sequence smallint
AS
BEGIN
IF NOT EXISTS (select ID from DocClassGroupHeading
where Name = @name)
  insert into DocClassGroupHeading
    (Name, Description, Sequence)
    values (@name, @description, @sequence);
END
GO
