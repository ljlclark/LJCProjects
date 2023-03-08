SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_DAGGetIDWithName', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_DAGGetIDWithName;  
GO  
CREATE PROCEDURE dbo.DAGGetIDWithName
  @name varchar(60)
AS
BEGIN
  SET NOCOUNT ON;
  select
    ID,
    Name
  from DocAssemblyGroup
  where Name = @name;
END
GO
