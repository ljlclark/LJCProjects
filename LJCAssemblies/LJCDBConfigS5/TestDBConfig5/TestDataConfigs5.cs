// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataConfigs5.cs
using LJCDBConfig5;
using LJCNetCommon5;

namespace TestDBConfig5
{
  // Provides LJCDataConfigs test methods.
  internal class TestDataConfigs
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataConfigs()
    {
      TestCommon = new LJCTestCommon("LJCDataConfigs");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Static Methods
      DataConfig();

      // Constructor Test Methods
      LoadData();

      // Collection Methods
      Add();
      Retrieve();
      Save();
    }
    #endregion

    #region Static Methods

    // Gets a DataConfig from the DataConfigs.xml file.
    private void DataConfig()
    {
      var methodName = "DataConfig()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };
      File.Delete("TestDataConfigs.xml");

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();

      // Test Method
      var dataConfigName = "DataConfig";
      var dataConfig = LJCDataConfigs.DataConfig(dataConfigName);

      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "DataConfig";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Test Methods

    // Retrieves the config data.
    private void LoadData()
    {
      var methodName = "LoadData()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };
      File.Delete("TestDataConfigs.xml");

      // Test Method
      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();

      // Retrieve default config.
      // Default ConnectionType = "SQLServer";
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);
      var result = dataConfig.Name;
      var compare = "DataConfig";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the supplied valus.
    private void Add()
    {
      var methodName = "Add()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Test Method
      var name = "DataConfig";
      var dbServer = "Machine_Name\\SQL_Instance_Name";
      var database = "Database_Name";
      var connectionType = "SQLServer";
      dataConfigs.Add(name, dbServer, database, connectionType);

      var dataConfig = dataConfigs.Retrieve("DataConfig");
      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "DataConfig";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieve the data configuration.
    private void Retrieve()
    {
      var methodName = "Retrieve()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };
      File.Delete("TestDataConfigs.xml");

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();

      // Test Method
      var dataConfig = dataConfigs.Retrieve("DataConfig");

      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "DataConfig";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Saves the config data.
    private void Save()
    {
      var methodName = "Save()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      var name = "DataConfig";
      var dbServer = "Machine_Name\\SQL_Instance_Name";
      var database = "Database_Name";
      var connectionType = "SQLServer";
      dataConfigs.Add(name, dbServer, database, connectionType);

      // Test Method
      dataConfigs.Save();

      dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };
      dataConfigs.LoadData();
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);
      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "DataConfig";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
