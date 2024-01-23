// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupGridCode.cs
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
  // Provides ClassGroupGrid methods for the GenDocList window.
  internal class ClassGroupGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassGroupGridCode(LJCGenDocList parentList)
    {
      DocList = parentList;
      AssemblyGrid = DocList.AssemblyItemGrid;
      ClassGroupGrid = DocList.ClassGroupGrid;
      Managers = DocList.Managers;
      // *** Begin *** Add - Data Views
      var dbManagers = new ManagersDbView();
      var settings = DocList.Settings;
      dbManagers.SetDbProperties(settings.DbServiceRef
        , settings.DataConfigName);
      mDataDbView = new DataDbView(dbManagers);
      // *** End   *** Add - Data Views
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      ClassGroupGrid.LJCRowsClear();

      // Use Assembly as parent.
      if (AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var manager = Managers.DocClassGroupManager;
        var names = new List<string>()
        {
          DocClassGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocClassGroup.ColumnDocAssemblyID, DocAssemblyID() }
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
      DocList.DoChange(Change.ClassGroup);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClassGroup dataRecord)
    {
      var retValue = ClassGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ClassGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = ClassGroupGrid.LJCRowAdd();

      var columnName = DocClassGroup.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ClassGroupGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocClassGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ClassGroupGrid.Rows)
        {
          if (ClassGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ClassGroupGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroup dataRecord)
    {
      if (ClassGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ClassGroupGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClassGroup dataRecord)
    {
      row.LJCSetInt32(DocClassGroup.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var classGroupRow = ClassGroupGrid.CurrentRow as LJCGridRow;
      if (AssemblyGrid.CurrentRow is LJCGridRow _
        && classGroupRow != null)
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
          { DocClass.ColumnDocAssemblyID, DocAssemblyID() },
          { DocClass.ColumnID, ClassGroupID() }
        };
        var manager = Managers.DocClassGroupManager;
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
        ClassGroupGrid.Rows.Remove(classGroupRow);
        DocList.TimedChange(Change.ClassGroup);
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      // Use Assembly as parent.
      if (AssemblyGrid.CurrentRow is LJCGridRow _
        && ClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassGroupDetail()
        {
          LJCAssemblyID = DocAssemblyID(),
          LJCGroupID = ClassGroupID(),
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
        , @"Class\ClassGroupList.html");
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      // Use Assembly as parent.
      if (AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassGroupDetail()
        {
          LJCAssemblyID = DocAssemblyID(),
          LJCManagers = Managers,
          LJCSequence = ClassGroupGrid.Rows.Count + 1
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list. 
    internal void DoRefresh()
    {
      DocList.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (ClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = ClassGroupID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClassGroup()
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
      var procedure = true;
      if (procedure)
      {
        var classGroupManager = Managers.DocClassGroupManager;
        classGroupManager.AssemblyID = DocAssemblyID();
        classGroupManager.ResetSequence();
      }
      else
      {
        // Example of using DataManager object.
        var dataManager = Managers.DocClassGroupManager.Manager;
        var assemblyID = DocAssemblyID();
        var where = $"where DocAssemblyID = {assemblyID}";
        dataManager.Resequence("ID", "Sequence", where);
      }
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ClassGroupDetail;
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
          ClassGroupGrid.LJCSetCurrentRow(row, true);
          CheckPreviousAndNext(detail);
          DoRefresh();
          DocList.TimedChange(Change.ClassGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    internal void DoDragDrop(short assemblyID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == ClassGroupGrid.LJCDragDataName)
      {
        var targetIndex = ClassGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = ClassGroupHeadingName(ClassGroupID(sourceRow));
          var manager = Managers.DocClassGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(assemblyID, sourceName);

          // Get target group.
          var targetRow = ClassGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = ClassGroupHeadingName(ClassGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(assemblyID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.AssemblyID = sourceGroup.DocAssemblyID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      // *** Begin *** Change - Data Views
      // Clear previous grid columns definition as view may have changed.
      ClassGroupGrid.Columns.Clear();

      // Get the view grid columns
      var viewCombo = DocList.ClassGroupViewCombo;
      var viewInfo = viewCombo.GetInfo();
      var gridColumns = mDataDbView.GetGridColumns(viewInfo.DataID);
      if (gridColumns != null)
      {
        // Setup the grid columns.
        var columns = gridColumns.Clone();
        columns.LJCRemoveColumn(DocClassGroup.ColumnID);
        ClassGroupGrid.LJCAddColumns(columns);
        ClassGroupGrid.LJCRestoreColumnValues(DocList.ControlValues);
      }
      else
      {
        // Did not load any Grid Columns.
        ViewCommon.DoViewEdit(viewInfo, DocList.ConfigFileName);

        string title = "Reload Confirmation";
        string message = "Reload View Combo?";
        if (DialogResult.Yes == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          viewCombo = DocList.ClassGroupViewCombo;
          viewCombo.Items.Clear();
          viewCombo.LJCLoad();
        }
      }
      // *** End   *** Change - Data Views
      FormCommon.NotSortable(ClassGroupGrid);
      ClassGroupGrid.LJCDragDataName = "DocClassGroup";
    }
    #endregion

    #region Get Data Methods

    // Retrieves the AssemblyItem name.
    private string ClassGroupHeadingName(short classGroupID)
    {
      string retValue = null;

      var docClassGroup = ClassGroupWithID(classGroupID);
      if (docClassGroup != null)
      {
        retValue = docClassGroup.HeadingName;
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short ClassGroupID(LJCGridRow row = null)
    {
      short retValue = 0;

      if (null == row)
      {
        row = ClassGroupGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = (short)row.LJCGetInt32(DocClassGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the ClassGroup with the ID value.
    private DocClassGroup ClassGroupWithID(short classGroupID)
    {
      DocClassGroup retValue = null;

      if (classGroupID > 0)
      {
        var manager = Managers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(classGroupID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short DocAssemblyID(LJCGridRow row = null)
    {
      short retValue = 0;

      if (null == row)
      {
        row = AssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = (short)row.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(ClassGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(ClassGroupDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = ClassGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = ClassGroupID();
          if (id > 0)
          {
            detail.LJCGroupID = id;
            detail.LJCNext = true;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = ClassGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = ClassGroupID();
          if (id > 0)
          {
            detail.LJCGroupID = id;
            detail.LJCPrevious = true;
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // Gets or sets the Assembly Grid reference.
    private LJCDataGrid AssemblyGrid { get; set; }

    // Gets or sets the ClassGroup Grid reference.
    private LJCDataGrid ClassGroupGrid { get; set; }

    // Gets or sets the Parent List reference.
    private LJCGenDocList DocList { get; set; }

    // The Managers object.
    private ManagersGenDoc Managers { get; set; }
    #endregion

    // *** Next Statement *** Add - Data View
    private readonly DataDbView mDataDbView;
  }
}
