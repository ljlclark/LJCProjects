/* Copyright(c) Lester J. Clark and Contributors. */
/* Licensed under the MIT License. */
/* sp_DataColumnData.sql */
USE [LJCDataUtility]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('[dbo].[sp_DataColumnData]', N'p')
 IS NOT NULL
  DROP PROCEDURE [dbo].[sp_DataColumnData];
GO
CREATE PROCEDURE [dbo].[sp_DataColumnData]
AS
BEGIN
EXEC sp_DataColumnAdd DataModule
 , 'ID', 'The Primary key.'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd DataModule
 , 'Name', 'The Unique Name value.'
 , 2, nvarchar, 0, 0, 60, False
EXEC sp_DataColumnAdd DataModule
 , 'Description', 'The Description text.'
 , 3, nvarchar, 0, 0, 80, True
EXEC sp_DataColumnAdd DataTable
 , 'ID', 'The Primary key.'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd DataTable
 , 'DataModuleID', 'The Foreign Key.'
 , 2, bigint, 0, 0, -1, False
EXEC sp_DataColumnAdd DataTable
 , 'Name', 'The Unique Name value.'
 , 3, nvarchar, 0, 0, 60, False
EXEC sp_DataColumnAdd DataTable
 , 'Description', 'The Description text.'
 , 4, nvarchar, 0, 0, 80, True
EXEC sp_DataColumnAdd DataColumn
 , 'ID', 'The Primary key.'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'DataTableID', 'The Foreign Key.'
 , 2, bigint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'Description', 'The Description text.'
 , 4, nvarchar, -1, -1, 80, True
EXEC sp_DataColumnAdd DataColumn
 , 'IdentityStart', 'The identity start value.'
 , 10, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'IdentityIncrement', 'The identity increment value.'
 , 11, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'MaxLength', 'The max length value.'
 , 7, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'AllowNull', 'Allow null.'
 , 8, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DataKey
 , 'ID', 'The Primary key.'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd DataKey
 , 'DataTableID', 'The Foreign Key.'
 , 2, bigint, 0, 0, -1, False
EXEC sp_DataColumnAdd DataKey
 , 'KeyType', 'The Key type value.'
 , 4, smallint, 0, 0, -1, False
EXEC sp_DataColumnAdd DataKey
 , 'Name', 'The Key name.'
 , 3, nvarchar, 0, 0, 60, False
EXEC sp_DataColumnAdd DataKey
 , 'SourceColumnName', 'The Source column name.'
 , 5, nvarchar, 0, 0, 60, True
EXEC sp_DataColumnAdd DataKey
 , 'TargetTableName', 'The Target table name.'
 , 6, nvarchar, 0, 0, 60, True
EXEC sp_DataColumnAdd DataKey
 , 'TargetColumnName', 'The Target column name.'
 , 7, nvarchar, 0, 0, 60, True
EXEC sp_DataColumnAdd DataKey
 , 'IsClustered', 'The Clustered indicator.'
 , 8, bit, 0, 0, -1, False
EXEC sp_DataColumnAdd DataKey
 , 'IsAscending', 'The Ascending indicator.'
 , 9, bit, 0, 0, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'Sequence', 'The Sequence value,'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'NewName', 'The New Name value.'
 , 12, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd DataColumn
 , 'NewMaxLength', 'The New Max Length.'
 , 13, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataColumn
 , 'Name', 'The unique Name value.'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataColumn
 , 'DefaultValue', 'The Default value.'
 , 9, nvarchar, -1, -1, 80, True
EXEC sp_DataColumnAdd DataColumn
 , 'TypeName', 'The SQL data type name.'
 , 6, nvarchar, -1, -1, 20, False
EXEC sp_DataColumnAdd City
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd City
 , 'ProvinceID', 'ProvinceID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd City
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd City
 , 'Description', 'Description'
 , 4, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd City
 , 'CityFlag', 'CityFlag'
 , 5, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd City
 , 'ZipCode', 'ZipCode'
 , 6, char, -1, -1, 4, True
EXEC sp_DataColumnAdd City
 , 'District', 'District'
 , 7, smallint, -1, -1, -1, True
EXEC sp_DataColumnAdd CitySection
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd CitySection
 , 'CityID', 'CityID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd CitySection
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd CitySection
 , 'Description', 'Description'
 , 4, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd CitySection
 , 'ZoneType', 'ZoneType'
 , 5, varchar, -1, -1, 25, True
EXEC sp_DataColumnAdd CitySection
 , 'Contact', 'Contact'
 , 6, varchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Region
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Region
 , 'Number', 'Number'
 , 2, nvarchar, -1, -1, 5, False
EXEC sp_DataColumnAdd Region
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Region
 , 'Description', 'Description'
 , 4, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd Province
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Province
 , 'RegionID', 'RegionID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Province
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Province
 , 'Description', 'Description'
 , 4, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd Province
 , 'Abbreviation', 'Abbreviation'
 , 5, char, -1, -1, 3, True
EXEC sp_DataColumnAdd Equipment
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Equipment
 , 'UnitID', 'UnitID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Equipment
 , 'Code', 'Code'
 , 3, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd Equipment
 , 'Description', 'Description'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Equipment
 , 'CodeTypeID', 'CodeTypeID'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Equipment
 , 'Make', 'Make'
 , 6, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Equipment
 , 'Model', 'Model'
 , 7, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Equipment
 , 'SerialNumber', 'SerialNumber'
 , 8, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Facility
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Facility
 , 'Code', 'Code'
 , 2, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd Facility
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Facility
 , 'CodeTypeID', 'CodeTypeID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Fixture
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Fixture
 , 'UnitID', 'UnitID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Fixture
 , 'Code', 'Code'
 , 3, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd Fixture
 , 'Description', 'Description'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Fixture
 , 'CodeTypeID', 'CodeTypeID'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Fixture
 , 'Make', 'Make'
 , 6, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Fixture
 , 'Model', 'Model'
 , 7, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Fixture
 , 'SerialNumber', 'SerialNumber'
 , 8, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Unit
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Unit
 , 'FacilityID', 'FacilityID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Unit
 , 'Code', 'Code'
 , 3, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd Unit
 , 'Description', 'Description'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Unit
 , 'CodeTypeID', 'CodeTypeID'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Unit
 , 'Beds', 'Beds'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd Unit
 , 'Baths', 'Baths'
 , 7, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd Unit
 , 'Phone', 'Phone'
 , 8, nvarchar, -1, -1, 18, True
EXEC sp_DataColumnAdd Unit
 , 'Extension', 'Extension'
 , 9, nchar, -1, -1, 4, True
EXEC sp_DataColumnAdd Business
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Business
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Business
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Business
 , 'CodeTypeID', 'CodeTypeID'
 , 4, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Business
 , 'EffectiveDate', 'EffectiveDate'
 , 5, datetime, -1, -1, -1, True
EXEC sp_DataColumnAdd Business
 , 'ExpirationDate', 'ExpirationDate'
 , 6, datetime, -1, -1, -1, True
EXEC sp_DataColumnAdd Business
 , 'Phone', 'Phone'
 , 7, nvarchar, -1, -1, 18, True
EXEC sp_DataColumnAdd Business
 , 'Extension', 'Extension'
 , 8, nchar, -1, -1, 4, True
EXEC sp_DataColumnAdd Business
 , 'Fax', 'Fax'
 , 9, nvarchar, -1, -1, 18, True
EXEC sp_DataColumnAdd BusinessAddress
 , 'BusinessID', 'BusinessID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd BusinessAddress
 , 'AddressID', 'AddressID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Address
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Address
 , 'RegionID', 'RegionID'
 , 2, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Address
 , 'ProvinceID', 'ProvinceID'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Address
 , 'CityID', 'CityID'
 , 4, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Address
 , 'CitySectionID', 'CitySectionID'
 , 5, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Address
 , 'Street', 'Street'
 , 6, nvarchar, -1, -1, 45, True
EXEC sp_DataColumnAdd Address
 , 'PostalCode', 'PostalCode'
 , 7, nvarchar, -1, -1, 10, True
EXEC sp_DataColumnAdd Address
 , 'CodeTypeID', 'CodeTypeID'
 , 8, int, -1, -1, -1, False
EXEC sp_DataColumnAdd CodeType
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd CodeType
 , 'Code', 'Code'
 , 2, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd CodeType
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd CodeType
 , 'CodeTypeClassID', 'CodeTypeClassID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd CodeTypeClass
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd CodeTypeClass
 , 'Code', 'Code'
 , 2, nvarchar, -1, -1, 25, False
EXEC sp_DataColumnAdd CodeTypeClass
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Person
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Person
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Person
 , 'PrincipleFlag', 'PrincipleFlag'
 , 3, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewColumn
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewColumn
 , 'ViewDataID', 'ViewDataID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewColumn
 , 'Sequence', 'Sequence'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ViewColumn
 , 'ColumnName', 'ColumnName'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewColumn
 , 'PropertyName', 'PropertyName'
 , 5, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewColumn
 , 'RenameAs', 'RenameAs'
 , 6, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewColumn
 , 'Caption', 'Caption'
 , 7, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewColumn
 , 'DataTypeName', 'DataTypeName'
 , 8, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewColumn
 , 'Value', 'Value'
 , 9, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewColumn
 , 'Width', 'Width'
 , 10, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ViewColumn
 , 'IsPrimaryKey', 'IsPrimaryKey'
 , 11, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewCondition
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewCondition
 , 'ViewConditionSetID', 'ViewConditionSetID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewCondition
 , 'FirstValue', 'FirstValue'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewCondition
 , 'SecondValue', 'SecondValue'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewCondition
 , 'ComparisonOperator', 'ComparisonOperator'
 , 5, nvarchar, -1, -1, 10, True
EXEC sp_DataColumnAdd ViewConditionSet
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewConditionSet
 , 'ViewFilterID', 'ViewFilterID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewConditionSet
 , 'BooleanOperator', 'BooleanOperator'
 , 3, nvarchar, -1, -1, 3, False
EXEC sp_DataColumnAdd ViewData
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewData
 , 'ViewTableID', 'ViewTableID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewData
 , 'Name', 'Name'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewData
 , 'Description', 'Description'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewFilter
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewFilter
 , 'ViewDataID', 'ViewDataID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewFilter
 , 'Name', 'Name'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewFilter
 , 'BooleanOperator', 'BooleanOperator'
 , 4, nvarchar, -1, -1, 3, False
EXEC sp_DataColumnAdd ViewGridColumn
 , 'ViewDataID', 'ViewDataID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewGridColumn
 , 'ViewColumnID', 'ViewColumnID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewGridColumn
 , 'Sequence', 'Sequence'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ViewGridColumn
 , 'Caption', 'Caption'
 , 4, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewGridColumn
 , 'Width', 'Width'
 , 5, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ViewJoin
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewJoin
 , 'ViewDataID', 'ViewDataID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewJoin
 , 'TableName', 'TableName'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewJoin
 , 'JoinType', 'JoinType'
 , 4, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoin
 , 'TableAlias', 'TableAlias'
 , 5, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'ViewJoinID', 'ViewJoinID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'ColumnName', 'ColumnName'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'PropertyName', 'PropertyName'
 , 4, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'RenameAs', 'RenameAs'
 , 5, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'Caption', 'Caption'
 , 6, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'DataTypeName', 'DataTypeName'
 , 7, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewJoinColumn
 , 'Value', 'Value'
 , 8, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ViewJoinOn
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewJoinOn
 , 'ViewJoinID', 'ViewJoinID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewJoinOn
 , 'FromColumnName', 'FromColumnName'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewJoinOn
 , 'ToColumnName', 'ToColumnName'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewJoinOn
 , 'JoinOnOperator', 'JoinOnOperator'
 , 5, nvarchar, -1, -1, 5, True
EXEC sp_DataColumnAdd ViewOrderBy
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewOrderBy
 , 'ViewDataID', 'ViewDataID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ViewOrderBy
 , 'ColumnName', 'ColumnName'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewTable
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ViewTable
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ViewTable
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataTable
 , 'Sequence', 'The Sequence value.'
 , 5, int, 0, 0, -1, False
EXEC sp_DataColumnAdd DataTable
 , 'NewName', 'NewName'
 , 6, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd UnitPerson
 , 'UnitID', 'UnitID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitPerson
 , 'PersonID', 'PersonID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitPerson
 , 'BeginDate', 'BeginDate'
 , 3, DataTime, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitPerson
 , 'EndDate', 'EndDate'
 , 4, DateTime, -1, -1, -1, True
EXEC sp_DataColumnAdd PersonRelation
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, True
EXEC sp_DataColumnAdd PersonRelation
 , 'PersonID', 'PersonID'
 , 2, int, 0, 0, -1, True
EXEC sp_DataColumnAdd PersonRelation
 , 'RelationID', 'RelationID'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd PersonRelation
 , 'RelationCodeTypeID', 'RelationCodeTypeID'
 , 4, int, -1, -1, -1, True
EXEC sp_DataColumnAdd PersonAddress
 , 'PersonID', 'PersonID'
 , 1, int, -1, -1, -1, True
EXEC sp_DataColumnAdd PersonAddress
 , 'AddressID', 'AddressID'
 , 2, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'Description', 'Description'
 , 2, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Account
 , 'PersonID', 'PersonID'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'BusinessID', 'BusinessID'
 , 4, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'GroupNumber', 'GroupNumber'
 , 6, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Account
 , 'PlanNumber', 'PlanNumber'
 , 7, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd Account
 , 'EffectiveDate', 'EffectiveDate'
 , 8, DateTime, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'ExpirationDate', 'ExpirationDate'
 , 9, DateTime, -1, -1, -1, True
EXEC sp_DataColumnAdd Account
 , 'IDNumber', 'IDNumber'
 , 5, nvarchar, -1, -1, 25, True
EXEC sp_DataColumnAdd BusinessPerson
 , 'BusinessID', 'BusinessID'
 , 1, int, -1, -1, -1, True
EXEC sp_DataColumnAdd BusinessPerson
 , 'PersonID', 'PersonID'
 , 2, int, -1, -1, -1, True
EXEC sp_DataColumnAdd DataProcess
 , 'DataProcessID', 'DataProcessID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DataProcess
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataProcess
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataProcess
 , 'ProcessStatusID', 'ProcessStatusID'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataSource
 , 'DataSourceID', 'DataSourceID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DataSource
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataSource
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataSource
 , 'SourceTypeID', 'SourceTypeID'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataSource
 , 'DataConfigName', 'DataConfigName'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataSource
 , 'SourceItemName', 'SourceItemName'
 , 6, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataSource
 , 'SourceLayoutID', 'SourceLayoutID'
 , 7, int, -1, -1, -1, False
EXEC sp_DataColumnAdd DataSource
 , 'SourceStatusID', 'SourceStatusID'
 , 8, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DataType
 , 'DataTypeID', 'DataTypeID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DataType
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataType
 , 'SQLName', 'SQLName'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DataType
 , 'Description', 'Description'
 , 4, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LayoutColumn
 , 'LayoutColumnID', 'LayoutColumnID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LayoutColumn
 , 'SourceLayoutID', 'SourceLayoutID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'Sequence', 'Sequence'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'DataTypeID', 'DataTypeID'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'Length', 'Length'
 , 7, int, -1, -1, -1, True
EXEC sp_DataColumnAdd LayoutColumn
 , 'IdentityKey', 'IdentityKey'
 , 8, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'PrimaryKey', 'PrimaryKey'
 , 9, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd LayoutColumn
 , 'AllowNull', 'AllowNull'
 , 10, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ProcessGroup
 , 'ProcessGroupID', 'ProcessGroupID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd ProcessGroup
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ProcessGroup
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd ProcessGroupProcess
 , 'ProcessGroupID', 'ProcessGroupID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ProcessGroupProcess
 , 'DataProcessID', 'DataProcessID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ProcessGroupProcess
 , 'Sequence', 'Sequence'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ProcessStatus
 , 'ProcessStatusID', 'ProcessStatusID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd ProcessStatus
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ProcessStatus
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd SourceType
 , 'SourceTypeID', 'SourceTypeID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd SourceType
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd SourceType
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd Step
 , 'StepID', 'StepID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd Step
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Step
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd Step
 , 'DataProcessID', 'DataProcessID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Step
 , 'Sequence', 'Sequence'
 , 5, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd Step
 , 'StatusID', 'StatusID'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd StepTask
 , 'StepTaskID', 'StepTaskID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd StepTask
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd StepTask
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd StepTask
 , 'StepID', 'StepID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd StepTask
 , 'Sequence', 'Sequence'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd StepTask
 , 'TaskTypeID', 'TaskTypeID'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd StepTask
 , 'ActionItemName', 'ActionItemName'
 , 7, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd StepTask
 , 'TaskStatusID', 'TaskStatusID'
 , 8, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskSource
 , 'StepTaskID', 'StepTaskID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskSource
 , 'DataSourceID', 'DataSourceID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskStatus
 , 'TaskStatusID', 'TaskStatusID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd TaskStatus
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd TaskStatus
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd TaskTransform
 , 'TransformID', 'TransformID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd TaskTransform
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd TaskTransform
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd TaskTransform
 , 'StepTaskID', 'StepTaskID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskTransform
 , 'DataSourceID', 'DataSourceID'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskTransform
 , 'TargetID', 'TargetID'
 , 6, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TaskType
 , 'TaskTypeID', 'TaskTypeID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd TaskType
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd TaskType
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd TransformMap
 , 'TransformMapID', 'TransformMapID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd TransformMap
 , 'TransformID', 'TransformID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMap
 , 'Sequence', 'Sequence'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMap
 , 'SourceColumnID', 'SourceColumnID'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMap
 , 'TargetColumnID', 'TargetColumnID'
 , 5, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMap
 , 'MapTypeID', 'MapTypeID'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMatch
 , 'TransformMatchID', 'TransformMatchID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd TransformMatch
 , 'TransformID', 'TransformID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMatch
 , 'Sequence', 'Sequence'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMatch
 , 'SourceColumnID', 'SourceColumnID'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd TransformMatch
 , 'TargetColumnID', 'TargetColumnID'
 , 5, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd SourceLayout
 , 'SourceLayoutID', 'SourceLayoutID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd SourceLayout
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd SourceLayout
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataInstance
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DataInstance
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Individual
 , 'ID', 'ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'DBID', 'DBID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'Parent1ID', 'Parent1ID'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'Parent1DBID', 'Parent1DBID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'Parent2ID', 'Parent2ID'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'Parent2DBID', 'Parent2DBID'
 , 6, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Individual
 , 'Sex', 'Sex'
 , 7, char, -1, -1, 1, False
EXEC sp_DataColumnAdd Individual
 , 'LastName', 'LastName'
 , 8, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd Individual
 , 'FirstName', 'FirstName'
 , 9, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'MiddleName', 'MiddleName'
 , 10, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthVillage', 'BirthVillage'
 , 11, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthCounty', 'BirthCounty'
 , 13, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthCity', 'BirthCity'
 , 12, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthCountry', 'BirthCountry'
 , 15, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthProvince', 'BirthProvince'
 , 14, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'DeathVillage', 'DeathVillage'
 , 16, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'DeathCity', 'DeathCity'
 , 17, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'DeathCounty', 'DeathCounty'
 , 18, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'DeathProvince', 'DeathProvince'
 , 19, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'DeathCountry', 'DeathCountry'
 , 20, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Individual
 , 'BirthYear', 'BirthYear'
 , 21, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'BirthMonth', 'BirthMonth'
 , 22, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'BirthDay', 'BirthDay'
 , 23, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'DeathYear', 'DeathYear'
 , 24, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'DeathMonth', 'DeathMonth'
 , 25, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'DeathDay', 'DeathDay'
 , 26, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Individual
 , 'CreateMultiGen', 'CreateMultiGen'
 , 27, tinyint, -1, -1, -1, True
EXEC sp_DataColumnAdd Partner
 , 'Partner1ID', 'Partner1ID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'Partner1DBID', 'Partner1DBID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'Partner2ID', 'Partner2ID'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'Partner2DBID', 'Partner2DBID'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'BeginVillage', 'BeginVillage'
 , 5, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'BeginCity', 'BeginCity'
 , 6, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'BeginCounty', 'BeginCounty'
 , 7, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'BeginProvince', 'BeginProvince'
 , 8, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'BeginCountry', 'BeginCountry'
 , 9, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'EndVillage', 'EndVillage'
 , 10, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'EndCity', 'EndCity'
 , 11, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'EndCounty', 'EndCounty'
 , 12, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'EndProvince', 'EndProvince'
 , 13, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'EndCountry', 'EndCountry'
 , 14, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd Partner
 , 'BeginYear', 'BeginYear'
 , 15, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Partner
 , 'BeginMonth', 'BeginMonth'
 , 16, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'BeginDay', 'BeginDay'
 , 17, int, -1, -1, -1, False
EXEC sp_DataColumnAdd Partner
 , 'EndYear', 'EndYear'
 , 18, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Partner
 , 'EndMonth', 'EndMonth'
 , 19, int, -1, -1, -1, True
EXEC sp_DataColumnAdd Partner
 , 'EndDay', 'EndDay'
 , 20, int, -1, -1, -1, True
EXEC sp_DataColumnAdd AppManagerUser
 , 'Id', 'Id'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd AppManagerUser
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd AppManagerUser
 , 'UserID', 'UserID'
 , 3, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd AppProgram
 , 'Id', 'Id'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd AppProgram
 , 'FileName', 'FileName'
 , 2, nvarchar, -1, -1, 40, False
EXEC sp_DataColumnAdd AppProgram
 , 'Title', 'Title'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd AppModule
 , 'Id', 'Id'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd AppModule
 , 'AppProgram_ID', 'AppProgram_ID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd AppModule
 , 'TypeName', 'TypeName'
 , 3, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd AppModule
 , 'Title', 'Title'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd UserAppProgram
 , 'AppManagerUser_Id', 'AppManagerUser_Id'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppProgram
 , 'AppProgram_Id', 'AppProgram_Id'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppProgram
 , 'Active', 'Active'
 , 3, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppModule
 , 'AppManagerUser_Id', 'AppManagerUser_Id'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppModule
 , 'AppProgram_Id', 'AppProgram_Id'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppModule
 , 'AppModule_Id', 'AppModule_Id'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UserAppModule
 , 'Active', 'Active'
 , 4, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocApp
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DocApp
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocApp
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocApp
 , 'RootPath', 'RootPath'
 , 4, varchar, -1, -1, 100, False
EXEC sp_DataColumnAdd DocAppFile
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DocAppFile
 , 'DocAppID', 'DocAppID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppFile
 , 'Path', 'Path'
 , 3, nvarchar, -1, -1, 300, True
EXEC sp_DataColumnAdd DocAppFile
 , 'FileName', 'FileName'
 , 4, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAppFile
 , 'Description', 'Description'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAppFile
 , 'CreateDate', 'CreateDate'
 , 6, datetime, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppFile
 , 'CreateUserID', 'CreateUserID'
 , 7, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocAppFile
 , 'Workstation', 'Workstation'
 , 8, nvarchar, -1, -1, 100, False
EXEC sp_DataColumnAdd DocAppFile
 , 'LocalPath', 'LocalPath'
 , 9, bigint, -1, -1, 300, False
EXEC sp_DataColumnAdd DocAppFile
 , 'LocalFileName', 'LocalFileName'
 , 10, bigint, -1, -1, 100, False
EXEC sp_DataColumnAdd DocAppFieldBehavior
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DocAppFieldBehavior
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocAppFieldBehavior
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAppField
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'DocAppID', 'DocAppID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'Sequence', 'Sequence'
 , 3, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'ColumnName', 'ColumnName'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocAppField
 , 'Caption', 'Caption'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAppField
 , 'Description', 'Description'
 , 6, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAppField
 , 'DocAppFieldBehaviorID', 'DocAppFieldBehaviorID'
 , 7, int, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'DataTypeName', 'DataTypeName'
 , 8, nvarchar, -1, -1, 30, True
EXEC sp_DataColumnAdd DocAppField
 , 'MaxLength', 'MaxLength'
 , 9, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'AllowDBNull', 'AllowDBNull'
 , 10, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'AutoIncrement', 'AutoIncrement'
 , 11, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAppField
 , 'DefaultValue', 'DefaultValue'
 , 12, nvarchar, -1, -1, 30, True
EXEC sp_DataColumnAdd DocAppField
 , 'Unique', 'Unique'
 , 13, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'DocAppID', 'DocAppID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'DocAppFileID', 'DocAppFileID'
 , 3, int, -1, -1, -1, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'ProvinceID', 'ProvinceID'
 , 4, bigint, -1, -1, 100, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'City', 'City'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'CitySection', 'CitySection'
 , 6, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'SaleDate', 'SaleDate'
 , 7, datetime, -1, -1, -1, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'LotCode', 'LotCode'
 , 8, nvarchar, -1, -1, 40, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'LotDescription', 'LotDescription'
 , 9, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'SaleValue', 'SaleValue'
 , 10, decimal, -1, -1, -1, False
EXEC sp_DataColumnAdd LandTitleValue
 , 'OwnerLastName', 'OwnerLastName'
 , 11, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'OwnerFirstName', 'OwnerFirstName'
 , 12, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'LastUpdateDate', 'LastUpdateDate'
 , 13, date, -1, -1, -1, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'UpdateUserID', 'UpdateUserID'
 , 14, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'Workstation', 'Workstation'
 , 15, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'LocalPath', 'LocalPath'
 , 16, nvarchar, -1, -1, 300, True
EXEC sp_DataColumnAdd LandTitleValue
 , 'LocalFileName', 'LocalFileName'
 , 17, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd DocAssemblyGroup
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocAssemblyGroup
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocAssemblyGroup
 , 'Heading', 'Heading'
 , 3, nvarchar, -1, -1, 100, False
EXEC sp_DataColumnAdd DocAssemblyGroup
 , 'Sequence', 'Sequence'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAssemblyGroup
 , 'ActiveFlag', 'ActiveFlag'
 , 5, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAssembly
 , 'ID', 'ID'
 , 1, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAssembly
 , 'DocAssemblyGroupID', 'DocAssemblyGroupID'
 , 2, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAssembly
 , 'Description', 'Description'
 , 3, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocAssembly
 , 'Sequence', 'Sequence'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocAssembly
 , 'FileSpec', 'FileSpec'
 , 5, nvarchar, -1, -1, -1, True
EXEC sp_DataColumnAdd DocAssembly
 , 'MainImage', 'MainImage'
 , 6, nvarchar, -1, -1, -1, True
EXEC sp_DataColumnAdd DocAssembly
 , 'ActiveFlag', 'ActiveFlag'
 , 7, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroupHeading
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocClassGroupHeading
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocClassGroupHeading
 , 'Heading', 'Heading'
 , 3, nvarchar, -1, -1, 100, False
EXEC sp_DataColumnAdd DocClassGroupHeading
 , 'Sequence', 'Sequence'
 , 4, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'ID', 'ID'
 , 1, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'DocAssemblyID', 'DocAssemblyID'
 , 2, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'DocClassGroupHeadingID', 'DocClassGroupHeadingID'
 , 3, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'HeadingName', 'HeadingName'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'HeadingTextCustom', 'HeadingTextCustom'
 , 5, nvarchar, -1, -1, -1, True
EXEC sp_DataColumnAdd DocClassGroup
 , 'Sequence', 'Sequence'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClassGroup
 , 'ActiveFlag', 'ActiveFlag'
 , 7, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClass
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocClass
 , 'DocAssemblyID', 'DocAssemblyID'
 , 2, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClass
 , 'DocClassGroupID', 'DocClassGroupID'
 , 3, bigint, -1, -1, -1, True
EXEC sp_DataColumnAdd DocClass
 , 'Name', 'Name'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocClass
 , 'Description', 'Description'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocClass
 , 'Sequence', 'Sequence'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocClass
 , 'ActiveFlag', 'ActiveFlag'
 , 7, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethodGroupHeading
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocMethodGroupHeading
 , 'Name', 'Name'
 , 2, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocMethodGroupHeading
 , 'Sequence', 'Sequence'
 , 3, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethodGroup
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocMethodGroup
 , 'DocClassID', 'DocClassID'
 , 2, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethodGroup
 , 'DocMethodGroupHeadingID', 'DocMethodGroupHeadingID'
 , 3, smallint, -1, -1, -1, True
EXEC sp_DataColumnAdd DocMethodGroup
 , 'HeadingName', 'HeadingName'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocMethodGroup
 , 'HeadingCustomText', 'HeadingCustomText'
 , 5, nvarchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DocMethodGroup
 , 'Sequence', 'Sequence'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethodGroup
 , 'ActiveFlag', 'ActiveFlag'
 , 7, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethod
 , 'ID', 'ID'
 , 1, smallint, 1, 1, -1, False
EXEC sp_DataColumnAdd DocMethod
 , 'DocClassID', 'DocClassID'
 , 2, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethod
 , 'DocMethodGroupID', 'DocMethodGroupID'
 , 3, smallint, -1, -1, -1, True
EXEC sp_DataColumnAdd DocMethod
 , 'Name', 'Name'
 , 4, nvarchar, -1, -1, 60, False
EXEC sp_DataColumnAdd DocMethod
 , 'Description', 'Description'
 , 5, nvarchar, -1, -1, 100, False
EXEC sp_DataColumnAdd DocMethod
 , 'Sequence', 'Sequence'
 , 6, smallint, -1, -1, -1, False
EXEC sp_DataColumnAdd DocMethod
 , 'OverloadName', 'OverloadName'
 , 7, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd DocMethod
 , 'ActiveFlag', 'ActiveFlag'
 , 8, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ID', 'ID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlDetail
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlDetail
 , 'DataConfigName', 'DataConfigName'
 , 4, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlDetail
 , 'TableName', 'TableName'
 , 5, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlDetail
 , 'UserID', 'UserID'
 , 6, nvarchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ControlDetail
 , 'DataValueCount', 'DataValueCount'
 , 7, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ColumnRowsLimit', 'ColumnRowsLimit'
 , 8, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'PageColumnsLimit', 'PageColumnsLimit'
 , 9, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'CharacterPixels', 'CharacterPixels'
 , 10, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'MaxControlCharacters', 'MaxControlCharacters'
 , 11, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'BorderHorizontal', 'BorderHorizontal'
 , 12, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'BorderVertical', 'BorderVertical'
 , 13, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ControlRowSpacing', 'ControlRowSpacing'
 , 14, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ControlRowHeight', 'ControlRowHeight'
 , 15, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ControlsHeight', 'ControlsHeight'
 , 16, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ControlsWidth', 'ControlsWidth'
 , 17, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlDetail
 , 'ColumnRowCount', 'ColumnRowCount'
 , 18, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlData
 , 'ID', 'ID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd ControlData
 , 'ControlDetailID', 'ControlDetailID'
 , 2, bigint, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlData
 , 'AllowDBNull', 'AllowDBNull'
 , 3, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlData
 , 'AutoIncrement', 'AutoIncrement'
 , 4, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlData
 , 'Caption', 'Caption'
 , 5, varchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ControlData
 , 'ColumnName', 'ColumnName'
 , 6, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlData
 , 'DataTypeName', 'DataTypeName'
 , 7, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlData
 , 'MaxLength', 'MaxLength'
 , 8, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ControlData
 , 'Position', 'Position'
 , 9, int, -1, -1, -1, True
EXEC sp_DataColumnAdd ControlData
 , 'PropertyNamr', 'PropertyNamr'
 , 10, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlData
 , 'RenameAs', 'RenameAs'
 , 11, varchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ControlData
 , 'SQLTypeName', 'SQLTypeName'
 , 12, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlData
 , 'VAlue', 'VAlue'
 , 13, varchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ControlTab
 , 'ID', 'ID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd ControlTab
 , 'ControlDetailID', 'ControlDetailID'
 , 2, bigint, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlTab
 , 'TabIndex', 'TabIndex'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlTab
 , 'Caption', 'Caption'
 , 4, varchar, -1, -1, 40, False
EXEC sp_DataColumnAdd ControlTab
 , 'Description', 'Description'
 , 5, varchar, -1, -1, 60, True
EXEC sp_DataColumnAdd ControlColumn
 , 'ID', 'ID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd ControlColumn
 , 'ControlTabID', 'ControlTabID'
 , 2, bigint, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlColumn
 , 'ColumnIndex', 'ColumnIndex'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlColumn
 , 'LabelsWidth', 'LabelsWidth'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlRow
 , 'ID', 'ID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd ControlRow
 , 'ControlColumnID', 'ControlColumnID'
 , 2, bigint, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlRow
 , 'DataValueName', 'DataValueName'
 , 3, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd ControlRow
 , 'RowIndex', 'RowIndex'
 , 4, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlRow
 , 'TabbingIndex', 'TabbingIndex'
 , 5, int, -1, -1, -1, False
EXEC sp_DataColumnAdd ControlRow
 , 'AllowDisplay', 'AllowDisplay'
 , 6, bit, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitCategory
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd UnitCategory
 , 'Code', 'Code'
 , 2, varchar, -1, -1, 5, False
EXEC sp_DataColumnAdd UnitCategory
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 30, False
EXEC sp_DataColumnAdd UnitSystem
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd UnitSystem
 , 'Code', 'Code'
 , 2, varchar, -1, -1, 5, False
EXEC sp_DataColumnAdd UnitSystem
 , 'Name', 'Name'
 , 3, varchar, -1, -1, 30, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'ID', 'ID'
 , 1, int, 1, 1, -1, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'UnitCategoryID', 'UnitCategoryID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'UnitSystemID', 'UnitSystemID'
 , 3, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'Code', 'Code'
 , 4, varchar, -1, -1, 5, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'Name', 'Name'
 , 5, varchar, -1, -1, 30, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'AltName', 'AltName'
 , 6, varchar, -1, -1, 30, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'Sequence', 'Sequence'
 , 7, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitMeasure
 , 'Description', 'Description'
 , 8, varchar, -1, -1, 40, True
EXEC sp_DataColumnAdd UnitConversion
 , 'FromUnitMeasureID', 'FromUnitMeasureID'
 , 1, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitConversion
 , 'ToUnitMeasureID', 'ToUnitMeasureID'
 , 2, int, -1, -1, -1, False
EXEC sp_DataColumnAdd UnitConversion
 , 'Expression', 'Expression'
 , 3, varchar, -1, -1, 25, False
EXEC sp_DataColumnAdd SourceStatus
 , 'SourceStatusID', 'SourceStatusID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd SourceStatus
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd SourceStatus
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd MapType
 , 'MapTypeID', 'MapTypeID'
 , 1, bigint, 1, 1, -1, False
EXEC sp_DataColumnAdd MapType
 , 'Name', 'Name'
 , 2, varchar, -1, -1, 60, False
EXEC sp_DataColumnAdd MapType
 , 'Description', 'Description'
 , 3, varchar, -1, -1, 100, True
EXEC sp_DataColumnAdd DataTable
 , 'SchemaName', 'SchemaName'
 , 8, nvarchar, -1, -1, 20, True
END