// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ConditionSetGridClass.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;

namespace LJCViewEditor
{
	// Provides ConditionSetGrid methods for the ViewEditorList window.
	internal class ConditionSetGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal ConditionSetGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewConditionSetManager = Parent.ViewHelper.ViewConditionSetManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/List.xml'/>
		internal void DataRetrieveConditionSet()
		{
			ViewConditionSets dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.ConditionSetGrid.Rows.Clear();

			ConfigureConditionSetGrid();
			if (Parent.FilterGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewFilterID = parentRow.LJCGetInt32(ViewFilter.ColumnID);

				dataRecords = mViewConditionSetManager.LoadWithParentID(viewFilterID);
				if (dataRecords != null && dataRecords.Count > 0)
				{
					foreach (ViewConditionSet dataRecord in dataRecords)
					{
						RowAddConditionSet(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.ConditionSet);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddConditionSet(ViewConditionSet dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.ConditionSetGrid.LJCRowAdd();
			SetStoredValuesConditionSet(retValue, dataRecord);

			// Sets the row values from a data object.
			Parent.ConditionSetGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateConditionSet(ViewConditionSet dataRecord)
		{
			LJCGridRow row;

			row = Parent.ConditionSetGrid.CurrentRow as LJCGridRow;
			if (row != null)
			{
				SetStoredValuesConditionSet(row, dataRecord);
				Parent.ConditionSetGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesConditionSet(LJCGridRow row
			, ViewConditionSet dataRecord)
		{
			row.LJCSetInt32(ViewConditionSet.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewConditionSet.ColumnBooleanOperator, dataRecord.BooleanOperator);
		}

		// Selects a row based on the key record values.
		private void RowSelectViewConditionSet(ViewConditionSet record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.ConditionSetGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewConditionSet.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						Parent.ConditionSetGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				Parent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewViewConditionSet()
		{
			ViewConditionSetDetail detail;

			if (Parent.FilterGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewFilter.ColumnID);
				string parentName = parentRow.LJCGetString(ViewFilter.ColumnName);

				var grid = Parent.ConditionSetGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionSetDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += ConditionSetDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditViewConditionSet()
		{
			ViewConditionSetDetail detail;

			if (Parent.FilterGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.ConditionSetGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(ViewConditionSet.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewFilter.ColumnID);
				string parentName = parentRow.LJCGetString(ViewFilter.ColumnName);

				var grid = Parent.ConditionSetGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionSetDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += ConditionSetDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void ConditionSetDetail_Change(object sender, EventArgs e)
		{
			ViewConditionSetDetail detail;
			ViewConditionSet record;
			LJCGridRow row;

			detail = sender as ViewConditionSetDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateConditionSet(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddConditionSet(record);
				Parent.ConditionSetGrid.LJCSetCurrentRow(row, true);
				Parent.TimedChange(ViewEditorList.Change.ConditionSet);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteViewConditionSet()
		{
			string title;
			string message;

			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewConditionSet.ColumnID, row.LJCGetInt32(ViewConditionSet.ColumnID) }
					};
					mViewConditionSetManager.Delete(keyColumns);
					if (mViewConditionSetManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.ConditionSetGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.ConditionSet);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshViewConditionSet()
		{
			ViewConditionSet record;
			int id = 0;

			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewConditionSet.ColumnID);
			}
			DataRetrieveConditionSet();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewConditionSet()
				{
					ID = id
				};
				RowSelectViewConditionSet(record);
			}
		}
		#endregion

		#region Setup Methods

		// Configures the View ConditionSet Grid.
		private void ConfigureConditionSetGrid()
		{
			if (0 == Parent.ConditionSetGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewConditionSet.ColumnBooleanOperator
				};

				// Get the display columns from the manager Data Definition.
				DbColumns conditionSetDisplayColumns
					= mViewConditionSetManager.GetColumns(propertyNames);

				// Setup the grid display columns.
				Parent.ConditionSetGrid.LJCAddDisplayColumns(conditionSetDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private readonly ViewEditorList Parent;
		private ViewConditionSetManager mViewConditionSetManager;
		#endregion
	}
}
