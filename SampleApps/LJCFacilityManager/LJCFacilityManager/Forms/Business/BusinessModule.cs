// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessModule.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBMessage;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	// Note: Module assemblies are loaded dynamically with reflection. Any changes
	//       to this code must be compiled and copied to the host program folder
	//       before they are available. See the build UpdatePost.cmd file.

	// The tab composite user control.
	/// <include path='items/ModuleA/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
	public partial class BusinessModule : UserControl
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessModule()
		{
			Cursor = Cursors.WaitCursor;
			InitializeComponent();
			string errorText = FacilityCommon.CheckDependencies();
			if (NetString.HasValue(errorText))
			{
				MessageBox.Show(errorText);
			}

			// Set default class data.
			LJCHelpFile = "FacilityManager.chm";
			Cursor = Cursors.Default;
		}
		#endregion

		#region Data Methods

		#region Business

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveBusiness()
		{
			Businesses records;

			Cursor = Cursors.WaitCursor;
			BusinessGrid.LJCRowsClear();

			var businessManager = Managers.BusinessManager;
			DbJoins dbJoins = businessManager.GetLoadJoins();
			businessManager.SetOrderByName();
			records = businessManager.Load(joins: dbJoins);

			if (NetCommon.HasItems(records))
			{
				foreach (Business record in records)
				{
					RowAddBusiness(record);
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Business);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddBusiness(Business dataRecord)
		{
			LJCGridRow retValue;

			retValue = BusinessGrid.LJCRowAdd();
			SetStoredValuesBusiness(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(BusinessGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateBusiness(Business dataRecord)
		{
			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesBusiness(row, dataRecord);
				row.LJCSetValues(BusinessGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesBusiness(LJCGridRow row, Business dataRecord)
		{
			row.LJCSetInt32(Business.ColumnID, dataRecord.ID);
			row.LJCSetString(Business.ColumnName, dataRecord.Name);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectBusiness(Business dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in BusinessGrid.Rows)
				{
					rowID = row.LJCGetInt32(Person.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						BusinessGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				Cursor = Cursors.Default;
			}
			return retValue;
		}
		#endregion

		#region Contact

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveContact()
		{
			Persons records;
			int businessID;

			Cursor = Cursors.WaitCursor;
			ContactGrid.LJCRowsClear();

			if (BusinessGrid.CurrentRow is LJCGridRow parentRow)
			{
				businessID = parentRow.LJCGetInt32(Business.ColumnID);
				string inValues = GetInValuesBusinessPerson(businessID);
				if (inValues != null)
				{
					var addressManager = Managers.AddressManager;
					var personManager = Managers.PersonManager;
					var dbFilters = addressManager.GetLoadFilters("Person.ID", inValues);
					DbJoins dbJoins = personManager.GetLoadJoins();
					personManager.SetOrderByFirstLast();
					records = personManager.Load(joins: dbJoins, filters: dbFilters);

					if (NetCommon.HasItems(records))
					{
						foreach (Person record in records)
						{
							RowAddContact(record);
						}
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Contact);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddContact(Person dataRecord)
		{
			LJCGridRow retValue;

			retValue = ContactGrid.LJCRowAdd();
			SetStoredValuesContact(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(ContactGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateContact(Person dataRecord)
		{
			if (ContactGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValuesContact(gridRow, dataRecord);
				gridRow.LJCSetValues(ContactGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesContact(LJCGridRow row, Person dataRecord)
		{
			row.LJCSetInt32(Person.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectContact(Person dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in ContactGrid.Rows)
				{
					rowID = row.LJCGetInt32(Person.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						ContactGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				Cursor = Cursors.Default;
			}
			return retValue;
		}

		/// <summary>
		/// Gets the child ID values.
		/// </summary>
		/// <param name="businessID">The parent ID value.</param>
		/// <returns>The child ID list.</returns>
		private string GetInValuesBusinessPerson(int businessID)
		{
			StringBuilder builder;
			BusinessPersons list;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ BusinessPerson.ColumnBusinessID, businessID }
			};
			list = Managers.BusinessPersonManager.Load(keyColumns);
			if (NetCommon.HasItems(list))
			{
				builder = new StringBuilder(64);
				foreach (BusinessPerson record in list)
				{
					if (builder.Length == 0)
					{
						builder.Append("(");
					}
					else
					{
						builder.Append(", ");
					}
					builder.Append(record.PersonID);
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

		#region Address

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DataRetrieveAddress()
		{
			Addresses records;
			int businessID;

			Cursor = Cursors.WaitCursor;
			AddressGrid.LJCRowsClear();

			if (BusinessGrid.CurrentRow is LJCGridRow parentRow)
			{
				businessID = parentRow.LJCGetInt32(Business.ColumnID);
				string inValues = GetInValuesBusinessAddress(businessID);
				if (inValues != null)
				{
					var addressManager = Managers.AddressManager;
					var dbFilters = addressManager.GetLoadFilters("ID", inValues);
					records = addressManager.Load(filters: dbFilters);

					if (NetCommon.HasItems(records))
					{
						foreach (Address record in records)
						{
							RowAddAddress(record);
						}
					}
				}
			}
			Cursor = Cursors.Default;
			DoChange(Change.Address);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddAddress(Address dataRecord)
		{
			LJCGridRow retValue;

			retValue = AddressGrid.LJCRowAdd();
			SetStoredValuesAddress(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(AddressGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateAddress(Address dataRecord)
		{
			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesAddress(row, dataRecord);
				row.LJCSetValues(AddressGrid, dataRecord);
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
				Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in AddressGrid.Rows)
				{
					rowID = row.LJCGetInt32(Address.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						AddressGrid.LJCSetCurrentRow(row, true);
						retValue = true;
						break;
					}
				}
				Cursor = Cursors.Default;
			}
			return retValue;
		}

		/// <summary>
		/// Gets the child ID values.
		/// </summary>
		/// <param name="businessID">The parent ID value.</param>
		/// <returns>The child ID list.</returns>
		public string GetInValuesBusinessAddress(int businessID)
		{
			StringBuilder builder;
			BusinessAddresses list;
			string retValue = null;

			var keyColumns = new DbColumns()
			{
				{ BusinessAddress.ColumnBusinessID, businessID }
			};
			list = Managers.BusinessAddressManager.Load(keyColumns);
			if (NetCommon.HasItems(list))
			{
				builder = new StringBuilder(64);
				foreach (BusinessAddress record in list)
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
		#endregion

		#region Action Methods

		#region Business

		// Displays a detail dialog for a new record.
		/// <include path='items/DoNew/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoNewBusiness()
		{
			BusinessDetail detail;

			detail = new BusinessDetail()
			{
				LJCHelpFileName = LJCHelpFile,
				LJCHelpPageName = "BusinessDetail.htm"
			};
			detail.LJCChange += new EventHandler<EventArgs>(BusinessDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditBusiness()
		{
			BusinessDetail detail;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Business.ColumnID);

				detail = new BusinessDetail()
				{
					LJCID = id,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = "BusinessDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(BusinessDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		void BusinessDetail_Change(object sender, EventArgs e)
		{
			BusinessDetail detail;
			LJCGridRow row;

			detail = sender as BusinessDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateBusiness(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddBusiness(detail.LJCRecord);
				BusinessGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.Business);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoDeleteBusiness()
		{
			string title;
			string message;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Business.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Business.ColumnID, id }
					};
					Managers.BusinessManager.Delete(keyColumns);
					if (Managers.BusinessManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						BusinessGrid.Rows.Remove(row);
						TimedChange(Change.Business);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshBusiness()
		{
			Business record;
			int id = 0;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Person.ColumnID);
			}
			DataRetrieveBusiness();

			// Select the original row.
			if (id > 0)
			{
				record = new Business()
				{
					ID = id
				};
				RowSelectBusiness(record);
			}
		}
		#endregion

		#region Contact

		/// <summary>
		/// Displays a detail dialog for a new record.
		/// </summary>
		private void DoAddContact()
		{
			PersonList personList;
			int businessID;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				// Get associated data.
				businessID = row.LJCGetInt32(Business.ColumnID);

				personList = new PersonList()
				{
					LJCIsSelect = true,
					LJCHelpFile = LJCHelpFile,
					LJCHelpPageList = "ContactList.htm",
					LJCHelpPageDetail = "ContactDetail.htm"
				};
				personList.ShowDialog();

				if (personList.DialogResult == DialogResult.OK
					&& personList.LJCSelectedRecord != null)
				{
					DoAssociatedData(personList.LJCSelectedRecord, businessID);
				}
			}
		}

		// Saves the parent associated data.
		private void DoAssociatedData(Person selectedRecord, int businessID)
		{
			BusinessPerson businessPersonRecord;

			if (RowSelectContact(selectedRecord))
			{
				RowUpdateContact(selectedRecord);
			}
			else
			{
				// Add record to BusinessPerson.
				businessPersonRecord = new BusinessPerson()
				{
					BusinessID = businessID,
					PersonID = selectedRecord.ID
				};
				Managers.BusinessPersonManager.Add(businessPersonRecord);
				if (Managers.BusinessPersonManager.AffectedCount > 0)
				{
					// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
					LJCGridRow addedRow = RowAddContact(selectedRecord);
					ContactGrid.LJCSetCurrentRow(addedRow);
					TimedChange(Change.Contact);
				}
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditContact()
		{
			PersonDetail detail;

			if (BusinessGrid.CurrentRow is LJCGridRow parentRow
				&& ContactGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Person.ColumnID);

				detail = new PersonDetail()
				{
					LJCID = id,
					LJCLabelText = "Business",
					LJCName = parentRow.LJCGetString(Business.ColumnName),
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = "ContactDetail.htm",
				};
				detail.LJCChange += new EventHandler<EventArgs>(ContactDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void ContactDetail_Change(object sender, EventArgs e)
		{
			PersonDetail detail;
			Person record;
			LJCGridRow row;

			detail = sender as PersonDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateContact(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddContact(record);
				ContactGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.Contact);
			}
		}

		/// <summary>Removes the selected row.</summary>
		private void DoRemoveContact()
		{
			string title;
			string message;

			// If child grid.
			if (BusinessGrid.CurrentRow is LJCGridRow parentRow
				&& ContactGrid.CurrentRow is LJCGridRow row)
			{
				title = "Remove Confirmation";
				message = "Are you sure you want to remove the selected item?";
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ BusinessPerson.ColumnBusinessID
							, parentRow.LJCGetInt32(Business.ColumnID) },
						{ BusinessPerson.ColumnPersonID, row.LJCGetInt32(Person.ColumnID) }
					};
					Managers.BusinessPersonManager.Delete(keyColumns);
					if (Managers.BusinessPersonManager.AffectedCount < 1)
					{
						message = "Unable to remove the selected item.\r\n"
							+ "There may be attached items or referencing items.";
						MessageBox.Show(message, "Remove Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						ContactGrid.Rows.Remove(row);
						TimedChange(Change.Contact);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshContact()
		{
			Person record;
			int id = 0;

			if (ContactGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Person.ColumnID);
			}
			DataRetrieveContact();

			// Select the original row.
			if (id > 0)
			{
				record = new Person()
				{
					ID = id
				};
				RowSelectContact(record);
			}
		}
		#endregion

		#region Address

		/// <summary>
		/// Displays a detail dialog for a new record.
		/// </summary>
		private void DoAddAddress()
		{
			AddressList addressList;
			Address addressRecord = null;
			int businessID;
			int addressID;

			if (BusinessGrid.CurrentRow is LJCGridRow row)
			{
				businessID = row.LJCGetInt32(Business.ColumnID);

				LJCGridRow childRow = AddressGrid.CurrentRow as LJCGridRow;
				addressID = childRow.LJCGetInt32(Address.ColumnID);

				// Get current record to seed the selection list.
				if (addressID > 0)
				{
					var address = Managers.AddressManager.RetrieveJoinWithID(addressID);
					addressRecord = new Address()
					{
						CodeTypeID = address.CodeTypeID
					};
				}

				addressList = new AddressList()
				{
					LJCIsSelect = true,
					LJCHelpFile = LJCHelpFile,
					LJCHelpPageList = "BusAddressList.htm",
					LJCHelpPageDetail = "BusAddressDetail.htm",
					LJCSelectedRecord = addressRecord
				};
				addressList.ShowDialog();
				if (addressList.DialogResult == DialogResult.OK
					&& addressList.LJCSelectedRecord != null)
				{
					DoAssociatedData(addressList.LJCSelectedRecord, businessID);
				}
			}
		}

		// Saves the parent associated data.
		private void DoAssociatedData(Address selectedRecord, int businessID)
		{
			BusinessAddress businessAddressRecord;

			if (RowSelectAddress(selectedRecord))
			{
				RowUpdateAddress(selectedRecord);
			}
			else
			{
				// Add record to BusinessAddress.
				businessAddressRecord = new BusinessAddress()
				{
					BusinessID = businessID,
					AddressID = selectedRecord.ID
				};
				Managers.BusinessAddressManager.Add(businessAddressRecord);
				if (Managers.BusinessAddressManager.AffectedCount > 0)
				{
					LJCGridRow addedRow = RowAddAddress(selectedRecord);
					AddressGrid.LJCSetCurrentRow(addedRow);
					TimedChange(Change.Address);
				}
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoEditAddress()
		{
			AddressDetail detail;

			if (BusinessGrid.CurrentRow is LJCGridRow parentRow
				&& AddressGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Address.ColumnID);
				string parentName = parentRow.LJCGetString(Business.ColumnName);

				detail = new AddressDetail()
				{
					LJCID = row.LJCGetInt32(Address.ColumnID),
					LJCParentName = parentName,
					LJCHelpFileName = LJCHelpFile,
					LJCHelpPageName = "BusAddressDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(AddressDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		void AddressDetail_Change(object sender, EventArgs e)
		{
			AddressDetail detail;
			Address record;
			LJCGridRow row;

			detail = sender as AddressDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateAddress(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddAddress(record);
				AddressGrid.LJCSetCurrentRow(row, true);
				TimedChange(Change.Address);
			}
		}

		/// <summary>
		/// Deletes the selected row.
		/// </summary>
		private void DoDeleteAddress()
		{
			string title;
			string message;

			LJCGridRow parentRow = BusinessGrid.CurrentRow as LJCGridRow;

			if (AddressGrid.CurrentRow is LJCGridRow row)
			{
				title = "Remove Confirmation";
				message = "Are you sure you want to remove the selected item?";
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var keyColumns = new DbColumns()
					{
						{ BusinessAddress.ColumnBusinessID
							, parentRow.LJCGetInt32(Business.ColumnID) },
						{ BusinessAddress.ColumnAddressID
							, row.LJCGetInt32(Address.ColumnID) }
					};
					Managers.BusinessAddressManager.Delete(keyColumns);
					if (Managers.BusinessAddressManager.AffectedCount < 1)
					{
						message = "Unable to remove the selected item.\r\n"
							+ "There may be attached items or referencing items.";
						MessageBox.Show(message, "Remove Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						AddressGrid.Rows.Remove(row);
						TimedChange(Change.Address);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void DoRefreshAddress()
		{
			Address record;
			int id = 0;

			if (AddressGrid.CurrentRow is LJCGridRow row)
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
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesFacility.Instance;
			mSettings = values.StandardSettings;

			// Initialize Class Data.
			Managers = new FacilityManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Configure controls.
			ContactToolNew.ToolTipText = "Add";
			ContactToolDelete.ToolTipText = "Remove";
			ContactMenuNew.Text = "&Add";
			ContactMenuDelete.Text = "&Remove";

			AddressToolNew.ToolTipText = "Add";
			AddressToolDelete.ToolTipText = "Remove";
			AddressMenuNew.Text = "&Add";
			AddressMenuDelete.Text = "&Remove";

			// Setup panel manager for main tab splitter.
			var _ = new LJCPanelManager(TabSplit, RelatedTabs, TileTabs);

			// Set initial control values.
			NetFile.CreateFolder("ControlValues");
			mControlValuesFileName = @"ControlValues\Business.xml";

			SetupGridBusiness();
			SetupGridContact();
			SetupGridAddress();
			StartItemChange();
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				BusinessSplit.SplitterWidth = 5;
				BusinessSplit.SplitterDistance = BusinessSplit.Height / 2;
				TabSplit.SplitterDistance = TabSplit.Width / 2;

				// Modify MainSplit.Panel1 controls.
				ListHelper.SetPanelControls(BusinessSplit.Panel1, null
					, BusinessToolPanel, BusinessGrid);

				// *** Related Tabs ***
				// Modify MainSplit.Panel2 Tabs control.
				ListHelper.SetPanelTabControl(BusinessSplit.Panel2, RelatedTabs);

				// Modify MainSplit.Panel2 TabPage controls.
				ListHelper.SetPageControls(ContactPage, null, ContactToolPanel
					, ContactGrid);

				// Modify MainSplit.Panel2 TabPage controls.
				ListHelper.SetPageControls(AddressPage, null, AddressToolPanel
					, AddressGrid);
			}
		}

		// Setup the grid columns.
		private void SetupGridBusiness()
		{
			BusinessGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == BusinessGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Business.ColumnName,
					Business.ColumnDescription
				};

				// Get the grid columns from the manager Data Definition.
				var columns = Managers.BusinessManager.GetColumns(propertyNames);

				// Setup the grid columns.
				BusinessGrid.LJCAddColumns(columns);
			}
		}

		// Setup the grid columns.
		private void SetupGridContact()
		{
			ContactGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == ContactGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Person.ColumnFirstName,
					Person.ColumnLastName
				};

				// Get the grid columns from the manager Data Definition.
				var columns = Managers.PersonManager.GetColumns(propertyNames);

				// Setup the grid columns.
				ContactGrid.LJCAddColumns(columns);
			}
		}

		// Setup the grid columns.
		private void SetupGridAddress()
		{
			AddressGrid.BackgroundColor = mSettings.BeginColor;

			// Setup default grid columns if no columns are defined.
			if (0 == AddressGrid.Columns.Count)
			{
				List<string> propertyNames = new List<string>
				{
					Address.PropertyProvinceName,
					Address.PropertyCityName,
					Address.ColumnStreet,
					Address.ColumnPostalCode,
					Address.ColumnCityStateZip
				};

				// Get the grid columns from the manager Data Definition.
				var gridColumns = Managers.AddressManager.GetColumns(propertyNames);

				// Setup the grid columns.
				AddressGrid.LJCAddColumns(gridColumns);
			}
		}

		// Saves the control values. 
		private void SaveControlValues()
		{
			Control parent = BusinessTabs.Parent;

			ControlValues controlValues = new ControlValues();

			// Save Grid Column values.
			BusinessGrid.LJCSaveColumnValues(controlValues);
			ContactGrid.LJCSaveColumnValues(controlValues);
			AddressGrid.LJCSaveColumnValues(controlValues);

			// Save Splitter values.
			controlValues.Add("BusinessSplit.SplitterDistance", 0, 0, 0, BusinessSplit.SplitterDistance);

			// Save Window values.
			// Tabs Parent is not this module form.
			if (parent != null
				&& parent.GetType().Name != Name)
			{
				controlValues.Add(Name, parent.Left, parent.Top
					, parent.Width, parent.Height);
			}

			NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
				, mControlValuesFileName);
		}

		// Restores the control values.
		private void RestoreControlValues()
		{
			ControlValue controlValue;
			Control parent = BusinessTabs.Parent;

			if (File.Exists(mControlValuesFileName))
			{
				ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
					, mControlValuesFileName) as ControlValues;

				if (ControlValues != null)
				{
					// Restore Window values.
					controlValue = ControlValues.LJCSearchName(Name);

					if (controlValue != null)
					{
						// Tabs Parent is not this module form.
						if (parent != null
								&& parent.GetType().Name != Name)
						{
							parent.Left = controlValue.Left;
							parent.Top = controlValue.Top;
							parent.Width = controlValue.Width;
							parent.Height = controlValue.Height;
						}
					}

					// Restore Splitter, Grid and other values.
					FormCommon.RestoreSplitDistance(BusinessSplit, ControlValues);

					BusinessGrid.LJCRestoreColumnValues(ControlValues);
					ContactGrid.LJCRestoreColumnValues(ControlValues);
					AddressGrid.LJCRestoreColumnValues(ControlValues);
				}
			}
		}

		/// <summary>Gets or sets the Allow Change value.</summary>
		public ControlValues ControlValues { get; set; }
		#endregion

		#region AppModule Implementation

		// Initializes the module.
		/// <include path='items/LJCInit/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void LJCInit()
		{
			InitializeControls();
		}

		// Returns a reference to the module tab control.
		/// <include path='items/LJCTabs/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public TabControl LJCTabs()
		{
			return BusinessTabs;
		}

		/// <summary>Gets the module assembly name.</summary>
		public string LJCProgramName
		{
			get { return "LJCFacilityManager.exe"; }
		}

		// Closes the current page.
		/// <include path='items/ClosePage/*' file='../../../CoreUtilities/LJCGenDoc/Common/Module.xml'/>
		public void ClosePage(TabPage tabPage)
		{
			LJCTabControl parentTabControl;

			// Set current page and invoke event.
			CloseTabPage = tabPage;
			OnPageClose();

			// Collapse tile panel if no tab pages left.
			parentTabControl = tabPage.Parent as LJCTabControl;
			parentTabControl?.LJCCloseEmptyPanel();
		}

		/// <summary>Gets or sets the close tab flag.</summary>
		public TabPage CloseTabPage { get; set; }

		/// <summary>Calls the PageClose event handlers.</summary>
		protected void OnPageClose()
		{
			PageClose?.Invoke(this, new EventArgs());
		}

		/// <summary>The page close event.</summary>
		public event EventHandler<EventArgs> PageClose;
		#endregion

		#region Item Change Processing

		// Execute the list and related item functions.
		internal void DoChange(Change change)
		{
			Cursor = Cursors.WaitCursor;
			switch (change)
			{
				case Change.Startup:
					ConfigureControls();
					RestoreControlValues();

					// Load first List.
					DataRetrieveBusiness();
					break;

				case Change.Business:
					DataRetrieveContact();
					DataRetrieveAddress();
					BusinessGrid.LJCSetLastRow();
					BusinessGrid.LJCSetCounter(BusinessCounter);
					break;

				case Change.Contact:
					ContactGrid.LJCSetLastRow();
					ContactGrid.LJCSetCounter(ContactCounter);
					break;

				case Change.Address:
					AddressGrid.LJCSetLastRow();
					AddressGrid.LJCSetCounter(AddressCounter);
					break;
			}
			SetControlState();
			Cursor = Cursors.Default;
		}

		// The ChangeType values.
		internal enum Change
		{
			Startup,
			Business,
			Contact,
			Address
		}

		#region Item Change Support

		// Start the Change processing.
		private void StartItemChange()
		{
			ChangeTimer = new ChangeTimer();
			ChangeTimer.ItemChange += ChangeTimer_ItemChange;
			TimedChange(Change.Startup);
		}

		// Change Event Handler
		private void ChangeTimer_ItemChange(object sender, EventArgs e)
		{
			Change changeType;

			changeType = (Change)Enum.Parse(typeof(Change)
				, ChangeTimer.ChangeName);
			DoChange(changeType);
		}

		// Starts the Timer with the Change value.
		internal void TimedChange(Change change)
		{
			ChangeTimer.DoChange(change.ToString());
		}

		// Gets or sets the ChangeTimer object.
		internal ChangeTimer ChangeTimer { get; set; }
		#endregion
		#endregion

		#region Private Methods

		// Sets the control states based on the current control values.
		private void SetControlState()
		{
			bool enableNew;
			bool enableEdit;

			enableNew = true;
			enableEdit = BusinessGrid.CurrentRow != null;
			FormCommon.SetToolState(BusinessTool, enableNew, enableEdit);
			FormCommon.SetMenuState(BusinessMenu, enableNew, enableEdit);

			enableNew = BusinessGrid.CurrentRow != null;
			enableEdit = ContactGrid.CurrentRow != null;
			FormCommon.SetToolState(ContactTool, enableNew, enableEdit);
			FormCommon.SetMenuState(ContactMenu, enableNew, enableEdit);

			enableEdit = AddressGrid.CurrentRow != null;
			FormCommon.SetToolState(AddressTool, enableNew, enableEdit);
			FormCommon.SetMenuState(AddressMenu, enableNew, enableEdit);
		}
		#endregion

		#region Action Event Handlers

		#region Business

		// Calls the New method.
		private void BusinessToolNew_Click(object sender, EventArgs e)
		{
			DoNewBusiness();
		}

		// Calls the Edit method.
		private void BusinessToolEdit_Click(object sender, EventArgs e)
		{
			DoEditBusiness();
		}

		// Calls the Delete method.
		private void BusinessToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteBusiness();
		}

		// Calls the New method.
		private void BusinessMenuNew_Click(object sender, EventArgs e)
		{
			DoNewBusiness();
		}

		// Calls the Edit method.
		private void BusinessMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditBusiness();
		}

		// Calls the Delete method.
		private void BusinessMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteBusiness();
		}

		// Calls the Refresh method.
		private void BusinessMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshBusiness();
		}

		// 
		private void BusinessMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportBusiness.{mSettings.ExportTextExtension}";
			BusinessGrid.LJCExportData(fileSpec);
		}

		// 
		private void BusinessMenuCSV_Click(object sender, EventArgs e)
		{
			BusinessGrid.LJCExportData("ExportBusiness.csv");
		}

		// Performs the Close function.
		private void BusinessMenuClose_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ClosePage(BusinessPage);
		}

		// Show the help page.
		private void BusinessMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "BusinessList.htm");
		}
		#endregion

		#region Contact

		// Calls the New method.
		private void ContactToolNew_Click(object sender, EventArgs e)
		{
			DoAddContact();
		}

		// Calls the Edit method.
		private void ContactEditButton_Click(object sender, EventArgs e)
		{
			DoEditContact();
		}

		// Calls the Delete method.
		private void ContactDeleteButton_Click(object sender, EventArgs e)
		{
			DoRemoveContact();
		}

		// Calls the New method.
		private void ContactMenuNew_Click(object sender, EventArgs e)
		{
			DoAddContact();
		}

		// Calls the Edit method.
		private void ContactMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditContact();
		}

		// Calls the Delete method.
		private void ContactMenuDelete_Click(object sender, EventArgs e)
		{
			DoRemoveContact();
		}

		// Calls the Refresh method.
		private void ContactMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshContact();
		}

		// 
		private void ContactMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportContact.{mSettings.ExportTextExtension}";
			BusinessGrid.LJCExportData(fileSpec);
		}

		// 
		private void ContactMenuCSV_Click(object sender, EventArgs e)
		{
			BusinessGrid.LJCExportData("ExportContact.csv");
		}

		// Display the help page.
		private void ContactMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "BusinessPersonList.htm");
		}
		#endregion

		#region Address

		// Calls the New method.
		private void AddressToolNew_Click(object sender, EventArgs e)
		{
			DoAddAddress();
		}

		// Calls the Edit method.
		private void AddressToolEdit_Click(object sender, EventArgs e)
		{
			DoEditAddress();
		}

		// Calls the Delete method.
		private void AddressToolDelete_Click(object sender, EventArgs e)
		{
			DoDeleteAddress();
		}

		// Calls the New method.
		private void AddressMenuNew_Click(object sender, EventArgs e)
		{
			DoAddAddress();
		}

		// Calls the Edit method.
		private void AddressMenuEdit_Click(object sender, EventArgs e)
		{
			DoEditAddress();
		}

		// Calls the Delete method.
		private void AddressMenuDelete_Click(object sender, EventArgs e)
		{
			DoDeleteAddress();
		}

		// Calls the Refresh method.
		private void AddressMenuRefresh_Click(object sender, EventArgs e)
		{
			DoRefreshAddress();
		}

		// 
		private void AddressMenuText_Click(object sender, EventArgs e)
		{
			var fileSpec
				= $"ExportBusinessAddress.{mSettings.ExportTextExtension}";
			BusinessGrid.LJCExportData(fileSpec);
		}

		// 
		private void AddressMenuCSV_Click(object sender, EventArgs e)
		{
			BusinessGrid.LJCExportData("ExportBusinessAddress.csv");
		}

		// Display the help page.
		private void AddressMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
				, "BusinessAddressList.htm");
		}
		#endregion
		#endregion

		#region Control Event Handlers

		#region Business

		// Handles the form keys.
		private void BusinessGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "BusinessList.htm");
					break;

				case Keys.F5:
					DoRefreshBusiness();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoEditBusiness();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						ContactGrid.Select();
					}
					else
					{
						ContactGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void BusinessGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& BusinessGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				BusinessGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Business);
			}
		}

		// Handles the SelectionChanged event.
		private void BusinessGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (BusinessGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Business);
			}
			BusinessGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void BusinessGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (BusinessGrid.LJCGetMouseRow(e) != null)
			{
				DoEditBusiness();
			}
		}
		#endregion

		#region Contact

		// Handles the form keys.
		private void ContactGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "BusinessPersonList.htm");
					break;

				case Keys.F5:
					DoRefreshContact();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoEditContact();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						BusinessGrid.Select();
					}
					else
					{
						BusinessGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void ContactGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& ContactGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				ContactGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Contact);
			}
		}

		// Handles the SelectionChanged event.
		private void ContactGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (ContactGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Contact);
			}
			ContactGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void ContactGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (ContactGrid.LJCGetMouseRow(e) != null)
			{
				DoEditContact();
			}
		}
		#endregion

		#region Address

		// Handles the form keys.
		private void AddressGrid_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
						, "BusinessAddressList.htm");
					break;

				case Keys.F5:
					DoRefreshAddress();
					e.Handled = true;
					break;

				case Keys.Enter:
					DoEditAddress();
					e.Handled = true;
					break;

				case Keys.Tab:
					if (e.Shift)
					{
						BusinessGrid.Select();
					}
					else
					{
						BusinessGrid.Select();
					}
					e.Handled = true;
					break;
			}
		}

		// Handles the MouseDown event.
		private void AddressGrid_MouseDown(object sender, MouseEventArgs e)
		{
			// LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
			if (e.Button == MouseButtons.Right
				&& AddressGrid.LJCIsDifferentRow(e))
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				AddressGrid.LJCSetCurrentRow(e);
				TimedChange(Change.Address);
			}
		}

		// Handles the SelectionChanged event.
		private void AddressGrid_SelectionChanged(object sender, EventArgs e)
		{
			if (AddressGrid.LJCAllowSelectionChange)
			{
				TimedChange(Change.Address);
			}
			AddressGrid.LJCAllowSelectionChange = true;
		}

		// Handles the MouseDoubleClick event.
		private void AddressGrid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (AddressGrid.LJCGetMouseRow(e) != null)
			{
				DoEditAddress();
			}
		}
		#endregion
		#endregion

		#region Properties

		/// <summary> The help file name.</summary>
		public string LJCHelpFile { get; set; }

		// The Managers object.
		internal FacilityManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mControlValuesFileName;
		#endregion
	}
}
