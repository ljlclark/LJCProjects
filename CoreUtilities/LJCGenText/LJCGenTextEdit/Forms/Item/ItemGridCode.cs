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
    internal ItemGridCode(EditList parent)
    {
      // Set default class data.
      mParent = parent;
      mSectionGrid = mParent.SectionGrid;
      mItemGrid = mParent.ItemGrid;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/List.xml'/>
    internal void DataRetrieveItem()
    {
      RepeatItems records;
      GenDataManager manager = mParent.GenDataManager;

      mItemGrid.LJCRowsClear();

      if (mSectionGrid.CurrentRow is LJCGridRow parentRow)
      {
        string sectionName = parentRow.LJCGetCellText("Name");
        records = manager.LoadRepeatItems(sectionName);

        if (records != null && records.Count > 0)
        {
          foreach (RepeatItem record in records)
          {
            RowAddItem(record);
          }
        }
      }
      mParent.DoChange(EditList.ChangeItem);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAddItem(RepeatItem dataRecord)
    {
      LJCGridRow retValue;

      retValue = mItemGrid.LJCRowAdd();

      // Sets the row values from a data object.
      mItemGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdateItem(RepeatItem dataRecord)
    {
      if (mItemGrid.CurrentRow is LJCGridRow row)
      {
        mItemGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Selects a row based on the key record values.
    private bool RowSelectItem(RepeatItem dataRecord)
    {
      string name;
      bool retValue = false;

      if (dataRecord != null)
      {
        foreach (LJCGridRow row in mItemGrid.Rows)
        {
          name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            mItemGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNewItem()
    {
      ItemDetail detail;

      if (mSectionGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        string parentName = parentRow.LJCGetCellText("Name");

        var grid = mItemGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ItemDetail()
        {
          LJCParentName = parentName,
          LJCGenDataManager = mParent.GenDataManager,
          LJCLocation = location
        };
        detail.LJCChange += ItemDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEditItem()
    {
      ItemDetail detail;

      if (mSectionGrid.CurrentRow is LJCGridRow parentRow
        && mItemGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        var grid = mItemGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ItemDetail()
        {
          LJCParentName = parentName,
          LJCItemName = name,
          LJCGenDataManager = mParent.GenDataManager,
          LJCLocation = location
        };
        detail.LJCChange += ItemDetail_Change;
        detail.ShowDialog();
      }
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void ItemDetail_Change(object sender, EventArgs e)
    {
      ItemDetail detail;
      RepeatItem dataRecord;
      LJCGridRow row;

      detail = sender as ItemDetail;
      dataRecord = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateItem(dataRecord);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddItem(dataRecord);
        mItemGrid.LJCSetCurrentRow(row, true);
        mParent.ChangeTimer.DoChange(EditList.ChangeItem);
      }
    }

    // Deletes the selected row.
    internal void DoDeleteItem()
    {
      string title;
      string message;
      GenDataManager manager = mParent.GenDataManager;

      if (mSectionGrid.CurrentRow is LJCGridRow parentRow
        && mItemGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from items.
          string parentName = parentRow.LJCGetCellText("Name");
          string name = row.LJCGetCellText("Name");

          manager.DeleteRepeatItem(parentName, name);
          manager.Save();
          mItemGrid.Rows.Remove(row);
          mParent.ChangeTimer.DoChange(EditList.ChangeItem);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefreshItem()
    {
      RepeatItem dataRecord;
      string name = null;

      mParent.Cursor = Cursors.WaitCursor;
      if (mItemGrid.CurrentRow is LJCGridRow row)
      {
        name = row.LJCGetCellText("Name");
      }
      DataRetrieveItem();

      // Select the original row.
      if (NetString.HasValue(name))
      {
        dataRecord = new RepeatItem()
        {
          Name = name
        };
        RowSelectItem(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }
    #endregion

    #region Class Data

    private readonly EditList mParent;
    private readonly LJCDataGrid mSectionGrid;
    private readonly LJCDataGrid mItemGrid;
    #endregion
  }
}
