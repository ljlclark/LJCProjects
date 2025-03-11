// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Unit.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a Unit component.
	/// <include path='items/Unit/*' file='Doc/Unit.xml'/>
	public class Unit : IComparable<Unit>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Unit()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Unit Clone()
		{
			Unit retValue = MemberwiseClone() as Unit;
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
		public int CompareTo(Unit other)
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

		/// <summary>Gets or sets the RequiresRange value.</summary>
		public bool RequiresRange { get; set; }
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class UnitNameComparer : IComparer<Unit>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(Unit x, Unit y)
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
	#endregion
}
