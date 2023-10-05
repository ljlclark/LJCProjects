// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDBViewDAL
{
  // Provides ViewJoinOn specific data manipulation methods.
  /// <include path='items/ViewJoinOnManager/*' file='Doc/ViewJoinOnManager.xml'/>
  public class ViewJoinOnManager
    : ObjectManager<ViewJoinOn, ViewJoinOns>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewJoinOnManagerC/*' file='Doc/ViewJoinOnManager.xml'/>
    public ViewJoinOnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewJoinOn")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewJoinOn.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewJoinOn.ColumnViewJoinID,
        ViewJoinOn.ColumnFromColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a collection of data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewJoinOnManager.xml'/>
    public ViewJoinOns LoadWithParentID(int viewJoinID)
    {
      ViewJoinOns retValue;

      var keyColumns = GetParentKey(viewJoinID);
      retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewJoinOnManager.xml'/>
    public DbResult ResultWithParentID(int viewJoinID)
    {
      DbResult retValue;

      var keyColumns = GetParentKey(viewJoinID);
      retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record by ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewJoinOnManager.xml'/>
    public ViewJoinOn RetrieveWithID(int id)
    {
      ViewJoinOn retValue;

      var keyColumns = GetIDKey(id);
      retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewJoinOnManager.xml'/>
    public ViewJoinOn RetrieveWithUniqueKey(int viewJoinID, string fromColumnName)
    {
      ViewJoinOn retValue;

      // Add(columnName, propertyName = null, renameAs = null
      //   , datatypeName = "String", caption = null);
      // Add(columnName, object value, dataTypeName = "String");
      var keyColumns = new DbColumns()
      {
        { ViewJoinOn.ColumnViewJoinID, viewJoinID },
        { ViewJoinOn.ColumnFromColumnName, (object)fromColumnName }
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
        { ViewJoinOn.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetIDKey/*' file='../../../CoreUtilities/LJCDocLib/Common/Manager.xml'/>
    public DbColumns GetParentKey(int id)
    {
      var retValue = new DbColumns()
      {
        { ViewJoinOn.ColumnViewJoinID, id }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewJoinOnManager.xml'/>
    public ViewJoinOn AddData(ViewJoinOn viewJoinOn)
    {
      ViewJoinOn retValue;

      retValue = Add(viewJoinOn);
      if (retValue != null)
      {
        viewJoinOn.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it already exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewJoinOnManager.xml'/>
    public bool SaveData(ViewJoinOn viewJoinOn)
    {
      bool retValue = true;

      if (0 == viewJoinOn.ID)
      {
        // Create record.
        if (null == AddData(viewJoinOn))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewJoinOn retrieveData = RetrieveWithID(viewJoinOn.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewJoinOn.ChangedNames.Count > 0)
          {
            var keyColumns = GetIDKey(retrieveData.ID);
            Update(viewJoinOn, keyColumns, viewJoinOn.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
