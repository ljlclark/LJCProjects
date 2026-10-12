// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataUtilTable5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using System.Data;

namespace TestDataUtilityDAL5
{
  // Tests the DataUtilTable object.
  internal class TestDataUtilTable
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataUtilTable.xml'
    ///  path='members/Constructor/*'/>
    public TestDataUtilTable()
    {
      TestCommon = new LJCTestCommon("TestDataUtilTable");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Data Object Methods
      AddChangedNames();
      Clone();
      CompareTo();
      LJCSetOriginalValues();
      TestToString();

      // Data Properties
      NoChange();
      Id();
      DbId();
      DataModuleId();
      DataModuleDbId();
      Name();
      Description();
      Sequence();
      SchemaName();
      NewName();
    }
    #endregion

    #region Data Object Methods

    // Adds changed propertynames.
    private void AddChangedNames()
    {
      var methodName = "AddChangedNames()";

      var dataTable = new DataUtilTable();

      var changedNames = new List<string>()
      {
        "Name",
        "Description",
      };

      // Test Method
      dataTable.ChangedNames.AddNames(changedNames);

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "Name, Description";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of this object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataTable = new DataUtilTable
      {
        DbId = 1,
        Id = 1,
        DataModuleDbId = 2,
        DataModuleId = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName"
      };

      // Test Method
      var newDataTable = dataTable.Clone();

      while (true)
      {
        var result = "";
        if (newDataTable != null)
        {
          result = newDataTable.Id.ToString();
        }
        var compare = "1";
        TestCommon.Write($"{methodName}1", result, compare);
        if (null == newDataTable)
        {
          break;
        }

        result = newDataTable.DataModuleId.ToString();
        compare = "2";
        TestCommon.Write($"{methodName}2", result, compare);

        result = newDataTable.Name;
        compare = "Name";
        TestCommon.Write($"{methodName}3", result, compare);

        result = newDataTable.Description;
        compare = "Description";
        TestCommon.Write($"{methodName}4", result, compare);

        result = newDataTable.Sequence.ToString();
        compare = "1";
        TestCommon.Write($"{methodName}5", result, compare);

        result = newDataTable.SchemaName;
        compare = "SchemaName";
        TestCommon.Write($"{methodName}6", result, compare);

        result = newDataTable.NewName;
        compare = "NewName";
        TestCommon.Write($"{methodName}7", result, compare);
        break;
      }
    }

    // Provides the default Sort functionality.
    private void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataTable = new DataUtilTable
      {
        DbId = 1,
        Id = 1,
      };
      var toDataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 2,
      };

      var result = $"{dataTable.CompareTo(toDataTable)}";
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{toDataTable.CompareTo(dataTable)}";
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataTable.DbId = 1;
      toDataTable.Id = 1;
      result = $"{dataTable.CompareTo(toDataTable)}";
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    private void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataTable = new DataUtilTable
      {
        DbId = 1,
        Id = 1,
        DataModuleDbId = 2,
        DataModuleId = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };

      // Test Method
      dataTable.LJCSetOriginalValues();

      dataTable.DbId = 1;
      dataTable.Id = 1;
      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleDbId = 2;
      dataTable.DataModuleId = 2;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}2", result, compare);

      dataTable.Name = "Name";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}3", result, compare);

      dataTable.Description = "Description";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}4", result, compare);

      dataTable.Sequence = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}5", result, compare);

      dataTable.SchemaName = "SchemaName";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}6", result, compare);

      dataTable.NewName = "NewName";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}7", result, compare);
    }

    // The object string identifier.
    private void TestToString()
    {
      var methodName = "TestToString()";

      var dataTable = new DataUtilTable
      {
        DbId = 1,
        Id = 1,
        Name = "Name",
      };

      // Test Method
      var result = dataTable.ToString();

      var compare = "Name:1";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // No changes.
    private void NoChange()
    {
      var methodName = "NoChange()";

      var dataTable = new DataUtilTable();

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change DbId
    private void DbId()
    {
      var methodName = "DbId()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        DbId = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DbId = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DbId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change ID
    private void Id()
    {
      var methodName = "Id()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        Id = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.Id = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "Id";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataModuleDbId
    private void DataModuleDbId()
    {
      var methodName = "DataModuleDbId()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        DataModuleDbId = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleDbId = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DataModuleDbId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataModuleID
    private void DataModuleId()
    {
      var methodName = "DataModuleId()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        DataModuleId = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleId = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DataModuleId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    private void Name()
    {
      var methodName = "Name()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        Name = "",
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.Name = "Name";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "Name";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Description
    private void Description()
    {
      var methodName = "Description()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        Description = null,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.Description = "Description";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "Description";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Sequence
    private void Sequence()
    {
      var methodName = "Sequence()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        Sequence = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.Sequence = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "Sequence";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change SchemaName
    private void SchemaName()
    {
      var methodName = "SchemaName()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        SchemaName = null,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.SchemaName = "SchemaName";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "SchemaName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change NewName
    private void NewName()
    {
      var methodName = "NewName()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        // Original Value
        NewName = null,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.SchemaName = "SchemaName";
      dataTable.NewName = "NewName";
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "SchemaName, NewName";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion
  }
}
