using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
	/// <summary>Gets the CVR Manager objects.</summary>
	public class CVRManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public CVRManagers()
		{
		}
		#endregion

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

		#region Properties

		/// <summary>Gets the CVVisitManager object.</summary>
		public CVVisitManager CVVisitManager
		{
			get
			{
				if (null == mCVVisitManager)
				{
					CVVisitManager
						= new CVVisitManager(mDbServiceRef, mDataConfigName);
				}
				return mCVVisitManager;
			}

			private set
			{
				if (value != null)
				{
					mCVVisitManager = value;
				}
			}
		}

		/// <summary>Gets the CVPersonManager object.</summary>
		public CVPersonManager CVPersonManager
		{
			get
			{
				if (null == mCVPersonManager)
				{
					CVPersonManager
						= new CVPersonManager(mDbServiceRef, mDataConfigName);
				}
				return mCVPersonManager;
			}

			private set
			{
				if (value != null)
				{
					mCVPersonManager = value;
				}
			}
		}

		/// <summary>Gets the CVSexManager object.</summary>
		public CVSexManager CVSexManager
		{
			get
			{
				if (null == mCVSexManager)
				{
					CVSexManager
						= new CVSexManager(mDbServiceRef, mDataConfigName);
				}
				return mCVSexManager;
			}

			private set
			{
				if (value != null)
				{
					mCVSexManager = value;
				}
			}
		}

		/// <summary>Gets the FacilityManager object.</summary>
		public FacilityManager FacilityManager
		{
			get
			{
				if (null == mFacilityManager)
				{
					FacilityManager
						= new FacilityManager(mDbServiceRef, mDataConfigName);
				}
				return mFacilityManager;
			}

			private set
			{
				if (value != null)
				{
					mFacilityManager = value;
				}
			}
		}

		/// <summary>Gets the CodeTypeManager object.</summary>
		public CodeTypeManager CodeTypeManager
		{
			get
			{
				if (null == mCodeTypeManager)
				{
					CodeTypeManager
						= new CodeTypeManager(mDbServiceRef, mDataConfigName);
				}
				return mCodeTypeManager;
			}

			private set
			{
				if (value != null)
				{
					mCodeTypeManager = value;
				}
			}
		}

		/// <summary>Gets the CodeTypeClassManager object.</summary>
		public CodeTypeClassManager CodeTypeClassManager
		{
			get
			{
				if (null == mCodeTypeClassManager)
				{
					CodeTypeClassManager
						= new CodeTypeClassManager(mDbServiceRef, mDataConfigName);
				}
				return mCodeTypeClassManager;
			}

			private set
			{
				if (value != null)
				{
					mCodeTypeClassManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private DbServiceRef mDbServiceRef;
		private string mDataConfigName;

		private CVVisitManager mCVVisitManager;
		private CVPersonManager mCVPersonManager;
		private CVSexManager mCVSexManager;
		private FacilityManager mFacilityManager;
		private CodeTypeManager mCodeTypeManager;
		private CodeTypeClassManager mCodeTypeClassManager;
		#endregion
	}
}
