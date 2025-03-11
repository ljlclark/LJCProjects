// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class FacilityGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal FacilityGridCode(FacilityModule parent)
		{
			mParent = parent;
			mFacilityGrid = mParent.FacilityGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveFacility()
		{
			Facilities records;

			mParent.Cursor = Cursors.WaitCursor;
			mFacilityGrid.LJCRowsClear();

			var facilityManager = mManagers.FacilityDbManager;
			DbJoins dbJoins = facilityManager.GetLoadJoins();
			facilityManager.SetOrderByCode();
			records = facilityManager.Load(joins: dbJoins);

			if (NetCommon.HasItems(records))
			{
				foreach (Facility record in records)
				{
					RowAddFacility(record);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(FacilityModule.Change.Facility);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddFacility(Facility dataRecord)
		{
			LJCGridRow retValue;

			retValue = mFacilityGrid.LJCRowAdd();
			SetStoredValuesFacility(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mFacilityGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateFacility(Facility dataRecord)
		{
			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesFacility(row, dataRecord);
				row.LJCSetValues(mFacilityGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesFacility(LJCGridRow row, Facility dataRecord)
		{
			row.LJCSetInt32(Facility.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
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
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoNewFacility()
		{
			FacilityDetail detail;

			detail = new FacilityDetail()
			{
				LJCHelpFileName = mParent.LJCHelpFile,
				LJCHelpPageName = "FacilityDetail.htm"
			};
			detail.LJCChange += new EventHandler<EventArgs>(FacilityDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditFacility()
		{
			FacilityDetail detail;

			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Facility.ColumnID);

				detail = new FacilityDetail()
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "FacilityDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(FacilityDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void FacilityDetail_Change(object sender, EventArgs e)
		{
			FacilityDetail detail;
			Facility record;
			LJCGridRow row;

			detail = sender as FacilityDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateFacility(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddFacility(record);
				mFacilityGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(FacilityModule.Change.Facility);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
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
					// Data from list items.
					int id = row.LJCGetInt32(Facility.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Facility.ColumnID, id }
					};
					mManagers.FacilityDbManager.Delete(keyColumns);
					if (mManagers.FacilityDbManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mFacilityGrid.Rows.Remove(row);
						mParent.TimedChange(FacilityModule.Change.Facility);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshFacility()
		{
			Facility record;
			int id = 0;

			if (mFacilityGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Facility.ColumnID);
			}
			DataRetrieveFacility();

			// Select the original row.
			if (id > 0)
			{
				record = new Facility()
				{
					ID = id
				};
				RowSelectFacility(record);
			}
		}
		#endregion

		#region Class Data

		private readonly FacilityModule mParent;
		private readonly LJCDataGrid mFacilityGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
