// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTableColumn.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  // Contains methods to complement a DataColumn object.
  public class LJCTableColumn
  {
    #region Static Methods

    // Clones a DataColumn object.
    /// <include path='items/DataColumnClone/*' file='Doc/LJCTableColumn.xml'/>
    public static DataColumn? Clone(DataColumn tableColumn)
    {
      DataColumn? retTableColumn = null;
      if (tableColumn != null)
      {
        retTableColumn = new DataColumn()
        {
          AllowDBNull = tableColumn.AllowDBNull,
          AutoIncrement = tableColumn.AutoIncrement,
          Caption = tableColumn.Caption,
          ColumnName = tableColumn.ColumnName,
          DataType = tableColumn.DataType,
          DefaultValue = tableColumn.DefaultValue,
          MaxLength = tableColumn.MaxLength,
          Unique = tableColumn.Unique
        };
      }
      return retTableColumn;
    }

    // Creates an LJCDataColumn object from a DataColumn object.
    /// <include path='items/GetDbColumn/*' file='Doc/LJCTableColumn.xml'/>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumn? ToDataColumn(DataColumn? tableColumn)
    {
      LJCDataColumn? retTableColumn = null;

      if (tableColumn != null)
      {
        retTableColumn = new LJCDataColumn()
        {
          AllowDBNull = tableColumn.AllowDBNull,
          AutoIncrement = tableColumn.AutoIncrement,
          Caption = tableColumn.ColumnName,
          ColumnName = tableColumn.ColumnName,
          DataTypeName = tableColumn.DataType.Name,
          MaxLength = tableColumn.MaxLength,
          PropertyName = tableColumn.ColumnName,
          Unique = tableColumn.Unique
        };
      }
      return retTableColumn;
    }
    #endregion
  }
}
