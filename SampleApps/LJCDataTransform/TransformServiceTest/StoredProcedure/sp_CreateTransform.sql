-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateTransform', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateTransform;  
GO  
CREATE PROCEDURE dbo.sp_CreateTransform
  @Name varchar(60),
	@Description varchar(80),
	@TaskName varchar(60),
	@FromSourceName varchar(60),
	@ToSourceName varchar(60)
AS
BEGIN
	SET NOCOUNT ON;

  if (len(@Name) > 0 and len(@Description) > 0
	 and len(@TaskName) > 0 and len(@FromSourceName) > 0
	 and len(@ToSourceName) > 0)
  begin
    declare @TaskID int;
    declare @DataSourceID int;
    declare @TargetID int;
		
    set @TaskID = (select StepTaskID from StepTask where Name = @TaskName);
    set @DataSourceID = (select DataSourceID from DataSource where Name = @FromSourceName);
    set @TargetID = (select DataSourceID from DataSource where Name = @ToSourceName);
    IF NOT EXISTS (select TransformID from TaskTransform where StepTaskID = @TaskID
     and DataSourceID = @DataSourceID and TargetID = @TargetID)
    insert into TaskTransform (Name, Description, StepTaskID, DataSourceID, TargetID)
     values(@Name, @Description, @TaskID, @DataSourceID, @TargetID);
  end
END
GO
