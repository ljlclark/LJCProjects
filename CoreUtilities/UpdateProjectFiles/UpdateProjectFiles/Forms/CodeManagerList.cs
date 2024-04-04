// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeManagerList.cs
using LJCNetCommon;
using LJCWinFormCommon;
using ProjectFilesDAL;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal partial class CodeManagerList : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal CodeManagerList()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Set default class data.
      var configValues = ValuesProjectFiles.Instance;
      mErrors = configValues.Errors;

      Text += $" - {configValues.FileSpec}";
      Cursor = Cursors.Default;
    }
    private string mErrors;
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void CodeManagerList_Load(object sender, System.EventArgs e)
    {
      NetString.ThrowArgError(mErrors);
      InitializeControls();
      SolutionExit.Click += CodeLineExit_Click;
      ProjectFileExit.Click += CodeLineExit_Click;
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    #region Tabs

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void MainTabsMove_Click(object sender, System.EventArgs e)
    {
      MainTabs.LJCMoveTabPageRight(TileTabs, TabsSplit);
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object sender, System.EventArgs e)
    {
      TileTabs.LJCMoveTabPageLeft(MainTabs, TabsSplit);
    }
    #endregion

    #region CodeLine

    // Displays a detail dialog for a new record.
    private void CodeLineNew_Click(object sender, System.EventArgs e)
    {
      mCodeLineGridCode.DoNew();
    }

    // Displays a detail dialog to edit a record.
    private void CodeLineEdit_Click(object sender, System.EventArgs e)
    {
      mCodeLineGridCode.DoDelete();
    }

    // Deletes the selected row.
    private void CodeLineDelete_Click(object sender, System.EventArgs e)
    {
      mCodeLineGridCode.DoDelete();
    }

    // Refreshes the list.
    private void CodeLineRefresh_Click(object sender, System.EventArgs e)
    {
      mCodeLineGridCode.DoRefresh();
    }

    // Performs the Exit function
    private void CodeLineExit_Click(object sender, System.EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region CodeGroup

    // Displays a detail dialog for a new record.
    private void CodeGroupNew_Click(object sender, System.EventArgs e)
    {
      mCodeGroupGridCode.DoNew();
    }

    // Displays a detail dialog to edit a record.
    private void CodeGroupEdit_Click(object sender, System.EventArgs e)
    {
      mCodeGroupGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void CodeGroupDelete_Click(object sender, System.EventArgs e)
    {
      mCodeGroupGridCode.DoDelete();
    }

    // Refreshes the list.
    private void CodeGroupRefresh_Click(object sender, System.EventArgs e)
    {
      mCodeGroupGridCode.DoRefresh();
    }
    #endregion

    #region Solution

    // Displays a detail dialog for a new record.
    private void SolutionNew_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoNew();
    }

    // Displays a detail dialog to edit a record.
    private void SolutionEdit_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void SolutionDelete_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoDelete();
    }

    // Refreshes the list.
    private void SolutionRefresh_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoRefresh();
    }

    // Clears the solution dependencies
    private void SolutionClear_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoDependencies(DependencyAction.Delete);
    }

    // Updates the solution dependencies.
    private void SolutionUpdate_Click(object sender, System.EventArgs e)
    {
      mSolutionGridCode.DoDependencies(DependencyAction.Copy);
    }
    #endregion

    #region Project

    // Displays a detail dialog for a new record.
    private void ProjectNew_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoNew();
    }

    // Displays a detail dialog to edit a record.
    private void ProjectEdit_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void ProjectDelete_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoDelete();
    }

    // Refreshes the list.
    private void ProjectRefresh_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoRefresh();
    }

    // Clears the project dependencies
    private void ProjectClear_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoDependencies(DependencyAction.Delete);
    }

    // Updates the project dependencies.
    private void ProjectUpdate_Click(object sender, System.EventArgs e)
    {
      mProjectGridCode.DoDependencies(DependencyAction.Copy);
    }
    #endregion

    #region ProjectFile

    // Displays a detail dialog for a new record.
    private void ProjectFileNew_Click(object sender, System.EventArgs e)
    {
      mProjectFileGridCode.DoNew();
    }

    // Displays a detail dialog to edit a record.
    private void ProjectFileEdit_Click(object sender, System.EventArgs e)
    {
      mProjectFileGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void ProjectFileDelete_Click(object sender, System.EventArgs e)
    {
      mProjectFileGridCode.DoDelete();
    }

    // Refreshes the list.
    private void ProjectFileRefresh_Click(object sender, System.EventArgs e)
    {
      mProjectFileGridCode.DoRefresh();
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

    #region CodeLine

    // Handles the grid keys.
    private void CodeLineGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mCodeLineGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mCodeLineGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(CodeLineGrid
              , MousePosition);
            CodeLineMenu.Show(position);
            CodeLineMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            CodeGroupGrid.Select();
          }
          else
          {
            CodeGroupGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void CodeLineGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (CodeLineGrid.LJCGetMouseRow(e) != null)
      {
        mCodeLineGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void CodeLineGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
        CodeLineGrid.Select();
        if (CodeLineGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
          CodeLineGrid.LJCSetCurrentRow(e);
          TimedChange(Change.CodeLine);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void CodeLineGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (CodeLineGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.CodeLine);
      }
      CodeLineGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region CodeGroup

    // Handles the grid keys.
    private void CodeGroupGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mCodeGroupGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mCodeGroupGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(CodeGroupGrid
              , MousePosition);
            CodeGroupMenu.Show(position);
            CodeGroupMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            CodeLineGrid.Select();
          }
          else
          {
            CodeLineGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void CodeGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (CodeGroupGrid.LJCGetMouseRow(e) != null)
      {
        mCodeGroupGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void CodeGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
      CodeGroupGrid.Select();
      if (CodeGroupGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
        CodeGroupGrid.LJCSetCurrentRow(e);
        TimedChange(Change.CodeGroup);
      }
    }

    // Handles the SelectionChanged event.
    private void CodeGroupGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (CodeGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.CodeGroup);
      }
      CodeGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Solution

    // Handles the grid keys.
    private void SolutionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mSolutionGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mSolutionGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(SolutionGrid
              , MousePosition);
            SolutionMenu.Show(position);
            SolutionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ProjectGrid.Select();
          }
          else
          {
            ProjectGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void SolutionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (SolutionGrid.LJCGetMouseRow(e) != null)
      {
        mSolutionGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void SolutionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
      SolutionGrid.Select();
      if (SolutionGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
        SolutionGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Solution);
      }
    }

    // Handles the SelectionChanged event.
    private void SolutionGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (SolutionGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Solution);
      }
      SolutionGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Project

    // Handles the grid keys.
    private void ProjectGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mProjectGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mProjectGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ProjectGrid
              , MousePosition);
            ProjectMenu.Show(position);
            ProjectMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            SolutionGrid.Select();
          }
          else
          {
            SolutionGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ProjectGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ProjectGrid.LJCGetMouseRow(e) != null)
      {
        mProjectGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ProjectGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
      ProjectGrid.Select();
      if (ProjectGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
        ProjectGrid.LJCSetCurrentRow(e);
        TimedChange(Change.Project);
      }
    }

    // Handles the SelectionChanged event.
    private void ProjectGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (ProjectGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Project);
      }
      ProjectGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region ProjectFile

    // Handles the grid keys.
    private void FileGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          mProjectFileGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F5:
          mProjectFileGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ProjectFileGrid
              , MousePosition);
            ProjectFileMenu.Show(position);
            ProjectFileMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            //FileGrid.Select();
          }
          else
          {
            //FileGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void FileGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ProjectFileGrid.LJCGetMouseRow(e) != null)
      {
        mProjectFileGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void FileGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // LJCIsDifferentRow() sets LJCLastRowIndex for new row.
      ProjectFileGrid.Select();
      if (ProjectFileGrid.LJCIsDifferentRow(e))
      {
        // LJCSetCurrentRow() sets LJCAllowSelectionChange to true.
        ProjectFileGrid.LJCSetCurrentRow(e);
        TimedChange(Change.ProjectFile);
      }
    }

    // Handles the SelectionChanged event.
    private void FileGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (ProjectFileGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ProjectFile);
      }
      ProjectFileGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #endregion
  }
}
