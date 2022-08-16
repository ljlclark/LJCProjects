// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewFilter table Data Record.</summary>
	public class ViewFilter : IComparable<ViewFilter>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ViewFilter()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(ViewFilter other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = Name.CompareTo(other.Name);

				// Not case sensitive.
				retValue = string.Compare(Name, other.Name, true);
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

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="nvarchar(60)")]
		public String Name
		{
			get { return mName; }
			set
			{
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private String mName;

		/// <summary>Gets or sets the BooleanOperator value.</summary>
		//[Required]
		//[Column("BooleanOperator", TypeName="nvarchar(3)")]
		public String BooleanOperator
		{
			get { return mBooleanOperator; }
			set
			{
				value = NetString.InitString(value);
				mBooleanOperator = ChangedNames.Add(ColumnBooleanOperator, mBooleanOperator, value);
			}
		}
		private String mBooleanOperator;
		#endregion

		#region Calculated and Join Data Properties
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewFilter";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewData ID column value.</summary>
		public static string ColumnViewDataID = "ViewDataID";

		/// <summary>The Name value.</summary>
		public static string ColumnName = "Name";

		/// <summary>The BooleanOperator column name.</summary>
		public static string ColumnBooleanOperator = "BooleanOperator";

		#region Join Class Data
		#endregion

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The BooleanOperator maximum length.</summary>
		public static int LengthBooleanOperator = 3;
		#endregion
	}
}
