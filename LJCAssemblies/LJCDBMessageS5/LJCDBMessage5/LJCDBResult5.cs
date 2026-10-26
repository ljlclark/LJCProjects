// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBResult.cs
using LJCNetCommon5;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace LJCDBMessage5
{
  // Represents a data result.
  /// <include file='Doc/DbResult.xml'
  ///  path='items/LJCDBResult/*'/>
  //[XmlRoot("DbResult")]
  public class LJCDBResult
  {
    #region Table Data Static Methods?

    // Creates a DataColumn object from a ADO Column object. 
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetDataColumn/*'/>
    public static LJCDataColumn GetDataColumn(DataColumn adoColumn)
    {
      LJCDataColumn retValue;

      retValue = new LJCDataColumn()
      {
        AllowDBNull = adoColumn.AllowDBNull,
        AutoIncrement = adoColumn.AutoIncrement,
        Caption = adoColumn.ColumnName,
        ColumnName = adoColumn.ColumnName,
        DataTypeName = adoColumn.DataType.Name,
        MaxLength = adoColumn.MaxLength,
        PropertyName = adoColumn.ColumnName,
        IsUniqueKey = adoColumn.Unique
      };
      return retValue;
    }

    // Creates a DataColumns collection from an ADO Columns collection.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetDataColumns/*'/>
    public static LJCDataColumns? GetDataColumns(DataColumnCollection adoColumns)
    {
      LJCDataColumns? retValue = null;

      //if (HasColumns(dataColumns))
      if (LJC.HasTableColumns(adoColumns))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (DataColumn adoColumn in adoColumns)
        {
          LJCDataColumn dataColumn = GetDataColumn(adoColumn);
          retValue.Add(dataColumn);
        }
      }
      return retValue;
    }

    // Checks the DataColumnCollection object for items.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns/*'/>
    public static bool HasColumns([NotNullWhen(true)] DataColumnCollection adoColumns)
    {
      bool retValue = false;

      if (LJC.HasTableColumns(adoColumns))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Static Methods

    // Creates combined DataColumns from DataColumns and DataValues.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/CreateResultColumns/*'/>
    public static LJCDataColumns? CreateResultColumns(LJCDBResult dbResult)
    {
      LJCDataColumn? findColumn;
      LJCDataColumns? retValue = null;

      var dataColumns = dbResult.Columns;
      var dataValues = dbResult.Rows[0].Values;
      if (LJC.HasListItems(dataColumns)
        && LJC.HasListItems(dataValues))
      {
        //retValue = new LJCDataColumns();
        retValue = [];
        foreach (LJCDataValue dataValue in dataValues)
        {
          //findColumn = dataColumns.LJCSearchPropertyName(dataValue.PropertyName);
          findColumn = dataColumns[dataValue.PropertyName];
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
              Value = dataValue.Value
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/DeserializeMessage/*'/>
    public static LJCDBResult? DeserializeMessage(string result)
    {
      LJCDBResult? retValue = null;

      if (LJC.HasText(result))
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetRowValues/*'/>
    public static LJCDataValues GetRowValues(LJCDataColumns dataColumns
      , DataRow dataRow)
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasColumns1/*'/>
    public static bool HasColumns([NotNullWhen(true)] LJCDBResult? dbResult)
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
    ///  path='items/HasData1/*'/>
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/HasRows1/*'/>
    public static bool HasRows([NotNullWhen(true)] LJCDBResult? dbResult)
    {
      bool retValue = false;

      if (dbResult != null
        && LJC.HasListItems(dbResult.Rows))
      {
        retValue = true;
      }
      return retValue;
    }

    // Adds the join values.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/AddJoinRowValues/*'/>
    private static void AddJoinRowValues(LJCDataValues dataValues
      , DataRow dataRow, LJCDBJoins? dbJoins)
    {
      if (LJC.HasListItems(dbJoins))
      {
        foreach (LJCDBJoin dbJoin in dbJoins)
        {
          if (LJC.HasListItems(dbJoin.Columns))
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

    #region Properties

    // Gets or sets the non-query affected record count.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/AffectedRecords/*'/>
    public int AffectedRecords { get; set; }

    // Gets the collection of columns that belong to this result.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Columns/*'/>
    public LJCDataColumns? Columns { get; set; }

    // Gets or sets the Database name.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/DatabaseName/*'/>
    public string? DatabaseName { get; set; }

    // Gets the Default File Name.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "DbResult.xml";
    }

    // Gets or sets the executed SQL statement.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/ExecutedSql/*'/>
    public string? ExecutedSql
    {
      get => mExecutedSql;
      set => mExecutedSql = value?.Trim();
    }
    private string? mExecutedSql;

    // Gets or sets the ProcedureName value.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/ProcedureName/*'/>
    public string? ProcedureName
    {
      get => mProcedureName;
      set => mProcedureName = value?.Trim();
    }
    private string? mProcedureName;

    // The request type.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/RequestTypeName/*'/>
    public string RequestTypeName
    {
      get => mRequestTypeName;
      set => mRequestTypeName = value.Trim();
    }
    private string mRequestTypeName;

    // A collection of DbValues objects.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Rows/*'/>
    [XmlArrayItem("DbRows")]
    public LJCDBRows Rows { get; set; }

    // The schema name.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SchemaName/*'/>
    public string? SchemaName
    {
      get => mSchemaName;
      set => mSchemaName = value?.Trim();
    }
    private string? mSchemaName;

    // The table name.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/TableName/*'/>
    public string? TableName
    {
      get => mTableName;
      set => mTableName = value?.Trim();
    }
    private string? mTableName;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBResult()
    {
      mRequestTypeName = RequestType.Select.ToString();

      Columns = [];
      //Rows = new DbRows();
      Rows = [];
    }

    // Initializes an object instance with the DbResult object.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Constructor/*'/>
    public LJCDBResult(LJCDBRequest dbRequest)
      : this(dbRequest.RequestTypeName, dbRequest.TableName
          , dbRequest.SchemaName, dbRequest.ProcedureName)
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/ConstructorParam/*'/>
    public LJCDBResult(string requestTypeName, string? tableName
      , string? schemaName = null, string? procedureName = null)
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

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ConstructorCopy/*'/>
    public LJCDBResult(LJCDBResult item)
    {
      mRequestTypeName = RequestType.Select.ToString();

      AffectedRecords = item.AffectedRecords;
      Columns = [];
      if (LJC.HasListItems(item.Columns))
      {
        Columns = [.. item.Columns];
      }
      DatabaseName = item.DatabaseName;
      ExecutedSql = item.ExecutedSql;
      RequestTypeName = item.RequestTypeName;
      //Rows = new DbRows(item.Rows);
      Rows = [.. item.Rows];
      SchemaName = item.SchemaName;
      TableName = item.TableName;
    }
    #endregion

    #region Collection Methods

    // Clones the structure of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBResult? Clone()
    {
      LJCDBResult? retValue = MemberwiseClone() as LJCDBResult;
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
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Methods

    // Get DataColumns from result records.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/GetValueColumns/*'/>
    public LJCDataColumns? GetValueColumns(LJCDataValues? dataValues = null)
    {
      LJCDataColumns? retValue = null;

      if (HasRows())
      {
        if (null == dataValues)
        {
          dataValues = Rows[0].Values;
        }
        if (LJC.HasListItems(dataValues))
        {
          retValue = dataValues.LJCCreateColumns(Columns);
        }
      }
      return retValue;
    }

    // Sets the Columns property from the principle and join columns.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetColumns1/*'/>
    public void SetColumns(LJCDataColumns? dataColumns, LJCDBJoins? dbJoins = null)
    {
      if (LJC.HasListItems(dataColumns))
      {
        Columns = dataColumns.Clone();
      }
      if (LJC.HasListItems(dbJoins)
        && Columns != null)
      {
        foreach (LJCDBJoin dbJoin in dbJoins)
        {
          if (LJC.HasListItems(dbJoin.Columns))
          {
            foreach (LJCDataColumn dataColumn in dbJoin.Columns)
            {
              Columns.Add(dataColumn);
            }
          }
        }
      }
    }

    // Sets the Columns property from the Request columns.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetColumns2/*'/>
    public void SetColumns(LJCDBRequest dbRequest)
    {
      SetColumns(dbRequest.Columns, dbRequest.Joins);
    }

    // Sets the result records from the DataTable and DbRequest objects.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetData/*'/>
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
    /// <include file='Doc/DbResult.xml'
    ///  path='items/SetRows/*'/>
    public void SetRows(DataTable dataTable, LJCDBJoins? dbJoins = null)
    {
      if (LJC.HasTableData(dataTable))
      {
        var dataColumns = GetDataColumns(dataTable.Columns);
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
  }
}
