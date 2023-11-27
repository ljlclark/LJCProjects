// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodGroupGridCode.cs
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
  // The MethodGroup grid code.
  // Provides MethodGroupGrid methods for the LJCDocList window.
  internal class MethodGroupGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal MethodGroupGridCode(LJCGenDocList parentList)
    {
      DocList = parentList;
      ClassGrid = DocList.ClassItemGrid;
      Managers = DocList.Managers;
      MethodGroupGrid = DocList.MethodGroupGrid;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      MethodGroupGrid.LJCRowsClear();

      // Use Class as parent.
      if (ClassGrid.CurrentRow is LJCGridRow _)
      {
        var manager = Managers.DocMethodGroupManager;
        var names = new List<string>()
        {
          DocMethodGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocMethodGroup.ColumnDocClassID, DocClassID() }
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
      DocList.DoChange(Change.MethodGroup);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethodGroup dataRecord)
    {
      var retValue = MethodGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MethodGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)  
    {
      var retValue = MethodGroupGrid.LJCRowAdd();

      var columnName = DocMethodGroup.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(MethodGroupGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocMethodGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in MethodGroupGrid.Rows)
        {
          if (MethodGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodGroupGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroup dataRecord)
    {
      if (MethodGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(MethodGroupGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocMethodGroup dataRecord)
    {
      row.LJCSetInt32(DocMethodGroup.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      // Use Class as parent.
      if (ClassGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodGroupDetail()
        {
          LJCClassID = DocClassID(),
          LJCManagers = Managers,
          LJCSequence = MethodGroupGrid.Rows.Count + 1
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      // Use Class as parent.
      if (ClassGrid.CurrentRow is LJCGridRow _
        && MethodGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodGroupDetail()
        {
          LJCGroupID = MethodGroupID(),
          LJCClassID = DocClassID(),
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
      var row = MethodGroupGrid.CurrentRow;
      if (ClassGrid.CurrentRow is LJCGridRow _
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
          { DocMethodGroup.ColumnID, MethodGroupID() },
          { DocMethodGroup.ColumnDocClassID, DocClassID() }
        };
        var manager = Managers.DocMethodGroupManager;
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
        MethodGroupGrid.Rows.Remove(row);
        DocList.TimedChange(Change.MethodGroup);
      }
    }

    // Refreshes the list. 
    internal void DoRefresh()
    {
      DocList.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (MethodGroupGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = MethodGroupID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocMethodGroup()
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
      var manager = Managers.DocMethodGroupManager;
      manager.ClassID = DocClassID();
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodGroupDetail;
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
          MethodGroupGrid.LJCSetCurrentRow(row, true);
          CheckPreviousAndNext(detail);
          DoRefresh();
          DocList.TimedChange(Change.MethodGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    internal void DoDragDrop(short docClassID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == MethodGroupGrid.LJCDragDataName)
      {
        var targetIndex = MethodGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = MethodGroupHeadingName(MethodGroupID(sourceRow));
          var manager = Managers.DocMethodGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(docClassID, sourceName);

          // Get target group.
          var targetRow = MethodGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = MethodGroupHeadingName(MethodGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(docClassID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ClassID = sourceGroup.DocClassID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == MethodGroupGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocMethodGroup.ColumnHeadingName,
          DocMethodGroup.ColumnHeadingTextCustom
        };

        // Get the grid columns from the manager Data Definition.
        var classGroupManager = Managers.DocMethodGroupManager;
        GridColumns = classGroupManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MethodGroupGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(MethodGroupGrid);
        MethodGroupGrid.LJCDragDataName = "DocMethodGroup";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item ID.
    private short DocClassID(LJCGridRow docClassRow = null)
    {
      short retValue = 0;

      if (null == docClassRow)
      {
        docClassRow = ClassGrid.CurrentRow as LJCGridRow;
      }
      if (docClassRow != null)
      {
        retValue = (short)docClassRow.LJCGetInt32(DocClass.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocMethodGroup name.
    private string MethodGroupHeadingName(short methodGroupID)
    {
      string retValue = null;

      var docMethodGroup = MethodGroupWithID(methodGroupID);
      if (docMethodGroup != null)
      {
        retValue = docMethodGroup.HeadingName;
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short MethodGroupID(LJCGridRow methodGroupRow = null)
    {
      short retValue = 0;

      if (null == methodGroupRow)
      {
        methodGroupRow = MethodGroupGrid.CurrentRow as LJCGridRow;
      }
      if (methodGroupRow != null)
      {
        retValue = (short)methodGroupRow.LJCGetInt32(DocMethodGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the MethodGroup with the ID value.
    private DocMethodGroup MethodGroupWithID(short methodGroupID)
    {
      DocMethodGroup retValue = null;

      if (methodGroupID > 0)
      {
        var manager = Managers.DocMethodGroupManager;
        retValue = manager.RetrieveWithID(methodGroupID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(MethodGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(MethodGroupDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = MethodGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var methodGroupID = MethodGroupID();
          if (methodGroupID > 0)
          {
            detail.LJCNext = true;
            detail.LJCGroupID = methodGroupID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(MethodGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid groupGrid = MethodGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          groupGrid.LJCSetCurrentRow(currentIndex - 1, true);
          var methodGroupID = MethodGroupID();
          if (methodGroupID > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCGroupID = methodGroupID;
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Class Grid reference.
    private LJCDataGrid ClassGrid { get; set; }

    // Gets or sets the Parent List reference.
    private LJCGenDocList DocList { get; set; }

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }

    // Gets or sets the MethodGroup Grid reference.
    private LJCDataGrid MethodGroupGrid { get; set; }
    #endregion
  }
}
