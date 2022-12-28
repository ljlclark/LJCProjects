// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ResultGridData.cs
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBMessage;
using System;
using System.Collections.Generic;

namespace LJCGridDataLib
{
  // Provides DbResult helpers for an LJCDataGrid control.
  /// <include path='items/ResultGrid/*' file='Doc/ProjectGridDataLib.xml'/>
  public class ResultGridData
  {
    #region Constructors

    // Initalizes an object instance.
    /// <include path='items/ResultGridDataC/*' file='Doc/ResultGridData.xml'/>
    public ResultGridData(LJCDataGrid grid = null)
    {
      Grid = grid;
    }
    #endregion

    #region Configuration Methods

    // Configure the Display Columns from the DbColumns definition.
    /// <include path='items/SetDisplayColumns/*' file='Doc/ResultGridData.xml'/>
    public void SetDisplayColumns(DbColumns dbColumns
      , List<string> columnNames = null)
    {
      DataDefinition = dbColumns.Clone();
      DisplayColumns = DataDefinition.Clone();

      if (columnNames != null)
      {
        DisplayColumns = DataDefinition.LJCGetColumns(columnNames);
      }
    }

    // Configure the Display Columns from the DbRequest object definition.
    /// <include path='items/SetDisplayColumns1/*' file='Doc/ResultGridData.xml'/>
    public void SetDisplayColumns(DbRequest dbRequest
      , List<string> columnNames = null)
    {
      DbColumns dbColumns;

      if (dbRequest != null && dbRequest.Columns != null)
      {
        DataDefinition = dbRequest.Columns.Clone();
        DisplayColumns = DataDefinition.Clone();

        if (columnNames != null)
        {
          DisplayColumns = dbRequest.Columns.LJCGetColumns(columnNames);
          if (dbRequest.Joins != null)
          {
            foreach (DbJoin dbJoin in dbRequest.Joins)
            {
              dbColumns = dbJoin.Columns.LJCGetColumns(columnNames);
              foreach (DbColumn dbColumn in dbColumns)
              {
                DisplayColumns.Add(dbColumn.Clone());
              }
            }
          }
        }
      }
    }

    // Configure the Display Columns from the Data object properties.
    /// <include path='items/SetDisplayColumns2/*' file='Doc/ResultGridData.xml'/>
    public void SetDisplayColumns(object dataObject
      , List<string> propertyNames = null)
    {
      DataDefinition = DbColumns.LJCCreateObjectColumns(dataObject);
      DisplayColumns = DataDefinition.Clone();

      if (propertyNames != null)
      {
        DisplayColumns = new DbColumns();
        foreach (string name in propertyNames)
        {
          if (IsIncluded(name, propertyNames))
          {
            var displayColumn = DataDefinition.LJCSearchPropertyName(name);
            if (displayColumn != null)
            {
              DisplayColumns.Add(displayColumn);
            }
          }
        }
      }
    }

    // Removes a display column.
    /// <include path='items/RemoveDisplayColumn/*' file='Doc/ResultGridData.xml'/>
    public void RemoveDisplayColumn(string columnName)
    {
      DbColumn column = DisplayColumns.Find(x => x.ColumnName == columnName);
      if (column != null)
      {
        DisplayColumns.Remove(column);
      }
    }
    #endregion

    #region Row Data Methods

    // Loads the grid rows from the result DbRecords records.
    /// <include path='items/LoadRows/*' file='Doc/ResultGridData.xml'/>
    public void LoadRows(DbResult dbResult)
    {
      if (DbResult.HasRows(dbResult))
      {
        if (null == DisplayColumns)
        {
          // Create default DisplayColumns
          DisplayColumns = dbResult.Columns;
        }
        LoadRows(dbResult.Rows);
      }
    }

    // Loads the grid rows from the DbRows object.
    /// <include path='items/LoadRows1/*' file='Doc/ResultGridData.xml'/>
    public void LoadRows(DbRows dbRows)
    {
      if (DbRows.HasItems(dbRows))
      {
        foreach (DbRow dbRow in dbRows)
        {
          RowAdd(dbRow.Values);
        }
      }
    }

    // Adds a grid row and updates it with the DbValues.
    /// <include path='items/RowAdd/*' file='Doc/ResultGridData.xml'/>
    public LJCGridRow RowAdd(DbValues record)
    {
      LJCGridRow retValue = null;

      if (Grid != null)
      {
        retValue = Grid.LJCRowAdd();
        RowSetValues(retValue, record);

        // Allow setting stored values.
        GridRow = retValue;
        DataRecord = record;
        OnAddRow();
      }
      return retValue;
    }

    // Updates a grid row with the DbValues.
    /// <include path='items/RowSetValues/*' file='Doc/ResultGridData.xml'/>
    public void RowSetValues(LJCGridRow gridRow, DbValues record)
    {
      if (DisplayColumns != null && DisplayColumns.Count > 0)
      {
        foreach (DbColumn dbColumn in DisplayColumns)
        {
          // Grid columns are named after the object property names.
          string propertyName = dbColumn.PropertyName;
          DbValue dbValue = record.LJCSearchPropertyName(propertyName);
          if (dbValue != null)
          {
            var value = NetCommon.GetString(dbValue.Value);
            gridRow.LJCSetCellText(propertyName, value);
            if (dbColumn.IsPrimaryKey)
            {
              AddPrimaryKeyValues(gridRow, dbValue);
            }
          }
        }
      }
    }

    // Updates the current row with the DbValues.
    /// <include path='items/RowUpdate/*' file='Doc/ResultGridData.xml'/>
    public void RowUpdate(DbValues record)
    {
      if (Grid != null
        && Grid.CurrentRow is LJCGridRow gridRow)
      {
        RowSetValues(gridRow, record);
      }
    }

    // Fires the AddRow event.
    /// <include path='items/OnAddRow/*' file='Doc/ResultGridData.xml'/>
    protected void OnAddRow()
    {
      AddRow?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Private Methods

    // Add the Primary Key lookup values.
    private void AddPrimaryKeyValues(LJCGridRow row, DbValue dbValue)
    {
      if (dbValue.Value != null)
      {
        DbColumn dbColumn = DataDefinition.LJCSearchPropertyName(dbValue.PropertyName);
        if (dbColumn != null && dbColumn.IsPrimaryKey)
        {
          switch (dbColumn.DataTypeName)
          {
            // *** Begin *** Add - 9/8
            case "Int64":
              long longKeyValue = (long)dbValue.Value;
              row.LJCSetInt64(dbColumn.ColumnName, longKeyValue);
              break;
            // *** End   *** Add - 9/8

            case "Int32":
              int intKeyValue = (int)dbValue.Value;
              row.LJCSetInt32(dbColumn.ColumnName, intKeyValue);
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

    // Checks if the column name is in the included names list. 
    private bool IsIncluded(string name, List<string> includedNames)
    {
      bool retValue = true;

      if (includedNames != null)
      {
        if (true == string.IsNullOrWhiteSpace(includedNames.Find(x => x.Equals(name))))
        {
          retValue = false;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Grid control value.</summary>
    public LJCDataGrid Grid;

    /// <summary>Gets or sets the GridRow value.</summary>
    public LJCGridRow GridRow { get; set; }

    /// <summary>Gets or sets the DataDefinition value.</summary>
    public DbColumns DataDefinition { get; set; }

    /// <summary>Gets or sets the DataRecord value.</summary>
    public DbValues DataRecord { get; set; }

    /// <summary>Gets or sets the Display Columns.</summary>
    public DbColumns DisplayColumns { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The AddRow event.</summary>
    public event EventHandler<EventArgs> AddRow;
    #endregion
  }
}
