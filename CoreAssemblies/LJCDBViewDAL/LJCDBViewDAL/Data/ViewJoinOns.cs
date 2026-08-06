// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOns.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
  // Represents a collection of ViewJoinOn objects. 
  /// <include file='Doc/ViewJoinOns.xml'
  ///  path='items/ViewJoinOns/*'/>
  public class ViewJoinOns : List<ViewJoinOn>
	{
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include file='Doc/ViewJoinOns.xml'
    ///  path='items/Add/*'/>
    public ViewJoinOn Add(int id, int viewJoinID, string fromColumnName
			, string toColumnName)
		{
			ViewJoinOn retValue = new ViewJoinOn()
			{
				ID = id,
				ViewJoinID = viewJoinID,
				FromColumnName = fromColumnName,
				ToColumnName = toColumnName
			};
			Add(retValue);
			return retValue;
		}

    // Retrieve the collection element with name.
    /// <include file='Doc/ViewJoinOns.xml'
    ///  path='items/LJCSearchName/*'/>
    public ViewJoinOn LJCSearchName(string fromColumnName)
		{
			ViewJoinOn retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewJoinOn searchItem = new ViewJoinOn()
			{
				FromColumnName = fromColumnName
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
