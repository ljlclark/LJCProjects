// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserModule.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBMessage;
using LJCDBClientLib;
using LJCAppManagerDAL;

namespace LJCAppManager
{
  // Note: Module assemblies are loaded dynamically with reflection. Any changes
  //       to this code must be compiled and copied to the host program folder
  //       before they are available. See the build UpdatePost.cmd file.

  // The User tab composite user control.
  /// <include path='items/UserModule/*' file='Doc/UserModule.xml'/>
  public partial class UserModule : UserControl
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public UserModule()
    {
      InitializeComponent();
      LJCHelpFile = "AppManager.chm";
    }
    #endregion

    #region Data Methods

    #region User

    // Retrieves the list rows.
    private void DataRetrieveUser()
    {
      AppUsers records;

      UserGrid.LJCRowsClear();
      records = Managers.AppUserManager.Load();

      if (NetCommon.HasItems(records))
      {
        foreach (AppUser record in records)
        {
          RowAddUser(record);
        }
      }
      DoChange(Change.User);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddUser(AppUser dataRecord)
    {
      LJCGridRow retValue;

      retValue = UserGrid.LJCRowAdd();
      SetStoredValuesUser(retValue, dataRecord);
      retValue.LJCSetValues(UserGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateUser(AppUser dataRecord)
    {
      if (UserGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesUser(row, dataRecord);
        row.LJCSetValues(UserGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValuesUser(LJCGridRow row, AppUser dataRecord)
    {
      row.LJCSetInt32(AppUser.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelectUser(AppUser dataRecord)
    {
      int rowID;
      bool retValue = false;

      foreach (LJCGridRow row in UserGrid.Rows)
      {
        rowID = row.LJCGetInt32(AppUser.ColumnID);
        if (rowID == dataRecord.ID)
        {
          UserGrid.LJCSetCurrentRow(row, AllowTrue);
          retValue = true;
          break;
        }
      }
      return retValue;
    }
    #endregion

    #region Program

    // Retrieves the list rows.
    private void DataRetrieveProgram()
    {
      UserAppPrograms records;

      ProgramGrid.LJCRowsClear();
      if (UserGrid.CurrentRow is LJCGridRow parentRow)
      {
        var keyColumns = new DbColumns()
        {
          { UserAppProgram.ColumnAppManagerUserID
            , parentRow.LJCGetInt32(AppUser.ColumnID) }
        };
        var userProgramManager = Managers.UserAppProgramManager;
        DbJoins dbJoins = userProgramManager.GetLoadJoins();
        records = userProgramManager.Load(keyColumns, joins: dbJoins);

        if (NetCommon.HasItems(records))
        {
          foreach (UserAppProgram record in records)
          {
            RowAddProgram(record);
          }
        }
      }
      DoChange(Change.Program);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddProgram(UserAppProgram dataRecord)
    {
      LJCGridRow retValue;

      retValue = ProgramGrid.LJCRowAdd();
      SetStoredValuesProgram(retValue, dataRecord);
      retValue.LJCSetValues(ProgramGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateProgram(UserAppProgram dataRecord)
    {
      if (ProgramGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesProgram(row, dataRecord);
        row.LJCSetValues(ProgramGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValuesProgram(LJCGridRow row
      , UserAppProgram dataRecord)
    {
      row.LJCSetInt32(UserAppProgram.ColumnAppManagerUserID, dataRecord.AppManagerUserID);
      row.LJCSetInt32(UserAppProgram.ColumnAppProgramID, dataRecord.AppProgramID);
      row.LJCSetInt32(UserAppProgram.ColumnActive, Convert.ToInt32(dataRecord.Active));
    }

    // Selects a row based on the key record values.
    private bool RowSelectProgram(UserAppProgram dataRecord)
    {
      int rowID;
      bool retValue = false;

      foreach (LJCGridRow row in ProgramGrid.Rows)
      {
        rowID = row.LJCGetInt32(UserAppProgram.ColumnAppProgramID);
        if (rowID == dataRecord.AppProgramID)
        {
          ProgramGrid.LJCSetCurrentRow(row, AllowTrue);
          retValue = true;
          break;
        }
      }
      return retValue;
    }
    #endregion

    #region Module

    // Retrieves the list rows.
    private void DataRetrieveModule()
    {
      UserAppModules records;
      int userID;
      int programID;

      ModuleGrid.LJCRowsClear();
      if (ProgramGrid.CurrentRow is LJCGridRow parentRow)
      {
        userID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppManagerUserID);
        programID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppProgramID);
        var userModuleManager = Managers.UserAppModuleManager;
        var keyColumns = userModuleManager.GetUserIDKeys(userID, programID);
        DbJoins joins = userModuleManager.GetLoadJoins();
        records = userModuleManager.Load(keyColumns, joins: joins);

        if (NetCommon.HasItems(records))
        {
          foreach (UserAppModule record in records)
          {
            RowAddModule(record);
          }
        }
      }
      DoChange(Change.Module);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddModule(UserAppModule dataRecord)
    {
      LJCGridRow retValue;

      retValue = ModuleGrid.LJCRowAdd();
      SetStoredValuesModule(retValue, dataRecord);
      retValue.LJCSetValues(ModuleGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateModule(UserAppModule dataRecord)
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesModule(row, dataRecord);
        row.LJCSetValues(ModuleGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValuesModule(LJCGridRow row
      , UserAppModule dataRecord)
    {
      row.LJCSetInt32(UserAppModule.ColumnAppModuleID, dataRecord.AppModuleID);
      row.LJCSetInt32(UserAppModule.ColumnActive, Convert.ToInt32(dataRecord.Active));
    }

    // Selects a row based on the key record values.
    private bool RowSelectModule(UserAppModule dataRecord)
    {
      int rowID;
      bool retValue = false;

      foreach (LJCGridRow row in ModuleGrid.Rows)
      {
        rowID = row.LJCGetInt32(UserAppModule.ColumnAppModuleID);
        if (rowID == dataRecord.AppModuleID)
        {
          ModuleGrid.LJCSetCurrentRow(row, AllowTrue);
          retValue = true;
          break;
        }
      }
      return retValue;
    }
    #endregion
    #endregion

    #region Action Methods

    #region User

    // Displays a detail dialog for a new record.
    private void DoNewUser()
    {
      UserDetail detail;

      detail = new UserDetail()
      {
        LJCHelpFileName = LJCHelpFile,
        LJCHelpPageName = "UserDetail.htm"
      };
      detail.Change += new EventHandler<EventArgs>(UserDetail_Change);
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    private void DoEditUser()
    {
      UserDetail detail;

      if (UserGrid.CurrentRow is LJCGridRow row)
      {
        detail = new UserDetail()
        {
          LJCID = row.LJCGetInt32(AppUser.ColumnID),
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = "UserDetail.htm"
        };
        detail.Change += new EventHandler<EventArgs>(UserDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    void UserDetail_Change(object sender, EventArgs e)
    {
      UserDetail detail;
      AppUser record;
      LJCGridRow row;

      detail = sender as UserDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateUser(record);
      }
      else
      {
        row = RowAddUser(record);
        UserGrid.LJCSetCurrentRow(row, AllowTrue);
        TimedChange(Change.User);
      }
    }

    // Deletes the selected row.
    private void DoDeleteUser()
    {
      string title;
      string message;

      if (UserGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = "Are you sure you want to delete the selected item?";
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          int id = row.LJCGetInt32(AppUser.ColumnID);
          var userManager = Managers.AppUserManager;
          var keyColumns = userManager.GetIDKey(id);
          userManager.Delete(keyColumns);
          if (userManager.AffectedCount > 0)
          {
            UserGrid.Rows.Remove(row);
            TimedChange(Change.User);
          }
        }
      }
    }

    // Refreshes the list.
    private void DoRefreshUser()
    {
      AppUser record;
      int id = 0;

      if (UserGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(AppUser.ColumnID);
      }
      DataRetrieveUser();

      // Select the original row.
      if (id > 0)
      {
        record = new AppUser()
        {
          ID = id
        };
        RowSelectUser(record);
      }
    }
    #endregion

    #region Program

    // Displays a detail dialog for a new record.
    private void DoAddProgram()
    {
      ProgramList programList;
      AppProgram appProgram;
      UserAppProgram userAppProgram;
      int userID;

      if (UserGrid.CurrentRow is LJCGridRow parentRow)
      {
        userID = parentRow.LJCGetInt32(AppUser.ColumnID);

        programList = new ProgramList()
        {
          LJCIsSelect = true
        };
        programList.ShowDialog();
        if (programList.DialogResult == DialogResult.OK
          && programList.LJCSelectedRecord != null)
        {
          appProgram = programList.LJCSelectedRecord;
          userAppProgram = new UserAppProgram()
          {
            AppManagerUserID = userID,
            AppProgramID = appProgram.ID,
            FileName = appProgram.FileName,
            Title = appProgram.Title,
            Active = true
          };
          if (RowSelectProgram(userAppProgram))
          {
            RowUpdateProgram(userAppProgram);
          }
          else
          {
            // Add record to UserAppProgram.
            var userProgramManager = Managers.UserAppProgramManager;
            userProgramManager.Add(userAppProgram);
            if (userProgramManager.AffectedCount > 0)
            {
              RowAddProgram(userAppProgram);
            }
            TimedChange(Change.Program);
          }
        }
      }
    }

    // Displays a detail dialog to edit an existing record.
    private void DoEditProgram()
    {
      ProgramDetail detail;

      if (UserGrid.CurrentRow is LJCGridRow parentRow
        && ProgramGrid.CurrentRow is LJCGridRow row)
      {
        detail = new ProgramDetail()
        {
          LJCID = row.LJCGetInt32(UserAppProgram.ColumnAppProgramID),
          LJCUserID = parentRow.LJCGetInt32(AppUser.ColumnID),
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = "ProgramDetail.htm"
        };
        detail.LJCChange += new EventHandler<EventArgs>(ProgramDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ProgramDetail_Change(object sender, EventArgs e)
    {
      ProgramDetail detail;
      AppProgram record;
      UserAppProgram userProgram;

      detail = sender as ProgramDetail;
      record = detail.LJCRecord;
      userProgram = new UserAppProgram()
      {
        AppManagerUserID = detail.LJCUserID,
        AppProgramID = record.ID,
        FileName = record.FileName,
        Title = record.Title
      };
      if (detail.LJCIsUpdate)
      {
        RowUpdateProgram(userProgram);
      }
    }

    // Deletes the selected row.
    private void DoRemoveProgram()
    {
      LJCGridRow parentRow;
      LJCGridRow row;
      string title;
      string message;

      parentRow = UserGrid.CurrentRow as LJCGridRow;
      row = ProgramGrid.CurrentRow as LJCGridRow;
      int userID = parentRow.LJCGetInt32(AppUser.ColumnUserID);
      int programID = row.LJCGetInt32(UserAppProgram.ColumnAppProgramID);

      title = "Remove Confirmation";
      message = "Are you sure you want to remove the selected item?";
      if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
        , MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var userProgramManager = Managers.UserAppProgramManager;
        var keyColumns = userProgramManager.GetIDKeys(userID, programID);
        userProgramManager.Delete(keyColumns);
        if (userProgramManager.AffectedCount > 0)
        {
          ProgramGrid.Rows.Remove(row);
        }
        TimedChange(Change.Program);
      }
    }

    // Refreshes the list.
    private void DoRefreshProgram()
    {
      UserAppProgram record;
      int id = 0;

      if (ProgramGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(AppProgram.ColumnID);
      }
      DataRetrieveProgram();

      // Select the original row.
      if (id > 0)
      {
        record = new UserAppProgram()
        {
          AppProgramID = id
        };
        RowSelectProgram(record);
      }
    }
    #endregion

    #region Module

    // Displays a detail dialog for a new record.
    private void DoAddModule()
    {
      ModuleList moduleList;
      AppModule appModule;
      UserAppModule userAppModule;
      int userID;
      int programID;

      if (ProgramGrid.CurrentRow is LJCGridRow parentRow)
      {
        userID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppManagerUserID);
        programID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppProgramID);

        moduleList = new ModuleList()
        {
          LJCIsSelect = true,
          LJCParentID = programID
        };
        moduleList.ShowDialog();
        if (moduleList.DialogResult == DialogResult.OK
          && moduleList.LJCSelectedRecord != null)
        {
          appModule = moduleList.LJCSelectedRecord;
          userAppModule = new UserAppModule()
          {
            AppManagerUserID = userID,
            AppProgramID = programID,
            AppModuleID = appModule.ID,
            TypeName = appModule.TypeName,
            Title = appModule.Title,
            Active = true
          };
          if (RowSelectModule(userAppModule))
          {
            RowUpdateModule(userAppModule);
          }
          else
          {
            // Add record to UserAppModule.
            var userModuleManager = Managers.UserAppModuleManager;
            userModuleManager.Add(userAppModule);
            if (userModuleManager.AffectedCount > 0)
            {
              RowAddModule(userAppModule);
            }
            TimedChange(Change.Module);
          }
        }
      }
    }

    // Displays a detail dialog to edit an existing record.
    private void DoEditModule()
    {
      ModuleDetail detail;

      if (ProgramGrid.CurrentRow is LJCGridRow parentRow
        && ModuleGrid.CurrentRow is LJCGridRow row)
      {
        detail = new ModuleDetail()
        {
          LJCID = row.LJCGetInt32(UserAppModule.ColumnAppModuleID),
          LJCUserID = row.LJCGetInt32(UserAppProgram.ColumnAppManagerUserID),
          LJCParentID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppProgramID),
          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = "ModuleDetail.htm"
        };
        detail.LJCChange += new EventHandler<EventArgs>(ModuleDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    void ModuleDetail_Change(object sender, EventArgs e)
    {
      ModuleDetail detail;
      AppModule record;
      UserAppModule userModule;

      detail = sender as ModuleDetail;
      record = detail.LJCRecord;
      userModule = new UserAppModule()
      {
        AppManagerUserID = detail.LJCUserID,
        AppProgramID = detail.LJCParentID,
        AppModuleID = record.ID,
        TypeName = record.TypeName,
        ModuleTitle = record.Title
      };
      if (detail.LJCIsUpdate)
      {
        RowUpdateModule(userModule);
      }
    }

    // Deletes the selected row.
    private void DoRemoveModule()
    {
      LJCGridRow parentRow;
      LJCGridRow row;
      string title;
      string message;

      parentRow = ProgramGrid.CurrentRow as LJCGridRow;
      row = ModuleGrid.CurrentRow as LJCGridRow;
      int userID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppManagerUserID);
      int programID = parentRow.LJCGetInt32(UserAppProgram.ColumnAppProgramID);
      int moduleID = row.LJCGetInt32(UserAppModule.ColumnAppModuleID);

      title = "Remove Confirmation";
      message = "Are you sure you want to remove the selected item?";
      if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
        , MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var keyColumns = new DbColumns()
        {
          { UserAppProgram.ColumnAppManagerUserID, userID },
          { UserAppProgram.ColumnAppProgramID, programID },
          { UserAppModule.ColumnAppModuleID, moduleID }
        };
        var userModuleManager = Managers.UserAppModuleManager;
        userModuleManager.Delete(keyColumns);
        if (userModuleManager.AffectedCount > 0)
        {
          ModuleGrid.Rows.Remove(row);
          TimedChange(Change.Module);
        }
      }
    }

    // Refreshes the list.
    private void DoRefreshModule()
    {
      UserAppModule record;
      int id = 0;

      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(AppModule.ColumnID);
      }
      DataRetrieveModule();

      // Select the original row.
      if (id > 0)
      {
        record = new UserAppModule()
        {
          AppModuleID = id
        };
        RowSelectModule(record);
      }
    }
    #endregion
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

      // Set initial control values.
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\User.xml";

      SetupGridUser();
      SetupGridModule();
      SetupGridProgram();
      StartItemChange();
      Cursor = Cursors.Default;
    }

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Make sure lists scroll vertically and counter labels show.
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        int oneThird = UserSplit.Height / 3;
        UserSplit.SplitterDistance = oneThird;
        ProgramSplit.SplitterDistance = oneThird;

        // Modify FacilityPage, FacilitySplit.
        ListHelper.SetPageSplitControl(UserPage, UserSplit);

        // Modify MainSplit.Panel1 controls.
        ListHelper.SetPanelControls(UserSplit.Panel1, null, UserToolPanel
          , UserGrid);

        // Modify MainSplit.Panel2, ChildSplit.
        ListHelper.SetPanelSplitControl(UserSplit.Panel2, ProgramSplit);

        // Modify ChildSplit.Panel1 controls.
        ListHelper.SetPanelControls(UserSplit.Panel1, ProgramHeading
          , ProgramToolPanel, ProgramGrid);

        // Modify ChildSplit.Panel2 controls.
        ListHelper.SetPanelControls(UserSplit.Panel2, ModuleHeading
          , ModuleToolPanel, ModuleGrid);
      }
    }

    // Setup the User grid.
    private void SetupGridUser()
    {
      UserGrid.BackgroundColor = mSettings.BeginColor;
      if (0 == UserGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          AppUser.ColumnName,
          AppUser.ColumnUserID
        };
        DbColumns gridColumns
          = Managers.AppUserManager.GetColumns(propertyNames);
        UserGrid.LJCAddColumns(gridColumns);
      }
    }

    // Setup the Program grid.
    private void SetupGridProgram()
    {
      ProgramGrid.BackgroundColor = mSettings.BeginColor;
      if (0 == ProgramGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          "FileName",
          "Title",
          AppProgram.ColumnActive
        };
        DbColumns gridColumns
          = Managers.UserAppProgramManager.GetColumns(propertyNames);
        ProgramGrid.LJCAddColumns(gridColumns);
      }
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
          "ModuleTitle",
          AppModule.ColumnActive
        };
        DbColumns gridColumns
          = Managers.UserAppModuleManager.GetColumns(propertyNames);
        ModuleGrid.LJCAddColumns(gridColumns);
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      Control parent = UserTabs.Parent;

      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      UserGrid.LJCSaveColumnValues(controlValues);
      ProgramGrid.LJCSaveColumnValues(controlValues);
      ModuleGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("UserSplit.SplitterDistance", 0, 0, 0, UserSplit.SplitterDistance);
      controlValues.Add("ProgramSplit.SplitterDistance", 0, 0, 0, ProgramSplit.SplitterDistance);

      // Save Window values.
      if (parent != null
        && parent.GetType().Name != Name)
      {
        controlValues.Add(Name, parent.Left, parent.Top
          , parent.Width, parent.Height);
      }

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;
      Control parent = UserTabs.Parent;

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
            if (parent != null
              && parent.GetType().Name != Name)
            {
              parent.Left = controlValue.Left;
              parent.Top = controlValue.Top;
              parent.Width = controlValue.Width;
              parent.Height = controlValue.Height;
            }
          }

          // Restore Splitter and other values.
          FormCommon.RestoreSplitDistance(UserSplit, ControlValues);
          FormCommon.RestoreSplitDistance(ProgramSplit, ControlValues);
        }
      }
    }

    /// <summary>Gets or sets the Allow Change value.</summary>
    public ControlValues ControlValues { get; set; }
    #endregion

    #region AppModule Implementation

    /// <summary>Initializes the module.</summary>
    public void LJCInit()
    {
      InitializeControls();
    }

    // Returns a reference to the module tab control.
    /// <include path='items/LJCTabs/*' file='Doc/UserModule.xml'/>
    public TabControl LJCTabs()
    {
      return UserTabs;
    }

    /// <summary>Gets the module assembly name.</summary>
    public string LJCProgramName
    {
      get { return "LJCAppManager.exe"; }
    }

    // Closes the current page.
    /// <include path='items/ClosePage/*' file='Doc/UserModule.xml'/>
    public void ClosePage(TabPage tabPage)
    {
      LJCTabControl parentTabControl;

      // Set current page and invoke event.
      CloseTabPage = tabPage;
      OnPageClose();

      // Collapse tile panel if no tab pages left.
      parentTabControl = tabPage.Parent as LJCTabControl;
      parentTabControl?.LJCCloseEmptyPanel();
    }

    /// <summary>Calls the PageClose event handlers.</summary>
    /// <remarks><para>Syntax: protected void OnPageClose()</para></remarks>
    protected void OnPageClose()
    {
      PageClose?.Invoke(this, new EventArgs());
    }

    /// <summary>Gets or sets the close tab page.</summary>
    public TabPage CloseTabPage { get; set; }

    /// <summary>The page close event.</summary>
    public event EventHandler<EventArgs> PageClose;
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
          UserGrid.LJCRestoreColumnValues(ControlValues);
          ProgramGrid.LJCRestoreColumnValues(ControlValues);
          ModuleGrid.LJCRestoreColumnValues(ControlValues);
          DataRetrieveUser();
          break;

        case Change.User:
          UserGrid.LJCSetLastRow();
          UserGrid.LJCSetCounter(UserCounter);
          DataRetrieveProgram();
          break;

        case Change.Program:
          ProgramGrid.LJCSetLastRow();
          ProgramGrid.LJCSetCounter(ProgramCounter);
          DataRetrieveModule();
          break;

        case Change.Module:
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
      User,
      Program,
      Module
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
      LJCGridRow parentRow = UserGrid.CurrentRow as LJCGridRow;
      bool enableNew = true;
      bool enableEdit = parentRow != null;
      FormCommon.SetToolState(UserTools, enableNew, enableEdit);
      FormCommon.SetMenuState(UserMenu, enableNew, enableEdit);

      enableNew = parentRow != null;
      enableEdit = ProgramGrid.CurrentRow != null;
      FormCommon.SetToolState(ProgramTools, enableNew, enableEdit);
      FormCommon.SetMenuState(ProgramMenu, enableNew, enableEdit);
      if (ProgramGrid.CurrentRow is LJCGridRow row)
      {
        ProgramMenuActive.Text = 1 == row.LJCGetInt32(UserAppProgram.ColumnActive)
          ? "Inactivate" : "Activate";
      }

      enableEdit = ModuleGrid.CurrentRow != null;
      FormCommon.SetToolState(ModuleTools, enableNew, enableEdit);
      FormCommon.SetMenuState(ModuleMenu, enableNew, enableEdit);
      if (ModuleGrid.CurrentRow is LJCGridRow moduleRow)
      {
        ModuleMenuActive.Text = 1 == moduleRow.LJCGetInt32(UserAppModule.ColumnActive)
          ? "Inactivate" : "Activate";
      }
    }
    #endregion

    #region Action Event Handlers

    #region User

    // Call the New method.
    private void UserToolNew_Click(object sender, EventArgs e)
    {
      DoNewUser();
    }

    // Call the Edit method.
    private void UserToolEdit_Click(object sender, EventArgs e)
    {
      DoEditUser();
    }

    // Call the Delete method.
    private void UserToolDelete_Click(object sender, EventArgs e)
    {
      DoDeleteUser();
    }

    // Call the New method.
    private void UserMenuNew_Click(object sender, EventArgs e)
    {
      DoNewUser();
    }

    // Call the Edit method.
    private void UserMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditUser();
    }

    // Call the Delete method.
    private void UserMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteUser();
    }

    // Call the Refresh method.
    private void UserMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshUser();
    }

    private void UserMenuText_Click(object sender, EventArgs e)
    {
      var fileSpec
        = $"ExportUser.{mSettings.ExportTextExtension}";
      UserGrid.LJCExportData(fileSpec);
    }

    private void UserMenuCSV_Click(object sender, EventArgs e)
    {
      UserGrid.LJCExportData("ExportUser.csv");
    }

    // Perform the Close function.
    private void UserMenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      ClosePage(UserPage);
    }
    #endregion

    #region Program

    // Call the New method.
    private void ProgramToolNew_Click(object sender, EventArgs e)
    {
      DoAddProgram();
    }

    // Call the Edit method.
    private void ProgramToolEdit_Click(object sender, EventArgs e)
    {
      DoEditProgram();
    }

    // Call the Delete method.
    private void ProgramToolDelete_Click(object sender, EventArgs e)
    {
      DoRemoveProgram();
    }

    // Call the New method.
    private void ProgramMenuNew_Click(object sender, EventArgs e)
    {
      DoAddProgram();
    }

    // Call the Edit method.
    private void ProgramMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditProgram();
    }

    // Call the Delete method.
    private void ProgramMenuDelete_Click(object sender, EventArgs e)
    {
      DoRemoveProgram();
    }

    // Call the Refresh method.
    private void ProgramMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshProgram();
    }

    // Activates or inactivates the selected item.
    private void ProgramMenuActive_Click(object sender, EventArgs e)
    {
      if (UserGrid.CurrentRow is LJCGridRow parentRow
        && ProgramGrid.CurrentRow is LJCGridRow row)
      {
        int active = 1 == row.LJCGetInt32(UserAppProgram.ColumnActive) ? 0 : 1;

        UserAppProgram dataRecord = new UserAppProgram()
        {
          Active = 1 == active
        };
        int parentID = parentRow.LJCGetInt32(AppUser.ColumnID);
        int childID = row.LJCGetInt32(UserAppProgram.ColumnAppProgramID);
        var userProgramManager = Managers.UserAppProgramManager;
        var keyColumns = userProgramManager.GetIDKeys(parentID, childID);
        userProgramManager.Update(dataRecord, keyColumns);
        if (userProgramManager.AffectedCount > 0)
        {
          row.LJCSetCellText(UserAppProgram.ColumnActive, dataRecord.Active);
          row.LJCSetInt32(UserAppProgram.ColumnActive, active);
        }
      }
    }

    // Exports the list items to a tab delimited text file.
    private void ProgramMenuText_Click(object sender, EventArgs e)
    {
      var fileSpec
        = $"ExportAuthProgram.{mSettings.ExportTextExtension}";
      ProgramGrid.LJCExportData(fileSpec);
    }

    // Exports the list items to a CSV file.
    private void ProgramMenuCSV_Click(object sender, EventArgs e)
    {
      ProgramGrid.LJCExportData("ExportAuthProgram.csv");
    }

    // Perform the Close function.</summary>
    private void ProgramMenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      ClosePage(UserPage);
    }
    #endregion

    #region Module

    // Call the New method.
    private void ModuleToolNew_Click(object sender, EventArgs e)
    {
      DoAddModule();
    }

    // Call the Edit method.
    private void ModuleToolEdit_Click(object sender, EventArgs e)
    {
      DoEditModule();
    }

    // Call the Delete method.
    private void ModuleToolDelete_Click(object sender, EventArgs e)
    {
      DoRemoveModule();
    }

    // Call the New method.
    private void ModuleMenuNew_Click(object sender, EventArgs e)
    {
      DoAddModule();
    }

    // Call the Edit method.
    private void ModuleMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditModule();
    }

    // Call the Delete method.
    private void ModuleMenuDelete_Click(object sender, EventArgs e)
    {
      DoRemoveModule();
    }

    // Activates or inactivates the selected item.
    private void ModuleMenuActive_Click(object sender, EventArgs e)
    {
      if (UserGrid.CurrentRow is LJCGridRow userRow
        && ProgramGrid.CurrentRow is LJCGridRow parentRow
        && ModuleGrid.CurrentRow is LJCGridRow row)
      {
        int active = 1 == row.LJCGetInt32(UserAppModule.ColumnActive) ? 0 : 1;

        UserAppModule dataRecord = new UserAppModule()
        {
          Active = 1 == active
        };
        var keyColumns = new DbColumns()
        {
          { AppUser.ColumnID, userRow.LJCGetInt32(AppUser.ColumnID) },
          { UserAppProgram.ColumnAppProgramID
            , parentRow.LJCGetInt32(UserAppProgram.ColumnAppProgramID) },
          { UserAppModule.ColumnAppModuleID
            , row.LJCGetInt32(UserAppModule.ColumnAppModuleID) }
        };
        var userModuleManager = Managers.UserAppModuleManager;
        userModuleManager.Update(dataRecord, keyColumns);
        if (userModuleManager.AffectedCount > 0)
        {
          row.LJCSetCellText(UserAppModule.ColumnActive, dataRecord.Active);
          row.LJCSetInt32(UserAppModule.ColumnActive, active);
        }
      }
    }

    // Call the Refresh method.
    private void ModuleMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshModule();
    }

    private void ModuleMenuText_Click(object sender, EventArgs e)
    {
      var fileSpec
        = $"ExportAuthModule.{mSettings.ExportTextExtension}";
      ModuleGrid.LJCExportData(fileSpec);
    }

    private void ModuleMenuCSV_Click(object sender, EventArgs e)
    {
      ModuleGrid.LJCExportData("ExportAuthModule.csv");
    }

    // Perform the Close function.
    private void ModuleMenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      ClosePage(UserPage);
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region User

    // Handles the form keys.
    private void UserGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "UserList.htm");
          break;

        case Keys.F5:
          DoRefreshUser();
          e.Handled = true;
          break;

        case Keys.Enter:
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ModuleGrid.Select();
          }
          else
          {
            ProgramGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void UserGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && UserGrid.LJCIsDifferentRow(e))
      {
        UserGrid.LJCSetCurrentRow(e);
        TimedChange(Change.User);
      }
    }

    // Handles the SelectionChanged event.
    private void UserGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (UserGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.User);
      }
      UserGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void UserGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (UserGrid.LJCGetMouseRow(e) != null)
      {
        DoEditUser();
      }
    }
    #endregion

    #region Program

    // Handles the form keys.
    private void ProgramGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "AuthProgramList.htm");
          break;

        case Keys.F5:
          DoRefreshProgram();
          e.Handled = true;
          break;

        case Keys.Enter:
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UserGrid.Select();
          }
          else
          {
            ModuleGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void ProgramGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && ProgramGrid.LJCIsDifferentRow(e))
      {
        ProgramGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Program);
      }
    }

    // Handles the SelectionChanged event.
    private void ProgramGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ProgramGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Program);
      }
      ProgramGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ProgramGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ProgramGrid.LJCGetMouseRow(e) != null)
      {
        DoEditProgram();
      }
    }
    #endregion

    #region Module

    // Handles the form keys.
    private void ModuleGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , "AuthModuleList.htm");
          break;

        case Keys.F5:
          DoRefreshModule();
          e.Handled = true;
          break;

        case Keys.Enter:
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ProgramGrid.Select();
          }
          else
          {
            UserGrid.Select();
          }
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
        TimedChange(Change.Module);
      }
    }

    // Handles the SelectionChanged event.
    private void ModuleGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ModuleGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Module);
      }
      ModuleGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ModuleGrid.LJCGetMouseRow(e) != null)
      {
        DoEditModule();
      }
    }
    #endregion
    #endregion

    #region Properties

    // The help file name.
    internal string LJCHelpFile { get; set; }

    // Gets or sets the Managers value.
    private AppManagers Managers { get; set; }
    #endregion

    #region Class Data

    private const bool AllowTrue = true;

    private StandardUISettings mSettings;
    private string mControlValuesFileName;
    #endregion
  }
}
