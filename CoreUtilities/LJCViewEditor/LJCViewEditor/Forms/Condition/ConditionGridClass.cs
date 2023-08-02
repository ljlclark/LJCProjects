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
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewConditionManager = Parent.ViewHelper.ViewConditionManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			ViewConditions dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.ConditionGrid.Rows.Clear();

			SetupGridCondition();
			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewConditionSetID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);

				dataRecords = mViewConditionManager.LoadWithParentID(viewConditionSetID);
				if (NetCommon.HasItems(dataRecords))
				{
					foreach (ViewCondition dataRecord in dataRecords)
					{
						RowAdd(dataRecord);
					}
				}
			}
			Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Condition);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewCondition dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.ConditionGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.ConditionGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(ViewCondition dataRecord)
		{
			LJCGridRow row;

			row = Parent.ConditionGrid.CurrentRow as LJCGridRow;
			if (row != null)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.ConditionGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row
			, ViewCondition dataRecord)
		{
			row.LJCSetInt32(ViewCondition.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewCondition record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.ConditionGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewCondition.ColumnID);
					if (rowID == record.ID)
					{
						Parent.ConditionGrid.LJCSetCurrentRow(row, true);
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
			ViewConditionDetail detail;

			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
				string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

				var grid = Parent.ConditionGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += ConditionDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			ViewConditionDetail detail;

			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.ConditionGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewCondition.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
				string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

				var grid = Parent.ConditionGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewConditionDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += ConditionDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (Parent.ConditionGrid.CurrentRow is LJCGridRow row)
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
						Parent.ConditionGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.Condition);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewCondition record;
			LJCGridRow row;
			int id = 0;

			row = Parent.ConditionGrid.CurrentRow as LJCGridRow;

			if (row != null)
			{
				id = row.LJCGetInt32(ViewCondition.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewCondition()
				{
					ID = id
				};
				RowSelect(record);
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
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.ConditionGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.ConditionSet);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Condition Grid.
    private void SetupGridCondition()
		{
			if (0 == Parent.ConditionGrid.Columns.Count)
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
				Parent.ConditionGrid.LJCAddDisplayColumns(conditionDisplayColumns);
			}
		}
		#endregion

		#region Class Data

		private ViewConditionManager mViewConditionManager;
		private readonly ViewEditorList Parent;
		#endregion
	}
}
