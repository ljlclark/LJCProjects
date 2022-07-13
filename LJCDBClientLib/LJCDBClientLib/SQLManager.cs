// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// SQLManager.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCDBClientLib
{
  /// <summary>Provides SQL data manipulation methods.</summary>
  public class SQLManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/SQLManagerC/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public SQLManager(string dataConfigName, string tableName)
    {
      Reset(dataConfigName, tableName);
    }

    /// Resets the data access configuration.
    /// <include path='items/Reset/*' file='Doc/DbManager.xml'/>
    public void Reset(string dataConfigName, string tableName)
    {
      DataConfigName = dataConfigName;
      TableName = tableName;

      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      DataConfig dataConfig = dataConfigs.LJCGetByName(dataConfigName);

      string connectionString = dataConfig.GetConnectionString();
      string providerName = dataConfig.GetProviderName();

      mDataAccess = new DataAccess(connectionString, providerName);
      if (mDataAccess != null
        && NetString.HasValue(TableName))
      {
        DataDefinition = CreateDataDefinition();
        LookupColumnNames = new List<string>();
      }
    }
    #endregion

    #region DataManager Related Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public DataTable Add(object dataObject, List<string> propertyNames = null)
    {
      DataTable retValue;

      // The record must not contain a value for DB Assigned columns.
      var addSQLStatement = CreateAddSQL(dataObject);
      AffectedCount = mDataAccess.ExecuteNonQuery(addSQLStatement);

      // Return the added record.
      var keyColumns = DbCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);

      // GetDataTable sets SQLStatement.
      retValue = GetDataTable(keyColumns, propertyNames);
      SQLStatement = $"{addSQLStatement}\r\n{SQLStatement}";
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      SQLStatement = CreateDeleteSQL(keyColumns, filters);
      AffectedCount = mDataAccess.ExecuteNonQuery(SQLStatement);
    }

    // Gets a DataTable object.
    /// <include path='items/GetDataTable/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public DataTable GetDataTable(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataTable retValue;

      SQLStatement = CreateSelectSQL(keyColumns, propertyNames, filters, joins);

      // The DataColumn names are the same as the Data Source
      // Table Column Names or the SQL rename 'AS' name.
      retValue = mDataAccess.GetDataTable(SQLStatement);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCDocLib/Common/SQLManager.xml'/>
    public void Update(object dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      SQLStatement = CreateUpdateSQL(dataObject, keyColumns, propertyNames
        , filters);
      AffectedCount = mDataAccess.ExecuteNonQuery(SQLStatement);
    }
    #endregion

    #region DataManager and DbDataManager Related SQL Methods

    // Creates the SQL "Insert" string.
    private string CreateAddSQL(object dataObject)
    {
      string retValue = null;

      if (dataObject != null)
      {
        // Create request with columns containing values from the record.
        // The inserted columns must not include the DB assigned columns.
        var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition);

        var dbRequest = ManagerCommon.CreateRequest(RequestType.Insert, TableName
          , dataColumns, DataConfigName, SchemaName);
        DbSqlBuilder sqlBuilder = new DbSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateAddSql();
      }
      return retValue;
    }

    // Creates the SQL "Delete" string.
    private string CreateDeleteSQL(DbColumns keyColumns
      , DbFilters filters = null)
    {
      string retValue = null;

      // Must have a KeyColumns definition.
      if (keyColumns != null && keyColumns.Count > 0)
      {
        var requestKeys = DbCommon.RequestKeys(keyColumns, BaseDefinition);

        var dbRequest = ManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeys, filters);
        DbSqlBuilder sqlBuilder = new DbSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateDeleteSql().Trim();
      }
      return retValue;
    }

    // Creates the SQL "Select" string.
    private string CreateSelectSQL(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      string retValue;

      var requestColumns = DbCommon.RequestColumns(BaseDefinition
        , propertyNames);
      var requestKeys = DbCommon.RequestKeys(keyColumns, BaseDefinition);

      var dbRequest = ManagerCommon.CreateRequest(RequestType.Select, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeys, filters, joins);
      DbSqlBuilder sqlBuilder = new DbSqlBuilder(dbRequest);
      retValue = sqlBuilder.CreateRetrieveSql(dbRequest).Trim();
      return retValue;
    }

    // Creates the SQL "Update" string.
    private string CreateUpdateSQL(object dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      string retValue = null;

      if (dataObject != null)
      {
        var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition
          , propertyNames);
        var requestKeys = DbCommon.RequestDataKeys(keyColumns, BaseDefinition);

        var dbRequest = ManagerCommon.CreateRequest(RequestType.Update, TableName
          , dataColumns, DataConfigName, SchemaName, requestKeys, filters);
        DbSqlBuilder sqlBuilder = new DbSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateUpdateSql().Trim();
      }
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Adds the lookup column names.
    /// <include path='items/SetLookupColumns/*' file='Doc/DataManager.xml'/>
    public void SetLookupColumns(string[] columnNames)
    {
      foreach (string columnName in columnNames)
      {
        string existingName = LookupColumnNames.Find(x => x == columnName);
        if (null == existingName)
        {
          LookupColumnNames.Add(columnName);
        }
      }
    }
    #endregion

    #region DataManager Related Create Data Methods

    // Creates a DataDefinition value.
    private DbColumns CreateDataDefinition()
    {
      DbColumns retValue = null;

      string sql = $"select * from {TableName}";
      DataTable dataTable = mDataAccess.GetSchemaOnly(sql);
      if (dataTable != null)
      {
        var dataColumns = TableGridData.DataColumnsClone(dataTable);
        BaseDefinition = TableGridData.GetDbColumns(dataColumns);
        retValue = BaseDefinition.Clone();
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the base data definition columns collection.</summary>
    public DbColumns BaseDefinition { get; private set; }

    /// <summary>Gets or sets the data configuration name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      private set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>Gets the data definition columns collection.</summary>
    public DbColumns DataDefinition { get; private set; }

    /// <summary>Gets or sets the LookupColumn names.</summary>
    public List<string> LookupColumnNames { get; set; }

    /// <summary>The Schema name.</summary>
    public string SchemaName
    {
      get { return mSchemaName; }
      set { mSchemaName = NetString.InitString(value); }
    }
    private string mSchemaName;

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }

    /// <summary>The primary table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion

    #region Class Data

    private DataAccess mDataAccess;
    #endregion
  }
}
