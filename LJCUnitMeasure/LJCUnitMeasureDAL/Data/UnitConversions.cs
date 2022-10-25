// Copyright (c) Lester J Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCUnitMeasureDAL
{
	/// <summary>Represents a collection of UnitConversion objects.</summary>
	/// <remarks>
	/// <para>-- Library Level Remarks</para>
	/// </remarks>
	[XmlRoot("UnitConversions")]
	public class UnitConversions : List<UnitConversion>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static UnitConversions LJCDeserialize(string fileSpec = null)
		{
			UnitConversions retValue;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(UnitConversions), fileSpec)
				as UnitConversions;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public UnitConversions()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Collection Methods

		// Creates and returns a clone of the object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public UnitConversions Clone()
		{
			UnitConversions retValue = new UnitConversions();
			foreach (UnitConversion item in this)
			{
				retValue.Add(item.Clone());
			}
			return retValue;
		}

		// Creates and adds the object from the provided values.
		/// <summary>
		/// Creates and adds the object from the provided values.
		/// </summary>
		/// <param name="fromUnitID">The 'From' UnitID value.</param>
		/// <param name="toUnitID">The 'To' UnitID value.</param>
		/// <param name="expression">The conversion expression.</param>
		/// <returns></returns>
		public UnitConversion Add(int fromUnitID, int toUnitID, string expression)
		{
			UnitConversion retValue = null;

			if (fromUnitID > 0 && toUnitID > 0
				&& NetString.HasValue(expression))
			{
				retValue = LJCSearch(fromUnitID, toUnitID);
				if (null == retValue)
				{
					retValue = new UnitConversion()
					{
						FromUnitMeasureID = fromUnitID,
						ToUnitMeasureID = toUnitID,
						Expression = expression
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
		public UnitConversions GetCollection(List<UnitConversion> list)
		{
			UnitConversions retValue = null;

			if (list != null && list.Count > 0)
			{
				retValue = new UnitConversions();
				foreach (UnitConversion item in list)
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
		public void LJCSort()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element.
		/// <summary>
		/// Returns the matching collection element.
		/// </summary>
		/// <param name="fromUnitID">The 'From' UnitID value.</param>
		/// <param name="toUnitID">The 'To' UnitID value.</param>
		/// <returns>The matching collection element.</returns>
		public UnitConversion LJCSearch(int fromUnitID, int toUnitID)
		{
			UnitConversion retValue = null;

			LJCSort();
			UnitConversion searchItem = new UnitConversion()
			{
				FromUnitMeasureID = fromUnitID,
				ToUnitMeasureID = toUnitID
			};
			int index = BinarySearch(searchItem);
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
			get { return "UnitConversions.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}

