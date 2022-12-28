using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace CVRManager
{
	internal class LogRanges : List<LogRange>
	{
		#region Collection Methods

		// Creates and adds the object from the provided values.
		internal LogRange Add(string rangeName)
		{
			LogRange retValue = null;

			if (NetString.HasValue(rangeName))
			{
				retValue = SearchName(rangeName);
				if (null == retValue)
				{
					retValue = new LogRange()
					{
						RangeName = rangeName
					};
					Add(retValue);
				}
			}
			return retValue;
		}
		#endregion

		#region Sort and Search Methods

		// Sort on RangeName.
		internal void SortName()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element.
		internal LogRange SearchName(string name)
		{
			LogRange retValue = null;

			SortName();
			LogRange searchItem = new LogRange()
			{
				RangeName = name
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
