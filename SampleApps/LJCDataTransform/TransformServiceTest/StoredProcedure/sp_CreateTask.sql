-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateTask', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateTask;  
GO  
CREATE PROCEDURE dbo.sp_CreateTask
  @Name varchar(60),
	@Description varchar(80),
	@StepName varchar(60),
	@Sequence int,
	@TaskTypeName varchar(60),
	@ActionItemName varchar(100),
	@TaskStatusName varchar(60)
AS
BEGIN
	SET NOCOUNT ON;

  if (len(@Name) > 0 and len(@Description) > 0
	 and len(@StepName) > 0 and @Sequence > 0
	 and len(@TaskTypeName) > 0 and len(@ActionItemName) > 0
	 and len(@TaskStatusName) > 0)
  begin
    declare @StepID int;
    declare @TaskTypeID int
    declare @TaskStatusID int;
		
    set @StepID = (select StepID from Step where Name = @StepName);
    set @TaskTypeID = (select TaskTypeID from TaskType where Name = @TaskTypeName);
    set @TaskStatusID = (select TaskStatusID from TaskStatus where Name = @TaskStatusName);
    IF NOT EXISTS (select Name from StepTask where StepID = @StepID and Name = @Name)
    insert into StepTask (Name, Description, StepID, Sequence, TaskTypeID, ActionItemName
		 , TaskStatusID)
     values(@Name, @Description, @StepID, 1, @TaskTypeID, @ActionItemName, @TaskStatusID);
  end
END
GO
