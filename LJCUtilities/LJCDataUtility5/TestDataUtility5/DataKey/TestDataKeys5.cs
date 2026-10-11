// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataKeys5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataTables collection.
  internal class TestDataKeys
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKeys.xml'
    ///  path='members/Constructor/*'/>
    public TestDataKeys()
    {
      TestCommon = new LJCTestCommon("TestDataKeys");
      Run();
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
      LJCGetWithId();
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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
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

      var result = "";
      if (newDataTables != null)
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

      var dataKeys = new List<DataKey>();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
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

      DataKey? testDataKey = null;
      if (LJC.HasListItems(newDataKeys))
      {
        testDataKey = newDataKeys[4, 3, "Name"];
      }
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

    #region Collection Methods

    // Creates and returns a clone of this object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
      var result = $"{newDataKey.DbId}";
      result += $", {newDataKey.Id}";
      var compare = "2, 1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataKey.DataTableId}";
      result += $", {newDataKey.DataTableDbId}";
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
    private void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataKeys = new DataKeys();

      // Test Method
      var result = dataKeys.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        Id = 2,
        DbId = 3,
        DataTableId = 4,
        DataTableDbId = 5,
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

      var result = "";
      if (LJC.HasListItems(newDataKeys))
      {
        result = newDataKeys.Count.ToString();
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
    private void LJCGetWithId()
    {
      var methodName = "LJCGetWithId()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
      var item = dataKeys.LJCGetWithID(2, 1);

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
      var methodName = "LJCGetUnique()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
      var item = dataKeys.LJCGetUnique(4, 3, "Name");

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

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
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
      dataKeys.LJCRemove(4, 3, "Name");

      dataKey = dataKeys[0];
      var result = dataKey.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    private void LJCSortId()
    {
      var methodName = "LJCSortID()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
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
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
      var result = dataKey.Id.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    private void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        DbId = 2,
        Id = 2,
        DataTableDbId = 4,
        DataTableId = 3,
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
      var comparer = new DataKeyUnique();
      dataKeys.LJCSortUnique(comparer);

      dataKey = dataKeys[0];
      var result = dataKey.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    private void UniqueIndexer()
    {
      var methodName = "UniqueIndexer()";

      var dataKeys = new DataKeys();
      var dataKey = new DataKey
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        KeyType = 1,
        IsClustered = false,
        IsAscending = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeys.Add(dataKey);

      var result = "";
      dataKey = dataKeys[4, 3, "Name"];
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
