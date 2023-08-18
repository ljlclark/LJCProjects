// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitMeasureGridCode.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCUnitMeasure
{
	// The grid code.
	internal class UnitMeasureGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitMeasureGridCode(UnitMeasureList parent)
		{
			mParent = parent;
			mSystemCombo = mParent.SystemCombo;
			mCategoryCombo = mParent.CategoryCombo;
			mUnitMeasureGrid = mParent.UnitMeasureGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/List.xml'/>
		internal void DataRetrieve()
		{
			UnitMeasures dataRecords;

			mParent.Cursor = Cursors.WaitCursor;
			mUnitMeasureGrid.LJCRowsClear();

			var unitMeasureManager = mManagers.UnitMeasureManager;

			// Data from items.
			int systemID = 0;
			if (mSystemCombo.SelectedIndex >= 0)
			{
				systemID = mSystemCombo.LJCSelectedItemID();
			}
			int categoryID = 0;
			if (mCategoryCombo.SelectedIndex >= 2)
			{
				categoryID = mCategoryCombo.LJCSelectedItemID();
			}

			var keyColumns = new DbColumns()
			{
				{ UnitMeasure.ColumnUnitSystemID, systemID },
				{ UnitMeasure.ColumnUnitCategoryID, categoryID }
			};
			dataRecords = unitMeasureManager.LoadBySequence(keyColumns);

			if (dataRecords != null && dataRecords.Count > 0)
			{
				foreach (UnitMeasure dataRecord in dataRecords)
				{
					RowAdd(dataRecord);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(UnitMeasureList.Change.UnitMeasure);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAdd(UnitMeasure dataRecord)
		{
			LJCGridRow retValue;

			retValue = mUnitMeasureGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(mUnitMeasureGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdate(UnitMeasure dataRecord)
		{
			if (mUnitMeasureGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValues(gridRow, dataRecord);
				gridRow.LJCSetValues(mUnitMeasureGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValues(LJCGridRow row, UnitMeasure dataRecord)
		{
			row.LJCSetInt32(UnitMeasure.ColumnID, dataRecord.ID);
			row.LJCSetString(UnitMeasure.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		private bool RowSelect(UnitMeasure dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mUnitMeasureGrid.Rows)
				{
					rowID = row.LJCGetInt32(UnitMeasure.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mUnitMeasureGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				mParent.Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNew()
		{
			UnitMeasureDetail detail;

			if (mSystemCombo.SelectedIndex >= 0)
			{
				// Data from items.
				int unitSystemID = mSystemCombo.LJCSelectedItemID();
				int unitCategoryID = mCategoryCombo.LJCSelectedItemID();

				detail = new UnitMeasureDetail(mManagers)
				{
					LJCUnitSystemID = unitSystemID,
					LJCUnitCategoryID = unitCategoryID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitMeasureDetail_Change;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEdit()
		{
			UnitMeasureDetail detail;

			if (mUnitMeasureGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(UnitMeasure.ColumnID);
				int unitSystemID = mSystemCombo.LJCSelectedItemID();
				int unitCategoryID = mCategoryCombo.LJCSelectedItemID();

				detail = new UnitMeasureDetail(mManagers)
				{
					LJCID = id,
					LJCUnitSystemID = unitSystemID,
					LJCUnitCategoryID = unitCategoryID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail,
				};
				detail.LJCChange += UnitMeasureDetail_Change;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates row with changes from the detail dialog.
		private void UnitMeasureDetail_Change(object sender, EventArgs e)
		{
			UnitMeasureDetail detail;
			UnitMeasure dataRecord;
			LJCGridRow row;

			detail = sender as UnitMeasureDetail;
			if (detail.LJCRecord != null)
			{
				dataRecord = detail.LJCRecord;
				if (detail.LJCIsUpdate)
				{
					RowUpdate(dataRecord);
				}
				else
				{
					// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
					row = RowAdd(dataRecord);
					mUnitMeasureGrid.LJCSetCurrentRow(row, true);
					mParent.TimedChange(UnitMeasureList.Change.UnitMeasure);
				}
			}
		}

		// Deletes the selected row.
		internal void DoDelete()
		{
			string title;
			string message;

			if (mUnitMeasureGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = row.LJCGetInt32(UnitMeasure.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ UnitMeasure.ColumnID, id }
					};
					mManagers.UnitMeasureManager.Delete(keyColumns);
					if (0 == mManagers.UnitMeasureManager.AffectedCount)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mUnitMeasureGrid.Rows.Remove(row);
						mParent.TimedChange(UnitMeasureList.Change.UnitMeasure);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefresh()
		{
			UnitMeasure dataRecord;
			int id = 0;

			mParent.Cursor = Cursors.WaitCursor;
			if (mUnitMeasureGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(UnitMeasure.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				dataRecord = new UnitMeasure()
				{
					ID = id
				};
				RowSelect(dataRecord);
			}
			mParent.Cursor = Cursors.Default;
		}
		#endregion

		#region Setup Methods

		// Setup the grid columns.
		internal DbColumns SetupGrid()
		{
			DbColumns retValue = null;

			// Setup default grid columns if no columns are defined.
			if (0 == mUnitMeasureGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>()
				{
					UnitMeasure.ColumnCode,
					UnitMeasure.ColumnName,
					UnitMeasure.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				retValue = mManagers.UnitMeasureManager.GetColumns(propertyNames);

				// Setup the grid columns.
				mUnitMeasureGrid.LJCAddColumns(retValue);
			}
			return retValue;
		}
		#endregion

		#region Class Data

		private readonly UnitMeasureList mParent;
		private readonly LJCItemCombo mSystemCombo;
		private readonly LJCItemCombo mCategoryCombo;
		private readonly LJCDataGrid mUnitMeasureGrid;
		private readonly UnitMeasureManagers mManagers;
		#endregion
	}
}
