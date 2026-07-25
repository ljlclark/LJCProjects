// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataValue.cs
using LJCNetCommon;
using System;

namespace TestData
{
  // Provides the LJCDataValue test methods.
  internal class TestDataValue
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataValue()
    {
      TestCommon = new TestCommon("TestDataValue");
      Console.WriteLine();
      Console.WriteLine("*********************");
      Console.Write("*** LJCDataValue ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Constructor Methods

      // Initializes an object instance.
      Constructor();

      // Initializes an object instance with the supplied values.
      ParamConstructor();

      // The Copy constructor.
      CopyConstructor();
      #endregion

      #region Data Methods

      // Creates and returns a clone of the object.
      Clone();

      // Provides the default Sort functionality.
      CompareTo();

      // Formats the column value for the SQL string.
      FormatValue();

      // The object string identifier.
      TestToString();

      // Creates a combined LJCDataColumn from a LJCDataValue and LJCDataColumn.
      CreateColumn();
      #endregion

      #region Data Properties

      // Gets or sets the DataTypeName value.
      DataTypeName();

      // Gets or sets the PropertyName value.
      PropertyName();

      // Gets or sets the Value object.
      Value();
      #endregion

      #region Additional Properties

      // Indicates if the value has changed.
      IsChanged();
      #endregion
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataValue = new LJCDataValue();

      var result = dataValue.DataTypeName;
      result += $", {dataValue.IsChanged}";
      var compare = "string, False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object instance with the supplied values.
    private void ParamConstructor()
    {
      var methodName = "ParamConstructor()";

      // Test Method
      var dataValue = new LJCDataValue("PropertyName");

      var result = dataValue.PropertyName;
      result += $", {dataValue.DataTypeName}";
      var compare = "PropertyName, string";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object instance from the supplied object.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataValue = new LJCDataValue("PropertyName", "Value", "int");

      // Test Method
      var testDataValue = new LJCDataValue(dataValue);

      var result = testDataValue.PropertyName;
      result += $", {testDataValue.Value}";
      result += $", {testDataValue.DataTypeName}";
      var compare = "PropertyName, Value, int";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataValue = new LJCDataValue("PropertyName", "Value", "int");

      // Test Method
      var testDataValue = dataValue.Clone();

      var result = testDataValue.PropertyName;
      result += $", {testDataValue.Value}";
      result += $", {testDataValue.DataTypeName}";
      var compare = "PropertyName, Value, int";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Provides the default Sort functionality.
    private void CompareTo()
    {
      var methodName = "CompareTo()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Formats the column value for an SQL string.
    private void FormatValue()
    {
      var methodName = "FormatValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the object string identifier.
    private void TestToString()
    {
      var methodName = "TestToString()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates a LJCDataValue object from an LJCDataColumn object.
    private void CreateColumn()
    {
      var methodName = "CreateColumn()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the DataTypeName value.
    private void DataTypeName()
    {
      var methodName = "DataTypeName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the PropertyName value.
    private void PropertyName()
    {
      var methodName = "PropertyName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Value object.
    private void Value()
    {
      var methodName = "Value()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Additional Properties

    // Indicates that the value has changed.
    private void IsChanged()
    {
      var methodName = "IsChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
