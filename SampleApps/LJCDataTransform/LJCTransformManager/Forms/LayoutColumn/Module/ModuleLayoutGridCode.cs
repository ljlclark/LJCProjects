// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ModuleLayoutGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCWinFormCommon;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the Module Layout Grid control.
	internal class ModuleLayoutGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ModuleLayoutGridCode(LayoutColumnModule parent)
		{
			mParent = parent;
			mLayoutGrid = mParent.LayoutGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveLayout()
		{
			SourceLayouts records;

			mParent.Cursor = Cursors.WaitCursor;
			mLayoutGrid.LJCRowsClear();

			var layoutManager = mManagers.SourceLayoutManager;
			records = layoutManager.Load();

			if (NetCommon.HasItems(records))
			{
				foreach (SourceLayout record in records)
				{
					RowAddLayout(record);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(LayoutColumnModule.Change.Layout);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddLayout(SourceLayout dataRecord)
		{
			LJCGridRow retValue;

			retValue = mLayoutGrid.LJCRowAdd();
			SetStoredValuesLayout(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mLayoutGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateLayout(SourceLayout dataRecord)
		{
			if (mLayoutGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesLayout(row, dataRecord);
				row.LJCSetValues(mLayoutGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesLayout(LJCGridRow row, SourceLayout dataRecord)
		{
			row.LJCSetInt32(SourceLayout.ColumnSourceLayoutID, dataRecord.SourceLayoutID);

			// Save Parent name.
			row.LJCSetString(SourceLayout.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelectLayout(SourceLayout dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mLayoutGrid.Rows)
				{
					rowID = row.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);
					if (rowID == dataRecord.SourceLayoutID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mLayoutGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewLayout()
		{
			LayoutDetail detail;

			detail = new LayoutDetail()
			{
			};
			detail.LJCChange += LayoutDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditLayout()
		{
			LayoutDetail detail;

			if (mLayoutGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);

				detail = new LayoutDetail()
				{
					LJCID = id
				};
				detail.LJCChange += LayoutDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void LayoutDetail_LJCChange(object sender, EventArgs e)
		{
			LayoutDetail detail;
			LJCGridRow row;

			detail = sender as LayoutDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateLayout(detail.LJCRecord);
			}
			else
			{
				row = RowAddLayout(detail.LJCRecord);
				mLayoutGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(LayoutColumnModule.Change.Layout);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteLayout()
		{
			string title;
			string message;

			if (mLayoutGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = row.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);

					var layoutManager = mManagers.SourceLayoutManager;
					var keyColumns = layoutManager.GetIDKey(id);
					layoutManager.Delete(keyColumns);
					if (layoutManager.AffectedCount > 0)
					{
						mLayoutGrid.Rows.Remove(row);
						mParent.TimedChange(LayoutColumnModule.Change.Layout);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshLayout()
		{
			SourceLayout record;
			int id = 0;

			if (mLayoutGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(SourceLayout.ColumnSourceLayoutID);
			}
			DataRetrieveLayout();

			// Select the original row.
			if (id > 0)
			{
				record = new SourceLayout()
				{
					SourceLayoutID = id
				};
				RowSelectLayout(record);
			}
		}
		#endregion

		#region Class Data

		private readonly LayoutColumnModule mParent;
		private readonly LJCDataGrid mLayoutGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
