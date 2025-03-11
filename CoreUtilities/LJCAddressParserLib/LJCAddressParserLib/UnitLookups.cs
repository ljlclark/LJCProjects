// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitLookups.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a colletion of SuffixLookup objects.
	/// <include path='items/UnitLookups/*' file='Doc/UnitLookups.xml'/>
	[XmlRoot("UnitLookups")]
	public class UnitLookups : List<UnitLookup>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/UnitLookups.xml'/>
		public static UnitLookups LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			UnitLookups retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateUnitLookups(out _, fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(UnitLookups)
					, fileSpec) as UnitLookups;
			}
			return retValue;
		}

		// Creates the default UnitLookup values.
		private static UnitLookups CreateUnitLookups(out string errorText, string fileSpec)
		{
			UnitLookups retValue = null;

			errorText = null;
			if (!File.Exists(fileSpec))
			{
				retValue = new UnitLookups();
				Units units = Units.LJCDeserialize(out errorText);
				foreach (Unit unit in units)
				{
					retValue.Add(unit.Code, unit.Name);
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
		public UnitLookups()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/UnitLookups.xml'/>
		public UnitLookup Add(string code, string lookupName)
		{
			UnitLookup retValue = null;

			if (NetString.HasValue(lookupName)
				&& NetString.HasValue(code))
			{
				retValue = LJCSearchLookupName(lookupName);
				if (null == retValue)
				{
					retValue = new UnitLookup()
					{
						LookupName = lookupName,
						Code = code
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Finds and returns the object that contains the supplied values.
		/// <include path='items/LJCSearchLookupName/*' file='Doc/UnitLookups.xml'/>
		public UnitLookup LJCSearchLookupName(string lookupName)
		{
			UnitLookup retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LookupName) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.LookupName;
			}

			UnitLookup searchItem = new UnitLookup()
			{
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
		/// <include path='items/LJCSearchLSoundex/*' file='Doc/UnitLookups.xml'/>
		public UnitLookup LJCSearchLSoundex(string lSoundex)
		{
			UnitLSoundexComparer lSoundexComparer;
			UnitLookup retValue = null;

			lSoundexComparer = new UnitLSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(lSoundexComparer);
				mSortType = SortType.LSoundex;
			}

			UnitLookup searchObject = new UnitLookup()
			{
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
		/// <include path='items/LJCSearchPSoundex/*' file='Doc/UnitLookups.xml'/>
		public UnitLookup LJCSearchPSoundex(string pSoundex)
		{
			UnitPSoundexComparer pSoundexComparer;
			UnitLookup retValue = null;

			pSoundexComparer = new UnitPSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.PSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(pSoundexComparer);
				mSortType = SortType.PSoundex;
			}

			UnitLookup searchObject = new UnitLookup()
			{
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
		/// <include path='items/LJCGenerateSoundex/*' file='Doc/UnitLookups.xml'/>
		public void LJCGenerateSoundex()
		{
			foreach (UnitLookup unitLookup in this)
			{
				unitLookup.LSoundex = NetString.CreateLSoundex(unitLookup.LookupName);
				unitLookup.PSoundex = NetString.CreatePSoundex(unitLookup.LookupName);
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\UnitLookups.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			LookupName,
			LSoundex,
			PSoundex
		}
		#endregion
	}
}
