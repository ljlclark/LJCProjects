// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Addresses.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of Address objects.</summary>
	public class Addresses : List<Address>
	{
		/// <summary>
		/// Creates and adds the object from the provided values.
		/// </summary>
		/// <param name="id">The record ID.</param>
		/// <param name="street">The item name.</param>
		/// <returns>A reference to the added item.</returns>
		public Address Add(int id, string street = null)
		{
			Address retValue = new Address()
			{
				ID = id,
				Street = street
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with ID.
		/// <include path='items/LJCSearchID/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public Address LJCSearchID(int id)
		{
			Address retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			Address searchItem = new Address()
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

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
