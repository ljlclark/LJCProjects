// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextDataReader5.cs
using LJCNetCommon5;
using System.Data;
using System.Text;

namespace LJCTextDataReader5
{
  // A text file data reader.
  /// <include file='Doc/ProjectTextDataReader.xml'
  ///  path='items/LJCTextDataReader/*'/>
  public class LJCTextDataReader : IDataReader
  {
    #region IDataReader Properties

    // Currently returns 0.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Depth/*'/>
    public int Depth
    {
      get => 0;
    }

    // Indicates if the reader is closed.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/IsClosed/*'/>
    public bool IsClosed { get; set; }

    // Currently returns -1.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/RecordsAffected/*'/>
    public int RecordsAffected
    {
      get => -1;
    }
    #endregion

    #region IDataRecord Properties

    // Gets the field count. Minimum Required
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

    // Gets the data field value for the supplied field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/this1/*'/>
    public object this[int i]
    {
      get
      {
        object retValue = "";

        if (i > -1 && i < FieldCount)
        {
          var result = LJCDataFields[i].Value;
          if (result != null)
          {
            retValue = result;
          }
        }
        return retValue;
      }
    }

    // Gets the data field value for the supplied field name.
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

    #region Custom Properties

    // Gets or sets the Current Line index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCCurrentLineIndex/*'/>
    public int LJCCurrentLineIndex { get; set; }

    // Gets or sets the Current RecordCount value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCCurrentRecordCount/*'/>
    public int LJCCurrentRecordCount { get; set; }

    // Gets or sets the current DataField values.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCDataFields/*'/>
    public LJCDataColumns LJCDataFields { get; private set; }

    // Gets or sets the field delimiter.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCFieldDelimiter/*'/>
    public char LJCFieldDelimiter
    {
      get => mFieldDelimiter;
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

    // Gets or sets the FileName value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCFileName/*'/>
    public string? LJCFileName { get; set; }

    // Gets or sets the FixedLength fields indicator.
    /// <remarks>Indicates if the fields are fixed length.</remarks>
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCFixedLengthFields/*'/>
    public bool LJCFixedLengthFields { get; set; }

    // Gets or sets the HasHeadingLine value.
    /// <remarks>Indicates if there is a header line.</remarks>
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCHasHeadingLine/*'/>
    public bool LJCHasHeadingLine { get; set; }

    // Gets the LineOffsets collection.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCLineOffsets/*'/>
    public LJCLineOffsets LJCLineOffsets { get; private set; }

    // Gets or sets the Lines value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCLines/*'/>
    public string[] LJCLines { get; set; }

    // Gets or sets the PageSize value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCPageSize/*'/>
    public int LJCPageSize { get; set; }

    // Gets or sets the PageStart index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCPageStartIndex/*'/>
    public long LJCPageStartIndex { get; set; }

    // Gets or sets the number of lines to skip.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSkipHeaderLines/*'/>
    public short LJCSkipHeaderLines { get; set; }

    // Gets the Stream value. Pagination
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCStream/*'/>
    public Stream? LJCStream
    {
      get
      {
        Stream? retValue = null;

        if (LJCStreamReader != null)
        {
          retValue = LJCStreamReader.BaseStream;
        }
        return retValue;
      }
    }

    // Gets or sets the StreamReader value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCStreamReader/*'/>
    public StreamReader? LJCStreamReader { get; set; }

    // Gets or sets the LayoutFile Name value.
    private string? LJCLayoutFileName { get; set; }

    // Gets or sets the current line TextRegion values.
    private LJCTextRegions LJCTextRegions { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Constructor/*'/>
    public LJCTextDataReader(bool hasHeadingLine = true, short skipHeaderLines = 0
      , bool fixedLengthFields = false)
    {
      // Provided Properties
      LJCSkipHeaderLines = skipHeaderLines;
      LJCFixedLengthFields = fixedLengthFields;

      // Public Properties
      LJCDataFields = [];

      LJCTextRegions = [];
      LJCFieldDelimiter = ',';

      LJCFileName = null;
      LJCHasHeadingLine = hasHeadingLine;
      LJCLineOffsets = [];
      LJCLines = [];
      LJCStreamReader = null;

      // Pagination
      LJCCurrentRecordCount = -1;
      LJCPageSize = 0;
      LJCPageStartIndex = -1;

      // Private Properties
      LJCLayoutFileName = null;

      // IDataReader Properties
      IsClosed = true;
    }
    #endregion

    #region IDataReader Methods

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
      GC.SuppressFinalize(this);
    }

    // Not supported.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetData/*'/>
    public IDataReader GetData(int i)
    {
      return this;
    }

    // Retrieves the data type name for the supplied field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDataTypeName1/*'/>
    public string GetDataTypeName(int i)
    {
      LJCDataColumn dataColumn;
      string retValue = "";

      if (i > -1
        && i < FieldCount)
      {
        retValue = "String";
        dataColumn = LJCDataFields[i];
        if (dataColumn.DataTypeName != null)
        {
          var value = DataTypeEnumName(dataColumn.DataTypeName);
          if (value != null)
          {
            retValue = value;
          }
        }
      }
      return retValue;
    }

    // Retrieves the data type name for the supplied field name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDataTypeName2/*'/>
    public string GetDataTypeName(string name)
    {
      int index = GetOrdinal(name);
      return GetDataTypeName(index);
    }

    // Retrieves the type of the field for the supplied field index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFieldType/*'/>
    public Type GetFieldType(int i)
    {
      string dataTypeName;
      Type retValue;

      dataTypeName = GetDataTypeName(i);
#pragma warning disable IDE0066 // Convert switch statement to expression
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
#pragma warning restore IDE0066 // Convert switch statement to expression
      return retValue;
    }

    // Retrieves the name of the data field with the supplied index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetName/*'/>
    public string GetName(int i)
    {
      string retValue = "";

      if (i > -1 && i < FieldCount)
      {
        retValue = LJCDataFields[i].ColumnName;
      }
      return retValue;
    }

    // Retrieves the index of the data field with the supplied name.
    // Required method if field mapping is used.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetOrdinal/*'/>
    public int GetOrdinal(string name)
    {
      int retValue = -1;

      for (int index = 0; index < FieldCount; index++)
      {
        var columnName = LJCDataFields[index].ColumnName.ToLower();
        var lowerName = name.ToLower();
        StringComparison compare = StringComparison.InvariantCultureIgnoreCase;
        if (columnName.Equals(lowerName, compare))
        {
          retValue = index;
          break;
        }
      }
      return retValue;
    }

    // Gets the Fields as a Schema DataTable object.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetSchemaTable/*'/>
    public DataTable GetSchemaTable()
    {
      // Ordinal Values
      const int AllowDBNull = 13;
      const int ColumnName = 0;
      const int DataType = 12;
      const int DataTypeName = 24;
      const int ColumnSize = 2;
      const int IsAutoIncrement = 18;
      const int IsUnique = 5;

      DataTable retValue = CreateSchemaTable();
      foreach (LJCDataColumn dataColumn in LJCDataFields)
      {
        DataRow dataRow = retValue.NewRow();
        dataRow[AllowDBNull] = dataColumn.AllowDBNull;
        dataRow[IsAutoIncrement] = dataColumn.AutoIncrement;
        dataRow[ColumnName] = dataColumn.ColumnName;
        if (LJC.HasText(dataColumn.DataTypeName))
        {
          dataRow[DataType] = GetDataType(dataColumn.DataTypeName);
        }
        dataRow[DataTypeName] = dataColumn.DataTypeName;
        dataRow[ColumnSize] = dataColumn.MaxLength;
        dataRow[IsUnique] = dataColumn.IsUniqueKey;
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
      string text = GetString(i);
      bool retValue = (!LJC.HasText(text));
      return retValue;
    }

    // Sets to the next result. Currently returns false.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/NextResult/*'/>
    public bool NextResult()
    {
      return false;
    }

    // Reads the next line of the text file. Minimum Required
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/Read/*'/>
    public bool Read()
    {
      string? line = null;
      bool retValue = true;

      while (true)
      {
        // *** Begin *** Pagination
        if (LJCCurrentRecordCount > -1
          && LJCCurrentRecordCount >= LJCPageSize)
        {
          // End of page.
          retValue = false;
          LJCCurrentRecordCount = -1;
          break;
        }
        // *** End *** Pagination

        if (LJCStreamReader != null)
        {
          line = LJCStreamReader.ReadLine();
        }
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
          LJCLineOffsets?.LJCSetNextLineOffset(line);
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

    #region Custom Related Methods

    // Gets the LJCFieldDataType enum name from DataTypeName.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/DataTypeEnumName/*'/>
    public static string? DataTypeEnumName(string name)
    {
      string? retName = null;

      var names = Enum.GetNames<LJCFieldDataType>();
      for (short index = 1; index < names.Length + 1; index++)
      {
        var enumName = Enum.GetName((LJCFieldDataType)index);
        if (enumName != null)
        {
          //if (enumName.ToLower() == name.ToLower())
          if (enumName.Equals(name
            , StringComparison.InvariantCultureIgnoreCase))
          {
            retName = enumName;
            break;
          }
        }
      }
      return retName;
    }

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

    #region IDataRecord Get Data with Index Methods

    // Returns the bool value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBoolean/*'/>
    public bool GetBoolean(int i)
    {
      var _ = bool.TryParse(GetString(i), out bool retValue);
      return retValue;
    }

    // Returns the byte value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetByte/*'/>
    public byte GetByte(int i)
    {
      var _ = byte.TryParse(GetString(i), out byte retValue);
      return retValue;
    }

    // Place field bytes into a byte array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBytes/*'/>
    public long GetBytes(int i, long fieldOffset, byte[]? buffer
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
      var _ = char.TryParse(GetString(i), out char retValue);
      return retValue;
    }

    // Place field characters into a character array buffer.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetChars/*'/>
    public long GetChars(int i, long fieldOffset, char[]? buffer
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
      var _ = DateTime.TryParse(GetString(i), out DateTime retValue);
      return retValue;
    }

    // Returns the decimal value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDecimal/*'/>
    public decimal GetDecimal(int i)
    {
      var _ = decimal.TryParse(GetString(i), out decimal retValue);
      return retValue;
    }

    // Returns the double value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDouble/*'/>
    public double GetDouble(int i)
    {
      var _ = double.TryParse(GetString(i), out double retValue);
      return retValue;
    }

    // Returns the float value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFloat/*'/>
    public float GetFloat(int i)
    {
      var _ = float.TryParse(GetString(i), out float retValue);
      return retValue;
    }

    // Returns the Guid value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetGuid/*'/>
    public Guid GetGuid(int i)
    {
      var _ = Guid.TryParse(GetString(i), out Guid retValue);
      return retValue;
    }

    // Returns the short int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt16/*'/>
    public short GetInt16(int i)
    {
      var _ = short.TryParse(GetString(i), out short retValue);
      return retValue;
    }

    // Returns the int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt32/*'/>
    public int GetInt32(int i)
    {
      var _ = int.TryParse(GetString(i), out int retValue);
      return retValue;
    }

    // Returns the long int value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt64/*'/>
    public long GetInt64(int i)
    {
      var _ = long.TryParse(GetString(i), out long retValue);
      return retValue;
    }

    // Returns the string value of the data field at the specified index.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetString/*'/>
    public string GetString(int i)
    {
      string retValue = "";

      object value = this[i];
      if (value != null)
      {
        var result = value.ToString();
        if (LJC.HasText(result))
        {
          retValue = result;
        }
      }
      return retValue;
    }

    // Gets the trimmed string value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetTrimValue/*'/>
    public string GetTrimValue(int i)
    {
      var retValue = "";

      var result = GetString(i);
      if (LJC.HasText(result))
      {
        retValue = result.Trim();
      }
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

    #region Get Data with Name Overloaded Methods

    // Returns the bool value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetBooleanN/*'/>
    public bool GetBoolean(string name)
    {
      _ = bool.TryParse(GetString(name), out bool retValue);
      return retValue;
    }

    // Returns the byte value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetByteN/*'/>
    public byte GetByte(string name)
    {
      _ = byte.TryParse(GetString(name), out byte retValue);
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
      _ = char.TryParse(GetString(name), out char retValue);
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
      _ = DateTime.TryParse(GetString(name), out DateTime retValue);
      return retValue;
    }

    // Returns the decimal value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDecimalN/*'/>
    public decimal GetDecimal(string name)
    {
      _ = decimal.TryParse(GetString(name), out decimal retValue);
      return retValue;
    }

    // Returns the double value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetDoubleN/*'/>
    public double GetDouble(string name)
    {
      _ = double.TryParse(GetString(name), out double retValue);
      return retValue;
    }

    // Returns the float value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetFloatN/*'/>
    public float GetFloat(string name)
    {
      _ = float.TryParse(GetString(name), out float retValue);
      return retValue;
    }

    // Returns the Guid value of the data field with the specified name. 
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetGuidN/*'/>
    public Guid GetGuid(string name)
    {
      _ = Guid.TryParse(GetString(name), out Guid retValue);
      return retValue;
    }

    // Returns the short int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt16N/*'/>
    public short GetInt16(string name)
    {
      _ = short.TryParse(GetString(name), out short retValue);
      return retValue;
    }

    // Returns the int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt32N/*'/>
    public int GetInt32(string name)
    {
      _ = int.TryParse(GetString(name), out int retValue);
      return retValue;
    }

    // Returns the long int value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetInt64N/*'/>
    public long GetInt64(string name)
    {
      _ = long.TryParse(GetString(name), out long retValue);
      return retValue;
    }

    // Returns the string value of the data field with the specified name.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetStringN/*'/>
    public string GetString(string name)
    {
      string retValue = "";

      object value = this[name];
      if (value != null)
      {
        var result = value.ToString();
        if (LJC.HasText(result))
        {
          retValue = result;
        }
      }
      return retValue;
    }

    // Gets the trimmed string value.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/GetTrimValueN/*'/>
    public string GetTrimValue(string name)
    {
      string retValue = "";

      var result = GetString(name);
      if (LJC.HasText(result))
      {
        retValue = result;
      }
      return retValue;
    }
    #endregion

    #region Custom Methods

    // Checks for NewLine at end of file.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCEndsWithNewLine/*'/>
    public bool LJCEndsWithNewLine()
    {
      var retValue = false;

      var doClose = false;
      if (IsClosed)
      {
        LJCOpen();
        doClose = true;
      }

      if (LJCStream != null)
      {
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

    // Opens with the existing file settings.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCOpen/*'/>
    public void LJCOpen()
    {
      Close();
      if (LJC.HasText(LJCFileName))
      {
        LJCStreamReader = new StreamReader(LJCFileName, Encoding.UTF8
          , true, 4096);
        IsClosed = false;

        // #Pagination Next Statement - Add
        if (LJCStream != null)
        {
          LJCLineOffsets = new LJCLineOffsets(LJCStream);
        }

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
    }

    // Sets the data field definitions from an XML file.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetFields/*'/>
    public void LJCSetFields(string layoutFileName)
    {
      LJCLayoutFileName = layoutFileName;
      if (LJC.HasText(LJCLayoutFileName))
      {
        var dataFields = LJC.XmlDeserialize(typeof(LJCDataColumns)
          , LJCLayoutFileName) as LJCDataColumns;
        if (LJC.HasListItems(dataFields))
        {
          LJCDataFields = dataFields;
        }
      }
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
        LJCDataFields = [];
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
        var lines = LJCTextRegions.LJCSplit(line);
        if (LJC.HasListItems(lines))
        {
          LJCSetValues(lines);
        }
      }
      else
      {
        LJCSetValues(line.Split(LJCFieldDelimiter));
      }
    }

    // Sets the source line string array.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetLines/*'/>
    public void LJCSetLines(string[] lines, char fieldDelimiter = ',')
    {
      LJCLines = lines;
      LJCFieldDelimiter = fieldDelimiter;
      LJCDataFields = [];

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
    public bool LJCSetNames(string? line)
    {
      int maxLength = 0;
      string? dataTypeName = null;
      bool retValue = false;

      if (0 == FieldCount
        && LJC.HasText(line))
      {
        int position = 1;
        string[] fields = line.Split(LJCFieldDelimiter);
        foreach (string field in fields)
        {
          string? fieldName = null;

          // Check for included width and type value.
          string[] values = field.Split('|');
          if (LJC.HasArrayElements(values))
          {
            fieldName = values[0].Trim();
            maxLength = 0;
            dataTypeName = "String";
            switch (values.Length)
            {
              case 1:
                // No width or type value.
                break;

              case 2:
                // Has width value.
                _ = int.TryParse(values[1], out maxLength);
                break;

              case 3:
                // Has width and type value.
                _ = int.TryParse(values[1], out maxLength);
                dataTypeName = values[2];
                break;
            }
          }

          if (LJC.HasText(fieldName))
          {
            // Add field data definition.
            var dataColumn = new LJCDataColumn()
            {
              ColumnName = fieldName,
              DataTypeName = dataTypeName,
              MaxLength = maxLength,
              Position = position
            };
            LJCDataFields.Add(dataColumn);

            if (maxLength > 0)
            {
              position += maxLength;
            }
          }
        }
        retValue = true;
      }
      return retValue;
    }

    // Sets the data object property values from the TextDataReader.
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetObjectValues/*'/>
    public void LJCSetObjectValues(object dataObject, LJCDataColumns? dataFields = null)
    {
      LJCReflect reflect;
      LJCDataColumns? fieldColumns;

      fieldColumns = LJCDataFields;
      if (dataFields != null)
      {
        fieldColumns = dataFields;
      }

      if (LJC.HasListItems(fieldColumns))
      {
        reflect = new LJCReflect(dataObject);
        foreach (LJCDataColumn dataColumn in fieldColumns)
        {
          string sourceColumnName = dataColumn.ColumnName;
          if (LJC.HasText(dataColumn.RenameAs))
          {
            sourceColumnName = dataColumn.RenameAs;
          }
          reflect.SetPropertyValue(dataColumn.PropertyName
            , this[sourceColumnName]);
        }
      }
    }

    // Sets the page starting record index. Pagination
    /// <include file='Doc/TextDataReader.xml'
    ///  path='items/LJCSetStartIndex/*'/>
    public bool LJCSetStartIndex(int recordIndex)
    {
      bool retValue = false;

      LJCCurrentRecordCount = -1;
      if (recordIndex > -1
        && LJC.HasText(LJCFileName))
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
          if (LJCStream != null)
          {
            LJCStream.Position = offset;
          }
          LJCStreamReader?.DiscardBufferedData();
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
      if (values.Length > FieldCount)
      {
        isError = true;

        // Skip empty line.
        if (1 == values.Length
          && !LJC.HasText(values[0]))
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
        for (int index = 0; index < values.Length; index++)
        {
          LJCDataColumn dataField = LJCDataFields[index];
          if (dataField != null)
          {
            dataField.Value = values[index].Trim();
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

    // Adds a DataColumn to the DataTable object.
    private static void AddColumn(DataTable dataTable, string columnName
      , Type? dataType = null, bool readOnly = false, bool allowNull = true
      , bool autoIncrement = false)
    {
      object? defaultValue = null;
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
    private static DataTable CreateSchemaTable()
    {
      var retValue = new DataTable();

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
    private static long GetBytesFromText(string text, long fieldOffset, byte[]? buffer
      , int bufferoffset)
    {
      long retValue = 0;

      if (text != null
        && buffer != null)
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
            _ = byte.TryParse(text[fieldIndex].ToString(), out byte byteValue);
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
    private static long GetCharsFromText(string text, long fieldOffset, char[]? buffer
      , int bufferoffset)
    {
      long retValue = 0;

      if (text != null
        && buffer != null)
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
    private static Type GetDataType(string dataTypeName)
    {
      Type retValue;

#pragma warning disable IDE0066 // Convert switch statement to expression
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
#pragma warning restore IDE0066 // Convert switch statement to expression
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
  }

  // The field data types.
  /// <include file='Doc/TextDataReader.xml'
  ///  path='items/LJCFieldDataType/*'/>
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
