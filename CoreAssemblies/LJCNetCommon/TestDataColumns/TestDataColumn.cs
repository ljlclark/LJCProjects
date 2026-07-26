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
    #region Constructor Methods

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
      #region Constructor Method Calls

      // Initializes an object instance.
      Constructor();

      // Initializes an object instance with the supplied values.
      ParamConstructor();

      // Initializes an object instance from the supplied object.
      CopyConstructor();
      #endregion

      #region Data Method Calls

      // Creates and returns a clone of the object.
      Clone();

      // Formats the column value for an SQL string.
      FormatValue();

      // Returns the object string identifier.
      TestToString();

      // Creates a LJCDataValue object from an LJCDataColumn object.
      DataColumnToDataValue();
      #endregion

      #region Data Property Calls

      // Gets or sets the AllowDBNull flag.
      AllowDBNull();

      // Gets or sets the AutoIncrement flag.
      AutoIncrement();

      // Gets or sets the Caption value.
      Caption();

      // Gets or sets the ColumnName value.
      ColumnName();

      // Gets or sets the DataTypeName value.
      DataTypeName();

      // Gets or sets the MaxLength value.
      MaxLength();

      // Gets or sets the Fixed Length Field Position value.
      Position();

      // Gets or sets the PropertyName value.
      PropertyName();

      // Gets or sets the RenameAs value.
      RenameAs();

      // Gets or sets the SQLTypeName value.
      SQLTypeName();

      // Gets or sets the Value object.
      Value();
      #endregion

      #region Additional Property Calls

      // Gets or sets the add order index.
      AddOrderIndex();

      // Gets or sets the default value.
      DefaultValue();

      // Gets or sets the changed indicator.
      IsChanged();

      // Gets or sets the primary key indicator.
      IsPrimaryKey();

      // Gets or sets the unique key indicator.
      IsUniqueKey();

      // Gets or sets the KeyType value.
      KeyType();

      // Gets or sets the original value.
      OriginalValue();
      #endregion

      #region View Join Data Property Calls

      // Gets or sets the ID value.
      ID();

      // Gets or sets the Sequence value.
      Sequence();

      // Gets or sets the ViewData ID value.
      ViewDataID();

      // Gets or sets the ViewJoin ID value.
      ViewJoinID();

      // Gets or sets the Width value.
      Width();
      #endregion
    }
    #endregion

    #region Constructor Test Methods

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

    // Formats the column value for an SQL string.
    private void FormatValue()
    {
      var methodName = "FormatValue()";

      var dataColumn = new LJCDataColumn("IsValue", "true", "Boolean");
      var result = dataColumn.FormatValue();
      var compare = "1";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.Value = new DateTime(2026, 1, 9);
      dataColumn.DataTypeName = "DateTime";
      result = dataColumn.FormatValue();
      compare = "'2026/01/09 00:00:00'";
      TestCommon.Write($"{methodName}2", result, compare);

      dataColumn.Value = "Name";
      dataColumn.DataTypeName = "String";
      result = dataColumn.FormatValue();
      compare = "'Name'";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Returns the object string identifier.
    private void TestToString()
    {
      var methodName = "TestToString()";

      var dataColumn = new LJCDataColumn("ID", "1", "Int64", "ColumnID")
      {
        IsPrimaryKey = true
      };

      var result = dataColumn.ToString();
      var compare = "ID-ColumnID-P:1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates a LJCDataValue object from an LJCDataColumn object.
    private void DataColumnToDataValue()
    {
      var methodName = "DataColumnToDataValue()";

      var dataColumn = new LJCDataColumn("ID", "1", "Int64", "ColumnID");

      // Test Method
      var dataValue = dataColumn;

      var result = dataValue.PropertyName;
      var compare = "ID";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the AllowDBNull flag.
    private void AllowDBNull()
    {
      var methodName = "AllowDBNull()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.AllowDBNull}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the AutoIncrement flag.
    private void AutoIncrement()
    {
      var methodName = "AutoIncrement()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.AutoIncrement}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Caption value.
    private void Caption()
    {
      var methodName = "Caption()";

      var dataColumn = new LJCDataColumn("Name");

      var result = dataColumn.PropertyName;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ColumnName value.
    private void ColumnName()
    {
      var methodName = "ColumnName()";

      var dataColumn = new LJCDataColumn("Name")
      {
        ColumnName = "ColumnName"
      };

      var result = dataColumn.ColumnName;
      var compare = "ColumnName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the DataTypeName value.
    private void DataTypeName()
    {
      var methodName = "DataTypeName()";

      var dataColumn = new LJCDataColumn("Name");

      var result = dataColumn.DataTypeName;
      var compare = "string";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the MaxLength value.
    private void MaxLength()
    {
      var methodName = "MaxLength()";

      var dataColumn = new LJCDataColumn("Name")
      {
        MaxLength = 20,
      };

      var result = $"{dataColumn.MaxLength}";
      var compare = "20";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Fixed Length Field Position value.
    private void Position()
    {
      var methodName = "Position()";

      var dataColumn = new LJCDataColumn("Name")
      {
        Position = 1,
        MaxLength = 20,
      };

      var result = $"{dataColumn.Position}";
      result += $", {dataColumn.MaxLength}";
      var compare = "1, 20";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the PropertyName value.
    private void PropertyName()
    {
      var methodName = "PropertyName()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.PropertyName}";
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the RenameAs value.
    private void RenameAs()
    {
      var methodName = "RenameAs()";

      var dataColumn = new LJCDataColumn("Name")
      {
        RenameAs = "NameRename",
      };

      var result = $"{dataColumn.RenameAs}";
      var compare = "NameRename";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the SQLTypeName value.
    private void SQLTypeName()
    {
      var methodName = "SQLTypeName()";

      var dataColumn = new LJCDataColumn("Name")
      {
        SQLTypeName = "varchar(60)",
      };

      var result = $"{dataColumn.SQLTypeName}";
      var compare = "varchar(60)";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Value object.
    private void Value()
    {
      var methodName = "Value()";

      var dataColumn = new LJCDataColumn("Name");
      var result = $"{dataColumn.IsChanged}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);

      dataColumn = new LJCDataColumn("Name")
      {
        Value = "First",
      };
      result = $"{dataColumn.IsChanged}";
      compare = "True";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Additional Properties

    // Gets or sets the add order index.
    private void AddOrderIndex()
    {
      var methodName = "AddOrderIndex()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.AddOrderIndex}";
      var compare = "-1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the default value.
    private void DefaultValue()
    {
      var methodName = "DefaultValue()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.DefaultValue}";
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the changed indicator.
    private void IsChanged()
    {
      var methodName = "IsChanged()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.IsChanged}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the primary key indicator.
    private void IsPrimaryKey()
    {
      var methodName = "IsPrimaryKey()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.IsPrimaryKey}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the unique key indicator.
    private void IsUniqueKey()
    {
      var methodName = "IsUniqueKey()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.IsUniqueKey}";
      var compare = "False";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the KeyType value.
    // "Natural", "Natural*", "Foreign"
    private void KeyType()
    {
      var methodName = "KeyType()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.KeyType}";
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the original value.
    private void OriginalValue()
    {
      var methodName = "OriginalValue()";

      var dataColumn = new LJCDataColumn("Name");

      var result = $"{dataColumn.OriginalValue}";
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region View Join Data Properties

    // Gets or sets the ID value.
    private void ID()
    {
      var methodName = "ID()";

      var dataColumn = new LJCDataColumn("Name")
      {
        ID = 1,
      };

      var result = $"{dataColumn.ID}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Sequence value.
    private void Sequence()
    {
      var methodName = "Sequence()";

      var dataColumn = new LJCDataColumn("Name")
      {
        Sequence = 1,
      };

      var result = $"{dataColumn.Sequence}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ViewData ID value.
    private void ViewDataID()
    {
      var methodName = "ViewDataID()";

      var dataColumn = new LJCDataColumn("Name")
      {
        ViewDataID = 1,
      };

      var result = $"{dataColumn.ViewDataID}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the ViewJoin ID value.
    private void ViewJoinID()
    {
      var methodName = "ViewJoinID()";

      var dataColumn = new LJCDataColumn("Name")
      {
        ViewJoinID = 1,
      };

      var result = $"{dataColumn.ViewJoinID}";
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets or sets the Width value.
    private void Width()
    {
      var methodName = "Width()";

      var dataColumn = new LJCDataColumn("Name")
      {
        Width = 25,
      };

      var result = $"{dataColumn.Width}";
      var compare = "25";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
