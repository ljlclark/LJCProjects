// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKeyManager.cs
using LJCDataSiteDAL;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataKeyManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataKeyManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataKey", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataKey, DataKeys>();

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

    #region Data Methods

    // Adds a Data Record to the database.
    /// <include path='items/Add/*' file='Doc/DataKeyManager.xml'/>
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
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
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
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DataKey dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      EntryManager.WriteDataEntry(Manager.SQLStatement);
    }

    // Creates a set of columns that match the supplied list.
    /// <include path='items/Columns/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns Columns(List<string> propertyNames)
    {
      return Manager.DataDefinition.LJCGetColumns(propertyNames);
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
    public DataKey RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = IDKey(id);
      var joins = GetJoins();
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataKey RetrieveWithUnique(long parentID, string name
      , List<string> propertyNames = null)
    {
      var joins = GetJoins();
      var keyColumns = UniqueKey(parentID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied values.
    /// <include path='items/RetrieveWithType/*' file='Doc/DataKeyManager.xml'/>
    public DataKey RetrieveWithType(long parentID, short keyType
      , List<string> propertyNames = null)
    {
      var joins = GetJoins();
      var keyColumns = TypeKey(parentID, keyType);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames
        , joins: joins);
      var retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/IDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns IDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataKey.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/ParentKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentKey(long parentID)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID }
      };
      return retValue;
    }

    // Gets the Unique key columns.
    /// <include path='items/UniqueKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(long parentID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID },
        { DataKey.ColumnName, (object)name }
      };
      return retValue;
    }

    // Gets the Type key columns.
    /// <include path='items/TypeKey/*' file='Doc/DataKeyManager.xml'/>
    public DbColumns TypeKey(long parentID, int keyType)
    {
      var retValue = new DbColumns()
      {
        { DataKey.ColumnDataTableID, parentID },
        { DataKey.ColumnKeyType, keyType }
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

    /// <summary>Gets the affected record count.</summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DataKey, DataKeys> ResultConverter { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
