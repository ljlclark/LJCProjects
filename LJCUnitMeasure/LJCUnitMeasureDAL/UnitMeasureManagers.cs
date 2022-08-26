// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using LJCDBClientLib;

namespace LJCUnitMeasureDAL
{
	/// <summary>Gets the Manager objects.</summary>
	public class UnitMeasureManagers
	{
		#region Constructors and Initialization Methods

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public UnitMeasureManagers()
		{
		}

		/// <summary>
		/// Sets the DB properties.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public void SetDBProperties(DbServiceRef dbServiceRef
			, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the UnitCategoryManager object.</summary>
		public UnitCategoryManager UnitCategoryManager
		{
			get
			{
				if (null == mUnitCategoryManager)
				{
					UnitCategoryManager
						= new UnitCategoryManager(mDbServiceRef, mDataConfigName);
				}
				return mUnitCategoryManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitCategoryManager = value;
				}
			}
		}

		/// <summary>Gets the UnitCategoryManager object.</summary>
		public UnitConversionManager UnitConversionManager
		{
			get
			{
				if (null == mUnitCategoryManager)
				{
					UnitConversionManager
						= new UnitConversionManager(mDbServiceRef, mDataConfigName);
				}
				return mUnitConversionManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitConversionManager = value;
				}
			}
		}

		/// <summary>Gets the UnitMeasureManager object.</summary>
		public UnitMeasureManager UnitMeasureManager
		{
			get
			{
				if (null == mUnitMeasureManager)
				{
					UnitMeasureManager
						= new UnitMeasureManager(mDbServiceRef, mDataConfigName);
				}
				return mUnitMeasureManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitMeasureManager = value;
				}
			}
		}

		/// <summary>Gets the UnitSystemManager object.</summary>
		public UnitSystemManager UnitSystemManager
		{
			get
			{
				if (null == mUnitSystemManager)
				{
					UnitSystemManager
						= new UnitSystemManager(mDbServiceRef, mDataConfigName);
				}
				return mUnitSystemManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitSystemManager = value;
				}
			}
		}

		/// <summary>Gets the UnitTypeManager object.</summary>
		public UnitTypeManager UnitTypeManager
		{
			get
			{
				if (null == mUnitTypeManager)
				{
					UnitTypeManager
						= new UnitTypeManager(mDbServiceRef, mDataConfigName);
				}
				return mUnitTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mUnitTypeManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private DbServiceRef mDbServiceRef;
		private string mDataConfigName;
		private UnitCategoryManager mUnitCategoryManager;
		private UnitConversionManager mUnitConversionManager;
		private UnitMeasureManager mUnitMeasureManager;
		private UnitSystemManager mUnitSystemManager;
		private UnitTypeManager mUnitTypeManager;
		#endregion
	}
}
