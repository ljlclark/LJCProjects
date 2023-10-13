-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
use [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateUnitMeasure', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_CreateUnitMeasure;  
GO

CREATE PROCEDURE [dbo].[sp_CreateUnitMeasure]
  @UnitCategoryCode varchar(5),
  @UnitSystemCode varchar(5),
  @Code varchar(5),
  @Name varchar(25),
  @AltName varchar(25),
  @Description varchar(25)
AS
BEGIN
  SET NOCOUNT ON;

  declare @UnitCategoryID int = (select ID from UnitCategory where Code = @UnitCategoryCode);
  declare @UnitSystemID int = (select ID from UnitSystem where Code = @UnitSystemCode);
  declare @Sequence int = (select max(Sequence) from UnitMeasure) + 1;
  if (@Sequence is null)
    set @Sequence = 1;

  if (@UnitCategoryID > 0 and @UnitSystemID > 0)
    if not exists (select 1 from UnitMeasure where Code = @Code)
      insert into UnitMeasure
        (UnitCategoryID, UnitSystemID, Code, Name, AltName, Sequence, Description)
        values(@UnitCategoryID, @UnitSystemID, @Code, @Name, @AltName, @Sequence, @Description);
END
GO
