-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateTaskSource', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateTaskSource;  
GO  
CREATE PROCEDURE dbo.sp_CreateTaskSource
	@TaskName varchar(60),
	@LayoutName varchar(60),
	@SourceName varchar(60)
AS
BEGIN
	SET NOCOUNT ON;

  if (len(@TaskName) > 0 and len(@LayoutName) > 0
	 and len(@SourceName) > 0)
  begin
    declare @TaskID int;
    declare @LayoutID int;
    declare @DataSourceID int;
		
    set @TaskID = (select StepTaskID from StepTask where Name = @TaskName);
    set @LayoutID = (select SourceLayoutID from SourceLayout where Name = @LayoutName);
    set @DataSourceID = (select DataSourceID from DataSource where SourceLayoutID = @LayoutID
		 and Name = @SourceName);
    IF NOT EXISTS (select @TaskID from TaskSource where StepTaskID = @TaskID
		 and DataSourceID = @DataSourceID)
    insert into TaskSource (StepTaskID, DataSourceID) values(@TaskID, @DataSourceID);
  end
END
GO
