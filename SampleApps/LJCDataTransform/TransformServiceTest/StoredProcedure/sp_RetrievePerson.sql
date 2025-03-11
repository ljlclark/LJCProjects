SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'dbo.sp_RetrievePerson', 'P' ) IS NOT NULL   
  DROP PROCEDURE dbo.sp_RetrievePerson;  
GO  
CREATE PROCEDURE dbo.sp_RetrievePerson
  @ID int
AS
BEGIN
  SET NOCOUNT ON;
  select
    Person_ID,
    Name
  from Person
  where Person_ID = @ID;
END
GO
