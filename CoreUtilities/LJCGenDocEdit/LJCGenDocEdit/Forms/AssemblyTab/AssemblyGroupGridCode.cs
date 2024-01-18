// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyGroupGridCode.cs
using LJCDBMessage;
// *** Begin *** Add - Data Views
using LJCDBViewControls;
using LJCDBViewDAL;
// *** End   *** Add - Data Views
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
  // Provides AssemblyGroupGrid methods for the LJCGenDocList window.
  internal class AssemblyGroupGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal AssemblyGroupGridCode(LJCGenDocList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      DocList = parentList;
      AssemblyGroupGrid = DocList.AssemblyGroupGrid;
      Managers = DocList.Managers;
      // *** Begin *** Add - Data Views
      var settings = DocList.Settings;
      var dbManagers = new ManagersDbView();
      dbManagers.SetDbProperties(settings.DbServiceRef
        , settings.DataConfigName);
      mDataDbView = new DataDbView(dbManagers);
      // *** End   *** Add - Data Views
      DocAssemblyGroupManager = Managers.DocAssemblyGroupManager;
      DocList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      AssemblyGroupGrid.LJCRowsClear();

      var manager = DocAssemblyGroupManager;
      var names = new List<string>()
      {
        DocAssemblyGroup.ColumnSequence
      };
      manager.SetOrderBy(names);

      DbResult result = manager.LoadResult();
      if (DbResult.HasRows(result))
      {
        foreach (DbRow dbRow in result.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      DocList.Cursor = Cursors.Default;
      DocList.DoChange(Change.AssemblyGroup);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssemblyGroup dataRecord)
    {
      var retValue = AssemblyGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(AssemblyGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = AssemblyGroupGrid.LJCRowAdd();

      var columnName = DocAssemblyGroup.ColumnID;
      var id = dbValues.LJCGetInt16(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(AssemblyGroupGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocAssemblyGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in AssemblyGroupGrid.Rows)
        {
          if (AssemblyGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            AssemblyGroupGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssemblyGroup dataRecord)
    {
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(AssemblyGroupGrid, dataRecord);
      }
    }

    // Sets the row stored record values.
    private void SetStoredValues(LJCGridRow row, DocAssemblyGroup dataRecord)
    {
      row.LJCSetInt32(DocAssemblyGroup.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = AssemblyGroupGrid.CurrentRow as LJCGridRow;
      if (row != null)
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
        var keyColumns = new DbColumns()
        {
          { DocAssemblyGroup.ColumnID, AssemblyGroupID() }
        };
        var manager = DocAssemblyGroupManager;
        manager.Delete(keyColumns);
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
        AssemblyGroupGrid.Rows.Remove(row);
        DocList.TimedChange(Change.AssemblyGroup);
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyGroupDetail()
        {
          LJCGroupID = AssemblyGroupID(),
          LJCManagers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Shows the help page.
    internal void DoHelp()
    {
      Help.ShowHelp(DocList, "GenDocEdit.chm", HelpNavigator.Topic
        , @"Assembly\AssemblyGroupList.html");
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var detail = new AssemblyGroupDetail()
      {
        LJCManagers = Managers,
        LJCSequence = AssemblyGroupGrid.Rows.Count + 1
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    // Refreshes the list. 
    internal void DoRefresh()
    {
      DocList.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = AssemblyGroupID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DocAssemblyGroup()
        {
          ID = id
        };
        RowSelect(record);
      }
      DocList.Cursor = Cursors.Default;
    }

    // Resets the Sequence column values.
    internal void DoResetSequence()
    {
      DocAssemblyGroupManager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as AssemblyGroupDetail;
      if (detail.LJCRecord != null)
      {
        var record = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
          CheckPreviousAndNext(detail);
          DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          AssemblyGroupGrid.LJCSetCurrentRow(row, true);
          CheckPreviousAndNext(detail);
          DoRefresh();
          DocList.TimedChange(Change.AssemblyGroup);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // The DragDrop method.
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == AssemblyGroupGrid.LJCDragDataName)
      {
        var targetIndex = AssemblyGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = AssemblyGroupName(AssemblyGroupID(sourceRow));
          var manager = Managers.DocAssemblyGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = AssemblyGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = AssemblyGroupName(AssemblyGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid(ViewInfo viewInfo)
    {
      // *** Begin *** Change - Data Views
      // Clear previous grid columns definition as view may have changed.
      AssemblyGroupGrid.Columns.Clear();

      // Get the view grid columns
      var gridColumns = mDataDbView.GetGridColumns(viewInfo.DataID);
      if (gridColumns != null)
      {
        // Setup the grid columns.
        var columns = gridColumns.Clone();
        columns.LJCRemoveColumn(DocAssemblyGroup.ColumnID);
        AssemblyGroupGrid.LJCAddColumns(columns);
        AssemblyGroupGrid.LJCRestoreColumnValues(DocList.ControlValues);
      }
      else
      {
        // Did not load any Grid Columns.
        var viewCombo = DocList.AssemblyGroupViewCombo;
        var dataID = viewCombo.LJCSelectedItemID();
        viewInfo.DataID = dataID;
        ViewCommon.DoViewEdit(viewInfo, DocList.ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          gridColumns = mDataDbView.GetGridColumns(viewInfo.DataID);
          AssemblyGroupGrid.LJCAddColumns(gridColumns);
          AssemblyGroupGrid.LJCRestoreColumnValues(DocList.ControlValues);
          viewCombo.Items.Clear();
          viewCombo.LJCLoad();
        }
      }
      // *** End   *** Change - Data Views
      FormCommon.NotSortable(AssemblyGroupGrid);
      AssemblyGroupGrid.LJCDragDataName = "DocAssemblyGroup";
    }
    #endregion

    #region Get Data Methods

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

    // Retrieves the AssemblyGroup name.
    private string AssemblyGroupName(short assemblyGroupID)
    {
      string retValue = null;

      var assemblyGroup = AssemblyGroupWithID(assemblyGroupID);
      if (assemblyGroup != null)
      {
        retValue = assemblyGroup.Name;
      }
      return retValue;
    }

    // Retrieves the AssemblyGroup with the ID value.
    private DocAssemblyGroup AssemblyGroupWithID(short assemblyGroupID)
    {
      DocAssemblyGroup retValue = null;

      if (assemblyGroupID > 0)
      {
        var manager = Managers.DocAssemblyGroupManager;
        retValue = manager.RetrieveWithID(assemblyGroupID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(AssemblyGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(AssemblyGroupDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid groupGrid = AssemblyGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < groupGrid.Rows.Count - 1)
        {
          groupGrid.LJCSetCurrentRow(currentIndex + 1, true);
          var assemblyGroupID = AssemblyGroupID();
          if (assemblyGroupID > 0)
          {
            detail.LJCNext = true;
            detail.LJCGroupID = assemblyGroupID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid groupGrid = AssemblyGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          groupGrid.LJCSetCurrentRow(currentIndex - 1, true);
          if (AssemblyGroupID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCGroupID = AssemblyGroupID();
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the AssemblyGroup Grid reference.
    private LJCDataGrid AssemblyGroupGrid { get; set; }

    // Gets or sets the Manager reference.
    private DocAssemblyGroupManager DocAssemblyGroupManager { get; set; }

    // Gets or sets the Parent List reference.
    private LJCGenDocList DocList { get; set; }

    // Gets or sets the GridColumns reference.
    //private DbColumns GridColumns { get; set; }

    // Gets or sets the Managers reference.
    private ManagersGenDoc Managers { get; set; }
    #endregion

    // *** Next Statement *** Add - Data View
    private readonly DataDbView mDataDbView;
  }
}
