-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateTransformMap', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateTransformMap;  
GO  
CREATE PROCEDURE dbo.sp_CreateTransformMap
	@TaskTransformName varchar(60),
	@Sequence int,
	@SourceLayoutName varchar(60),
	@SourceColumnName varchar(60),
	@TargetLayoutName varchar(60),
	@TargetColumnName varchar(60),
	@MapTypeName varchar(60)
AS
BEGIN
	SET NOCOUNT ON;

  if (len(@TaskTransformName) > 0 and @Sequence > 0
	 and len(@SourceLayoutName) > 0 and len(@SourceColumnName) > 0
	 and len(@TargetLayoutName) > 0 and len(@TargetColumnName) > 0
	 and len(@MapTypeName) > 0)
  begin
    declare @TransformID int;
    declare @SourceLayoutID int;
    declare @SourceColumnID int;
    declare @TargetLayoutID int;
    declare @TargetColumnID int;
    declare @MapTypeID int;
		
    set @TransformID = (select TransformID from TaskTransform where Name = @TaskTransformName);
    set @SourceLayoutID = (select SourceLayoutID from SourceLayout where Name = @SourceLayoutName);
    set @SourceColumnID = (select LayoutColumnID from LayoutColumn where SourceLayoutID = @SourceLayoutID
     and Name = @SourceColumnName);
    set @TargetLayoutID = (select SourceLayoutID from SourceLayout where Name = @TargetLayoutName);
    set @TargetColumnID = (select LayoutColumnID from LayoutColumn where SourceLayoutID = @TargetLayoutID
     and Name = @TargetColumnName);
    set @MapTypeID = (select MapTypeID from MapType where Name = @MapTypeName);
    IF NOT EXISTS (select TransformMapID from TransformMap where TransformID = @TransformID
     and SourceColumnID = @SourceColumnID and TargetColumnID = @TargetColumnID)
	   insert into TransformMap
	    (TransformID, [Sequence], SourceColumnID, TargetColumnID, MapTypeID)
	    values (@TransformID, @Sequence, @SourceColumnID, @TargetColumnID, @MapTypeID);
  end
END
GO
