// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDbColumn5.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestDataAccess5
{
  internal class TestDbColumn
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDbColumn.xml'
    ///  path='members/Constructor/*'/>
    public TestDbColumn()
    {
      TestCommon = new LJCTestCommon("TestDbColumn");
      Run();
    }

    // Run the tests.
    private void Run()
    {

      // Static Methods
      Clone();
      ToDataColumn();
    }
    #endregion

    #region Static Methods

    // Clones a DataColumn object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dbColumn = new DataColumn()
      {
        AllowDBNull = false,
        AutoIncrement = true,
        Caption = "Name",
        ColumnName = "Name",
        DataType = typeof(string),
        DefaultValue = null,
        MaxLength = 60,
        Unique = false,
      };

      // Test Method
      var cloneColumn = LJCTableColumn.Clone(dbColumn);
      var result = "";
      if (cloneColumn != null)
      {
        result = cloneColumn.ColumnName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void ToDataColumn()
    {
      var methodName = "ToDataColumn()";

      var dbColumn = new DataColumn()
      {
        AllowDBNull = false,
        AutoIncrement = true,
        Caption = "Name",
        ColumnName = "Name",
        DataType = typeof(string),
        DefaultValue = null,
        MaxLength = 60,
        Unique = false,
      };

      // Test Method
      var dataColumn = LJCTableColumn.ToDataColumn(dbColumn);
      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.ColumnName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
