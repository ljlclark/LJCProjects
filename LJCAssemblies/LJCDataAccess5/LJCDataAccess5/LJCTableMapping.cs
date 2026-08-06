// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableMapping.cs
using System.Data.Common;

namespace LJCDataAccess5
{
  /// <summary>
  /// Implements a helper class for creating table mappings.
  /// </summary>
  public class LJCTableMapping
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCTableMapping()
    {
      //TableMaps = new DataTableMappingCollection();
      TableMaps = [];
    }
    #endregion

    #region Methods

    // Adds a DataTable column map to the table mapping.
    /// <include file='Doc/TableMapping.xml'
    ///  path='items/AddColumnMap/*'/>
    public DataColumnMapping? AddColumnMap(string dataSetTable, string sourceColumn
      , string dataSetColumn)
    {
      DataTableMapping tableMap;
      DataColumnMapping? retValue = null;

      tableMap = TableMaps.GetByDataSetTable(dataSetTable);
      if (tableMap != null)
      {
        retValue = tableMap.ColumnMappings.Add(sourceColumn, dataSetColumn);
      }
      return retValue;
    }

    // Adds a DataTable map to the TableMaps collection.
    /// <include file='Doc/TableMapping.xml'
    ///  path='items/AddTableMap/*'/>
    public DataTableMapping AddTableMap(string dataSetTable)
    {
      string sourceTable;
      DataTableMapping retValue;

      sourceTable = "Table";
      if (TableMaps.Count > 0)
      {
        sourceTable = $"Table{TableMaps.Count}";
      }
      retValue = TableMaps.Add(sourceTable, dataSetTable);
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets a reference to the TableMaps object.</summary>
    public DataTableMappingCollection TableMaps { get; private set; }
    #endregion
  }
}
