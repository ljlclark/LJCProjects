// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleList.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCAppManagerDAL;
using LJCDBClientLib;

namespace LJCAppManager
{
  // The Module list form.
  /// <include path='items/ModuleList/*' file='Doc/ModuleList.xml'/>
  public partial class ModuleList : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ModuleList()
    {
      InitializeComponent();
      LJCHelpFile = "AppManager.chm";
      LJCHelpPageList = "ModuleList.htm";
      LJCHelpPageDetail = "ModuleDetail.htm";
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ModuleList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    private void DataRetrieveModule()
    {
      AppModules records;

      ModuleGrid.LJCRowsClear();
      var keyColumns = new DbColumns()
      {
        { AppModule.ColumnAppProgramID, LJCParentID }
      };
      var moduleManager = Managers.AppModuleManager;
      DbJoins dbJoins = moduleManager.GetLoadJoins();
      records = moduleManager.Load(keyColumns, joins: dbJoins);

      if (NetCommon.HasItems(records))
      {
        foreach (AppModule record in records)
        {
          RowAddModule(record);
        }
      }
      DoChange(Change.AppModule);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddModule(AppModule dataRecord)
    {
      LJCGridRow retValue;

      retValue = ModuleGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ModuleGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateModule(AppModule dataRecord)
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ModuleGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, AppModule dataRecord)
    {
      row.LJCSetInt32(AppModule.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelectModule(AppModule dataRecord)
    {
      int rowID;
      bool retVal = false;

      foreach (LJCGridRow row in ModuleGrid.Rows)
      {
        rowID = row.LJCGetInt32(AppModule.ColumnID);
        if (rowID == dataRecord.ID)
        {
          ModuleGrid.LJCSetCurrentRow(row, true);
          retVal = true;
          break;
        }
      }
      return retVal;
    }
    #endregion

    #region Action Methods

    // Performs the default list action.
    private void DoDefaultAction()
    {
      if (LJCIsSelect)
      {
        DoSelect();
      }
      else
      {
        DoEditModule();
      }
    }

    // Displays a detail dialog for a new record.
    private void DoNewModule()
    {
      ModuleDetail detail;

      detail = new ModuleDetail()
      {
        LJCParentID = LJCParentID,
        LJCHelpFileName = LJCHelpFile,
        LJCHelpPageName = LJCHelpPageDetail
      };
      detail.LJCChange += new EventHandler<EventArgs>(ModuleDetail_Change);
      detail.ShowDialog();
      ModuleGrid.LJCSetCounter(ModuleCounter);
    }

    // Displays a detail dialog to edit an existing record.
    private void DoEditModule()
    {
      ModuleDetail detail;

      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        detail = new ModuleDetail()
        {
          LJCID = row.LJCGetInt32(AppModule.ColumnID),
          LJCParentID = LJCParentID,
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = LJCHelpPageDetail
        };
        detail.LJCChange += new EventHandler<EventArgs>(ModuleDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ModuleDetail_Change(object sender, EventArgs e)
    {
      ModuleDetail detail;
      LJCGridRow row;

      detail = sender as ModuleDetail;
      if (detail.LJCIsUpdate)
      {
        RowUpdateModule(detail.LJCRecord);
      }
      else
      {
        row = RowAddModule(detail.LJCRecord);
        ModuleGrid.LJCSetCurrentRow(row, true);
        TimedChange(Change.AppModule);
      }
    }

    // Deletes the selected row.
    private void DoDeleteModule()
    {
      string title;
      string message;

      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = "Are you sure you want to delete the selected item?";
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          int id = row.LJCGetInt32(AppModule.ColumnID);
          var moduleManager = Managers.AppModuleManager;
          var keyColumns = moduleManager.GetIDKey(id);
          moduleManager.Delete(keyColumns);
          if (moduleManager.AffectedCount > 0)
          {
            ModuleGrid.Rows.Remove(row);
            TimedChange(Change.AppModule);
          }
        }
      }
    }

    // Refreshes the list.
    private void DoRefreshModule()
    {
      AppModule record;
      int id = 0;

      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(AppModule.ColumnID);
      }
      DataRetrieveModule();

      // Select the original row.
      if (id > 0)
      {
        record = new AppModule()
        {
          ID = id
        };
        RowSelectModule(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    private void DoSelect()
    {
      AppModule record;
      int id;

      LJCSelectedRecord = null;
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(AppModule.ColumnID);
        var keyColumns = Managers.AppModuleManager.GetIDKey(id);
        record = Managers.AppModuleManager.Retrieve(keyColumns);
        if (record != null)
        {
          LJCSelectedRecord = record;
        }
      }
      DialogResult = DialogResult.OK;
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesAppManager.Instance;

      mSettings = values.StandardSettings;

      // Initialize Class Data.
      Managers = new AppManagers(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      // Configure controls.
      if (LJCIsSelect)
      {
        Text = "Module Selection";
        ModuleMenuEdit.ShortcutKeyDisplayString = "";
        ModuleMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
      }
      else
      {
        Text = "Module List";
        ModuleMenuSelect.Visible = false;
      }

      SetupGridModule();
      StartItemChange();
      Cursor = Cursors.Default;
    }

    // Setup the Module grid.
    private void SetupGridModule()
    {
      ModuleGrid.BackgroundColor = mSettings.BeginColor;
      if (0 == ModuleGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          AppModule.ColumnTypeName,
          AppModule.ColumnTitle
        };
        DbColumns gridColumns = Managers.AppModuleManager.GetColumns(propertyNames);
        ModuleGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          //RestoreControlValues();
          DataRetrieveModule();
          break;

        case Change.AppModule:
          ModuleGrid.LJCSetLastRow();
          ModuleGrid.LJCSetCounter(ModuleCounter);
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      AppModule
    }

    #region Item Change Support

    // Start the Change processing.
    private void StartItemChange()
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

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    internal ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = ModuleGrid.CurrentRow != null;
      FormCommon.SetToolState(ModuleTools, enableNew, enableEdit);
      FormCommon.SetMenuState(ModuleMenu, enableNew, enableEdit);
    }
    #endregion

    #region Action Event Handlers

    // <summary> Call the New method.</summary>
    private void ModuleToolNew_Click(object sender, EventArgs e)
    {
      DoNewModule();
    }

    // <summary> Call the Edit method.</summary>
    private void ModuleToolEdit_Click(object sender, EventArgs e)
    {
      DoEditModule();
    }

    // <summary> Call the Delete method.</summary>
    private void ModuleToolDelete_Click(object sender, EventArgs e)
    {
      DoDeleteModule();
    }

    // <summary> Call the New method.</summary>
    private void ModuleMenuNew_Click(object sender, EventArgs e)
    {
      DoNewModule();
    }

    // <summary> Call the Edit method.</summary>
    private void ModuleMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditModule();
    }

    // <summary> Call the Delete method.</summary>
    private void ModuleMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteModule();
    }

    // <summary> Call the Refresh method.</summary>
    private void ModuleMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshModule();
    }

    // <summary> Call the Select method.</summary>
    private void ModuleMenuSelect_Click(object sender, EventArgs e)
    {
      DoSelect();
    }

    // <summary> Perform the Close function.</summary>
    private void ModuleMenuClose_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region Control Event Handlers

    // Handles the form keys.
    private void ModuleGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
          break;

        case Keys.F5:
          DoRefreshModule();
          e.Handled = true;
          break;

        case Keys.Enter:
          DoDefaultAction();
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ModuleGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && ModuleGrid.LJCIsDifferentRow(e))
      {
        ModuleGrid.LJCSetCurrentRow(e);
        TimedChange(Change.AppModule);
      }
    }

    // Handles the SelectionChanged event.
    private void ModuleGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ModuleGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AppModule);
      }
      ModuleGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ModuleGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultAction();
      }
    }
    #endregion

    #region Properties

    // Gets or sets the IsSelect value.
    internal bool LJCIsSelect { get; set; }

    // Gets or sets the parent ID value.
    internal int LJCParentID { get; set; }

    // Gets a reference to the selected record.
    internal AppModule LJCSelectedRecord { get; private set; }

    // The help file name.
    internal string LJCHelpFile { get; set; }

    // The List help page name.
    internal string LJCHelpPageList { get; set; }

    // The Detail help page name.
    internal string LJCHelpPageDetail { get; set; }

    // Gets or sets the Managers value.
    private AppManagers Managers { get; set; }
    #endregion

    #region Class Data

    private StandardUISettings mSettings;
    #endregion
  }
}
