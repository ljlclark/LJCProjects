// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FixtureGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class FixtureGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal FixtureGridCode(FacilityModule parent)
		{
			mParent = parent;
			mUnitGrid = mParent.UnitGrid;
			mFixtureGrid = mParent.FixtureGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveFixture()
		{
			Fixtures records;

			mParent.Cursor = Cursors.WaitCursor;
			mFixtureGrid.LJCRowsClear();

			if (mUnitGrid.CurrentRow is LJCGridRow parentRow)
			{
				var keyColumns = new DbColumns()
				{
					{ Fixture.ColumnUnitID, parentRow.LJCGetInt32(Unit.ColumnID) }
				};
				var fixtureManager = mManagers.FixtureManager;
				DbJoins dbJoins = fixtureManager.GetLoadJoins();
				fixtureManager.SetOrderByCode();
				records = fixtureManager.Load(keyColumns, joins: dbJoins);

				if (NetCommon.HasItems(records))
				{
					foreach (Fixture record in records)
					{
						RowAddFixture(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(FacilityModule.Change.Fixture);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddFixture(Fixture dataRecord)
		{
			LJCGridRow retValue;

			retValue = mFixtureGrid.LJCRowAdd();
			SetStoredValuesFixture(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mFixtureGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateFixture(Fixture dataRecord)
		{
			if (mFixtureGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesFixture(gridRow, dataRecord);
				gridRow.LJCSetValues(mFixtureGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesFixture(LJCGridRow row, Fixture dataRecord)
		{
			row.LJCSetInt32(Fixture.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectFixture(Fixture dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mFixtureGrid.Rows)
				{
					rowID = row.LJCGetInt32(Fixture.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mFixtureGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewFixture()
		{
			FixtureDetail detail;

			if (mUnitGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Unit.ColumnID);

				detail = new FixtureDetail()
				{
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "FixtureDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(FixtureDetail_Change);
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditFixture()
		{
			FixtureDetail detail;

			if (mUnitGrid.CurrentRow is LJCGridRow parentRow
				&& mFixtureGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Fixture.ColumnID);
				int parentID = parentRow.LJCGetInt32(Unit.ColumnID);

				detail = new FixtureDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "FixtureDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(FixtureDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void FixtureDetail_Change(object sender, EventArgs e)
		{
			FixtureDetail detail;
			Fixture record;
			LJCGridRow row;

			detail = sender as FixtureDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateFixture(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddFixture(record);
				mFixtureGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(FacilityModule.Change.Fixture);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteFixture()
		{
			string title;
			string message;

			if (mFixtureGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Fixture.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Fixture.ColumnID, id }
					};
					mManagers.FixtureManager.Delete(keyColumns);
					if (mManagers.FixtureManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mFixtureGrid.Rows.Remove(row);
						mParent.TimedChange(FacilityModule.Change.Fixture);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshFixture()
		{
			Fixture record;
			int id = 0;

			if (mFixtureGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Fixture.ColumnID);
			}
			DataRetrieveFixture();

			// Select the original row.
			if (id > 0)
			{
				record = new Fixture()
				{
					ID = id
				};
				RowSelectFixture(record);
			}
		}
		#endregion

		#region Class Data

		private readonly FacilityModule mParent;
		private readonly LJCDataGrid mUnitGrid;
		private readonly LJCDataGrid mFixtureGrid;
		private readonly LJCFacilityManagerDAL.FacilityManagers mManagers;
		#endregion
	}
}
