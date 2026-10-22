// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCObjectManager5.cs
using LJCDataAccess5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDBClientLib5
{
  // Provides object specific data manipulation methods.
  /// <include file='Doc/ProjectObjectManager.xml'
  ///  path='members/ObjectManager/*'/>
  public class LJCObjectManager<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region Properties

    // Gets or sets the non-select affected record count.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount { get; set; }

    // Gets the base data definition columns collection.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/BaseDefinition/*'/>
    public LJCDataColumns BaseDefinition
    {
      get => DataManager.BaseDefinition;
    }

    // Gets DbServiceRef object.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/DbServiceRef/*'/>
    public LJCDbServiceRef DbServiceRef { get; private set; }

    // Gets or sets the data configuration name.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/DataConfigName/*'/>
    public string DataConfigName
    {
      get => mDataConfigName;
      private set => mDataConfigName = value.Trim();
    }
    private string mDataConfigName;

    // Gets a reference to the Data Definition columns collection.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/DataDefinition/*'/>
    public LJCDataColumns? DataDefinition
    {
      get => DataManager.DataDefinition;
    }

    // The Schema name.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/SchemaName/*'/>
    public string? SchemaName
    {
      get => mSchemaName;
      set => mSchemaName = value?.Trim();
    }
    private string? mSchemaName;

    // Gets or sets the last SQL statement.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/SQLStatement/*'/>
    public string? SQLStatement { get; set; }

    // The primary table name.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/TableName/*'/>
    public string? TableName
    {
      get => mTableName;
      set => mTableName = value?.Trim();
    }
    private string? mTableName;
    #endregion

    #region Other Properties

    // The Data Manager object.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/DataManager/*'/>
    public LJCDataManager DataManager { get; private set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCObjectManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName, string? schemaName = null)
    {
      // Testing in LJCDBClientLib\TestObjectManager.
      DbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
      mTableName = tableName;
      SchemaName = schemaName;
      DataManager = new LJCDataManager(DbServiceRef, DataConfigName, TableName
        , SchemaName);
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/Add/*'/>
    public TData? Add(TData dataObject, List<string>? propertyNames = null)
    {
      TData? retValue = default;

      if (dataObject != null)
      {
        // The record must not contain a value for DB Assigned columns.
        // The DbResult contains a record with only the DB Assigned columns.
        var dbResult = DataManager.Add(dataObject, propertyNames);
        AffectedCount = DataManager.AffectedCount;
        if (LJC.HasText(DataManager.SQLStatement))
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/CreateLoadRequest/*'/>
    public LJCDBRequest CreateLoadRequest(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      LJCDBRequest retValue = DataManager.CreateLoadRequest(keyColumns, propertyNames
        , filters, joins);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/Delete/*'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      DataManager.Delete(keyColumns, filters);
      AffectedCount = DataManager.AffectedCount;
      if (LJC.HasText(DataManager.SQLStatement))
      {
        SQLStatement = DataManager.SQLStatement;
      }
    }

    // Executes a non-query client SQL statement.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/ExecuteClientSql/*'/>
    public void ExecuteClientSql(string sql)
    {
      DataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
      AffectedCount = DataManager.AffectedCount;
      if (LJC.HasText(DataManager.SQLStatement))
      {
        SQLStatement = DataManager.SQLStatement;
      }
    }

    // Execute the supplied request.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/ExecuteRequest/*'/>
    public LJCDBResult? ExecuteRequest(LJCDBRequest dbRequest)
    {
      return DataManager.ExecuteRequest(dbRequest);
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/Load/*'/>
    public TList? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      TList? retValue = null;

      LJCDBResult? dbResult = DataManager.Load(keyColumns, propertyNames, filters, joins);
      if (LJC.HasText(DataManager.SQLStatement))
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/LoadClientSql/*'/>
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/LoadProcedure/*'/>
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/Retrieve/*'/>
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/RetrieveClientSql/*'/>
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/Update/*'/>
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/GetColumns/*'/>
    public LJCDataColumns? GetColumns(List<string> propertyNames)
    {
      LJCDataColumns? retColumns = null;
      if (LJC.HasListItems(DataManager.DataDefinition))
      {
        retColumns = DataManager.DataDefinition.LJCColumns(propertyNames);
      }
      return retColumns;
    }

    // Maps the column property and rename values.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/MapNames/*'/>
    public void MapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      DataManager.MapNames(columnName, propertyName, renameAs, caption);
    }

    // Sets the database assigned value column names.
    /// <include file='Doc/DataManager.xml'
    ///  path='members/SetDbAssignedColumns/*'/>
    public void SetDbAssignedColumns(string[] propertyNames)
    {
      DataManager.SetDbAssignedColumns(propertyNames);
    }

    // Adds the lookup column names.
    /// <include file='Doc/DataManager.xml'
    ///  path='members/SetLookupColumns/*'/>
    public void SetLookupColumns(string[] propertyNames)
    {
      DataManager.SetLookupColumns(propertyNames);
    }
    #endregion

    #region Create Data Methods

    // Creates a collection from the result records.
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/CreateCollection/*'/>
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
          if (LJC.HasListItems(dbValues))
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
    /// <include file='Doc/ObjectManager.xml'
    ///  path='members/CreateData/*'/>
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
  }
}
