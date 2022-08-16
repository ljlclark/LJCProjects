// Copyright (c) Lester J Clark 2018-2020 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewJoinColumn table Data Record.</summary>
	public class ViewJoinColumn : IComparable<ViewJoinColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ViewJoinColumn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ViewJoinColumn Clone()
		{
			ViewJoinColumn retValue = MemberwiseClone() as ViewJoinColumn;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue = mColumnName;

			if (mPropertyName != mColumnName)
			{
				retValue = $"{mColumnName}-{mPropertyName}";
			}
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewJoinColumn other)
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
				retValue = string.Compare(ColumnName, other.ColumnName, true);
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

		/// <summary>Gets or sets the ViewJoinID value.</summary>
		//[Required]
		//[Column("ViewJoinID", TypeName="int")]
		public Int32 ViewJoinID
		{
			get { return mViewJoinID; }
			set
			{
				mViewJoinID = ChangedNames.Add(ColumnViewJoinID, mViewJoinID, value);
			}
		}
		private Int32 mViewJoinID;

		/// <summary>Gets or sets the ColumnName value.</summary>
		//[Required]
		//[Column("ColumnName", TypeName="nvarchar(60)")]
		public String ColumnName
		{
			get { return mColumnName; }
			set
			{
				mColumnName = ChangedNames.Add(ColumnColumnName, mColumnName, value);
			}
		}
		private String mColumnName;

		/// <summary>Gets or sets the Caption value.</summary>
		//[Column("Caption", TypeName="nvarchar(60)")]
		public String Caption
		{
			get { return mCaption; }
			set
			{
				value = NetString.InitString(value);
				mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
			}
		}
		private String mCaption;

		/// <summary>Gets or sets the DataTypeName value.</summary>
		//[Required]
		//[Column("DataTypeName", TypeName="nvarchar(60)")]
		public String DataTypeName
		{
			get { return mDataTypeName; }
			set
			{
				value = NetString.InitString(value);
				mDataTypeName = ChangedNames.Add(ColumnDataTypeName, mDataTypeName, value);
			}
		}
		private String mDataTypeName;

		/// <summary>Gets or sets the PropertyName value.</summary>
		//[Column("PropertyName", TypeName="nvarchar(60)")]
		public String PropertyName
		{
			get { return mPropertyName; }
			set
			{
				value = NetString.InitString(value);
				mPropertyName = ChangedNames.Add(ColumnPropertyName, mPropertyName, value);
			}
		}
		private String mPropertyName;

		/// <summary>Gets or sets the RenameAs value.</summary>
		//[Column("RenameAs", TypeName="nvarchar(60)")]
		public String RenameAs
		{
			get { return mRenameAs; }
			set
			{
				value = NetString.InitString(value);
				mRenameAs = ChangedNames.Add(ColumnRenameAs, mRenameAs, value);
			}
		}
		private String mRenameAs;

		/// <summary>Gets or sets the Value value.</summary>
		//[Column("Value", TypeName="nvarchar(60)")]
		public String Value
		{
			get { return mValue; }
			set
			{
				value = NetString.InitString(value);
				mValue = ChangedNames.Add(ColumnValue, mValue, value);
			}
		}
		private String mValue;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the SaveState value.</summary>
		public SaveState SaveState { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "ViewJoinColumn";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewJoinID column name.</summary>
		public static string ColumnViewJoinID = "ViewJoinID";

		/// <summary>The ColumnName column name.</summary>
		public static string ColumnColumnName = "ColumnName";

		/// <summary>The PropertyName column name.</summary>
		public static string ColumnPropertyName = "PropertyName";

		/// <summary>The RenameAs column name.</summary>
		public static string ColumnRenameAs = "RenameAs";

		/// <summary>The Caption column name.</summary>
		public static string ColumnCaption = "Caption";

		/// <summary>The DataTypeName column name.</summary>
		public static string ColumnDataTypeName = "DataTypeName";

		/// <summary>The Value column name.</summary>
		public static string ColumnValue = "Value";

		/// <summary>The ColumnName maximum length.</summary>
		public static int LengthColumnName = 60;

		/// <summary>The PropertyName maximum length.</summary>
		public static int LengthPropertyName = 60;

		/// <summary>The RenameAs maximum length.</summary>
		public static int LengthRenameAs = 60;

		/// <summary>The Caption maximum length.</summary>
		public static int LengthCaption = 60;

		/// <summary>The DataTypeName maximum length.</summary>
		public static int LengthDataTypeName = 60;

		/// <summary>The Value maximum length.</summary>
		public static int LengthValue = 60;
		#endregion
	}
}
