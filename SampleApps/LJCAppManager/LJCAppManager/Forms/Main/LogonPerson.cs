// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LogonPerson.cs
using System;
using System.Security.Principal;
using LJCWinFormCommon;
using LJCNetCommon;
using LJCAppManagerDAL;
using LJCDBClientLib;

namespace LJCAppManager
{
	/// <summary>Represents the logon person.</summary>
	public class LogonPerson
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LogonPerson()
		{
			InitializeClass();
		}
		#endregion

		#region Public Methods

		// Verifies the current windows user.
		/// <include path='items/IsAuthenticated/*' file='Doc/LogonPerson.xml'/>
		public bool IsAuthenticated()
		{
			bool retValue = false;

			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			if (identity.IsAuthenticated)
			{
				retValue = true;
				WindowsUserID = identity.Name.Replace("\\", "\\\\");
			}
			return retValue;
		}

		// Get the person data.
		/// <include path='items/GetUserData/*' file='Doc/LogonPerson.xml'/>
		public AppUser GetUserData()
		{
			AppUser retValue;

			var keyColumns = new DbColumns()
			{
				{ AppUser.ColumnUserID, (object)WindowsUserID }
			};
			var appUserManager = mManagers.AppUserManager;
			retValue = appUserManager.Retrieve(keyColumns);
			return retValue;
		}
		#endregion

		#region Setup Methods

		// Configures the class.
		private void InitializeClass()
		{
			// Get singleton values.
			ValuesAppManager values = ValuesAppManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mManagers = new AppManagers(mSettings.DbServiceRef
			, mSettings.DataConfigName);
		}
		#endregion

		#region Properties

		/// <summary>The windows user ID.</summary>
		public string WindowsUserID { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private AppManagers mManagers;
		#endregion
	}
}
