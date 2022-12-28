// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewJoin objects. 
	/// <include path='items/ViewJoins/*' file='Doc/ViewJoins.xml'/>
	public class ViewJoins : List<ViewJoin>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ViewJoins.xml'/>
		public ViewJoin Add(int id, string joinTableName)
		{
			ViewJoin retValue = new ViewJoin()
			{
				ID = id,
				JoinTableName = joinTableName
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public ViewJoin LJCSearchName(string name)
		{
			ViewJoin retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewJoin searchItem = new ViewJoin()
			{
				JoinTableName = name
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
