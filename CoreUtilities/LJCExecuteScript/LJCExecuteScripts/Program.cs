using LJCDataAccess;
using LJCDataAccessConfig;
using LJCNetCommon;
using System;
using System.IO;
using System.Runtime;

namespace LJCExecuteScripts
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string connectionString = null;
      bool errors = false;

      // Get data configuration and connection string.
      string dataConfigName = args[1];
      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      DataConfig dataConfig = dataConfigs.LJCGetByName(name: dataConfigName);
      if (null == dataConfig)
      {
        errors = true;
        Console.WriteLine($"Configuration '{dataConfigName}' was not found.");
      }
      else
      {
        connectionString = dataConfig.SQLIntegratedConnectionString();
        NetCommon.ConsoleConfig(dataConfigName);
      }

      if (!errors)
      {
        var sourcePath = args[0];
        //var option = SearchOption.AllDirectories;
        var option = SearchOption.TopDirectoryOnly;
        var fileNames = Directory.GetFiles(sourcePath, "*.sql", option);
        if (fileNames.Length > 0)
        {
          DataAccess dataAccess = new DataAccess(connectionString
            , "System.Data.SqlClient")
          {
            TableName = "TestData"
          };
          foreach (var fileName in fileNames)
          {
            var name = Path.GetFileName(fileName);
            Console.WriteLine(name);
            try
            {
              dataAccess.ExecuteScript(fileName);
            }
            catch (Exception e)
            {
              errors = true;
              Console.WriteLine(e.Message);
              Console.WriteLine(dataAccess.ScriptSQL);
            }
          }
        }
      }

      if (errors)
      {
        Console.ReadKey();
      }
    }
  }
}
