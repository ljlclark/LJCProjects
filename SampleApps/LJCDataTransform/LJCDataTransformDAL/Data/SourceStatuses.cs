// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceStatuses.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Represents a collection of SourceStatus objects.</summary>
	public class SourceStatuses : List<SourceStatus>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public SourceStatuses()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public SourceStatus Add(short id, string name)
		{
			SourceStatus retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new SourceStatus()
					{
						SourceStatusID = id,
						Name = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		/// <summary>Sort on the default values.</summary>
		public void LJCSortDefault()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public SourceStatus LJCSearchName(string name)
		{
			SourceStatus retValue = null;

			LJCSortDefault();
			SourceStatus searchItem = new SourceStatus()
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
