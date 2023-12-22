// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVVisitGridCode.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CVRDAL;
using LJCDataAccess;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace CVRManager
{
	internal class CVVisitGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal CVVisitGridCode(CVVisitList parent)
		{
			mParent = parent;
			mCVVisitGrid = mParent.CVVisitGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveCVVisit(CVVisits dataRecords)
		{
			mParent.Cursor = Cursors.WaitCursor;
			string region = "DataRetrieveCVisit";
			LogTime logTime = new LogTime("CVRManager.log", region, true);
			mCVVisitGrid.LJCRowsClear();

			if (NetCommon.HasItems(dataRecords))
			{
				foreach (CVVisit dataRecord in dataRecords)
				{
					RowAddCVVisit(dataRecord);
				}
			}
			logTime.Stop(region);
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(CVVisitList.Change.CVVisit);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddCVVisit(CVVisit dataRecord)
		{
			LJCGridRow retValue;

			retValue = mCVVisitGrid.LJCRowAdd();
			SetStoredValuesCVVisit(retValue, dataRecord);

      // Sets the row values from a data object.
      retValue.LJCSetValues(mCVVisitGrid, dataRecord);
			CustomValuesCVVisit(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateCVVisit(CVVisit dataRecord)
		{
			if (mCVVisitGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesCVVisit(gridRow, dataRecord);
				gridRow.LJCSetValues(mCVVisitGrid, dataRecord);
				CustomValuesCVVisit(gridRow, dataRecord);
			}
		}

		// Add the Calulated and Custom values.
		private void CustomValuesCVVisit(LJCGridRow row, CVVisit dataRecord)
		{
			row.LJCSetCellText(mParent.mGridColumnsRegisterDate.ColumnName
				, DataCommon.GetUIDateString(dataRecord.RegisterTime));
			string timeString = DataCommon.GetUITimeString(dataRecord.RegisterTime);
			row.LJCSetCellText(CVVisit.ColumnRegisterTime, timeString);
			timeString = DataCommon.GetUITimeString(dataRecord.EnterTime);
			row.LJCSetCellText(CVVisit.ColumnEnterTime, timeString);
			timeString = DataCommon.GetUITimeString(dataRecord.ExitTime);
			row.LJCSetCellText(CVVisit.ColumnExitTime, timeString);
		}

		// Sets the row stored values.
		private void SetStoredValuesCVVisit(LJCGridRow row, CVVisit dataRecord)
		{
			row.LJCSetInt64(CVVisit.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectCVVisit(CVVisit dataRecord)
		{
			long rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mCVVisitGrid.Rows)
				{
					rowID = row.LJCGetInt64(CVVisit.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mCVVisitGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewCVVisit()
		{
			CVVisitDetail detail;

			if (mParent.FacilityCombo.SelectedItem != null)
			{
				// Data from items.
				int parentID = mParent.FacilityCombo.LJCSelectedItemID();
				string parentName = mParent.FacilityCombo.Text;

				detail = new CVVisitDetail()
				{
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail
				};
				detail.LJCChange += CVVisitDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditCVVisit()
		{
			CVVisitDetail detail;

			if (mParent.FacilityCombo.SelectedItem != null
				&& mCVVisitGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				long id = row.LJCGetInt64(CVVisit.ColumnID);
				int parentID = mParent.FacilityCombo.LJCSelectedItemID();
				string parentName = mParent.FacilityCombo.Text;

				detail = new CVVisitDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCParentName = parentName,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail
				};
				detail.LJCChange += CVVisitDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void CVVisitDetail_LJCChange(object sender, EventArgs e)
		{
			CVVisitDetail detail;
			CVVisit dataRecord;
			LJCGridRow row;

			detail = sender as CVVisitDetail;
			dataRecord = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateCVVisit(dataRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddCVVisit(dataRecord);
				mCVVisitGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(CVVisitList.Change.CVVisit);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteCVVisit()
		{
			string title;
			string message;

			if (mCVVisitGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					long id = row.LJCGetInt64(CVVisit.ColumnID);

					var cvVisitManager = mManagers.CVVisitManager;
					cvVisitManager.DeleteWithID(id);
					if (cvVisitManager.AffectedCount > 0)
					{
						mCVVisitGrid.Rows.Remove(row);
						mParent.TimedChange(CVVisitList.Change.CVVisit);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshCVVisit()
		{
			CVVisit dataRecord;
			long id = 0;

			mParent.Cursor = Cursors.WaitCursor;
			if (mCVVisitGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt64(CVVisit.ColumnID);
			}
			//DataRetrieveCVVisit();
			DoShow();

			// Select the original row.
			if (id > 0)
			{
				dataRecord = new CVVisit()
				{
					ID = id
				};
				RowSelectCVVisit(dataRecord);
			}
			mParent.Cursor = Cursors.Default;
		}

		// Sets the Enter Time for the selected row.
		internal void DoEnterCVVisit()
		{
			if (mCVVisitGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				long id = row.LJCGetInt64(CVVisit.ColumnID);

				CVVisit dataRecord = new CVVisit()
				{
					EnterTime = DateTime.Now
				};

				var cvVisitManager = mManagers.CVVisitManager;
				var keyColumns = cvVisitManager.GetIDKey(id);
				List<string> columnNames = new List<string>()
				{
					CVVisit.ColumnEnterTime
				};
				cvVisitManager.Update(dataRecord, keyColumns, columnNames);
				DoRefreshCVVisit();
			}
		}

		// Sets the Exit Time for the selected row.
		internal void DoExitCVVisit()
		{
			if (mCVVisitGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				long id = row.LJCGetInt64(CVVisit.ColumnID);

				CVVisit dataRecord = new CVVisit()
				{
					ExitTime = DateTime.Now
				};

				var cvVisitManager = mManagers.CVVisitManager;
				var keyColumns = cvVisitManager.GetIDKey(id);
				List<string> columnNames = new List<string>()
				{
					CVVisit.ColumnExitTime
				};
				cvVisitManager.Update(dataRecord, keyColumns, columnNames);
				DoRefreshCVVisit();
			}
		}

		// Display the filtered list.
		internal void DoShow()
		{
			CVVisits dataRecords;
			string startDateString = null;
			string endDateString = null;

			mParent.Cursor = Cursors.WaitCursor;

			// Get Start Date
			DateTime dateTime = DataCommon.GetDbDate(mParent.DateMask.Text);
			if (dateTime > DataCommon.GetMinDateTime())
			{
				startDateString = DataCommon.GetDbDateString(dateTime);
			}

			if (NetString.HasValue(mParent.mEndDateString))
			{
				// Use End Date from Visit Filter Dialog.
				endDateString = mParent.mEndDateString;
			}
			else
			{
				if (NetString.HasValue(startDateString))
				{
					// If no End Date use Start Date.
					DateTime endDate = dateTime.AddDays(1);
					endDateString = DataCommon.GetDbDateString(endDate);
				}
			}

			// Create Filters.
			var	cvVisitManager = mManagers.CVVisitManager;

			if (NetString.HasValue(startDateString)
				|| NetString.HasValue(mParent.mLastName)
				|| NetString.HasValue(mParent.mMiddleName)
				|| NetString.HasValue(mParent.mFirstName))
			{
				DbFilters dbFilters = new DbFilters();
				DbFilter dbFilter = new DbFilter();
				DbConditions dbConditions = dbFilter.ConditionSet.Conditions;

				int facilityID = mParent.mFacilityID;
				dbConditions.Add(CVVisit.ColumnFacilityID, facilityID.ToString());
				if (NetString.HasValue(startDateString))
				{
					startDateString = $"'{startDateString}'";
					dbConditions.Add(CVVisit.ColumnRegisterTime, startDateString, ">=");
					endDateString = $"'{endDateString}'";
					dbConditions.Add(CVVisit.ColumnRegisterTime, endDateString, "<");
				}
				if (NetString.HasValue(mParent.mLastName))
				{
					var likeValue = $"'{mParent.mLastName}%'";
					dbConditions.Add(CVVisit.JoinLastName, likeValue, "Like");
				}
				if (NetString.HasValue(mParent.mMiddleName))
				{
					var likeValue = $"'{mParent.mMiddleName}%'";
					dbConditions.Add(CVVisit.JoinMiddleName, likeValue, "Like");
				}
				if (NetString.HasValue(mParent.mFirstName))
				{
					var likeValue = $"'{mParent.mFirstName}%'";
					dbConditions.Add(CVVisit.JoinFirstName, likeValue, "Like");
				}
				dbFilters.Add(dbFilter);
				dataRecords = cvVisitManager.LoadWithJoins(mParent.mFacilityID
					, dbFilters);
			}
			else
			{
				dataRecords = cvVisitManager.LoadWithJoins(mParent.mFacilityID);
			}
			DataRetrieveCVVisit(dataRecords);
			mParent.Cursor = Cursors.Default;
		}

		// Clear the list filters.
		internal void DoClearFilters()
		{
			mParent.mEndDateString = null;
			mParent.mLastName = null;
			mParent.mFirstName = null;
			mParent.ClearFiltersButton.Visible = false;
		}
		#endregion

		#region Class Data

		private readonly LJCDataGrid mCVVisitGrid;
		private readonly CVRManagers mManagers;
		private readonly CVVisitList mParent;
		#endregion
	}
}
