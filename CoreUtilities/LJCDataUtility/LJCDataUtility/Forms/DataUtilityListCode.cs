// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityListCode.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The list form code.
  internal partial class DataUtilityList : Form
  {
    #region Get Value Methods

    // *** DataColumn ***

    // Gets the current Column Grid row.
    internal LJCGridRow DataColumnCurrent()
    {
      LJCGridRow retRow = ColumnGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal int DataColumnID(LJCGridRow row = null)
    {
      int retColumnID = 0;

      if (null == row)
      {
        row = DataColumnCurrent();
      }
      if (row is LJCGridRow
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnID = row.LJCGetInt32(DataUtilColumn.ColumnID);
      }
      return retColumnID;
    }

    // Gets the selected row Name.
    internal string DataColumnName(LJCGridRow row = null)
    {
      string retColumnName = null;

      if (null == row)
      {
        row = DataColumnCurrent();
      }
      if (row is LJCGridRow
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnName = row.LJCGetString(DataUtilColumn.ColumnName);
      }
      return retColumnName;
    }

    // *** DataKey ***

    // Gets the current Key Grid row.
    internal LJCGridRow DataKeyCurrent()
    {
      LJCGridRow retRow = KeyGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal int DataKeyID(LJCGridRow row = null)
    {
      int retKeyID = 0;

      if (null == row)
      {
        row = DataKeyCurrent();
      }
      if (row is LJCGridRow
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeyID = row.LJCGetInt32(DataKey.ColumnID);
      }
      return retKeyID;
    }

    // Gets the selected row Name.
    internal string DataKeyName(LJCGridRow row = null)
    {
      string retKeyName = null;

      if (null == row)
      {
        row = DataKeyCurrent();
      }
      if (row is LJCGridRow
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeyName = row.LJCGetString(DataKey.ColumnName);
      }
      return retKeyName;
    }

    // *** DataModule ***

    // Gets the current Module Grid row.
    internal LJCGridRow DataModuleCurrent()
    {
      LJCGridRow retRow = ModuleGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal int DataModuleID(LJCGridRow row = null)
    {
      int retModuleID = 0;

      if (null == row)
      {
        row = DataModuleCurrent();
      }
      if (row is LJCGridRow
        && "ModuleGrid" == row.DataGridView.Name)
      {
        retModuleID = row.LJCGetInt32(DataModule.ColumnID);
      }
      return retModuleID;
    }

    // Gets the selected row Name.
    internal string DataModuleName(LJCGridRow row = null)
    {
      string retModuleName = null;

      if (null == row)
      {
        row = DataModuleCurrent();
      }
      if (row is LJCGridRow
        && "ModuleGrid" == row.DataGridView.Name)
      {
        retModuleName = row.LJCGetString(DataModule.ColumnName);
      }
      return retModuleName;
    }

    // *** DataUtilTable ***

    // Gets the current Table Grid row.
    internal LJCGridRow DataTableCurrent()
    {
      LJCGridRow retRow = TableGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal int DataTableID(LJCGridRow row = null)
    {
      int retTableID = 0;

      if (row == null)
      {
        row = DataTableCurrent();
      }
      if (row is LJCGridRow
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableID = row.LJCGetInt32(DataUtilTable.ColumnID);
      }
      return retTableID;
    }

    // Gets the selected row Name.
    internal string DataTableName(LJCGridRow row = null)
    {
      string retTableName = null;

      if (row == null)
      {
        row = DataTableCurrent();
      }
      if (row is LJCGridRow
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableName = row.LJCGetString(DataUtilTable.ColumnName);
      }
      return retTableName;
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      SetupGridCode();
      InitialControlValues();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (NetString.HasValue(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      Managers = values.Managers;
      Settings = values.StandardSettings;
      Text += $" - {Settings.DataConfigName}";
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ModuleGridCode = new DataModuleGridCode(this);
      TableGridCode = new DataTableGridCode(this);
      ColumnGridCode = new DataColumnGridCode(this);
      KeyGridCode = new DataKeyGridCode(this);
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      ControlValuesFileName = @"ControlValues\DataUtility.xml";
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      ModuleGridCode.SetupGrid();
      TableGridCode.SetupGrid();
      ColumnGridCode.SetupGrid();
      KeyGridCode.SetupGrid();
    }
    #endregion

    #region Private Methods

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(ControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , ControlValuesFileName) as ControlValues;

        if (ControlValues != null)
        {
          // Restore Window values.
          controlValue = ControlValues.LJCSearchName(Name);
          if (controlValue != null)
          {
            Left = controlValue.Left;
            Top = controlValue.Top;
            Width = controlValue.Width;
            Height = controlValue.Height;
          }

          // Restore Grid column sizes.
          ModuleGrid.LJCRestoreColumnValues(ControlValues);
          TableGrid.LJCRestoreColumnValues(ControlValues);
          ColumnGrid.LJCRestoreColumnValues(ControlValues);
          KeyGrid.LJCRestoreColumnValues(ControlValues);

          // Restore Font sizes.
          FormCommon.RestoreTabsFontSize(MainTabs, ControlValues);
          ModuleGrid.LJCRestoreFontSize(ControlValues);
          TableGrid.LJCRestoreFontSize(ControlValues);
          ColumnGrid.LJCRestoreFontSize(ControlValues);
          KeyGrid.LJCRestoreFontSize(ControlValues);

          // Restore Menu Font sizes.
          FormCommon.RestoreMenuFontSize(ModuleMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(TableMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(ColumnMenu, ControlValues);
          FormCommon.RestoreMenuFontSize(KeyMenu, ControlValues);

          InfoValue = ControlValues.LJCSearchName("AddProc");
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Window values.
      controlValues.Add(Name, Left, Top
        , Width, Height);

      // Save Grid column sizes.
      ModuleGrid.LJCSaveColumnValues(controlValues);
      TableGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      KeyGrid.LJCSaveColumnValues(controlValues);

      // Save Font sizes.
      FormCommon.SaveTabFontSize(MainTabs, controlValues);
      ModuleGrid.LJCSaveFontSize(controlValues);
      TableGrid.LJCSaveFontSize(controlValues);
      ColumnGrid.LJCSaveFontSize(controlValues);
      KeyGrid.LJCSaveFontSize(controlValues);

      // Save Menu Font sizes.
      FormCommon.SaveMenuFontSize(ModuleMenu, controlValues);
      FormCommon.SaveMenuFontSize(TableMenu, controlValues);
      FormCommon.SaveMenuFontSize(ColumnMenu, controlValues);
      FormCommon.SaveMenuFontSize(KeyMenu, controlValues);

      controlValues.Add(InfoValue);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , ControlValuesFileName);
    }
    #endregion

    #region Properties

    // The ControlValues file name.
    internal string ControlValuesFileName { get; set; }

    internal ControlValue InfoValue { get; set; }

    // The Managers object.
    internal ManagersDataUtility Managers { get; set; }

    // Grid Code
    private DataColumnGridCode ColumnGridCode { get; set; }
    private DataKeyGridCode KeyGridCode { get; set; }
    private DataModuleGridCode ModuleGridCode { get; set; }
    private DataTableGridCode TableGridCode { get; set; }

    // Gets or sets the ControlValues object.
    private ControlValues ControlValues { get; set; }

    private StandardUISettings Settings { get; set; }
    #endregion
  }
}
