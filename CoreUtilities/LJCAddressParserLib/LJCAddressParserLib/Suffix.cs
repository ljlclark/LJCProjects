// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Suffix.cs
using System;

namespace LJCAddressParserLib
{
	// Represents a Suffix component.
	/// <include path='items/Suffix/*' file='Doc/Suffix.xml'/>
	public class Suffix : IComparable<Suffix>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Suffix()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Suffix Clone()
		{
			Suffix retValue = MemberwiseClone() as Suffix;
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
		public int CompareTo(Suffix other)
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
		#endregion
	}
}
