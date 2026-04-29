// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestLJC5Program.cs
using LJCNetCommon5;
using System.Xml.Serialization;

namespace TestLJC5
{
  // The entry class.
  internal class TestLJC5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJC");
      Console.WriteLine();
      Console.WriteLine("*** LJC ***");

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
      GetDateTime();
      GetDecimal();
      GetDouble();
      GetInt16();
      GetInt32();
      GetInt64();
      GetObject();
      GetSingle();
      GetString();

      // Serialization Functions
      XmlDeserialize();
      XmlDeserializeMessage();
      XmlSerialize();
      XmlSerializeToString();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Text Transform Functions

    // Decodes a Base64 byte array to a Text value.
    private static void Base64BytesToText()
    {
      // Setup
      byte[] base64Bytes = [86, 71, 86, 52, 100, 65, 61, 61];

      // Create Text from Base64 byte array.
      var result = LJC.Base64BytesToText(base64Bytes);
      var compare = "Text";
      TestCommon?.Write("Base64BytesToText()", result, compare);
    }

    // Encodes a Text value to a Base64 byte array.
    private static void TextToBase64Bytes()
    {
      // Create Base64 byte array from text.
      byte[] base64Bytes = LJC.TextToBase64Bytes("Text");

      // Check the text.
      var result = LJC.Base64BytesToText(base64Bytes);
      var compare = "Text";
      TestCommon?.Write("TextToBase64Bytes()", result, compare);
    }

    // Decodes a Base64 byte array to a Text byte array.
    private static void Base64BytesToTextBytes()
    {
      // Setup
      byte[] base64Bytes = [86, 71, 86, 52, 100, 65, 61, 61];

      // Decodes a Base64 byte array to a Text byte array.
      byte[] textBytes = LJC.Base64BytesToTextBytes(base64Bytes);

      // Check the text.
      var result = LJC.BytesToText(textBytes);
      var compare = "Text";
      TestCommon?.Write("Base64BytesToTextBytes()", result, compare);
    }

    // Encodes a Text byte array to a Base64 byte array.
    private static void TextBytesToBase64Bytes()
    {
      // Setup
      byte[] textBytes = [84, 101, 120, 116];

      // Encodes a byte array to a Base64 byte array.
      byte[] base64Bytes = LJC.TextBytesToBase64Bytes(textBytes);

      // Check the text.
      var result = LJC.Base64BytesToText(base64Bytes);
      var compare = "Text";
      TestCommon?.Write("TextBytesToBase64Bytes()", result, compare);
    }

    // Decodes a Base64 value to a Text value.
    private static void Base64ToText()
    {
      // Setup
      var base64 = "VGV4dA==";

      // Decodes a Base64 string to Text.
      var result = LJC.Base64ToText(base64);
      var compare = "Text";
      TestCommon?.Write("Base64BytesToText()", result, compare);
    }

    // Encodes a Text value to a Base64 value.
    private static void TextToBase64()
    {
      string text = "Text";

      // Encodes text to a Base64 string.
      var result = LJC.TextToBase64(text);

      // Check the text.
      var compare = "VGV4dA==";
      TestCommon?.Write("TextToBase64()", result, compare);
    }

    // Decodes a Base64 value to a Text byte array.
    public static void Base64ToTextBytes()
    {
      // Setup
      //string base64 = LJC.TextToBase64("Text");
      var base64 = "VGV4dA==";

      // Decodes a Base64 value to a Text byte array.
      byte[] textBytes = LJC.Base64ToTextBytes(base64);

      // Check the text.
      var result = LJC.BytesToText(textBytes);
      var compare = "Text";
      TestCommon?.Write("Base64ToTextBytes()", result, compare);
    }

    // Encodes a Text byte array to a Base64 value.
    public static void TextBytesToBase64()
    {
      // Setup
      byte[] textBytes = [84, 101, 120, 116];

      // Encodes a Text byte array to a Base64 value.
      string base64 = LJC.TextBytesToBase64(textBytes);

      // Check the text.
      string result = null;
      if (LJC.HasValue(base64))
      {
        result = LJC.Base64ToText(base64);
      }
      var compare = "Text";
      TestCommon?.Write("TextBytesToBase64()", result, compare);
    }

    // Creates text from a byte array.
    private static void BytesToText()
    {
      // Setup
      byte[] textBytes = [84, 101, 120, 116];

      // Creates text from a byte array.
      var result = LJC.BytesToText(textBytes);
      var compare = "Text";
      TestCommon?.Write("BytesToText()", result, compare);
    }

    // Creates a byte array from text.
    private static void TextToBytes()
    {
      // Setup
      string text = "Text";

      // Creates a byte array from text.
      byte[] bytes = LJC.TextToBytes(text);

      // Check the text.
      var result = LJC.BytesToText(bytes);
      var compare = "Text";
      TestCommon?.Write("TextToBytes()", result, compare);
    }

    // Copies a memory stream to a byte array.
    private static void MemStreamToBytes()
    {
      // Setup
      byte[] textBytes;
      using (Stream stream = LJC.StringToMemStream("Text"))
      {
        // Copies a memory stream to a byte array.
        textBytes = LJC.MemStreamToBytes(stream);
      }

      // Check the text.
      var result = LJC.BytesToText(textBytes);
      var compare = "Text";
      TestCommon?.Write("MemStreamToBytes()", result, compare);
    }

    // Copies a byte array to a memory stream.
    private static void BytesToMemStream()
    {
      // Setup
      byte[] bytes = LJC.TextToBytes("Text");

      // Copies a byte array to a memory stream.
      using Stream stream = LJC.BytesToMemStream(bytes);
      // Check the text.
      var result = LJC.MemStreamToString(stream);
      var compare = "Text";
      TestCommon?.Write("BytesToMemStream()", result, compare);
    }

    // Creates a string from a memory stream.
    private static void MemStreamToString()
    {
      // Setup
      var text = "Text";
      using Stream stream = LJC.StringToMemStream(text);
      // Creates a string from a memory stream.
      var result = LJC.MemStreamToString(stream);
      var compare = "Text";
      TestCommon?.Write("MemStreamToString()", result, compare);
    }

    // Creates a memory stream from a string.
    private static void StringToMemStream()
    {
      var text = "Text";
      using Stream stream = LJC.StringToMemStream(text);
      // Check the text.
      var result = LJC.MemStreamToString(stream);
      var compare = "Text";
      TestCommon?.Write("StringToMemStream()", result, compare);
    }

    // Decodes an encoded XML string.
    private static void XmlDecode()
    {
      // Setup
      string encoded = "&lt;text&gt;Here &amp; There&lt;/text&gt;";

      // Decodes an encoded XML string.
      var result = LJC.XmlDecode(encoded);
      var compare = "<text>Here & There</text>";
      TestCommon?.Write("XmlDecode()", result, compare);
    }

    // Encodes a string with XML escape values.
    private static void XmlEncode()
    {
      // Setup
      string? encoded = LJC.XmlEncode("<text>Here & There</text>");

      // Check the text.
      if (LJC.HasValue(encoded))
      {
        var result = LJC.XmlDecode(encoded);
        var compare = "<text>Here & There</text>";
        TestCommon?.Write("XmlEncode()", result, compare);
      }
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
      bool value = LJC.GetBoolean(obj);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("GetBoolean()", result, compare);
    }

    // Gets a byte value from an object.
    private static void GetByte()
    {
      // Simulates an Object value like that received from a DataTable.
      byte setup = Convert.ToByte('A');
      object obj = setup;

      // Gets a byte value from an object.
      byte value = LJC.GetByte(obj);
      var result = value.ToString();
      var compare = "65";
      TestCommon?.Write("GetByte()", result, compare);
    }

    // Gets a char value from an object.
    private static void GetChar()
    {
      // Simulates an Object value like that received from a DataTable.
      char setup = Convert.ToChar('A');
      object obj = setup;

      // Gets a byte value from an object.
      char value = LJC.GetChar(obj);
      var result = value.ToString();
      var compare = "A";
      TestCommon?.Write("GetChar()", result, compare);
    }

    // Gets a DateTime value from an object.
    private static void GetDateTime()
    {
      // Simulates an Object value like that received from a DataTable.
      string setup = "2026/06/15 12:30:00 PM";
      object obj = setup;

      // Gets a decimal value from an object.
      DateTime? returnValue = LJC.GetDateTime(obj);
      if (returnValue != null)
      {
        DateTime value = (DateTime)returnValue;
        var result = $"{value:yyyy/MM/dd HH:mm:ss tt}";
        //result = value.ToString("yyyy/MM/dd HH:mm:ss tt");
        var compare = "2026/06/15 12:30:00 PM";
        TestCommon?.Write("GetDateTime()", result, compare);
      }
    }

    // Gets a decimal value from an object.
    private static void GetDecimal()
    {
      // Simulates an Object value like that received from a DataTable.
      decimal setup = 3.14m;
      object obj = setup;

      // Gets a decimal value from an object.
      decimal value = LJC.GetDecimal(obj);
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetDecimal()", result, compare);
    }

    // Gets a double value from an object.
    private static void GetDouble()
    {
      // Simulates an Object value like that received from a DataTable.
      double setup = 3.14d;
      object obj = setup;

      // Gets a decimal value from an object.
      double value = LJC.GetDouble(obj);
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetDouble()", result, compare);
    }

    // Gets a short value from an object.
    private static void GetInt16()
    {
      // Simulates an Object value like that received from a DataTable.
      short setup = 3;
      object obj = setup;

      // Gets a short value from an object.
      short value = LJC.GetInt16(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt16()", result, compare);
    }

    // Gets an int value from an object.
    private static void GetInt32()
    {
      // Simulates an Object value like that received from a DataTable.
      int setup = 3;
      object obj = setup;

      // Gets an int value from an object.
      int value = LJC.GetInt32(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt32()", result, compare);
    }

    // Gets a long value from an object.
    private static void GetInt64()
    {
      // Simulates an Object value like that received from a DataTable.
      long setup = 3;
      object obj = setup;

      // Gets a long value from an object.
      long value = LJC.GetInt64(obj);
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("GetInt64()", result, compare);
    }

    private static void GetObject()
    {
    }

    // Gets a float (single) value from an object.
    private static void GetSingle()
    {
      // Simulates an Object value like that received from a DataTable.
      float setup = 3.14f;
      object obj = setup;

      // Gets a decimal value from an object.
      float value = LJC.GetSingle(obj);
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("GetDouble()", result, compare);
    }

    // Gets a trimmed string value from an object.
    private static void GetString()
    {
      // Simulates an Object value like that received from a DataTable.
      string setup = "3";
      object obj = setup;

      // Gets a string value from an object.
      string? value = LJC.GetString(obj);
      var result = value;
      var compare = "3";
      TestCommon?.Write("GetString", result, compare);
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
      //LJC.XmlSerialize(person.GetType(), person, null, file);
      LJC.XmlSerialize(typeof(Person), person, null, file);

      // Deserialize an XML message file to an object.
      Person? newPerson;
      newPerson = LJC.XmlDeserialize(typeof(Person), file) as Person;
      if (null == newPerson)
      {
        Console.WriteLine("XmlDeserialize()1 newPerson is null.");
      }
      else
      {
        var result = $"{newPerson.Id}, {newPerson.Name}" +
            $", {newPerson.PrincipleFlag}";
        var compare = "1, Text, True";
        TestCommon?.Write("XmlDeserialize()2", result, compare);
      }
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
      //string message = LJC.XmlSerializeToString(person.GetType(), person
      //  , null);
      var nameSpaces = new XmlSerializerNamespaces();
      string message = LJC.XmlSerializeToString(person.GetType(), person
        , nameSpaces);

      // Deserialize an XML message string to an object.
      Person? newPerson;
      newPerson = LJC.XmlDeserializeMessage(typeof(Person), message) as Person;
      if (null == newPerson)
      {
        Console.WriteLine("XmlDeserializeMessage()1 newPerson is null.");
      }
      else
      {
        var result = $"{newPerson.Id}, {newPerson.Name}" +
        $", {newPerson.PrincipleFlag}";
        var compare = "2, Text, True";
        TestCommon?.Write("XmlDeserializeMessage()2", result, compare);
      }
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
      LJC.XmlSerialize(person.GetType(), person, null, file);

      // Check the object.
      Person? newPerson;
      newPerson = LJC.XmlDeserialize(typeof(Person), file) as Person;
      if (null == newPerson)
      {
        Console.WriteLine("XmlSerialize()1 newPerson is null.");
      }
      else
      {
        var result = $"{newPerson.Id}, {newPerson.Name}" +
          $", {newPerson.PrincipleFlag}";
        var compare = "2, Text, True";
        TestCommon?.Write("XmlSerialize()2", result, compare);
      }
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
      //string message = LJC.XmlSerializeToString(person.GetType(), person
      //  , null);
      var nameSpaces = new XmlSerializerNamespaces();
      string message = LJC.XmlSerializeToString(person.GetType(), person
        , nameSpaces);

      // Check the object.
      Person? newPerson;
      newPerson = LJC.XmlDeserializeMessage(typeof(Person), message) as Person;
      if (null == newPerson)
      {
        Console.WriteLine("XmlSerializeToString()1 newPerson is null.");
      }
      else
      {
        var result = $"{newPerson.Id}, {newPerson.Name}" +
          $", {newPerson.PrincipleFlag}";
        var compare = "2, Text, True";
        TestCommon?.Write("XmlSerializeToString()2", result, compare);
      }
    }
    #endregion

    #region Class Data

    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }

  public class Person
  {
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool PrincipleFlag { get; set; }
  }
}
