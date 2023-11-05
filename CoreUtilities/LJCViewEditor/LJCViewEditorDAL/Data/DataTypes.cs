// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTypes.cs
using System;
using System.Collections.Generic;

namespace LJCViewEditorDAL
{
	/// <summary>Represents a collection of DataType objects.</summary>
	public class DataTypes : List<DataType>
	{
		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public DataType Add(short id, string name)
		{
			DataType retValue = new DataType()
			{
				DataTypeID = id,
				Name = name
			};
			Add(retValue);
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		/// <summary>Sort on Name.</summary>
		public void LJCSortName()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public DataType LJCSearchName(string name)
		{
			DataType retValue = null;

			LJCSortName();
			DataType searchItem = new DataType()
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
