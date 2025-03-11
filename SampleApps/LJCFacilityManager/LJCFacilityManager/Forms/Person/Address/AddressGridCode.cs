// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AddressGridCode.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class AddressGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal AddressGridCode(PersonModule parent)
		{
			mParent = parent;
			mAddressGrid = mParent.RelationGrid;
			mPersonGrid = mParent.PersonGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveAddress()
		{
			Addresses records;

			mParent.Cursor = Cursors.WaitCursor;
			mAddressGrid.LJCRowsClear();

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				string inValues = GetInValues(parentID);
				if (inValues != null)
				{
					// Added joins to create City, State Zip.
					var addressManager = mManagers.AddressManager;
					DbJoins dbJoins = addressManager.GetLoadJoins();
					var dbFilters = addressManager.GetLoadFilters("Address.ID", inValues);
					if (dbFilters != null)
					{
						records = addressManager.Load(filters: dbFilters, joins: dbJoins);

						if (NetCommon.HasItems(records))
						{
							foreach (Address record in records)
							{
								RowAddAddress(record);
							}
						}
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(PersonModule.Change.Address);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddAddress(Address dataRecord)
		{
			LJCGridRow retValue;

			retValue = mAddressGrid.LJCRowAdd();
			SetStoredValuesAddress(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mAddressGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateAddress(Address dataRecord)
		{
			if (mAddressGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesAddress(gridRow, dataRecord);
				gridRow.LJCSetValues(mAddressGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesAddress(LJCGridRow row, Address dataRecord)
		{
			row.LJCSetInt32(Address.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectAddress(Address dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mAddressGrid.Rows)
				{
					rowID = row.LJCGetInt32(Address.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mAddressGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
			}
			return retValue;
		}

		/// <summary>
		/// Gets the child ID values.
		/// </summary>
		/// <param name="personID">The parent ID value.</param>
		/// <returns>The child ID list.</returns>
		private string GetInValues(int personID)
		{
			StringBuilder builder;
			PersonAddresses list;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ PersonAddress.ColumnPersonID, personID }
			};
			list = mManagers.PersonAddressManager.Load(keyColumns);
			if (NetCommon.HasItems(list))
			{
				builder = new StringBuilder(64);
				foreach (PersonAddress record in list)
				{
					if (builder.Length == 0)
					{
						builder.Append("(");
					}
					else
					{
						builder.Append(", ");
					}
					builder.Append(record.AddressID);
				}
				if (builder.Length > 0)
				{
					builder.Append(")");
				}
				retValue = builder.ToString();
			}
			return retValue;
		}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoAddAddress()
		{
			AddressList addressList;
			Address addressRecord = null;
			int personID;
			int addressID;

			if (mPersonGrid.CurrentRow is LJCGridRow row)
			{
				// Get associated data.
				personID = row.LJCGetInt32(Person.ColumnID);

				LJCGridRow childRow = mAddressGrid.CurrentRow as LJCGridRow;
				addressID = childRow.LJCGetInt32(Address.ColumnID);

				// Get current record to seed the selection list.
				if (addressID > 0)
				{
					var address = mManagers.AddressManager.RetrieveJoinWithID(addressID);
					addressRecord = new Address()
					{
						CodeTypeID = address.CodeTypeID
					};
				}

				addressList = new AddressList()
				{
					LJCIsSelect = true,
					LJCParentName = row.LJCGetString(Person.ColumnFullName),
					LJCHelpFile = mParent.LJCHelpFile,
					LJCHelpPageList = "AddressList.htm",
					LJCHelpPageDetail = "AddressDetail.htm",
					LJCSelectedRecord = addressRecord
				};
				addressList.ShowDialog();

				if (addressList.DialogResult == DialogResult.OK
					&& addressList.LJCSelectedRecord != null)
				{
					DoAssociatedData(addressList.LJCSelectedRecord, personID);
				}
			}
		}

		// Saves the parent associated data.
		private void DoAssociatedData(Address selectedRecord, int personID)
		{
			PersonAddress personAddressRecord;

			if (RowSelectAddress(selectedRecord))
			{
				RowUpdateAddress(selectedRecord);
			}
			else
			{
				// Add record to PersonAddress.
				personAddressRecord = new PersonAddress()
				{
					PersonID = personID,
					AddressID = selectedRecord.ID
				};
				mManagers.PersonAddressManager.Add(personAddressRecord);
				if (mManagers.PersonAddressManager.AffectedCount > 0)
				{
					LJCGridRow addedRow = RowAddAddress(selectedRecord);
					mAddressGrid.LJCSetCurrentRow(addedRow);
					mParent.TimedChange(PersonModule.Change.Address);
				}
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditAddress()
		{
			AddressDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow
				&& mAddressGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Address.ColumnID);
				string parentName = parentRow.LJCGetString(Person.ColumnFullName);

				detail = new AddressDetail()
				{
					LJCID = id,
					LJCParentName = parentName,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "Address.htm",
				};
				detail.LJCChange += new EventHandler<EventArgs>(AddressDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void AddressDetail_Change(object sender, EventArgs e)
		{
			AddressDetail detail;
			LJCGridRow row;

			detail = sender as AddressDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateAddress(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddAddress(detail.LJCRecord);
				mAddressGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(PersonModule.Change.Address);
			}
		}

		/// <summary>
		/// Deletes the selected row.
		/// </summary>
		internal void DoRemoveAddress()
		{
			LJCGridRow parentRow;
			string title;
			string message;

			parentRow = mPersonGrid.CurrentRow as LJCGridRow;
			if (mAddressGrid.CurrentRow is LJCGridRow row)
			{
				title = "Remove Confirmation";
				message = "Are you sure you want to remove the selected item?";
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ PersonAddress.ColumnPersonID
							, parentRow.LJCGetInt32(Person.ColumnID) },
						{ PersonAddress.ColumnAddressID
							, row.LJCGetInt32(Address.ColumnID) }
					};
					mManagers.PersonAddressManager.Delete(keyColumns);
					if (mManagers.PersonAddressManager.AffectedCount < 1)
					{
						message = "Unable to remove the selected item.";
						MessageBox.Show(message, "Remove Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mAddressGrid.Rows.Remove(row);
						mParent.TimedChange(PersonModule.Change.Address);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshAddress()
		{
			Address record;
			int id = 0;

			if (mAddressGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Address.ColumnID);
			}
			DataRetrieveAddress();

			// Select the original row.
			if (id > 0)
			{
				record = new Address()
				{
					ID = id
				};
				RowSelectAddress(record);
			}
		}
		#endregion

		#region Class Data

		private readonly PersonModule mParent;
		private readonly LJCDataGrid mAddressGrid;
		private readonly LJCDataGrid mPersonGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
