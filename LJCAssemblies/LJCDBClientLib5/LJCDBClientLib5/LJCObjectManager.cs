// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ObjectManager.cs
using LJCDataAccess5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDBClientLib5
{
  // Provides object specific data manipulation methods.
  /// <include path='items/ObjectManager/*' file='Doc/ObjectManager.xml'/>
  public class LJCObjectManager<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='Doc/ObjectManager.xml'/>
    public LJCObjectManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName, string? schemaName = null)
    {
      // Testing in LJCDBClientLib\TestObjectManager.
      DbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
      mTableName = tableName;
      SchemaName = schemaName;
      DataManager = new LJCDataManager(DbServiceRef, DataConfigName, TableName, SchemaName);
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='Doc/ObjectManager.xml'/>
    public TData? Add(TData dataObject, List<string>? propertyNames = null)
    {
      TData? retValue = default;

      if (dataObject != null)
      {
        // The record must not contain a value for DB Assigned columns.
        // The DbResult contains a record with only the DB Assigned columns.
        var dbResult = DataManager.Add(dataObject, propertyNames);
        AffectedCount = DataManager.AffectedCount;
        if (LJC.HasValue(DataManager.SQLStatement))
        {
          SQLStatement = DataManager.SQLStatement;
        }
        if (LJCDBResult.HasRows(dbResult))
        {
          retValue = CreateData(dbResult.Rows[0].Values);
        }
      }
      return retValue;
    }

    // Creates the Load DbRequest object.
    /// <include path='items/CreateLoadRequest/*' file='Doc/ObjectManager.xml'/>
    public LJCDBRequest CreateLoadRequest(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      LJCDBRequest retValue = DataManager.CreateLoadRequest(keyColumns, propertyNames
        , filters, joins);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='Doc/ObjectManager.xml'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      DataManager.Delete(keyColumns, filters);
      AffectedCount = DataManager.AffectedCount;
      if (LJC.HasValue(DataManager.SQLStatement))
      {
        SQLStatement = DataManager.SQLStatement;
      }
    }

    // Executes a non-query client SQL statement.
    /// <include path='items/ExecuteClientSql/*' file='Doc/ObjectManager.xml'/>
    public void ExecuteClientSql(string sql)
    {
      DataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
      AffectedCount = DataManager.AffectedCount;
      if (LJC.HasValue(DataManager.SQLStatement))
      {
        SQLStatement = DataManager.SQLStatement;
      }
    }

    // Execute the supplied request.
    /// <include path='items/ExecuteRequest/*' file='Doc/ObjectManager.xml'/>
    public LJCDBResult? ExecuteRequest(LJCDBRequest dbRequest)
    {
      return DataManager.ExecuteRequest(dbRequest);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='Doc/ObjectManager.xml'/>
    public TList? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      TList? retValue = null;

      LJCDBResult? dbResult = DataManager.Load(keyColumns, propertyNames, filters, joins);
      if (LJC.HasValue(DataManager.SQLStatement))
      {
        SQLStatement = DataManager.SQLStatement;
      }
      if (LJCDBResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Executes a "Load" client SQL statement.
    /// <include path='items/LoadClientSql/*' file='Doc/ObjectManager.xml'/>
    public TList? LoadClientSql(string sql)
    {
      LJCDBResult? dbResult;
      TList? retValue = null;

      dbResult = DataManager.ExecuteClientSql(RequestType.LoadSQL, sql);
      SQLStatement = DataManager.SQLStatement;
      if (LJCDBResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadProcedure/*' file='Doc/ObjectManager.xml'/>
    public TList? LoadProcedure(string procedureName
      , LJCProcedureParameters? parameters = null, LJCDBJoins? joins = null)
    {
      TList? retValue = null;

      LJCDBResult? dbResult = DataManager.LoadProcedure(procedureName, parameters
        , joins);
      SQLStatement = DataManager.SQLStatement;
      if (LJCDBResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='Doc/ObjectManager.xml'/>
    public TData? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      TData? retValue = default;

      LJCDBResult? dbResult = DataManager.Retrieve(keyColumns, propertyNames
        , filters, joins);
      if (dbResult != null)
      {
        SQLStatement = DataManager.SQLStatement;
        if (LJCDBResult.HasRows(dbResult))
        {
          var row = dbResult.Rows[0];
          retValue = CreateData(row.Values);
        }
      }
      return retValue;
    }

    // Executes a "Retrieve" client SQL statement.
    /// <include path='items/RetrieveClientSql/*' file='Doc/ObjectManager.xml'/>
    public TData? RetrieveClientSql(string sql)
    {
      LJCDBResult? dbResult;
      TData? retValue = default;

      dbResult = DataManager.ExecuteClientSql(RequestType.RetrieveSQL, sql);
      SQLStatement = DataManager.SQLStatement;
      if (LJCDBResult.HasRows(dbResult))
      {
        var row = dbResult.Rows[0];
        retValue = CreateData(row.Values);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='Doc/ObjectManager.xml'/>
    public void Update(TData dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      DataManager.Update(dataObject, keyColumns, propertyNames, filters);
      AffectedCount = DataManager.AffectedCount;
      SQLStatement = DataManager.SQLStatement;
    }
    #endregion

    #region Other Public Methods

    // Creates a set of columns that match the supplied list.
    /// <include path='items/GetColumns/*' file='Doc/ObjectManager.xml'/>
    public LJCDataColumns? GetColumns(List<string> propertyNames)
    {
      LJCDataColumns? retColumns = null;
      if (LJC.HasItems(DataManager.DataDefinition))
      {
        retColumns = DataManager.DataDefinition.LJCGetColumns(propertyNames);
      }
      return retColumns;
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/ObjectManager.xml'/>
    public void MapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      DataManager.MapNames(columnName, propertyName, renameAs, caption);
    }

    // Sets the database assigned value column names.
    /// <include path='items/SetDbAssignedColumns/*' file='Doc/DataManager.xml'/>
    public void SetDbAssignedColumns(string[] propertyNames)
    {
      DataManager.SetDbAssignedColumns(propertyNames);
    }

    // Adds the lookup column names.
    /// <include path='items/SetLookupColumns/*' file='Doc/DataManager.xml'/>
    public void SetLookupColumns(string[] propertyNames)
    {
      DataManager.SetLookupColumns(propertyNames);
    }
    #endregion

    #region Create Data Methods

    // Creates a collection from the result records.
    /// <include path='items/CreateCollection/*' file='Doc/ObjectManager.xml'/>
    public TList CreateCollection(LJCDBResult? dbResult)
    {
      // Also in LJC.DBMessage.ResultConverter.
      // Used here because TList and TData are already defined.
      //TList retValue = new TList();
      TList retValue = new();

      if (dbResult != null
        && LJCDBResult.HasRows(dbResult))
      {
        foreach (LJCDBRow dbRow in dbResult.Rows)
        {
          LJCDataValues? dbValues = dbRow.Values;
          if (LJC.HasItems(dbValues))
          {
            TData? data = CreateData(dbValues);
            if (data != null)
            {
              retValue.Add(data);
            }
          }
        }
      }
      return retValue;
    }

    // Creates a data object from the result record.
    /// <include path='items/CreateData/*' file='Doc/ObjectManager.xml'/>
    public TData? CreateData(LJCDataValues? dbValues)
    {
      // Also in LJCDBMessage.ResultConverter.
      // Used here because TData is already defined.
      TData? retValue = null;

      if (dbValues != null)
      {
        // Populate a data object with the result values.
        // Uses retValue as an object and processes with reflection.
        retValue = new TData();
        LJCDBCommon.SetObjectValues(dbValues, retValue);
        LJCDBCommon.ClearChanged(retValue);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the base data definition columns collection.</summary>
    public LJCDataColumns BaseDefinition
    {
      get { return DataManager.BaseDefinition; }
    }

    /// <summary>Gets DbServiceRef object.</summary>
    public LJCDbServiceRef DbServiceRef { get; private set; }

    /// <summary>Gets or sets the data configuration name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      private set
      {
        if (value != null)
        {
          mDataConfigName = value.Trim();
        }
      }
    }
    private string mDataConfigName;

    /// <summary>Gets a reference to the Data Definition columns collection.</summary>
    public LJCDataColumns? DataDefinition
    {
      get { return DataManager.DataDefinition; }
    }

    /// <summary>The Schema name.</summary>
    public string? SchemaName
    {
      get => mSchemaName;
      set { mSchemaName = LJCNetString.InitString(value); }
    }
    private string? mSchemaName;

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string? SQLStatement { get; set; }

    /// <summary>The primary table name.</summary>
    public string TableName
    {
      get => mTableName;
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

    #region Other Properties

    /// <summary>The Data Manager object.</summary>
    public LJCDataManager DataManager { get; private set; }
    #endregion
  }
}
