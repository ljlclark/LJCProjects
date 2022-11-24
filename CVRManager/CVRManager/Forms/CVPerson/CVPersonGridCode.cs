// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using CVRDAL;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace CVRManager
{
	internal class CVPersonGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal CVPersonGridCode(CVPersonList parent)
		{
			mParent = parent;
			mCVPersonGrid = mParent.CVPersonGrid;
			mManagers = mParent.Managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		private void DataRetrieveCVPerson(CVPersons dataRecords)
		{
			mParent.Cursor = Cursors.WaitCursor;
			mCVPersonGrid.LJCRowsClear();

			if (dataRecords != null && dataRecords.Count > 0)
			{
				foreach (CVPerson record in dataRecords)
				{
					RowAddCVPerson(record);
				}
			}
			mParent.Cursor = Cursors.Default;
			mParent.TimedChange(CVPersonList.Change.CVPerson);
		}

		// Adds a grid row and updates it with the record values.
		private LJCGridRow RowAddCVPerson(CVPerson dataRecord)
		{
			LJCGridRow retValue;

			retValue = mCVPersonGrid.LJCRowAdd();
			SetStoredValuesCVPerson(retValue, dataRecord);

			// Sets the row values from a data object.
			mCVPersonGrid.LJCRowSetValues(retValue, dataRecord);
			return retValue;
		}

		// Updates the current row with the record values.
		private void RowUpdateCVPerson(CVPerson dataRecord)
		{
			if (mCVPersonGrid.CurrentRow is LJCGridRow row)
			{
				SetStoredValuesCVPerson(row, dataRecord);
				mCVPersonGrid.LJCRowSetValues(row, dataRecord);
			}
		}

		// Sets the row stored values.
		private void SetStoredValuesCVPerson(LJCGridRow row, CVPerson dataRecord)
		{
			row.LJCSetInt64(CVPerson.ColumnID, dataRecord.ID);
		}

		// Selects a row based on the key record values.
		private bool RowSelectCVPerson(DbColumns keyColumns)
		{
			long rowID;
			bool retValue = false;

			if (keyColumns != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				foreach (LJCGridRow row in mCVPersonGrid.Rows)
				{
					rowID = row.LJCGetInt64(CVPerson.ColumnID);
					var keyColumn = keyColumns.LJCSearchPropertyName(CVPerson.ColumnID);
					if (rowID == (long)keyColumn.Value)
					{
						// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
						mCVPersonGrid.LJCSetCurrentRow(row, true);
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

		// Performs the default list action.
		internal void DoDefaultCVPerson()
		{
			if (mParent.LJCIsSelect)
			{
				DoSelectCVPerson();
			}
			else
			{
				DoEditCVPerson();
			}
		}

		// Displays a detail dialog for a new record.
		internal void DoNewCVPerson()
		{
			CVPersonDetail detail;

			detail = new CVPersonDetail()
			{
				LJCHelpFileName = mParent.LJCHelpFile,
				LJCHelpPageName = mParent.LJCHelpPageDetail
			};
			detail.LJCChange += CVPersonDetail_LJCChange;
			detail.ShowDialog();
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditCVPerson()
		{
			CVPersonDetail detail;

			if (mCVPersonGrid.CurrentRow is LJCGridRow row)
			{
				// Data from items.
				long id = row.LJCGetInt64(CVPerson.ColumnID);

				detail = new CVPersonDetail()
				{
					LJCID = id,
					LJCHelpFileName = mParent.LJCHelpFile,
					LJCHelpPageName = mParent.LJCHelpPageDetail
				};
				detail.LJCChange += CVPersonDetail_LJCChange;
				detail.ShowDialog();
			}
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void CVPersonDetail_LJCChange(object sender, EventArgs e)
		{
			CVPersonDetail detail;
			CVPerson record;
			LJCGridRow row;

			detail = sender as CVPersonDetail;
			record = detail.LJCRecord;
			if (detail.LJCIsUpdate)
			{
				RowUpdateCVPerson(record);
			}
			else
			{
				// LJCSetCurrentRow sets the LJCAllowSelectionChange property.
				row = RowAddCVPerson(record);
				mCVPersonGrid.LJCSetCurrentRow(row, true);
				mParent.TimedChange(CVPersonList.Change.CVPerson);
			}
		}

		// Deletes the selected row.
		internal void DoDeleteCVPerson()
		{
			string title;
			string message;

			if (mCVPersonGrid.CurrentRow is LJCGridRow row)
			{
				title = "Delete Confirmation";
				message = FormCommon.DeleteConfirm;
				if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
					, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					// Data from items.
					long id = row.LJCGetInt64(CVPerson.ColumnID);

					var cvPersonManager = mManagers.CVPersonManager;
					cvPersonManager.DeleteWithID(id);
					if (cvPersonManager.AffectedCount > 0)
					{
						mCVPersonGrid.Rows.Remove(row);
						mParent.TimedChange(CVPersonList.Change.CVPerson);
					}
				}
			}
		}

		// Refreshes the list.
		internal void DoRefreshCVPerson()
		{
			long id = 0;

			mParent.Cursor = Cursors.WaitCursor;
			if (mCVPersonGrid.CurrentRow is LJCGridRow row)
			{
				id = row.LJCGetInt64(CVPerson.ColumnID);
			}
			DoShow();

			// Select the original row.
			if (id > 0)
			{
				var keyColumns = mManagers.CVPersonManager.GetIDKey(id);
				RowSelectCVPerson(keyColumns);
			}
			mParent.Cursor = Cursors.Default;
		}

		// Sets the selected item and returns to the parent form.
		internal void DoSelectCVPerson()
		{
			CVPerson record;
			long id;

			mParent.LJCSelectedRecord = null;
			if (mCVPersonGrid.CurrentRow is LJCGridRow row)
			{
				mParent.Cursor = Cursors.WaitCursor;
				id = row.LJCGetInt64(CVPerson.ColumnID);

				var keyColumns = mManagers.CVPersonManager.GetIDKey(id);
				record = mManagers.CVPersonManager.Retrieve(keyColumns);
				if (record != null)
				{
					mParent.LJCSelectedRecord = record;
				}
				mParent.Cursor = Cursors.Default;
				mParent.DialogResult = DialogResult.OK;
			}
		}

		// Retrieve the filtered records.
		internal void DoShow()
		{
			CVPersons dataRecords;

			string lastName = mParent.LastNameTextBox.Text.Trim();
			string firstName = mParent.FirstNameTextBox.Text.Trim();

			var cvPersonManager = mManagers.CVPersonManager;
			if (NetString.HasValue(lastName)
				|| NetString.HasValue(firstName))
			{
				DbFilters dbFilters = new DbFilters();
				DbFilter dbFilter = new DbFilter();
				DbConditions dbConditions = dbFilter.ConditionSet.Conditions;

				if (NetString.HasValue(lastName))
				{
					var likeValue = $"'{lastName}%'";
					dbConditions.Add(CVPerson.ColumnLastName, likeValue, "Like");
				}
				if (NetString.HasValue(firstName))
				{
					var likeValue = $"'{firstName}%'";
					dbConditions.Add(CVPerson.ColumnFirstName, likeValue, "Like");
				}
				dbFilters.Add(dbFilter);
				dataRecords = cvPersonManager.Load(filters: dbFilters);
			}
			else
			{
				dataRecords = cvPersonManager.Load();
			}
			DataRetrieveCVPerson(dataRecords);
		}
		#endregion

		#region Class Data

		private readonly CVPersonList mParent;
		private readonly LJCDataGrid mCVPersonGrid;
		private readonly CVRManagers mManagers;
		#endregion
	}
}
