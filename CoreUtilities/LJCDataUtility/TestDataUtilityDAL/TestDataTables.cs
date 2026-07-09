// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataTables.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataTables collection.
  internal class TestDataTables
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataTables()
    {
      TestCommon = new TestCommon("TestDataTables");
    }

    // Run the tests.
    public void Run()
    {
      // Static Methods
      LJCDeserialize();
      LJCGetCollection();

      // Collection Methods
      Clone();
      LJCHasItems();
      LJCSerialize();

      // Collection Data Methods
      Add();
      LJCGetWithID();
      LJCGetWithUnique();
      LJCRemove();

      // Sort Methods
      LJCSortID();
      LJCSortUnique();

      // Data Properties
      UniqueIndexer();
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    public void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);
      dataTables.LJCSerialize();

      // Test Method
      var newDataTables = DataTables.LJCDeserialize();

      var result = newDataTables.Count.ToString();
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get custom collection from List<T>.
    public void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataTables = new List<DataUtilTable>();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      var newDataTables = DataTables.LJCGetCollection(dataTables);

      var testDataTable = newDataTables[3, 4, "Name"];
      if (null == testDataTable)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataTable != null)
      {
        var result = testDataTable.Name;
        var compare = "Name";
        TestCommon.Write($"{methodName}2", result, compare);
      }
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      var newDataTables = dataTables.Clone();

      var newDataTable = newDataTables[0];
      var result = newDataTable.ID.ToString();
      result += $", {newDataTable.DataSiteID}";
      var compare = "1, 2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataTable.DataModuleID}";
      result += $", {newDataTable.DataModuleSiteID}";
      compare = "3, 4";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataTable.Name;
      result += $", {newDataTable.Description}";
      compare = "Name, Description";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newDataTable.Sequence.ToString();
      result += $", {newDataTable.SchemaName}";
      result += $", {newDataTable.NewName}";
      compare = "1, SchemaName, NewName";
      TestCommon.Write($"{methodName}4", result, compare);
    }

    // Checks if the collection has items.
    public void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataTables = new DataTables();

      // Test Method
      var result = dataTables.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      result = dataTables.LJCHasItems().ToString();
      compare = "True";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Serializes the collection to a file.
    public void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      dataTables.LJCSerialize();

      var newDataTables = DataTables.LJCDeserialize();

      var result = newDataTables.Count.ToString();
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Creates and adds the object from the supplied values.
    public void Add()
    {
      var methodName = "Add()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };

      // Test Method
      dataTables.Add(dataTable);

      var item = dataTables[0];
      var result = item.Name;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item.
    public void LJCGetWithID()
    {
      var methodName = "LJCGetWithID()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      var item = dataTables.LJCGetWithID(1, 2);

      var result = "";
      if (item != null)
      {
        result = item.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item with unique values.
    public void LJCGetWithUnique()
    {
      var methodName = "LJCGetWithName()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      var item = dataTables.LJCGetWithUnique(3, 4, "Name");

      var result = "";
      if (item != null)
      {
        result = item.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Removes an item by name.
    public void LJCRemove()
    {
      var methodName = "LJCRemove()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      dataTables.LJCRemove(3, 4, "Name");

      dataTable = dataTables[0];
      var result = dataTable.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    public void LJCSortID()
    {
      var methodName = "LJCSortID()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      dataTables.LJCSortID();

      dataTable = dataTables[0];
      var result = dataTable.ID.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    public void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      var comparer = new DataTableUniqueComparer();
      dataTables.LJCSortUnique(comparer);

      dataTable = dataTables[0];
      var result = dataTable.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    public void UniqueIndexer()
    {
      var methodName = "NameIndexer()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        ID = 1,
        DataSiteID = 2,
        DataModuleID = 3,
        DataModuleSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        ID = 2,
        DataSiteID = 3,
        DataModuleID = 4,
        DataModuleSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      dataTable = dataTables[3, 4, "Name"];
      var result = dataTable.Name;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
