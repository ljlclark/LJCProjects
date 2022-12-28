// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewColumn table Data Record.</summary>
	public class ViewColumn : IComparable<ViewColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public ViewColumn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public ViewColumn Clone()
		{
			ViewColumn retValue = MemberwiseClone() as ViewColumn;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewColumn other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = ColumnName.CompareTo(other.ColumnName);

				// Not case sensitive.
				retValue = string.Compare(ColumnName, other.ColumnName, true);
			}
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue = mColumnName;

			if (mPropertyName != mColumnName)
			{
				retValue = $"{mColumnName}-{mPropertyName}";
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

		/// <summary>Gets or sets the ViewDataID value.</summary>
		//[Required]
		//[Column("ViewDataID", TypeName="int")]
		public Int32 ViewDataID
		{
			get { return mViewDataID; }
			set
			{
				mViewDataID = ChangedNames.Add(ColumnViewDataID, mViewDataID, value);
			}
		}
		private Int32 mViewDataID;

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

		/// <summary>Gets or sets the ColumnName value.</summary>
		//[Required]
		//[Column("ColumnName", TypeName="nvarchar(60)")]
		public String ColumnName
		{
			get { return mColumnName; }
			set
			{
				value = NetString.InitString(value);
				mColumnName = ChangedNames.Add(ColumnColumnName, mColumnName, value);
			}
		}
		private String mColumnName;

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

		/// <summary>Gets or sets the Width value.</summary>
		//[Column("Width", TypeName="int")]
		public Int32 Width
		{
			get { return mWidth; }
			set
			{
				mWidth = ChangedNames.Add(ColumnWidth, mWidth, value);
			}
		}
		private Int32 mWidth;

		/// <summary>Gets or sets the ViewDataID value.</summary>
		//[Required]
		//[Column("IsPrimaryKey", TypeName="bit")]
		public bool IsPrimaryKey
		{
			get { return mIsPrimaryKey; }
			set
			{
				mIsPrimaryKey = ChangedNames.Add(ColumnIsPrimaryKey, mIsPrimaryKey
					, value);
			}
		}
		private bool mIsPrimaryKey;
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

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewColumn";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewData ID column value.</summary>
		public static string ColumnViewDataID = "ViewDataID";

		/// <summary>The Sequence column value.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The ColumnName column value.</summary>
		public static string ColumnColumnName = "ColumnName";

		/// <summary>The PropertyName column value.</summary>
		public static string ColumnPropertyName = "PropertyName";

		/// <summary>The RenameAs column value.</summary>
		public static string ColumnRenameAs = "RenameAs";

		/// <summary>The Caption column value.</summary>
		public static string ColumnCaption = "Caption";

		/// <summary>The DataTypeName column value.</summary>
		public static string ColumnDataTypeName = "DataTypeName";

		/// <summary>The Value column value.</summary>
		public static string ColumnValue = "Value";

		/// <summary>The Width column value.</summary>
		public static string ColumnWidth = "Width";

		/// <summary>The Value column value.</summary>
		public static string ColumnIsPrimaryKey = "IsPrimaryKey";

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

	/// <summary>The record save states.</summary>
	public enum SaveState
	{
		/// <summary>Do not change the record.</summary>
		None,
		/// <summary>Add the record.</summary>
		Add,
		/// <summary>Update the record.</summary>
		Update,
		/// <summary>Delete the record.</summary>
		Delete
	}
}
