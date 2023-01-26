// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOn.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewJoinOn table Data Record.</summary>
	public class ViewJoinOn : IComparable<ViewJoinOn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public ViewJoinOn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Public Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return $"on {FromColumnName} {JoinOnOperator} {ToColumnName}";
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewJoinOn other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = FromColumnName.CompareTo(other.FromColumnName);

				// Not case sensitive.
				retValue = string.Compare(FromColumnName, other.FromColumnName, true);
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

		/// <summary>Gets or sets the FromColumnName value.</summary>
		//[Required]
		//[Column("FromColumnName", TypeName="nvarchar(60)")]
		public String FromColumnName
		{
			get { return mFromColumnName; }
			set
			{
				value = NetString.InitString(value);
				mFromColumnName = ChangedNames.Add(ColumnFromColumnName, mFromColumnName, value);
			}
		}
		private String mFromColumnName;

		/// <summary>Gets or sets the ToColumnName value.</summary>
		//[Required]
		//[Column("ToColumnName", TypeName="nvarchar(60)")]
		public String ToColumnName
		{
			get { return mToColumnName; }
			set
			{
				value = NetString.InitString(value);
				mToColumnName = ChangedNames.Add(ColumnToColumnName, mToColumnName, value);
			}
		}
		private String mToColumnName;

		/// <summary>Gets or sets the JoinOnOperator value.</summary>
		//[Column("JoinOnOperator", TypeName="nvarchar(5)")]
		public String JoinOnOperator
		{
			get { return mJoinOnOperator; }
			set
			{
				value = NetString.InitString(value);
				mJoinOnOperator = ChangedNames.Add(ColumnJoinOnOperator, mJoinOnOperator, value);
			}
		}
		private String mJoinOnOperator;
		#endregion

		#region Calculated and Join Data Properties
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewJoinOn";

		/// <summary>The ID value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewJoinID value.</summary>
		public static string ColumnViewJoinID = "ViewJoinID";

		/// <summary>The FromColumnName value.</summary>
		public static string ColumnFromColumnName = "FromColumnName";

		/// <summary>The ToColumnName value.</summary>
		public static string ColumnToColumnName = "ToColumnName";

		/// <summary>The JoinOnOperator value.</summary>
		public static string ColumnJoinOnOperator = "JoinOnOperator";

		/// <summary>The FromColumnName maximum length.</summary>
		public static int LengthFromColumnName = 60;

		/// <summary>The ToColumnName maximum length.</summary>
		public static int LengthToColumnName = 60;

		/// <summary>The JoinOperator maximum length.</summary>
		public static int LengthJoinOperator = 5;
		#endregion
	}
}
