// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Data;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents a data result.
  /// <include path='items/DbResult/*' file='Doc/DbResult.xml'/>
  //[XmlRoot("DbResult")]
  public class DbResult
  {
    #region Static Functions

    // Deserializes the DbResult message.
    /// <include path='items/DeserializeMessage/*' file='Doc/DbResult.xml'/>
    public static DbResult DeserializeMessage(string result)
    {
      DbResult retValue = null;

      if (NetString.HasValue(result))
      {
        retValue = NetCommon.XmlDeserializeMessage(typeof(DbResult), result)
          as DbResult;
        if (null == retValue)
        {
          retValue = new DbResult();
        }
      }
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include path='items/HasColumns1/*' file='Doc/DbResult.xml'/>
    public static bool HasColumns(DbResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null && dbResult.Columns != null
        && dbResult.Columns.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the result has Columns and Rows.
    /// <include path='items/HasColumns1/*' file='Doc/DbResult.xml'/>
    public static bool HasData(DbResult dbResult)
    {
      bool retValue;

      retValue = HasColumns(dbResult);
      if (retValue)
      {
        retValue = HasRows(dbResult);
      }
      return retValue;
    }

    // <summary>Checks if the result has Rows.</summary>
    /// <include path='items/HasRows1/*' file='Doc/DbResult.xml'/>
    public static bool HasRows(DbResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null && dbResult.Rows != null
        && dbResult.Rows.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbResult()
    {
      Rows = new DbRows();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbResult(DbResult item)
    {
      AffectedRecords = item.AffectedRecords;
      DatabaseName = item.DatabaseName;
      ExecutedSql = item.ExecutedSql;
      RequestTypeName = item.RequestTypeName;
      SchemaName = item.SchemaName;
      TableName = item.TableName;
      Columns = new DbColumns(item.Columns);
      Rows = new DbRows(item.Rows);
    }

    // Initializes an object instance with the DbResult object.
    /// <include path='items/DbResultC1/*' file='Doc/DbResult.xml'/>
    public DbResult(DbRequest dbRequest)
      : this(dbRequest.RequestTypeName, dbRequest.TableName, dbRequest.SchemaName
      , dbRequest.ProcedureName)
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbResultC2/*' file='Doc/DbResult.xml'/>
    public DbResult(string requestTypeName, string tableName, string schemaName = null
      , string procedureName = null)
    {
      ProcedureName = procedureName;
      RequestTypeName = requestTypeName;
      Rows = new DbRows();
      SchemaName = schemaName;
      TableName = tableName;
    }
    #endregion

    #region Collection Methods

    // Clones the structure of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbResult Clone()
    {
      DbResult retValue = MemberwiseClone() as DbResult;
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include path='items/HasColumns2/*' file='Doc/DbResult.xml'/>
    public bool HasColumns()
    {
      bool retValue = false;

      if (Rows != null && Rows.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the result has Columns and Rows.
    /// <include path='items/HasColumns2/*' file='Doc/DbResult.xml'/>
    public bool HasData()
    {
      bool retValue;

      retValue = HasColumns();
      if (retValue)
      {
        retValue = HasRows();
      }
      return retValue;
    }

    // <summary>Checks if the result has Rows.</summary>
    /// <include path='items/HasRows2/*' file='Doc/DbResult.xml'/>
    public bool HasRows()
    {
      bool retValue = false;

      if (Rows != null && Rows.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serializes the object and returns the serialized string.
    /// <include path='items/Serialize1/*' file='Doc/DbResult.xml'/>
    public string Serialize()
    {
      string retValue;

      retValue = NetCommon.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include path='items/Serialize2/*' file='Doc/DbResult.xml'/>
    public void Serialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods

    // Get DbColumns from result records.
    /// <include path='items/GetValueColumns/*' file='Doc/DbResult.xml'/>
    public DbColumns GetValueColumns(DbValues dbValues = null)
    {
      DbColumns retValue = null;

      if (HasRows())
      {
        if (null == dbValues)
        {
          //dbValues = Rows[0];
          dbValues = Rows[0].Values;
        }
        retValue = dbValues.LJCCreateColumns(Columns);
      }
      return retValue;
    }

    // Sets the Columns property from the principle and join columns.
    /// <include path='items/SetColumns/*' file='Doc/DbResult.xml'/>
    public void SetColumns(DbColumns dbColumns, DbJoins dbJoins = null)
    {
      Columns = dbColumns.Clone();
      if (dbJoins != null && dbJoins.Count > 0)
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (dbJoin.Columns != null && dbJoin.Columns.Count > 0)
          {
            foreach (DbColumn dbColumn in dbJoin.Columns)
            {
              Columns.Add(dbColumn);
            }
          }
        }
      }
    }

    /// <summary>
    /// Sets the Columns property from the Request columns.
    /// </summary>
    /// <param name="dbRequest">The Request object.</param>
    public void SetColumns(DbRequest dbRequest)
    {
      SetColumns(dbRequest.Columns, dbRequest.Joins);
    }

    // Sets the result records from the DataTable and DbRequest objects.
    /// <include path='items/SetData/*' file='Doc/DbResult.xml'/>
    public void SetData(DataTable dataTable, DbRequest dbRequest)
    {
      SetColumns(dbRequest);
      SetRows(dataTable, dbRequest.Columns, dbRequest.Joins);
    }

    // Sets the result records from the DataTable, principle values and join values.
    /// <include path='items/SetRows/*' file='Doc/DbResult.xml'/>
    public void SetRows(DataTable dataTable
      , DbColumns dbColumns, DbJoins dbJoins = null)
    {
      if (NetCommon.HasData(dataTable))
      {
        foreach (DataRow dataRow in dataTable.Rows)
        {
          DbValues dbValues = GetRowValues(dbColumns, dataRow);
          AddJoinRowValues(dbValues, dataRow, dbJoins);
          DbRow row = new DbRow()
          {
            Values = dbValues
          };
          Rows.Add(row);
        }
      }
    }

    // Gets the result values from the data row.
    /// <include path='items/GetRowValues/*' file='Doc/DbResult.xml'/>
    public DbValues GetRowValues(DbColumns dbColumns, DataRow dataRow)
    {
      // Similar logic in LJCDBMessage.ResultConverter.GetPropertyName().
      object value;
      DbValues retValue = new DbValues();

      foreach (DbColumn dbColumn in dbColumns)
      {
        // Get the datarow value.
        string columnName = dbColumn.ColumnName;
        if (dbColumn.RenameAs != null)
        {
          columnName = dbColumn.RenameAs;
        }

        value = dataRow[columnName];
        if (DBNull.Value == value)
        {
          value = null;
        }

        if (value != null || dbColumn.AllowDBNull)
        {
          DbValue dbValue = dbColumn;
          dbValue.Value = value;
          retValue.Add(dbValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Adds the join values.
    /// <include path='items/AddJoinRowValues/*' file='Doc/DbResult.xml'/>
    private void AddJoinRowValues(DbValues dbValues, DataRow dataRow
      , DbJoins dbJoins)
    {
      if (dbJoins != null && dbJoins.Count > 0)
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (dbJoin.Columns != null && dbJoin.Columns.Count > 0)
          {
            DbValues joinValues = GetRowValues(dbJoin.Columns, dataRow);
            foreach (DbValue dbValue in joinValues)
            {
              dbValues.Add(dbValue);
            }
          }
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-query affected record count.</summary>
    public int AffectedRecords { get; set; }

    /// <summary>Gets the collection of columns that belong to this result.</summary>
    //[XmlArrayItem("Columns")]
    public DbColumns Columns { get; set; }

    /// <summary>Gets or sets the Database name.</summary>
    public string DatabaseName { get; set; }

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbResult.xml"; }
    }

    /// <summary>Gets or sets the executed SQL statement.</summary>
    public string ExecutedSql
    {
      get { return mExecutedSql; }
      set { mExecutedSql = NetString.InitString(value); }
    }
    private string mExecutedSql;

    /// <summary>Gets or sets the ProcedureName value.</summary>
    public string ProcedureName
    {
      get { return mProcedureName; }
      set { mProcedureName = NetString.InitString(value); }
    }
    private string mProcedureName;

    /// <summary>The request type.</summary>
    public string RequestTypeName
    {
      get { return mRequestTypeName; }
      set { mRequestTypeName = NetString.InitString(value); }
    }
    private string mRequestTypeName;

    /// <summary>A collection of DbValues objects.</summary>
    [XmlArrayItem("DbRows")]
    public DbRows Rows { get; set; }

    /// <summary>The schema name.</summary>
    public string SchemaName
    {
      get { return mSchemaName; }
      set { mSchemaName = NetString.InitString(value); }
    }
    private string mSchemaName;

    /// <summary>The table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion
  }
}
