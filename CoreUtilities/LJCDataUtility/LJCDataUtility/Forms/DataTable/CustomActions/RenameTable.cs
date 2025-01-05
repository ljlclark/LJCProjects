// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RenameTable.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;

namespace LJCDataUtility
{
  // Provides methods to generate the RenameTable SQL.
  internal class RenameTable
  {
    #region Constructors

    // Initializes an object instance.
    public RenameTable(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Managers = Parent.Managers;
    }
    #endregion

    #region Methods

    // Generates the Rename Table SQL.
    internal void RenameTableSQL()
    {
      string dbName = "LJCDataUtility";
      var tableID = Parent.DataTableID();
      var keyManager = Managers.DataKeyManager;
      //var keyColumns = keyManager.ParentKey(tableID);
      //var dataKeys = keyManager.Load(keyColumns);
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var tableName = Parent.DataTableName();
        var proc = new ProcBuilder(dbName, tableName);
        var value = proc.RenameTableSQL(tableID, dataKeys);

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Rename Table SQL", infoValue);
        Parent.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
