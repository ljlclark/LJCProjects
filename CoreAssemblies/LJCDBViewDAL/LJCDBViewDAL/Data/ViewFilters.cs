// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewFilters.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
  // Represents a collection of ViewFilter objects. 
  /// <include file='Doc/ViewFilters.xml'
  ///  path='items/ViewFilters/*'/>
  public class ViewFilters : List<ViewFilter>
	{
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include file='Doc/ViewFilters.xml'
    ///  path='items/Add/*'/>
    public ViewFilter Add(int id)
		{
			ViewFilter retValue = new ViewFilter()
			{
				ID = id
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
