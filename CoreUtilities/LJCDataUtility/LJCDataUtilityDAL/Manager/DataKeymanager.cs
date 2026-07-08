// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeyManager.cs
using LJCDataSiteDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  // Provides table specific data methods.
  /// <include file='Doc/DataKeyManager.xml'
  ///  path='members/DataKeyManager/*'/>
  public class DataKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Constructor/*'/>
    public DataKeyManager()
    {
      Manager = null;
      ResultConverter = new ResultConverter<DataKey, DataKeys>();
      EntryManager = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataKey", string schemaName = null) : this()
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataKey.ColumnID, caption: "DataKey ID");
      //Manager.MapNames(DataKey.ColumnSourceColumnName
      //  , DataKey.PropertySourceColumnNames, caption: "Columns");
      //Manager.MapNames(DataKey.ColumnTargetColumnName
      //  , DataKey.PropertyTargetColumnNames, caption: "Target Columns");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataKey.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataKey.ColumnDataTableID,
        DataKey.ColumnName
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
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/Add/*'/>
    public DataKey Add(DataKey dataObject
      , List<string> propertyNames = null, bool includeNull = false)
    {
      DataKey retValue;

      var dbResult = Manager.Add(dataObject, propertyNames
        , includeNull);
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
    public DataKeys Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataKeys retValue;

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
    public DataKey Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataKey retValue;

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
    public void Update(DataKey dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/LoadWithForeign/*'/>
    public DataKeys LoadWithForeign(string tableName
      , List<string> propertyNames = null)
    {
      var keyColumns = ForeignKey(tableName);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/LoadWithParent/*'/>
    public DataKeys LoadWithParent(long parentID, short parentDbID
      , List<string> propertyNames = null)
    {
      var keyColumns = ParentKey(parentID, parentDbID);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/LoadWithParentType/*'/>
    public DataKeys LoadWithParentType(long parentID, short parentDbID
      , int keyType, List<string> propertyNames = null)
    {
      var keyColumns = ParentTypeKey(parentID, parentDbID, keyType);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Loads records with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/LoadWithType/*'/>
    public DataKeys LoadWithType(long id, short dbID, short keyType
      , List<string> propertyNames = null)
    {
      var keyColumns = TypeKey(id, dbID, keyType);
      var retKeys = Load(keyColumns, propertyNames);
      return retKeys;
    }

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/RetrieveWithIDs/*'/>
    public DataKey RetrieveWithID(long id, short dbID
      , List<string> propertyNames = null)
    {
      var keyColumns = IDKeys(id, dbID);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/RetrieveWithParentType/*'/>
    public DataKey RetrieveWithParentType(long parentID, short parentDbID
      , short keyType, List<string> propertyNames = null)
    {
      var keyColumns = ParentTypeKey(parentID, parentDbID, keyType);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='items/RetrieveWithUnique/*'/>
    public DataKey RetrieveWithUnique(long parentID, short parentDbID
      , string name, List<string> propertyNames = null)
    {
      var keyColumns = UniqueKey(parentID, parentDbID, name);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the foreign key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ForeignKey/*'/>
    public DbColumns ForeignKey(string targetTableName)
    {
      var foreignKeyType = 3;
      var retValue = new DbColumns()
      {
        { DataKey.ColumnTargetTableName, (object)targetTableName },
        { DataKey.ColumnKeyType, foreignKeyType },
      };
      return retValue;
    }

    // Gets the primary key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/IDKeys/*'/>
    public DbColumns IDKeys(long id, short dbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataKey.ColumnID, id },
        { DataKey.ColumnDataSiteID, dbID },
      };
      return retValue;
    }

    // Gets the parent key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/ParentKey/*'/>
    public DbColumns ParentKey(long parentID, short parentDbID)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID },
        { DataKey.ColumnDataTableSiteID, parentDbID },
      };
      return retValue;
    }

    // Gets the parent by type key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/ParentTypeKey/*'/>
    public DbColumns ParentTypeKey(long parentID, short parentDbID, int keyType)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID },
        { DataKey.ColumnDataTableSiteID, parentDbID },
        { DataKey.ColumnKeyType, keyType },
      };
      return retValue;
    }

    // Gets the parent by type key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/TypeKey/*'/>
    public DbColumns TypeKey(long id, short dbID, int keyType)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, id },
        { DataKey.ColumnDataTableSiteID, dbID },
        { DataKey.ColumnKeyType, keyType },
      };
      return retValue;
    }

    // Gets the unique key columns.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='items/UniqueKey/*'/>
    public DbColumns UniqueKey(long parentID, short parentDbID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID },
        { DataKey.ColumnDataTableSiteID, parentDbID },
        { DataKey.ColumnName, (object)name },
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
      // DataTable.Name
      //left join DataTable
      // on ((DataKey.TableID = DataTable.ID))

      DbJoin dbJoin;
      dbJoin = new DbJoin
      {
        TableName = "DataTable",
        JoinType = "left",
        JoinOns = new DbJoinOns()
        {
          { DataKey.ColumnDataTableID, DataUtilTable.ColumnID }
        },
        Columns = new DbColumns()
        {
          // columnName, propertyName = null, renameAs = null
          //   , dataTypeName = "String", caption = null
          { DataUtilTable.ColumnName, "DataTableName", "DataTableName" }
        }
      };
      retValue.Add(dbJoin);
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get => Manager.AffectedCount;
    }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/Manager/*'/>
    public DataManager Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/ResultConverter/*'/>
    public ResultConverter<DataKey, DataKeys> ResultConverter { get; set; }

    // Gets or sets the EntryManager reference.
    /// <include file='Doc/DataKeyManager.xml'
    ///  path='members/EntryManager/*'/>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
