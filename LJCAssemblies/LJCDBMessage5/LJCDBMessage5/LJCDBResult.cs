// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBResult.cs
using System.Data;
using System.Xml.Serialization;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a data result.
  /// <include path='items/DbResult/*' file='Doc/DbResult.xml'/>
  //[XmlRoot("DbResult")]
  public class LJCDBResult
  {
    #region TableData Static Methods?

    /// <summary>
    /// Creates a DbColumn object from a DataColumn object. 
    /// </summary>
    /// <param name="dataColumn">The DataColumn reference.</param>
    /// <returns>The DbColumn Object.</returns>
    // Note: Also in LJCGridDataLib.TableData
    public static LJCDataColumn GetDbColumn(DataColumn dataColumn)
    {
      LJCDataColumn retValue;

      retValue = new LJCDataColumn()
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
    public static LJCDataColumns? GetDbColumns(DataColumnCollection dataColumns)
    {
      LJCDataColumns? retValue = null;

      if (HasColumns(dataColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (DataColumn dataColumn in dataColumns)
        {
          LJCDataColumn dbColumn = GetDbColumn(dataColumn);
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

      if (LJC.HasColumns(dataColumns))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Static Methods

    // Creates combined DbColumns from DbColumns and DbValues.
    /// <summary>
    /// Creates combined DbColumns from result DbColumns and DbValues.
    /// </summary>
    /// <param name="dbResult">The DbResult object.</param>
    /// <returns>The DbColumns collection.</returns>
    public static LJCDataColumns? CreateResultColumns(LJCDBResult dbResult)
    {
      LJCDataColumn? findColumn;
      LJCDataColumns? retValue = null;

      var columns = dbResult.Columns;
      var values = dbResult.Rows[0].Values;
      if (LJC.HasItems(columns)
        && LJC.HasItems(values))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (LJCDataValue value in values)
        {
          findColumn = columns.LJCSearchPropertyName(value.PropertyName);
          if (findColumn != null)
          {
            var dataColumn = new LJCDataColumn()
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
            if (0 == dataColumn.MaxLength)
            {
              dataColumn.MaxLength = 10;
            }
            if (dataColumn.MaxLength < 5)
            {
              dataColumn.MaxLength += 3;
            }
            retValue.Add(dataColumn);
          }
        }
      }
      return retValue;
    }

    // Deserializes the DbResult message.
    /// <include path='items/DeserializeMessage/*' file='Doc/DbResult.xml'/>
    public static LJCDBResult? DeserializeMessage(string result)
    {
      LJCDBResult? retValue = null;

      if (LJC.HasValue(result))
      {
        retValue = LJC.XmlDeserializeMessage(typeof(LJCDBResult), result)
          as LJCDBResult;
        if (null == retValue)
        {
          retValue = new LJCDBResult();
        }
      }
      return retValue;
    }

    // Gets the result values from the data row.
    /// <include path='items/GetRowValues/*' file='Doc/DbResult.xml'/>
    public static LJCDataValues GetRowValues(LJCDataColumns dataColumns, DataRow dataRow)
    {
      // Similar logic in LJCDBMessage.ResultConverter.GetPropertyName().
      object? value;
      var retValue = new LJCDataValues();

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

        if (value != null
          || dataColumn.AllowDBNull)
        {
          LJCDataValue dataValue = dataColumn;
          dataValue.Value = value;
          retValue.Add(dataValue);
        }
      }
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include path='items/HasColumns1/*' file='Doc/DbResult.xml'/>
    public static bool HasColumns(LJCDBResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && LJC.HasItems(dbResult.Columns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks if the result has Columns and Rows.
    /// <include path='items/HasColumns1/*' file='Doc/DbResult.xml'/>
    public static bool HasData(LJCDBResult dbResult)
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
    public static bool HasRows(LJCDBResult dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && LJC.HasItems(dbResult.Rows))
      {
        retValue = true;
      }
      return retValue;
    }

    // Adds the join values.
    /// <include path='items/AddJoinRowValues/*' file='Doc/DbResult.xml'/>
    private static void AddJoinRowValues(LJCDataValues dataValues, DataRow dataRow
      , LJCDBJoins? dbJoins)
    {
      if (LJC.HasItems(dbJoins))
      {
        foreach (LJCDBJoin dbJoin in dbJoins)
        {
          if (LJC.HasItems(dbJoin.Columns))
          {
            LJCDataValues joinValues = GetRowValues(dbJoin.Columns, dataRow);
            foreach (LJCDataValue dataValue in joinValues)
            {
              dataValues.Add(dataValue);
            }
          }
        }
      }
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBResult()
    {
      mRequestTypeName = RequestType.Select.ToString();

      Columns = [];
      //Rows = new DbRows();
      Rows = [];
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBResult(LJCDBResult item)
    {
      mRequestTypeName = RequestType.Select.ToString();

      AffectedRecords = item.AffectedRecords;
      //Columns = new LJCDataColumns(item.Columns);
      Columns = [.. item.Columns];
      DatabaseName = item.DatabaseName;
      ExecutedSql = item.ExecutedSql;
      RequestTypeName = item.RequestTypeName;
      //Rows = new DbRows(item.Rows);
      Rows = [.. item.Rows];
      SchemaName = item.SchemaName;
      TableName = item.TableName;
    }

    // Initializes an object instance with the DbResult object.
    /// <include path='items/DbResultC1/*' file='Doc/DbResult.xml'/>
    public LJCDBResult(LJCDBRequest dbRequest)
      : this(dbRequest.RequestTypeName, dbRequest.TableName, dbRequest.SchemaName
      , dbRequest.ProcedureName)
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbResultC2/*' file='Doc/DbResult.xml'/>
    public LJCDBResult(string requestTypeName, string tableName, string? schemaName = null
      , string? procedureName = null)
    {
      mRequestTypeName = RequestType.Select.ToString();

      //Columns = new LJCDataColumns();
      Columns = [];
      DatabaseName = null;
      ProcedureName = procedureName;
      RequestTypeName = requestTypeName;
      //Rows = new DbRows();
      Rows = [];
      SchemaName = schemaName;
      TableName = tableName;
    }
    #endregion

    #region Collection Methods

    // Clones the structure of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBResult? Clone()
    {
      LJCDBResult? retValue = MemberwiseClone() as LJCDBResult;
      return retValue;
    }

    // Checks if the result has Columns.
    /// <include path='items/HasColumns2/*' file='Doc/DbResult.xml'/>
    public bool HasColumns()
    {
      bool retValue = false;

      if (LJC.HasItems(Rows))
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

      if (LJC.HasItems(Rows))
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

      retValue = LJC.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include path='items/Serialize2/*' file='Doc/DbResult.xml'/>
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Public Methods

    // Get DbColumns from result records.
    /// <include path='items/GetValueColumns/*' file='Doc/DbResult.xml'/>
    public LJCDataColumns? GetValueColumns(LJCDataValues? dataValues = null)
    {
      LJCDataColumns? retValue = null;

      if (HasRows())
      {
        if (null == dataValues)
        {
          dataValues = Rows[0].Values;
        }
        if (LJC.HasItems(dataValues))
        {
          retValue = dataValues.LJCCreateColumns(Columns);
        }
      }
      return retValue;
    }

    // Sets the Columns property from the principle and join columns.
    /// <include path='items/SetColumns/*' file='Doc/DbResult.xml'/>
    public void SetColumns(LJCDataColumns? dataColumns, LJCDBJoins? dbJoins = null)
    {
      if (LJC.HasItems(dataColumns))
      {
        Columns = dataColumns.Clone();
      }
      if (LJC.HasItems(dbJoins))
      {
        foreach (LJCDBJoin dbJoin in dbJoins)
        {
          if (LJC.HasItems(dbJoin.Columns))
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
    public void SetColumns(LJCDBRequest dbRequest)
    {
      SetColumns(dbRequest.Columns, dbRequest.Joins);
    }

    // Sets the result records from the DataTable and DbRequest objects.
    /// <include path='items/SetData/*' file='Doc/DbResult.xml'/>
    public void SetData(DataTable dataTable, LJCDBRequest dbRequest)
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
    public void SetRows(DataTable dataTable, LJCDBJoins? dbJoins = null)
    {
      if (LJC.HasData(dataTable))
      {
        var dataColumns = GetDbColumns(dataTable.Columns);
        if (dataColumns != null)
        {
          foreach (DataRow dataRow in dataTable.Rows)
          {
            LJCDataValues dataValues = GetRowValues(dataColumns, dataRow);
            AddJoinRowValues(dataValues, dataRow, dbJoins);
            var row = new LJCDBRow()
            {
              Values = dataValues
            };
            Rows.Add(row);
          }
        }
      }
    }
    #endregion

    #region Private Methods
    #endregion

    #region Properties

    /// <summary>Gets or sets the non-query affected record count.</summary>
    public int AffectedRecords { get; set; }

    /// <summary>Gets the collection of columns that belong to this result.</summary>
    //[XmlArrayItem("Columns")]
    public LJCDataColumns Columns { get; set; }

    /// <summary>Gets or sets the Database name.</summary>
    public string? DatabaseName { get; set; }

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbResult.xml"; }
    }

    /// <summary>Gets or sets the executed SQL statement.</summary>
    public string? ExecutedSql
    {
      get => mExecutedSql;
      set => mExecutedSql = LJCNetString.InitString(value);
    }
    private string? mExecutedSql;

    /// <summary>Gets or sets the ProcedureName value.</summary>
    public string? ProcedureName
    {
      get => mProcedureName;
      set => mProcedureName = LJCNetString.InitString(value);
    }
    private string? mProcedureName;

    /// <summary>The request type.</summary>
    public string RequestTypeName
    {
      get => mRequestTypeName;
      set
      {
        if (value != null)
        {
          mRequestTypeName = value.Trim();
        }
      }
    }
    private string mRequestTypeName;

    /// <summary>A collection of DbValues objects.</summary>
    [XmlArrayItem("DbRows")]
    public LJCDBRows Rows { get; set; }

    /// <summary>The schema name.</summary>
    public string? SchemaName
    {
      get => mSchemaName;
      set => mSchemaName = LJCNetString.InitString(value);
    }
    private string? mSchemaName;

    /// <summary>The table name.</summary>
    public string? TableName
    {
      get => mTableName;
      set => mTableName = LJCNetString.InitString(value);
    }
    private string? mTableName;
    #endregion
  }
}
