// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Business.cs
using System;
using System.Runtime.Serialization;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>The Business table Data Record.</summary>
	[DataContract]
	public class Business : IComparable<Business>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Business()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Business Clone()
		{
			Business retValue = MemberwiseClone() as Business;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(Business other)
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

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mDescription;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ID value.</summary>
		//[Required]
		//[Column("ID", TypeName="int")]
		[DataMember]
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the Name value.</summary>
		//[Column("Name", TypeName="nvarchar(60)")]
		[DataMember]
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
		//[Column("Description", TypeName="nvarchar(60)")]
		[DataMember]
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

		/// <summary>Gets or sets the TypeID value.</summary>
		//[Column("CodeTypeID", TypeName="int")]
		[DataMember]
		public Int32 CodeTypeID
		{
			get { return mCodeTypeID; }
			set
			{
				mCodeTypeID = ChangedNames.Add(ColumnCodeTypeID, mCodeTypeID, value);
			}
		}
		private Int32 mCodeTypeID;

		/// <summary>Gets or sets the EffectiveDate value.</summary>
		//[Column("EffectiveDate", TypeName="DateTime")]
		[DataMember]
		public DateTime EffectiveDate
		{
			get { return mEffectiveDate; }
			set
			{
				mEffectiveDate = ChangedNames.Add(ColumnEffectiveDate, mEffectiveDate, value);
			}
		}
		private DateTime mEffectiveDate;

		/// <summary>Gets or sets the ExpirationDate value.</summary>
		[DataMember]
		//[Column("ExpirationDate", TypeName="DateTime")]
		public DateTime ExpirationDate
		{
			get { return mExpirationDate; }
			set
			{
				mExpirationDate = ChangedNames.Add(ColumnExpirationDate, mExpirationDate, value);
			}
		}
		private DateTime mExpirationDate;

		/// <summary>Gets or sets the Phone value.</summary>
		//[Column("Phone", TypeName="nvarchar(18)")]
		[DataMember]
		public String Phone
		{
			get { return mPhone; }
			set
			{
				value = NetString.InitString(value);
				mPhone = ChangedNames.Add(ColumnPhone, mPhone, value);
			}
		}
		private String mPhone;

		/// <summary>Gets or sets the Extension value.</summary>
		//[Column("Extension", TypeName="nchar(4)")]
		[DataMember]
		public String Extension
		{
			get { return mExtension; }
			set
			{
				value = NetString.InitString(value);
				mExtension = ChangedNames.Add(ColumnExtension, mExtension, value);
			}
		}
		private String mExtension;

		/// <summary>Gets or sets the Fax value.</summary>
		//[Column("Fax", TypeName="nvarchar(18)")]
		[DataMember]
		public String Fax
		{
			get { return mFax; }
			set
			{
				value = NetString.InitString(value);
				mFax = ChangedNames.Add(ColumnFax, mFax, value);
			}
		}
		private String mFax;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>
		/// Gets or sets the join TypeDescription value.
		/// </summary>
		[DataMember]
		public string TypeDescription
		{
			get { return mTypeDescription; }
			set { mTypeDescription = NetString.InitString(value); }
		}
		private string mTypeDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Business";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The ID column name.</summary>
		public static string ColumnName = "Name";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code Type ID column name.</summary>
		public static string ColumnCodeTypeID = "CodeTypeID";

		/// <summary>The Effective Date column name.</summary>
		public static string ColumnEffectiveDate = "EffectiveDate";

		/// <summary>The Expiration Date column name.</summary>
		public static string ColumnExpirationDate = "ExpirationDate";

		/// <summary>The ID column name.</summary>
		public static string ColumnPhone = "Phone";

		/// <summary>The ID column name.</summary>
		public static string ColumnExtension = "Extension";

		/// <summary>The ID column name.</summary>
		public static string ColumnFax = "Fax";

		#region Join Class Data

		/// <summary>The join Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";
		#endregion

		/// <summary>The Name maximum length.</summary>
		public static int LengthName = 60;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;

		/// <summary>The Phone maximum length.</summary>
		public static int LengthPhone = 18;

		/// <summary>The Extension maximum length.</summary>
		public static int LengthExtension = 4;

		/// <summary>The Fax maximum length.</summary>
		public static int LengthFax = 18;
		#endregion
	}
}
