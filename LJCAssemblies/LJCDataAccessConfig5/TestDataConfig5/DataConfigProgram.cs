// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfigProgram.cs
using LJCDataAccessConfig5;
using LJCNetCommon5;

namespace TestDataConfig5
{
  // The entry class.
  internal class DataConfigProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataConfig");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataConfig ***");

      // Static Methods
      ProviderName();

      // Data Class Methods
      ToStringMethod();
      CompareTo();

      // Methods
      ConnectionString();
      ConnectionStringFromTemplate();
      SQLIntegratedConnectionString();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Retrieves the provider name value.
    private static void ProviderName()
    {
      var methodName = "ProviderName()";

      var result = LJCDataConfig.ProviderName("SQLServer");
      var compare = "System.Data.SqlClient";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Class Methods

    // The object string value.
    private static void ToStringMethod()
    {
      var methodName = "ToStringMethod()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();
      // Retrieve default config.
      // Default ConnectionType = "SQLServer";
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);

      // Test Method
      var result = dataConfig.ToString();
      var compare = "DataConfig";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var methodName = "CompareTo()";

      var dataConfig = new LJCDataConfig()
      {
        Name = "TestTemplate",
      };
      var other = new LJCDataConfig()
      {
        Name = "TestTemplate",
      };

      // Test Method
      var value = dataConfig.CompareTo(other);
      var result = value.ToString();
      var compare = LJCNetString.CompareEqual.ToString();
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // 
    private static void ConnectionString()
    {
      var methodName = "ConnectionString()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();
      // Retrieve default config.
      // Default ConnectionType = "SQLServer";
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);

      // Test Method
      // Creates ConnectionTemplates.xml if not found.
      var result = dataConfig.ConnectionString(dataConfig.ConnectionType);
      var compare = "Data Source=Machine_Name\\SQL_Instance_Name;Initial ";
      compare += "Catalog=Database_Name;Integrated Security=True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates the populated connection string from the template text.
    private static void ConnectionStringFromTemplate()
    {
      var methodName = "ConnectionStringFromTemplate()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();
      // Retrieve default config.
      // Default ConnectionType = "SQLServer";
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);

      // Test Method
      // Normally let dataConfig.ConnectionString() get connectionTemplate.
      // This is called for direct method testing.
      LJCConnectionTemplates connectionTemplates = [];
      connectionTemplates.LoadData();
      var connectionTemplate
        = connectionTemplates.Retrieve(dataConfig.ConnectionType);
      var result = "";
      if (connectionTemplate != null)
      {
        result
          = dataConfig.ConnectionStringFromTemplate(connectionTemplate.Template);
      }
      var compare = "Data Source=Machine_Name\\SQL_Instance_Name;Initial ";
      compare += "Catalog=Database_Name;Integrated Security=True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates the populated connection string from the template text.
    private static void SQLIntegratedConnectionString()
    {
      var methodName = "SQLIntegratedConnectionString()";

      // ConfigFileSpec defaults to "DataConfigs.xml";
      var dataConfigs = new LJCDataConfigs()
      {
        ConfigFileSpec = "TestDataConfigs.xml"
      };

      // Creates default config file if not found.
      // Default config file contains "DataConfig" data configuration.
      dataConfigs.LoadData();
      // Retrieve default config.
      // Default ConnectionType = "SQLServer";
      var dataConfigName = "DataConfig";
      var dataConfig = dataConfigs.Retrieve(dataConfigName);

      // Test Method
      var result = dataConfig.SQLIntegratedConnectionString();
      var compare = "Data Source=Machine_Name\\SQL_Instance_Name;Initial ";
      compare += "Catalog=Database_Name;Integrated Security=True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
