// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumn.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The LayoutColumn table Data Record.</summary>
	public class LayoutColumn : IComparable<LayoutColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutColumn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public LayoutColumn Clone()
		{
			LayoutColumn retValue = MemberwiseClone() as LayoutColumn;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(LayoutColumn other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Not case sensitive.
				retValue = string.Compare(Name, other.Name, true);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the LayoutColumnID value.</summary>
		//[Required]
		//[Column("ID", TypeName="int")]
		public Int16 LayoutColumnID
		{
			get { return mLayoutColumnID; }
			set
			{
				mLayoutColumnID = ChangedNames.Add(ColumnLayoutColumnID, mLayoutColumnID, value);
			}
		}
		private Int16 mLayoutColumnID;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="varchar(60)")]
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

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="varchar(100)")]
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

		/// <summary>Gets or sets the LayoutID value.</summary>
		//[Required]
		//[Column("SourceLayoutID", TypeName="int")]
		public Int32 SourceLayoutID
		{
			get { return mSourceLayoutID; }
			set
			{
				mSourceLayoutID = ChangedNames.Add(ColumnSourceLayoutID, mSourceLayoutID, value);
			}
		}
		private Int32 mSourceLayoutID;

		/// <summary>Gets or sets the Sequence value.</summary>
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

		/// <summary>Gets or sets the DataTypeID value.</summary>
		//[Required]
		//[Column("DataTypeID", TypeName="smallint")]
		public Int16 DataTypeID
		{
			get { return mDataTypeID; }
			set
			{
				mDataTypeID = ChangedNames.Add(ColumnDataTypeID, mDataTypeID, value);
			}
		}
		private Int16 mDataTypeID;

		/// <summary>Gets or sets the Length value.</summary>
		//[Column("Length", TypeName="int")]
		public Int32 Length
		{
			get { return mLength; }
			set
			{
				mLength = ChangedNames.Add(ColumnLength, mLength, value);
			}
		}
		private Int32 mLength;

		/// <summary>Gets or sets the IdentityKey flag.</summary>
		//[Required]
		//[Column("IdentityKey", TypeName="bit")]
		public bool IdentityKey
		{
			get { return mIdentityKey; }
			set
			{
				mIdentityKey = ChangedNames.Add(ColumnIdentityKey, mIdentityKey, value);
			}
		}
		private bool mIdentityKey;

		/// <summary>Gets or sets the PrimaryKey flag.</summary>
		//[Required]
		//[Column("PrimaryKey", TypeName="bit")]
		public bool PrimaryKey
		{
			get { return mPrimaryKey; }
			set
			{
				mPrimaryKey = ChangedNames.Add(ColumnPrimaryKey, mPrimaryKey, value);
			}
		}
		private bool mPrimaryKey;

		/// <summary>Gets or sets the AllowNull flag.</summary>
		//[Required]
		//[Column("AllowNull", TypeName="bit")]
		public bool AllowNull
		{
			get { return mAllowNull; }
			set
			{
				mAllowNull = ChangedNames.Add(ColumnAllowNull, mAllowNull, value);
			}
		}
		private bool mAllowNull;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "LayoutColumn";

		/// <summary>The LayoutColumnID column name.</summary>
		public static string ColumnLayoutColumnID = "LayoutColumnID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The LayoutID column name.</summary>
		public static string ColumnSourceLayoutID = "SourceLayoutID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The DataTypeID column name.</summary>
		public static string ColumnDataTypeID = "DataTypeID";

		/// <summary>The Length column name.</summary>
		public static string ColumnLength = "Length";

		/// <summary>The IdentityKey column name.</summary>
		public static string ColumnIdentityKey = "IdentityKey";

		/// <summary>The PrimaryKey column name.</summary>
		public static string ColumnPrimaryKey = "PrimaryKey";

		/// <summary>The AllowNull column name.</summary>
		public static string ColumnAllowNull = "AllowNull";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
