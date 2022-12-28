// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using LJCDataAccessConfig;

namespace ConfigTestConsole
{
  // The program entry point class.
  /// <include path='items/Program/*' file='Doc/ProjectConfigTestConsole.xml'/>
  public class Program
  {
    // The program entry point function.
    /// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
    private static void Main()
    {
      TestDataConfigs();
      TestCreateTemplates();
    }

    // <summary></summary>
    private static void TestDataConfigs()
    {
      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      string dataConfigName = "FacilityManager";
      DataConfig dataConfig = dataConfigs.LJCGetByName(dataConfigName);
      if (dataConfig != null)
      {
        dataConfig.DbServer = dataConfig.B(dataConfig.DbServer);
        //string dbServer = dataConfig.E(dataConfig.DbServer);
      }
    }

    // Creates the template file.
    private static void TestCreateTemplates()
    {
      ConnectionTemplates templates = new ConnectionTemplates
      {
        {
          "SQLServer",
          "Data Source={DbServer}; Initial Catalog={Database}; " +
          "Integrated Security=True"
        },
        { "MySQL", "server={DbServer}; UserId=root; Password=root; database={Database}" },
        {
          "OLEDB",
          "Provider=SQLOLEDB;Data Source={DbServer}\\instance; " +
          "Initial Catalog ={Database}; User Id=myUsername; Password=myPassword"
        },
        {
          "Access",
          "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\myAccessFile.accdb;" +
          "Persist Security Info = False;"
        },
        {
          "ODBC",
          "Driver={SQL Server}; Server=myServerAddress; Database=myDataBase; " +
          "Uid = myUsername; Pwd = myPassword;"
        }
      };
      templates.LJCSave();
    }
  }
}
