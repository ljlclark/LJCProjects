// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitTypeComboCode.cs
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormControls;

namespace LJCUnitMeasure
{
	// The combo code.
	internal class UnitTypeComboCode
	{
		#region Constructors

		// Initializes an object instance.
		internal UnitTypeComboCode(UnitMeasureManagers managers
			, UnitMeasureList parent = null)
		{
			// Allows LoadCombo() to be called without specifying the list parent
			// such as when called from a Detail dialgo.
			if (parent != null)
			{
				mParent = parent;
			}
			mManagers = managers;
		}
		#endregion

		#region Data Methods

		// Retrieves the combo items.
		internal void DataRetrieve(LJCItemCombo combo)
		{
			if (mParent != null)
			{
				mParent.Cursor = Cursors.WaitCursor;
				LoadCombo(combo);
				mParent.Cursor = Cursors.Default;
			}
		}

		// Loads the combo items.
		internal void LoadCombo(LJCItemCombo combo)
		{
			combo.Items.Clear();

			var unitTypeManager = mManagers.UnitTypeManager;
			var dataRecords = unitTypeManager.Load();

			if (NetCommon.HasItems(dataRecords))
			{
				foreach (UnitType dataRecord in dataRecords)
				{
					combo.LJCAddItem(dataRecord.ID, dataRecord.Name);
				}
				if (combo.Items.Count > 0)
				{
					combo.SelectedIndex = 0;
				}
			}
		}
		#endregion

		#region Action Methods
		#endregion

		#region Class Data

		private readonly UnitMeasureList mParent;
		private readonly UnitMeasureManagers mManagers;
		#endregion
	}
}
