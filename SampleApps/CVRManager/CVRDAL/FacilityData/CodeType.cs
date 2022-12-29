// Copyright (c) Lester J Clark 2020 - All Rights Reserved
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
	/// <summary>The CodeType table Data Class.</summary>
	public class CodeType : IComparable<CodeType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public CodeType()
		{
			ChangedNames = new ChangedNames();
		}
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public CodeType Clone()
		{
			CodeType retValue = MemberwiseClone() as CodeType;
			return retValue;
		}

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(CodeType other)
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

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../../CoreUtilities/../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public override string ToString()
		{
			return mCode;
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

		/// <summary>Gets or sets the CodeTypeClassID value.</summary>
		//[Required]
		//[Column("CodeTypeClassID", TypeName="int")]
		public int CodeTypeClassID
		{
			get { return mCodeTypeClassID; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				mCodeTypeClassID = ChangedNames.Add(ColumnCodeTypeClassID
					, mCodeTypeClassID, value);
			}
		}
		private int mCodeTypeClassID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "CodeType";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Description column name.</summary>
		public static string ColumnCodeTypeClassID = "CodeTypeClassID";

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Code value.</summary>
	public class CodeTypeCodeComparer : IComparer<CodeType>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int Compare(CodeType x, CodeType y)
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
