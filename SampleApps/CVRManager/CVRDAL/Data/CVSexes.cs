// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVSexes.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CVRDAL
{
	/// <summary>Represents a collection of CVSex objects.</summary>
	[XmlRoot("CVSexes")]
	public class CVSexes : List<CVSex>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CVSexes()
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
		public void LJCSortCode(CVSexCodeComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Code) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.Code;
			}
		}

		/// <summary>Sort on Name.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortName(CVSexNameComparer comparer)
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
		public CVSex LJCSearchID(long id)
		{
			CVSex retValue = null;

			LJCSortID();
			CVSex searchItem = new CVSex()
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
		/// <include path='items/LJCSearchCode/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public CVSex LJCSearchCode(string code)
		{
			CVSexCodeComparer comparer;
			CVSex retValue = null;

			comparer = new CVSexCodeComparer();
			LJCSortCode(comparer);
			CVSex searchItem = new CVSex()
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

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public CVSex LJCSearchName(string name)
		{
			CVSexNameComparer comparer;
			CVSex retValue = null;

			comparer = new CVSexNameComparer();
			LJCSortName(comparer);
			CVSex searchItem = new CVSex()
			{
				Name = name
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
			Code,
			Name
		}
		#endregion
	}
}
