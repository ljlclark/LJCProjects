// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;

namespace LJCViewEditor
{
	// Provides JoinGrid methods for the ViewEditorList window.
	internal class JoinGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal JoinGridClass(ViewEditorList parent)
		{
			Parent = parent;
      JoinGrid = Parent.JoinGrid;
      ViewGrid = Parent.ViewGrid;
      ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
      Managers = Parent.Managers;
      mViewJoinManager = Managers.ViewJoinManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			JoinGrid.Rows.Clear();
			Parent.JoinOnGrid.Rows.Clear();
			Parent.JoinColumnGrid.Rows.Clear();

			SetupGridJoin();
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var manager = mViewJoinManager;
        var result = manager.ResultWithParentID(viewDataID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Join);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewJoin dataRecord)
		{
			var retValue = JoinGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
			retValue.LJCSetValues(JoinGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = JoinGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoin.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewJoin.ColumnTableName;
      var name = dbValues.LJCGetString(columnName);
      retValue.LJCSetString(columnName, name);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoin dataRecord)
		{
			if (JoinGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(JoinGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewJoin dataRecord)
		{
			row.LJCSetInt32(ViewJoin.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewJoin.ColumnTableName, dataRecord.JoinTableName);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewJoin record)
		{
			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in JoinGrid.Rows)
				{
					var rowID = row.LJCGetInt32(ViewJoin.ColumnID);
					if (rowID == record.ID)
					{
						JoinGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				Parent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = JoinGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewJoinDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			if (ViewGrid.CurrentRow is LJCGridRow parentRow
				&& JoinGrid.CurrentRow is LJCGridRow row)
			{
        // Data from list items.
        int id = row.LJCGetInt32(ViewJoin.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = JoinGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewJoinDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			if (JoinGrid.CurrentRow is LJCGridRow row)
			{
				var title = "Delete Confirmation";
				var message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewJoin.ColumnID, row.LJCGetInt32(ViewJoin.ColumnID) }
					};
					mViewJoinManager.Delete(keyColumns);
					if (mViewJoinManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						JoinGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.Join);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;
			if (JoinGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoin.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new ViewJoin()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void JoinDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewJoinDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        JoinGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Join);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Join Grid.
    private void SetupGridJoin()
		{
			if (0 == JoinGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoin.PropertyTableName,
					ViewJoin.ColumnJoinType,
					ViewJoin.ColumnTableAlias
				};

				// Get the grid columns from the manager Data Definition.
				var joinGridColumns
					= mViewJoinManager.GetColumns(propertyNames);

				// Setup the grid columns.
				JoinGrid.LJCAddColumns(joinGridColumns);
			}
		}
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid JoinGrid { get; set; }

    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewJoinManager mViewJoinManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
