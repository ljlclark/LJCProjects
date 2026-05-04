// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataManager.cs
using LJCCipherLib5;
using LJCDataAccess5;
using LJCDBDataAccess5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDBClientLib5
{
  // Provides standard message data manipulation methods.
  /// <include path='items/DataManager/*' file='Doc/DataManager.xml'/>
  public class LJCDataManager : ILJCDataManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC1/*' file='Doc/DataManager.xml'/>
    public LJCDataManager(LJCDbServiceRef? dbServiceRef, string dataConfigName
      , string? tableName = null, string? schemaName = null
      , bool? useEncryption = true)
    {
      // Initialize required properties.
      BaseDefinition = [];
      LookupColumnNames = [];
      mDataConfigName = dataConfigName;
      mRequestCipherItems = new CipherItems();
      mResponseCipherItems = new CipherItems();
      mRequestInsertItems = [];
      mRequestSendCipher = new SendCipher();
      mResponseSendCipher = new SendCipher();
      mSchemaName = "";
      OrderByNames = [];

      Reset(dbServiceRef, dataConfigName, tableName, schemaName
        , useEncryption);
    }

    // Initializes an object instance.
    /// <include path='items/DataManagerC2/*' file='Doc/DataManager.xml'/>
    public LJCDataManager(string dataConfigName, string? tableName = null
      , string? schemaName = null, bool? useEncryption = true)
    {
      // Initialize required properties.
      BaseDefinition = [];
      LookupColumnNames = [];
      mDataConfigName = dataConfigName;
      mRequestCipherItems = new CipherItems();
      mResponseCipherItems = new CipherItems();
      mRequestInsertItems = [];
      mRequestSendCipher = new SendCipher();
      mResponseSendCipher = new SendCipher();
      mSchemaName = "";
      OrderByNames = [];

      Reset(null, dataConfigName, tableName, schemaName, useEncryption);
    }

    // Resets the data access configuration.
    /// <include path='items/Reset/*' file='Doc/DataManager.xml'/>
    public void Reset(LJCDbServiceRef? dbServiceRef, string dataConfigName
      , string? tableName = null, string? schemaName = null
      , bool? useEncryption = true)
    {
      DataConfigName = dataConfigName;
      TableName = tableName;
      if (LJC.HasValue(schemaName))
      {
        SchemaName = schemaName;
      }

      //if (useEncryption == null)
      //{
      //  useEncryption = true;
      //}
      useEncryption ??= true;
      UseEncryption = (bool)useEncryption;
      if (UseEncryption)
      {
        //mRequestCipherItems = new CipherItems();
        //mRequestInsertItems = mRequestCipherItems.CreateItems();
        //mRequestSendCipher = new SendCipher(mRequestInsertItems);

        //mResponseCipherItems = new CipherItems();
        //mResponseSendCipher = new SendCipher();
      }

      if (dbServiceRef != null)
      {
        DbServiceRef = dbServiceRef;
      }
      else
      {
        // Default data access object.
        DbServiceRef = new LJCDbServiceRef()
        {
          DbDataAccess = new DbDataAccess(dataConfigName)
        };
      }
      if (DbServiceRef != null
        && LJC.HasValue(TableName))
      {
        DataDefinition = CreateBaseDefinition();
      }
    }
    #endregion

    #region Public Data Methods

    //string sqlQuery = String.Format("Insert into Steps (ProjectID, "
    //    + "Description, Date) Values({0}, '[New Step]', '{1}'); “
    //    + “Select @@Identity", 
    //    projectID, DateTime.Today.ToString("yyyy-MM-dd"));
    //// Create and open a connection
    //SqlConnection connection = new SqlConnection(m_ConnectionString);
    //connection.Open();
    //// Create a Command object
    //SqlCommand command = new SqlCommand(sqlQuery, connection);
    //// Execute the command
    //int stepID = Convert.ToInt32((decimal)command.ExecuteScalar());

    // Adds a record to the database.
    /// <include path='items/Add/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? Add(object dataObject, List<string>? propertyNames = null
      , bool? includeNull = false)
    {
      //LJCDBResult? retResult;

      // The record must not contain a value for DB Assigned columns.
      var dataColumns = LJCDBCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames, includeNull);
      var keyColumns = LJCDBCommon.RequestLookupKeys(dataObject, BaseDefinition
        , LookupColumnNames);

      Request = LJCManagerCommon.CreateRequest(RequestType.Insert, TableName
        , dataColumns, DataConfigName, SchemaName, keyColumns);
      if (DbAssignedColumns != null)
      {
        Request.DbAssignedColumns = DbAssignedColumns.Clone();
      }

      // The LJCDBResult contains a record with only the DB Assigned columns.
      var retResult = ExecuteRequest(Request);
      return retResult;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='Doc/DataManager.xml'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      // Make sure delete is restricted.
      if (LJC.HasItems(keyColumns)
        || LJC.HasItems(filters))
      {
        var requestKeyColumns = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition);
        if (null == filters
          && !LJC.HasItems(requestKeyColumns))
        {
          throw new ArgumentException("keyColumns or filters");
        }

        Request = LJCManagerCommon.CreateRequest(RequestType.Delete, TableName
          , null, DataConfigName, SchemaName, requestKeyColumns, filters);
        ExecuteRequest(Request);
      }
      else
      {
        throw new ArgumentNullException(nameof(filters));
      }
    }

    // Executes a non-query (DML "insert", "delete", "update") SQL statement.
    /// <include path='items/ExecuteClientSql/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? ExecuteClientSql(RequestType requestType, string sql
      , LJCDataColumns? requestColumns = null)
    {
      //LJCDBResult? retResult;

      if (null == requestColumns)
      {
        requestColumns = BaseDefinition;
      }

      Request = LJCManagerCommon.CreateRequest(requestType, TableName
        , requestColumns, DataConfigName, SchemaName);
      Request.ClientSql = sql;
      var retResult = ExecuteRequest(Request);
      return retResult;
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      //LJCDBResult? retResult;

      Request = CreateLoadRequest(keyColumns, propertyNames, filters, joins);
      var retResult = ExecuteRequest(Request);
      return retResult;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadProcedure/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? LoadProcedure(string procedureName
      , LJCProcedureParameters? parameters = null, LJCDBJoins? joins = null)
    {
      //LJCDBResult? retResult;

      var requestColumns = BaseDefinition.Clone();

      Request = LJCManagerCommon.CreateRequest(RequestType.SelectProcedure, null
        , requestColumns, DataConfigName, SchemaName, null, null, joins);
      Request.OrderByNames = OrderByNames;
      Request.PageSize = PageSize;
      Request.PageStartIndex = PageStartIndex;
      Request.ProcedureName = procedureName;
      Request.Parameters = parameters;
      var retResult = ExecuteRequest(Request);
      return retResult;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      LJCDBResult? retResult;

      // Make sure select is restricted.
      if (LJC.HasItems(keyColumns)
        || LJC.HasItems(filters))
      {
        var requestColumns = LJCDBCommon.RequestColumns(BaseDefinition
          , propertyNames);
        var requestKeyColumns = LJCDBCommon.RequestKeys(keyColumns
          , BaseDefinition, joins);
        if (null == filters
          && !LJC.HasItems(requestKeyColumns))
        {
          throw new ArgumentException("keyColumns or filters");
        }

        Request = LJCManagerCommon.CreateRequest(RequestType.Select, TableName
          , requestColumns, DataConfigName, SchemaName, requestKeyColumns
          , filters, joins);
        retResult = ExecuteRequest(Request);
      }
      else
      {
        throw new ArgumentNullException(nameof(filters));
      }
      return retResult;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='Doc/DataManager.xml'/>
    public void Update(object dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      // Make sure update is restricted.
      if (LJC.HasItems(keyColumns)
        || LJC.HasItems(filters))
      {
        var dataColumns = LJCDBCommon.RequestDataColumns(dataObject, BaseDefinition
        , propertyNames);
        if (LJC.HasItems(dataColumns))
        {
          var requestKeyColumns = LJCDBCommon.RequestDataKeys(keyColumns
            , BaseDefinition);
          if (null == filters
            && !LJC.HasItems(requestKeyColumns))
          {
            throw new ArgumentException("keyColumns or filters");
          }

          Request = LJCManagerCommon.CreateRequest(RequestType.Update, TableName
            , dataColumns, DataConfigName, SchemaName, requestKeyColumns
            , filters);
          ExecuteRequest(Request);
        }
      }
      else
      {
        throw new ArgumentNullException(nameof(filters));
      }
    }
    #endregion

    #region Other Public Methods

    // Creates the Load LJCDBRequest object.
    /// <include path='items/CreateLoadRequest/*' file='Doc/DataManager.xml'/>
    public LJCDBRequest CreateLoadRequest(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      LJCDBRequest retValue;

      var requestColumns = LJCDBCommon.RequestColumns(BaseDefinition, propertyNames);
      var requestKeyColumns = LJCDBCommon.RequestKeys(keyColumns, BaseDefinition, joins);

      retValue = LJCManagerCommon.CreateRequest(RequestType.Load, TableName
        , requestColumns, DataConfigName, SchemaName, requestKeyColumns, filters
        , joins);
      retValue.OrderByNames = OrderByNames;
      retValue.PageSize = PageSize;
      retValue.PageStartIndex = PageStartIndex;
      return retValue;
    }

    // Executes the supplied request.
    /// <include path='items/ExecuteRequest/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? ExecuteRequest(LJCDBRequest dbRequest)
    {
      LJCDBResult? retValue = null;

      // Retrieve the result.
      if (DbServiceRef != null
        && DbServiceRef.DbDataAccess != null)
      {
        // Use DbDataAccess.
        retValue = DbServiceRef.DbDataAccess.Execute(dbRequest);
        if (retValue != null)
        {
          Result = retValue.Clone();
        }
      }
      else
      {
        //// Use DbService.
        //string request = dbRequest.Serialize();
        //if (request != null)
        //{
        //  if (UseEncryption)
        //  {
        //    byte[] requestCipher = GetOutgoingCipher(request);
        //    var tempRequest = Convert.ToBase64String(requestCipher);

        //    // Test decrypt.
        //    //byte[] responseSendCipher = Convert.FromBase64String(tempRequest);
        //    //var resultText = GetIncommingText(responseSendCipher);

        //    request = tempRequest;
        //  }
        //  else
        //  {
        //    request = LJC.TextToBase64(request);
        //  }

        //  string result;
        //  if (DbServiceRef != null
        //    && DbServiceRef.DbService != null)
        //  {
        //    result = DbServiceRef.DbService.Execute(request);
        //  }
        //  else
        //  {
        //    result = DbServiceRef?.DbServiceClient.Execute(request);
        //  }

        //  if (LJC.HasValue(result))
        //  {
        //    string resultText;
        //    if (UseEncryption)
        //    {
        //      byte[] responseSendCipher = Convert.FromBase64String(result);
        //      resultText = GetIncommingText(responseSendCipher);
        //    }
        //    else
        //    {
        //      resultText = LJC.Base64ToText(result);
        //    }
        //    retValue = LJCDBResult.DeserializeMessage(resultText);
        //    Result = retValue?.Clone();
        //  }
        //}
      }

      if (retValue != null)
      {
        AffectedCount = retValue.AffectedRecords;
        SQLStatement = retValue.ExecutedSql;
      }

      OrderByNames?.Clear();
      return retValue;
    }

    // Creates a PropertyNames list from the data definition.
    /// <include path='items/GetPropertyNames/*' file='Doc/DataManager.xml'/>
    public List<string> GetPropertyNames()
    {
      var retValue = new List<string>();

      foreach (LJCDataColumn dbColumn in BaseDefinition)
      {
        retValue.Add(dbColumn.PropertyName);
      }
      return retValue;
    }

    // Retrieves the column names for the specified table.
    /// <include path='items/GetSchemaOnly/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? GetSchemaOnly(string? dataConfigName = null
      , string? tableName = null)
    {
      LJCDBResult? retValue = null;

      if (null == dataConfigName)
      {
        dataConfigName = DataConfigName;
      }
      if (null == tableName)
      {
        tableName = TableName;
      }

      var dbRequest = LJCManagerCommon.CreateRequest(RequestType.SchemaOnly, tableName
        , null, dataConfigName, SchemaName);
      if (dbRequest != null)
      {
        retValue = ExecuteRequest(dbRequest);
      }
      return retValue;
    }

    // Retrieves the table names for the data configuration database.
    /// <include path='items/GetTableNames/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? GetTableNames()
    {
      LJCDataColumns? includedColumns;
      LJCDBResult? retValue = null;

      // ToDo: Why is this needed when DbDataAccess.TableNames also adds it?
      if (null == DataDefinition)
      {
        //DataDefinition = new LJCDataColumns()
        //{
        //  new LJCDataColumn("TABLE_NAME", propertyName: "Name")
        //};
        DataDefinition =
        [
          new LJCDataColumn("TABLE_NAME", propertyName: "Name")
        ];
      }

      Request = LJCManagerCommon.CreateRequest(RequestType.TableNames, TableName
        , null, DataConfigName, SchemaName);
      if (DataDefinition != null)
      {
        var propertyNames = new List<string>() { "Name" };
        includedColumns = DataDefinition.LJCGetColumns(propertyNames);
        Request.Columns = includedColumns?.Clone();
      }
      if (Request != null)
      {
        retValue = ExecuteRequest(Request);
      }
      return retValue;
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/DataManager.xml'/>
    public void MapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      BaseDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
      if (LJC.HasItems(DataDefinition))
      {
        DataDefinition.LJCMapNames(columnName, propertyName, renameAs, caption);
      }
    }

    // Resequence the sequence column values.
    /// <include path='items/Resequence/*' file='Doc/DataManager.xml'/>
    public void Resequence(string idName, string sequenceName
      , string whereClause)
    {
      var dataManager = new LJCDataManager(DbServiceRef, DataConfigName
        , TableName);
      var columns = new LJCDataColumns()
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
        foreach (LJCDBRow dbRow in dbResult.Rows)
        {
          sequence++;
          object? id = null;
          if (LJC.HasItems(dbRow.Values))
          {
            var value = dbRow.Values[idName];
            if (value != null)
            {
              id = value.Value;
            }
          }
          var updateSql = startSql + $"{sequence} where {idName} = {id}";
          dataManager.ExecuteClientSql(RequestType.ExecuteSQL, updateSql);
        }
      }
    }

    // Sets the database assigned value column names. 
    /// <include path='items/SetDbAssignedColumns/*' file='Doc/DataManager.xml'/>
    public void SetDbAssignedColumns(string[] propertyNames)
    {
      //DbAssignedColumns = new LJCDataColumns();
      DbAssignedColumns = [];
      foreach (string propertyName in propertyNames)
      {
        // *** Next Statement *** Change - 12/26/23
        //LJCDataColumn dbColumn = DataDefinition.LJCSearchPropertyName(propertyName);
        LJCDataColumn? dbColumn = BaseDefinition.LJCSearchPropertyName(propertyName);
        if (null == dbColumn)
        {
          throw new MissingMemberException($"Column '{propertyName}' was not found.");
        }
        dbColumn.AutoIncrement = true;
        var clone = dbColumn.Clone();
        if (clone != null)
        {
          DbAssignedColumns.Add(clone);
        }
      }
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
    #endregion

    #region Create Data Methods

    // Takes a result object and transforms it into a result of column names.
    /// <include path='items/CreateSchemaColumnsResult1/*' file='Doc/DataManager.xml'/>
    public static LJCDBResult CreateSchemaColumnsResult(LJCDBResult? dbResult)
    {
      var retValue = new LJCDBResult();

      if (dbResult != null
        && LJC.HasItems(dbResult.Columns))
      {
        // Create result data records.
        LJCDataColumns dbColumns = dbResult.Columns;
        foreach (LJCDataColumn dbColumnNew in dbColumns)
        {
          var dataValues = new LJCDataValues
          {
            { "ColumnName", dbColumnNew.ColumnName },
            { "PropertyName", dbColumnNew.ColumnName }
          };
          retValue.Rows.Add(dataValues);
        }

        // Create result columns.
        //retValue.Columns = new LJCDataColumns();
        retValue.Columns = [];
        retValue.Columns.Add("ColumnName", caption: "Column Name");
        retValue.Columns.Add("PropertyName", caption: "Property Name");
        retValue.Columns.Add("RenameAs", caption: "Rename As");
      }
      return retValue;
    }

    // Retrieves the schema result for the specified table and transforms
    // it into a result of column names.
    /// <include path='items/CreateSchemaColumnsResult/*' file='Doc/DataManager.xml'/>
    public LJCDBResult? CreateSchemaColumnsResult(string dataConfigName
      , string tableName)
    {
      LJCDBResult? retResult = null;

      LJCDBResult? dbResult = GetSchemaOnly(dataConfigName, tableName);
      if (dbResult != null)
      {
        retResult = CreateSchemaColumnsResult(dbResult);
      }
      return retResult;
    }
    #endregion

    #region Private Methods

    // Creates a DataDefinition value.
    private LJCDataColumns? CreateBaseDefinition()
    {
      LJCDataColumns? retValue = null;

      LJCDBResult? dbResult = GetSchemaOnly(DataConfigName, TableName);
      if (LJCDBResult.HasColumns(dbResult))
      {
        //BaseDefinition = new LJCDataColumns(dbResult.Columns);
        if (LJC.HasItems(dbResult.Columns))
        {
          BaseDefinition = [.. dbResult.Columns];
        }
        //retValue = new LJCDataColumns(BaseDefinition);
        retValue = [.. BaseDefinition];
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
    public LJCDataColumns BaseDefinition { get; set; }

    /// <summary>Gets DbServiceRef object.</summary>
    public LJCDbServiceRef? DbServiceRef { get; private set; }

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

    // Gets or sets a reference to the Data Definition columns collection.
    /// <include path='items/DataDefinition/*' file='Doc/DataManager.xml'/>
    public LJCDataColumns? DataDefinition { get; set; }

    /// <summary>Gets or sets the Database assigned columns.</summary>
    public LJCDataColumns? DbAssignedColumns { get; set; }

    /// <summary>Gets or sets the LookupColumn names.</summary>
    public List<string> LookupColumnNames { get; set; }

    /// <summary>The Schema name.</summary>
    public string SchemaName
    {
      get => mSchemaName;
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
    public string? TableName
    {
      get => mTableName;
      set
      {
        if (value != null)
        {
          mTableName = value;
        }
      }
    }
    private string? mTableName;
    #endregion

    #region Other Properties

    /// <summary>Gets or sets the order by names.</summary>
    public List<string> OrderByNames { get; set; }

    /// <summary>Gets or sets the pagination size.</summary>
    public int PageSize { get; set; }

    /// <summary>Gets or sets the pagination start index.</summary>
    public int PageStartIndex { get; set; }

    // Gets or sets the LJCDBRequest object reference.
    /// <include path='items/Request/*' file='Doc/DataManager.xml'/>
    public LJCDBRequest? Request { get; set; }

    /// <summary>Gets or sets the LJCDBResult object reference.</summary>
    public LJCDBResult? Result { get; set; }

    /// <summary>Gets or sets the UseEncyption flag.</summary>
    public bool UseEncryption { get; set; }
    #endregion

    #region Class Data

    private readonly CipherItems mResponseCipherItems;
    private readonly SendCipher mResponseSendCipher;
    private readonly CipherItems mRequestCipherItems;
    private readonly InsertItems mRequestInsertItems;
    private readonly SendCipher mRequestSendCipher;
    #endregion
  }
}
