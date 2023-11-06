// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAccess.cs
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using LJCNetCommon;
using LJCDataAccessConfig;
using System.Text;

namespace LJCDataAccess
{
  // Implements an ADO.NET SQL data access control layer.
  /// <include path='items/DataAccess/*' file='Doc/ProjectDataAccess.xml'/>
  public partial class DataAccess
  {
    #region Static Functions

    // Creates a connection string.
    /// <include path='items/GetConnectionString/*' file='Doc/DataCommon.xml'/>
    public static string GetConnectionString(string dataSourceName
      , string databaseName, string userID = null, string password = null
      , params string[] pairs)
    {
      string retValue;

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
      }
      else
      {
        var pair = GetPair("User Id", userID);
        connectionBuilder.Add(pair[0], pair[1]);
        pair = GetPair("Password", password);
        connectionBuilder.Add(pair[0], pair[1]);
      }
      if (pairs != null)
      {
        foreach (var value in pairs)
        {
          var pair = GetPair("", value);
          if (pair != null && 2 == pair.Length)
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
    public static DataAccess GetDataAccess(string dataSourceName
      , string databaseName, string providerName = "System.Data.SqlClient")
    {
      DataAccess retValue;

      string connectionString = GetConnectionString(dataSourceName
        , databaseName);
      retValue = new DataAccess(connectionString, providerName);
      return retValue;
    }

    // Get a Connection String element pair.
    private static string[] GetPair(string text, string pair)
    {
      string[] retValue = pair.Split('|');
      if (retValue.Length < 2)
      {
        retValue = new string[] { text, pair };
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DataAccess()
    {
    }

    // Initializes an object instance.
    /// <include path='items/DataAccessC/*' file='Doc/DataAccess.xml'/>
    public DataAccess(string connectionString, string providerName = null)
    {
      ConnectionString = connectionString;
      ProviderName = providerName;
    }
    #endregion

    #region Public Methods

    // Closes the database connection.
    /// <include path='items/CloseConnection/*' file='Doc/DataAccess.xml'/>
    public void CloseConnection()
    {
      ProviderFactory.CloseConnection();
    }

    // Executes an Insert, Update or Delete statement.
    /// <include path='items/ExecuteNonQuery/*' file='Doc/DataAccess.xml'/>
    public int ExecuteNonQuery(string sql)
    {
      DbCommand command;
      int retValue = 0;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.ExecuteNonQuery(sql);
      }
      else
      {
        try
        {
          using (command = ProviderFactory.CreateCommand(sql))
          {
            ProviderFactory.OpenConnection();
            retValue = command.ExecuteNonQuery();
          }
        }
        finally
        {
          ProviderFactory.CloseConnection();
        }
      }
      return retValue;
    }

    // Executes a script file.
    /// <include path='items/ExecuteScript/*' file='Doc/DataAccess.xml'/>
    public void ExecuteScript(string scriptFileSpec)
    {
      if (false == File.Exists(scriptFileSpec))
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
      string[] separators = new string[] { "GO\r\n", "go\r\n", "GO\n", "go\n" };
      string[] commands = scriptText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

      var first = true;
      foreach (string command in commands)
      {
        ScriptSQL = command;
        var sql = ScriptSQL;
        if (GetUse(ref first, ref sql))
        {
          ScriptSQL = sql;
          if (IsMySql(mProviderName))
          {
            mMySqlDataAccess.ExecuteNonQuery(ScriptSQL);
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
      , DataTableMappingCollection tableMapping = null)
    {
      DbCommand dbCommand;
      DbDataAdapter dbDataAdapter;

      if (IsMySql(mProviderName))
      {
        mMySqlDataAccess.FillDataTable(sql, dataTable);
      }
      else
      {
        using (dbCommand = ProviderFactory.CreateCommand(sql))
        {
          dbDataAdapter = ProviderFactory.CreateDataAdapter();
          DataCommon.SetTableMapping(dbDataAdapter, tableMapping);
          dbDataAdapter.SelectCommand = dbCommand;
          try
          {
            dbDataAdapter.Fill(dataTable);
          }
          catch (Exception e)
          {
            string message = NetString.ExceptionString(e);
            throw new Exception(message);
          }
        }
      }
    }

    // Executes a Select statement and retrieves the DbDataReader object.
    /// <include path='items/GetDataReader/*' file='Doc/DataAccess.xml'/>
    public DbDataReader GetDataReader(string sql)
    {
      DbCommand command;
      DbDataReader retValue;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetDataReader(sql);
      }
      else
      {
        command = ProviderFactory.CreateCommand(sql);
        ProviderFactory.OpenConnection();
        retValue = command.ExecuteReader();
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataSet object.
    /// <include path='items/GetDataSet/*' file='Doc/DataAccess.xml'/>
    public DataSet GetDataSet(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      DbCommand dbCommand;
      DbDataAdapter dbDataAdapter;
      DataSet retValue = null;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetDataSet(sql, tableMapping);
      }
      else
      {
        using (dbCommand = ProviderFactory.CreateCommand(sql))
        {
          dbCommand.CommandText = sql;
          dbDataAdapter = ProviderFactory.CreateDataAdapter();
          DataCommon.SetTableMapping(dbDataAdapter, tableMapping);
          dbDataAdapter.SelectCommand = dbCommand;
          retValue = new DataSet();
          dbDataAdapter.Fill(retValue);
        }
      }
      return retValue;
    }

    // Executes a Select statement and retrieves the DataTable object.
    /// <include path='items/GetDataTable/*' file='Doc/DataAccess.xml'/>
    public DataTable GetDataTable(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      DataTable retValue;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetDataTable(sql, tableMapping);
      }
      else
      {
        retValue = GetSchemaOnly(sql);
        FillDataTable(sql, retValue, tableMapping);
      }
      return retValue;
    }

    // Executes a Stored Procedure and retrieves the DataTable object.
    /// <include path='items/GetProcedureDataTable/*' file='Doc/DataAccess.xml'/>
    public DataTable GetProcedureDataTable(string procedureName
      , ProcedureParameters parameters)
    {
      DbCommand dbCommand;
      DbDataAdapter dbDataAdapter;
      DataTable retValue = null;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetProcedureDataTable(procedureName
          , parameters);
      }
      else
      {
        using (dbCommand = ProviderFactory.CreateCommand(procedureName
          , CommandType.StoredProcedure))
        {
          if (parameters != null)
          {
            foreach (ProcedureParameter parameter in parameters)
            {
              SqlParameter parm = new SqlParameter()
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
          dbDataAdapter.SelectCommand = dbCommand;
          DataSet dataSet = new DataSet();
          dbDataAdapter.Fill(dataSet);
          if (dataSet != null && dataSet.Tables.Count > 0)
          {
            retValue = dataSet.Tables[0];
          }
        }
      }
      return retValue;
    }

    // Retrieves the DataTable object with schema only.
    /// <include path='items/GetSchemaOnly/*' file='Doc/DataAccess.xml'/>
    public DataTable GetSchemaOnly(string sql
      , DataTableMappingCollection tableMapping = null)
    {
      DataTable retValue = null;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetSchemaOnly(sql);
      }
      else
      {
        if (IsValidateProviderFactory())
        {
          using (var dbCommand = ProviderFactory.CreateCommand(sql))
          {
            var dbDataAdapter = ProviderFactory.CreateDataAdapter();
            dbDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataCommon.SetTableMapping(dbDataAdapter, tableMapping);
            dbDataAdapter.SelectCommand = dbCommand;
            retValue = new DataTable();
            try
            {
              dbDataAdapter.FillSchema(retValue, SchemaType.Mapped);
            }
            catch (Exception e)
            {
              string message = NetString.ExceptionString(e);
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
    public DataTable GetColumnSQLTypes(string dbName, string tableName)
    {
      DataTable retValue;

      StringBuilder builder = new StringBuilder(128);
      builder.AppendLine("select COLUMN_NAME, DATA_TYPE ");
      builder.AppendLine("from INFORMATION_SCHEMA.COLUMNS ");
      builder.AppendLine($"where TABLE_CATALOG = '{dbName}'");
      builder.AppendLine($" and TABLE_NAME = '{tableName}'");
      string sql = builder.ToString();
      retValue = GetDataTable(sql);
      return retValue;
    }

    /// <summary>Checks for the "use" command.</summary>
    public bool IsUseCommand(string sql)
    {
      var retValue = false;

      var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
      var index = sql.IndexOf("use", ignoreCase);
      if (index >= 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Creates the LJCProviderFactory object.
    private void CreateProviderFactory(string connectionString, string providerName)
    {
      if (providerName != "MySql.Data.MySqlClient")
      {
        ProviderFactory = new ProviderFactory(connectionString, providerName);
      }
    }

    // Returns the default SQL Server provider name.
    private string GetSQLServerProviderName()
    {
      string retValue;

      string connectionTypeName = ConnectionType.SqlServer.ToString();
      retValue = DataConfig.GetProviderName(connectionTypeName);
      return retValue;
    }

    // Gets the "use" command and returns true if valid.
    private bool GetUse(ref bool first, ref string sql)
    {
      bool retValue = true;

      if (first)
      {
        if (IsUseCommand(sql))
        {
          if (NetString.HasValue(TableName))
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

    // Checks for MySql provider.
    private bool IsMySql(string providerName)
    {
      bool retValue = false;

      if ("MySql.Data.MySqlClient" == providerName)
      {
        retValue = true;
      }
      return retValue;
    }

    // Sets provider factory errors.
    private bool IsValidateProviderFactory()
    {
      string errorText = null;
      bool retValue = true;

      if (null == ProviderFactory)
      {
        retValue = false;
        errorText += "The LJCDataAccess.ProviderFactory value is not set.\r\n";
      }
      if (false == NetString.HasValue(ConnectionString))
      {
        errorText += "The LJCDataAccess.ConnectionString value is not set.\r\n";
      }
      if (false == NetString.HasValue(ProviderName))
      {
        errorText += "The LJCDataAccess.ProviderName value is not set.\r\n";
      }
      if (NetString.HasValue(errorText))
      {
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }

    // Retrieves the Table name from an SQL statement.
    private string GetTableName(string sql, string tableName)
    {
      string retValue = tableName;

      var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
      var index = sql.IndexOf(" from ", ignoreCase);
      if (index > -1)
      {
        var beginIndex = index + " from ".Length;
        var endIndex = sql.IndexOf(" ", beginIndex);
        if (-1 == endIndex)
        {
          endIndex = sql.Length;
        }
        retValue = sql.Substring(beginIndex, endIndex - beginIndex);
        retValue = retValue.Trim();
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConnectionString value.</summary>
    public string ConnectionString
    {
      get { return mConnectionString; }
      set
      {
        mConnectionString = value;
        if (false == NetString.HasValue(mProviderName))
        {
          mProviderName = GetSQLServerProviderName();
        }

        if ("MySql.Data.MySqlClient" == mProviderName)
        {
          mMySqlDataAccess = new MySqlDataAccess(mConnectionString);
        }
        else
        {
          CreateProviderFactory(mConnectionString, mProviderName);
        }
      }
    }
    private string mConnectionString;

    // Gets a reference to the
    /// <include path='items/ProviderFactory/*' file='Doc/DataAccess.xml'/>
    public ProviderFactory ProviderFactory { get; private set; }

    /// <summary>Gets the ProviderName value.</summary>
    public string ProviderName
    {
      get { return mProviderName; }
      set
      {
        mProviderName = value;
        if (false == NetString.HasValue(mProviderName))
        {
          mProviderName = GetSQLServerProviderName();
        }
        if (NetString.HasValue(mConnectionString))
        {
          if ("MySql.Data.MySqlClient" == mProviderName)
          {
            mMySqlDataAccess = new MySqlDataAccess(mConnectionString);
          }
          else
          {
            CreateProviderFactory(mConnectionString, mProviderName);
          }
        }
      }
    }
    private string mProviderName;

    /// <summary>Gets the ScriptSQL value.</summary>
    public string ScriptSQL
    {
      get { return mScriptSQL; }
      set { mScriptSQL = NetString.InitString(value); }
    }
    private string mScriptSQL;

    /// <summary>Gets the TableName value.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion

    #region Class Data

    private MySqlDataAccess mMySqlDataAccess;
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
