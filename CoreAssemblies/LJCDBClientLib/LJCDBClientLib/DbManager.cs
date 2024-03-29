﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbManager.cs
using LJCDBDataAccess;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBClientLib
{
  /// <summary>Provides DbDataAccess data manipulation methods.</summary>
  public class DbManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='Doc/DbManager.xml'/>
    public DbManager(string dataConfigName, string tableName)
    {
      Reset(dataConfigName, tableName);
    }

    /// Resets the data access configuration.
    /// <include path='items/Reset/*' file='Doc/DbManager.xml'/>
    public void Reset(string dataConfigName, string tableName)
    {
      DataConfigName = dataConfigName;
      TableName = tableName;

      mDbDataAccess = new DbDataAccess(DataConfigName);
      if (mDbDataAccess != null
        && NetString.HasValue(TableName))
      {
        DataDefinition = CreateDataDefinition();
        LookupColumnNames = new List<string>();
      }
    }
    #endregion

    #region DataManager related Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbResult Add(object dataObject, List<string> propertyNames = null)
    {
      DbResult retValue;

      // The record must not contain a value for DB Assigned columns.
      var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
      var keyColumns = DbCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);

      var dbRequest = ManagerCommon.CreateRequest(RequestType.Insert, TableName
        , dataColumns, DataConfigName, SchemaName, keyColumns);

      // The DbResult contains a record with only the DB Assigned columns.
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      if (NetCommon.HasItems(keyColumns)
        || NetCommon.HasItems(filters))
      {
        var requestKeys = DbCommon.RequestKeys(keyColumns, BaseDefinition);

        var dbRequest = ManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeys, filters);
        ExecuteRequest(dbRequest);
      }
    }

    // Executes the supplied request.
    /// <include path='items/ExecuteRequest/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public DbResult ExecuteRequest(DbRequest dbRequest)
    {
      DbResult retValue;

      retValue = mDbDataAccess.Execute(dbRequest);
      if (retValue != null)
      {
        AffectedCount = retValue.AffectedRecords;
        SQLStatement = retValue.ExecutedSql;
      }
      return retValue;
    }

    // Loads a collection of data records.
    /// <include path='items/Load/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public DbResult Load(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      DbResult retValue;

      var requestColumns = DbCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeys = DbCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      var dbRequest = ManagerCommon.CreateRequest(RequestType.Load, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeys, filters, joins);
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public DbResult Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      DbResult retValue;

      var requestColumns = DbCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeys = DbCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      var dbRequest = ManagerCommon.CreateRequest(RequestType.Select, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeys, filters, joins);
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public void Update(object dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
      var requestKeys = DbCommon.RequestDataKeys(keyColumns, BaseDefinition);

      var dbRequest = ManagerCommon.CreateRequest(RequestType.Update, TableName
        , dataColumns, DataConfigName, SchemaName, requestKeys, filters);
      ExecuteRequest(dbRequest);
    }
    #endregion

    #region Other Public Methods

    // Adds the lookup column names.
    /// <include path='items/SetLookupColumns/*' file='Doc/DataManager.xml'/>
    public void SetLookupColumns(string[] propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        string existingName = LookupColumnNames.Find(x => x == propertyName);
        if (null == existingName)
        {
          LookupColumnNames.Add(propertyName);
        }
      }
    }
    #endregion

    #region DataManager Related Create Data Methods

    // Creates a DataDefinition value.
    private DbColumns CreateDataDefinition()
    {
      DbColumns retValue = null;

      var dbRequest = new DbRequest(RequestType.SchemaOnly, TableName);
      var dbResult = ExecuteRequest(dbRequest);
      //if (dbResult != null)
      if (DbResult.HasColumns(dbResult))
      {
        BaseDefinition = dbResult.Columns.Clone();
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

    private DbDataAccess mDbDataAccess;
    #endregion
  }
}
