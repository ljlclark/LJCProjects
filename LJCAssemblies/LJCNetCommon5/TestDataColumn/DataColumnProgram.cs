// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnProgram.cs
using LJCNetCommon5;

namespace TestDataColumn5
{
  // The entry class.
  internal class DataColumnProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataColumn");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataColumn ***");

      // Constructor Methods
      CopyConstructor();
      ParmConstructor();

      // Data Class Methods
      Clone();
      CompareTo();
      FormatValue();
      ToStringMethod();

      // Conversions
      CreateValue();
    }

    #region Constructor Methods

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var dataColumn = new LJCDataColumn()
      {
        DataTypeName = "String",
        IsChanged = false,
        PropertyName = "TestValue",
        Value = 3
      };
      // Test Method
      var newDataColumn = new LJCDataValue(dataColumn);
      var result = newDataColumn.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }

    // Initializes an object instance with the supplied values.
    private static void ParmConstructor()
    {
      // Test Method
      var dataColumn = new LJCDataColumn("TestValue");
      dataColumn.Value = 3;
      var result = dataColumn.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("ParmConstructor()", result, compare);
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var dataColumn = new LJCDataColumn("TestValue");
      dataColumn.Value = 3;
      // Test Method
      var newDataColumn = dataColumn.Clone();
      var result = newDataColumn?.PropertyName;
      var compare = "TestValue";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var dataColumns = new LJCDataColumns();
      var compareObject = new LJCDataColumn("Alpha");
      dataColumns.Add(compareObject);
      var compareToObject = new LJCDataColumn("Beta");
      dataColumns.Add(compareToObject);
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
      var dataColumn = new LJCDataColumn("TestValue");
      dataColumn.Value = 3;
      // Test Method
      var result = dataColumn.FormatValue();
      var compare = "'3'";
      TestCommon?.Write("FormatValue()1", result, compare);

      dataColumn = new LJCDataColumn("TestValue", "O'Brian");
      // Test Method
      result = dataColumn.FormatValue();
      compare = "'O''Brian'";
      TestCommon?.Write("FormatValue()2", result, compare);

      dataColumn = new LJCDataColumn("TestValue", "true", LJC.TypeBoolean);
      // Test Method
      result = dataColumn.FormatValue();
      compare = "1";
      TestCommon?.Write("FormatValue()3", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataColumn = new LJCDataColumn("TestValue", dateTimeString, LJC.TypeDateTime);
      // Test Method
      result = dataColumn.FormatValue();
      compare = "'2026/01/01 00:00:00'";
      TestCommon?.Write("FormatValue()4", result, compare);
    }

    // The object string identifier.
    private static void ToStringMethod()
    {
      var dataColumn = new LJCDataColumn("TestValue");
      dataColumn.Value = 3;
      // Test Method
      var result = dataColumn.ToString();
      var compare = "TestValue:3";
      TestCommon?.Write("ToStringMethod()1", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataColumn = new LJCDataColumn("TestValue", dateTimeString, LJC.TypeDateTime);
      // Test Method
      result = dataColumn.ToString();
      compare = "TestValue:1/1/2026 12:00:00 AM";
      TestCommon?.Write("ToStringMethod()2", result, compare);
    }
    #endregion

    #region Conversions

    // Creates a combined LJCDataColumn from an LJCDataValue and LJCDataColumn.
    private static void CreateValue()
    {
      var dataColumn = new LJCDataColumn("TestValue");
      dataColumn.Value = 3;
      // Test Method
      var newDataValue = dataColumn;
      var result = newDataValue?.Value?.ToString();
      var compare = "3";
      TestCommon?.Write("CreateColumn()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
