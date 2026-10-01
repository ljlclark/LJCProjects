// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ReplacementGridCode.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using static LJCGenTextEdit5.EditList;

namespace LJCGenTextEdit5
{
  // Contains the ReplacementGrid methods.
  internal class ReplacementGridCode
  {
    #region Parent Object Properties

    // Gets or sets the Section Manager reference.
    internal SectionManager SectionManager
    {
      get => mSectionManager;
      set
      {
        mSectionManager = value;
        if (mSectionManager != null)
        {
          Sections sections = mSectionManager.Load();
          ReplacementManager = new ReplacementManager(sections);
        }
      }
    }
    private SectionManager mSectionManager = null!;

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    // Gets or sets the Item Grid reference.
    private LJCDataGrid ItemGrid { get; set; }

    // Gets or sets the Replacement Grid reference.
    private LJCDataGrid ReplacementGrid { get; set; }

    // Gets or sets the Manager reference.
    private ReplacementManager ReplacementManager { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }

    private TemplateTextCode TemplateTextCode { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal ReplacementGridCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      ReplacementManager = EditList.ReplacementManager;
      ItemGrid = EditList.ItemGrid;
      ReplacementGrid = EditList.ReplacementGrid;
      SectionGrid = EditList.SectionGrid;
      SectionManager = EditList.SectionManager;
      TemplateTextCode = EditList.TemplateTextCode;

      MenuEventHandlers();
      ControlEventHandlers();
      EditList.Cursor = Cursors.Default;
    }

    // Creates the menu event handlers.
    private void MenuEventHandlers()
    {
      var list = EditList;
      list.ReplacementMenuNew.Click += ReplacementMenuNew_Click;
      list.ReplacementMenuEdit.Click += ReplacementMenuEdit_Click;
      list.ReplacementMenuDelete.Click += ReplacementMenuDelete_Click;
      list.ReplacementMenuRefresh.Click += ReplacementMenuRefresh_Click;
      list.ReplacementGenerate.Click += ReplacementGenerate_Click;
      list.ReplacementSave.Click += ReplacementSave_Click;
      list.ReplacementMenuExit.Click += ReplacementMenuExit_Click;
      list.ReplacementMenuHelp.Click += ReplacementMenuHelp_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      var grid = EditList.ReplacementGrid;
      grid.KeyDown += Grid_KeyDown;
      grid.MouseDoubleClick += Grid_MouseDoubleClick;
      grid.MouseDown += ReplacementGrid_MouseDown;
      grid.SelectionChanged += ReplacementGrid_SelectionChanged;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ReplacementGrid.LJCRowsClear();

      if (SectionGrid.CurrentRow is LJCGridRow parentRow
        && ItemGrid.CurrentRow is LJCGridRow row
        && ReplacementManager != null)
      {
        // Data from items.
        string? sectionName = parentRow.LJCGetCellText("Name");
        string? repeatItemName = row.LJCGetCellText("Name");

        if (LJC.HasText(sectionName)
          && LJC.HasText(repeatItemName))
        {
          var records = ReplacementManager.Load(sectionName, repeatItemName);
          if (LJC.HasListItems(records))
          {
            foreach (Replacement record in records)
            {
              RowAdd(record);
            }
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Replacement);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Replacement dataRecord)
    {
      var retValue = ReplacementGrid.LJCRowAdd();
      retValue.LJCSetValues(dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Replacement dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        foreach (LJCGridRow row in ReplacementGrid.Rows)
        {
          var name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ReplacementGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Replacement dataRecord)
    {
      if (ReplacementGrid.CurrentRow is LJCGridRow gridRow)
      {
        gridRow.LJCSetValues(dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void New()
    {
      //ReplacementDetail detail;

      if (SectionGrid.CurrentRow is LJCGridRow sectionRow
        && ItemGrid.CurrentRow is LJCGridRow parentRow)
      {
        //// Data from items.
        //string sectionName = sectionRow.LJCGetCellText("Name");
        //string parentName = parentRow.LJCGetCellText("Name");

        //var location = FormPoint.DialogScreenPoint(ReplacementGrid);
        //detail = new ReplacementDetail()
        //{
        //  LJCGenDataManager = GenDataManager,
        //  LJCLocation = location,
        //  LJCParentName = parentName,
        //  LJCSectionName = sectionName
        //};
        //detail.LJCChange += ReplacementDetail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void Edit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow sectionRow
        && ItemGrid.CurrentRow is LJCGridRow parentRow
        && ReplacementGrid.CurrentRow is LJCGridRow row)
      {
        //// Data from items.
        //string sectionName = sectionRow.LJCGetCellText("Name");
        //string parentName = parentRow.LJCGetCellText("Name");
        //string name = row.LJCGetCellText("Name");

        //var location = FormPoint.DialogScreenPoint(ReplacementGrid);
        //var detail = new ReplacementDetail()
        //{
        //  LJCGenDataManager = EditList.GenDataManager,
        //  LJCLocation = location,
        //  LJCParentName = parentName,
        //  LJCReplacementName = name,
        //  LJCSectionName = sectionName
        //};
        //detail.LJCChange += ReplacementDetail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void Delete()
    {
      var sectionRow = SectionGrid.CurrentRow as LJCGridRow;
      var parentRow = ItemGrid.CurrentRow as LJCGridRow;
      var row = ReplacementGrid.CurrentRow as LJCGridRow;

      while (true)
      {
        if (null == sectionRow
          || null == parentRow
          || null == row)
        {
          break;
        }

        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) != DialogResult.Yes)
        {
          break;
        }

        // Data from items.
        string? sectionName = sectionRow.LJCGetCellText("Name");
        string? parentName = parentRow.LJCGetCellText("Name");
        string? name = row.LJCGetCellText("Name");

        if (LJC.HasText(sectionName)
          && LJC.HasText(parentName)
          && LJC.HasText(name))
        {
          if (!ReplacementManager.Delete(sectionName, parentName, name))
          {
            break;
          }
        }

        if (!EditList.SectionManager.Save())
        {
          break;
        }

        ReplacementGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Replacement);
        break;
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string? name = null;
      if (ReplacementGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (LJC.HasText(name))
      {
        var record = new Replacement()
        {
          Name = name
        };
        RowSelect(record);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void ReplacementDetail_Change(object sender, EventArgs e)
    {
      //var detail = sender as ReplacementDetail;
      //var record = detail.LJCRecord;
      //if (record != null)
      //{
      //  if (detail.LJCIsUpdate)
      //  {
      //    RowUpdate(record);
      //  }
      //  else
      //  {
      //    var row = RowAdd(record);
      //    ReplacementGrid.LJCSetCurrentRow(row, true);
      //    EditList.TimedChange(Change.Replacement);
      //  }
      //}
    }
    #endregion

    #region Action Menu Event Handlers

    // Calls the New method.
    private void ReplacementMenuNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Calls the Edit method.
    private void ReplacementMenuEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Calls the Delete method.
    private void ReplacementMenuDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Calls the Refresh method.
    private void ReplacementMenuRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }

    // Performs the Generate Output function.
    private void ReplacementGenerate_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.Generate();
    }

    // Performs the Save function.
    private void ReplacementSave_Click(object? sender, EventArgs e)
    {
      SectionGridCode sectionGridCode = EditList.SectionGridCode;
      sectionGridCode.DataXMLSave();
    }

    // Performs the Close function.
    private void ReplacementMenuExit_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void ReplacementMenuHelp_Click(object? sender, EventArgs e)
    {
      Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
        , @"Data\ReplacementList.html");
    }
    #endregion

    #region Control Event Handlers

    // Handles the form keys.
    private void Grid_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          //mReplacementGridCode.DoEdit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
            , @"Data\ReplacementList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          //mReplacementGridCode.DoRefresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ReplacementGrid
              , Control.MousePosition);
            EditList.ReplacementMenu.Show(position);
            EditList.ReplacementMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ItemGrid.Select();
          }
          else
          {
            SectionGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void Grid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (ReplacementGrid.LJCGetMouseRow(e) != null)
      {
        EditList.ReplacementGridCode.Edit();
      }
    }

    // Handles the MouseDown event.
    private void ReplacementGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ReplacementGrid.Select();
        if (ReplacementGrid.LJCIsDifferentRow(e))
        {
          ReplacementGrid.LJCSetCurrentRow(e);
          EditList.TimedChange(Change.Replacement);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ReplacementGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (ReplacementGrid.LJCAllowSelectionChange)
      {
        EditList.TimedChange(Change.Replacement);
      }
      ReplacementGrid.LJCAllowSelectionChange = true;
    }
    #endregion
  }
}
