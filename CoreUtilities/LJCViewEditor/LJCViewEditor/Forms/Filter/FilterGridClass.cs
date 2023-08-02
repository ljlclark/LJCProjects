// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FilterGridClass.cs
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
		internal void DataRetrieve()
		{
			ViewFilters dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.FilterGrid.Rows.Clear();
			Parent.ConditionSetGrid.Rows.Clear();
			Parent.ConditionGrid.Rows.Clear();

			SetupGridFilter();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

				dataRecords = mViewFilterManager.LoadWithParentID(viewDataID);
				if (NetCommon.HasItems(dataRecords))
				{
					foreach (ViewFilter dataRecord in dataRecords)
					{
						RowAdd(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Filter);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewFilter dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.FilterGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.FilterGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(ViewFilter dataRecord)
		{
			if (Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.FilterGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewFilter dataRecord)
		{
			row.LJCSetInt32(ViewFilter.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewFilter.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewFilter dataRecord)
		{
			int rowID;

			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.FilterGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewFilter.ColumnID);
					if (rowID == dataRecord.ID)
					{
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
		internal void DoNew()
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
		internal void DoEdit()
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

		// Deletes the selected row.
		internal void DoDelete()
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
		internal void DoRefresh()
		{
			ViewFilter record;
			int id = 0;

			if (Parent.FilterGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewFilter.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewFilter()
				{
					ID = id
				};
				RowSelect(record);
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
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.FilterGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Filter);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Filter Grid.
    private void SetupGridFilter()
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

		private ViewFilterManager mViewFilterManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
