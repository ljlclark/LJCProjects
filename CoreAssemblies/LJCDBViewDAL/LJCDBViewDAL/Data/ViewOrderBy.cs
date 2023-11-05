// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderBy.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;

namespace LJCDBViewDAL
{
	/// <summary>The ViewOrderBy table Data Record.</summary>
	public class ViewOrderBy : IComparable<ViewOrderBy>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ViewOrderBy()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Public Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mColumnName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(ViewOrderBy other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
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
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewOrderBy";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewDataID column value.</summary>
		public static string ColumnViewDataID = "ViewDataID";

		/// <summary>The ColumnName value.</summary>
		public static string ColumnColumnName = "ColumnName";

		/// <summary>The ColumnName maximum length.</summary>
		public static int LengthColumnName = 60;
		#endregion
	}
}
