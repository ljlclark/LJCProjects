// Copyright (c) Lester J Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCUnitMeasureDAL
{
	/// <summary>The UnitType table Data Object.</summary>
	public class UnitType : IComparable<UnitType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitType()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitType Clone()
		{
			UnitType retValue = MemberwiseClone() as UnitType;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(UnitType other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = Name.CompareTo(other.Name);

				// Not case sensitive.
				retValue = string.Compare(Name, other.Name, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ID value.</summary>
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the UnitCategoryID value.</summary>
		public Int32 UnitCategoryID
		{
			get { return mUnitCategoryID; }
			set
			{
				mUnitCategoryID = ChangedNames.Add(ColumnUnitCategoryID, mUnitCategoryID, value);
			}
		}
		private Int32 mUnitCategoryID;

		/// <summary>Gets or sets the Code value.</summary>
		public String Code
		{
			get { return mCode; }
			set
			{
				value = NetString.InitString(value);
				mCode = ChangedNames.Add(ColumnCode, mCode, value);
			}
		}
		private String mCode;

		/// <summary>Gets or sets the Name value.</summary>
		public String Name
		{
			get { return mName; }
			set
			{
				value = NetString.InitString(value);
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private String mName;
		#endregion

		#region Calculated and Join Data Properties

		///// <summary>Gets or sets the Join TypeName value.</summary>
		//public string TypeName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "UnitType";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The UnitCategoryID column name.</summary>
		public static string ColumnUnitCategoryID = "UnitCategoryID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 4;

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 15;
		#endregion

		#region Calculated and Join Class Data

		///// <summary>The Join TypeName column name.</summary>
		//public static string ColumnTypeName = "TypeName";
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class UnitTypeNameComparer : IComparer<UnitType>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int Compare(UnitType x, UnitType y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				retValue = NetCommon.CompareNull(x.Name, y.Name);
				if (-2 == retValue)
				{
					// Case sensitive.
					//retValue = x.Name.CompareTo(y.Name);

					// Not case sensitive.
					retValue = string.Compare(x.Name, y.Name, true);
				}
			}
			return retValue;
		}
	}
	#endregion
}
