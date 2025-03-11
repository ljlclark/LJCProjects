// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GridCodePerson.cs
using System;
using System.Windows.Forms;
using LJCDBMessage;
using LJCDBViewDAL;
using LJCFacilityManagerDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	internal class GridCodePerson
	{
		#region Constructors

		// Initializes an object instance.
		internal GridCodePerson(PersonModule parent)
		{
			mParent = parent;
			mPersonGrid = mParent.PersonGrid;
			mManagers = mParent.Managers;

			// *** Begin *** Add - view
			mDataDbView = mParent.mDataDbView;
			mViewInfo = mParent.mViewInfoPerson;
			//mViewInfo.GridData.AddRow += ResultGrid_AddRow;
			// *** End   *** Add - view
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DataRetrieve()
		{
			mPersonGrid.LJCRowsClear();

			// *** Begin *** Change - View
			// Load the selected view data.
			DbResult dbResult = mDataDbView.GetViewData(mViewInfo.TableName
				, mViewInfo.DataID);
			//if (dbResult != null)
			if (DbResult.HasRows(dbResult))
			{
				// *** Change *** 
				//mViewInfo.GridData.LoadRows(dbResult);
				foreach (DbRow dbRow in dbResult.Rows)
				{
					var gridRow = mParent.PersonGrid.LJCRowAdd();
					gridRow.LJCSetValues(mParent.PersonGrid, dbRow.Values);
				}
				mParent.DoChange(PersonModule.Change.Person);
			}
			// *** End   *** Change - View
		}

		//// Handles the AddRow event to allow setting the stored values and
		//// other new row processing.
		//private void ResultGrid_AddRow(object sender, EventArgs e)
		//{
		//	ResultGridData resultGridData = sender as ResultGridData;

		//	LJCGridRow row = resultGridData.GridRow;
		//	DbValues dbValues = resultGridData.DataRecord;
		//	Person record = new Person()
		//	{
		//		ID = dbValues.LJCGetInt32(Person.ColumnID),
		//		FirstName = dbValues.LJCGetValue(Person.ColumnFirstName),
		//		MiddleInitial = dbValues.LJCGetValue(Person.ColumnMiddleInitial),
		//		LastName = dbValues.LJCGetValue(Person.ColumnLastName)
		//	};
		//	SetStoredValues(row, record);
		//}

		// Adds a grid row and updates it with the record values.
		/// <include path='items/RowAdd/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private LJCGridRow RowAdd(Person dataRecord)
		{
			LJCGridRow retValue;

			retValue = mPersonGrid.LJCRowAdd();
			SetStoredValues(retValue, dataRecord);

			// Sets the row values from a data object.
			retValue.LJCSetValues(mPersonGrid, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		/// <include path='items/RowUpdate/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void RowUpdate(Person dataRecord)
		{
			if (mPersonGrid.CurrentRow is LJCGridRow gridRow)
			{
				SetStoredValues(gridRow, dataRecord);
				gridRow.LJCSetValues(mPersonGrid, dataRecord);
			}
		}

		// Sets the row stored values.
		/// <include path='items/SetStoredValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void SetStoredValues(LJCGridRow row, Person dataRecord)
		{
			row.LJCSetInt32(Person.ColumnID, dataRecord.ID);
			row.LJCSetString(Person.ColumnFullName, dataRecord.FullName);
		}

		// Selects a row based on the key record values.
		/// <include path='items/RowSelect/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private bool RowSelect(Person dataRecord)
		{
			int rowID;
			bool retValue = false;

			if (dataRecord != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mPersonGrid.Rows)
				{
					rowID = row.LJCGetInt32(Person.ColumnID);
					if (rowID == dataRecord.ID)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mPersonGrid.LJCSetCurrentRow(row, true);
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
		internal void DoNew()
		{
			PersonDetail detail;

			detail = new PersonDetail()
			{
				LJCHelpFileName = mParent.LJCHelpFile,
				LJCHelpPageName = "PersonDetail.htm"
			};
			detail.LJCChange += new EventHandler<EventArgs>(PersonDetail_Change);
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		/// <include path='items/DoEdit/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoEdit()
		{
			PersonDetail detail;

			if (mPersonGrid.CurrentRow is LJCGridRow row)
			{
				// Data from list items.
				int id = row.LJCGetInt32(Person.ColumnID);

				detail = new PersonDetail()
				{
					LJCID = id,
					LJCHelpPageName = "PersonDetail.htm"
				};
				detail.LJCChange += new EventHandler<EventArgs>(PersonDetail_Change);
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from detail dialog.
		/// <include path='items/Detail_Change/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void PersonDetail_Change(object sender, EventArgs e)
		{
			PersonDetail detail;
			Person record;
			LJCGridRow row;

			detail = sender as PersonDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdate(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAdd(record);
				mPersonGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(PersonModule.Change.Person);
			}
		}

		// Deletes the selected row.
		/// <include path='items/DoDelete/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoDelete()
		{
			string title;
			string message;

			if (mPersonGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from list items.
					int id = row.LJCGetInt32(Person.ColumnID);
					var keyColumns = mManagers.PersonManager.GetIDKey(id);

					mManagers.PersonManager.Delete(keyColumns);
					if (mManagers.PersonManager.AffectedCount < 1)
					{
						message = FormCommon.DeleteError;
						MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
					else
					{
						mPersonGrid.Rows.Remove(row);
						mParent.TimedChange(PersonModule.Change.Person);
					}
				}
			}
		}

		// Refreshes the list.
		/// <include path='items/DoRefresh/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		internal void DoRefresh()
		{
			Person record;
			int id = 0;

			if (mPersonGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt32(Person.ColumnID);
			}
			DataRetrieve();

			// Select the original row.
			if (id > 0)
			{
				record = new Person()
				{
					ID = id
				};
				RowSelect(record);
			}
		}
		#endregion

		#region Class Data

		private readonly PersonModule mParent;
		private readonly LJCDataGrid mPersonGrid;
		private readonly FacilityManagers mManagers;

		// *** Begin *** Add - view
		private readonly DataDbView mDataDbView;
		private readonly ViewInfo mViewInfo;
		// *** End   *** Add - view
		#endregion
	}
}
