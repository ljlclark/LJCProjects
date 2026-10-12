// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataKey5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataUtilTable object.
  internal class TestDataKey
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKey.xml'
    ///  path='members/Constructor/*'/>
    public TestDataKey()
    {
      TestCommon = new LJCTestCommon("TestDataKey");
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
      DataTableId();
      DataTableDbId();
      Name();
      KeyType();
      IsAscending();
      IsClustered();
      SourceColumnName();
      TargetTableName();
      TargetColumnName();
    }
    #endregion

    #region Data Object Methods

    // Adds changed propertynames.
    private void AddChangedNames()
    {
      var methodName = "AddChangedNames()";

      var dataKey = new DataKey();

      var changedNames = new List<string>()
      {
        "Name",
      };

      // Test Method
      dataKey.ChangedNames.AddNames(changedNames);

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of this object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataKey = new DataKey
      {
        DbId = 1,
        Id = 1,
        DataTableDbId = 2,
        DataTableId = 2,
        Name = "Name",
        KeyType = 1,
        IsAscending = true,
        IsClustered = true,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };

      // Test Method
      var newDataKey = dataKey.Clone();

      while (true)
      {
        var result = "";
        if (newDataKey != null)
        {
          result = $"{newDataKey.Id}";
          result += $", {newDataKey.DbId}";
        }
        var compare = "1, 1";
        TestCommon.Write($"{methodName}1", result, compare);
        if (null == newDataKey)
        {
          break;
        }

        result = newDataKey.DataTableId.ToString();
        result += $", {newDataKey.DataTableDbId}";
        compare = "2, 2";
        TestCommon.Write($"{methodName}2", result, compare);

        result = newDataKey.Name;
        result += $", {newDataKey.KeyType}";
        compare = "Name, 1";
        TestCommon.Write($"{methodName}3", result, compare);

        result = $"{newDataKey.IsAscending}";
        result += $", {newDataKey.IsClustered}";
        compare = "True, True";
        TestCommon.Write($"{methodName}4", result, compare);

        result = newDataKey.SourceColumnName;
        result += $", {newDataKey.TargetTableName}";
        result += $", {newDataKey.TargetColumnName}";
        compare = "SourceColumnName, TargetTableName, TargetColumnName";
        TestCommon.Write($"{methodName}5", result, compare);
        break;
      }
    }

    // Provides the default Sort functionality.
    private void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataKey = new DataKey
      {
        DbId = 1,
        Id = 1,
      };
      var toDataKey = new DataKey
      {
        DbId = 2,
        Id = 2,
      };

      var result = dataKey.CompareTo(toDataKey).ToString();
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = toDataKey.CompareTo(dataKey).ToString();
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataKey.DbId = 1;
      toDataKey.Id = 1;
      result = dataKey.CompareTo(toDataKey).ToString();
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    private void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataKey = new DataKey
      {
        DbId = 1,
        Id = 1,
        DataTableDbId = 2,
        DataTableId = 2,
        Name = "Name",
        KeyType = 1,
        IsAscending = true,
        IsClustered = true,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };

      // Test Method
      dataKey.LJCSetOriginalValues();

      dataKey.DbId = 1;
      dataKey.Id = 1;
      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableDbId = 2;
      dataKey.DataTableId = 2;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}2", result, compare);

      dataKey.Name = "Name";
      dataKey.KeyType = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}3", result, compare);

      dataKey.IsAscending = true;
      dataKey.IsClustered = true;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}4", result, compare);

      dataKey.SourceColumnName = "SourceColumnName";
      dataKey.TargetTableName = "TargetTableName";
      dataKey.TargetColumnName = "TargetColumnName";
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}5", result, compare);
    }

    // The object string identifier.
    private void TestToString()
    {
      var methodName = "TestToString()";

      var dataKey = new DataKey
      {
        DbId = 1,
        Id = 1,
        Name = "Name",
      };

      // Test Method
      var result = dataKey.ToString();

      var compare = "Name:1";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // No changes.
    private void NoChange()
    {
      var methodName = "NoChange()";

      var dataKey = new DataKey();

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change DbId
    private void DbId()
    {
      var methodName = "DbId()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        DbId = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DbId = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DbId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Id
    private void Id()
    {
      var methodName = "ID()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        Id = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.Id = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "Id";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataTableDbId
    private void DataTableDbId()
    {
      var methodName = "DataTableDbId()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        DataTableDbId = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableDbId = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DataTableDbId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataTableId
    private void DataTableId()
    {
      var methodName = "DataTableId()";

      var dataKey = new DataKey
      {
        // Original Value
        DataTableId = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableId = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DataTableId";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    private void Name()
    {
      var methodName = "Name()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        Name = "",
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.Name = "Name";
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "Name";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change KeyType
    private void KeyType()
    {
      var methodName = "KeyType()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        KeyType = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.KeyType = 2;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "KeyType";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change IsAscending
    private void IsAscending()
    {
      var methodName = "IsAscending()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        IsAscending = false,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.IsAscending = true;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "IsAscending";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change IsClustered
    private void IsClustered()
    {
      var methodName = "IsClustered()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        IsClustered = false,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.IsClustered = true;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "IsClustered";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change SourceColumnName
    private void SourceColumnName()
    {
      var methodName = "SourceColumnName()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value.
        SourceColumnName = "",
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.SourceColumnName = "SourceColumnName";
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "SourceColumnName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TargetTableName
    private void TargetTableName()
    {
      var methodName = "TargetTableName()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        TargetTableName = null,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.TargetTableName = "TargetTableName";
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "TargetTableName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TargetColumnName
    private void TargetColumnName()
    {
      var methodName = "TargetColumnName()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        TargetColumnName = null,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.TargetColumnName = "TargetColumnName";
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "TargetColumnName";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion
  }
}
