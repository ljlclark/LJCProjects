// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataValue.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // Provides the LJCDataValue test methods.
  internal class TestDataValue
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataValue()
    {
      TestCommon = new LJCTestCommon("TestDataValue");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataValue ***");

      //TestCommon.ShowNotImplemented = false;
      Run();
    }

    private static void Run()
    {
      #region Constructor Test Methods

      // Initializes an object instance with the supplied values.
      ParamConstructor();

      // The Copy constructor.
      CopyConstructor();
      #endregion

      #region Data Methods

      // Creates and returns a clone of the object.
      Clone();

      // Formats the column value for an SQL string.
      FormatValue();

      // Returns the object string identifier.
      ToStringMethod();

      // Creates a combined LJCDataColumn from an LJCDataValue and LJCDataColumn.
      CreateColumn();
      #endregion

      #region Data Properties

      // Gets or sets the Value object.
      Value();
      #endregion
    }
    #endregion

    #region Constructor Test Methods

    // Initializes an object instance with the supplied values.
    private static void ParamConstructor()
    {
      var methodName = "ParamConstructor()";

      // Test Method
      var dataValue = new LJCDataValue("TestValue", 3);
      var result = dataValue.PropertyName;
      var compare = "TestValue";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

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
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dataValue = new LJCDataValue("TestValue", 3);

      // Test Method
      var newDataValue = dataValue.Clone();
      var result = newDataValue?.PropertyName;
      var compare = "TestValue";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Formats the column value for an SQL string.
    private static void FormatValue()
    {
      var methodName = "FormatValue()";

      var dataColumn = new LJCDataValue("TestValue")
      {
        Value = 3
      };
      // Test Method
      var result = dataColumn.FormatValue();
      var compare = "'3'";
      TestCommon?.Show($"{methodName}1", result!, compare);

      dataColumn = new LJCDataValue("TestValue", "O'Brian");
      // Test Method
      result = dataColumn.FormatValue();
      compare = "'O''Brian'";
      TestCommon?.Show($"{methodName}2", result, compare);

      dataColumn = new LJCDataValue("TestValue", "true", LJC.TypeBoolean);
      // Test Method
      result = dataColumn.FormatValue();
      compare = "1";
      TestCommon?.Show($"{methodName}3", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataColumn = new LJCDataValue("TestValue", dateTimeString, LJC.TypeDateTime);
      result = dataColumn.FormatValue();
      compare = "'2026/01/01 00:00:00'";
      TestCommon?.Show($"{methodName}4", result, compare);
    }

    // Returns the object string identifier.
    private static void ToStringMethod()
    {
      var methodName = "ToStringMethod()";

      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Method
      var result = dataValue.ToString();
      var compare = "TestValue:3";
      TestCommon?.Show($"{methodName}1", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataValue = new LJCDataColumn("TestValue", dateTimeString, LJC.TypeDateTime);
      // Test Method
      result = dataValue.ToString();
      compare = "TestValue:1/1/2026 12:00:00 AM";
      TestCommon?.Show($"{methodName}2", result, compare);
    }

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
      TestCommon?.Show("CreateColumn()1", result, compare);

      dataColumn = new LJCDataColumn("Test");
      // Test Method
      newDataColumn = dataValue.CreateColumn(dataColumn);
      value = newDataColumn?.Value;
      result = value?.ToString();
      compare = "3";
      TestCommon?.Show("CreateColumn()2", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the Value object.
    private static void Value()
    {
      var methodName = "Value()";

      var dataValue = new LJCDataValue("TestValue", 3);
      // Test Value
      var value = dataValue.Value;
      var result = value?.ToString();
      var compare = "3";
      TestCommon?.Show($"{methodName}1", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "True";
      TestCommon?.Show($"{methodName}2", result, compare);

      // Test Value
      dataValue.Value = 3;
      value = dataValue.Value;
      result = value.ToString();
      compare = "3";
      TestCommon?.Show($"{methodName}3", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "True";
      TestCommon?.Show($"{methodName}4", result, compare);

      // Test Value
      dataValue.Value = 4;
      value = dataValue.Value;
      result = value.ToString();
      compare = "4";
      TestCommon?.Show($"{methodName}5", result, compare);
      // Test Value
      value = dataValue.IsChanged;
      result = value.ToString();
      compare = "True";
      TestCommon?.Show($"{methodName}6", result, compare);
    }
    #endregion

    #region Properties

    private static bool ShowNotImplemented { get; set; }

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
