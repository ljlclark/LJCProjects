// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitPersonGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	class UnitPersonGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitPersonGridCode(PersonModule parent)
		{
			mParent = parent;
			mUnitPersonGrid = mParent.UnitPersonGrid;
			mPersonGrid = mParent.PersonGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveUnitPerson()
		{
			UnitPersons records;

			mParent.Cursor = Cursors.WaitCursor;
			mUnitPersonGrid.LJCRowsClear();

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				//  // Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ UnitPerson.ColumnPersonID, parentID }
				};
				var unitPersonManager = mManagers.UnitPersonManager;
				DbJoins dbJoins = unitPersonManager.GetLoadJoins();
				records = unitPersonManager.Load(keyColumns, joins: dbJoins);

				if (NetCommon.HasItems(records))
				{
					foreach (UnitPerson record in records)
					{
						RowAddUnitPerson(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(PersonModule.Change.UnitPerson);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddUnitPerson(UnitPerson dataRecord)
		{
			LJCGridRow retValue;

			retValue = mUnitPersonGrid.LJCRowAdd();
			SetStoredValuesUnitPerson(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mUnitPersonGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateUnitPerson(UnitPerson dataRecord)
		{
			if (mUnitPersonGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesUnitPerson(gridRow, dataRecord);
				gridRow.LJCSetValues(mUnitPersonGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesUnitPerson(LJCGridRow row, UnitPerson dataRecord)
		{
			row.LJCSetInt32(UnitPerson.ColumnUnitID, dataRecord.UnitID);
			row.LJCSetInt32(UnitPerson.ColumnPersonID, dataRecord.PersonID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectUnitPerson(UnitPerson dataRecord)
		{
			int rowUnitID;
			int rowPersonID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mUnitPersonGrid.Rows)
				{
					rowUnitID = row.LJCGetInt32(UnitPerson.ColumnUnitID);
					rowPersonID = row.LJCGetInt32(UnitPerson.ColumnPersonID);
					if (rowUnitID == dataRecord.UnitID
						&& rowPersonID == dataRecord.PersonID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mUnitPersonGrid.LJCSetCurrentRow(row, true);
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
			UnitPersonDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				detail = new UnitPersonDetail()
				{
					LJCPersonID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "UnitDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(UnitPersonDetail_Change);
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditUnit()
		{
			UnitPersonDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow
				&& mUnitPersonGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);
				int unitID = row.LJCGetInt32(UnitPerson.ColumnUnitID);

				detail = new UnitPersonDetail()
				{
					LJCPersonID = parentID,
					LJCUnitID = unitID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "UnitDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(UnitPersonDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void UnitPersonDetail_Change(object sender, EventArgs e)
		{
			UnitPersonDetail detail;
			UnitPerson record;
			LJCGridRow row;

			detail = sender as UnitPersonDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateUnitPerson(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddUnitPerson(record);
				mUnitPersonGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(PersonModule.Change.UnitPerson);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteUnit()
		{
			string title;
			string message;

			if (mUnitPersonGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ UnitPerson.ColumnUnitID, row.LJCGetInt32(UnitPerson.ColumnUnitID) },
						{ UnitPerson.ColumnPersonID
							, row.LJCGetInt32(UnitPerson.ColumnPersonID) }
					};
					mManagers.UnitPersonManager.Delete(keyColumns);
					if (mManagers.UnitPersonManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mUnitPersonGrid.Rows.Remove(row);
						mParent.TimedChange(PersonModule.Change.UnitPerson);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshUnit()
		{
			UnitPerson record;
			int unitID = 0;
			int personID = 0;

			if (mUnitPersonGrid.CurrentRow is LJCGridRow row)
			{
				unitID = row.LJCGetInt32(UnitPerson.ColumnUnitID);
				personID = row.LJCGetInt32(UnitPerson.ColumnPersonID);
			}
			DataRetrieveUnitPerson();

			// Select the original row.
			if (unitID > 0
				&& personID > 0)
			{
				record = new UnitPerson()
				{
					UnitID = unitID,
					PersonID = personID
				};
				RowSelectUnitPerson(record);
			}
		}
		#endregion

		#region Class Data

		private readonly PersonModule mParent;
		private readonly LJCDataGrid mUnitPersonGrid;
		private readonly LJCDataGrid mPersonGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
