// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataKeyTypes.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCSQLUtilLibDAL
{
	/// <summary>The DBMetaDataKeyType table Data Record.</summary>
	public class DbMetaDataKeyType : IComparable<DbMetaDataKeyType>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public DbMetaDataKeyType()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public DbMetaDataKeyType Clone()
		{
			DbMetaDataKeyType retValue = MemberwiseClone() as DbMetaDataKeyType;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mName;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(DbMetaDataKeyType other)
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

		/// <summary>Gets or sets the Name value.</summary>
		//[Required]
		//[Column("Name", TypeName="varchar(15)")]
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

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="nvarchar(40)")]
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

		/// <summary>The table name.</summary>
		public static string TableName = "DBMetaDataKeyType";

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Name column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 15;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 40;
		#endregion
	}
}
