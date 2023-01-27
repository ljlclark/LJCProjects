// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataKeyTypes.cs
using System.Collections.Generic;

namespace LJCSQLUtilLibDAL
{
	/// <summary>Represents a collection of DbMetaDataKeyType objects.</summary>
	public class DbMetaDataKeyTypes : List<DbMetaDataKeyType>
	{
		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public DbMetaDataKeyType Add(int id, string name)
		{
			DbMetaDataKeyType retValue = new DbMetaDataKeyType()
			{
				ID = id,
				Name = name
			};
			Add(retValue);
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public DbMetaDataKeyType LJCSearchName(string name)
		{
			DbMetaDataKeyType retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			DbMetaDataKeyType searchItem = new DbMetaDataKeyType()
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
