// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ItemGridCode.cs
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // Contains the ItemGrid methods.
  internal class ItemGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ItemGridCode(EditList parentList)
    {
      // Set default class data.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;
      GenDataManager = EditList.GenDataManager;
      ItemGrid = EditList.ItemGrid;
      SectionGrid = EditList.SectionGrid;
      EditList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ItemGrid.LJCRowsClear();

      if (SectionGrid.CurrentRow is LJCGridRow parentRow
        && GenDataManager!= null)
      {
        // Data from items.
        string sectionName = parentRow.LJCGetCellText("Name");

        var records = GenDataManager.LoadRepeatItems(sectionName);
        if (NetCommon.HasItems(records))
        {
          foreach (RepeatItem record in records)
          {
            RowAdd(record);
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(EditList.Change.Item);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(RepeatItem dataRecord)
    {
      var retValue = ItemGrid.LJCRowAdd();
      retValue.LJCSetValues(ItemGrid, dataRecord);
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
        gridRow.LJCSetValues(ItemGrid, dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (SectionGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        string parentName = parentRow.LJCGetCellText("Name");

        var location = FormCommon.GetDialogScreenPoint(ItemGrid);
        var detail = new ItemDetail()
        {
          LJCGenDataManager = EditList.GenDataManager,
          LJCLocation = location,
          LJCParentName = parentName
        };
        detail.LJCChange += ItemDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow parentRow
        && ItemGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        var location = FormCommon.GetDialogScreenPoint(ItemGrid);
        var detail = new ItemDetail()
        {
          LJCGenDataManager = GenDataManager,
          LJCItemName = name,
          LJCLocation = location,
          LJCParentName = parentName
        };
        detail.LJCChange += ItemDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
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

      if (success)
      {
        // Data from items.
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        success = GenDataManager.DeleteRepeatItem(parentName, name);
      }

      if (success)
      {
        success = GenDataManager.Save();
      }

      if (success)
      {
        ItemGrid.Rows.Remove(row);
        EditList.TimedChange(EditList.Change.Item);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string name = null;
      if (ItemGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (NetString.HasValue(name))
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
      var detail = sender as ItemDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ItemGrid.LJCSetCurrentRow(row, true);
        EditList.TimedChange(EditList.Change.Item);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Manager reference.
    internal GenDataManager GenDataManager { get; set; }

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    // Gets or sets the Item Grid reference.
    private LJCDataGrid ItemGrid { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }
    #endregion
  }
}
