// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocListCode.cs
using LJCDBClientLib;
// *** Next Statement *** Add - Data Views
using LJCDBViewControls;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  internal partial class LJCGenDocList : Form
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
          mAssemblyGroupGridCode.DataRetrieve();
          break;

        case Change.AssemblyGroup:
          mAssemblyItemGridCode.DataRetrieve();
          AssemblyGroupGrid.Select();
          break;

        case Change.AssemblyItem:
          mClassGroupGridCode.DataRetrieve();
          mAssemblyItemComboCode.DataRetrieve();
          AssemblyComboSelect();
          break;

        case Change.AssemblyCombo:
          AssemblyItemSelect();
          break;

        case Change.ClassGroup:
          mClassItemGridCode.DataRetrieve();
          break;

        case Change.ClassItem:
          mMethodGroupGridCode.DataRetrieve();
          mClassItemComboCode.DataRetrieve();
          ClassComboSelect();
          break;

        case Change.ClassCombo:
          ClassItemSelect();
          break;

        case Change.MethodGroup:
          mMethodItemGridCode.DataRetrieve();
          break;

        case Change.MethodItem:
          MethodItemGrid.LJCSetLastRow();
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
        AssemblySplit.SplitterWidth = 4;
        ClassSplit.SplitterWidth = 4;
        ClassSplit.Top += 5;
        TabsSplit.SplitterWidth = 4;
        MethodSplit.Top += 5;

        // *** Begin *** Add - Data Views
        AssemblyGroupHeader.Height += 4;
        AssemblyItemHeader.Height += 4;
        ClassGroupHeader.Height += 4;
        ClassItemHeader.Height += 4;
        MethodGroupHeader.Height += 4;
        MethodItemHeader.Height += 4;

        ClassGroupViewCombo.Top += 1;
        ClassViewCombo.Top += 1;
        MethodGroupViewCombo.Top += 1;
        MethodViewCombo.Top += 1;

        AssemblyGroupGrid.Top += 6;
        AssemblyItemGrid.Top += 6;
        ClassGroupGrid.Top += 6;
        ClassItemGrid.Top += 6;
        MethodGroupGrid.Top += 6;
        MethodItemGrid.Top += 6;
        // *** End   *** Add - Data Views

        ListHelper.SetPanelControls(AssemblySplit.Panel1, AssemblyGroupHeader
          , null, AssemblyGroupGrid);
        ListHelper.SetPanelControls(AssemblySplit.Panel2, AssemblyItemHeader
          , null, AssemblyItemGrid);

        ListHelper.SetPanelControls(ClassSplit.Panel1, ClassGroupHeader
          , null, ClassGroupGrid);
        ListHelper.SetPanelControls(AssemblySplit.Panel2, ClassItemHeader
          , null, ClassItemGrid);

        //ListHelper.SetPanelControls(ClassSplit.Panel1, ClassGroupHeader
        //  , null, ClassGroupGrid);
        //ListHelper.SetPanelControls(ClassSplit.Panel2, ClassItemHeader
        //  , null, ClassItemGrid);
        //ClassItemGrid.Height -= 2;

        ListHelper.SetPanelControls(MethodSplit.Panel1, MethodGroupHeader
          , null, MethodGroupGrid);
        ListHelper.SetPanelControls(MethodSplit.Panel2, MethodItemHeader
          , null, MethodItemGrid);
        MethodItemGrid.Height -= 2;
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
    #endregion

    #region Setup Support

    // Initial Control setup.
    private void ControlSetup()
    {
      // Provides additional Drag features between split LJCTabControls.
      var _ = new LJCPanelManager(TabsSplit, MainTabs, TileTabs);
    }

    // Gets the saved View DataID.
    // *** New Method *** Data Views
    private long ControlValueDataID(string name)
    {
      long retValue = 0;

      var controlValue = ControlValues.LJCSearchName(name);
      if (controlValue != null
        && controlValue.Left > 0)
      {
        retValue = controlValue.Left;
      }
      return retValue;
    }

    // Set initial Control values.
    private void InitialControlValues()
    {
      NetFile.CreateFolder("ExportFiles");
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\GenDocList.xml";

      // Splitter is not in the first TabPage.
      ClassSplit.Resize += ClassSplit_Resize;

      BackColor = Settings.BeginColor;
    }

    // Initialize the Class Data.
    private void InitializeClassData()
    {
      var values = ValuesGenDocEdit.Instance;
      Settings = values.StandardSettings;
      ConfigFileName = values.ConfigFileName;

      Managers = new ManagersGenDoc();
      Managers.SetDBProperties(Settings.DbServiceRef
        , Settings.DataConfigName);
      var dbServiceRef = Settings.DbServiceRef;
      var dataConfigName = Settings.DataConfigName;

      // *** Begin *** - Data Views
      AssemblyGroupViewCombo.LJCInit(DocAssemblyGroup.TableName, dbServiceRef
        , dataConfigName);
      AssemblyViewCombo.LJCInit(DocAssembly.TableName, dbServiceRef
        , dataConfigName);

      ClassGroupViewCombo.LJCInit(DocClassGroup.TableName, dbServiceRef
        , dataConfigName);
      ClassViewCombo.LJCInit(DocClass.TableName, dbServiceRef
        , dataConfigName);

      MethodGroupViewCombo.LJCInit(DocMethodGroup.TableName, dbServiceRef
        , dataConfigName);
      MethodViewCombo.LJCInit(DocMethod.TableName, dbServiceRef
        , dataConfigName);
      // *** End   *** - Data Views
    }

    // Loads the initial Control data.
    private void LoadControlData()
    {
      // *** Begin *** Add - Data Views
      LoadComboAssemblyGroup();
      LoadComboAssembly();
      LoadComboClassGroup();
      LoadComboClass();
      LoadComboMethodGroup();
      LoadComboMethod();
      // *** End   *** Add - Data Views
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

          AssemblyGroupGrid.LJCRestoreColumnValues(ControlValues);
          AssemblyItemGrid.LJCRestoreColumnValues(ControlValues);
          ClassGroupGrid.LJCRestoreColumnValues(ControlValues);
          ClassItemGrid.LJCRestoreColumnValues(ControlValues);
          MethodGroupGrid.LJCRestoreColumnValues(ControlValues);
          MethodItemGrid.LJCRestoreColumnValues(ControlValues);

          // *** Begin *** Add - Data Views
          SetViewComboItem(AssemblyGroupViewCombo, "ViewAssemblyGroup");
          SetViewComboItem(AssemblyViewCombo, "ViewAssembly");

          SetViewComboItem(ClassGroupViewCombo, "ViewClassGroup");
          SetViewComboItem(ClassViewCombo, "ViewClass");

          SetViewComboItem(MethodGroupViewCombo, "ViewMethodGroup");
          SetViewComboItem(MethodViewCombo, "ViewMethod");
          // *** End   *** Add - Data Views
        }
      }
    }

    // Saves the control values. 
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      AssemblyGroupGrid.LJCSaveColumnValues(controlValues);
      AssemblyItemGrid.LJCSaveColumnValues(controlValues);

      ClassGroupGrid.LJCSaveColumnValues(controlValues);
      ClassItemGrid.LJCSaveColumnValues(controlValues);

      MethodGroupGrid.LJCSaveColumnValues(controlValues);
      MethodItemGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("AssemblySplit.SplitterDistance"
        , height: AssemblySplit.SplitterDistance);
      controlValues.Add("ClassSplit.SplitterDistance"
        , height: ClassSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      // Save other values.
      // *** Begin *** Add - Data Views
      var viewInfo = AssemblyGroupViewCombo.GetInfo();
      controlValues.Add("ViewAssemblyGroup", left: viewInfo.DataID);
      viewInfo = AssemblyViewCombo.GetInfo();
      controlValues.Add("ViewAssembly", left: viewInfo.DataID);

      viewInfo = ClassGroupViewCombo.GetInfo();
      controlValues.Add("ViewClassGroup", left: viewInfo.DataID);
      viewInfo = ClassViewCombo.GetInfo();
      controlValues.Add("ViewClass", left: viewInfo.DataID);

      viewInfo = MethodGroupViewCombo.GetInfo();
      controlValues.Add("ViewMethodGroup", left: viewInfo.DataID);
      viewInfo = MethodViewCombo.GetInfo();
      controlValues.Add("ViewMethod", left: viewInfo.DataID);
      // *** End   *** Add - Data Views

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Setup the grid code references.
    private void SetupGridCode()
    {
      mAssemblyGroupGridCode = new AssemblyGroupGridCode(this);
      mAssemblyItemComboCode = new AssemblyItemComboCode(this);
      mAssemblyItemGridCode = new AssemblyItemGridCode(this);
      mClassGroupGridCode = new ClassGroupGridCode(this);
      mClassItemComboCode = new ClassItemComboCode(this);
      mClassItemGridCode = new ClassItemGridCode(this);
      mMethodGroupGridCode = new MethodGroupGridCode(this);
      mMethodItemGridCode = new MethodItemGridCode(this);
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      mAssemblyGroupGridCode.SetupGrid();
      mAssemblyItemGridCode.SetupGrid();

      mClassGroupGridCode.SetupGrid();
      mClassItemGridCode.SetupGrid();

      mMethodGroupGridCode.SetupGrid();
      mMethodItemGridCode.SetupGrid();
    }

    // Sets the ViewCombo item.
    // *** New Method *** Data Views
    private void SetViewComboItem(ViewCombo viewCombo, string viewName)
    {
      var dataID = (int)ControlValueDataID(viewName);
      if (dataID > 0)
      {
        viewCombo.LJCSetByItemID(dataID);
      }
    }

    // Splitter is not in the first TabPage so Set values on first display.
    private void ClassSplit_Resize(object sender, EventArgs e)
    {
      if (ControlValues != null)
      {
        if (!mIsClassSplitSet)
        {
          FormCommon.RestoreSplitDistance(ClassSplit, ControlValues);
        }
        mIsClassSplitSet = true;
      }
    }
    private bool mIsClassSplitSet;

    /// <summary>Gets or sets the ControlValues item.</summary>
    internal ControlValues ControlValues { get; set; }
    #endregion

    // *** New Methods *** - Data Views
    #region Load View Combos

    // Load the AssemblyGroup View Combo.
    private void LoadComboAssemblyGroup()
    {
      if (!AssemblyGroupViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = AssemblyGroupViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Assembly Group View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          AssemblyGroupViewCombo.LJCLoad();
        }
      }
    }

    // Load the Assembly View Combo.
    private void LoadComboAssembly()
    {
      if (!AssemblyViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = AssemblyViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Assembly View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          AssemblyViewCombo.LJCLoad();
        }
      }
    }

    // Load the ClassGroup View Combo.
    private void LoadComboClassGroup()
    {
      if (!ClassGroupViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = ClassGroupViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Class Group View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          ClassGroupViewCombo.LJCLoad();
        }
      }
    }

    // Load the Class View Combo.
    private void LoadComboClass()
    {
      if (!ClassViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = ClassViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Class View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          ClassViewCombo.LJCLoad();
        }
      }
    }

    // Load the MethodGroup View Combo.
    private void LoadComboMethodGroup()
    {
      if (!MethodGroupViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = MethodGroupViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Methods Group View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          MethodGroupViewCombo.LJCLoad();
        }
      }
    }

    // Load the Class View Combo.
    private void LoadComboMethod()
    {
      if (!MethodViewCombo.LJCLoad())
      {
        // Did not load any Views.
        var viewInfo = MethodViewCombo.GetInfo();
        ViewCommon.DoViewEdit(viewInfo, ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload Method View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          MethodViewCombo.LJCLoad();
        }
      }
    }
    #endregion

    #region Private Methods

    // Selects the AssemblyCombo item with the current AssemblyItem.
    private void AssemblyComboSelect()
    {
      var assemblyItem = mAssemblyItemGridCode.CurrentAssembly();
      if (assemblyItem != null)
      {
        mAssemblyItemComboCode.RowSelect(assemblyItem);
      }
    }

    // Selects the AssemblyCombo item with the current AssemblyItem.
    private void AssemblyItemSelect()
    {
      var assemblyItem = mAssemblyItemComboCode.CurrentAssembly();
      if (assemblyItem != null)
      {
        mAssemblyItemGridCode.RowSelect(assemblyItem);
      }
    }

    // Selects the ClassCombo item with the current ClassItem.
    private void ClassComboSelect()
    {
      var classItem = mClassItemGridCode.DocClass();
      if (classItem != null)
      {
        mClassItemComboCode.RowSelect(classItem);
      }
    }

    // Selects the AssemblyCombo item with the current AssemblyItem.
    private void ClassItemSelect()
    {
      var classItem = mClassItemComboCode.CurrentClass();
      if (classItem != null)
      {
        mClassItemGridCode.RowSelect(classItem);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = AssemblyGroupGrid.CurrentRow != null;
      FormCommon.SetMenuState(AssemblyGroupMenu, enableNew, enableEdit);
      AssemblyGroupHeading.Enabled = true;

      enableNew = AssemblyGroupGrid.CurrentRow != null;
      enableEdit = AssemblyItemGrid.CurrentRow != null;
      FormCommon.SetMenuState(AssemblyMenu, enableNew, enableEdit);
      AssemblyHeading.Enabled = true;

      enableNew = AssemblyItemGrid.CurrentRow != null;
      enableEdit = ClassGroupGrid.CurrentRow != null;
      FormCommon.SetMenuState(ClassGroupMenu, enableNew, enableEdit);
      ClassGroupHeading.Enabled = true;

      //enableNew = ClassGroupGrid.CurrentRow != null;
      enableNew = true;
      enableEdit = ClassItemGrid.CurrentRow != null;
      FormCommon.SetMenuState(ClassMenu, enableNew, enableEdit);
      ClassHeading.Enabled = true;

      enableNew = ClassItemGrid.CurrentRow != null;
      enableEdit = MethodGroupGrid.CurrentRow != null;
      FormCommon.SetMenuState(MethodGroupMenu, enableNew, enableEdit);
      MethodGroupHeading.Enabled = true;

      enableNew = MethodGroupGrid.CurrentRow != null;
      enableEdit = MethodItemGrid.CurrentRow != null;
      FormCommon.SetMenuState(MethodItemMenu, enableNew, enableEdit);
      MethodItemHeading.Enabled = true;
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(MouseEventArgs e)
    {
      var tabPage = MainTabs.LJCGetTabPage(e);
      switch (tabPage.Name)
      {
        case "AssemblyTab":
          AssemblyGroupGrid.Select();
          break;

        case "ClassTab":
          ClassGroupGrid.Select();
          break;

        case "MethodTab":
          MethodGroupGrid.Select();
          break;
      }
    }
    #endregion

    #region Properties

    /// <summary>The ConfigFile name.</summary>
    internal string ConfigFileName { get; set; }

    // The Managers object.
    internal ManagersGenDoc Managers { get; set; }

    // The Configuration Settings.
    internal StandardUISettings Settings { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private AssemblyGroupGridCode mAssemblyGroupGridCode;
    private AssemblyItemComboCode mAssemblyItemComboCode;
    private AssemblyItemGridCode mAssemblyItemGridCode;
    private ClassGroupGridCode mClassGroupGridCode;
    private ClassItemComboCode mClassItemComboCode;
    private ClassItemGridCode mClassItemGridCode;
    private MethodGroupGridCode mMethodGroupGridCode;
    private MethodItemGridCode mMethodItemGridCode;
    #endregion
  }
}
