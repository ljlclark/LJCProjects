// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersDataUtility5.cs
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // Gets the Manager objects.
  /// <include file='Doc/ManagersDataUtility.xml'
  ///  path='members/ManagersDataUtility/*'/>
  public class ManagersDataUtility
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ParamConstructor/*'/>
    public ManagersDataUtility(string dataConfigName
      , LJCDbServiceRef? dbServiceRef = null)
    {
      _ArgError = new LJCArgError("LJCDataUtility5.ManagersDataUtility");
      Reset(dataConfigName, dbServiceRef);
    }

    // Sets the DB properties.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/SetDBProperties/*'/>
    public void Reset(string dataConfigName
      , LJCDbServiceRef? dbServiceRef = null)
    {
      _DataConfigName = dataConfigName;
      if (dbServiceRef != null)
      {
        _DbServiceRef = dbServiceRef;
      }
      else
      {
        // Initialize local message data access.
        _DbServiceRef = new LJCDbServiceRef
        {
          DbDataAccess = new DbDataAccess(dataConfigName)
        };
      }
      _ResetNames =
      [
        "DataColumnManager",
        "DataKeyManager",
        "DataModuleManager",
        "DataTableManager",
      ];
    }
    #endregion

    #region Methods

    // Clears a reset name.
    private void ClearResetName(string name)
    {
      if (_ResetNames != null
        && _ResetNames.Contains(name))
      {
        _ResetNames.Remove(name);
      }
    }

    // Throws an ID error if id is less than 1.
    private void IDError(long id, string methodName, string argument)
    {
      if (id < 1)
      {
        _ArgError.MethodName = methodName;
        var message = $"Param {argument} must be greater than zero.\r\n";
        _ArgError.Add(message);
        var errorText = _ArgError.ToString();
        if (LJC.HasText(errorText))
        {
          LJCNetString.ThrowArgError(errorText);
        }
      }
    }
    #endregion

    #region Data Methods

    // Gets the DataColumn by ID.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/GetDataColumn/*'/>
    public DataUtilColumn? GetDataColumn(short dbId, long id)
    {
      DataUtilColumn? retDataColumn = null;

      IDError(dbId, "GetDataColumn(dbId, id)", "dbId");
      IDError(id, "GetDataColumn(dbId, id)", "id");

      var manager = DataColumnManager;
      if (manager != null)
      {
        retDataColumn = manager.RetrieveWithId(dbId, id);
      }
      return retDataColumn;
    }

    // Gets the DataKey by ID.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/GetDataKey/*'/>
    public DataKey? GetDataKey(short dbId, long id)
    {
      DataKey? retDataKey = null;

      IDError(dbId, "GetDataKey(dbId, id)", "dbId");
      IDError(id, "GetDataKey(dbId, id)", "id");

      var manager = DataKeyManager;
      if (manager != null)
      {
        retDataKey = manager.RetrieveWithId(dbId, id);
      }
      return retDataKey;
    }

    // Gets the DataModule by ID.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/GetDataModule/*'/>
    public DataModule? GetDataModule(short dbId, long id)
    {
      DataModule? retDataModule = null;

      IDError(dbId, "GetDataModule(dbId, id)", "dbId");
      IDError(id, "GetDataModule(long id)", "id");

      var manager = DataModuleManager;
      if (manager != null)
      {
        retDataModule = manager.RetrieveWithId(dbId, id);
      }
      return retDataModule;
    }

    // Gets the DataTable by ID.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/GetDataTable/*'/>
    public DataUtilTable? GetDataTable(short dbId, long id)
    {
      DataUtilTable? retDataTable = null;

      IDError(dbId, "GetDataTable(dbId, id)", "dbId");
      IDError(id, "GetDataTable(dbId, id)", "id");

      var manager = DataTableManager;
      if (manager != null)
      {
        retDataTable = manager.RetrieveWithId(dbId, id);
      }
      return retDataTable;
    }

    // Gets the table DataColumns.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/TableDataColumns/*'/>
    public DataColumns? TableDataColumns(short tableDbId, long tableId
      , List<string>? orderByNames = null)
    {
      DataColumns? retColumns = null;

      IDError(tableDbId, "TableDataColumns(tableDbId, tableId)", "tableDbId");
      IDError(tableId, "TableDataColumns(tableDbId, tableId)", "tableId");

      var columnManager = DataColumnManager;
      if (columnManager != null)
      {
        if (columnManager.Manager != null
          && LJC.HasListItems(orderByNames))
        {
          if (columnManager.Manager.OrderByNames != null)
          {
            columnManager.Manager.OrderByNames = orderByNames;
          }
        }
        var keyColumns = DataColumnManager.ParentKey(tableDbId, tableId);
        var items = columnManager.Load(keyColumns);
        if (LJC.HasListItems(items))
        {
          retColumns = items;
        }
      }
      return retColumns;
    }
    #endregion

    #region Properties

    // Gets the DataColumnManager object.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/DataColumnManager/*'/>
    public DataColumnManager DataColumnManager
    {
      get
      {
        var managerName = "DataColumnManager";
        if (null == _DataColumnManager
          || _ResetNames.Contains(managerName))
        {
          _DataColumnManager
            = new DataColumnManager(_DbServiceRef, _DataConfigName);
          ClearResetName(managerName);
        }
        return _DataColumnManager;
      }
    }
    private DataColumnManager _DataColumnManager = null!;

    // Gets the DataKeyManager object.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/DataKeyManager/*'/>
    public DataKeyManager DataKeyManager
    {
      get
      {
        var managerName = "DataKeyManager";
        if (null == _DataKeyManager
          || _ResetNames.Contains(managerName))
        {
          _DataKeyManager
            = new DataKeyManager(_DbServiceRef, _DataConfigName);
          ClearResetName(managerName);
        }
        return _DataKeyManager;
      }
    }
    private DataKeyManager _DataKeyManager = null!;

    // Gets the DataModuleManager object.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/DataModuleManager/*'/>
    public DataModuleManager DataModuleManager
    {
      get
      {
        var managerName = "DataModuleManager";
        if (null == _DataModuleManager
          || _ResetNames.Contains(managerName))
        {
          _DataModuleManager
            = new DataModuleManager(_DbServiceRef, _DataConfigName);
          ClearResetName(managerName);
        }
        return _DataModuleManager;
      }
    }
    private DataModuleManager _DataModuleManager = null!;

    // Gets the DataTableManager object.
    /// <include file='Doc/ManagersDataUtility.xml'
    ///  path='members/DataTableManager/*'/>
    public DataTableManager DataTableManager
    {
      get
      {
        var managerName = "DataTableManager";
        if (null == _DataTableManager
          || _ResetNames.Contains(managerName))
        {
          _DataTableManager
            = new DataTableManager(_DbServiceRef, _DataConfigName);
          ClearResetName(managerName);
        }
        return _DataTableManager;
      }
    }
    private DataTableManager _DataTableManager = null!;
    #endregion

    #region Class Data

    private readonly LJCArgError _ArgError;
    private LJCDbServiceRef _DbServiceRef = null!;
    private string _DataConfigName = null!;
    private List<string> _ResetNames = null!;
    #endregion
  }
}
