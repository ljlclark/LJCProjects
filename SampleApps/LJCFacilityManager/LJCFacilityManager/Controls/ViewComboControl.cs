// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewComboControl.cs
using System;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBViewDAL;
using LJCDBClientLib;

namespace LJCFacilityManager
{
	/// <summary>A Combobox for ViewData selection.</summary>
	public partial class ViewComboControl : LJCItemCombo
	{
		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewComboControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes the object values after the object has been created. 
		/// </summary>
		/// <param name="tableName">The TableName value.</param>
		/// <param name="dbServiceRef">The DbServiceRef object.</param>
		/// <param name="dataConfigName">The DataConfig name.</param>
		public void LJCInit(string tableName, DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			LJCTableName = tableName;
			LJCDataConfigName = dataConfigName;
      Managers = new ManagersDbView();
      Managers.SetDbProperties(dbServiceRef, dataConfigName);
		}

		// Loads the object data after it has been initialized with LJCInit().
		/// <include path='items/LJCLoad/*' file='../../../CoreUtilities/LJCGenDoc/Common/Control.xml'/>
		public bool LJCLoad()
		{
			bool retValue = false;

			if (mTableName != null && mViewTableManager != null)
			{
				ViewTable viewTable = mViewTableManager.RetrieveWithUniqueKey(mTableName);
				if (viewTable != null)
				{
					int tableID = viewTable.ID;
					Views views = mViewDataManager.LoadWithParentID(tableID);
					if (views != null)
					{
						foreach (ViewData viewData in views)
						{
							LJCAddItem(viewData.ID, viewData.Description);
						}
						if (Items.Count > 0)
						{
							retValue = true;
							LJCSetByItemID(tableID);
							if (SelectedIndex < 0)
							{
								SelectedIndex = 0;
							}
						}
					}
				}
			}
			return retValue;
		}

		#region Properties

		/// <summary>Gets or sets the TableName value.</summary>
		public string LJCTableName
		{
			get { return mTableName; }
			set
			{
				mTableName = NetString.InitString(value);
			}
		}
		private string mTableName;

		/// <summary>Gets  or sets the DataConfigName value.</summary>
		public string LJCDataConfigName
		{
			get { return mDataConfigName; }
			set
			{
				mDataConfigName = NetString.InitString(value);
				if (value != null)
				{
					mViewTableManager = Managers.ViewTableManager;
					mViewDataManager = Managers.ViewDataManager;
				}
			}
		}
		private string mDataConfigName;

    internal ManagersDbView Managers { get; set; }
		#endregion

		#region Class Data

		private ViewTableManager mViewTableManager;
		private ViewDataManager mViewDataManager;
		#endregion
	}
}
