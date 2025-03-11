// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserAppPrograms.cs
using System.Collections.Generic;

namespace LJCAppManagerDAL
{
	// Represents a collection of UserAppProgram objects. 
	/// <include path='items/UserAppPrograms/*' file='Doc/UserAppPrograms.xml'/>
	public class UserAppPrograms : List<UserAppProgram>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/UserAppPrograms.xml'/>
		public UserAppProgram Add(int id, int programID)
		{
			UserAppProgram retValue = new UserAppProgram()
			{
				AppManagerUserID = id,
				AppProgramID = programID
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
