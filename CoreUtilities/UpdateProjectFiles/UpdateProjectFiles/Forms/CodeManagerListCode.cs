// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeManagerListCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.IO;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  // The list form code.
  internal partial class CodeManagerList : Form
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

          // Load first control.
          mCodeLineGridCode.DataRetrieve();
          break;

        case Change.CodeLine:
          var codeGroupParentKey = GetCodeGroupParentKey();
          mCodeGroupGridCode.DataRetrieve(codeGroupParentKey);
          break;

        case Change.CodeGroup:
          var solutionParentKey = GetSolutionParentKey();
          mSolutionGridCode.DataRetrieve(solutionParentKey);
          break;

        case Change.Solution:
          var projectParentKey = GetProjectParentKey();
          mProjectGridCode.DataRetrieve(projectParentKey);
          break;

        case Change.Project:
          mProjectFileGridCode.DataRetrieve();
          break;

        case Change.ProjectFile:
          ProjectFileGrid.LJCSetLastRow();
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      CodeLine,
      CodeGroup,
      Solution,
      Project,
      ProjectFile,
    }

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

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        CodeLineSplit.SplitterWidth = 4;
        SolutionSplit.SplitterWidth = 4;

        ListHelper.SetPanelControls(CodeLineSplit.Panel2, CodeGroupHeader
          , null, CodeGroupGrid);
        ListHelper.SetPanelControls(SolutionSplit.Panel2, ProjectHeader
          , null, ProjectGrid);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
      InitializeClassData();
      SetupGridCode();
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
      // Provides additional Drag features between split LJCTabControls.
      var _ = new LJCPanelManager(TabsSplit, MainTabs, TileTabs);
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      ConfigValues = ValuesUpdateProjectFiles.Instance;
      Data = ConfigValues.Data;
      Managers = ConfigValues.Managers;
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mValuesFileSpec = @"ControlValues\CodeManagerList.xml";

      // Splitter is not in the first TabPage.
      SolutionSplit.Resize += SolutionSplit_Resize;

      BackColor = ConfigValues.BeginColor;
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(mValuesFileSpec))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , mValuesFileSpec) as ControlValues;

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
          FormCommon.RestoreSplitDistance(CodeLineSplit, ControlValues);
          FormCommon.RestoreSplitDistance(SolutionSplit, ControlValues);

          CodeLineGrid.LJCRestoreColumnValues(ControlValues);
          CodeGroupGrid.LJCRestoreColumnValues(ControlValues);
          SolutionGrid.LJCRestoreColumnValues(ControlValues);
          ProjectGrid.LJCRestoreColumnValues(ControlValues);
          ProjectFileGrid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      CodeLineGrid.LJCSaveColumnValues(controlValues);
      CodeGroupGrid.LJCSaveColumnValues(controlValues);
      SolutionGrid.LJCSaveColumnValues(controlValues);
      ProjectGrid.LJCSaveColumnValues(controlValues);
      ProjectFileGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("CodeLineSplit.SplitterDistance"
        , height: CodeLineSplit.SplitterDistance);
      controlValues.Add("SolutionSplit.SplitterDistance"
        , height: SolutionSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mValuesFileSpec);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      mCodeLineGridCode = new CodeLineGridCode(this);
      mCodeGroupGridCode = new CodeGroupGridCode(this);
      mSolutionGridCode = new SolutionGridCode(this);
      mProjectGridCode = new ProjectGridCode(this);
      mProjectFileGridCode = new ProjectFileGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      mCodeLineGridCode.SetupGrid();
      mCodeGroupGridCode.SetupGrid();
      mSolutionGridCode.SetupGrid();
      mProjectGridCode.SetupGrid();
      mProjectFileGridCode.SetupGrid();
    }

    // Splitter is not in the first TabPage so Set values on first display.
    private void SolutionSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (!mIsSolutionSplitSet)
        {
          FormCommon.RestoreSplitDistance(SolutionSplit, ControlValues);
        }
        mIsSolutionSplitSet = true;
      }
    }
    private bool mIsSolutionSplitSet;

    /// <summary>Gets or sets the ControlValues item.</summary>
    internal ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Creates the CodeGroup parent key.
    private string GetCodeGroupParentKey()
    {
      string retValue = null;

      if (CodeLineGrid.CurrentRow is LJCGridRow row)
      {
        retValue = row.LJCGetString("Name");
      }
      return retValue;
    }

    // Creates the Solution parent key.
    private SolutionParentKey GetSolutionParentKey()
    {
      SolutionParentKey retValue = null;

      if (CodeGroupGrid.CurrentRow is LJCGridRow row)
      {
        retValue = new SolutionParentKey()
        {
          CodeLine = row.LJCGetString("CodeLine"),
          CodeGroup = row.LJCGetString("CodeGroup")
        };
      }
      return retValue;
    }

    // Creates the Solution parent key.
    private ProjectParentKey GetProjectParentKey()
    {
      ProjectParentKey retValue = null;

      if (SolutionGrid.CurrentRow is LJCGridRow row)
      {
        retValue = new ProjectParentKey()
        {
          CodeLine = row.LJCGetString("CodeLine"),
          CodeGroup = row.LJCGetString("CodeGroup"),
          Solution = row.LJCGetString("Name")
        };
      }
      return retValue;
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = CodeLineGrid.CurrentRow != null;
      FormCommon.SetMenuState(CodeLineMenu, enableNew, enableEdit);

      enableNew = CodeLineGrid.CurrentRow != null;
      enableEdit = CodeGroupGrid.CurrentRow != null;
      FormCommon.SetMenuState(CodeGroupMenu, enableNew, enableEdit);

      enableNew = CodeGroupGrid.CurrentRow != null;
      enableEdit = SolutionGrid.CurrentRow != null;
      FormCommon.SetMenuState(SolutionMenu, enableNew, enableEdit);

      enableNew = SolutionGrid.CurrentRow != null;
      enableEdit = ProjectGrid.CurrentRow != null;
      FormCommon.SetMenuState(ProjectMenu, enableNew, enableEdit);

      enableNew = ProjectGrid.CurrentRow != null;
      enableEdit = ProjectFileGrid.CurrentRow != null;
      FormCommon.SetMenuState(ProjectFileMenu, enableNew, enableEdit);
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(MouseEventArgs e)
    {
      var tabPage = MainTabs.LJCGetTabPage(e);
      switch (tabPage.Name)
      {
        case "CodeLineTab":
          CodeLineGrid.Select();
          break;

        case "SolutionTab":
          SolutionGrid.Select();
          break;

        case "FileTab":
          ProjectFileGrid.Select();
          break;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Data object.
    internal Data Data { get; set; }

    // The Managers object.
    internal ManagersProjectFiles Managers { get; set; }

    // The Configuration Settings.
    private ValuesUpdateProjectFiles ConfigValues { get; set; }
    #endregion

    #region Class Data

    private string mValuesFileSpec;
    private CodeLineGridCode mCodeLineGridCode;
    private CodeGroupGridCode mCodeGroupGridCode;
    private SolutionGridCode mSolutionGridCode;
    private ProjectGridCode mProjectGridCode;
    private ProjectFileGridCode mProjectFileGridCode;
    #endregion
  }
}
