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
      ViewConditionSets retValue;

      var keyColumns = GetParentKey(filterID);
      retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewConditionSetManager.xml'/>
    public DbResult ResultWithParentID(int filterID)
    {
      DbResult retValue;

      var keyColumns = GetParentKey(filterID);
      retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSet RetrieveWithID(int id)
    {
      ViewConditionSet retValue;

      var keyColumns = GetIDKey(id);
      retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewConditionSetManager.xml'/>
    public ViewConditionSet RetrieveWithUniqueKey(int viewFilterID)
    {
      ViewConditionSet retValue;

      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var keyColumns = new DbColumns()
      {
        { ViewConditionSet.ColumnViewFilterID, viewFilterID }
      };
      retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewConditionSet.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetParentKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewConditionSet.ColumnViewFilterID, id }
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
          if (viewConditionSet.ChangedNames.Count > 0)
          {
            var keyColumns = GetIDKey(retrieveData.ID);
            Update(viewConditionSet, keyColumns, viewConditionSet.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
