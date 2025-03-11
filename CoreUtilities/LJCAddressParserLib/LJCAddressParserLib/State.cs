// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// State.cs
using System;

namespace LJCAddressParserLib
{
	// Represents an Address State component
	/// <include path='items/State/*' file='Doc/State.xml'/>
	public class State : IComparable<State>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public State()
		{
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public State Clone()
		{
			State retValue = MemberwiseClone() as State;
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
		public int CompareTo(State other)
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
					retValue = string.Compare(Code, other.Code, true);
				}
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the CountryCode value.</summary>
		public string CountryCode { get; set; }

		/// <summary>Gets or sets the Code value.</summary>
		public string Code { get; set; }

		/// <summary>Gets or sets the Name value.</summary>
		public string Name { get; set; }
		#endregion
	}
}
