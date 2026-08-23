// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumnManager5.cs
using LJCDBClientLib5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // Provides table specific data methods.
  /// <include file='Doc/DataColumnManager.xml'
  ///  path='members/DataColumnManager/*'/>
  public class DataColumnManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataColumnManager()
    {
      Manager = null;
      ResultConverter = new LJCResultConverter<DataUtilColumn, DataColumns>();
      //EntryManager = null;
    }

    // Initializes an object instance.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataColumnManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataColumn", string? schemaName = null) : this()
    {
      Manager = new LJCDataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataUtilColumn.ColumnId, caption: "DataColumn ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(
      [
        DataUtilColumn.ColumnId,
      ]);

      // Create the list of lookup column names.
      Manager.SetLookupColumns(
      [
        DataUtilColumn.ColumnDataTableId,
        DataUtilColumn.ColumnDataTableDbId,
        DataUtilColumn.ColumnName,
      ]);

      var values = ValuesDataUtility.Instance;
      //var ManagersDataSite = values.SiteManagers;
      //DbId = ManagersDataSite.DbGroupManager.DbId;
      DbId = 1;  // Testing
      //EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Manager Methods

    // Creates a set of columns that match the supplied list.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Columns/*'/>
    public LJCDataColumns? Columns(List<string> propertyNames)
    {
      var retColumns = Manager?.DataDefinition;
      if (retColumns != null
        && LJC.HasListItems(propertyNames))
      {
        var dataColumns = Manager?.DataDefinition;
        retColumns = dataColumns?.LJCColumns(propertyNames);
      }
      return retColumns;
    }

    // Creates a list of BaseDefinition property names.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/PropertyNames/*'/>
    public List<string>? PropertyNames()
    {
      return Manager?.GetPropertyNames();
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/Add/*'/>
    public DataUtilColumn? Add(DataUtilColumn dataObject
      , List<string>? propertyNames = null, bool includeNull = false)
    {
      DataUtilColumn? retValue = null;

      var dbResult = Manager?.Add(dataObject, propertyNames
        , includeNull);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
        if (retValue != null)
        {
          dataObject.Id = retValue.Id;
          //EntryManager.WriteDataEntry(Manager.SQLStatement);
        }
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Delete/*'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      Manager?.Delete(keyColumns, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Retrieves a collection of data records.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Load/*'/>
    public DataColumns? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataColumns? retValue = null;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager?.Load(keyColumns, propertyNames, filters, joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataUtilColumn? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataUtilColumn? retValue = null;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames, filters
        , joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Updates the record.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataUtilColumn dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      Manager?.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/RetrieveWithId/*'/>
    public DataUtilColumn? RetrieveWithId(short dbId, long id
      , List<string>? propertyNames = null)
    {
      DataUtilColumn? retValue = null;

      var keyColumns = IdKey(dbId, id);
      var joins = GetJoins();
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames
        , joins: joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/RetrieveUnique/*'/>
    public DataUtilColumn? RetrieveUnique(short parentDbId, long parentId
      , string name, List<string>? propertyNames = null)
    {
      DataUtilColumn? retValue = null;

      var keyColumns = UniqueKey(parentDbId, parentId, name);
      var joins = GetJoins();
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames
        , joins: joins);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/IdKey/*'/>
    public static LJCDataColumns IdKey(short dbId, long id)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataUtilColumn.ColumnDbId, dbId},
        { DataUtilColumn.ColumnId, id },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ParentKey/*'/>
    public static LJCDataColumns ParentKey(short parentDbId, long parentId)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataUtilColumn.ColumnDataTableDbId, parentDbId },
        { DataUtilColumn.ColumnDataTableId, parentId },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/UniqueKey/*'/>
    public static LJCDataColumns UniqueKey(short parentDbId, long parentId, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new LJCDataColumns()
      {
        { DataUtilColumn.ColumnDataTableDbId, parentDbId },
        { DataUtilColumn.ColumnDataTableId, parentId },
        { DataUtilColumn.ColumnName, (object)name },
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/GetJoins/*'/>
    public static LJCDBJoins GetJoins()
    {
      LJCDBJoins retValue = [];

      // Note: JoinOn Columns must have properties in the DataObject
      // to receive the join values.
      // The RenameAs property is required if there is another table column
      // with the same name.
      // dbColumns.Add(string columnName, string propertyName = null
      //   , string renameAs = null, string dataTypeName = "String"
      //   , string caption = null) 

      // Example SQL additions
      // DataModule.Name
      //left join DataModule
      // on ((DataTable.ModuleId = Module.Id))

      LJCDBJoin dbJoin;
      dbJoin = new LJCDBJoin
      {
        TableName = "DataUtilTable",
        JoinType = "left",
        JoinOns = new LJCDBJoinOns()
        {
          { DataUtilColumn.ColumnDataTableId, DataUtilTable.ColumnId }
        },
        Columns = new LJCDataColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DataUtilTable.ColumnName, "TableName", "TableName" }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get
      {
        var retValue = 0;
        if (Manager != null)
        {
          retValue = Manager.AffectedCount;
        }
        return retValue;
      }
    }

    // Gets or sets the Database ID.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/DbId/*'/>
    public short DbId { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/Manager/*'/>
    public LJCDataManager? Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ResultConverter/*'/>
    public LJCResultConverter<DataUtilColumn, DataColumns> ResultConverter { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/EntryManager/*'/>
    //private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
