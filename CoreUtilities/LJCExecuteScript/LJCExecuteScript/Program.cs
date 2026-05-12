// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using System;
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCNetCommon;

namespace LJCExecuteScript
{
	// A command line program to execute a database script file.
	///  <include path='items/Program/*' file='../../LJCGenDoc/Common/Program.xml'/>
	public class Program
	{
		// The main entry point method for the application.
		///  <include path='items/Main/*' file='../../LJCGenDoc/Common/Program.xml'/>
		private static void Main(string[] args)
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

      // Execute commands.
      if (!errors)
      {
        DataAccess dataAccess = new DataAccess(connectionString
        , "System.Data.SqlClient");
        try
        {
          dataAccess.ExecuteScript(args[0]);
        }
        catch (Exception e)
        {
          errors = true;
          Console.WriteLine(e.Message);
        }
      }

      if (errors)
      {
        Console.ReadKey();
      }
    }
  }
}
