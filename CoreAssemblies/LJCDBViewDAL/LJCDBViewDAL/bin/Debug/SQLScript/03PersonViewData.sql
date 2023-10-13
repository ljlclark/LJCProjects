/* 3PersonViewData.sql */
use [LJCData]

-- ViewTable
IF NOT EXISTS (select ID from ViewTable where Name = 'Person')
insert into ViewTable
 (Name, Description)
 values ('Person', 'Facility Manager Person Data');
go

-- DBView
IF NOT EXISTS (select ID from DBView where Name = 'PersonStandard')
insert into DBView
 (ViewTableID, Name, Description)
 values
  ((select ID from ViewTable where Name = 'Person'),
  'PersonStandard', 'The Person table standard view.');
go

-- ViewColumn
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
IF NOT EXISTS (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'FirstName')
insert into ViewColumn
 (DBViewID, Name, DataTypeName, Heading, PropertyName)
 values (@DBViewID, 'FirstName', 'String', 'First Name', null)

IF NOT EXISTS (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'LastName')
insert into ViewColumn
 (DBViewID, Name, DataTypeName, Heading, PropertyName)
 values (@DBViewID, 'LastName', 'String', 'Last Name', null)

IF NOT EXISTS (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'Code')
insert into ViewColumn
 (DBViewID, Name, DataTypeName, Heading, PropertyName)
 values (@DBViewID, 'Code', 'String', 'Type Code', null)

IF NOT EXISTS (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'Description')
insert into ViewColumn
 (DBViewID, Name, DataTypeName, Heading, PropertyName)
 values (@DBViewID, 'Description', 'String', 'Type Description', 'TypeDescription')
go

-- ViewJoin
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
IF NOT EXISTS (select ID from ViewJoin
 where DBViewID = @DBViewID and TableName = 'CodeType')
insert into ViewJoin
 (DBViewID, TableName, JoinType)
 values (@DBViewID, 'CodeType', 'left');
go

-- ViewJoinOn
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
declare @ViewJoinID int = (select ID from ViewJoin
 where DBViewID = @DBViewID and TableName = 'CodeType');
IF NOT EXISTS (select ID from ViewJoinOn
 where ViewJoinID = @ViewJoinID and FromColumnName = 'CodeType_Id')
insert into ViewJoinOn
 (ViewJoinID, FromColumnName, ToColumnName, JoinOnOperator)
 values (@ViewJoinID, 'CodeType_Id', 'Id', '=');
go

-- ViewJoinColumn
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
declare @ViewJoinID int = (select ID from ViewJoin
 where DBViewID = @DBViewID and TableName = 'CodeType');
declare @ViewColumnID int = (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'Code');
IF NOT EXISTS (select ViewColumnID from ViewJoinColumn
 where ViewJoinID = @ViewJoinID and ViewColumnID = @ViewColumnID)
insert into ViewJoinColumn
 (ViewJoinID, ViewColumnID) values (@ViewJoinID, @ViewColumnID);

set @ViewColumnID = (select ID from ViewColumn
 where DBViewID = @DBViewID and Name = 'Description')
IF NOT EXISTS (select ViewColumnID from ViewJoinColumn
 where ViewJoinID = @ViewJoinID and ViewColumnID = @ViewColumnID)
insert into ViewJoinColumn
 (ViewJoinID, ViewColumnID) values (@ViewJoinID, @ViewColumnID);
go

-- ViewFilter
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
IF NOT EXISTS (select ID from ViewFilter where DBViewID = @DBViewID)
insert into ViewFilter
 (DBViewID) values (@DBViewID);
go

-- ViewConditionSet
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
declare @ViewFilterID int = (select ID from ViewFilter where DBViewID = @DBViewID);
IF NOT EXISTS (select ID from ViewConditionSet where ViewFilterID = @ViewFilterID)
insert into ViewConditionSet
 (ViewFilterID, BooleanOperator) values (@ViewFilterID, 'and');
go

-- ViewCondition
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
declare @ViewFilterID int = (select ID from ViewFilter where DBViewID = @DBViewID);
declare @ViewConditionSetID int = (select ID from ViewConditionSet
 where ViewFilterID = @ViewFilterID);
IF NOT EXISTS (select ID from ViewCondition
 where ViewConditionSetID = @ViewConditionSetID and FirstValue = 'LastName')
insert into ViewCondition
 (ViewConditionSetID, FirstValue, SecondValue, ComparisonOperator)
 values (@ViewConditionSetID, 'LastName', '''%c%''', 'Like');
go

-- ViewOrderBy
declare @DBViewID int = (select ID from DBView where Name = 'PersonStandard');
IF NOT EXISTS (select ID from ViewOrderBy
 where DBViewID = @DBViewID and ColumnName = 'LastName')
insert into ViewOrderBy
 (DBViewID, ColumnName) values (@DBViewID, 'LastName');

IF NOT EXISTS (select ID from ViewOrderBy
 where DBViewID = @DBViewID and ColumnName = 'FirstName')
insert into ViewOrderBy
 (DBViewID, ColumnName) values (@DBViewID, 'FirstName');
go


