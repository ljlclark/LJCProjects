// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class UnitGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitGridCode(FacilityModule parent)
		{
			mParent = parent;
			mFacilityGrid = mParent.FacilityGrid;
			mUnitGrid = mParent.UnitGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveUnit()
		{
			Units records;
			int facilityID;

			mParent.Cursor = Cursors.WaitCursor;
			mUnitGrid.LJCRowsClear();

			if (mFacilityGrid.CurrentRow is LJCGridRow parentRow)
			{
				facilityID = parentRow.LJCGetInt32(Facility.ColumnID);
				records = mManagers.UnitManager.LoadWithParentID(facilityID);

				if (NetCommon.HasItems(records))
				{
					foreach (Unit record in records)
					{
						RowAddUnit(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(FacilityModule.Change.Unit);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddUnit(Unit dataRecord)
		{
			LJCGridRow retValue;

			retValue = mUnitGrid.LJCRowAdd();
			SetStoredValuesUnit(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mUnitGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateUnit(Unit dataRecord)
		{
			if (mUnitGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesUnit(gridRow, dataRecord);
				gridRow.LJCSetValues(mUnitGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesUnit(LJCGridRow row, Unit dataRecord)
		{
			row.LJCSetInt32(Unit.ColumnID, dataRecord.ID);
			row.LJCSetInt32(Unit.ColumnPersonID, dataRecord.PersonID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectUnit(Unit dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mUnitGrid.Rows)
				{
					rowID = row.LJCGetInt32(Unit.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mUnitGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewUnit()
		{
			UnitDetail detail;

			if (mFacilityGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Facility.ColumnID);

				detail = new UnitDetail()
				{
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "UnitDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(UnitDetail_Change);
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditUnit()
		{
			UnitDetail detail;

			if (mFacilityGrid.CurrentRow is LJCGridRow parentRow
				&& mUnitGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Unit.ColumnID);
				int parentID = parentRow.LJCGetInt32(Facility.ColumnID);

				detail = new UnitDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "UnitDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(UnitDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void UnitDetail_Change(object sender, EventArgs e)
		{
			UnitDetail detail;
			Unit record;
			LJCGridRow row;

			detail = sender as UnitDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateUnit(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddUnit(record);
				mUnitGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(FacilityModule.Change.Unit);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteUnit()
		{
			string title;
			string message;

			if (mUnitGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Unit.ColumnID);

					var unitManager = mManagers.UnitManager;
					var keyColumns = unitManager.GetIDKey(id);
					unitManager.Delete(keyColumns);
					if (unitManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mUnitGrid.Rows.Remove(row);
						mParent.TimedChange(FacilityModule.Change.Unit);
					}
				}
			}
		}

		/// <summary>
		/// Displays a detail dialog to view an existing record.
		/// </summary>
		internal void DoViewOccupant()
		{
			PersonDetail detail;
			int personID;

			if (mUnitGrid.CurrentRow is LJCGridRow row)
			{
				personID = row.LJCGetInt32(Unit.ColumnPersonID);
				if (personID > 0)
				{
					detail = new PersonDetail()
					{
						LJCID = personID,
						LJCReadOnly = true
					};
					detail.ShowDialog();
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshUnit()
		{
			Unit record;
			int id = 0;

			if (mUnitGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Unit.ColumnID);
			}
			DataRetrieveUnit();

			// Select the original row.
			if (id > 0)
			{
				record = new Unit()
				{
					ID = id
				};
				RowSelectUnit(record);
			}
		}
		#endregion

		#region Class Data

		private readonly FacilityModule mParent;
		private readonly LJCDataGrid mFacilityGrid;
		private readonly LJCDataGrid mUnitGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
