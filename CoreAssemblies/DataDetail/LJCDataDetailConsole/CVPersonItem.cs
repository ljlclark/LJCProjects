using System;
using CVRManagerDAL;
using LJCDBClientLib;
using LJCDBDataAccessLib;
using LJCNetCommon;
using LJCRegionItem;
using LJCRegionManagerDAL;

namespace LJCDataDetailConsole
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
			DataConfigName = dataConfigName;
			SetRecordColumns(id);
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
			mRegionManagers = new RegionManagers(mDbServiceRef, DataConfigName);
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
			retValue.Append(cvSexManager.KeyItems("CVSexID"));

			RegionItem regionItem = new RegionItem(DataConfigName);
			ProvinceItem provinceItem = new ProvinceItem(DataConfigName);
			CityItem cityItem = new CityItem(DataConfigName);
			CitySectionItem citySectionItem = new CitySectionItem(DataConfigName);

			// Foreign and Static KeyItems.
			retValue.Add(regionItem.KeyItem(RecordColumns, "RegionID", true));
			retValue.Add(provinceItem.KeyItem(RecordColumns, "ProvinceID"));
			retValue.Add(cityItem.KeyItem(RecordColumns, "CityID"));
			retValue.Add(citySectionItem.KeyItem(RecordColumns, "CitySectionID"));
			return retValue;
		}

		/// <summary>
		/// Sets and returns the DbColumns data record. 
		/// </summary>
		/// <param name="id">The record ID value.</param>
		/// <returns>The RecordColumns object.</returns>
		public DbColumns SetRecordColumns(long id)
		{
			DbColumns retValue = null;

			var manager = mCVRManagers.CVPersonManager;
			RecordColumns = manager.RecordColumns(id);
			retValue = RecordColumns;
			return retValue;
		}
		#endregion

		#region Properties

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
				SetRecordColumns((int)id);
			}
		}
		private long id;

		/// <summary>Gets the RecordColumn value.</summary>
		public DbColumns RecordColumns { get; private set; }
		#endregion

		#region Class Data

		private CVRManagers mCVRManagers;
		private DbServiceRef mDbServiceRef;
		private RegionManagers mRegionManagers;
		#endregion
	}
}
