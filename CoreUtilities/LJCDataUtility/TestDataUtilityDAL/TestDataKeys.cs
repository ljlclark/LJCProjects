// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataKeys.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataTables collection.
  internal class TestDataKeys
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataKeys()
    {
      TestCommon = new TestCommon("TestDataKeys");
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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);
      dataKeys.LJCSerialize();

      // Test Method
      var newDataTables = DataKeys.LJCDeserialize();

      var result = newDataTables.Count.ToString();
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get custom collection from List<T>.
    public void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataKeys = new List<DataKey>();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);

      // Test Method
      var newDataKeys = DataKeys.LJCGetCollection(dataKeys);

      var testDataKey = newDataKeys[3, 4, "Name"];
      if (null == testDataKey)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataKey != null)
      {
        var result = testDataKey.Name;
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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      var newDataKeys = dataKeys.Clone();

      var newDataKey = newDataKeys[0];
      var result = $"{newDataKey.ID}";
      result += $", {newDataKey.DataSiteID}";
      var compare = "1, 2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataKey.DataTableID}";
      result += $", {newDataKey.DataTableSiteID}";
      compare = "3, 4";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataKey.Name;
      result += $", {newDataKey.KeyType}";
      compare = "Name, 1";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newDataKey.SourceColumnName;
      result += $", {newDataKey.TargetTableName}";
      result += $", {newDataKey.TargetColumnName}";
      compare = "SourceColumnName, TargetTableName, TargetColumnName";
      TestCommon.Write($"{methodName}4", result, compare);
    }

    // Checks if the collection has items.
    public void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataKeys = new DataKeys();

      // Test Method
      var result = dataKeys.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      result = dataKeys.LJCHasItems().ToString();
      compare = "True";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Serializes the collection to a file.
    public void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 0,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = null,
        TargetTableName = null,
        TargetColumnName = null,
      };
      dataKeys.Add(dataKey);

      // Test Method
      dataKeys.LJCSerialize();

      var newDataKeys = DataKeys.LJCDeserialize();

      var result = newDataKeys.Count.ToString();
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
      var dataKeys = new DataKeys
      {
        { 1, 2, 3, 4, "Name" },
      };

      var item = dataKeys[0];
      var result = item.Name;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item.
    public void LJCGetWithID()
    {
      var methodName = "LJCGetWithID()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      var item = dataKeys.LJCGetWithID(1, 2);

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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      var item = dataKeys.LJCGetWithUnique(3, 4, "Name");

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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      dataKeys.LJCRemove(3, 4, "Name");

      dataKey = dataKeys[0];
      var result = dataKey.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    public void LJCSortID()
    {
      var methodName = "LJCSortID()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      dataKeys.LJCSortID();

      dataKey = dataKeys[0];
      var result = dataKey.ID.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    public void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "First",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      // Test Method
      var comparer = new DataKeyUniqueComparer();
      dataKeys.LJCSortUnique(comparer);

      dataKey = dataKeys[0];
      var result = dataKey.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    public void UniqueIndexer()
    {
      var methodName = "NameIndexer()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 2,
        DataTableID = 3,
        DataTableSiteID = 4,
        Name = "Name",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      dataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 3,
        DataTableID = 4,
        DataTableSiteID = 5,
        Name = "First",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      dataKey = dataKeys[3, 4, "Name"];
      var result = dataKey.Name;
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
