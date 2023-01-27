// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeClasses.cs
using System;
using System.Collections.Generic;

namespace CVRDAL
{
	/// <summary>Represents a collection of CodeTypeClass objects.</summary>
	public class CodeTypeClasses : List<CodeTypeClass>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CodeTypeClasses()
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
		public void LJCSortCode(CodeTypeClassCodeComparer comparer)
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
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public CodeTypeClass LJCSearchID(int id)
		{
			CodeTypeClass retValue = null;

			LJCSortID();
			CodeTypeClass searchItem = new CodeTypeClass()
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
		public CodeTypeClass LJCSearchCode(string code)
		{
			CodeTypeClassCodeComparer comparer;
			CodeTypeClass retValue = null;

			comparer = new CodeTypeClassCodeComparer();
			LJCSortCode(comparer);
			CodeTypeClass searchItem = new CodeTypeClass()
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
