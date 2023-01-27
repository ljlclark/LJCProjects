// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataKey.cs
using System;
using LJCDBClientLib;

namespace LJCSQLUtilLibDAL
{
	/// <summary>
	/// The DBMetaDataKey table Data Record. 
	/// </summary>
	public class DbMetaDataKey
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbMetaDataKey()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
		public DbMetaDataKey Clone()
		{
			DbMetaDataKey retValue = MemberwiseClone() as DbMetaDataKey;
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

		/// <summary>Gets or sets the DbMetaDataColumnID value.</summary>
		//[Required]
		//[Column("DBMetaDataColumnID", TypeName="int")]
		public Int32 DbMetaDataColumnID
		{
			get { return mDbMetaDataColumnID;  }
			set
			{
				mDbMetaDataColumnID = ChangedNames.Add(ColumnDbMetaDataColumnID, mDbMetaDataColumnID, value);
			}
		}
		private Int32 mDbMetaDataColumnID;

		/// <summary>Gets or sets the DbMetaDataKeyTypeID value.</summary>
		//[Required]
		//[Column("DBMetaDataKeyTypeID", TypeName="int")]
		public Int32 DbMetaDataKeyTypeID
		{
			get { return mDbMetaDataKeyTypeID;  }
			set
			{
				mDbMetaDataKeyTypeID = ChangedNames.Add(ColumnDbMetaDataKeyTypeID, mDbMetaDataKeyTypeID, value);
			}
		}
		private Int32 mDbMetaDataKeyTypeID;

		/// <summary>Gets or sets the ToTableID value.</summary>
		//[Column("ToTableID", TypeName="int")]
		public Int32 ToTableID
		{
			get { return mToTableID; }
			set
			{
				mToTableID = ChangedNames.Add(ColumnToTableID, mToTableID, value);
			}
		}
		private Int32 mToTableID;

		/// <summary>Gets or sets the ToColumnID value.</summary>
		//[Column("ToColumnID", TypeName="int")]
		public Int32 ToColumnID
		{
			get { return mToColumnID; }
			set
			{
				mToColumnID = ChangedNames.Add(ColumnToColumnID, mToColumnID, value);
			}
		}
		private Int32 mToColumnID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the join ColumnName value.</summary>
		public string ColumnName { get; set; }

		/// <summary>Gets or sets the join ToTableName value.</summary>
		public string ToTableName { get; set; }

		/// <summary>Gets or sets the join ToColumnName value.</summary>
		public string ToColumnName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>The table name.</summary>
		public static string TableName = "DBMetaDataKey";

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The DbMetaDataColumnID column name.</summary>
		public static string ColumnDbMetaDataColumnID = "DBMetaDataColumnID";

		/// <summary>The DbMetaDataColumnID property name.</summary>
		public static string PropertyDbMetaDataColumnID = "DbMetaDataColumnID";

		/// <summary>The DbMetaDataKeyTypeID column name.</summary>
		public static string ColumnDbMetaDataKeyTypeID = "DBMetaDataKeyTypeID";

		/// <summary>The DbMetaDataKeyTypeID property name.</summary>
		public static string PropertyDbMetaDataKeyTypeID = "DbMetaDataKeyTypeID";

		/// <summary>The ToTableID column name.</summary>
		public static string ColumnToTableID = "ToTableID";

		/// <summary>The ToColumnID column name.</summary>
		public static string ColumnToColumnID = "ToColumnID";
		#endregion

		#region Join Class Data

		/// <summary>The ToColumnID column name.</summary>
		public static string ColumnColumnName = "ColumnName";

		/// <summary>The ToColumnID column name.</summary>
		public static string ColumnToTableName = "ToTableName";

		/// <summary>The ToColumnID column name.</summary>
		public static string ColumnToColumnName = "ToColumnName";
		#endregion
	}
}
