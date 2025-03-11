// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Directional.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents an Address Directional component.
	/// <include path='items/Directional/*' file='Doc/ProjectAddressParser.xml'/>
	public class Directional : IComparable<Directional>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Directional()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Directional Clone()
		{
			Directional retValue = MemberwiseClone() as Directional;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return Name;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(Directional other)
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
				retValue = string.Compare(Code, other.Code, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the Code value.</summary>
		public string Code { get; set; }

		/// <summary>Gets or sets the Name value.</summary>
		public string Name { get; set; }

		/// <summary>Gets or sets the SpanishName value.</summary>
		public string SpanishName { get; set; }

		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class NameComparer : IComparer<Directional>
	{
		/// <summary>Compares two objects.</summary>
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(Directional x, Directional y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.Name, y.Name, true);
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Spanish Name value.</summary>
	public class SpanishNameComparer : IComparer<Directional>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(Directional x, Directional y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Not case sensitive.
				retValue = string.Compare(x.SpanishName, y.SpanishName, true);
			}
			return retValue;
		}
	}
	#endregion
}
