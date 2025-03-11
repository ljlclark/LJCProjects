// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EquipmentGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class EquipmentGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal EquipmentGridCode(FacilityModule parent)
		{
			mParent = parent;
			mEquipmentGrid = mParent.EquipmentGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveEquipment()
		{
			EquipmentItems records;

			mParent.Cursor = Cursors.WaitCursor;
			mEquipmentGrid.LJCRowsClear();

			var equipmentManager = mManagers.EquipmentManager;
			DbJoins dbJoins = equipmentManager.GetLoadJoins();
			records = equipmentManager.Load(joins: dbJoins);

			if (NetCommon.HasItems(records))
			{
				foreach (Equipment record in records)
				{
					RowAddEquipment(record);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(FacilityModule.Change.Equipment);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddEquipment(Equipment dataRecord)
		{
			LJCGridRow retValue;

			retValue = mEquipmentGrid.LJCRowAdd();
			SetStoredValuesEquipment(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mEquipmentGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateEquipment(Equipment dataRecord)
		{
			if (mEquipmentGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesEquipment(row, dataRecord);
				row.LJCSetValues(mEquipmentGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesEquipment(LJCGridRow row, Equipment dataRecord)
		{
			row.LJCSetInt32(Equipment.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectEquipment(Equipment dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mEquipmentGrid.Rows)
				{
					rowID = row.LJCGetInt32(Equipment.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mEquipmentGrid.LJCSetCurrentRow(row.Index, true);
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
		internal void DoNewEquipment()
		{
			EquipmentDetail detail;

			detail = new EquipmentDetail()
			{
				LJCHelpFileName = mParent.LJCHelpFile,
				LJCHelpPageName = "EquipmentDetail.htm"
			};
			detail.LJCChange += new EventHandler<EventArgs>(EquipmentDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditEquipment()
		{
			EquipmentDetail detail;

			if (mEquipmentGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Equipment.ColumnID);

				detail = new EquipmentDetail()
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "EquipmentDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(EquipmentDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void EquipmentDetail_Change(object sender, EventArgs e)
		{
			EquipmentDetail detail;
			LJCGridRow row;

			detail = sender as EquipmentDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateEquipment(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddEquipment(detail.LJCRecord);
				mEquipmentGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(FacilityModule.Change.Equipment);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteEquipment()
		{
			string title;
			string message;

			if (mEquipmentGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Equipment.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Equipment.ColumnID, id }
					};
					mManagers.EquipmentManager.Delete(keyColumns);
					if (mManagers.EquipmentManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mEquipmentGrid.Rows.Remove(row);
						mParent.TimedChange(FacilityModule.Change.Equipment);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshEquipment()
		{
			Equipment record;
			int id = 0;

			if (mEquipmentGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Equipment.ColumnID);
			}
			DataRetrieveEquipment();

			// Select the original row.
			if (id > 0)
			{
				record = new Equipment()
				{
					ID = id
				};
				RowSelectEquipment(record);
			}
		}
		#endregion

		#region Class Data

		private readonly FacilityModule mParent;
		private readonly LJCDataGrid mEquipmentGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
