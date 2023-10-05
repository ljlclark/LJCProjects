// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinColumnGridClass.cs
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
	// Provides JoinColumnGrid methods for the ViewEditorList window.
	internal class JoinColumnGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal JoinColumnGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mViewJoinColumnManager = Parent.ViewHelper.ViewJoinColumnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			Parent.JoinColumnGrid.Rows.Clear();

			SetupGridJoinColumn();
			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewJoinID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

        // *** Begin *** Change- 10/5/23
        var manager = mViewJoinColumnManager;
        var result = manager.ResultWithParentID(viewJoinID);
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
			Parent.DoChange(ViewEditorList.Change.JoinColumn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewJoinColumn dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.JoinColumnGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.JoinColumnGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.JoinColumnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoinColumn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoinColumn dataRecord)
		{
			if (Parent.JoinColumnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.JoinColumnGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row
			, ViewJoinColumn dataRecord)
		{
			row.LJCSetInt32(ViewJoinColumn.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewJoinColumn record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.JoinColumnGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewJoinColumn.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						Parent.JoinColumnGrid.LJCSetCurrentRow(row, true);
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
			ViewJoinColumnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinColumnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinColumnDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinColumnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			ViewJoinColumnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.JoinColumnGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(ViewJoinColumn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinColumnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinColumnDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinColumnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (Parent.JoinColumnGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewJoinColumn.ColumnID, row.LJCGetInt32(ViewJoinColumn.ColumnID) }
					};
					mViewJoinColumnManager.Delete(keyColumns);
					if (mViewJoinColumnManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.JoinColumnGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.JoinColumn);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewJoinColumn record;
			int id = 0;

			if (Parent.JoinColumnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoinColumn.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewJoinColumn()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void JoinColumnDetail_Change(object sender, EventArgs e)
    {
      ViewJoinColumnDetail detail;
      ViewJoinColumn record;
      LJCGridRow row;

      detail = sender as ViewJoinColumnDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.JoinColumnGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.JoinColumn);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View JoinColumn Grid.
    private void SetupGridJoinColumn()
		{
			if (0 == Parent.JoinColumnGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoinColumn.ColumnColumnName,
					ViewJoinColumn.ColumnPropertyName,
					ViewJoinColumn.ColumnRenameAs
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns joinColumnGridColumns
					= mViewJoinColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.JoinColumnGrid.LJCAddColumns(joinColumnGridColumns);
			}
		}
		#endregion

		#region Class Data

		private ViewJoinColumnManager mViewJoinColumnManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
