// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemGridCode.cs
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // Provides AssemblyItemGrid methods for the LJCGenDocList window.
  internal class AssemblyItemGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal AssemblyItemGridCode(LJCGenDocList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      DocList = parentList;
      AssemblyGrid = DocList.AssemblyItemGrid;
      AssemblyGroupGrid = DocList.AssemblyGroupGrid;
      DocAssemblyManager = Managers.DocAssemblyManager;
      Managers = DocList.Managers;
      DocList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      AssemblyGrid.LJCRowsClear();

      if (DocList.AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        var manager = DocAssemblyManager;
        var names = new List<string>()
        {
          DocAssembly.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, AssemblyGroupID() }
        };
        DbResult result = manager.LoadResult(keyColumns);
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      DocList.Cursor = Cursors.Default;
      DocList.DoChange(Change.AssemblyItem);
    }

    // Selects a row based on the key record values.
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in AssemblyGrid.Rows)
        {
          if (AssemblyID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            AssemblyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = AssemblyGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(AssemblyGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = AssemblyGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = DocAssembly.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (AssemblyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(AssemblyGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssembly dataRecord)
    {
      row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyDetail()
        {
          LJCGroupID = AssemblyGroupID(),
          LJCManagers = Managers,
          LJCSequence = AssemblyGrid.Rows.Count + 1
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow _
        && AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyDetail()
        {
          LJCAssemblyID = AssemblyID(),
          LJCGroupID = AssemblyGroupID(),
          LJCManagers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = AssemblyGrid.CurrentRow as LJCGridRow;
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow _
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
        var keyRecord = new DbColumns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, AssemblyGroupID() },
          { DocAssembly.ColumnID, AssemblyID() }
        };
        var manager = DocAssemblyManager;
        manager.Delete(keyRecord);
        if (0 == manager.Manager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        AssemblyGrid.Rows.Remove(row);
        DocList.TimedChange(Change.AssemblyItem);
      }
    }

    // Refreshes the list. 
    internal void DoRefresh()
    {
      DocList.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = AssemblyID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocAssembly()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      DocList.Cursor = Cursors.Default;
    }

    // Resets the Sequence column values.
    internal void DoResetSequence()
    {
      var manager = Managers.DocAssemblyManager;
      manager.AssemblyGroupID = AssemblyGroupID();
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as AssemblyDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(dataRecord);
          CheckPreviousAndNext(detail);
          DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          CheckPreviousAndNext(detail);
          AssemblyGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          DocList.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == AssemblyGrid.LJCDragDataName)
      {
        var targetIndex = AssemblyGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = AssemblyName(AssemblyID(sourceRow));
          var manager = DocAssemblyManager;
          var sourceGroup = manager.RetrieveWithName(sourceName);

          // Get target group.
          var targetRow = AssemblyGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = AssemblyName(AssemblyID(targetRow));
          var targetGroup = manager.RetrieveWithName(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.AssemblyGroupID = sourceGroup.DocAssemblyGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == AssemblyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocAssembly.ColumnName,
          DocAssembly.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var assemblyManager = DocAssemblyManager;
        GridColumns = assemblyManager.GetColumns(propertyNames);

        // Setup the grid columns.
        AssemblyGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(AssemblyGrid);
        AssemblyGrid.LJCDragDataName = "DocAssembly";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item.
    internal DocAssembly CurrentAssembly()
    {
      DocAssembly retValue = null;

      if (AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var assemblyID = AssemblyID();
        if (assemblyID > 0)
        {
          var manager = DocAssemblyManager;
          retValue = manager.RetrieveWithID(assemblyID);
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short AssemblyGroupID(LJCGridRow assemblyGroupRow = null)
    {
      short retValue = 0;

      if (null == assemblyGroupRow)
      {
        assemblyGroupRow = AssemblyGroupGrid.CurrentRow as LJCGridRow;
      }
      if (assemblyGroupRow != null)
      {
        retValue = (short)assemblyGroupRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short AssemblyID(LJCGridRow assemblyRow = null)
    {
      short retValue = 0;

      if (null == assemblyRow)
      {
        assemblyRow = AssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (assemblyRow != null)
      {
        retValue = (short)assemblyRow.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocAssembly name.
    private string AssemblyName(short docAssemblyID)
    {
      string retValue = null;

      var docAssembly = AssemblyWithID(docAssemblyID);
      if (docAssembly != null)
      {
        retValue = docAssembly.Name;
      }
      return retValue;
    }

    // Retrieves the DoAssembly with the ID value.
    private DocAssembly AssemblyWithID(short docAssemblyID)
    {
      DocAssembly retValue = null;

      if (docAssemblyID > 0)
      {
        var manager = Managers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(docAssemblyID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(AssemblyDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(AssemblyDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = AssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          if (AssemblyID() > 0)
          {
            detail.LJCNext = true;
            detail.LJCAssemblyID = AssemblyID();
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = AssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          if (AssemblyID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCAssemblyID = AssemblyID();
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // Gets or sets the AssemblyGrid reference.
    private LJCDataGrid AssemblyGrid { get; set; }

    // Gets or sets the AssemblyGroupGrid reference.
    private LJCDataGrid AssemblyGroupGrid { get; set; }

    // Gets or sets the Manager reference.
    private DocAssemblyManager DocAssemblyManager { get; set; }

    // Gets or sets the Parent List reference.
    private LJCGenDocList DocList { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion
  }
}
