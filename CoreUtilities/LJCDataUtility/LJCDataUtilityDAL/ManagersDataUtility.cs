// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDataUtility.cs
using System;
using System.Collections.Generic;
using System.Reflection;
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
      mArgError = new ArgError("LJCDataUtility.ManagersDataUtility");
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

    #region Data Methods

    /// <summary>Gets the DataColumn by ID.</summary>
    public DataUtilColumn GetDataColumn(int id)
    {
      DataUtilColumn retDataColumn = null;

      IDError(id, "GetDataColumn(int id)", "id");
      var manager = DataColumnManager;
      if (manager != null)
      {
        retDataColumn = manager.RetrieveWithID(id);
      }
      return retDataColumn;
    }

    /// <summary>Gets the DataKey by ID.</summary>
    public DataKey GetDataKey(int id)
    {
      DataKey retDataKey = null;

      IDError(id, "GetDataKey(int id)", "id");
      var manager = DataKeyManager;
      if (manager != null)
      {
        retDataKey = manager.RetrieveWithID(id);
      }
      return retDataKey;
    }

    /// <summary>Gets the DataModule by ID.</summary>
    public DataModule GetDataModule(int id)
    {
      DataModule retDataModule = null;

      IDError(id, "GetDataModule(int id)", "id");
      var manager = DataModuleManager;
      if (manager != null)
      {
        retDataModule = manager.RetrieveWithID(id);
      }
      return retDataModule;
    }

    /// <summary>Gets the DataKey by ID.</summary>
    public DataUtilTable GetDataTable(int id)
    {
      DataUtilTable retDataTable = null;

      IDError(id, "GetDataTable(int id)", "id");
      var manager = DataTableManager;
      if (manager != null)
      {
        retDataTable = manager.RetrieveWithID(id);
      }
      return retDataTable;
    }

    /// <summary>Gets the table DataColumns.</summary>
    public DataColumns TableDataColumns(int tableID
      , List<string> orderByNames = null)
    {
      DataColumns retColumns = null;

      IDError(tableID, "TableDataColumns(int tableID)", "tableID");
      var columnManager = DataColumnManager;
      var keyColumns = columnManager.ParentKey(tableID);
      if (NetCommon.HasItems(orderByNames))
      {
        columnManager.Manager.OrderByNames = orderByNames;
      }
      var items = columnManager.Load(keyColumns);
      if (NetCommon.HasItems(items))
      {
        retColumns = items;
      }
      return retColumns;
    }

    // Throws an ID error if id is less than 1.
    private void IDError(int id, string methodName, string argument)
    {
      if (id < 1)
      {
        mArgError.MethodName = methodName;
        var message = $"Param {argument} must be greater than zero.\r\n";
        mArgError.Add(message);
        NetString.ThrowArgError(mArgError.ToString());
      }
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

    private ArgError mArgError;
    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    #endregion
  }
}
