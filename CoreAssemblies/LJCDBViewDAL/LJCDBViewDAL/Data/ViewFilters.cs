// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewFilter objects. 
	/// <include path='items/ViewFilters/*' file='Doc/ViewFilters.xml'/>
	public class ViewFilters : List<ViewFilter>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ViewFilters.xml'/>
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
