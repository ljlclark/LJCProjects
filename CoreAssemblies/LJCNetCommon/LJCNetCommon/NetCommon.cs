// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetCommon.cs
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Data;
using System.Collections;

namespace LJCNetCommon
{
  // Contains common static functions. (D)
  /// <include path='items/NetCommon/*' file='Doc/NetCommon.xml'/>
  public class NetCommon
  {
    #region Check Values Functions

    // Check for missing argument of type: string with no value, null, 
    // integer = 0, IList with no items, decimal = 0 or DataTable with no rows.
    /// <include path='items/CheckArgument/*' file='Doc/NetCommon.xml'/>
    public static void CheckArgument<T>(T argument)
    {
      if (typeof(string) == argument.GetType())
      {
        if (!NetString.HasValue(GetString(argument)))
        {
          var message = $"Missing argument {nameof(argument)}.";
          throw new ArgumentNullException(message);
        }
      }

      if (null == argument)
      {
        var message = $"Missing argument {nameof(argument)}.";
        throw new ArgumentNullException(message);
      }

      if (typeof(long) == argument.GetType()
        || typeof(int) == argument.GetType()
        || typeof(short) == argument.GetType())
      {
        if (0 == NetCommon.GetInt64(argument))
        {
          var message = $"Argument {nameof(argument)} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(IList) == argument.GetType())
      {
        if (NetCommon.HasItems((IList)argument))
        {
          var message = $"Missing argument {nameof(argument)}.";
          throw new ArgumentNullException(message);
        }
      }

      if (typeof(double) == argument.GetType()
        || typeof(decimal) == argument.GetType()
        || typeof(float) == argument.GetType())
      {
        if (0 == NetCommon.GetDouble(argument))
        {
          var message = $"Argument {nameof(argument)} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(DataTable) == argument.GetType())
      {
        DataTable dataTable = argument as DataTable;
        if (NetCommon.HasData(dataTable))
        {
          var message = $"Missing argument {nameof(argument)}.";
          throw new ArgumentNullException(message);
        }
      }
    }

    // Compare null values. (DE)
    /// <include path='items/CompareNull/*' file='Doc/NetCommon.xml'/>
    public static int CompareNull(object x, object y)
    {
      int retValue;

      retValue = -2;

      if (null == x)
      {
        // Null is less than not null.
        retValue = -1;
      }

      if (null == y)
      {
        // Not null is greater than null.
        retValue = 1;
      }

      if (null == x
        && null == y)
      {
        // Null is equal to null.
        retValue = 0;
      }
      return retValue;
    }

    // Checks a data table for columns definitions.
    /// <include path='items/HasColumns/*' file='Doc/NetCommon.xml'/>
    public static bool HasColumns(DataTable dataTable)
    {
      bool retValue = true;

      if (dataTable.Columns != null
        && dataTable.Columns.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks a data table and returns true if it contains any rows. (E)
    /// <include path='items/HasData/*' file='Doc/NetCommon.xml'/>
    public static bool HasData(DataTable dataTable)
    {
      bool retValue = true;

      if (dataTable == null || dataTable.Rows == null
        || dataTable.Rows.Count == 0)
      {
        retValue = false;
      }
      return retValue;
    }

    // Checks an IList collection for items.
    /// <include path='items/HasItems/*' file='Doc/NetCommon.xml'/>
    public static bool HasItems(IList list)
    {
      bool retValue = false;

      if (list != null && list.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if two values are equal.
    /// <include path='items/IsEqual/*' file='Doc/NetCommon.xml'/>
    public static bool IsEqual(object oldValue, object newValue)
    {
      bool retValue = false;

      if (null == oldValue && null == newValue)
      {
        retValue = true;
      }

      if (oldValue != null && newValue != null)
      {
        string typeName = oldValue.GetType().Name;
        switch (typeName)
        {
          case TypeBoolean:
            if (GetBoolean(oldValue) == GetBoolean(newValue))
            {
              retValue = true;
            }
            break;

          case TypeByte:
            if (GetByte(oldValue) == GetByte(newValue))
            {
              retValue = true;
            }
            break;

          case TypeChar:
            if (GetChar(oldValue) == GetChar(newValue))
            {
              retValue = true;
            }
            break;

          case TypeDateTime:
            if (GetDateTime(oldValue) == GetDateTime(newValue))
            {
              retValue = true;
            }
            break;

          case TypeDecimal:
            if (GetDecimal(oldValue) == GetDecimal(newValue))
            {
              retValue = true;
            }
            break;

          case TypeDouble:
            if (GetDouble(oldValue) == GetDouble(newValue))
            {
              retValue = true;
            }
            break;

          case TypeInt16:
            if (GetInt16(oldValue) == GetInt16(newValue))
            {
              retValue = true;
            }
            break;

          case TypeInt32:
            if (GetInt32(oldValue) == GetInt32(newValue))
            {
              retValue = true;
            }
            break;

          case TypeInt64:
            if (GetInt64(oldValue) == GetInt64(newValue))
            {
              retValue = true;
            }
            break;

          case TypeSingle:
            if (GetSingle(oldValue) == GetSingle(newValue))
            {
              retValue = true;
            }
            break;

          case TypeString:
            if (0 == string.Compare(oldValue.ToString(), newValue.ToString()
              , true))
            {
              retValue = true;
            }
            break;
        }
      }
      return retValue;
    }

    // Check for DB Minimum date or less.
    /// <include path='items/IsDbMinDate/*' file='Doc/NetCommon.xml'/>
    public static bool IsDbMinDate(DateTime? dateTime)
    {
      bool retValue = false;
      if (dateTime != null)
      {
        DateTime tempDateTime = (DateTime)dateTime;
        if (tempDateTime.Year < 1753)
        {
          retValue = true;
        }
        if (1753 == tempDateTime.Year
          && 1 == tempDateTime.Month
          && 1 == tempDateTime.Day)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion

    #region Text Transform Functions

    // ** Base64 Bytes and Text
    // Decodes a Base64 byte array to a Text value. (E)
    /// <include path='items/Base64BytesToText/*' file='Doc/NetCommon.xml'/>
    public static string Base64BytesToText(byte[] bytes)
    {
      byte[] byteArray = NetCommon.Base64BytesToTextBytes(bytes);
      return NetCommon.BytesToText(byteArray);
    }

    // Encodes a Text value to a Base64 byte array. (E)
    /// <include path='items/TextToBase64Bytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] TextToBase64Bytes(string text)
    {
      string base64 = NetCommon.TextToBase64(text);
      byte[] retValue = NetCommon.TextToBytes(base64);
      return retValue;
    }

    // ** Base64 Bytes and Text Bytes
    // Decodes a Base64 byte array to a Text byte array. (E)
    /// <include path='items/Base64BytesToTextBytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] Base64BytesToTextBytes(byte[] bytes)
    {
      char[] chars;
      byte[] retValue;

      // Copy bytes to chars.
      chars = new char[bytes.Length];
      Array.Copy(bytes, chars, chars.Length);

      // Convert Base64 chars to original bytes.
      retValue = Convert.FromBase64CharArray(chars, 0, chars.Length);
      return retValue;
    }

    // Encodes a Text byte array to a Base64 byte array. (E)
    /// <include path='items/TextBytesToBase64Bytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] TextBytesToBase64Bytes(byte[] bytes)
    {
      char[] chars;
      long charsLength;
      byte[] retValue;

      // Convert bytes to Base64 chars.
      charsLength = (long)((4.0d / 3.0d) * bytes.Length);
      if (charsLength % 4 != 0)
      {
        charsLength += 4 - charsLength % 4;
      }
      chars = new char[charsLength];
      Convert.ToBase64CharArray(bytes, 0, bytes.Length, chars, 0);

      // Copy chars to bytes.
      retValue = new byte[chars.Length];
      for (int index = 0; index < retValue.Length; index++)
      {
        retValue[index] = (byte)chars[index];
      }
      return retValue;
    }

    // ** Base64 and Text
    // Decodes a Base64 value to a Text value. (E)
    /// <include path='items/Base64ToText/*' file='Doc/NetCommon.xml'/>
    public static string Base64ToText(string base64Text)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
    }

    // Encodes a Text value to a Base64 value. (E)
    /// <include path='items/TextToBase64/*' file='Doc/NetCommon.xml'/>
    public static string TextToBase64(string text)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }

    // * Text Bytes and Base64
    // Decodes a Base64 value to a Text byte array. (E)
    /// <include path='items/Base64ToTextBytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] Base64ToTextBytes(string base64)
    {
      string text = Base64ToText(base64);
      byte[] retValue = TextToBytes(text);
      return retValue;
    }

    // Encodes a Text byte array to a Base64 value. (E)
    /// <include path='items/TextBytesToBase64/*' file='Doc/NetCommon.xml'/>
    public static string TextBytesToBase64(byte[] bytes)
    {
      byte[] base64Bytes = TextBytesToBase64Bytes(bytes);
      string retValue = BytesToText(base64Bytes);
      return retValue;
    }

    // ** Bytes and Text
    // Creates text from a byte array. (E)
    /// <include path='items/BytesToText/*' file='Doc/NetCommon.xml'/>
    public static string BytesToText(byte[] bytes)
    {
      string retValue = Encoding.UTF8.GetString(bytes);
      return retValue;
    }

    // Creates a byte array from text. (E)
    /// <include path='items/TextToBytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] TextToBytes(string text)
    {
      string data = text.Trim();
      byte[] retValue = new byte[data.Length];

      for (int index = 0; index < data.Length; index++)
      {
        retValue[index] = Convert.ToByte(data[index]);
      }
      return retValue;
    }

    // ** Stream and Bytes
    // Copies a memory stream to a byte array. (E)
    /// <include path='items/MemStreamToBytes/*' file='Doc/NetCommon.xml'/>
    public static byte[] MemStreamToBytes(Stream stream)
    {
      byte[] retValue = new byte[stream.Length];

      stream.Position = 0;
      for (int index = 0; index < stream.Length; index++)
      {
        retValue[index] = Convert.ToByte(stream.ReadByte());
      }
      stream.Position = 0;
      return retValue;
    }

    // Copies a byte array to a memory stream. (E)
    /// <include path='items/BytesToMemStream/*' file='Doc/NetCommon.xml'/>
    public static Stream BytesToMemStream(byte[] bytes)
    {
      MemoryStream retValue;

      retValue = new MemoryStream(bytes, 0, bytes.Length)
      {
        Position = 0
      };
      return retValue;
    }

    // ** Stream and String (Text)
    // Creates a string from a memory stream. (E)
    /// <include path='items/MemStreamToString/*' file='Doc/NetCommon.xml'/>
    public static string MemStreamToString(Stream stream)
    {
      TextReader reader;
      string retValue;

      stream.Position = 0;
      reader = new StreamReader(stream);
      retValue = reader.ReadToEnd();
      reader.Close();
      return retValue;
    }

    // Creates a memory stream from a string. (E)
    /// <include path='items/StringToMemStream/*' file='Doc/NetCommon.xml'/>
    public static Stream StringToMemStream(string text)
    {
      Stream retValue = new MemoryStream();

      TextWriter writer = new StreamWriter(retValue);
      writer.Write(text);
      writer.Flush();
      retValue.Position = 0;
      return retValue;
    }

    // **
    // Decodes an encoded XML string. (E)
    /// <include path='items/XmlDecode/*' file='Doc/NetCommon.xml'/>
    public static string XmlDecode(string text)
    {
      string retValue = null;

      if (NetString.HasValue(text))
      {
        retValue = text.Replace("&lt;", "<");
        retValue = retValue.Replace("&gt;", ">");
        retValue = retValue.Replace("&apos;", "'");
        retValue = retValue.Replace("&quot;", "\"");
        retValue = retValue.Replace("&amp;", "&");
      }
      return retValue;
    }

    // Encodes a string with XML escape values. (E)
    /// <include path='items/XmlEncode/*' file='Doc/NetCommon.xml'/>
    public static string XmlEncode(string text)
    {
      string retValue = null;

      if (NetString.HasValue(text))
      {
        retValue = text.Replace("&", "&amp;");
        retValue = retValue.Replace("<", "&lt;");
        retValue = retValue.Replace(">", "&gt;");
        retValue = retValue.Replace("'", "&apos;");
        retValue = retValue.Replace("\"", "&quot;");
      }
      return retValue;
    }
    #endregion

    #region Serialization Functions

    // Deserialize an XML message file to an object. (E)
    /// <include path='items/XmlDeserialize/*' file='Doc/NetCommon.xml'/>
    public static object XmlDeserialize(Type type, string fileSpec
      , string rootName = null)
    {
      string errorText;
      object retValue = null;

      if (!File.Exists(fileSpec))
      {
        errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        FileStream fileStream = null;
        try
        {
          XmlSerializer serializer;
          if (NetString.HasValue(rootName))
          {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            serializer = new XmlSerializer(type, root);
          }
          else
          {
            serializer = new XmlSerializer(type);
          }
          fileStream = new FileStream(fileSpec, FileMode.Open);
          try
          {
            retValue = serializer.Deserialize(fileStream);
          }
          catch (Exception e)
          {
            errorText = e.Message;
            throw new Exception(errorText);
          }
        }
        finally
        {
          fileStream?.Close();
        }
      }
      return retValue;
    }

    // Deserialize an XML message string to an object. (E)
    /// <include path='items/XmlDeserializeMessage/*' file='Doc/NetCommon.xml'/>
    public static object XmlDeserializeMessage(Type type, string message)
    {
      Stream stream = null;
      object retValue = null;

      try
      {
        stream = StringToMemStream(message);
        XmlSerializer serializer = new XmlSerializer(type);
        retValue = serializer.Deserialize(stream);
      }
      finally
      {
        stream?.Close();
      }
      return retValue;
    }

    // Serialize an object to an XML message file. (E)
    /// <include path='items/XmlSerialize/*' file='Doc/NetCommon.xml'/>
    public static void XmlSerialize(Type type, object data
      , XmlSerializerNamespaces namespaces, string fileSpec
      , string rootName = null)
    {
      FileStream fileStream;
      string errorText;

      if (!NetString.HasValue(fileSpec))
      {
        errorText = "Missing file specification.";
        throw new ArgumentException(errorText);
      }

      string folder = Path.GetDirectoryName(fileSpec);
      if (NetString.HasValue(folder)
        && !Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }

      // Serialize to XML.
      XmlSerializer serializer;
      if (NetString.HasValue(rootName))
      {
        XmlRootAttribute root = new XmlRootAttribute(rootName);
        serializer = new XmlSerializer(type, root);
      }
      else
      {
        serializer = new XmlSerializer(type);
      }
      fileStream = new FileStream(fileSpec, FileMode.Create);
      try
      {
        if (namespaces == null)
        {
          serializer.Serialize(fileStream, data);
        }
        else
        {
          serializer.Serialize(fileStream, data, namespaces);
        }
      }
      finally
      {
        fileStream?.Close();
      }
      return;
    }

    // Serialize an object to an XML message string. (E)
    /// <include path='items/XmlSerializeToString/*' file='Doc/NetCommon.xml'/>
    public static string XmlSerializeToString(Type type, object data
      , XmlSerializerNamespaces namespaces)
    {
      MemoryStream memStream;
      string retValue;

      // Serialize to XML.
      XmlSerializer xmlSerializer = new XmlSerializer(type);
      memStream = new MemoryStream();

      if (namespaces == null)
      {
        xmlSerializer.Serialize(memStream, data);
      }
      else
      {
        xmlSerializer.Serialize(memStream, data, namespaces);
      }

      retValue = MemStreamToString(memStream);
      memStream.Close();
      return retValue;
    }
    #endregion

    #region Config Functions

    // Retrieves the Config bool value. (RE)
    /// <include path='items/ConfigBool/*' file='Doc/NetCommon.xml'/>
    public static bool ConfigBool(string key)
    {
      bool retValue = false;

      if (NetString.HasValue(key))
      {
        string configValue = ConfigurationManager.AppSettings[key];
        if (NetString.HasValue(configValue))
        {
          bool.TryParse(configValue, out retValue);
        }
      }
      return retValue;
    }

    // Retrieves the Config Color value. (RE)
    /// <include path='items/ConfigColor/*' file='Doc/NetCommon.xml'/>
    public static bool ConfigColor(string key, out Color color)
    {
      bool retValue = false;

      color = Color.Black;

      if (NetString.HasValue(key))
      {
        string configValue = ConfigurationManager.AppSettings[key];
        if (NetString.HasValue(configValue))
        {
          color = Color.FromName(configValue);
          if (color != null)
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }

    // Retrieves the Config string value. (RE)
    /// <include path='items/ConfigString/*' file='Doc/NetCommon.xml'/>
    public static string ConfigString(string key)
    {
      string retValue = null;

      if (NetString.HasValue(key))
      {
        retValue = ConfigurationManager.AppSettings[key];
      }
      return retValue;
    }

    /// <summary>Accept or Select the DataConfig.</summary>
    public static void ConsoleConfig(string dataConfigName)
    {
      Console.Write($"Continue with DataConfig - {dataConfigName}? (Y/N) ");
      if (Console.ReadKey().Key != ConsoleKey.Y)
      {
        Console.WriteLine();
        Environment.Exit(0);
      }
      Console.WriteLine();
    }
    #endregion

    #region Value Functions

    // Gets a decimal value from an object. (E)
    /// <include path='items/GetBoolean/*' file='Doc/NetCommon.xml'/>
    public static bool GetBoolean(object value)
    {
      bool retValue = default;

      var type = value.GetType();
      if (typeof(bool) == type)
      {
        retValue = Convert.ToBoolean(value);
      }
      return retValue;
    }

    // Gets a byte value from an object. (E)
    /// <include path='items/GetByte/*' file='Doc/NetCommon.xml'/>
    public static byte GetByte(object value)
    {
      byte retValue = default;

      var type = value.GetType();
      if (typeof(byte) == type)
      {
        retValue = Convert.ToByte(value);
      }
      return retValue;
    }

    // Gets a character value from an object. (E)
    /// <include path='items/GetChar/*' file='Doc/NetCommon.xml'/>
    public static char GetChar(object value)
    {
      char retValue = default;

      var type = value.GetType();
      if (typeof(char) == type)
      {
        retValue = Convert.ToChar(value);
      }
      return retValue;
    }

    // Gets a decimal value from an object. (E)
    /// <include path='items/GetDateTime/*' file='Doc/NetCommon.xml'/>
    public static DateTime? GetDateTime(object value)
    {
      DateTime? retValue = null;

      var type = value.GetType();
      if (typeof(DateTime) == type)
      {
        retValue = Convert.ToDateTime(value);
      }
      return retValue;
    }

    // Gets a decimal value from an object. (E)
    /// <include path='items/GetDecimal/*' file='Doc/NetCommon.xml'/>
    public static decimal GetDecimal(object value)
    {
      decimal retValue = default;

      var type = value.GetType();
      if (typeof(decimal) == type
        || typeof(long) == type
        || typeof(int) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets a decimal value from an object. (E)
    /// <include path='items/GetDouble/*' file='Doc/NetCommon.xml'/>
    public static double GetDouble(object value)
    {
      double retValue = default;

      var type = value.GetType();
      if (typeof(double) == type
        || typeof(decimal) == type
        || typeof(long) == type
        || typeof(int) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToDouble(value);
      }
      return retValue;
    }

    // Gets a short value from an object. (E)
    /// <include path='items/GetInt16/*' file='Doc/NetCommon.xml'/>
    public static short GetInt16(object value)
    {
      short retValue = default;

      var type = value.GetType();
      if (typeof(short) == type)
      {
        retValue = Convert.ToInt16(value);
      }
      return retValue;
    }

    // Gets an integer value from an object. (E)
    /// <include path='items/GetInt32/*' file='Doc/NetCommon.xml'/>
    public static int GetInt32(object value)
    {
      int retValue = default;

      var type = value.GetType();
      if (typeof(int) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToInt32(value);
      }
      return retValue;
    }

    // Gets a long value from an object. (E)
    /// <include path='items/GetInt64/*' file='Doc/NetCommon.xml'/>
    public static long GetInt64(object value)
    {
      long retValue = default;

      var type = value.GetType();
      if (typeof(long) == type
        || typeof(int) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Gets an instantiated object value.
    /// <include path='items/GetObject/*' file='Doc/NetCommon.xml'/>
    public static object GetObject(object value)
    {
      object retValue = null;

      if (value != null)
      {
        var typeName = value.GetType().Name;
        switch (typeName)
        {
          case TypeBoolean:
            retValue = GetBoolean(value);
            break;

          case TypeByte:
            retValue = GetByte(value);
            break;

          case TypeChar:
            retValue = GetChar(value);
            break;

          case TypeDateTime:
            retValue = GetDateTime(value);
            break;

          case TypeDecimal:
            retValue = GetDecimal(value);
            break;

          case TypeDouble:
            retValue = GetDouble(value);
            break;

          case TypeInt16:
            retValue = GetInt16(value);
            break;

          case TypeInt32:
            retValue = GetInt32(value);
            break;

          case TypeInt64:
            retValue = GetInt64(value);
            break;

          case TypeSingle:
            retValue = GetSingle(value);
            break;

          case TypeString:
            retValue = GetString(value);
            break;
        }
      }
      return retValue;
    }

    // Gets a float value from an object. (E)
    /// <include path='items/GetSingle/*' file='Doc/NetCommon.xml'/>
    public static float GetSingle(object value)
    {
      float retValue = default;

      var type = value.GetType();
      if (typeof(Single) == type
        || typeof(long) == type
        || typeof(int) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets a trimmed string value from an object. (E)
    /// <include path='items/GetString/*' file='Doc/NetCommon.xml'/>
    public static string GetString(object value)
    {
      string retValue = default;

      if (value != null
        && NetString.HasValue(value.ToString()))
      {
        retValue = value.ToString().Trim();
      }
      return retValue;
    }
    #endregion

    #region DataType Names

    /// <summary>The Boolean type name.</summary>
    public const string TypeBoolean = "Boolean";

    /// <summary>The Byte type name.</summary>
    public const string TypeByte = "Byte";

    /// <summary>The Char type name.</summary>
    public const string TypeChar = "Char";

    /// <summary>The DateTime type name.</summary>
    public const string TypeDateTime = "DateTime";

    /// <summary>Type Decimal type name.</summary>
    public const string TypeDecimal = "Decimal";

    /// <summary>The Double type name.</summary>
    public const string TypeDouble = "Double";

    /// <summary>The Int16 type name.</summary>
    public const string TypeInt16 = "Int16";

    /// <summary>The Int32 type name.</summary>
    public const string TypeInt32 = "Int32";

    /// <summary>The Int64 type name.</summary>
    public const string TypeInt64 = "Int64";

    /// <summary>The Single type name.</summary>
    public const string TypeSingle = "Single";

    /// <summary>The String type name.</summary>
    public const string TypeString = "String";
    #endregion
  }
}
