// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// ObjectManager.cs
using LJCNetCommon;
using LJCDBMessage;
using LJCDataAccess;
using System.Collections.Generic;

namespace LJCDBClientLib
{
  // Provides object specific data manipulation methods.
  /// <include path='items/ObjectManager/*' file='Doc/ObjectManager.xml'/>
  public class ObjectManager<TData, TList>
    where TData : class, new()
    where TList : List<TData>, new()
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='Doc/ObjectManager.xml'/>
    public ObjectManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName, string schemaName = null)
    {
      // Testing in LJCDBClientLib\TestObjectManager.
      DbServiceRef = dbServiceRef;
      DataConfigName = dataConfigName;
      TableName = tableName;
      SchemaName = schemaName;
      DataManager = new DataManager(DbServiceRef, DataConfigName, TableName, SchemaName);
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='Doc/ObjectManager.xml'/>
    public TData Add(TData dataObject, List<string> propertyNames = null)
    {
      TData retValue = default;

      // The record must not contain a value for DB Assigned columns.
      // The DbResult contains a record with only the DB Assigned columns.
      DbResult dbResult = DataManager.Add(dataObject, propertyNames);
      AffectedCount = DataManager.AffectedCount;
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Creates the Load DbRequest object.
    /// <include path='items/CreateLoadRequest/*' file='Doc/ObjectManager.xml'/>
    public DbRequest CreateLoadRequest(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbRequest retValue = DataManager.CreateLoadRequest(keyColumns, propertyNames
        , filters, joins);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='Doc/ObjectManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      DataManager.Delete(keyColumns, filters);
      AffectedCount = DataManager.AffectedCount;
      SQLStatement = DataManager.SQLStatement;
    }

    // Executes a non-query client SQL statement.
    /// <include path='items/ExecuteClientSql/*' file='Doc/ObjectManager.xml'/>
    public void ExecuteClientSql(string sql)
    {
      DataManager.ExecuteClientSql(RequestType.ExecuteSQL, sql);
      AffectedCount = DataManager.AffectedCount;
      SQLStatement = DataManager.SQLStatement;
    }

    // Execute the supplied request.
    /// <include path='items/ExecuteRequest/*' file='Doc/ObjectManager.xml'/>
    public DbResult ExecuteRequest(DbRequest dbRequest)
    {
      return DataManager.ExecuteRequest(dbRequest);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='Doc/ObjectManager.xml'/>
    public TList Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      TList retValue = null;

      DbResult dbResult = DataManager.Load(keyColumns, propertyNames, filters, joins);
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Executes a "Load" client SQL statement.
    /// <include path='items/LoadClientSql/*' file='Doc/ObjectManager.xml'/>
    public TList LoadClientSql(string sql)
    {
      DbResult dbResult;
      TList retValue = null;

      dbResult = DataManager.ExecuteClientSql(RequestType.LoadSQL, sql);
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadProcedure/*' file='Doc/ObjectManager.xml'/>
    public TList LoadProcedure(string procedureName
      , ProcedureParameters parameters = null, DbJoins joins = null)
    {
      TList retValue = null;

      DbResult dbResult = DataManager.LoadProcedure(procedureName, parameters
        , joins);
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='Doc/ObjectManager.xml'/>
    public TData Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      TData retValue = default;

      DbResult dbResult = DataManager.Retrieve(keyColumns, propertyNames, filters, joins);
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Executes a "Retrieve" client SQL statement.
    /// <include path='items/RetrieveClientSql/*' file='Doc/ObjectManager.xml'/>
    public TData RetrieveClientSql(string sql)
    {
      DbResult dbResult;
      TData retValue = default;

      dbResult = DataManager.ExecuteClientSql(RequestType.RetrieveSQL, sql);
      SQLStatement = DataManager.SQLStatement;
      if (DbResult.HasRows(dbResult))
      {
        retValue = CreateData(dbResult.Rows[0].Values);
      }
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='Doc/ObjectManager.xml'/>
    public void Update(TData dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      DataManager.Update(dataObject, keyColumns, propertyNames, filters);
      AffectedCount = DataManager.AffectedCount;
      SQLStatement = DataManager.SQLStatement;
    }
    #endregion

    #region Other Public Methods

    // Creates a set of columns that match the supplied list.
    /// <include path='items/GetColumns/*' file='Doc/ObjectManager.xml'/>
    public DbColumns GetColumns(List<string> propertyNames)
    {
      return DataManager.DataDefinition.LJCGetColumns(propertyNames);
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/ObjectManager.xml'/>
    public void MapNames(string columnName, string propertyName = null
      , string renameAs = null, string caption = null)
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
    public TList CreateCollection(DbResult dbResult)
    {
      // Also in LJC.DBMessage.ResultConverter.
      // Used here because TList and TData are already defined.
      TList retValue = new TList();

      if (DbResult.HasRows(dbResult))
      {
        foreach (DbRow dbRow in dbResult.Rows)
        {
          DbValues dbValues = dbRow.Values;
          if (DbValues.HasItems(dbValues))
          {
            TData data = CreateData(dbValues);
            retValue.Add(data);
          }
        }
      }
      return retValue;
    }

    // Creates a data object from the result record.
    /// <include path='items/CreateData/*' file='Doc/ObjectManager.xml'/>
    public TData CreateData(DbValues dbValues)
    {
      // Also in LJCDBMessage.ResultConverter.
      // Used here because TData is already defined.
      TData retValue;

      // Populate a data object with the result values.
      // Uses retValue as an object and processes with reflection.
      retValue = new TData();
      DbCommon.SetObjectValues(dbValues, retValue);
      DbCommon.ClearChanged(retValue);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the base data definition columns collection.</summary>
    public DbColumns BaseDefinition
    {
      get { return DataManager.BaseDefinition; }
    }

    /// <summary>Gets DbServiceRef object.</summary>
    public DbServiceRef DbServiceRef { get; private set; }

    /// <summary>Gets or sets the data configuration name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      private set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>Gets a reference to the Data Definition columns collection.</summary>
    public DbColumns DataDefinition
    {
      get { return DataManager.DataDefinition; }
    }

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

    #region Other Properties

    /// <summary>The Data Manager object.</summary>
    public DataManager DataManager { get; private set; }
    #endregion
  }
}
