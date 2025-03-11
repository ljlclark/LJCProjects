// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Source Grid control.
	internal class SourceGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal SourceGridCode(TaskSourceModule parent)
		{
			mParent = parent;
			mTaskGrid = mParent.TaskGrid;
			mDataSourceGrid = mParent.DataSourceGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveDataSource()
		{
			DataSources records;

			mParent.Cursor = Cursors.WaitCursor;
			mDataSourceGrid.LJCRowsClear();

			if (mTaskGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);

				DataSourceManager dataSourceManager = mManagers.DataSourceManager;
				records = dataSourceManager.LoadWithTaskID(parentID);

				if (NetCommon.HasItems(records))
				{
					foreach (DataSource record in records)
					{
						RowAddDataSource(record);
					}
				}
			}
			mParent.DoChange(TaskSourceModule.Change.DataSource);
			mParent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddDataSource(DataSource dataRecord)
		{
			LJCGridRow retValue;

			retValue = mDataSourceGrid.LJCRowAdd();
			SetStoredValuesDataSource(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mDataSourceGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateDataSource(DataSource dataRecord)
		{
			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesDataSource(row, dataRecord);
				row.LJCSetValues(mDataSourceGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesDataSource(LJCGridRow row, DataSource dataRecord)
		{
			row.LJCSetInt32(DataSource.ColumnDataSourceID, dataRecord.DataSourceID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectDataSource(DataSource dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mDataSourceGrid.Rows)
				{
					rowID = row.LJCGetInt32(StepTask.ColumnStepTaskID);
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

		// Displays a detail dialog for a new record.
		internal void DoAddDataSource()
		{
			DataSource dataSource;

			if (mTaskGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
				string parentName = parentRow.LJCGetString(StepTask.ColumnName);
				dataSource = GetCurrentRowDataSource();

				DataSourceList list = new DataSourceList()
				{
					LJCIsSelect = true,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCSelectedRecord = dataSource
				};
				list.ShowDialog();

				if (DialogResult.OK == list.DialogResult
					&& list.LJCSelectedRecord != null)
				{
					DoAssociatedData(list);
				}
			}
		}

		// Saves the parent associated data.
		internal void DoAssociatedData(DataSourceList list)
		{
			DataSource dataSource = list.LJCSelectedRecord;

			// Add associated record.
			TaskSource taskSource = new TaskSource()
			{
				StepTaskID = list.LJCParentID,
				DataSourceID = dataSource.DataSourceID
			};
			TaskSourceManager taskSourceManager = mManagers.TaskSourceManager;
			taskSourceManager.Add(taskSource);
			if (taskSourceManager.AffectedCount > 0)
			{
				LJCGridRow addedRow = RowAddDataSource(dataSource);
				mDataSourceGrid.LJCSetCurrentRow(addedRow);
				mParent.TimedChange(TaskSourceModule.Change.DataSource);
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditDataSource()
		{
			DataSourceDetail detail;

			if (mTaskGrid.CurrentRow is LJCGridRow parentRow
				&& mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
				string parentName = parentRow.LJCGetString(StepTask.ColumnName);

				detail = new DataSourceDetail()
				{
					LJCID = row.LJCGetInt32(DataSource.ColumnDataSourceID),

					// Data from items.
					LJCParentID = parentID,
					LJCParentName = parentName
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
				mParent.TimedChange(TaskSourceModule.Change.DataSource);
			}
		}

		// Deletes the selected row.
		internal void DoRemoveDataSource()
		{
			string title;
			string message;

			if (mTaskGrid.CurrentRow is LJCGridRow parentRow
				&& mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				title = "Remove Confirmation";
				message = FormCommon.DeleteConfirm.Replace("delete", "remove");
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int parentID = parentRow.LJCGetInt32(StepTask.ColumnStepTaskID);
					int id = row.LJCGetInt32(DataSource.ColumnDataSourceID);

					var keyColumns = new DbColumns()
					{
						{ TaskSource.ColumnStepTaskID, parentID },
						{ TaskSource.ColumnDataSourceID, id }
					};
					TaskSourceManager taskSourceManager = mManagers.TaskSourceManager;
					taskSourceManager.Delete(keyColumns);
					if (taskSourceManager.AffectedCount > 0)
					{
						mDataSourceGrid.Rows.Remove(row);
						mParent.TimedChange(TaskSourceModule.Change.DataSource);
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
		#endregion

		#region DataRow Methods

		// Retrieves the current row DataSource record.
		internal DataSource GetCurrentRowDataSource()
		{
			DataSource retValue = null;

			if (mDataSourceGrid.CurrentRow is LJCGridRow row)
			{
				retValue = GetRowDataSource(row);
			}
			return retValue;
		}

		// Retrieves the DataSource record.
		internal DataSource GetRowDataSource(LJCGridRow row)
		{
			DataSource retValue = null;

			int id = row.LJCGetInt32(DataSource.ColumnDataSourceID);
			if (id > 0)
			{
				DataSourceManager dataSourceManager = mManagers.DataSourceManager;
				retValue = dataSourceManager.RetrieveWithID(id);
			}
			return retValue;
		}
		#endregion

		#region Class Data

		private readonly TaskSourceModule mParent;
		private readonly LJCDataGrid mTaskGrid;
		private readonly LJCDataGrid mDataSourceGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
