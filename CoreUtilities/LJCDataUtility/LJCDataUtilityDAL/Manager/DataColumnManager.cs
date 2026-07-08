// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumnManager.cs
using LJCDataSiteDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
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
      ResultConverter = new ResultConverter<DataUtilColumn, DataColumns>();
      EntryManager = null;
    }

    // Initializes an object instance.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataColumn", string schemaName = null) : this()
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(DataUtilColumn.ColumnID, caption: "DataColumn ID");

      // Add Calculated and Join columns.
      // Enables adding Calculated and Join columns to a grid configuration.

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataUtilColumn.ColumnID,
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataUtilColumn.ColumnDataTableID,
        DataUtilColumn.ColumnDataTableDbID,
        DataUtilColumn.ColumnName,
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
    ///  path='items/Add/*'/>
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
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Retrieve/*'/>
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
    /// <include file='../../LJCGenDoc/Common/Manager.xml'
    ///  path='members/Update/*'/>
    public void Update(DataUtilColumn dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
      //EntryManager.WriteDataEntry(Manager.SQLStatement);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied value.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/RetrieveWithID/*'/>
    public DataUtilColumn RetrieveWithID(long id, short dbID
      , List<string> propertyNames = null)
    {
      DataUtilColumn retValue;

      var keyColumns = IDKey(id, dbID);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/RetrieveWithUnique/*'/>
    public DataUtilColumn RetrieveWithUnique(long parentID, short parentDbID
      , string name, List<string> propertyNames = null)
    {
      DataUtilColumn retValue;

      var keyColumns = UniqueKey(parentID, parentDbID, name);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/IDKey/*'/>
    public DbColumns IDKey(long id, short dbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilColumn.ColumnID, id },
        { DataUtilColumn.ColumnDbID, dbID},
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ParentKey/*'/>
    public DbColumns ParentKey(long parentID, short parentDbID)
    {
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataUtilColumn.ColumnDataTableID, parentID },
        { DataUtilColumn.ColumnDataTableDbID, parentDbID },
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/UniqueKey/*'/>
    public DbColumns UniqueKey(long parentID, short parentDbID, string name)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataUtilColumn.ColumnDataTableID, parentID },
        { DataUtilColumn.ColumnDataTableDbID, parentDbID },
        { DataUtilColumn.ColumnName, (object)name },
      };
      return retValue;
    }
    #endregion

    #region Properties

    // Gets the affected record count.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/AffectedCount/*'/>
    public int AffectedCount
    {
      get => Manager.AffectedCount;
    }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/Manager/*'/>
    public DataManager Manager { get; set; }

    // Gets or sets the ResultConverter reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/ResultConverter/*'/>
    public ResultConverter<DataUtilColumn, DataColumns> ResultConverter { get; set; }

    // Gets or sets the DataManager reference.
    /// <include file='Doc/DataColumnManager.xml'
    ///  path='members/EntryManager/*'/>
    private DataEntryManager EntryManager { get; set; }
    #endregion
  }
}
