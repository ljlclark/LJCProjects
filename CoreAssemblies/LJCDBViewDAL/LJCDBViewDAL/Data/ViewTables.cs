// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	/// <summary>Represents a collection of ViewTable objects.</summary>
	public class ViewTables : List<ViewTable>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public ViewTable Add(int id, string name)
		{
			ViewTable retValue = new ViewTable()
			{
				ID = id,
				Name = name
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public ViewTable LJCSearchName(string name)
		{
			ViewTable retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewTable searchItem = new ViewTable()
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
