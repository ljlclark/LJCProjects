using LJCNetCommon5;
using System.Data;

namespace LJCDataAccess5
{
  public class LJCDbColumns
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

    // Clones a DataColumn collection.
    /// <include path='items/DataColumnsClone/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection? Clone(DataTable dataTable)
    {
      DataColumnCollection? retDbColumns = null;

      ArgumentDataTable(dataTable);

      if (LJC.HasColumns(dataTable))
      {
        retDbColumns = CreateColumns();
        foreach (DataColumn dbColumn in dataTable.Columns)
        {
          var dbColumnClone = Clone(dbColumn);
          if (dbColumnClone != null)
          {
            retDbColumns.Add(dbColumnClone);
          }
        }
      }
      return retDbColumns;
    }

    // Returns a set of DataColumns that match the supplied list.
    /// <include path='items/GetDataColumns/*' file='Doc/TableData.xml'/>
    public static DataColumnCollection? Columns(DataColumnCollection dbColumns
      , List<string> columnNames)
    {
      DataColumnCollection? retDbColumns = null;

      if (HasColumns(dbColumns)
        && LJC.HasItems(columnNames))
      {
        // Create columns from names.
        var workTable = new DataTable();
        foreach (string columnName in columnNames)
        {
          DataColumn? dbColumn = dbColumns[columnName];
          if (dbColumn != null)
          {
            var dbColumnClone = Clone(dbColumn);
            if (dbColumnClone != null)
            {
              workTable.Columns.Add(dbColumnClone);
            }
          }
        }
        retDbColumns = workTable.Columns;
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

    // Creates a PropertyNames list from a DataColumns collection.
    /// <include path='items/GetPropertyNames/*' file='Doc/TableData.xml'/>
    public static List<string>? PropertyNames(DataColumnCollection dbColumns)
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

    // Creates a DbColumns collection from a DataColumns collection.
    /// <summary>
    /// Creates a DbColumns collection from a DataColumns collection.
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
          var newDbColumn = ToDataColumn(dbColumn);
          if (newDbColumn != null)
          {
            retColumns.Add(newDbColumn);
          }
        }
      }
      return retColumns;
    }

    // Checks the DataTable argument.
    private static bool ArgumentDataTable(DataTable dataTable)
    {
      if (null == dataTable)
      {
        var message = "Missing argument dataTable.";
        throw new ArgumentNullException(message);
      }
      return true;
    }
    #endregion
  }
}
