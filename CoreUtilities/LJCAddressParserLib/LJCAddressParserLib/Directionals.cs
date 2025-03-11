// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Directionals.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCAddressParserLib
{
	// Represents a collection of Directional objects.
	/// <include path='items/Directionals/*' file='Doc/Directionals.xml'/>
	[XmlRoot("States")]
	public class Directionals : List<Directional>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='Doc/Directionals.xml'/>
		public static Directionals LJCDeserialize(out string errorText
			, string fileSpec = null)
		{
			Directionals retValue;

			errorText = null;
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				retValue = CreateDirectionals(fileSpec);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(Directionals)
					, fileSpec) as Directionals;
			}
			return retValue;
		}

		// Create the default Directional values.
		private static Directionals CreateDirectionals(string fileSpec)
		{
			Directionals retValue = null;

			if (!File.Exists(fileSpec))
			{
				retValue = new Directionals
				{
					{ "N", "North", "Norte" },
					{ "NE", "Northeast", "Noreste" },
					{ "NW", "Northwest", "Noroeste" },  // NO
					{ "S", "South", "Sur" },
					{ "SE", "Southeast", "Sureste" },
					{ "SW", "Southwest", "Suroeste" },  // SO
					{ "E", "East", "Este" },
					{ "W", "West", "Oeste" }  // O
				};
				retValue.LJCSerialize(fileSpec);
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Directionals()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the supplied values.
		/// <include path='items/Add/*' file='Doc/Directionals.xml'/>
		public Directional Add(string code, string name, string spanishName = null)
		{
			Directional retValue = null;

			if (NetString.HasValue(code)
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchCode(code);
				if (null == retValue)
				{
					retValue = new Directional()
					{
						Code = code,
						Name = name,
						SpanishName = spanishName
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Directional LJCSearchCode(string code)
		{
			Directional retValue = null;

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Code) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.Code;
			}

			Directional searchItem = new Directional()
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

		// Retrieve the collection element.
		/// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
		public Directional LJCSearchName(string name)
		{
			NameComparer nameComparer;
			Directional retValue = null;

			nameComparer = new NameComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Name) != 0)
			{
				mPrevCount = Count;
				Sort(nameComparer);
				mSortType = SortType.Name;
			}

			Directional searchItem = new Directional()
			{
				Name = name
			};
			int index = BinarySearch(searchItem, nameComparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchSpanishName/*' file='Doc/Directionals.xml'/>
		public Directional LJCSearchSpanishName(string spanishName)
		{
			SpanishNameComparer nameComparer;
			Directional retValue = null;

			nameComparer = new SpanishNameComparer();

			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.SpanishName) != 0)
			{
				mPrevCount = Count;
				Sort(nameComparer);
				mSortType = SortType.SpanishName;
			}

			Directional searchItem = new Directional()
			{
				Code = spanishName
			};
			int index = BinarySearch(searchItem, nameComparer);
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
			get { return "AddressData\\Directionals.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		private SortType mSortType;

		private enum SortType
		{
			Code,
			Name,
			SpanishName
		}
		#endregion
	}
}
