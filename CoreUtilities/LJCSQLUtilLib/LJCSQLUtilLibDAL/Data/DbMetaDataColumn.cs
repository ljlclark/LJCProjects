// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbMetaDataColumn.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCSQLUtilLibDAL
{
	/// <summary>The DbMetaDataColumn table Data Record.</summary>
	public class DbMetaDataColumn : IComparable<DbMetaDataColumn>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public DbMetaDataColumn()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public DbMetaDataColumn Clone()
		{
			DbMetaDataColumn retValue = MemberwiseClone() as DbMetaDataColumn;
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
		public int CompareTo(DbMetaDataColumn other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				//retValue = ColumnName.CompareTo(other.ColumnName);

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
			get { return mID;  }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the DbMetaDataTableID value.</summary>
		//[Required]
		//[Column("DBMetaDataTableID", TypeName="int")]
		public Int32 DbMetaDataTableID
		{
			get { return mDbMetaDataTableID;  }
			set
			{
				mDbMetaDataTableID = ChangedNames.Add(PropertyDbMetaDataTableID
					, mDbMetaDataTableID, value);
			}
		}
		private Int32 mDbMetaDataTableID;

		/// <summary>Gets or sets the Sequence value.</summary>
		//[Required]
		//[Column("Sequence", TypeName="int")]
		public Int32 Sequence
		{
			get { return mSequence;  }
			set
			{
				mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
			}
		}
		private Int32 mSequence;

		/// <summary>Gets or sets the ColumnName value.</summary>
		//[Required]
		//[Column("ColumnName", TypeName="nvarchar(60)")]
		public String ColumnName
		{
			get { return mColumnName;  }
			set
			{
				value = NetString.InitString(value);
				mColumnName = ChangedNames.Add(ColumnColumnName, mColumnName, value);
			}
		}
		private String mColumnName;

		/// <summary>Gets or sets the PropertyName value.</summary>
		//[Required]
		//[Column("PropertyName", TypeName="nvarchar(60)")]
		public String PropertyName
		{
			get { return mPropertyName;  }
			set
			{
				value = NetString.InitString(value);
				mPropertyName = ChangedNames.Add(ColumnPropertyName, mPropertyName
					, value);
			}
		}
		private String mPropertyName;

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

		/// <summary>Gets or sets the ShortCaption value.</summary>
		//[Required]
		//[Column("ShortCaption", TypeName="nvarchar(30)")]
		public String ShortCaption
		{
			get { return mShortCaption;  }
			set
			{
				value = NetString.InitString(value);
				mShortCaption = ChangedNames.Add(ColumnShortCaption, mShortCaption
					, value);
			}
		}
		private String mShortCaption;

		/// <summary>Gets or sets the Caption value.</summary>
		//[Required]
		//[Column("Caption", TypeName="nvarchar(60)")]
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

		/// <summary>Gets or sets the DataTypeName value.</summary>
		//[Required]
		//[Column("DataTypeName", TypeName="nvarchar(20)")]
		public String DataTypeName
		{
			get { return mDataTypeName;  }
			set
			{
				value = NetString.InitString(value);
				mDataTypeName = ChangedNames.Add(ColumnDataTypeName, mDataTypeName
					, value);
			}
		}
		private String mDataTypeName;

		/// <summary>Gets or sets the MaxLength value.</summary>
		//[Required]
		//[Column("MaxLength", TypeName="int")]
		public Int32 MaxLength
		{
			get { return mMaxLength;  }
			set
			{
				mMaxLength = ChangedNames.Add(ColumnMaxLength, mMaxLength, value);
			}
		}
		private Int32 mMaxLength;

		/// <summary>Gets or sets the AllowDbNull value.</summary>
		//[Required]
		//[Column("AllowDBNull", TypeName="bit")]
		public Boolean AllowDBNull
		{
			get { return mAllowDBNull;  }
			set
			{
				mAllowDBNull = ChangedNames.Add(ColumnAllowDbNull, mAllowDBNull, value);
			}
		}
		private Boolean mAllowDBNull;

		/// <summary>Gets or sets the AutoIncrement value.</summary>
		//[Required]
		//[Column("AutoIncrement", TypeName="bit")]
		public Boolean AutoIncrement
		{
			get { return mAutoIncrement;  }
			set
			{
				mAutoIncrement = ChangedNames.Add(ColumnAutoIncrement, mAutoIncrement
					, value);
			}
		}
		private Boolean mAutoIncrement;

		/// <summary>Gets or sets the Unique value.</summary>
		//[Required]
		//[Column("Unique", TypeName="bit")]
		public Boolean Unique
		{
			get { return mUnique;  }
			set
			{
				mUnique = ChangedNames.Add(ColumnUnique, mUnique, value);
			}
		}
		private Boolean mUnique;

		/// <summary>Gets or sets the DefaultValue value.</summary>
		//[Column("DefaultValue", TypeName="nvarchar(60)")]
		public String DefaultValue
		{
			get { return mDefaultValue; }
			set
			{
				value = NetString.InitString(value);
				mDefaultValue = ChangedNames.Add(ColumnDefaultValue, mDefaultValue
					, value);
			}
		}
		private String mDefaultValue;
		#endregion

		#region Class Properties

		/// <summary>The table name.</summary>
		public static string TableName = "DBMetaDataColumn";

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The DbMetaDataTableID column name.</summary>
		public static string ColumnDbMetaDataTableID = "DBMetaDataTableID";

		/// <summary>The DbMetaDataTableID property name.</summary>
		public static string PropertyDbMetaDataTableID = "DbMetaDataTableID";

		/// <summary>The Sequence column name.</summary>
		public static string ColumnSequence = "Sequence";

		/// <summary>The ColumnName column name.</summary>
		public static string ColumnColumnName = "ColumnName";

		/// <summary>The PropertyName column name.</summary>
		public static string ColumnPropertyName = "PropertyName";

		/// <summary>The Name column name.</summary>
		/// <remarks>
		/// Changed this field because there was already a property named
		/// "ColumnName".</remarks>
		public static string ColumnXName = "Name";

		/// <summary>The ShortCaption column name.</summary>
		public static string ColumnShortCaption = "ShortCaption";

		/// <summary>The Caption column name.</summary>
		public static string ColumnCaption = "Caption";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The DataTypeName column name.</summary>
		public static string ColumnDataTypeName = "DataTypeName";

		/// <summary>The MaxLength column name.</summary>
		public static string ColumnMaxLength = "MaxLength";

		/// <summary>The AllowDbNull column name.</summary>
		public static string ColumnAllowDbNull = "AllowDBNull";

		/// <summary>The AutoIncrement column name.</summary>
		public static string ColumnAutoIncrement = "AutoIncrement";

		/// <summary>The Unique column name.</summary>
		public static string ColumnUnique = "Unique";

		/// <summary>The DefaultValue column name.</summary>
		public static string ColumnDefaultValue = "DefaultValue";

		/// <summary>The ColumnName maximum length.</summary>
		public static int LengthColumnName = 60;

		/// <summary>The PropertyName maximum length.</summary>
		public static int LengthPropertyName = 60;

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The ShortCaption maximum length.</summary>
		public static int LengthShortCaption = 30;

		/// <summary>The Caption maximum length.</summary>
		public static int LengthCaption = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 100;

		/// <summary>The DataTypeName maximum length.</summary>
		public static int LengthDataTypeName = 20;

		/// <summary>The DefaultValue maximum length.</summary>
		public static int LengthDefaultValue = 60;
		#endregion
	}

	/// <summary>Sort and search MDColumn on Sequence.</summary>
	public class MDSequenceComparer : IComparer<DbMetaDataColumn>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public int Compare(DbMetaDataColumn x, DbMetaDataColumn y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				retValue = x.Sequence.CompareTo(y.Sequence);
			}
			return retValue;
		}
	}
}
