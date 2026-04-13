// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnsProgram.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // The entry class.
  internal class DataColumnsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataColumns");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataColumns ***");

      // Static Methods
      LJCDeserialize();
      LJCGetMinSqlDate();
      LJCObjectDataColumns();
      LJCObjectPropertyNames();
      DataColumnsToDataValues();

      // Methods
      LJCSetColumnCaptions();
      LJCMapNames();

      // Collection Methods
      Add1();
      Add2();
      Add3();
      Add4();
      Clone();
      HasItems();
      LJCAddPropertyAs();
      LJCGetColumn();
      LJCGetColumns1();
      LJCGetColumns2();
      LJCPropertyNames();
      LJCRemoveColumn();
      LJCSerialize();
      LJCSetData();

      // Item Methods
      LJCClearChanged();
      LJCGetChanged();

      // Search and Sort Methods
      LJCSearchColumnName();
      LJCSearchPropertyName();
      LJCSearchRenameAs();
      LJCSortAddOrderIndex();
      LJCSortName();
      LJCSortProperty();
      LJCSortRenameAs();

      // Value Methods
      // Also in LJCDataValues
      LJCGetBoolean();
      LJCGetByte();
      LJCGetChar();
      LJCGetDbDateTime();
      LJCGetDecimal();
      LJCGetDouble();
      LJCGetInt16();
      LJCGetInt32();
      LJCGetInt64();
      LJCGetObject();
      LJCGetSingle();
      LJCGetString();
      LJCSetValue();
    }

    #region Static Methods

    // Deserializes from the specified XML file.
    private static void LJCDeserialize()
    {
      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      dataColumns.LJCSerialize();
      // Test Method
      var newDataColumns = LJCDataColumns.LJCDeserialize();
      // Check Result
      var dataColumn = newDataColumns?.LJCSearchPropertyName("ID");
      var result = dataColumn?.PropertyName;
      var compare = "ID";
      TestCommon?.Write("LJCDeserialize()", result, compare);
    }

    // Get the minimum date value.
    private static void LJCGetMinSqlDate()
    {
      var result = LJCDataColumns.LJCMinSqlDate();
      var compare = "1753/01/01 00:00:00";
      TestCommon?.Write("LJCGetMinSqlDate()", result, compare);
    }

    // Creates LJCDataColumns from a Data Object.
    private static void LJCObjectDataColumns()
    {
      var dataObject = new { ID = 1, Name = "Name" };
      // Test Method
      var dataColumns = LJCDataColumns.LJCObjectDataColumns(dataObject);
      // Check Result
      var dataColumn = dataColumns?[0];
      var result = dataColumn?.PropertyName;
      var compare = "ID";
      TestCommon?.Write("LJCObjectDataColumns()", result, compare);
    }

    // Creates a PropertyNames list from a DataObject.
    private static void LJCObjectPropertyNames()
    {
      var dataObject = new { ID = 1, Name = "Name" };
      // Test Method
      var values = LJCDataColumns.LJCObjectPropertyNames(dataObject);
      // Check Result
      string result = null;
      if (LJC.HasItems(values))
      {
        result = string.Join(", ", values);
      }
      var compare = "ID, Name";
      TestCommon?.Write("LJCObjectPropertyNames()", result, compare);
    }

    // Creates an LJCDataValues object from an LJCDataColumns object.
    private static void DataColumnsToDataValues()
    {
      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      // Test Method
      var dataValues = dataColumns;
      // Check Result
      var dataValue = dataValues[0];
      var result = dataValue.PropertyName;
      var compare = "ID";
      TestCommon?.Write("DataValuesToDataColumns()", result, compare);
    }
    #endregion

    #region Methods

    // Sets the caption properties.
    private static void LJCSetColumnCaptions()
    {
      var withCaptions = new LJCDataColumns();
      var dataColumn = withCaptions.Add("Name", 1);
      dataColumn.Caption = "Name Value";
      dataColumn = withCaptions.Add("ID", 1, "Int64");
      dataColumn.Caption = "ID Value";
      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      // Test Method
      withCaptions.LJCSetColumnCaptions(dataColumns);
      // Check Result
      dataColumn = dataColumns[1];
      var result = dataColumn.Caption;
      var compare = "Name Value";
      TestCommon?.Write("LJCSetColumnCaptions()", result, compare);
    }

    // Maps the column property and rename values.
    private static void LJCMapNames()
    {
      var dataColumns = new LJCDataColumns()
      {
        { "Name", 1 },
        { "ID", 1, "Int64" },
      };
      // Test Method
      dataColumns.LJCMapNames("Name", "NameProperty", "NameRename"
        , "Name Property");
      // Check Result
      var value = dataColumns.LJCSearchColumnName("Name");
      var result = value?.PropertyName;
      var compare = "NameProperty";
      TestCommon?.Write("LJCMapNames()1", result, compare);

      result = value?.RenameAs;
      compare = "NameRename";
      TestCommon?.Write("LJCMapNames()2", result, compare);

      result = value?.Caption;
      compare = "Name Property";
      TestCommon?.Write("LJCMapNames()3", result, compare);
    }
    #endregion

    #region Collection Methods

    // Adds the object element to the collection
    private static void Add1()
    {
      var dataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("Name", value: "1");
      // Test Method
      dataColumns.Add(dataColumn);
      // Check Result
      dataColumn = dataColumns[0];
      var result = dataColumn?.PropertyName;
      var compare = "Name";
      TestCommon?.Write("Add1()", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    private static void Add2()
    {
      var dataColumns = new LJCDataColumns();
      // Test Method
      var dataColumn = dataColumns.Add("ID", 1);
      dataColumn.DataTypeName = LJC.TypeInt64;
      // Unsigned 64-bit = 20 digits decimal.
      dataColumns.Add("Name", 21, 60);
      // Check Result
      dataColumn = dataColumns[1];
      var result = dataColumn.Position.ToString();
      var compare = "21";
      TestCommon?.Write("Add2()", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add3()
    {
      var dataColumns = new LJCDataColumns();
      // Test Method
      var dataColumn = dataColumns.Add("ID", dataTypeName: LJC.TypeInt64
        , caption: "Primary Key");
      // Check Result
      var result = dataColumn.Caption;
      var compare = "Primary Key";
      TestCommon?.Write("Add3()1", result, compare);

      // Test Method
      dataColumns.Add("Name", caption: "Name Column");
      // Check Result
      var value = dataColumns[1];
      result = value.Caption;
      compare = "Name Column";
      TestCommon?.Write("Add3()2", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add4()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Check Result
      var value = dataColumns[1];
      var result = value.PropertyName;
      var compare = "Name";
      TestCommon?.Write("Add4()", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      var newDataColumns = dataColumns.Clone();
      // Check Result
      var value = dataColumns[1];
      var result = value.PropertyName;
      var compare = "Name";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      var value = dataColumns.HasItems();
      // Check Result
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("HasItems()", result, compare);
    }

    // Creates the LJCDataColumn from the supplied values and adds to the collection.
    private static void LJCAddPropertyAs()
    {
      var dataColumns = new LJCDataColumns();
      // Test Method
      dataColumns.LJCAddPropertyAs("IDProperty", dataTypeName: LJC.TypeInt64);
      dataColumns.LJCAddPropertyAs("NameProperty");
      // Check Result
      var value = dataColumns[1];
      var result = value.PropertyName;
      var compare = "NameProperty";
      TestCommon?.Write("LJCAddPropertyAs()", result, compare);
    }

    // Returns a column by property name.
    private static void LJCGetColumn()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      var value = dataColumns.LJCGetColumn("Name");
      var result = value?.Value?.ToString();
      var compare = "Name Value";
      TestCommon?.Write("LJCGetColumn()", result, compare);
    }

    // Returns a set of columns that match the supplied list.
    private static void LJCGetColumns1()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      var propertNames = new List<string>
      {
        "Name",
      };
      var newDataColumns = dataColumns.LJCGetColumns(propertNames);
      // Check Result
      var result = newDataColumns?.Count.ToString();
      var compare = "1";
      TestCommon?.Write("LJCGetColumns1()1", result, compare);

      if (LJC.HasItems(newDataColumns))
      {
        var value = newDataColumns[0];
        result = value?.Value?.ToString();
        compare = "Name Value";
        TestCommon?.Write("LJCGetColumns1()2", result, compare);
      }
    }

    // Configure the Grid Columns from the Data object properties.
    private static void LJCGetColumns2()
    {
      var dataObject = new TestObject();
      dataObject.Name = "Name Value";
      // Test Method
      var propertyNames = new List<string>
      {
        "Name",
      };
      // Creates columns from the data object.
      var newDataColumns = LJCDataColumns.LJCGetColumns(dataObject
        , propertyNames);
      // Check Result
      var result = newDataColumns?.Count.ToString();
      var compare = "1";
      TestCommon?.Write("LJCGetColumns2()1", result, compare);

      if (LJC.HasItems(newDataColumns))
      {
        var value = newDataColumns[0];
        result = value?.Value?.ToString();
        compare = "Name Value";
        TestCommon?.Write("LJCGetColumns2()2", result, compare);
      }
    }

    // Get the list of property names.
    private static void LJCPropertyNames()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      var names = dataColumns.LJCGetPropertyNames();
      // Check Result
      string result = null;
      if (LJC.HasItems(names))
      {
        result = string.Join(", ", names);
      }
      var compare = "ID, Name";
      TestCommon?.Write("LJCPropertyNames()", result, compare);
    }

    // Removes an LJCDataColumn item.
    private static void LJCRemoveColumn()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" }
      };
      // Test Method
      dataColumns.LJCRemoveColumn("ID");
      // Check Result
      var result = dataColumns?.Count.ToString();
      var compare = "1";
      TestCommon?.Write("LJCRemoveColumn()1", result, compare);

      if (LJC.HasItems(dataColumns))
      {
        var value = dataColumns[0];
        result = value?.Value?.ToString();
        compare = "Name Value";
        TestCommon?.Write("LJCRemoveColumn()2", result, compare);
      }
    }

    // Serializes the collection
    private static void LJCSerialize()
    {
      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      // Test Method
      dataColumns.LJCSerialize();
      // Check Result
      var newDataColumns = LJCDataColumns.LJCDeserialize();
      var dataColumn = newDataColumns?.LJCGetColumn("ID");
      var result = dataColumn?.PropertyName;
      var compare = "ID";
      TestCommon?.Write("LJCSerialize()", result, compare);
    }

    // Add or Update.
    private static void LJCSetData()
    {
      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
      };
      // Test Method
      var dataColumn = new LJCDataColumn()
      {
        DataTypeName = LJC.TypeString,
        MaxLength = 60,
        PropertyName = "Name",
        Value = "Name Value",
      };
      dataColumns.LJCSetData(dataColumn);
      // Check Result
      var result = dataColumns?.Count.ToString();
      var compare = "2";
      TestCommon?.Write("LJCSetData()1", result, compare);

      if (LJC.HasItems(dataColumns))
      {
        var testDataColumn = dataColumns[1];
        result = testDataColumn.PropertyName;
        compare = "Name";
        TestCommon?.Write("LJCSetData()2", result, compare);
      }

      dataColumn.Value = "Updated";
      // Test Method
      dataColumns?.LJCSetData(dataColumn);
      if (LJC.HasItems(dataColumns))
      {
        var testDataColumn = dataColumns[1];
        result = testDataColumn?.Value?.ToString();
        compare = "Updated";
        TestCommon?.Write("LJCSetData()3", result, compare);
      }
    }
    #endregion

    #region Item Methods

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1 },
        { "Name", (object)"Name Value" }
      };
      // ToDo: This should not be necessary.
      dataColumns.LJCClearChanged();
      // Test Method
      var testDataColumns = dataColumns.LJCGetChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Write("LJCClearChanged()1", result, compare);

      dataColumns.LJCSetValue("Name", "Updated");
      testDataColumns = dataColumns.LJCGetChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Write("LJCClearChanged()2", result, compare);

      // Test Method
      dataColumns.LJCClearChanged();
      testDataColumns = dataColumns.LJCGetChanged();
      result = testDataColumns.Count.ToString();
      compare = "0";
      TestCommon?.Write("LJCClearChanged()3", result, compare);

    }

    // Gets a collection of changed columns.
    private static void LJCGetChanged()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1 },
        { "Name", (object)"Name Value" }
      };
      // ToDo: This should not be necessary.
      dataColumns.LJCClearChanged();
      // Test Method
      var testDataColumns = dataColumns.LJCGetChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Write("LJCGetChanged()1", result, compare);

      dataColumns.LJCSetValue("Name", "Updated");
      testDataColumns = dataColumns.LJCGetChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Write("LJCGetChanged()2", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    private static void LJCSearchColumnName()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" },
      };
      // Test Method
      var dataColumn = dataColumns.LJCSearchPropertyName("Name");
      // Check Result
      var result = dataColumn?.ColumnName;
      var compare = "Name";
      TestCommon?.Write("LJCSearchColumnName()", result, compare);
    }

    // Finds and returns the column that contains the supplied property name.
    private static void LJCSearchPropertyName()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" },
      };
      // Test Method
      var dataColumn = dataColumns.LJCSearchPropertyName("Name");
      // Check Result
      var result = dataColumn?.PropertyName;
      var compare = "Name";
      TestCommon?.Write("LJCSearchPropertyName()", result, compare);
    }

    // Finds and returns the column that contains the supplied property name.
    private static void LJCSearchRenameAs()
    {
      var dataColumns = new LJCDataColumns();
      dataColumns.Add("ID", dataTypeName: LJC.TypeInt64);
      dataColumns.Add("Name", renameAs: "ResultName");
      // Test Method
      var testDataColumn = dataColumns.LJCSearchRenameAs("ResultName");
      // Check Result
      var result = testDataColumn?.ColumnName;
      var compare = "Name";
      TestCommon?.Write("LJCSearchRenameAs()", result, compare);
    }

    // Sort on AddOrderIndex.
    private static void LJCSortAddOrderIndex()
    {
      var dataColumns = new LJCDataColumns
      {
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };
      dataColumns.LJCSortName(new DbColumnNameComparer());
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.PropertyName;
      var compare = "Name";
      TestCommon?.Write("LJCSortAddOrderIndex()1", result, compare);

      // Test Method
      dataColumns.LJCSortAddOrderIndex();
      // Check Result
      testDataColumn = dataColumns[1];
      result = testDataColumn.PropertyName;
      compare = "ID";
      TestCommon?.Write("LJCSortAddOrderIndex()2", result, compare);
    }

    // Sort on ColumnName.
    private static void LJCSortName()
    {
      var dataColumns = new LJCDataColumns
      {
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };
      // Test Method
      dataColumns.LJCSortName(new DbColumnNameComparer());
      // Check Result
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.ColumnName;
      var compare = "Name";
      TestCommon?.Write("LJCSortName()", result, compare);
    }

    // Sort on PropertyName.
    private static void LJCSortProperty()
    {
      var dataColumns = new LJCDataColumns
      {
        // Must add object cast to get the Add() value overload method.
        { "Name", (object)"Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };
      // Test Method
      dataColumns.LJCSortProperty(new DbColumnPropertyComparer());
      // Check Result
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.PropertyName;
      var compare = "Name";
      TestCommon?.Write("LJCSortProperty()", result, compare);
    }

    // Sort on PropertyName.
    private static void LJCSortRenameAs()
    {
      var dataColumns = new LJCDataColumns();
      dataColumns.Add("Name", renameAs: "ResultName");
      dataColumns.Add("ID", dataTypeName: LJC.TypeInt64);
      // Test Method
      dataColumns.LJCSortRenameAs(new LJCDataColumnRenameAsComparer());
      // Check Result
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.RenameAs;
      var compare = "ResultName";
      TestCommon?.Write("LJCSortRenameAs()", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private static void LJCGetBoolean()
    {
      var dataColumns = new LJCDataColumns
      {
        { "TestValue", 1, LJC.TypeBoolean },
      };
      // Test Method
      var value = dataColumns.LJCGetBoolean("TestValue");
      // Check REsult
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("LJCGetBoolean()", result, compare);
    }

    // Gets the column object value as a byte.
    private static void LJCGetByte()
    {
      var test = "C";
      var testBytes = LJC.TextToBytes(test);
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", testBytes, "byte" },
      };
      // Test Method
      var value = dataColumns.LJCGetByte("TestValue");
      // Check Result
      var bytes = new byte[] { value };
      var result = LJC.BytesToText(bytes);
      var compare = "C";
      TestCommon?.Write("LJCGetBytes()", result, compare);
    }

    // Gets the column object value as a char.
    private static void LJCGetChar()
    {
      var test = 'C';
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataColumns.LJCGetChar("TestValue");

      var result = value.ToString();
      var compare = "C";
      TestCommon?.Write("LJCGetChar()", result, compare);
    }

    // Gets the column object value as a DateTime.
    private static void LJCGetDbDateTime()
    {
      var test = new DateTime(2026, 1, 1);
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataColumns.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon?.Write("LJCGetDbDateTime()", result, compare);
    }

    // Gets the column object value as a decimal.
    private static void LJCGetDecimal()
    {
      var test = 3.14m;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataColumns.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetDecimal()", result, compare);
    }

    // Gets the column object value as a double.
    private static void LJCGetDouble()
    {
      var test = 3.14d;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataColumns.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetDouble()", result, compare);
    }

    // Gets the column object value as a short int.
    private static void LJCGetInt16()
    {
      var test = (short)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt16()", result, compare);
    }

    // Gets the column object value as an int.
    private static void LJCGetInt32()
    {
      var test = 3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt32()", result, compare);
    }

    // Gets the column object value as a long int.
    private static void LJCGetInt64()
    {
      var test = (long)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt64()", result, compare);
    }

    // Gets the column object value as an object.
    private static void LJCGetObject()
    {
      var test = (object)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Object" },
      };

      // Test Method
      var value = dataColumns.LJCGetObject("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetObject()", result, compare);
    }

    // Gets the column object value as a single.
    private static void LJCGetSingle()
    {
      var test = 3.14f;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataColumns.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetSingle()", result, compare);
    }

    // Gets the string value for the column with the specified name.
    private static void LJCGetString()
    {
      var test = "3.14";
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      var result = dataColumns.LJCGetString("TestValue");

      var compare = "3.14";
      TestCommon?.Write("LJCGetString()", result, compare);
    }

    // Update column value.
    private static void LJCSetValue()
    {
      var test = "3.14";
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      dataColumns.LJCSetValue("TestValue", "3.14159");

      var result = dataColumns.LJCGetString("TestValue");
      var compare = "3.14159";
      TestCommon?.Write("LJCSetValue()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }

  public class TestObject
  {
    public long ID { get; set; }
    public string? Name { get; set; }
  }
}
