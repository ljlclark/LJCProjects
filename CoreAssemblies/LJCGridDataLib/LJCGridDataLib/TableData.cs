// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableData.cs
//using LJCDBMessage;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LJC = LJCNetCommon.NetCommon;

namespace LJCGridDataLib
{
  // Provides DataTable helpers.
  /// <include file='Doc/TableData.xml'
  ///  path='items/TableData/*'/>
  public class TableData
  {
    #region Methods

    // Configure the Grid Columns from the DbRequest object definition.
    /// <include file='Doc/ResultData.xml'
    ///  path='items/GetGridColumns/*'/>
    public static LJCDataColumns GetGridColumns(DbRequest dbRequest
      , List<string> propertyNames = null)
    {
      LJCDataColumns retValue = null;

      if (dbRequest != null && dbRequest.Columns != null)
      {
        retValue = dbRequest.Columns.Clone();
        if (propertyNames != null)
        {
          retValue = dbRequest.Columns.LJCColumns(propertyNames);
          if (dbRequest.Joins != null)
          {
            foreach (DbJoin dbJoin in dbRequest.Joins)
            {
              retValue = dbJoin.Columns.LJCColumns(propertyNames);
              foreach (LJCDataColumn dbColumn in retValue)
              {
                retValue.Add(dbColumn.Clone());
              }
            }
          }
        }
      }
      return retValue;
    }

    // Updates a grid row with the DataRow values.
    /// <include file='Doc/TableData.xml'
    ///  path='items/RowSetValues/*'/>
    public static void RowSetValues(LJCGridRow ljcGridRow, DataRow adoRow
      , LJCDataColumns dataDefinition)
    {
      ArgumentDataRow(adoRow);

      object value;
      List<object> listValues = new List<object>();
      var gridColumns = ljcGridRow.DataGridView.Columns;
      foreach (DataGridViewColumn gridColumn in gridColumns)
      {
        var dataColumnName = gridColumn.Name;

        if (dataDefinition != null)
        {
          //var dbColumn = dataDefinition.LJCSearchPropertyName(dataColumnName);
          var dbColumn = dataDefinition[dataColumnName];
          if (dbColumn?.RenameAs != null)
          {
            dataColumnName = dbColumn.RenameAs;
          }
        }

        value = null;
        if (adoRow.Table.Columns.Contains(dataColumnName))
        {
          value = adoRow[dataColumnName];
        }
        listValues.Add(value);
      }
      var values = listValues.ToArray();
      ljcGridRow.SetValues(values);
    }
    #endregion

    #region Private Functions

    // Checks the DataRow argument.
    private static bool ArgumentDataRow(DataRow adoRow)
    {
      if (null == adoRow)
      {
        var message = "Missing argument dataRow.";
        throw new ArgumentNullException(message);
      }
      return true;
    }
    #endregion
  }
}