// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeCombo.cs
using System.Windows.Forms;
using LJCFacilityManagerDAL;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManager
{
	/// <summary>A Combobox for CodeType selection. </summary>
	public partial class CodeTypeCombo : ComboBox
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeTypeCombo()
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

			mCodeTypeManager = new CodeTypeManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
		}

		/// <summary>
		/// Loads the object data after it has been initialized with LJCInit().
		/// </summary>
		/// <param name="classID">The ClassID value.</param>
		public void LJCLoad(int classID)
		{
			CodeTypes list;

			Items.Clear();
			var keyColumns = new DbColumns()
			{
				{ CodeType.ColumnCodeTypeClassID, classID }
			};
			mCodeTypeManager.SetOrderByDescription();
			list = mCodeTypeManager.Load(keyColumns);
			// Next Statement - Add 1/6/21
			if (list != null)
			{
				foreach (CodeType record in list)
				{
					Items.Add(record);
				}
			}
		}

		// Sets the selected item based on the supplied ID value.
		/// <include path='items/LJCSetSelectedItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCSetSelectedItem(int id)
		{
			CodeType record;

			for (int index = 0; index < Items.Count; index++)
			{
				record = Items[index] as CodeType;
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
		public CodeType LJCGetSelectedItem()
		{
			CodeType retValue;

			retValue = SelectedItem as CodeType;
			return retValue;
		}

		// Retrieves the selected item ID value.
		/// <include path='items/LJCGetSelectedItemID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public int LJCGetSelectedItemID()
		{
			CodeType record;
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
		private CodeTypeManager mCodeTypeManager;
		#endregion
	}
}
