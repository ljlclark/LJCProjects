// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTypeClass.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace CVRDAL
{
	/// <summary>The CodeTypeClass table Data Class.</summary>
	public class CodeTypeClass : IComparable<CodeTypeClass>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CodeTypeClass()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CodeTypeClass Clone()
		{
			CodeTypeClass retValue = MemberwiseClone() as CodeTypeClass;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mCode;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(CodeTypeClass other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				retValue = ID.CompareTo(other.ID);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		/// <summary>Gets or sets the ID value.</summary>
		//[Required]
		//[Column("ID", TypeName="int")]
		public int ID
		{
			get { return mID; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private int mID;

		/// <summary>Gets or sets the FirstName value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(25)")]
		public string Code
		{
			get { return mCode; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				mCode = ChangedNames.Add(ColumnCode, mCode, value);
			}
		}
		private string mCode;

		/// <summary>Gets or sets the Description value.</summary>
		//[Required]
		//[Column("Description", TypeName="nvarchar(60)")]
		public string Description
		{
			get { return mDescription; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				mDescription = ChangedNames.Add(ColumnDescription, mDescription
					, value);
			}
		}
		private string mDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "CodeTypeClass";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Code value.</summary>
	public class CodeTypeClassCodeComparer : IComparer<CodeTypeClass>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int Compare(CodeTypeClass x, CodeTypeClass y)
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
	#endregion
}
