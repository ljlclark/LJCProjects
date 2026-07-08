// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModuleManager.cs
using LJCDataSiteDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
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
      ResultConverter = new ResultConverter<DataModule, DataModules>();
      EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataModuleManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataModule", string schemaName = null) : this()
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataModule.ColumnID, caption: "DataModule ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataModule.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataModule.ColumnName
      });

      var values = ValuesDataUtility.Instance;
      var ManagersDataSite = values.SiteManagers;
      EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Manager Methods

    // Creates a set of columns that match the supplied list.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Columns/*'/>
    public DbColumns Columns(List<string> propertyNames)
    {
      return Manager.DataDefinition.LJCGetColumns(propertyNames);
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Add/*'/>
    public DataModule Add(DataModule dataObject
      , List<string> propertyNames = null)
    {
      DataModule retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
        //EntryManager.WriteDataEntry(Manager.SQLStatement);
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Delete/*'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Retrieves a collection of data records.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Load/*'/>
    public DataModules Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataModules retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataModule Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataModule retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataModule dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/RetrieveWithID/*'/>
    public DataModule RetrieveWithID(long id, short dbID
      , List<string> propertyNames = null)
    {
      DataModule retValue;

      var keyColumns = IDKey(id, dbID);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/RetrieveWithUnique/*'/>
    public DataModule RetrieveWithUnique(string name
      , List<string> propertyNames = null)
    {
      DataModule retValue;

      var keyColumns = UniqueKey(name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/IDKey/*'/>
    public DbColumns IDKey(long id, short dbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataModule.ColumnID, id },
        { DataModule.ColumnDataSiteID, dbID },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='items/UniqueKey/*'/>
    public DbColumns UniqueKey(string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataModule.ColumnName, (object)name },
      };
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get => Manager.AffectedCount;
    }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/Manager/*'/>
    public DataManager Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/ResultConverter/*'/>
    public ResultConverter<DataModule, DataModules> ResultConverter { get; set; }

    // Gets or sets the EntryManager reference.
    /// <include file='Doc/DataModuleManager.xml'
    ///  path='members/EntryManager/*'/>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
