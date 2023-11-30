// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewJoin specific data manipulation methods.
  /// <include path='items/ViewJoinManager/*' file='Doc/ViewJoinManager.xml'/>
  public class ViewJoinManager
    : ObjectManager<ViewJoin, ViewJoins>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewJoinManagerC/*' file='Doc/ViewJoinManager.xml'/>
    public ViewJoinManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewJoin")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Map table names with property names or captions
      // that differ from the column names.
      MapNames(ViewJoin.ColumnTableName, ViewJoin.PropertyTableName);

      // Add calculated and join columns.
      // Enables adding to a grid configuration and populating a Data Object.

      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewJoin.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewJoin.ColumnViewDataID,
        ViewJoin.ColumnTableName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewJoinManager.xml'/>
    public ViewJoins LoadWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewJoinManager.xml'/>
    public DbResult ResultWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewJoinManager.xml'/>
    public ViewJoin RetrieveWithID(int id)
    {
      var keyColumns = IDKey(id);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewJoinManager.xml'/>
    public ViewJoin RetrieveWithUniqueKey(int viewDataID, string joinTableName)
    {
      var keyColumns = UniqueKey(viewDataID, joinTableName);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns IDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewJoin.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentIDKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewJoin.ColumnViewDataID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/UniqueKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(int parentID, string name)
    {
      var retValue = new DbColumns()
      {
        { ViewJoin.ColumnViewDataID, parentID },
        { ViewJoin.ColumnTableName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewJoinManager.xml'/>
    public ViewJoin AddData(ViewJoin viewJoin)
    {
      ViewJoin retValue;

      retValue = Add(viewJoin);
      if (retValue != null)
      {
        viewJoin.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewJoinManager.xml'/>
    public bool SaveData(ViewJoin viewJoin)
    {
      bool retValue = true;

      if (0 == viewJoin.ID)
      {
        // Create record.
        if (null == AddData(viewJoin))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewJoin retrieveData = RetrieveWithID(viewJoin.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewJoin.ChangedNames.Count > 0)
          {
            var keyColumns = IDKey(retrieveData.ID);
            Update(viewJoin, keyColumns, viewJoin.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
