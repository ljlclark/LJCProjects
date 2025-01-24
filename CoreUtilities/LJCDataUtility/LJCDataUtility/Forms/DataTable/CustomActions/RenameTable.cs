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
      ParentList = parentList;
      Managers = ParentList.Managers;
    }
    #endregion

    #region Methods

    // Generates the Rename Table SQL.
    internal void RenameTableSQL()
    {
      var tableID = ParentList.DataTableID();
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var tableName = ParentList.DataTableName();
        string dbName = ParentList.DataConfigCombo.Text;
        var proc = new ProcBuilder(dbName, tableName);
        var value = proc.RenameTableSQL(tableID, dataKeys);

        var infoValue = ParentList.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Rename Table SQL", infoValue);
        ParentList.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
