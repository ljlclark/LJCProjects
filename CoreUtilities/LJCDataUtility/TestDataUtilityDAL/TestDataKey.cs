// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataKey.cs
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace TestDataUtilityDAL
{
  // Tests the DataUtilTable object.
  internal class TestDataKey
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataKey()
    {
      TestCommon = new TestCommon("TestDataKey");
    }

    // Run the tests.
    public void Run()
    {
      // Data Object Methods
      Clone();
      CompareTo();
      LJCSetOriginalValues();
      TestToString();

      // Data Properties
      NoChange();
      ID();
      DataSiteID();
      DataTableID();
      DataTableSiteID();
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

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 1,
        DataTableID = 2,
        DataTableSiteID = 2,
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

      var result = $"{newDataKey.ID}";
      result += $", {newDataKey.DataSiteID}";
      var compare = "1, 1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataKey.DataTableID.ToString();
      result += $", {newDataKey.DataTableSiteID}";
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
    }

    // Provides the default Sort functionality.
    public void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 1,
      };
      var toDataKey = new DataKey
      {
        ID = 2,
        DataSiteID = 2,
      };

      var result = dataKey.CompareTo(toDataKey).ToString();
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = toDataKey.CompareTo(dataKey).ToString();
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataKey.ID = 1;
      toDataKey.DataSiteID = 1;
      result = dataKey.CompareTo(toDataKey).ToString();
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    public void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataKey = new DataKey
      {
        ID = 1,
        DataSiteID = 1,
        DataTableID = 2,
        DataTableSiteID = 2,
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

      dataKey.ID = 1;
      dataKey.DataSiteID = 1;
      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableID = 2;
      dataKey.DataTableSiteID = 2;
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
    public void TestToString()
    {
      var methodName = "TestToString()";

      var dataKey = new DataKey
      {
        ID = 1,
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
    public void NoChange()
    {
      var methodName = "NoChange()";

      var dataKey = new DataKey();

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change ID
    public void ID()
    {
      var methodName = "ID()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        ID = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.ID = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "ID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataSiteID()
    {
      var methodName = "DataSiteID()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        DataSiteID = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataSiteID = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DataSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataModuleID
    public void DataTableID()
    {
      var methodName = "DataTableID()";

      var dataKey = new DataKey
      {
        // Original Value
        DataTableID = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableID = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DataTableID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataTableSiteID()
    {
      var methodName = "DataTableSiteID()";

      // Test Method
      var dataKey = new DataKey
      {
        // Original Value
        DataTableSiteID = 0,
      };

      var result = dataKey.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataKey.DataTableSiteID = 1;
      result = dataKey.ChangedNames.ChangedPropertyNames;
      compare = "DataTableSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    public void Name()
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
    public void KeyType()
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
    public void IsAscending()
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
    public void IsClustered()
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
    public void SourceColumnName()
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
    public void TargetTableName()
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
    public void TargetColumnName()
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

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
