// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderByManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewOrderBy specific data manipulation methods.
  /// <include path='items/ViewOrderByManager/*' file='Doc/ViewOrderByManager.xml'/>
  public class ViewOrderByManager
    : ObjectManager<ViewOrderBy, ViewOrderBys>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewOrderByManagerC/*' file='Doc/ViewOrderByManager.xml'/>
    public ViewOrderByManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewOrderBy")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewOrderBy.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewOrderBy.ColumnViewDataID,
        ViewOrderBy.ColumnColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewOrderByManager.xml'/>
    public ViewOrderBys LoadWithParentID(int viewDataID)
    {
      ViewOrderBys retValue;

      var keyColumns = GetParentKey(viewDataID);
      retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewOrderByManager.xml'/>
    public DbResult ResultWithParentID(int viewDataID)
    {
      DbResult retValue;

      var keyColumns = GetParentKey(viewDataID);
      retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewOrderByManager.xml'/>
    public ViewOrderBy RetrieveWithID(int id)
    {
      ViewOrderBy retValue;

      var keyColumns = GetIDKey(id);
      retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewOrderByManager.xml'/>
    public ViewOrderBy RetrieveWithUniqueKey(string columnName)
    {
      ViewOrderBy retValue;

      var keyColumns = GetNameKey(columnName);
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
        { ViewOrderBy.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetNameKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetNameKey(string name)
    {
      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var retValue = new DbColumns()
      {
        { ViewOrderBy.ColumnColumnName, (object)name }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetParentKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewOrderBy.ColumnViewDataID, id }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewOrderByManager.xml'/>
    public ViewOrderBy AddData(ViewOrderBy viewOrderBy)
    {
      ViewOrderBy retValue;

      retValue = Add(viewOrderBy);
      if (retValue != null)
      {
        viewOrderBy.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it already exists, othserwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewOrderByManager.xml'/>
    public bool SaveData(ViewOrderBy viewOrderBy)
    {
      bool retValue = true;
      if (0 == viewOrderBy.ID)
      {
        // Create record.
        if (null == AddData(viewOrderBy))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewOrderBy retrieveData = RetrieveWithID(viewOrderBy.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewOrderBy.ChangedNames.Count > 0)
          {
            var keyColumns = GetIDKey(retrieveData.ID);
            Update(viewOrderBy, keyColumns, viewOrderBy.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
