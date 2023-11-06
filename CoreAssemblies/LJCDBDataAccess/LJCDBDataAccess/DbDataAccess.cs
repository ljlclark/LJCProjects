// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbDataAccess.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Data;
using System.Text;

namespace LJCDBDataAccess
{
  // The Data Access methods.
  /// <include path='items/DbDataAccess/*' file='Doc/ProjectDBDataAccess.xml'/>
  public partial class DbDataAccess
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbDataAccess()
    {
      mDataAccess = new DataAccess();
    }

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbDataAccess(string dataConfigName)
    {
      mDataAccess = new DataAccess();
      DataConfigName = dataConfigName;
      GetConfigValues(DataConfigName);
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbDataAccessC/*' file='Doc/DbDataAccess.xml'/>
    public DbDataAccess(string databaseName, string connectionString = null
      , string providerName = null)
    {
      mDataAccess = new DataAccess();
      DatabaseName = databaseName;

      // Private properties default to SQLClient provider if set to null.
      ConnectionString = connectionString;
      ProviderName = providerName;
    }
    #endregion

    #region Public Methods

    // Executes the specified database request XML message.
    /// <include path='items/Execute/*' file='Doc/DbDataAccess.xml'/>
    public DbResult Execute(DbRequest dbRequest)
    {
      DbResult retValue = null;

      // Creates mDbSqlBuilder.
      DbRequest = dbRequest;

      switch (DbRequest.RequestTypeName.ToLower())
      {
        case "insert":
          retValue = Add();
          break;
        case "select":
          retValue = Retrieve();
          break;
        case "load":
          retValue = Load();
          break;
        case "update":
          retValue = Update();
          break;
        case "delete":
          retValue = Delete();
          break;
        case "selectprocedure":
          retValue = SelectProcedure();
          break;
        case "retrievesql":
          retValue = RetrieveClientSql();
          break;
        case "loadsql":
          retValue = LoadClientSql();
          break;
        case "executesql":
          retValue = ExecuteClientSql();
          break;
        case "schemaonly":
          retValue = TableColumns();
          break;
        case "tablenames":
          retValue = TableNames();
          break;
      }
      return retValue;
    }
    #endregion

    #region Private Data Methods

    // Inserts a record in the database with the specified columns.
    /// <include path='items/Add/*' file='Doc/DbDataAccess.xml'/>
    private DbResult Add()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = mDbSqlBuilder.CreateAddSql();
      AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
      retValue.AffectedRecords = AffectedCount;
      retValue.ExecutedSql = SqlStatement;

      if (DbRequest.DbAssignedColumns != null
        && DbRequest.DbAssignedColumns.Count > 0)
      {
        string saveSql = SqlStatement;

        DbRequest request = new DbRequest(RequestType.Select
          , DbRequest.TableName)
        {
          Columns = DbRequest.DbAssignedColumns.Clone(),
          KeyColumns = DbRequest.KeyColumns.Clone()
        };
        retValue = Retrieve(request);
        retValue.AffectedRecords = AffectedCount;

        SqlStatement = $"{saveSql}\r\n{SqlStatement}";
        retValue.ExecutedSql = SqlStatement;
      }
      return retValue;
    }

    // Delete a record in the database.
    /// <include path='items/Delete/*' file='Doc/DbDataAccess.xml'/>
    private DbResult Delete()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = mDbSqlBuilder.CreateDeleteSql();
      AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
      retValue.AffectedRecords = AffectedCount;
      retValue.ExecutedSql = SqlStatement;
      return retValue;
    }

    // Executes a non-query client SQL statement.
    /// <include path='items/ExecuteClientSql/*' file='Doc/DbDataAccess.xml'/>
    private DbResult ExecuteClientSql()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = DbRequest.ClientSql;
      AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
      retValue.AffectedRecords = AffectedCount;
      retValue.ExecutedSql = SqlStatement;
      return retValue;
    }

    // Retrieves multiple data rows.
    /// <include path='items/Load/*' file='Doc/DbDataAccess.xml'/>
    private DbResult Load()
    {
      if (DbRequest.AddMissingColumns)
      {
        AddTableColumns(DbRequest);
      }
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = mDbSqlBuilder.CreateLoadSql();
      DataTable dataTable = mDataAccess.GetDataTable(SqlStatement);
      retValue.ExecutedSql = SqlStatement;

      if (NetCommon.HasData(dataTable))
      {
        retValue.SetData(dataTable, DbRequest);
      }
      return retValue;
    }

    // Executes a "Load" client SQL statement.
    /// <include path='items/LoadClientSql/*' file='Doc/DbDataAccess.xml'/>
    private DbResult LoadClientSql()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = DbRequest.ClientSql;
      DataTable dataTable = mDataAccess.GetDataTable(SqlStatement);
      retValue.ExecutedSql = SqlStatement;

      if (NetCommon.HasData(dataTable))
      {
        if (null == DbRequest.Columns || 0 == DbRequest.Columns.Count)
        {
          DbRequest.Columns = CreateDbColumnsFromTable(dataTable);
        }
        retValue.SetData(dataTable, DbRequest);
      }
      return retValue;
    }

    // Retrieves the data row values.
    /// <include path='items/Retrieve/*' file='Doc/DbDataAccess.xml'/>
    private DbResult Retrieve(DbRequest dbRequest = null)
    {
      if (null == dbRequest)
      {
        dbRequest = DbRequest;
      }
      if (dbRequest.AddMissingColumns)
      {
        AddTableColumns(dbRequest);
      }
      DbResult retValue = CreateResult(dbRequest);

      SqlStatement = mDbSqlBuilder.CreateRetrieveSql(dbRequest);
      DataTable dataTable = mDataAccess.GetDataTable(SqlStatement);
      retValue.ExecutedSql = SqlStatement;

      if (NetCommon.HasData(dataTable))
      {
        retValue.SetData(dataTable, dbRequest);
      }
      return retValue;
    }

    // Executes a "Retrieve" client SQL statement.
    /// <include path='items/RetrieveClientSql/*' file='Doc/DbDataAccess.xml'/>
    private DbResult RetrieveClientSql()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = DbRequest.ClientSql;
      DataTable dataTable = mDataAccess.GetDataTable(SqlStatement);
      retValue.ExecutedSql = SqlStatement;

      if (NetCommon.HasData(dataTable))
      {
        if (null == DbRequest.Columns || 0 == DbRequest.Columns.Count)
        {
          DbRequest.Columns = CreateDbColumnsFromTable(dataTable);
        }
        retValue.SetData(dataTable, DbRequest);
      }
      return retValue;
    }

    // Retrieves the data row values from a Stored Procedure.
    /// <include path='items/SelectProcedure/*' file='Doc/DbDataAccess.xml'/>
    private DbResult SelectProcedure(DbRequest dbRequest = null)
    {
      if (null == dbRequest)
      {
        dbRequest = DbRequest;
      }
      if (dbRequest.AddMissingColumns)
      {
        AddTableColumns(dbRequest);
      }
      DbResult retValue = CreateResult(dbRequest);

      SqlStatement = dbRequest.ProcedureName;
      DataTable dataTable = mDataAccess.GetProcedureDataTable(SqlStatement
        , dbRequest.Parameters);
      retValue.ExecutedSql = SqlStatement;

      retValue.SetData(dataTable, dbRequest);
      return retValue;
    }

    // Retrieves the column definitions.
    private DbResult TableColumns(DbRequest dbRequest = null)
    {
      if (null == dbRequest)
      {
        dbRequest = DbRequest;
      }
      DbResult retValue = CreateResult(dbRequest);

      DataTable dataTable = GetTableSchema(dbRequest);
      if (dataTable != null)
      {
        retValue.Columns = CreateDbColumnsFromTable(dataTable);
      }
      return retValue;
    }

    // Retrieves the table names for the specified database.
    /// <include path='items/TableNames/*' file='Doc/DbDataAccess.xml'/>
    private DbResult TableNames(string databaseName = null, DbRequest dbRequest = null)
    {
      if (null == databaseName)
      {
        databaseName = mDatabaseName;
      }
      if (null == dbRequest)
      {
        dbRequest = DbRequest;
      }
      DbResult retValue = CreateResult(dbRequest);

      DataTable dataTable = GetTableNames(databaseName);
      if (NetCommon.HasData(dataTable))
      {
        // Add TABLE_NAME if it is not already defined.
        // *** Next Statement *** Change - 11/24/22
        var dbColumn = dbRequest.Columns.LJCSearchPropertyName("TABLE_NAME");
        if (null == dbColumn)
        {
          dbRequest.Columns.Add("TABLE_NAME");
        }
        retValue.SetRows(dataTable, DbRequest.Columns, DbRequest.Joins);
      }
      return retValue;
    }

    // Updates a database record.
    /// <include path='items/Update/*' file='Doc/DbDataAccess.xml'/>
    private DbResult Update()
    {
      DbResult retValue = CreateResult(DbRequest);

      SqlStatement = mDbSqlBuilder.CreateUpdateSql();
      AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
      retValue.AffectedRecords = AffectedCount;
      retValue.ExecutedSql = SqlStatement;
      return retValue;
    }
    #endregion

    #region DataConfig Methods

    // Retrieves the data configuration.
    private DataConfig GetDataConfig(string dataConfigName)
    {
      DataConfig retValue;

      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      retValue = dataConfigs.LJCGetByName(dataConfigName);
      if (null == retValue)
      {
        var errorText = $"Data configuration '{dataConfigName}' was not found.";
        throw new MissingMemberException(errorText);
      }
      else
      {
        if (null == retValue.ConnectionTypeName)
        {
          // Default connection type to SQL Server.
          retValue.ConnectionTypeName = ConnectionType.SqlServer.ToString();
        }
      }
      return retValue;
    }

    // Sets the DataConfig values.
    /// <include path='items/GetConfigValues/*' file='Doc/DbDataAccess.xml'/>
    private void GetConfigValues(string dataConfigName)
    {
      DataConfig dataConfig = GetDataConfig(dataConfigName);
      DatabaseName = dataConfig.Database;
      ConnectionString = dataConfig.GetConnectionString();
      mDataAccess.ConnectionString = ConnectionString;
      ProviderName = DataConfig.GetProviderName(dataConfig.ConnectionTypeName);
    }
    #endregion

    #region Private Methods

    // Adds the missing table columns from the database schema.
    private void AddTableColumns(DbRequest dbRequest)
    {
      DataTable dataTable = GetTableSchema(dbRequest);
      if (dataTable != null)
      {
        foreach (DataColumn dataColumn in dataTable.Columns)
        {
          // *** Next Statement *** Change - 11/24/22
          var dbColumn
            = dbRequest.Columns.LJCSearchPropertyName(dataColumn.ColumnName);
          if (null == dbColumn)
          {
            DbColumn newDbColumn = CreateDbColumnFromDataColumn(dataColumn);
            if (newDbColumn != null)
            {
              dbRequest.Columns.Add(newDbColumn);
            }
          }
        }
      }
    }

    // Creates the result object with common values.
    /// <include path='items/CreateResult/*' file='Doc/DbDataAccess.xml'/>
    private DbResult CreateResult(DbRequest dbRequest)
    {
      DbResult retValue;

      if (null == dbRequest)
      {
        dbRequest = new DbRequest()
        {
          RequestTypeName = null,
          DataConfigName = DataConfigName
        };
      }

      retValue = new DbResult(dbRequest)
      {
        DatabaseName = DatabaseName
      };
      return retValue;
    }

    //// Creates a DbColumns object with each key column that has a value in the
    //// data object.
    //private DbColumns CreateKeyValueColumns(DbColumns dataColumns, DbColumns keyColumns)
    //{
    //	DbColumns retValue;

    //	retValue = new DbColumns();
    //	foreach (DbColumn keyColumn in keyColumns)
    //	{
    //		DbColumn dataColumn = dataColumns.LJCSearchName(keyColumn.ColumnName);
    //		if (dataColumn != null
    //			&& dataColumn.Value != null && dataColumn.Value.ToString() != "0")
    //		{
    //			retValue.Add(dataColumn.Clone());
    //		}
    //	}
    //	return retValue;
    //}

    // Get the database table names.
    private DataTable GetTableNames(string dbName)
    {
      DataTable retValue;

      // AND TABLE_CATALOG/TABLE_SCHEMA = 'dbName'
      StringBuilder builder = new StringBuilder(64);
      builder.AppendLine("select TABLE_NAME");
      builder.AppendLine($"from {dbName}.INFORMATION_SCHEMA.TABLES");
      builder.AppendLine("where TABLE_TYPE = 'BASE TABLE'");
      // *** Next Statement *** Add- 9/9
      builder.AppendLine("order by TABLE_NAME");
      SqlStatement = builder.ToString();

      try
      {
        retValue = mDataAccess.GetDataTable(SqlStatement);
      }
      catch (Exception)
      {
        builder = new StringBuilder(64);
        builder.AppendLine("select TABLE_NAME");
        builder.AppendLine("from INFORMATION_SCHEMA.TABLES\r\n");
        builder.AppendLine("where TABLE_TYPE = 'BASE TABLE'");
        builder.Append($" and TABLE_SCHEMA = '{dbName}'");
        SqlStatement = builder.ToString();
        retValue = mDataAccess.GetDataTable(SqlStatement);
      }
      return retValue;
    }

    // Gets the table schema DataTable.
    private DataTable GetTableSchema(DbRequest dbRequest)
    {
      DataTable retValue;

      StringBuilder builder = new StringBuilder(64);
      builder.Append("select * \r\n");
      builder.Append("from ");
      if (NetString.HasValue(dbRequest.SchemaName))
      {
        builder.Append($"{dbRequest.SchemaName}.");
      }
      builder.AppendLine($"{dbRequest.TableName}");
      SqlStatement = builder.ToString();
      
      retValue = mDataAccess.GetSchemaOnly(SqlStatement);
      return retValue;
    }
    #endregion

    #region Conversion Methods

    // Creates a DbColumn object from a DataColumn object.
    private DbColumn CreateDbColumnFromDataColumn(DataColumn dataColumn)
    {
      DbColumn retValue = null;

      if (dataColumn != null)
      {
        retValue = new DbColumn
        {
          AllowDBNull = dataColumn.AllowDBNull,
          AutoIncrement = dataColumn.AutoIncrement,
          Caption = dataColumn.Caption,
          ColumnName = dataColumn.ColumnName,
          DataTypeName = dataColumn.DataType.Name,
          MaxLength = dataColumn.MaxLength
        };
      }
      return retValue;
    }

    // Creates a DbColumns object from the DataTable columns.
    private DbColumns CreateDbColumnsFromTable(DataTable dataTable)
    {
      DbColumns retValue = null;

      if (dataTable != null)
      {
        DataTable sqlTypesTable = mDataAccess.GetColumnSQLTypes(DatabaseName
          , mDbRequest.TableName);

        retValue = new DbColumns();
        foreach (DataColumn dataColumn in dataTable.Columns)
        {
          DbColumn dbColumn = CreateDbColumnFromDataColumn(dataColumn);
          if (dbColumn != null)
          {
            SetSQLTypeName(sqlTypesTable, dbColumn);
            SetPrimaryKey(dataTable, dbColumn);
            retValue.Add(dbColumn);
          }
        }
      }
      return retValue;
    }

    // Get the SQL Type name.
    private void SetSQLTypeName(DataTable sqlTypesTable, DbColumn dataColumn)
    {
      if (NetCommon.HasData(sqlTypesTable))
      {
        foreach (DataRow dataRow in sqlTypesTable.Rows)
        {
          if (dataRow["COLUMN_NAME"].ToString() == dataColumn.ColumnName)
          {
            dataColumn.SQLTypeName = dataRow["DATA_TYPE"].ToString();
            break;
          }
        }
      }
    }

    // Sets the PrimaryKey value.
    private void SetPrimaryKey(DataTable dataTable, DbColumn dataColumn)
    {
      if (dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length > 0)
      {
        foreach (DataColumn primaryColumn in dataTable.PrimaryKey)
        {
          if (primaryColumn.ColumnName == dataColumn.ColumnName)
          {
            dataColumn.IsPrimaryKey = true;
            break;
          }
        }
      }
    }
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the Database name.</summary>
    public string DatabaseName
    {
      get { return mDatabaseName; }
      set
      {
        if (NetString.HasValue(value))
        {
          mDatabaseName = NetString.InitString(value);
        }
      }
    }
    private string mDatabaseName;

    /// <summary>Gets or sets the DbRequest object.</summary>
    public DbRequest DbRequest
    {
      get { return mDbRequest; }
      set
      {
        mDbRequest = value;
        mDbSqlBuilder = new DbSqlBuilder(mDbRequest);
      }
    }
    private DbRequest mDbRequest;
    #endregion

    #region Private Properties

    // Gets or sets the non-query affected record count.
    private int AffectedCount { get; set; }

    // Gets or sets the DataConfig name.
    private string DataConfigName { get; set; }

    // Gets or sets the Connection string.
    private string ConnectionString
    {
      get { return mConnectionString; }
      set
      {
        if (NetString.HasValue(value))
        {
          mConnectionString = NetString.InitString(value);
          mDataAccess.ConnectionString = mConnectionString;
        }
      }
    }
    private string mConnectionString;

    // Gets or sets the Provider name.
    internal string ProviderName
    {
      get { return mProviderName; }
      set
      {
        if (NetString.HasValue(value))
        {
          mProviderName = NetString.InitString(value);
          mDataAccess.ProviderName = mProviderName;
        }
      }
    }
    private string mProviderName;

    // Gets or sets the last action SQL statement.
    private string SqlStatement { get; set; }
    #endregion

    #region Class Data

    private readonly DataAccess mDataAccess;
    private DbSqlBuilder mDbSqlBuilder;
    #endregion
  }
}
