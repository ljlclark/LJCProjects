// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataUtilTable.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataUtilTable object.
  internal class TestDataUtilTable
  {
    // Initializes an object instance.
    public TestDataUtilTable()
    {
      TestCommon = new TestCommon("DataTableManager");
    }

    // Run the tests.
    public void Run()
    {
      // Data Object Methods
      AddChangedNames();
      Clone();
      CompareTo();
      LJCSetOriginalValues();
      TestToString();

      // Data Properties
      NoChange();
      ID();
      DataSiteID();
      DataModuleID();
      DataModuleSiteID();
      Name();
      Description();
      SchemaName();
      NewName();
    }

    #region Data Object Methods

    // Adds changed propertynames.
    public void AddChangedNames()
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
    public void Clone()
    {
      var methodName = "Clone()";

      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataModuleID = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName"
      };

      // Test Method
      var newDataTable = dataTable.Clone();

      var result = newDataTable.ID.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataTable.DataModuleID.ToString();
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
    }

    // Provides the default Sort functionality.
    public void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataTable = new DataUtilTable
      {
        ID = 1
      };
      var toDataTable = new DataUtilTable
      {
        ID = 2
      };

      var result = dataTable.CompareTo(toDataTable).ToString();
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = toDataTable.CompareTo(dataTable).ToString();
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataTable.ID = 1;
      result = dataTable.CompareTo(toDataTable).ToString();
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    public void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataModuleID = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };

      // Test Method
      dataTable.LJCSetOriginalValues();

      dataTable.ID = 1;
      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleID = 2;
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
    public void TestToString()
    {
      var methodName = "TestToString()";

      var dataTable = new DataUtilTable
      {
        ID = 1,
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
    public void NoChange()
    {
      var methodName = "NoChange()";

      var dataTable = new DataUtilTable();

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change ID
    public void ID()
    {
      var methodName = "ID()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        ID = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.ID = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "ID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataSiteID()
    {
      var methodName = "DataSiteID()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        DataSiteID = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataSiteID = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DataSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataModuleID
    public void DataModuleID()
    {
      var methodName = "DataModuleID()";

      var dataTable = new DataUtilTable
      {
        DataModuleID = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleID = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DataModuleID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataModuleSiteID()
    {
      var methodName = "DataModuleSiteID()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        DataModuleSiteID = 0,
      };

      var result = dataTable.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataTable.DataModuleSiteID = 1;
      result = dataTable.ChangedNames.ChangedPropertyNames;
      compare = "DataModuleSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    public void Name()
    {
      var methodName = "Name()";

      // Test Method
      var dataTable = new DataUtilTable
      {
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
    public void Description()
    {
      var methodName = "Description()";

      // Test Method
      var dataTable = new DataUtilTable
      {
        Description = "",
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
    public void Sequence()
    {
      var methodName = "Sequence()";

      // Test Method
      var dataTable = new DataUtilTable
      {
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
    public void SchemaName()
    {
      var methodName = "SchemaName()";

      // Test Method
      var dataTable = new DataUtilTable
      {
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
    public void NewName()
    {
      var methodName = "NewName()";

      // Test Method
      var dataTable = new DataUtilTable
      {
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

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
