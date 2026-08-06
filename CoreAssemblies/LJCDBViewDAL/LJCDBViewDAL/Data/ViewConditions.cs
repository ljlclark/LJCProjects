// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditions.cs
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewCondition objects.
	/// <include path='items/ViewConditions/*' file='Doc/ViewConditions.xml'/>
	public class ViewConditions : List<ViewCondition>
	{
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include file='Doc/ViewConditions.xml'
    ///  path='items/Add/*'/>
    public ViewCondition Add(int id, string firstValue, string secondValue
			, string comparisonOperator = "=")
		{
			ViewCondition retValue = new ViewCondition()
			{
				ID = id,
				FirstValue = firstValue,
				SecondValue = secondValue,
				ComparisonOperator = comparisonOperator
			};
			Add(retValue);
			return retValue;
		}
		#endregion
	}
}
