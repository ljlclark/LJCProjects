// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocListCode.cs
using LJCDBClientLib;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal partial class LJCGenDocList : Form
  {
    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        AssemblySplit.SplitterWidth = 4;
        ClassSplit.SplitterWidth = 4;
        ClassSplit.Top += 4;
        TabsSplit.SplitterWidth = 4;

        ListHelper.SetPanelControls(AssemblySplit.Panel1, AssemblyHeading
          , null, AssemblyGrid);
        ListHelper.SetPanelControls(AssemblySplit.Panel2, AssemblyItemHeading
          , null, AssemblyItemGrid);
        ListHelper.SetPanelControls(ClassSplit.Panel1, ClassHeading
          , null, ClassGrid);
        ListHelper.SetPanelControls(AssemblySplit.Panel2, ClassItemHeading
          , null, ClassItemGrid);
        ListHelper.SetPanelControls(ClassSplit.Panel1, ClassHeading
          , null, ClassGrid);
        ListHelper.SetPanelControls(ClassSplit.Panel2, ClassItemHeading
          , null, ClassItemGrid);
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      Cursor = Cursors.WaitCursor;
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
      // Provides additional Drag features between split LJCTabControls.
      var _ = new LJCPanelManager(TabsSplit, MainTabs, TileTabs);
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\_ClassName_.xml";

      // Splitter is not in the first TabPage.
      ClassSplit.Resize += ClassSplit_Resize;

      BackColor = mSettings.BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesGenDocEdit.Instance;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;

      Managers = new ManagersDocGen();
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
          FormCommon.RestoreSplitDistance(AssemblySplit, ControlValues);
          FormCommon.RestoreSplitDistance(ClassSplit, ControlValues);
          FormCommon.RestoreSplitDistance(TabsSplit, ControlValues);

          //_ClassName_Grid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      //_ClassName_Grid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("AssemblySplit.SplitterDistance", 0, 0, 0
        , AssemblySplit.SplitterDistance);
      controlValues.Add("ClassSplit.SplitterDistance", 0, 0, 0
        , ClassSplit.SplitterDistance);
      controlValues.Add("TabsSplit.SplitterDistance", 0, 0, 0
        , TabsSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(this.Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      mAssemblyGroupGridCode = new AssemblyGroupGridCode(this);
      mAssemblyItemGridCode = new AssemblyItemGridCode(this);
      mAssemblyItemComboCode = new AssemblyItemComboCode(this);
      mClassGridCode = new ClassGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      SetupGridAssembly();
      SetupGridAssemblyItem();
    }

    // Setup the grid display columns.
    private void SetupGridAssembly()
    {
      AssemblyGrid.BackgroundColor = mSettings.BeginColor;

      // Setup default display columns if no columns are defined.
      if (0 == AssemblyGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssemblyGroup.ColumnName,
          DocAssemblyGroup.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var assemblyGroupManager = Managers.DocAssemblyGroupManager;
        mDisplayColumnsAssembly = assemblyGroupManager.GetColumns(columnNames);

        // Setup the grid display columns.
        AssemblyGrid.LJCAddDisplayColumns(mDisplayColumnsAssembly);
      }
    }
    private DbColumns mDisplayColumnsAssembly;

    // Setup the grid display columns.
    private void SetupGridAssemblyItem()
    {
      AssemblyItemGrid.BackgroundColor = mSettings.BeginColor;

      // Setup default display columns if no columns are defined.
      if (0 == AssemblyItemGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssembly.ColumnName,
          DocAssembly.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        var assemblyManager = Managers.DocAssemblyManager;
        mDisplayColumnsAssemblyItem = assemblyManager.GetColumns(columnNames);

        // Setup the grid display columns.
        AssemblyItemGrid.LJCAddDisplayColumns(mDisplayColumnsAssemblyItem);
      }
    }
    private DbColumns mDisplayColumnsAssemblyItem;

    // Splitter is not in the first TabPage so Set values on first display.
    private void ClassSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (false == mIsClassSplitSet)
        {
          FormCommon.RestoreSplitDistance(ClassSplit, ControlValues);
        }
        mIsClassSplitSet = true;
      }
    }
    private bool mIsClassSplitSet;

    /// <summary>Gets or sets the ControlValues item.</summary>
    private ControlValues ControlValues { get; set; }
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
          mAssemblyGroupGridCode.DataRetrieve();
          break;

        case Change.AssemblyGroup:
          mAssemblyItemGridCode.DataRetrieve();
          mAssemblyItemComboCode.DataRetrieve();
          mAssemblyItemComboCode.RowSelect();
          break;

        case Change.AssemblyItem:
          mAssemblyItemComboCode.RowSelect();
          AssemblyItemGrid.LJCSetLastRow();
          break;

        case Change.AssemblyCombo:
          mClassGridCode.DataRetrieve();
          break;

        case Change.ClassGroup:
          break;

        case Change.ClassItem:
          break;

        case Change.ClassCombo:
          break;

        case Change.MethodGroup:
          break;

        case Change.MethodItem:
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      AssemblyGroup,
      AssemblyItem,
      AssemblyCombo,
      ClassGroup,
      ClassItem,
      ClassCombo,
      MethodGroup,
      MethodItem
    }

    #region Item Change Support

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

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    private ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Private Methods

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
    }
    #endregion

    #region Internal Properties

    // The Managers object.
    internal ManagersDocGen Managers { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // Gets or sets the _ClassName_GridClass value.
    //private _ClassName_GridClass _ClassName_GridClass { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;
    private AssemblyGroupGridCode mAssemblyGroupGridCode;
    private AssemblyItemGridCode mAssemblyItemGridCode;
    private AssemblyItemComboCode mAssemblyItemComboCode;
    private ClassGridCode mClassGridCode;

    // Foreign Keys
    #endregion
  }
}
