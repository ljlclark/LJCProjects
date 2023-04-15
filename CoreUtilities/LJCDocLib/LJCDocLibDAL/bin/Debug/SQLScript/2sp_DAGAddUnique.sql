/* sp_MoveSequenceDown.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DAGAddUnique', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DAGAddUnique;  
GO  
CREATE PROCEDURE dbo.sp_DAGAddUnique
  @name nvarchar(60),
  @heading nvarchar(100),
  @sequence smallint,
  @activeFlag bit = 1
AS
BEGIN
IF NOT EXISTS (select ID from DocAssemblyGroup
where Name = @name)
  insert into DocAssemblyGroup
    (Name, Heading, Sequence, ActiveFlag)
    values (@name, @heading, @sequence, @activeFlag);
END
GO
