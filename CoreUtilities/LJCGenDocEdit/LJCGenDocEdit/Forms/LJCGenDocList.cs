// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocList.cs
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The list form.
  // <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  internal partial class LJCGenDocList : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal LJCGenDocList()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenDocEdit.chm";

      // Set default class data.
      // Set DAL config before using anywhere in the program.
      var configValues = ValuesDocGen.Instance;
      configValues.SetConfigFile("LJCGenDocEdit.exe.config");
      var settings = configValues.StandardSettings;
      Text += $" - {settings.DataConfigName}";
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void LJCGenDocEdit_Load(object sender, EventArgs e)
    {
      InitializeControls();
      ClassGroupExit.Click += MenuExit_Click;
      MethodGroupExit.Click += MenuExit_Click;
      MoveTabRight("Class");
      CenterToParent();
    }

    private void MoveTabRight(string tabName)
    {
      var tabPage = MainTabs.LJCGetTabPage(tabName);
      MainTabs.SelectTab(tabPage);
      MainTabs.LJCMoveTabPageRight(TileTabs, TabsSplit);
      TabsSplit.SplitterDistance = ClientRectangle.Width / 2;
    }
    #endregion

    #region Action Event Handlers

    #region Tabs

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void MainTabsMove_Click(object sender, EventArgs e)
    {
      MainTabs.LJCMoveTabPageRight(TileTabs, TabsSplit);
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object sender, EventArgs e)
    {
      TileTabs.LJCMoveTabPageLeft(MainTabs, TabsSplit);
    }
    #endregion

    #region Assembly Group

    // Displays a detail dialog for a new record.
    private void AssemblyGroupNew_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoNew();
    }

    // Calls the Edit method.
    private void AssemblyGroupEdit_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void AssemblyGroupDelete_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoDelete();
    }

    // Refreshes the list.
    private void AssemblyGroupRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void AssemblyGroupReset_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoResetSequence();
    }

    // Export a text file.
    private void AssemblyGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\AssemblyGroup.{extension}";
      AssemblyGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyGroupCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\AssemblyGroup.csv";
      AssemblyGroupGrid.LJCExportData(fileSpec);
    }

    // Performs the Exit function.
    private void MenuExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the help page.
    private void AssemblyGroupHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Assembly\AssemblyGroupList.html");
    }
    #endregion

    #region Assembly Item

    // Displays a detail dialog for a new record.
    private void AssemblyNew_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoNew();
    }

    // Calls the Edit method.
    private void AssemblyEdit_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void AssemblyDelete_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoDelete();
    }

    // Refreshes the list.
    private void AssemblyRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void AssemblyReset_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoResetSequence();
    }

    // Export a text file.
    private void AssemblyText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Assembly.{extension}";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Assembly.csv";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }

    // Shows the help page.
    private void AssemblyHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Assembly\AssemblyItemList.html");
    }
    #endregion

    #region Class Group

    // Displays a detail dialog for a new record.
    private void ClassGroupNew_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ClassGroupEdit_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void ClassGroupDelete_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoDelete();
    }

    // Refreshes the list.
    private void ClassGroupRefresh_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void ClassGroupReset_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoResetSequence();
    }

    // Export a text file.
    private void ClassGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\ClassGroup.{extension}";
      ClassGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassGroupCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\ClassGroup.csv";
      ClassGroupGrid.LJCExportData(fileSpec);
    }

    // Shows the help page.
    private void ClassGroupHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Class\ClassGroupList.html");
    }
    #endregion

    #region Class Item

    // Displays a detail dialog for a new record.
    private void ClassNew_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ClassEdit_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void ClassDelete_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoDelete();
    }

    // Refreshes the list.
    private void ClassRefresh_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void ClassReset_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoResetSequence();
    }

    // Export a text file.
    private void ClassText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Class.{extension}";
      ClassItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Class.csv";
      ClassItemGrid.LJCExportData(fileSpec);
    }

    // Shows the help page.
    private void ClassHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Class\ClassItemList.html");
    }
    #endregion

    #region Method Group

    // Displays a detail dialog for a new record.
    private void MethodGroupNew_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoNew();
    }

    // Calls the Edit method.
    private void MethodGroupEdit_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void MethodGroupDelete_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoDelete();
    }

    // Refreshes the list.
    private void MethodGroupRefresh_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void MethodGroupReset_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoResetSequence();
    }

    // Export a text file.
    private void MethodGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\MethodGroup.{extension}";
      MethodGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MethodGroupCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\MethodGroup.csv";
      MethodGroupGrid.LJCExportData(fileSpec);
    }

    // Shows the help page.
    private void MethodGroupHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Method\MethodGroupList.html");
    }
    #endregion

    #region Method Item

    // Displays a detail dialog for a new record.
    private void MethodNew_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoNew();
    }

    // Calls the Edit method.
    private void MethodEdit_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void MethodDelete_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoDelete();
    }

    // Refreshes the list.
    private void MethodItemRefresh_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void MethodItemReset_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoResetSequence();
    }

    // Export a text file.
    private void MethodItemText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Method.{extension}";
      MethodItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MethodItemCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Method.csv";
      MethodItemGrid.LJCExportData(fileSpec);
    }

    // Shows the help page.
    private void MethodItemHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Method\MethodItemList.html");
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Tabs

    // Handles the MouseDown event.
    private void MainTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MainTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      SetFocusTab(e);
    }
    #endregion

    #region Assembly Group

    // Handles the DragDrop event.
    private void AssemblyGroupGrid_DragDrop(object sender, DragEventArgs e)
    {
      mAssemblyGroupGridCode.DoDragDrop(e);
    }

    // Handles the form keys.
    private void AssemblyGroupGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mAssemblyGroupGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mAssemblyGroupGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(AssemblyGroupGrid
              , MousePosition);
            AssemblyGroupMenu.Show(position);
            AssemblyGroupMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            AssemblyItemGrid.Select();
          }
          else
          {
            AssemblyItemGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void AssemblyGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (AssemblyGroupGrid.LJCGetMouseRow(e) != null)
      {
        mAssemblyGroupGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void AssemblyGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        AssemblyGroupGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (AssemblyGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          AssemblyGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.AssemblyGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void AssemblyGroupGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (AssemblyGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AssemblyGroup);
      }
      AssemblyGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Assembly Item

    // Handles the DragDrop event.
    private void AssemblyItemGrid_DragDrop(object sender, DragEventArgs e)
    {
      mAssemblyItemGridCode.DoDragDrop(e);
    }

    // Handles the form keys.
    private void AssemblyItemGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mAssemblyGroupGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mAssemblyGroupGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(AssemblyItemGrid
              , MousePosition);
            AssemblyMenu.Show(position);
            AssemblyMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            AssemblyGroupGrid.Select();
          }
          else
          {
            AssemblyGroupGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void AssemblyItemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (AssemblyItemGrid.LJCGetMouseRow(e) != null)
      {
        mAssemblyItemGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void AssemblyItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        AssemblyItemGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (AssemblyItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          AssemblyItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.AssemblyItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void AssemblyItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (AssemblyItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AssemblyItem);
      }
      AssemblyItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Assembly Combo

    // Handles the SelectionChanged event.
    private void AssemblyCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.AssemblyCombo);
    }
    #endregion

    #region Class Group

    // Handles the DragDrop event.
    private void ClassGroupGrid_DragDrop(object sender, DragEventArgs e)
    {
      var assemblyItem = mAssemblyItemGridCode.CurrentAssembly();
      var assemblyID = assemblyItem.ID;
      mClassGroupGridCode.DoDragDrop(assemblyID, e);
    }

    // Handles the form keys.
    private void ClassGroupGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mClassGroupGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mClassGroupGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ClassGroupGrid
              , MousePosition);
            ClassGroupMenu.Show(position);
            ClassGroupMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ClassItemGrid.Select();
          }
          else
          {
            ClassItemGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ClassGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ClassGroupGrid.LJCGetMouseRow(e) != null)
      {
        mClassGroupGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ClassGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ClassGroupGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (ClassGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          ClassGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ClassGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ClassGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ClassGroup);
      }
      ClassGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Class Item

    // Handles the DragDrop event.
    private void ClassItemGrid_DragDrop(object sender, DragEventArgs e)
    {
      var assemblyItem = mAssemblyItemGridCode.CurrentAssembly();
      var assemblyID = assemblyItem.ID;
      mClassItemGridCode.DoDragDrop(assemblyID, e);
    }

    // Handles the form keys.
    private void ClassItemGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mClassItemGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mClassItemGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ClassItemGrid
              , MousePosition);
            ClassMenu.Show(position);
            ClassMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ClassGroupGrid.Select();
          }
          else
          {
            ClassGroupGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ClassItemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ClassItemGrid.LJCGetMouseRow(e) != null)
      {
        mClassItemGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ClassItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ClassItemGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (ClassItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          ClassItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ClassItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ClassItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ClassItem);
      }
      ClassItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Class Combo

    // Handles the SelectionChanged event.
    private void ClassCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.ClassCombo);
    }
    #endregion

    #region Method Group

    // Handles the DragDrop event.
    private void MethodGroupGrid_DragDrop(object sender, DragEventArgs e)
    {
      var classItem = mClassItemGridCode.DocClass();
      var classID = classItem.ID;
      mMethodGroupGridCode.DoDragDrop(classID, e);
    }

    // Handles the form keys.
    private void MethodGroupGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mMethodGroupGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mMethodGroupGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(MethodGroupGrid
              , MousePosition);
            MethodGroupMenu.Show(position);
            MethodGroupMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            MethodItemGrid.Select();
          }
          else
          {
            MethodItemGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void MethodGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (MethodGroupGrid.LJCGetMouseRow(e) != null)
      {
        mMethodGroupGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void MethodGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MethodGroupGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (MethodGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          MethodGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.MethodGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodGroupGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (MethodGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.MethodGroup);
      }
      MethodGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Method Item

    // Handles the DragDrop event.
    private void MethodItemGrid_DragDrop(object sender, DragEventArgs e)
    {
      var classItem = mClassItemGridCode.DocClass();
      var classID = classItem.ID;
      mMethodItemGridCode.DoDragDrop(classID, e);
    }

    // Handles the form keys.
    private void MethodItemGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mMethodItemGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mMethodItemGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(MethodItemGrid
              , MousePosition);
            MethodItemMenu.Show(position);
            MethodItemMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            MethodGroupGrid.Select();
          }
          else
          {
            MethodGroupGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void MethodItemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (MethodItemGrid.LJCGetMouseRow(e) != null)
      {
        mMethodItemGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void MethodItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MethodItemGrid.Select();

        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        if (MethodItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          MethodItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.MethodItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (MethodItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.MethodItem);
      }
      MethodItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion
    #endregion

    #region Internal Properties

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;
    #endregion
  }
}
