// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RenameTable.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Data;
using System.Windows.Forms;

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
      var parentID = ParentObject.DataTableID(out long parentSiteID);
      var keyManager = Managers.DataKeyManager;
      var dataKeys = keyManager.Load();
      if (NetCommon.HasItems(dataKeys))
      {
        var parentTableName = ParentObject.DataTableName();
        string dbName = ParentObject.DataConfigCombo.Text;

        var connectionType = ParentObject.ConnectionType;
        if (!NetString.HasValue(connectionType))
        {
          // Default value.
          connectionType = "SQLServer";
        }

        // Testing
        if (DialogResult.Yes == MessageBox.Show("Use MySQL?", "MySQL"
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          connectionType = "MySQL";
        }

        string showText = null;
        switch (connectionType.ToLower())
        {
          case "mysql":
            var myProc = new MyProcBuilder(ParentObject, dbName
              , parentTableName);
            showText = myProc.RenameTableSQL(parentID, parentSiteID, dataKeys);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.RenameTableSQL(parentID, parentSiteID, dataKeys);
            break;
        }

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
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
