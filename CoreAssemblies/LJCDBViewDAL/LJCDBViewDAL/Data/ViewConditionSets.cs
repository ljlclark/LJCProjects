// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSets.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewConditionSet objects. 
	/// <include path='items/ViewConditionSets/*' file='Doc/ViewConditionSets.xml'/>
	public class ViewConditionSets : List<ViewConditionSet>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ViewConditionSets.xml'/>
		public ViewConditionSet Add(int id, int viewFilterID)
		{
			ViewConditionSet retValue = new ViewConditionSet()
			{
				ID = id,
				ViewFilterID = viewFilterID
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
