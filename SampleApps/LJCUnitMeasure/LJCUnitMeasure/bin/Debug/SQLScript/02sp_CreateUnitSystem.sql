-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
use [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_CreateUnitSystem', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_CreateUnitSystem;  
GO

CREATE PROCEDURE [dbo].[sp_CreateUnitSystem]
  @Code varchar(5),
  @Name varchar(25)
AS
BEGIN
  SET NOCOUNT ON;

  if not exists (select 1 from UnitSystem where Code = @Code)
    insert into UnitSystem
      (Code, Name)
      values(@Code, @Name);
END
GO
