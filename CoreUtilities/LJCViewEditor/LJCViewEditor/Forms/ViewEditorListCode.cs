// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewEditorListCode.cs
using LJCDataAccessConfig;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace LJCViewEditor
{
  public partial class ViewEditorList : Form
  {
    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          ConfigureControls();
          RestoreControlValues();

          int comboIndex = ConfigCombo.FindString(DataConfigName);
          if (comboIndex > -1)
          {
            ConfigCombo.SelectedIndex = comboIndex;
          }

          if (NetString.HasValue(mStartupTableName))
          {
            comboIndex = TableCombo.FindString(mStartupTableName);
            if (comboIndex > -1)
            {
              TableCombo.SelectedIndex = comboIndex;
            }
          }
          break;

        case Change.Config:
          DataConfigName = ConfigCombo.Text;
          DataRetrieveTable();
          break;

        case Change.Table:
          ViewGridCode.DataRetrieve();

          if (StartupViewDataID > 0)
          {
            ViewData viewData = new ViewData()
            {
              ID = StartupViewDataID
            };
            ViewGridCode.RowSelect(viewData);
          }
          break;

        case Change.View:
          ColumnGridCode.DataRetrieve();
          JoinGridCode.DataRetrieve();
          FilterGridCode.DataRetrieve();
          OrderByGridCode.DataRetrieve();
          ViewGrid.LJCSetLastRow();
          break;

        case Change.Column:
          ColumnGrid.LJCSetLastRow();
          break;

        case Change.Join:
          JoinOnGridCode.DataRetrieve();
          JoinColumnGridCode.DataRetrieve();
          JoinGrid.LJCSetLastRow();
          break;

        case Change.JoinOn:
          JoinOnGrid.LJCSetLastRow();
          break;

        case Change.JoinColumn:
          JoinGrid.LJCSetLastRow();
          break;

        case Change.Filter:
          ConditionSetGridCode.DataRetrieve();
          ConditionGridCode.DataRetrieve();
          FilterGrid.LJCSetLastRow();
          break;

        case Change.ConditionSet:
          ConditionSetGrid.LJCSetLastRow();
          break;

        case Change.Condition:
          ConditionGrid.LJCSetLastRow();
          break;

        case Change.OrderBy:
          OrderByGrid.LJCSetLastRow();
          break;

        case Change.Data:
          DataGrid.LJCSetLastRow();
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Config,
      Table,
      View,
      Column,
      Join,
      JoinOn,
      JoinColumn,
      Filter,
      ConditionSet,
      Condition,
      OrderBy,
      Data
    }

    #region Item Change Support

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Start the Change processing.
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Gets or sets the ChangeTimer object.
    private ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Make sure lists scroll vertically and counter labels show.
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        MainSplit.SplitterWidth = 4;
        ViewSplit.SplitterWidth = 4;
        JoinSplit.SplitterWidth = 4;
        FilterSplit.SplitterWidth = 4;

        // MainSplit.
        int clientWidth = ClientSize.Width;
        MainSplit.Width = clientWidth - (MainSplit.Left * 2);

        // Modify MainSplit.Panel1, ViewSplit.
        ListHelper.SetPanelSplitControl(MainSplit.Panel1, ViewSplit
          , 0, -3, -3);

        // Modify ViewSplit.Panel1 controls.
        PanelControlsAdjust adjust = new PanelControlsAdjust(-6, -6, -6, -4);
        ListHelper.SetPanelControls(ViewSplit.Panel1, ViewHeading
          , null, ViewGrid, adjust);

        // *** View Tabs ***
        // Modify ViewSplit.Panel2 Tabs control.
        ListHelper.SetPanelTabControl(ViewSplit.Panel2, ViewTabs);

        // *** Join Tab ***
        ListHelper.SetPageSplitControl(JoinPage, JoinSplit, 0, -1, -1);

        // *** Join Tabs ***
        // Modify JoinSplit.Panel2 Tabs control.
        ListHelper.SetPanelTabControl(JoinSplit.Panel2, JoinTabs);

        // *** Filter Tab ***
        // Modify ViewSplit.Panel2, FilterPage, FilterSplit.
        ListHelper.SetPageSplitControl(FilterPage, FilterSplit, 0, -1, -1);

        // Modify FilterSplit.Panel1 controls.
        ListHelper.SetPanelControls(FilterSplit.Panel1, null, null, FilterGrid, -4);

        // ** Filter Tabs ***
        // Modify FilterSplit.Panel2 Tab control.
        ListHelper.SetPanelTabControl(FilterSplit.Panel2, ConditionSetTabs);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      SetupGridCode();
      LoadControlData();
      InitialControlValues();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\_ClassName_.xml";

      JoinSplit.Resize += JoinSplit_Resize;
      FilterSplit.Resize += FilterSplit_Resize;

      BackColor = BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesViewEditor.Instance;
      mSettings = values.StandardSettings;
      Text += $" - {mSettings.DataConfigName}";
      BeginColor = mSettings.BeginColor;
      DataConfigName = mSettings.DataConfigName;
      DbServiceRef = mSettings.DbServiceRef;
      mPrevConfigName = DataConfigName;
      //Managers = new ManagersDbView();
      //Managers.SetDbProperties(DbServiceRef, DataConfigName);
      Managers = values.Managers;

      try
      {
        DataDbView = new DataDbView(Managers);
      }
      catch (SystemException e)
      {
        ViewEditorCommon.CreateTables(e, DataConfigName);
        DataDbView = new DataDbView(Managers);
      }
    }

    // Load initial Control data.
    private void LoadControlData()
    {
      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      foreach (DataConfig dataConfig in dataConfigs)
      {
        ConfigCombo.Items.Add(dataConfig);
      }
    }

    // Restores the window size values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , mControlValuesFileName) as ControlValues;

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

          // Restore Splitter, Grid and other values.
          FormCommon.RestoreSplitDistance(MainSplit, ControlValues);
          FormCommon.RestoreSplitDistance(ViewSplit, ControlValues);
          FormCommon.RestoreSplitDistance(JoinSplit, ControlValues);
          FormCommon.RestoreSplitDistance(FilterSplit, ControlValues);

          ViewGrid.LJCRestoreColumnValues(ControlValues);
          ColumnGrid.LJCRestoreColumnValues(ControlValues);
          JoinGrid.LJCRestoreColumnValues(ControlValues);
          JoinOnGrid.LJCRestoreColumnValues(ControlValues);
          JoinColumnGrid.LJCRestoreColumnValues(ControlValues);
          FilterGrid.LJCRestoreColumnValues(ControlValues);
          ConditionSetGrid.LJCRestoreColumnValues(ControlValues);
          ConditionGrid.LJCRestoreColumnValues(ControlValues);
          OrderByGrid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      ViewGrid.LJCSaveColumnValues(controlValues);
      ColumnGrid.LJCSaveColumnValues(controlValues);
      JoinGrid.LJCSaveColumnValues(controlValues);
      JoinOnGrid.LJCSaveColumnValues(controlValues);
      JoinColumnGrid.LJCSaveColumnValues(controlValues);
      FilterGrid.LJCSaveColumnValues(controlValues);
      ConditionSetGrid.LJCSaveColumnValues(controlValues);
      ConditionGrid.LJCSaveColumnValues(controlValues);
      OrderByGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("MainSplit.SplitterDistance", 0, 0, 0
        , MainSplit.SplitterDistance);
      controlValues.Add("ViewSplit.SplitterDistance", 0, 0, 0
        , ViewSplit.SplitterDistance);
      controlValues.Add("JoinSplit.SplitterDistance", 0, 0, 0
        , JoinSplit.SplitterDistance);
      controlValues.Add("FilterSplit.SplitterDistance", 0, 0, 0
        , FilterSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ColumnGridCode = new ColumnGridCode(this);
      ConditionGridCode = new ConditionGridCode(this);
      ConditionSetGridCode = new ConditionSetGridCode(this);
      DataGridCode = new DataGridCode(this);
      FilterGridCode = new FilterGridCode(this);
      JoinColumnGridCode = new JoinColumnGridCode(this);
      JoinGridCode = new JoinGridCode(this);
      JoinOnGridCode = new JoinOnGridCode(this);
      OrderByGridCode = new OrderByGridCode(this);
      ViewGridCode = new ViewGridCode(this);
    }

    // Splitter is in a TabPage so Set values on first display.
    private void JoinSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (!mIsJoinSplitSet)
        {
          FormCommon.RestoreSplitDistance(JoinSplit, ControlValues);
        }
        mIsJoinSplitSet = true;
      }
    }
    private bool mIsJoinSplitSet;

    // Splitter is in a TabPage so Set values on first display.
    private void FilterSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (!mIsFilterSplitSet)
        {
          FormCommon.RestoreSplitDistance(FilterSplit, ControlValues);
        }
        mIsFilterSplitSet = true;
      }
    }
    private bool mIsFilterSplitSet;

    //Gets or sets the ControlValues item.
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew;
      bool enableEdit;

      enableNew = TableCombo.SelectedIndex != -1;
      enableEdit = ViewGrid.CurrentRow != null;
      FormCommon.SetMenuState(ViewMenu, enableNew, enableEdit);
      ViewTitle.Enabled = true;
      ViewFileEdit.Enabled = true;
      ViewMenuHelp.Enabled = true;
      ViewMenuAbout.Enabled = true;

      enableNew = ViewGrid.CurrentRow != null;
      enableEdit = ColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(ColumnMenu, enableNew, enableEdit);
      ColumnTitle.Enabled = true;
      ColumnMenuAdd.Enabled = enableNew;
      ColumnMenuHelp.Enabled = true;

      enableEdit = JoinGrid.CurrentRow != null;
      FormCommon.SetMenuState(JoinMenu, enableNew, enableEdit);
      JoinTitle.Enabled = true;
      JoinMenuHelp.Enabled = true;

      enableEdit = FilterGrid.CurrentRow != null;
      FormCommon.SetMenuState(FilterMenu, enableNew, enableEdit);
      FilterTitle.Enabled = true;
      FilterMenuHelp.Enabled = true;

      enableEdit = OrderByGrid.CurrentRow != null;
      FormCommon.SetMenuState(OrderByMenu, enableNew, enableEdit);
      OrderByTitle.Enabled = true;
      OrderByMenuHelp.Enabled = true;

      enableNew = JoinGrid.CurrentRow != null;
      enableEdit = JoinOnGrid.CurrentRow != null;
      FormCommon.SetMenuState(JoinOnMenu, enableNew, enableEdit);
      JoinOnTitle.Enabled = true;
      JoinOnMenuHelp.Enabled = true;

      enableEdit = JoinColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(JoinColumnMenu, enableNew, enableEdit);
      JoinColumnTitle.Enabled = true;
      JoinColumnHelp.Enabled = true;

      enableNew = FilterGrid.CurrentRow != null
        && ConditionSetGrid.Rows.Count < 1;
      enableEdit = ConditionSetGrid.CurrentRow != null;
      FormCommon.SetMenuState(ConditionSetMenu, enableNew, enableEdit);
      ConditionSetTitle.Enabled = true;
      ConditionSetHelp.Enabled = true;

      enableNew = ConditionSetGrid.CurrentRow != null;
      enableEdit = ConditionGrid.CurrentRow != null;
      FormCommon.SetMenuState(ConditionMenu, enableNew, enableEdit);
      ConditionTitle.Enabled = true;
      ConditionMenuHelp.Enabled = true;

      enableNew = DataGrid.CurrentRow != null;
      enableEdit = DataGrid.CurrentRow != null;
      FormCommon.SetMenuState(DataMenu, enableNew, enableEdit);
      DataTitle.Enabled = true;
    }

    // Sets the View tab initial focus control.
    private void ViewSetFocusTab(TabPage tabPage)
    {
      switch (tabPage.Name)
      {
        case "ColumnPage":
          ColumnGrid.Select();
          break;

        case "JoinPage":
          JoinGrid.Select();
          break;

        case "FilterPage":
          FilterGrid.Select();
          break;

        case "OrderByPage":
          OrderByGrid.Select();
          break;
      }
    }

    // Sets the View tab initial focus control.
    private void JoinSetFocusTab(TabPage tabPage)
    {
      switch (tabPage.Name)
      {
        case "JoinOnPage":
          JoinOnGrid.Select();
          break;

        case "JoinColumnPage":
          JoinColumnGrid.Select();
          break;
      }
    }

    // Sets the View tab initial focus control.
    private void FilterSetFocusTab(TabPage tabPage)
    {
      switch (tabPage.Name)
      {
        case "ConditionSetPage":
          ConditionSetGrid.Select();
          break;

        case "ConditionPage":
          ConditionGrid.Select();
          break;
      }
    }
    #endregion

    #region Properties

    // The Managers object.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the ViewColumnCode value.
    private ColumnGridCode ColumnGridCode { get; set; }

    // Gets or sets the ConditionGridCode value.
    private ConditionGridCode ConditionGridCode { get; set; }

    // Gets or sets the ConditionSetGridCode value.
    private ConditionSetGridCode ConditionSetGridCode { get; set; }

    // Gets or sets the DataGridCode value.
    private DataGridCode DataGridCode { get; set; }

    // Gets or sets the JoinGridCode value.
    private FilterGridCode FilterGridCode { get; set; }

    // Gets or sets the JoinColumnGridCode value.
    private JoinColumnGridCode JoinColumnGridCode { get; set; }

    // Gets or sets the JoinGridCode value.
    private JoinGridCode JoinGridCode { get; set; }

    // Gets or sets the JoinGridCode value.
    private JoinOnGridCode JoinOnGridCode { get; set; }

    // Gets or sets the OrderByGridCode value.
    private OrderByGridCode OrderByGridCode { get; set; }

    // Gets or sets the ViewGridCode value.
    private ViewGridCode ViewGridCode { get; set; }
    #endregion
  }
}
