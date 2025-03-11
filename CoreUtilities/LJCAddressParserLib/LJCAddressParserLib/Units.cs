// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Units.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a colletion of Unit objects.
	/// <include path='items/Units/*' file='Doc/Units.xml'/>
	[XmlRoot("Units")]
	public class Units : List<Unit>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/Units.xml'/>
		public static Units LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			Units retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateUnits(fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(Units)
					, fileSpec) as Units;
			}
			return retValue;
		}

		// Creates the default Unit values.
		private static Units CreateUnits(string fileSpec)
		{
			Units retValue = null;

			if (!File.Exists(fileSpec))
			{
				retValue = new Units
				{
					{ "APT", "Apartment" },
					{ "BSMT", "Basement", false },
					{ "BLDG", "Building" },
					{ "DEPT", "Department" },
					{ "FL", "Floor" },
					{ "FRNT", "Front", false },
					{ "HNGR", "Hanger" },
					{ "KEY", "Key" },
					{ "LBBY", "Lobby", false },
					{ "LOT", "Lot" },
					{ "LOWR", "Lower", false },
					{ "OFC", "Office", false },
					{ "PH", "Penthouse", false },
					{ "PIER", "Pier" },
					{ "REAR", "Rear", false },
					{ "RM", "Room" },
					{ "SIDE", "Side", false },
					{ "SLIP", "Skip" },
					{ "SPC", "Space" },
					{ "STOP", "Stop" },
					{ "STE", "Suite" },
					{ "TRLR", "Trailer" },
					{ "UNIT", "Unit" },
					{ "UPPR", "Upper", false }
				};
				retValue.LJCSerialize();
				UnitLookups.LJCDeserialize(out string _
					, UnitLookups.LJCDefaultFileName);
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Units()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/Units.xml'/>
		public Unit Add(string code, string name, bool requiresRange = true)
		{
			Unit retValue = null;

			if (NetString.HasValue(code)
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchCode(code);
				if (null == retValue)
				{
					retValue = new Unit()
					{
						Code = code,
						Name = name,
						RequiresRange = requiresRange
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Unit LJCSearchCode(string code)
		{
			Unit retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Code) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.Code;
			}

			Unit searchItem = new Unit()
			{
				Code = code
			};
			int index = BinarySearch(searchItem);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Finds and returns the object that contains the supplied values.
		/// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Unit LJCSearchName(string name)
		{
			UnitNameComparer nameComparer;
			Unit retValue = null;

			nameComparer = new UnitNameComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Name) != 0)
			{
				mPrevCount = Count;
				Sort(nameComparer);
				mSortType = SortType.Name;
			}

			Unit searchObject = new Unit()
			{
				Name = name
			};
			int index = BinarySearch(searchObject, nameComparer);
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
		/// <include path='items/LJCUpdateLookups/*' file='Doc/Units.xml'/>
		public UnitLookups LJCUpdateLookups(UnitLookups unitLookups)
		{
			UnitLookups retValue = unitLookups;
			if (null == retValue)
			{
				retValue = new UnitLookups();
			}
			foreach (Unit unit in this)
			{
				string value = unit.Code[0] + unit.Code.Substring(1).ToLower();
				retValue.Add(unit.Code, value);
				value = unit.Name[0] + unit.Name.Substring(1).ToLower();
				retValue.Add(unit.Code, value);
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
			get { return "AddressData\\Units.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			Code,
			Name
		}
		#endregion
	}
}
