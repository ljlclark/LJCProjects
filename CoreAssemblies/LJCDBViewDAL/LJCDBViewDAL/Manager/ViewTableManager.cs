// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewTableManager.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBViewDAL
{
  // Provides ViewTable specific data manipulation methods.
  /// <include path='items/ViewTableManager/*' file='Doc/ViewTableManager.xml'/>
  public class ViewTableManager
    : ObjectManager<ViewTable, ViewTables>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewTableManagerC/*' file='Doc/ViewTableManager.xml'/>
    public ViewTableManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewTable")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewTable.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewTable.ColumnName
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ViewTable RetrieveWithID(int id, List<string> propertyNames = null)
    {
      ViewTable retValue;

      var keyColumns = new DbColumns()
      {
        { ViewTable.ColumnID, id }
      };
      retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves the record with the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewTableManager.xml'/>
    public ViewTable RetrieveWithUniqueKey(string tableName)
    {
      ViewTable retValue;

      var keyColumns = new DbColumns()
      {
        { ViewTable.ColumnName, (object)tableName }
      };
      retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewTableManager.xml'/>
    public ViewTable AddData(ViewTable viewTable)
    {
      ViewTable retValue;

      retValue = Add(viewTable);
      if (retValue != null)
      {
        viewTable.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewTableManager.xml'/>
    public bool SaveData(ViewTable viewTable)
    {
      bool retValue = true;

      if (0 == viewTable.ID)
      {
        // Create record.
        if (null == AddData(viewTable))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewTable retrieveData = RetrieveWithID(viewTable.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (NetCommon.HasItems(viewTable.ChangedNames))
          {
            var keyColumns = new DbColumns()
            {
              { ViewTable.ColumnID, retrieveData.ID }
            };
            Update(viewTable, keyColumns, viewTable.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
