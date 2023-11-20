// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FilterGridClass.cs
using LJCNetCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;

namespace LJCViewEditor
{
	// Provides FilterGrid methods for the ViewEditorList window.
	internal class FilterGridClass
	{
		#region Constructors

		// Initializes an object instance.
		internal FilterGridClass(ViewEditorList parent)
		{
			Parent = parent;
      FilterGrid = FilterGrid;
      ViewGrid = ViewGrid;
			ResetData();
		}

		/// <summary>Resets the DataConfig dependent objects.</summary>
		internal void ResetData()
		{
      Managers = Parent.Managers;
      mViewFilterManager = Managers.ViewFilterManager;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieve()
		{
			Parent.Cursor = Cursors.WaitCursor;
			FilterGrid.Rows.Clear();
			Parent.ConditionSetGrid.Rows.Clear();
			Parent.ConditionGrid.Rows.Clear();

			SetupGridFilter();
			if (ViewGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from items.
				int viewDataID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var manager = mViewFilterManager;
        var result = manager.ResultWithParentID(viewDataID);
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
			Parent.DoChange(ViewEditorList.Change.Filter);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(ViewFilter dataRecord)
		{
			var retValue = FilterGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);
			retValue.LJCSetValues(FilterGrid, dataRecord);
			return retValue;
		}

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = FilterGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewFilter.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewFilter.ColumnName;
      var name = dbValues.LJCGetString(columnName);
      retValue.LJCSetString(columnName, name);
      
      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewFilter dataRecord)
		{
			if (FilterGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValues(row, dataRecord);
				row.LJCSetValues(FilterGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, ViewFilter dataRecord)
		{
			row.LJCSetInt32(ViewFilter.ColumnID, dataRecord.ID);
			row.LJCSetString(ViewFilter.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private void RowSelect(ViewFilter dataRecord)
		{
			if (dataRecord != null)
			{
				Parent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in FilterGrid.Rows)
				{
					var rowID = row.LJCGetInt32(ViewFilter.ColumnID);
					if (rowID == dataRecord.ID)
					{
						FilterGrid.LJCSetCurrentRow(row, true);
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

				var defaultName = $"Filter{FilterGrid.Rows.Count + 1}";
				var grid = FilterGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewFilterDetail
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCDefaultName = defaultName,
					LJCLocation = location
				};
				detail.LJCChange += FilterDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			if (ViewGrid.CurrentRow is LJCGridRow parentRow
				&& FilterGrid.CurrentRow is LJCGridRow row)
			{
				int id = row.LJCGetInt32(ViewFilter.ColumnID);
				int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
				string parentName = parentRow.LJCGetString(ViewData.ColumnName);

				var grid = FilterGrid;
				var location = FormCommon.GetDialogScreenPoint(grid);
				var detail = new ViewFilterDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCLocation = location
				};
				detail.LJCChange += FilterDetail_Change;
				detail.ShowDialog();
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			if (FilterGrid.CurrentRow is LJCGridRow row)
			{
				var title = "Delete Confirmation";
				var message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ ViewFilter.ColumnID, row.LJCGetInt32(ViewFilter.ColumnID) }
					};
					mViewFilterManager.Delete(keyColumns);
					if (mViewFilterManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						FilterGrid.Rows.Remove(row);
						Parent.TimedChange(ViewEditorList.Change.Filter);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			int id = 0;
			if (FilterGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(ViewFilter.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				var record = new ViewFilter()
				{
					ID = id
				};
				RowSelect(record);
			}
		}

    // Adds new row or updates existing row with changes from the detail dialog.
    private void FilterDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewFilterDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        FilterGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.Filter);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Filter Grid.
    private void SetupGridFilter()
		{
			if (0 == FilterGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string> {
					ViewFilter.ColumnName,
					ViewFilter.ColumnBooleanOperator
				};

				// Get the grid columns from the manager Data Definition.
				DbColumns filterGridColumns
					= mViewFilterManager.GetColumns(propertyNames);

				// Setup the grid columns.
				FilterGrid.LJCAddColumns(filterGridColumns);
			}
		}
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid FilterGrid { get; set; }

    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewFilterManager mViewFilterManager;
    private readonly ViewEditorList Parent;
    #endregion
  }
}
