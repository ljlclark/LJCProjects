/* 11DCLJCDBMessage.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select
  ID, DocClassGroupID, Name, Description, Sequence
from DocClass;
*/

declare @assemblyName nvarchar(60) = 'LJCDBMessage';
declare @headingName nvarchar(60) = 'Request';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbCondition'
  , 'Represents a filter condition.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditions'
  , 'Represents a collection of DbCondition objects.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbConditionSet'
  , 'Represents the conditions and properties. (E)', 3;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilter'
  , 'Represents a filter which is part of a where clause.', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbFilters'
  , 'Represents a collection of DbFilter objects. (E)', 4;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoin'
  , 'Represents a database table join.', 5;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOn'
  , 'Represents a Join On definition.', 6;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoinOns'
  , 'Represents a collection of join on definitions.', 7;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbJoins'
  , 'Represents a collection of table joins. (E)', 8;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRequest'
  , 'Represents a database request. (E)', 9;

set @headingName = 'Result';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbResult'
  , 'Represents a Request result. (R)', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRow'
  , 'Represents a result Row.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'DbRows'
  , 'Represents a collection of LJCNetCommon.DbValues.', 3;
