// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinOnGridClass.cs
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
	// Provides JoinOnGrid methods for the ViewEditorList window.
	internal class JoinOnGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal JoinOnGridClass(ViewEditorList parent)
		{
			Parent = parent;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
      Managers = Parent.DataDbView.Managers;
			mViewJoinOnManager = Managers.ViewJoinOnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			Parent.JoinOnGrid.Rows.Clear();

			SetupGridJoinOn();
			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewJoinID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

        // *** Begin *** Change- 10/5/23
        var manager = mViewJoinOnManager;
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
			Parent.DoChange(ViewEditorList.Change.JoinOn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewJoinOn dataRecord)
		{
			LJCGridRow retValue;

			retValue = Parent.JoinOnGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(Parent.JoinOnGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = Parent.JoinOnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoinOn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoinOn dataRecord)
		{
			if (Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(Parent.JoinOnGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewJoinOn dataRecord)
		{
			row.LJCSetInt32(ViewJoinOn.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewJoinOn dataRecord)
		{
			int rowID;

			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in Parent.JoinOnGrid.Rows)
				{
					rowID = row.LJCGetInt32(ViewJoinOn.ColumnID);
					if (rowID == dataRecord.ID)
					{
						Parent.JoinOnGrid.LJCSetCurrentRow(row, true);
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
			ViewJoinOnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinOnDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinOnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			ViewJoinOnDetail detail;

			if (Parent.JoinGrid.CurrentRow is LJCGridRow parentRow
				&& Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewJoinOn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = Parent.JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				detail = new ViewJoinOnDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += JoinOnDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			LJCGridRow row;
			string title;
			string message;

			row = Parent.JoinOnGrid.CurrentRow as LJCGridRow;
			if (row != null)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewJoinOn.ColumnID, row.LJCGetInt32(ViewJoinOn.ColumnID) }
					};
					mViewJoinOnManager.Delete(keyColumns);
					if (mViewJoinOnManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						Parent.JoinOnGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.JoinOn);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			ViewJoinOn record;
			int id = 0;

			if (Parent.JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoinOn.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new ViewJoinOn()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void JoinOnDetail_Change(object sender, EventArgs e)
    {
      ViewJoinOnDetail detail;
      ViewJoinOn record;
      LJCGridRow row;

      detail = sender as ViewJoinOnDetail;
      record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        row = RowAdd(record);
        Parent.JoinOnGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.JoinOn);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View JoinOn Grid.
    private void SetupGridJoinOn()
		{
			if (0 == Parent.JoinOnGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewJoinOn.ColumnFromColumnName,
					ViewJoinOn.ColumnJoinOnOperator,
					ViewJoinOn.ColumnToColumnName
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns joinOnGridColumns
					= mViewJoinOnManager.GetColumns(propertyNames);

				// Setup the grid columns.
				Parent.JoinOnGrid.LJCAddColumns(joinOnGridColumns);
			}
		}
    #endregion

    internal ManagersDbView Managers { get; set; }

    #region Class Data

    private ViewJoinOnManager mViewJoinOnManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
