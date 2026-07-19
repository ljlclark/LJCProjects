// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumn.cs
using LJCNetCommon;
using System;

namespace TestData
{
  // Provides the LJCDataColumn test methods.
  internal class TestDataColumn
  {
    // Initializes an object instance.
    public TestDataColumn()
    {
      TestCommon = new TestCommon("TestDataColumn");
      Console.WriteLine();
      Console.WriteLine("*********************");
      Console.Write("*** LJCDataColumn ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Constructor Methods
      Constructor();
      ParamConstructor();
      CopyConstructor();

      // Data Methods
      Clone();
      CompareTo();
      FormatValue();
      TestToString();
      DataColumnToDataValue();

      // Data Properties
      AllowDbNull();
      AutoIncrement();
      Caption();
      ColumnName();
      DataTypeName();
      MaxLength();
      Position();
      PropertyName();
      RenameAs();
      SQLTypeName();
      Value();

      // Additional Properties
      AddOrderIndex();
      DefaultValue();
      IsChanged();
      IsPrimaryKey();
      KeyType();
      OriginalValue();
      IsUniqueKey();

      // View Join Data Properties
      ID();
      Sequence();
      ViewDataID();
      ViewJoinID();
      Width();
    }

    #region Constructor Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataColumn = new LJCDataColumn();

      var result = dataColumn.DataTypeName;
      result += $", {dataColumn.AddOrderIndex}";
      var compare = "string, -1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object instance with the supplied values.
    private void ParamConstructor()
    {
      var methodName = "ParamConstructor()";

      // Test Method
      var dataColumn = new LJCDataColumn("PropertyName"
        , columnName: "ColumnName");

      var result = dataColumn.PropertyName;
      result += $", {dataColumn.ColumnName}";
      result += $", {dataColumn.DataTypeName}";
      var compare = "PropertyName, ColumnName, string";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object instance from the supplied object.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataColumn = new LJCDataColumn("PropertyName", "Value"
        , "int", "ColumnName", true, "Renamed");


      // Test Method
      var testDataColumn = new LJCDataColumn(dataColumn);

      var result = testDataColumn.PropertyName;
      result += $", {testDataColumn.AutoIncrement}";
      result += $", {testDataColumn.RenameAs}";
      result += $", {testDataColumn.Value}";
      var compare = "PropertyName, True, Renamed, Value";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataColumn = new LJCDataColumn("PropertyName"
        , columnName: "ColumnName");

      // Test Method
      var testDataColumn = dataColumn.Clone();

      var result = testDataColumn.PropertyName;
      result += $", {testDataColumn.ColumnName}";
      result += $", {testDataColumn.DataTypeName}";
      var compare = "PropertyName, ColumnName, string";
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
    private void DataColumnToDataValue()
    {
      var methodName = "DataColumnToDataValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the AllowDBNull flag.
    private void AllowDbNull()
    {
      var methodName = "AllowDbNull()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the AutoIncrement flag.
    private void AutoIncrement()
    {
      var methodName = "AutoIncrement()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Caption value.
    private void Caption()
    {
      var methodName = "Caption()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ColumnName value.
    private void ColumnName()
    {
      var methodName = "ColumnName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the DataTypeName value.
    private void DataTypeName()
    {
      var methodName = "DataTypeName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the MaxLength value.
    private void MaxLength()
    {
      var methodName = "MaxLength()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Fixed Length Field Position value.
    private void Position()
    {
      var methodName = "Position()";

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

    // Gets or sets the RenameAs value.
    private void RenameAs()
    {
      var methodName = "RenameAs()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the SQLTypeName value.
    private void SQLTypeName()
    {
      var methodName = "SQLTypeName()";

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

    // Gets or sets the add order index.
    private void AddOrderIndex()
    {
      var methodName = "AddOrderIndex()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the default value.
    private void DefaultValue()
    {
      var methodName = "DefaultValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the changed indicator.
    private void IsChanged()
    {
      var methodName = "IsChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the primary key indicator.
    private void IsPrimaryKey()
    {
      var methodName = "IsPrimaryKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the KeyType value.
    // "Natural", "Natural*", "Foreign"
    private void KeyType()
    {
      var methodName = "KeyType()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the original value.
    private void OriginalValue()
    {
      var methodName = "OriginalValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the unique key indicator.
    private void IsUniqueKey()
    {
      var methodName = "IsUniqueKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region View Join Data Properties

    // Gets or sets the ID value.
    private void ID()
    {
      var methodName = "ID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Sequence value.
    private void Sequence()
    {
      var methodName = "Sequence()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ViewData ID value.
    private void ViewDataID()
    {
      var methodName = "ViewDataID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ViewJoin ID value.
    private void ViewJoinID()
    {
      var methodName = "ViewJoinID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Width value.
    private void Width()
    {
      var methodName = "Width()";

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
