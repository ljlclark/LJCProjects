// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SuffixLookup.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a SuffixLookup component.
	/// <include path='items/SuffixLookup/*' file='Doc/SuffixLookup.xml'/>
	public class SuffixLookup : IComparable<SuffixLookup>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public SuffixLookup()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public SuffixLookup Clone()
		{
			SuffixLookup retValue = MemberwiseClone() as SuffixLookup;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return LookupName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(SuffixLookup other)
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
				retValue = string.Compare(LookupName, other.LookupName, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the LookupName value.</summary>
		public string LookupName { get; set; }

		/// <summary>Gets or sets the Letter Soundex value.</summary>
		public string LSoundex { get; set; }

		/// <summary>Gets or sets the Phonetic Soundex value.</summary>
		public string PSoundex { get; set; }

		/// <summary>Gets or sets the Code value.</summary>
		public string Code { get; set; }
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Letter Soundex value.</summary>
	public class SuffixLSoundexComparer : IComparer<SuffixLookup>
	{
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(SuffixLookup x, SuffixLookup y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.LSoundex, y.LSoundex, true);
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Phonetic Soundex value.</summary>
	public class SuffixPSoundexComparer : IComparer<SuffixLookup>
	{
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(SuffixLookup x, SuffixLookup y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.PSoundex, y.PSoundex, true);
			}
			return retValue;
		}
	}
	#endregion
}
