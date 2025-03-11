// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RoadLookups.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	/// <summary>Represents a collection of RoadLookup objects.</summary>
	public class RoadLookups : List<RoadLookup>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/RoadLookups.xml'/>
		public static RoadLookups LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			RoadLookups retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateRoadLookups(out errorText, fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(RoadLookups)
					, fileSpec) as RoadLookups;
			}
			return retValue;
		}

		// Create the default RoadLookup values.
		private static RoadLookups CreateRoadLookups(out string errorText
			, string fileSpec)
		{
			RoadLookups retValue = null;

			errorText = null;
			if (!File.Exists(fileSpec))
			{
				retValue = new RoadLookups();
				PrimaryRoads primaryRoads = PrimaryRoads.LJCDeserialize(out errorText);
				foreach (PrimaryRoad primaryRoad in primaryRoads)
				{
					retValue.Add(primaryRoad.CountryCode, primaryRoad.Code, primaryRoad.Name);
				}

				retValue.Add("USA", "HWY", "HIWAY");
				retValue.Add("USA", "HWY", "HIWY");
				retValue.Add("USA", "I", "IH");

				retValue.LJCGenerateSoundex();
				retValue.LJCSerialize();
			}
			return retValue;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/RoadLookups.xml'/>
		public RoadLookup Add(string countryCode, string code, string lookupName)
		{
			RoadLookup retValue = null;

			if (NetString.HasValue(countryCode)
				&& NetString.HasValue(lookupName)
				&& NetString.HasValue(code))
			{
				retValue = LJCSearchLookupName(countryCode, lookupName);
				if (null == retValue)
				{
					retValue = new RoadLookup()
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
		/// <include path='items/LJCSearchLookupName/*' file='Doc/RoadLookups.xml'/>
		public RoadLookup LJCSearchLookupName(string countryCode, string lookupName)
		{
			RoadLookup retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LookupName) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.LookupName;
			}

			RoadLookup searchItem = new RoadLookup()
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
		/// <include path='items/LJCSearchLSoundex/*' file='Doc/RoadLookups.xml'/>
		public RoadLookup LJCSearchLSoundex(string countryCode, string lSoundex)
		{
			RoadLSoundexComparer lSoundexComparer;
			RoadLookup retValue = null;

			lSoundexComparer = new RoadLSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(lSoundexComparer);
				mSortType = SortType.LSoundex;
			}

			RoadLookup searchObject = new RoadLookup()
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
		/// <include path='items/LJCSearchPSoundex/*' file='Doc/RoadLookups.xml'/>
		public RoadLookup LJCSearchPSoundex(string countryCode, string pSoundex)
		{
			RoadPSoundexComparer pSoundexComparer;
			RoadLookup retValue = null;

			pSoundexComparer = new RoadPSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.PSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(pSoundexComparer);
				mSortType = SortType.PSoundex;
			}

			RoadLookup searchObject = new RoadLookup()
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
		/// <summary>Generates the Soundex values.</summary>
		public void LJCGenerateSoundex()
		{
			foreach (RoadLookup roadLookup in this)
			{
				roadLookup.LSoundex = NetString.CreateLSoundex(roadLookup.LookupName);
				roadLookup.PSoundex = NetString.CreatePSoundex(roadLookup.LookupName);
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\RoadLookups.xml"; }
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
