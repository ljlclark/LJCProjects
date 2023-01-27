// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitSystems.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>Represents a collection of UnitSystem objects.</summary>
	/// <remarks>
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	[XmlRoot("UnitSystems")]
	public class UnitSystems : List<UnitSystem>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public static UnitSystems LJCDeserialize(string fileSpec = null)
		{
			UnitSystems retValue;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(UnitSystems), fileSpec)
				as UnitSystems;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitSystems()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitSystems Clone()
		{
			UnitSystems retValue = new UnitSystems();
			foreach (UnitSystem item in this)
			{
				retValue.Add(item.Clone());
			}
			return retValue;
		}

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public UnitSystem Add(int id, string name)
		{
			UnitSystem retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				retValue = LJCSearchName(name);
				if (null == retValue)
				{
					retValue = new UnitSystem()
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
		public UnitSystems GetCollection(List<UnitSystem> list)
		{
			UnitSystems retValue = null;

			if (list != null && list.Count > 0)
			{
				retValue = new UnitSystems();
				foreach (UnitSystem item in list)
				{
					retValue.Add(item);
				}
			}
			return retValue;
		}

		// Serializes the collection to a file.
		/// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
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
		public void LJCSortName(UnitSystemNameComparer comparer)
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
		/// <include path='items/LJCSearchCode/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public UnitSystem LJCSearchCode(string code)
		{
			UnitSystem retValue = null;

			LJCSortCode();
			UnitSystem searchItem = new UnitSystem()
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
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
		public UnitSystem LJCSearchName(string name)
		{
			UnitSystemNameComparer comparer;
			UnitSystem retValue = null;

			comparer = new UnitSystemNameComparer();
			LJCSortName(comparer);
			UnitSystem searchItem = new UnitSystem()
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
			get { return "UnitSystems.xml"; }
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

