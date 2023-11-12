/* Copyright(c) Lester J.Clark and Contributors. */
/* Licensed under the MIT License. */
/* 17DocMethod.sql */
USE[LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select dm.ID 'DocMethod', da.Name 'Assembly Name', dm.DocClassID, dc.Name,
  DocMethodGroupID, dmg.HeadingName 'Group Heading Name', dm.Name,
  dm.Description, dm.Sequence, OverloadName
from DocMethod as dm
left join DocClass as dc on DocClassID = dc.ID
left join DocAssembly as da on dc.DocAssemblyID = da.ID
left join DocMethodGroup as dmg on DocMethodGroupID = dmg.ID
order by da.Name, dc.Name, dmg.HeadingName, Sequence;
*/

declare @docClassName nvarchar(60);
declare @headingName nvarchar(60);
declare @seq int

/* LJCAddressParserLib */
set @docClassName = 'Directionals';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'Directionals';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'Directionals';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchCode',
  'Retrieve the collection element with code.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchSpanishName',
  'Retrieve the collection element.',
  @seq;

set @docClassName = 'Directionals';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'PrimaryRoads';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'PrimaryRoads';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchCode',
  'Retrieve the collection element.',
  @seq;

set @docClassName = 'PrimaryRoads';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'RoadLookups';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGenerateSoundex',
  'Generates the Soundex values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'RoadLookups';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchLookupName',
  'Finds and returns the object that contains the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchLSoundex',
  'Finds and returns the object that contains the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchPSoundex',
  'Finds and returns the object that contains the supplied values.',
  @seq;

set @docClassName = 'RoadLookups';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

/* LJCDataAccess */
set @docClassName = 'DataAccess';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance with the provided values.',
  @seq, 1, 'ctor1';

set @docClassName = 'DataAccess';
set @headingName = 'NonQuery';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExecuteNonQuery',
  'Executes an Insert, Update or Delete statement.      (E)',
  @seq;

set @docClassName = 'DataAccess';
set @headingName = 'Script';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExecuteScript',
  'Executes a DB script file.      (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExecuteScriptText',
  'Executes a DB script text string.      (E)',
  @seq;

set @docClassName = 'DataAccess';
set @headingName = 'Select';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'FillDataTable',
  'Executes a Select statement and fills the specified      DataTable.      (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDataReader',
  'Executes a Select statement and retrieves the      DbDataReader      object.      (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDataSet',
  'Executes a Select statement and retrieves the      DataSet      object.      (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDataTable',
  'Executes a Select statement and retrieves the      DataTable      object.      (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetSchemaOnly',
  'Retrieves the      DataTable      object with schema only.      (E)',
  @seq;

set @docClassName = 'DataAccess';
set @headingName = 'StoredProcedure';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetProcedureDataTable',
  'Executes a Stored Procedure and retrieves the      DataTable      object.      (E)',
  @seq;

set @docClassName = 'ProcedureParameters';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq, 1, 'Add1';

set @docClassName = 'ProcedureParameters';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ProcedureParameters';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchName',
  'Retrieve the collection element with name.',
  @seq;

/* LJCDataDetailDAL */
set @docClassName = 'ControlColumn';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlColumn';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'ControlColumnManager';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ControlColumnManager';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds a Data Record to the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Delete',
  'Deletes a Data Record with the specified ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsDuplicate',
  'Check for duplicate unique key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Load',
  'Retrieves a collection of Data Records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LoadWithParentID',
  'Loads the child records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Retrieve',
  'Retrieves a Data Record from the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithID',
  'Retrieves a Data Record with the supplied value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithUnique',
  'Retrieves a record with the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Update',
  'Updates the Data Record.',
  @seq;

set @docClassName = 'ControlColumnManager';
set @headingName = 'Key';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetIDKey',
  'Gets the ID key record.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetParentKey',
  'Gets the Parent ID key columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetUniqueKey',
  'Gets the unique key columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetJoins',
  'Creates and returns the Load Joins object.',
  @seq;

set @docClassName = 'ControlColumns';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetCollection',
  'Get custom collection from List.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'ControlColumns';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlColumns';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchID',
  'Retrieve the collection element.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchUnique',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortID',
  'Sort on ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortUnique',
  'Sort on Unique key.',
  @seq;

set @docClassName = 'ControlColumns';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'ControlDetail';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlDetail';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'ControlDetailManager';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ControlDetailManager';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds a Data Record to the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Delete',
  'Deletes a Data Record with the specified ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Load',
  'Retrieves a collection of Data Records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Retrieve',
  'Retrieves a Data Record from the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithID',
  'Retrieves a Data Record with the supplied value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithUnique',
  'Retrieves a Data Record with the supplied name value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithUniqueTable',
  'Retrieves a record with the supplied name value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Update',
  'Updates the Data Record.',
  @seq;

set @docClassName = 'ControlDetailManager';
set @headingName = 'Key';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetIDKey',
  'Gets the ID key record.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetUniqueKey',
  'Gets the Name keys record.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetUniqueTableKey',
  'Gets the ID key columns.',
  @seq;

set @docClassName = 'ControlDetails';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetCollection',
  'Get custom collection from List.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'ControlDetails';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlDetails';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchID',
  'Retrieve the collection element.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchUnique',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortID',
  'Sort on Code.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortUnique',
  'Sort on Name.',
  @seq;

set @docClassName = 'ControlDetails';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'ControlRow';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlRow';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'ControlRowManager';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ControlRowManager';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds a Data Record to the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Delete',
  'Deletes a Data Record with the specified ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsDuplicate',
  'Check for duplicate unique key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Load',
  'Retrieves a collection of Data Records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LoadWithParentID',
  'Loads the parent records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Retrieve',
  'Retrieves a Data Record from the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithID',
  'Retrieves a Data Record with the supplied value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithUnique',
  'Retrieves a record with the supplied name value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Update',
  'Updates the Data Record.',
  @seq;

set @docClassName = 'ControlRowManager';
set @headingName = 'Key';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetIDKey',
  'Gets the ID key record.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetParentKey',
  'Gets the Parent ID key columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetUniqueKey',
  'Gets the ID key columns.',
  @seq;

set @docClassName = 'ControlRows';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetCollection',
  'Get custom collection from List.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'ControlRows';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlRows';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchID',
  'Retrieve the collection element.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchUnique',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortID',
  'Sort on Code.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortUnique',
  'Sort on Name.',
  @seq;

set @docClassName = 'ControlRows';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'ControlTab';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlTab';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'ControlTabItems';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetCollection',
  'Get custom collection from List.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSerialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'ControlTabItems';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'ControlTabItems';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchID',
  'Retrieve the collection element.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchUnique',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortID',
  'Sort on ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSortUnique',
  'Sort on Unique key.',
  @seq;

set @docClassName = 'ControlTabItems';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

set @docClassName = 'ControlTabManager';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ControlTabManager';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds a Data Record to the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Delete',
  'Deletes a Data Record with the specified ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsDuplicate',
  'Check for duplicate unique key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Load',
  'Retrieves a collection of Data Records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LoadWithParentID',
  'Loads the child records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Retrieve',
  'Retrieves a Data Record from the database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithID',
  'Retrieves a Data Record with the supplied value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RetrieveWithUnique',
  'Retrieves a record with the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Update',
  'Updates the Data Record.',
  @seq;

set @docClassName = 'ControlTabManager';
set @headingName = 'Key';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetIDKey',
  'Gets the ID key record.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetParentKey',
  'Gets the Parent ID key columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetUniqueKey',
  'Gets the unique key columns.',
  @seq;

set @docClassName = 'DataDetailData';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Missing Summary',
  @seq, 1, 'ctor';

set @docClassName = 'DataDetailData';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'AddControlColumn',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'AddControlRow',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'AddControlTab',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetControlDetail',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetControlTab',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'UpdateControlColumn',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'UpdateControlDetail',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'UpdateControlRow',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'UpdateControlTab',
  'Missing Summary',
  @seq;

set @docClassName = 'DataDetailManagers';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'DataDetailManagers';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetDBProperties',
  'Sets the DB properties.',
  @seq;

/* LJCDBClientLib */
set @docClassName = 'DataManager';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Reset',
  'Resets the data access configuration.       (R)',
  @seq;

set @docClassName = 'DataManager';
set @headingName = 'DataAccess';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreateLoadRequest',
  'Creates and returns the Load      DbRequest      object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Delete',
  'Deletes the records with the specified key values.      (DE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExecuteClientSql',
  'Executes a non-query client SQL statement.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExecuteRequest',
  'Executes the supplied request.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetSchemaOnly',
  'Retrieves the column names for the specified table.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetTableNames',
  'Retrieves the table names for the data configuration database.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Load',
  'Retrieves a collection of data records.      (DE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LoadProcedure',
  'Retrieves a collection of data records.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Retrieve',
  'Retrieves a record from the database.      (DE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Update',
  'Updates the record.      (DE)',
  @seq;

set @docClassName = 'DataManager';
set @headingName = 'OtherData';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyNames',
  'Creates a PropertyNames list from the data definition.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'MapNames',
  'Maps the column property and rename values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetDbAssignedColumns',
  'Sets the database assigned value columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetLookupColumns',
  'Adds the lookup column names.',
  @seq;

/* LJCDBMessage */
set @docClassName = 'DbCondition';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbCondition';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbConditions';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the element from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'DbConditions';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbConditionSet';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Creates and returns a clone of the object.',
  @seq;

set @docClassName = 'DbFilter';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbFilter';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbFilters';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the element from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the element from the supplied values.',
  @seq, 1, 'Add1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbFilters';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbJoin';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbJoinOn';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbJoinOn';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbJoinOns';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the element from the supplied values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbJoinOns';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbJoins';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates the element from the supplied values and adds it to the collection.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbJoins';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance with the supplied values.',
  @seq, 1, 'ctor2';

set @docClassName = 'DbRequest';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the object and returns the serialized string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serialize the object to the specified file.',
  @seq, 1, 'Serialize1';

set @docClassName = 'DbRequest';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes the      DbRequest message.',
  @seq;

set @docClassName = 'DbResult';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasColumns',
  'Checks if the result has Columns.',
  @seq, 1, 'HasColumns1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasData',
  'Checks if the result has Columns.',
  @seq, 1, 'HasData1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasRows',
  'Checks if the result has Rows.',
  @seq, 1, 'HasRows1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the object and returns the serialized string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serialize the object to the specified file.',
  @seq, 1, 'Serialize1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetColumns',
  'Sets the Columns property from the principle and join columns.',
  @seq, 1, 'SetColumns1';

set @docClassName = 'DbResult';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance with the DbRequest object.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor2';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance with the supplied values.',
  @seq, 1, 'ctor3';

set @docClassName = 'DbResult';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'DeserializeMessage',
  'Deserializes the      DbResult      message.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasColumns',
  'Checks if the result has Columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasData',
  'Checks if the result has Columns.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasRows',
  'Checks if the result has Rows.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetColumns',
  'Sets the Columns property from the Request columns.',
  @seq;

set @docClassName = 'DbRow';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;

set @docClassName = 'DbRow';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbRow';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'DbRows';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serialize the object to the specified file.',
  @seq;

set @docClassName = 'DbRows';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'DbRows';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCDeserialize',
  'Deserializes from the specified XML file.',
  @seq;

/* LJCGridDataLib */
set @docClassName = 'TableData';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreateDataColumns',
  'Creates a new DataColumns object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'DataColumnClone',
  'Clones a DataColumn object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'DataColumnsClone',
  'Clones a DataColumn collection.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDataColumns',
  'Returns a set of DataColumns that match the supplied list.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDbColumn',
  'Creates a DbColumn object from a DataColumn object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDbColumns',
  'Creates a DbColumns collection from a DataColumns collection.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetGridColumns',
  'Missing Summary',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyNames',
  'Creates a PropertyNames list from a DataColumns collection.',
  @seq;

/* LJCNetCommon */
set @docClassName = 'CommonDataTypes';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'CommonDataTypes';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'CommonDataTypes';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'CommonDataTypes';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'CommonKeywords';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'CommonKeywords';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'CommonKeywords';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'CommonKeywords';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'CommonModifiers';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'CommonModifiers';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'CommonModifiers';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'CommonModifiers';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'DbColumn';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance with the supplied values.',
  @seq, 1, 'ctor2';

set @docClassName = 'DbColumn';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'FormatValue',
  'Formats the column value for the SQL string. (D)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'DbColumn';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'op_Implicit',
  'Creates a      DbValue      object from a      DbColumn      object. (E)',
  @seq;

set @docClassName = 'Keywords';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'Keywords';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'Keywords';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'Keywords';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'LibTypes';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'LibTypes';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'LibTypes';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'LibTypes';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'LJCAssemblyReflect';
set @headingName = 'BoolCheckMethods';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsNotCommonClassification',
  'Indicates if the Type is not a common type.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsNotCommonInterface',
  'Indicates if the Interface is not a common type.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsNotProperty',
  'Indicates if the Method is not a property getter or setter.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsOverride',
  'Indicates if the method is "override".',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsPublic',
  'Indicates if the method is "public".',
  @seq;

set @docClassName = 'LJCAssemblyReflect';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'LJCAssemblyReflect';
set @headingName = 'GetSyntax';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetConstructorSyntax',
  'Creates and returns the Constructor syntax. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetFieldSyntax',
  'Creates and returns the Field syntax string. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetGenericTypeSyntax',
  'Creates and returns the Generic Type syntax.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetMethodSyntax',
  'Creates and returns the Method syntax. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertySyntax',
  'Creates and returns the Property syntax string. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetTypeSyntax',
  'Creates and returns the Type syntax. (E)',
  @seq;

set @docClassName = 'LJCAssemblyReflect';
set @headingName = 'SetReflectionObjects';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetAssembly',
  'Retrieves the Assembly reference. (R)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetConstructorInfo',
  'Set the ConstructorInfo reference. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetFieldInfo',
  'Set the FieldInfo reference. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetMethodInfo',
  'Set the MethodInfo reference. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetPropertyInfo',
  'Set the PropertyInfo reference. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetTypeReference',
  'Set the Type reference. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetIndexerInfo',
  'Gets the Indexer Property info.',
  @seq;

set @docClassName = 'LJCReflect';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyInfo',
  'Gets the cached PropertyInfo value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyNames',
  'Gets a list of the property names.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyType',
  'Get the property type.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasProperty',
  'Missing Summary',
  @seq;

set @docClassName = 'LJCReflect';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Instantiates an instance of the class.',
  @seq, 1, 'ctor';

set @docClassName = 'LJCReflect';
set @headingName = 'SetMethods';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetPropertyValue',
  'Sets the property value based on value type. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetSource',
  'Sets the source object and type values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetValue',
  'Sets the property value.',
  @seq;

set @docClassName = 'LJCReflect';
set @headingName = 'Value';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetBoolean',
  'Gets the property value as a boolean.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetByte',
  'Gets the property value as a byte.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetChar',
  'Gets the property value as a char.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDateTime',
  'Gets the property value as a DateTime value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDbDateString',
  'Gets the property value as a DB date/time string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDecimal',
  'Gets the property value as a decimal.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDouble',
  'Gets the property value as a double.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt16',
  'Gets the property value as a short.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt32',
  'Gets the property value as an integer.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt64',
  'Gets the property value as a long.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetSingle',
  'Gets the property value as a float.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetString',
  'Gets the property value as a string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetValue',
  'Gets the property value as an object using a delegate. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetValueReflect',
  'Gets the property value as an object using reflection.',
  @seq;

set @docClassName = 'Modifiers';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'Modifiers';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'Modifiers';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'Modifiers';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

set @docClassName = 'NetCommon';
set @headingName = 'Config';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ConfigBool',
  'Retrieves the Config bool value. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ConfigColor',
  'Retrieves the Config Color value. (RE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ConfigString',
  'Retrieves the Config string value. (RE)',
  @seq;

set @docClassName = 'NetCommon';
set @headingName = 'Serialize';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlDeserialize',
  'Deserialize an XML message file to an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlDeserializeMessage',
  'Deserialize an XML message string to an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlSerialize',
  'Serialize an object to an XML message file. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlSerializeToString',
  'Serialize an object to an XML message string. (E)',
  @seq;

set @docClassName = 'NetCommon';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CheckArgument``1',
  'Check for missing argument of type: string with no value, null, integer = 0, IList with no ite',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareNull',
  'Compare null values. (DE)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasColumns',
  'Checks a data table for columns definitions.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasData',
  'Checks a data table and returns true if it contains any rows. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks a List<> Collection and returns true if it contains any items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsEqual',
  'Checks if two values are equal.',
  @seq;

set @docClassName = 'NetCommon';
set @headingName = 'TextTransform';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Base64BytesToText',
  'Decodes a Base64 byte array to a Text value. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextToBase64Bytes',
  'Encodes a Text value to a Base64 byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Base64BytesToTextBytes',
  'Decodes a Base64 byte array to a Text byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextBytesToBase64Bytes',
  'Encodes a byte array to a Base64 byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Base64ToText',
  'Decodes a Base64 value to a Text value. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextToBase64',
  'Encodes a Text value to a Base64 value. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Base64ToTextBytes',
  'Decodes a Base64 value to a Text byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextBytesToBase64',
  'Encodes a Text byte array to a Base64 value. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'BytesToText',
  'Creates text from a byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextToBytes',
  'Creates a byte array from text. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'MemStreamToBytes',
  'Copies a memory stream to a byte array. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'BytesToMemStream',
  'Copies a byte array to a memory stream. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'MemStreamToString',
  'Creates a string from a memory stream. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'StringToMemStream',
  'Creates a memory stream from a string. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlDecode',
  'Decodes an encoded XML string. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'XmlEncode',
  'Encodes a string with XML escape values. (E)',
  @seq;

set @docClassName = 'NetCommon';
set @headingName = 'Value';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetBoolean',
  'Gets a boolean value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetByte',
  'Gets a byte value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetChar',
  'Gets a char value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDateTime',
  'Gets a DateTime value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDecimal',
  'Gets a decimal value from an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDouble',
  'Gets a double value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt16',
  'Gets a short value from an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt32',
  'Gets an integer value from an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInt64',
  'Gets a long value from an object. (E)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetObject',
  'Gets an instantiated object value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetSingle',
  'Gets a single value from an object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetString',
  'Gets a trimmed string value from an object. (E)',
  @seq;

set @docClassName = 'NetString';
set @headingName = 'CheckValues';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasValue',
  'Checks if a text value exists.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsDigits',
  'Checks a string value for digits.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsEqual',
  'Do an Ignore Case string compare.',
  @seq;

set @docClassName = 'NetString';
set @headingName = 'Formatting';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ExceptionString',
  'Creates an exception string with outer and inner exception.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetPropertyName',
  'Gets a column name with underscores converted to Pascal case.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'InitString',
  'Initializes a string to the trimmed value or null.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RemoveSection',
  'Removes a section from a text value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Truncate',
  'Truncates a text string to the specified length.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetSearchName',
  'Gets the Search Property name.',
  @seq;

set @docClassName = 'NetString';
set @headingName = 'Parsing';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'FindTag',
  'Finds a tag in a text value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDelimitedAndIndexes',
  'Get the delimited string begin and end index.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDelimitedString',
  'Gets the string between the specified delimiters.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetStringWithDelimiters',
  'Get the string including the specified delimiters.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RemoveTags',
  'Removes tags from a text value.',
  @seq;

set @docClassName = 'NetString';
set @headingName = 'Soundex';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreateLSoundex',
  'Creates a letter based soundex value. (D)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreatePSoundex',
  'Creates a Phonetic based soundex value. (D)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'IsSoundexLetter',
  'Checks if the letter is a soundex skipped letter. (R)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Phonetic',
  'Creates a Phonetic character from the supplied text starting at the      supplied index. (D)',
  @seq;

set @docClassName = 'PropertyDelegates';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds a PropertyDelegate object to the collection. (R)',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCCreateDelegate',
  'Creates and returns the delegate for the named property.',
  @seq;

set @docClassName = 'PropertyDelegates';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchName',
  'Returns the PropertyDelegate object if found in the list.',
  @seq;

set @docClassName = 'RefTypes';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Adds the specified object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEnumerator',
  'Gets the Collection Enumerator.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq, 1, 'HasItems1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Serialize',
  'Serializes the collection to a file.',
  @seq;

set @docClassName = 'RefTypes';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'The Copy constructor.',
  @seq, 1, 'ctor1';

set @docClassName = 'RefTypes';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SearchName',
  'Retrieve the collection element with name.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SortName',
  'Sort on Name.',
  @seq;

set @docClassName = 'RefTypes';
set @headingName = 'Static';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Deserialize',
  'Deserializes from the specified XML file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasItems',
  'Checks if the collection has items.',
  @seq;

/* LJCWinFormCommon */
set @docClassName = 'ControlValue';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates the ControlValue object from the supplied values and adds      the element to the collectio',
  @seq;

set @docClassName = 'ControlValues';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ControlValues';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchName',
  'Retrieve the collection element with name.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'ActionState';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetMenuState',
  'Sets the enable state for the menu items.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetToolState',
  'Sets the enable state for the tool items.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'Error';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ShowError',
  'Displays the error text if not null.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ShowHasError',
  'Displays "No records affected." if the affected count is less than 1.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'File';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SaveFile',
  'Displays the Save dialog to select a file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SelectFile',
  'Displays the Open dialog to select a file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ShellFile',
  'Execute a program with the selected file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ShellProgram',
  'Executes an external program.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'General';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreateTablesPrompt',
  'Verify create of missing tables.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'RestoreSplitDistance',
  'Restore the splitter distance.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetString',
  'Sets the string to "-" if it is empty or blanks and to "" if it is "-".',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'SetLabelsBackColor',
  'Sets the BackColor for the labels.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'Image';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CreateGradient',
  'Draws a gradient in the specified rectangle.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CropImage',
  'Crops an image.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ResizeImage',
  'Resizes an image.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TransformCrop',
  'Transforms the crop rectangle values of the sample image relative      to the values of the origina',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'KeyHandler';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HandleNumberOrEditKey',
  'Checks the key character for a numeric or allowed control value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HandleSpace',
  'Checks the key character for a space.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'StripBlanks',
  'Strips blanks from the string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'StripNonDigits',
  'Strips non-digits from a string.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextBoxNoSpace_KeyPress',
  'Does not allow spaces.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextBoxNoSpace_TextChanged',
  'Strips blanks from the text value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'TextBoxNumeric_KeyPress',
  'Only allows numbers or edit keys.',
  @seq;

set @docClassName = 'FormCommon';
set @headingName = 'ScreenPoint';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetDialogScreenPoint',
  'Gets the Grid target Dialog screen position.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetMenuScreenPoint',
  'Get the control target menu screen position.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetScreenRectangle',
  'Gets the Control screen rectangle.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetScreenPoint',
  'Converts the Control point to Screen point.',
  @seq;

set @docClassName = 'ModuleNameComparer';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Compare',
  'Compares two objects.',
  @seq;

set @docClassName = 'ModuleReference';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareTo',
  'Provides the default Sort functionality.',
  @seq;

set @docClassName = 'ModuleReference';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'ModuleReference';
set @headingName = 'GetReference';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetAssembly',
  'Retrieves the Assembly reference.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetControlInstance',
  'Retrieves the ConrolInstance reference.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetControlType',
  'Retrieves the ControlType reference.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetEventInfo',
  'Retrieves the PageClose event info.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetInitMethodInfo',
  'Retrieves the LJCInit() MethodInfo reference.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetTabControl',
  'Retrieves the TabControl reference.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'GetTabsMethodInfo',
  'Retrieves the LJCTabs() MethodInfo reference.',
  @seq;

/* LJCWinFormControls */
set @docClassName = 'LJCDataGrid';
set @headingName = 'ColumnData';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetMouseColumn',
  'Retrieves the column where the mouse was clicked.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetMouseColumnIndex',
  'Retrieves the column index where the mouse was clicked.',
  @seq;

set @docClassName = 'LJCDataGrid';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance and adds it to a container.',
  @seq, 1, 'ctor1';

set @docClassName = 'LJCDataGrid';
set @headingName = 'GridConfig';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetPlain',
  'Sets the grid to a simple read-only grid.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetLastColumnAutoSizeFill',
  'Sets the last column AutoSizeMode to "Fill" if the columns width is less      than the grid width.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCAddColumn',
  'Adds a column to the grid.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCAddColumn',
  'Adds a grid column.',
  @seq, 1, 'LJCAddColumn1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCAddCheckColumn',
  'Adds a Checkbox column.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSaveColumnValues',
  'Saves the grid column values.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCRestoreColumnValues',
  'Restores the grid column values.',
  @seq;

set @docClassName = 'LJCDataGrid';
set @headingName = 'RowData';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCRowAdd',
  'Adds a GridRow control to the grid.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCRowInsert',
  'Inserts a GridRow control into the grid.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCRowsClear',
  'Clears the rows without allowing SelectionChange.',
  @seq;

set @docClassName = 'LJCDataGrid';
set @headingName = 'RowSelection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCIsDifferentRow',
  'Compares the current row against the last selected row.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetLastRow',
  'Saves the last selected row index.',
  @seq;

set @docClassName = 'LJCDataGrid';
set @headingName = 'RowSet';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCurrentRow',
  'Sets the current row to the specified index.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCurrentRow',
  'Sets the current row.',
  @seq, 1, 'LJCSetCurrentRow1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCurrentRow',
  'Sets the current row to the mouse row.',
  @seq, 1, 'LJCSetCurrentRow2';

set @docClassName = 'LJCGridRow';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'LJCGridRow';
set @headingName = 'Value';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetCellText',
  'Sets the cell value.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetInt32',
  'Gets the stored int value using an int key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetInt32',
  'Gets the stored int value using a string key.',
  @seq, 1, 'LJCGetInt321';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetInt64',
  'Gets the stored long value using a long key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetInt64',
  'Gets the stored long value using a string key.',
  @seq, 1, 'LJCGetInt641';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetString',
  'Gets the stored string value using an int key.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetString',
  'Gets the stored string value using a string key.',
  @seq, 1, 'LJCGetString1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCellText',
  'Sets the cell value by index.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCellText',
  'Sets the cell value by name.',
  @seq, 1, 'LJCSetCellText1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetInt32',
  'Stores an int key and int value pair.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetInt32',
  'Stores a string key and int value pair.',
  @seq, 1, 'LJCSetInt321';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetInt64',
  'Stores a long key and long value pair.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetInt64',
  'Stores a string key and long value pair.',
  @seq, 1, 'LJCSetInt641';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetString',
  'Stores a int key and string value pair.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetString',
  'Stores a string key and string value pair.',
  @seq, 1, 'LJCSetString1';

set @docClassName = 'LJCItem';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Clone',
  'Clones the structure of the object.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'ToString',
  'The object string identifier.',
  @seq;

set @docClassName = 'LJCItemCombo';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'LJCItemCombo';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCAddItem',
  'Adds an Item to the ComboBox.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCExportData',
  'Exports the grid values to a data file.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSelectedItemID',
  'Gets the combo SelectedItem ID.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetByItemID',
  'Sets the combo SelectedIndex to the item with the specified ID value.',
  @seq;

set @docClassName = 'LJCTabControl';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Instantiates an instance of the class and adds it to a container.',
  @seq, 1, 'ctor1';

set @docClassName = 'LJCTabControl';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCCloseEmptyPanel',
  'Closes the tiled split panel.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetTabPage',
  'Gets the tab page if the position corresponds to a tab label.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetTabPage',
  'Gets the tab page with the matching text.',
  @seq, 1, 'LJCGetTabPage1';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetTabPage',
  'Retrieves the tab page where the mouse was clicked.',
  @seq, 1, 'LJCGetTabPage2';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCGetTabPageIndex',
  'Gets the tab index for a tab page.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCMoveTabPage',
  'Moves a tab page.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCMoveTabPageLeft',
  'Moves the tab page left to the main tabs.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCMoveTabPageRight',
  'Moves the tab page right to the tile tabs.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSetCurrentTabPage',
  'Sets the current tab page.',
  @seq;

set @docClassName = 'LJCTabPanel';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';

set @docClassName = 'LJCTabPanel';
set @headingName = 'EventHandlers';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCOnAddTile',
  'The AddTile event method.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCOnRemoveTile',
  'The remove tile event method.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'OnDragOver',
  'The DragOver event method override.',
  @seq;

set @docClassName = 'PanelControlsAdjust';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor';
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq, 1, 'ctor1';
