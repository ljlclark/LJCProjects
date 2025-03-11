// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FacilityCombo.cs
using System.Windows.Forms;
using LJCFacilityManagerDAL;
using LJCDBClientLib;
using LJCDataAccess;

namespace LJCFacilityManager
{
	/// <summary>A Combobox for Facility selection.</summary>
	public partial class FacilityCombo : ComboBox
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public FacilityCombo()
		{
			InitializeComponent();
		}

		// Initializes the object values after the object has been created.
		/// <summary>
		/// Initializes the object values after the object has been created.
		/// </summary>
		/// <param name="connectionString">The Connection string.</param>
		/// <param name="connectionType">The Database Type.</param>
		public void LJCInit(string connectionString
			, ConnectionType connectionType = ConnectionType.SqlServer)
		{
			// Get singleton values.
			ValuesFacility values = ValuesFacility.Instance;

			mSettings = values.StandardSettings;

			mFacilityDbManager = new FacilityDbManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
		}

		// Loads the object data after it has been initialized with LJCInit().
		/// <include path='items/LJCLoad/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCLoad()
		{
			Facilities list;

			Items.Clear();
			mFacilityDbManager.DataManager.OrderByNames = new System.Collections.Generic.List<string>()
			{
				Facility.ColumnCodeTypeID
			};
			list = mFacilityDbManager.Load();
			foreach (Facility record in list)
			{
				Items.Add(record);
			}
		}

		// Sets the selected item based on the supplied ID value.
		/// <include path='items/LJCSetSelectedItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCSetSelectedItem(int id)
		{
			Facility record;

			for (int index = 0; index < Items.Count; index++)
			{
				record = Items[index] as Facility;
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
		public Facility LJCGetSelectedItem()
		{
			var retValue = SelectedItem as Facility;
			return retValue;
		}

		// Retrieves the selected item ID value.
		/// <include path='items/LJCGetSelectedItemID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public int LJCGetSelectedItemID()
		{
			Facility record;
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
		private FacilityDbManager mFacilityDbManager;
		#endregion
	}
}
