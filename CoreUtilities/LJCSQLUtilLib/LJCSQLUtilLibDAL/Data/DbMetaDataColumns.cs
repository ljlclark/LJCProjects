// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCSQLUtilLibDAL
{
	/// <summary>Represents a collection of DbMetaDataColumn objects.</summary>
	public class DbMetaDataColumns : List<DbMetaDataColumn>
	{
		#region Collection Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbMetaDataColumns Clone()
		{
			DbMetaDataColumns retValue = new DbMetaDataColumns();

			foreach (DbMetaDataColumn mdColumn in this)
			{
				retValue.Add(mdColumn.Clone());
			}
			return retValue;
		}

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public DbMetaDataColumn Add(int id, string name)
		{
			DbMetaDataColumn retValue = new DbMetaDataColumn()
			{
				ID = id,
				Name = name,
				ColumnName = name,
				PropertyName = name
			};
			Add(retValue);
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Sorts the collection with the Name value.
		/// <include path='items/LJCSortName/*' file='Doc/DbMetaDataColumns.xml'/>
		public void LJCSortName()
		{
			if (Count != mPrevCount
				|| 0 != mSortType.CompareTo(SortType.Name))
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.Name;
			}
		}

		// Sorts the collection with the Sequence value.
		/// <include path='items/LJCSortSequence/*' file='Doc/DbMetaDataColumns.xml'/>
		public void LJCSortSequence(MDSequenceComparer mdSequenceComparer)
		{
			if (Count != mPrevCount
				|| 0 != mSortType.CompareTo(SortType.Sequence))
			{
				mPrevCount = Count;
				Sort(mdSequenceComparer);
				mSortType = SortType.Sequence;
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchColumnName/*' file='Doc/DbMetaDataColumns.xml'/>
		public DbMetaDataColumn LJCSearchColumnName(string columnName)
		{
			DbMetaDataColumn retValue = null;

			LJCSortName();
			DbMetaDataColumn searchItem = new DbMetaDataColumn()
			{
				ColumnName = columnName
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element with sequence.
		/// <include path='items/LJCSearchSequence/*' file='Doc/DbMetaDataColumns.xml'/>
		public DbMetaDataColumn LJCSearchSequence(int sequence)
		{
			MDSequenceComparer mdSequenceComparer;
			DbMetaDataColumn retValue = null;

			mdSequenceComparer = new MDSequenceComparer();
			LJCSortSequence(mdSequenceComparer);
			DbMetaDataColumn searchItem = new DbMetaDataColumn()
			{
				Sequence = sequence
			};
			int index = BinarySearch(searchItem, mdSequenceComparer);
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
			Name,
			Sequence
		}
		#endregion
	}
}
