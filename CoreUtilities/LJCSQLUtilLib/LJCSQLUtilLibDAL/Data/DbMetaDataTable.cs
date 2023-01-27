// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataTable.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCSQLUtilLibDAL
{
	/// <summary>The DbMetaDataTable table Data Record.</summary>
	public class DbMetaDataTable : IComparable<DbMetaDataTable>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbMetaDataTable()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbMetaDataTable Clone()
		{
			DbMetaDataTable retValue = MemberwiseClone() as DbMetaDataTable;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
		public int CompareTo(DbMetaDataTable other)
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
			get { return mID;  }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the TableName value.</summary>
		//[Required]
		//[Column("TableName", TypeName="nvarchar(60)")]
		public String TableName
		{
			get { return mTableName;  }
			set
			{
				value = NetString.InitString(value);
				mTableName = ChangedNames.Add(ColumnTableName, mTableName, value);
			}
		}
		private String mTableName;

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="nvarchar(60)")]
		public String Name
		{
			get { return mName;  }
			set
			{
				value = NetString.InitString(value);
				mName = ChangedNames.Add(ColumnName, mName, value);
			}
		}
		private String mName;

		/// <summary>Gets or sets the PluralName value.</summary>
		//[Required]
		//[Column("PluralName", TypeName="nvarchar(60)")]
		public String PluralName
		{
			get { return mPluralName;  }
			set
			{
				value = NetString.InitString(value);
				mPluralName = ChangedNames.Add(ColumnPluralName, mPluralName, value);
			}
		}
		private String mPluralName;

		/// <summary>Gets or sets the Caption value.</summary>
		//[Required]
		//[Column("Caption", TypeName="nvarchar(40)")]
		public String Caption
		{
			get { return mCaption;  }
			set
			{
				value = NetString.InitString(value);
				mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
			}
		}
		private String mCaption;

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="nvarchar(100)")]
		public String Description
		{
			get { return mDescription;  }
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
		/// <remarks>
		/// Changed field name as there is already a Data Property "TableName".
		/// </remarks>
		public static string TableXName = "DBMetaDataTable";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The TableName column name.</summary>
		public static string ColumnTableName = "TableName";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The PluralName column name.</summary>
		public static string ColumnPluralName = "PluralName";

		/// <summary>The Caption column name.</summary>
		public static string ColumnCaption = "Caption";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The TableName maximum length.</summary>
		public static int LengthTableName = 60;

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The PluralName maximum length.</summary>
		public static int LengthPluralName = 60;

		/// <summary>The Caption maximum length.</summary>
		public static int LengthCaption = 40;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;
		#endregion
	}
}
