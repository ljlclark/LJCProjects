// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SQLManager.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDBMessage5;
using LJCNetCommon5;
using System.Data;

namespace LJCDBClientLib5
{
  /// <summary>Provides SQL data manipulation methods.</summary>
  public class LJCSQLManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/SQLManagerC/*' file='Doc/SQLManager.xml'/>
    public LJCSQLManager(string dataConfigName, string tableName
      , string? connectionString = null, string? providerName = null)
    {
      // Initialize required properties.
      BaseDefinition = [];
      DataDefinition = [];
      DbAssignedColumns = [];
      LookupColumnNames = [];
      mDataAccess = new LJCDataAccess("");
      mDataConfigName = "";
      mSchemaName = "";
      mTableName = tableName;

      Reset(dataConfigName, tableName, connectionString, providerName);
    }

    /// Resets the data access configuration.
    /// <include path='items/Reset/*' file='Doc/SQLManager.xml'/>
    public void Reset(string dataConfigName, string tableName
      , string? connectionString = null, string? providerName = null)
    {
      DataConfigName = dataConfigName;
      TableName = tableName;

      LJC.CheckArgument<string>(dataConfigName);
      LJC.CheckArgument<string>(tableName);

      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      var dataConfig = dataConfigs.Retrieve(dataConfigName);

      if (!LJC.HasValue(connectionString))
      {
        connectionString = dataConfig.ConnectionString();
      }
      if (!LJC.HasValue(providerName))
      {
        providerName = LJCDataConfig.ProviderName();
      }
      if (LJC.HasValue(connectionString))
      {
        mDataAccess = new LJCDataAccess(connectionString, providerName);
        if (mDataAccess != null)
        {
          var dataDefinition = CreateDataDefinition();
          if (dataDefinition != null)
          {
            DataDefinition = dataDefinition;
          }
          //LookupColumnNames = new List<string>();
          LookupColumnNames = [];
        }
      }
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='Doc/SQLManager.xml'/>
    public DataTable? Add(object dataObject, List<string>? propertyNames = null)
    {
      LJC.CheckArgument(dataObject);
      DataTable? retTable = null;

      // The record must not contain a value for DB Assigned columns.
      var addSQLStatement = CreateAddSQL(dataObject, propertyNames);
      AffectedCount = mDataAccess.ExecuteNonQuery(addSQLStatement);

      // Return the added record.
      var keyColumns = LJCDBCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);
      if (LJC.HasItems(keyColumns))
      {
        // GetDataTable sets SQLStatement.
        retTable = GetDataTable(keyColumns, DbAssignedColumns);
        SQLStatement = $"{addSQLStatement}\r\n{SQLStatement}";
      }
      return retTable;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='Doc/SQLManager.xml'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      SQLStatement = CreateDeleteSQL(keyColumns, filters);
      if (LJC.HasValue(SQLStatement))
      {
        AffectedCount = mDataAccess.ExecuteNonQuery(SQLStatement);
      }
    }

    // Gets a DataTable object.
    /// <include path='items/GetDataTable/*' file='Doc/SQLManager.xml'/>
    public DataTable? GetDataTable(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      SQLStatement = CreateSelectSQL(keyColumns, propertyNames, filters, joins);

      // The DataColumn names are the same as the Data Source
      // Table Column Names or the SQL rename 'AS' name.
      var retValue = mDataAccess.GetDataTable(SQLStatement);
      return retValue;
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/DataManager.xml'/>
    public void MapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      if (LJC.HasItems(DataDefinition))
      {
        DataDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
      }

      BaseDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
    }

    // Adds the lookup column names.
    /// <include path='items/SetLookupColumns/*' file='Doc/DataManager.xml'/>
    public void SetLookupColumns(string[] propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        string? existingName = LookupColumnNames.Find(x => x == propertyName);
        if (null == existingName)
        {
          LookupColumnNames.Add(propertyName);
        }
      }
    }

    // Updates the record.
    /// <include path='items/Update/*' file='Doc/SQLManager.xml'/>
    public void Update(object dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      LJC.CheckArgument<object>(dataObject);

      SQLStatement = CreateUpdateSQL(dataObject, keyColumns, propertyNames
        , filters);
      if (LJC.HasValue(SQLStatement))
      {
        AffectedCount = mDataAccess.ExecuteNonQuery(SQLStatement);
      }
    }
    #endregion

    #region Private Methods

    // Creates the SQL "Insert" string.
    private string? CreateAddSQL(object dataObject
      , List<string>? propertyNames = null)
    {
      string? retValue = null;

      if (dataObject != null)
      {
        // Create request with columns containing values from the record.
        // The inserted columns must not include the DB assigned columns.
        var dataColumns = LJCDBCommon.RequestDataColumns(dataObject
          , BaseDefinition, propertyNames);
        var keyColumns = LJCDBCommon.RequestLookupKeys(dataObject, BaseDefinition
          , LookupColumnNames);

        var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Insert
          , TableName, dataColumns, DataConfigName, SchemaName, keyColumns);
        var sqlBuilder = new LJCDBSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateAddSql();
      }
      return retValue;
    }

    // Creates a DataDefinition value.
    private LJCDataColumns? CreateDataDefinition()
    {
      LJCDataColumns? retValue = null;

      string sql = $"select * from {TableName}";
      var dataTable = mDataAccess.GetSchemaOnly(sql);
      if (dataTable != null)
      {
        var dbColumns = LJCDbColumns.Clone(dataTable);
        if (dbColumns != null)
        {
          var baseDefinition = LJCDbColumns.ToDataColumns(dbColumns);
          if (baseDefinition != null)
          {
            BaseDefinition = baseDefinition;
          }
        }
        if (BaseDefinition != null)
        {
          retValue = BaseDefinition.Clone();
        }
      }
      return retValue;
    }

    // Creates the SQL "Delete" string.
    private string? CreateDeleteSQL(LJCDataColumns keyColumns
      , LJCDBFilters? filters = null)
    {
      string? retValue = null;

      // Must have a KeyColumns definition.
      if (LJC.HasItems(keyColumns)
        || filters != null)
      {
        var requestKeyColumns = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition);

        var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeyColumns, filters);
        var sqlBuilder = new LJCDBSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateDeleteSql().Trim();
      }
      return retValue;
    }

    // Creates the SQL "Select" string.
    private string CreateSelectSQL(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      var requestColumns = LJCDBCommon.RequestColumns(BaseDefinition
        , propertyNames);
      var requestKeyColumns = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition);

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Select, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeyColumns, filters
        , joins);
      var sqlBuilder = new LJCDBSqlBuilder(dbRequest);
      var retValue = sqlBuilder.CreateRetrieveSql(dbRequest).Trim();
      return retValue;
    }

    // Creates the SQL "Update" string.
    private string? CreateUpdateSQL(object dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      string? retValue = null;

      if (dataObject != null
        && (LJC.HasItems(keyColumns)
        || filters != null))
      {
        var dataColumns = LJCDBCommon.RequestDataColumns(dataObject, BaseDefinition
      , propertyNames);
        var requestKeyColumns = LJCDBCommon.RequestDataKeys(keyColumns
          , BaseDefinition);

        var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Update, TableName
          , dataColumns, DataConfigName, SchemaName, requestKeyColumns, filters);
        var sqlBuilder = new LJCDBSqlBuilder(dbRequest);
        retValue = sqlBuilder.CreateUpdateSql().Trim();
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the base data definition columns collection.</summary>
    public LJCDataColumns BaseDefinition { get; private set; }

    /// <summary>Gets or sets the data configuration name.</summary>
    public string DataConfigName
    {
      get => mDataConfigName;
      private set
      {
        if (value != null)
        {
          mDataConfigName = value.Trim();
        }
      }
    }
    private string mDataConfigName;

    /// <summary>Gets the data definition columns collection.</summary>
    public LJCDataColumns DataDefinition { get; private set; }

    /// <summary>Gets or sets the Database assigned columns.</summary>
    public List<string> DbAssignedColumns { get; set; }

    /// <summary>Gets or sets the LookupColumn names.</summary>
    public List<string> LookupColumnNames { get; set; }

    /// <summary>The Schema name.</summary>
    public string SchemaName
    {
      get { return mSchemaName; }
      set
      {
        if (value != null)
        {
          mSchemaName = value.Trim();
        }
      }
    }
    private string mSchemaName;

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string? SQLStatement { get; set; }

    /// <summary>The primary table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set
      {
        if (value != null)
        {
          mTableName = value.Trim();
        }
      }
    }
    private string mTableName;
    #endregion

    #region Class Data

    private LJCDataAccess mDataAccess;
    #endregion
  }
}
