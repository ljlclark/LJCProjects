// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataTables.cs
using System.Collections.Generic;

namespace LJCSQLUtilLibDAL
{
	/// <summary>Represents a collection of DbMetaDataTable objects.</summary>
	public class DbMetaDataTables : List<DbMetaDataTable>
	{
		#region Collection Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public DbMetaDataTable Add(int id, string name)
		{
			DbMetaDataTable retValue = new DbMetaDataTable()
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
		public DbMetaDataTable LJCSearchName(string name)
		{
			DbMetaDataTable retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			DbMetaDataTable searchItem = new DbMetaDataTable()
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
