/* 17DM_NetCommon.sql */
/* DocMethods */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
select
  ID, DocMethodGroupID, Name, Description, Sequence
from DocMethod;
*/

declare @docClassName nvarchar(60) = 'NetCommon';
declare @headingName nvarchar(60) = 'Static';
exec sp_DMAddUnique @docClassName, @headingName
  , 'CompareNull'
  , 'Compare null values. (DE)'
  , 1;
exec sp_DMAddUnique @docClassName, @headingName
  , 'HasData'
  , 'Checks a data table and returns true if it contains any rows. (E)'
  , 2;
exec sp_DMAddUnique @docClassName, @headingName
  , 'HasItems'
  , 'Checks a List<> Collection and returns true if it contains any items.'
  , 3;
exec sp_DMAddUnique @docClassName, @headingName
  , 'IsEqual'
  , 'Checks if two values are equal.'
  , 4;

set @headingName = 'TextTransform';
exec sp_DMAddUnique @docClassName, @headingName
  , 'Base64BytesToText'
  , 'Decodes a Base64 byte array to a Text value. (E)'
  , 1;
exec sp_DMAddUnique @docClassName, @headingName
  , 'TextToBase64Bytes'
  , 'Encodes a Text value to a Base64 byte array. (E)'
  , 2;
exec sp_DMAddUnique @docClassName, @headingName
  , 'Base64BytesToTextBytes'
  , 'Decodes a Base64 byte array to a Text byte array. (E)'
  , 3;
exec sp_DMAddUnique @docClassName, @headingName
  , 'TextBytesToBase64Bytes'
  , 'Encodes a byte array to a Base64 byte array. (E)'
  , 4;
exec sp_DMAddUnique @docClassName, @headingName
  , 'Base64ToText'
  , 'Decodes a Base64 value to a Text value. (E)'
  , 5;
exec sp_DMAddUnique @docClassName, @headingName
  , 'TextToBase64'
  , 'Encodes a Text value to a Base64 value. (E)'
  , 6;
exec sp_DMAddUnique @docClassName, @headingName
  , 'Base64ToTextBytes'
  , 'Decodes a Base64 value to a Text byte array. (E)'
  , 7;
exec sp_DMAddUnique @docClassName, @headingName
  , 'TextBytesToBase64'
  , 'Encodes a Text byte array to a Base64 value. (E)'
  , 8;
exec sp_DMAddUnique @docClassName, @headingName
  , 'BytesToText'
  , 'Creates text from a byte array. (E)'
  , 9;
exec sp_DMAddUnique @docClassName, @headingName
  , 'TextToBytes'
  , 'Creates a byte array from text. (E)'
  , 10;
exec sp_DMAddUnique @docClassName, @headingName
  , 'MemStreamToBytes'
  , 'Copies a memory stream to a byte array. (E)'
  , 11;
exec sp_DMAddUnique @docClassName, @headingName
  , 'BytesToMemStream'
  , 'Copies a byte array to a memory stream. (E)'
  , 12;
exec sp_DMAddUnique @docClassName, @headingName
  , 'MemStreamToString'
  , 'Creates a string from a memory stream. (E)'
  , 13;
exec sp_DMAddUnique @docClassName, @headingName
  , 'StringToMemStream'
  , 'Creates a memory stream from a string. (E)'
  , 14;
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlDecode'
  , 'Decodes an encoded XML string. (E)'
  , 15;
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlEncode'
  , 'Encodes a string with XML escape values. (E)'
  , 16;

set @headingName = 'Serialize';
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlDeserialize'
  , 'Deserialize an XML message file to an object. (E)'
  , 1;
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlDeserializeMessage'
  , 'Deserialize an XML message string to an object. (E)'
  , 2;
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlSerialize'
  , 'Serialize an object to an XML message file. (E)'
  , 3;
exec sp_DMAddUnique @docClassName, @headingName
  , 'XmlSerializeToString'
  , 'Serialize an object to an XML message string. (E)'
  , 4;

set @HeadingName = 'Config';
exec sp_DMAddUnique @docClassName, @headingName
  , 'ConfigBool'
  , 'Retrieves the Config bool value. (RE)'
  , 1;
exec sp_DMAddUnique @docClassName, @headingName
  , 'ConfigColor'
  , 'Retrieves the Config Color value. (RE)'
  , 2;
exec sp_DMAddUnique @docClassName, @headingName
  , 'ConfigString'
  , 'Retrieves the Config string value. (RE)'
  , 3;

set @HeadingName = 'Value';
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetBoolean'
  , 'Gets a boolean value from an object.'
  , 1;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetByte'
  , 'Gets a byte value from an object.'
  , 2;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetChar'
  , 'Gets a char value from an object.'
  , 3;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetDateTime'
  , 'Gets a DateTime value from an object.'
  , 4;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetDecimal'
  , 'Gets a decimal value from an object. (E)'
  , 5;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetDouble'
  , 'Gets a double value from an object.'
  , 6;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetInt16'
  , 'Gets a short value from an object. (E)'
  , 7;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetInt32'
  , 'Gets an integer value from an object. (E)'
  , 8;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetInt64'
  , 'Gets a long value from an object. (E)'
  , 9;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetObject'
  , 'Gets an instantiated object value.'
  , 10;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetSingle'
  , 'Gets a single value from an object.'
  , 11;
exec sp_DMAddUnique @docClassName, @headingName
  , 'GetString'
  , 'Gets a trimmed string value from an object. (E)'
  , 12;
