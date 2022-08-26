// Copyright (c) Lester J Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>Represents a collection of UnitMeasure objects.</summary>
	/// <remarks>
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	[XmlRoot("UnitMeasures")]
	public class UnitMeasures : List<UnitMeasure>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static UnitMeasures LJCDeserialize(string fileSpec = null)
		{
			UnitMeasures retValue = null;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(UnitMeasures), fileSpec)
				as UnitMeasures;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public UnitMeasures()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public UnitMeasures Clone()
		{
			UnitMeasures retValue = new UnitMeasures();
			foreach (UnitMeasure item in this)
			{
				retValue.Add(item.Clone());
			}
			return retValue;
		}

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public UnitMeasure Add(int id, string name)
		{
			UnitMeasure retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new UnitMeasure()
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
		public UnitMeasures GetCollection(List<UnitMeasure> list)
		{
			UnitMeasures retValue = null;

			if (list != null && list.Count > 0)
			{
				retValue = new UnitMeasures();
				foreach (UnitMeasure item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}

		// Serializes the collection to a file.
		/// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public void LJCSerialize(string fileSpec = null)
		{
			if (false == NetString.HasValue(fileSpec))
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
		public void LJCSortName(UnitMeasureNameComparer comparer)
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
		/// <include path='items/LJCSearchCode/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public UnitMeasure LJCSearchCode(string code)
		{
			UnitMeasure retValue = null;

			LJCSortCode();
			UnitMeasure searchItem = new UnitMeasure()
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
		/// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public UnitMeasure LJCSearchName(string name)
		{
			UnitMeasureNameComparer comparer;
			UnitMeasure retValue = null;

			comparer = new UnitMeasureNameComparer();
			LJCSortName(comparer);
			UnitMeasure searchItem = new UnitMeasure()
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
			get { return "UnitMeasures.xml"; }
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

