// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTableManager.cs
using LJCDataSiteDAL;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataTableManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataTableManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataTable", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataUtilTable, DataTables>();

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
        DataUtilTable.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataUtilTable.ColumnDataModuleID,
        DataUtilTable.ColumnName
      });

      var values = ValuesDataUtility.Instance;
      var ManagersDataSite = values.SiteManagers;
      EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataUtilTable Add(DataUtilTable dataObject
      , List<string> propertyNames = null)
    {
      DataUtilTable retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
        EntryManager.WriteDataEntry(Manager.SQLStatement);
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
      EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCGenDoc/Common/Manager.xml'/>
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
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
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
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DataUtilTable dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Creates a collection of columns that match the supplied list.
    /// <include path='items/Columns/*' file='../../LJCGenDoc/Common/Manager.xml'/>
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
    /// <include path='items/PropertyNames/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public List<string> PropertyNames()
    {
      return Manager.GetPropertyNames();
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataUtilTable RetrieveWithID(long id
      , List<string> propertyNames = null)
    {
      DataUtilTable retValue;

      var keyColumns = IDKey(id);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataUtilTable RetrieveWithUnique(int parentID, string name
      , List<string> propertyNames = null)
    {
      DataUtilTable retValue;

      var keyColumns = UniqueKey(parentID, name);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns IDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/ParentKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentKey(int parentID)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnDataModuleID, parentID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/UniqueKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(int parentID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataUtilTable.ColumnDataModuleID, parentID },
        { DataUtilTable.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    /// <include path='items/GetLoadJoins/*' file='../../LJCGenDoc/Common/Manager.xml'/>
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

    /// <summary>Gets the affected record count.</summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DataUtilTable, DataTables> ResultConverter { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
