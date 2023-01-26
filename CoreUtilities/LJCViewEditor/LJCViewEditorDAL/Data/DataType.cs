// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataType.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCViewEditorDAL
{
	/// <summary>The DataType table Data Record.</summary>
	public class DataType : IComparable<DataType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DataType()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(DataType other)
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

		/// <summary>Gets or sets the SQLName value.</summary>
		//[Required]
		//[Column("SQLName", TypeName="varchar(60)")]
		public String SQLName
		{
			get { return mSQLName; }
			set
			{
				value = NetString.InitString(value);
				mSQLName = ChangedNames.Add(ColumnSQLName, mSQLName, value);
			}
		}
		private String mSQLName;

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
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "DataType";

		/// <summary>The DataTypeID column name.</summary>
		public static string ColumnDataTypeID = "DataTypeID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The SQLName column name.</summary>
		public static string ColumnSQLName = "SQLName";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The SQLName maximum length.</summary>
		public static int LengthSQLName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
