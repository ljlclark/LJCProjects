// Copyright(c) Lester J.Clark and Contributors.
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
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
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

      foreach (string command in commands)
      {
        if (IsMySql(mProviderName))
        {
          mMySqlDataAccess.ExecuteNonQuery(command);
        }
        else
        {
          ExecuteNonQuery(command);
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
          dbDataAdapter.Fill(dataTable);
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
      DataTable retValue = null;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetDataTable(sql, tableMapping);
      }
      else
      {
        DataSet dataSet = GetDataSet(sql, tableMapping);
        if (dataSet != null && dataSet.Tables.Count > 0)
        {
          retValue = dataSet.Tables[0];
        }
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
      DbCommand dbCommand;
      DbDataAdapter dbDataAdapter;
      DataSet dataSet;
      DataTable retValue = null;

      if (IsMySql(mProviderName))
      {
        retValue = mMySqlDataAccess.GetSchemaOnly(sql);
      }
      else
      {
        if (IsValidateProviderFactory())
        {
          using (dbCommand = ProviderFactory.CreateCommand(sql))
          {
            dbDataAdapter = ProviderFactory.CreateDataAdapter();
            dbDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataCommon.SetTableMapping(dbDataAdapter, tableMapping);
            dbDataAdapter.SelectCommand = dbCommand;
            dataSet = new DataSet();
            dbDataAdapter.FillSchema(dataSet, SchemaType.Source);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
              retValue = dataSet.Tables[0];
            }
          }
        }
      }
      return retValue;
    }

    /// <summary>
    /// Get the column SQL types.
    /// </summary>
    /// <param name="dbName">The database name.</param>
    /// <param name="tableName">The table name.</param>
    /// <returns>The SQLTypes DataTable.</returns>
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
