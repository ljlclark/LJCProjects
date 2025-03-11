// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StateLookups.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a colletion of StateLookup objects.
	/// <include path='items/StateLookups/*' file='Doc/StateLookups.xml'/>
	[XmlRoot("StateLookups")]
	public class StateLookups : List<StateLookup>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/StateLookups.xml'/>
		public static StateLookups LJCDeserialize(string fileSpec = null)
		{
			StateLookups retValue;

			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateStateLookups(out string _, fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(StateLookups)
					, fileSpec) as StateLookups;
			}
			return retValue;
		}

		// Creates the default StateLookup values.
		private static StateLookups CreateStateLookups(out string errorText
			, string fileSpec)
		{
			StateLookups retValue = null;

			errorText = null;
			if (!File.Exists(fileSpec))
			{
				retValue = new StateLookups();
				States states = States.LJCDeserialize(out errorText);
				foreach (State state in states)
				{
					retValue.Add(state.CountryCode, state.Code, state.Name);
				}
				retValue.LJCGenerateSoundex();
				retValue.LJCSerialize();
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public StateLookups()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/StateLookups.xml'/>
		public StateLookup Add(string countryCode, string code, string lookupName)
		{
			StateLookup retValue = null;

			if (NetString.HasValue(countryCode)
				&& NetString.HasValue(lookupName)
				&& NetString.HasValue(code))
			{
				retValue = LJCSearchLookupName(countryCode, lookupName);
				if (null == retValue)
				{
					retValue = new StateLookup()
					{
						CountryCode = countryCode,
						LookupName = lookupName,
						Code = code
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Finds and returns the object that contains the supplied values.
		/// <include path='items/LJCSearchLookupName/*' file='Doc/StateLookups.xml'/>
		public StateLookup LJCSearchLookupName(string countryCode, string lookupName)
		{
			StateLookup retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LookupName) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.LookupName;
			}

			StateLookup searchItem = new StateLookup()
			{
				CountryCode = countryCode,
				LookupName = lookupName
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Finds and returns the object that contains the supplied values.
		/// <include path='items/LJCSearchLSoundex/*' file='Doc/StateLookups.xml'/>
		public StateLookup LJCSearchLSoundex(string countryCode, string lSoundex)
		{
			StateLSoundexComparer lSoundexComparer;
			StateLookup retValue = null;

			lSoundexComparer = new StateLSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(lSoundexComparer);
				mSortType = SortType.LSoundex;
			}

			StateLookup searchObject = new StateLookup()
			{
				CountryCode = countryCode,
				LSoundex = lSoundex
			};
			int index = BinarySearch(searchObject, lSoundexComparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Finds and returns the object that contains the supplied values.
		/// <include path='items/LJCSearchPSoundex/*' file='Doc/StateLookups.xml'/>
		public StateLookup LJCSearchPSoundex(string countryCode, string pSoundex)
		{
			StatePSoundexComparer pSoundexComparer;
			StateLookup retValue = null;

			pSoundexComparer = new StatePSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.PSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(pSoundexComparer);
				mSortType = SortType.PSoundex;
			}

			StateLookup searchObject = new StateLookup()
			{
				CountryCode = countryCode,
				PSoundex = pSoundex
			};
			int index = BinarySearch(searchObject, pSoundexComparer);
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

		// Generates the Soundex values.
		/// <include path='items/LJCGenerateSoundex/*' file='Doc/StateLookups.xml'/>
		public void LJCGenerateSoundex()
		{
			foreach (StateLookup stateLookup in this)
			{
				stateLookup.LSoundex = NetString.CreateLSoundex(stateLookup.LookupName);
				stateLookup.PSoundex = NetString.CreatePSoundex(stateLookup.LookupName);
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\StateLookups.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			Code,
			LookupName,
			LSoundex,
			PSoundex
		}
		#endregion
	}
}
