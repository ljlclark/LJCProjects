// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Suffixes.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a colletion of StateLookup objects.
	/// <include path='items/Suffixes/*' file='Doc/Suffixes.xml'/>
	[XmlRoot("Suffixes")]
	public class Suffixes : List<Suffix>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/Suffixes.xml'/>
		public static Suffixes LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			Suffixes retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateSuffixes(fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(Suffixes)
					, fileSpec) as Suffixes;
			}
			return retValue;
		}

		// Creates the default Suffix values.
		private static Suffixes CreateSuffixes(string fileSpec)
		{
			Suffixes retValue = null;

			if (!File.Exists(fileSpec))
			{
				retValue = new Suffixes
				{
					{ "ALY", "ALLEY" },
					{ "ANX", "ANNEX" },
					{ "ARC", "ARCADE" },
					{ "AVE", "AVENUE" },
					{ "BYU", "BAYOU" },
					{ "BCH", "BEACH" },
					{ "BND", "BEND" },
					{ "BLF", "BLUFF" },
					{ "BLFS", "BLUFFS" },
					{ "BTM", "BOTTOM" },
					{ "BLVD", "BOULEVARD" },
					{ "BR", "BRANCH" },
					{ "BRG", "BRIDGE" },
					{ "BRK", "BROOK" },
					{ "BRKS", "BROOKS" },
					{ "BG", "BURG" },
					{ "BGS", "BURGS" },
					{ "BYP", "BYPASS" },
					{ "CP", "CAMP" },
					{ "CYN", "CANYON" },
					{ "CPE", "CAPE" },
					{ "CSWY", "CAUSEWAY" },
					{ "CTR", "CENTER" },
					{ "CTRS", "CENTERS" },
					{ "CIR", "CIRCLE" },
					{ "CIRS", "CIRCLES" },
					{ "CLF", "CLIFF" },
					{ "CLFS", "CLIFFS" },
					{ "CLB", "CLUB" },
					{ "CMN", "COMMON" },
					{ "CMNS", "COMMONS" },
					{ "COR", "CORNER" },
					{ "CORS", "CORNERS" },
					{ "CRSE", "COURSE" },
					{ "CT", "COURT" },
					{ "CTS", "COURTS" },
					{ "CV", "COVE" },
					{ "CVS", "COVES" },
					{ "CRK", "CREEK" },
					{ "CRES", "CRESCENT" },
					{ "CRST", "CREST" },
					{ "XING", "CROSSING" },
					{ "XRD", "CROSSROAD" },
					{ "XRDS", "CROSSROADS" },
					{ "CURV", "CURVE" },
					{ "DL", "DALE" },
					{ "DM", "DAM" },
					{ "DV", "DIVIDE" },
					{ "DR", "DRIVE" },
					{ "DRS", "DRIVES" },
					{ "EST", "ESTATE" },
					{ "ESTS", "ESTATES" },
					{ "EXPY", "EXPRESSWAY" },
					{ "EXT", "EXTENSION" },
					{ "EXTS", "EXTENSIONS" },
					{ "FALL", "FALL" },
					{ "FLS", "FALLS" },
					{ "FRY", "FERRY" },
					{ "FLD", "FIELD" },
					{ "FLDS", "FLDS" },
					{ "FLT", "FLAT" },
					{ "FLTS", "FLATS" },
					{ "FRD", "FORD" },
					{ "FRDS", "FORDS" },
					{ "FRST", "FOREST" },
					{ "FRG", "FORGE" },
					{ "FRGS", "FORGES" },
					{ "FRK", "FORK" },
					{ "FRKS", "FORKS" },
					{ "FT", "FORT" },
					{ "FWY", "FREEWAY" },
					{ "GDN", "GARDEN" },
					{ "GDNS", "GARDENS" },
					{ "GTWY", "GATEWAY" },
					{ "GLN", "GLEN" },
					{ "GLNS", "GLENS" },
					{ "GRN", "GREEN" },
					{ "GRNS", "GREENS" },
					{ "GRV", "GROVE" },
					{ "GRVS", "GROVES" },
					{ "HBR", "HARBOR" },
					{ "HBRS", "HARBORS" },
					{ "HVN", "HAVEN" },
					{ "HTS", "HEIGHTS" },
					{ "HWY", "HIGHWAY" },
					{ "HL", "HILL" },
					{ "HLS", "HILLS" },
					{ "HOLW", "HOLLOW" },
					{ "INLT", "INLET" },
					{ "IS", "ISLAND" },
					{ "ISS", "ISLANDS" },
					{ "ISLE", "ISLE" },
					{ "JCT", "JUNCTION" },
					{ "JCTS", "JUNCTIONS" },
					{ "KY", "KEY" },
					{ "KYS", "KEYS" },
					{ "KNL", "KNOLL" },
					{ "KNLS", "KNOLLS" },
					{ "LK", "LAKE" },
					{ "LKS", "LAKES" },
					{ "LAND", "LAND" },
					{ "LNDG", "LANDING" },
					{ "LN", "LANE" },
					{ "LGT", "LIGHT" },
					{ "LGTS", "LIGHTS" },
					{ "LF", "LOAF" },
					{ "LCK", "LOCK" },
					{ "LCKS", "LOCKS" },
					{ "LDG", "LODGE" },
					{ "LOOP", "LOOP" },
					{ "MALL", "MALL" },
					{ "MNR", "MANOR" },
					{ "MNRS", "MANORS" },
					{ "MDW", "MEADOW" },
					{ "MDWS", "MEADOWS" },
					{ "MEWS", "MEWS" },
					{ "ML", "MILL" },
					{ "MLS", "MILLS" },
					{ "MSN", "MISSION" },
					{ "MTWY", "MOTORWAY" },
					{ "MT", "MOUNT" },
					{ "MTN", "MOUNTAIN" },
					{ "MTNS", "MOUNTAINS" },
					{ "NCK", "NECK" },
					{ "ORCH", "ORCHARD" },
					{ "OVAL", "OVAL" },
					{ "OPAS", "OVERPASS" },
					{ "PARK", "PARK" },
					{ "PKWY", "PARKWAY" },
					{ "PASS", "PASS" },
					{ "PSGE", "PASSAGE" },
					{ "PATH", "PATH" },
					{ "PIKE", "PIKE" },
					{ "PNE", "PINE" },
					{ "PNES", "PINES" },
					{ "PL", "PLACE" },
					{ "PLN", "PLAIN" },
					{ "PLNS", "PLAINS" },
					{ "PLZ", "PLAZA" },
					{ "PT", "POINT" },
					{ "PTS", "POINTS" },
					{ "PRT", "PORT" },
					{ "PRTS", "PORTS" },
					{ "PR", "PRAIRIE" },
					{ "RADL", "RADIAL" },
					{ "RAMP", "RAMP" },
					{ "RNCH", "RANCH" },
					{ "RPD", "RAPID" },
					{ "RPDS", "RAPIDS" },
					{ "RST", "REST" },
					{ "RDG", "RIDGE" },
					{ "RDGS", "RIDGES" },
					{ "RIV", "RIVER" },
					{ "RD", "ROAD" },
					{ "RDS", "ROADS" },
					{ "RTE", "ROUTE" },
					{ "ROW", "ROW" },
					{ "RUE", "RUE" },
					{ "RUN", "RUN" },
					{ "SHL", "SHOAL" },
					{ "SHLS", "SHOALS" },
					{ "SHR", "SHORE" },
					{ "SHRS", "SHORES" },
					{ "SKWY", "SKYWAY" },
					{ "SPG", "SPRING" },
					{ "SPGS", "SPRINGS" },
					{ "SPUR", "SPUR" },
					{ "SQ", "SQUARE" },
					{ "SQS", "SQUARES" },
					{ "STA", "STATION" },
					{ "STRA", "STRAVENUE" },
					{ "STRM", "STREAM" },
					{ "ST", "STREET" },
					{ "STS", "STREETS" },
					{ "SMT", "SUMMIT" },
					{ "TER", "TERRACE" },
					{ "TRWY", "THROUGHWAY" },
					{ "TRCE", "RACE" },
					{ "TRAK", "TRACK" },
					{ "TRFY", "TRAFFICWAY" },
					{ "TRL", "TRAIL" },
					{ "TRLR", "TRAILER" },
					{ "TUNL", "TUNNEL" },
					{ "TPKE", "TURNPIKE" },
					{ "UPAS", "UNDERPASS" },
					{ "UN", "UNION" },
					{ "UNS", "UNIONS" },
					{ "VLY", "VALLEY" },
					{ "VLYS", "VALLEYS" },
					{ "VIA", "VIADUCT" },
					{ "VW", "VIEW" },
					{ "VWS", "VIEWS" },
					{ "VLG", "VILLAGE" },
					{ "VLGS", "VILLAGES" },
					{ "VL", "VILLE" },
					{ "VIS", "VISTA" },
					{ "WALK", "WALK" },
					{ "WALL", "WALL" },
					{ "WAY", "WAY" },
					{ "WAYS", "WAYS" },
					{ "WL", "WELL" },
					{ "WLS", "WELLS" }
				};
				retValue.LJCSerialize();
				SuffixLookups.LJCDeserialize(SuffixLookups.LJCDefaultFileName);
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Suffixes()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/LJCAddCode/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Suffix Add(string code, string name)
		{
			Suffix retValue = null;

			if (NetString.HasValue(code)
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchCode(code);
				if (null == retValue)
				{
					retValue = new Suffix()
					{
						Code = code,
						Name = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Suffix LJCSearchCode(string code)
		{
			Suffix retValue = null;

			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}

			Suffix searchItem = new Suffix()
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
		/// <include path='items/LJCUpdateLookups/*' file='Doc/Suffixes.xml'/>
		public SuffixLookups LJCUpdateLookups(SuffixLookups suffixLookups)
		{
			SuffixLookups retValue = suffixLookups;
			if (null == retValue)
			{
				retValue = new SuffixLookups();
			}
			foreach (Suffix suffix in this)
			{
				string value = suffix.Code[0] + suffix.Code.Substring(1).ToLower();
				retValue.Add(suffix.Code, value);
				value = suffix.Name[0] + suffix.Name.Substring(1).ToLower();
				retValue.Add(suffix.Code, value);
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
			get { return "AddressData\\Suffixes.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
