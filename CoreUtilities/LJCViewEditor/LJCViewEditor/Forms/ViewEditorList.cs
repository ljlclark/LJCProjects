// Copyright(c) Lester J. Clark and Contributors.
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
using System.Drawing;
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
    public ViewEditorList(string tableName = null, bool splash = false)
    {
      Cursor = Cursors.WaitCursor;
      mStartupTableName = tableName;
      if (splash)
      {
        ViewEditSplash splashDialog = new ViewEditSplash(true);
        splashDialog.ShowDialog();
      }
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "ViewEditor.chm";

      // Set default class data.
      BeginColor = Color.AliceBlue;
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
          ViewGridClass.DataRetrieve();

          if (StartupViewDataID > 0)
          {
            ViewData viewData = new ViewData()
            {
              ID = StartupViewDataID
            };
            ViewGridClass.RowSelect(viewData);
          }
          break;

        case Change.View:
          ColumnGridClass.DataRetrieve();
          JoinGridClass.DataRetrieve();
          FilterGridClass.DataRetrieve();
          OrderByGridClass.DataRetrieve();
          ViewGrid.LJCSetLastRow();
          break;

        case Change.Column:
          ColumnGrid.LJCSetLastRow();
          break;

        case Change.Join:
          JoinOnGridClass.DataRetrieve();
          JoinColumnGridClass.DataRetrieve();
          JoinGrid.LJCSetLastRow();
          break;

        case Change.JoinOn:
          JoinOnGrid.LJCSetLastRow();
          break;

        case Change.JoinColumn:
          JoinGrid.LJCSetLastRow();
          break;

        case Change.Filter:
          ConditionSetGridClass.DataRetrieve();
          ConditionGridClass.DataRetrieve();
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
      BeginColor = mSettings.BeginColor;
      DataConfigName = mSettings.DataConfigName;
      DbServiceRef = mSettings.DbServiceRef;
      mPrevConfigName = DataConfigName;

      try
      {
        ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
      }
      catch (SystemException e)
      {
        ViewEditorCommon.CreateTables(e, DataConfigName);
        ViewHelper = new ViewHelper(DbServiceRef, DataConfigName);
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

    #region Action Event Handlers

    #region Form Controls

    // Shows the Help page.
    private void DataConfigHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"DataConfig.html");
    }

    // Handles the Menu Title click.
    private void TableTitle_Click(object sender, EventArgs e)
    {
      //TableMenu.Focus();
    }

    // Shows the Help page.
    private void TableHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Table.html");
    }
    #endregion

    #region View Data

    // Calls the New method.
    private void ViewMenuNew_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoNew();
    }

    // Calls the Edit method.
    private void ViewMenuEdit_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void ViewMenuDelete_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoDelete();
    }

    // Calls the Refresh method.
    private void ViewMenuRefresh_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoRefresh();
    }

    // Allows for display and edit of a file.
    private void ViewFileEdit_Click(object sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // Shows the Data.
    private void ViewMenuShowData_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoShowData();
    }

    // Shows the SQL statement.
    private void ViewMenuShowSQL_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoShowSQL();
    }

    // Show the DbRequest code.
    private void ViewMenuShowCode_Click(object sender, EventArgs e)
    {
      ViewGridClass.ShowCode();
    }

    // Performs the Close function.
    private void ViewMenuExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the Help page.
    private void ViewMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"View\ViewList.html");
    }

    // Shows the About box.
    private void ViewMenuAbout_Click(object sender, EventArgs e)
    {
      ViewEditSplash splash = new ViewEditSplash();
      splash.ShowDialog();
    }
    #endregion

    #region Column

    // Calls the AddAll method.
    private void ColumnMenuAdd_Click(object sender, EventArgs e)
    {
      ColumnGridClass.DoAddAll(TableCombo.Text.Trim());
    }

    // Calls the New method.
    private void ColumnMenuNew_Click(object sender, EventArgs e)
    {
      ColumnGridClass.DoNew();
    }

    // Calls the Edit method.
    private void ColumnMenuEdit_Click(object sender, EventArgs e)
    {
      ColumnGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void ColumnMenuDelete_Click(object sender, EventArgs e)
    {
      ColumnGridClass.DoDelete();
    }

    // Calls the Refresh method.
    private void ColumnMenuRefresh_Click(object sender, EventArgs e)
    {
      ColumnGridClass.DoRefresh();
    }

    // Shows the Help page.
    private void ColumnMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Column\ColumnList.html");
    }
    #endregion

    #region Join

    // Calls the New method.
    private void JoinMenuNew_Click(object sender, EventArgs e)
    {
      JoinGridClass.DoNew();
    }

    // Calls the Edit method.
    private void JoinMenuEdit_Click(object sender, EventArgs e)
    {
      JoinGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void JoinMenuDelete_Click(object sender, EventArgs e)
    {
      JoinGridClass.DoDelete();
    }

    // Shows the Help page.
    private void JoinMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinList.html");
    }
    #endregion

    #region JoinOn

    // Calls the New method.
    private void JoinOnMenuNew_Click(object sender, EventArgs e)
    {
      JoinOnGridClass.DoNew();
    }

    // Calls the Edit method.
    private void JoinOnMenuEdit_Click(object sender, EventArgs e)
    {
      JoinOnGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void JoinOnMenuDelete_Click(object sender, EventArgs e)
    {
      JoinOnGridClass.DoDelete();
    }

    // Shows the Help page.
    private void JoinOnMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinOnList.html");
    }
    #endregion

    #region JoinColumn

    // Calls the New method.
    private void JoinColumnNew_Click(object sender, EventArgs e)
    {
      JoinColumnGridClass.DoNew();
    }

    // Calls the Edit method.
    private void JoinColumnEdit_Click(object sender, EventArgs e)
    {
      JoinColumnGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void JoinColumnDelete_Click(object sender, EventArgs e)
    {
      JoinColumnGridClass.DoDelete();
    }

    // Shows the Help page.
    private void JoinColumnHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinColumnList.html");
    }
    #endregion

    #region Filter

    // Calls the New method.
    private void FilterMenuNew_Click(object sender, EventArgs e)
    {
      FilterGridClass.DoNew();
    }

    // Calls the Edit method.
    private void FilterMenuEdit_Click(object sender, EventArgs e)
    {
      FilterGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void FilterMenuDelete_Click(object sender, EventArgs e)
    {
      FilterGridClass.DoDelete();
    }

    // Shows the Help page.
    private void FilterMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\FilterList.html");
    }
    #endregion

    #region ConditionSet

    // Calls the New method.
    private void ConditionSetNew_Click(object sender, EventArgs e)
    {
      ConditionSetGridClass.DoNew();
    }

    // Calls the Edit method.
    private void ConditionSetEdit_Click(object sender, EventArgs e)
    {
      ConditionSetGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void ConditionSetDelete_Click(object sender, EventArgs e)
    {
      ConditionSetGridClass.DoDelete();
    }

    // Shows the Help page.
    private void ConditionSetHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionSetList.html");
    }
    #endregion

    #region Condition

    // Calls the New method.
    private void ConditionMenuNew_Click(object sender, EventArgs e)
    {
      ConditionGridClass.DoNew();
    }

    // Calls the Edit method.
    private void ConditionMenuEdit_Click(object sender, EventArgs e)
    {
      ConditionGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void ConditionMenuDelete_Click(object sender, EventArgs e)
    {
      ConditionGridClass.DoDelete();
    }

    // Shows the Help page.
    private void ConditionMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionList.html");
    }
    #endregion

    #region OrderBy

    // Calls the New method.
    private void OrderByMenuNew_Click(object sender, EventArgs e)
    {
      OrderByGridClass.DoNew();
    }

    // Calls the Edit method.
    private void OrderByMenuEdit_Click(object sender, EventArgs e)
    {
      OrderByGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void OrderByMenuDelete_Click(object sender, EventArgs e)
    {
      OrderByGridClass.DoDelete();
    }

    // Shows the Help page.
    private void OrderByMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"OrderBy\OrderByList.html");
    }
    #endregion

    #region Data

    // Calls the New method.
    private void DataMenuNew_Click(object sender, EventArgs e)
    {
      DataGridClass.DoNew();
    }

    // Calls the Edit method.
    private void DataMenuEdit_Click(object sender, EventArgs e)
    {
      DataGridClass.TableName = TableCombo.Text.Trim();
      DataGridClass.DoEdit();
    }

    // Calls the Delete method.
    private void DataMenuDelete_Click(object sender, EventArgs e)
    {
      DataGridClass.DoDelete();
    }

    // Calls the Refresh method.
    private void DataMenuRefresh_Click(object sender, EventArgs e)
    {
      ViewGridClass.DoShowData();
    }

    // Performs the Close function.
    private void DataMenuExit_Click(object sender, EventArgs e)
    {
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
      TimedChange(Change.Config);
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
          ViewGridClass.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"View\ViewList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ViewGridClass.DoRefresh();
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

    // Handles the MouseDoubleClick event.
    private void ViewGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ViewGrid.LJCGetMouseRow(e) != null)
      {
        ViewGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ViewGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ViewGrid.Select();
        if (ViewGrid.LJCIsDifferentRow(e))
        {
          ViewGrid.LJCSetCurrentRow(e);
          TimedChange(Change.View);
        }
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

    private void ViewTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ViewTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = ViewTabs.LJCGetTabPage(e);
      ViewSetFocusTab(tabPage);
    }
    #endregion

    #region Column

    // Handles the control keys.
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ColumnGridClass.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Column\ColumnList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ColumnGridClass.DoRefresh();
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

    // Handles the MouseDoubleClick event.
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        ColumnGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ColumnGrid.Select();
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          ColumnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Column);
        }
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
    #endregion

    #region Join

    // Handles the control keys.
    private void JoinGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void JoinGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinGrid.LJCGetMouseRow(e) != null)
      {
        JoinGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinGrid.Select();
        if (JoinGrid.LJCIsDifferentRow(e))
        {
          JoinGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Join);
        }
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

    private void JoinTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = JoinTabs.LJCGetTabPage(e);
      JoinSetFocusTab(tabPage);
    }
    #endregion

    #region JoinOn

    // Handles the control keys.
    private void JoinOnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinOnGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void JoinOnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinOnGrid.LJCGetMouseRow(e) != null)
      {
        JoinOnGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinOnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinOnGrid.Select();
        if (JoinOnGrid.LJCIsDifferentRow(e))
        {
          JoinOnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.JoinOn);
        }
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
    #endregion

    #region JoinColumn

    // Handles the control keys.
    private void JoinColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinColumnGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void JoinColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinColumnGrid.LJCGetMouseRow(e) != null)
      {
        JoinColumnGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinColumnGrid.Select();
        if (JoinColumnGrid.LJCIsDifferentRow(e))
        {
          JoinColumnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.JoinColumn);
        }
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
    #endregion

    #region Filter

    // Handles the control keys.
    private void FilterGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          FilterGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void FilterGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (FilterGrid.LJCGetMouseRow(e) != null)
      {
        FilterGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void FilterGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        FilterGrid.Select();
        if (FilterGrid.LJCIsDifferentRow(e))
        {
          FilterGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Filter);
        }
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

    private void ConditionSetTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionSetTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = ConditionSetTabs.LJCGetTabPage(e);
      FilterSetFocusTab(tabPage);
    }
    #endregion

    #region ConditionSet

    // Handles the control keys.
    private void ConditionSetGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionSetGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void ConditionSetGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionSetGrid.LJCGetMouseRow(e) != null)
      {
        ConditionSetGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ConditionSetGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionSetGrid.Select();
        if (ConditionSetGrid.LJCIsDifferentRow(e))
        {
          ConditionSetGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ConditionSet);
        }
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
    #endregion

    #region Condition

    // Handles the control keys.
    private void ConditionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void ConditionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionGrid.LJCGetMouseRow(e) != null)
      {
        ConditionGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ConditionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionGrid.Select();
        if (ConditionGrid.LJCIsDifferentRow(e))
        {
          ConditionGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Condition);
        }
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
    #endregion

    #region OrderBy

    // Handles the control keys.
    private void OrderByGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          OrderByGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void OrderByGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (OrderByGrid.LJCGetMouseRow(e) != null)
      {
        OrderByGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void OrderByGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        OrderByGrid.Select();
        if (OrderByGrid.LJCIsDifferentRow(e))
        {
          OrderByGrid.LJCSetCurrentRow(e);
          TimedChange(Change.OrderBy);
        }
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
    #endregion

    #region Data

    // Handles the control keys.
    private void DataGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DataGridClass.DoEdit();
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

    // Handles the MouseDoubleClick event.
    private void DataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DataGrid.LJCGetMouseRow(e) != null)
      {
        DataGridClass.TableName = TableCombo.Text.Trim();
        DataGridClass.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void DataGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        DataGrid.Select();
        if (DataGrid.LJCIsDifferentRow(e))
        {
          DataGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Data);
        }
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

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

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
    private StandardUISettings mSettings;
    private readonly string mStartupTableName;
    #endregion
  }
}
