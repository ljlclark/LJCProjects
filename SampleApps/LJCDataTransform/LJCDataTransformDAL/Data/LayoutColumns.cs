// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumns.cs
using System;
using System.Collections.Generic;
using System.IO;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>Represents a collection of LayoutColumn objects.</summary>
	public class LayoutColumns : List<LayoutColumn>
	{
		#region Static Functions

		// Deserializes from the specified XML file.
		/// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public static LayoutColumns LJCDeserialize(string fileSpec = null)
		{
			LayoutColumns retValue = null;

			if (!NetString.HasValue(fileSpec))
			{
				fileSpec = LJCDefaultFileName;
			}
			if (!File.Exists(fileSpec))
			{
				string errorText = $"File '{fileSpec}' was not found.";
				throw new FileNotFoundException(errorText);
			}
			else
			{
				retValue = NetCommon.XmlDeserialize(typeof(LayoutColumns)
					, fileSpec) as LayoutColumns;
			}
			return retValue;
		}
		#endregion

		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutColumns()
		{
			mPrevCount = -1;
		}
		#endregion

		#region Public Methods

		// Creates and adds the object from the provided values.
		/// <include path='items/Add/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public LayoutColumn Add(int id, string name)
		{
			LayoutColumn retValue = null;

			if (id > 0
				&& NetString.HasValue(name))
			{
				//retValue = LJCSearchByName(name);
				//if (null == retValue)
				//{
				retValue = new LayoutColumn()
				{
					SourceLayoutID = id,
					Name = name
				};
				Add(retValue);
				//}
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

		/// <summary>Sort on the default values.</summary>
		public void LJCSortDefault()
		{
			if (Count != mPrevCount)
			{
				mPrevCount = Count;
				Sort();
			}
		}

		// Retrieve the collection element with name.
		/// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
		public LayoutColumn LJCSearchName(string name)
		{
			LayoutColumn retValue = null;

			LJCSortDefault();
			LayoutColumn searchItem = new LayoutColumn()
			{
				Name = name
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
			get { return "LayoutColumns.xml"; }
		}
		#endregion

		#region Class Data

		private int mPrevCount;
		#endregion
	}
}
