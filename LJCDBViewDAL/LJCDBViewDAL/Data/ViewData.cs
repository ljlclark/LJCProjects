// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDBViewDAL
{
	/// <summary>The ViewData table Data Record.</summary>
	public class ViewData : IComparable<ViewData>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ViewData()
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
		public int CompareTo(ViewData other)
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

		/// <summary>Gets or sets the ViewTableID value.</summary>
		//[Required]
		//[Column("ViewTableID", TypeName="int")]
		public Int32 ViewTableID
		{
			get { return mViewTableID; }
			set
			{
				mViewTableID = ChangedNames.Add(ColumnViewTableID, mViewTableID, value);
			}
		}
		private Int32 mViewTableID;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="nvarchar(60)")]
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
		#endregion

		#region Calculated and Join Data Properties
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "ViewData";

		/// <summary>The ID column value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ViewTableID column value.</summary>
		public static string ColumnViewTableID = "ViewTableID";

		/// <summary>The Name value.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description value.</summary>
		public static string ColumnDescription = "Description";

		#region Join Class Data
		#endregion

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;
		#endregion
	}
}
