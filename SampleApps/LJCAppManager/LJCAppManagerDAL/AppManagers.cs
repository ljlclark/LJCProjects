// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppManagers.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCAppManagerDAL
{
	/// <summary>Creates the GenealogyDAL Manager objects.</summary>
	public class AppManagers
	{
		#region Constructors

		// Initializes an object instance.
		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dbServiceRef">The database service reference object.</param>
		/// <param name="dataConfigName">The data configuration name.</param>
		public AppManagers(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;
		}
		#endregion

		#region Properties

		/// <summary>Gets the AppUserManager object.</summary>
		public AppUserManager2 AppUserManager
		{
			get
			{
				if (null == mAppUserManager)
				{
					AppUserManager
						= new AppUserManager2(mDbServiceRef, mDataConfigName);
				}
				return mAppUserManager;
			}

			private set
			{
				if (value != null)
				{
					mAppUserManager = value;
				}
			}
		}

		/// <summary>Gets the AppProgramManager object.</summary>
		public AppProgramManager2 AppProgramManager
		{
			get
			{
				if (null == mAppProgramManager)
				{
					AppProgramManager
						= new AppProgramManager2(mDbServiceRef, mDataConfigName);
				}
				return mAppProgramManager;
			}

			private set
			{
				if (value != null)
				{
					mAppProgramManager = value;
				}
			}
		}

		/// <summary>Gets the AppModuleManager object.</summary>
		public AppModuleManager2 AppModuleManager
		{
			get
			{
				if (null == mAppModuleManager)
				{
					AppModuleManager
						= new AppModuleManager2(mDbServiceRef, mDataConfigName);
				}
				return mAppModuleManager;
			}

			private set
			{
				if (value != null)
				{
					mAppModuleManager = value;
				}
			}
		}

		/// <summary>Gets the UserAppProgramManager object.</summary>
		public UserAppProgramManager2 UserAppProgramManager
		{
			get
			{
				if (null == mUserAppProgramManager)
				{
					UserAppProgramManager
						= new UserAppProgramManager2(mDbServiceRef, mDataConfigName);
				}
				return mUserAppProgramManager;
			}

			private set
			{
				if (value != null)
				{
					mUserAppProgramManager = value;
				}
			}
		}

		/// <summary>Gets the UserAppModuleManager object.</summary>
		public UserAppModuleManager2 UserAppModuleManager
		{
			get
			{
				if (null == mUserAppModuleManager)
				{
					UserAppModuleManager
						= new UserAppModuleManager2(mDbServiceRef, mDataConfigName);
				}
				return mUserAppModuleManager;
			}

			private set
			{
				if (value != null)
				{
					mUserAppModuleManager = value;
				}
			}
		}
		#endregion

		#region Class Data

		private readonly DbServiceRef mDbServiceRef;
		private readonly string mDataConfigName;

		private AppUserManager2 mAppUserManager;
		private AppProgramManager2 mAppProgramManager;
		private AppModuleManager2 mAppModuleManager;
		private UserAppProgramManager2 mUserAppProgramManager;
		private UserAppModuleManager2 mUserAppModuleManager;
		#endregion
	}
}
