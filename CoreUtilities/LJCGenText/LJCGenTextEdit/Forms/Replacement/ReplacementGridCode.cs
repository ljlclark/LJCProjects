// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ReplacementGridCode.cs
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Windows.Forms;
using static LJCGenTextEdit.EditList;

namespace LJCGenTextEdit
{
  // Contains the ReplacementGrid methods.
  internal class ReplacementGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ReplacementGridCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;
      GenDataManager = EditList.GenDataManager;
      ItemGrid = EditList.ItemGrid;
      ReplacementGrid = EditList.ReplacementGrid;
      SectionGrid = EditList.SectionGrid;
      EditList.Cursor = Cursors.Default;
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
        && GenDataManager!= null)
      {
        // Data from items.
        string sectionName = parentRow.LJCGetCellText("Name");
        string repeatItemName = row.LJCGetCellText("Name");

        var records = GenDataManager.LoadReplacements(sectionName, repeatItemName);
        if (records != null && records.Count > 0)
        {
          foreach (Replacement record in records)
          {
            RowAdd(record);
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
      retValue.LJCSetValues(ReplacementGrid, dataRecord);
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
        gridRow.LJCSetValues(ReplacementGrid, dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      ReplacementDetail detail;

      if (SectionGrid.CurrentRow is LJCGridRow sectionRow
        && ItemGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        string sectionName = sectionRow.LJCGetCellText("Name");
        string parentName = parentRow.LJCGetCellText("Name");

        var location = FormCommon.GetDialogScreenPoint(ReplacementGrid);
        detail = new ReplacementDetail()
        {
          LJCGenDataManager = GenDataManager,
          LJCLocation = location,
          LJCParentName = parentName,
          LJCSectionName = sectionName
        };
        detail.LJCChange += ReplacementDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow sectionRow
        && ItemGrid.CurrentRow is LJCGridRow parentRow
        && ReplacementGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string sectionName = sectionRow.LJCGetCellText("Name");
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        var location = FormCommon.GetDialogScreenPoint(ReplacementGrid);
        var detail = new ReplacementDetail()
        {
          LJCGenDataManager = EditList.GenDataManager,
          LJCLocation = location,
          LJCParentName = parentName,
          LJCReplacementName = name,
          LJCSectionName = sectionName
        };
        detail.LJCChange += ReplacementDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var sectionRow = SectionGrid.CurrentRow as LJCGridRow; ;
      var parentRow = ItemGrid.CurrentRow as LJCGridRow; ;
      var row = ReplacementGrid.CurrentRow as LJCGridRow; ;
      if (sectionRow != null
        && parentRow != null
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
        string sectionName = sectionRow.LJCGetCellText("Name");
        string parentName = parentRow.LJCGetCellText("Name");
        string name = row.LJCGetCellText("Name");

        success = GenDataManager.DeleteReplacement(sectionName, parentName, name);
      }

      if (success)
      {
        success = GenDataManager.Save();
      }

      if (success)
      {
        ReplacementGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Replacement);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string name = null;
      if (ReplacementGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (NetString.HasValue(name))
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
      var detail = sender as ReplacementDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          var row = RowAdd(record);
          ReplacementGrid.LJCSetCurrentRow(row, true);
          EditList.TimedChange(Change.Replacement);
        }
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

    // Gets or sets the Replacement Grid reference.
    private LJCDataGrid ReplacementGrid { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }
    #endregion
  }
}
