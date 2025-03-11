// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSourceGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the DataSource Grid control.
	internal class DataSourceGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal DataSourceGridCode(DataSourceList parent)
		{
			mParent = parent;
			mDataSourceGrid = mParent.DataSourceGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveDataSource()
		{
			DataSources records;

			mParent.Cursor = Cursors.WaitCursor;
			mDataSourceGrid.LJCRowsClear();

			var dataSourceManager = mManagers.DataSourceManager;
			if (mParent.LJCParentID > 0)
			{
				// This is a Selection List.
				mParent.Text = "Data Source Selection";
				//records = mDataSourceManager.LoadNotInStepTask(LJCParentID);
				records = dataSourceManager.Load();
			}
			else
			{
				// This is a display list.
				mParent.Text = "Data Source List";
				records = dataSourceManager.Load();
			}

			if (NetCommon.HasItems(records))
			{
				foreach (DataSource record in records)
				{
					RowAddDataSource(record);
				}
			}
			mParent.DoChange(DataSourceList.Change.DataSource);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddDataSource(DataSource dataRecord)
		{
			LJCGridRow retValue;

			retValue = mDataSourceGrid.LJCRowAdd();
			SetStoredValuesDataProcess(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(mDataSourceGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateDataSource(DataSource dataRecord)
		{
			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesDataProcess(row, dataRecord);
				row.LJCSetValues(mDataSourceGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesDataProcess(LJCGridRow row, DataSource dataRecord)
		{
			row.LJCSetInt32(DataSource.ColumnDataSourceID, dataRecord.DataSourceID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectDataSource(DataSource dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mDataSourceGrid.Rows)
				{
					rowID = row.LJCGetInt32(DataProcess.ColumnDataProcessID);
					if (rowID == dataRecord.DataSourceID)
					{
						mDataSourceGrid.LJCSetCurrentRow(row, true);
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
		internal void DoDefaultDataSource()
		{
			if (mParent.LJCIsSelect)
			{
				DoSelectDataSource();
			}
			else
			{
				DoEditDataSource();
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNewDataSource()
		{
			DataSourceDetail detail;

			detail = new DataSourceDetail()
			{
			};
			detail.LJCChange += DataSourceDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditDataSource()
		{
			DataSourceDetail detail;

			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				detail = new DataSourceDetail()
				{
					LJCID = row.LJCGetInt32(DataSource.ColumnDataSourceID),
				};
				detail.LJCChange += DataSourceDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void DataSourceDetail_LJCChange(object sender, EventArgs e)
		{
			DataSourceDetail detail;
			LJCGridRow row;

			detail = sender as DataSourceDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateDataSource(detail.LJCRecord);
			}
			else
			{
				row = RowAddDataSource(detail.LJCRecord);
				mDataSourceGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(DataSourceList.Change.DataSource);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteDataSource()
		{
			string title;
			string message;

			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = row.LJCGetInt32(DataSource.ColumnDataSourceID);
					var dataSourceManager = mManagers.DataSourceManager;
					var keyColumns = dataSourceManager.GetIDKey(id);
					dataSourceManager.Delete(keyColumns);
					if (dataSourceManager.AffectedCount > 0)
					{
						mDataSourceGrid.Rows.Remove(row);
						mParent.TimedChange(DataSourceList.Change.DataSource);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshDataSource()
		{
			DataSource record;
			int id = 0;

			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DataSource.ColumnDataSourceID);
			}
			DataRetrieveDataSource();

			// Select the original row.
			if (id > 0)
			{
				record = new DataSource()
				{
					DataSourceID = id
				};
				RowSelectDataSource(record);
			}
		}

		// Sets the selected item and returns to the parent form.
		internal void DoSelectDataSource()
		{
			DataSource record;
			int id;

			mParent.Cursor = Cursors.WaitCursor;
			mParent.LJCSelectedRecord = null;
			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(DataSource.ColumnDataSourceID);
				var dataSourceManager = mManagers.DataSourceManager;
				var keyColumns = dataSourceManager.GetIDKey(id);
				record = dataSourceManager.Retrieve(keyColumns);
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

		private readonly DataSourceList mParent;
		private readonly LJCDataGrid mDataSourceGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
