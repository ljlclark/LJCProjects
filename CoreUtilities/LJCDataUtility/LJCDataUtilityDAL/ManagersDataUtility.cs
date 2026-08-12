// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDataUtility.cs
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDataUtilityDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersDataUtility
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='members/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ManagersDataUtility()
    {
      mArgError = new ArgError("LJCDataUtility.ManagersDataUtility");
    }

    // Sets the DB properties.
    /// <include path='members/DefaultConstructor/*' file='Doc/ManagersDataUtility.xml'/>
    public void SetDBProperties(DbServiceRef dbServiceRef
      , string dataConfigName)
    {
      mDbServiceRef = dbServiceRef;
      mDataConfigName = dataConfigName;
    }
    #endregion

    #region Data Methods

    /// <summary>Gets the DataColumn by ID.</summary>
    public DataUtilColumn GetDataColumn(long id, short dbId)
    {
      DataUtilColumn retDataColumn = null;

      IDError(id, "GetDataColumn(long id)", "id");
      var manager = DataColumnManager;
      if (manager != null)
      {
        retDataColumn = manager.RetrieveWithId(id, dbId);
      }
      return retDataColumn;
    }

    /// <summary>Gets the DataKey by ID.</summary>
    public DataKey GetDataKey(long id, short dbId)
    {
      DataKey retDataKey = null;

      IDError(id, "GetDataKey(long id)", "id");
      var manager = DataKeyManager;
      if (manager != null)
      {
        retDataKey = manager.RetrieveWithID(id, dbId);
      }
      return retDataKey;
    }

    /// <summary>Gets the DataModule by ID.</summary>
    public DataModule GetDataModule(long id, short dbId)
    {
      DataModule retDataModule = null;

      IDError(id, "GetDataModule(long id)", "id");
      var manager = DataModuleManager;
      if (manager != null)
      {
        retDataModule = manager.RetrieveWithID(id, dbId);
      }
      return retDataModule;
    }

    /// <summary>Gets the DataKey by ID.</summary>
    public DataUtilTable GetDataTable(long id, short dbId)
    {
      DataUtilTable retDataTable = null;

      IDError(id, "GetDataTable(id, dbId)", "id");
      IDError(dbId, "GetDataTable(id, dbId)", "dbId");
      var manager = DataTableManager;
      if (manager != null)
      {
        retDataTable = manager.RetrieveWithID(id, dbId);
      }
      return retDataTable;
    }

    /// <summary>Gets the table DataColumns.</summary>
    public DataColumns TableDataColumns(long tableId, short dbId
      , List<string> orderByNames = null)
    {
      DataColumns retColumns = null;

      IDError(tableId, "TableDataColumns(int tableId)", "tableId");
      var columnManager = DataColumnManager;
      var keyColumns = columnManager.ParentKey(tableId, dbId);
      if (LJC.HasListItems(orderByNames))
      {
        columnManager.Manager.OrderByNames = orderByNames;
      }
      var items = columnManager.Load(keyColumns);
      if (LJC.HasListItems(items))
      {
        retColumns = items;
      }
      return retColumns;
    }

    // Throws an ID error if id is less than 1.
    private void IDError(long id, string methodName, string argument)
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

    private readonly ArgError mArgError;
    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    #endregion
  }
}
