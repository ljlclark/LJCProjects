// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataValues.cs
using LJCNetCommon;
using System;
using LJC = LJCNetCommon.NetCommon;

namespace TestData
{
  // Provides the LJCDataValues test methods.
  internal class TestDataValues
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataValues()
    {
      TestCommon = new TestCommon("TestDataValues");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataValues ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Static Method Calls

      // Deserializes from the specified XML file.
      LJCDeserialize();
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

      // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
      LJCCreateColumns();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Method Calls

      // Creates item with Value and adds it to the collection.
      Add();

      // Returns the column that matches the key columns.
      LJCGetUnique();

      // Sorts on the current key columns.
      LJCSort();
      #endregion

      #region Value Method Calls

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
      LJCKeyColumns();

      // Returns the item with the supplied property name.
      PropertyNameIndexer();
      #endregion
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      dataValues.LJCSerialize();

      // Test Method
      var newDataValues = LJCDataValues.LJCDeserialize();

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var dataValue = newDataValues?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Test Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataValues = new LJCDataValues();

      var result = $"{dataValues?.Count}";
      var compare = "0";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object from the supplied item.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataValues = new LJCDataValues(dataValues);

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var dataValue = newDataValues?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataValues = dataValues?.Clone();

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var dataValue = newDataValues?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Checks if the collection has items.
    private void HasItems()
    {
      var methodName = "HasItems()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var value = dataValues?.HasItems();

      var result = $"{value}";
      var compare = "True";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a collection of changed columns.
    private void LJCChanged()
    {
      var methodName = "LJCChanged()";

      var dataValues = new LJCDataValues();

      // Test Method
      var changed = dataValues?.LJCChanged();

      var result = $"{changed.Count}";
      var compare = "0";
      TestCommon.Write($"{methodName}1", result, compare);

      dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      changed = dataValues?.LJCChanged();

      result = $"{changed.Count}";
      compare = "2";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Sets the IsChanged value to false for all items.
    private void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
      };
      var changed = dataValues?.LJCChanged();
      var result = $"{changed.Count}";
      var compare = "2";
      TestCommon?.Write($"{methodName}1", result, compare);

      // Test Method
      dataValues.LJCClearChanged();

      changed = dataValues?.LJCChanged();
      result = $"{changed.Count}";
      compare = "0";
      TestCommon?.Write($"{methodName}2", result, compare);
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    private void LJCCreateColumns()
    {
      var methodName = "LJCColumns2()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
      };

      var dataColumns = new LJCDataColumns()
      {
        { "ID", 0, "Int64" },
        { "Name", "", "String", 60 },
      };

      // Test Method
      var newDataColumns = dataValues.LJCCreateColumns(dataColumns);

      // Get where DataColumn property = "PropertyName", value = "Name".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "Name");
      var dataColumn = newDataColumns?.LJCGetUnique(keys);
      var result = $"{dataColumn?.Value}";
      var compare = "NameValue";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Serializes the collection
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      dataValues.LJCSerialize();

      var newDataValues = LJCDataValues.LJCDeserialize();

      // Get where DataColumn property = "PropertyName", value = "Name".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "Name");
      var dataValue = newDataValues?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "string";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Creates item with Value and adds it to the collection.
    private void Add()
    {
      var methodName = "Add()";

      var dataValues = new LJCDataValues();

      // Test Method
      dataValues?.Add("ID", 1, "Int64");

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var dataValue = dataValues?.LJCGetUnique(keys);
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the column that matches the key columns.
    private void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var dataValue = dataValues?.LJCGetUnique(keys);

      var result = $"{dataValue?.Value}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts on the current key columns.
    private void LJCSort()
    {
      var methodName = "LJCSort()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "Name", 1 },
        { "ID", 1, "Int64" },
      };

      // Add the unique compare values.
      // Get where DataColumn property = "DataTypeName", value = "string".
      var keys = LJC.Keys(LJCDataColumn.ColumnDataTypeName, "string");
      dataValues.LJCKeys = keys;

      // Test Method
      dataValues.LJCSort();

      var dataValue = dataValues.LJCGetUnique(keys);
      var result = $"{dataValue?.DataTypeName}";
      var compare = "string";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private void LJCGetBoolean()
    {
      var methodName = "LJCGetBoolean()";

      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", "true", "Boolean" },
      };

      // Test Method
      var value = dataValues.LJCGetBoolean("TestValue");

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
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", testByte, "byte" },
      };

      // Test Method
      var value = dataValues.LJCGetByte("TestValue");

      // Check Result
      var ch = (char)value;
      var result = ch.ToString();
      var compare = "C";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a char.
    private void LJCGetChar()
    {
      var methodName = "LJCGetChar()";

      var test = 'C';
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataValues.LJCGetChar("TestValue");

      var result = $"{value}";
      var compare = "C";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a DateTime.
    private void LJCGetDbDateTime()
    {
      var methodName = "LJCGetDbDateTime()";

      var test = new DateTime(2026, 1, 1);
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataValues.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a decimal.
    private void LJCGetDecimal()
    {
      var methodName = "LJCGetDecimal()";

      var test = 3.14m;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataValues.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a double.
    private void LJCGetDouble()
    {
      var methodName = "LJCGetDouble()";

      var test = 3.14d;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataValues.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a short int.
    private void LJCGetInt16()
    {
      var methodName = "LJCGetInt16()";

      var test = (short)3;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataValues.LJCGetInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as an int.
    private void LJCGetInt32()
    {
      var methodName = "LJCGetInt32()";

      var test = 3;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataValues.LJCGetInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a long int.
    private void LJCGetInt64()
    {
      var methodName = "LJCGetInt64()";

      var test = (long)3;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataValues.LJCGetInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a single.
    private void LJCGetSingle()
    {
      var methodName = "LJCGetSingle()";

      var test = 3.14f;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataValues.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a string.
    private void LJCGetString()
    {
      var methodName = "LJCGetString()";

      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      var result = dataValues.LJCGetString("TestValue");

      var compare = "3.14";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value.
    private void LJCGetValue()
    {
      var methodName = "LJCGetValue()";

      var test = (object)3;
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test, "Object" },
      };

      // Test Method
      var value = dataValues.LJCGetValue("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sets the column object value.
    private void LJCSetValue()
    {
      var methodName = "LJCSetValue()";

      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      dataValues.LJCSetValue("TestValue", "3.14159");

      var result = dataValues.LJCGetString("TestValue");
      var compare = "3.14159";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Returns the item with the supplied property name.
    private void LJCKeyColumns()
    {
      var methodName = "LJCKeyColumns()";

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var keyColumn = keys[0];
      var result = keyColumn.PropertyName;
      result += $", {keyColumn.Value}";
      var compare = "PropertyName, ID";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the item with the supplied property name.
    private void PropertyNameIndexer()
    {
      var methodName = "PropertyNameIndexer()";

      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", test },
      };

      // Test Method
      var dataValue = dataValues["TestValue"];

      var result = $"{dataValue.Value}";
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
