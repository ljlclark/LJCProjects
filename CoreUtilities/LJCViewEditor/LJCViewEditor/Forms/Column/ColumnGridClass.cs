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
      ColumnGrid = Parent.ColumnGrid;
      ViewGrid = Parent.ViewGrid;
      ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
			mDataConfigName = Parent.DataConfigName;
			mDbServiceRef = Parent.DbServiceRef;
      Managers = Parent.Managers;
			mColumnManager = Managers.ViewColumnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			ColumnGrid.Rows.Clear();

			SetupGridColumn();
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var result = mColumnManager.ResultWithParentID(viewDataID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Column);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewColumn dataRecord)
		{
			var retValue = ColumnGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ColumnGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = ColumnGrid;
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
			if (ColumnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(ColumnGrid, dataRecord);
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
			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in ColumnGrid.Rows)
				{
					var rowID = row.LJCGetInt32(ViewColumn.ColumnID);
					if (rowID == dataRecord.ID)
					{
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
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
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

				var dataDbView = new DataDbView(Managers);
        var dataHelper = new DataHelper(Managers.DbServiceRef
          , Managers.DataConfigName);
				var tableColumns = dataHelper.GetTableColumns(tableName);
				foreach (var dbColumn in tableColumns)
				{
					var viewColumn = dataDbView.GetViewColumnFromDbColumn(dbColumn);
					viewColumn.ViewDataID = parentID;

					var keyColumns = new DbColumns()
					{
						{ ViewColumn.ColumnViewDataID, viewColumn.ViewDataID },
						{ ViewColumn.ColumnColumnName, (object)viewColumn.ColumnName }
					};
					var lookupRecord = mColumnManager.Retrieve(keyColumns);
					if (false == mColumnManager.IsDuplicate(lookupRecord, viewColumn, false))
					{
						var addedRecord = mColumnManager.AddWithFlags(viewColumn);
						if (addedRecord != null)
						{
							viewColumn.ID = addedRecord.ID;
							var row = RowAdd(viewColumn);
							ColumnGrid.LJCSetCurrentRow(row, true);
						}
					}
				}
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(ColumnGrid);
				var detail = new ViewColumnDetail
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
			if (ViewGrid.CurrentRow is LJCGridRow parentRow
				&& ColumnGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(ViewColumn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var location = FormCommon.GetDialogScreenPoint(ColumnGrid);
				var detail = new ViewColumnDetail()
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
			if (ViewGrid.CurrentRow is LJCGridRow parentRow
				&& ColumnGrid.CurrentRow is LJCGridRow row)
			{
        bool success = false;
        var title = "Delete Confirmation";
				var message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					success = true;
				}

        int id = 0;
				if (success)
				{
					// Data from items.
					id = row.LJCGetInt32(ViewColumn.ColumnID);
					var parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

					var gridColumnManager	= Managers.ViewGridColumnManager;
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
					mColumnManager.Delete(keyColumns);
					if (mColumnManager.AffectedCount < 1)
					{
						success = false;
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
				}

				if (success)
				{
					ColumnGrid.Rows.Remove(row);
					Parent.TimedChange(ViewEditorList.Change.Column);
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;
			if (ColumnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewColumn.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new ViewColumn()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ColumnDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewColumnDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ColumnGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Column);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Column Grid.
    private void SetupGridColumn()
		{
			if (0 == ColumnGrid.Columns.Count)
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
					= mColumnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				ColumnGrid.LJCAddColumns(gridColumns);
			}
		}
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid ColumnGrid { get; set; }

    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewColumnManager mColumnManager;
    private string mDataConfigName;
		private DbServiceRef mDbServiceRef;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
