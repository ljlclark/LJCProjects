// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateTable.cs
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides methods to generate the CreateTable procedure.
  internal class CreateTable
  {
    #region Constructors

    // Initializes an object instance.
    internal CreateTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      Managers = ParentObject.Managers;
    }
    #endregion

    #region Methods

    // Generates the CreateTable procedure.
    internal void CreateTableProc()
    {
      var parentTableID = ParentObject.DataTableID();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var dataColumns = Managers.TableDataColumns(parentTableID
        , orderByNames);

      if (NetCommon.HasItems(dataColumns))
      {
        var parentTableName = ParentObject.DataTableName();
        // *** Begin *** Add 2/7/25
        var configCombo = ParentObject.DataConfigCombo;
        var dataConfig = configCombo.SelectedItem as DataConfig;
        var dbName = dataConfig.Database;
        // *** End   ***

        // *** Begin *** Add #MySQL 1/29/25
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
            var myProc = new MyProcBuilder(ParentObject, dbName, parentTableName);
            showText = myProc.CreateTableProc(dataColumns);
            break;

          case "sqlserver":
            var proc = new ProcBuilder(ParentObject, dbName, parentTableName);
            showText = proc.CreateTableProc(dataColumns);
            break;
        }
        // *** End   ***

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(showText
          , "Create Table Procedure", infoValue);
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
