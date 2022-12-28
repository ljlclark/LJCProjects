// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	/// Represents a collection of ViewJoinOn objects. 
	/// <include path='items/ViewJoinOns/*' file='Doc/ViewJoinOns.xml'/>
	public class ViewJoinOns : List<ViewJoinOn>
	{
		#region Public Methods

		/// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ViewJoinOns.xml'/>
		public ViewJoinOn Add(int id, int viewJoinID, string fromColumnName
			, string toColumnName)
		{
			ViewJoinOn retValue = new ViewJoinOn()
			{
				ID = id,
				ViewJoinID = viewJoinID,
				FromColumnName = fromColumnName,
				ToColumnName = toColumnName
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='Doc/ViewJoinOns.xml'/>
		public ViewJoinOn LJCSearchName(string fromColumnName)
		{
			ViewJoinOn retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewJoinOn searchItem = new ViewJoinOn()
			{
				FromColumnName = fromColumnName
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
