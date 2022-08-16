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
	// Provides JoinOnGrid methods for the ViewEditorList window.
	internal class JoinOnGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal JoinOnGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewJoinOnManager = Parent.ViewHelper.ViewJoinOnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveJoinOn()
		{
			ViewJoinOns dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.JoinOnGrid.Rows.Clear();

			ConfigureJoinOnGrid();
			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewJoinID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

				dataRecords = mViewJoinOnManager.LoadWithParentID(viewJoinID);
				if (dataRecords != null && dataRecords.Count > 0)
				{
					foreach (ViewJoinOn dataRecord in dataRecords)
					{
						RowAddJoinOn(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.JoinOn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddJoinOn(ViewJoinOn dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.JoinOnGrid.LJCRowAdd();
			SetStoredValuesJoinOn(retValue, dataRecord);

			// Sets the row values from a data object.
			Parent.JoinOnGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateJoinOn(ViewJoinOn dataRecord)
		{
			if (Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesJoinOn(row, dataRecord);
				Parent.JoinOnGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesJoinOn(LJCGridRow row, ViewJoinOn dataRecord)
		{
			row.LJCSetInt32(ViewJoinOn.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelectViewJoinOn(ViewJoinOn record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.JoinOnGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewJoinOn.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						Parent.JoinOnGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				Parent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewViewJoinOn()
		{
			ViewJoinOnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinOnDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinOnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditViewJoinOn()
		{
			ViewJoinOnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewJoinOn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinOnDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinOnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void JoinOnDetail_Change(object sender, EventArgs e)
		{
			ViewJoinOnDetail detail;
			ViewJoinOn record;
			LJCGridRow row;

			detail = sender as ViewJoinOnDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateJoinOn(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddJoinOn(record);
				Parent.JoinOnGrid.LJCSetCurrentRow(row, true);
				Parent.TimedChange(ViewEditorList.Change.JoinOn);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteViewJoinOn()
		{
			LJCGridRow row;
			string title;
			string message;

			row = Parent.JoinOnGrid.CurrentRow as LJCGridRow;
			if (row != null)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewJoinOn.ColumnID, row.LJCGetInt32(ViewJoinOn.ColumnID) }
					};
					mViewJoinOnManager.Delete(keyColumns);
					if (mViewJoinOnManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.JoinOnGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.JoinOn);
					}
				}
			}
		}

		// Refreshes the list.
		private void DoRefreshViewJoinOn()
		{
			ViewJoinOn record;
			int id = 0;

			if (Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoinOn.ColumnID);
			}
			DataRetrieveJoinOn();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewJoinOn()
				{
					ID = id
				};
				RowSelectViewJoinOn(record);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the View JoinOn Grid.
		private void ConfigureJoinOnGrid()
		{
			if (0 == Parent.JoinOnGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoinOn.ColumnFromColumnName,
					ViewJoinOn.ColumnJoinOnOperator,
					ViewJoinOn.ColumnToColumnName
				};

				// Get the display columns from the manager Data Definition.
				DbColumns joinOnDisplayColumns
					= mViewJoinOnManager.GetColumns(propertyNames);

				// Setup the grid display columns.
				Parent.JoinOnGrid.LJCAddDisplayColumns(joinOnDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private ViewEditorList Parent;
		private ViewJoinOnManager mViewJoinOnManager;
		#endregion
	}
}
