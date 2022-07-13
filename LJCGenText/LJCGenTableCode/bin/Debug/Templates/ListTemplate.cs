// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// ListTemplate.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
using _FullAppName_DAL;

namespace _Namespace_
{
  // The list form.
  /// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  internal partial class _ClassName_List : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal _ClassName_List()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "_AppName_.chm";
      LJCHelpPageList = "_ClassName_List.htm";
      LJCHelpPageDetail = "_ClassName_Detail.htm";

      // Set default class data.
      mViewTableName = _ClassName_.TableName;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void _ClassName_List_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Data Methods

    #region Combo

    // Retrieves the combo items.
    private void DataRetrieveCombo()
    {
      //ComboRecords dataRecords;

      //Cursor = Cursors.WaitCursor;
      //Combo.Items.Clear();

      //dataRecords = mComboManager.Load();

      //if (dataRecords != null && records.Count > 0)
      //{
      //	foreach (ComboRecord dataRecord in dataRecords)
      //	{
      //		Combo.Items.Add(dataRecord);
      //	}
      //	if (Combo.Items.Count > 0)
      //	{
      //		Combo.SelectedIndex = 0;
      //	}
      //}
      //Cursor = Cursors.Default;
    }
    #endregion

    #region _ClassName_

    // Retrieves the list rows.
    private void DataRetrieve_ClassName_()
    {
      _CollectionName_ dataRecords;

      Cursor = Cursors.WaitCursor;
      _ClassName_Grid.LJCRowsClear();

      // If the grid is a child grid.
      //var parentRow = SalesCommon.CurrentRow(_ClassName_Grid);
      //if (parentRow != null)
      //{
      //  // Data from items.
      //  int parentID = parentRow.LJCGetInt32(Parent.ColumnID);
      //
      //	DbColumns keyRecord = new DbColumns()
      //	{
      //		{ _ClassName_.ColumnParentID, parentID }
      //	};
      //var manager = Managers._ClassName_Manager;
      //	dataRecords = manager.Load(keyRecord);
      // If the grid is not a child grid.
      var manager = Managers._ClassName_Manager;
      dataRecords = manager.Load();

      if (dataRecords != null && dataRecords.Count > 0)
      {
        foreach (_ClassName_ dataRecord in dataRecords)
        {
          RowAdd_ClassName_(dataRecord);
        }
      }
      //}
      Cursor = Cursors.Default;
      DoChange(Change._ClassName_);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd_ClassName_(_ClassName_ dataRecord)
    {
      LJCGridRow retValue;

      retValue = _ClassName_Grid.LJCRowAdd();
      SetStoredValues_ClassName_(retValue, dataRecord);

      // Sets the row values from a data object.
      _ClassName_Grid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate_ClassName_(_ClassName_ dataRecord)
    {
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues_ClassName_(row, dataRecord);
        _ClassName_Grid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues_ClassName_(LJCGridRow row, _ClassName_ dataRecord)
    {
      row.LJCSetInt32(_ClassName_.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelect_ClassName_(_ClassName_ dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in _ClassName_Grid.Rows)
        {
          var rowID = row.LJCGetInt32(_ClassName_.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            _ClassName_Grid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Cursor = Cursors.Default;
      }
      return retValue;
    }
    #endregion
    #endregion

    #region Action Methods

    #region _ClassName_

    // Performs the default list action.
    internal void DoDefault_ClassName_()
    {
      if (LJCIsSelect)
      {
        DoSelect_ClassName_();
      }
      else
      {
        DoEdit_ClassName_();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew_ClassName_()
    {
      //if (ParentGrid.CurrentRow is LJCGridRow parentRow)
      //{
      //  // Data from items.
      //  int parentID = parentRow.LJCGetInt64(Parent.ColumnID);
      //  string parentName = parentRow.LJCGetString(Parent.ColumnName);

      var detail = new _ClassName_Detail()
      {
        //LJCParentID = parentID,
        //LJCParentName = parentName,
        LJCHelpFileName = LJCHelpFile,
        LJCHelpPageName = LJCHelpPageDetail,
        Managers = Managers
      };
      detail.LJCChange += _ClassName_Detail_Change;
      detail.ShowDialog();
      //}
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit_ClassName_()
    {
      //if (ParentGrid.CurrentRow is LJCGridRow parentRow
      //  && _ClassName_Grid.CurrentRow is LJCGridRow row)
      //{
      //  // Data from items.
      //  int id = row.LJCGetInt(_ClassName_.ColumnID);
      //  int parentID = parentRow.LJCGetInt(Parent.ColumnID);
      //  string parentName = parentRow.LJCGetString(Parent.ColumnName);
      //
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var id = row.LJCGetInt32(_ClassName_.ColumnID);

        var detail = new _ClassName_Detail()
        {
          LJCID = id,
          //LJCParentID = parentID,
          //LJCParentName = parentName,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = LJCHelpPageDetail,
          Managers = Managers
        };
        detail.LJCChange += _ClassName_Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete_ClassName_()
    {
      string title;
      string message;
      bool success = false;

      LJCGridRow row = _ClassName_Grid.CurrentRow as LJCGridRow;
      //if (ParentGrid.CurrentRow is LJCGridRow parentRow
      //  && row != null)
      //{
      if (row != null)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt(_ClassName_.ColumnID);
        //var parentID = parentRow.LJCGetInt(Parent.ColumnId);

        var keyRecord = new DbColumns()
        {
          { _ClassName_.ColumnID, id }
					//{ ParentData.ColumnID, id}
				};
        Managers._ClassName_Manager.Delete(keyRecord);
        if (0 == Managers.m_ClassName_Manager.AffectedCount)
        {
          success = false;
          message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        _ClassName_Grid.Rows.Remove(row);
        mParent.TimedChange(ParentList.Change._ClassName_);
      }
    }

    // Refreshes the list.
    internal void DoRefresh_ClassName_()
    {
      int id = 0;

      Cursor = Cursors.WaitCursor;
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt(_ClassName_.ColumnID);
      }
      DataRetrieve_ClassName_();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new _ClassName_()
        {
          ID = id
        };
        RowSelect_ClassName_(dataRecord);
      }
      Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    private void DoSelect_ClassName_()
    {
      LJCSelectedRecord = null;
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        Cursor = Cursors.WaitCursor;
        var id = row.LJCGetInt32(_ClassName_.ColumnID);

        var manager = mManagers._ClassName_Manager;
        var keyRecord = manager.GetIDKey(id);
        dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          LJCSelectedRecord = dataRecord;
        }
        Cursor = Cursors.Default;
        DialogResult = DialogResult.OK;
      }
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void _ClassName_Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as _ClassName_Detail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate_ClassName_(dataRecord);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd_ClassName_(dataRecord);
          _ClassName_Grid.LJCSetCurrentRow(row, true);
          mParent.TimedChange(ParentList.Change._ClassName_);
        }
      }
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

          // Load first control.
          DataRetrieveCombo();
          break;

        case Change.Combo:
          DataRetrieve_ClassName_();
          break;

        case Change._ClassName_:
          //DataRetrieveChild();
          _ClassName_Grid.LJCSetLastRow();
          _ClassName_Grid.LJCSetCounter(_ClassName_Counter);
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Combo,
      _ClassName_
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
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        //Split.SplitterWidth = 4;

        // Modify MainSplit.Panel1 controls.
        ListHelper.SetPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
          , _ClassName_ToolPanel, _ClassName_Grid);
        //_ClassName_Grid.Height = ClientSize.Height - _ClassName_Tools.Height;

        // Modify MainSplit.Panel2 controls.
        //ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
        //	, ChildToolPanel, ChildGrid);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      SetAppConfigValues();
      InitializeClassData();
      SetupGridCode();
      LoadControlData();
      ControlSetup();
      InitialControlValues();
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Initial Control setup.
    private void ControlSetup()
    {
      if (LJCIsSelect)
      {
        // This is a Selection List.
        Text = "_ClassName_ Selection";
        _ClassName_MenuEdit.ShortcutKeyDisplayString = "";
        _ClassName_MenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
      }
      else
      {
        // This is a display list.
        Text = "_ClassName_ List";
        _ClassName_Separator.Visible = false;
        _ClassName_MenuSelect.Visible = false;
      }

      // Provides additional Drag features between split LJCTabControls.
      //var _ = new LJCPanelManager(TabsSplit, MainTabs, TileTabs);
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\_ClassName_.xml";

      // Splitter is not in the first TabPage.
      //Split.Resize += Split_Resize;

      BackColor = mSettings.BeginColor;
      //MainTools.BackColor = mSettings.BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      Managers = new Managers();
      Managers.SetDBProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);
    }

    // Loads the initial Control data.
    private void LoadControlData()
    {
      //ComboItems comboItems = ComboManager.Load();
      //foreach (ComboItem comboItem in comboItems)
      //{
      //	Combo.Items.Add(comboItem);
      //}
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;
      Control parent = _ClassName_Tabs.Parent;

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
            // Tabs Parent is not this module form.
            if (parent != null
              && parent.GetType().Name != Name)
            {
              parent.Left = controlValue.Left;
              parent.Top = controlValue.Top;
              parent.Width = controlValue.Width;
              parent.Height = controlValue.Height;
            }
          }

          // Restore Splitter, Grid and other values.
          FormCommon.RestoreSplitDistance(_ClassName_Split, ControlValues);

          //_ClassName_Grid.LJCRestoreColumnValues(ControlValues);

          //controlValue = ControlValues.LJCSearchName("View");
          //if (controlValue != null)
          //{
          //	mViewDataId = controlValue.Left;
          //}
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      Control parent = _ClassName_Tabs.Parent;
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      _ClassName_Grid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("_ClassName_Split.SplitterDistance", 0, 0, 0
        , _ClassName_Split.SplitterDistance);

      // Save Window values.
      // Tabs Parent is not this module form.
      if (parent != null
        && parent.GetType().Name != Name)
      {
        controlValues.Add(formName, parent.Left, parent.Top
          , parent.Width, parent.Height);
      }

      // Save other values.
      //mViewDataId = ViewCombo.LJCSelectedItemId();
      //controlValues.Add("View", mViewDataId, 0, 0, 0);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Sets the Application Configuration values.
    private void SetAppConfigValues()
    {
      var values = Values_AppName_.Instance;
      mSettings = values.StandardSettings;
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      //	m_ClassName_GridCode = new _ClassName_GridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      SetupGrid_ClassName_();
    }

    // Setup the grid display columns.
    private void SetupGrid_ClassName_()
    {
      _ClassName_Grid.BackgroundColor = mSettings.BeginColor;

      // Setup default display columns if no columns are defined.
      if (0 == _ClassName_Grid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          _ClassName_.ColumnName,
          _ClassName_.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        mDisplayColumns_ClassName_
          = Managers._ClassName_Manager.GetColumns(columnNames);

        // Add Calculated columns.
        //mDisplayColumnsSource.Add(LayoutColumn.ColumnMapTypeName, caption: "MapType Name");

        // Setup the grid display columns.
        _ClassName_Grid.LJCAddDisplayColumns(mDisplayColumns_ClassName_);
      }
    }
    private DbColumns mDisplayColumns_ClassName_;

    // Splitter is not in the first TabPage so Set values on first display.
    //private void Split_Resize(object sender, EventArgs e)
    //{
    //	if (ControlValues != null)
    //	{
    //		if (false == mIsSplitSet)
    //		{
    //			FormCommon.RestoreSplitDistance(Split, ControlValues);
    //		}
    //		mIsSplitSet = true;
    //	}
    //}
    //private bool mIsSplitSet;

    /// <summary>Gets or sets the ControlValues item.</summary>
    private ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = Combo.SelectedIndex != -1; ;
      bool enableEdit = _ClassName_Grid.CurrentRow != null;
      FormCommon.SetToolState(_ClassName_Tool, enableNew, enableEdit);
      FormCommon.SetMenuState(_ClassName_Menu, enableNew, enableEdit);
    }
    #endregion

    #region Action Event Handlers

    #region _ClassName_

    // Calls the New method.
    private void _ClassName_ToolNew_Click(object sender, EventArgs e)
    {
      DoNew_ClassName_();
    }

    // Calls the Edit method.
    private void _ClassName_ToolEdit_Click(object sender, EventArgs e)
    {
      DoEdit_ClassName_();
    }

    // Calls the Delete method.
    private void _ClassName_ToolDelete_Click(object sender, EventArgs e)
    {
      DoDelete_ClassName_();
    }

    // Calls the New method.
    private void _ClassName_MenuNew_Click(object sender, EventArgs e)
    {
      DoNew_ClassName_();
    }

    // Calls the Edit method.
    private void _ClassName_MenuEdit_Click(object sender, EventArgs e)
    {
      DoEdit_ClassName_();
    }

    // Calls the Delete method.
    private void _ClassName_MenuDelete_Click(object sender, EventArgs e)
    {
      DoDelete_ClassName_();
    }

    // Calls the Refresh method.
    private void _ClassName_MenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefresh_ClassName_();
    }

    // Export a text file.
    private void MainMenuExportText_Click(object sender, EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\_ClassName_.{extension}";
      _ClassName_Grid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MainMenuExportCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\_ClassName_.csv";
      _ClassName_Grid.LJCExportData(fileSpec);
    }

    // Calls the Select method.
    private void _ClassName_MenuSelect_Click(object sender, EventArgs e)
    {
      DoSelect_ClassName_();
    }

    // Performs the Close function.
    private void _ClassName_MenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the help page.
    private void _ClassName_MenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , LJCHelpPageList);
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Combo

    //// Handles the SelectedIndexChanged event.
    //private void Combo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //	ChangeTimer.DoChange(Change.Startup.ToString());
    //}
    #endregion

    #region _ClassName_

    // Handles the form keys.
    private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DoDefault_ClassName_();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , LJCHelpPageList);
          e.Handled = true;
          break;

        case Keys.F5:
          DoRefresh_ClassName_();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(_ClassName_Grid
              , MousePosition);
            _ClassName_Menu.Show(position);
            _ClassName_Menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            //Combo.Select();
          }
          else
          {
            //Combo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void _ClassName_Grid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
      Select();
      if (e.Button == MouseButtons.Right
        && _ClassName_Grid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        _ClassName_Grid.LJCSetCurrentRow(e);
        ChangeTimer.DoChange(Change._ClassName_.ToString());
      }
    }

    // Handles the MouseDoubleClick event.
    private void _ClassName_Grid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (_ClassName_Grid.LJCGetMouseRowIndex(e) > -1)
      {
        DoDefault_ClassName_();
      }
    }

    // Handles the SelectionChanged event.
    private void _ClassName_Grid_SelectionChanged(object sender, EventArgs e)
    {
      if (_ClassName_Grid.LJCAllowSelectionChange)
      {
        ChangeTimer.DoChange(Change._ClassName_.ToString());
      }
      _ClassName_Grid.LJCAllowSelectionChange = true;
    }
    #endregion
    #endregion

    #region Public Properties
    #endregion

    #region Internal Properties

    // Gets or sets the parent ID value.
    internal int LJCParentID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets or sets the LJCIsSelect value.
    internal bool LJCIsSelect { get; set; }

    // Gets a reference to the selected record.
    internal _ClassName_ LJCSelectedRecord { get; private set; }

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    // The List help page name.
    internal string LJCHelpPageList
    {
      get { return mHelpPageList; }
      set { mHelpPageList = NetString.InitString(value); }
    }
    private string mHelpPageList;

    // The Detail help page name.
    internal string LJCHelpPageDetail
    {
      get { return mHelpPageDetail; }
      set { mHelpPageDetail = NetString.InitString(value); }
    }
    private string mHelpPageDetail;

    // The Managers object.
    internal _ClassName_Managers Managers { get; set; }
    #endregion

    #region GridClass Properties

    // Gets or sets the _ClassName_GridClass value.
    private _ClassName_GridClass _ClassName_GridClass { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardSettings mSettings;

    // Foreign Keys
    #endregion
  }
}
// #SectionEnd Class
