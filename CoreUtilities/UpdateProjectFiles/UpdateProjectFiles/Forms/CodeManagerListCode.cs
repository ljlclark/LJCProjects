// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeManagerListCode.cs
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Windows.Forms;

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
          //RestoreControlValues();

          // Load first control.
          mCodeLineGridCode.DataRetrieve();
          break;

        case Change.CodeLine:
          //mCodeGroupGridCode.DataRetrieve();
          break;

        case Change.CodeGroup:
          //mSolutionGridCode.DataRetrieve();
          break;

        case Change.Solution:
          //mProjectGridCode.DataRetrieve();
          break;

        case Change.Project:
          //mProjectFileGridCode.DataRetrieve();
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
      SetupGrids();
      StartChangeProcessing();
      Cursor = Cursors.Default;
    }

    #region Setup Support

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesUpdateProjectFiles.Instance;
      Managers = values.Managers;
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
    }
    #endregion
    #endregion

    #region Properties

    // The Managers object.
    internal ManagersProjectFiles Managers { get; set; }
    #endregion

    #region Class Data

    private CodeLineGridCode mCodeLineGridCode;
    private CodeGroupGridCode mCodeGroupGridCode;
    private SolutionGridCode mSolutionGridCode;
    private ProjectGridCode mProjectGridCode;
    private ProjectFileGridCode mProjectFileGridCode;
    #endregion
  }
}
