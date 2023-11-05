// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Facilities.cs
using System;
using System.Collections.Generic;

namespace CVRDAL
{
	/// <summary>Represents a collection of Facility objects.</summary>
	public class Facilities : List<Facility>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Facilities()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Sort and Search Methods

		/// <summary>Sort on ID.</summary>
		public void LJCSortID()
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.ID) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.ID;
			}
		}

		/// <summary>Sort on Code.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortCode(FacilityCodeComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Code) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.Code;
			}
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public Facility LJCSearchID(int id)
		{
			Facility retValue = null;

			LJCSortID();
			Facility searchItem = new Facility()
			{
				ID = id
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchCode/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public Facility LJCSearchCode(string code)
		{
			FacilityCodeComparer comparer;
			Facility retValue = null;

			comparer = new FacilityCodeComparer();
			LJCSortCode(comparer);
			Facility searchItem = new Facility()
			{
				Code = code
			};
			int index = BinarySearch(searchItem, comparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			ID,
			Code
		}
		#endregion
	}
}
