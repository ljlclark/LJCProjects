// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System.Collections.Generic;

namespace LJCDBViewDAL
{
	// Represents a collection of ViewGridColumn objects. 
	/// <include path='items/ViewGridColumns/*' file='Doc/ViewGridColumns.xml'/>
	public class ViewGridColumns : List<ViewGridColumn>
	{
		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='Doc/ViewGridColumns.xml'/>
		public ViewGridColumn Add(int viewDataID, int viewColumnID)
		{
			ViewGridColumn retValue = new ViewGridColumn()
			{
				ViewDataID = viewDataID,
				ViewColumnID = viewColumnID
			};
			Add(retValue);
			return retValue;
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchID/*' file='Doc/ViewGridColumns.xml'/>
		public ViewGridColumn LJCSearchID(int viewDataID, int viewColumnID)
		{
			ViewGridColumn retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			ViewGridColumn searchItem = new ViewGridColumn()
			{
				ViewDataID = viewDataID,
				ViewColumnID = viewColumnID
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
