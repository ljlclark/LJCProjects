// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderBys.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewOrderBy objects. 
	/// <include path='items/ViewOrderBys/*' file='Doc/ViewOrderBys.xml'/>
	public class ViewOrderBys : List<ViewOrderBy>
	{
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include file='Doc/ViewOrderBys.xml'
    ///  path='items/Add/*'/>
    public ViewOrderBy Add(int id, string columnName)
		{
			ViewOrderBy retValue = new ViewOrderBy()
			{
				ID = id,
				ColumnName = columnName
			};
			Add(retValue);
			return retValue;
		}

    // Retrieve the collection element with name.
    /// <include file='Doc/ViewOrderBys.xml'
    ///  path='items/LJCSearchName/*'/>
    public ViewOrderBy LJCSearchName(string columnName)
		{
			ViewOrderBy retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewOrderBy searchItem = new ViewOrderBy()
			{
				ColumnName = columnName
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
