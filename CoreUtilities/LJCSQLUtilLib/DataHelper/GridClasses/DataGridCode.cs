// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataGridCode.cs
using System;
using LJCWinFormControls;
using LJCSQLUtilLibDAL;

namespace DataHelper
{
	// Contains the Grid methods.
	internal class DataGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal DataGridCode(MainList parent)
		{
			mParent = parent;
			// Testing
			if (null == mParent) { }
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveData()
		{
		}

		//// Adds a grid row and updates it with the record values.
		//private LJCGridRow RowAddData(DbMetaDataKey record)
		//{
		//	LJCGridRow retValue = null;

		//	return retValue;
		//}

		//// Updates the current row with the record values.
		//private void RowUpdateData(DbMetaDataKey record)
		//{
		//}

		//// Sets the row stored values.
		//private void SetStoredValuesData(LJCGridRow row, DbMetaDataKey record)
		//{
		//}

		//// Selects a row based on the key record values.
		//private bool RowSelectData(DbMetaDataKey record)
		//{
		//	bool retValue = true;

		//	return retValue;
		//}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewData()
		{
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditData()
		{
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void DataDetail_Change(object sender, EventArgs e)
		{
		}

		// Deletes the selected row.
		internal void DoDeleteData()
		{
		}

		// Refreshes the list.
		internal void DoRefreshData()
		{
		}
		#endregion

		#region Class Data

		private readonly MainList mParent;
		#endregion
	}
}
