// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDataUtility.cs
using System;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataUtilityDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersDataUtility
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../CommonData.xml'/>
    public ManagersDataUtility()
    {
    }

    /// <summary>
    /// Sets the DB properties.
    /// </summary>
    /// <param name="dbServiceRef">The database service reference object.</param>
    /// <param name="dataConfigName">The data configuration name.</param>
    public void SetDBProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }
    #endregion

    #region Methods

    // Gets the DataKey by ID.
    // Move to Managers?
    // ********************
    public DataKey GetDataKey(int id)
    {
      DataKey retDataKey = null;

      if (id > 0)
      {
        var manager = DataKeyManager;
        if (manager != null)
        {
          retDataKey = manager.RetrieveWithID(id);
        }
      }
      return retDataKey;
    }

    // Gets the DataKey by ID.
    // Move to Managers?
    // ********************
    internal DataUtilTable GetDataTable(int id)
    {
      DataUtilTable retDataTable = null;

      if (id > 0)
      {
        var manager = DataTableManager;
        if (manager != null)
        {
          retDataTable = manager.RetrieveWithID(id);
        }
      }
      return retDataTable;
    }

    // Gets the table DataColumns.
    // ********************
    public DataColumns TableDataColumns(int tableID)
    {
      DataColumns retColumns = null;

      var columnManager = DataColumnManager;
      var keyColumns = columnManager.ParentKey(tableID);
      var items = columnManager.Load(keyColumns);
      if (NetCommon.HasItems(items))
      {
        retColumns = items;
      }
      return retColumns;
    }
    #endregion

    #region Properties

    /// <summary>Gets the DataColumnManager object.</summary>
    public DataColumnManager DataColumnManager
    {
      get
      {
        if (null == mDataColumnManager)
        {
          mDataColumnManager
            = new DataColumnManager(mDbServiceRef, mDataConfigName);
        }
        return mDataColumnManager;
      }
    }
    private DataColumnManager mDataColumnManager;

    /// <summary>Gets the DataKeyManager object.</summary>
    public DataKeyManager DataKeyManager
    {
      get
      {
        if (null == mDataKeyManager)
        {
          mDataKeyManager
            = new DataKeyManager(mDbServiceRef, mDataConfigName);
        }
        return mDataKeyManager;
      }
    }
    private DataKeyManager mDataKeyManager;

    /// <summary>Gets the DataModuleManager object.</summary>
    public DataModuleManager DataModuleManager
    {
      get
      {
        if (null == mDataModuleManager)
        {
          mDataModuleManager
            = new DataModuleManager(mDbServiceRef, mDataConfigName);
        }
        return mDataModuleManager;
      }
    }
    private DataModuleManager mDataModuleManager;

    /// <summary>Gets the DataTableManager object.</summary>
    public DataTableManager DataTableManager
    {
      get
      {
        if (null == mDataTableManager)
        {
          mDataTableManager
            = new DataTableManager(mDbServiceRef, mDataConfigName);
        }
        return mDataTableManager;
      }
    }
    private DataTableManager mDataTableManager;
    #endregion

    #region Class Data

    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    #endregion
  }
}
