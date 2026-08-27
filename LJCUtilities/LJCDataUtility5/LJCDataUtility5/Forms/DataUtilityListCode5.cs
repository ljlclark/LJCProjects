// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityListCode5.cs
using LJCControls5;
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // The list form.
  internal partial class DataUtilityList : Form
  {
    #region Setup Methods

    // Initializes the window controls.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;

      // Action Event Handlers
      ColumnTabMove.Click += ColumnTabMove_Click;
      KeyTabMove.Click += KeyTabMove_Click;

      // Control Event Handlers
      ColumnTabs.MouseDown += ColumnTabs_MouseDown;
      TileTabs.MouseDown += TileTabs_MouseDown;

      InitializeClassData();
      SetupControlCode();
      ControlSetup();
      InitialControlValues();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    // Initializes the Class Data.
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
      ModuleComboCode = new DataModuleComboCode(this, DbGroupId);
      ConfigComboCode = new DataConfigComboCode(this, DbGroupId);
      TableGridCode = new DataTableGridCode(this, DbGroupId);
      ColumnGridCode = new DataColumnGridCode(this, DbGroupId);
      KeyGridCode = new DataKeyGridCode(this, DbGroupId);
    }

    // Initial Control setup.
    private void ControlSetup()
    {
      // Provides additional Drag features between split LJCTabControls.
      var _ = new LJCPanelManager(ColumnsSplit, ColumnTabs, TileTabs);
    }

    // Sets the initial Control values.
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

          FormCommon.RestoreSplitDistance(MainSplit, ControlValues);
          InfoValue = ControlValues.LJCSearchName("AddProc");
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      var controlValues = new ControlValues
      {
        // Save Window values.
        { Name, Left, Top, Width, Height },
      };

      // Save Grid column sizes.
      TableGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      KeyGrid.LJCSaveColumnValues(controlValues);

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

    #region Action Event Handlers

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void ColumnTabMove_Click(object? sender, EventArgs e)
    {
      ColumnTabs.LJCMoveTabPageRight(TileTabs, ColumnsSplit);
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void KeyTabMove_Click(object? sender, EventArgs e)
    {
      TileTabs.LJCMoveTabPageLeft(ColumnTabs, ColumnsSplit);
    }
    #endregion

    #region Control Event Handlers

    // Handles the MouseDown event.
    private void ColumnTabs_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ColumnTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }
    #endregion

    #region Properties

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the connection type value.
    internal string ConnectionType { get; set; } = null!;

    // Gets or sets the ControlValues file name.
    internal string ControlValuesFileName { get; set; } = null!;

    // Gets or sets the InfoValue item.
    internal ControlValue? InfoValue { get; set; }

    // Gets or sets the Managers object.
    internal ManagersDataUtility Managers { get; set; } = null!;

    // Gets or sets the configuration settings.
    //internal LJCStandardUISettings Settings { get; set; }

    // Gets or sets the DataColumnGridCode reference.
    internal DataColumnGridCode ColumnGridCode { get; set; } = null!;

    // Gets or sets the ModuleComboCode reference.
    internal DataConfigComboCode ConfigComboCode { get; set; } = null!;

    // Gets or sets the ModuleComboCode reference.
    internal DataModuleComboCode ModuleComboCode { get; set; } = null!;

    // Gets or sets the DataTableGridCode reference.
    internal DataTableGridCode TableGridCode { get; set; } = null!;

    // Gets or sets the KeyGridCode reference.
    private DataKeyGridCode KeyGridCode { get; set; } = null!;

    // Gets or sets the control values reference.
    private ControlValues? ControlValues { get; set; }
    #endregion
  }

  // The table key types.
  internal enum KeyType : short
  {
    Primary = 1,
    Unique,
    Foreign,
    Table
  }
}
