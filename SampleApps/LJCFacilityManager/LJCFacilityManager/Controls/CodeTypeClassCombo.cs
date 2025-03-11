// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeClassCombo.cs
using System;
using System.Windows.Forms;
using LJCFacilityManagerDAL;
using LJCDBClientLib;

namespace LJCFacilityManager
{
	/// <summary>A Combobox for CodeTypeClass selection.</summary>
	public partial class CodeTypeClassCombo : ComboBox
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeTypeClassCombo()
		{
			InitializeComponent();
		}

		// Initializes the object values after the object has been created.
		/// <include path='items/LJCInit/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCInit()
		{
			// Get singleton values.
			ValuesFacility values = ValuesFacility.Instance;

			mSettings = values.StandardSettings;

			mCodeTypeClassManager = new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
		}

		// Loads the object data after it has been initialized with LJCInit().
		/// <include path='items/LJCLoad/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCLoad()
		{
			CodeTypeClasses list;

			Items.Clear();
			mCodeTypeClassManager.SetOrderByDescription();
			list = mCodeTypeClassManager.Load();
			foreach (CodeTypeClass record in list)
			{
				Items.Add(record);
			}
		}

		// Sets the selected item based on the supplied ID value.
		/// <include path='items/LJCSetSelectedItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCSetSelectedItem(int id)
		{
			CodeTypeClass record;

			for (int index = 0; index < Items.Count; index++)
			{
				record = Items[index] as CodeTypeClass;
				if (record != null
					&& record.ID == id)
				{
					SelectedIndex = index;
					break;
				}
			}
		}

		// Retrieves the select item object.
		/// <include path='items/LJCGetSelectedItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public CodeTypeClass LJCGetSelectedItem()
		{
			var retValue = SelectedItem as CodeTypeClass;
			return retValue;
		}

		// Retrieves the selected item ID value.
		/// <include path='items/LJCGetSelectedItemID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public int LJCGetSelectedItemID()
		{
			CodeTypeClass record;
			int retValue = -1;

			record = LJCGetSelectedItem();
			if (record != null)
			{
				retValue = record.ID;
			}
			return retValue;
		}

		#region Class Data

		private StandardUISettings mSettings;
		private CodeTypeClassManager mCodeTypeClassManager;
		#endregion
	}
}
