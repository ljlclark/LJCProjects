// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewColumnManager.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;

namespace LJCDBViewDAL
{
  // Provides ViewColumn specific data manipulation methods.
  /// <include path='items/ViewColumnManager/*' file='Doc/ProjectDbViewDAL.xml'/>
  public class ViewColumnManager
    : ObjectManager<ViewColumn, ViewColumns>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewColumnManagerC/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumnManager(DbServiceRef dbServiceRef, string dataConfigName
      , string tableName = "ViewColumn")
      : base(dbServiceRef, dataConfigName, tableName)
    {
      // Create the list of database assigned columns.
      SetDbAssignedColumns(new string[]
      {
        ViewColumn.ColumnID
      });

      // Create the list of lookup column names.
      SetLookupColumns(new string[]
      {
        ViewColumn.ColumnViewDataID,
        ViewColumn.ColumnPropertyName,
        ViewColumn.ColumnRenameAs
      });
    }
    #endregion

    #region Data Helper Methods

    // Adds a record including the flag values.
    /// <include path='items/AddWithFlags/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumn AddWithFlags(ViewColumn dataObject, List<string> propertyNames = null)
    {
      ViewColumn retValue;

      dataObject.ChangedNames.Add(ViewColumn.ColumnIsPrimaryKey);
      retValue = Add(dataObject, propertyNames);
      return retValue;
    }

    // Retrieves a DbColumns collection for the specified parent ID.
    /// <include path='items/LoadDbColumnsWithParentID/*' file='Doc/ViewColumnManager.xml'/>
    public DbColumns LoadDbColumnsWithParentID(int viewDataID, string tableName)
    {
      DbResult viewColumnResult;
      DbColumns retValue;

      // Load from DataManager to get DbColumns result.
      var keyColumns = ParentIDKey(viewDataID);
      viewColumnResult = DataManager.Load(keyColumns);

      // Copies ViewColumn properties to DbColumn objects.
      // Where ViewColumn properties match DbColumn.PropertyName.
      ResultConverter<DbColumn, DbColumns> resultConverter
        = new ResultConverter<DbColumn, DbColumns>();
      retValue = resultConverter.CreateCollection(viewColumnResult);

      // Get table definition.
      var dataManager = new DataManager(DataManager.DbServiceRef
        , DataConfigName, tableName);
      var recordColumns = dataManager.DataDefinition;

      // Populate missing values.
      // Process each Data Object column.
      foreach (DbColumn column in retValue)
      {
        var findColumn
          = recordColumns.LJCSearchPropertyName(column.PropertyName);
        if (findColumn != null)
        {
          column.MaxLength = findColumn.MaxLength;
          column.IsPrimaryKey = findColumn.IsPrimaryKey;
        }
      }
      return retValue;
    }

    // Retrieves a collection of Data records for the specified parent ID.
    /// <include path='items/LoadWithParentID/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumns LoadWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = Load(keyColumns);
      return retValue;
    }

    // Retrieves the DbResult set of data rows.
    /// <include path='items/ResultWithParentID/*' file='Doc/ViewColumnManager.xml'/>
    public DbResult ResultWithParentID(int viewDataID)
    {
      var keyColumns = ParentIDKey(viewDataID);
      var retValue = DataManager.Load(keyColumns);
      return retValue;
    }

    // Retrieve the record with ID.
    /// <include path='items/RetrieveWithID/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumn RetrieveWithID(int id)
    {
      var keyColumns = IDKey(id);
      var retValue = Retrieve(keyColumns);
      return retValue;
    }

    // Retrieves the record with the unique key.
    /// <include path='items/RetrieveWithUniqueKey/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumn RetrieveWithUniqueKey(int viewDataID, string columnName)
    {
      var keyColumns = UniqueKey(viewDataID, columnName);
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
        { ViewColumn.ColumnID, id }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/ParentIDKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns ParentIDKey(int parentID)
    {
      var retValue = new DbColumns()
      {
        { ViewColumn.ColumnViewDataID, parentID }
      };
      return retValue;
    }

    // Gets the ID key record.
    /// <include path='items/UniqueKey/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public DbColumns UniqueKey(int parentID, string name)
    {
      var retValue = new DbColumns()
      {
        { ViewColumn.ColumnViewDataID, parentID },
        { ViewColumn.ColumnColumnName, (object)name }
      };
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Check for duplicate unique key.
    /// <include path='items/IsDuplicate/*' file='../../../CoreUtilities/LJCGenDoc/Common/Manager.xml'/>
    public bool IsDuplicate(ViewColumn lookupRecord, ViewColumn currentRecord
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
    #endregion

    #region Custom Data Methods

    // Adds a data record.
    /// <include path='items/AddData/*' file='Doc/ViewColumnManager.xml'/>
    public ViewColumn AddData(ViewColumn viewColumn)
    {
      ViewColumn retValue;

      retValue = AddWithFlags(viewColumn);
      if (retValue != null)
      {
        viewColumn.ID = retValue.ID;
      }
      return retValue;
    }

    // Updates the record if it exists, otherwise creates it.
    /// <include path='items/SaveData/*' file='Doc/ViewColumnManager.xml'/>
    public bool SaveData(ViewColumn viewColumn)
    {
      bool retValue = true;

      if (0 == viewColumn.ID)
      {
        // Create record.
        if (null == AddData(viewColumn))
        {
          retValue = false;
        }
      }
      else
      {
        // Update record.
        ViewColumn retrieveData = RetrieveWithID(viewColumn.ID);
        if (null == retrieveData)
        {
          retValue = false;
        }

        if (retValue)
        {
          // Note: Changed to update only changed columns.
          if (viewColumn.ChangedNames.Count > 0)
          {
            var keyColumns = IDKey(retrieveData.ID);
            Update(viewColumn, keyColumns, viewColumn.ChangedNames);
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
