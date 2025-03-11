// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleStepGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Step Grid control.
	internal class ModuleStepGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ModuleStepGridCode(ProcessStepModule parent)
		{
			mParent = parent;
			mStepGrid = mParent.StepGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveStep()
		{
			Steps records;

			mParent.Cursor = Cursors.WaitCursor;
			mStepGrid.LJCRowsClear();

			if (mParent.DataProcessCombo.SelectedIndex > -1)
			{
				// Data from list items.
				int parentID = mParent.DataProcessCombo.LJCSelectedItemID();

				var stepManager = mManagers.StepManager;
				records = stepManager.LoadWithProcessID(parentID);

				if (NetCommon.HasItems(records))
				{
					foreach (Step record in records)
					{
						RowAddStep(record);
					}
				}
			}
			mParent.DoChange(ProcessStepModule.Change.Step);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddStep(Step dataRecord)
		{
			LJCGridRow retValue;

			retValue = mStepGrid.LJCRowAdd();
			SetStoredValuesStep(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mStepGrid, dataRecord);
			SetStatusValuesStep(retValue, dataRecord.StatusID);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateStep(Step dataRecord)
		{
			if (mStepGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesStep(row, dataRecord);
				row.LJCSetValues(mStepGrid, dataRecord);
				SetStatusValuesStep(row, dataRecord.StatusID);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesStep(LJCGridRow row, Step dataRecord)
		{
			row.LJCSetInt32(Step.ColumnStepID, dataRecord.StepID);
			row.LJCSetInt32(Step.ColumnStatusID, dataRecord.StatusID);

			// if has child records.
			row.LJCSetString(Step.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectStep(Step dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mStepGrid.Rows)
				{
					rowID = row.LJCGetInt32(Step.ColumnStepID);
					if (rowID == dataRecord.StepID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mStepGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			return retValue;
		}

		// Refresh the statuses.
		internal void RefreshStatusStep()
		{
			foreach (LJCGridRow row in mStepGrid.Rows)
			{
				int statusID = row.LJCGetInt32(Step.ColumnStatusID);
				int id = row.LJCGetInt32(Step.ColumnStepID);
				Step step = mManagers.StepManager.RetrieveWithID(id);
				if (step != null)
				{
					// Record status is greater than Active and row status is not updated.
					if (step.StatusID > (int)StepTaskStatus.Active
						&& statusID != step.StatusID)
					{
						//WriteLogLine("RefreshStatusStep: StepName:{0}, RowStatusID:{1}, StepStatusID:{2}"
						//	, step.Name, statusID, step.StatusID);
						RowSelectStep(step);
						row.LJCSetInt32(Step.ColumnStatusID, step.StatusID);
					}
					SetStatusValuesStep(row, step.StatusID);
				}
			}
			//RefreshStatusTask();
		}

		// Set text and color.
		private void SetStatusValuesStep(LJCGridRow row, short statusID)
		{
			TaskStatus taskStatus;
			string statusColumnName = Step.ColumnStatusID;

			taskStatus = mManagers.TaskStatusManager.RetrieveWithID(statusID);
			row.LJCSetCellText(statusColumnName, taskStatus.Name);

			if (mStepGrid.Columns.Contains(statusColumnName))
			{
				DataGridViewCell cell = row.Cells[statusColumnName];
				//WriteLogLine("SetStatusValuesStep: CellValue:{0}", cell.Value.ToString());
				TransformCommon.SetStatusColor(cell, statusID);
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewStep()
		{
			StepDetail detail;

			// If child grid.
			if (mParent.DataProcessCombo.SelectedIndex > -1)
			{
				// Data from parent window or list.
				int parentID = mParent.DataProcessCombo.LJCSelectedItemID();
				string parentName = mParent.DataProcessCombo.Text;

				detail = new StepDetail()
				{
					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName
				};
				detail.LJCChange += StepDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditStep()
		{
			StepDetail detail;

			if (mParent.DataProcessCombo.SelectedIndex > -1
				&& mStepGrid.CurrentRow is LJCGridRow row)
			{
				// Data from parent window or list.
				int parentID = mParent.DataProcessCombo.LJCSelectedItemID();
				string parentName = mParent.DataProcessCombo.Text;

				detail = new StepDetail()
				{
					LJCID = row.LJCGetInt32(Step.ColumnStepID),

					// Data from parent window or list.
					LJCParentID = parentID,
					LJCParentName = parentName,
				};
				detail.LJCChange += StepDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void StepDetail_LJCChange(object sender, EventArgs e)
		{
			StepDetail detail;
			Step record;
			LJCGridRow row;

			detail = sender as StepDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateStep(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddStep(record);
				mStepGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(ProcessStepModule.Change.Step);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteStep()
		{
			string title;
			string message;

			if (mStepGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = row.LJCGetInt32(Step.ColumnStepID);
					var stepManager = mManagers.StepManager;
					var keyColumns = stepManager.GetIDKey(id);
					stepManager.Delete(keyColumns);
					if (stepManager.AffectedCount > 0)
					{
						mStepGrid.Rows.Remove(row);
						mParent.TimedChange(ProcessStepModule.Change.Step);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshStep()
		{
			Step record;
			int id = 0;

			if (mStepGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Step.ColumnStepID);
			}
			DataRetrieveStep();

			// Select the original row.
			if (id > 0)
			{
				record = new Step()
				{
					StepID = id
				};
				RowSelectStep(record);
			}
		}
		#endregion

		#region Class Data

		private readonly ProcessStepModule mParent;
		private readonly LJCDataGrid mStepGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
