// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataTables5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataTables collection.
  internal class TestDataTables
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataTables()
    {
      TestCommon = new LJCTestCommon("TestDataTables");
    }

    // Run the tests.
    private void Run()
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
      LJCGetUnique();
      LJCRemove();

      // Sort Methods
      LJCSortId();
      LJCSortUnique();

      // Data Properties
      UniqueIndexer();
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
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

      var result = "";
      if (LJC.HasListItems(newDataTables))
      {
        result = newDataTables.Count.ToString();
      }
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get custom collection from List<T>.
    private void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataTables = new List<DataUtilTable>();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      var newDataTables = DataTables.LJCGetCollection(dataTables);

      DataUtilTable? testDataTable = null;
      if (LJC.HasListItems(newDataTables))
      {
        testDataTable = newDataTables[4, 3, "Name"];
      }
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
    private void Clone()
    {
      var methodName = "Clone()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
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
      var result = newDataTable.Id.ToString();
      result += $", {newDataTable.DbId}";
      var compare = "1, 2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataTable.DataModuleId}";
      result += $", {newDataTable.DataModuleDbId}";
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
    private void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataTables = new DataTables();

      // Test Method
      var result = dataTables.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
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
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
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

      var result = "";
      if (LJC.HasListItems(newDataTables))
      {
        result = newDataTables.Count.ToString();
      }
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Creates and adds the object from the supplied values.
    private void Add()
    {
      var methodName = "Add()";

      // Test Method
      var dataTables = new DataTables
      {
        { 1, 2, 3, 4, "Name" },
      };

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
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
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
    private void LJCGetUnique()
    {
      var methodName = "LJCGetWithName()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      var item = dataTables.LJCGetUnique(4, 3, "Name");

      var result = "";
      if (item != null)
      {
        result = item.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Removes an item by name.
    private void LJCRemove()
    {
      var methodName = "LJCRemove()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      dataTables.LJCRemove(4, 3, "Name");

      dataTable = dataTables[0];
      var result = dataTable.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    private void LJCSortId()
    {
      var methodName = "LJCSortID()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      // Test Method
      dataTables.LJCSortId();

      dataTable = dataTables[0];
      var result = dataTable.Id.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    private void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      // Test Method
      var comparer = new DataTableUnique();
      dataTables.LJCSortUnique(comparer);

      dataTable = dataTables[0];
      var result = dataTable.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    private void UniqueIndexer()
    {
      var methodName = "NameIndexer()";

      var dataTables = new DataTables();
      var dataTable = new DataUtilTable
      {
        DbId = 2,
        Id = 1,
        DataModuleDbId = 4,
        DataModuleId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        SchemaName = "SchemaName",
        NewName = "NewName",
      };
      dataTables.Add(dataTable);

      dataTable = new DataUtilTable
      {
        DbId = 3,
        Id = 2,
        DataModuleDbId = 5,
        DataModuleId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        SchemaName = "SchemaName2",
        NewName = "NewName2",
      };
      dataTables.Add(dataTable);

      var result = "";
      dataTable = dataTables[4, 3, "Name"];
      if (dataTable != null)
      {
        result = dataTable.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
