// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAccessProgram.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCNetCommon5;
using System.Data;
using System.Data.Common;

namespace TestDataAccess5
{
  // The entry class.
  internal class DataAccessProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataAccess");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataAccess ***");

      // Static Methods
      GetConnectionString();
      GetDataAccess();
      IsUseCommand();

      // Constructor Methods
      ConstructorParam();

      // Methods
      CloseConnection();
      ExecuteNonQuery();
      ExecuteScript();
      ExecuteScriptText();
      FillDataTable();
      GetDataReader();
      GetDataSet();
      GetDataTable();
      GetProcedureDataTable();
      GetSchemaOnly();
      GetColumnSQLTypes();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Creates a connection string.
    private static void GetConnectionString()
    {
      var methodName = "GetConnectionString()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Test Method
      var userID = "userID";
      var password = "password";
      string[] others =
      [
        "Encrypt|False",
      ];
      var result = LJCDataAccess.GetConnectionString(dataSourceName
        , databaseName, userID, password, others);
      var compare = "Data Source=DESKTOP-PDPBE34;Initial Catalog=LJCData;";
      compare += "User Id=userID;Password=password;Encrypt=False";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates the DataAccess object.
    private static void GetDataAccess()
    {
      var methodName = "GetDataAccess()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: Access, MySql, Odbc, OleDb, SqlServer.
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      var result = "";
      if (dataAccess != null
        && dataAccess.ProviderName == "Microsoft.Data.SqlClient")
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Checks for the "use" command.
    private static void IsUseCommand()
    {
      var methodName = "IsUseCommand()";

      var command = "use[Database]";
      var value = LJCDataAccess.IsUseCommand(command);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private static void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";
      string[] addPairs =
      [
        "Encrypt|False",
      ];

      // Available connection types: Access, MySql, Odbc, OleDb, SqlServer.
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);
      var connectionString = LJCDataAccess.GetConnectionString(dataSourceName
        , databaseName, pairs: addPairs);

      // Test Method
      var dataAccess = new LJCDataAccess(connectionString, providerName);

      var result = "";
      if (dataAccess != null
        && dataAccess.ProviderName == "Microsoft.Data.SqlClient")
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Closes the database connection.
    private static void CloseConnection()
    {
      var methodName = "CloseConnection()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      var sql = "select * from Person ";
      DbDataReader? dbDataReader;
      var result = "";
      var compare = "";
      using (dbDataReader = dataAccess.GetDataReader(sql))
      {
        if (dbDataReader != null
          && dbDataReader.Read())
        {
          result = "Open";
        }
        compare = "Open";
        TestCommon?.Write($"{methodName}1", result, compare);

        // Test Method
        dataAccess.CloseConnection();
        result = "Closed";
        if (dbDataReader != null
          && !dbDataReader.IsClosed)
        {
          result = "Open";
        }
        compare = "Closed";
        TestCommon?.Write($"{methodName}2", result, compare);
      }
    }

    // Executes an Insert, Update or Delete statement.
    private static void ExecuteNonQuery()
    {
      var methodName = "ExecuteNonQuery()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      // Test Method
      var sql = "update Person set Name = 'John Doe Updated'";
      sql += " where Name = 'John Doe';";
      var affectedCount = dataAccess.ExecuteNonQuery(sql);
      var result = affectedCount.ToString();
      var compare = "1";
      TestCommon?.Write($"{methodName}1", result, compare);

      sql = "select * from Person";
      sql += " where Name = 'John Doe Updated'";
      var personTable = dataAccess.GetDataTable(sql);
      result = "";
      if (LJC.HasData(personTable))
      {
        result = "True";
      }
      compare = "True";
      TestCommon?.Write($"{methodName}2", result, compare);

      sql = "update Person set Name = 'John Doe'";
      sql += " where Name = 'John Doe Updated';";
      dataAccess.ExecuteNonQuery(sql);
    }

    // Executes a script file.
    private static void ExecuteScript()
    {
      var methodName = "ExecuteScript()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      var tb = new LJCTextBuilder();
      tb.AddLine("insert ");
      tb.AddLine("into Person ");
      tb.AddLine("(");
      tb.AddLine("  Name,");
      tb.AddLine("  PrincipleFlag");
      tb.AddLine(")");
      tb.AddLine("values ");
      tb.AddLine("(");
      tb.AddLine("  'Name 1',");
      tb.AddLine("  0");
      tb.AddLine(");");
      tb.AddLine("go");
      tb.AddLine("insert ");
      tb.AddLine("into Person ");
      tb.AddLine("(");
      tb.AddLine("  Name,");
      tb.AddLine("  PrincipleFlag");
      tb.AddLine(")");
      tb.AddLine("values ");
      tb.AddLine("(");
      tb.AddLine("  'Name 2',");
      tb.AddLine("  1");
      tb.AddLine(");");
      var sql = tb.ToString();
      File.WriteAllText("TestScript.txt", sql);

      // Test Method
      dataAccess.ExecuteScript("TestScript.txt");
      File.Delete("TestScript.txt");

      // Verify script was executed.
      tb = new LJCTextBuilder();
      tb.AddLine("select * ");
      tb.AddLine("from Person ");
      tb.AddLine("where Name = 'Name 1';");
      sql = tb.ToString();
      var resultTable = dataAccess.GetDataTable(sql);
      var result = "";
      if (resultTable != null)
      {
        var row = resultTable.Rows[0];
        result = row["Name"].ToString();
      }
      var compare = "Name 1";
      TestCommon?.Write($"{methodName}1", result, compare);

      // Clear test data
      sql = "delete from Person where Name = 'Name 1';";
      dataAccess.ExecuteNonQuery(sql);
      sql = "delete from Person where Name = 'Name 2';";
      dataAccess.ExecuteNonQuery(sql);

      // Verify data was deleted.
      sql = "select * from Person where Name = 'Name 1';";
      resultTable = dataAccess.GetDataTable(sql);
      result = "True";
      if (LJC.HasData(resultTable))
      {
        result = "False";
      }
      compare = "True";
      TestCommon?.Write($"{methodName}2", result, compare);

      sql = "select * from Person where Name = 'Name 2';";
      resultTable = dataAccess.GetDataTable(sql);
      result = "True";
      if (LJC.HasData(resultTable))
      {
        result = "False";
      }
      compare = "True";
      TestCommon?.Write($"{methodName}3", result, compare);
    }

    // Executes a script text string.
    private static void ExecuteScriptText()
    {
      var methodName = "ExecuteScriptText()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      var tb = new LJCTextBuilder();
      tb.AddLine("insert ");
      tb.AddLine("into Person ");
      tb.AddLine("(");
      tb.AddLine("  Name,");
      tb.AddLine("  PrincipleFlag");
      tb.AddLine(")");
      tb.AddLine("values ");
      tb.AddLine("(");
      tb.AddLine("  'Name 1',");
      tb.AddLine("  0");
      tb.AddLine(");");
      tb.AddLine("go");
      tb.AddLine("insert ");
      tb.AddLine("into Person ");
      tb.AddLine("(");
      tb.AddLine("  Name,");
      tb.AddLine("  PrincipleFlag");
      tb.AddLine(")");
      tb.AddLine("values ");
      tb.AddLine("(");
      tb.AddLine("  'Name 2',");
      tb.AddLine("  1");
      tb.AddLine(");");
      var sql = tb.ToString();

      // Test Method
      dataAccess.ExecuteScriptText(sql);

      // Verify script was executed.
      tb = new LJCTextBuilder();
      tb.AddLine("select * ");
      tb.AddLine("from Person ");
      tb.AddLine("where Name = 'Name 1';");
      sql = tb.ToString();
      var resultTable = dataAccess.GetDataTable(sql);
      var result = "";
      if (resultTable != null)
      {
        var row = resultTable.Rows[0];
        result = row["Name"].ToString();
      }
      var compare = "Name 1";
      TestCommon?.Write($"{methodName}1", result, compare);

      // Clear test data
      sql = "delete from Person where Name = 'Name 1';";
      dataAccess.ExecuteNonQuery(sql);
      sql = "delete from Person where Name = 'Name 2';";
      dataAccess.ExecuteNonQuery(sql);

      // Verify data was deleted.
      sql = "select * from Person where Name = 'Name 1';";
      resultTable = dataAccess.GetDataTable(sql);
      result = "True";
      if (LJC.HasData(resultTable))
      {
        result = "False";
      }
      compare = "True";
      TestCommon?.Write($"{methodName}2", result, compare);

      sql = "select * from Person where Name = 'Name 2';";
      resultTable = dataAccess.GetDataTable(sql);
      result = "True";
      if (LJC.HasData(resultTable))
      {
        result = "False";
      }
      compare = "True";
      TestCommon?.Write($"{methodName}3", result, compare);
    }

    // Executes a Select statement and fills the specified DataTable.
    private static void FillDataTable()
    {
      var methodName = "FillDataTable()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      var sql = "select * from Person";
      var personTable = dataAccess.GetSchemaOnly(sql);

      // Test Method
      if (personTable != null)
      {
        dataAccess.FillDataTable(sql, personTable);
      }
      var result = "";
      if (LJC.HasData(personTable))
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and retrieves the DbDataReader object.
    private static void GetDataReader()
    {
      var methodName = "GetDataReader()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      var sql = "insert into Person values('Test Name', 0)";
      dataAccess.ExecuteNonQuery(sql);

      // Test Method
      sql = "select * from Person ";
      sql += "where Name = 'Test Name';";
      DbDataReader? dbDataReader;
      var result = "";
      using (dbDataReader = dataAccess.GetDataReader(sql))
      {
        if (dbDataReader != null)
        {
          if (dbDataReader.Read())
          {
            var value = dbDataReader[1];
            result = value.ToString();
          }
          dbDataReader.Close();
          dataAccess.CloseConnection();
        }
      }
      var compare = "Test Name";
      TestCommon?.Write($"{methodName}", result, compare);

      // Clear test data
      sql = "delete from Person where Name = 'Test Name';";
      dataAccess.ExecuteNonQuery(sql);
    }

    // Executes a Select statement and retrieves the DataSet object.
    private static void GetDataSet()
    {
      var methodName = "GetDataSet()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      var sql = "insert into Person values('Name 1', 0)";
      dataAccess.ExecuteNonQuery(sql);
      sql = "insert into Person values('Name 2', 0)";
      dataAccess.ExecuteNonQuery(sql);

      // Test Method
      sql = "select * from Person where Name = 'Name 1'";
      sql += ";select * from Person where Name = 'Name 2'";
      var personSet = dataAccess.GetDataSet(sql);

      if (personSet != null)
      {
        DataTable table = personSet.Tables[0];
        DataRow row = table.Rows[0];
        var result = row["Name"].ToString();
        var compare = "Name 1";
        TestCommon?.Write($"{methodName}1", result, compare);

        table = personSet.Tables[1];
        row = table.Rows[0];
        result = row["Name"].ToString();
        compare = "Name 2";
        TestCommon?.Write($"{methodName}2", result, compare);
      }
    }

    // Executes a Select statement and retrieves the DataTable object.
    private static void GetDataTable()
    {
      var methodName = "GetDataTable()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      // Test Method
      var sql = "select * from Person";
      var personTable = dataAccess.GetDataTable(sql);
      var result = "";
      if (LJC.HasData(personTable))
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Stored Procedure and retrieves the DataTable object.
    private static void GetProcedureDataTable()
    {
      var methodName = "GetProcedureDataTable()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);
      dataAccess.TableName = "Person";

      var tb = new LJCTextBuilder();
      tb.AddLine("USE[Database]");
      tb.AddLine("GO");
      tb.AddLine("SET ANSI_NULLS ON");
      tb.AddLine("GO");
      tb.AddLine("SET QUOTED_IDENTIFIER ON");
      tb.AddLine("GO");
      tb.AddLine("IF OBJECT_ID('[dbo].[sp_TestProc]', N'p')");
      tb.AddLine(" IS NOT NULL");
      tb.AddLine("  DROP PROCEDURE[dbo].[sp_TestProc];");
      tb.AddLine("GO");
      tb.AddLine("CREATE PROCEDURE[dbo].[sp_TestProc]");
      tb.AddLine("  @name nvarchar(60)");
      tb.AddLine("AS");
      tb.AddLine("BEGIN");
      tb.AddLine("select * from Person ");
      tb.AddLine("where Name = @name;");
      tb.AddLine("END");
      var sql = tb.ToString();
      dataAccess.ExecuteScriptText(sql);

      // Test Method
      var parameter = new LJCProcedureParameter
      {
        Direction = ParameterDirection.Input,
        ParameterName = "name",
        MySqlDbTypeID = (int)LJCMySqlDbType.VarChar,
        SqlDbTypeID = (int)SqlDbType.NVarChar,
        Size = 60,
        Value = "Name 1",
      };
      var parameters = new LJCProcedureParameters
      {
        // ToDo: Solve must load MySql.Data.dll.
        parameter
      };
      var dataTable = dataAccess.GetProcedureDataTable("sp_TestProc"
        , parameters);
      var result = "";
      if (LJC.HasData(dataTable))
      {
        var row = dataTable.Rows[0];
        result = row["Name"].ToString();
      }
      var compare = "Name 1";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieves the DataTable object with schema only.
    private static void GetSchemaOnly()
    {
      var methodName = "GetSchemaOnly()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      // Test Method
      var sql = "select * from Person";
      var personTable = dataAccess.GetSchemaOnly(sql);
      var result = "";
      if (LJC.HasColumns(personTable))
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Get the column SQL types.
    private static void GetColumnSQLTypes()
    {
      var methodName = "GetColumnSQLTypes()";

      //var dataSourceName = "Machine_Name\\SQL_Instance_Name";
      //var databaseName = "Database";
      // Local Testing
      var dataSourceName = "DESKTOP-PDPBE34";
      var databaseName = "LJCData";

      // Available connection types: SqlServer, MySql, OleDb.
      // Untested: Access, Odbc
      var connectionType = ConnectionType.SqlServer.ToString();
      var providerName = LJCDataConfig.ProviderName(connectionType);

      var dataAccess = LJCDataAccess.GetDataAccess(dataSourceName
        , databaseName, providerName);

      // Test Method
      var personTable = dataAccess.GetColumnSQLTypes(databaseName, "Person");
      var result = "";
      if (LJC.HasData(personTable))
      {
        result = "True";
      }
      var compare = "True";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
