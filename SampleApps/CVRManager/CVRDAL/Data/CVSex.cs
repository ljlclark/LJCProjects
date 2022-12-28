// Copyright (c) Lester J Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Text;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
	/// <summary>The CVDex table Data Object.</summary>
	public class CVSex : IComparable<CVSex>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public CVSex()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public CVSex Clone()
		{
			CVSex retValue = MemberwiseClone() as CVSex;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder(64);
			builder.Append(mCode.Trim());
			if (NetString.HasValue(mName))
			{
				builder.Append($"-{mName}");
			}
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(CVSex other)
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
				//retValue = Code.CompareTo(other.Code);

				// Not case sensitive.
				retValue = string.Compare(Code, other.Code, true);
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
		public long ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private long mID;

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="char(2)")]
		public string Code
		{
			get { return mCode; }
			set
			{
				value = NetString.InitString(value);
				mCode = ChangedNames.Add(ColumnCode, mCode, value);
			}
		}
		private string mCode;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="varchar(15)")]
		public string Name
		{
			get { return mName; }
			set
			{
				value = NetString.InitString(value);
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private string mName;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "CVSex";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>the Code maximum length.</summary>
		public static int LengthCode = 2;

		/// <summary>the Name maximum length.</summary>
		public static int LengthName = 15;
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Code value.</summary>
	public class CVSexCodeComparer : IComparer<CVSex>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int Compare(CVSex x, CVSex y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				//retValue = x.Code.CompareTo(y.Code);

				// Not case sensitive.
				retValue = string.Compare(x.Code, y.Code, true);
			}
			return retValue;
		}
	}

	/// <summary>Sort and search on Name value.</summary>
	public class CVSexNameComparer : IComparer<CVSex>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int Compare(CVSex x, CVSex y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				//retValue = x.Name.CompareTo(y.Name);

				// Not case sensitive.
				retValue = string.Compare(x.Name, y.Name, true);
			}
			return retValue;
		}
	}
	#endregion
}
