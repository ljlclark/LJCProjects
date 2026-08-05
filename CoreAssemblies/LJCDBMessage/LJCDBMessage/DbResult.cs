// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbResult.cs
using System;
using System.Data;
using System.Xml.Serialization;
using LJCDataAccess;

//using LJCGridDataLib;
using LJCNetCommon;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Represents a data result.
  /// <include path='items/DbResult/*' file='Doc/DbResult.xml'/>
  //[XmlRoot("DbResult")]
  public class DbResult
  {
    #region Static Functions

    // Deserializes the DbResult message.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/DeserializeMessage/*'/>
    public static DbResult DeserializeMessage(string result)
    {
      DbResult retValue = null;

      if (NetString.HasValue(result))
      {
        retValue = LJC.XmlDeserializeMessage(typeof(DbResult), result)
          as DbResult;
        if (null == retValue)
        {
          retValue = new DbResult();
        }
      }
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns1/*'/>
    public static bool HasColumns(DbResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && LJC.HasListItems(dbResult.Columns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the result has Columns and Rows.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns1/*'/>
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasRows1/*'/>
    public static bool HasRows(DbResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && LJC.HasListItems(dbResult.Rows))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbResult()
    {
      Rows = new DbRows();
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbResult(DbResult item)
    {
      AffectedRecords = item.AffectedRecords;
      DatabaseName = item.DatabaseName;
      ExecutedSql = item.ExecutedSql;
      RequestTypeName = item.RequestTypeName;
      SchemaName = item.SchemaName;
      TableName = item.TableName;
      Columns = new LJCDataColumns(item.Columns);
      Rows = new DbRows(item.Rows);
    }

    // Initializes an object instance with the DbResult object.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/DbResultC1/*'/>
    public DbResult(DbRequest dbRequest)
      : this(dbRequest.RequestTypeName, dbRequest.TableName, dbRequest.SchemaName
      , dbRequest.ProcedureName)
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/DbResultC2/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbResult Clone()
    {
      DbResult retValue = MemberwiseClone() as DbResult;
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns2/*'/>
    public bool HasColumns()
    {
      bool retValue = false;

      if (LJC.HasListItems(Rows))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the result has Columns and Rows.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns2/*'/>
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasRows2/*'/>
    public bool HasRows()
    {
      bool retValue = false;

      if (LJC.HasListItems(Rows))
      {
        retValue = true;
      }
      return retValue;
    }

    // Serializes the object and returns the serialized string.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Serialize1/*'/>
    public string Serialize()
    {
      string retValue;

      retValue = LJC.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Serialize2/*'/>
    public void Serialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    /// <summary>
    /// Creates combined LJCDataColumns from result LJCDataColumns and LJCDataValues.
    /// </summary>
    /// <param name="dbResult">The DbResult object.</param>
    /// <returns>The LJCDataColumns collection.</returns>
    public LJCDataColumns CreateResultColumns(DbResult dbResult)
    {
      LJCDataColumn findColumn;
      LJCDataColumns retValue = null;

      var columns = dbResult.Columns;
      var values = dbResult.Rows[0].Values;
      if (LJC.HasListItems(columns)
        && LJC.HasListItems(values))
      {
        retValue = new LJCDataColumns();
        foreach (LJCDataValue value in values)
        {
          //findColumn = columns.LJCSearchPropertyName(value.PropertyName);
          findColumn = columns[value.PropertyName];
          LJCDataColumn dbColumn = new LJCDataColumn()
          {
            AllowDBNull = findColumn.AllowDBNull,
            AutoIncrement = findColumn.AutoIncrement,
            Caption = findColumn.Caption,
            ColumnName = findColumn.ColumnName,
            DataTypeName = findColumn.DataTypeName,
            MaxLength = findColumn.MaxLength,
            PropertyName = findColumn.PropertyName,
            Value = value.Value
          };
          if (0 == dbColumn.MaxLength)
          {
            dbColumn.MaxLength = 10;
          }
          if (dbColumn.MaxLength < 5)
          {
            dbColumn.MaxLength += 3;
          }
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    // Get LJCDataColumns from result records.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetValueColumns/*'/>
    public LJCDataColumns GetValueColumns(LJCDataValues dataValues = null)
    {
      LJCDataColumns retValue = null;

      if (HasRows())
      {
        if (null == dataValues)
        {
          //dbValues = Rows[0];
          dataValues = Rows[0].Values;
        }
        retValue = dataValues.LJCCreateColumns(Columns);
      }
      return retValue;
    }

    // Sets the Columns property from the principle and join columns.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetColumns/*'/>
    public void SetColumns(LJCDataColumns dataColumns, DbJoins dbJoins = null)
    {
      Columns = dataColumns.Clone();
      if (LJC.HasListItems(dbJoins))
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (LJC.HasListItems(dbJoin.Columns))
          {
            foreach (LJCDataColumn dbColumn in dbJoin.Columns)
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetData/*'/>
    public void SetData(DataTable dataTable, DbRequest dbRequest)
    {
      // *** Next Statement *** Add 12/25/24
      //var dbColumns = TableData.GetLJCDataColumns(dataTable.Columns);
      // *** Begin *** Change 12/25/24
      ////SetColumns(dbRequest);
      ////SetRows(dataTable, dbRequest.Columns, dbRequest.Joins);
      //SetColumns(dbColumns);
      //SetRows(dataTable, dbColumns, dbRequest.Joins);
      SetRows(dataTable, dbRequest.Joins);
      // *** End   *** Change 12/25/24
    }

    // Sets the result records from the DataTable, principle values
    // and join values.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetRows/*'/>
    public void SetRows(DataTable dataTable, DbJoins dbJoins = null)
    {
      if (LJC.HasData(dataTable))
      {
        var dataColumns = LJCTableColumns.ToDataColumns(dataTable.Columns);
        foreach (DataRow dataRow in dataTable.Rows)
        {
          LJCDataValues dataValues = GetRowValues(dataColumns, dataRow);
          AddJoinRowValues(dataValues, dataRow, dbJoins);
          DbRow row = new DbRow()
          {
            Values = dataValues
          };
          Rows.Add(row);
        }
      }
    }

    // Gets the result values from the data row.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetRowValues/*'/>
    public LJCDataValues GetRowValues(LJCDataColumns dataColumns, DataRow dataRow)
    {
      // Similar logic in LJCDBMessage.ResultConverter.GetPropertyName().
      object value;
      LJCDataValues retValue = new LJCDataValues();

      foreach (LJCDataColumn dataColumn in dataColumns)
      {
        // Get the datarow value.
        string columnName = dataColumn.ColumnName;
        if (dataColumn.RenameAs != null)
        {
          columnName = dataColumn.RenameAs;
        }

        value = dataRow[columnName];
        if (DBNull.Value == value)
        {
          value = null;
        }

        if (value != null || dataColumn.AllowDBNull)
        {
          LJCDataValue dataValue = dataColumn;
          dataValue.Value = value;
          retValue.Add(dataValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Adds the join values.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/AddJoinRowValues/*'/>
    private void AddJoinRowValues(LJCDataValues dbValues, DataRow dataRow
      , DbJoins dbJoins)
    {
      if (LJC.HasListItems(dbJoins))
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (LJC.HasListItems(dbJoin.Columns))
          {
            LJCDataValues joinValues = GetRowValues(dbJoin.Columns, dataRow);
            foreach (LJCDataValue dbValue in joinValues)
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
    public LJCDataColumns Columns { get; set; }

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

    /// <summary>A collection of LJCDataValues objects.</summary>
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
