-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
use [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateUnitCategory', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_CreateUnitCategory;  
GO

CREATE PROCEDURE [dbo].[sp_CreateUnitCategory]
  @Code varchar(5),
  @Name varchar(25)
AS
BEGIN
  SET NOCOUNT ON;

  if not exists (select 1 from UnitCategory where Code = @Code)
    insert into UnitCategory
      (Code, Name)
      values(@Code, @Name);
END
GO
