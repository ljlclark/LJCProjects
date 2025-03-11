// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// States.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a collection of State objects.
	/// <include path='items/States/*' file='Doc/States.xml'/>
	[XmlRoot("States")]
	public class States : List<State>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/States.xml'/>
		public static States LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			States retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateStates(fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(States)
					, fileSpec) as States;
			}
			return retValue;
		}

		// Create the default State values.
		private static States CreateStates(string fileSpec)
		{
			States retValue = null;

			if (!File.Exists(fileSpec))
			{
				retValue = new States
				{
					{ "USA", "AL", "Alabama" },
					{ "USA", "AK", "Alaska" },
					{ "USA", "AS", "American Samoa" },
					{ "USA", "AZ", "Arizona" },
					{ "USA", "AR", "Arkansas" },
					{ "USA", "CA", "California" },
					{ "USA", "CO", "Colorado" },
					{ "USA", "CT", "Connecticut" },
					{ "USA", "DE", "Delaware" },
					{ "USA", "DC", "District of Columbia" },
					{ "USA", "FM", "Federated States of Micronesia" },
					{ "USA", "FL", "Florida" },
					{ "USA", "GA", "Georgia" },
					{ "USA", "GU", "Guam" },
					{ "USA", "HI", "Hawaii" },
					{ "USA", "ID", "Idaho" },
					{ "USA", "IL", "Illinois" },
					{ "USA", "IN", "Indiana" },
					{ "USA", "IA", "Iowa" },
					{ "USA", "KS", "Kansas" },
					{ "USA", "KY", "Kentucky" },
					{ "USA", "LA", "Louisiana" },
					{ "USA", "ME", "Main" },
					{ "USA", "MH", "Marshall Islands" },
					{ "USA", "ME", "Maryland" },
					{ "USA", "MA", "Massachusetts" },
					{ "USA", "MI", "Michigan" },
					{ "USA", "MN", "Minnesota" },
					{ "USA", "MS", "Mississippi" },
					{ "USA", "MO", "Missouri" },
					{ "USA", "MT", "Montana" },
					{ "USA", "NE", "Nebraska" },
					{ "USA", "NV", "Nevada" },
					{ "USA", "NH", "New Hampshire" },
					{ "USA", "NJ", "New Jersey" },
					{ "USA", "NM", "New Mexico" },
					{ "USA", "NY", "New York" },
					{ "USA", "NC", "North Carolina" },
					{ "USA", "ND", "North Dakota" },
					{ "USA", "MP", "Northern Mariana Islands" },
					{ "USA", "OH", "Ohio" },
					{ "USA", "OK", "Oklahoma" },
					{ "USA", "OR", "Oregon" },
					{ "USA", "PW", "Palau" },
					{ "USA", "PA", "Pennsylvania" },
					{ "USA", "PR", "Puerto Rico" },
					{ "USA", "FI", "Rhode Island" },
					{ "USA", "SC", "South Carolina" },
					{ "USA", "SD", "South Dakota" },
					{ "USA", "TN", "Tennessee" },
					{ "USA", "TX", "Texas" },
					{ "USA", "UT", "Utah" },
					{ "USA", "VT", "Vermont" },
					{ "USA", "VI", "Virgin Islands" },
					{ "USA", "VA", "Virginia" },
					{ "USA", "WA", "Washington" },
					{ "USA", "WV", "West Virginia" },
					{ "USA", "WI", "Wisconsin" },
					{ "USA", "WY", "Wyoming" }
				};
				retValue.LJCSerialize(fileSpec);
				StateLookups.LJCDeserialize(StateLookups.LJCDefaultFileName);
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public States()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/States.xml'/>
		public State Add(string countryCode, string code, string name)
		{
			State retValue = null;

			if (NetString.HasValue(countryCode)
				&& NetString.HasValue(code)
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchCode(countryCode, code);
				if (null == retValue)
				{
					retValue = new State()
					{
						CountryCode = countryCode,
						Code = code,
						Name = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='Doc/States.xml'/>
		public State LJCSearchCode(string countryCode, string code)
		{
			State retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			State searchItem = new State()
			{
				CountryCode = countryCode,
				Code = code
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Serializes the collection to a file.
		/// <include path='items/LJCSerialize/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public void LJCSerialize(string fileSpec = null)
		{
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
		}

		// Updates the Lookups object.
		/// <include path='items/LJCUpdateLookups/*' file='Doc/States.xml'/>
		public StateLookups LJCUpdateLookups(StateLookups stateLookups)
		{
			StateLookups retValue = stateLookups;
			if (null == retValue)
			{
				retValue = new StateLookups();
			}
			foreach (State state in this)
			{
				string value = state.Code[0] + state.Code.Substring(1).ToLower();
				retValue.Add(state.CountryCode, state.Code, value);
				value = state.Name[0] + state.Name.Substring(1).ToLower();
				retValue.Add(state.CountryCode, state.Code, value);
			}
			retValue.LJCGenerateSoundex();
			retValue.LJCSerialize();
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\States.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
