// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbManager.cs
using LJCDBDataAccess5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDBClientLib5
{
  /// <summary>Provides DbDataAccess data manipulation methods.</summary>
  public class LJCDbManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbManagerC/*' file='Doc/DbManager.xml'/>
    public LJCDbManager(string dataConfigName, string tableName)
    {
      BaseDefinition = [];
      mDataConfigName = "";
      DataDefinition = [];
      LookupColumnNames = [];
      mSchemaName = "";
      SQLStatement = "";
      mTableName = "";

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
        && LJC.HasValue(TableName))
      {
        DataDefinition = CreateDataDefinition();
        LookupColumnNames = new List<string>();
      }
    }
    #endregion

    #region DataManager related Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public LJCDBResult? Add(object dataObject, List<string>? propertyNames = null)
    {
      LJCDBResult? retValue = null;

      // The record must not contain a value for DB Assigned columns.
      var dataColumns = LJCDBCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
      var keyColumns = LJCDBCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Insert, TableName
        , dataColumns, DataConfigName, SchemaName, keyColumns);

      // The DbResult contains a record with only the DB Assigned columns.
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      if (LJC.HasItems(keyColumns)
        || LJC.HasItems(filters))
      {
        var requestKeys = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition);

        var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeys, filters);
        ExecuteRequest(dbRequest);
      }
    }

    // Executes the supplied request.
    /// <include path='items/ExecuteRequest/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public LJCDBResult? ExecuteRequest(LJCDBRequest? dbRequest)
    {
      LJCDBResult? retValue = null;

      retValue = mDbDataAccess.Execute(dbRequest);
      if (retValue != null)
      {
        AffectedCount = retValue.AffectedRecords;
        if (LJC.HasValue(retValue.ExecutedSql))
        {
          SQLStatement = retValue.ExecutedSql;
        }
      }
      return retValue;
    }

    // Loads a collection of data records.
    /// <include path='items/Load/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public LJCDBResult? Load(LJCDataColumns keyColumns, List<string>? propertyNames = null
      , LJCDBFilters? filters = null, LJCDBJoins? joins = null)
    {
      LJCDBResult? retValue;

      var requestColumns = LJCDBCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeys = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Load, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeys, filters, joins);
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public LJCDBResult? Retrieve(LJCDataColumns keyColumns, List<string>? propertyNames = null
      , LJCDBFilters? filters = null, LJCDBJoins? joins = null)
    {
      LJCDBResult? retValue;

      var requestColumns = LJCDBCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeys = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Select, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeys, filters, joins);
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../../CoreUtilities/LJCGenDoc/Common/DbManager.xml'/>
    public void Update(object dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      var dataColumns = LJCDBCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
      var requestKeys = LJCDBCommon.RequestDataKeys(keyColumns, BaseDefinition);

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.Update, TableName
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
    private LJCDataColumns? CreateDataDefinition()
    {
      LJCDataColumns? retColumns = null;

      var dbRequest = new LJCDBRequest(RequestType.SchemaOnly, TableName);
      var dbResult = ExecuteRequest(dbRequest);
      if (dbResult != null
        && LJCDBResult.HasColumns(dbResult))
      {
        BaseDefinition = dbResult.Columns.Clone();
        retColumns = BaseDefinition.Clone();
      }
      return retColumns;
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
      get { return mDataConfigName; }
      private set { mDataConfigName = LJCNetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>Gets the data definition columns collection.</summary>
    public LJCDataColumns DataDefinition { get; private set; }

    /// <summary>Gets or sets the LookupColumn names.</summary>
    public List<string> LookupColumnNames { get; set; }

    /// <summary>The Schema name.</summary>
    public string SchemaName
    {
      get { return mSchemaName; }
      set { mSchemaName = LJCNetString.InitString(value); }
    }
    private string mSchemaName;

    /// <summary>Gets or sets the last SQL statement.</summary>
    public string SQLStatement { get; set; }

    /// <summary>The primary table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = LJCNetString.InitString(value); }
    }
    private string mTableName;
    #endregion

    #region Class Data

    private DbDataAccess? mDbDataAccess;
    #endregion
  }
}
