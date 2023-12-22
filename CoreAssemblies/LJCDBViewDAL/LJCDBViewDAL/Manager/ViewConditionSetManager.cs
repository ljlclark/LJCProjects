// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSetManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewConditionSet specific data manipulation methods.
  /// <include path='items/ViewConditionSetManager/*' file='Doc/ViewConditionSetManager.xml'/>
  public class ViewConditionSetManager
    : ObjectManager<ViewConditionSet, ViewConditionSets>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewConditionSetManagerC/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSetManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewConditionSet")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewConditionSet.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewConditionSet.ColumnViewFilterID
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSets LoadWithParentID(int filterID)
    {
      var keyColumns = ParentIDKey(filterID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewConditionSetManager.xml'/>
    public DbResult ResultWithParentID(int filterID)
    {
      var keyColumns = ParentIDKey(filterID);
      var retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSet RetrieveWithID(int id)
    {
      var keyColumns = IDKey(id);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithParentID/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSet RetrieveWithParentID(int filterID)
    {
      var keyColumns = ParentIDKey(filterID);
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
        { ViewConditionSet.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/ParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentIDKey(int parentID)
    {
      var retValue = new DbColumns()
      {
        { ViewConditionSet.ColumnViewFilterID, parentID }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSet AddData(ViewConditionSet viewConditionSet)
    {
      ViewConditionSet retValue;

      retValue = Add(viewConditionSet);
      if (retValue != null)
      {
        viewConditionSet.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it already exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewConditionSetManager.xml'/>
    public bool SaveData(ViewConditionSet viewConditionSet)
    {
      bool retValue = true;

      if (0 == viewConditionSet.ID)
      {
        // Create record.
        if (null == AddData(viewConditionSet))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewConditionSet retrieveData = RetrieveWithID(viewConditionSet.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (NetCommon.HasItems(viewConditionSet.ChangedNames))
          {
            var keyColumns = IDKey(retrieveData.ID);
            Update(viewConditionSet, keyColumns, viewConditionSet.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
