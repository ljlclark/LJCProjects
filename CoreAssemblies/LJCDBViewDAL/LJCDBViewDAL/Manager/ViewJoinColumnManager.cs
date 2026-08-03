// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinColumnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBViewDAL
{
  /// <summary>Provides Table specific data manipulation methods.</summary>
  public class ViewJoinColumnManager
    : ObjectManager<ViewJoinColumn, ViewJoinColumns>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ObjectManagerC/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ViewJoinColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewJoinColumn")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewJoinColumn.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewJoinColumn.ColumnViewJoinID,
        ViewJoinColumn.ColumnPropertyName,
        ViewJoinColumn.ColumnRenameAs
      });
    }
    #endregion

    #region Load/Retrieve Methods

    // Retrieves a LJCDataColumns collection for the specified parent ID.
    /// <include path='items/LoadDbColumnsWithParentID/*' file='Doc/ViewJoinColumnManager.xml'/>
    public LJCDataColumns LoadDbColumnsWithParentID(int viewJoinID)
    {
      // Load from DataManager to get LJCDataColumns result.
      var keyColumns = ParentIDKey(viewJoinID);
      var dbResult = DataManager.Load(keyColumns);
      SQLStatement = DataManager.SQLStatement;
      ResultConverter<LJCDataColumn, LJCDataColumns> resultConverter
        = new ResultConverter<LJCDataColumn, LJCDataColumns>();
      var retValue = resultConverter.CreateCollection(dbResult);
      return retValue;
    }

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewJoinColumnManager.xml'/>
    public ViewJoinColumns LoadWithParentID(int viewJoinID)
    {
      var keyColumns = ParentIDKey(viewJoinID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewJoinColumnManager.xml'/>
    public DbResult ResultWithParentID(int viewJoinID)
    {
      var keyColumns = ParentIDKey(viewJoinID);
      var retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieves a Data Record with the supplied value.
    /// <include path='items/RetrieveWithID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public ViewJoinColumn RetrieveWithID(int id, List<string> propertyNames = null)
    {
      var keyColumns = IDKey(id);
      var retValue = Retrieve(keyColumns, propertyNames);
      return retValue;
    }

    // Retrieves the record by the unique key.
    /// <summary>
    /// Retrieves the record by the unique key.
    /// </summary>
    /// <param name="viewJoinID">The parent ID.</param>
    /// <param name="propertyName">The Property name.</param>
    /// <param name="renameAs">The RenameAs value.</param>
    /// <returns>The ViewJoinColumn object.</returns>
    public ViewJoinColumn RetrieveWithUnique(int viewJoinID
      , string propertyName, string renameAs)
    {
      var keyColumns = UniqueKey(viewJoinID, propertyName, renameAs);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key record.
    /// <include path='items/IDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public LJCDataColumns IDKey(int id)
    {
      var retValue = new LJCDataColumns()
      {
        { ViewJoinColumn.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/ParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public LJCDataColumns ParentIDKey(int parentID)
    {
      var retValue = new LJCDataColumns()
      {
        { ViewJoinColumn.ColumnViewJoinID, parentID }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/GetUniqueKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public LJCDataColumns UniqueKey(int parentID, string propertyName
      , string renameAs)
    {
      var retValue = new LJCDataColumns()
      {
        { ViewJoinColumn.ColumnViewJoinID, parentID },
        { ViewJoinColumn.ColumnPropertyName, propertyName },
        { ViewJoinColumn.ColumnRenameAs, renameAs }
      };
      return retValue;
    }
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewJoinColumnManager.xml'/>
    public ViewJoinColumn AddData(ViewJoinColumn viewJoinColumn)
    {
      ViewJoinColumn retValue;

      retValue = Add(viewJoinColumn);
      if (retValue != null)
      {
        viewJoinColumn.ID = retValue.ID;
      }
      return retValue;
    }

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(ViewJoinColumn lookupRecord, ViewJoinColumn currentRecord
      , bool isUpdate = false)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (!isUpdate)
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

    // Updates the record if it exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewJoinColumnManager.xml'/>
    public bool SaveData(ViewJoinColumn viewJoinColumn)
    {
      bool retValue = true;

      if (0 == viewJoinColumn.ID)
      {
        // Create record.
        if (null == AddData(viewJoinColumn))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewJoinColumn retrieveData = RetrieveWithID(viewJoinColumn.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          // *** Change ***
          var changedNames = viewJoinColumn.ChangedNames.ChangedProperties;
          //if (NetCommon.HasItems(viewJoinColumn.ChangedNames))
          if (LJC.HasListItems(changedNames))
          {
            var keyColumns = IDKey(retrieveData.ID);
            //Update(viewJoinColumn, keyColumns, viewJoinColumn.ChangedNames);
            Update(viewJoinColumn, keyColumns, changedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
