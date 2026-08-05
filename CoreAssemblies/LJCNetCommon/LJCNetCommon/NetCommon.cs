// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetCommon.cs
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Contains common static methods.
  /// <include file='Doc/NetCommon.xml'
  ///  path='members/NetCommon/*'/>
  public class NetCommon
  {
    #region Common Methods

    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCMinSqlDate/*'/>
    public static LJCDataValues Keys(string propertyName, string searchValue)
    {
      return new LJCDataValues()
      {
        { propertyName, searchValue },
      };
    }

    // Get the minimum date value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/MinSqlDate/*'/>
    public static string MinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }
    #endregion

    #region Check Values Methods

    // Check for missing argument of type: string with no value, null, 
    // integer = 0, IList with no items, decimal = 0 or DataTable with no rows.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/CheckArgument/*'/>
    public static void CheckArgument<T>(T argument)
    {
      if (argument != null
        && typeof(string) == argument.GetType())
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

      if (typeof(int) == argument.GetType()
        || typeof(long) == argument.GetType()
        || typeof(short) == argument.GetType())
      {
        if (0 == GetInt64(argument))
        {
          var message = $"Argument {nameof(argument)} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(IList) == argument.GetType())
      {
        if (HasListItems((IList)argument))
        {
          var message = $"Missing argument {nameof(argument)}.";
          throw new ArgumentNullException(message);
        }
      }

      if (typeof(decimal) == argument.GetType()
        || typeof(double) == argument.GetType()
        || typeof(float) == argument.GetType())
      {
        if (0 == GetDouble(argument))
        {
          var message = $"Argument {nameof(argument)} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(DataTable) == argument.GetType())
      {
        DataTable dataTable = argument as DataTable;
        if (HasData(dataTable))
        {
          var message = $"Missing argument {nameof(argument)}.";
          throw new ArgumentNullException(message);
        }
      }
    }

    // Compare null values.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/CompareNull/*'/>
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

    // Checks a DataColumns collection for items.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasColumns/*'/>
    public static bool HasColumns(DataColumnCollection dataColumns)
    {
      bool retValue = true;

      if (dataColumns != null
        && dataColumns.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks a data table for columns definitions.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasColumns1/*'/>
    public static bool HasColumns(DataTable dataTable)
    {
      bool retValue = true;

      if (HasColumns(dataTable.Columns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks a data table for rows.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasData/*'/>
    public static bool HasData(DataTable dataTable)
    {
      bool retValue = false;

      if (dataTable != null
        && dataTable.Rows != null
        && dataTable.Rows.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks an array for elements.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasElements/*'/>
    public static bool HasElements(object array)
    {
      bool retValue = false;

      object[] setArray = null;
      if (array != null
        && array.GetType().IsArray)
      {
        setArray = (object[])array;
      }
      if (setArray != null
        && setArray.Length > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks an IList collection for items.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasItems/*'/>
    [Obsolete ("Use HasListItems()")]
    public static bool HasItems(IList list)
    {
      bool retValue = false;

      if (list != null
        && list.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks an IList collection for items.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasListItems/*'/>
    public static bool HasListItems(IList list)
    {
      bool retValue = false;

      if (list != null
        && list.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks a DataSet for tables.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasTables/*'/>
    public static bool HasTables(DataSet dataSet)
    {
      bool retValue = false;

      if (dataSet != null
        && dataSet.Tables != null
        && dataSet.Tables.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if a text value exists.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/HasText/*'/>
    public static bool HasText(string text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }

    // Checks if two values are equal.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/IsEqual/*'/>
    public static bool IsEqual(object oldValue, object newValue)
    {
      bool retValue = false;

      if (null == oldValue
        && null == newValue)
      {
        retValue = true;
      }

      if (oldValue != null
        && newValue != null)
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
            if (0 == string.Compare(oldValue.ToString(), newValue.ToString()))
            {
              retValue = true;
            }
            break;
        }
      }
      return retValue;
    }

    // Check for DB Minimum date or less.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/IsDbMinDate/*'/>
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
    // Decodes a Base64 byte array to a Text value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/Base64BytesToText/*'/>
    public static string Base64BytesToText(byte[] bytes)
    {
      byte[] byteArray = NetCommon.Base64BytesToTextBytes(bytes);
      return BytesToText(byteArray);
    }

    // Encodes a Text value to a Base64 byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TextToBase64Bytes/*'/>
    public static byte[] TextToBase64Bytes(string text)
    {
      string base64 = TextToBase64(text);
      byte[] retValue = TextToBytes(base64);
      return retValue;
    }

    // ** Base64 Bytes and Text Bytes
    // Decodes a Base64 byte array to a Text byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/Base64BytesToTextBytes/*'/>
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

    // Encodes a Text byte array to a Base64 byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TextBytesToBase64Bytes/*'/>
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
    // Decodes a Base64 value to a Text value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/Base64ToText/*'/>
    public static string Base64ToText(string base64Text)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
    }

    // Encodes a Text value to a Base64 value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TextToBase64/*'/>
    public static string TextToBase64(string text)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }

    // * Text Bytes and Base64
    // Decodes a Base64 value to a Text byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/Base64ToTextBytes/*'/>
    public static byte[] Base64ToTextBytes(string base64)
    {
      string text = Base64ToText(base64);
      byte[] retValue = TextToBytes(text);
      return retValue;
    }

    // Encodes a Text byte array to a Base64 value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TextBytesToBase64/*'/>
    public static string TextBytesToBase64(byte[] bytes)
    {
      byte[] base64Bytes = TextBytesToBase64Bytes(bytes);
      string retValue = BytesToText(base64Bytes);
      return retValue;
    }

    // ** Bytes and Text
    // Creates text from a byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/BytesToText/*'/>
    public static string BytesToText(byte[] bytes)
    {
      string retValue = null;

      if (bytes != null)
      {
        retValue = Encoding.UTF8.GetString(bytes);
      }
      return retValue;
    }

    // Creates a byte array from text.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TextToBytes/*'/>
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
    // Copies a memory stream to a byte array.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/MemStreamToBytes/*'/>
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

    // Copies a byte array to a memory stream.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/BytesToMemStream/*'/>
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
    // Creates a string from a memory stream.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/MemStreamToString/*'/>
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

    // Creates a memory stream from a string.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/StringToMemStream/*'/>
    public static Stream StringToMemStream(string text)
    {
      Stream retValue = new MemoryStream();

      TextWriter writer = new StreamWriter(retValue);
      writer.Write(text);
      writer.Flush();
      retValue.Position = 0;
      return retValue;
    }

    // XML Entities
    // Decodes an encoded XML string.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlDecode/*'/>
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

    // Encodes a string with XML escape values.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlEncode/*'/>
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

    // Deserialize an XML message file to an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlDeserialize/*'/>
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

    // Deserialize an XML message string to an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlDeserializeMessage/*'/>
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

    // Serialize an object to an XML message file.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlSerialize/*'/>
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

    // Serialize an object to an XML message string.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/XmlSerializeToString/*'/>
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

    // Retrieves the Config bool value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ConfigBool/*'/>
    public static bool ConfigBool(string key)
    {
      bool retValue = false;

      if (NetString.HasValue(key))
      {
        string configValue = ConfigurationManager.AppSettings[key];
        if (NetString.HasValue(configValue))
        {
          _ = bool.TryParse(configValue, out retValue);
        }
      }
      return retValue;
    }

    // Retrieves the Config Color value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ConfigColor/*'/>
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
          if (color != Color.FromArgb(0, 0, 0, 0))
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }

    // Retrieves the Config string value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ConfigString/*'/>
    public static string ConfigString(string key)
    {
      string retValue = null;

      if (NetString.HasValue(key))
      {
        retValue = ConfigurationManager.AppSettings[key];
      }
      return retValue;
    }

    // Accept or Select the DataConfig.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ConsoleConfig/*'/>
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

    // Gets a decimal value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetBoolean/*'/>
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

    // Gets a byte value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetByte/*'/>
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

    // Gets a character value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetChar/*'/>
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

    // Gets a DateTime value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetDateTime/*'/>
    public static DateTime? GetDateTime(object value)
    {
      DateTime? retValue = null;

      var type = value.GetType();
      //if (typeof(DateTime) == type)
      if (typeof(string) == type)
      {
        retValue = Convert.ToDateTime(value);
      }
      return retValue;
    }

    // Gets a decimal value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetDecimal/*'/>
    public static decimal GetDecimal(object value)
    {
      decimal retValue = default;

      var type = value.GetType();
      if (typeof(decimal) == type
        || typeof(int) == type
        || typeof(long) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets a decimal value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetDouble/*'/>
    public static double GetDouble(object value)
    {
      double retValue = default;

      var type = value.GetType();
      if (typeof(double) == type
        || typeof(decimal) == type
        || typeof(float) == type
        || typeof(int) == type
        || typeof(long) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToDouble(value);
      }
      return retValue;
    }

    // Gets a short value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetInt16/*'/>
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

    // Gets an integer value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetInt32/*'/>
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

    // Gets a long value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetInt64/*'/>
    public static long GetInt64(object value)
    {
      long retValue = default;

      var type = value.GetType();
      if (typeof(int) == type
        || typeof(long) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Gets an instantiated object value.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetObject/*'/>
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

    // Gets a float value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetSingle/*'/>
    public static float GetSingle(object value)
    {
      float retValue = default;

      var type = value.GetType();
      if (typeof(float) == type
        || typeof(int) == type
        || typeof(long) == type
        || typeof(short) == type)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets a trimmed string value from an object.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/GetString/*'/>
    public static string GetString(object value)
    {
      string retValue = default;

      if (value != null)
      {
        var text = value.ToString();
        if (NetString.HasValue(text))
        {
          retValue = text.Trim();
        }
      }
      return retValue;
    }

    // Gets a short from a text string.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ToInt16/*'/>
    public static short ToInt16(string text)
    {
      _ = short.TryParse(text, out short value);
      return value;
    }

    // Gets an int from a text string.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/ToInt32/*'/>
    public static int ToInt32(string text)
    {
      _ = int.TryParse(text, out int value);
      return value;
    }
    #endregion

    #region DataType Names

    // The Boolean type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeBoolean/*'/>
    public const string TypeBoolean = "Boolean";

    // The Byte type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeByte/*'/>
    public const string TypeByte = "Byte";

    // The Char type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeChar/*'/>
    public const string TypeChar = "Char";

    // The DateTime type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeDateTime/*'/>
    public const string TypeDateTime = "DateTime";

    // Type Decimal type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeDecimal/*'/>
    public const string TypeDecimal = "Decimal";

    // The Double type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeDouble/*'/>
    public const string TypeDouble = "Double";

    // The Int16 type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeInt16/*'/>
    public const string TypeInt16 = "Int16";

    // The Int32 type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeInt32/*'/>
    public const string TypeInt32 = "Int32";

    // The Int64 type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeInt64/*'/>
    public const string TypeInt64 = "Int64";

    // The Single type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeSingle/*'/>
    public const string TypeSingle = "Single";

    // The String type name.
    /// <include file='Doc/NetCommon.xml'
    ///  path='members/TypeString/*'/>
    public const string TypeString = "String";
    #endregion
  }
}
