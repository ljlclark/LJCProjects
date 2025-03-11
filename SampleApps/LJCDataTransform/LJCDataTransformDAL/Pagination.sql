--SQL 2000 Paging Method
use DataTransform;
DECLARE @Start INT
DECLARE @End INT
SELECT @Start = 0 ,@End = 10000

CREATE TABLE #processes (
  RowNumber INT IDENTITY(1,1),
  Name VARCHAR(60),
  Description VARCHAR(100)
)
  
INSERT INTO #processes (Name, Description)
SELECT Name, Description 
FROM Process 
ORDER BY Name, Description

SELECT Name, Description 
  FROM #processes
 WHERE RowNumber > @Start AND RowNumber <= @End
  
DROP TABLE #processes

  
GO

  
--SQL 2005/2008 Paging Method Using Derived Table
DECLARE @Start INT
DECLARE @End INT
SELECT @Start = 14000,@End = 14050

  
SELECT LastName, FirstName, EmailAddress
FROM (SELECT LastName, FirstName, EmailAddress,
      ROW_NUMBER() OVER (ORDER BY LastName, FirstName, EmailAddress) AS RowNumber
      FROM Employee) EmployeePage
WHERE RowNumber > @Start AND RowNumber <= @End
ORDER BY LastName, FirstName, EmailAddress
GO

  
--SQL 2005/2008 Paging Method Using CTE
DECLARE @Start INT
DECLARE @End INT
SELECT @Start = 14000,@End = 14050;

  
WITH EmployeePage AS
(SELECT LastName, FirstName, EmailAddress,
 ROW_NUMBER() OVER (ORDER BY LastName, FirstName, EmailAddress) AS RowNumber
 FROM Employee)
SELECT LastName, FirstName, EmailAddress
FROM EmployeePage
WHERE RowNumber > @Start AND RowNumber <= @End
ORDER BY LastName, FirstName, EmailAddress
GO

  
--SQL SERVER 2012
SELECT LastName, FirstName, EmailAddress
FROM Employee
ORDER BY LastName, FirstName, EmailAddress
OFFSET 14000 ROWS
FETCH NEXT 50 ROWS ONLY;
