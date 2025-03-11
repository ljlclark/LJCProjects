// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppUsers.cs
using System.Collections.Generic;

namespace LJCAppManagerDAL
{
	// Represents a collection of AppManagerUser objects. 
	/// <include path='items/AppUsers/*' file='Doc/AppUsers.xml'/>
	public class AppUsers : List<AppUser>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/AppUsers.xml'/>
		public AppUser Add(int id, string name, string userID)
		{
			AppUser retValue = new AppUser()
			{
				ID = id,
				Name = name,
				UserID = userID
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element by windows user ID.
		/// <include path='items/SearchByUserID/*' file='Doc/AppUsers.xml'/>
		public AppUser SearchByUserID(string userID)
		{
			AppUser retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			AppUser searchItem = new AppUser()
			{
				UserID = userID
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
