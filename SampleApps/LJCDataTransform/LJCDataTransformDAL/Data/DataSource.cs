// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataSource.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDataTransformDAL
{
	/// <summary>The Source table Data Record.</summary>
	public class DataSource : IComparable<DataSource>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataSource()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public DataSource Clone()
		{
			DataSource retValue = MemberwiseClone() as DataSource;
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
		public int CompareTo(DataSource other)
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

		/// <summary>Gets or sets the SourceID value.</summary>
		//[Required]
		//[Column("DataSourceID", TypeName="int")]
		public Int32 DataSourceID
		{
			get { return mDataSourceID; }
			set
			{
				mDataSourceID = ChangedNames.Add(ColumnDataSourceID, mDataSourceID, value);
			}
		}
		private Int32 mDataSourceID;

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

		/// <summary>Gets or sets the SourceTypeID value.</summary>
		//[Required]
		//[Column("SourceTypeID", TypeName="smallint")]
		public Int16 SourceTypeID
		{
			get { return mSourceTypeID; }
			set
			{
				mSourceTypeID = ChangedNames.Add(ColumnSourceTypeID, mSourceTypeID, value);
			}
		}
		private Int16 mSourceTypeID;

		/// <summary>Gets or sets the DataConfigName value.</summary>
		//[Column("DataConfigName", TypeName="nvarchar(100)")]
		public String DataConfigName
		{
			get { return mDataConfigName; }
			set
			{
				value = NetString.InitString(value);
				mDataConfigName = ChangedNames.Add(ColumnDataConfigName
					, mDataConfigName, value);
			}
		}
		private String mDataConfigName;

		/// <summary>Gets or sets the SourceItemName value.</summary>
		//[Column("SourceItemName", TypeName="nvarchar(100)")]
		public String SourceItemName
		{
			get { return mSourceItemName; }
			set
			{
				value = NetString.InitString(value);
				mSourceItemName = ChangedNames.Add(ColumnSourceItemName, mSourceItemName
					, value);
			}
		}
		private String mSourceItemName;

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

		/// <summary>Gets or sets the SourceStatusID value.</summary>
		//[Required]
		//[Column("SourceStatusID", TypeName="smallint")]
		public Int16 SourceStatusID
		{
			get { return mSourceStatusID; }
			set
			{
				mSourceStatusID = ChangedNames.Add(ColumnSourceStatusID, mSourceStatusID
					, value);
			}
		}
		private Int16 mSourceStatusID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "DataSource";

		/// <summary>The SourceID column name.</summary>
		public static string ColumnDataSourceID = "DataSourceID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The SourceTypeID column name.</summary>
		public static string ColumnSourceTypeID = "SourceTypeID";

		/// <summary>The ConnectionString column name.</summary>
		public static string ColumnDataConfigName = "DataConfigName";

		/// <summary>The SourceItemName column name.</summary>
		public static string ColumnSourceItemName = "SourceItemName";

		/// <summary>The LayoutID column name.</summary>
		public static string ColumnSourceLayoutID = "SourceLayoutID";

		/// <summary>The SourceStatusID column name.</summary>
		public static string ColumnSourceStatusID = "SourceStatusID";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;

		/// <summary>The ConnectionString maximum length.</summary>
		public static int LengthConnectionString = 100;

		/// <summary>The SourceItemName maximum length.</summary>
		public static int LengthSourceItemName = 100;
		#endregion
	}
}
