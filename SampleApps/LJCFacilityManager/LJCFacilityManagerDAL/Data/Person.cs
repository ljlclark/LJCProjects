// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Person.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class Persons : List<Person> { }

	/// <summary>The Person table Data Record.</summary>
	[DataContract]
	public class Person
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Person()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Person Clone()
		{
			Person retValue = MemberwiseClone() as Person;
			return retValue;
		}

		/// <summary>
		/// Creates the full combined name.
		/// </summary>
		/// <returns>Returns the full name.</returns>
		private void CreateFullName()
		{
			StringBuilder builder;

			builder = new StringBuilder(64);
			if (NetString.HasValue(mFirstName))
			{
				builder.Append(mFirstName);
			}
			if (NetString.HasValue(mMiddleInitial))
			{
				if (builder.Length > 0)
				{
					builder.Append(" ");
				}
				builder.Append(mMiddleInitial);
			}
			if (NetString.HasValue(mLastName))
			{
				if (builder.Length > 0)
				{
					builder.Append(" ");
				}
				builder.Append(mLastName);
			}
			mFullName = builder.ToString();
			mDisplayText = mFullName;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mDisplayText;
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

		/// <summary>Gets or sets the FirstName value.</summary>
		//[Column("FirstName", TypeName="nvarchar(45)")]
		public String FirstName
		{
			get { return mFirstName; }
			set
			{
				value = NetString.InitString(value);
				mFirstName = ChangedNames.Add(ColumnFirstName, mFirstName, value);
				CreateFullName();
			}
		}
		private String mFirstName;

		/// <summary>Gets or sets the MiddleInitial value.</summary>
		//[Column("MiddleInitial", TypeName="nchar(1)")]
		public String MiddleInitial
		{
			get { return mMiddleInitial; }
			set
			{
				value = NetString.InitString(value);
				mMiddleInitial = ChangedNames.Add(ColumnMiddleInitial, mMiddleInitial, value);
				CreateFullName();
			}
		}
		private String mMiddleInitial;

		/// <summary>Gets or sets the LastName value.</summary>
		//[Column("LastName", TypeName="nvarchar(45)")]
		public String LastName
		{
			get { return mLastName; }
			set
			{
				value = NetString.InitString(value);
				mLastName = ChangedNames.Add(ColumnLastName, mLastName, value);
				CreateFullName();
			}
		}
		private String mLastName;

		/// <summary>Gets or sets the Sex value.</summary>
		//[Column("Sex", TypeName="nchar(1)")]
		public String Sex
		{
			get { return mSex; }
			set
			{
				value = NetString.InitString(value);
				mSex = ChangedNames.Add(ColumnSex, mSex, value);
			}
		}
		private String mSex;

		/// <summary>Gets or sets the CodeTypeID value.</summary>
		//[Required]
		//[Column("CodeTypeID", TypeName="int")]
		public Int32 CodeTypeID
		{
			get { return mCodeTypeID; }
			set
			{
				mCodeTypeID = ChangedNames.Add(ColumnCodeTypeID, mCodeTypeID, value);
			}
		}
		private Int32 mCodeTypeID;

		/// <summary>Gets or sets the PrincipleFlag value.</summary>
		//[Required]
		//[Column("PrincipleFlag", TypeName="bit")]
		public Boolean PrincipleFlag
		{
			get { return mPrincipleFlag; }
			set
			{
				mPrincipleFlag = ChangedNames.Add(ColumnPrincipleFlag, mPrincipleFlag, value);
			}
		}
		private Boolean mPrincipleFlag;

		/// <summary>Gets or sets the PrincipleTitle value.</summary>
		//[Column("PrincipleTitle", TypeName="nvarchar(30)")]
		public String PrincipleTitle
		{
			get { return mPrincipleTitle; }
			set
			{
				value = NetString.InitString(value);
				mPrincipleTitle = ChangedNames.Add(ColumnPrincipleTitle, mPrincipleTitle, value);
			}
		}
		private String mPrincipleTitle;

		/// <summary>Gets or sets the BirthDate value.</summary>
		//[Required]
		//[Column("BirthDate", TypeName="DateTime")]
		public DateTime BirthDate
		{
			get { return mBirthDate; }
			set
			{
				mBirthDate = ChangedNames.Add(ColumnBirthDate, mBirthDate, value);
			}
		}
		private DateTime mBirthDate;

		/// <summary>Gets or sets the Phone value.</summary>
		//[Column("Phone", TypeName="nvarchar(18)")]
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

		/// <summary>Gets or sets the CellPhone value.</summary>
		//[Column("CellPhone", TypeName="nvarchar(18)")]
		public String CellPhone
		{
			get { return mCellPhone; }
			set
			{
				value = NetString.InitString(value);
				mCellPhone = ChangedNames.Add(ColumnCellPhone, mCellPhone, value);
			}
		}
		private String mCellPhone;

		/// <summary>Gets or sets the Fax value.</summary>
		//[Column("Fax", TypeName="nvarchar(18)")]
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

		/// <summary>Gets or sets the UserID value.</summary>
		//[Column("UserID", TypeName="nvarchar(10)")]
		public String UserID
		{
			get { return mUserID; }
			set
			{
				value = NetString.InitString(value);
				mUserID = ChangedNames.Add(ColumnUserID, mUserID, value);
			}
		}
		private String mUserID;

		/// <summary>Gets or sets the Password value.</summary>
		//[Column("Password", TypeName="nvarchar(100)")]
		public String Password
		{
			get { return mPassword; }
			set
			{
				value = NetString.InitString(value);
				mPassword = ChangedNames.Add(ColumnPassword, mPassword, value);
			}
		}
		private String mPassword;
		#endregion

		#region Calculated and Join Data Propertiees

		/// <summary>Gets or sets the DisplayText calculated value.</summary>
		[DataMember]
		public string DisplayText
		{
			get { return mDisplayText; }
			set
			{
				mDisplayText = ChangedNames.Add("DisplayText", mDisplayText, value);
			}
		}
		private string mDisplayText;

		/// <summary>Gets the FullName calculated value.</summary>
		[DataMember]
		public string FullName
		{
			get { return mFullName; }
			set
			{
				mFullName = ChangedNames.Add("FullName", mFullName, value);
			}
		}
		private string mFullName;

		/// <summary>Gets or sets the join Code value.</summary>
		public string Code
		{
			get { return mCode; }
			set
			{
				mCode = ChangedNames.Add(ColumnCode, mCode
					, value);
			}
		}
		private string mCode;

		/// <summary>Gets or sets the join Code TypeDescription value.</summary>
		[DataMember]
		public string TypeDescription
		{
			get { return mTypeDescription; }
			set
			{
				mTypeDescription = ChangedNames.Add(ColumnTypeDescription, mTypeDescription
					, value);
			}
		}
		private string mTypeDescription;

		/// <summary>Gets or sets the join UnitDescription value.</summary>
		[DataMember]
		public string UnitDescription
		{
			get { return mUnitDescription; }
			set
			{
				mUnitDescription = ChangedNames.Add(ColumnUnitDescription, mUnitDescription
					, value);
			}
		}
		private string mUnitDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Person";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The FirstName column name.</summary>
		public static string ColumnFirstName = "FirstName";

		/// <summary>The Middle Initial column name.</summary>
		public static string ColumnMiddleInitial = "MiddleInitial";

		/// <summary>The Last Name column name.</summary>
		public static string ColumnLastName = "LastName";

		/// <summary>The Gender column name.</summary>
		public static string ColumnSex = "Sex";

		/// <summary>The Code Type ID column name.</summary>
		public static string ColumnCodeTypeID = "CodeTypeID";

		/// <summary>The Principle Flag column name.</summary>
		public static string ColumnPrincipleFlag = "PrincipleFlag";

		/// <summary>The Principle Title column name.</summary>
		public static string ColumnPrincipleTitle = "PrincipleTitle";

		/// <summary>The Birth Date column name.</summary>
		public static string ColumnBirthDate = "BirthDate";

		/// <summary>The Phone column name.</summary>
		public static string ColumnPhone = "Phone";

		/// <summary>The Extension column name.</summary>
		public static string ColumnExtension = "Extension";

		/// <summary>The Cell Phone column name.</summary>
		public static string ColumnCellPhone = "CellPhone";

		/// <summary>The Fax column name.</summary>
		public static string ColumnFax = "Fax";

		/// <summary>The User ID column name.</summary>
		public static string ColumnUserID = "UserID";

		/// <summary>The Password column name.</summary>
		public static string ColumnPassword = "Password";

		#region Join Class Data

		/// <summary>The Full Name column name.</summary>
		public static string ColumnFullName = "FullName";

		/// <summary>The join Type Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The join Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";

		/// <summary>The join Unit Description column name.</summary>
		public static string ColumnUnitDescription = "UnitDescription";
		#endregion

		/// <summary>The FirstName maximum length.</summary>
		public static int LengthFirstName = 45;

		/// <summary>The MiddleInitial maximum length.</summary>
		public static int LengthMiddleInitial = 1;

		/// <summary>The LastName maximum length.</summary>
		public static int LengthLastName = 45;

		/// <summary>The Sex maximum length.</summary>
		public static int LengthSex = 1;

		/// <summary>The PrincipleTitle maximum length.</summary>
		public static int LengthPrincipleTitle = 30;

		/// <summary>The Phone maximum length.</summary>
		public static int LengthPhone = 18;

		/// <summary>The Extension maximum length.</summary>
		public static int LengthExtension = 4;

		/// <summary>The CellPhone maximum length.</summary>
		public static int LengthCellPhone = 18;

		/// <summary>The Fax maximum length.</summary>
		public static int LengthFax = 18;

		/// <summary>The UserID maximum length.</summary>
		public static int LengthUserID = 10;

		/// <summary>The Password maximum length.</summary>
		public static int LengthPassword = 100;
		#endregion
	}
}
