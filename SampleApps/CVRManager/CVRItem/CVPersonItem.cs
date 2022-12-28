using CVRDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using LJCRegionDAL;
using LJCRegionItem;
using System;

namespace CVRItem
{
	/// <summary>Provides ItemKey methods.</summary>
	public class CVPersonItem
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public CVPersonItem(long id, string dataConfigName)
		{
			// Add reference to LJCDBClientLib, DbDataAccessLib and LJCDBMessage.
			mCVRManagers = new CVRManagers();

			// Calls SetDataConfig.
			DataConfigName = dataConfigName;

			SetDataColumns(id);
		}

		// Sets the Data Configuration.
		private void SetDataConfig()
		{
			// Add reference to LJCDBClientLib, DbDataAccessLib and LJCDBMessage.
			mDbServiceRef = new DbServiceRef()
			{
				DbDataAccess = new DbDataAccess(DataConfigName)
			};
			mCVRManagers.SetDBProperties(mDbServiceRef, DataConfigName);
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Creates and returns the KeyItems object.
		/// </summary>
		/// <returns>The KeyItems object.</returns>
		public KeyItems KeyItems()
		{
			KeyItems retValue = new KeyItems();

			// KeyItems for CVSex DropDown list.
			var cvSexManager = mCVRManagers.CVSexManager;
			retValue.Append(cvSexManager.GetKeyItems("CVSexID"));

			RegionItem regionItem = new RegionItem(DataConfigName);
			ProvinceItem provinceItem = new ProvinceItem(DataConfigName);
			CityItem cityItem = new CityItem(DataConfigName);
			CitySectionItem citySectionItem = new CitySectionItem(DataConfigName);

			// Foreign and Static KeyItems.
			retValue.Add(regionItem.KeyItem(DataColumns, "RegionID", true));
			retValue.Add(provinceItem.KeyItem(DataColumns, "ProvinceID"));
			retValue.Add(cityItem.KeyItem(DataColumns, "CityID"));
			retValue.Add(citySectionItem.KeyItem(DataColumns, "CitySectionID"));
			return retValue;
		}

		/// <summary>
		/// Sets and returns the DataColumns record. 
		/// </summary>
		/// <param name="id">The record ID value.</param>
		/// <returns>The RecordColumns object.</returns>
		public DbColumns SetDataColumns(long id)
		{
			DbColumns retValue;

			var manager = mCVRManagers.CVPersonManager;
			DataColumns = manager.DataColumns(id);
			retValue = DataColumns;
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets the DataColumn value.</summary>
		public DbColumns DataColumns { get; private set; }

		/// <summary>Gets or sets the DataConfigName value.</summary>
		public string DataConfigName
		{
			get { return mDataConfigName; }
			set
			{
				mDataConfigName = value;
				if (false == NetString.HasValue(mDataConfigName))
				{
					throw new NullReferenceException("DataConfigName is null.");
				}
				SetDataConfig();
			}
		}
		private string mDataConfigName;

		/// <summary>Gets or sets the ID value.</summary>
		public long ID
		{
			get { return id; }
			set
			{
				id = value;
				if (id < 1)
				{
					throw new NullReferenceException("ID is less than 1.");
				}
				SetDataColumns((int)id);
			}
		}
		private long id;
		#endregion

		#region Class Data

		private readonly CVRManagers mCVRManagers;
		private DbServiceRef mDbServiceRef;
		#endregion
	}
}
