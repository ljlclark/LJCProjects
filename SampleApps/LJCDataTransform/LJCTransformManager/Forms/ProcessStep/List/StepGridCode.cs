// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StepGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Step Grid control.
	internal class StepGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal StepGridCode(StepList parent)
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

			if (mParent.LJCParentID > 0)
			{
				var stepManager = mManagers.StepManager;
				records = stepManager.LoadWithProcessID(mParent.LJCParentID);

				if (NetCommon.HasItems(records))
				{
					foreach (Step record in records)
					{
						RowAddStep(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(StepList.Change.Step);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddStep(Step dataRecord)
		{
			LJCGridRow retValue;

			retValue = mStepGrid.LJCRowAdd();
			SetStoredValuesStep(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mStepGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateStep(Step dataRecord)
		{
			if (mStepGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesStep(row, dataRecord);
				row.LJCSetValues(mStepGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesStep(LJCGridRow row
			, Step dataRecord)
		{
			row.LJCSetInt32(Step.ColumnStepID, dataRecord.StepID);

			// Save Parent name.
			//row.LJCSetString(_ClassName_.ColumnName, record.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectStep(Step dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
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
				mParent.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Performs the default list action.
		internal void DoDefaultStep()
		{
			if (mParent.LJCIsSelect)
			{
				DoSelectStep();
			}
			else
			{
				DoEditStep();
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNewStep()
		{
			StepDetail detail;

			if (mParent.LJCParentID > 0)
			{
				detail = new StepDetail()
				{
					LJCParentID = mParent.LJCParentID,
					LJCParentName = mParent.LJCParentName
				};
				detail.LJCChange += StepDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditStep()
		{
			StepDetail detail;

			if (mParent.LJCParentID > 0
				&& mStepGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(Step.ColumnStepID);

				detail = new StepDetail()
				{
					LJCID = id,
					LJCParentID = mParent.LJCParentID,
					LJCParentName = mParent.LJCParentName
				};
				detail.LJCChange += StepDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void StepDetail_Change(object sender, EventArgs e)
		{
			StepDetail detail;
			Step record;
			LJCGridRow row;

			detail = sender as StepDetail;
			if (detail.LJCRecord != null)
			{
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
					mParent.TimedChange(StepList.Change.Step);
				}
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
					// Data from items.
					int id = row.LJCGetInt32(Step.ColumnStepID);

					var stepManager = mManagers.StepManager;
					var keyColumns = stepManager.GetIDKey(id);
					stepManager.Delete(keyColumns);
					if (stepManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mStepGrid.Rows.Remove(row);
						mParent.TimedChange(StepList.Change.Step);
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

		// Sets the selected item and returns to the parent form.
		internal void DoSelectStep()
		{
			Step record;
			int id;

			mParent.LJCSelectedRecord = null;
			if (mStepGrid.CurrentRow is LJCGridRow row)
			{
				mParent.Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt32(Step.ColumnStepID);

				var stepManager = mManagers.StepManager;
				var keyColumns = stepManager.GetIDKey(id);
				record = stepManager.Retrieve(keyColumns);
				if (record != null)
				{
					mParent.LJCSelectedRecord = record;
				}
				mParent.Cursor = Cursors.Default;
				mParent.DialogResult = DialogResult.OK;
			}
		}
		#endregion

		#region Class Data

		private readonly StepList mParent;
		private readonly LJCDataGrid mStepGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
