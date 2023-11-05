// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Views.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewData objects. 
	/// <include path='items/Views/*' file='Doc/Views.xml'/>
	public class Views : List<ViewData>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewData Add(int id, string name)
		{
			ViewData retValue = new ViewData()
			{
				ID = id,
				Name = name
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewData LJCSearchName(string name)
		{
			ViewData retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewData searchItem = new ViewData()
			{
				Name = name
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
