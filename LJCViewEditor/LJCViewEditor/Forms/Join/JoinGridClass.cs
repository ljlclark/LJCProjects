// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;

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
		internal void DataRetrieveJoin()
		{
			ViewJoins dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.JoinGrid.Rows.Clear();
			Parent.JoinOnGrid.Rows.Clear();
			Parent.JoinColumnGrid.Rows.Clear();

			ConfigureJoinGrid();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

				dataRecords = mViewJoinManager.LoadWithParentID(viewDataID);
				if (dataRecords != null && dataRecords.Count > 0)
				{
					foreach (ViewJoin dataRecord in dataRecords)
					{
						RowAddJoin(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Join);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddJoin(ViewJoin dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.JoinGrid.LJCRowAdd();
			SetStoredValuesJoin(retValue, dataRecord);

			// Sets the row values from a data object.
			Parent.JoinGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateJoin(ViewJoin dataRecord)
		{
			if (Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesJoin(row, dataRecord);
				Parent.JoinGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesJoin(LJCGridRow row, ViewJoin dataRecord)
		{
			row.LJCSetInt32(ViewJoin.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewJoin.ColumnTableName, dataRecord.JoinTableName);
		}

		// Selects a row based on the key record values.
		private void RowSelectViewJoin(ViewJoin record)
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
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
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
		internal void DoNewViewJoin()
		{
			ViewJoinDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
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
		internal void DoEditViewJoin()
		{
			ViewJoinDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
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
				RowUpdateJoin(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddJoin(record);
				Parent.JoinGrid.LJCSetCurrentRow(row, true);
				Parent.TimedChange(ViewEditorList.Change.Join);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteViewJoin()
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
		internal void DoRefreshViewJoin()
		{
			ViewJoin record;
			int id = 0;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoin.ColumnID);
			}
			DataRetrieveJoin();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewJoin()
				{
					ID = id
				};
				RowSelectViewJoin(record);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the View Join Grid.
		private void ConfigureJoinGrid()
		{
			if (0 == Parent.JoinGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoin.PropertyTableName,
					ViewJoin.ColumnJoinType,
					ViewJoin.ColumnTableAlias
				};

				// Get the display columns from the manager Data Definition.
				DbColumns joinDisplayColumns
					= mViewJoinManager.GetColumns(propertyNames);

				// Setup the grid display columns.
				Parent.JoinGrid.LJCAddDisplayColumns(joinDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private readonly ViewEditorList Parent;
		private ViewJoinManager mViewJoinManager;
		#endregion
	}
}
