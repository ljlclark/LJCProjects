// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppModules.cs
using System.Collections.Generic;

namespace LJCAppManagerDAL
{
	// Represents a collection of UserAppModule objects. 
	/// <include path='items/UserAppModules/*' file='Doc/UserAppModules.xml'/>
	public class UserAppModules : List<UserAppModule>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/UserAppModules.xml'/>
		public UserAppModule Add(int id, int programID, int moduleID)
		{
			UserAppModule retValue = new UserAppModule()
			{
				AppManagerUserID = id,
				AppProgramID = programID,
				AppModuleID = moduleID
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
