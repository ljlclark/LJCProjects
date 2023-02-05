// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConditionGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCViewEditor
{
	// Provides ConditionGrid methods for the ViewEditorList window.
	internal class ConditionGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal ConditionGridClass(ViewEditorList parent)
		{
			mParent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewConditionManager = mParent.ViewHelper.ViewConditionManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveCondition()
		{
			ViewConditions dataRecords;

			mParent.Cursor = Cursors.WaitCursor;
			mParent.ConditionGrid.Rows.Clear();

			ConfigureConditionGrid();
			if (mParent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewConditionSetID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);

				dataRecords = mViewConditionManager.LoadWithParentID(viewConditionSetID);
				if (dataRecords != null && dataRecords.Count > 0)
				{
					foreach (ViewCondition dataRecord in dataRecords)
					{
						RowAddCondition(dataRecord);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(ViewEditorList.Change.Condition);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddCondition(ViewCondition dataRecord)
		{
			LJCGridRow retValue;

			retValue = mParent.ConditionGrid.LJCRowAdd();
			SetStoredValuesCondition(retValue, dataRecord);

			// Sets the row values from a data object.
			mParent.ConditionGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateCondition(ViewCondition dataRecord)
		{
			LJCGridRow row;

			row = mParent.ConditionGrid.CurrentRow as LJCGridRow;
			if (row != null)
			{
				SetStoredValuesCondition(row, dataRecord);
				mParent.ConditionGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesCondition(LJCGridRow row
			, ViewCondition dataRecord)
		{
			row.LJCSetInt32(ViewCondition.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelectViewCondition(ViewCondition record)
		{
			int rowID;

			if (record != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mParent.ConditionGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewCondition.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mParent.ConditionGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				mParent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewViewCondition()
		{
			ViewConditionDetail detail;

			if (mParent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
				string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

				var grid = mParent.ConditionGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = mParent.TableCombo.Text,
				};
				detail.LJCChange += ConditionDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditViewCondition()
		{
			ViewConditionDetail detail;

			if (mParent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow
				&& mParent.ConditionGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewCondition.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
				string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

				var grid = mParent.ConditionGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = mParent.TableCombo.Text,
				};
				detail.LJCChange += ConditionDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDeleteViewCondition()
		{
			string title;
			string message;

			if (mParent.ConditionGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewCondition.ColumnID, row.LJCGetInt32(ViewCondition.ColumnID) }
					};
					mViewConditionManager.Delete(keyColumns);
					if (mViewConditionManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mParent.ConditionGrid.Rows.Remove(row);
						mParent.TimedChange(ViewEditorList.Change.Condition);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshViewCondition()
		{
			ViewCondition record;
			LJCGridRow row;
			int id = 0;

			row = mParent.ConditionGrid.CurrentRow as LJCGridRow;

			if (row != null)
			{
				id = row.LJCGetInt32(ViewCondition.ColumnID);
			}
			DataRetrieveCondition();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewCondition()
				{
					ID = id
				};
				RowSelectViewCondition(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ConditionDetail_Change(object sender, EventArgs e)
    {
      ViewConditionDetail detail;
      ViewCondition record;
      LJCGridRow row;

      detail = sender as ViewConditionDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdateCondition(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAddCondition(record);
        mParent.ConditionGrid.LJCSetCurrentRow(row, true);
        mParent.TimedChange(ViewEditorList.Change.ConditionSet);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Condition Grid.
    private void ConfigureConditionGrid()
		{
			if (0 == mParent.ConditionGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewCondition.ColumnFirstValue,
					ViewCondition.ColumnComparisonOperator,
					ViewCondition.ColumnSecondValue
				};

				// Get the display columns from the manager Data Definition.
				DbColumns conditionDisplayColumns
					= mViewConditionManager.GetColumns(propertyNames);

				// Setup the grid display columns.
				mParent.ConditionGrid.LJCAddDisplayColumns(conditionDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private ViewConditionManager mViewConditionManager;
		private readonly ViewEditorList mParent;
		#endregion
	}
}
