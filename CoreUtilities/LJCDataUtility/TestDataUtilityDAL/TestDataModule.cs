// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataModule.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataModule object.
  internal class TestDataModule
  {
    // Initializes an object instance.
    public TestDataModule()
    {
      TestCommon = new TestCommon("TestDataModule");
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
      Name();
      Description();
    }

    #region Data Object Methods

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var dataModule = new DataModule
      {
        ID = 1,
        DataSiteID = 2,
        Name = "Name",
        Description = "Description",
      };

      // Test Method
      var newDataModule = dataModule.Clone();

      var result = newDataModule.ID.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataModule.DataSiteID.ToString();
      compare = "2";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataModule.Name;
      compare = "Name";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newDataModule.Description;
      compare = "Description";
      TestCommon.Write($"{methodName}4", result, compare);
    }

    // Provides the default Sort functionality.
    public void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataModule = new DataModule
      {
        ID = 1
      };
      var toDataModule = new DataModule
      {
        ID = 2
      };

      var result = dataModule.CompareTo(toDataModule).ToString();
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = toDataModule.CompareTo(dataModule).ToString();
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toDataModule.ID = 1;
      result = dataModule.CompareTo(toDataModule).ToString();
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }

    // Initializes the original values.
    public void LJCSetOriginalValues()
    {
      var methodName = "SetOriginalValues()";

      var dataModule = new DataModule
      {
        ID = 1,
        DataSiteID = 2,
        Name = "Name",
        Description = "Description",
      };

      // Test Method
      dataModule.LJCSetOriginalValues();

      dataModule.ID = 1;
      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataModule.DataSiteID = 2;
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}2", result, compare);

      dataModule.Name = "Name";
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}3", result, compare);

      dataModule.Description = "Description";
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "No Result";
      TestCommon.Write($"{methodName}4", result, compare);
    }

    // The object string identifier.
    public void TestToString()
    {
      var methodName = "TestToString()";

      var dataModule = new DataModule
      {
        ID = 1,
        DataSiteID = 2,
        Name = "Name",
      };

      // Test Method
      var result = dataModule.ToString();

      var compare = "Name:1";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // No changes.
    public void NoChange()
    {
      var methodName = "NoChange()";

      var dataModule = new DataModule();

      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Change ID
    public void ID()
    {
      var methodName = "ID()";

      // Test Method
      var dataModule = new DataModule
      {
        ID = 0,
      };

      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataModule.ID = 1;
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "ID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DataSiteID
    public void DataSiteID()
    {
      var methodName = "DataSiteID()";

      // Test Method
      var dataModule = new DataModule
      {
        DataSiteID = 0,
      };

      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataModule.DataSiteID = 1;
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "DataSiteID";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Name
    public void Name()
    {
      var methodName = "Name()";

      // Test Method
      var dataModule = new DataModule
      {
        Name = "",
      };

      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataModule.Name = "Name";
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "Name";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change Description
    public void Description()
    {
      var methodName = "Description()";

      // Test Method
      var dataModule = new DataModule
      {
        Description = "",
      };

      var result = dataModule.ChangedNames.ChangedPropertyNames;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      dataModule.Description = "Description";
      result = dataModule.ChangedNames.ChangedPropertyNames;
      compare = "Description";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
