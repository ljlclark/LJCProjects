// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlTabManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDataDetailDAL
{
  /// <summary>Provides table specific data methods.</summary>
  public class ControlTabManager
  {
    // Initializes an object instance.
    /// <include path='items/DataManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlTabManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ControlTab", string schemaName = null)
    {
      Manager = new DataManager(dbServiceRef, dataConfigName, tableName
        , schemaName);
      ResultConverter = new ResultConverter<ControlTab, ControlTabItems>();

      // Map table names with property names or captions
      // that differ from the column names.
      Manager.MapNames(ControlTab.ColumnID, caption: "ControlTab ID");

      // Create the list of database assigned columns.
      Manager.SetDbAssignedColumns(new string[]
      {
        ControlTab.ColumnID
      });

      // Create the list of lookup column names.
      Manager.SetLookupColumns(new string[]
      {
        ControlTab.ColumnControlDetailID,
        ControlTab.ColumnTabIndex
      });
    }

    #region Data Methods

    // Adds a record to the database.
    /// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlTab Add(ControlTab dataObject, List<string> propertyNames = null)
    {
      ControlTab retValue;

      var dbResult = Manager.Add(dataObject, propertyNames);
      retValue = ResultConverter.CreateData(dbResult);
      if (retValue != null)
      {
        dataObject.ID = retValue.ID;
      }
      return retValue;
    }

    // Deletes the records with the specified key values.
    /// <include path='items/Delete/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void Delete(DbColumns keyColumns, DbFilters filters = null)
    {
      Manager.Delete(keyColumns, filters);
    }

    // Retrieves a collection of data records.
    /// <include path='items/Load/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlTabItems Load(DbColumns keyColumns = null
      , List<string> propertyNames = null, DbFilters filters = null
      , DbJoins joins = null)
    {
      ControlTabItems retValue;

      var dbResult = Manager.Load(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a record from the database.
    /// <include path='items/Retrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlTab Retrieve(DbColumns keyColumns, List<string> propertyNames = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      ControlTab retValue;

      var dbResult = Manager.Retrieve(keyColumns, propertyNames, filters, joins);
      retValue = ResultConverter.CreateData(dbResult);
      return retValue;
    }

    // Updates the record.
    /// <include path='items/Update/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public void Update(ControlTab dataObject, DbColumns keyColumns
      , List<string> propertyNames = null, DbFilters filters = null)
    {
      Manager.Update(dataObject, keyColumns, propertyNames, filters);
    }
    #endregion

    #region Custom Load/Retrieve Methods

    // Loads the parent records.
    /// <include path='items/LoadWithParentID/*' file='Doc/ControlTabManager.xml'/>
    public ControlTabItems LoadWithParentID(long controlDetailID)
    {
      var keyColumns = GetParentKey(controlDetailID);
      Manager.OrderByNames = new List<string>()
      {
        ControlTab.ColumnTabIndex
      };
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves a record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ControlTab RetrieveWithID(long id, List<string> propertyNames = null)
    {
      var keyColumns = GetIDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves a record with the supplied name value.
    /// <include path='items/RetrieveWithUnique/*' file='Doc/ControlTabManager.xml'/>
    public ControlTab RetrieveWithUnique(long controlDetailID, int tabIndex
      , List<string> propertyNames = null)
    {
      var keyColumns = GetUniqueKey(controlDetailID, tabIndex);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns GetIDKey(long id)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      // Needs (object) cast for string value to select correct Add overload.
      var retValue = new DbColumns()
      {
        { ControlTab.ColumnID, id }
      };
      return retValue;
    }

    // Gets the Parent ID key columns.
    /// <include path='items/GetParentKey/*' file='Doc/ControlTabManager.xml'/>
    public DbColumns GetParentKey(long controlDetailID)
    {
      var retValue = new DbColumns()
      {
        { ControlTab.ColumnControlDetailID, controlDetailID }
      };
      return retValue;
    }

    // Gets the ID key columns.
    /// <include path='items/GetUniqueKey/*' file='Doc/ControlTabManager.xml'/>
    public DbColumns GetUniqueKey(long controlDetailID, int tabIndex)
    {
      var retValue = new DbColumns()
      {
        { ControlTab.ColumnControlDetailID, controlDetailID },
        { ControlTab.ColumnTabIndex, tabIndex }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(ControlTab lookupRecord, ControlTab currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (false == isUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.ID != currentRecord.ID)
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

    /// <summary>
    /// Gets or sets the non-select affected record count.
    /// </summary>
    public int AffectedCount
    {
      get { return Manager.AffectedCount; }
    }

    /// <summary>Gets or sets the DataManager reference.</summary>
    public DataManager Manager { get; private set; }

    /// <summary>Gets or sets the ResultConverter reference.</summary>
    public ResultConverter<ControlTab, ControlTabItems> ResultConverter { get; set; }
    #endregion
  }
}
