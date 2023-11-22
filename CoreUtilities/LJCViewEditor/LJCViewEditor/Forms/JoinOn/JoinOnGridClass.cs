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
      JoinGrid = Parent.JoinGrid;
      JoinOnGrid = Parent.JoinOnGrid;
			ResetData();
		}

		// Resets the DataConfig dependent objects.
		internal void ResetData()
		{
      Managers = Parent.Managers;
			mViewJoinOnManager = Managers.ViewJoinOnManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			JoinOnGrid.Rows.Clear();

			SetupGridJoinOn();
			if (JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewJoinID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

        var manager = mViewJoinOnManager;
        var result = manager.ResultWithParentID(viewJoinID);
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.JoinOn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewJoinOn dataRecord)
		{
			var retValue = JoinOnGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(JoinOnGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = JoinOnGrid;
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
			if (JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(JoinOnGrid, dataRecord);
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
			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in JoinOnGrid.Rows)
				{
					var rowID = row.LJCGetInt32(ViewJoinOn.ColumnID);
					if (rowID == dataRecord.ID)
					{
						JoinOnGrid.LJCSetCurrentRow(row, true);
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
			if (JoinGrid.CurrentRow is LJCGridRow parentRow)
			{
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewJoinOnDetail
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
			if (JoinGrid.CurrentRow is LJCGridRow parentRow
				&& JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewJoinOn.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
				string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

				var grid = JoinOnGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewJoinOnDetail()
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
      if (JoinOnGrid.CurrentRow is LJCGridRow row)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
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
            JoinOnGrid.Rows.Remove(row);
            Parent.TimedChange(ViewEditorList.Change.JoinOn);
          }
        }
      }
    }

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;
			if (JoinOnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewJoinOn.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new ViewJoinOn()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void JoinOnDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewJoinOnDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        JoinOnGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.JoinOn);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View JoinOn Grid.
    private void SetupGridJoinOn()
		{
			if (0 == JoinOnGrid.Columns.Count)
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
				JoinOnGrid.LJCAddColumns(joinOnGridColumns);
			}
		}
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid JoinGrid { get; set; }

    private LJCDataGrid JoinOnGrid { get; set; }
    #endregion

    #region Class Data

    private ViewJoinOnManager mViewJoinOnManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
