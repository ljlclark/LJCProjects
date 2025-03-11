// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntryManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class DataEntryManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntryManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "DataEntry", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<DataEntry, DataEntries>();

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        DataEntry.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        DataEntry.ColumnDataSiteID,
        DataEntry.ColumnEntryTime
      });
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntry Add(DataEntry dataObject
      , List<string> propertyNames = null)
    {
      DataEntry retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
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
    public DataEntries Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataEntries retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntry Retrieve(DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      DataEntry retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public void Update(DataEntry dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../LJCGenDoc/Common/Manager.xml'/>
    public DataEntry RetrieveWithID(long id, List<string> propertyNames = null)
    {
      DataEntry retValue;

      var keyColumns = IDKey(id);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Retrieves a record with the supplied unique values.
    /// <include path='items/RetrieveWithUnique/*' file='Doc/DataEntryManager.xml'/>
    public DataEntry RetrieveWithUnique(long dataSiteID, DateTime entryTime
      , List<string> propertyNames = null)
    {
      DataEntry retValue;

      var keyColumns = UniqueKey(dataSiteID, entryTime);
      var dbResult = Manager.Retrieve(keyColumns, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    /// <summary>Write the DataEntry record.</summary>
    public void WriteDataEntry(string sql)
    {
      var dataEntry = new DataEntry()
      {
        // ToDo: Assign to site.
        DataSiteID = 1,
        EntryTime = DateTime.Now,
        EntryData = sql
      };
      Add(dataEntry);
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
        { DataEntry.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Unique key columns.
    /// <include path='items/UniqueKey/*' file='Doc/DataEntryManager.xml'/>
    public DbColumns UniqueKey(long dataSiteID, DateTime entryTime)
    {
      // Needs cast for string to select the correct Add overload.
      var retValue = new DbColumns()
      {
        { DataEntry.ColumnDataSiteID, dataSiteID },
        { DataEntry.ColumnEntryTime, entryTime }
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<DataEntry, DataEntries> ResultConverter { get; set; }
    #endregion
  }
}
