// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableGridData.cs
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
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
    public TableGridData(LJCDataGrid grid)
    {
      mGrid = grid;
    }
    #endregion

    #region Configuration Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public bool HasDisplayColumn(string propertyName)
    {
      bool retValue = false;

      var dbColumn = DisplayColumns.LJCGetColumn(propertyName);
      if (dbColumn != null)
      {
        retValue = true;
      }
      return retValue;
    }

    // Sets the Display Columns from the DataColumns object.
    /// <include path='items/SetDisplayColumns/*' file='Doc/TableGridData.xml'/>
    public void SetDisplayColumns(DataColumnCollection dataColumns
      , List<string> propertyNames = null)
    {
      // Create DataDefinition from dataColumns.
      DataDefinition = GetDbColumns(dataColumns);
      DisplayColumns = new DbColumns(DataDefinition);

      // Create DisplayColumns with property names.
      if (NetCommon.HasItems(propertyNames))
      {
        DisplayColumns = DataDefinition.LJCGetColumns(propertyNames);
      }
    }

    // Sets the Display Columns from the DataObject properties.
    /// <include path='items/SetDisplayColumns1/*' file='Doc/TableGridData.xml'/>
    public void SetDisplayColumns(object dataObject
      , DbColumns dataDefinition = null, List<string> propertyNames = null)
    {
      // Create DataDefinition from dataObject value.
      DataDefinition = DbColumns.LJCCreateObjectColumns(dataObject
        , dataDefinition);
      DisplayColumns = new DbColumns(DataDefinition);

      // Create DisplayColumns with property names.
      if (NetCommon.HasItems(propertyNames))
      {
        DisplayColumns = DataDefinition.LJCGetColumns(propertyNames);
      }
    }

    // Removes a display column.
    /// <include path='items/RemoveDisplayColumn/*' file='Doc/TableGridData.xml'/>
    public void RemoveDisplayColumn(string columnName)
    {
      foreach (DbColumn dataColumn in DisplayColumns)
      {
        if (dataColumn.ColumnName == columnName)
        {
          DisplayColumns.Remove(dataColumn);
          break;
        }
      }
    }
    #endregion

    #region Row Data Methods

    // Loads grid rows from the DataRows collection restricted by the
    // DisplayColumns property.
    /// <include path='items/LoadRows/*' file='Doc/TableGridData.xml'/>
    public void LoadRows(DataTable dataTable)
    {
      if (NetCommon.HasData(dataTable))
      {
        foreach (DataRow dataRow in dataTable.Rows)
        {
          RowAdd(dataRow);
        }
      }
    }

    // Adds a grid row and updates it with the DataRow values.
    /// <include path='items/RowAdd/*' file='Doc/TableGridData.xml'/>
    public LJCGridRow RowAdd(DataRow dataRow)
    {
      LJCGridRow retValue = null;

      if (mGrid != null)
      {
        retValue = mGrid.LJCRowAdd();
        RowSetValues(retValue, dataRow);

        // Allow setting stored values.
        GridRow = retValue;
        DataRecord = dataRow;
        OnAddRow();
      }
      return retValue;
    }

    // Updates a grid row with the DataRow values.
    /// <include path='items/RowSetValues/*' file='Doc/TableGridData.xml'/>
    public void RowSetValues(LJCGridRow gridRow, DataRow dataRow)
    {
      List<object> listValues = new List<object>();
      foreach (DataGridViewColumn gridColumn in mGrid.Columns)
      {
        var propertyName = gridColumn.Name;
        if (IncludeValue(propertyName))
        {
          var value = dataRow[propertyName];
          if (value != null)
          {
            //gridRow.LJCSetCellText(propertyName, value);
            listValues.Add(value);
          }
        }
      }
      var values = listValues.ToArray();
      gridRow.SetValues(values);
    }

    // Updates the current row with the DataRow values.
    /// <include path='items/RowUpdate/*' file='Doc/TableGridData.xml'/>
    public void RowUpdate(DataRow dataRow)
    {
      if (mGrid != null
        && mGrid.CurrentRow is LJCGridRow gridRow)
      {
        RowSetValues(gridRow, dataRow);
      }
    }

    // If DisplayColumns, check for included column.
    private bool IncludeValue(string propertyName)
    {
      bool retValue = true;

      if (NetCommon.HasItems(DisplayColumns)
        && false == HasDisplayColumn(propertyName))
      {
        retValue = false;
      }
      return retValue;
    }

    // Fires the AddRow event.
    /// <include path='items/OnAddRow/*' file='Doc/ResultGridData.xml'/>
    protected void OnAddRow()
    {
      AddRow?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataDefinition value.</summary>
    public DbColumns DataDefinition { get; set; }

    /// <summary>Gets or sets the DataRecord value.</summary>
    public DataRow DataRecord { get; set; }

    /// <summary>Gets the DisplayColumns.</summary>
    //public DataColumnCollection DisplayColumns { get; private set; }
    public DbColumns DisplayColumns { get; private set; }

    /// <summary>Gets or sets the GridRow value.</summary>
    public LJCGridRow GridRow { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;

    /// <summary>The AddRow event.</summary>
    public event EventHandler<EventArgs> AddRow;
    #endregion
  }
}
