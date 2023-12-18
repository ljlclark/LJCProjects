// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassItemGridCode.cs
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
  // Provides ClassItemGrid methods for the LJCGenDocList window.
  internal class ClassItemGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassItemGridCode(LJCGenDocList parentList)
    {
      DocList = parentList;
      AssemblyGrid = DocList.AssemblyItemGrid;
      ClassGrid = DocList.ClassItemGrid;
      ClassGroupGrid = DocList.ClassGroupGrid;
      Managers = DocList.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      DocList.Cursor = Cursors.WaitCursor;
      ClassGrid.LJCRowsClear();

      if (ClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        var manager = Managers.DocClassManager;
        var names = new List<string>()
        {
          LJCGenDocDAL.DocClass.ColumnSequence
        };
        manager.SetOrderBy(names);

        DbColumns keyColumns;
        if (0 == ClassGroupID())
        {
          // Get ungrouped classes.
          keyColumns = new DbColumns()
          {
            { LJCGenDocDAL.DocClass.ColumnDocClassGroupID, (object)"'-null'" },
            { LJCGenDocDAL.DocClass.ColumnDocAssemblyID, DocAssemblyID() }
          };
        }
        else
        {
          keyColumns = new DbColumns()
          {
            { LJCGenDocDAL.DocClass.ColumnDocClassGroupID, ClassGroupID() }
          };
        }
        DbResult result = manager.LoadResult(keyColumns);

        // Duplicate in ResultGridData.LoadRows()?
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      DocList.Cursor = Cursors.Default;
      DocList.DoChange(Change.ClassItem);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCGenDoc/Common/List.xml'/>
    internal bool RowSelect(DocClass dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        DocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ClassGrid.Rows)
        {
          if (DocClassID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ClassGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        DocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClass dataRecord)
    {
      var retValue = ClassGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ClassGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = ClassGrid.LJCRowAdd();

      var columnName = LJCGenDocDAL.DocClass.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ClassGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClass dataRecord)
    {
      if (ClassGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ClassGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClass dataRecord)
    {
      row.LJCSetInt32(LJCGenDocDAL.DocClass.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = ClassGrid.CurrentRow as LJCGridRow;
      if (ClassGroupGrid.CurrentRow is LJCGridRow _
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
          { LJCGenDocDAL.DocClass.ColumnDocClassGroupID, ClassGroupID() },
          { LJCGenDocDAL.DocClass.ColumnID, DocClassID() }
        };
        var manager = Managers.DocClassManager;
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
        ClassGrid.Rows.Remove(row);
        DocList.TimedChange(Change.ClassItem);
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (AssemblyGrid.CurrentRow is LJCGridRow _
        && ClassGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassDetail()
        {
          LJCAssemblyID = DocAssemblyID(),
          LJCClassID = DocClassID(),
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
        , @"Class\ClassItemList.html");
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      // Class has to belong to an Assembly.
      if (AssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassDetail()
        {
          LJCAssemblyID = DocAssemblyID(),
          LJCGroupID = ClassGroupID(),
          LJCManagers = Managers,
          LJCSequence = ClassGrid.Rows.Count + 1
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
      if (ClassGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = DocClassID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClass()
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
      var manager = Managers.DocClassManager;
      manager.ClassGroupID = ClassGroupID();
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ClassDetail;
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
          ClassGrid.LJCSetCurrentRow(row, true);
          CheckPreviousAndNext(detail);
          DoRefresh();
          DocList.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../Doc/ClassItemGridCode.xml'/>
    internal void DoDragDrop(short assemblyID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == ClassGrid.LJCDragDataName)
      {
        var targetIndex = ClassGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = DocClassName(DocClassID(sourceRow));
          var manager = Managers.DocClassManager;
          var sourceGroup = manager.RetrieveWithUnique(assemblyID, sourceName);

          // Get target group.
          var targetRow = ClassGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = DocClassName(DocClassID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(assemblyID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ClassGroupID = sourceGroup.DocClassGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == ClassGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          LJCGenDocDAL.DocClass.ColumnName,
          LJCGenDocDAL.DocClass.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var classManager = Managers.DocClassManager;
        GridColumns = classManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ClassGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(ClassGrid);
        ClassGrid.LJCDragDataName = "DocClass";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the DocClass row item.
    /// <include path='items/DocClass/*' file='../../Doc/ClassItemGridCode.xml'/>
    internal DocClass DocClass(LJCGridRow docClassRow = null)
    {
      DocClass retValue = null;

      if (null == docClassRow)
      {
        docClassRow = ClassGrid.CurrentRow as LJCGridRow;
      }
      if (docClassRow != null)
      {
        retValue = DocClassWithID(DocClassID());
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short ClassGroupID(LJCGridRow classGroupRow = null)
    {
      short retValue = 0;

      if (null == classGroupRow)
      {
        classGroupRow = ClassGroupGrid.CurrentRow as LJCGridRow;
      }
      if (classGroupRow != null)
      {
        retValue = (short)classGroupRow.LJCGetInt32(DocClassGroup.ColumnID);
        var classGroup = DocGroupWithID(retValue);
        if ("ungrouped" == classGroup.HeadingName.ToLower())
        {
          retValue = 0;
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short DocAssemblyID(LJCGridRow docAssemblyRow = null)
    {
      short retValue = 0;

      if (null == docAssemblyRow)
      {
        docAssemblyRow = AssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (docAssemblyRow != null)
      {
        retValue = (short)docAssemblyRow.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocGroup with the ID value.
    private DocClassGroup DocGroupWithID(short docGroupID)
    {
      DocClassGroup retValue = null;

      if (docGroupID > 0)
      {
        var manager = Managers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(docGroupID);
      }
      return retValue;
    }

    // Retrieves the DocClass name.
    private string DocClassName(short docClassID)
    {
      string retValue = null;

      var docClass = DocClassWithID(docClassID);
      if (docClass != null)
      {
        retValue = docClass.Name;
      }
      return retValue;
    }

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
        retValue = (short)docClassRow.LJCGetInt32(LJCGenDocDAL.DocClass.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocClass with the ID value.
    private DocClass DocClassWithID(short docClassID)
    {
      DocClass retValue = null;

      if (docClassID > 0)
      {
        var manager = Managers.DocClassManager;
        retValue = manager.RetrieveWithID(docClassID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(ClassDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(ClassDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = ClassGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          if (DocClassID() > 0)
          {
            detail.LJCClassID = DocClassID();
            detail.LJCNext = true;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = ClassGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          if (DocClassID() > 0)
          {
            detail.LJCClassID = DocClassID();
            detail.LJCPrevious = true;
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Assembly Grid reference.
    private LJCDataGrid AssemblyGrid { get; set; }

    // Gets or sets the Parent List reference.
    private LJCGenDocList DocList { get; set; }

    // Gets or sets the GridColumns value.
    private DbColumns GridColumns { get; set; }

    // Gets or sets the ClassGroup Grid reference.
    private LJCDataGrid ClassGroupGrid { get; set; }

    // Gets or sets the Class Grid reference.
    private LJCDataGrid ClassGrid { get; set; }

    // The Managers object.
    private ManagersGenDoc Managers { get; set; }
    #endregion
  }
}
