// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleProcessGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCWinFormCommon;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the ProcessGroup Grid control.
	internal class ModuleProcessGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ModuleProcessGridCode(ProcessGroupModule parent)
		{
			mParent = parent;
			mProcessGroupGrid = mParent.ProcessGroupGrid;
			mDataProcessGrid = mParent.DataProcessGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveDataProcess()
		{
			DataProcesses records;

			mParent.Cursor = Cursors.WaitCursor;
			mDataProcessGrid.LJCRowsClear();

			if (mProcessGroupGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);

				var dataProcessManager = mManagers.DataProcessManager;
				records = dataProcessManager.LoadWithGroupID(parentID);

				if (NetCommon.HasItems(records))
				{
					foreach (DataProcess record in records)
					{
						RowAddDataProcess(record);
					}
				}
			}
			mParent.DoChange(ProcessGroupModule.Change.DataProcess);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddDataProcess(DataProcess dataRecord)
		{
			LJCGridRow retValue;

			retValue = mDataProcessGrid.LJCRowAdd();
			SetStoredValuesDataProcess(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mDataProcessGrid, dataRecord);
			SetStatusValues(retValue, dataRecord.ProcessStatusID);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateDataProcess(DataProcess dataRecord)
		{
			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesDataProcess(row, dataRecord);
				row.LJCSetValues(mDataProcessGrid, dataRecord);
				SetStatusValues(row, dataRecord.ProcessStatusID);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesDataProcess(LJCGridRow row
			, DataProcess dataRecord)
		{
			row.LJCSetInt32(DataProcess.ColumnDataProcessID, dataRecord.DataProcessID);

			// if has child records.
			row.LJCSetString(DataProcess.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectDataProcess(DataProcess dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mDataProcessGrid.Rows)
				{
					rowID = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
					if (rowID == dataRecord.DataProcessID)
					{
						mDataProcessGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			return retValue;
		}

		// Set text and color.
		private void SetStatusValues(LJCGridRow row, short statusID)
		{
			string statusColumnName = DataProcess.ColumnProcessStatusID;

			ProcessStatus processStatus
				= mManagers.ProcessStatusManager.RetrieveWithID(statusID);
			row.LJCSetCellText(statusColumnName, processStatus.Name);

			if (mDataProcessGrid.Columns.Contains(statusColumnName))
			{
				DataGridViewCell cell = row.Cells[statusColumnName];
				TransformCommon.SetStatusColor(cell, statusID);
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoAddDataProcess()
		{
			DataProcess dataProcess;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
				string parentName = parentRow.LJCGetString(ProcessGroup.ColumnName);

				if (mDataProcessGrid.CurrentRow is LJCGridRow row)
				{
					int id = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
					dataProcess
						= mManagers.DataProcessManager.RetrieveWithID(id);

					DataProcessList list = new DataProcessList()
					{
						LJCIsSelect = true,
						LJCParentID = parentID,
						LJCParentName = parentName,
						LJCSelectedRecord = dataProcess
					};
					list.ShowDialog();

					if (DialogResult.OK == list.DialogResult
						&& list.LJCSelectedRecord != null)
					{
						DoAssociatedData(list);
					}
				}
			}
		}

		// Saves the parent associated data.
		internal void DoAssociatedData(DataProcessList list)
		{
			DataProcess dataProcess = list.LJCSelectedRecord;
			int groupID = list.LJCParentID;
			ProcessGroupProcess lookupRecord
				= mManagers.ProcessGroupProcessManager.GetMaxSequence(groupID);
			int sequence = lookupRecord.Sequence + 1;

			// Add associated record.
			ProcessGroupProcess processGroupProcess = new ProcessGroupProcess()
			{
				ProcessGroupID = list.LJCParentID,
				DataProcessID = dataProcess.DataProcessID,
				Sequence = sequence
			};
			var processGroupProcessManager = mManagers.ProcessGroupProcessManager;
			processGroupProcessManager.Add(processGroupProcess);
			if (processGroupProcessManager.AffectedCount > 0)
			{
				LJCGridRow addedRow = RowAddDataProcess(dataProcess);
				mDataProcessGrid.LJCSetCurrentRow(addedRow);
				mParent.TimedChange(ProcessGroupModule.Change.DataProcess);
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditDataProcess()
		{
			DataProcessDetail detail;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow parentRow
				&& mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
				string parentName = parentRow.LJCGetString(ProcessGroup.ColumnName);

				detail = new DataProcessDetail()
				{
					LJCID = row.LJCGetInt32(DataProcess.ColumnDataProcessID),

					// Data from items.
					LJCParentID = parentID,
					LJCParentName = parentName
				};
				detail.LJCChange += DataProcessDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void DataProcessDetail_LJCChange(object sender, EventArgs e)
		{
			DataProcessDetail detail;
			LJCGridRow row;

			detail = sender as DataProcessDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateDataProcess(detail.LJCRecord);
			}
			else
			{
				row = RowAddDataProcess(detail.LJCRecord);
				mDataProcessGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(ProcessGroupModule.Change.DataProcess);
			}
		}

		// Deletes the selected row.
		internal void DoRemoveDataProcess()
		{
			string title;
			string message;

			if (mProcessGroupGrid.CurrentRow is LJCGridRow parentRow
				&& mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				title = "Remove Confirmation";
				message = FormCommon.DeleteConfirm.Replace("delete", "remove");
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int parentID = parentRow.LJCGetInt32(ProcessGroup.ColumnProcessGroupID);
					int childID = row.LJCGetInt32(DataProcess.ColumnDataProcessID);

					var keyColumns = new DbColumns()
					{
						{ ProcessGroupProcess.ColumnProcessGroupID, parentID },
						{ ProcessGroupProcess.ColumnDataProcessID, childID }
					};
					var processGroupProcessManager = mManagers.ProcessGroupProcessManager;
					processGroupProcessManager.Delete(keyColumns);
					if (processGroupProcessManager.AffectedCount > 0)
					{
						mDataProcessGrid.Rows.Remove(row);
						mParent.TimedChange(ProcessGroupModule.Change.DataProcess);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshDataProcess()
		{
			DataProcess record;
			int id = 0;

			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
			}
			DataRetrieveDataProcess();

			// Select the original row.
			if (id > 0)
			{
				record = new DataProcess()
				{
					DataProcessID = id
				};
				RowSelectDataProcess(record);
			}
		}
		#endregion

		#region Class Data

		private readonly ProcessGroupModule mParent;
		private readonly LJCDataGrid mProcessGroupGrid;
		private readonly LJCDataGrid mDataProcessGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
