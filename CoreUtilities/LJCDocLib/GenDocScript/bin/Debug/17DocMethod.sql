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

/* LJCDataAccess */
set @docClassName = 'DataAccess';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'CompareNull',
  'Initializes an object instance.',
  @seq;
set @seq += 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq;

set @docClassName = 'DataAccess';
set @headingName = 'Data';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'HasData',
  'Checks a data table and returns true if it contains any rows. (E)',
  @seq;

set @docClassName = 'ProcedureParameters';
set @headingName = 'Collection';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'Add',
  'Creates and adds the object from the provided values.',
  @seq;

set @docClassName = 'ProcedureParameters';
set @headingName = 'Constructor';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  '#ctor',
  'Initializes an object instance.',
  @seq;

set @docClassName = 'ProcedureParameters';
set @headingName = 'SearchSort';
set @seq = 1;
exec sp_DMAddUnique @docClassName, @headingName,
  'LJCSearchName',
  'Retrieve the collection element with name.',
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
  'Reset',
  'Resets the data access configuration.       (R)',
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

/* LJCNetCommon */
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
