// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewJoin table Data Record.</summary>
	public class ViewJoin : IComparable<ViewJoin>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ViewJoin()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Public Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mJoinTableName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewJoin other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = JoinTableName.CompareTo(other.JoinTableName);

				// Not case sensitive.
				retValue = string.Compare(JoinTableName, other.JoinTableName, true);
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

		/// <summary>Gets or sets the JoinTableName value.</summary>
		//[Required]
		//[Column("JoinTableName", TypeName="nvarchar(60)")]
		public String JoinTableName
		{
			get { return mJoinTableName; }
			set
			{
				value = NetString.InitString(value);
				mJoinTableName = ChangedNames.Add(PropertyTableName, mJoinTableName, value);
			}
		}
		private String mJoinTableName;

		/// <summary>Gets or sets the JoinType value.</summary>
		//[Column("JoinType", TypeName="nvarchar(60)")]
		public String JoinType
		{
			get { return mJoinType; }
			set
			{
				value = NetString.InitString(value);
				mJoinType = ChangedNames.Add(ColumnJoinType, mJoinType, value);
			}
		}
		private String mJoinType;

		/// <summary>Gets or sets the TableAlias value.</summary>
		//[Column("TableAlias", TypeName="nvarchar(60)")]
		public String TableAlias
		{
			get { return mTableAlias; }
			set
			{
				value = NetString.InitString(value);
				mTableAlias = ChangedNames.Add(ColumnTableAlias, mTableAlias, value);
			}
		}
		private String mTableAlias;
		#endregion

		#region Calculated and Join Data Properties
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewJoin";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewDataID value.</summary>
		public static string ColumnViewDataID = "ViewDataID";

		/// <summary>The TableName value.</summary>
		public static string ColumnTableName = "TableName";

		/// <summary>The TableName value.</summary>
		public static string PropertyTableName = "JoinTableName";

		/// <summary>The JoinType value.</summary>
		public static string ColumnJoinType = "JoinType";

		/// <summary>The TableAlias column name.</summary>
		public static string ColumnTableAlias = "TableAlias";

		#region Join Class Data
		#endregion

		/// <summary>The TableName maximum length.</summary>
		public static int LengthTableName = 60;

		/// <summary>The JoinType maximum length.</summary>
		public static int LengthJoinType = 60;

		/// <summary>The TableAlias maximum length.</summary>
		public static int LengthTableAlias = 60;
		#endregion
	}
}
