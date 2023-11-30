// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewFilterManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewFilter specific data manipulation methods.
  /// <include path='items/ViewFilterManager/*' file='Doc/ViewFilterManager.xml'/>
  public class ViewFilterManager
    : ObjectManager<ViewFilter, ViewFilters>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewFilterManagerC/*' file='Doc/ViewFilterManager.xml'/>
    public ViewFilterManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewFilter")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewFilter.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewFilter.ColumnViewDataID,
        ViewFilter.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewFilterManager.xml'/>
    public ViewFilters LoadWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewFilterManager.xml'/>
    public DbResult ResultWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewFilterManager.xml'/>
    public ViewFilter RetrieveWithID(int id)
    {
      var keyColumns = IDKey(id);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewFilterManager.xml'/>
    public ViewFilter RetrieveWithUniqueKey(int viewDataID, string name)
    {
      var keyColumns = UniqueKey(viewDataID, name);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/IDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns IDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewFilter.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/ParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentIDKey(int parentID)
    {
      var retValue = new DbColumns()
      {
        { ViewFilter.ColumnViewDataID, parentID }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/UniqueKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(int parentID, string name)
    {
      var retValue = new DbColumns()
      {
        { ViewFilter.ColumnViewDataID, parentID },
        { ViewFilter.ColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewFilterManager.xml'/>
    public ViewFilter AddData(ViewFilter viewFilter)
    {
      ViewFilter retValue;

      retValue = Add(viewFilter);
      if (retValue != null)
      {
        viewFilter.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it already exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewFilterManager.xml'/>
    public bool SaveData(ViewFilter viewFilter)
    {
      bool retValue = true;

      if (0 == viewFilter.ID)
      {
        // Create record.
        if (null == AddData(viewFilter))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewFilter retrieveData = RetrieveWithID(viewFilter.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewFilter.ChangedNames.Count > 0)
          {
            var keyColumns = IDKey(retrieveData.ID);
            Update(viewFilter, keyColumns, viewFilter.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
