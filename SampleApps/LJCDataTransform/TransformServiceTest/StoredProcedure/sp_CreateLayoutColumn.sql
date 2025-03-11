-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateLayoutColumn', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateLayoutColumn;  
GO  
CREATE PROCEDURE dbo.sp_CreateLayoutColumn
  @Name varchar(60),
	@Description varchar(80),
	@LayoutName varchar(60),
	@Sequence int,
	@DataTypeName varchar(60),
	@Length int,
	@IdentityKey bit,
	@PrimaryKey bit,
	@AllowNull bit
AS
BEGIN
	SET NOCOUNT ON;
	/*
  declare @Name varchar(60) = 'Address'
	declare @Description varchar(80) = 'Test address.'
	declare @LayoutName varchar(60) = 'AddressTextLayout'
	declare @Sequence int = 2
	declare @DataTypeName varchar(60) = 'String'
	declare @Length int = 80
	declare @IdentityKey bit = 0
	declare @PrimaryKey bit = 0
	declare @AllowNull bit = 1
	*/
  if (len(@Name) > 0 and len(@Description) > 0
	 and len(@LayoutName) > 0 and len(@DataTypeName) > 0
	 and @Sequence > 0)
  begin
    declare @SourceLayoutID int;
    declare @DataTypeID int;
		
    set @SourceLayoutID = (select SourceLayoutID from SourceLayout where Name = @LayoutName);
    set @DataTypeID = (select DataTypeID from DataType where Name = @DataTypeName);
    IF NOT EXISTS (select Name from LayoutColumn where SourceLayoutID = @SourceLayoutID
		 and Name = @Name)
    insert into LayoutColumn (Name, Description, SourceLayoutID, Sequence, DataTypeID
		  , Length, IdentityKey, PrimaryKey, AllowNull)
     values(@Name, @Description, @SourceLayoutID, @Sequence, @DataTypeID, @Length
		  , @IdentityKey, @PrimaryKey, @AllowNull);
  end
END
GO
