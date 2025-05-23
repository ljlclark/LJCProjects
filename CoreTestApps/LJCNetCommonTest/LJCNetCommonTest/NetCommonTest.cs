﻿using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJCNetCommonTest
{
  internal class NetCommonTest
  {
    public static void Test()
    {
      // Text Transform Functions
      Base64BytesToText();
      TextToBase64Bytes();
      Base64BytesToTextBytes();
      TextBytesToBase64Bytes();
      Base64ToText();
      TextToBase64();
      Base64ToTextBytes();
      TextBytesToBase64();
      BytesToText();
      TextToBytes();
      MemStreamToBytes();
      BytesToMemStream();
      MemStreamToString();
      StringToMemStream();

      // Object Data Functions
      GetBoolean();
      GetByte();
      GetChar();
      GetDecimal();
      GetInt16();
      GetInt32();
      GetInt64();
      GetString();

      // Serialization Functions
      XmlDeserialize();
      XmlDeserializeMessage();
      XmlSerialize();
      XmlSerializeToString();
    }

    #region Text Transform Functions

    // Decodes a Base64 byte array to a Text value.
    private static void Base64BytesToText()
    {
      // Setup
      byte[] base64Bytes = NetCommon.TextToBase64Bytes("Text");

      // Decodes a Base64 byte array to a Text value.
      string text = NetCommon.Base64BytesToText(base64Bytes);
    }

    // Encodes a Text value to a Base64 byte array.
    private static void TextToBase64Bytes()
    {
      // Create Text from Base64 byte array.
      byte[] base64Bytes = NetCommon.TextToBase64Bytes("Text");

      // Check the text.
      string text = NetCommon.Base64BytesToText(base64Bytes);
    }

    // Decodes a Base64 byte array to a Text byte array.
    private static void Base64BytesToTextBytes()
    {
      // Setup
      byte[] base64Bytes = NetCommon.TextToBase64Bytes("Text");

      // Decodes a Base64 byte array to a Text byte array.
      byte[] textBytes = NetCommon.Base64BytesToTextBytes(base64Bytes);

      // Check the text.
      string text = NetCommon.BytesToText(textBytes);
    }

    // Encodes a Text byte array to a Base64 byte array.
    private static void TextBytesToBase64Bytes()
    {
      // Setup
      byte[] textBytes = NetCommon.TextToBytes("Text");

      // Encodes a byte array to a Base64 byte array.
      byte[] base64Bytes = NetCommon.TextBytesToBase64Bytes(textBytes);

      // Check the text.
      string text = NetCommon.Base64BytesToText(base64Bytes);
    }

    // Decodes a Base64 value to a Text value.
    private static void Base64ToText()
    {
      // Setup
      string base64 = NetCommon.TextToBase64("Text");

      // Decodes a Base64 string to Text.
      string text = NetCommon.Base64ToText(base64);
    }

    // Encodes a Text value to a Base64 value.
    private static void TextToBase64()
    {
      string text = "Text";

      // Encodes text to a Base64 string.
      string base64 = NetCommon.TextToBase64(text);

      // Check the text.
      text = NetCommon.Base64ToText(base64);
    }

    // Decodes a Base64 value to a Text byte array.
    public static void Base64ToTextBytes()
    {
      // Setup
      string base64 = NetCommon.TextToBase64("Text");

      // Decodes a Base64 value to a Text byte array.
      byte[] textBytes = NetCommon.Base64ToTextBytes(base64);

      // Check the text.
      string text = NetCommon.BytesToText(textBytes);
    }

    // Encodes a Text byte array to a Base64 value.
    public static void TextBytesToBase64()
    {
      // Setup
      byte[] TextBytes = NetCommon.TextToBytes("Text");

      // Encodes a Text byte array to a Base64 value.
      string base64 = NetCommon.TextBytesToBase64(TextBytes);

      // Check the text.
      string text = NetCommon.Base64ToText(base64);
    }

    // Creates text from a byte array.
    private static void BytesToText()
    {
      // Setup
      byte[] bytes = NetCommon.TextToBytes("Text");

      // Creates text from a byte array.
      string text = NetCommon.BytesToText(bytes);
    }

    // Creates a byte array from text.
    private static void TextToBytes()
    {
      // Setup
      string text = "Text";

      // Creates a byte array from text.
      byte[] bytes = NetCommon.TextToBytes(text);

      // Check the text.
      text = NetCommon.BytesToText(bytes);
    }

    // Copies a memory stream to a byte array.
    private static void MemStreamToBytes()
    {
      // Setup
      byte[] textBytes;
      using (Stream stream = NetCommon.StringToMemStream("Text"))
      {
        // Copies a memory stream to a byte array.
        textBytes = NetCommon.MemStreamToBytes(stream);
      }

      // Check the text.
      string text = NetCommon.BytesToText(textBytes);
    }

    // Copies a byte array to a memory stream.
    private static void BytesToMemStream()
    {
      // Setup
      byte[] bytes = NetCommon.TextToBytes("Text");

      // Copies a byte array to a memory stream.
      using (Stream stream = NetCommon.BytesToMemStream(bytes))
      {
        // Check the text.
        string text = NetCommon.MemStreamToString(stream);
      }
    }

    // Creates a string from a memory stream.
    private static void MemStreamToString()
    {
      // Setup
      using (Stream stream = NetCommon.StringToMemStream("Text"))
      {
        // Creates a string from a memory stream.
        string text = NetCommon.MemStreamToString(stream);
      }
    }

    // Creates a memory stream from a string.
    private static void StringToMemStream()
    {
      using (Stream stream = NetCommon.StringToMemStream("Text"))
      {
        // Check the text.
        string text = NetCommon.MemStreamToString(stream);
      }
    }

    // Decodes an encoded XML string.
    private static void XmlDecode()
    {
      // Setup
      string xml = "<text>Here & There</text>";
      string encoded = NetCommon.XmlEncode(xml);

      // Decodes an encoded XML string.
      string text = NetCommon.XmlDecode(encoded);
    }

    // Encodes a string with XML escape values.
    private static void XmlEncode()
    {
      // Setup
      string xml = "<text>Here & There</text>";

      // Encodes a string with XML escape values.
      string encoded = NetCommon.XmlEncode(xml);

      // Check the text.
      string text = NetCommon.XmlDecode(encoded);
    }
    #endregion

    #region Object Data Functions

    // Gets a bool value from an object.
    private static void GetBoolean()
    {
      // Simulates an Object value like that received from a DataTable.
      bool setup = true;
      object obj = setup;

      // Gets a short value from an object.
      bool value = NetCommon.GetBoolean(obj);
    }

    // Gets a byte value from an object.
    private static void GetByte()
    {
      // Simulates an Object value like that received from a DataTable.
      byte setup = Convert.ToByte('A');
      object obj = setup;

      // Gets a byte value from an object.
      byte value = NetCommon.GetByte(obj);
    }

    // Gets a char value from an object.
    private static void GetChar()
    {
      // Simulates an Object value like that received from a DataTable.
      char setup = Convert.ToChar('A');
      object obj = setup;

      // Gets a byte value from an object.
      char value = NetCommon.GetChar(obj);
    }

    // Gets a decimal value from an object.
    private static void GetDecimal()
    {
      // Simulates an Object value like that received from a DataTable.
      decimal setup = 3.14m;
      object obj = setup;

      // Gets a decimal value from an object.
      decimal value = NetCommon.GetDecimal(obj);
    }

    // Gets a short value from an object.
    private static void GetInt16()
    {
      // Simulates an Object value like that received from a DataTable.
      short setup = 3;
      object obj = setup;

      // Gets a short value from an object.
      short value = NetCommon.GetInt16(obj);
    }

    // Gets an int value from an object.
    private static void GetInt32()
    {
      // Simulates an Object value like that received from a DataTable.
      int setup = 3;
      object obj = setup;

      // Gets an int value from an object.
      int value = NetCommon.GetInt32(obj);
    }

    // Gets a long value from an object.
    private static void GetInt64()
    {
      // Simulates an Object value like that received from a DataTable.
      long setup = 3;
      object obj = setup;

      // Gets a long value from an object.
      long value = NetCommon.GetInt64(obj);
    }

    // Gets a trimmed string value from an object.
    private static void GetString()
    {
      // Simulates an Object value like that received from a DataTable.
      string setup = "3";
      object obj = setup;

      // Gets a string value from an object.
      string value = NetCommon.GetString(obj);
    }
    #endregion

    #region Serialization Functions

    // Deserialize an XML message file to an object.
    private static void XmlDeserialize()
    {
      // Setup
      var person = new Person()
      {
        Id = 1,
        Name = "Text",
        PrincipleFlag = true
      };
      string file = "Text.xml";
      //NetCommon.XmlSerialize(person.GetType(), person, null, file);
      NetCommon.XmlSerialize(typeof(Person), person, null, file);

      // Deserialize an XML message file to an object.
      Person newPerson;
      newPerson = NetCommon.XmlDeserialize(typeof(Person), file) as Person;
    }

    // Deserialize an XML message string to an object.
    private static void XmlDeserializeMessage()
    {
      // Setup
      var person = new Person()
      {
        Id = 2,
        Name = "Text",
        PrincipleFlag = true
      };
      string message = NetCommon.XmlSerializeToString(person.GetType(), person, null);

      // Deserialize an XML message string to an object.
      Person newPerson;
      newPerson = NetCommon.XmlDeserializeMessage(typeof(Person), message) as Person;
    }

    // Serialize an object to an XML message file.
    private static void XmlSerialize()
    {
      // Setup
      var person = new Person()
      {
        Id = 2,
        Name = "Text",
        PrincipleFlag = true
      };
      string file = "Text.xml";

      // Serialize an object to an XML message file.
      NetCommon.XmlSerialize(person.GetType(), person, null, file);

      // Check the object.
      Person newPerson;
      newPerson = NetCommon.XmlDeserialize(typeof(Person), file) as Person;
    }

    // Serialize an object to an XML message string.
    private static void XmlSerializeToString()
    {
      // Serialize an object to an XML message string.
      var person = new Person()
      {
        Id = 2,
        Name = "Text",
        PrincipleFlag = true
      };
      string message = NetCommon.XmlSerializeToString(person.GetType(), person, null);

      // Check the object.
      Person newPerson;
      newPerson = NetCommon.XmlDeserializeMessage(typeof(Person), message) as Person;
    }
    #endregion
  }

  public class Person
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool PrincipleFlag { get; set; }
  }
}
