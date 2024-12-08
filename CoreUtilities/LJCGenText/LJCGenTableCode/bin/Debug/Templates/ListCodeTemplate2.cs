// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _FullAppName_
// _FullAppName_ListCode.cs
using _FullAppName_DAL;
// #SectionEnd Title
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using system.IO;
using System.Windows.Forms;

// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
namespace _Namespace_
{
	// The list form code.
	/// <include path='items/ListFormDAW/*' file='../../LJCGenDoc/Common/List.xml'/>
	internal partial class _FullAppName_List : Form
	{
    #region Setup Methods

    // Configure the initial control settings.
    // ********************
    private void ConfigureControls()
		{
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				//Split.SplitterWidth = 4;

				//ListHelper.SetPanelControls(_ClassName_Split.Panel1, _ClassName_Heading
				//	, _ClassName_ToolPanel, _ClassName_Grid);
				//_ClassName_Grid.Height = ClientSize.Height - _ClassName_Tools.Height;

				//ListHelper.SetPanelControls(_ClassName_Split.Panel2, ChildHeading
				//	, ChildToolPanel, ChildGrid);
			}
		}

    // Configures the controls and loads the selection control data.
    // ********************
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

    // Initialize the Class Data.
    // ********************
    private void InitializeClassData()
    {
      var values = Values_AppName_.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (NetString.HasValue(errors))
      {
        MessageBox.Show(errors, "Config Errors", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      Managers = Values.Managers;
      Settings = values.StandardSettings;
      Text += $" - {Settings.DataConfigName}";
    }

    // Setup the grid code references.
    // ********************
    private void SetupGridCode()
    {
      _ClassName_GridCode = new _ClassName_GridCode(this);
    }

    // Loads the initial Control data.
    // ********************
    private void LoadControlData()
    {
      //ComboItems comboItems = ComboManager.Load();
      //foreach (ComboItem comboItem in comboItems)
      //{
      //	Combo.Items.Add(comboItem);
      //}
    }

    // Initial Control setup.
    // ********************
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
    // ********************
    private void InitialControlValues()
		{
			NetFile.CreateFolder("ExportFiles");
			NetFile.CreateFolder("ControlValues");
			ControlValuesFileName = @"ControlValues\_ClassName_.xml";

			// Splitter is not in the first TabPage.
			//Split.Resize += Split_Resize;

      // *** Next Statement *** Delete
			//BackColor = Settings.BeginColor;
			//MainTools.BackColor = Settings.BeginColor;
		}

    // Setup the data grids.
    // ********************
    private void SetupGrids()
    {
      _ClassName_GridCode.SetupGrid();
    }

    // Splitter is not in the first TabPage so Set values on first display.
    // ********************
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
    #endregion

    #region Private Methods

    // Restores the control values.
    // ********************
    private void RestoreControlValues()
    {
      ControlValue controlValue;
      Control parent = _ClassName_Tabs.Parent;

      if (File.Exists(ControlValuesFileName))
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

          // Restore Grid column sizes.
          //Grid.LJCRestoreColumnValues(ControlValues);

          // Restore Font sizes.
          //FormCommon.RestoreTabsFontSize(MainTabs, ControlValues);
          //Grid.LJCRestoreFontSize(ControlValues);

          // Restore Menu Font sizes.
          //FormCommon.RestoreMenuFontSize(Menu, ControlValues);

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
    // ********************
    private void SaveControlValues()
    {
      Control parent = _ClassName_Tabs.Parent;
      ControlValues controlValues = new ControlValues();

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

      // Save Grid column sizes.
      //Grid.LJCSaveColumnValues(controlValues);

      // Save Font sizes.
      //FormCommon.SaveTabFontSize(MainTabs, controlValues);
      //Grid.LJCSaveFontSize(controlValues);

      // Save Menu Font sizes.
      //FormCommon.SaveMenuFontSize(Menu, controlValues);

      // Save other values.
      //mViewDataId = ViewCombo.LJCSelectedItemId();
      //controlValues.Add("View", mViewDataId, 0, 0, 0);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , ControlValuesFileName);
    }

  //  // Sets the control states based on the current control values.
  //  // ********************
  //  private void SetControlState()
		//{
		//	bool enableNew = Combo.SelectedIndex != -1;
		//	bool enableEdit = _ClassName_Grid.CurrentRow != null;
		//	FormCommon.SetToolState(_ClassName_Tool, enableNew, enableEdit);
		//	FormCommon.SetMenuState(_ClassName_Menu, enableNew, enableEdit);
		//}

    // Sets the tab initial focus control.
    // ********************
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

		// The Managers object.
		internal Managers_ClassName_ Managers { get; set; }

    // Grid Code
    // Gets or sets the _ClassName_GridCode value.
    private _ClassName_GridCode _ClassName_GridCode { get; set; }

    // Gets or sets the ControlValues object.
    private ControlValues ControlValues { get; set; }

    private string ControlValuesFileName { get; set; }

    private StandardUISettings Settings { get; set; }
    #endregion
  }
}
// #SectionEnd Class
