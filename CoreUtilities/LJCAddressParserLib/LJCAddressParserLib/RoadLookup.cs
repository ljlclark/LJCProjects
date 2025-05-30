﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RoadLookup.cs
using System;
using System.Collections.Generic;
using System.Text;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	/// <summary>Represents a RoadLookup component.</summary>
	public class RoadLookup : IComparable<RoadLookup>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public RoadLookup()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public RoadLookup Clone()
		{
			RoadLookup retValue = MemberwiseClone() as RoadLookup;
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
		public int CompareTo(RoadLookup other)
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
				retValue = string.Compare(CountryCode, other.CountryCode, true);
				if (0 == retValue)
				{
					retValue = string.Compare(LookupName, other.LookupName, true);
				}
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the CountryCode value.</summary>
		public string CountryCode { get; set; }

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
	public class RoadLSoundexComparer : IComparer<RoadLookup>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(RoadLookup x, RoadLookup y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.CountryCode, y.CountryCode, true);
				if (0 == retValue)
				{
					retValue = string.Compare(x.LSoundex, y.LSoundex, true);
				}
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Phonetic Soundex value.</summary>
	public class RoadPSoundexComparer : IComparer<RoadLookup>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(RoadLookup x, RoadLookup y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.CountryCode, y.CountryCode, true);
				if (0 == retValue)
				{
					retValue = string.Compare(x.PSoundex, y.PSoundex, true);
				}
			}
			return retValue;
		}
	}
	#endregion
}
