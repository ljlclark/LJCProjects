// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTableManager.cs
using LJCDataSiteDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  // Provides table specific data methods.
  /// <include file='Doc/DataTableManager.xml'
  ///  path='members/DataTableManager/*'/>
  public class DataTableManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataTableManager()
    {
      Manager = null;
      ResultConverter = new ResultConverter<DataUtilTable, DataTables>();
      EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataTableManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataTable", string schemaName = null) : this()
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataUtilTable.ColumnID, caption: "DataTable ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.
      var propertyName = "ModuleName";
      Manager.DataDefinition.Add(DataModule.ColumnName
        , propertyName, propertyName, caption: "Module Name");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataUtilTable.ColumnID,
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataUtilTable.ColumnDataModuleID,
        DataUtilTable.ColumnDataModuleDbID,
        DataUtilTable.ColumnName,
      });

      var values = ValuesDataUtility.Instance;
      var ManagersDataSite = values.SiteManagers;
      EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Manager Methods

    // Creates a collection of columns that match the supplied list.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Columns/*'/>
    public DbColumns Columns(List<string> propertyNames = null)
    {
      var retColumns = Manager.DataDefinition;
      if (NetCommon.HasItems(propertyNames))
      {
        retColumns = Manager.DataDefinition.LJCGetColumns(propertyNames);
      }
      return retColumns;
    }

    // Creates a list of BaseDefinition property names.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/PropertyNames/*'/>
    public List<string> PropertyNames()
    {
      return Manager.GetPropertyNames();
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Add/*'/>
    public DataUtilTable Add(DataUtilTable dataObject
      , List<string> propertyNames = null)
    {
      DataUtilTable retValue;

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
    public DataTables Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataTables retValue;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
    public DataUtilTable Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataUtilTable retValue;

      if (null == joins)
      {
        joins = GetJoins();
      }
      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataUtilTable dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied key values.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/RetrieveWithID/*'/>
    public DataUtilTable RetrieveWithID(long id, short dbID
      , List<string> propertyNames = null)
    {
      DataUtilTable retValue;

      var keyColumns = IDKey(id, dbID);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/RetrieveWithUnique/*'/>
    public DataUtilTable RetrieveWithUnique(long parentID, short parentDbID
      , string name, List<string> propertyNames = null)
    {
      DataUtilTable retValue;

      var keyColumns = UniqueKey(parentID, parentDbID, name);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region Get Key Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/IDKey/*'/>
    public DbColumns IDKey(long id, short dbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnID, id },
        { DataUtilTable.ColumnDbID, dbID},
      };
      return retValue;
    }

    // Gets the Parent ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ParentKey/*'/>
    public DbColumns ParentKey(long parentID, short parentDbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnDataModuleID, parentID },
        { DataUtilTable.ColumnDataModuleDbID, parentDbID },
      };
      return retValue;
    }

    // Gets the Unique ID key columns.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/UniqueKey/*'/>
    public DbColumns UniqueKey(long parentID, short parentDbID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnDataModuleID, parentID },
        { DataUtilTable.ColumnDataModuleDbID, parentDbID },
        { DataUtilTable.ColumnName, (object)name },
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/GetLoadJoins/*'/>
    public DbJoins GetJoins()
    {
      DbJoins retValue = new DbJoins();

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
      // on ((DataTable.ModuleID = Module.ID))

      DbJoin dbJoin;
      dbJoin = new DbJoin
      {
        TableName = "DataModule",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { DataUtilTable.ColumnDataModuleID, DataModule.ColumnID }
        },
        Columns = new DbColumns()
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
      get => Manager.AffectedCount;
    }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/Manager/*'/>
    public DataManager Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/ResultConverter/*'/>
    public ResultConverter<DataUtilTable, DataTables> ResultConverter { get; set; }

    // Gets or sets the EntryManager reference.
    /// <include file='Doc/DataTableManager.xml'
    ///  path='members/EntryManager/*'/>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
