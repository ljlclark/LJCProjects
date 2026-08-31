// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTableManager5.cs
using LJCDBClientLib5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // Provides table specific data methods.
  /// <include file='Doc/DataTableManager.xml'
  ///  path='members/DataTableManager/*'/>
  public class DataTableManager
  {
    #region Static Methods

    // Check for duplicate unique key.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='items/IsDuplicate/*'/>
    public static bool IsDuplicate(DataUtilTable lookupRecord
      , DataUtilTable currentRecord, bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (!isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          // If not the current record.
          if (lookupRecord.DbId != currentRecord.DbId
            && lookupRecord.Id != currentRecord.Id)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataTableManager()
    {
      Manager = null;
      ResultConverter = new LJCResultConverter<DataUtilTable, DataTables>();
      //EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataTableManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataTable", string? schemaName = null) : this()
    {
      Manager = new LJCDataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataUtilTable.ColumnId, caption: "DataTable ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      var propertyName = "ModuleName";
      Manager.DataDefinition.Add(DataModule.ColumnName
        , propertyName, propertyName, caption: "Module Name");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(
      [
        DataUtilTable.ColumnId,
      ]);

      // Create the list of lookup column names.
      Manager.SetLookupColumns(
      [
        DataUtilTable.ColumnDataModuleId,
        DataUtilTable.ColumnDataModuleDbId,
        DataUtilTable.ColumnName,
      ]);

      var values = ValuesDataUtility.Instance;
      //var ManagersDataSite = values.SiteManagers;
      //DbId = ManagersDataSite.DbGroupManager.DbId;
      DbId = values.DbGroupId;
      //EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Manager Methods

    // Creates a collection of columns that match the supplied list.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Columns/*'/>
    public LJCDataColumns? Columns(List<string>? propertyNames = null)
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
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/PropertyNames/*'/>
    public List<string>? PropertyNames()
    {
      return Manager?.GetPropertyNames();
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Add/*'/>
    public DataUtilTable? Add(DataUtilTable dataObject
      , List<string>? propertyNames = null)
    {
      DataUtilTable? retValue = null;

      var dbResult = Manager?.Add(dataObject, propertyNames);
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
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Delete/*'/>
    public void Delete(LJCDataColumns keyColumns, LJCDBFilters? filters = null)
    {
      Manager?.Delete(keyColumns, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Retrieves a collection of data records.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Load/*'/>
    public DataTables? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataTables? retValue = null;

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
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataUtilTable? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      DataUtilTable? retValue = null;

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
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataUtilTable dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      Manager?.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied key values.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/RetrieveWithId/*'/>
    public DataUtilTable? RetrieveWithId(short dbId, long id
      , List<string>? propertyNames = null)
    {
      DataUtilTable? retValue = null;

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
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/RetrieveUnique/*'/>
    public DataUtilTable? RetrieveUnique(short parentDbId, long parentId
      , string name, List<string>? propertyNames = null)
    {
      DataUtilTable? retValue = null;

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

    #region Get Key Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/IdKey/*'/>
    public static LJCDataColumns IdKey(short dbId, long id)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataUtilTable.ColumnDbId, dbId},
        { DataUtilTable.ColumnId, id },
      };
      return retValue;
    }

    // Gets the Parent ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ParentKey/*'/>
    public static LJCDataColumns ParentKey(short parentDbId, long parentId)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataUtilTable.ColumnDataModuleDbId, parentDbId },
        { DataUtilTable.ColumnDataModuleId, parentId },
      };
      return retValue;
    }

    // Gets the Unique ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/UniqueKey/*'/>
    public static LJCDataColumns UniqueKey(short parentDbId, long parentId
      , string name)
    {
      var retValue = new LJCDataColumns()
      {
        { DataUtilTable.ColumnDataModuleDbId, parentDbId },
        { DataUtilTable.ColumnDataModuleId, parentId },
        { DataUtilTable.ColumnName, name },
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include file='../../LJCGenDoc5/Common/Manager.xml'
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
        TableName = "DataModule",
        JoinType = "left",
        JoinOns = new LJCDBJoinOns()
        {
          { DataUtilTable.ColumnDataModuleId, DataModule.ColumnId }
        },
        Columns = new LJCDataColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DataModule.ColumnName, "ModuleName", "ModuleName" }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataTableManager.xml'
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
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/DbId/*'/>
    public short DbId { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/Manager/*'/>
    public LJCDataManager? Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ResultConverter/*'/>
    public LJCResultConverter<DataUtilTable, DataTables> ResultConverter { get; set; }

    //// Gets or sets the EntryManager reference.
    ///// <include file='Doc/DataTableManager.xml'
    /////  path='members/EntryManager/*'/>
    //private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
