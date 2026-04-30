// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAccess.cs
using System.Data;
using System.Data.Common;
using LJCNetCommon5;
using LJCDataAccessConfig5;
using System.Text;
using Microsoft.Data.SqlClient;

namespace LJCDataAccess5
{
  // Implements an ADO.NET SQL data access control layer.
  /// <include path='items/DataAccess/*' file='Doc/ProjectDataAccess.xml'/>
  public partial class LJCDataAccess
  {
    #region Static Functions

    // Creates a connection string.
    /// <include path='items/GetConnectionString/*' file='Doc/DataCommon.xml'/>
    public static string GetConnectionString(string dataSourceName
      , string databaseName, string? userID = null, string? password = null
      , params string[] pairs)
    {
      string retValue;

      RegisterSqlClient();
      var dataSource = GetPair("Data Source", dataSourceName);
      var database = GetPair("Initial Catalog", databaseName);
      var connectionBuilder = new DbConnectionStringBuilder()
      {
        { dataSource[0], dataSource[1] },
        { database[0], database[1] },
      };
      if (null == userID
        || null == password)
      {
        connectionBuilder.Add("Integrated Security", "True");
        connectionBuilder.Add("Encrypt", "False");
      }
      else
      {
        var pair = GetPair("User Id", userID);
        connectionBuilder.Add(pair[0], pair[1]);
        pair = GetPair("Password", password);
        connectionBuilder.Add(pair[0], pair[1]);
      }

      // Get any additional pairs separated with "|".
      if (pairs != null)
      {
        foreach (var value in pairs)
        {
          var pair = GetPair("", value);
          if (pair != null
            && 2 == pair.Length)
          {
            connectionBuilder.Add(pair[0], pair[1]);
          }
        }
      }
      retValue = connectionBuilder.ConnectionString;
      return retValue;
    }

    // Creates the DataAccess object.
    /// <include path='items/GetDataAccess/*' file='Doc/DataCommon.xml'/>
    public static LJCDataAccess GetDataAccess(string dataSourceName
      , string databaseName, string providerName = "Microsoft.Data.SqlClient")
    {
      LJCDataAccess retValue;

      RegisterSqlClient();
      string connectionString = GetConnectionString(dataSourceName
        , databaseName);
      retValue = new LJCDataAccess(connectionString, providerName);
      return retValue;
    }

    /// <summary>Checks for the "use" command.</summary>
    public static bool IsUseCommand(string sql)
    {
      var retValue = false;

      var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
      //var index = sql.IndexOf("use", ignoreCase);
      if (sql.Contains("use", ignoreCase))
      {
        retValue = true;
      }
      return retValue;
    }

    public static void RegisterSqlClient()
    {
      DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient"
        , Microsoft.Data.SqlClient.SqlClientFactory.Instance);
    }

    // Get a Connection String element pair.
    private static string[] GetPair(string text, string pair)
    {
      string[] retValue = pair.Split('|');
      if (retValue.Length < 2)
      {
        //retValue = new string[] { text, pair };
        retValue = [text, pair];
      }
      return retValue;
    }

    // Returns the default SQL Server provider name.
    private static string GetSQLServerProviderName()
    {
      string retValue;

      string connectionTypeName = ConnectionType.SqlServer.ToString();
      retValue = LJCDataConfig.ProviderName(connectionTypeName);
      return retValue;
    }

    // Retrieves the Table name from an SQL statement.
    private static string GetTableName(string sql, string tableName)
    {
      string retValue = tableName;

      var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
      var index = sql.IndexOf(" from ", ignoreCase);
      if (index > -1)
      {
        var beginIndex = index + " from ".Length;
        var endIndex = sql.IndexOf(' ', beginIndex);
        if (-1 == endIndex)
        {
          endIndex = sql.Length;
        }
        //retValue = sql.Substring(beginIndex, endIndex - beginIndex);
        retValue = sql[beginIndex..endIndex];
        retValue = retValue.Trim();
      }
      return retValue;
    }

    // Checks for MySql provider.
    private static bool IsMySql(string providerName)
    {
      bool retValue = false;

      if ("MySql.Data.MySqlClient" == providerName)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDataAccess()
    {
      RegisterSqlClient();
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DataAccessC/*' file='Doc/DataAccess.xml'/>
    public LJCDataAccess(string connectionString, string? providerName = null)
    {
      RegisterSqlClient();
      ConnectionString = connectionString;
      ProviderName = providerName;
    }
    #endregion

    #region Methods

    // Closes the database connection.
    /// <include path='items/CloseConnection/*' file='Doc/DataAccess.xml'/>
    public void CloseConnection()
    {
      ProviderFactory?.CloseConnection();
    }

    // Executes an Insert, Update or Delete statement.
    /// <include path='items/ExecuteNonQuery/*' file='Doc/DataAccess.xml'/>
    public int ExecuteNonQuery(string? sql)
    {
      DbCommand? command;
      int retValue = 0;

      if (LJC.HasValue(sql))
      {
        if (LJC.HasValue(mProviderName)
          && IsMySql(mProviderName))
        {
          if (mMySqlDataAccess != null)
          {
            retValue = mMySqlDataAccess.ExecuteNonQuery(sql);
          }
        }
        else
        {
          if (ProviderFactory != null)
          {
            try
            {
              using (command = ProviderFactory.CreateCommand(sql))
              {
                if (command != null)
                {
                  ProviderFactory.OpenConnection();
                  retValue = command.ExecuteNonQuery();
                }
              }
            }
            finally
            {
              ProviderFactory.CloseConnection();
            }
          }
        }
      }
      return retValue;
    }

    // Executes a script file.
    /// <include path='items/ExecuteScript/*' file='Doc/DataAccess.xml'/>
    public void ExecuteScript(string scriptFileSpec)
    {
      if (!File.Exists(scriptFileSpec))
      {
        var errorText = $"File '{scriptFileSpec}' was not found.\r\n";
        throw new ArgumentException(errorText);
      }
      else
      {
        string scriptText = File.ReadAllText(scriptFileSpec);
        ExecuteScriptText(scriptText);
      }
    }

    // Executes a script text string.
    /// <include path='items/ExecuteScriptText/*' file='Doc/DataAccess.xml'/>
    public void ExecuteScriptText(string scriptText)
    {
      //string[] separators = new string[] { "GO\r\n", "go\r\n", "GO\n", "go\n" };
      //string[] separators = ["GO\r\n", "go\r\n", "GO\n", "go\n"];
      //string[] commands = scriptText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

      // Testing
      var text = scriptText.Replace("GO\r\n", "go\r\n", StringComparison.OrdinalIgnoreCase);
      text = text.Replace("GO\n", "go\r\n", StringComparison.OrdinalIgnoreCase);
      string[] commands = text.Split(["go\r\n"], StringSplitOptions.RemoveEmptyEntries);

      var first = true;
      foreach (string command in commands)
      {
        ScriptSQL = command;
        var sql = ScriptSQL;
        if (GetUse(ref first, ref sql))
        {
          ScriptSQL = sql;
          if (LJC.HasValue(mProviderName)
            && IsMySql(mProviderName))
          {
            mMySqlDataAccess?.ExecuteNonQuery(ScriptSQL);
          }
          else
          {
            ExecuteNonQuery(ScriptSQL);
          }
        }
      }
    }

    // Executes a Select statement and fills the specified DataTable.
    /// <include path='items/FillDataTable/*' file='Doc/DataAccess.xml'/>
    public void FillDataTable(string sql, DataTable dataTable
      , DataTableMappingCollection? tableMapping = null)
    {
      DbCommand? dbCommand;
      DbDataAdapter? dbDataAdapter = null;

      if (LJC.HasValue(mProviderName)
        && IsMySql(mProviderName))
      {
        mMySqlDataAccess?.FillDataTable(sql, dataTable);
      }
      else
      {
        if (ProviderFactory != null)
        {
          bool success = true;
          using (dbCommand = ProviderFactory!.CreateCommand(sql))
          {
            if (dbCommand != null)
            {
              dbDataAdapter = ProviderFactory.CreateDataAdapter();
              if (null == dbDataAdapter)
              {
                success = false;
              }
            }

            if (success)
            {
              LJCDataCommon.SetTableMapping(dbDataAdapter, tableMapping);
              if (dbDataAdapter != null)
              {
                dbDataAdapter.SelectCommand = dbCommand;
              }
              try
              {
                dbDataAdapter?.Fill(dataTable);
              }
              catch (Exception e)
              {
                string message = LJCNetString.ExceptionString(e);
                throw new Exception(message);
              }
            }
          }
        }
      }
    }

    // Executes a Select statement and retrieves the DbDataReader object.
    /// <include path='items/GetDataReader/*' file='Doc/DataAccess.xml'/>
    public DbDataReader? GetDataReader(string sql)
    {
      DbCommand? command;
      DbDataReader? retValue = null;

      if (LJC.HasValue(mProviderName)
        && IsMySql(mProviderName))
      {
        if (mMySqlDataAccess != null)
        {
          retValue = mMySqlDataAccess.GetDataReader(sql);
        }
      }
      else
      {
        if (ProviderFactory != null)
        {
          command = ProviderFactory.CreateCommand(sql);
          if (command != null)
          {
            ProviderFactory.OpenConnection();
            retValue = command.ExecuteReader();
          }
        }
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataSet object.
    /// <include path='items/GetDataSet/*' file='Doc/DataAccess.xml'/>
    public DataSet? GetDataSet(string sql
      , DataTableMappingCollection? tableMapping = null)
    {
      DbCommand? dbCommand;
      DbDataAdapter? dbDataAdapter = null;
      DataSet? retValue = null;

      if (LJC.HasValue(mProviderName)
        && IsMySql(mProviderName))
      {
        if (mMySqlDataAccess != null)
        {
          retValue = mMySqlDataAccess.GetDataSet(sql, tableMapping);
        }
      }
      else
      {
        if (ProviderFactory != null)
        {
          using (dbCommand = ProviderFactory.CreateCommand(sql))
          {
            bool success = true;
            if (dbCommand != null)
            {
              dbCommand!.CommandText = sql;
              dbDataAdapter = ProviderFactory.CreateDataAdapter();
              if (null == dbDataAdapter)
              {
                success = false;
              }
            }

            if (success)
            {
              LJCDataCommon.SetTableMapping(dbDataAdapter, tableMapping);
              dbDataAdapter!.SelectCommand = dbCommand;
              retValue = new DataSet();
              dbDataAdapter!.Fill(retValue);
            }
          }
        }
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataTable object.
    /// <include path='items/GetDataTable/*' file='Doc/DataAccess.xml'/>
    public DataTable? GetDataTable(string sql
      , DataTableMappingCollection? tableMapping = null)
    {
      DataTable? retTable = null;

      if (LJC.HasValue(sql))
      {
        if (LJC.HasValue(mProviderName)
          && IsMySql(mProviderName))
        {
          if (mMySqlDataAccess != null)
          {
            retTable = mMySqlDataAccess.GetDataTable(sql, tableMapping);
          }
        }
        else
        {
          retTable = GetSchemaOnly(sql);
          //if (LJC.HasData(retValue))
          if (LJC.HasColumns(retTable))
          {
            FillDataTable(sql, retTable, tableMapping);
          }
        }
      }
      return retTable;
    }

    // Executes a Stored Procedure and retrieves the DataTable object.
    /// <include path='items/GetProcedureDataTable/*' file='Doc/DataAccess.xml'/>
    public DataTable? GetProcedureDataTable(string procedureName
      , LJCProcedureParameters? parameters)
    {
      DbCommand? dbCommand;
      DbDataAdapter? dbDataAdapter;
      DataTable? retValue = null;

      if (LJC.HasValue(mProviderName)
        && IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess?.GetProcedureDataTable(procedureName
          , parameters);
      }
      else
      {
        if (ProviderFactory != null)
        {
          using (dbCommand = ProviderFactory.CreateCommand(procedureName
            , CommandType.StoredProcedure))
          {
            if (dbCommand != null
              && parameters != null)
            {
              foreach (LJCProcedureParameter parameter in parameters)
              {
                var parm = new SqlParameter()
                {
                  ParameterName = parameter.ParameterName,
                  SqlDbType = parameter.SqlDbType,
                  Size = parameter.Size,
                  Direction = parameter.Direction,
                  Value = parameter.Value
                };
                dbCommand.Parameters.Add(parm);
              }
            }

            dbDataAdapter = ProviderFactory.CreateDataAdapter();
            if (dbDataAdapter != null)
            {
              dbDataAdapter.SelectCommand = dbCommand;
              var dataSet = new DataSet();
              dbDataAdapter.Fill(dataSet);
              if (LJC.HasTables(dataSet))
              {
                retValue = dataSet.Tables[0];
              }
            }
          }
        }
      }
      return retValue;
    }

    // Retrieves the DataTable object with schema only.
    /// <include path='items/GetSchemaOnly/*' file='Doc/DataAccess.xml'/>
    public DataTable? GetSchemaOnly(string sql
      , DataTableMappingCollection? tableMapping = null)
    {
      DataTable? retValue = null;

      if (LJC.HasValue(mProviderName)
        && IsMySql(mProviderName))
      {
        if (mMySqlDataAccess != null)
        {
          retValue = mMySqlDataAccess.GetSchemaOnly(sql);
        }
      }
      else
      {
        if (IsValidateProviderFactory())
        {
          using var dbCommand = ProviderFactory?.CreateCommand(sql);
          var dbDataAdapter = ProviderFactory?.CreateDataAdapter();
          if (dbDataAdapter != null)
          {
            dbDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            LJCDataCommon.SetTableMapping(dbDataAdapter, tableMapping);
            dbDataAdapter.SelectCommand = dbCommand;
            retValue = new DataTable();
            try
            {
              dbDataAdapter.FillSchema(retValue, SchemaType.Mapped);
            }
            catch (Exception e)
            {
              string message = LJCNetString.ExceptionString(e);
              message += "\r\n" + sql;
              throw new Exception(message);
            }
            retValue.TableName = GetTableName(sql, retValue.TableName);
          }
        }
      }
      return retValue;
    }

    // Get the column SQL types.
    /// <include path='items/GetColumnSQLTypes/*' file='Doc/DataAccess.xml'/>
    public DataTable? GetColumnSQLTypes(string dbName, string tableName)
    {
      DataTable? retValue;

      var builder = new StringBuilder(128);
      builder.AppendLine("select COLUMN_NAME, DATA_TYPE ");
      builder.AppendLine("from INFORMATION_SCHEMA.COLUMNS ");
      builder.AppendLine($"where TABLE_CATALOG = '{dbName}'");
      builder.AppendLine($" and TABLE_NAME = '{tableName}'");
      string sql = builder.ToString();
      retValue = GetDataTable(sql);
      return retValue;
    }

    // Creates the LJCProviderFactory object.
    private void CreateProviderFactory(string connectionString, string providerName)
    {
      if (providerName != "MySql.Data.MySqlClient")
      {
        ProviderFactory = new LJCProviderFactory(connectionString, providerName);
      }
    }

    // Gets the "use" command and returns true if valid.
    private bool GetUse(ref bool first, ref string sql)
    {
      bool retValue = true;

      if (first)
      {
        if (IsUseCommand(sql))
        {
          if (LJC.HasValue(TableName))
          {
            sql = $"use {TableName}";
          }
          else
          {
            retValue = false;
          }
        }
      }
      first = false;
      return retValue;
    }

    // Sets provider factory errors.
    private bool IsValidateProviderFactory()
    {
      string? errorText = null;
      bool retValue = true;

      if (null == ProviderFactory)
      {
        retValue = false;
        errorText += "The LJCDataAccess.ProviderFactory value is not set.\r\n";
      }
      if (!LJC.HasValue(ConnectionString))
      {
        errorText += "The LJCDataAccess.ConnectionString value is not set.\r\n";
      }
      if (!LJC.HasValue(ProviderName))
      {
        errorText += "The LJCDataAccess.ProviderName value is not set.\r\n";
      }
      if (LJC.HasValue(errorText))
      {
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConnectionString value.</summary>
    public string? ConnectionString
    {
      get { return mConnectionString; }
      set
      {
        if (LJC.HasValue(value))
        {
          mConnectionString = value;
          if (!LJC.HasValue(mProviderName))
          {
            mProviderName = GetSQLServerProviderName();
          }

          if ("MySql.Data.MySqlClient" == mProviderName)
          {
            mMySqlDataAccess = new LJCMySqlDataAccess(mConnectionString);
          }
          else
          {
            CreateProviderFactory(mConnectionString, mProviderName);
          }
        }
      }
    }
    private string? mConnectionString;

    // Gets a reference to the
    /// <include path='items/ProviderFactory/*' file='Doc/DataAccess.xml'/>
    public LJCProviderFactory? ProviderFactory { get; private set; }

    /// <summary>Gets the ProviderName value.</summary>
    public string? ProviderName
    {
      get { return mProviderName; }
      set
      {
        if (LJC.HasValue(value))
        {
          mProviderName = value;
          if (!LJC.HasValue(mProviderName))
          {
            mProviderName = GetSQLServerProviderName();
          }
          if (LJC.HasValue(mConnectionString))
          {
            if ("MySql.Data.MySqlClient" == mProviderName)
            {
              mMySqlDataAccess = new LJCMySqlDataAccess(mConnectionString);
            }
            else
            {
              CreateProviderFactory(mConnectionString, mProviderName);
            }
          }
        }
      }
    }
    private string? mProviderName;

    /// <summary>Gets the ScriptSQL value.</summary>
    public string? ScriptSQL
    {
      get { return mScriptSQL; }
      set { mScriptSQL = LJCNetString.InitString(value); }
    }
    private string? mScriptSQL;

    /// <summary>Gets the TableName value.</summary>
    public string? TableName
    {
      get { return mTableName; }
      set { mTableName = LJCNetString.InitString(value); }
    }
    private string? mTableName;
    #endregion

    #region Class Data

    private LJCMySqlDataAccess? mMySqlDataAccess;
    #endregion
  }

  /// <summary>The Connection types.</summary>
  public enum ConnectionType
  {
    /// <summary>No connection type.</summary>
    None,

    /// <summary>Use an Access connection.</summary>
    Access,

    /// <summary>Use a MySQL connection.</summary>
    MySql,

    /// <summary>Use an ODBC connection.</summary>
    Odbc,

    /// <summary>Use an OleDB connection.</summary>
    OleDb,

    /// <summary>Use a SQLServer connection.</summary>
    SqlServer
  }
}
