// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTextDataReader.cs
using LJCNetCommon5;
using LJCTextDataReader5;

namespace TestTextDataReader5
{
  // The entry class.
  internal class TestTextDataReader
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCTextDataReader");
      Console.WriteLine();
      Console.WriteLine("*** LJCTextDataReader ***");

      // IDataReader Methods
      Close();
      GetDataTypeName();
      GetFieldType();
      GetName();
      GetOrdinal();
      GetSchemaTable();
      GetValues();
      IsDBNull();
      Read();

      // Custom Related Methods
      DataTypeEnumName();
      LJCReadLine();

      // IDataRecord Get Data Methods
      GetWithIndex();
      GetWithName();

      // IDataReader Properties
      IsClosed();

      // IDataRecord Properties
      FieldCount();
      GetValueWithIndex();
      GetValueWithName();

      // Custom Methods
      LJCEndsWithNewLine();
      GetFieldNames();
      LJCOpen();
      LJCSetFields();
      LJCSetFile();
      LJCSetLine();
      LJCSetLines();
      LJCSetNames();
      LJCSetObjectValues();
      LJCSetStartIndex();
      LJCSetValues1();
      LJCSetValues2();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region IDataReader Methods

    // Closes the data reader.
    public static void Close()
    {
      var methodName = "Close()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      object value = textReader.IsClosed;
      var result = value.ToString();
      var compare = "False";
      TestCommon?.Write($"{methodName}0", result, compare);

      while (textReader.Read())
      {
        // Get row values as "object".

        // Get values by field name.
        var name = textReader["name"];
        result = name.ToString();
        compare = "Name Value";
        TestCommon?.Write($"{methodName}1", result, compare);

        var id = textReader["ID"];
        result = id.ToString();
        compare = "1";
        TestCommon?.Write("{methodName}2", result, compare);

        value = textReader["Value"];
        result = value.ToString();
        compare = "3.14";
        TestCommon?.Write($"{methodName}3", result, compare);

        // Get values by field index.
        name = textReader[0];
        result = name.ToString();
        compare = "Name Value";
        TestCommon?.Write($"{methodName}4", result, compare);

        id = textReader[1];
        result = id.ToString();
        compare = "1";
        TestCommon?.Write($"{methodName}5", result, compare);

        value = textReader[2];
        result = value.ToString();
        compare = "3.14";
        TestCommon?.Write($"{methodName}6", result, compare);
      }

      // Test Method
      textReader.Close();
      value = textReader.IsClosed;
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("{methodName}7", result, compare);
    }

    // Retrieves the data type name for the supplied field.
    private static void GetDataTypeName()
    {
      var methodName = "GetDataTypeName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      // Get by field name.
      var result = textReader.GetDataTypeName("ID");
      var compare = "Int64";
      TestCommon?.Write($"{methodName}1", result, compare);
      // Get by field index.
      result = textReader.GetDataTypeName(1);
      compare = "Int64";
      TestCommon?.Write($"{methodName}2", result, compare);
      textReader.Close();
    }

    // Retrieves the type of the field for the supplied field index.
    private static void GetFieldType()
    {
      var methodName = "GetFieldType()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      var type = textReader.GetFieldType(1);
      var result = type.Name;
      var compare = "Int64";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Retrieves the name of the data field with the supplied index.
    private static void GetName()
    {
      var methodName = "GetName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      var result = textReader.GetName(2);
      var compare = "Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Retrieves the index of the data field with the supplied name.
    private static void GetOrdinal()
    {
      var methodName = "GetOrdinal()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      var value = textReader.GetOrdinal("Value");
      var result = value.ToString();
      var compare = "2";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Gets the Fields as a Schema DataTable object.
    private static void GetSchemaTable()
    {
      var methodName = "GetSchemaTable()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      var schemaTable = textReader.GetSchemaTable();
      var row = schemaTable.Rows[0];
      var value = row["ColumnName"];
      var result = value.ToString();
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Gets an array of record values.
    private static void GetValues()
    {
      var methodName = "GetValues()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Method
      var values = new object[textReader.FieldCount];
      _ = textReader.GetValues(values);
      var value = values[2];
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Indicates if the field value is null or whitespace.
    private static void IsDBNull()
    {
      var methodName = "IsDBNull()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, , 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Method
      var value = textReader.IsDBNull(1);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Reads the next line of the text file.
    private static void Read()
    {
      var methodName = "Read()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      textReader.Read();
      // Read each line.
      // while (textReader.Read())
      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }
    #endregion

    #region Custom Related Methods

    // Gets the LJCFieldDataType enum name from DataTypeName.
    private static void DataTypeEnumName()
    {
      var methodName = "DataTypeEnumName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      var dataTypeName = textReader.GetDataTypeName(1);

      // Test Method
      var result = LJCTextDataReader.DataTypeEnumName(dataTypeName);
      var compare = "Int64";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Reads the next line from the line string array.
    private static void LJCReadLine()
    {
      var methodName = "LJCReadLine()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      // Sets the source line string array.
      textReader.LJCSetLines(lines);

      // Test Method
      textReader.LJCReadLine();
      // Read each line.
      //while (textReader.LJCReadLine))
      var value = textReader["ID"];
      var result = value.ToString();
      var compare = "1";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }
    #endregion

    #region IDataRecord Get Data Methods

    // Reads the next line of the text file.
    private static void GetWithIndex()
    {
      var methodName = "GetWithIndex()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      //var result = textReader.GetString(0);
      var result = textReader.GetTrimValue(0);
      result += $", {textReader.GetInt64(1)}";
      result += $", {textReader.GetDecimal(2)}";
      var compare = "Name Value, 1, 3.14";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }
    #endregion

    #region Get Data Custom Overloaded Methods

    // Reads the next line of the text file.
    private static void GetWithName()
    {
      var methodName = "GetWithName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      //var result = textReader.GetString("Name");
      var result = textReader.GetTrimValue("Name");
      result += $", {textReader.GetInt64("ID")}";
      result += $", {textReader.GetDecimal("Value")}";
      var compare = "Name Value, 1, 3.14";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }
    #endregion

    #region IDataReader Properties

    // Indicates if the reader is closed.
    private static void IsClosed()
    {
      var methodName = "GetWithName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      var value = textReader.IsClosed;
      var result = value.ToString();
      var compare = "False";
      TestCommon?.Write($"{methodName}1", result, compare);

      textReader.Close();
      value = textReader.IsClosed;
      result = value.ToString();
      compare = "True";
      TestCommon?.Write($"{methodName}2", result, compare);
    }
    #endregion

    #region IDataRecord Properties

    // Indicates if the reader is closed.
    private static void FieldCount()
    {
      var methodName = "FieldCount()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value|0|decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Methods
      var value = textReader.FieldCount;
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write($"{methodName}1", result, compare);
      textReader.Close();
    }

    // Gets the data field value for the supplied field index.
    private static void GetValueWithIndex()
    {
      var methodName = "GetValueWithIndex()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      var value = textReader[2];
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write($"{methodName}1", result, compare);
      textReader.Close();
    }

    // Gets the data field value for the supplied field index.
    private static void GetValueWithName()
    {
      var methodName = "GetValueWithName()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      var value = textReader["Value"];
      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write($"{methodName}1", result, compare);
      textReader.Close();
    }
    #endregion

    #region Custom Methods

    // Checks for NewLine at end of file.
    private static void LJCEndsWithNewLine()
    {
      var methodName = "LJCEndsWithNewLine()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Read();

      // Test Methods
      var value = textReader.LJCEndsWithNewLine();
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Checks for NewLine at end of file.
    private static void GetFieldNames()
    {
      var methodName = "GetFieldNames()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Methods
      var values = textReader.LJCGetFieldNames();
      var result = "";
      foreach (var value in values)
      {
        if (LJC.HasValue(result))
        {
          result += ", ";
        }
        result += value;
      }
      var compare = "Name, ID, Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Checks for NewLine at end of file.
    private static void LJCOpen()
    {
      var methodName = "LJCOpen()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");
      textReader.Close();

      // Test Methods
      textReader.LJCOpen();
      textReader.Read();
      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the data field definitions from an XML file.
    private static void LJCSetFields()
    {
      var methodName = "LJCSetFields()";
      string[] lines =
      [
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);
      var dataColumns = new LJCDataColumns
      {
        { "Name" },
        { "ID", 0, "int64" },
        { "Value", 0, "decimal" },
      };
      dataColumns.LJCSerialize("Text.xml");

      var hasHeadingLine = false;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      textReader.LJCSetFields("Text.xml");

      textReader.Read();
      // Read each line.
      // while (textReader.Read())

      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the source text file values.
    private static void LJCSetFile()
    {
      var methodName = "LJCSetFile()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);

      // Test Method
      textReader.LJCSetFile("Text.txt");
      var value = textReader.FieldCount;
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the line field values.
    private static void LJCSetLine()
    {
      var methodName = "LJCSetLine()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];

      var hasHeadingLine = false;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetNames(lines[0]);

      // Test Method
      textReader.LJCSetLine(lines[1]);
      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Set the source line string array.
    private static void LJCSetLines()
    {
      var methodName = "LJCSetLines()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      var textReader = new LJCTextDataReader();

      // Test Method
      textReader.LJCSetLines(lines);
      var value = textReader.LJCDataFields.Count;
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write($"{methodName}1", result, compare);
      textReader.Close();

      // Tab separated values.
      lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name\tID|0|int64\tValue|0|decimal",
        "Name Value\t1\t3.14",
      ];
      textReader = new LJCTextDataReader
      {
        LJCFieldDelimiter = "\t"[0]
      };
      textReader.LJCSetLines(lines, "\t"[0]);
      value = textReader.LJCDataFields.Count;
      result = value.ToString();
      compare = "3";
      TestCommon?.Write($"{methodName}2", result, compare);
      textReader.Close();
    }

    // Sets the data field names.
    private static void LJCSetNames()
    {
      var methodName = "LJCSetNames()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];

      var hasHeadingLine = false;
      var textReader = new LJCTextDataReader(hasHeadingLine);

      // Test Method
      // Normally let LJCSetFile() or LJCSetLines() call LJCSetNames().
      // This is called for direct method testing.
      textReader.LJCSetNames(lines[0]);

      var value = textReader.LJCDataFields.Count;
      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the data object property values from the TextDataReader.
    private static void LJCSetObjectValues()
    {
      var methodName = "LJCSetObjectValues()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetLines(lines);
      textReader.LJCReadLine();
      // Read each line.
      //while (textReader.LJCReadLine())

      // Test Method
      var dataObject = new Test();
      var dataFields = textReader.LJCDataFields;
      textReader.LJCSetObjectValues(dataObject, dataFields);
      var result = dataObject.Name;
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the page starting record index.
    private static void LJCSetStartIndex()
    {
      var methodName = "LJCSetStartIndex()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetFile("Text.txt");

      // Test Method
      // Initializes Pagination
      textReader.LJCPageStartIndex = 1;

      var value = textReader.LJCCurrentLineIndex;
      var result = value.ToString();
      var compare = "0";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the current line values.
    private static void LJCSetValues1()
    {
      var methodName = "LJCSetValues1()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name, ID||int64, Value||decimal",
        "Name Value, 1, 3.14",
      ];

      var hasHeadingLine = true;
      var textReader = new LJCTextDataReader(hasHeadingLine);
      textReader.LJCSetLines(lines);
      var line = lines[1];

      // Test Method
      // Normally let Read() or LJCReadLine() call LJCSetValues(string[]).
      // This is called for direct method testing.
      var values = line.Split(',');
      textReader.LJCSetValues(values);

      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }

    // Sets the current, fixed length, line values.
    private static void LJCSetValues2()
    {
      var methodName = "LJCSetValues2()";
      string[] lines =
      [
        // ColumnName|Length = 0|Type = "String".
        "Name|10, ID|2|int64, Value|5|decimal",
        "Name Value, 1, 3.14",
      ];
      File.WriteAllLines("Text.txt", lines);

      var textReader = new LJCTextDataReader()
      {
        LJCHasHeadingLine = true,
        LJCFixedLengthFields = true,
      };
      textReader.LJCSetFile("Text.txt");
      var line = lines[1];

      // Normally let Read() call LJCSetValues(string).
      // This is called for direct method testing.
      textReader.LJCSetValues(line);

      var value = textReader["Name"];
      var result = value.ToString();
      var compare = "Name Value";
      TestCommon?.Write($"{methodName}", result, compare);
      textReader.Close();
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion

    // The test Data Object.
    private class Test()
    {
      public string? Name { get; set; }

      public long ID { get; set; }

      public decimal Value { get; set; }
    }
  }
}
