// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableData.cs
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCGridDataLib
{
  // Provides DataTable helpers.
  /// <include path='items/TableData/*' file='Doc/TableData.xml'/>
  public class TableData
  {
    #region Public Functions

    // Creates a new DataColumns object.
    /// <include path='items/CreateDataColumns/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection CreateDataColumns()
    {
      DataColumnCollection retValue;

      DataTable workTable = new DataTable();
      retValue = workTable.Columns;
      return retValue;
    }

    // Clones a DataColumn object.
    /// <include path='items/DataColumnClone/*' file='Doc/TableData.xml'/>
    public static DataColumn DataColumnClone(DataColumn dataColumn)
    {
      DataColumn retValue = null;
      if (dataColumn != null)
      {
        retValue = new DataColumn()
        {
          AllowDBNull = dataColumn.AllowDBNull,
          AutoIncrement = dataColumn.AutoIncrement,
          Caption = dataColumn.Caption,
          ColumnName = dataColumn.ColumnName,
          DataType = dataColumn.DataType,
          DefaultValue = dataColumn.DefaultValue,
          MaxLength = dataColumn.MaxLength,
          Unique = dataColumn.Unique
        };
      }
      return retValue;
    }

    // Clones a DataColumn collection.
    /// <include path='items/DataColumnsClone/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection DataColumnsClone(DataTable dataTable)
    {
      DataColumn dataColumnClone;
      DataColumnCollection retValue = null;

      ArgumentDataTable(dataTable);

      if (dataTable.Columns != null && dataTable.Columns.Count > 0)
      {
        retValue = CreateDataColumns();
        foreach (DataColumn dataColumn in dataTable.Columns)
        {
          dataColumnClone = DataColumnClone(dataColumn);
          retValue.Add(dataColumnClone);
        }
      }
      return retValue;
    }

    // Creates a DbColumn object from a DataColumn object.
    /// <include path='items/GetDbColumn/*' file='Doc/TableData.xml'/>
    public static DbColumn GetDbColumn(DataColumn dataColumn)
    {
      DbColumn retValue;

      retValue = new DbColumn()
      {
        ColumnName = dataColumn.ColumnName,
        PropertyName = dataColumn.ColumnName,
        Caption = dataColumn.ColumnName,
        DataTypeName = dataColumn.DataType.Name,
        MaxLength = dataColumn.MaxLength,
        AutoIncrement = dataColumn.AutoIncrement,
        AllowDBNull = dataColumn.AllowDBNull,
        Unique = dataColumn.Unique
      };
      return retValue;
    }

    // Creates a DbColumns collection from a DataColumns collection.
    /// <summary>
    /// Creates a DbColumns collection from a DataColumns collection.
    /// </summary>
    /// <param name="dataColumns"></param>
    /// <returns></returns>
    public static DbColumns GetDbColumns(DataColumnCollection dataColumns)
    {
      DbColumns retValue = null;

      if (dataColumns != null && dataColumns.Count > 0)
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

    // Returns a set of DataColumns that match the supplied list.
    /// <include path='items/GetDataColumns/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection GetDataColumns(DataColumnCollection dataColumns
      , List<string> columnNames)
    {
      DataColumn dataColumnClone;
      DataColumnCollection retValue = null;

      if (dataColumns != null && dataColumns.Count > 0
        && columnNames != null && columnNames.Count > 0)
      {
        // Create columns from names.
        DataTable workTable = new DataTable();
        foreach (string columnName in columnNames)
        {
          DataColumn dataColumn = dataColumns[columnName];
          if (dataColumn != null)
          {
            dataColumnClone = DataColumnClone(dataColumn);
            workTable.Columns.Add(dataColumnClone);
          }
        }
        retValue = workTable.Columns;
      }
      return retValue;
    }

    // Configure the Grid Columns from the DbRequest object definition.
    /// <include path='items/GetGridColumns1/*' file='Doc/ResultData.xml'/>
    public static DbColumns GetGridColumns(DbRequest dbRequest
      , List<string> propertyNames = null)
    {
      DbColumns retValue = null;

      if (dbRequest != null && dbRequest.Columns != null)
      {
        retValue = dbRequest.Columns.Clone();
        if (propertyNames != null)
        {
          retValue = dbRequest.Columns.LJCGetColumns(propertyNames);
          if (dbRequest.Joins != null)
          {
            foreach (DbJoin dbJoin in dbRequest.Joins)
            {
              retValue = dbJoin.Columns.LJCGetColumns(propertyNames);
              foreach (DbColumn dbColumn in retValue)
              {
                retValue.Add(dbColumn.Clone());
              }
            }
          }
        }
      }
      return retValue;
    }

    // Creates a PropertyNames list from a DataColumns collection.
    /// <include path='items/GetPropertyNames/*' file='Doc/TableData.xml'/>
    public static List<string> GetPropertyNames(DataColumnCollection dataColumns)
    {
      List<string> retValue = null;

      if (dataColumns != null && dataColumns.Count > 0)
      {
        retValue = new List<string>();
        foreach (DataColumn dataColumn in dataColumns)
        {
          retValue.Add(dataColumn.ColumnName);
        }
      }
      return retValue;
    }

    // Updates a grid row with the DataRow values.
    /// <include path='items/RowSetValues/*' file='Doc/TableData.xml'/>
    public static void RowSetValues(LJCGridRow ljcGridRow, DataRow dataRow
      , DbColumns dataDefinition)
    {
      ArgumentDataRow(dataRow);

      object value;
      List<object> listValues = new List<object>();
      var gridColumns = ljcGridRow.DataGridView.Columns;
      foreach (DataGridViewColumn gridColumn in gridColumns)
      {
        var dataColumnName = gridColumn.Name;

        if (dataDefinition != null)
        {
          var dbColumn = dataDefinition.LJCSearchPropertyName(dataColumnName);
          if (dbColumn?.RenameAs != null)
          {
            dataColumnName = dbColumn.RenameAs;
          }
        }

        value = null;
        if (dataRow.Table.Columns.Contains(dataColumnName))
        {
          value = dataRow[dataColumnName];
        }
        listValues.Add(value);
      }
      var values = listValues.ToArray();
      ljcGridRow.SetValues(values);
    }

    #region Private Functions

    // Add the Primary Key lookup values.
    private static void AddPrimaryKeyValues(DbColumns dataDefinition, LJCGridRow row
      , DbValue dbValue)
    {
      if (dbValue.Value != null)
      {
        DbColumn dbColumn = dataDefinition.LJCSearchPropertyName(dbValue.PropertyName);
        if (dbColumn != null && dbColumn.IsPrimaryKey)
        {
          switch (dbColumn.DataTypeName)
          {
            case "Int32":
              int intKeyValue = (int)dbValue.Value;
              row.LJCSetInt32(dbColumn.ColumnName, intKeyValue);
              break;

            case "Int64":
              long longKeyValue = (long)dbValue.Value;
              row.LJCSetInt64(dbColumn.ColumnName, longKeyValue);
              break;

            case "String":
              if (dbValue.Value != null)
              {
                row.LJCSetString(dbColumn.ColumnName, dbValue.Value.ToString());
              }
              break;
          }
        }
      }
    }

    // Checks the DataRow argument.
    private static bool ArgumentDataRow(DataRow dataRow)
    {
      if (null == dataRow)
      {
        var message = "Missing argument dataRow.";
        throw new ArgumentNullException(message);
      }
      return true;
    }

    // Checks the DataTable argument.
    private static bool ArgumentDataTable(DataTable dataTable)
    {
      if (null == dataTable)
      {
        var message = "Missing argument dataTable.";
        throw new ArgumentNullException(message);
      }
      return true;
    }
    #endregion
    #endregion
  }
}