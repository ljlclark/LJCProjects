// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextDataReader.cs
using LJCNetCommon;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace LJCTextDataReaderLib
{
  // A text file data reader.
  /// <include path='items/TextDataReader/*' file='Doc/TextDataReader.xml'/>
  public class TextDataReader : IDataReader
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/TextDataReaderC/*'/>
    public TextDataReader(bool hasHeadingLine = true, short skipHeaderLines = 0
      , bool fixedLengthFields = false)
    {
      // #Pagination Begin - Add
      LJCPageStartIndex = -1;
      LJCPageSize = 0;
      LJCCurrentRecordCount = -1;
      // #Pagination End - Add

      LJCTextRegions = new TextRegions();
      LJCFieldDelimiter = ',';
      LJCFixedLengthFields = fixedLengthFields;
      LJCHasHeadingLine = hasHeadingLine;
      LJCSkipHeaderLines = skipHeaderLines;
      LJCDataFields = new LJCDataColumns();

      IsClosed = true;
    }
    #endregion

    #region Public IDataReader Methods

    // Closes the data reader.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Close/*'/>
    public void Close()
    {
      if (LJCStreamReader != null)
      {
        LJCStreamReader.Close();
        LJCStreamReader = null;
      }
      IsClosed = true;
    }

    // Disposes the object.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Dispose/*'/>
    public void Dispose()
    {
      Close();
    }

    // Not supported. Currently returns null.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetData/*'/>
    public IDataReader GetData(int i)
    {
      return null;
    }

    // Retrieves the data type name for the specified field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDataTypeName1/*'/>
    public string GetDataTypeName(int i)
    {
      LJCDataColumn dbColumn;
      string retValue = null;

      if (i > -1 && i < FieldCount)
      {
        retValue = "String";
        dbColumn = LJCDataFields[i];
        if (dbColumn.DataTypeName != null)
        {
          //retValue = Enum.GetName(typeof(FieldDataType), dbColumn.DataTypeName);
          retValue = dbColumn.DataTypeName;
        }
      }
      return retValue;
    }

    // Retrieves the data type name for the specified field name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDataTypeName2/*'/>
    public string GetDataTypeName(string name)
    {
      int index = GetOrdinal(name);
      return GetDataTypeName(index);
    }

    // Retrieves the type of the field for the specified field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFieldType/*'/>
    public Type GetFieldType(int i)
    {
      string dataTypeName;
      Type retValue;

      dataTypeName = GetDataTypeName(i);
      switch (dataTypeName)
      {
        case "Boolean":
          retValue = typeof(Boolean);
          break;
        case "Byte":
          retValue = typeof(Byte);
          break;
        case "DateTime":
          retValue = typeof(DateTime);
          break;
        case "Decimal":
          retValue = typeof(Decimal);
          break;
        case "Double":
          retValue = typeof(Double);
          break;
        case "Int16":
          retValue = typeof(Int16);
          break;
        case "Int32":
          retValue = typeof(Int32);
          break;
        case "Int64":
          retValue = typeof(Int64);
          break;
        case "Single":
          retValue = typeof(Single);
          break;
        case "String":
          retValue = typeof(String);
          break;
        default:
          retValue = typeof(String);
          break;
      }
      return retValue;
    }

    // Retrieves the name of the data field with the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetName/*'/>
    public string GetName(int i)
    {
      string retValue = null;

      if (i > -1 && i < FieldCount)
      {
        retValue = LJCDataFields[i].ColumnName;
      }
      return retValue;
    }

    // Retrieves the index of the data field with the specified name.
    // Required method if field mapping is used.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetOrdinal/*'/>
    public int GetOrdinal(string name)
    {
      int retValue = -1;

      for (int index = 0; index < FieldCount; index++)
      {
        if (LJCDataFields[index].ColumnName.Equals(name))
        {
          retValue = index;
          break;
        }
      }
      return retValue;
    }

    // Gets the Schema DataTable object.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetSchemaTable/*'/>
    public DataTable GetSchemaTable()
    {
      const int AllowDBNull = 13;
      const int IsAutoIncrement = 18;
      const int ColumnName = 0;
      const int DataType = 12;
      const int DataTypeName = 24;
      const int ColumnSize = 2;
      const int IsUnique = 5;

      DataTable retValue = CreateSchemaTable();
      foreach (LJCDataColumn dbColumn in LJCDataFields)
      {
        DataRow dataRow = retValue.NewRow();
        dataRow[AllowDBNull] = dbColumn.AllowDBNull;
        dataRow[IsAutoIncrement] = dbColumn.AutoIncrement;
        dataRow[ColumnName] = dbColumn.ColumnName;
        dataRow[DataType] = GetDataType(dbColumn.DataTypeName);
        dataRow[DataTypeName] = dbColumn.DataTypeName;
        dataRow[ColumnSize] = dbColumn.MaxLength;
        dataRow[IsUnique] = dbColumn.IsUniqueKey;
        retValue.Rows.Add(dataRow);
      }
      return retValue;
    }

    // Gets an array of record values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetValues/*'/>
    public int GetValues(object[] values)
    {
      for (int index = 0; index < values.Length; index++)
      {
        if (index < FieldCount)
        {
          values[index] = this[index];
        }
      }
      return values.Length;
    }

    // Indicates if the field value is null or whitespace.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/IsDBNull/*'/>
    public bool IsDBNull(int i)
    {
      bool retValue;

      string text = GetString(i);
      retValue = (!NetString.HasValue(text));
      return retValue;
    }

    // Sets to the next result. Currently returns false.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/NextResult/*'/>
    public bool NextResult()
    {
      return false;
    }

    // Reads the next line of the text file.
    // This is a minimum required method.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Read/*'/>
    public bool Read()
    {
      string line;
      bool retValue = true;

      while (true)
      {
        // #Pagination Begin - Add
        if (LJCCurrentRecordCount > -1
          && LJCCurrentRecordCount >= LJCPageSize)
        {
          // End of page.
          retValue = false;
          LJCCurrentRecordCount = -1;
          break;
        }
        // #Pagination End - Add

        line = LJCStreamReader.ReadLine();
        if (null == line)
        {
          retValue = false;
          break;
        }

        // Ends with text file EOF marker.
        if (1 == line.Length
          && '\x001a' == line[0])
        {
          retValue = false;
          break;
        }

        if (!retValue)
        {
          Close();
          break;
        }

        // Give names if none are defined.
        if (0 == FieldCount)
        {
          SetDefaultNames(line);
        }

        // #Pagination Begin - Add
        if (LJCCurrentRecordCount > -1)
        {
          LJCCurrentRecordCount++;
          LJCLineOffsets.LJCSetNextLineOffset(line);
        }
        // #Pagination End - Add

        if (LJCFixedLengthFields)
        {
          LJCSetValues(line);
        }
        else
        {
          LJCSetLine(line);
        }
        break;
      }
      return retValue;
    }
    #endregion

    #region Public DataReader Custom Related Methods

    // Reads the next line from the line string array.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCReadLine/*'/>
    public bool LJCReadLine()
    {
      string line;
      bool retValue = true;

      if (null == LJCLines || LJCCurrentLineIndex < 0
        || LJCCurrentLineIndex >= LJCLines.Length)
      {
        retValue = false;
      }
      else
      {
        line = LJCLines[LJCCurrentLineIndex];

        // Give names if none are defined.
        if (0 == FieldCount)
        {
          SetDefaultNames(line);
        }

        //LJCSetLine(line);
        if (LJCFixedLengthFields)
        {
          LJCSetValues(line);
        }
        else
        {
          LJCSetLine(line);
        }
        LJCCurrentLineIndex++;
      }
      return retValue;
    }
    #endregion

    #region Public IDataRecord Get Data Methods

    // Returns the bool value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBoolean/*'/>
    public bool GetBoolean(int i)
    {
      bool.TryParse(GetString(i), out bool retValue);
      return retValue;
    }

    // Returns the byte value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetByte/*'/>
    public byte GetByte(int i)
    {
      byte.TryParse(GetString(i), out byte retValue);
      return retValue;
    }

    // Place field bytes into a byte array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBytes/*'/>
    public long GetBytes(int i, long fieldOffset, byte[] buffer
      , int bufferoffset, int length)
    {
      long retValue;

      string text = GetString(i);
      retValue = GetBytesFromText(text, fieldOffset, buffer, bufferoffset);
      return retValue;
    }

    // Returns the char value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetChar/*'/>
    public char GetChar(int i)
    {
      char.TryParse(GetString(i), out char retValue);
      return retValue;
    }

    // Place field characters into a character array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetChars/*'/>
    public long GetChars(int i, long fieldOffset, char[] buffer
      , int bufferoffset, int length)
    {
      long retValue;

      string text = GetString(i);
      retValue = GetCharsFromText(text, fieldOffset, buffer, bufferoffset);
      return retValue;
    }

    // Returns the DateTime value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDateTime/*'/>
    public DateTime GetDateTime(int i)
    {
      DateTime.TryParse(GetString(i), out DateTime retValue);
      return retValue;
    }

    // Returns the decimal value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDecimal/*'/>
    public decimal GetDecimal(int i)
    {
      decimal.TryParse(GetString(i), out decimal retValue);
      return retValue;
    }

    // Returns the double value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDouble/*'/>
    public double GetDouble(int i)
    {
      double.TryParse(GetString(i), out double retValue);
      return retValue;
    }

    // Returns the float value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFloat/*'/>
    public float GetFloat(int i)
    {
      float.TryParse(GetString(i), out float retValue);
      return retValue;
    }

    // Returns the Guid value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetGuid/*'/>
    public Guid GetGuid(int i)
    {
      Guid.TryParse(GetString(i), out Guid retValue);
      return retValue;
    }

    // Returns the short int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt16/*'/>
    public short GetInt16(int i)
    {
      short.TryParse(GetString(i), out short retValue);
      return retValue;
    }

    // Returns the int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt32/*'/>
    public int GetInt32(int i)
    {
      int.TryParse(GetString(i), out int retValue);
      return retValue;
    }

    // Returns the long int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt64/*'/>
    public long GetInt64(int i)
    {
      long.TryParse(GetString(i), out long retValue);
      return retValue;
    }

    // Returns the string value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetString/*'/>
    public string GetString(int i)
    {
      string retValue = null;

      object value = this[i];
      if (value != null
        && NetString.HasValue(value.ToString()))
      {
        retValue = value.ToString();
      }
      return retValue;
    }

    /// <summary>Gets the trimmed string value.</summary>
    public string GetTrimValue(int i)
    {
      string retValue = GetString(i);
      retValue?.Trim();
      return retValue;
    }

    // Returns the object value of the data field at the specified index.
    // This is a minimum required method.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetValue/*'/>
    public object GetValue(int i)
    {
      return this[i];
    }
    #endregion

    #region Public Get Data Custom Overload Methods

    // Returns the bool value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBooleanN/*'/>
    public bool GetBoolean(string name)
    {
      bool.TryParse(GetString(name), out bool retValue);
      return retValue;
    }

    // Returns the byte value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetByteN/*'/>
    public byte GetByte(string name)
    {
      byte.TryParse(GetString(name), out byte retValue);
      return retValue;
    }

    // Place field bytes into a byte array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBytesN/*'/>
    public long GetBytes(string name, long fieldOffset, byte[] buffer
      , int bufferoffset)
    {
      long retValue;

      string text = GetString(name);
      retValue = GetBytesFromText(text, fieldOffset, buffer, bufferoffset);
      return retValue;
    }

    // Returns the char value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetCharN/*'/>
    public char GetChar(string name)
    {
      char.TryParse(GetString(name), out char retValue);
      return retValue;
    }

    // Place field characters into a character array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetCharsN/*'/>
    public long GetChars(string name, long fieldOffset, char[] buffer
      , int bufferoffset)
    {
      long retValue;

      string text = GetString(name);
      retValue = GetCharsFromText(text, fieldOffset, buffer, bufferoffset);
      return retValue;
    }

    // Returns the DateTime value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDateTimeN/*'/>
    public DateTime GetDateTime(string name)
    {
      DateTime.TryParse(GetString(name), out DateTime retValue);
      return retValue;
    }

    // Returns the decimal value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDecimalN/*'/>
    public decimal GetDecimal(string name)
    {
      decimal.TryParse(GetString(name), out decimal retValue);
      return retValue;
    }

    // Returns the double value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDoubleN/*'/>
    public double GetDouble(string name)
    {
      double.TryParse(GetString(name), out double retValue);
      return retValue;
    }

    // Returns the float value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFloatN/*'/>
    public float GetFloat(string name)
    {
      float.TryParse(GetString(name), out float retValue);
      return retValue;
    }

    // Returns the Guid value of the data field with the specified name. 
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetGuidN/*'/>
    public Guid GetGuid(string name)
    {
      Guid.TryParse(GetString(name), out Guid retValue);
      return retValue;
    }

    // Returns the short int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt16N/*'/>
    public short GetInt16(string name)
    {
      short.TryParse(GetString(name), out short retValue);
      return retValue;
    }

    // Returns the int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt32N/*'/>
    public int GetInt32(string name)
    {
      int.TryParse(GetString(name), out int retValue);
      return retValue;
    }

    // Returns the long int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt64N/*'/>
    public long GetInt64(string name)
    {
      long.TryParse(GetString(name), out long retValue);
      return retValue;
    }

    // Returns the string value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetStringN/*'/>
    public string GetString(string name)
    {
      string retValue = null;

      object value = this[name];
      if (value != null
        && NetString.HasValue(value.ToString()))
      {
        retValue = value.ToString();
      }
      return retValue;
    }

    /// <summary>Gets the trimmed string value.</summary>
    public string GetTrimValue(string name)
    {
      var retValue = GetString(name);
      retValue = retValue?.Trim();
      return retValue;
    }
    #endregion

    #region Public IDataReader Properties

    /// <summary>Currently returns 0.</summary>
    public int Depth
    {
      get { return 0; }
    }

    /// <summary>Indicates if the reader is closed.</summary>
    public bool IsClosed { get; set; }

    /// <summary>Currently returns -1.</summary>
    public int RecordsAffected
    {
      get { return -1; }
    }
    #endregion

    #region Public IDataRecord Properties

    // Gets the field count.
    // This is a minimum required property.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/FieldCount/*'/>
    public int FieldCount
    {
      get
      {
        int retValue = 0;

        if (LJCDataFields != null)
        {
          retValue = LJCDataFields.Count;
        }
        return retValue;
      }
    }

    // Gets the data field value for the specified field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/this1/*'/>
    public object this[int i]
    {
      get
      {
        object retValue = null;

        if (i > -1 && i < FieldCount)
        {
          retValue = LJCDataFields[i].Value;
        }
        return retValue;
      }
    }

    // Gets the data field value for the specified field name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/this2/*'/>
    public object this[string name]
    {
      get
      {
        int index = GetOrdinal(name);
        return this[index];
      }
    }
    #endregion

    #region Public Custom Methods

    /// <summary>Checks for NewLine at end of file.</summary>
    public bool LJCEndsWithNewLine()
    {
      var retValue = false;

      var doClose = false;
      if (IsClosed)
      {
        LJCOpen();
        doClose = true;
      }

      long prevPosition = LJCStream.Position;
      LJCStream.Seek(-1, SeekOrigin.End);
      int value = LJCStream.ReadByte();
      if (10 == value)
      {
        retValue = true;
      }
      LJCStream.Position = prevPosition;

      if (doClose)
      {
        Close();
      }
      return retValue;
    }

    // Returns the field names.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCGetFieldNames/*'/>
    public string[] LJCGetFieldNames()
    {
      string[] retValue = new string[FieldCount];
      for (int index = 0; index < FieldCount; index++)
      {
        retValue[index] = LJCDataFields[index].ColumnName;
      }
      return retValue;
    }

    /// <summary>Opens with the existing file settings.</summary>
    public void LJCOpen()
    {
      Close();
      LJCStreamReader = new StreamReader(LJCFileName, Encoding.UTF8
        , true, 4096);
      IsClosed = false;

      // #Pagination Next Statement - Add
      LJCLineOffsets = new LineOffsets(LJCStream);


      if (LJCHasHeadingLine)
      {
        var line = LJCStreamReader.ReadLine();

        // #Pagination Next Statement - Add
        LJCLineOffsets.LJCSetNextLineOffset(line);

        // Get Headings and set FieldCount;
        LJCSetNames(line);
      }

      for (int count = 0; count < LJCSkipHeaderLines; count++)
      {
        var line = LJCStreamReader.ReadLine();

        // #Pagination Next Statement - Add
        LJCLineOffsets.LJCSetNextLineOffset(line);
      }
    }

    // Sets the data field definitions from an XML file.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetFields/*'/>
    public void LJCSetFields(string layoutFileName)
    {
      LJCLayoutFileName = layoutFileName;
      LJCDataFields = NetCommon.XmlDeserialize(typeof(LJCDataColumns)
        , LJCLayoutFileName) as LJCDataColumns;
    }

    // Sets the source text file values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetFile/*'/>
    public void LJCSetFile(string fileName, char fieldDelimiter = ',')
    {
      LJCFileName = fileName;
      LJCFieldDelimiter = fieldDelimiter;
      if (null == LJCDataFields)
      {
        LJCDataFields = new LJCDataColumns();
      }

      if (!File.Exists(fileName))
      {
        string errorText = $"File: '{fileName}' was not found.";
        throw new FileNotFoundException(errorText);
      }

      LJCOpen();
    }

    // Sets the line field values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetLine/*'/>
    public void LJCSetLine(string line)
    {
      if (LJCTextRegions.LJCHasRegions(line))
      {
        LJCSetValues(LJCTextRegions.LJCSplit(line));
      }
      else
      {
        LJCSetValues(line.Split(LJCFieldDelimiter));
      }
    }

    // Set the source line string array.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetLines/*'/>
    public void LJCSetLines(string[] lines, char fieldDelimiter = ',')
    {
      LJCLines = lines;
      LJCFieldDelimiter = fieldDelimiter;
      LJCDataFields = new LJCDataColumns();

      LJCCurrentLineIndex = 0;
      if (LJCHasHeadingLine)
      {
        string line = lines[0];

        // Get Headings and set FieldCount;
        LJCSetNames(line);
        LJCCurrentLineIndex++;
      }

      for (int count = 0; count < LJCSkipHeaderLines; count++)
      {
        LJCCurrentLineIndex++;
      }
    }

    // Sets the data field names.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetNames/*'/>
    public bool LJCSetNames(string line)
    {
      string fieldName = null;
      int maxLength = 0;
      string dataTypeName = null;
      bool retValue = false;

      if (0 == FieldCount)
      {
        int position = 1;
        string[] fields = line.Split(LJCFieldDelimiter);
        foreach (string field in fields)
        {
          // Check for included width and type value.
          string[] values = field.Split('|');
          switch (values.Length)
          {
            case 1:
              // No width or type value.
              fieldName = values[0];
              dataTypeName = "String";
              break;

            case 2:
              // Has width value.
              fieldName = values[0];
              int.TryParse(values[1], out maxLength);
              position += maxLength;
              dataTypeName = "String";
              break;

            case 3:
              // Has width and type value.
              fieldName = values[0];
              int.TryParse(values[1], out maxLength);
              position += maxLength;
              dataTypeName = values[2];
              break;
          }

          // Add field data definition.
          LJCDataColumn dbColumn = new LJCDataColumn()
          {
            ColumnName = fieldName,
            DataTypeName = dataTypeName,
            MaxLength = maxLength,
            Position = position
          };
          LJCDataFields.Add(dbColumn);
        }
        retValue = true;
      }
      return retValue;
    }

    // Sets the data object property values from the TextDataReader.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetObjectValues/*'/>
    public void LJCSetObjectValues(object dataObject, LJCDataColumns dataFields = null)
    {
      LJCReflect reflect;
      LJCDataColumns fieldColumns;

      fieldColumns = LJCDataFields;
      if (dataFields != null)
      {
        fieldColumns = dataFields;
      }

      reflect = new LJCReflect(dataObject);
      foreach (LJCDataColumn dbColumn in fieldColumns)
      {
        string sourceColumnName = dbColumn.ColumnName;
        if (NetString.HasValue(dbColumn.RenameAs))
        {
          sourceColumnName = dbColumn.RenameAs;
        }
        reflect.SetPropertyValue(dbColumn.PropertyName
          , this[sourceColumnName]);
      }
    }

    // #Pagination - New Method
    // Sets the starting record index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetStartIndex/*'/>
    public bool LJCSetStartIndex(int recordIndex)
    {
      bool retValue = false;

      LJCCurrentRecordCount = -1;
      if (recordIndex > -1)
      {
        if (IsClosed)
        {
          LJCStreamReader = new StreamReader(LJCFileName, Encoding.UTF8, true, 4096);
          LJCLineOffsets.LJCStream = LJCStream;
        }
        long offset = LJCLineOffsets.LJCGetLineOffset(recordIndex + 1);
        if (offset > -1)
        {
          LJCPageStartIndex = recordIndex;
          LJCStream.Position = offset;
          LJCStreamReader.DiscardBufferedData();
          //LJCStream.Seek(offset, SeekOrigin.Begin);
          LJCCurrentRecordCount = 0;
          retValue = true;
        }
      }
      return retValue;
    }

    // Sets the current line values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetValues1/*'/>
    public void LJCSetValues(string[] values)
    {
      bool isError;

      // Allow values to be less than FieldCount.
      // *** Next Statement *** Change
      //if (values.Length != FieldCount)
      if (values.Length > FieldCount)
      {
        isError = true;

        // Skip empty line.
        if (1 == values.Length
          && !NetString.HasValue(values[0]))
        {
          isError = false;
        }

        if (isError)
        {
          string errorText = $"Values Length:{values.Length} does not match"
            + $" FieldCount:{FieldCount}.";
          throw new Exception(errorText);
        }
      }
      else
      {
        // Allow values to be less than FieldCount.
        // *** Next Statement *** Change
        //for (int index = 0; index < FieldCount; index++)
        for (int index = 0; index < values.Length; index++)
        {
          LJCDataColumn dataField = LJCDataFields[index];
          if (dataField != null)
          {
            dataField.Value = values[index];
          }
        }
      }
    }

    // Sets the current, fixed length, line values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetValues2/*'/>
    public void LJCSetValues(string line)
    {
      // Allow values to be less than FieldCount.
      // *** Next Statement *** Change
      //for (int index = 0; index < FieldCount; index++)
      for (int index = 0; index < LJCDataFields.Count; index++)
      {
        LJCDataColumn dataField = LJCDataFields[index];
        if (dataField != null)
        {
          dataField.Value = line.Substring(dataField.Position - 1
            , dataField.MaxLength).Trim();
        }
      }
    }
    #endregion

    #region Private Methods

    // Adds a DataColumn to the DataTable object.
    private static void AddColumn(DataTable dataTable, string columnName
      , Type dataType = null, bool readOnly = false, bool allowNull = true
      , bool autoIncrement = false)
    {
      object defaultValue = null;
      DataColumn dataColumn;

      if (null == dataType)
      {
        dataType = typeof(String);
      }

      switch (dataType.Name)
      {
        case "Decimal":
        case "Double":
        case "Int16":
        case "Int32":
        case "Int64":
        case "Single":
          defaultValue = 0;
          break;
      }

      dataColumn = new DataColumn()
      {
        AllowDBNull = allowNull,
        AutoIncrement = autoIncrement,
        ColumnName = columnName,
        DataType = dataType,
        DefaultValue = defaultValue,
        ReadOnly = readOnly
      };
      dataTable.Columns.Add(dataColumn);
    }

    // Creates the Schema DataTable object.
    private DataTable CreateSchemaTable()
    {
      DataTable retValue = new DataTable();

      AddColumn(retValue, "ColumnName");
      AddColumn(retValue, "ColumnOrdinal", typeof(Int32), true);
      AddColumn(retValue, "ColumnSize", typeof(Int32), true);
      AddColumn(retValue, "NumericPrecision", typeof(Int16), true);
      AddColumn(retValue, "NumericScale", typeof(Int16), true);
      AddColumn(retValue, "IsUnique", typeof(Boolean), true);
      AddColumn(retValue, "IsKey", typeof(Boolean), true);
      AddColumn(retValue, "BaseServerName", readOnly: true);
      AddColumn(retValue, "BaseCatalogName", readOnly: true);
      AddColumn(retValue, "BaseColumnName", readOnly: true);
      AddColumn(retValue, "BaseSchemaName", readOnly: true);
      AddColumn(retValue, "BaseTableName", readOnly: true);
      AddColumn(retValue, "DataType", typeof(Type), readOnly: true);
      AddColumn(retValue, "AllowDBNull", typeof(Boolean), readOnly: true);
      AddColumn(retValue, "ProviderType", typeof(Int32), true);
      AddColumn(retValue, "IsAliased", typeof(Boolean), true);
      AddColumn(retValue, "IsExpression", typeof(Boolean), true);
      AddColumn(retValue, "IsIdentity", typeof(Boolean), true);
      AddColumn(retValue, "IsAutoIncrement", typeof(Boolean), true);
      AddColumn(retValue, "IsRowVersion", typeof(Boolean), true);
      AddColumn(retValue, "IsHidden", typeof(Boolean), true);
      AddColumn(retValue, "IsLong", typeof(Boolean), true);
      AddColumn(retValue, "IsReadOnly", typeof(Boolean), true);
      AddColumn(retValue, "ProviderSpecifiedDataType", typeof(Type), readOnly: true);
      AddColumn(retValue, "DataTypeName", readOnly: true);
      AddColumn(retValue, "XmlSchemaDatabase", readOnly: true);
      AddColumn(retValue, "XmlSchemaOwningSchema", readOnly: true);
      AddColumn(retValue, "XmlSchemaName", readOnly: true);
      AddColumn(retValue, "UdtAssemblyQualifiedName", readOnly: true);
      AddColumn(retValue, "NonVersionedProviderType", typeof(Int32), true);
      AddColumn(retValue, "IsColumnSet", typeof(Boolean), true);
      return retValue;
    }

    // Place field bytes into a byte array buffer.
    //  , int bufferoffset, int length)
    private long GetBytesFromText(string text, long fieldOffset, byte[] buffer
      , int bufferoffset)
    {
      long retValue = 0;

      if (text != null)
      {
        int fieldIndex = (int)fieldOffset;
        for (int index = bufferoffset; index < buffer.Length; index++)
        {
          if (fieldIndex > text.Length)
          {
            break;
          }
          else
          {
            byte.TryParse(text[fieldIndex].ToString(), out byte byteValue);
            buffer[index] = byteValue;
            retValue++;
            fieldIndex++;
          }
        }
      }
      return retValue;
    }

    // Place field characters into a character array buffer.
    //  , int bufferoffset, int length)
    private long GetCharsFromText(string text, long fieldOffset, char[] buffer
      , int bufferoffset)
    {
      long retValue = 0;

      if (text != null)
      {
        int fieldIndex = (int)fieldOffset;
        for (int index = bufferoffset; index < buffer.Length; index++)
        {
          if (fieldIndex > text.Length)
          {
            break;
          }
          else
          {
            buffer[index] = text[fieldIndex];
            retValue++;
            fieldIndex++;
          }
        }
      }
      return retValue;
    }

    // Get a Type value for the specified DataType name.
    private Type GetDataType(string dataTypeName)
    {
      Type retValue;

      switch (dataTypeName.ToLower())
      {
        case "boolean":
          retValue = typeof(Boolean);
          break;
        case "byte":
          retValue = typeof(Byte);
          break;
        case "datetime":
          retValue = typeof(DateTime);
          break;
        case "decimal":
          retValue = typeof(Decimal);
          break;
        case "double":
          retValue = typeof(Double);
          break;
        case "int16":
          retValue = typeof(Int16);
          break;
        case "int32":
          retValue = typeof(Int32);
          break;
        case "int64":
          retValue = typeof(Int64);
          break;
        case "single":
          retValue = typeof(Single);
          break;
        default:
          retValue = typeof(String);
          break;
      }
      return retValue;
    }

    // Give names if none are defined.
    // The names include the field index.
    private void SetDefaultNames(string line)
    {
      if (0 == FieldCount)
      {
        int fieldNumber = -1;
        string[] fields = line.Split(LJCFieldDelimiter);
        foreach (string field in fields)
        {
          fieldNumber++;
          LJCDataFields.Add($"Field{fieldNumber}");
        }
      }
    }
    #endregion

    #region Public Custom Properties

    /// <summary>Gets or sets the Current Line index.</summary>
    public int LJCCurrentLineIndex { get; set; }

    /// <summary>Gets or sets the Current RecordCount value.</summary>
    public int LJCCurrentRecordCount { get; set; }

    /// <summary>Gets or sets the current DataField values.</summary>
    public LJCDataColumns LJCDataFields { get; private set; }

    /// <summary>Gets or sets the field delimiter.</summary>
    public char LJCFieldDelimiter
    {
      get { return mFieldDelimiter; }
      set
      {
        if (value != '\0')
        {
          mFieldDelimiter = value;
          LJCTextRegions.LJCFieldDelimiter = value;
        }
      }
    }
    private char mFieldDelimiter;

    /// <summary>Gets or sets the FileName value.</summary>
    public string LJCFileName { get; set; }

    /// <summary>Gets or sets the FixedLength fields indicator.</summary>
    /// <remarks>Indicates if the fields are fixed length.</remarks>
    public bool LJCFixedLengthFields { get; set; }

    /// <summary>Gets or sets the HasHeadingLine value.</summary>
    /// <remarks>Indicates if there is a header line.</remarks>
    public bool LJCHasHeadingLine { get; set; }

    /// <summary>Gets the LineOffsets collection.</summary>
    public LineOffsets LJCLineOffsets { get; private set; }

    /// <summary>Gets or sets the Lines value.</summary>
    public string[] LJCLines { get; set; }

    // #Pagination - New Property
    /// <summary>Gets or sets the PageSize value.</summary>
    public int LJCPageSize { get; set; }

    // #Pagination - New Property
    /// <summary>Gets or sets the PageStart index.</summary>
    public long LJCPageStartIndex { get; set; }

    /// <summary>Gets or sets the number of lines to skip.</summary>
    /// <remarks>Indicates the number of header lines to skip.</remarks>
    public short LJCSkipHeaderLines { get; set; }

    // #Pagination - New Property
    /// <summary>Gets the Stream value.</summary>
    public Stream LJCStream
    {
      get { return LJCStreamReader.BaseStream; }
    }

    /// <summary>Gets or sets the StreamReader value.</summary>
    public StreamReader LJCStreamReader { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the LayoutFile Name value.
    private string LJCLayoutFileName { get; set; }

    // Gets or sets the current line TextRegion values.
    private TextRegions LJCTextRegions { get; set; }
    #endregion
  }

  /// <summary>The field data types.</summary>
  public enum LJCFieldDataType : short
  {
    /// <summary>The Boolean value.</summary>
    Boolean = 1,
    /// <summary>The Byte value.</summary>
    Byte,
    /// <summary>The DateTime value.</summary>
    DateTime,
    /// <summary>The Decimal value.</summary>
    Decimal,
    /// <summary>The Double value.</summary>
    Double,
    /// <summary>The Int16 value.</summary>
    Int16,
    /// <summary>The Int32 value.</summary>
    Int32,
    /// <summary>The Int64 value.</summary>
    Int64,
    /// <summary>The Single value.</summary>
    Single,
    /// <summary>The String value.</summary>
    String
  }
}
