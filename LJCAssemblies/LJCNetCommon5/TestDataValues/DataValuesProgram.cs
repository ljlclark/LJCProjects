// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataValuesProgram.cs
using LJCNetCommon5;
using System.Reflection.Emit;

namespace TestDataValues5
{
  // The entry class.
  internal class DataValuesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataValues");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataValues ***");

      // Static Methods
      LJCDeserialize();

      // Constructor Methods
      CopyConstructor();

      // Collection Methods
      Add();
      Clone();
      HasItems();
      LJCSerialize();

      // Item Methods
      LJCClearChanged();
      LJCCreateColumns();
      LJCGetChanged();

      // Search and Sort Methods
      LJCSearchPropertyName();

      // Value Methods
      LJCGetBoolean();
      LJCGetByte();
      LJCGetBytes();
      LJCGetChar();
      LJCGetDbDateTime();
      LJCGetDecimal();
      LJCGetDouble();
      LJCGetInt16();
      LJCGetInt32();
      LJCGetInt64();
      LJCGetMinSqlDate();
      LJCGetObject();
      LJCGetSingle();
      LJCGetString();
      LJCSetValue();
    }

    #region Static Methods

    // Deserializes from the specified XML file.
    private static void LJCDeserialize()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };
      dataValues.LJCSerialize();

      // Test Method
      var newDataValues = LJCDataValues.LJCDeserialize();

      var dataValue = newDataValues?.LJCSearchPropertyName("ID");
      var result = dataValue?.PropertyName;
      var compare = "ID";
      TestCommon?.Write("LJCDeserialize()", result, compare);
    }
    #endregion

    #region Constructor Methods

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataValues = new LJCDataValues(dataValues);

      var dataValue = newDataValues?.LJCSearchPropertyName("ID");
      var result = dataValue?.PropertyName;
      var compare = "ID";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add()
    {
      var dataValues = new LJCDataValues();

      // Test Method
      dataValues?.Add("ID", 1, "Int64");

      var dataValue = dataValues?.LJCSearchPropertyName("ID");
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon?.Write("Add()", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var newDataValues = dataValues?.Clone();

      var dataValue = newDataValues?.LJCSearchPropertyName("ID");
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      var value = dataValues.HasItems();
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("HasItems()", result, compare);
    }

    // Serializes the collection
    private static void LJCSerialize()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", 1 },
      };

      // Test Method
      dataValues.LJCSerialize();

      var newDataValues = LJCDataValues.LJCDeserialize();
      var dataValue = newDataValues?.LJCSearchPropertyName("ID");
      var result = dataValue?.DataTypeName;
      var compare = "Int64";
      TestCommon?.Write("LJCSerialize()", result, compare);
    }
    #endregion

    #region Item Methods

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
      };
      var changed = dataValues.LJCGetChanged();
      var result = changed.Count.ToString();
      var compare = "0";
      TestCommon?.Write("LJCClearChanged1()", result, compare);

      dataValues = new LJCDataValues();
      dataValues?.Add("ID", 1, "Int64");
      dataValues?.Add("Name", "NameValue");
      changed = dataValues?.LJCGetChanged();
      result = changed?.Count.ToString();
      compare = "0";
      TestCommon?.Write("LJCClearChanged2()", result, compare);

      // Test Method
      dataValues?.LJCClearChanged();
      result = changed?.Count.ToString();
      compare = "0";
      TestCommon?.Write("LJCClearChanged3()", result, compare);
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    private static void LJCCreateColumns()
    {
      var dataValues = new LJCDataValues()
      {
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

      var dataColumn = newDataColumns?.LJCGetColumn("Name");
      var result = dataColumn?.Value?.ToString();
      var compare = "NameValue";
      TestCommon?.Write("LJCCreateColumns()", result, compare);
    }

    // Gets a collection of changed columns.
    private static void LJCGetChanged()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetChanged()", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    private static void LJCSearchPropertyName()
    {
      var dataValues = new LJCDataValues()
      {
        { "ID", 1, "Int64" },
        { "Name", "NameValue" },
      };

      // Test Method
      var dataValue = dataValues.LJCSearchPropertyName("Name");

      var result = dataValue?.Value?.ToString();
      var compare = "NameValue";
      TestCommon?.Write("LJCSearchPropertyName()", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private static void LJCGetBoolean()
    {
      var dataValues = new LJCDataValues()
      {
        { "TestValue", "true", "Boolean" },
      };

      // Test Method
      var value = dataValues.LJCGetBoolean("TestValue");

      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("LJCGetBoolean()", result, compare);
    }

    // Gets the column object value as a byte.
    private static void LJCGetByte()
    {
      var test = "C";
      var testBytes = LJC.TextToBytes(test);
      var dataValues = new LJCDataValues()
      {
        { "TestValue", testBytes, "byte" },
      };

      // Test Method
      var value = dataValues.LJCGetByte("TestValue");

      var bytes = new byte[] { value };
      var result = LJC.BytesToText(bytes);
      var compare = "C";
      TestCommon?.Write("LJCGetBytes()", result, compare);
    }

    private static void LJCGetBytes()
    {
      var test = "Check";
      var testBytes = LJC.TextToBytes(test);
      var dataValues = new LJCDataValues()
      {
        { "TestValue", testBytes, "byte[]" },
      };

      // Test Method
      var value = dataValues.LJCGetBytes("TestValue");

      var result = LJC.BytesToText(value);
      var compare = "Check";
      TestCommon?.Write("LJCGetBytes()", result, compare);
    }

    // Gets the column object value as a char.
    private static void LJCGetChar()
    {
      var test = 'C';
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "char" },
      };

      // Test Method
      var value = dataValues.LJCGetChar("TestValue");

      var result = value.ToString();
      var compare = "C";
      TestCommon?.Write("LJCGetChar()", result, compare);
    }

    // Gets the column object value as a DateTime.
    private static void LJCGetDbDateTime()
    {
      var test = new DateTime(2026, 1, 1);
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataValues.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/2026";
      TestCommon?.Write("LJCGetDbDateTime()", result, compare);
    }

    // Gets the column object value as a decimal.
    private static void LJCGetDecimal()
    {
      var test = 3.14m;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Decimal" },
      };

      // Test Method
      var value = dataValues.LJCGetDecimal("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetDecimal()", result, compare);
    }

    // Gets the column object value as a double.
    private static void LJCGetDouble()
    {
      var test = 3.14d;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Double" },
      };

      // Test Method
      var value = dataValues.LJCGetDouble("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetDouble()", result, compare);
    }

    // Gets the column object value as a short int.
    private static void LJCGetInt16()
    {
      var test = (short)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int16" },
      };

      // Test Method
      var value = dataValues.LJCGetInt16("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt16()", result, compare);
    }

    // Gets the column object value as an int.
    private static void LJCGetInt32()
    {
      var test = 3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int32" },
      };

      // Test Method
      var value = dataValues.LJCGetInt32("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt32()", result, compare);
    }

    // Gets the column object value as a long int.
    private static void LJCGetInt64()
    {
      var test = (long)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Int64" },
      };

      // Test Method
      var value = dataValues.LJCGetInt64("TestValue");

      var result = value.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetInt64()", result, compare);
    }

    // Get the minimum date value.
    private static void LJCGetMinSqlDate()
    {
      var test = new DateTime(1753, 1, 1);
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "DateTime" },
      };

      // Test Method
      var value = dataValues.LJCGetDbDateTime("TestValue");

      var result = value.ToShortDateString();
      var compare = "1/1/1753";
      TestCommon?.Write("LJCGetMinSqlDate()", result, compare);
    }

    // Gets the column object value as an object.
    private static void LJCGetObject()
    {
      var test = (object)3;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Object" },
      };

      // Test Method
      var value = dataValues.LJCGetObject("TestValue");

      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Write("LJCGetObject()", result, compare);
    }

    // Gets the column object value as a single.
    private static void LJCGetSingle()
    {
      var test = 3.14f;
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test, "Single" },
      };

      // Test Method
      var value = dataValues.LJCGetSingle("TestValue");

      var result = value.ToString();
      var compare = "3.14";
      TestCommon?.Write("LJCGetSingle()", result, compare);
    }

    // Gets the string value for the column with the specified name.
    private static void LJCGetString()
    {
      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test },
      };

      // Test Method
      var result = dataValues.LJCGetString("TestValue");

      var compare = "3.14";
      TestCommon?.Write("LJCGetString()", result, compare);
    }

    // Sets the object value for the column with the specified name.
    private static void LJCSetValue()
    {
      var test = "3.14";
      var dataValues = new LJCDataValues()
      {
        { "TestValue", test },
      };

      // Test Method
      dataValues.LJCSetValue("TestValue", "3.14159");

      var result = dataValues.LJCGetString("TestValue");
      var compare = "3.14159";
      TestCommon?.Write("LJCSetValue()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
