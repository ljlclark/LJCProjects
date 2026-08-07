// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbDataAccess.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDBMessage5;
using LJCNetCommon5;
using System.Data;
using System.Text;

namespace LJCDBDataAccess5
{
  // The Data Access methods.
  /// <include file='Doc/ProjectDBDataAccess.xml'
  ///  path='items/DbDataAccess/*'/>
  public partial class DbDataAccess
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbDataAccess()
    {
      mDataAccess = new LJCDataAccess();
    }

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbDataAccess(string dataConfigName)
    {
      mDataAccess = new LJCDataAccess();
      DataConfigName = dataConfigName;
      GetConfigValues(DataConfigName);
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/DbDataAccessC/*'/>
    public DbDataAccess(string databaseName, string? connectionString = null
      , string? providerName = null)
    {
      mDataAccess = new LJCDataAccess();
      DatabaseName = databaseName;

      // Private properties default to SQLClient provider if set to null.
      ConnectionString = connectionString;
      ProviderName = providerName;
    }
    #endregion

    #region Public Methods

    // Executes the specified database request XML message.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Execute/*'/>
    public LJCDBResult? Execute(LJCDBRequest dbRequest)
    {
      LJCDBResult? retValue = null;

      // Creates mDbSqlBuilder.
      DBRequest = dbRequest;

      switch (DBRequest.RequestTypeName.ToLower())
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
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Add/*'/>
    private LJCDBResult? Add()
    {
      LJCDBResult? retResult = null;

      if (DBRequest != null)
      {
        retResult = CreateResult(DBRequest);
        if (retResult != null
          && mDbSqlBuilder != null)
        {
          SqlStatement = mDbSqlBuilder.CreateAddSql();
          AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
          retResult.AffectedRecords = AffectedCount;
          retResult.ExecutedSql = SqlStatement;

          if (LJC.HasListItems(DBRequest.DbAssignedColumns))
          {
            string saveSql = SqlStatement;

            LJCDataColumns? keyColumns = null;
            if (LJC.HasListItems(DBRequest.KeyColumns))
            {
              keyColumns = DBRequest.KeyColumns;
            }
            var request = new LJCDBRequest(RequestType.Select
              , DBRequest.TableName)
            {
              Columns = DBRequest.DbAssignedColumns.Clone(),
              KeyColumns = keyColumns,
            };

            var retRetrieve = Retrieve(request);
            if (retRetrieve != null)
            {
              retRetrieve.AffectedRecords = AffectedCount;
              SqlStatement = $"{saveSql}\r\n{SqlStatement}";
              retRetrieve.ExecutedSql = SqlStatement;
            }
          }
        }
      }
      return retResult;
    }

    // Delete a record in the database.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Delete/*'/>
    private LJCDBResult? Delete()
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        retValue = CreateResult(DBRequest);
        if (mDbSqlBuilder != null)
        {
          SqlStatement = mDbSqlBuilder.CreateDeleteSql();
          AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
          retValue.AffectedRecords = AffectedCount;
          retValue.ExecutedSql = SqlStatement;
        }
      }
      return retValue;
    }

    // Executes a non-query client SQL statement.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/ExecuteClientSql/*'/>
    private LJCDBResult? ExecuteClientSql()
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        retValue = CreateResult(DBRequest);
        SqlStatement = DBRequest.ClientSql;
        if (LJC.HasText(SqlStatement))
        {
          AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
          retValue.AffectedRecords = AffectedCount;
          retValue.ExecutedSql = SqlStatement;
        }
      }
      return retValue;
    }

    // Retrieves multiple data rows.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Load/*'/>
    private LJCDBResult? Load()
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        if (DBRequest.AddMissingColumns)
        {
          AddTableColumns(DBRequest);
        }
        retValue = CreateResult(DBRequest);

        if (mDbSqlBuilder != null)
        {
          SqlStatement = mDbSqlBuilder.CreateLoadSql();
          if (LJC.HasText(SqlStatement))
          {
            var dataTable = mDataAccess.GetDataTable(SqlStatement);
            retValue.ExecutedSql = SqlStatement;

            if (LJC.HasTableData(dataTable))
            {
              retValue.SetData(dataTable, DBRequest);
            }
          }
        }
      }
      return retValue;
    }

    // Executes a "Load" client SQL statement.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/LoadClientSql/*'/>
    private LJCDBResult? LoadClientSql()
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        retValue = CreateResult(DBRequest);
        SqlStatement = DBRequest.ClientSql;
        if (LJC.HasText(SqlStatement))
        {
          var dataTable = mDataAccess.GetDataTable(SqlStatement);
          retValue.ExecutedSql = SqlStatement;

          if (LJC.HasTableData(dataTable))
          {
            if (null == DBRequest.Columns
              || 0 == DBRequest.Columns.Count)
            {
              DBRequest.Columns = CreateDataColumnsFromTable(dataTable);
            }
            retValue.SetData(dataTable, DBRequest);
          }
        }
      }
      return retValue;
    }

    // Retrieves the data row values.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Retrieve/*'/>
    private LJCDBResult? Retrieve(LJCDBRequest? dbRequest = null)
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        if (null == dbRequest)
        {
          dbRequest = DBRequest;
        }
        if (dbRequest.AddMissingColumns)
        {
          AddTableColumns(dbRequest);
        }
        retValue = CreateResult(dbRequest);

        if (mDbSqlBuilder != null)
        {
          SqlStatement = mDbSqlBuilder.CreateRetrieveSql(dbRequest);
          var dataTable = mDataAccess.GetDataTable(SqlStatement);
          retValue.ExecutedSql = SqlStatement;

          if (LJC.HasTableData(dataTable))
          {
            retValue.SetData(dataTable, dbRequest);
          }
        }
      }
      return retValue;
    }

    // Executes a "Retrieve" client SQL statement.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/RetrieveClientSql/*'/>
    private LJCDBResult? RetrieveClientSql()
    {
      LJCDBResult? retValue = null;

      if (DBRequest != null)
      {
        retValue = CreateResult(DBRequest);
        SqlStatement = DBRequest.ClientSql;
        if (LJC.HasText(SqlStatement))
        {
          var dataTable = mDataAccess.GetDataTable(SqlStatement);
          retValue.ExecutedSql = SqlStatement;

          if (LJC.HasTableData(dataTable))
          {
            if (null == DBRequest.Columns
              || 0 == DBRequest.Columns.Count)
            {
              DBRequest.Columns = CreateDataColumnsFromTable(dataTable);
            }
            retValue.SetData(dataTable, DBRequest);
          }
        }
      }
      return retValue;
    }

    // Retrieves the data row values from a Stored Procedure.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/SelectProcedure/*'/>
    private LJCDBResult? SelectProcedure(LJCDBRequest? dbRequest = null)
    {
      LJCDBResult? retValue = null;

      if (null == dbRequest)
      {
        if (DBRequest != null)
        {
          if (DBRequest != null)
          {
            dbRequest = DBRequest;
          }
        }
      }
      if (dbRequest != null)
      {
        if (dbRequest.AddMissingColumns)
        {
          AddTableColumns(dbRequest);
        }
        retValue = CreateResult(dbRequest);

        SqlStatement = dbRequest.ProcedureName;
        if (LJC.HasText(SqlStatement))
        {
          var dataTable = mDataAccess.GetProcedureDataTable(SqlStatement
            , dbRequest.Parameters);
          if (dataTable != null)
          {
            retValue.ExecutedSql = SqlStatement;
            retValue.SetData(dataTable, dbRequest);
          }
        }
      }
      return retValue;
    }

    // Retrieves the column definitions.
    private LJCDBResult? TableColumns(LJCDBRequest? dbRequest = null)
    {
      LJCDBResult? retResult = null;

      if (null == dbRequest)
      {
        dbRequest = DBRequest;
      }
      if (dbRequest != null)
      {
        retResult = CreateResult(dbRequest);
        if (retResult != null)
        {
          var dataTable = GetTableSchema(dbRequest);
          if (dataTable != null)
          {
            retResult.Columns = CreateDataColumnsFromTable(dataTable);
          }
        }
      }
      return retResult;
    }

    // Retrieves the table names for the specified database.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/TableNames/*'/>
    private LJCDBResult? TableNames(string? databaseName = null
      , LJCDBRequest? dbRequest = null)
    {
      LJCDBResult? retResult = null;

      if (null == databaseName)
      {
        databaseName = mDatabaseName;
      }
      if (null == dbRequest
        && DBRequest != null)
      {
        dbRequest = DBRequest;
      }

      if (LJC.HasText(databaseName)
        && dbRequest != null)
      {
        retResult = CreateResult(dbRequest);
        var dataTable = GetTableNames(databaseName);
        if (LJC.HasTableData(dataTable)
          && LJC.HasListItems(dbRequest.Columns))
        {
          // Add TABLE_NAME if it is not already defined.
          var dataColumn = dbRequest.Columns.LJCSearchPropertyName("TABLE_NAME");
          if (null == dataColumn)
          {
            dbRequest.Columns.Add("TABLE_NAME");
          }
          //retResult.SetRows(dataTable, DBRequest.Joins);
          retResult.SetRows(dataTable, dbRequest.Joins);
        }
      }
      return retResult;
    }

    // Updates a database record.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/Update/*'/>
    private LJCDBResult? Update()
    {
      LJCDBResult? retResult = null;

      if (DBRequest != null)
      {
        retResult = CreateResult(DBRequest);
        if (mDbSqlBuilder != null)
        {
          SqlStatement = mDbSqlBuilder.CreateUpdateSql();
          AffectedCount = mDataAccess.ExecuteNonQuery(SqlStatement);
          retResult.AffectedRecords = AffectedCount;
          retResult.ExecutedSql = SqlStatement;
        }
      }
      return retResult;
    }
    #endregion

    #region LJCDataConfig Methods

    // Retrieves the data configuration.
    private static LJCDataConfig GetLJCDataConfig(string dataConfigName)
    {
      LJCDataConfig retValue;

      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      retValue = dataConfigs.Retrieve(dataConfigName);
      if (null == retValue)
      {
        var errorText = $"Data configuration '{dataConfigName}' was not found.";
        throw new MissingMemberException(errorText);
      }
      else
      {
        if (null == retValue.ConnectionType)
        {
          // Default connection type to SQL Server.
          retValue.ConnectionType = ConnectionType.SqlServer.ToString();
        }
      }
      return retValue;
    }

    // Sets the LJCDataConfig values.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/GetConfigValues/*'/>
    private void GetConfigValues(string dataConfigName)
    {
      LJCDataConfig dataConfig = GetLJCDataConfig(dataConfigName);
      DatabaseName = dataConfig.Database;
      ConnectionString = dataConfig.ConnectionString(dataConfig.ConnectionType);
      mDataAccess.ConnectionString = ConnectionString;
      ProviderName = LJCDataConfig.ProviderName(dataConfig.ConnectionType);
    }
    #endregion

    #region Private Methods

    // Adds the missing table columns from the database schema.
    private void AddTableColumns(LJCDBRequest dbRequest)
    {
      if (dbRequest != null
        && LJC.HasListItems(dbRequest.Columns))
      {
        var dataTable = GetTableSchema(dbRequest);
        if (dataTable != null)
        {
          foreach (DataColumn dataColumn in dataTable.Columns)
          {
            var foundDataColumn
              = dbRequest.Columns.LJCSearchPropertyName(dataColumn.ColumnName);
            if (null == foundDataColumn)
            {
              var newDataColumn = CreateDataColumnFromDbColumn(dataColumn);
              if (newDataColumn != null)
              {
                dbRequest.Columns.Add(newDataColumn);
              }
            }
          }
        }
      }
    }

    // Creates the result object with common values.
    /// <include file='Doc/DbDataAccess.xml'
    ///  path='items/CreateResult/*'/>
    private LJCDBResult CreateResult(LJCDBRequest dbRequest)
    {
      LJCDBResult retValue;

      if (null == dbRequest)
      {
        dbRequest = new LJCDBRequest()
        {
          RequestTypeName = "",
          DataConfigName = DataConfigName
        };
      }

      retValue = new LJCDBResult(dbRequest)
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
    private DataTable? GetTableNames(string dbName)
    {
      DataTable? retValue;

      // AND TABLE_CATALOG/TABLE_SCHEMA = 'dbName'
      var builder = new StringBuilder(64);
      builder.AppendLine("select TABLE_NAME");
      builder.AppendLine($"from {dbName}.INFORMATION_SCHEMA.TABLES");
      builder.AppendLine("where TABLE_TYPE = 'BASE TABLE'");
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
    private DataTable? GetTableSchema(LJCDBRequest dbRequest)
    {
      DataTable? retValue;

      var builder = new StringBuilder(64);
      builder.Append("select * \r\n");
      builder.Append("from ");
      if (LJC.HasText(dbRequest.SchemaName))
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

    // Creates an LJCDataColumn object from a DataColumn object.
    private static LJCDataColumn? CreateDataColumnFromDbColumn(DataColumn dbColumn)
    {
      LJCDataColumn? retValue = null;

      if (dbColumn != null)
      {
        retValue = new LJCDataColumn
        {
          AllowDBNull = dbColumn.AllowDBNull,
          AutoIncrement = dbColumn.AutoIncrement,
          Caption = dbColumn.Caption,
          ColumnName = dbColumn.ColumnName,
          DataTypeName = dbColumn.DataType.Name,
          MaxLength = dbColumn.MaxLength
        };
      }
      return retValue;
    }

    // Get the SQL Type name.
    private static void SetSQLTypeName(DataTable sqlTypesTable, LJCDataColumn dataColumn)
    {
      if (LJC.HasTableData(sqlTypesTable))
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
    private static void SetPrimaryKey(DataTable dataTable, LJCDataColumn dataColumn)
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

    // Creates an LJCDataColumns object from the DataTable columns.
    private LJCDataColumns? CreateDataColumnsFromTable(DataTable? dataTable)
    {
      LJCDataColumns? retValue = null;

      if (dataTable != null
        && LJC.HasText(DatabaseName)
        && mDBRequest != null
        && LJC.HasText(mDBRequest.TableName))
      {
        var sqlTypesTable = mDataAccess.GetColumnSQLTypes(DatabaseName
          , mDBRequest.TableName);
        if (sqlTypesTable != null)
        {
          //retValue = new LJCDataColumns();
          retValue = [];
          foreach (DataColumn dataColumn in dataTable.Columns)
          {
            var newDataColumn = CreateDataColumnFromDbColumn(dataColumn);
            if (newDataColumn != null)
            {
              SetSQLTypeName(sqlTypesTable, newDataColumn);
              SetPrimaryKey(dataTable, newDataColumn);
              retValue.Add(newDataColumn);
            }
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the Database name.</summary>
    public string? DatabaseName
    {
      get => mDatabaseName;
      set { mDatabaseName = LJCNetString.InitString(value); }
    }
    private string? mDatabaseName;

    /// <summary>Gets or sets the DBRequest object.</summary>
    public LJCDBRequest? DBRequest
    {
      get { return mDBRequest; }
      set
      {
        if (value != null)
        {
          mDBRequest = value;
          mDbSqlBuilder = new LJCDBSqlBuilder(mDBRequest);
        }
      }
    }
    private LJCDBRequest? mDBRequest;
    #endregion

    #region Private Properties

    // Gets or sets the non-query affected record count.
    private int AffectedCount { get; set; }

    // Gets or sets the LJCDataConfig name.
    private string? DataConfigName { get; set; }

    // Gets or sets the Connection string.
    private string? ConnectionString
    {
      get => mConnectionString;
      set
      {
        if (LJC.HasText(value))
        {
          if (value != null)
          {
            mConnectionString = value.Trim();
            mDataAccess.ConnectionString = mConnectionString;
          }
        }
      }
    }
    private string? mConnectionString;

    // Gets or sets the Provider name.
    internal string? ProviderName
    {
      get => mProviderName;
      set
      {
        if (LJC.HasText(value))
        {
          mProviderName = value.Trim();
          mDataAccess.ProviderName = mProviderName;
        }
      }
    }
    private string? mProviderName;

    // Gets or sets the last action SQL statement.
    private string? SqlStatement { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataAccess mDataAccess;
    private LJCDBSqlBuilder? mDbSqlBuilder;
    #endregion
  }
}
