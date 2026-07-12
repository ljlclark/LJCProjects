// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumn()s.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataTables collection.
  internal class TestDataColumns
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumns()
    {
      TestCommon = new TestCommon("TestDataColumns");
      Run();
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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);
      dataColumns.LJCSerialize();

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

      var dataColumns = new List<DataUtilColumn>();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var newDataTables = DataColumns.LJCGetCollection(dataColumns);

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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var newDataColumns = dataColumns.Clone();

      var newDataColumn = newDataColumns[0];
      var result = newDataColumn.ID.ToString();
      result += $", {newDataColumn.DataSiteID}";
      var compare = "1, 2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataColumn.DataTableID}";
      result += $", {newDataColumn.DataTableSiteID}";
      compare = "3, 4";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataColumn.Name;
      result += $", {newDataColumn.Description}";
      compare = "Name, Description";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newDataColumn.Sequence.ToString();
      result += $", {newDataColumn.NewName}";
      compare = "1, NewName";
      TestCommon.Write($"{methodName}4", result, compare);
    }

    // Checks if the collection has items.
    public void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataColumns = new DataColumns();

      // Test Method
      var result = dataColumns.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      result = dataColumns.LJCHasItems().ToString();
      compare = "True";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Serializes the collection to a file.
    public void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCSerialize();

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

      // Test Method
      var dataColumns = new DataColumns
      {
        { 1, 2, 3, 4, "Name" },
      };

      var item = dataColumns[0];
      var result = item.Name;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item.
    public void LJCGetWithID()
    {
      var methodName = "LJCGetWithID()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var item = dataColumns.LJCGetWithID(1, 2);

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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var item = dataColumns.LJCGetWithUnique(3, 4, "Name");

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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCRemove(3, 4, "Name");

      dataColumn = dataColumns[0];
      var result = dataColumn.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    public void LJCSortID()
    {
      var methodName = "LJCSortID()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCSortID();

      dataColumn = dataColumns[0];
      var result = dataColumn.ID.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    public void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var comparer = new DataColumnUnique();
      dataColumns.LJCSortUnique(comparer);

      dataColumn = dataColumns[0];
      var result = dataColumn.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    public void UniqueIndexer()
    {
      var methodName = "NameIndexer()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      dataColumn = dataColumns[3, 4, "Name"];
      var result = dataColumn.Name;
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
