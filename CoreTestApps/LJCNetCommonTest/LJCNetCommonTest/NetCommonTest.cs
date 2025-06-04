// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// NetCommonTest.cs
using LJCNetCommon;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace LJCNetCommonTest
{
  internal class NetCommonTest
  {
    public static void Test()
    {
      TestCommon = new TestCommon("NetCommon");
      Console.WriteLine();
      Console.WriteLine("*** NetCommon ***");

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
      XmlEncode();
      XmlDecode();

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

      // Create Text from Base64 byte array.
      string text = NetCommon.Base64BytesToText(base64Bytes);
      var result = text;
      var compare = "Text";
      TestCommon.Write("Base64BytesToText()", result, compare);
    }

    // Encodes a Text value to a Base64 byte array.
    private static void TextToBase64Bytes()
    {
      // Create Base64 byte array from text.
      byte[] base64Bytes = NetCommon.TextToBase64Bytes("Text");

      // Check the text.
      string text = NetCommon.Base64BytesToText(base64Bytes);
      var result = text;
      var compare = "Text";
      TestCommon.Write("TextToBase64Bytes()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("Base64BytesToTextBytes()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("TextBytesToBase64Bytes()", result, compare);
    }

    // Decodes a Base64 value to a Text value.
    private static void Base64ToText()
    {
      // Setup
      string base64 = NetCommon.TextToBase64("Text");

      // Decodes a Base64 string to Text.
      string text = NetCommon.Base64ToText(base64);
      var result = text;
      var compare = "Text";
      TestCommon.Write("Base64BytesToText()", result, compare);
    }

    // Encodes a Text value to a Base64 value.
    private static void TextToBase64()
    {
      string text = "Text";

      // Encodes text to a Base64 string.
      string base64 = NetCommon.TextToBase64(text);

      // Check the text.
      text = NetCommon.Base64ToText(base64);
      var result = text;
      var compare = "Text";
      TestCommon.Write("TextToBase64()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("Base64ToTextBytes()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("TextBytesToBase64()", result, compare);
    }

    // Creates text from a byte array.
    private static void BytesToText()
    {
      // Setup
      byte[] bytes = NetCommon.TextToBytes("Text");

      // Creates text from a byte array.
      string text = NetCommon.BytesToText(bytes);
      var result = text;
      var compare = "Text";
      TestCommon.Write("BytesToText()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("TextToBytes()", result, compare);
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
      var result = text;
      var compare = "Text";
      TestCommon.Write("MemStreamToBytes()", result, compare);
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
        var result = text;
        var compare = "Text";
        TestCommon.Write("BytesToMemStream()", result, compare);
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
        var result = text;
        var compare = "Text";
        TestCommon.Write("MemStreamToString()", result, compare);
      }
    }

    // Creates a memory stream from a string.
    private static void StringToMemStream()
    {
      using (Stream stream = NetCommon.StringToMemStream("Text"))
      {
        // Check the text.
        string text = NetCommon.MemStreamToString(stream);
        var result = text;
        var compare = "Text";
        TestCommon.Write("StringToMemStream()", result, compare);
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
      var result = text;
      var compare = "<text>Here & There</text>";
      TestCommon.Write("XmlDecode()", result, compare);
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
      var result = text;
      var compare = "<text>Here & There</text>";
      TestCommon.Write("XmlEncode()", result, compare);
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
      var result = value.ToString();
      var compare = "True";
      TestCommon.Write("GetBoolean()", result, compare);
    }

    // Gets a byte value from an object.
    private static void GetByte()
    {
      // Simulates an Object value like that received from a DataTable.
      byte setup = Convert.ToByte('A');
      object obj = setup;

      // Gets a byte value from an object.
      byte value = NetCommon.GetByte(obj);
      var result = value.ToString();
      var compare = "65";
      TestCommon.Write("GetByte()", result, compare);
    }

    // Gets a char value from an object.
    private static void GetChar()
    {
      // Simulates an Object value like that received from a DataTable.
      char setup = Convert.ToChar('A');
      object obj = setup;

      // Gets a byte value from an object.
      char value = NetCommon.GetChar(obj);
      var result = value.ToString();
      var compare = "A";
      TestCommon.Write("GetChar()", result, compare);
    }

    // Gets a decimal value from an object.
    private static void GetDecimal()
    {
      // Simulates an Object value like that received from a DataTable.
      decimal setup = 3.14m;
      object obj = setup;

      // Gets a decimal value from an object.
      decimal value = NetCommon.GetDecimal(obj);
      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write("GetDecimal()", result, compare);
    }

    // Gets a short value from an object.
    private static void GetInt16()
    {
      // Simulates an Object value like that received from a DataTable.
      short setup = 3;
      object obj = setup;

      // Gets a short value from an object.
      short value = NetCommon.GetInt16(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt16()", result, compare);
    }

    // Gets an int value from an object.
    private static void GetInt32()
    {
      // Simulates an Object value like that received from a DataTable.
      int setup = 3;
      object obj = setup;

      // Gets an int value from an object.
      int value = NetCommon.GetInt32(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt32()", result, compare);
    }

    // Gets a long value from an object.
    private static void GetInt64()
    {
      // Simulates an Object value like that received from a DataTable.
      long setup = 3;
      object obj = setup;

      // Gets a long value from an object.
      long value = NetCommon.GetInt64(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon.Write("GetInt64()", result, compare);
    }

    // Gets a trimmed string value from an object.
    private static void GetString()
    {
      // Simulates an Object value like that received from a DataTable.
      string setup = "3";
      object obj = setup;

      // Gets a string value from an object.
      string value = NetCommon.GetString(obj);
      var result = value;
      var compare = "3";
      TestCommon.Write("GetString", result, compare);
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
      var result = $"{newPerson.Id}, {newPerson.Name}" +
        $", {newPerson.PrincipleFlag}";
      var compare = "1, Text, True";
      TestCommon.Write("XmlDeserialize()", result, compare);
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
      var result = $"{newPerson.Id}, {newPerson.Name}" +
        $", {newPerson.PrincipleFlag}";
      var compare = "2, Text, True";
      TestCommon.Write("XmlDeserializeMessage()", result, compare);
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
      var result = $"{newPerson.Id}, {newPerson.Name}" +
        $", {newPerson.PrincipleFlag}";
      var compare = "2, Text, True";
      TestCommon.Write("XmlSerialize()", result, compare);
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
      var result = $"{newPerson.Id}, {newPerson.Name}" +
        $", {newPerson.PrincipleFlag}";
      var compare = "2, Text, True";
      TestCommon.Write("XmlSerializeToString()", result, compare);
    }
    #endregion

    #region Class Data

    private static TestCommon TestCommon { get; set; }
    #endregion
  }

  public class Person
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool PrincipleFlag { get; set; }
  }
}
