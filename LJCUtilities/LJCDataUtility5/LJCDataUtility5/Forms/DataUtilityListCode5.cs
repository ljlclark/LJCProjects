// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityListCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  internal partial class DataUtilityList : Form
  {
    #region Setup Methods

    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;

      // *** Testing ***
      ColumnsSplit.Panel2Collapsed = true;

      InitializeClassData();
      SetupControlCode();
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
      if (LJC.HasText(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      ConnectionType = values.ConnectionType;
      DbGroupId = values.DbGroupId;
      Managers = values.Managers;
      //Settings = values.StandardSettings;
      //Text += $" - {Settings.DataConfigName}";
      Text += $" - {values.DataConfigName}";
    }

    // Setup the grid code references.
    private void SetupControlCode()
    {
      //ModuleComboCode = new DataModuleComboCode(this);
      TableGridCode = new DataTableGridCode(this);
      ColumnGridCode = new DataColumnGridCode(this);
      KeyGridCode = new DataKeyGridCode(this);
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      LJCNetFile.CreateFolder("ExportFiles");
      LJCNetFile.CreateFolder("ControlValues");
      ControlValuesFileName = @"ControlValues\DataUtility.xml";
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      TableGridCode.SetupGrid();
      ColumnGridCode.SetupGrid();
      KeyGridCode.SetupGrid();
    }
    #endregion

    #region Other Methods

    // Restores the control values.
    private void RestoreControlValues()
    {
      //ControlValue controlValue;

      if (File.Exists(ControlValuesFileName))
      {
        ControlValues = LJC.XmlDeserialize(typeof(ControlValues)
          , ControlValuesFileName) as ControlValues;

        if (ControlValues != null)
        {
          // Restore Window values.
          var controlValue = ControlValues.LJCSearchName(Name);
          if (controlValue != null)
          {
            Left = controlValue.Left;
            Top = controlValue.Top;
            Width = controlValue.Width;
            Height = controlValue.Height;
          }

          // Restore Grid column sizes.
          TableGrid.LJCRestoreColumnValues(ControlValues);
          ColumnGrid.LJCRestoreColumnValues(ControlValues);
          KeyGrid.LJCRestoreColumnValues(ControlValues);

          // Restore Font sizes.
          FormCommon.RestoreTabsFontSize(ColumnTabs, ControlValues);
          //TableGrid.LJCRestoreFontSize(ControlValues);
          //ColumnGrid.LJCRestoreFontSize(ControlValues);
          //KeyGrid.LJCRestoreFontSize(ControlValues);

          // Restore Menu Font sizes.
          //FormCommon.RestoreMenuFontSize(ModuleMenu, ControlValues);
          //FormCommon.RestoreMenuFontSize(TableMenu, ControlValues);
          //FormCommon.RestoreMenuFontSize(ColumnMenu, ControlValues);
          //FormCommon.RestoreMenuFontSize(KeyMenu, ControlValues);

          FormCommon.RestoreSplitDistance(MainSplit, ControlValues);
          InfoValue = ControlValues.LJCSearchName("AddProc");
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues
      {
        // Save Window values.
        { Name, Left, Top, Width, Height },
      };

      // Save Grid column sizes.
      TableGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      KeyGrid.LJCSaveColumnValues(controlValues);

      // Save Font sizes.
      FormCommon.SaveTabFontSize(ColumnTabs, controlValues);
      //TableGrid.LJCSaveFontSize(controlValues);
      //ColumnGrid.LJCSaveFontSize(controlValues);
      //KeyGrid.LJCSaveFontSize(controlValues);

      // Save Menu Font sizes.
      //FormCommon.SaveMenuFontSize(ModuleMenu, controlValues);
      //FormCommon.SaveMenuFontSize(TableMenu, controlValues);
      //FormCommon.SaveMenuFontSize(ColumnMenu, controlValues);
      //FormCommon.SaveMenuFontSize(KeyMenu, controlValues);

      controlValues.Add("MainSplit.SplitterDistance", 0, 0, 0
        , MainSplit.SplitterDistance);
      if (InfoValue != null)
      {
        controlValues.Add(InfoValue);
      }

      LJC.XmlSerialize(controlValues.GetType(), controlValues, null
        , ControlValuesFileName);
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(MouseEventArgs e)
    {
      var tabPage = ColumnTabs.LJCGetTabPage(e);
      if (tabPage != null)
      {
        switch (tabPage.Name)
        {
          case "ColumnPage":
            ColumnGrid.Select();
            break;
          case "KeyPage":
            KeyGrid.Select();
            break;
        }
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the MouseDown event.
    private void ColumnTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ColumnTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }
    #endregion

    #region Properties

    internal short DbGroupId { get; set; }

    // Gets or sets the connection type value.
    internal string ConnectionType { get; set; } = null!;

    // Gets or sets the ControlValues file name.
    internal string ControlValuesFileName { get; set; }

    // Gets or sets the InfoValue item.
    internal ControlValue? InfoValue { get; set; }

    // Gets or sets the Managers object.
    internal ManagersDataUtility Managers { get; set; } = null!;

    // Gets or sets the configuration settings.
    //internal LJCStandardUISettings Settings { get; set; }

    // Gets or sets the DataColumnGridCode reference.
    private DataColumnGridCode ColumnGridCode { get; set; }

    // Gets or sets the DataTableGridCode reference.
    internal DataTableGridCode TableGridCode { get; set; } = null!;

    // Gets or sets the KeyGridCode reference.
    private DataKeyGridCode KeyGridCode { get; set; }

    // Gets or sets the ModuleComboCode reference.
    //private DataModuleComboCode ModuleComboCode { get; set; }

    private ControlValues? ControlValues { get; set; }
    #endregion
  }

  /// <summary></summary>
  internal enum KeyType : short
  {
    Primary = 1,
    Unique,
    Foreign,
    Table
  }
}
