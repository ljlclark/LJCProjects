// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataManager.cs
using CipherLib;
using LJCDataAccess;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDBClientLib
{
  // Provides standard message data manipulation methods.
  /// <include path='items/DataManager/*' file='Doc/DataManager.xml'/>
  public class DataManager : IDataManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC1/*' file='Doc/DataManager.xml'/>
    public DataManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = null, string schemaName = null
      , bool useEncryption = true)
    {
      Reset(dbServiceRef, dataConfigName, tableName, schemaName
        , useEncryption);
    }

    // Initializes an object instance.
    /// <include path='items/DataManagerC2/*' file='Doc/DataManager.xml'/>
    public DataManager(string dataConfigName, string tableName = null
      , string schemaName = null, bool useEncryption = true)
    {
      Reset(null, dataConfigName, tableName, schemaName, useEncryption);
    }

    // Resets the data access configuration.
    /// <include path='items/Reset/*' file='Doc/DataManager.xml'/>
    public void Reset(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = null, string schemaName = null
      , bool useEncryption = true)
    {
      DataConfigName = dataConfigName;
      TableName = tableName;
      if (NetString.HasValue(schemaName))
      {
        SchemaName = schemaName;
      }

      UseEncryption = useEncryption;
      if (UseEncryption)
      {
        mRequestCipherItems = new CipherItems();
        mRequestInsertItems = mRequestCipherItems.CreateItems();
        mRequestSendCipher = new SendCipher(mRequestInsertItems);

        mResponseCipherItems = new CipherItems();
        mResponseSendCipher = new SendCipher();
      }

      if (dbServiceRef != null)
      {
        DbServiceRef = dbServiceRef;
      }
      else
      {
        // Default data access object.
        DbServiceRef = new DbServiceRef()
        {
          DbDataAccess = new DbDataAccess(dataConfigName)
        };
      }
      if (DbServiceRef != null
        && NetString.HasValue(TableName))
      {
        DataDefinition = CreateDataDefinition();
        LookupColumnNames = new List<string>();
        OrderByNames = new List<string>();
      }
    }
    #endregion

    #region Public Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='Doc/DataManager.xml'/>
    public DbResult Add(object dataObject, List<string> propertyNames = null)
    {
      DbResult retValue;

      // The record must not contain a value for DB Assigned columns.
      var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
      var keyColumns = DbCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);

      Request = ManagerCommon.CreateRequest(RequestType.Insert, TableName
        , dataColumns, DataConfigName, SchemaName, keyColumns);

      if (DbAssignedColumns != null)
      {
        Request.DbAssignedColumns = DbAssignedColumns.Clone();
      }

      // The DbResult contains a record with only the DB Assigned columns.
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='Doc/DataManager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      // Make sure delete is restricted.
      if ((keyColumns != null && keyColumns.Count > 0)
        || (filters != null && filters.Count > 0))
      {
        var requestKeyColumns = DbCommon.RequestKeys(keyColumns, BaseDefinition);

        Request = ManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeyColumns, filters);
        ExecuteRequest(Request);
      }
      else
      {
        throw new ArgumentNullException("keyColumns or filters");
      }
    }

    // Executes a non-query client SQL statement.
    /// <include path='items/ExecuteClientSql/*' file='Doc/DataManager.xml'/>
    public DbResult ExecuteClientSql(RequestType requestType, string sql
      , DbColumns requestColumns = null)
    {
      DbResult retValue;

      if (null == requestColumns)
      {
        requestColumns = DataDefinition;
      }

      Request = ManagerCommon.CreateRequest(requestType, TableName
        , requestColumns, DataConfigName, SchemaName);
      Request.ClientSql = sql;
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='Doc/DataManager.xml'/>
    public DbResult Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbResult retValue;

      Request = CreateLoadRequest(keyColumns, propertyNames, filters, joins);
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadProcedure/*' file='Doc/DataManager.xml'/>
    public DbResult LoadProcedure(string procedureName
      , ProcedureParameters parameters = null, DbJoins joins = null)
    {
      DbResult retValue;

      var requestColumns = BaseDefinition.Clone();

      Request = ManagerCommon.CreateRequest(RequestType.SelectProcedure, null
        , requestColumns, DataConfigName, SchemaName, null, null, joins);
      Request.OrderByNames = OrderByNames;
      Request.PageSize = PageSize;
      Request.PageStartIndex = PageStartIndex;
      Request.ProcedureName = procedureName;
      Request.Parameters = parameters;
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='Doc/DataManager.xml'/>
    public DbResult Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbResult retValue;

      var requestColumns = DbCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeyColumns = DbCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      Request = ManagerCommon.CreateRequest(RequestType.Select, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeyColumns, filters
        , joins);
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='Doc/DataManager.xml'/>
    public void Update(object dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      // Make sure update is restricted.
      if ((keyColumns != null && keyColumns.Count > 0)
        || (filters != null && filters.Count > 0))
      {
        var dataColumns = DbCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
        if (dataColumns.Count > 0)
        {
          var requestKeyColumns = DbCommon.RequestDataKeys(keyColumns
            , BaseDefinition);

          Request = ManagerCommon.CreateRequest(RequestType.Update, TableName
            , dataColumns, DataConfigName, SchemaName, requestKeyColumns
            , filters);
          ExecuteRequest(Request);
        }
      }
      else
      {
        throw new ArgumentNullException("keyColumns or filters");
      }
    }
    #endregion

    #region Other Public Methods

    // Creates the Load DbRequest object.
    /// <include path='items/CreateLoadRequest/*' file='Doc/DataManager.xml'/>
    public DbRequest CreateLoadRequest(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DbRequest retValue;

      var requestColumns = DbCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeyColumns = DbCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      retValue = ManagerCommon.CreateRequest(RequestType.Load, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeyColumns, filters
        , joins);
      retValue.OrderByNames = OrderByNames;
      retValue.PageSize = PageSize;
      retValue.PageStartIndex = PageStartIndex;
      return retValue;
    }

    // Executes the supplied request.
    /// <include path='items/ExecuteRequest/*' file='Doc/DataManager.xml'/>
    public DbResult ExecuteRequest(DbRequest dbRequest)
    {
      string result;
      DbResult retValue = null;

      // Retrieve the result.
      if (DbServiceRef.DbDataAccess != null)
      {
        // Use DbDataAccess.
        retValue = DbServiceRef.DbDataAccess.Execute(dbRequest);
        Result = retValue.Clone();
      }
      else
      {
        // Use DbService.
        string request = dbRequest.Serialize();
        if (request != null)
        {
          if (UseEncryption)
          {
            byte[] requestCipher = GetOutgoingCipher(request);
            var tempRequest = Convert.ToBase64String(requestCipher);

            // Test decrypt.
            //byte[] responseSendCipher = Convert.FromBase64String(tempRequest);
            //var resultText = GetIncommingText(responseSendCipher);

            request = tempRequest;
          }
          else
          {
            request = NetCommon.TextToBase64(request);
          }

          if (DbServiceRef.DbService != null)
          {
            result = DbServiceRef.DbService.Execute(request);
          }
          else
          {
            result = DbServiceRef.DbServiceClient.Execute(request);
          }

          if (NetString.HasValue(result))
          {
            string resultText;
            if (UseEncryption)
            {
              byte[] responseSendCipher = Convert.FromBase64String(result);
              resultText = GetIncommingText(responseSendCipher);
            }
            else
            {
              resultText = NetCommon.Base64ToText(result);
            }
            retValue = DbResult.DeserializeMessage(resultText);
            Result = retValue.Clone();
          }
        }
      }

      if (retValue != null)
      {
        AffectedCount = retValue.AffectedRecords;
        SQLStatement = retValue.ExecutedSql;
      }

      OrderByNames?.Clear();
      return retValue;
    }

    /// <summary>
    /// Creates a PropertyNames list from the data definition.
    /// </summary>
    /// <returns>The full PropertyNames list.</returns>
    public List<string> GetPropertyNames()
    {
      List<string> retValue = new List<string>();

      foreach (DbColumn dbColumn in DataDefinition)
      {
        retValue.Add(dbColumn.PropertyName);
      }
      return retValue;
    }

    // Retrieves the column names for the specified table.
    /// <include path='items/GetSchemaOnly/*' file='Doc/DataManager.xml'/>
    public DbResult GetSchemaOnly(string dataConfigName = null
      , string tableName = null)
    {
      DbResult retValue;

      if (null == dataConfigName)
      {
        dataConfigName = DataConfigName;
      }
      if (null == tableName)
      {
        tableName = TableName;
      }

      var dbRequest = ManagerCommon.CreateRequest(RequestType.SchemaOnly, tableName
        , null, dataConfigName, SchemaName);
      retValue = ExecuteRequest(dbRequest);
      return retValue;
    }

    // Retrieves the table names for the data configuration database.
    /// <include path='items/GetTableNames/*' file='Doc/DataManager.xml'/>
    public DbResult GetTableNames()
    {
      DbColumns includedColumns;
      DbResult retValue;

      // ToDo: Why is this needed when DbDataAccess.TableNames also adds it?
      if (null == DataDefinition)
      {
        DataDefinition = new DbColumns()
        {
          // *** Next Statement *** Change - 2/6/23
          new DbColumn("TABLE_NAME", propertyName: "Name")
        };
      }

      Request = ManagerCommon.CreateRequest(RequestType.TableNames, TableName
        , null, DataConfigName, SchemaName);
      if (DataDefinition != null)
      {
        List<string> propertyNames = new List<string>() { "Name" };
        includedColumns = DataDefinition.LJCGetColumns(propertyNames);
        Request.Columns = includedColumns.Clone();
      }
      retValue = ExecuteRequest(Request);
      return retValue;
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/DataManager.xml'/>
    public void MapNames(string columnName, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      DataDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
      BaseDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idName"></param>
    /// <param name="sequenceName"></param>
    /// <param name="whereClause"></param>
    public void Resequence(string idName, string sequenceName
      , string whereClause)
    {
      var dataManager = new DataManager(DbServiceRef, DataConfigName
        , TableName);
      var columns = new DbColumns()
      {
        { idName }
      };
      string sql = $"select {idName} from {TableName}";
      sql += $" {whereClause} order by {sequenceName}";
      var dbResult = dataManager.ExecuteClientSql(RequestType.LoadSQL, sql
        , columns);
      if (dbResult != null)
      {
        var sequence = 0;
        var startSql = $"update {TableName} set {sequenceName} = ";
        foreach (DbRow dbRow in dbResult.Rows)
        {
          sequence++;
          var id = dbRow.Values[idName].Value;
          var updateSql = startSql + $"{sequence} where {idName} = {id}";
          dataManager.ExecuteClientSql(RequestType.ExecuteSQL, updateSql);
        }
      }
    }

    // Sets the database assigned value column names. 
    /// <include path='items/SetDbAssignedColumns/*' file='Doc/DataManager.xml'/>
    public void SetDbAssignedColumns(string[] propertyNames)
    {
      DbAssignedColumns = new DbColumns();
      foreach (string propertyName in propertyNames)
      {
        DbColumn dbColumn = DataDefinition.LJCSearchPropertyName(propertyName);
        if (null == dbColumn)
        {
          throw new MissingMemberException($"Column '{propertyName}' was not found.");
        }
        dbColumn.AutoIncrement = true;
        DbAssignedColumns.Add(dbColumn.Clone());
      }
    }

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

    #region Create Data Methods

    // Retrieves the schema result for the specified table and transforms
    // it into a result of column names.
    /// <include path='items/CreateSchemaColumnsResult/*' file='Doc/DataManager.xml'/>
    public DbResult CreateSchemaColumnsResult(string dataConfigName
      , string tableName)
    {
      DbResult dbResult = GetSchemaOnly(dataConfigName, tableName);
      return CreateSchemaColumnsResult(dbResult);
    }

    // Takes a result object and transforms it into a result of column names.
    /// <include path='items/CreateSchemaColumnsResult1/*' file='Doc/DataManager.xml'/>
    public DbResult CreateSchemaColumnsResult(DbResult dbResult)
    {
      DbResult retValue = new DbResult();

      // Create result data records.
      DbColumns dbColumns = dbResult.Columns;
      foreach (DbColumn dbColumnNew in dbColumns)
      {
        DbValues dbValues = new DbValues
        {
          { "ColumnName", dbColumnNew.ColumnName },
          { "PropertyName", dbColumnNew.ColumnName }
        };
        retValue.Rows.Add(dbValues);
      }

      // Create result columns.
      retValue.Columns = new DbColumns();
      retValue.Columns.Add("ColumnName", caption: "Column Name");
      retValue.Columns.Add("PropertyName", caption: "Property Name");
      retValue.Columns.Add("RenameAs", caption: "Rename As");
      return retValue;
    }
    #endregion

    #region Private Methods

    // Creates a DataDefinition value.
    private DbColumns CreateDataDefinition()
    {
      DbColumns retValue = null;

      DbResult dbResult = GetSchemaOnly(DataConfigName, TableName);
      if (DbResult.HasColumns(dbResult))
      {
        BaseDefinition = new DbColumns(dbResult.Columns);
        retValue = new DbColumns(BaseDefinition);
      }
      return retValue;
    }

    // Decrypt Response Cipher
    private string GetIncommingText(byte[] responseSendCipher)
    {
      var responseInsertItems = mResponseCipherItems.CreateReceivedItems(responseSendCipher);
      mResponseSendCipher.SetInsertItems(responseInsertItems);
      byte[] responseCipher = mResponseSendCipher.SendCipherToCipher(responseSendCipher);
      var retValue = mResponseCipherItems.CreatePlainText(responseCipher);
      return retValue;
    }

    // Encrypt Request Cipher
    private byte[] GetOutgoingCipher(string plainText)
    {
      byte[] cipher = mRequestCipherItems.CreateCipher(plainText);
      byte[] retValue = mRequestSendCipher.GetSendCipher(cipher);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-select affected record count.</summary>
    public int AffectedCount { get; set; }

    /// <summary>Gets the base data definition columns collection.</summary>
    public DbColumns BaseDefinition { get; set; }

    /// <summary>Gets DbServiceRef object.</summary>
    public DbServiceRef DbServiceRef { get; private set; }

    /// <summary>Gets or sets the data configuration name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      private set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>Gets the data definition columns collection.</summary>
    public DbColumns DataDefinition { get; set; }

    /// <summary>Gets or sets the Database assigned columns.</summary>
    public DbColumns DbAssignedColumns { get; set; }

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

    #region Other Properties

    /// <summary>Gets or sets the order by names.</summary>
    public List<string> OrderByNames { get; set; }

    /// <summary>Gets or sets the pagination size.</summary>
    public int PageSize { get; set; }

    /// <summary>Gets or sets the pagination start index.</summary>
    public int PageStartIndex { get; set; }

    // Gets or sets the DbRequest object reference.
    /// <include path='items/Request/*' file='Doc/DataManager.xml'/>
    public DbRequest Request { get; set; }

    /// <summary>Gets or sets the DbResult object reference.</summary>
    public DbResult Result { get; set; }

    /// <summary>Gets or sets the UseEncyption flag.</summary>
    public bool UseEncryption { get; set; }
    #endregion

    #region Class Data

    private CipherItems mResponseCipherItems;
    private SendCipher mResponseSendCipher;
    private CipherItems mRequestCipherItems;
    private InsertItems mRequestInsertItems;
    private SendCipher mRequestSendCipher;
    #endregion
  }
}
