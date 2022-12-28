// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// FilterGridClass.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using LJCWinFormCommon;

namespace LJCViewEditor
{
	// Provides FilterGrid methods for the ViewEditorList window.
	internal class FilterGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal FilterGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		/// <summary>Resets the DataConfig dependent objects.</summary>
		internal void ResetData()
		{
			mViewFilterManager = Parent.ViewHelper.ViewFilterManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveFilter()
		{
			ViewFilters dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.FilterGrid.Rows.Clear();
			Parent.ConditionSetGrid.Rows.Clear();
			Parent.ConditionGrid.Rows.Clear();

			ConfigureFilterGrid();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

				dataRecords = mViewFilterManager.LoadWithParentID(viewDataID);
				if (dataRecords != null && dataRecords.Count > 0)
				{
					foreach (ViewFilter dataRecord in dataRecords)
					{
						RowAddFilter(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Filter);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddFilter(ViewFilter dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.FilterGrid.LJCRowAdd();
			SetStoredValuesFilter(retValue, dataRecord);

			// Sets the row values from a data object.
			Parent.FilterGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateFilter(ViewFilter dataRecord)
		{
			if (Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesFilter(row, dataRecord);
				Parent.FilterGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesFilter(LJCGridRow row, ViewFilter dataRecord)
		{
			row.LJCSetInt32(ViewFilter.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewFilter.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private void RowSelectViewFilter(ViewFilter record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.FilterGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewFilter.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						Parent.FilterGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				Parent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewViewFilter()
		{
			ViewFilterDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var defaultName = $"Filter{Parent.FilterGrid.Rows.Count + 1}";
				var grid = Parent.FilterGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewFilterDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCDefaultName = defaultName,
					LJCLocation = location
				};
				detail.LJCChange += FilterDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditViewFilter()
		{
			ViewFilterDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewFilter.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.FilterGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewFilterDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += FilterDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void FilterDetail_Change(object sender, EventArgs e)
		{
			ViewFilterDetail detail;
			ViewFilter record;
			LJCGridRow row;

			detail = sender as ViewFilterDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateFilter(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddFilter(record);
				Parent.FilterGrid.LJCSetCurrentRow(row, true);
				Parent.TimedChange(ViewEditorList.Change.Filter);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteViewFilter()
		{
			string title;
			string message;

			if (Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewFilter.ColumnID, row.LJCGetInt32(ViewFilter.ColumnID) }
					};
					mViewFilterManager.Delete(keyColumns);
					if (mViewFilterManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.FilterGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.Filter);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshViewFilter()
		{
			ViewFilter record;
			int id = 0;

			if (Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewFilter.ColumnID);
			}
			DataRetrieveFilter();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewFilter()
				{
					ID = id
				};
				RowSelectViewFilter(record);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the View Filter Grid.
		private void ConfigureFilterGrid()
		{
			if (0 == Parent.FilterGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewFilter.ColumnName,
					ViewFilter.ColumnBooleanOperator
				};

				// Get the display columns from the manager Data Definition.
				DbColumns filterDisplayColumns
					= mViewFilterManager.GetColumns(propertyNames);

				// Setup the grid display columns.
				Parent.FilterGrid.LJCAddDisplayColumns(filterDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private readonly ViewEditorList Parent;
		private ViewFilterManager mViewFilterManager;
		#endregion
	}
}
