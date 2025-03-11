// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMatches.cs
using System;
using System.Collections.Generic;

namespace LJCDataTransformDAL
{
	// Represents a collection of TransformMatch objects. 
	/// <include path='items/TransformMatches/*' file='Doc/TransformMatches.xml'/>
	public class TransformMatches : List<TransformMatch>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMatches()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/TransformMatches.xml'/>
		public TransformMatch Add(int matchID, int transformID)
		{
			TransformMatch retValue = null;

			if (matchID > 0
				&& transformID > 0)
			{
				retValue = new TransformMatch()
				{
					TransformMatchID = matchID,
					TransformID = transformID
				};
				Add(retValue);
			}
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		/// <summary>Sort on default values.</summary>
		public void LJCSortDefault()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		/// <summary>Sort with Source ID.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortSourceID(MatchSourceIDComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.SourceID) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.SourceID;
			}
		}

		/// <summary>Sort with Target ID.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortTargetID(MatchTargetIDComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.TargetID) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.TargetID;
			}
		}

		/// <summary>Sort with Column IDs.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortColumnIDs(MatchColumnIDsComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.ColumnIDs) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.ColumnIDs;
			}
		}

		// Retrieve the collection element with ID.
		/// <include path='items/SearchID/*' file='Doc/TransformMatches.xml'/>
		public TransformMatch SearchID(int matchID)
		{
			TransformMatch retValue = null;

			LJCSortDefault();
			TransformMatch searchItem = new TransformMatch()
			{
				TransformMatchID = matchID
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element with Source ID.
		/// <include path='items/LJCSearchSourceID/*' file='Doc/TransformMaps.xml'/>
		public TransformMatch LJCSearchSourceID(int transformID, int sourceID)
		{
			MatchSourceIDComparer comparer;
			TransformMatch retValue = null;

			comparer = new MatchSourceIDComparer();
			LJCSortSourceID(comparer);
			TransformMatch searchItem = new TransformMatch()
			{
				TransformID = transformID,
				SourceColumnID = (short)sourceID
			};
			int index = BinarySearch(searchItem, comparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element with Target ID.
		/// <include path='items/LJCSearchTargetID/*' file='Doc/TransformMaps.xml'/>
		public TransformMatch LJCSearchTargetID(int transformID, int targetID)
		{
			MatchTargetIDComparer comparer;
			TransformMatch retValue = null;

			comparer = new MatchTargetIDComparer();
			LJCSortTargetID(comparer);
			TransformMatch searchItem = new TransformMatch()
			{
				TransformID = transformID,
				TargetColumnID = (short)targetID
			};
			int index = BinarySearch(searchItem, comparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element with Source and Target IDs.
		/// <include path='items/LJCSearchColumnIDs/*' file='Doc/TransformMaps.xml'/>
		public TransformMatch LJCSearchColumnIDs(int transformID, int sourceColumnID, int targetColumnID)
		{
			MatchColumnIDsComparer comparer;
			TransformMatch retValue = null;

			comparer = new MatchColumnIDsComparer();
			LJCSortColumnIDs(comparer);
			TransformMatch searchItem = new TransformMatch()
			{
				TransformID = transformID,
				SourceColumnID = (short)sourceColumnID,
				TargetColumnID = (short)targetColumnID
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
			SourceID,
			TargetID,
			ColumnIDs
		}
		#endregion
	}
}
