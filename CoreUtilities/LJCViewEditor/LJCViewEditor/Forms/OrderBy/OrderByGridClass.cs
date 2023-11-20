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
			OrderByGrid.Rows.Clear();

			SetupGridOrderBy();
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var manager = mViewOrderByManager;
        var result = manager.ResultWithParentID(viewDataID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewOrderBy dataRecord)
		{
			var retValue = OrderByGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(OrderByGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = OrderByGrid;
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
			if (OrderByGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(OrderByGrid, dataRecord);
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
			if (record != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in OrderByGrid.Rows)
				{
					var rowID = row.LJCGetInt32(ViewOrderBy.ColumnID);
					if (rowID == record.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						OrderByGrid.LJCSetCurrentRow(row, true);
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
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = OrderByGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewOrderByDetail
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
			if (ViewGrid.CurrentRow is LJCGridRow parentRow
				&& OrderByGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewOrderBy.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = OrderByGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewOrderByDetail()
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
			if (OrderByGrid.CurrentRow is LJCGridRow row)
			{
				var title = "Delete Confirmation";
				var message = FormCommon.DeleteConfirm;
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
						OrderByGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.OrderBy);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;
			if (OrderByGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewOrderBy.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new ViewOrderBy()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void OrderByDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewOrderByDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        OrderByGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.OrderBy);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View OrderBy Grid.
    private void SetupGridOrderBy()
		{
			if (0 == OrderByGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewOrderBy.ColumnColumnName
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns orderByGridColumns
					= mViewOrderByManager.GetColumns(propertyNames);

				// Setup the grid columns.
				OrderByGrid.LJCAddColumns(orderByGridColumns);
			}
		}
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid OrderByGrid { get; set; }

    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewOrderByManager mViewOrderByManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
