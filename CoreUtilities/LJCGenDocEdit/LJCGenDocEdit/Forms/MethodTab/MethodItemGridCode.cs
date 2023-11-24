// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodItemGridCode.cs
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
  /// <summary>The MethodItem grid code.</summary>
  internal class MethodItemGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodItemGridCode(LJCGenDocList parentList)
    {
      DocList = parentList;
      ClassGrid = DocList.ClassItemGrid;
      Managers = DocList.Managers;
      MethodGrid = DocList.MethodItemGrid;
      MethodGroupGrid = DocList.MethodGroupGrid;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      MethodGrid.LJCRowsClear();

      if (DocList.MethodGroupGrid.CurrentRow is LJCGridRow _)
      {
        var manager = Managers.DocMethodManager;
        var names = new List<string>()
        {
          DocMethod.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocMethod.ColumnDocMethodGroupID, MethodGroupID() }
        };
        DbResult result = manager.LoadResult(keyColumns);

        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
        DocList.Cursor = Cursors.Default;
        DocList.DoChange(Change.MethodItem);
      }
      DocList.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal bool RowSelect(DocMethod dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in MethodGrid.Rows)
        {
          if (DocMethodID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethod dataRecord)
    {
      var retValue = MethodGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MethodGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = MethodGrid.LJCRowAdd();
      var columnName = DocMethod.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      retValue.LJCSetValues(MethodGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethod dataRecord)
    {
      if (MethodGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(MethodGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocMethod dataRecord)
    {
      row.LJCSetInt32(DocMethod.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (ClassGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodDetail()
        {
          LJCClassID = DocClassID(),
          LJCGroupID = MethodGroupID(),
          Managers = Managers,
          Sequence = MethodGrid.Rows.Count + 1
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (ClassGrid.CurrentRow is LJCGridRow _
        && MethodGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodDetail()
        {
          LJCClassID = DocClassID(),
          LJCGroupID = MethodGroupID(),
          Managers = Managers,
          LJCMethodID = DocMethodID()
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Deletes the selected row.</summary>
    internal void DoDelete()
    {
      bool success = false;
      var methodRow = MethodGrid.CurrentRow as LJCGridRow;
      if (methodRow != null)
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
          { DocMethod.ColumnID, DocMethodID() }
        };
        var manager = Managers.DocMethodManager;
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
        MethodGrid.Rows.Remove(methodRow);
        DocList.TimedChange(Change.MethodItem);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      DocList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var id = DocMethodID();
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocMethod()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      DocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocMethodManager;
      manager.MethodGroupID = MethodGroupID();
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(dataRecord);
          CheckPreviousAndNext(detail);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          RowAdd(dataRecord);
          CheckPreviousAndNext(detail);
          DocList.TimedChange(Change.MethodItem);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../Doc/MethodItemGridCode.xml'/>
    internal void DoDragDrop(short docClassID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == MethodGrid.LJCDragDataName)
      {
        var targetIndex = MethodGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          // *** Begin *** Change - 9/25/23 #Overload
          var sourceMethod = DocMethodWithID(DocMethodID(sourceRow));

          // Get target group.
          var targetRow = MethodGrid.Rows[targetIndex] as LJCGridRow;
          var targetMethod = DocMethodWithID(DocMethodID(targetRow));

          var sourceSequence = sourceMethod.Sequence;
          var targetSequence = targetMethod.Sequence;
          var manager = Managers.DocMethodManager;
          manager.MethodGroupID = sourceMethod.DocMethodGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          // *** End   *** Change - 9/25/23 #Overload
          DoRefresh();
        }
      }
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == MethodGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocMethod.ColumnName,
          DocMethod.ColumnOverloadName,
          DocMethod.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var methodManager = Managers.DocMethodManager;
        GridColumns = methodManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MethodGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(MethodGrid);
        MethodGrid.LJCDragDataName = "DocMethod";
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

    // Retrieves the current row item ID.
    private short DocMethodID(LJCGridRow docMethodRow = null)
    {
      short retValue = 0;

      if (null == docMethodRow)
      {
        docMethodRow = MethodGrid.CurrentRow as LJCGridRow;
      }
      if (docMethodRow != null)
      {
        retValue = (short)docMethodRow.LJCGetInt32(DocMethod.ColumnID);
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

    // Retrieves the DocMethod with the ID value.
    private DocMethod DocMethodWithID(short docMethodID)
    {
      DocMethod retValue = null;

      if (docMethodID > 0)
      {
        var manager = Managers.DocMethodManager;
        retValue = manager.RetrieveWithID(docMethodID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(MethodDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(MethodDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = MethodGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var methodID = DocMethodID();
          if (methodID > 0)
          {
            detail.LJCNext = true;
            detail.LJCMethodID = methodID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(MethodDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = MethodGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var methodID = DocMethodID();
          if (methodID > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCMethodID = methodID;
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

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }

    // Gets or sets the Method Grid reference.
    private LJCDataGrid MethodGrid { get; set; }

    // Gets or sets the MethodGroup Grid reference.
    private LJCDataGrid MethodGroupGrid { get; set; }
    #endregion
  }
}
