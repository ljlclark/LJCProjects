/* 9DM_NetCommon.sql */
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

declare @DocClassName nvarchar(60) = 'NetCommon';
declare @DocClassID smallint
  = (select ID from DocClass where Name = @DocClassName);

declare @HeadingName nvarchar(60);
declare @DocMethodGroupID smallint;
declare @Name nvarchar(60);

set @HeadingName = 'Static';
set @DocMethodGroupID  = (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName);
set @Name = 'CompareNull';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Compare null values. (DE)'
     , 1);
set @Name = 'HasData';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Checks a data table and returns true if it contains any rows. (E)'
     , 2);
set @Name = 'HasItems';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Checks a List<> Collection and returns true if it contains any items.'
     , 3);
set @Name = 'IsEqual';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Checks if two values are equal.'
     , 4);

set @HeadingName = 'TextTransform';
set @DocMethodGroupID  = (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName);
set @Name = 'Base64BytesToText';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Decodes a Base64 byte array to a Text value. (E)'
     , 1);
set @Name = 'TextToBase64Bytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Encodes a Text value to a Base64 byte array. (E)'
     , 2);
set @Name = 'Base64BytesToTextBytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Decodes a Base64 byte array to a Text byte array. (E)'
     , 3);
set @Name = 'TextBytesToBase64Bytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Encodes a byte array to a Base64 byte array. (E)'
     , 4);
set @Name = 'Base64ToText';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Decodes a Base64 value to a Text value. (E)'
     , 5);
set @Name = 'TextToBase64';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Encodes a Text value to a Base64 value. (E)'
     , 6);
set @Name = 'Base64ToTextBytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Decodes a Base64 value to a Text byte array. (E)'
     , 7);
set @Name = 'TextBytesToBase64';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Encodes a Text byte array to a Base64 value. (E)'
     , 8);
set @Name = 'BytesToText';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Creates text from a byte array. (E)'
     , 9);
set @Name = 'TextToBytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Creates a byte array from text. (E)'
     , 10);
set @Name = 'MemStreamToBytes';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Copies a memory stream to a byte array. (E)'
     , 11);
set @Name = 'BytesToMemStream';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Copies a byte array to a memory stream. (E)'
     , 12);
set @Name = 'MemStreamToString';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Creates a string from a memory stream. (E)'
     , 13);
set @Name = 'StringToMemStream';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Creates a memory stream from a string. (E)'
     , 14);
set @Name = 'XmlDecode';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Decodes an encoded XML string. (E)'
     , 15);
set @Name = 'XmlEncode';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Encodes a string with XML escape values. (E)'
     , 16);

set @HeadingName = 'Serialize';
set @DocMethodGroupID  = (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName);
set @Name = 'XmlDeserialize';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Deserialize an XML message file to an object. (E)'
     , 1);
set @Name = 'XmlDeserializeMessage';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Deserialize an XML message string to an object. (E)'
     , 2);
set @Name = 'XmlSerialize';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Serialize an object to an XML message file. (E)'
     , 3);
set @Name = 'XmlSerializeToString';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Serialize an object to an XML message string. (E)'
     , 4);

set @HeadingName = 'Config';
set @DocMethodGroupID  = (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName);
set @Name = 'ConfigBool';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Retrieves the Config bool value. (RE)'
     , 1);
set @Name = 'ConfigColor';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Retrieves the Config Color value. (RE)'
     , 2);
set @Name = 'ConfigString';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Retrieves the Config string value. (RE)'
     , 3);

set @HeadingName = 'Value';
set @DocMethodGroupID  = (select ID from DocMethodGroup
where DocClassID = @DocClassID
  and HeadingName = @HeadingName);
set @Name = 'GetBoolean';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a boolean value from an object.'
     , 1);
set @Name = 'GetByte';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a byte value from an object.'
     , 2);
set @Name = 'GetChar';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a char value from an object.'
     , 3);
set @Name = 'GetDateTime';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a DateTime value from an object.'
     , 4);
set @Name = 'GetDecimal';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a decimal value from an object. (E)'
     , 5);
set @Name = 'GetDouble';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a double value from an object.'
     , 6);
set @Name = 'GetInt16';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a short value from an object. (E)'
     , 7);
set @Name = 'GetInt32';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets an integer value from an object. (E)'
     , 8);
set @Name = 'GetInt64';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a long value from an object. (E)'
     , 9);
set @Name = 'GetObject';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets an instantiated object value.'
     , 10);
set @Name = 'GetSingle';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a single value from an object.'
     , 11);
set @Name = 'GetString';
IF NOT EXISTS (select ID from DocMethod
where DocMethodGroupID = @DocMethodGroupID
  and Name = @Name)
  insert into DocMethod (DocMethodGroupID, Name, Description, Sequence)
   values (@DocMethodGroupID, @Name
     , 'Gets a trimmed string value from an object. (E)'
     , 12);
