// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataModules5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataModules collection.
  internal class TestDataModules
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataModules()
    {
      TestCommon = new LJCTestCommon("TestDataModules");
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
      LJCGetWithName();
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

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);
      dataModules.LJCSerialize();

      // Test Method
      var newDataModules = DataModules.LJCDeserialize();

      var result = "";
      if (newDataModules != null)
      {
        result = newDataModules.Count.ToString();
      }
      var compare = "2";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get custom collection from List<T>.
    private void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataModules = new List<DataModule>();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      var newDataModules = DataModules.LJCGetCollection(dataModules);

      DataModule? testDataModule = null;
      if (newDataModules != null)
      {
        testDataModule = newDataModules["Name"];
      }
      if (null == testDataModule)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataModule != null)
      {
        var result = testDataModule.Name;
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

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      var newDataModules = dataModules.Clone();

      var newDataModule = newDataModules[0];
      var result = $"{newDataModule.DbId}";
      result += $", {newDataModule.Id}";
      var compare = "2, 1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataModule.Name;
      result += $", {newDataModule.Description}";
      compare = "Name, Description";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Checks if the collection has items.
    private void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var dataModules = new DataModules();

      // Test Method
      var result = dataModules.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      result = dataModules.LJCHasItems().ToString();
      compare = "True";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Serializes the collection to a file.
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      dataModules.LJCSerialize();

      var newDataModules = DataModules.LJCDeserialize();

      var result = "";
      if (LJC.HasListItems(newDataModules))
      {
        result = newDataModules.Count.ToString();
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
      var dataModules = new DataModules
      {
        { 1, 2, "Name" },
      };

      var item = dataModules[0];
      var result = item.Name;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item.
    private void LJCGetWithId()
    {
      var methodName = "LJCGetWithId()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      var item = dataModules.LJCGetWithID(2, 1);

      var result = "";
      if (item != null)
      {
        result = item.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the collection item with unique values.
    private void LJCGetWithName()
    {
      var methodName = "LJCGetWithName()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Name = "Name",
        Id = 1,
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      var item = dataModules.LJCGetUnique("Name");

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

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      dataModules.LJCRemove("Name");

      dataModule = dataModules[0];
      var result = dataModule.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sort on ID.
    private void LJCSortId()
    {
      var methodName = "LJCSortID()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      dataModules.LJCSortID();

      dataModule = dataModules[0];
      var result = dataModule.Id.ToString();
      var compare = "1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sort on Unique values.
    private void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      // Test Method
      var comparer = new DataModuleUnique();
      dataModules.LJCSortUnique(comparer);

      dataModule = dataModules[0];
      var result = dataModule.Name;
      var compare = "First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    private void UniqueIndexer()
    {
      string methodName = "UniqueIndexer()";

      var dataModules = new DataModules();
      var dataModule = new DataModule
      {
        DbId = 3,
        Id = 2,
        Name = "Name",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      dataModule = new DataModule
      {
        DbId = 2,
        Id = 1,
        Name = "First",
        Description = "Description",
      };
      dataModules.Add(dataModule);

      var result = "";
      dataModule = dataModules["Name"];
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
