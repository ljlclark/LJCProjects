// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntrySiteManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataEntrySiteManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntrySiteManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataEntrySite", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataEntrySite, DataEntrySites>();
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntrySite Add(DataEntrySite dataObject
      , List<string> propertyNames = null)
    {
      DataEntrySite retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntrySites Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataEntrySites retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntrySite Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataEntrySite retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DataEntrySite dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied values.
    /// <include path='items/RetrieveWithPrimary/*' file='Doc/DataEntrySiteManager.xml'/>
    public DataEntrySite RetrieveWithPrimary(long dataEntryID
      , long dataEntrySiteID, long dataSiteID
      , List<string> propertyNames = null)
    {
      DataEntrySite retValue;

      var keyColumns = PrimaryKey(dataEntryID, dataEntrySiteID, dataSiteID);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the primary key columns.
    /// <include path='items/PrimaryKey/*' file='Doc/DataEntrySiteManager.xml'/>
    public DbColumns PrimaryKey(long dataEntryID, long dataEntrySiteID
      , long dataSiteID)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { DataEntrySite.ColumnDataEntryID, dataEntryID },
        { DataEntrySite.ColumnDataEntrySiteID, dataEntrySiteID },
        { DataEntrySite.ColumnDataSiteID, dataSiteID }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DataEntrySite, DataEntrySites> ResultConverter { get; set; }
    #endregion
  }
}
