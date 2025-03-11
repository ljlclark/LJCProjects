// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppModules.cs
using System.Collections.Generic;

namespace LJCAppManagerDAL
{
	// Represents a collection of AppModule objects. 
	/// <include path='items/AppModules/*' file='Doc/AppModules.xml'/>
	public class AppModules : List<AppModule>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/AppModules.xml'/>
		public AppModule Add(int id, int programID, string typeName
			, string title)
		{
			AppModule retValue = new AppModule()
			{
				ID = id,
				AppProgramID = programID,
				TypeName = typeName,
				Title = title
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
