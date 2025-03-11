// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TaskGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Task Grid control.
	internal class TaskGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal TaskGridCode(TaskSourceModule parent)
		{
			mParent = parent;
			mTaskGrid = mParent.TaskGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveTask()
		{
			StepTasks records;

			mParent.Cursor = Cursors.WaitCursor;
			mTaskGrid.LJCRowsClear();

			if (mParent.StepCombo.SelectedIndex > -1)
			{
				// Data from items.
				int parentID = mParent.StepCombo.LJCSelectedItemID();

				StepTaskManager taskManager = mManagers.StepTaskManager;
				records = taskManager.LoadWithStepID(parentID);

				if (NetCommon.HasItems(records))
				{
					foreach (StepTask record in records)
					{
						RowAddTask(record);
					}
				}
			}
			mParent.DoChange(TaskSourceModule.Change.Task);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddTask(StepTask dataRecord)
		{
			LJCGridRow retValue;

			retValue = mTaskGrid.LJCRowAdd();
			SetStoredValuesTask(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mTaskGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateTask(StepTask dataRecord)
		{
			if (mTaskGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesTask(row, dataRecord);
				row.LJCSetValues(mTaskGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesTask(LJCGridRow row, StepTask dataRecord)
		{
			row.LJCSetInt32(StepTask.ColumnStepTaskID, dataRecord.StepTaskID);

			// if has child records.
			row.LJCSetString(StepTask.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectTask(StepTask dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mTaskGrid.Rows)
				{
					rowID = row.LJCGetInt32(StepTask.ColumnStepTaskID);
					if (rowID == dataRecord.StepTaskID)
					{
						mTaskGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewTask()
		{
			// If child grid.
			if (mParent.StepCombo.SelectedIndex > -1)
			{
				// Data from parent window or list.
				int parentID = mParent.StepCombo.LJCSelectedItemID();
				string parentName = mParent.StepCombo.Text;

				TaskDetail detail = new TaskDetail()
				{
					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName,
				};
				detail.LJCChange += TaskDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditTask()
		{
			// If child grid.
			if (mParent.StepCombo.SelectedIndex > -1
				&& mTaskGrid.CurrentRow is LJCGridRow row)
			{
				//  // Data from parent window or list.
				int parentID = mParent.StepCombo.LJCSelectedItemID();
				string parentName = mParent.StepCombo.Text;

				TaskDetail detail = new TaskDetail()
				{
					LJCID = row.LJCGetInt32(StepTask.ColumnStepTaskID),

					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName
				};
				detail.LJCChange += TaskDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void TaskDetail_LJCChange(object sender, EventArgs e)
		{
			TaskDetail detail;
			LJCGridRow row;

			detail = sender as TaskDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateTask(detail.LJCRecord);
			}
			else
			{
				row = RowAddTask(detail.LJCRecord);
				mTaskGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(TaskSourceModule.Change.Task);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteTask()
		{
			string title;

			if (mTaskGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				if (MessageBox.Show(FormCommon.DeleteConfirm, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = row.LJCGetInt32(StepTask.ColumnStepTaskID);
					StepTaskManager taskManager = mManagers.StepTaskManager;
					var keyColumns = taskManager.GetIDKey(id);
					taskManager.Delete(keyColumns);
					if (taskManager.AffectedCount > 0)
					{
						mTaskGrid.Rows.Remove(row);
						mParent.TimedChange(TaskSourceModule.Change.Task);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshTask()
		{
			StepTask record;
			int id = 0;

			if (mTaskGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(StepTask.ColumnStepTaskID);
			}
			DataRetrieveTask();

			// Select the original row.
			if (id > 0)
			{
				record = new StepTask()
				{
					StepTaskID = id
				};
				RowSelectTask(record);
			}
		}
		#endregion

		#region Class Data

		private readonly TaskSourceModule mParent;
		private readonly LJCDataGrid mTaskGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
