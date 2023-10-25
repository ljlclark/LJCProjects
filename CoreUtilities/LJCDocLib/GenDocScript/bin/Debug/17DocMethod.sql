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
