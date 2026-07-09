// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumn.cs
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace TestDataUtilityDAL
{
  // Tests the DataUtilTable object.
  internal class TestDataColumn
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumn()
    {
      TestCommon = new TestCommon("TestDataColumn");
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
      Description();
      Sequence();
      AllowNull();
      DefaultValue();
      IdentityStart();
      IdentityIncrement();
      MaxLength();
      NewName();
      NewMaxLength();
      TypeName();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var dataColumn = new DataUtilColumn
      {
        ID = 1,
        DataSiteID = 1,
        DataTableID = 2,
        DataTableSiteID = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        AllowNull = false,
        DefaultValue = "DefaultValue",
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = "NewName",
        NewMaxLength = 1,
        TypeName = "TypeName",
      };

      // Test Method
      var newDataColumn = dataColumn.Clone();

      var result = $"{newDataColumn.ID}";
      result += $", {newDataColumn.DataSiteID}";
      var compare = "1, 1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{newDataColumn.DataTableID}";
      result += $", {newDataColumn.DataTableSiteID}";
      compare = "2, 2";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataColumn.Name;
      result += $", {newDataColumn.Description}";
      compare = "Name, Description";
      TestCommon.Write($"{methodName}3", result, compare);

      result = $"{newDataColumn.Sequence}";
      result += $", {newDataColumn.AllowNull}";
      result += $", {newDataColumn.DefaultValue}";
      compare = "1, False, DefaultValue";
      TestCommon.Write($"{methodName}4", result, compare);

      result = $"{newDataColumn.IdentityStart}";
      result += $", {newDataColumn.IdentityIncrement}";
      compare = "1, 1";
      TestCommon.Write($"{methodName}5", result, compare);

      result = $"{newDataColumn.MaxLength}";
      result += $", {newDataColumn.NewName}";
      compare = "1, NewName";
      TestCommon.Write($"{methodName}6", result, compare);

      result = $"{newDataColumn.NewMaxLength}";
      result += $", {newDataColumn.TypeName}";
      compare = "1, TypeName";
      TestCommon.Write($"{methodName}7", result, compare);
    }

    // Provides the default Sort functionality.
    public void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataColumn = new DataUtilColumn
      {
        ID = 1,
        DataSiteID = 1,
      };
      var toDataColumn = new DataUtilColumn
      {
        ID = 2,
        DataSiteID = 2,
      };

      var result = $"{dataColumn.CompareTo(toDataColumn)}";
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = $"{toDataColumn.CompareTo(dataColumn)}";
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataColumn.ID = 1;
      result = $"{dataColumn.CompareTo(toDataColumn)}";
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    public void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataColumn = new DataUtilColumn
      {
        ID = 1,
        DataSiteID = 1,
        DataTableID = 2,
        DataTableSiteID = 2,
        Name = "Name",
        Description = "Description",
        Sequence = 1,
        AllowNull = false,
        DefaultValue = "DefaultValue",
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = "NewName",
        NewMaxLength = 1,
        TypeName = "TypeName",
      };

      // Test Method
      dataColumn.LJCSetOriginalValues();

      dataColumn.ID = 1;
      dataColumn.DataSiteID = 1;
      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.DataTableID = 2;
      dataColumn.DataTableSiteID = 2;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}2", result, compare);

      dataColumn.Name = "Name";
      dataColumn.Description = "Description";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}3", result, compare);

      dataColumn.Sequence = 1;
      dataColumn.AllowNull = false;
      dataColumn.DefaultValue = "DefaultValue";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}4", result, compare);

      dataColumn.IdentityStart = 1;
      dataColumn.IdentityIncrement = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}5", result, compare);

      dataColumn.MaxLength = 1;
      dataColumn.NewName = "NewName";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}6", result, compare);

      dataColumn.NewMaxLength = 1;
      dataColumn.TypeName = "TypeName";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}7", result, compare);
    }

    // The object string identifier.
    public void TestToString()
    {
      var methodName = "TestToString()";

      var dataColumn = new DataUtilColumn
      {
        ID = 1,
        Name = "Name",
      };

      // Test Method
      var result = dataColumn.ToString();

      var compare = "Name:1";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // No changes.
    public void NoChange()
    {
      var methodName = "NoChange()";

      var dataColumn = new DataUtilColumn();

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change ID
    public void ID()
    {
      var methodName = "ID()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        ID = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.ID = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "ID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataSiteID()
    {
      var methodName = "DataSiteID()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        DataSiteID = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.DataSiteID = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "DataSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataTableID
    public void DataTableID()
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

    // Change DataTableSiteID
    public void DataTableSiteID()
    {
      var methodName = "DataTableSiteID()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        DataTableSiteID = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.DataTableSiteID = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "DataTableSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    public void Name()
    {
      var methodName = "Name()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        Name = "",
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.Name = "Name";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "Name";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Description
    public void Description()
    {
      var methodName = "Description()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        Description = null,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.Description = "Description";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "Description";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Sequence
    public void Sequence()
    {
      var methodName = "Sequence()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        Sequence = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.Sequence = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "Sequence";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change AllowNull
    public void AllowNull()
    {
      var methodName = "AllowNull()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        AllowNull = false,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.AllowNull = true;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "AllowNull";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DefaultValue
    public void DefaultValue()
    {
      var methodName = "DefaultValue()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        DefaultValue = null,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.DefaultValue = "DefaultValue";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "DefaultValue";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change IdentityStart
    public void IdentityStart()
    {
      var methodName = "IdentityStart()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        IdentityStart = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.IdentityStart = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "IdentityStart";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change IdentityIncrement
    public void IdentityIncrement()
    {
      var methodName = "IdentityIncrement()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        IdentityIncrement = 1,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.IdentityIncrement = 2;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "IdentityIncrement";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change MaxLength
    public void MaxLength()
    {
      var methodName = "MaxLength()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        MaxLength = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.MaxLength = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "MaxLength";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change NewName
    public void NewName()
    {
      var methodName = "NewName()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        NewName = null,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.NewName = "NewName";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "NewName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change NewMaxLength
    public void NewMaxLength()
    {
      var methodName = "NewMaxLength()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        NewMaxLength = 0,
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.NewMaxLength = 1;
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "NewMaxLength";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TypeName
    public void TypeName()
    {
      var methodName = "TypeName()";

      // Test Method
      var dataColumn = new DataUtilColumn
      {
        TypeName = "",
      };

      var result = dataColumn.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataColumn.TypeName = "TypeName";
      result = dataColumn.ChangedNames.ChangedPropertyNames;
      compare = "TypeName";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
