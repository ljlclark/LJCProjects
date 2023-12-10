// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewEditorList.cs
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCViewEditor
{
  // The View Editor form.
  /// <include path='items/ListFormDAW/*' file='../../LJCGenDoc/Common/List.xml'/>
  public partial class ViewEditorList : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/ViewEditorListC/*' file='Doc/ViewEditorList.xml'/>
    public ViewEditorList(string tableName = null, bool splash = false)
    {
      Cursor = Cursors.WaitCursor;
      mStartupTableName = tableName;
      if (splash)
      {
        ViewEditSplash splashDialog = new ViewEditSplash(true);
        splashDialog.ShowDialog();
      }
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "ViewEditor.chm";

      // Set default class data.
      BeginColor = Color.AliceBlue;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ViewEditorList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }
    #endregion

    #region Data Methods

    // Retrieves the combo items.
    private void DataRetrieveTable()
    {
      DbResult dbResult;

      Cursor = Cursors.WaitCursor;
      TableCombo.Items.Clear();

      // Reset the DataConfig dependent items.
      ResetData();

      SqlTableManager sqlTableManager = new SqlTableManager(DbServiceRef
        , DataConfigName);

      dbResult = sqlTableManager.DataManager.GetTableNames();
      if (DbResult.HasRows(dbResult))
      {
        SqlTables dataRecords = sqlTableManager.CreateCollection(dbResult);
        if (dataRecords != null)
        {
          foreach (SqlTable dataRecord in dataRecords)
          {
            if (dataRecord.Name != null)
            {
              TableCombo.Items.Add(dataRecord);
            }
          }
        }
      }
      Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    private void ResetData()
    {
      if (DataConfigName != mPrevConfigName)
      {
        mPrevConfigName = DataConfigName;
        if (DbServiceRef.DbDataAccess != null)
        {
          DbServiceRef.DbDataAccess = new DbDataAccess(DataConfigName);
        }

        try
        {
          DataDbView = new DataDbView(Managers);
        }
        catch (SystemException e)
        {
          ViewEditorCommon.CreateTables(e, DataConfigName);
          DataDbView = new DataDbView(Managers);
        }

        ViewGridCode.ResetData();
        ColumnGridCode.ResetData();
        JoinGridCode.ResetData();
        JoinOnGridCode.ResetData();
        JoinColumnGridCode.ResetData();
        FilterGridCode.ResetData();
        ConditionSetGridCode.ResetData();
        ConditionGridCode.ResetData();
        OrderByGridCode.ResetData();
      }
    }
    #endregion

    #region Action Methods

    #region View Data

    // Creates and returns the View DbRequest object.
    internal DbRequest DoGetViewRequest()
    {
      DbRequest retValue = null;

      if (ViewGrid.CurrentRow is LJCGridRow gridRow)
      {
        // Get View Request.
        string tableName = TableCombo.Text;
        string viewDataName = gridRow.LJCGetString(ViewData.ColumnName);

        retValue = DataDbView.GetViewRequest(tableName, viewDataName);
      }
      return retValue;
    }
    #endregion
    #endregion

    #region Action Event Handlers

    #region Form Controls

    // Shows the Help page.
    private void DataConfigHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"DataConfig.html");
    }

    // Handles the Menu Title click.
    private void TableTitle_Click(object sender, EventArgs e)
    {
      //TableMenu.Focus();
    }

    // Shows the Help page.
    private void TableHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Table.html");
    }
    #endregion

    #region View Data

    // Calls the New method.
    private void ViewMenuNew_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ViewMenuEdit_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void ViewMenuDelete_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoDelete();
    }

    // Calls the Refresh method.
    private void ViewMenuRefresh_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoRefresh();
    }

    // Allows for display and edit of a file.
    private void ViewFileEdit_Click(object sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // Shows the Data.
    private void ViewMenuShowData_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoShowData();
    }

    // Shows the SQL statement.
    private void ViewMenuShowSQL_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoShowSQL();
    }

    // Show the DbRequest code.
    private void ViewMenuShowCode_Click(object sender, EventArgs e)
    {
      ViewGridCode.ShowCode();
    }

    // Performs the Close function.
    private void ViewMenuExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the Help page.
    private void ViewMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"View\ViewList.html");
    }

    // Shows the About box.
    private void ViewMenuAbout_Click(object sender, EventArgs e)
    {
      ViewEditSplash splash = new ViewEditSplash();
      splash.ShowDialog();
    }
    #endregion

    #region Column

    // Calls the AddAll method.
    private void ColumnMenuAdd_Click(object sender, EventArgs e)
    {
      ColumnGridCode.DoAddAll(TableCombo.Text.Trim());
    }

    // Calls the New method.
    private void ColumnMenuNew_Click(object sender, EventArgs e)
    {
      ColumnGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ColumnMenuEdit_Click(object sender, EventArgs e)
    {
      ColumnGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void ColumnMenuDelete_Click(object sender, EventArgs e)
    {
      ColumnGridCode.DoDelete();
    }

    // Calls the Refresh method.
    private void ColumnMenuRefresh_Click(object sender, EventArgs e)
    {
      ColumnGridCode.DoRefresh();
    }

    // Shows the Help page.
    private void ColumnMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Column\ColumnList.html");
    }
    #endregion

    #region Join

    // Calls the New method.
    private void JoinMenuNew_Click(object sender, EventArgs e)
    {
      JoinGridCode.DoNew();
    }

    // Calls the Edit method.
    private void JoinMenuEdit_Click(object sender, EventArgs e)
    {
      JoinGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void JoinMenuDelete_Click(object sender, EventArgs e)
    {
      JoinGridCode.DoDelete();
    }

    // Shows the Help page.
    private void JoinMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinList.html");
    }
    #endregion

    #region JoinOn

    // Calls the New method.
    private void JoinOnMenuNew_Click(object sender, EventArgs e)
    {
      JoinOnGridCode.DoNew();
    }

    // Calls the Edit method.
    private void JoinOnMenuEdit_Click(object sender, EventArgs e)
    {
      JoinOnGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void JoinOnMenuDelete_Click(object sender, EventArgs e)
    {
      JoinOnGridCode.DoDelete();
    }

    // Shows the Help page.
    private void JoinOnMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinOnList.html");
    }
    #endregion

    #region JoinColumn

    // Calls the New method.
    private void JoinColumnNew_Click(object sender, EventArgs e)
    {
      JoinColumnGridCode.DoNew();
    }

    // Calls the Edit method.
    private void JoinColumnEdit_Click(object sender, EventArgs e)
    {
      JoinColumnGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void JoinColumnDelete_Click(object sender, EventArgs e)
    {
      JoinColumnGridCode.DoDelete();
    }

    // Shows the Help page.
    private void JoinColumnHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Join\JoinColumnList.html");
    }
    #endregion

    #region Filter

    // Calls the New method.
    private void FilterMenuNew_Click(object sender, EventArgs e)
    {
      FilterGridCode.DoNew();
    }

    // Calls the Edit method.
    private void FilterMenuEdit_Click(object sender, EventArgs e)
    {
      FilterGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void FilterMenuDelete_Click(object sender, EventArgs e)
    {
      FilterGridCode.DoDelete();
    }

    // Shows the Help page.
    private void FilterMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\FilterList.html");
    }
    #endregion

    #region ConditionSet

    // Calls the New method.
    private void ConditionSetNew_Click(object sender, EventArgs e)
    {
      ConditionSetGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ConditionSetEdit_Click(object sender, EventArgs e)
    {
      ConditionSetGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void ConditionSetDelete_Click(object sender, EventArgs e)
    {
      ConditionSetGridCode.DoDelete();
    }

    // Shows the Help page.
    private void ConditionSetHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionSetList.html");
    }
    #endregion

    #region Condition

    // Calls the New method.
    private void ConditionMenuNew_Click(object sender, EventArgs e)
    {
      ConditionGridCode.DoNew();
    }

    // Calls the Edit method.
    private void ConditionMenuEdit_Click(object sender, EventArgs e)
    {
      ConditionGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void ConditionMenuDelete_Click(object sender, EventArgs e)
    {
      ConditionGridCode.DoDelete();
    }

    // Shows the Help page.
    private void ConditionMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Filter\ConditionList.html");
    }
    #endregion

    #region OrderBy

    // Calls the New method.
    private void OrderByMenuNew_Click(object sender, EventArgs e)
    {
      OrderByGridCode.DoNew();
    }

    // Calls the Edit method.
    private void OrderByMenuEdit_Click(object sender, EventArgs e)
    {
      OrderByGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void OrderByMenuDelete_Click(object sender, EventArgs e)
    {
      OrderByGridCode.DoDelete();
    }

    // Shows the Help page.
    private void OrderByMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"OrderBy\OrderByList.html");
    }
    #endregion

    #region Data

    // Calls the New method.
    private void DataMenuNew_Click(object sender, EventArgs e)
    {
      DataGridCode.DoNew();
    }

    // Calls the Edit method.
    private void DataMenuEdit_Click(object sender, EventArgs e)
    {
      DataGridCode.TableName = TableCombo.Text.Trim();
      DataGridCode.DoEdit();
    }

    // Calls the Delete method.
    private void DataMenuDelete_Click(object sender, EventArgs e)
    {
      DataGridCode.DoDelete();
    }

    // Calls the Refresh method.
    private void DataMenuRefresh_Click(object sender, EventArgs e)
    {
      ViewGridCode.DoShowData();
    }

    // Performs the Close function.
    private void DataMenuExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Tabs

    // Handles the MouseDown event.
    private void ViewTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ViewTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = ViewTabs.LJCGetTabPage(e);
      ViewSetFocusTab(tabPage);
    }
    #endregion

    #region Table

    // Handles the SelectionChanged event.
    private void ConfigCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.Config);
    }

    // Handles the SelectionChanged event.
    private void TableCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.Table);
    }
    #endregion

    #region View Data

    // Handles the control keys.
    private void ViewGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ViewGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"View\ViewList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ViewGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ViewGrid
              , MousePosition);
            ViewMenu.Show(position);
            ViewMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ViewGrid.Select();
          }
          else
          {
            ViewTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ViewGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ViewGrid.LJCGetMouseRow(e) != null)
      {
        ViewGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ViewGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ViewGrid.Select();
        if (ViewGrid.LJCIsDifferentRow(e))
        {
          ViewGrid.LJCSetCurrentRow(e);
          TimedChange(Change.View);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ViewGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ViewGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.View);
      }
      ViewGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Column

    // Handles the control keys.
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ColumnGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Column\ColumnList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          ColumnGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ColumnGrid
              , MousePosition);
            ColumnMenu.Show(position);
            ColumnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        ColumnGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ColumnGrid.Select();
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          ColumnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Column);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Join

    // Handles the control keys.
    private void JoinGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\Join.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinGrid
              , MousePosition);
            JoinMenu.Show(position);
            JoinMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinGrid.Select();
          }
          else
          {
            JoinTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void JoinGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinGrid.LJCGetMouseRow(e) != null)
      {
        JoinGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinGrid.Select();
        if (JoinGrid.LJCIsDifferentRow(e))
        {
          JoinGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Join);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void JoinGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Join);
      }
      JoinGrid.LJCAllowSelectionChange = true;
    }

    private void JoinTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = JoinTabs.LJCGetTabPage(e);
      JoinSetFocusTab(tabPage);
    }
    #endregion

    #region JoinOn

    // Handles the control keys.
    private void JoinOnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinOnGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\JoinOnList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinOnGrid
              , MousePosition);
            JoinOnMenu.Show(position);
            JoinOnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinOnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void JoinOnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinOnGrid.LJCGetMouseRow(e) != null)
      {
        JoinOnGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinOnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinOnGrid.Select();
        if (JoinOnGrid.LJCIsDifferentRow(e))
        {
          JoinOnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.JoinOn);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void JoinOnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinOnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.JoinOn);
      }
      JoinOnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region JoinColumn

    // Handles the control keys.
    private void JoinColumnGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          JoinColumnGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Join\JoinColumnList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(JoinColumnGrid
              , MousePosition);
            JoinColumnMenu.Show(position);
            JoinColumnMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            JoinColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void JoinColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (JoinColumnGrid.LJCGetMouseRow(e) != null)
      {
        JoinColumnGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void JoinColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        JoinColumnGrid.Select();
        if (JoinColumnGrid.LJCIsDifferentRow(e))
        {
          JoinColumnGrid.LJCSetCurrentRow(e);
          TimedChange(Change.JoinColumn);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void JoinColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (JoinColumnGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.JoinColumn);
      }
      JoinColumnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Filter

    // Handles the control keys.
    private void FilterGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          FilterGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\FilterList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(FilterGrid
              , MousePosition);
            FilterMenu.Show(position);
            FilterMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            FilterGrid.Select();
          }
          else
          {
            ConditionSetTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void FilterGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (FilterGrid.LJCGetMouseRow(e) != null)
      {
        FilterGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void FilterGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        FilterGrid.Select();
        if (FilterGrid.LJCIsDifferentRow(e))
        {
          FilterGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Filter);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void FilterGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (FilterGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Filter);
      }
      FilterGrid.LJCAllowSelectionChange = true;
    }

    private void ConditionSetTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionSetTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = ConditionSetTabs.LJCGetTabPage(e);
      FilterSetFocusTab(tabPage);
    }
    #endregion

    #region ConditionSet

    // Handles the control keys.
    private void ConditionSetGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionSetGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\ConditionSetList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ConditionSetGrid
              , MousePosition);
            ConditionSetMenu.Show(position);
            ConditionSetMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ConditionSetGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ConditionSetGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionSetGrid.LJCGetMouseRow(e) != null)
      {
        ConditionSetGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ConditionSetGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionSetGrid.Select();
        if (ConditionSetGrid.LJCIsDifferentRow(e))
        {
          ConditionSetGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ConditionSet);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ConditionSetGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ConditionSetGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ConditionSet);
      }
      ConditionSetGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Condition

    // Handles the control keys.
    private void ConditionGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ConditionGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Filter\ConditionList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ConditionGrid
              , MousePosition);
            ConditionMenu.Show(position);
            ConditionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ConditionGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ConditionGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ConditionGrid.LJCGetMouseRow(e) != null)
      {
        ConditionGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ConditionGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ConditionGrid.Select();
        if (ConditionGrid.LJCIsDifferentRow(e))
        {
          ConditionGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Condition);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ConditionGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ConditionGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Condition);
      }
      ConditionGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region OrderBy

    // Handles the control keys.
    private void OrderByGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          OrderByGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"OrderBy\OrderByList.html");
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(OrderByGrid
              , MousePosition);
            OrderByMenu.Show(position);
            OrderByMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void OrderByGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (OrderByGrid.LJCGetMouseRow(e) != null)
      {
        OrderByGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void OrderByGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        OrderByGrid.Select();
        if (OrderByGrid.LJCIsDifferentRow(e))
        {
          OrderByGrid.LJCSetCurrentRow(e);
          TimedChange(Change.OrderBy);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void OrderByGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (OrderByGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.OrderBy);
      }
      OrderByGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Data

    // Handles the control keys.
    private void DataGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DataGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ColumnGrid.Select();
          }
          else
          {
            TableCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void DataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DataGrid.LJCGetMouseRow(e) != null)
      {
        DataGridCode.TableName = TableCombo.Text.Trim();
        DataGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void DataGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        DataGrid.Select();
        if (DataGrid.LJCIsDifferentRow(e))
        {
          DataGrid.LJCSetCurrentRow(e);
          TimedChange(Change.Data);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void DataGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (DataGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.Data);
      }
      DataGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    // Handles the InfoWindow Close event.
    internal void InfoWindow_CloseEvent(object sender, EventArgs e)
    {
      mInfoWindow = null;
    }
    #endregion

    #region Properties

    /// <summary>The ViewData ID value.</summary>
    public int StartupViewDataID { get; set; }

    // The DataConfig name.
    internal string DataConfigName { get; set; }

    // The DbService.
    internal DbServiceRef DbServiceRef { get; set; }

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    // Gets or sets the ViewHelper value.
    internal DataDbView DataDbView { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    internal InfoWindow mInfoWindow;
    private string mPrevConfigName;
    private StandardUISettings mSettings;
    private readonly string mStartupTableName;
    #endregion
  }
}
