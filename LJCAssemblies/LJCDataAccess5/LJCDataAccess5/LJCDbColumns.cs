// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDbColumns.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  public class LJCDbColumns
  {
    #region Static Methods

    // Clones a DataColumn collection.
    /// <include path='items/DataColumnsClone/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection? Clone(DataColumnCollection dbColumns)
    {
      DataColumnCollection? retDbColumns = null;

      if (HasColumns(dbColumns))
      {
        retDbColumns = CreateColumns();
        foreach (DataColumn dbColumn in dbColumns)
        {
          var dbColumnClone = LJCDbColumn.Clone(dbColumn);
          if (dbColumnClone != null)
          {
            retDbColumns.Add(dbColumnClone);
          }
        }
      }
      return retDbColumns;
    }

    // Creates a PropertyNames list from a DataColumns collection.
    /// <include path='items/GetPropertyNames/*' file='Doc/TableData.xml'/>
    public static List<string>? ColumnNames(DataColumnCollection dbColumns)
    {
      List<string>? retValue = null;

      if (HasColumns(dbColumns))
      {
        //retValue = new List<string>();
        retValue = [];
        foreach (DataColumn dbColumn in dbColumns)
        {
          retValue.Add(dbColumn.ColumnName);
        }
      }
      return retValue;
    }

    // Returns a set of DataColumns that match the supplied list.
    /// <include path='items/GetDataColumns/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection? Columns(DataColumnCollection dbColumns
      , List<string>? columnNames = null)
    {
      DataColumnCollection? retDbColumns = null;

      if (HasColumns(dbColumns))
      {
        if (!LJC.HasItems(columnNames))
        {
          retDbColumns = Clone(dbColumns);
        }
        else
        {
          // Create columns from names.
          retDbColumns = CreateColumns();
          foreach (string columnName in columnNames)
          {
            DataColumn? dbColumn = dbColumns[columnName];
            if (dbColumn != null)
            {
              var dbColumnClone = LJCDbColumn.Clone(dbColumn);
              if (dbColumnClone != null)
              {
                retDbColumns.Add(dbColumnClone);
              }
            }
          }
        }
      }
      return retDbColumns;
    }

    // Creates a new DataColumns object.
    /// <include path='items/CreateDataColumns/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection CreateColumns()
    {
      DataColumnCollection retDbColumns;

      var workTable = new DataTable();
      retDbColumns = workTable.Columns;
      return retDbColumns;
    }

    /// <summary>
    /// Checks the DataColumnCollection object for items.
    /// </summary>
    /// <param name="dataColumns">The DataColumnCollection reference.</param>
    /// <returns>true if there are items; otherwise false.</returns>
    // Note: Also in LJCDBMessage.DbResult
    public static bool HasColumns(DataColumnCollection dbColumns)
    {
      bool retValue = false;

      if (LJC.HasColumns(dbColumns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates an LJCDataColumns collection from a DataColumnCollection.
    /// <summary>
    /// Creates an LJCDataColumns collection from a DataColumnCollection.
    /// </summary>
    /// <param name="dataColumns"></param>
    /// <returns></returns>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumns? ToDataColumns(DataColumnCollection dbColumns)
    {
      LJCDataColumns? retColumns = null;

      if (HasColumns(dbColumns))
      {
        //retColumns = new LJCDataColumns();
        retColumns = [];
        foreach (DataColumn dbColumn in dbColumns)
        {
          var newDbColumn = LJCDbColumn.ToDataColumn(dbColumn);
          if (newDbColumn != null)
          {
            retColumns.Add(newDbColumn);
          }
        }
      }
      return retColumns;
    }
    #endregion
  }
}
