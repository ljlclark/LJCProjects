using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCNetCommon;

namespace LJCDataAccessTest
{
  // Program to test the LJCDataAccess object.
  internal class ProgramDataAccessTest
  {
    // The program entry point function.
    static void Main(string[] args)
    {
      TestDataAccess();

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Main Test Method Calls

    // Test LJCDataAccess.
    private static void TestDataAccess()
    {
      // Get application settings.
      var appSettings = new AppSettings("LJCDataAccessTest.exe.config");
      string dataConfigName = appSettings.GetString("DataConfigName");

      // Get the DataConfig.
      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      var dataConfig = dataConfigs.LJCGetByName(dataConfigName);

      // Create the DataAccess object.
      var connectionString = dataConfig.GetConnectionString();
      var providerName = dataConfig.GetProviderName();

      var connectionBuilder = new DbConnectionStringBuilder()
      {
        { "Provider", "Microsoft.ACE.OLEDB.12.0" },
        { "Data Source", "Names.txt" },
        { "Extended Properties", "text" },
        { "HDR", "yes" },
        { "FMT", "Delimited" }
      };
      connectionString = connectionBuilder.ToString();
      providerName = "Microsoft.ACE.OLEDB.12.0";

      var dataAccess = new DataAccess(connectionString, providerName);

      DataAccessTest test = new DataAccessTest(dataAccess);

      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("insert into Person");
      builder.AppendLine(" (Name, PrincipleFlag)");
      builder.AppendLine("values('TestRecord', 1)");
      string sql = builder.ToString();
      test.ExecuteNonQuery(sql);

      test.GetDataReader();
      test.GetDataTable();
      test.GetDataSet();
      test.GetProcedureDataTable();
      test.FillDataTable();
      test.GetSchemaOnly();

      builder = new StringBuilder(64);
      builder.AppendLine("update Person");
      builder.AppendLine("set Name='UpdatedRecord'");
      builder.AppendLine("where Name='TestRecord'");
      sql = builder.ToString();
      test.ExecuteNonQuery(sql);

      builder = new StringBuilder(64);
      builder.AppendLine("delete from Person");
      builder.AppendLine("where Name='UpdatedRecord'");
      sql = builder.ToString();
      test.ExecuteNonQuery(sql);

      test.ExecuteScript();
      test.ExecuteScriptText();
    }
    #endregion
  }

  /// <summary>A Person DataObject class.</summary>
  public class Person
  {
    /// <summary>Gets or sets the ID value.</summary>
    public long Id { get; set; }

    /// <summary>Gets or sets the Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Principle Flag value.</summary>
    public bool PrincipleFlag { get; set; }
  }

  /// <summary>Represents a collection of Person DataObjects.</summary>
  public class Persons : List<Person> { }
}
