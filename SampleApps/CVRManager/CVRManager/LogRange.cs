// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LogRange.cs
using System;

namespace CVRManager
{
	internal class LogRange : IComparable<LogRange>
	{
		#region Data Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return RangeName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(LogRange other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				// Not case sensitive.
				retValue = string.Compare(RangeName, other.RangeName, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		internal string RangeName { get; set; }

		internal TimeSpan StartTime { get; set; }
		#endregion
	}
}
