// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConditionSetGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;

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
		internal void DataRetrieve()
		{
			//ViewConditionSets dataRecords;

			Parent.Cursor = Cursors.WaitCursor;
			Parent.ConditionSetGrid.Rows.Clear();

			SetupGridConditionSet();
			if (Parent.FilterGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewFilterID = parentRow.LJCGetInt32(ViewFilter.ColumnID);

        // *** Begin *** Change- 10/5/23
        //dataRecords = mViewConditionSetManager.LoadWithParentID(viewFilterID);
        //if (NetCommon.HasItems(dataRecords))
        //{
        //	foreach (ViewConditionSet dataRecord in dataRecords)
        //	{
        //		RowAdd(dataRecord);
        //	}
        //}
        var manager = mViewConditionSetManager;
        DbResult result = manager.ResultWithParentID(viewFilterID);
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
        // *** End   *** Change- 10/5/23
      }
      Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.ConditionSet);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewConditionSet dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.ConditionSetGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.ConditionSetGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.ConditionSetGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewConditionSet.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewConditionSet.ColumnBooleanOperator;
      var name = dbValues.LJCGetValue(columnName);
      retValue.LJCSetString(columnName, name);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewConditionSet dataRecord)
		{
			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.ConditionSetGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row
			, ViewConditionSet dataRecord)
		{
			row.LJCSetInt32(ViewConditionSet.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewConditionSet.ColumnBooleanOperator
        , dataRecord.BooleanOperator);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewConditionSet dataRecord)
		{
			int rowID;

			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.ConditionSetGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewConditionSet.ColumnID);
					if (rowID == dataRecord.ID)
					{
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
		internal void DoNew()
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
		internal void DoEdit()
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

		// Deletes the selected row.
		internal void DoDelete()
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
		internal void DoRefresh()
		{
			ViewConditionSet record;
			int id = 0;

			if (Parent.ConditionSetGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewConditionSet.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewConditionSet()
				{
					ID = id
				};
				RowSelect(record);
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
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.ConditionSetGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.ConditionSet);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View ConditionSet Grid.
    private void SetupGridConditionSet()
		{
			if (0 == Parent.ConditionSetGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewConditionSet.ColumnBooleanOperator
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns conditionSetGridColumns
					= mViewConditionSetManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.ConditionSetGrid.LJCAddColumns(conditionSetGridColumns);
			}
		}
		#endregion

		#region Class Data

		private readonly ViewEditorList Parent;
		private ViewConditionSetManager mViewConditionSetManager;
		#endregion
	}
}
