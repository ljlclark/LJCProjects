// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformMaps.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	// Represents a collection of TransformMap objects. 
	/// <include path='items/TransformMaps/*' file='Doc/TransformMaps.xml'/>
	public class TransformMaps : List<TransformMap>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public TransformMaps()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/TransformMaps.xml'/>
		public TransformMap Add(int mapID, int transformID)
		{
			TransformMap retValue = null;

			if (mapID > 0
				&& transformID > 0)
			{
				retValue = new TransformMap()
				{
					TransformMapID = mapID,
					TransformID = transformID
				};
				Add(retValue);
			}
			return retValue;
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

		/// <summary>Sort with Source ID.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortSourceID(SourceIDComparer comparer)
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
		public void LJCSortTargetID(TargetIDComparer comparer)
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
		public void LJCSortColumnIDs(ColumnIDsComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.ColumnIDs) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.ColumnIDs;
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchID/*' file='Doc/TransformMaps.xml'/>
		public TransformMap LJCSearchID(int mapID)
		{
			TransformMap retValue = null;

			LJCSortID();
			TransformMap searchItem = new TransformMap()
			{
				TransformMapID = mapID
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
		public TransformMap LJCSearchSourceID(int transformID, int sourceID)
		{
			SourceIDComparer comparer;
			TransformMap retValue = null;

			comparer = new SourceIDComparer();
			LJCSortSourceID(comparer);
			TransformMap searchItem = new TransformMap()
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
		public TransformMap LJCSearchTargetID(int transformID, int targetID)
		{
			TargetIDComparer comparer;
			TransformMap retValue = null;

			comparer = new TargetIDComparer();
			LJCSortTargetID(comparer);
			TransformMap searchItem = new TransformMap()
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
		public TransformMap LJCSearchColumnIDs(int transformID, int sourceColumnID, int targetColumnID)
		{
			ColumnIDsComparer comparer;
			TransformMap retValue = null;

			comparer = new ColumnIDsComparer();
			LJCSortColumnIDs(comparer);
			TransformMap searchItem = new TransformMap()
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
