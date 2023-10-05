// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewCondition specific data manipulation methods.
  /// <include path='items/ViewConditionManager/*' file='Doc/ViewConditionManager.xml'/>
  public class ViewConditionManager
    : ObjectManager<ViewCondition, ViewConditions>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewConditionManagerC/*' file='Doc/ViewConditionManager.xml'/>
    public ViewConditionManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewCondition")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewCondition.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewCondition.ColumnViewConditionSetID,
        ViewCondition.ColumnFirstValue
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewConditionManager.xml'/>
    public ViewConditions LoadWithParentID(int conditionSetID)
    {
      ViewConditions retValue;

      var keyColumns = GetParentKey(conditionSetID);
      retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewConditionManager.xml'/>
    public DbResult ResultWithParentID(int conditionSetID)
    {
      DbResult retValue;

      var keyColumns = GetParentKey(conditionSetID);
      retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewConditionManager.xml'/>
    public ViewCondition RetrieveWithUniqueKey(int viewConditionSetID, string firstValue)
    {
      ViewCondition retValue;

      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var keyColumns = new DbColumns()
      {
        { ViewCondition.ColumnViewConditionSetID, viewConditionSetID },
        { ViewCondition.ColumnFirstValue, (object)firstValue }
      };
      retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetParentKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewCondition.ColumnViewConditionSetID, id }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewConditionManager.xml'/>
    public ViewCondition AddData(ViewCondition viewCondition)
    {
      ViewCondition retValue;

      retValue = Add(viewCondition);
      if (retValue != null)
      {
        viewCondition.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it already exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewConditionManager.xml'/>
    public bool SaveData(ViewCondition viewCondition)
    {
      bool retValue = true;

      if (0 == viewCondition.ID)
      {
        // Create record.
        if (null == AddData(viewCondition))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewCondition retrieveData = RetrieveWithUniqueKey(viewCondition.ID
          , viewCondition.FirstValue);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewCondition.ChangedNames.Count > 0)
          {
            var keyColumns = new DbColumns()
          {
            { ViewCondition.ColumnID, retrieveData.ID }
          };
            Update(viewCondition, keyColumns, viewCondition.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
