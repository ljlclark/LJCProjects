// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitLookup.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a UnitLookup component.
	/// <include path='items/UnitLookup/*' file='Doc/UnitLookup.xml'/>
	public class UnitLookup : IComparable<UnitLookup>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public UnitLookup()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public UnitLookup Clone()
		{
			UnitLookup retValue = MemberwiseClone() as UnitLookup;
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
		public int CompareTo(UnitLookup other)
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
	public class UnitLSoundexComparer : IComparer<UnitLookup>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(UnitLookup x, UnitLookup y)
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
	public class UnitPSoundexComparer : IComparer<UnitLookup>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(UnitLookup x, UnitLookup y)
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
