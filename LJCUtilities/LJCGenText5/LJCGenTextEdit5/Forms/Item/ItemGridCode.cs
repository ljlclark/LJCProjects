// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ItemGridCode.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using static LJCGenTextEdit5.EditList;

namespace LJCGenTextEdit5
{
  // Contains the ItemGrid methods.
  internal class ItemGridCode
  {
    #region Control Code Properties
    #endregion

    #region Parent Object Properties

    // Gets or sets the Item Manager reference.
    internal RepeatItemManager ItemManager { get; set; }

    // Gets or sets the Section Manager reference.
    internal SectionManager SectionManager { get; set; }

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    // Gets or sets the Item Grid reference.
    private LJCDataGrid ItemGrid { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }

    private TemplateTextCode TemplateTextCode { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal ItemGridCode(EditList parentList)
    {
      // Set default class data.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      ItemGrid = EditList.ItemGrid;
      ItemManager = EditList.ItemManager;
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
      EditList.ItemMenuNew.Click += ItemMenuNew_Click;
      EditList.ItemMenuEdit.Click += ItemMenuEdit_Click;
      EditList.ItemMenuDelete.Click += ItemMenuDelete_Click;
      EditList.ItemMenuRefresh.Click += ItemMenuRefresh_Click;
      EditList.ItemMenuGenerate.Click += ItemMenuGenerate_Click;
      EditList.ItemMenuSave.Click += ItemMenuSave_Click;
      EditList.ItemMenuExit.Click += ItemMenuExit_Click;
      EditList.ItemMenuHelp.Click += ItemMenuHelp_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      ItemGrid.KeyDown += ItemGrid_KeyDown;
      ItemGrid.MouseDoubleClick += ItemGrid_MouseDoubleClick;
      ItemGrid.MouseDown += ItemGrid_MouseDown;
      ItemGrid.SelectionChanged += ItemGrid_SelectionChanged;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ItemGrid.LJCRowsClear();

      if (SectionGrid.CurrentRow is LJCGridRow parentRow
        && ItemManager != null)
      {
        // Data from items.
        string? sectionName = parentRow.LJCGetCellText("Name");

        if (LJC.HasText(sectionName))
        {
          var records = ItemManager.Load(sectionName);
          if (LJC.HasListItems(records))
          {
            foreach (RepeatItem record in records)
            {
              RowAdd(record);
            }
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Item);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(RepeatItem dataRecord)
    {
      var retValue = ItemGrid.LJCRowAdd();
      retValue.LJCSetValues(dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(RepeatItem dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ItemGrid.Rows)
        {
          var name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ItemGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(RepeatItem dataRecord)
    {
      if (ItemGrid.CurrentRow is LJCGridRow gridRow)
      {
        gridRow.LJCSetValues(dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void New()
    {
      if (SectionGrid.CurrentRow is LJCGridRow parentRow)
      {
        //// Data from items.
        //string parentName = parentRow.LJCGetCellText("Name");

        //var location = FormPoint.DialogScreenPoint(ItemGrid);
        //var detail = new ItemDetail()
        //{
        //  LJCGenDataManager = EditList.GenDataManager,
        //  LJCLocation = location,
        //  LJCParentName = parentName
        //};
        //detail.LJCChange += ItemDetail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void Edit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow parentRow
        && ItemGrid.CurrentRow is LJCGridRow row)
      {
        //// Data from items.
        //string parentName = parentRow.LJCGetCellText("Name");
        //string name = row.LJCGetCellText("Name");

        //var location = FormPoint.DialogScreenPoint(ItemGrid);
        //var detail = new ItemDetail()
        //{
        //  LJCGenDataManager = ItemManager,
        //  LJCItemName = name,
        //  LJCLocation = location,
        //  LJCParentName = parentName
        //};
        //detail.LJCChange += ItemDetail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var parentRow = SectionGrid.CurrentRow as LJCGridRow;
      var row = ItemGrid.CurrentRow as LJCGridRow;
      if (parentRow != null
        && row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (parentRow != null
        && row != null
        && success)
      {
        // Data from items.
        string? parentName = parentRow.LJCGetCellText("Name");
        string? name = row.LJCGetCellText("Name");

        if (LJC.HasText(parentName)
          && LJC.HasText(name))
        {
          success = ItemManager.Delete(parentName, name);
        }
      }

      if (success)
      {
        success = SectionManager.Save();
      }

      if (success)
      {
        ItemGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Item);
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string? name = null;
      if (ItemGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (LJC.HasText(name))
      {
        var dataRecord = new RepeatItem()
        {
          Name = name
        };
        RowSelect(dataRecord);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void ItemDetail_Change(object sender, EventArgs e)
    {
      //var detail = sender as ItemDetail;
      //var record = detail.LJCRecord;
      //if (detail.LJCIsUpdate)
      //{
      //  RowUpdate(record);
      //}
      //else
      //{
      //  // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
      //  var row = RowAdd(record);
      //  ItemGrid.LJCSetCurrentRow(row, true);
      //  EditList.TimedChange(EditList.Change.Item);
      //}
    }
    #endregion

    #region Action Event Handlers

    // Calls the New method.
    private void ItemMenuNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Calls the Edit method.
    private void ItemMenuEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // <summary>Calls the Delete method.</summary>
    private void ItemMenuDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Calls the Refresh method.
    private void ItemMenuRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }

    // Performs the Generate Output function.
    private void ItemMenuGenerate_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.Generate();
    }

    // Performs the Save function.
    private void ItemMenuSave_Click(object? sender, EventArgs e)
    {
      SectionGridCode sectionGridCode = EditList.SectionGridCode;
      sectionGridCode.DataXMLSave();
    }

    // Performs the Close function.
    private void ItemMenuExit_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void ItemMenuHelp_Click(object? sender, EventArgs e)
    {
      Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
        , @"Data\ItemList.html");
    }
    #endregion

    #region Control Event Handlers

    // Handles the form keys.
    private void ItemGrid_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
            , @"Data\ItemList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ItemGrid
              , Control.MousePosition);
            EditList.ItemMenu.Show(position);
            EditList.ItemMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            SectionGrid.Select();
          }
          else
          {
            EditList.ReplacementGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ItemGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (ItemGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void ItemGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        ItemGrid.Select();
        if (ItemGrid.LJCIsDifferentRow(e))
        {
          ItemGrid.LJCSetCurrentRow(e);
          EditList.TimedChange(Change.Item);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ItemGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (ItemGrid.LJCAllowSelectionChange)
      {
        EditList.TimedChange(Change.Item);
      }
      ItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion
  }
}
