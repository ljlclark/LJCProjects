// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PrimaryRoads.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	/// <summary>Represents a collection of PrimaryRoad objects.</summary>
	public class PrimaryRoads : List<PrimaryRoad>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/States.xml'/>
		public static PrimaryRoads LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			PrimaryRoads retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreatePrimaryRoads(fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(PrimaryRoads)
					, fileSpec) as PrimaryRoads;
			}
			return retValue;
		}

		// Create the default PrimaryRoad values.
		private static PrimaryRoads CreatePrimaryRoads(string fileSpec)
		{
			PrimaryRoads retValue = null;

			if (!File.Exists(fileSpec))
			{
				retValue = new PrimaryRoads
				{
					{ "USA", "Bypass", "BYP" },
					{ "USA", "County", "CNTY" },
					{ "USA", "County Road", "CR" },
					{ "USA", "Expressway", "EXPY" },
					{ "USA", "Highway", "HWY" },
					{ "USA", "Interstate", "I" },
					{ "USA", "Loop" },
					{ "USA", "Road", "RD" },
					{ "USA", "Route", "RT" },
					{ "USA", "State", "ST" },
					{ "USA", "State Road", "SR" },
					{ "USA", "Route", "RT" },
					{ "USA", "Township Road", "TSR" },
					{ "USA", "US", "US" }
				};
				retValue.LJCSerialize(fileSpec);
				RoadLookups.LJCDeserialize(out string _
					, RoadLookups.LJCDefaultFileName);
			}
			return retValue;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/PrimaryRoads.xml'/>
		public PrimaryRoad Add(string countryCode, string name, string code = null
			, string stateCode = null)
		{
			PrimaryRoad retValue = null;

			if (NetString.HasValue(countryCode)
				&& NetString.HasValue(code)
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchCode(countryCode, code);
				if (null == retValue)
				{
					retValue = new PrimaryRoad()
					{
						CountryCode = countryCode,
						StateCode = stateCode,
						Code = code,
						Name = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='Doc/PrimaryRoads.xml'/>
		public PrimaryRoad LJCSearchCode(string countryCode, string code)
		{
			PrimaryRoad retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			PrimaryRoad searchItem = new PrimaryRoad()
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
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\PrimaryRoads.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
