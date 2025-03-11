// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceLayoutColumnGridCode.cs
using System;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDataTransformDAL;
using LJCNetCommon;

namespace LJCTransformManager
{
	// Code for the SourceLayoutColumn Grid control.
	internal class SourceLayoutColumnGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal SourceLayoutColumnGridCode(SourceLayoutModule parent)
		{
			mParent = parent;
			mLayoutColumnGrid = mParent.LayoutColumnGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveLayoutColumn()
		{
			LayoutColumns records;

			mParent.Cursor = Cursors.WaitCursor;
			mLayoutColumnGrid.LJCRowsClear();

			int sourceLayoutID = mParent.mSourceLayoutID;
			if (sourceLayoutID > 0)
			{
				var layoutColumnManager = mManagers.LayoutColumnManager;
				records = layoutColumnManager.LoadWithLayoutID(sourceLayoutID);

				if (NetCommon.HasItems(records))
				{
					foreach (LayoutColumn record in records)
					{
						RowAddLayoutColumn(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(SourceLayoutModule.Change.LayoutColumn);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddLayoutColumn(LayoutColumn dataRecord)
		{
			LJCGridRow retValue;

			retValue = mLayoutColumnGrid.LJCRowAdd();
			SetStoredValuesLayoutColumn(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mLayoutColumnGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateLayoutColumn(LayoutColumn dataRecord)
		{
			if (mLayoutColumnGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesLayoutColumn(row, dataRecord);
				row.LJCSetValues(mLayoutColumnGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesLayoutColumn(LJCGridRow row
			, LayoutColumn dataRecord)
		{
			row.LJCSetInt32(LayoutColumn.ColumnLayoutColumnID, dataRecord.LayoutColumnID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectLayoutColumn(LayoutColumn dataRecord)
		{
			int rowID;
			bool retValue = false;

			mParent.Cursor = Cursors.WaitCursor;
			if (dataRecord != null)
			{
				foreach (LJCGridRow row in mLayoutColumnGrid.Rows)
				{
					rowID = row.LJCGetInt32(LayoutColumn.ColumnLayoutColumnID);
					if (rowID == dataRecord.SourceLayoutID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mLayoutColumnGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewLayoutColumn()
		{
			if (mParent.mSourceLayoutID > 0)
			{
				// Data from items.
				string parentName = mParent.SourceLayoutTextbox.Text;

				LayoutColumnDetail detail = new LayoutColumnDetail()
				{
					// Data from items.
					LJCParentID = mParent.mSourceLayoutID,
					LJCParentName = parentName,
				};
				detail.LJCChange += LayoutColumnDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditLayoutColumn()
		{
			LayoutColumnDetail detail;

			if (mParent.mSourceLayoutID > 0
				&& mLayoutColumnGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				string parentName = mParent.SourceLayoutTextbox.Text;
				short id = (short)row.LJCGetInt32(LayoutColumn.ColumnLayoutColumnID);

				detail = new LayoutColumnDetail()
				{
					// Data from items.
					LJCID = id,
					LJCParentID = mParent.mSourceLayoutID,
					LJCParentName = parentName
				};
				detail.LJCChange += LayoutColumnDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		private void LayoutColumnDetail_LJCChange(object sender, EventArgs e)
		{
			LayoutColumnDetail detail;
			LJCGridRow row;

			detail = sender as LayoutColumnDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateLayoutColumn(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddLayoutColumn(detail.LJCRecord);
				mLayoutColumnGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(SourceLayoutModule.Change.LayoutColumn);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteLayoutColumn()
		{
			string title;
			string message;

			if (mLayoutColumnGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					short id = (short)row.LJCGetInt32(LayoutColumn.ColumnLayoutColumnID);

					var layoutColumnManager = mManagers.LayoutColumnManager;
					var keyColumns = layoutColumnManager.GetIDKey(id);
					layoutColumnManager.Delete(keyColumns);
					if (layoutColumnManager.AffectedCount > 0)
					{
						mLayoutColumnGrid.Rows.Remove(row);
						mParent.TimedChange(SourceLayoutModule.Change.LayoutColumn);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshLayoutColumn()
		{
			LayoutColumn record;
			int id = 0;

			if (mLayoutColumnGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(LayoutColumn.ColumnLayoutColumnID);
			}
			DataRetrieveLayoutColumn();

			// Select the original row.
			if (id > 0)
			{
				record = new LayoutColumn()
				{
					SourceLayoutID = id
				};
				RowSelectLayoutColumn(record);
			}
		}
		#endregion

		#region Class Data

		private readonly SourceLayoutModule mParent;
		private readonly LJCDataGrid mLayoutColumnGrid;
		private readonly TransformManagers mManagers;
		#endregion
	}
}
