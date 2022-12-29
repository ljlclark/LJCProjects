// Copyright (c) Lester J Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CVRDAL
{
	/// <summary>Represents a collection of CVPerson objects.</summary>
	[XmlRoot("CVPersons")]
	public class CVPersons : List<CVPerson>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CVPersons()
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

		/// <summary>Sort on Name.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortName(CVPersonNameComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Name) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.Name;
			}
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public CVPerson LJCSearchID(long id)
		{
			CVPerson retValue = null;

			LJCSortID();
			CVPerson searchItem = new CVPerson()
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
		/// <include path='items/LJCSearchName/*' file='Doc/CVPersons.xml'/>
		public CVPerson LJCSearchName(string firstName, string middleName
			, string lastName)
		{
			CVPersonNameComparer comparer;
			CVPerson retValue = null;

			comparer = new CVPersonNameComparer();
			LJCSortName(comparer);
			CVPerson searchItem = new CVPerson()
			{
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName
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
			Name
		}
		#endregion
	}
}
