// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataValueProgram.cs
using LJCNetCommon5;

namespace TestDataValue5
{
  // The entry class.
  internal class DataValueProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataValue");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataValue ***");

      // Constructor Methods
      CopyConstructor();
      ParmConstructor();

      // Data Methods
      Clone();
      CompareTo();
      FormatValue();
      ToStringMethod();

      // Conversions
      CreateColumn();

      // Data Properties
      Value();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var dataValue = new LJCDataValue()
      {
        DataTypeName = "String",
        IsChanged = false,
        PropertyName = "TestValue",
        Value = 3
      };
      // Test Method
      var newDataValue = new LJCDataValue(dataValue);
      var result = newDataValue.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }

    // Initializes an object instance with the supplied values.
    private static void ParmConstructor()
    {
      // Test Method
      var dataValue = new LJCDataValue("TestValue", 3);
      var result = dataValue.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("ParmConstructor()", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Method
      var newDataValue = dataValue.Clone();
      var result = newDataValue?.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var compareObject = new LJCDataValue("Alpha", 3);
      var compareToObject = new LJCDataValue("Beta", 3);
      // Test Method
      var value = compareObject.CompareTo(null);
      var result = value.ToString();
      var compare = "1";
      TestCommon?.Write("CompareTo()1", result, compare);

      // Test Method
      value = compareObject.CompareTo(compareToObject);
      result = value.ToString();
      compare = "-1";
      TestCommon?.Write("CompareTo()2", result, compare);

      // Test Method
      value = compareObject.CompareTo(compareObject);
      result = value.ToString();
      compare = "0";
      TestCommon?.Write("CompareTo()3", result, compare);

      // Test Method
      value = compareToObject.CompareTo(compareObject);
      result = value.ToString();
      compare = "1";
      TestCommon?.Write("CompareTo()4", result, compare);
    }

    // Formats the column value for the SQL string.
    private static void FormatValue()
    {
      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Method
      var result = dataValue.FormatValue();
      var compare = "'3'";
      TestCommon?.Write("FormatValue()1", result, compare);

      dataValue = new LJCDataValue("TestValue", "O'Brian");
      // Test Method
      result = dataValue.FormatValue();
      compare = "'O''Brian'";
      TestCommon?.Write("FormatValue()2", result, compare);

      dataValue = new LJCDataValue("TestValue", "true", LJC.TypeBoolean);
      // Test Method
      result = dataValue.FormatValue();
      compare = "1";
      TestCommon?.Write("FormatValue()3", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      dataValue = new LJCDataValue("TestValue", dateTime, LJC.TypeDateTime);
      // Test Method
      result = dataValue.FormatValue();
      compare = "'2026/01/01 00:00:00'";
      TestCommon?.Write("FormatValue()4", result, compare);
    }

    // The object string identifier.
    private static void ToStringMethod()
    {
      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Method
      var result = dataValue.ToString();
      var compare = "TestValue:3";
      TestCommon?.Write("ToStringMethod()1", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      dataValue = new LJCDataValue("TestValue", dateTime, LJC.TypeDateTime);
      // Test Method
      result = dataValue.ToString();
      compare = "TestValue:1/1/2026 12:00:00 AM";
      TestCommon?.Write("ToStringMethod()2", result, compare);
    }
    #endregion

    #region Conversions

    // Creates a combined LJCDataColumn from an LJCDataValue and LJCDataColumn.
    private static void CreateColumn()
    {
      var dataValue = new LJCDataValue("TestValue", 3);
      var dataColumn = new LJCDataColumn("TestValue");
      // Test Method
      var newDataColumn = dataValue.CreateColumn(dataColumn);
      var value = newDataColumn?.Value;
      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Write("CreateColumn()1", result, compare);

      dataColumn = new LJCDataColumn("Test");
      // Test Method
      newDataColumn = dataValue.CreateColumn(dataColumn);
      value = newDataColumn?.Value;
      result = value?.ToString();
      compare = "No Result";
      TestCommon?.Write("CreateColumn()2", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the Value object.
    private static void Value()
    {
      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Value
      var value = dataValue.Value;
      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Write("Value()1", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("Value()2", result, compare);

      // Test Value
      dataValue.Value = 3;
      value = dataValue.Value;
      result = value.ToString();
      compare = "3";
      TestCommon?.Write("Value()3", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("Value()4", result, compare);

      // Test Value
      dataValue.Value = 4;
      value = dataValue.Value;
      result = value.ToString();
      compare = "4";
      TestCommon?.Write("Value()5", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("Value()6", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
