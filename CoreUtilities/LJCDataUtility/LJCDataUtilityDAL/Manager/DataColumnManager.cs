// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumnManager.cs
using LJCDataSiteDAL;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataColumnManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataColumn", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataUtilColumn, DataColumns>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataUtilColumn.ColumnID, caption: "DataColumn ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataUtilColumn.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataUtilColumn.ColumnDataTableID,
        DataUtilColumn.ColumnName
      });

      var values = ValuesDataUtility.Instance;
      var ManagersDataSite = values.SiteManagers;
      EntryManager = ManagersDataSite.DataEntryManager;
    }
    #endregion

    #region Data Methods

    // Adds a Data Record to the database.
    /// <summary>
    /// Adds a Data Record to the database.
    /// </summary>
    /// <param name="dataObject">The data record.</param>
    /// <param name="propertyNames">The included property names.</param>
    /// <param name="includeNull"></param>
    /// <returns>The Data Object with the DB assigned key values.</returns>
    public DataUtilColumn Add(DataUtilColumn dataObject
      , List<string> propertyNames = null, bool includeNull = false)
    {
      DataUtilColumn retValue;

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
    public DataColumns Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataColumns retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataUtilColumn Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataUtilColumn retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DataUtilColumn dataObject, DbColumns keyColumns
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
    public DataUtilColumn RetrieveWithID(long id, List<string> propertyNames = null)
    {
      DataUtilColumn retValue;

      var keyColumns = IDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataUtilColumn RetrieveWithUnique(long parentID, string name
      , List<string> propertyNames = null)
    {
      DataUtilColumn retValue;

      var keyColumns = UniqueKey(parentID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
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
        { DataUtilColumn.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentKey(long parentID)
    {
      var retValue = new DbColumns()
      {
        { DataUtilColumn.ColumnDataTableID, parentID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/UniqueKey/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(long parentID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataUtilColumn.ColumnDataTableID, parentID },
        { DataUtilColumn.ColumnName, (object)name }
      };
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
    public ResultConverter<DataUtilColumn, DataColumns> ResultConverter { get; set; }

    /// <summary>Gets or sets the DataManager reference.</summary>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
