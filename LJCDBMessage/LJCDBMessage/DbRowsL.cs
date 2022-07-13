// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDBMessage
{
	/// <summary> Represents a collection of DbValues objects.</summary>
	[XmlRoot("DbRows")]
	public class DbRowsL : List<DbValues>
	{
		#region Static Functions

		// Checks if the collection has items.
		/// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static bool HasItems(DbRowsL collectionObject)
		{
			bool retValue = false;

			if (collectionObject != null && collectionObject.Count > 0)
			{
				retValue = true;
			}
			return retValue;
		}

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public static DbRowsL LJCDeserialize(string fileSpec = null)
		{
			DbRowsL retValue = null;

			if (false == NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			retValue = NetCommon.XmlDeserialize(typeof(DbRowsL), fileSpec)
				as DbRowsL;
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsL()
		{
		}

		// The Copy constructor.
		/// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsL(DbRowsL value)
		{
			if (HasItems(value))
			{
				foreach (var item in this)
				{
					Add(new DbValues(item));
				}
			}
		}
		#endregion

		#region Collection Methods

		// Clones the structure of the object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbRowsL Clone()
		{
			var retValue = MemberwiseClone() as DbRowsL;
			return retValue;
		}

		// Checks if the collection has items.
		/// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
		public bool HasItems()
		{
			bool retValue = false;

			if (Count > 0)
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
			NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
		}
		#endregion

		#region Properties

		/// <summary>Gets the Default File Name.</summary>
		public static string LJCDefaultFileName
		{
			get { return "DbRows.xml"; }
		}
		#endregion
	}
}
