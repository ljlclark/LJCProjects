// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewEditorList.cs
using LJCDataAccessConfig;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace LJCViewEditor
{
  // The View Editor form.
  /// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  public partial class ViewEditorList : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewEditorListC/*' file='Doc/ViewEditorList.xml'/>
    public ViewEditorList(string tableName = null, bool splash = true)
    {
      Cursor = Cursors.WaitCursor;
      // *** Begin *** Add - 8/24/21
      mStartupTableName = tableName;
      if (splash)
      {
        ViewEditSplash splashDialog = new ViewEditSplash(true);
        splashDialog.ShowDialog();
      }
      // *** End *** Add - 8/24/21
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "ViewEditor.chm";
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ViewEditorList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }
    #endregion

    #region Data Methods

    // Retrieves the combo items.
    private void DataRetrieveTable()
    {
      DbResult dbResult;

      Cursor = Cursors.WaitCursor;
      TableCombo.Items.Clear();

      // Reset the DataConfig dependent items.
      ResetData();

      SqlTableManager sqlTableManager = new SqlTableManager(DbServiceRef
        , DataConfigName);

      dbResult = sqlTableManager.DataManager.GetTableNames();
      if (DbResult.HasRows(dbResult))
      {
        SqlTables dataRecords = sqlTableManager.CreateCollection(dbResult);
        if (dataRecords != null)
        {
          foreach (SqlTable dataRecord in dataRecords)
          {
            if (dataRecord.Name != null)
            {
              TableCombo.Items.Add(dataRecord);
            }
          }
        }
      }
      Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    private void ResetData()
    {
      if (DataConfigName != mPrevConfigName)
      {
        mPrevConfigName = DataConfigName;
        if (DbServiceRef.DbDataAccess != null)
        {
          DbServiceRef.DbDataAccess = new DbDataAccess(DataConfigName);
        }

        try
        {
          ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
        }
        catch (SystemException e)
        {
          ViewEditorCommon.CreateTables(e, DataConfigName);
          ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
        }

        ViewGridClass.ResetData();
        ColumnGridClass.ResetData();
        JoinGridClass.ResetData();
        JoinOnGridClass.ResetData();
        JoinColumnGridClass.ResetData();
        FilterGridClass.ResetData();
        ConditionSetGridClass.ResetData();
        ConditionGridClass.ResetData();
        OrderByGridClass.ResetData();
      }
    }
    #endregion

    #region Action Methods

    #region View Data

    // Creates and returns the View DbRequest object.
    internal DbRequest DoGetViewRequest()
    {
      DbRequest retValue = null;

      if (ViewGrid.CurrentRow is LJCGridRow gridRow)
      {
        // Get View Request.
        string tableName = TableCombo.Text;
        string viewDataName = gridRow.LJCGetString(ViewData.ColumnName);

        retValue = ViewHelper.GetViewRequest(tableName, viewDataName);
      }
      return retValue;
    }
    #endregion
    #endregion

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

          // *** Begin *** Add - 8/24/21
          if (NetString.HasValue(mStartupTableName))
          {
            comboIndex = TableCombo.FindString(mStartupTableName);
            if (comboIndex > -1)
            {
              TableCombo.SelectedIndex = comboIndex;
            }
          }
          // *** End *** Add - 8/24/21
          break;

        case Change.Table:
          ViewGridClass.DataRetrieveViewData();

          // *** Begin *** Add - 8/24/21
          if (StartupViewDataID > 0)
          {
            ViewData viewData = new ViewData()
            {
              ID = StartupViewDataID
            };
            ViewGridClass.RowSelectViewData(viewData);
          }
          // *** End *** Add - 8/24/21
          break;

        case Change.View:
          ColumnGridClass.DataRetrieveColumn();
          JoinGridClass.DataRetrieveJoin();
          FilterGridClass.DataRetrieveFilter();
          OrderByGridClass.DataRetrieveOrderBy();
          ViewGrid.LJCSetLastRow();
          break;

        case Change.Column:
          ColumnGrid.LJCSetLastRow();
          break;

        case Change.Join:
          JoinOnGridClass.DataRetrieveJoinOn();
          JoinColumnGridClass.DataRetrieveJoinColumn();
          JoinGrid.LJCSetLastRow();
          break;

        case Change.JoinOn:
          JoinOnGrid.LJCSetLastRow();
          break;

        case Change.JoinColumn:
          JoinGrid.LJCSetLastRow();
          break;

        case Change.Filter:
          ConditionSetGridClass.DataRetrieveConditionSet();
          ConditionGridClass.DataRetrieveCondition();
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
      SetAppConfigValues();

      // Initialize Class Data
      try
      {
        ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
      }
      catch (SystemException e)
      {
        ViewEditorCommon.CreateTables(e, DataConfigName);
        ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
      }

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

    // Sets the Application Configuration values.
    private void SetAppConfigValues()
    {
      var values = ValuesViewEditor.Instance;
      mSettings = values.StandardSettings;
      DataConfigName = mSettings.DataConfigName;
      DbServiceRef = mSettings.DbServiceRef;
      mPrevConfigName = mSettings.DataConfigName;
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      ColumnGridClass = new ColumnGridClass(this);
      ConditionGridClass = new ConditionGridClass(this);
      ConditionSetGridClass = new ConditionSetGridClass(this);
      DataGridClass = new DataGridClass(this);
      FilterGridClass = new FilterGridClass(this);
      JoinColumnGridClass = new JoinColumnGridClass(this);
      JoinGridClass = new JoinGridClass(this);
      JoinOnGridClass = new JoinOnGridClass(this);
      OrderByGridClass = new OrderByGridClass(this);
      ViewGridClass = new ViewGridClass(this);
    }

    // Splitter is in a TabPage so Set values on first display.
    private void JoinSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (false == mIsJoinSplitSet)
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
        if (false == mIsFilterSplitSet)
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
    #endregion

    #region Action Event Handlers

    // Closes the menus.
    private void DoMenuClose()
    {
      mAllowClose = true;
      ViewMenu.Close();
      JoinMenu.Close();
      JoinOnMenu.Close();
      JoinColumnMenu.Close();
      FilterMenu.Close();
      ConditionSetMenu.Close();
      ConditionMenu.Close();
      DataMenu.Close();
    }

    // Handles the menu keys.
    private void Menu_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        DoMenuClose();
      }
    }

    // Handles the Menu Closing event.
    private void Menu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
    {
      if (false == mAllowClose)
      {
        e.Cancel = true;
      }
      mAllowClose = false;
    }
    private bool mAllowClose;

    #region Form Controls

    // Handles the Menu Title click.
    private void DataConfigTitle_Click(object sender, EventArgs e)
    {
      DataConfigMenu.Focus();
    }

    // Shows the Help page.
    private void DataConfigHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"DataConfig.html");
    }

    // Handles the Menu Title click.
    private void TableTitle_Click(object sender, EventArgs e)
    {
      TableMenu.Focus();
    }

    // Shows the Help page.
    private void TableHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Table.html");
    }
    #endregion

    #region View Data

    // Handles the Menu Title click.
    private void ViewTitle_Click(object sender, EventArgs e)
    {
      ViewMenu.Focus();
    }

    // Calls the New method.
    private void ViewMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoNewViewData();
    }

    // Calls the Edit method.
    private void ViewMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoEditViewData();
    }

    // Calls the Delete method.
    private void ViewMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoDeleteViewData();
    }

    // Calls the Refresh method.
    private void ViewMenuRefresh_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoRefreshViewData();
    }

    // Allows for display and edit of a file.
    private void ViewFileEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      FormCommon.ShellFile("NotePad.exe");
    }

    // Shows the Data.
    private void ViewMenuShowData_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoShowData();
    }

    // Shows the SQL statement.
    private void ViewMenuShowSQL_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoShowSQL();
    }

    // Show the DbRequest code.
    private void ViewMenuShowCode_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.ShowCode();
    }

    // Performs the Close function.
    private void ViewMenuExit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      SaveControlValues();
      Close();
    }

    // Shows the Help page.
    private void ViewMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"View\ViewList.html");
    }

    // Shows the About box.
    private void ViewMenuAbout_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewEditSplash splash = new ViewEditSplash();
      splash.ShowDialog();
    }
    #endregion

    #region Column

    // Handles the Menu Title click.
    private void ColumnTitle_Click(object sender, EventArgs e)
    {
      ColumnMenu.Focus();
    }

    // Calls the AddAll method.
    private void ColumnMenuAdd_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ColumnGridClass.DoAddAll(TableCombo.Text.Trim());
    }

    // Calls the New method.
    private void ColumnMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ColumnGridClass.DoNewViewColumn();
    }

    // Calls the Edit method.
    private void ColumnMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ColumnGridClass.DoEditViewColumn();
    }

    // Calls the Delete method.
    private void ColumnMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ColumnGridClass.DoDeleteViewColumn();
    }

    // Calls the Refresh method.
    private void ColumnMenuRefresh_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ColumnGridClass.DoRefreshViewColumn();
    }

    // Shows the Help page.
    private void ColumnMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Column\ColumnList.html");
    }
    #endregion

    #region Join

    // Handles the Menu Title click.
    private void JoinTitle_Click(object sender, EventArgs e)
    {
      JoinMenu.Focus();
    }

    // Calls the New method.
    private void JoinMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinGridClass.DoNewViewJoin();
    }

    // Calls the Edit method.
    private void JoinMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinGridClass.DoEditViewJoin();
    }

    // Calls the Delete method.
    private void JoinMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinGridClass.DoDeleteViewJoin();
    }

    // Shows the Help page.
    private void JoinMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinList.html");
    }
    #endregion

    #region JoinOn

    // Handles the Menu Title click.
    private void JoinOnTitle_Click(object sender, EventArgs e)
    {
      JoinOnMenu.Focus();
    }

    // Calls the New method.
    private void JoinOnMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinOnGridClass.DoNewViewJoinOn();
    }

    // Calls the Edit method.
    private void JoinOnMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinOnGridClass.DoEditViewJoinOn();
    }

    // Calls the Delete method.
    private void JoinOnMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinOnGridClass.DoDeleteViewJoinOn();
    }

    // Shows the Help page.
    private void JoinOnMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinOnList.html");
    }
    #endregion

    #region JoinColumn

    // Handles the Menu Title click.
    private void JoinColumnTitle_Click(object sender, EventArgs e)
    {
      JoinColumnMenu.Focus();
    }

    // Calls the New method.
    private void JoinColumnNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinColumnGridClass.DoNewViewJoinColumn();
    }

    // Calls the Edit method.
    private void JoinColumnEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinColumnGridClass.DoEditViewJoinColumn();
    }

    // Calls the Delete method.
    private void JoinColumnDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      JoinColumnGridClass.DoDeleteViewJoinColumn();
    }

    // Shows the Help page.
    private void JoinColumnHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinColumnList.html");
    }
    #endregion

    #region Filter

    // Handles the Menu Title click.
    private void FilterTitle_Click(object sender, EventArgs e)
    {
      FilterMenu.Focus();
    }

    // Calls the New method.
    private void FilterMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      FilterGridClass.DoNewViewFilter();
    }

    // Calls the Edit method.
    private void FilterMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      FilterGridClass.DoEditViewFilter();
    }

    // Calls the Delete method.
    private void FilterMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      FilterGridClass.DoDeleteViewFilter();
    }

    // Shows the Help page.
    private void FilterMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\FilterList.html");
    }
    #endregion

    #region ConditionSet

    // Handles the Menu Title click.
    private void ConditionSetTitle_Click(object sender, EventArgs e)
    {
      ConditionSetMenu.Focus();
    }

    // Calls the New method.
    private void ConditionSetNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionSetGridClass.DoNewViewConditionSet();
    }

    // Calls the Edit method.
    private void ConditionSetEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionSetGridClass.DoEditViewConditionSet();
    }

    // Calls the Delete method.
    private void ConditionSetDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionSetGridClass.DoDeleteViewConditionSet();
    }

    // Shows the Help page.
    private void ConditionSetHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionSetList.html");
    }
    #endregion

    #region Condition

    // Handles the Menu Title click.
    private void ConditionTitle_Click(object sender, EventArgs e)
    {
      ConditionMenu.Focus();
    }

    // Calls the New method.
    private void ConditionMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionGridClass.DoNewViewCondition();
    }

    // Calls the Edit method.
    private void ConditionMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionGridClass.DoEditViewCondition();
    }

    // Calls the Delete method.
    private void ConditionMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ConditionGridClass.DoDeleteViewCondition();
    }

    // Shows the Help page.
    private void ConditionMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionList.html");
    }
    #endregion

    #region OrderBy

    // Handles the Menu Title click.
    private void OrderByTitle_Click(object sender, EventArgs e)
    {
      OrderByMenu.Focus();
    }

    // Calls the New method.
    private void OrderByMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      OrderByGridClass.DoNewViewOrderBy();
    }

    // Calls the Edit method.
    private void OrderByMenuEdit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      OrderByGridClass.DoEditViewOrderBy();
    }

    // Calls the Delete method.
    private void OrderByMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      OrderByGridClass.DoDeleteViewOrderBy();
    }

    // Shows the Help page.
    private void OrderByMenuHelp_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"OrderBy\OrderByList.html");
    }
    #endregion

    #region Data

    // Handles the Menu Title click.
    private void DataTitle_Click(object sender, EventArgs e)
    {
      DataMenu.Focus();
    }

    // Calls the New method.
    private void DataMenuNew_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      DataGridClass.DoNewData();
    }

    // Calls the Edit method.
    private void DataMenuEdit_Click(object sender, EventArgs e)
    {
      DataGridClass.TableName = TableCombo.Text.Trim();
      DoMenuClose();
      DataGridClass.DoEditData();
    }

    // Calls the Delete method.
    private void DataMenuDelete_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      DataGridClass.DoDeleteData();
    }

    // Calls the Refresh method.
    private void DataMenuRefresh_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      ViewGridClass.DoShowData();
    }

    // Performs the Close function.
    private void DataMenuExit_Click(object sender, EventArgs e)
    {
      DoMenuClose();
      SaveControlValues();
      Close();
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Table

    // Handles the SelectionChanged event.
    private void ConfigCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      DataConfigName = ConfigCombo.Text;
      DataRetrieveTable();
    }

    // Handles the SelectionChanged event.
    private void TableCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.Table);
    }
    #endregion

    #region View Data

    // Handles the control keys.
    private void ViewGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ViewGridClass.DoEditViewData();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"View\ViewList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ViewGridClass.DoRefreshViewData();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ViewGrid
              , MousePosition);
            ViewMenu.Show(position);
            ViewMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ViewGrid.Select();
          }
          else
          {
            ViewTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ViewGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      ViewGrid.Select();
      if (e.Button == MouseButtons.Right
        && ViewGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        ViewGrid.LJCSetCurrentRow(e);
        TimedChange(Change.View);
      }
    }

    // Handles the SelectionChanged event.
    private void ViewGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ViewGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.View);
      }
      ViewGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ViewGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ViewGrid.LJCGetMouseRow(e) != null)
      {
        ViewGridClass.DoEditViewData();
      }
    }
    #endregion

    #region Column

    // Handles the control keys.
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ColumnGridClass.DoEditViewColumn();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Column\ColumnList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ColumnGridClass.DoRefreshViewColumn();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ColumnGrid
              , MousePosition);
            ColumnMenu.Show(position);
            ColumnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      ColumnGrid.Select();
      if (e.Button == MouseButtons.Right
        && ColumnGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        ColumnGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Column);
      }
    }

    // Handles the SelectionChanged event.
    private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        ColumnGridClass.DoEditViewColumn();
      }
    }
    #endregion

    #region Join

    // Handles the control keys.
    private void JoinGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinGridClass.DoEditViewJoin();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\Join.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinGrid
              , MousePosition);
            JoinMenu.Show(position);
            JoinMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinGrid.Select();
          }
          else
          {
            JoinTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void JoinGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      JoinGrid.Select();
      if (e.Button == MouseButtons.Right
        && JoinGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        JoinGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Join);
      }
    }

    // Handles the SelectionChanged event.
    private void JoinGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Join);
      }
      JoinGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void JoinGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinGrid.LJCGetMouseRow(e) != null)
      {
        JoinGridClass.DoEditViewJoin();
      }
    }
    #endregion

    #region JoinOn

    // Handles the control keys.
    private void JoinOnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinOnGridClass.DoEditViewJoinOn();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\JoinOnList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinOnGrid
              , MousePosition);
            JoinOnMenu.Show(position);
            JoinOnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinOnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void JoinOnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      JoinOnGrid.Select();
      if (e.Button == MouseButtons.Right
        && JoinOnGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        JoinOnGrid.LJCSetCurrentRow(e);
        TimedChange(Change.JoinOn);
      }
    }

    // Handles the SelectionChanged event.
    private void JoinOnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinOnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.JoinOn);
      }
      JoinOnGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void JoinOnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinOnGrid.LJCGetMouseRow(e) != null)
      {
        JoinOnGridClass.DoEditViewJoinOn();
      }
    }
    #endregion

    #region JoinColumn

    // Handles the control keys.
    private void JoinColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinColumnGridClass.DoEditViewJoinColumn();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\JoinColumnList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinColumnGrid
              , MousePosition);
            JoinColumnMenu.Show(position);
            JoinColumnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void JoinColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      JoinColumnGrid.Select();
      if (e.Button == MouseButtons.Right
        && JoinColumnGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        JoinColumnGrid.LJCSetCurrentRow(e);
        TimedChange(Change.JoinColumn);
      }
    }

    // Handles the SelectionChanged event.
    private void JoinColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinColumnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.JoinColumn);
      }
      JoinColumnGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void JoinColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinColumnGrid.LJCGetMouseRow(e) != null)
      {
        JoinColumnGridClass.DoEditViewJoinColumn();
      }
    }
    #endregion

    #region Filter

    // Handles the control keys.
    private void FilterGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          FilterGridClass.DoEditViewFilter();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\FilterList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(FilterGrid
              , MousePosition);
            FilterMenu.Show(position);
            FilterMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            FilterGrid.Select();
          }
          else
          {
            ConditionSetTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void FilterGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      FilterGrid.Select();
      if (e.Button == MouseButtons.Right
        && FilterGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        FilterGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Filter);
      }
    }

    // Handles the SelectionChanged event.
    private void FilterGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (FilterGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Filter);
      }
      FilterGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void FilterGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (FilterGrid.LJCGetMouseRow(e) != null)
      {
        FilterGridClass.DoEditViewFilter();
      }
    }
    #endregion

    #region ConditionSet

    // Handles the control keys.
    private void ConditionSetGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionSetGridClass.DoEditViewConditionSet();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\ConditionSetList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ConditionSetGrid
              , MousePosition);
            ConditionSetMenu.Show(position);
            ConditionSetMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ConditionSetGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ConditionSetGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      ConditionSetGrid.Select();
      if (e.Button == MouseButtons.Right
        && ConditionSetGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        ConditionSetGrid.LJCSetCurrentRow(e);
        TimedChange(Change.ConditionSet);
      }
    }

    // Handles the SelectionChanged event.
    private void ConditionSetGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ConditionSetGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ConditionSet);
      }
      ConditionSetGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ConditionSetGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionSetGrid.LJCGetMouseRow(e) != null)
      {
        ConditionSetGridClass.DoEditViewConditionSet();
      }
    }
    #endregion

    #region Condition

    // Handles the control keys.
    private void ConditionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionGridClass.DoEditViewCondition();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\ConditionList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ConditionGrid
              , MousePosition);
            ConditionMenu.Show(position);
            ConditionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ConditionGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ConditionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      ConditionGrid.Select();
      if (e.Button == MouseButtons.Right
        && ConditionGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        ConditionGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Condition);
      }
    }

    // Handles the SelectionChanged event.
    private void ConditionGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ConditionGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Condition);
      }
      ConditionGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ConditionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionGrid.LJCGetMouseRow(e) != null)
      {
        ConditionGridClass.DoEditViewCondition();
      }
    }
    #endregion

    #region OrderBy

    // Handles the control keys.
    private void OrderByGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          OrderByGridClass.DoEditViewOrderBy();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"OrderBy\OrderByList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(OrderByGrid
              , MousePosition);
            OrderByMenu.Show(position);
            OrderByMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void OrderByGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      OrderByGrid.Select();
      if (e.Button == MouseButtons.Right
        && OrderByGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        OrderByGrid.LJCSetCurrentRow(e);
        TimedChange(Change.OrderBy);
      }
    }

    // Handles the SelectionChanged event.
    private void OrderByGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (OrderByGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.OrderBy);
      }
      OrderByGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void OrderByGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (OrderByGrid.LJCGetMouseRow(e) != null)
      {
        OrderByGridClass.DoEditViewOrderBy();
      }
    }
    #endregion

    #region Data

    // Handles the control keys.
    private void DataGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DataGridClass.DoEditData();
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void DataGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      DataGrid.Select();
      if (e.Button == MouseButtons.Right
        && DataGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        DataGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Data);
      }
    }

    // Handles the SelectionChanged event.
    private void DataGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (DataGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Data);
      }
      DataGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void DataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DataGrid.LJCGetMouseRow(e) != null)
      {
        DataGridClass.TableName = TableCombo.Text.Trim();
        DataGridClass.DoEditData();
      }
    }
    #endregion

    // Handles the InfoWindow Close event.
    internal void InfoWindow_CloseEvent(object sender, EventArgs e)
    {
      mInfoWindow = null;
    }
    #endregion

    #region Public Properties

    /// <summary>The ViewData ID value.</summary>
    public int StartupViewDataID { get; set; }
    #endregion

    #region Internal Properties

    // The DataConfig name.
    internal string DataConfigName { get; set; }

    // The DbService.
    internal DbServiceRef DbServiceRef { get; set; }

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    // Gets or sets the ViewHelper value.
    internal ViewHelper ViewHelper { get; set; }
    #endregion

    #region GridClass Properties

    // Gets or sets the ViewColumnClass value.
    private ColumnGridClass ColumnGridClass { get; set; }

    // Gets or sets the ConditionGridClass value.
    private ConditionGridClass ConditionGridClass { get; set; }

    // Gets or sets the ConditionSetGridClass value.
    private ConditionSetGridClass ConditionSetGridClass { get; set; }

    // Gets or sets the DataGridClass value.
    private DataGridClass DataGridClass { get; set; }

    // Gets or sets the JoinGridClass value.
    private FilterGridClass FilterGridClass { get; set; }

    // Gets or sets the JoinColumnGridClass value.
    private JoinColumnGridClass JoinColumnGridClass { get; set; }

    // Gets or sets the JoinGridClass value.
    private JoinGridClass JoinGridClass { get; set; }

    // Gets or sets the JoinGridClass value.
    private JoinOnGridClass JoinOnGridClass { get; set; }

    // Gets or sets the OrderByGridClass value.
    private OrderByGridClass OrderByGridClass { get; set; }

    // Gets or sets the ViewGridClass value.
    private ViewGridClass ViewGridClass { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    internal InfoWindow mInfoWindow;
    private string mPrevConfigName;
    private StandardSettings mSettings;
    private readonly string mStartupTableName;
    #endregion
  }
}
