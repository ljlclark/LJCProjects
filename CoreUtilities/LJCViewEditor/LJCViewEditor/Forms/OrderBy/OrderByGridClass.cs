// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
//OrderByGridClass.cs
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
	// Provides OrderByGrid methods for the ViewEditorList window.
	internal class OrderByGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal OrderByGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
      Managers = Parent.DataDbView.Managers;
			mViewOrderByManager = Managers.ViewOrderByManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			Parent.OrderByGrid.Rows.Clear();

			SetupGridOrderBy();
			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        // *** Begin *** Change- 10/5/23
        var manager = mViewOrderByManager;
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
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewOrderBy dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.OrderByGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.OrderByGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.OrderByGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewOrderBy.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewOrderBy dataRecord)
		{
			if (Parent.OrderByGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.OrderByGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewOrderBy dataRecord)
		{
			row.LJCSetInt32(ViewOrderBy.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewOrderBy record)
		{
			int rowID;

			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.OrderByGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewOrderBy.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						Parent.OrderByGrid.LJCSetCurrentRow(row, true);
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
			ViewOrderByDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.OrderByGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewOrderByDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += OrderByDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			ViewOrderByDetail detail;

			if (Parent.ViewGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.OrderByGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewOrderBy.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = Parent.OrderByGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewOrderByDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location,

					// Use table name to get table columns.
					LJCTableName = Parent.TableCombo.Text,
				};
				detail.LJCChange += OrderByDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (Parent.OrderByGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewOrderBy.ColumnID, row.LJCGetInt32(ViewOrderBy.ColumnID) }
					};
					mViewOrderByManager.Delete(keyColumns);
					if (mViewOrderByManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.OrderByGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.OrderBy);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewOrderBy record;
			int id = 0;

			if (Parent.OrderByGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewOrderBy.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewOrderBy()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void OrderByDetail_Change(object sender, EventArgs e)
    {
      ViewOrderByDetail detail;
      ViewOrderBy record;
      LJCGridRow row;

      detail = sender as ViewOrderByDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.OrderByGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.OrderBy);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View OrderBy Grid.
    private void SetupGridOrderBy()
		{
			if (0 == Parent.OrderByGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewOrderBy.ColumnColumnName
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns orderByGridColumns
					= mViewOrderByManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.OrderByGrid.LJCAddColumns(orderByGridColumns);
			}
		}
    #endregion

    internal ManagersDbView Managers { get; set; }

    #region Class Data

    private ViewOrderByManager mViewOrderByManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
