// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumn.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // Provides the LJCDataColumn test methods.
  internal class TestDataColumn
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumn()
    {
      TestCommon = new LJCTestCommon("TestDataColumn");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataColumn ***");

      TestCommon.ShowNotImplemented = false;
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
      TestToString();

      // Creates a LJCDataValue object from an LJCDataColumn object.
      CreateValue();
      #endregion

      #region Data Properties

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

      #region Additional Properties

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
      // "Natural", "Natural*", "Foreign"
      KeyType();

      // Gets or sets the original value.
      OriginalValue();
      #endregion

      #region View Join Data Properties

      // Gets or sets the view ID value.
      ID();

      // Gets or sets the view Sequence value.
      Sequence();

      // Gets or sets the view DataID value.
      ViewDataID();

      // Gets or sets the view JoinID value.
      ViewJoinID();

      // Gets or sets the view Width value.
      Width();
      #endregion
    }
    #endregion

    #region Constructor Test Methods

    // Initializes an object instance with the supplied values.
    private static void ParamConstructor()
    {
      var methodName = "ParamConstructor()";

      // Test Method
      var dataColumn = new LJCDataColumn("TestValue")
      {
        Value = 3
      };
      var result = $"{dataColumn.PropertyName}";
      var compare = "TestValue";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataColumn = new LJCDataColumn()
      {
        DataTypeName = "String",
        IsChanged = false,
        PropertyName = "TestValue",
        Value = 3
      };

      // Test Method
      var newDataColumn = new LJCDataValue(dataColumn);
      var result = $"{newDataColumn.PropertyName}";
      var compare = "TestValue";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dataColumn = new LJCDataColumn("TestValue", "3");

      // Test Method
      var newDataColumn = dataColumn.Clone();
      var result = $"{newDataColumn?.PropertyName}";
      var compare = "TestValue";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Formats the column value for an SQL string.
    private static void FormatValue()
    {
      var methodName = "FormatValue()";

      var dataColumn = new LJCDataColumn("TestValue")
      {
        Value = 3
      };
      // Test Method
      var result = dataColumn.FormatValue();
      var compare = "'3'";
      TestCommon?.Show($"{methodName}1", result!, compare);

      dataColumn = new LJCDataColumn("TestValue", "O'Brian");
      // Test Method
      result = dataColumn.FormatValue();
      compare = "'O''Brian'";
      TestCommon?.Show($"{methodName}2", result, compare);

      dataColumn = new LJCDataColumn("TestValue", "true", LJC.TypeBoolean);
      // Test Method
      result = dataColumn.FormatValue();
      compare = "1";
      TestCommon?.Show($"{methodName}3", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataColumn = new LJCDataColumn("TestValue", dateTimeString, LJC.TypeDateTime);
      result = dataColumn.FormatValue();
      compare = "'2026/01/01 00:00:00'";
      TestCommon?.Show($"{methodName}4", result, compare);
    }

    // Returns the object string identifier.
    private static void TestToString()
    {
      var methodName = "TestToString()";

      var dataColumn = new LJCDataColumn("TestValue")
      {
        Value = 3
      };
      // Test Method
      var result = dataColumn.ToString();
      var compare = "TestValue:3";
      TestCommon?.Show($"{methodName}1", result, compare);

      var dateTime = new DateTime(2026, 1, 1);
      var dateTimeString = dateTime.ToString();
      dataColumn = new LJCDataColumn("TestValue", dateTimeString, LJC.TypeDateTime);
      // Test Method
      result = dataColumn.ToString();
      compare = "TestValue:1/1/2026 12:00:00 AM";
      TestCommon?.Write("ToStringMethod()2", result, compare);
      TestCommon?.Show($"{methodName}2", result, compare);
    }

    // Creates a LJCDataValue object from an LJCDataColumn object.
    private static void CreateValue()
    {
      var methodName = "CreateValue()";

      var dataColumn = new LJCDataColumn("TestValue")
      {
        Value = 3
      };
      // Test Method
      var newDataValue = dataColumn;
      var result = $"{newDataValue.Value}";
      var compare = "3";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Gets or sets the AllowDBNull flag.
    private static void AllowDBNull()
    {
      var methodName = "AllowDBNull()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the AutoIncrement flag.
    private static void AutoIncrement()
    {
      var methodName = "AutoIncrement()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the Caption value.
    private static void Caption()
    {
      var methodName = "Caption()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the ColumnName value.
    private static void ColumnName()
    {
      var methodName = "ColumnName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the DataTypeName value.
    private static void DataTypeName()
    {
      var methodName = "DataTypeName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the MaxLength value.
    private static void MaxLength()
    {
      var methodName = "MaxLength()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the Fixed Length Field Position value.
    private static void Position()
    {
      var methodName = "Position()";

      var result = "";
      var compare = "Not Implemented";
      if (compare != "Not Implemented")
        TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the PropertyName value.
    private static void PropertyName()
    {
      var methodName = "PropertyName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the RenameAs value.
    private static void RenameAs()
    {
      var methodName = "RenameAs()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the SQLTypeName value.
    private static void SQLTypeName()
    {
      var methodName = "SQLTypeName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the Value object.
    private static void Value()
    {
      var methodName = "Value()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Additional Properties

    // Gets or sets the add order index.
    private static void AddOrderIndex()
    {
      var methodName = "AddOrderIndex()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the default value.
    private static void DefaultValue()
    {
      var methodName = "DefaultValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the changed indicator.
    private static void IsChanged()
    {
      var methodName = "IsChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the primary key indicator.
    private static void IsPrimaryKey()
    {
      var methodName = "IsPrimaryKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the unique key indicator.
    private static void IsUniqueKey()
    {
      var methodName = "IsUniqueKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the KeyType value.
    // "Natural", "Natural*", "Foreign"
    private static void KeyType()
    {
      var methodName = "KeyType()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the original value.
    private static void OriginalValue()
    {
      var methodName = "KeyType()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region View Join Data Properties

    // Gets or sets the view ID value.
    private static void ID()
    {
      var methodName = "ID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the view Sequence value.
    private static void Sequence()
    {
      var methodName = "Sequence()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the view DataID value.
    private static void ViewDataID()
    {
      var methodName = "ViewDataID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the view JoinID value.
    private static void ViewJoinID()
    {
      var methodName = "ViewJoinID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }

    // Gets or sets the view Width value.
    private static void Width()
    {
      var methodName = "Width()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon?.Show($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    private static bool ShowNotImplemented { get; set; }

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
