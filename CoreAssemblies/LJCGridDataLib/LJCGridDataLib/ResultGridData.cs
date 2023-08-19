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
    public ResultGridData()
    {
      //Grid = grid;
    }
    #endregion

    #region Configuration Methods

    // Configure the Grid Columns from the DbRequest object definition.
    /// <include path='items/GetGridColumns1/*' file='Doc/ResultGridData.xml'/>
    public DbColumns GetGridColumns(DbRequest dbRequest
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

    //// Configure the Grid Columns from the Data object properties.
    ///// <include path='items/GetGridColumns2/*' file='Doc/ResultGridData.xml'/>
    //public DbColumns GetGridColumns(object dataObject
    //  , List<string> propertyNames = null)
    //{
    //  var retValue = DbColumns.LJCCreateObjectColumns(dataObject);
    //  if (propertyNames != null)
    //  {
    //    retValue = retValue.LJCGetColumns(propertyNames);
    //  }
    //  return retValue;
    //}
    #endregion

    #region Private Methods

    // Add the Primary Key lookup values.
    private void AddPrimaryKeyValues(DbColumns dataDefinition, LJCGridRow row
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
    #endregion
  }
}
