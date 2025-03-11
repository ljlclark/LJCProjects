// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypes.cs
using System.Collections.Generic;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of CodeType objects.</summary>
	public class CodeTypes : List<CodeType>
	{
		#region Public Methods

		/// <summary>
		/// Creates and adds the object from the provided values.
		/// </summary>
		/// <param name="id">The record ID.</param>
		/// <param name="code">The item name.</param>
		/// <returns>A reference to the added item.</returns>
		public CodeType Add(int id, string code)
		{
			CodeType retValue = new CodeType()
			{
				ID = id,
				Code = code
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with code.
		/// <include path='items/LJCSearchCode/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public CodeType LJCSearchCode(string code)
		{
			CodeType retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			CodeType searchItem = new CodeType()
			{
				Code = code
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
