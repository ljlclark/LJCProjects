// Copyright (c) Lester J Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CVRDAL
{
	/// <summary>Represents a collection of CVVisit objects.</summary>
	[XmlRoot("CVVisits")]
	public class CVVisits : List<CVVisit>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CVVisits()
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

		// Retrieve the collection element.
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public CVVisit LJCSearchID(long id)
		{
			CVVisit retValue = null;

			LJCSortID();
			CVVisit searchItem = new CVVisit()
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
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			ID
		}
		#endregion
	}
}
