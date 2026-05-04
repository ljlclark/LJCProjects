// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTableColumns.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  // Contains methods to complement a DataColumnCollection object.
  public class LJCTableColumns
  {
    #region Static Methods

    // Clones a DataColumn collection.
    /// <include path='items/Clone/*' file='Doc/LJCTableColumns.xml'/>
    public static DataColumnCollection? Clone(DataColumnCollection tableColumns)
    {
      DataColumnCollection? retTableColumns = null;

      if (HasColumns(tableColumns))
      {
        retTableColumns = CreateColumns();
        foreach (DataColumn tableColumn in tableColumns)
        {
          var tableColumnClone = LJCTableColumn.Clone(tableColumn);
          if (tableColumnClone != null)
          {
            retTableColumns.Add(tableColumnClone);
          }
        }
      }
      return retTableColumns;
    }

    // Creates a PropertyNames list from a DataColumns collection.
    /// <include path='items/GetPropertyNames/*' file='Doc/LJCTableColumns.xml'/>
    public static List<string>? ColumnNames(DataColumnCollection tableColumns)
    {
      List<string>? retValue = null;

      if (HasColumns(tableColumns))
      {
        //retValue = new List<string>();
        retValue = [];
        foreach (DataColumn tableColumn in tableColumns)
        {
          retValue.Add(tableColumn.ColumnName);
        }
      }
      return retValue;
    }

    // Returns a set of DataColumns that match the supplied list.
    /// <include path='items/Columns/*' file='Doc/LJCTableColumns.xml'/>
    public static DataColumnCollection? Columns(DataColumnCollection tableColumns
      , List<string>? columnNames = null)
    {
      DataColumnCollection? retTableColumns = null;

      if (HasColumns(tableColumns))
      {
        if (!LJC.HasItems(columnNames))
        {
          retTableColumns = Clone(tableColumns);
        }
        else
        {
          // Create columns from names.
          retTableColumns = CreateColumns();
          foreach (string columnName in columnNames)
          {
            DataColumn? tableColumn = tableColumns[columnName];
            if (tableColumn != null)
            {
              var tableColumnClone = LJCTableColumn.Clone(tableColumn);
              if (tableColumnClone != null)
              {
                retTableColumns.Add(tableColumnClone);
              }
            }
          }
        }
      }
      return retTableColumns;
    }

    // Creates a new DataColumnCollection object.
    /// <include path='items/CreateColumns/*' file='Doc/LJCDbColumns.xml'/>
    public static DataColumnCollection CreateColumns()
    {
      DataColumnCollection retTableColumns;

      var workTable = new DataTable();
      retTableColumns = workTable.Columns;
      return retTableColumns;
    }

    // Checks the DataColumnCollection object for items.
    /// <include path='items/HasColumns/*' file='Doc/LJCDbColumns.xml'/>
    // Note: Also in LJCDBMessage.DbResult
    public static bool HasColumns(DataColumnCollection tableColumns)
    {
      bool retValue = false;

      if (LJC.HasColumns(tableColumns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates an LJCDataColumns collection from a DataColumnCollection.
    /// <include path='items/ToDataColumns/*' file='Doc/LJCDbColumns.xml'/>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumns? ToDataColumns(DataColumnCollection tableColumns)
    {
      LJCDataColumns? retDataColumns = null;

      if (HasColumns(tableColumns))
      {
        retDataColumns = [];
        foreach (DataColumn tableColumn in tableColumns)
        {
          var dataColumn = LJCTableColumn.ToDataColumn(tableColumn);
          if (dataColumn != null)
          {
            retDataColumns.Add(dataColumn);
          }
        }
      }
      return retDataColumns;
    }
    #endregion
  }
}
