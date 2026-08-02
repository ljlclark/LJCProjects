// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumns.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // Provides the LJCDataColumns test methods.
  internal class TestDataColumns
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumns()
    {
      TestCommon = new LJCTestCommon("TestDataColumns");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataColumns ***");

      //TestCommon.ShowNotImplemented = false;
      Run();
    }

    // Runs the test methods.
    private static void Run()
    {
      #region Static Methods

      // Deserializes from the specified XML file.
      LJCDeserialize();

      // Creates LJCDataColumns from a Data Object.
      LJCObjectColumns();

      // Gets a collection of items from a data object that match the supplied
      // property Names.
      LJCObjectColumnsInList();

      // Creates a PropertyNames list from a DataObject.
      LJCObjectPropertyNames();

      // Creates an LJCDataValues object from an LJCDataColumns object.
      DataColumnsToDataValues();
      #endregion

      #region Collection Methods

      // Creates and returns a clone of the object.
      Clone();

      // Checks if the collection has items.
      HasItems();

      // Gets a collection of changed columns.
      LJCChanged();

      // Sets the IsChanged value to false for all elements in the collection.
      LJCClearChanged();

      // Returns a set of columns that match the supplied list.
      LJCColumns();

      // Gets a list of property names from the unique keys.
      LJCKeyPropertyNames();

      // Get the list of property names.
      LJCPropertyNames();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Methods

      // Adds the supplied item to the collection
      Add1();

      // Creates item with Position and MaxLength and adds it to the collection.
      Add2();

      // Creates item with Value and adds it to the collection.
      Add3();

      // Gets the column that matches the key columns.
      // The column is identified by its property names and values.
      LJCGetUnique();

      // Removes the item with the supplied property name.
      LJCRemove();

      // Add or Update.
      LJCSetData();

      // Sorts on the current key columns.
      LJCSort();
      #endregion

      #region Other Methods

      // Sets the caption properties.
      LJCSetColumnCaptions();

      // Maps the column property and rename values.
      LJCMapNames();
      #endregion

      #region Get Unique Examples

      // Finds and returns the object that matches the supplied values.
      UniqueColumnName();

      // Finds and returns the column that contains the supplied property name.
      UniquePropertyName();

      // Sort on AddOrderIndex.
      UniqueAddOrderIndex();
      #endregion

      #region Value Methods

      // Gets the column object value as a bool.
      LJCGetBoolean();

      // Gets the column object value as a byte.
      LJCGetByte();

      // Gets the column object value as a char.
      LJCGetChar();

      // Gets the column object value as a DateTime.
      LJCGetDbDateTime();

      // Gets the column object value as a decimal.
      LJCGetDecimal();

      // Gets the column object value as a double.
      LJCGetDouble();

      // Gets the column object value as a short int.
      LJCGetInt16();

      // Gets the column object value as an int.
      LJCGetInt32();

      // Gets the column object value as a long int.
      LJCGetInt64();

      // Gets the column object value as an object.
      LJCGetValue();

      // Gets the column object value as a single.
      LJCGetSingle();

      // Gets the string value for the column with the specified name.
      LJCGetString();

      // Update column value.
      LJCSetValue();
      #endregion
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private static void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", "Name Value" },
      };
      dataColumns.LJCSerialize();

      // Test Method
      var newDataColumns = LJCDataColumns.LJCDeserialize();
      // Check Result
      var result = "";
      if (newDataColumns != null)
      {
        var dataColumn = newDataColumns["ID"];
        result = dataColumn?.PropertyName;
      }
      var compare = "ID";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets a collection of items from a data object.
    private static void LJCObjectColumns()
    {
      var methodName = "LJCObjectColumns()";

      var dataObject = new { ID = 1, Name = "Name Value" };

      // Test Method
      var dataColumns = LJCDataColumns.LJCObjectColumns(dataObject);
      // Check Result
      var dataColumn = dataColumns?[0];
      var result = dataColumn?.PropertyName;
      var compare = "ID";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets a collection of items from a data object.
    private static void LJCObjectColumnsInList()
    {
      var methodName = "LJCObjectColumnsInList()";

      var dataObject = new { ID = 1, Name = "Name Value" };

      var propertyNames = new List<string>()
      {
        "Name",
      };

      // Test Method
      var dataColumns = LJCDataColumns.LJCObjectColumnsInList(dataObject
        , propertyNames);
      // Check Result
      var dataColumn = dataColumns?[0];
      var result = dataColumn?.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets a list of property names from a data object.
    private static void LJCObjectPropertyNames()
    {
      var methodName = "LJCObjectPropertyNames()";

      var dataObject = new { ID = 1, Name = "Name Value" };

      // Test Method
      var values = LJCDataColumns.LJCObjectPropertyNames(dataObject);
      // Check Result
      string result = null;
      if (LJC.HasListItems(values))
      {
        result = string.Join(", ", values);
      }
      var compare = "ID, Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Creates an LJCDataValues object from an LJCDataColumns object.
    private static void DataColumnsToDataValues()
    {
      var methodName = "DataColumnsToDataValues()";

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", "Name Value" },
      };

      // Test Method
      var dataValues = dataColumns;
      // Check Result
      var dataValue = dataValues[0];
      var result = dataValue.PropertyName;
      var compare = "ID";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var newDataColumns = dataColumns.Clone();
      // Check Result
      var value = newDataColumns[1];
      var result = value.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems()
    {
      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var value = dataColumns.HasItems();
      // Check Result
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Show("HasItems()", result, compare);
    }

    // Gets a collection of changed columns.
    private static void LJCChanged()
    {
      var methodName = "LJCChanged()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1 },
        { "Name", (object)"Name Value" },
      };
      // ToDo: This should not be necessary.
      dataColumns.LJCClearChanged();

      // Test Method
      var testDataColumns = dataColumns.LJCChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Show($"{methodName}1", result, compare);

      dataColumns.LJCSetValue("Name", "Updated");
      testDataColumns = dataColumns.LJCChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1 },
        { "Name", (object)"Name Value" },
      };
      // ToDo: This should not be necessary.
      dataColumns.LJCClearChanged();

      // Test Method
      var testDataColumns = dataColumns.LJCChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Show($"{methodName}1", result, compare);

      dataColumns.LJCSetValue("Name", "Updated");
      testDataColumns = dataColumns.LJCChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Show($"{methodName}2", result, compare);

      // Test Method
      dataColumns.LJCClearChanged();
      testDataColumns = dataColumns.LJCChanged();
      result = testDataColumns.Count.ToString();
      compare = "0";
      TestCommon?.Show($"{methodName}3", result, compare);

    }

    // Returns a set of columns that match the supplied list.
    private static void LJCColumns()
    {
      var methodName = "LJCColumns()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var propertNames = new List<string>
      {
        "Name",
      };
      var newDataColumns = dataColumns.LJCColumns(propertNames);
      // Check Result
      var result = newDataColumns?.Count.ToString();
      var compare = "1";
      TestCommon?.Show($"{methodName}1", result, compare);

      if (LJC.HasListItems(newDataColumns))
      {
        var value = newDataColumns[0];
        result = value?.Value?.ToString();
        compare = "Name Value";
        TestCommon?.Show($"{methodName}2", result, compare);
      }
    }

    // Gets a list of property names from the unique keys.
    private static void LJCKeyPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };
      var keys = new LJCDataColumns()
      {
        { "ParentID", 1 },
        { "Name", "Name Value" },
      };
      dataColumns.LJCKeys = keys;

      // Test Method
      var names = dataColumns.LJCKeyPropertyNames();
      // Check Result
      string result = null;
      if (LJC.HasListItems(names))
      {
        result = string.Join(", ", names);
      }
      var compare = "ParentID, Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets a list of property names from the collection items.
    private static void LJCPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var names = dataColumns.LJCPropertyNames();
      // Check Result
      string result = null;
      if (LJC.HasListItems(names))
      {
        result = string.Join(", ", names);
      }
      var compare = "ID, Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Serializes the collection
    private static void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      dataColumns.LJCSerialize();
      // Check Result
      var newDataColumns = LJCDataColumns.LJCDeserialize();
      var result = "";
      if (newDataColumns != null)
      {
        var dataColumn = newDataColumns["ID"];
        result = dataColumn?.PropertyName;
      }
      var compare = "ID";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Adds the object element to the collection
    private static void Add1()
    {
      var methodName = "Add1()";

      var dataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("Name", "Name Value");

      // Test Method
      dataColumns.Add(dataColumn);
      // Check Result
      dataColumn = dataColumns[0];
      var result = dataColumn?.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    private static void Add2()
    {
      var methodName = "Add2()";

      var dataColumns = new LJCDataColumns();

      // Test Method
      var dataColumn = dataColumns.Add("ID", 1);
      dataColumn.DataTypeName = LJC.TypeInt64;
      // Unsigned 64-bit = 20 digits decimal.
      int position = 21;
      int length = 60;
      dataColumns.Add("Name", position, length);
      // Check Result
      dataColumn = dataColumns[1];
      var result = dataColumn.Position.ToString();
      var compare = "21";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add3()
    {
      var methodName = "Add3()";

      var dataColumns = new LJCDataColumns();

      // Test Method
      var dataColumn = dataColumns.Add("ID", dataTypeName: LJC.TypeInt64);
      // Check Result
      var result = dataColumn.DataTypeName;
      var compare = "Int64";
      TestCommon?.Show($"{methodName}1", result, compare);

      // Test Method
      dataColumns.Add("Name");
      // Check Result
      var value = dataColumns[1];
      result = value.DataTypeName;
      compare = "string";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Gets the column that matches the key columns.
    // The column is identified by its property names and values.
    private static void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      var keys = new LJCDataColumns()
      {
        { "PropertyName", "Name" },
      };
      var dataColumns = new LJCDataColumns()
      {
        { "Name", "Name Value" },
        { "Value", 1 },
      };
      dataColumns.LJCKeys = keys;

      // Test Method
      var dataColumn = dataColumns.LJCGetUnique();
      // Check Result
      var result = "";
      if (dataColumn != null)
      {
        result = $"{dataColumn.PropertyName}";
      }
      var compare = "Name";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Removes an LJCDataColumn item.
    private static void LJCRemove()
    {
      var methodName = "LJCRemove()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" }
      };
      // Test Method
      dataColumns.LJCRemove("ID");
      // Check Result
      var result = dataColumns?.Count.ToString();
      var compare = "1";
      TestCommon?.Show($"{methodName}1", result, compare);

      if (LJC.HasListItems(dataColumns))
      {
        var dataColumn = dataColumns[0];
        result = $"{dataColumn.Value}";
        compare = "Name Value";
        TestCommon?.Show($"{methodName}2", result, compare);
      }
    }

    // Add or Update.
    private static void LJCSetData()
    {
      var methodName = "LJCSetData()";

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 1, "Int64" },
      };
      var dataColumn = new LJCDataColumn()
      {
        DataTypeName = LJC.TypeString,
        MaxLength = 60,
        PropertyName = "Name",
        Value = "Name Value",
      };

      // Test Method
      dataColumns.LJCSetData(dataColumn);
      // Check Result
      var result = dataColumns?.Count.ToString();
      var compare = "2";
      TestCommon?.Show($"{methodName}1", result, compare);

      if (LJC.HasListItems(dataColumns))
      {
        var testDataColumn = dataColumns[1];
        result = testDataColumn.PropertyName;
        compare = "Name";
        TestCommon?.Show($"{methodName}2", result, compare);
      }

      dataColumn.Value = "Updated";
      // Test Method
      dataColumns?.LJCSetData(dataColumn);
      // Check Result
      if (LJC.HasListItems(dataColumns))
      {
        var testDataColumn = dataColumns[1];
        result = testDataColumn?.Value?.ToString();
        compare = "Updated";
        TestCommon?.Show($"{methodName}3", result, compare);
      }
    }

    // Sorts on the current key columns.
    private static void LJCSort()
    {
      var methodName = "LJCSort()";

      var dataColumns = new LJCDataColumns
      {
        { "Name", "Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };

      // Test Method
      // Sort on LJCDataColumn.PropertyName.
      var propertyName = LJCDataColumn.ColumnPropertyName;
      var keys = LJC.Keys(propertyName, "");
      dataColumns.LJCSort(keys);
      // Check Result
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Other Methods

    // Sets the caption properties.
    private static void LJCSetColumnCaptions()
    {
      var methodName = "LJCsetColumnCaptions()";

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
      withCaptions.LJCSetCaptions(dataColumns);
      // Check Result
      dataColumn = dataColumns[1];
      var result = dataColumn.Caption;
      var compare = "Name Value";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Maps the column property and rename values.
    private static void LJCMapNames()
    {
      var methodName = "LJCMapNames()";

      var dataColumns = new LJCDataColumns()
      {
        { "Name", 1 },
        { "ID", 1, "Int64" },
      };

      // Test Method
      // ColumnName, PropertyName?, RenameAs?, Caption?);
      dataColumns.LJCMapNames("Name", "NameProperty", "NameRename"
        , "Name Property");
      // Check Result
      // Get where DataColumn property = "PropertyName"
      // and value = "Name".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "NameProperty");
      var value = dataColumns.LJCGetUnique(keys);
      var result = value?.PropertyName;
      var compare = "NameProperty";
      TestCommon?.Show($"{methodName}1", result, compare);

      result = value?.RenameAs;
      compare = "NameRename";
      TestCommon?.Show($"{methodName}2", result, compare);

      result = value?.Caption;
      compare = "Name Property";
      TestCommon?.Show($"{methodName}3", result, compare);
    }
    #endregion

    #region Get Unique Examples

    // Finds and returns the object that matches the supplied values.
    private static void UniqueColumnName()
    {
      var methodName = "UniqueColumnName()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      // Get where DataColumn property = "ColumnName"
      //   , value = "Name".
      var keys = LJC.Keys(LJCDataColumn.ColumnColumnName, "Name");
      var dataColumn = dataColumns.LJCGetUnique(keys);
      // Check Result
      var result = dataColumn?.ColumnName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Finds and returns the column that contains the supplied property name.
    private static void UniquePropertyName()
    {
      var methodName = "UniquePropertyName()";

      var dataColumns = new LJCDataColumns
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      // Get where DataColumn property = "PropertyName"
      //   , value = "Name".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "Name");
      var dataColumn = dataColumns.LJCGetUnique(keys);
      // Check Result
      var result = dataColumn?.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Sort on AddOrderIndex.
    private static void UniqueAddOrderIndex()
    {
      var methodName = "LJCSortAddOrderIndex()";

      var dataColumns = new LJCDataColumns
      {
        { "Name", "Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };

      // Test Method
      // Sort on LJCDataColumn.PropertyName.
      var propertyName = LJCDataColumn.ColumnPropertyName;
      var keys = LJC.Keys(propertyName, "");
      dataColumns.LJCSort(keys);
      // Check Result
      var testDataColumn = dataColumns[1];
      var result = testDataColumn.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}1", result, compare);

      // Test Method
      // Sort on LJCDataColumn.PropertyName.
      propertyName = "AddOrderIndex";
      keys = LJC.Keys(propertyName, "");
      dataColumns.LJCSort(keys);
      // Check Result
      testDataColumn = dataColumns[1];
      result = testDataColumn.PropertyName;
      compare = "ID";
      TestCommon?.Show($"{methodName}2", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private static void LJCGetBoolean()
    {
      var methodName = "LJCGetBoolean()";

      var dataColumns = new LJCDataColumns
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", "true", LJC.TypeBoolean },
        { "TestValue2", true, LJC.TypeBoolean },
      };

      // Test Method
      var value = dataColumns.LJCGetBoolean("TestValue");

      // Check Result
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a byte.
    private static void LJCGetByte()
    {
      var methodName = "LJCGetByte()";

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
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a char.
    private static void LJCGetChar()
    {
      var methodName = "LJCGetChar()";

      var test = 'C';
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataColumns.LJCGetChar("TestValue");

      var result = value.ToString();
      var compare = "C";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a DateTime.
    private static void LJCGetDbDateTime()
    {
      var methodName = "LJCGetDbDateTime()";

      var test = new DateTime(2026, 1, 1);
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataColumns.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a decimal.
    private static void LJCGetDecimal()
    {
      var methodName = "LJCGetDecimal()";

      var test = 3.14m;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataColumns.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a double.
    private static void LJCGetDouble()
    {
      var methodName = "LJCGetDouble()";

      var test = 3.14d;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataColumns.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a short int.
    private static void LJCGetInt16()
    {
      var methodName = "LJCGetInt16()";

      var test = (short)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as an int.
    private static void LJCGetInt32()
    {
      var methodName = "LJCGetInt32()";

      var test = 3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a long int.
    private static void LJCGetInt64()
    {
      var methodName = "LJCGetInt64()";

      var test = (long)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as an object.
    private static void LJCGetValue()
    {
      var methodName = "LJCGetValue()";

      var test = (object)3;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Object" },
      };

      // Test Method
      // *** c
      //var value = dataColumns.LJCGetObject("TestValue");
      var value = dataColumns.LJCGetValue("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a single.
    private static void LJCGetSingle()
    {
      var methodName = "LJCGetSingle()";

      var test = 3.14f;
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataColumns.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the string value for the column with the specified name.
    private static void LJCGetString()
    {
      var methodName = "LJCGetString()";

      var test = "3.14";
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      var result = dataColumns.LJCGetString("TestValue");

      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Update column value.
    private static void LJCSetValue()
    {
      var methodName = "LJCSetValue()";

      var test = "3.14";
      var dataColumns = new LJCDataColumns()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      dataColumns.LJCSetValue("TestValue", "3.14159");

      var result = dataColumns.LJCGetString("TestValue");
      var compare = "3.14159";
      TestCommon?.Show($"{methodName}", result, compare);
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
