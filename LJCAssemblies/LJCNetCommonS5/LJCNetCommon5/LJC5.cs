// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJC5.cs
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Contains common static methods.
  /// <include file='Doc/LJC.xml'
  ///  path='members/LJC/*'/>
  public class LJC
  {
    #region Common Methods

    // Formats the DateTime value to a date/time string in database format.
    /// <include file='Doc/LJC.xml'
    ///  path='members/DbDateTimeString/*'/>
    public static string DbDateTimeString(DateTime dateTime)
    {
      string retValue;

      retValue = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
      return retValue;
    }

    // Creates the keys collection.
    /// <include file='Doc/LJC.xml'
    ///  path='members/Keys/*'/>
    public static LJCDataColumns Keys(string propertyName, string searchValue)
    {
      return new LJCDataColumns()
      {
        { propertyName, searchValue },
      };
    }

    // Get the minimum date value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/MinSqlDate/*'/>
    public static string MinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Gets a short from a text string.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ToInt16/*'/>
    public static short ToInt16(string text)
    {
      _ = short.TryParse(text, out short value);
      return value;
    }

    // Gets an int from a text string.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ToInt32/*'/>
    public static int ToInt32(string text)
    {
      _ = int.TryParse(text, out int value);
      return value;
    }

    // Format the date value for display.
    /// <include file='Doc/LJC.xml'
    ///  path='members/UIDateString/*'/>
    public static string? UIDateString(DateTime? dateTime)
    {
      string? retValue = null;

      if (dateTime != null
        && !LJC.IsDbMinDate(dateTime))
      {
        DateTime tempDateTime = (DateTime)dateTime;
        retValue = tempDateTime.ToString("MM/dd/yyyy");
      }
      return retValue;
    }

    // Format the date/time value for display.
    /// <include file='Doc/LJC.xml'
    ///  path='members/UIDateTimeString/*'/>
    public static string? UIDateTimeString(DateTime dateTime)
    {
      string? retValue = null;

      if (!IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
      }
      return retValue;
    }

    // Format the date/time to time for display.
    /// <include file='Doc/LJC.xml'
    ///  path='members/UITimeString/*'/>
    public static string? UITimeString(DateTime dateTime)
    {
      string? retValue = null;

      if (!IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToShortTimeString();
        if (7 == retValue.Length)
        {
          retValue = "0" + retValue;
        }
      }
      return retValue;
    }
    #endregion

    #region Check Values Methods

    // Check for missing argument of type: string with no value, null, 
    //   , numeric, IList, DataTable.
    /// <include file='Doc/LJC.xml'
    ///  path='members/CheckArgument/*'/>
    public static bool CheckArgument<T>([NotNullWhen(true)] T? argument
      , string argName)
    {
      var retValue = true;

      if (argument != null
        && typeof(string) == argument.GetType())
      {
        if (!HasText(GetString(argument)))
        {
          var message = $"Missing argument {argName}.";
          throw new ArgumentNullException(message);
        }
      }

      if (null == argument)
      {
        var message = $"Missing argument {argName}.";
        throw new ArgumentNullException(message);
      }

      if (typeof(int) == argument.GetType()
        || typeof(long) == argument.GetType()
        || typeof(short) == argument.GetType())
      {
        if (0 == GetInt64(argument))
        {
          var message = $"Argument {argName} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(IList) == argument.GetType())
      {
        if (HasListItems((IList)argument))
        {
          var message = $"Missing argument {argName}.";
          throw new ArgumentNullException(message);
        }
      }

      if (typeof(decimal) == argument.GetType()
        || typeof(double) == argument.GetType()
        || typeof(float) == argument.GetType())
      {
        if (0 == GetDouble(argument))
        {
          var message = $"Argument {argName} is not allowed to be zero.";
          throw new ArgumentException(message);
        }
      }

      if (typeof(DataTable) == argument.GetType())
      {
        if (argument is DataTable dataTable
          && HasTableData(dataTable))
        {
          var message = $"Missing argument {argName}.";
          throw new ArgumentNullException(message);
        }
      }
      return retValue;
    }

    // Compare null values.
    /// <include file='Doc/LJC.xml'
    ///  path='members/CompareNull/*'/>
    public static int CompareNull(object? x, object? y)
    {
      int retValue;

      retValue = LJCNetString.CompareNotNullOrEqual;

      if (null == x)
      {
        // Null is less than not null.
        retValue = LJCNetString.CompareLess;
      }

      if (null == y)
      {
        // Not null is greater than null.
        retValue = LJCNetString.CompareGreater;
      }

      if (null == x
        && null == y)
      {
        // Null is equal to null.
        retValue = LJCNetString.CompareEqual;
      }
      return retValue;
    }

    // Checks a DataColumnCollection for items.
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasTableColumns1/*'/>
    public static bool HasTableColumns([NotNullWhen(true)]
      DataColumnCollection? dataColumns)
    {
      bool retValue = false;

      if (dataColumns != null
        && dataColumns.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks a table for data columns.
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasTableColumns2/*'/>
    public static bool HasTableColumns([NotNullWhen(true)] DataTable? dataTable)
    {
      bool retValue = false;

      if (dataTable != null)
      {
        retValue = HasTableColumns(dataTable.Columns);
      }
      return retValue;
    }

    // Checks a data table for rows.
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasTableData/*'/>
    public static bool HasTableData([NotNullWhen(true)] DataTable? dataTable)
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasArrayElements/*'/>
    public static bool HasArrayElements([NotNullWhen(true)] object? array)
    {
      bool retValue = false;

      object[]? setArray = null;
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasListItems/*'/>
    public static bool HasListItems([NotNullWhen(true)] IList? list)
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasTables/*'/>
    public static bool HasTables([NotNullWhen(true)] DataSet? dataSet)
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/HasText/*'/>
    public static bool HasText([NotNullWhen(true)] string? text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }

    // Checks if two values are equal.
    /// <include file='Doc/LJC.xml'
    ///  path='members/IsEqual/*'/>
    public static bool IsEqual<T>(T compareValue, T compareToValue
      , bool caseSensitive = false)
    {
      bool retValue;

      if (compareValue is string compare
        && compareToValue is string compareTo)
      {
        if (caseSensitive)
        {
          retValue = string.Equals(compare, compareTo);
        }
        else
        {
          retValue = string.Equals(compare, compareTo
            , StringComparison.OrdinalIgnoreCase);
        }
      }
      else
      {
        retValue = EqualityComparer<T>.Default.Equals(compareValue, compareToValue);
      }
      return retValue;
    }

    // Check for DB Minimum date or less.
    /// <include file='Doc/LJC.xml'
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

    #region Console Methods

    // Clears an invalid ReadKey() selection.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ClearReadKey/*'/>
    public static void ClearReadKey(string prompt, char value)
    {
      while (true)
      {
        if ("\r\t ".Contains(value))
        {
          var left = prompt.Length;
          Console.SetCursorPosition(left, Console.CursorTop);
          break;
        }

        if ("\b".Contains(value))
        {
          Console.Write(" ");
          break;
        }

        Console.Write('\b');
        Console.Write(' ');
        Console.Write('\b');
        break;
      }
    }

    // Clears an invalid ReadLine() selection.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ClearReadLine/*'/>
    public static void ClearReadLine(string prompt, string value)
    {
      var left = prompt.Length + value.Length;
      var backspaces = new string('\b', value.Length);
      var spaces = new string(' ', value.Length);
      Console.SetCursorPosition(left, Console.CursorTop - 1);
      Console.Write(backspaces);
      Console.Write(spaces);
      Console.Write(backspaces);
    }

    // Get a console character option value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/KeyOption/*'/>
    public static char KeyOption(string prompt, string options)
    {
      char retOption;

      // Get valid key.
      while (true)
      {
        while (true)
        {
          var info = Console.ReadKey();
          retOption = info.KeyChar;
          if (options.Contains(retOption))
          {
            break;
          }
          ClearReadKey(prompt, retOption);
        }

        // Wait for ENTER key.
        var success = false;
        while (true)
        {
          var info = Console.ReadKey();
          var keyChar = info.KeyChar;
          if ('\b' == keyChar)
          {
            Console.Write(' ');
            Console.Write('\b');
            break;
          }
          if ('\r' == keyChar)
          {
            success = true;
            break;
          }
          else
          {
            ClearReadKey(prompt, keyChar);
          }
        }
        if (success)
        {
          break;
        }
      }
      Console.WriteLine();
      return retOption;
    }
    #endregion

    #region Object Value Methods

    // Gets a decimal value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetBoolean/*'/>
    public static bool GetBoolean(object? value)
    {
      bool retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(bool) == type)
        {
          retValue = Convert.ToBoolean(value);
        }
      }
      return retValue;
    }

    // Gets a byte value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetByte/*'/>
    public static byte GetByte(object? value)
    {
      byte retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(byte) == type)
        {
          retValue = (byte)value;
        }
      }
      return retValue;
    }

    // Gets an array of bytes from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetBytes/*'/>
    public static byte[] GetBytes(object? value)
    {
      byte[] retValue = [];

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(byte[]) == type)
        {
          retValue = (byte[])value;
        }
      }
      return retValue;
    }

    // Gets a character value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetChar/*'/>
    public static char GetChar(object? value)
    {
      char retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(char) == type)
        {
          retValue = Convert.ToChar(value);
        }
      }
      return retValue;
    }

    // Gets a character array from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetChars/*'/>
    public static char[] GetChars(object? value)
    {
      char[] retValue = [];

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(char[]) == type)
        {
          retValue = (char[])value;
        }
      }
      return retValue;
    }

    // Gets a DateTime value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetDateTime/*'/>
    public static DateTime GetDateTime(object? value)
    {
      DateTime retValue = DateTime.MinValue;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(DateTime) == type
          || typeof(string) == type)
        {
          retValue = Convert.ToDateTime(value);
        }
      }
      return retValue;
    }

    // Gets a decimal value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetDecimal/*'/>
    public static decimal GetDecimal(object? value)
    {
      decimal retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(decimal) == type
          || typeof(float) == type
          || typeof(int) == type
          || typeof(long) == type
          || typeof(short) == type)
        {
          retValue = Convert.ToDecimal(value);
        }
      }
      return retValue;
    }

    // Gets a decimal value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetDouble/*'/>
    public static double GetDouble(object? value)
    {
      double retValue = default;

      if (value != null)
      {
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
      }
      return retValue;
    }

    // Gets a float value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetFloat/*'/>
    public static float GetFloat(object? value)
    {
      float retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(float) == type
          || typeof(int) == type
          || typeof(long) == type
          || typeof(short) == type)
        {
          retValue = Convert.ToSingle(value);
        }
      }
      return retValue;
    }

    // Returns the Guid value of the data field with the specified name. 
    /// <include file='Doc/LJC.xml'
    ///  path='items/GetGuid/*'/>
    public static Guid GetGuid(object? value)
    {
      Guid retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(Guid) == type)
        {
          var result = GetString(value);
          _ = Guid.TryParse(result, out retValue);
        }
      }
      return retValue;
    }

    // Gets a short value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetInt16/*'/>
    public static short GetInt16(object? value)
    {
      short retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(short) == type)
        {
          retValue = Convert.ToInt16(value);
        }
      }
      return retValue;
    }

    // Gets an integer value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetInt32/*'/>
    public static int GetInt32(object? value)
    {
      int retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(int) == type
          || typeof(short) == type)
        {
          retValue = Convert.ToInt32(value);
        }
      }
      return retValue;
    }

    // Gets a long value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetInt64/*'/>
    public static long GetInt64(object? value)
    {
      long retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(int) == type
          || typeof(long) == type
          || typeof(short) == type)
        {
          retValue = Convert.ToInt64(value);
        }
      }
      return retValue;
    }

    // Gets a trimmed string value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetString/*'/>
    public static string? GetString(object? value)
    {
      string? retValue = default;

      if (value != null)
      {
        var text = value.ToString();
        if (HasText(text))
        {
          retValue = text.Trim();
        }
      }
      return retValue;
    }

    // Gets an instantiated object value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetValue/*'/>
    public static object? GetValue(object value)
    {
      object? retValue = null;

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
            retValue = GetFloat(value);
            break;

          case TypeString:
            retValue = GetString(value);
            break;
        }
      }
      return retValue;
    }
    #endregion

    #region Additional Value Methods

    // Gets a database DateTime string value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetDbDateString/*'/>
    public static string? GetDbDateString(object? value)
    {
      string? retValue = default;

      if (value != null)
      {
        var type = value.GetType();
        if (type == typeof(DateTime))
        {
          var dateTime = GetDateTime(value);
          //retValue = $"'{dateTime:yyyy/MM/dd HH:mm:ss}'";
          retValue = DbDateTimeString(dateTime);
        }
      }
      return retValue;
    }

    // Gets a DateTime value from an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/GetDbDateTime/*'/>
    public static DateTime GetDbDateTime(object? value)
    {
      DateTime retValue = DateTime.MinValue;

      if (value != null)
      {
        var type = value.GetType();
        if (typeof(string) == type)
        {
          string result = value.ToString();
          if (HasText(result))
          {
            retValue = DateTime.Parse(result);
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Project Config Values Methods

    // Retrieves the Config bool value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ConfigBool/*'/>
    public static bool ConfigBool(string key)
    {
      bool retValue = false;

      if (HasText(key))
      {
        string? configValue = ConfigurationManager.AppSettings[key];
        if (HasText(configValue))
        {
          _ = bool.TryParse(configValue, out retValue);
        }
      }
      return retValue;
    }

    // Retrieves the Config Color value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/ConfigColor/*'/>
    public static bool ConfigColor(string key, out Color color)
    {
      bool retValue = false;

      color = Color.Black;

      if (HasText(key))
      {
        string? configValue = ConfigurationManager.AppSettings[key];
        if (HasText(configValue))
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/ConfigString/*'/>
    public static string? ConfigString(string key)
    {
      string? retValue = null;

      if (HasText(key))
      {
        retValue = ConfigurationManager.AppSettings[key];
      }
      return retValue;
    }

    // Accept or Select the DataConfig.
    /// <include file='Doc/LJC.xml'
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

    #region Serialization Methods

    // Deserialize an XML message file to an object.
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlDeserialize/*'/>
    public static object? XmlDeserialize(Type type, string? fileSpec
      , string? rootName = null)
    {
      string errorText;
      object? retValue = null;

      if (!File.Exists(fileSpec))
      {
        errorText = $"File '{fileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        FileStream? fileStream = null;
        try
        {
          XmlSerializer serializer;
          if (HasText(rootName))
          {
            var root = new XmlRootAttribute(rootName);
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlDeserializeMessage/*'/>
    public static object? XmlDeserializeMessage(Type type, string message)
    {
      Stream? stream = null;
      object? retValue = null;

      try
      {
        stream = StringToMemStream(message);
        var serializer = new XmlSerializer(type);
        retValue = serializer.Deserialize(stream);
      }
      finally
      {
        stream?.Close();
      }
      return retValue;
    }

    // Serialize an object to an XML message file.
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlSerialize/*'/>
    public static void XmlSerialize(Type type, object data
      , XmlSerializerNamespaces? namespaces, string? fileSpec
      , string? rootName = null)
    {
      FileStream fileStream;
      string errorText;

      if (!HasText(fileSpec))
      {
        errorText = "Missing file specification.";
        throw new ArgumentException(errorText);
      }

      string? folder = Path.GetDirectoryName(fileSpec);
      if (HasText(folder)
        && !Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }

      // Serialize to XML.
      XmlSerializer serializer;
      if (HasText(rootName))
      {
        var root = new XmlRootAttribute(rootName);
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
    }

    // Serialize an object to an XML message string.
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlSerializeToString/*'/>
    public static string XmlSerializeToString(Type type, object data
      , XmlSerializerNamespaces? namespaces)
    {
      MemoryStream memStream;
      string retValue;

      // Serialize to XML.
      var xmlSerializer = new XmlSerializer(type);
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

    #region Text Conversion Methods

    // ** Base64 Bytes and Text
    // Decodes a Base64 byte array to a Text value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/Base64BytesToText/*'/>
    public static string? Base64BytesToText(byte[] bytes)
    {
      byte[] byteArray = Base64BytesToTextBytes(bytes);
      return BytesToText(byteArray);
    }

    // Encodes a Text value to a Base64 byte array.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TextToBase64Bytes/*'/>
    public static byte[] TextToBase64Bytes(string text)
    {
      string base64 = TextToBase64(text);
      byte[] retValue = TextToBytes(base64);
      return retValue;
    }

    // ** Base64 Bytes and Text Bytes
    // Decodes a Base64 byte array to a Text byte array.
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/Base64ToText/*'/>
    public static string Base64ToText(string base64Text)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
    }

    // Encodes a Text value to a Base64 value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TextToBase64/*'/>
    public static string TextToBase64(string text)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }

    // * Text Bytes and Base64
    // Decodes a Base64 value to a Text byte array.
    /// <include file='Doc/LJC.xml'
    ///  path='members/Base64ToTextBytes/*'/>
    public static byte[] Base64ToTextBytes(string base64)
    {
      string text = Base64ToText(base64);
      byte[] retValue = TextToBytes(text);
      return retValue;
    }

    // Encodes a Text byte array to a Base64 value.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TextBytesToBase64/*'/>
    public static string? TextBytesToBase64(byte[] bytes)
    {
      byte[] base64Bytes = TextBytesToBase64Bytes(bytes);
      string retValue = BytesToText(base64Bytes);
      return retValue;
    }

    // ** Bytes and Text
    // Creates text from a byte array.
    /// <include file='Doc/LJC.xml'
    ///  path='members/BytesToText/*'/>
    public static string? BytesToText(byte[]? bytes)
    {
      string retValue = null;
      if (bytes != null)
      {
        retValue = Encoding.UTF8.GetString(bytes);
      }
      return retValue;
    }

    // Creates a byte array from text.
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
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
    /// <include file='Doc/LJC.xml'
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

    // ** XML Entities
    // Decodes an encoded XML string.
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlDecode/*'/>
    public static string? XmlDecode(string text)
    {
      string? retValue = null;

      if (HasText(text))
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
    /// <include file='Doc/LJC.xml'
    ///  path='members/XmlEncode/*'/>
    public static string? XmlEncode(string text)
    {
      string? retValue = null;

      if (HasText(text))
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

    #region DataType Names

    // The Boolean type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeBoolean/*'/>
    public const string TypeBoolean = "Boolean";

    // The Byte type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeByte/*'/>
    public const string TypeByte = "Byte";

    // The Char type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeChar/*'/>
    public const string TypeChar = "Char";

    // The DateTime type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeDateTime/*'/>
    public const string TypeDateTime = "DateTime";

    // Type Decimal type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeDecimal/*'/>
    public const string TypeDecimal = "Decimal";

    // The Double type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeDouble/*'/>
    public const string TypeDouble = "Double";

    // The Int16 type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeInt16/*'/>
    public const string TypeInt16 = "Int16";

    // The Int32 type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeInt32/*'/>
    public const string TypeInt32 = "Int32";

    // The Int64 type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeInt64/*'/>
    public const string TypeInt64 = "Int64";

    // The Single type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeSingle/*'/>
    public const string TypeSingle = "Single";

    // The String type name.
    /// <include file='Doc/LJC.xml'
    ///  path='members/TypeString/*'/>
    public const string TypeString = "String";
    #endregion

    // The ignore case constant.
    /// <include file='Doc/LJC.xml'
    ///  path='members/IgnoreCase/*'/>
    public const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;
  }
}
