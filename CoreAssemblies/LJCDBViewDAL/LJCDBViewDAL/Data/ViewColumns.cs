// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewColumns.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	// <summary>Represents a collection of object items.</summary>
	/// <include path='items/Collection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
	public class ViewColumns : List<ViewColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewColumns()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewColumn Add(int id, string name)
		{
			ViewColumn retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new ViewColumn()
					{
						ID = id,
						ColumnName = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Get custom collection from List<T>.
		/// <include path='items/GetCollection/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewColumns GetCollection(List<ViewColumn> list)
		{
			ViewColumns retValue = null;

			if (NetCommon.HasItems(list))
			{
				retValue = new ViewColumns();
				foreach (ViewColumn item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public ViewColumn LJCSearchName(string name)
		{
			ViewColumn retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewColumn searchItem = new ViewColumn()
			{
				ColumnName = name
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
