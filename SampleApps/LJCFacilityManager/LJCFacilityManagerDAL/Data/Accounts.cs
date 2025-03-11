// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Accounts.cs
using System;
using System.Collections.Generic;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of Account objects.</summary>
	public class Accounts : List<Account>
	{
		#region Public Methods

		/// <summary>
		/// Creates and adds the object from the provided values.
		/// </summary>
		/// <param name="id">The record ID.</param>
		/// <param name="description">The item description.</param>
		/// <returns>A reference to the added item.</returns>
		public Account Add(int id, string description = null)
		{
			Account retValue = new Account()
			{
				ID = id,
				Description = description
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with ID.
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public Account LJCSearchID(int id)
		{
			Account retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			Account searchItem = new Account()
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
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
