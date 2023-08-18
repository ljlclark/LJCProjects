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
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewJoinManager = Parent.ViewHelper.ViewJoinManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			ViewJoins dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.JoinGrid.Rows.Clear();
			Parent.JoinOnGrid.Rows.Clear();
			Parent.JoinColumnGrid.Rows.Clear();

			SetupGridJoin();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

				dataRecords = mViewJoinManager.LoadWithParentID(viewDataID);
				if (NetCommon.HasItems(dataRecords))
				{
					foreach (ViewJoin dataRecord in dataRecords)
					{
						RowAdd(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Join);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewJoin dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.JoinGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.JoinGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(ViewJoin dataRecord)
		{
			if (Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.JoinGrid, dataRecord);
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
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.JoinGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewJoin.ColumnID);
					if (rowID == record.ID)
					{
						Parent.JoinGrid.LJCSetCurrentRow(row, true);
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
			ViewJoinDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.JoinGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinDetail
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
			ViewJoinDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
        // Data from list items.
        int id = row.LJCGetInt32(ViewJoin.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.JoinGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinDetail()
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
			string title;
			string message;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
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
						Parent.JoinGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.Join);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewJoin record;
			int id = 0;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoin.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewJoin()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void JoinDetail_Change(object sender, EventArgs e)
    {
      ViewJoinDetail detail;
      ViewJoin record;
      LJCGridRow row;

      detail = sender as ViewJoinDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.JoinGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Join);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Join Grid.
    private void SetupGridJoin()
		{
			if (0 == Parent.JoinGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoin.PropertyTableName,
					ViewJoin.ColumnJoinType,
					ViewJoin.ColumnTableAlias
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns joinGridColumns
					= mViewJoinManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.JoinGrid.LJCAddColumns(joinGridColumns);
			}
		}
		#endregion

		#region Class Data

		private ViewJoinManager mViewJoinManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
