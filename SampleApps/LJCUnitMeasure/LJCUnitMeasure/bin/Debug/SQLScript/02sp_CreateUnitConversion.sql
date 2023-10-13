-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
use [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateUnitConversion', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.sp_CreateUnitConversion;  
GO

CREATE PROCEDURE [dbo].[sp_CreateUnitConversion]
  @FromUnitCode varchar(5),
  @ToUnitCode varchar(5),
  @Expression varchar(25)
AS
BEGIN
  SET NOCOUNT ON;

  declare @FromID int = (select ID from UnitMeasure where Code = @FromUnitCode);
  declare @ToID int = (select ID from UnitMeasure where Code = @ToUnitCode);

  if (@FromID > 0 and @ToID > 0)
    if not exists (select 1 from UnitConversion where FromUnitMeasureID = @FromID and ToUnitMeasureID = @ToID)
      insert into UnitConversion
        (FromUnitMeasureID, ToUnitMeasureID, Expression)
        values(@FromID, @ToID, @Expression);
END
GO
