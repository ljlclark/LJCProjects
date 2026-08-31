// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataValues.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // Provides the LJCDataValues test methods.
  internal class TestDataValues
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataValues()
    {
      TestCommon = new LJCTestCommon("TestDataValues");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataValues ***");

      //TestCommon.ShowNotImplemented = false;
      Run();
    }

    // Runs the test methods.
    private static void Run()
    {
      #region Static Methods

      // Deserializes from the specified XML file.
      LJCDeserialize();
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

      // Gets a list of property names from the unique keys.
      LJCKeyPropertyNames();

      // Get the list of property names.
      LJCPropertyNames();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Methods

      // Creates item with Position and MaxLength and adds it to the collection.
      Add();

      // Gets the column that matches the key columns.
      // The column is identified by its property names and values.
      LJCGetUnique();

      // Sorts on the current key columns.
      LJCSort();
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

      // Gets the column object value as a single.
      LJCGetSingle();

      // Gets the string value for the column with the specified name.
      LJCGetString();

      // Gets the column object value as an object.
      LJCGetValue();

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

      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", "Name Value" },
      };
      dataValues.LJCSerialize();

      // Test Method
      var newDataValues = LJCDataValues.LJCDeserialize();
      // Check Result
      var result = "";
      if (newDataValues != null)
      {
        var dataColumn = newDataValues["ID"];
        result = dataColumn?.PropertyName;
      }
      var compare = "ID";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dataValues = new LJCDataValues
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var newDataColumns = dataValues.Clone();
      // Check Result
      var value = newDataColumns[1];
      var result = value.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems()
    {
      var dataValues = new LJCDataValues
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var value = dataValues.HasItems();
      // Check Result
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Show("HasItems()", result, compare);
    }

    // Gets a collection of changed columns.
    private static void LJCChanged()
    {
      var methodName = "LJCChanged()";

      var dataValues = new LJCDataValues
      {
        { "ID", 1 },
        { "Name", "Name Value" },
      };
      // ToDo: This should not be necessary.
      dataValues.LJCClearChanged();

      // Test Method
      var testDataColumns = dataValues.LJCChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Show($"{methodName}1", result, compare);

      dataValues.LJCSetValue("Name", "Updated");
      testDataColumns = dataValues.LJCChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var dataValues = new LJCDataValues
      {
        { "ID", 1 },
        { "Name", (object)"Name Value" },
      };
      // ToDo: This should not be necessary.
      dataValues.LJCClearChanged();

      // Test Method
      var testDataColumns = dataValues.LJCChanged();
      var result = testDataColumns.Count.ToString();
      var compare = "0";
      TestCommon?.Show($"{methodName}1", result, compare);

      dataValues.LJCSetValue("Name", "Updated");
      testDataColumns = dataValues.LJCChanged();
      var dataColumn = testDataColumns[0];
      result = dataColumn.Value?.ToString();
      compare = "Updated";
      TestCommon?.Show($"{methodName}2", result, compare);

      // Test Method
      dataValues.LJCClearChanged();
      testDataColumns = dataValues.LJCChanged();
      result = testDataColumns.Count.ToString();
      compare = "0";
      TestCommon?.Show($"{methodName}3", result, compare);
    }

    // Gets a list of property names from the unique keys.
    private static void LJCKeyPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var dataValues = new LJCDataValues()
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };
      var keys = new LJCDataColumns()
      {
        { "ParentID", 1 },
        { "Name", "Name Value" },
      };
      dataValues.LJCKeys = keys;

      // Test Method
      var names = dataValues.LJCKeyPropertyNames();
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

      var dataValues = new LJCDataValues
      {
        { "ID", 1, LJC.TypeInt64 },
        { "Name", "Name Value" },
      };

      // Test Method
      var names = dataValues.LJCPropertyNames();
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

      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      dataValues.LJCSerialize();
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

    // Creates the Object from the arguments and adds it to the collection. (R)
    private static void Add()
    {
      var methodName = "Add()";

      var dataValues = new LJCDataValues();

      // Test Method
      var dataColumn = dataValues.Add("ID", dataTypeName: LJC.TypeInt64);
      // Check Result
      var result = dataColumn.DataTypeName;
      var compare = "Int64";
      TestCommon?.Show($"{methodName}1", result, compare);

      // Test Method
      dataValues.Add("Name");
      // Check Result
      var value = dataValues[1];
      result = value.DataTypeName;
      compare = "String";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Gets the column that matches the key columns.
    // The column is identified by its property names and values.
    private static void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      var keys = new LJCDataValues()
      {
        { "PropertyName", "Name" },
      };
      var dataValues = new LJCDataValues()
      {
        { "Name", "Name Value" },
        { "Value", 1 },
      };
      dataValues.LJCKeys = keys;

      // Test Method
      var dataValue = dataValues.LJCGetUnique();
      // Check Result
      var result = "";
      if (dataValue != null)
      {
        result = $"{dataValue.PropertyName}";
      }
      var compare = "Name";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Sorts on the current key columns.
    private static void LJCSort()
    {
      var methodName = "LJCSort()";

      var dataValues = new LJCDataValues
      {
        { "Name", "Name Value" },
        { "ID", 1, LJC.TypeInt64 },
      };

      // Test Method
      // Sort on LJCDataColumn.PropertyName.
      var propertyName = LJCDataValue.ColumnPropertyName;
      var keys = LJC.Keys(propertyName, "");
      dataValues.LJCSort(keys);
      // Check Result
      var testDataColumn = dataValues[1];
      var result = testDataColumn.PropertyName;
      var compare = "Name";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private static void LJCGetBoolean()
    {
      var methodName = "LJCGetBoolean()";

      var dataValues = new LJCDataValues
      {
        // PropertyName, Value, DataTypeName
        { "TestValue", "true", LJC.TypeBoolean },
        { "TestValue2", true, LJC.TypeBoolean },
      };

      // Test Method
      var value = dataValues.LJCBoolean("TestValue");

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
      var dataValues = new LJCDataValues()
      {
        { "TestValue", testBytes, "byte" },
      };
      // Test Method
      var value = dataValues.LJCGetByte("TestValue");
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
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataValues.LJCGetChar("TestValue");

      var result = value.ToString();
      var compare = "C";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a DateTime.
    private static void LJCGetDbDateTime()
    {
      var methodName = "LJCGetDbDateTime()";

      var test = new DateTime(2026, 1, 1);
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataValues.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a decimal.
    private static void LJCGetDecimal()
    {
      var methodName = "LJCGetDecimal()";

      var test = 3.14m;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataValues.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a double.
    private static void LJCGetDouble()
    {
      var methodName = "LJCGetDouble()";

      var test = 3.14d;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataValues.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a short int.
    private static void LJCGetInt16()
    {
      var methodName = "LJCGetInt16()";

      var test = (short)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataValues.LJCInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as an int.
    private static void LJCGetInt32()
    {
      var methodName = "LJCGetInt32()";

      var test = 3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataValues.LJCInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a long int.
    private static void LJCGetInt64()
    {
      var methodName = "LJCGetInt64()";

      var test = (long)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataValues.LJCInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as a single.
    private static void LJCGetSingle()
    {
      var methodName = "LJCGetSingle()";

      var test = 3.14f;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataValues.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the string value for the column with the specified name.
    private static void LJCGetString()
    {
      var methodName = "LJCGetString()";

      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      var result = dataValues.LJCString("TestValue");

      var compare = "3.14";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets the column object value as an object.
    private static void LJCGetValue()
    {
      var methodName = "LJCGetValue()";

      var test = (object)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Object" },
      };

      // Test Method
      // *** c
      //var value = dataColumns.LJCGetObject("TestValue");
      var value = dataValues.LJCValue("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Update column value.
    private static void LJCSetValue()
    {
      var methodName = "LJCSetValue()";

      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        { "TestValue", (object)test },
      };

      // Test Method
      dataValues.LJCSetValue("TestValue", "3.14159");

      var result = dataValues.LJCString("TestValue");
      var compare = "3.14159";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
