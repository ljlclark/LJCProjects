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
using UpdateProjectFiles.Properties;

namespace UpdateProjectFiles
{
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
          mCodeGroupGridCode.DataRetrieve();
          break;

        case Change.CodeGroup:
          mSolutionGridCode.DataRetrieve();
          break;

        case Change.Solution:
          mProjectGridCode.DataRetrieve();
          break;

        case Change.Project:
          mProjectFileGridCode.DataRetrieve();
          break;

        case Change.ProjectFile:
          FileGrid.LJCSetLastRow();
          break;
      }
      //SetControlState();
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
      Values = ValuesUpdateProjectFiles.Instance;
      Managers = Values.Managers;
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mValuesFileSpec = @"ControlValues\GenDocList.xml";

      // Splitter is not in the first TabPage.
      //ClassSplit.Resize += ClassSplit_Resize;

      BackColor = Values.BeginColor;
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
          //FormCommon.RestoreSplitDistance(AssemblySplit, ControlValues);
          //FormCommon.RestoreSplitDistance(ClassSplit, ControlValues);

          CodeLineGrid.LJCRestoreColumnValues(ControlValues);
          CodeGroupGrid.LJCRestoreColumnValues(ControlValues);
          SolutionGrid.LJCRestoreColumnValues(ControlValues);
          ProjectGrid.LJCRestoreColumnValues(ControlValues);
          FileGrid.LJCRestoreColumnValues(ControlValues);
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

      FileGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      //controlValues.Add("AssemblySplit.SplitterDistance"
      //  , height: AssemblySplit.SplitterDistance);
      //controlValues.Add("ClassSplit.SplitterDistance"
      //  , height: ClassSplit.SplitterDistance);

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

    /// <summary>Gets or sets the ControlValues item.</summary>
    internal ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Properties

    // The Managers object.
    internal ManagersProjectFiles Managers { get; set; }

    // The Configuration Settings.
    private ValuesUpdateProjectFiles Values { get; set; }
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
