// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitCombo.cs
using System.Text;
using System.Windows.Forms;
using LJCFacilityManagerDAL;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCDataAccess;

namespace LJCFacilityManager
{
	/// <summary>A Combobox for Unit selection.</summary>
	public partial class UnitCombo : ComboBox
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitCombo()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes the object values after the object has been created.
		/// </summary>
		/// <param name="connectionString">The Connection string.</param>
		/// <param name="connectionType">The Database Type.</param>
		public void LJCInit(string connectionString
			, ConnectionType connectionType = ConnectionType.SqlServer)
		{
			DbServiceRef dbServiceRef = new DbServiceRef()
			{
				DbService = new DbService()
			};
			string dataConfigName = "FacilityManager";
			mUnitManager = new UnitManager(dbServiceRef, dataConfigName);
		}

		/// <summary>
		/// Loads the object data after it has been initialized with LJCInit().
		/// </summary>
		/// <param name="facilityID">The FacilityID value.</param>
		public void LJCLoad(int facilityID)
		{
			Units list;

			Items.Clear();
			var keyColumns = mUnitManager.GetParentIDKey(facilityID);
			mUnitManager.SetOrderByCode();
			list = mUnitManager.Load(keyColumns);
			foreach (Unit record in list)
			{
				Items.Add(record);
			}
		}

		// Sets the selected item based on the supplied ID value.
		/// <include path='items/LJCSetSelectedItem/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public void LJCSetSelectedItem(int id)
		{
			Unit record;

			for (int index = 0; index < Items.Count; index++)
			{
				record = Items[index] as Unit;
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
		public Unit LJCGetSelectedItem()
		{
			Unit retValue;

			retValue = SelectedItem as Unit;
			return retValue;
		}

		// Retrieves the selected item ID value.
		/// <include path='items/LJCGetSelectedItemID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public int LJCGetSelectedItemID()
		{
			Unit record;
			int retValue = -1;

			record = LJCGetSelectedItem();
			if (record != null)
			{
				retValue = record.ID;
			}
			return retValue;
		}

		#region Member Data

		// Class Data.
		private UnitManager mUnitManager;
		//private DatabaseType mDatabaseType;
		#endregion
	}
}
