// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfigsProgram.cs
using LJCDataAccessConfig5;
using LJCNetCommon5;

namespace TestDataConfigs5
{
  // The entry class.
  internal class DataConfigsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataConfigs");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataConfigs ***");

      // Static Methods
      DataConfig();

      // Constructor Methods
      LoadData();

      // Collection Methods
      Add();
      Retrieve();
      Save();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Gets a DataConfig from the DataConfigs.xml file.
    private static void DataConfig()
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
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

    private static void LoadData()
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
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and adds the object from the supplied valus.
    private static void Add()
    {
      var methodName = "Add()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Test Method
      var name = "TestConfig";
      var dbServer = "Machine_Name\\SQL_Instance_Name";
      var database = "Database_Name";
      var connectionType = "SQLServer";
      dataConfigs.Add(name, dbServer, database, connectionType);

      var dataConfig = dataConfigs.Retrieve("TestConfig");
      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "TestConfig";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieve the data configuration.
    private static void Retrieve()
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
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Saves the config data.
    private static void Save()
    {
      var methodName = "Save()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      var name = "TestConfig";
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
      var dataConfigName = "TestConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);
      var result = "";
      if (dataConfig != null)
      {
        result = dataConfig.Name;
      }
      var compare = "TestConfig";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
