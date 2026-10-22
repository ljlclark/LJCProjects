// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTableColumn5.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  // Contains methods to complement a DataColumn object.
  /// <include file='Doc/ProjectTableColumn.xml'
  ///  path='members/LJCTableColumn/*'/>
  public class LJCTableColumn
  {
    #region Static Methods

    // Clones a DataColumn object.
    /// <include file='Doc/TableColumn.xml'
    ///  path='members/Clone/*'/>
    public static DataColumn? Clone(DataColumn adoColumn)
    {
      DataColumn? retTableColumn = null;
      if (adoColumn != null)
      {
        retTableColumn = new DataColumn()
        {
          AllowDBNull = adoColumn.AllowDBNull,
          AutoIncrement = adoColumn.AutoIncrement,
          Caption = adoColumn.Caption,
          ColumnName = adoColumn.ColumnName,
          DataType = adoColumn.DataType,
          DefaultValue = adoColumn.DefaultValue,
          MaxLength = adoColumn.MaxLength,
          Unique = adoColumn.Unique
        };
      }
      return retTableColumn;
    }

    // Creates an LJCDataColumn object from a DataColumn object.
    /// <include file='Doc/TableColumn.xml'
    ///  path='members/ToDataColumn/*'/>
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
          IsUniqueKey = tableColumn.Unique
        };
      }
      return retTableColumn;
    }
    #endregion
  }
}
