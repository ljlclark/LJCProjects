// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbResult.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
//using LJCGridDataLib;
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

    #region TableData Methods?

    /// <summary>
    /// Creates a DbColumn object from a DataColumn object. 
    /// </summary>
    /// <param name="dataColumn">The DataColumn reference.</param>
    /// <returns>The DbColumn Object.</returns>
    // Note: Also in LJCGridDataLib.TableData
    public static DbColumn GetDbColumn(DataColumn dataColumn)
    {
      DbColumn retValue;

      retValue = new DbColumn()
      {
        AllowDBNull = dataColumn.AllowDBNull,
        AutoIncrement = dataColumn.AutoIncrement,
        Caption = dataColumn.ColumnName,
        ColumnName = dataColumn.ColumnName,
        DataTypeName = dataColumn.DataType.Name,
        MaxLength = dataColumn.MaxLength,
        PropertyName = dataColumn.ColumnName,
        Unique = dataColumn.Unique
      };
      return retValue;
    }

    /// <summary>
    /// Creates a DbColumns collection from a DataColumns collection.
    /// </summary>
    /// <param name="dataColumns">The DataColumnCollection reference.</param>
    /// <returns>The DbColumns object.</returns>
    // Note: Also in LJCGridDataLib.TableData
    public static DbColumns GetDbColumns(DataColumnCollection dataColumns)
    {
      DbColumns retValue = null;

      if (HasColumns(dataColumns))
      {
        retValue = new DbColumns();
        foreach (DataColumn dataColumn in dataColumns)
        {
          DbColumn dbColumn = GetDbColumn(dataColumn);
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    /// <summary>
    /// Checks the DataColumnCollection object for items.
    /// </summary>
    /// <param name="dataColumns">The DataColumnCollection reference.</param>
    /// <returns>true if there are items; otherwise false.</returns>
    // Note: Also in LJCGridDataLib.TableData
    public static bool HasColumns(DataColumnCollection dataColumns)
    {
      bool retValue = false;

      if (NetCommon.HasColumns(dataColumns))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    // Checks if the result has Columns.
    /// <include path='items/HasColumns1/*' file='Doc/DbResult.xml'/>
    public static bool HasColumns(DbResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && NetCommon.HasItems(dbResult.Columns))
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

      if (dbResult != null
        && NetCommon.HasItems(dbResult.Rows))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbResult()
    {
      Rows = new DbRows();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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

      if (NetCommon.HasItems(Rows))
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

      if (NetCommon.HasItems(Rows))
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
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods

    // Creates combined DbColumns from DbColumns and DbValues.
    /// <summary>
    /// Creates combined DbColumns from result DbColumns and DbValues.
    /// </summary>
    /// <param name="dbResult">The DbResult object.</param>
    /// <returns>The DbColumns collection.</returns>
    public DbColumns CreateResultColumns(DbResult dbResult)
    {
      DbColumn findColumn;
      DbColumns retValue = null;

      var columns = dbResult.Columns;
      var values = dbResult.Rows[0].Values;
      if (NetCommon.HasItems(columns) && NetCommon.HasItems(values))
      {
        retValue = new DbColumns();
        foreach (DbValue value in values)
        {
          findColumn = columns.LJCSearchPropertyName(value.PropertyName);
          DbColumn dbColumn = new DbColumn()
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

    // Get DbColumns from result records.
    /// <include path='items/GetValueColumns/*' file='Doc/DbResult.xml'/>
    public DbColumns GetValueColumns(DbValues dataValues = null)
    {
      DbColumns retValue = null;

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
    /// <include path='items/SetColumns/*' file='Doc/DbResult.xml'/>
    public void SetColumns(DbColumns dataColumns, DbJoins dbJoins = null)
    {
      Columns = dataColumns.Clone();
      if (NetCommon.HasItems(dbJoins))
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (NetCommon.HasItems(dbJoin.Columns))
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
      // *** Next Statement *** Add 12/25/24
      //var dbColumns = TableData.GetDbColumns(dataTable.Columns);
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
    /// <include path='items/SetRows/*' file='Doc/DbResult.xml'/>
    public void SetRows(DataTable dataTable, DbJoins dbJoins = null)
    {
      if (NetCommon.HasData(dataTable))
      {
        // *** Next Statement *** Add 12/25/24
        var dataColumns = GetDbColumns(dataTable.Columns);
        foreach (DataRow dataRow in dataTable.Rows)
        {
          DbValues dataValues = GetRowValues(dataColumns, dataRow);
          AddJoinRowValues(dataValues, dataRow, dbJoins);
          DbRow row = new DbRow()
          {
            Values = dataValues
          };
          Rows.Add(row);
        }
      }
    }

    //// Sets the result records from the DataTable, principle values
    //// and join values.
    ///// <include path='items/SetRows/*' file='Doc/DbResult.xml'/>
    //[Obsolete("Use SetRows(DataTable, DbJoins")]
    //public void SetRows(DataTable dataTable
    //  , DbColumns dataColumns, DbJoins dbJoins = null)
    //{
    //  if (NetCommon.HasData(dataTable))
    //  {
    //    // *** Next Statement *** Add 12/25/24
    //    dataColumns = TableData.GetDbColumns(dataTable.Columns);
    //    foreach (DataRow dataRow in dataTable.Rows)
    //    {
    //      DbValues dataValues = GetRowValues(dataColumns, dataRow);
    //      AddJoinRowValues(dataValues, dataRow, dbJoins);
    //      DbRow row = new DbRow()
    //      {
    //        Values = dataValues
    //      };
    //      Rows.Add(row);
    //    }
    //  }
    //}

    // Gets the result values from the data row.
    /// <include path='items/GetRowValues/*' file='Doc/DbResult.xml'/>
    public DbValues GetRowValues(DbColumns dataColumns, DataRow dataRow)
    {
      // Similar logic in LJCDBMessage.ResultConverter.GetPropertyName().
      object value;
      DbValues retValue = new DbValues();

      foreach (DbColumn dbColumn in dataColumns)
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
          DbValue dataValue = dbColumn;
          dataValue.Value = value;
          retValue.Add(dataValue);
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
      if (NetCommon.HasItems(dbJoins))
      {
        foreach (DbJoin dbJoin in dbJoins)
        {
          if (NetCommon.HasItems(dbJoin.Columns))
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
