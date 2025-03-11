// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SuffixLookups.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a colletion of SuffixLookup objects.
	/// <include path='items/SuffixLookups/*' file='Doc/SuffixLookups.xml'/>
	[XmlRoot("SuffixLookups")]
	public class SuffixLookups : List<SuffixLookup>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/SuffixLookups.xml'/>
		public static SuffixLookups LJCDeserialize(string fileSpec = null)
		{
			SuffixLookups retValue;

			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateSuffixLookups(out string _, fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(SuffixLookups)
					, fileSpec) as SuffixLookups;
			}
			return retValue;
		}

		// Creates the default SuffixLookup values.
		private static SuffixLookups CreateSuffixLookups(out string errorText
			, string fileSpec)
		{
			SuffixLookups retValue = null;

			errorText = null;
			if (!File.Exists(fileSpec))
			{
				retValue = new SuffixLookups();
				Suffixes suffixes = Suffixes.LJCDeserialize(out errorText);
				foreach (Suffix suffix in suffixes)
				{
					retValue.Add(suffix.Code, suffix.Name);
				}

				retValue.Add("ALY", "ALLEE");
				retValue.Add("ALY", "ALLY");

				retValue.Add("ANX", "ANNEX");
				retValue.Add("ANX", "ANNX");

				retValue.Add("AVE", "AV");
				retValue.Add("AVE", "AVEN");
				retValue.Add("AVE", "AVENU");
				retValue.Add("AVE", "AVNUE");

				retValue.Add("BYU", "BAYOO");

				retValue.Add("BLF", "BLUF");

				retValue.Add("BTM", "BOT");
				retValue.Add("BTM", "BOTTM");

				retValue.Add("BLVD", "BOUL");
				retValue.Add("BLVD", "BOULV");

				retValue.Add("BR", "BRNCH");

				retValue.Add("BRG", "BRDGE");

				retValue.Add("BYP", "BYPA");
				retValue.Add("BYP", "BYPAS");
				retValue.Add("BYP", "BYPS");

				retValue.Add("CP", "CMP");

				retValue.Add("CYN", "CNYN");
				retValue.Add("CYN", "CANYN");

				retValue.Add("CSWY", "CAUSWA");

				retValue.Add("CTR", "CEN");
				retValue.Add("CTR", "CENT");
				retValue.Add("CTR", "CENTR");
				retValue.Add("CTR", "CENTRE");
				retValue.Add("CTR", "CNTER");
				retValue.Add("CTR", "CNTR");

				retValue.Add("CIR", "CIRC");
				retValue.Add("CIR", "CIRCL");
				retValue.Add("CIR", "CRCL");
				retValue.Add("CIR", "CRCLE");

				retValue.Add("CRES", "CRSENT");
				retValue.Add("CRES", "CRSNT");

				retValue.Add("XING", "CRSSNG");

				retValue.Add("DV", "DIV");
				retValue.Add("DV", "DVD");

				retValue.Add("DR", "DRIV");
				retValue.Add("DR", "DRV");

				retValue.Add("EXPY", "EXP");
				retValue.Add("EXPY", "EXPR");
				retValue.Add("EXPY", "EXPRESS");
				retValue.Add("EXPY", "EXPW");

				retValue.Add("EXT", "EXTN");
				retValue.Add("EXT", "EXTNSN");

				retValue.Add("FRY", "FRRY");

				retValue.Add("FRST", "FORESTS");

				retValue.Add("FRG", "FORG");

				retValue.Add("FT", "FRT");

				retValue.Add("FWY", "FREEWY");
				retValue.Add("FWY", "FRWAY");
				retValue.Add("FWY", "FRWY");

				retValue.Add("GDN", "GARDN");
				retValue.Add("GDN", "GRDEN");
				retValue.Add("GDN", "GRDN");

				retValue.Add("GDNS", "GRDNS");

				retValue.Add("GTWY", "GATEWY");
				retValue.Add("GTWY", "GATWAY");
				retValue.Add("GTWY", "GTWAY");

				retValue.Add("GRV", "GROV");

				retValue.Add("HBR", "HARB");
				retValue.Add("HBR", "HARBR");
				retValue.Add("HBR", "HRBOR");

				retValue.Add("HWY", "HIGHWY");
				retValue.Add("HWY", "HIWAY");
				retValue.Add("HWY", "HIWY");
				retValue.Add("HWY", "HWAY");

				retValue.Add("HOLW", "HLLW");
				retValue.Add("HOLW", "HOLLOWS");
				retValue.Add("HOLW", "HOLWS");

				retValue.Add("IS", "ISLND");

				retValue.Add("ISS", "ISLNDS");

				retValue.Add("JCT", "JCTION");
				retValue.Add("JCT", "JCTN");
				retValue.Add("JCT", "JUNCTN");
				retValue.Add("JCT", "JUNCTON");

				retValue.Add("JCTS", "JCTNS");

				retValue.Add("KNL", "KNOL");

				retValue.Add("LNDG", "LNDNG");

				retValue.Add("LDG", "LDGE");
				retValue.Add("LDG", "LODG");

				retValue.Add("MDWS", "MEDOWS");

				retValue.Add("MT", "MNT");

				retValue.Add("MTN", "MNTAIN");
				retValue.Add("MTN", "MNTN");
				retValue.Add("MTN", "MOUNTIN");
				retValue.Add("MTN", "MTIN");

				retValue.Add("ORCH", "ORCHRD");

				retValue.Add("PARK", "PARKS");

				retValue.Add("PKWY", "PARKWY");
				retValue.Add("PKWY", "PKWAY");
				retValue.Add("PKWY", "PKY");
				retValue.Add("PKWY", "PARKWAYS");
				retValue.Add("PKWY", "PKWYS");

				retValue.Add("PLZ", "PLZA");

				retValue.Add("RADL", "RAD");
				retValue.Add("RADL", "RADIEL");

				retValue.Add("RNCH", "RANCHES");
				retValue.Add("RNCH", "RNCHS");

				retValue.Add("RIV", "RVR");
				retValue.Add("RIV", "RIVR");

				retValue.Add("SHRS", "SHOARS");

				retValue.Add("SPG", "SPNG");
				retValue.Add("SPG", "SPRNG");

				retValue.Add("SPGS", "SPNGS");
				retValue.Add("SPGS", "SPRNGS");

				retValue.Add("SQ", "SQR");
				retValue.Add("SQ", "SQRE");
				retValue.Add("SQ", "SQU");

				retValue.Add("STA", "STATN");
				retValue.Add("STA", "STN");

				retValue.Add("STRA", "STRAV");
				retValue.Add("STRA", "STRAVEN");
				retValue.Add("STRA", "STRAVN");
				retValue.Add("STRA", "STRVN");
				retValue.Add("STRA", "STRVNUE");

				retValue.Add("STRM", "STREME");

				retValue.Add("ST", "STRT");
				retValue.Add("ST", "STR");

				retValue.Add("SMT", "SUMIT");
				retValue.Add("SMT", "SUMITT");

				retValue.Add("TER", "TERR");

				retValue.Add("TRCE", "TRACES");

				retValue.Add("TRAK", "TRACKS");
				retValue.Add("TRAK", "TRAK");
				retValue.Add("TRAK", "TRK");

				retValue.Add("TRL", "TRAILS");
				retValue.Add("TRL", "TRLS");

				retValue.Add("TRLR", "TRLRS");

				retValue.Add("TUNL", "TUNEL");
				retValue.Add("TUNL", "TUNLS");
				retValue.Add("TUNL", "TUNNELS");
				retValue.Add("TUNL", "TUNNL");

				retValue.Add("TPKE", "TRNPK");
				retValue.Add("TPKE", "TURNPK");

				retValue.Add("VLY", "VALLY");
				retValue.Add("VLY", "VLLY");

				retValue.Add("VIA", "VDCT");
				retValue.Add("VIA", "VIADCT");

				retValue.Add("VLG", "VILL");
				retValue.Add("VLG", "VILLAG");
				retValue.Add("VLG", "VILLG");
				retValue.Add("VLG", "VILLIAG");

				retValue.Add("VIS", "VIST");
				retValue.Add("VIS", "VST");
				retValue.Add("VIS", "VSTA");

				retValue.Add("WALK", "WALKS");

				retValue.LJCGenerateSoundex();
				retValue.LJCSerialize();
			}
			return retValue;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/SuffixLookups.xml'/>
		public SuffixLookup Add(string code, string lookupName)
		{
			SuffixLookup retValue = null;

			if (NetString.HasValue(lookupName)
				&& NetString.HasValue(code))
			{
				retValue = LJCSearchLookupName(lookupName);
				if (null == retValue)
				{
					retValue = new SuffixLookup()
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
		/// <include path='items/LJCSearchLookupName/*' file='Doc/SuffixLookups.xml'/>
		public SuffixLookup LJCSearchLookupName(string lookupName)
		{
			SuffixLookup retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LookupName) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.LookupName;
			}

			SuffixLookup searchItem = new SuffixLookup()
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
		/// <include path='items/LJCSearchLSoundex/*' file='Doc/SuffixLookups.xml'/>
		public SuffixLookup LJCSearchLSoundex(string lSoundex)
		{
			SuffixLSoundexComparer lSoundexComparer;
			SuffixLookup retValue = null;

			lSoundexComparer = new SuffixLSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.LSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(lSoundexComparer);
				mSortType = SortType.LSoundex;
			}

			SuffixLookup searchObject = new SuffixLookup()
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
		/// <include path='items/LJCSearchPSoundex/*' file='Doc/SuffixLookups.xml'/>
		public SuffixLookup LJCSearchPSoundex(string pSoundex)
		{
			SuffixPSoundexComparer pSoundexComparer;
			SuffixLookup retValue = null;

			pSoundexComparer = new SuffixPSoundexComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.PSoundex) != 0)
			{
				mPrevCount = Count;
				Sort(pSoundexComparer);
				mSortType = SortType.PSoundex;
			}

			SuffixLookup searchObject = new SuffixLookup()
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
		/// <include path='items/LJCGenerateSoundex/*' file='Doc/SuffixLookups.xml'/>
		public void LJCGenerateSoundex()
		{
			foreach (SuffixLookup suffixLookup in this)
			{
				suffixLookup.LSoundex = NetString.CreateLSoundex(suffixLookup.LookupName);
				suffixLookup.PSoundex = NetString.CreatePSoundex(suffixLookup.LookupName);
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "AddressData\\SuffixLookups.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			LookupName,
			Code,
			LSoundex,
			PSoundex
		}
		#endregion
	}
}
