// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataProcessGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the DataProcess Grid control.
	internal class DataProcessGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal DataProcessGridCode(DataProcessList parent)
		{
			mParent = parent;
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

			var dataProcessManager = mManagers.DataProcessManager;
			if (mParent.LJCParentID > 0)
			{
				// This is a Selection List.
				mParent.Text = "Data Process Selection";
				records = dataProcessManager.LoadNotInGroup();
			}
			else
			{
				// This is a display list.
				mParent.Text = "Data Process List";
				records = dataProcessManager.Load();
			}

			if (NetCommon.HasItems(records))
			{
				foreach (DataProcess record in records)
				{
					RowAddDataProcess(record);
				}
			}
			mParent.DoChange(DataProcessList.Change.DataProcess);
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
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateDataProcess(DataProcess dataRecord)
		{
			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesDataProcess(row, dataRecord);
				row.LJCSetValues(mDataProcessGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesDataProcess(LJCGridRow row, DataProcess dataRecord)
		{
			row.LJCSetInt32(DataProcess.ColumnDataProcessID, dataRecord.DataProcessID);
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
		#endregion

		#region Action Methods

		// Performs the default list action.
		internal void DoDefaultDataProcess()
		{
			if (mParent.LJCIsSelect)
			{
				DoSelectDataProcess();
			}
			else
			{
				DoEditDataProcess();
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNewDataProcess()
		{
			DataProcessDetail detail;

			detail = new DataProcessDetail()
			{
			};
			detail.LJCChange += DataProcessDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditDataProcess()
		{
			DataProcessDetail detail;

			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				detail = new DataProcessDetail()
				{
					LJCID = row.LJCGetInt32(DataProcess.ColumnDataProcessID),
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
				mParent.TimedChange(DataProcessList.Change.DataProcess);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteDataProcess()
		{
			string title;

			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				if (MessageBox.Show(FormCommon.DeleteConfirm, title
					, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var dataProcessID = row.LJCGetInt32(DataProcess.ColumnDataProcessID);

					var dataProcessManager = mManagers.DataProcessManager;
					var keyColumns = dataProcessManager.GetIDKey(dataProcessID);
					dataProcessManager.Delete(keyColumns);
					mDataProcessGrid.Rows.Remove(row);
					mParent.TimedChange(DataProcessList.Change.DataProcess);
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

		// Sets the selected item and returns to the parent form.
		internal void DoSelectDataProcess()
		{
			DataProcess record;
			int id;

			mParent.Cursor = Cursors.WaitCursor;
			mParent.LJCSelectedRecord = null;
			if (mDataProcessGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
				var keyColumns = mManagers.DataProcessManager.GetIDKey(id);
				record = mManagers.DataProcessManager.Retrieve(keyColumns);
				if (record != null)
				{
					mParent.LJCSelectedRecord = record;
				}
			}
			mParent.SaveControlValues();
			mParent.Cursor = Cursors.Default;
			mParent.DialogResult = DialogResult.OK;
		}
		#endregion

		#region Class Data

		private readonly DataProcessList mParent;
		private readonly LJCDataGrid mDataProcessGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
