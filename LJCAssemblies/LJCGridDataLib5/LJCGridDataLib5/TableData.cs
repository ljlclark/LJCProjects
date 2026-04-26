// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableData.cs
using LJCDBMessage5;
using LJCNetCommon5;
//using LJCWinFormControls;
using System.Data;

namespace LJCGridDataLib5
{
  // Provides DataTable helpers.
  /// <include path='items/TableData/*' file='Doc/TableData.xml'/>
  public class TableData
  {
    #region Public Functions

    //// Updates a grid row with the DataRow values.
    ///// <include path='items/RowSetValues/*' file='Doc/TableData.xml'/>
    //public static void RowSetValues(LJCGridRow ljcGridRow, DataRow dataRow
    //  , DbColumns dataDefinition)
    //{
    //  ArgumentDataRow(dataRow);

    //  object value;
    //  List<object> listValues = new List<object>();
    //  var gridColumns = ljcGridRow.DataGridView.Columns;
    //  foreach (DataGridViewColumn gridColumn in gridColumns)
    //  {
    //    var dataColumnName = gridColumn.Name;

    //    if (dataDefinition != null)
    //    {
    //      var dbColumn = dataDefinition.LJCSearchPropertyName(dataColumnName);
    //      if (dbColumn?.RenameAs != null)
    //      {
    //        dataColumnName = dbColumn.RenameAs;
    //      }
    //    }

    //    value = null;
    //    if (dataRow.Table.Columns.Contains(dataColumnName))
    //    {
    //      value = dataRow[dataColumnName];
    //    }
    //    listValues.Add(value);
    //  }
    //  var values = listValues.ToArray();
    //  ljcGridRow.SetValues(values);
    //}
    #endregion

    #region Private Functions

    //// Add the Primary Key lookup values.
    //private static void AddPrimaryKeyValues(DbColumns dataDefinition, LJCGridRow row
    //  , DbValue dbValue)
    //{
    //  if (dbValue.Value != null)
    //  {
    //    DbColumn dbColumn = dataDefinition.LJCSearchPropertyName(dbValue.PropertyName);
    //    if (dbColumn != null && dbColumn.IsPrimaryKey)
    //    {
    //      switch (dbColumn.DataTypeName)
    //      {
    //        case "Int32":
    //          int intKeyValue = (int)dbValue.Value;
    //          row.LJCSetInt32(dbColumn.ColumnName, intKeyValue);
    //          break;

    //        case "Int64":
    //          long longKeyValue = (long)dbValue.Value;
    //          row.LJCSetInt64(dbColumn.ColumnName, longKeyValue);
    //          break;

    //        case "String":
    //          if (dbValue.Value != null)
    //          {
    //            row.LJCSetString(dbColumn.ColumnName, dbValue.Value.ToString());
    //          }
    //          break;
    //      }
    //    }
    //  }
    //}

    //// Checks the DataRow argument.
    //private static bool ArgumentDataRow(DataRow dataRow)
    //{
    //  if (null == dataRow)
    //  {
    //    var message = "Missing argument dataRow.";
    //    throw new ArgumentNullException(message);
    //  }
    //  return true;
    //}
    #endregion
  }
}