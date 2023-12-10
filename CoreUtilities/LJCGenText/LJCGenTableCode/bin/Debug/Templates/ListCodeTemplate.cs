// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
// _FullAppName_.cs
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using _FullAppName_DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _Namespace_
{
  // The list form.
  /// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  internal partial class _ClassName_List : Form
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
          DataRetrieveCombo();
          break;

        case Change.Combo:
          DataRetrieve_ClassName_();
          break;

        case Change._ClassName_:
          //DataRetrieveChild();
          _ClassName_Grid.LJCSetLastRow();
          //_ClassName_Grid.LJCSetCounter(_ClassName_Counter);
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

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        //Split.SplitterWidth = 4;

        ListHelper.SetPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
          , _ClassName_ToolPanel, _ClassName_Grid);
        //_ClassName_Grid.Height = ClientSize.Height - _ClassName_Tools.Height;

        //ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
        //	, ChildToolPanel, ChildGrid);
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
      var values = Values_AppName_.Instance;
      mSettings = values.StandardSettings;

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

    // Setup the grid code references.
    private void SetupGridCode()
    {
      //	m_ClassName_GridCode = new _ClassName_GridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      mClassName_GridCode.SetupGrid();
    }

    // Splitter is not in the first TabPage so Set values on first display.
    //private void Split_Resize(object sender, EventArgs e)
    //{
    //	if (ControlValues != null)
    //	{
    //		if (!mIsSplitSet)
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
      bool enableNew = Combo.SelectedIndex != -1;
      bool enableEdit = _ClassName_Grid.CurrentRow != null;
      FormCommon.SetToolState(_ClassName_Tool, enableNew, enableEdit);
      FormCommon.SetMenuState(_ClassName_Menu, enableNew, enableEdit);
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(MouseEventArgs e)
    {
      var tabPage = MainTabs.LJCGetTabPage(e);
      switch (tabPage.Name)
      {
        case "_ClassName_Tab":
          _ClassName_Grid.Select();
          break;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the LJCIsSelect value.
    internal bool LJCIsSelect { get; set; }

    // Gets a reference to the selected record.
    internal _ClassName_ LJCSelectedRecord { get; private set; }

    // Gets or sets the _ClassName_GridCode value.
    private _ClassName_GridCode _ClassName_GridCode { get; set; }

    // The Managers object.
    private Managers_ClassName_ Managers { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;

    // Foreign Keys
    #endregion
  }
}
// #SectionEnd Class
