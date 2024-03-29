﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ChildGridCode.cs
using System;

namespace DataHelper
{
	// Contains the Grid methods.
	internal class ChildGridCode
	{
		#region Constructors

		// Initializes an object instance.
		internal ChildGridCode(MainList parent)
		{
			mParent = parent;
			if (null == mParent) { }
		}
		#endregion

		#region Data Methods

		// Retrieves the list rows.
		internal void DataRetrieveChild()
		{
		}

		//// Adds a grid row and updates it with the record values.
		//private LJCGridRow RowAddChild(DbMetaDataKey record)
		//{
		//	LJCGridRow retValue = null;

		//	return retValue;
		//}

		//// Updates the current row with the record values.
		//private void RowUpdateChild(DbMetaDataKey record)
		//{
		//}

		//// Sets the row stored values.
		//private void SetStoredValuesChild(LJCGridRow row, DbMetaDataKey record)
		//{
		//}

		//// Selects a row based on the key record values.
		//private bool RowSelectChild(DbMetaDataKey record)
		//{
		//	bool retValue = true;

		//	return retValue;
		//}
		#endregion

		#region Action Methods

		// Displays a detail dialog for a new record.
		internal void DoNewChild()
		{
		}

		// Displays a detail dialog to edit an existing record.
		internal void DoEditChild()
		{
		}

		// Adds new row or updates existing row with changes from the detail dialog.
		private void ChildDetail_Change(object sender, EventArgs e)
		{
		}

		// Deletes the selected row.
		internal void DoDeleteChild()
		{
		}

		// Refreshes the list.
		internal void DoRefreshChild()
		{
		}
		#endregion

		#region Class Data

		private readonly MainList mParent;
		#endregion
	}
}
