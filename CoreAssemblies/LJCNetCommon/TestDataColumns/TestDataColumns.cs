// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumns.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using LJC = LJCNetCommon.NetCommon;

namespace TestData
{
  // Provides the LJCDataColumns test methods.
  internal class TestDataColumns
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumns()
    {
      TestCommon = new TestCommon("TestDataColumns");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataColumns ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Static Method Calls

      // Deserializes from the specified XML file.
      LJCDeserialize();

      // Gets a collection of items from a data object.
      LJCObjectColumns();

      // Gets a collection of items from a data object that match the supplied
      // property Names.
      LJCObjectColumnsInList();

      // Gets a list of property names from a data object.
      LJCObjectPropertyNames();

      // Operator to create LJCDataValues from LJCDataColumns.
      ToDataValues();
      #endregion

      #region Constructor Method Calls

      // Initializes an object instance.
      Constructor();

      // Initializes an object from the supplied items.
      CopyConstructor();
      #endregion

      #region Collection Method Calls

      // Creates and returns a clone of the object.
      Clone();

      // Checks if the collection has items.
      HasItems();

      // Gets a collection of changed columns.
      LJCChanged();

      // Sets the IsChanged value to false for all items.
      LJCClearChanged();

      // Returns a collection of items that match a list of property names.
      LJCColumns();

      // Gets a list of property names from the collection items.
      LJCKeys();

      // Gets a list of property names from the collection items.
      LJCKeyPropertyNames();

      // Gets a list of property names from the collection items.
      LJCPropertyNames();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Method Calls

      // Adds the supplied item to the collection
      Add1();

      // Creates item with Position and MaxLength and adds it to the collection.
      Add2();

      // Creates item with Value and adds it to the collection.
      Add3();

      // Returns the column that matches the key columns.
      LJCGetUnique();

      // Removes the item with the supplied property name.
      LJCRemove();

      // Add or Update.
      LJCSetData();

      // Sorts on the current key columns.
      LJCSort();
      #endregion

      #region Other Public Method Calls

      // Sets the caption properties.
      LJCSetCaptions();

      // Maps the column property and rename values.
      LJCMapNames();
      #endregion

      #region Value Method Calls

      // Gets the column object value as a bool.
      LJCGetBoolean();

      // Gets the column object value as a byte.
      LJCGetByte();

      // Gets the column object value as a byte array.
      LJCGetBytes();

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

      // Gets the column object value as a single.
      LJCGetSingle();

      // Gets the column object value as a string.
      LJCGetString();

      // Gets the column object value.
      LJCGetValue();

      // Sets the column object value.
      LJCSetValue();
      #endregion

      #region Property Calls

      // Gets or sets the key columns.
      LJCKeys();

      // Returns the item with the supplied property name.
      Indexer();
      #endregion
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      dataColumns.LJCSerialize();

      // Test Method
      var newDataColumns = LJCDataValues.LJCDeserialize();

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var dataColumn = newDataColumns?.LJCGetUnique(keys);
      var result = dataColumn?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a collection of items from a data object.
    private void LJCObjectColumns()
    {
      var methodName = "LJCObjectColumns()";

      var dataColumn = new LJCDataColumn("Name");

      // Test Method
      var dataColumns = LJCDataColumns.LJCObjectColumns(dataColumn);

      // Get where DataColumn property = "PropertyName", value = "PropertyName".
      var keys = LJC.Keys("PropertyName", "PropertyName");
      var foundDataValue = dataColumns?.LJCGetUnique(keys);
      var result = foundDataValue?.PropertyName;
      var compare = "PropertyName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns a collection of items from the data object properties.
    private void LJCObjectColumnsInList()
    {
      var methodName = "LJCColumns2()";

      var dataColumn = new LJCDataColumn("Name", "NameValue");

      var propertyNames = new List<string>()
      {
        "PropertyName",
        "Value",
      };

      // Test Method
      var dataColumns = LJCDataColumns.LJCObjectColumnsInList(dataColumn, propertyNames);

      dataColumn = dataColumns[1];
      var result = $"{dataColumn.Value}";
      var compare = "NameValue";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a list of property names from a data object.
    private void LJCObjectPropertyNames()
    {
      var methodName = "LJCObjectPropertyNames()";

      var dataColumn = new LJCDataColumn("Name");

      // Test Method
      var columnList = LJCDataColumns.LJCObjectPropertyNames(dataColumn);

      var result = columnList[0];
      var compare = "AllowDBNull";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Operator to creates LJCDataValues from LJCDataColumns.
    private void ToDataValues()
    {
      var methodName = "DataColumnsToDataValues()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var dataValues = dataColumns;

      var dataValue = dataValues[1];
      var result = dataValue.PropertyName;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Test Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataColumns = new LJCDataColumns();

      var dataColumn = dataColumns.Add("PropertyName");
      dataColumn.ColumnName = "ColumnName";
      var result = dataColumn.PropertyName;
      result += $", {dataColumn.ColumnName}";
      var compare = "PropertyName, ColumnName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object from the supplied item.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataColumns = new LJCDataValues(dataColumns);

      var dataColumn = newDataColumns[0];
      var result = dataColumn.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataColumns = dataColumns?.Clone();

      var dataColumn = newDataColumns[0];
      var result = dataColumn.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Checks if the collection has items.
    private void HasItems()
    {
      var methodName = "HasItems()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var value = dataColumns?.HasItems();

      var result = $"{value}";
      var compare = "True";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a collection of changed columns.
    private void LJCChanged()
    {
      var methodName = "LJCChanged()";

      var dataColumns = new LJCDataValues();

      // Test Method
      var changed = dataColumns?.LJCChanged();

      var result = $"{changed.Count}";
      var compare = "0";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      changed = dataColumns?.LJCChanged();

      result = $"{changed.Count}";
      compare = "2";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Sets the IsChanged value to false for all items.
    private void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
      };
      var changed = dataColumns?.LJCChanged();
      var result = $"{changed.Count}";
      var compare = "2";
      TestCommon?.Write($"{methodName}1", result, compare);

      // Test Method
      dataColumns.LJCClearChanged();

      changed = dataColumns?.LJCChanged();
      result = $"{changed.Count}";
      compare = "0";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Returns a collection of items that match a list of property names.
    private void LJCColumns()
    {
      var methodName = "LJCColumns1()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      var propertyNames = new List<string>()
      {
        "Name",
        "Description",
      };

      // Test Method
      var newDataColumns = dataColumns.LJCColumns(propertyNames);

      var dataColumn = newDataColumns[1];
      var result = dataColumn.PropertyName;
      var compare = "Description";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a list of property names from the collection items.
    private void LJCKeyPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };
      var dataColumns = new LJCDataColumns
      {
        LJCKeys = dataValues
      };

      // Test Method
      var propertyNames = dataColumns.LJCKeyPropertyNames();

      var result = propertyNames[1];
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a list of property names from the collection items.
    private void LJCPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      // Test Method
      var propertyNames = dataColumns.LJCPropertyNames();

      var result = propertyNames[1];
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Serializes the collection
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      dataColumns.LJCSerialize();

      var newDataColumns = LJCDataValues.LJCDeserialize();

      // Get where DataColumn property = "PropertyName", value = "Name".
      var keys = LJC.Keys("PropertyName", "Name");
      var dataColumn = newDataColumns?.LJCGetUnique(keys);
      var result = dataColumn?.DataTypeName;
      var compare = "string";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Adds the supplied item to the collection
    private void Add1()
    {
      var methodName = "Add1()";

      var dataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("ID", "1", "Int64");

      // Test Method
      dataColumns?.Add(dataColumn);

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var dataValue = dataColumns?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with Position and MaxLength and adds it to the collection.
    private void Add2()
    {
      var methodName = "Add2()";

      var dataColumns = new LJCDataColumns();

      // Test Method
      var dataColumn = dataColumns?.Add("ID", 1, 6);
      dataColumn.DataTypeName = "Int64";

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var dataValue = dataColumns?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with Value and adds it to the collection.
    private void Add3()
    {
      var methodName = "Add3()";

      var dataColumns = new LJCDataColumns();

      // Test Method
      dataColumns?.Add("ID", 1, "Int64");

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var dataValue = dataColumns?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the column that matches the key columns.
    private void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var dataColumn = dataColumns?.LJCGetUnique(keys);

      var result = $"{dataColumn?.Value}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Removes the item with the supplied property name.
    private void LJCRemove()
    {
      var methodName = "LJCRemove()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      // Test Method
      dataColumns.LJCRemove("Name");

      //var dataColumn = dataColumns[1];
      var dataColumn = dataColumns["Description"];
      var result = dataColumn.PropertyName;
      var compare = "Description";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Add or Update.
    private void LJCSetData()
    {
      var methodName = "LJCSetData()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      // Test Method
      var dataColumn = new LJCDataColumn("Name", "NameUpdated");
      dataColumns.LJCSetData(dataColumn);

      // Get where DataColumn property = "PropertyName", value = "Name".
      var keys = LJC.Keys("PropertyName", "Name");
      var foundDataColumn = dataColumns?.LJCGetUnique(keys);
      var result = $"{foundDataColumn.Value}";
      var compare = "NameUpdated";
      TestCommon.Write($"{methodName}", result, compare);

      // Test Method
      dataColumn = new LJCDataColumn("Sequence", "1");
      dataColumns.LJCSetData(dataColumn);

      // Get where DataColumn property = "PropertyName", value = "Sequence".
      keys = LJC.Keys("PropertyName", "Sequence");
      foundDataColumn = dataColumns?.LJCGetUnique(keys);
      result = $"{foundDataColumn.Value}";
      compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts on the current key columns.
    private void LJCSort()
    {
      var methodName = "LJCSort()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "Name", 1 },
        { "ID", 1, "Int64" },
      };

      // Add the unique compare values.
      // Get where DataColumn property = "DataTypeName", value = "string".
      var keys = LJC.Keys("DataTypeName", "string");
      dataColumns.LJCKeys = keys;

      // Test Method
      dataColumns.LJCSort();

      var dataColumn = dataColumns.LJCGetUnique(keys);
      var result = $"{dataColumn?.DataTypeName}";
      var compare = "string";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Other Public Methods

    // Sets the caption properties.
    private void LJCSetCaptions()
    {
      var methodName = "LJCSetColumnCaptions()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      var newDataColumns = new LJCDataColumns();
      newDataColumns.Add("ID", "1", "Int64", caption: "ID Caption");
      newDataColumns.Add("Name", "NameValue", caption: "Name Caption");
      newDataColumns.Add("Description", "DescriptionValue"
        , caption: "Description Caption");

      // Test Method
      newDataColumns.LJCSetCaptions(dataColumns);

      // Get where DataColumn property = "PropertyName", value = "Name".
      var keys = LJC.Keys("PropertyName", "Name");
      var foundDataColumn = dataColumns?.LJCGetUnique(keys);
      var result = foundDataColumn.Caption;
      var compare = "Name Caption";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Maps the column property and rename values.
    private void LJCMapNames()
    {
      var methodName = "LJCMapNames()";

      var dataColumns = new LJCDataColumns()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
        { "Description", "DescriptionValue" },
      };

      // Test Method
      // ColumnName, PropertyName, RenameAs, Caption.
      dataColumns.LJCMapNames("Name", "NewName", "RenameName", "New Caption");

      // Get where DataColumn property = "PropertyName", value = "NewName".
      var keys = LJC.Keys("PropertyName", "NewName");
      var foundDataColumn = dataColumns?.LJCGetUnique(keys);
      var result = $"{foundDataColumn.Value}";
      result += $", {foundDataColumn.RenameAs}";
      result += $", {foundDataColumn.Caption}";
      var compare = "NameValue, RenameName, New Caption";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private void LJCGetBoolean()
    {
      var methodName = "LJCGetBoolean()";

      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", "true", "Boolean" },
      };

      // Test Method
      var value = dataColumns.LJCGetBoolean("TestValue");

      // Check Result
      var result = value.ToString();
      var compare = "True";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a byte.
    private void LJCGetByte()
    {
      var methodName = "LJCGetByte()";

      var testByte = (byte)'C';
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", testByte, "byte" },
      };

      // Test Method
      var value = dataColumns.LJCGetByte("TestValue");

      // Check Result
      var ch = (char)value;
      var result = ch.ToString();
      var compare = "C";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a byte array.
    private void LJCGetBytes()
    {
      var methodName = "LJCGetByte()";

      var text = "C";
      var testBytes = LJC.TextToBytes(text);
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", testBytes, "byte[]" },
      };

      // Test Method
      var value = dataColumns.LJCGetBytes("TestValue");

      // Check Result
      var result = LJC.BytesToText(value);
      var compare = "C";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a char.
    private void LJCGetChar()
    {
      var methodName = "LJCGetChar()";

      var test = 'C';
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataColumns.LJCGetChar("TestValue");

      var result = $"{value}";
      var compare = "C";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a DateTime.
    private void LJCGetDbDateTime()
    {
      var methodName = "LJCGetDbDateTime()";

      var test = new DateTime(2026, 1, 1);
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataColumns.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a decimal.
    private void LJCGetDecimal()
    {
      var methodName = "LJCGetDecimal()";

      var test = 3.14m;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataColumns.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a double.
    private void LJCGetDouble()
    {
      var methodName = "LJCGetDouble()";

      var test = 3.14d;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataColumns.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a short int.
    private void LJCGetInt16()
    {
      var methodName = "LJCGetInt16()";

      var test = (short)3;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as an int.
    private void LJCGetInt32()
    {
      var methodName = "LJCGetInt32()";

      var test = 3;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a long int.
    private void LJCGetInt64()
    {
      var methodName = "LJCGetInt64()";

      var test = (long)3;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataColumns.LJCGetInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a single.
    private void LJCGetSingle()
    {
      var methodName = "LJCGetSingle()";

      var test = 3.14f;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataColumns.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a string.
    private void LJCGetString()
    {
      var methodName = "LJCGetString()";

      var test = "3.14";
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      var result = dataColumns.LJCGetString("TestValue");

      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value.
    private void LJCGetValue()
    {
      var methodName = "LJCGetValue()";

      var test = (object)3;
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Object" },
      };

      // Test Method
      var value = dataColumns.LJCGetValue("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sets the column object value.
    private void LJCSetValue()
    {
      var methodName = "LJCSetValue()";

      var test = "3.14";
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      dataColumns.LJCSetValue("TestValue", "3.14159");

      var result = dataColumns.LJCGetString("TestValue");
      var compare = "3.14159";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Returns the item with the supplied property name.
    private void LJCKeys()
    {
      var methodName = "LJCKeyColumns()";

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys("PropertyName", "ID");
      var key = keys[0];
      var result = key.PropertyName;
      result += $", {key.Value}";
      var compare = "PropertyName, ID";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the item with the supplied property name.
    private void Indexer()
    {
      var methodName = "PropertyNameIndexer()";

      var test = "3.14";
      var dataColumns = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      var dataColumn = dataColumns["TestValue"];

      var result = $"{dataColumn.Value}";
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
