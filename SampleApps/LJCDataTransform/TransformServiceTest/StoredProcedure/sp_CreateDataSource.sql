-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateDataSource', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateDataSource;  
GO  
CREATE PROCEDURE dbo.sp_CreateDataSource
  @Name varchar(60),
	@Description varchar(80),
	@SourceTypeName varchar(60),
	@DataConfigName varchar(60),
	@SourceItemName varchar(80),
	@LayoutName varchar(60),
	@SourceStatusName varchar(60)
AS
BEGIN
	SET NOCOUNT ON;

  if (len(@Name) > 0 and len(@Description) > 0
	 and len(@SourceTypeName) > 0 and len(@LayoutName) > 0
	 and len(@SourceStatusName) > 0)
  begin
    declare @SourceTypeID int;
    declare @SourceLayoutID int;
    declare @SourceStatusID int;
		
    set @SourceTypeID = (select SourceTypeID from SourceType
		 where Name = @SourceTypeName);
    set @SourceLayoutID = (select SourceLayoutID from SourceLayout where Name = @LayoutName);
    set @SourceStatusID = (select SourceStatusID from SourceStatus
		 where Name = @SourceStatusName);
    IF NOT EXISTS (select Name from DataSource where Name = @Name)
     insert into DataSource (Name, Description, SourceTypeID, DataConfigName
		  , SourceItemName, SourceLayoutID, SourceStatusID)
      values(@Name, @Description, @SourceTypeID, @DataConfigName, @SourceItemName
			, @SourceLayoutID, @SourceStatusID);
  end
END
GO
