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
    internal RenameTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the Rename Table SQL.
    internal void RenameTableSQL()
    {
      var tableID = ParentObject.DataTableID();
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var tableName = ParentObject.DataTableName();
        string dbName = ParentObject.DataConfigCombo.Text;
        var proc = new ProcBuilder(ParentObject, dbName, tableName);
        var value = proc.RenameTableSQL(tableID, dataKeys);

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Rename Table SQL", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
