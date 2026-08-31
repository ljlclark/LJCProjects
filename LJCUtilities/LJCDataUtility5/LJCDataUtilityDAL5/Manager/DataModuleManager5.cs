// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModuleManager5.cs
using LJCDBClientLib5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // Provides table specific data methods.
  /// <include file='Doc/DataModuleManager.xml'
  ///  path='members/DataModuleManager/*'/>
  public class DataModuleManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataModuleManager()
    {
      Manager = null;
      ResultConverter = new LJCResultConverter<DataModule, DataModules>();
      //EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataModuleManager(LJCDbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataModule", string? schemaName = null) : this()
    {
      Manager = new LJCDataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataModule.ColumnId, caption: "DataModule ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(
      [
        DataModule.ColumnId
      ]);

      // Create the list of lookup column names.
      Manager.SetLookupColumns(
      [
        DataModule.ColumnName
      ]);

      var values = ValuesDataUtility.Instance;
      //var ManagersDataSite = values.SiteManagers;
      //DbId = ManagersDataSite.DbGroupManager.DbId;
      DbId = values.DbGroupId;
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
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Add/*'/>
    public DataModule? Add(DataModule dataObject
      , List<string>? propertyNames = null)
    {
      DataModule? retValue = null;

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
    public DataModules? Load(LJCDataColumns? keyColumns = null
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      DataModules? retValue = null;

      var dbResult = Manager?.Load(keyColumns, propertyNames, filters);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateCollection(dbResult);
      }
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataModule? Retrieve(LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      DataModule? retValue = null;

      var dbResult = Manager?.Retrieve(keyColumns, propertyNames, filters);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Updates the record.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataModule dataObject, LJCDataColumns keyColumns
      , List<string>? propertyNames = null, LJCDBFilters? filters = null)
    {
      Manager?.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/RetrieveWithId/*'/>
    public DataModule? RetrieveWithId(short dbId, long id
      , List<string>? propertyNames = null)
    {
      DataModule? retValue = null;

      var keyColumns = IdKey(dbId, id);
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/RetrieveUnique/*'/>
    public DataModule? RetrieveUnique(string name
      , List<string>? propertyNames = null)
    {
      DataModule? retValue = null;

      var keyColumns = UniqueKey(name);
      var dbResult = Manager?.Retrieve(keyColumns, propertyNames);
      if (dbResult != null)
      {
        retValue = ResultConverter.CreateData(dbResult);
      }
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/IdKey/*'/>
    public static LJCDataColumns IdKey(short dbId, long id)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new LJCDataColumns()
      {
        { DataModule.ColumnDbId, dbId },
        { DataModule.ColumnId, id },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/UniqueKey/*'/>
    public static LJCDataColumns UniqueKey(string name)
    {
      var retValue = new LJCDataColumns()
      {
        { DataModule.ColumnName, name },
      };
      return retValue;
    }

    // Check for duplicate unique key.
    /// <include file='../../LJCDocLib/Common/Manager.xml'
    ///  path='items/IsDuplicate/*'/>
    public bool IsDuplicate(DataModule lookupRecord, DataModule currentRecord
      , bool isUpdate = false)
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

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataModuleManager.xml'
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
      /// <include file='Doc/DataModuleManager.xml'
      ///  path='members/DbID/*'/>
    public short DbId { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/Manager/*'/>
    public LJCDataManager? Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/ResultConverter/*'/>
    public LJCResultConverter<DataModule, DataModules> ResultConverter { get; set; }

    // Gets or sets the EntryManager reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/EntryManager/*'/>
    //private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
