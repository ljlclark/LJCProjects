// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformGridCode.cs
using System;
using LJCWinFormControls;
using LJCDataTransformDAL;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Transform Grid control.
	internal class TransformGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal TransformGridCode(TaskSourceModule parent)
		{
			mParent = parent;
			mTaskGrid = mParent.TaskGrid;
			mTransformGrid = mParent.TransformGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveTransform()
		{
			TaskTransformManager taskTransformManager;
			TaskTransforms records;

			mParent.Cursor = Cursors.WaitCursor;
			mTransformGrid.LJCRowsClear();

			// if is child grid.
			if (mTaskGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
				taskTransformManager = mManagers.TaskTransformManager;
				records = taskTransformManager.LoadWithTaskID(parentID);

				if (NetCommon.HasItems(records))
				{
					foreach (TaskTransform record in records)
					{
						RowAddTransform(record);
					}
				}
			}
			mParent.DoChange(TaskSourceModule.Change.TaskTransform);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddTransform(TaskTransform dataRecord)
		{
			LJCGridRow retValue;

			retValue = mTransformGrid.LJCRowAdd();
			SetStoredValuesTransform(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mTransformGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateTransform(TaskTransform dataRecord)
		{
			if (mTransformGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesTransform(row, dataRecord);
				row.LJCSetValues(mTransformGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesTransform(LJCGridRow row
			, TaskTransform dataRecord)
		{
			row.LJCSetInt32(TaskTransform.ColumnTransformID, dataRecord.TransformID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectTransform(TaskTransform dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mTransformGrid.Rows)
				{
					rowID = row.LJCGetInt32(TaskTransform.ColumnTransformID);
					if (rowID == dataRecord.TransformID)
					{
						mTransformGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewTransform()
		{
			// If child grid.
			if (mTaskGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from parent window or list.
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
				string parentName = parentRow.LJCGetString(StepTask.ColumnName);

				TransformDetail detail = new TransformDetail()
				{
					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName,
				};
				detail.LJCChange += TransformDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditTransform()
		{
			// If child grid.
			if (mTaskGrid.CurrentRow is LJCGridRow parentRow
				&& mTransformGrid.CurrentRow is LJCGridRow row)
			{
				// Data from parent window or list.
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
				string parentName = parentRow.LJCGetString(StepTask.ColumnName);

				TransformDetail detail = new TransformDetail()
				{
					LJCID = row.LJCGetInt32(TaskTransform.ColumnTransformID),

					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName
				};
				detail.LJCChange += TransformDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void TransformDetail_LJCChange(object sender, EventArgs e)
		{
			LJCGridRow row;

			TransformDetail detail = sender as TransformDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateTransform(detail.LJCRecord);
			}
			else
			{
				row = RowAddTransform(detail.LJCRecord);
				mTransformGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(TaskSourceModule.Change.TaskTransform);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteTransform()
		{
			TaskTransformManager taskTransformManager;
			string title;

			if (mTransformGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				if (MessageBox.Show(FormCommon.DeleteConfirm, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ TaskTransform.ColumnTransformID
							, row.LJCGetInt32(TaskTransform.ColumnTransformID) }
					};
					taskTransformManager = mManagers.TaskTransformManager;
					taskTransformManager.Delete(keyColumns);
					if (taskTransformManager.AffectedCount > 0)
					{
						mTransformGrid.Rows.Remove(row);
						mParent.TimedChange(TaskSourceModule.Change.TaskTransform);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshTransform()
		{
			TaskTransform record;
			int id = 0;

			if (mTransformGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(TaskTransform.ColumnTransformID);
			}
			DataRetrieveTransform();

			// Select the original row.
			if (id > 0)
			{
				record = new TaskTransform()
				{
					TransformID = id
				};
				RowSelectTransform(record);
			}
		}
		#endregion

		#region Class Data

		private readonly TaskSourceModule mParent;
		private readonly LJCDataGrid mTaskGrid;
		private readonly LJCDataGrid mTransformGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
