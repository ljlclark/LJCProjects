// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDBMessage
{
	/// <summary> Represents a collection of DbValues objects.</summary>
	//[XmlRoot("DbRows")]
	public class DbRowsE : IEnumerable<DbValues>
	{
		#region Static Functions

		// Checks if the collection has items.
		/// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static bool HasItems(DbRowsE collectionObject)
		{
			bool retValue = false;

			if (collectionObject != null
				&& collectionObject.Items != null && collectionObject.Items.Count > 0)
			{
				retValue = true;
			}
			return retValue;
		}

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static DbRowsE LJCDeserialize(string fileSpec = null)
		{
			DbRowsE retValue = null;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(DbRowsE), fileSpec, RootName)
				as DbRowsE;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsE()
		{
			Items = new List<DbValues>();
		}

		// The Copy constructor.
		/// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsE(DbRowsE value)
		{
			Items = new List<DbValues>();
			if (HasItems(value))
			{
				foreach (var item in value.Items)
				{
					Add(new DbValues(item));
				}
			}
		}
		#endregion

		#region Collection Methods

		// Adds the specified object.
		/// <include path='items/Add/*' file='Doc/DbRows.xml'/>
		public void Add(DbValues dbValues)
		{
			if (DbValues.HasItems(dbValues))
			{
				Items.Add(dbValues);
			}
		}

		// Clones the structure of the object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsE Clone()
		{
			var retValue = MemberwiseClone() as DbRowsE;
			return retValue;
		}

		// Checks if the collection has items.
		/// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public bool HasItems()
		{
			bool retValue = false;

			if (Items != null && Items.Count > 0)
			{
				retValue = true;
			}
			return retValue;
		}

		// Serializes the collection
		/// <include path='items/LJCSerialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public void LJCSerialize(string fileSpec = null)
		{
			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			NetCommon.XmlSerialize(GetType(), this, null, fileSpec, RootName);
		}
		#endregion

		#region IEnumerable Methods and Properties

		/// <summary>The Collection count.</summary>
		public int Count
		{
			get { return Items.Count; }
		}

		// Gets the Collection Enumerator.
		/// <include path='items/GetEnumerator/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public IEnumerator<DbValues> GetEnumerator()
		{
			return ((IEnumerable<DbValues>)Items).GetEnumerator();
		}

		// Gets the Collection Enumerator.
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<DbValues>)Items).GetEnumerator();
		}

		/// <summary>The DbValues items.</summary>
		//[XmlArray(RootName)]
		[XmlArrayItem("DbValues")]
		//[XmlArrayItem(Type = typeof(DbValues))]
		public List<DbValues> Items { get; set; }

		// Gets the item by index value.
		/// <include path='items/Indexer/*' file='../../LJCDocLib/Common/Collection.xml'/>
		/// <remarks>Because of the indexer, this class cannot implement an Item property.</remarks>
		public DbValues this[int index]
		{
			get
			{
				DbValues retValue = null;

				if (index >= 0 && index < Count)
				{
					retValue = Items[index];
				}
				return retValue;
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "DbRows.xml"; }
		}
		#endregion

		#region Class Data

		//private const string RootName = "DbValues";
		private const string RootName = "DbRows";
		#endregion
	}
}
