// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDbColumn.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  // Contains DataColumn related methods.
  public class LJCDbColumn
  {
    #region Static Methods

    // Clones a DataColumn object.
    /// <include path='items/DataColumnClone/*' file='Doc/TableData.xml'/>
    public static DataColumn? Clone(DataColumn dbColumn)
    {
      DataColumn? retDbColumn = null;
      if (dbColumn != null)
      {
        retDbColumn = new DataColumn()
        {
          AllowDBNull = dbColumn.AllowDBNull,
          AutoIncrement = dbColumn.AutoIncrement,
          Caption = dbColumn.Caption,
          ColumnName = dbColumn.ColumnName,
          DataType = dbColumn.DataType,
          DefaultValue = dbColumn.DefaultValue,
          MaxLength = dbColumn.MaxLength,
          Unique = dbColumn.Unique
        };
      }
      return retDbColumn;
    }

    // Creates a DbColumn object from a DataColumn object.
    /// <include path='items/GetDbColumn/*' file='Doc/TableData.xml'/>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumn? ToDataColumn(DataColumn? dbColumn)
    {
      LJCDataColumn? retColumn = null;

      if (dbColumn != null)
      {
        retColumn = new LJCDataColumn()
        {
          AllowDBNull = dbColumn.AllowDBNull,
          AutoIncrement = dbColumn.AutoIncrement,
          Caption = dbColumn.ColumnName,
          ColumnName = dbColumn.ColumnName,
          DataTypeName = dbColumn.DataType.Name,
          MaxLength = dbColumn.MaxLength,
          PropertyName = dbColumn.ColumnName,
          Unique = dbColumn.Unique
        };
      }
      return retColumn;
    }
    #endregion
  }
}
