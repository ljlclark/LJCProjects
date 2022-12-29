// Copyright (c) Lester J Clark 2021 - All Rights Reserved
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCUnitMeasureDAL
{
	/// <summary>The UnitMeasure table Data Object.</summary>
	public class UnitMeasure : IComparable<UnitMeasure>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitMeasure()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitMeasure Clone()
		{
			UnitMeasure retValue = MemberwiseClone() as UnitMeasure;
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
		public int CompareTo(UnitMeasure other)
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
		//[Required]
		//[Column("ID", TypeName="int")]
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
		//[Required]
		//[Column("UnitCategoryID", TypeName="int")]
		public Int32 UnitCategoryID
		{
			get { return mUnitCategoryID; }
			set
			{
				mUnitCategoryID = ChangedNames.Add(ColumnUnitCategoryID, mUnitCategoryID, value);
			}
		}
		private Int32 mUnitCategoryID;

		/// <summary>Gets or sets the UnitSystemID value.</summary>
		//[Required]
		//[Column("UnitSystemID", TypeName="int")]
		public Int32 UnitSystemID
		{
			get { return mUnitSystemID; }
			set
			{
				mUnitSystemID = ChangedNames.Add(ColumnUnitSystemID, mUnitSystemID, value);
			}
		}
		private Int32 mUnitSystemID;

		/// <summary>Gets or sets the UnitTypeID value.</summary>
		//[Required]
		//[Column("UnitTypeID", TypeName="int")]
		public Int32 UnitTypeID
		{
			get { return mUnitTypeID; }
			set
			{
				mUnitTypeID = ChangedNames.Add(ColumnUnitTypeID, mUnitTypeID, value);
			}
		}
		private Int32 mUnitTypeID;

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(5)")]
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
		//[Required]
		//[Column("Name", TypeName="nvarchar(30)")]
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

		/// <summary>Gets or sets the AltName value.</summary>
		//[Column("AltName", TypeName="nvarchar(30)")]
		public String AltName
		{
			get { return mAltName; }
			set
			{
				value = NetString.InitString(value);
				mAltName = ChangedNames.Add(ColumnAltName, mAltName, value);
			}
		}
		private String mAltName;

		/// <summary>Gets or sets the Sequence value.</summary>
		//[Required]
		//[Column("Sequence", TypeName="int")]
		public Int32 Sequence
		{
			get { return mSequence; }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int32 mSequence;

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="nvarchar(40)")]
		public String Description
		{
			get { return mDescription; }
			set
			{
				value = NetString.InitString(value);
				mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
			}
		}
		private String mDescription;
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
		public static string TableName = "UnitMeasure";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The UnitCategoryID column name.</summary>
		public static string ColumnUnitCategoryID = "UnitCategoryID";

		/// <summary>The UnitSystemID column name.</summary>
		public static string ColumnUnitSystemID = "UnitSystemID";

		/// <summary>The UnitTypeID column name.</summary>
		public static string ColumnUnitTypeID = "UnitTypeID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The AltName column name.</summary>
		public static string ColumnAltName = "AltName";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 5;

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 25;

		/// <summary>The AltName maximum length.</summary>
		public static int LengthAltName = 30;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 30;
		#endregion

		#region Calculated and Join Class Data

		///// <summary>The Join TypeName column name.</summary>
		//public static string ColumnTypeName = "TypeName";
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class UnitMeasureNameComparer : IComparer<UnitMeasure>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int Compare(UnitMeasure x, UnitMeasure y)
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
