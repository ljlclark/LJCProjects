// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTableColumn.cs
using LJCNetCommon;
using System.Data;

namespace LJCDataAccess
{
  // Contains methods to complement a DataColumn object.
  /// <include file='Doc/LJCTableColumn.xml'
  ///  path='items/LJCTableColumn/*'/>
  public class LJCTableColumn
  {
    #region Static Methods

    // Clones a DataColumn object.
    /// <include file='Doc/LJCTableColumn.xml'
    ///  path='items/DataColumnClone/*'/>
    // Note: Also in LJCGridDataLib.TableData
    public static DataColumn Clone(DataColumn adoColumn)
    {
      DataColumn retTableColumn = null;
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
    /// <include file='Doc/LJCTableColumn.xml'
    ///  path='items/ToDataColumn/*'/>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumn ToDataColumn(DataColumn adoColumn)
    {
      LJCDataColumn retTableColumn = null;

      if (adoColumn != null)
      {
        retTableColumn = new LJCDataColumn()
        {
          AllowDBNull = adoColumn.AllowDBNull,
          AutoIncrement = adoColumn.AutoIncrement,
          Caption = adoColumn.ColumnName,
          ColumnName = adoColumn.ColumnName,
          DataTypeName = adoColumn.DataType.Name,
          MaxLength = adoColumn.MaxLength,
          PropertyName = adoColumn.ColumnName,
          IsUniqueKey = adoColumn.Unique
        };
      }
      return retTableColumn;
    }
    #endregion
  }
}
