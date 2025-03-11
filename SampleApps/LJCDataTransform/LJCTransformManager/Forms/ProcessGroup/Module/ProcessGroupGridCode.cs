// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCWinFormCommon;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the ProcessGroup Grid control.
	internal class ProcessGroupGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ProcessGroupGridCode(ProcessGroupModule parent)
		{
			mParent = parent;
			mProcessGroupGrid = mParent.ProcessGroupGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveProcessGroup()
		{
			ProcessGroups records;

			mParent.Cursor = Cursors.WaitCursor;
			mProcessGroupGrid.LJCRowsClear();

			var processGroupManager = mManagers.ProcessGroupManager;
			records = processGroupManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (ProcessGroup record in records)
				{
					RowAddProcessGroup(record);
				}
			}
			mParent.DoChange(ProcessGroupModule.Change.ProcessGroup);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddProcessGroup(ProcessGroup dataRecord)
		{
			LJCGridRow retValue;

			retValue = mProcessGroupGrid.LJCRowAdd();
			SetStoredValuesProcessGroup(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mProcessGroupGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateProcessGroup(ProcessGroup dataRecord)
		{
			if (mProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesProcessGroup(row, dataRecord);
				row.LJCSetValues(mProcessGroupGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesProcessGroup(LJCGridRow row
			, ProcessGroup dataRecord)
		{
			row.LJCSetInt32(ProcessGroup.ColumnProcessGroupID, dataRecord.ProcessGroupID);

			// if has child records.
			row.LJCSetString(ProcessGroup.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectProcessGroup(ProcessGroup dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mProcessGroupGrid.Rows)
				{
					rowID = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
					if (rowID == dataRecord.ProcessGroupID)
					{
						mProcessGroupGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewProcessGroup()
		{
			ProcessGroupDetail detail;

			detail = new ProcessGroupDetail()
			{
			};
			detail.LJCChange += ProcessGroupDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditProcessGroup()
		{
			ProcessGroupDetail detail;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				detail = new ProcessGroupDetail()
				{
					LJCID = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID),
				};
				detail.LJCChange += ProcessGroupDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void ProcessGroupDetail_LJCChange(object sender, EventArgs e)
		{
			ProcessGroupDetail detail;
			LJCGridRow row;

			detail = sender as ProcessGroupDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateProcessGroup(detail.LJCRecord);
			}
			else
			{
				row = RowAddProcessGroup(detail.LJCRecord);
				mProcessGroupGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(ProcessGroupModule.Change.ProcessGroup);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteProcessGroup()
		{
			string title;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				if (MessageBox.Show(FormCommon.DeleteConfirm, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
					var processGroupManager = mManagers.ProcessGroupManager;
					var keyColumns = processGroupManager.GetIDKey(id);
					processGroupManager.Delete(keyColumns);
					if (processGroupManager.AffectedCount > 0)
					{
						mProcessGroupGrid.Rows.Remove(row);
						mParent.TimedChange(ProcessGroupModule.Change.ProcessGroup);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshProcessGroup()
		{
			ProcessGroup record;
			int id = 0;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
			}
			DataRetrieveProcessGroup();

			// Select the original row.
			if (id > 0)
			{
				record = new ProcessGroup()
				{
					ProcessGroupID = id
				};
				RowSelectProcessGroup(record);
			}
		}
		#endregion

		#region Class Data

		private readonly ProcessGroupModule mParent;
		private readonly LJCDataGrid mProcessGroupGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
