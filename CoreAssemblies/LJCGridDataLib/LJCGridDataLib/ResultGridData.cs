// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ResultGridData.cs
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBMessage;
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
    //public ResultGridData(LJCDataGrid grid = null)
    public ResultGridData()
    {
      //Grid = grid;
    }
    #endregion

    #region Configuration Methods

    // Configure the Grid Columns from the DbRequest object definition.
    /// <include path='items/SetGridColumns1/*' file='Doc/ResultGridData.xml'/>
    public void SetGridColumns(DbRequest dbRequest
      , List<string> propertyNames = null)
    {
      DbColumns dbColumns;

      if (dbRequest != null && dbRequest.Columns != null)
      {
        DataDefinition = dbRequest.Columns.Clone();
        GridColumns = DataDefinition.Clone();

        if (propertyNames != null)
        {
          GridColumns = dbRequest.Columns.LJCGetColumns(propertyNames);
          if (dbRequest.Joins != null)
          {
            foreach (DbJoin dbJoin in dbRequest.Joins)
            {
              dbColumns = dbJoin.Columns.LJCGetColumns(propertyNames);
              foreach (DbColumn dbColumn in dbColumns)
              {
                GridColumns.Add(dbColumn.Clone());
              }
            }
          }
        }
      }
    }

    // Configure the Display Columns from the Data object properties.
    /// <include path='items/SetGridColumns2/*' file='Doc/ResultGridData.xml'/>
    public void SetGridColumns(object dataObject
      , List<string> propertyNames = null)
    {
      DataDefinition = DbColumns.LJCCreateObjectColumns(dataObject);
      GridColumns = DataDefinition.Clone();

      if (propertyNames != null)
      {
        GridColumns = new DbColumns();
        foreach (string name in propertyNames)
        {
          if (IsIncluded(name, propertyNames))
          {
            var gridColumn = DataDefinition.LJCSearchPropertyName(name);
            if (gridColumn != null)
            {
              GridColumns.Add(gridColumn);
            }
          }
        }
      }
    }

    // Removes a display column.
    /// <include path='items/RemoveGridColumn/*' file='Doc/ResultGridData.xml'/>
    public void RemoveGridColumn(string columnName)
    {
      DbColumn column = GridColumns.Find(x => x.ColumnName == columnName);
      if (column != null)
      {
        GridColumns.Remove(column);
      }
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

    ///// <summary>Gets or sets the GridRow value.</summary>
    //public LJCGridRow LJCGridRow { get; set; }

    /// <summary>Gets or sets the DataDefinition value.</summary>
    public DbColumns DataDefinition { get; set; }

    ///// <summary>Gets or sets the DataValues value.</summary>
    //public DbValues DataValues { get; set; }

    /// <summary>Gets or sets the Grid Columns.</summary>
    public DbColumns GridColumns { get; private set; }
    #endregion
  }
}
