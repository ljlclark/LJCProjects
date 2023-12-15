// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitTypes.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>Represents a collection of UnitType objects.</summary>
	/// <remarks>
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	[XmlRoot("UnitTypes")]
	public class UnitTypes : List<UnitType>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public static UnitTypes LJCDeserialize(string fileSpec = null)
		{
			UnitTypes retValue;

			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(UnitTypes), fileSpec)
				as UnitTypes;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitTypes()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitTypes Clone()
		{
			UnitTypes retValue = new UnitTypes();
			foreach (UnitType item in this)
			{
				retValue.Add(item.Clone());
			}
			return retValue;
		}

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public UnitType Add(int id, string name)
		{
			UnitType retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new UnitType()
					{
						ID = id,
						Name = name
					};
					Add(retValue);
				}
			}
			return retValue;
		}

		// Get custom collection from List<T>.
		/// <summary>
		/// Get custom collection from List&lt;T&gt;.
		/// </summary>
		/// <param name="list">The list object reference.</param>
		/// <returns>The collection object reference.</returns>
		public UnitTypes GetCollection(List<UnitType> list)
		{
			UnitTypes retValue = null;

			if (list != null && list.Count > 0)
			{
				retValue = new UnitTypes();
				foreach (UnitType item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}

		// Serializes the collection to a file.
		/// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public void LJCSerialize(string fileSpec = null)
		{
			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
		}
		#endregion

		#region Sort and Search Methods

		/// <summary>Sort on Code.</summary>
		public void LJCSortCode()
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Code) != 0)
			{
				mPrevCount = Count;
				Sort();
				mSortType = SortType.Code;
			}
		}

		/// <summary>Sort on Name.</summary>
		/// <param name="comparer">The Comparer object.</param>
		public void LJCSortName(UnitTypeNameComparer comparer)
		{
			if (Count != mPrevCount
				|| mSortType.CompareTo(SortType.Name) != 0)
			{
				mPrevCount = Count;
				Sort(comparer);
				mSortType = SortType.Name;
			}
		}

		// Retrieve the collection element.
		/// <include path='items/LJCSearchCode/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public UnitType LJCSearchCode(string code)
		{
			UnitType retValue = null;

			LJCSortCode();
			UnitType searchItem = new UnitType()
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

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public UnitType LJCSearchName(string name)
		{
			UnitTypeNameComparer comparer;
			UnitType retValue = null;

			comparer = new UnitTypeNameComparer();
			LJCSortName(comparer);
			UnitType searchItem = new UnitType()
			{
				Name = name
			};
			int index = BinarySearch(searchItem, comparer);
			if (index > -1)
			{
				retValue = this[index];
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "UnitTypes.xml"; }
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

