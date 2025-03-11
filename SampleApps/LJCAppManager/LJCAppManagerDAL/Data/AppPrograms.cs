// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppPrograms.cs
using System.Collections.Generic;

namespace LJCAppManagerDAL
{
	// Represents a collection of AppProgram objects. 
	/// <include path='items/AppPrograms/*' file='Doc/AppPrograms.xml'/>
	public class AppPrograms : List<AppProgram>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/AppProgramsC/*' file='Doc/AppPrograms.xml'/>
		public AppProgram Add(int id, string fileName, string title)
		{
			// Changed name to fileName.
			// Added title.
			AppProgram retValue = new AppProgram()
			{
				ID = id,
				FileName = fileName,
				Title = title
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
