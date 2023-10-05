// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ReplacementGridCode.cs
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // Contains the ReplacementGrid methods.
  internal class ReplacementGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ReplacementGridCode(EditList parent)
    {
      // Set default class data.
      mParent = parent;
      mSectionGrid = mParent.SectionGrid;
      mItemGrid = mParent.ItemGrid;
      mReplacementGrid = mParent.ReplacementGrid;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/List.xml'/>
    internal void DataRetrieve()
    {
      Replacements records;

      mReplacementGrid.LJCRowsClear();
      GenDataManager manager = mParent.GenDataManager;

      if (mSectionGrid.CurrentRow is LJCGridRow parentRow
        && mItemGrid.CurrentRow is LJCGridRow row)
      {
        string sectionName = parentRow.LJCGetCellText("Name");
        string repeatItemName = row.LJCGetCellText("Name");
        records = manager.LoadReplacements(sectionName, repeatItemName);

        if (records != null && records.Count > 0)
        {
          foreach (Replacement record in records)
          {
            RowAdd(record);
          }
        }
      }
      mParent.DoChange(EditList.Change.Replacement);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Replacement dataRecord)
    {
      LJCGridRow retValue;

      retValue = mReplacementGrid.LJCRowAdd();

      // Sets the row values from a data object.
      retValue.LJCSetValues(mReplacementGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Replacement dataRecord)
    {
      if (mReplacementGrid.CurrentRow is LJCGridRow gridRow)
      {
        gridRow.LJCSetValues(mReplacementGrid, dataRecord);
      }
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Replacement dataRecord)
    {
      string name;
      bool retValue = false;

      if (dataRecord != null)
      {
        foreach (LJCGridRow row in mReplacementGrid.Rows)
        {
          name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            mReplacementGrid.LJCSetCurrentRow(row, true);
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
    internal void DoNew()
    {
      ReplacementDetail detail;

      if (mSectionGrid.CurrentRow is LJCGridRow sectionRow
        && mItemGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        string sectionName = sectionRow.LJCGetCellText("Name");
        string parentName = parentRow.LJCGetCellText("Name");

        var grid = mReplacementGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ReplacementDetail()
        {
          LJCSectionName = sectionName,
          LJCParentName = parentName,
          LJCGenDataManager = mParent.GenDataManager,
          LJCLocation = location
        };
        detail.LJCChange += ReplacementDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      ReplacementDetail detail;

      if (mSectionGrid.CurrentRow is LJCGridRow sectionRow
        && mItemGrid.CurrentRow is LJCGridRow parentRow
        && mReplacementGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string sectionName = sectionRow.LJCGetCellText("Name");
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        var grid = mReplacementGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new ReplacementDetail()
        {
          LJCSectionName = sectionName,
          LJCParentName = parentName,
          LJCReplacementName = name,
          LJCGenDataManager = mParent.GenDataManager,
          LJCLocation = location
        };
        detail.LJCChange += ReplacementDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      string title;
      string message;
      GenDataManager manager = mParent.GenDataManager;

      if (mSectionGrid.CurrentRow is LJCGridRow sectionRow
        && mItemGrid.CurrentRow is LJCGridRow parentRow
        && mReplacementGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from items.
          string sectionName = sectionRow.LJCGetCellText("Name");
          string parentName = parentRow.LJCGetCellText("Name");
          string name = row.LJCGetCellText("Name");

          manager.DeleteReplacement(sectionName, parentName, name);
          manager.Save();
          mReplacementGrid.Rows.Remove(row);
          mParent.TimedChange(EditList.Change.Replacement);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      Replacement dataRecord;
      string name = null;

      mParent.Cursor = Cursors.WaitCursor;
      if (mReplacementGrid.CurrentRow is LJCGridRow row)
      {
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (NetString.HasValue(name))
      {
        dataRecord = new Replacement()
        {
          Name = name
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void ReplacementDetail_Change(object sender, EventArgs e)
    {
      ReplacementDetail detail;
      Replacement dataRecord;
      LJCGridRow row;

      detail = sender as ReplacementDetail;
      dataRecord = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(dataRecord);
      }
      else
      {
        row = RowAdd(dataRecord);
        mReplacementGrid.LJCSetCurrentRow(row, true);
        mParent.TimedChange(EditList.Change.Replacement);
      }
    }
    #endregion

    #region Class Data

    private readonly EditList mParent;
    private readonly LJCDataGrid mSectionGrid;
    private readonly LJCDataGrid mItemGrid;
    private readonly LJCDataGrid mReplacementGrid;
    #endregion
  }
}
