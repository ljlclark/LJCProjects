// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AccountGridCode.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCFacilityManagerDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class AccountGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal AccountGridCode(PersonModule parent)
		{
			mParent = parent;
			mAccountGrid = mParent.AccountGrid;
			mPersonGrid = mParent.PersonGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieveAccount()
		{
			Accounts records;

			mParent.Cursor = Cursors.WaitCursor;
			mAccountGrid.LJCRowsClear();

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				var keyColumns = new DbColumns()
				{
					{ Account.ColumnPersonID, parentID }
				};
				var accountManager = mManagers.AccountManager;
				DbJoins dbJoins = accountManager.GetLoadJoins();
				records = accountManager.Load(keyColumns, joins: dbJoins);

				if (NetCommon.HasItems(records))
				{
					foreach (Account record in records)
					{
						RowAddAccount(record);
					}
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.DoChange(PersonModule.Change.Account);
		}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAddAccount(Account dataRecord)
		{
			LJCGridRow retValue;

			retValue = mAccountGrid.LJCRowAdd();
			SetStoredValuesAccount(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mAccountGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdateAccount(Account dataRecord)
		{
			if (mAccountGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesAccount(row, dataRecord);
				row.LJCSetValues(mAccountGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValuesAccount(LJCGridRow row, Account dataRecord)
		{
			row.LJCSetInt32(Account.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelectAccount(Account dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mAccountGrid.Rows)
				{
					rowID = row.LJCGetInt32(Account.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mAccountGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNewAccount()
		{
			AccountDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow)
			{
				// Data from list items.
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				detail = new AccountDetail()
				{
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "AccountDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(AccountDetail_Change);
				detail.ShowDialog();
			}
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEditAccount()
		{
			AccountDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow parentRow
				&& mAccountGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Account.ColumnID);
				int parentID = parentRow.LJCGetInt32(Person.ColumnID);

				detail = new AccountDetail()
				{
					LJCID = id,
					LJCParentID = parentID,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = "AccountDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(AccountDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void AccountDetail_Change(object sender, EventArgs e)
		{
			AccountDetail detail;
			LJCGridRow row;

			detail = sender as AccountDetail;
			if (detail.LJCIsUpdate)
			{
				RowUpdateAccount(detail.LJCRecord);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddAccount(detail.LJCRecord);
				mAccountGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(PersonModule.Change.Account);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDeleteAccount()
		{
			string title;
			string message;

			if (mAccountGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Account.ColumnID);

					var keyColumns = new DbColumns()
					{
						{ Account.ColumnID, id }
					};
					mManagers.AccountManager.Delete(keyColumns);
					if (mManagers.AccountManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mAccountGrid.Rows.Remove(row);
						mParent.TimedChange(PersonModule.Change.Account);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefreshAccount()
		{
			Account record;
			int id = 0;

			if (mAccountGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Account.ColumnID);
			}
			DataRetrieveAccount();

			// Select the original row.
			if (id > 0)
			{
				record = new Account()
				{
					ID = id
				};
				RowSelectAccount(record);
			}
		}
		#endregion

		#region Class Data

		private readonly PersonModule mParent;
		private readonly LJCDataGrid mAccountGrid;
		private readonly LJCDataGrid mPersonGrid;
		private readonly FacilityManagers mManagers;
		#endregion
	}
}
