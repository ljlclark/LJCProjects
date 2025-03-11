// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeType.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>The CodeType table Data Record.</summary>
	public class CodeType : IComparable<CodeType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeType()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CodeType Clone()
		{
			CodeType retValue = MemberwiseClone() as CodeType;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(CodeType other)
		{
			int retValue;

			if (null == other)
			{
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

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mDescription;
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

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(25)")]
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

		/// <summary>Gets or sets the Description value.</summary>
		//[Required]
		//[Column("Description", TypeName="nvarchar(60)")]
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

		/// <summary>Gets or sets the CodeTypeClassID value.</summary>
		//[Required]
		//[Column("CodeTypeClassID", TypeName="int")]
		public Int32 CodeTypeClassID
		{
			get { return mCodeTypeClassID; }
			set
			{
				mCodeTypeClassID = ChangedNames.Add(ColumnCodeTypeClassID, mCodeTypeClassID, value);
			}
		}
		private Int32 mCodeTypeClassID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the join CodeTypeClassDescription value.</summary>
		public String CodeTypeClassDescription
		{
			get { return mClassDescription; }
			set
			{
				mClassDescription = ChangedNames.Add(ColumnCodeTypeClassDescription
					, mClassDescription, value);
			}
		}
		private String mClassDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "CodeType";

		/// <summary>The ID value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Code value.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description value.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The CodeTypeClassID value.</summary>
		public static string ColumnCodeTypeClassID = "CodeTypeClassID";

		#region Join Class Data

		/// <summary>The ColumnClassDescription value.</summary>
		public static string ColumnCodeTypeClassDescription = "CodeTypeClassDescription";
		#endregion

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;
		#endregion
	}
}
