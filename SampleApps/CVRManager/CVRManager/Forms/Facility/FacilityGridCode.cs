// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityGridCode.cs
using System;
using System.Windows.Forms;
using CVRDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace CVRManager
{
	internal class FacilityGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal FacilityGridCode(FacilityList parent)
		{
			mParent = parent;
			mFacilityGrid = mParent.FacilityGrid;
			mManagers = mParent.Managers;
			//mChangeTimer = mParent.ChangeTimer;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveFacility()
		{
			Facilities dataRecords;

			mParent.Cursor = Cursors.WaitCursor;
			mFacilityGrid.LJCRowsClear();

			dataRecords = mManagers.FacilityManager.Load();

			//FacilityManager facilityManager = mManagers.FacilityManager;
			if (dataRecords != null && dataRecords.Count > 0)
			{
				foreach (Facility dataRecord in dataRecords)
				{
					RowAddFacility(dataRecord);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(FacilityList.Change.Facility);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddFacility(Facility dataRecord)
		{
			LJCGridRow retValue;

			retValue = mFacilityGrid.LJCRowAdd();
			SetStoredValuesFacility(retValue, dataRecord);

			// Sets the row values from a data object.
			mFacilityGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateFacility(Facility dataRecord)
		{
			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesFacility(row, dataRecord);
				mFacilityGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesFacility(LJCGridRow row, Facility dataRecord)
		{
			row.LJCSetInt32(Facility.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectFacility(Facility dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mFacilityGrid.Rows)
				{
					rowID = row.LJCGetInt32(Facility.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mFacilityGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewFacility()
		{
			FacilityDetail detail;

			detail = new FacilityDetail()
			{
				LJCHelpFileName = mParent.LJCHelpFile,
				LJCHelpPageName = mParent.LJCHelpPageDetail
			};
			detail.LJCChange += FacilityDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditFacility()
		{
			FacilityDetail detail;

			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				int id = row.LJCGetInt32(Facility.ColumnID);

				detail = new FacilityDetail()
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail
				};
				detail.LJCChange += FacilityDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void FacilityDetail_LJCChange(object sender, EventArgs e)
		{
			FacilityDetail detail;
			Facility dataRecord;
			LJCGridRow row;

			detail = sender as FacilityDetail;
			dataRecord = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateFacility(dataRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddFacility(dataRecord);
				mFacilityGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(FacilityList.Change.Facility);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteFacility()
		{
			string title;
			string message;

			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					int id = row.LJCGetInt32(Facility.ColumnID);

					mManagers.FacilityManager.DeleteWithID(id);
					if (mManagers.FacilityManager.AffectedCount > 0)
					{
						mFacilityGrid.Rows.Remove(row);
						mParent.TimedChange(FacilityList.Change.Facility);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshFacility()
		{
			Facility dataRecord;
			int id = 0;

			mParent.Cursor = Cursors.WaitCursor;
			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Facility.ColumnID);
			}
			DataRetrieveFacility();

			// Select the original row.
			if (id > 0)
			{
				dataRecord = new Facility()
				{
					ID = id
				};
				RowSelectFacility(dataRecord);
			}
			mParent.Cursor = Cursors.Default;
		}
		#endregion

		#region Class Data

		private readonly FacilityList mParent;
		private readonly LJCDataGrid mFacilityGrid;
		private readonly CVRManagers mManagers;
		//private readonly ChangeTimer mChangeTimer;
		#endregion
	}
}
