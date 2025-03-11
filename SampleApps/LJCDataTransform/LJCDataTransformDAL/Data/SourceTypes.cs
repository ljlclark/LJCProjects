// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SourceTypes.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Represents a collection of SourceType objects.</summary>
	public class SourceTypes : List<SourceType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public SourceTypes()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public SourceType Add(short id, string name)
		{
			SourceType retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				//retValue = LJCSearchByName(name);
				//if (null == retValue)
				//{
				retValue = new SourceType()
				{
					SourceTypeID = id,
					Name = name
				};
				Add(retValue);
				//}
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
		public SourceType LJCSearchName(string name)
		{
			SourceType retValue = null;

			LJCSortDefault();
			SourceType searchItem = new SourceType()
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
