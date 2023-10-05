// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using LJCViewEditorDAL;
using LJCDBClientLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;
using System.Data;

namespace LJCViewEditor
{
	// Provides ColumnGrid methods for the ViewEditorList window.
	internal class ColumnGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal ColumnGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mDataConfigName = Parent.DataConfigName;
			mDbServiceRef = Parent.DbServiceRef;
			mViewColumnManager = Parent.ViewHelper.ViewColumnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			Parent.ColumnGrid.Rows.Clear();

			SetupGridColumn();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        // *** Begin *** Change- 10/5/23
        var manager = mViewColumnManager;
        var result = manager.ResultWithParentID(viewDataID);
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
			Parent.DoChange(ViewEditorList.Change.Column);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewColumn dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.ColumnGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(Parent.ColumnGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.ColumnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewColumn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewColumn dataRecord)
		{
			if (Parent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.ColumnGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewColumn dataRecord)
		{
			row.LJCSetInt32(ViewColumn.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewColumn dataRecord)
		{
			int rowID;

			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.ColumnGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewColumn.ColumnID);
					if (rowID == dataRecord.ID)
					{
						Parent.ColumnGrid.LJCSetCurrentRow(row, true);
						break;
					}
				}
				Parent.Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Action Methods

		// Adds all missing columns.
		internal void DoAddAll(string tableName)
		{
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

				ViewHelper viewHelper = new ViewHelper(mDbServiceRef, mDataConfigName);
				DataHelper dataHelper = new DataHelper(mDbServiceRef, mDataConfigName);
				var tableColumns = dataHelper.GetTableColumns(tableName);
				foreach (DbColumn dbColumn in tableColumns)
				{
					ViewColumn viewColumn = viewHelper.GetViewColumnFromDbColumn(dbColumn);
					viewColumn.ViewDataID = parentID;

					var keyColumns = new DbColumns()
					{
						{ ViewColumn.ColumnViewDataID, viewColumn.ViewDataID },
						{ ViewColumn.ColumnColumnName, (object)viewColumn.ColumnName }
					};
					var lookupRecord = mViewColumnManager.Retrieve(keyColumns);
					if (false == mViewColumnManager.IsDuplicate(lookupRecord, viewColumn, false))
					{
						var addedRecord = mViewColumnManager.AddWithFlags(viewColumn);
						if (addedRecord != null)
						{
							viewColumn.ID = addedRecord.ID;
							var row = RowAdd(viewColumn);
							Parent.ColumnGrid.LJCSetCurrentRow(row, true);
						}
					}
				}
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			ViewColumnDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.ColumnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewColumnDetail
				{
					LJCDataConfigName = mDataConfigName,
					LJCDbServiceRef = mDbServiceRef,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += ColumnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			ViewColumnDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(ViewColumn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.ColumnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewColumnDetail()
				{
					LJCDataConfigName = mDataConfigName,
					LJCDbServiceRef = mDbServiceRef,
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += ColumnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;
			int parentID;
			int id = 0;
			bool success = false;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					success = true;
				}

				if (success)
				{
					// Data from items.
					id = row.LJCGetInt32(ViewColumn.ColumnID);
					parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

					ViewGridColumnManager gridColumnManager
						= Parent.ViewHelper.ViewGridColumnManager;
					var keyGridColumns = new DbColumns()
					{
						{ ViewGridColumn.ColumnViewDataID, parentID },
						{ ViewGridColumn.ColumnViewColumnID, id }
					};
					gridColumnManager.Delete(keyGridColumns);
				}

				if (success)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewColumn.ColumnID, id }
					};
					mViewColumnManager.Delete(keyColumns);
					if (mViewColumnManager.AffectedCount < 1)
					{
						success = false;
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
				}

				if (success)
				{
					Parent.ColumnGrid.Rows.Remove(row);
					Parent.TimedChange(ViewEditorList.Change.Column);
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewColumn record;
			int id = 0;

			if (Parent.ColumnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewColumn.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewColumn()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ColumnDetail_Change(object sender, EventArgs e)
    {
      ViewColumnDetail detail;
      ViewColumn record;
      LJCGridRow row;

      detail = sender as ViewColumnDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.ColumnGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Column);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Column Grid.
    private void SetupGridColumn()
		{
			if (0 == Parent.ColumnGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewColumn.ColumnCaption,
					ViewColumn.ColumnColumnName,
					ViewColumn.ColumnPropertyName,
					ViewColumn.ColumnRenameAs,
					ViewColumn.ColumnDataTypeName
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns gridColumns
					= mViewColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.ColumnGrid.LJCAddColumns(gridColumns);
			}
		}
		#endregion

		#region Class Data

		private string mDataConfigName;
		private DbServiceRef mDbServiceRef;
		private ViewColumnManager mViewColumnManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
