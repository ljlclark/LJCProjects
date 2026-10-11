// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumns5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using System.Collections.Generic;

namespace TestDataUtilityDAL5
{
  // Tests the DataTables collection.
  internal class TestDataColumns
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataColumns.xml'
    ///  path='members/Constructor/*'/>
    public TestDataColumns()
    {
      TestCommon = new LJCTestCommon("TestDataColumns");
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
      LJCSortID();
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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);
      dataColumns.LJCSerialize();

      // Test Method
      var newDataColumns = DataColumns.LJCDeserialize();

      var result = "";
      if (newDataColumns != null)
      {
        result = newDataColumns.Count.ToString();
      }
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get custom collection from List<T>.
    private void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataColumns = new List<DataUtilColumn>();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var newDataColumns = DataColumns.LJCGetCollection(dataColumns);

      DataUtilColumn? testDataColumn = null;
      if (LJC.HasListItems(newDataColumns))
      {
        testDataColumn = newDataColumns[3, 4, "Name"];
      }
      if (null == testDataColumn)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataColumn != null)
      {
        var result = testDataColumn.Name;
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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var newDataColumns = dataColumns.Clone();

      var newDataColumn = newDataColumns[0];
      var result = newDataColumn.Id.ToString();
      result += $", {newDataColumn.DbId}";
      var compare = "1, 2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataColumn.DataTableId}";
      result += $", {newDataColumn.DataTableDbId}";
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
    private void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataColumns = new DataColumns();

      // Test Method
      var result = dataColumns.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
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
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCSerialize();

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
    private void LJCGetWithId()
    {
      var methodName = "LJCGetWithID()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var item = dataColumns.LJCGetWithId(2, 1);

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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      var item = dataColumns.LJCGetUnique(4, 3, "Name");

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

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCRemove(4, 3, "Name");

      dataColumn = dataColumns[0];
      var result = dataColumn.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    private void LJCSortID()
    {
      var methodName = "LJCSortID()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      // Test Method
      dataColumns.LJCSortId();

      dataColumn = dataColumns[0];
      var result = dataColumn.Id.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    private void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 4,
        DataTableId = 3,
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
    private void UniqueIndexer()
    {
      var methodName = "NameIndexer()";

      var dataColumns = new DataColumns();
      var dataColumn = new DataUtilColumn()
      {
        DbId = 2,
        Id = 1,
        DataTableDbId = 4,
        DataTableId = 3,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        NewName = "NewName",
      };
      dataColumns.Add(dataColumn);

      dataColumn = new DataUtilColumn()
      {
        DbId = 3,
        Id = 2,
        DataTableDbId = 5,
        DataTableId = 4,
        Name = "First",
        Description = "Description2",
        Sequence = 2,
        NewName = "NewName2",
      };
      dataColumns.Add(dataColumn);

      var result = "";
      dataColumn = dataColumns[4, 3, "Name"];
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
