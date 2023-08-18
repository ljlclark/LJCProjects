// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableGridData.cs
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCGridDataLib
{
  // Provides DataTable helpers for an LJCDataGrid control.
  /// <include path='items/TableGridData/*' file='Doc/TableGridData.xml'/>
  public class TableGridData
  {
    #region Static Methods

    // Creates a new DataColumns object.
    /// <include path='items/CreateDataColumns/*' file='Doc/TableGridData.xml'/>
    public static DataColumnCollection CreateDataColumns()
    {
      DataColumnCollection retValue;

      DataTable workTable = new DataTable();
      retValue = workTable.Columns;
      return retValue;
    }

    // Clones a DataColumn object.
    /// <include path='items/DataColumnClone/*' file='Doc/TableGridData.xml'/>
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
    /// <include path='items/DataColumnsClone/*' file='Doc/TableGridData.xml'/>
    public static DataColumnCollection DataColumnsClone(DataTable dataTable)
    {
      DataColumn dataColumnClone;
      DataColumnCollection retValue = null;

      if (dataTable != null
        && dataTable.Columns != null && dataTable.Columns.Count > 0)
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

    // Creates a ColumnNames list from a DataColumns collection.
    /// <include path='items/GetColumnNames/*' file='Doc/TableGridData.xml'/>
    public static List<string> GetColumnNames(DataColumnCollection dataColumns)
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

    // Creates a DbColumn object from a DataColumn object.
    /// <include path='items/GetDbColumn/*' file='Doc/TableGridData.xml'/>
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
    /// <include path='items/GetDbColumns/*' file='Doc/TableGridData.xml'/>
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
    /// <include path='items/GetDataColumns/*' file='Doc/TableGridData.xml'/>
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
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/TableGridC/*' file='Doc/TableGridData.xml'/>
    public TableGridData(LJCDataGrid ljcGrid)
    {
      ArgumentLJCGrid(ljcGrid);

      mLJCGrid = ljcGrid;
    }
    #endregion

    #region Row Data Methods

    // Loads grid rows from the DataRows collection.
    /// <include path='items/LoadRows/*' file='Doc/TableGridData.xml'/>
    public void LoadRows(DataTable dataTable)
    {
      MemberLJCGrid();
      ArgumentDataTable(dataTable);

      foreach (DataRow dataRow in dataTable.Rows)
      {
        var ljcGridRow = mLJCGrid.LJCRowAdd();
        RowSetValues(ljcGridRow, dataRow);
      }
    }

    // Updates a grid row with the DataRow values.
    /// <include path='items/RowSetValues/*' file='Doc/TableGridData.xml'/>
    public void RowSetValues(LJCGridRow ljcGridRow, DataRow dataRow)
    {
      MemberLJCGrid();
      ArgumentDataRow(dataRow);

      List<object> listValues = new List<object>();
      var gridColumns = ljcGridRow.DataGridView.Columns;
      foreach (DataGridViewColumn gridColumn in gridColumns)
      {
        listValues.Add(dataRow[gridColumn.Name]);
      }
      var values = listValues.ToArray();
      ljcGridRow.SetValues(values);
    }

    // Updates the current row with the DataRow values.
    /// <include path='items/RowUpdate/*' file='Doc/TableGridData.xml'/>
    public void RowUpdate(DataRow dataRow)
    {
      MemberLJCGrid();
      ArgumentDataRow(dataRow);

      if (mLJCGrid.CurrentRow is LJCGridRow ljcGridRow)
      {
        RowSetValues(ljcGridRow, dataRow);
      }
    }
    #endregion

    #region Private Methods

    // Checks the DataRow argument.
    private bool ArgumentDataRow(DataRow dataRow)
    {
      if (null == dataRow)
      {
        var message = "Missing argument dataRow.";
        throw new ArgumentNullException(message);
      }
      return true;
    }

    // Checks the DataTable argument.
    private bool ArgumentDataTable(DataTable dataTable)
    {
      if (false == NetCommon.HasData(dataTable))
      {
        var message = "Missing argument dataTable.";
        throw new ArgumentNullException(message);
      }
      return true;
    }

    // Checks the LJCDataGrid argument.
    private bool ArgumentLJCGrid(LJCDataGrid ljcGrid)
    {
      if (null == ljcGrid)
      {
        var message = "Missing argument ljcGrid.";
        throw new MissingMemberException(message);
      }
      return true;
    }

    // Checks the mLJCGrid member.
    private bool MemberLJCGrid()
    {
      if (null == mLJCGrid)
      {
        var message = "Missing member mLJCGrid.";
        throw new MissingMemberException(message);
      }
      return true;
    }
    #endregion

    private readonly LJCDataGrid mLJCGrid;
  }
}