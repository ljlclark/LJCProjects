// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTableColumns5.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  // Contains methods to complement a DataColumnCollection object.
  /// <include file='Doc/TableColumns.xml'
  ///  path='members/LJCTableColumns/*'/>
  public class LJCTableColumns
  {
    #region Static Methods

    // Clones a DataColumn collection.
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/Clone/*'/>
    public static DataColumnCollection? Clone(DataColumnCollection adoColumns)
    {
      DataColumnCollection? retTableColumns = null;

      if (HasColumns(adoColumns))
      {
        retTableColumns = CreateColumns();
        foreach (DataColumn tableColumn in adoColumns)
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
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/ColumnNames/*'/>
    public static List<string>? ColumnNames(DataColumnCollection adoColumns)
    {
      List<string>? retValue = null;

      if (HasColumns(adoColumns))
      {
        //retValue = new List<string>();
        retValue = [];
        foreach (DataColumn tableColumn in adoColumns)
        {
          retValue.Add(tableColumn.ColumnName);
        }
      }
      return retValue;
    }

    // Returns a set of DataColumns that match the supplied list.
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/Columns/*'/>
    public static DataColumnCollection? Columns(DataColumnCollection adoColumns
      , List<string>? columnNames = null)
    {
      DataColumnCollection? retTableColumns = null;

      if (HasColumns(adoColumns))
      {
        if (!LJC.HasListItems(columnNames))
        {
          retTableColumns = Clone(adoColumns);
        }
        else
        {
          // Create columns from names.
          retTableColumns = CreateColumns();
          foreach (string columnName in columnNames)
          {
            DataColumn? tableColumn = adoColumns[columnName];
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
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/CreateColumns/*'/>
    public static DataColumnCollection CreateColumns()
    {
      DataColumnCollection retTableColumns;

      var workTable = new DataTable();
      retTableColumns = workTable.Columns;
      return retTableColumns;
    }

    // Checks the DataColumnCollection object for items.
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/HasColumns/*'/>
    // Note: Also in LJCDBMessage.DbResult
    public static bool HasColumns(DataColumnCollection adoColumns)
    {
      bool retValue = false;

      if (LJC.HasTableColumns(adoColumns))
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates an LJCDataColumns collection from a DataColumnCollection.
    /// <include file='Doc/TableColumns.xml'
    ///  path='members/ToDataColumns/*'/>
    // Note: Also in LJCDBMessage.DbResult
    public static LJCDataColumns? ToDataColumns(DataColumnCollection adoColumns)
    {
      LJCDataColumns? retDataColumns = null;

      if (HasColumns(adoColumns))
      {
        retDataColumns = [];
        foreach (DataColumn tableColumn in adoColumns)
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
