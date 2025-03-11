// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Unit.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class Units : List<Unit> { }

	// Represents a data record.
	/// <include path='items/Unit/*' file='Doc/Unit.xml'/>
	[DataContract]
	public class Unit
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Unit()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Unit Clone()
		{
			Unit retValue = MemberwiseClone() as Unit;
			return retValue;
		}

		/// <summary>
		/// Creates the combined person name.
		/// </summary>
		private void CreatePersonName()
		{
			Person record;

			record = new Person()
			{
				FirstName = mFirstName,
				MiddleInitial = mMiddleInitial,
				LastName = mLastName
			};
			PersonName = record.FullName;
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
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the FacilityID value.</summary>
		//[Required]
		//[Column("FacilityID", TypeName="int")]
		public Int32 FacilityID
		{
			get { return mFacilityID; }
			set
			{
				mFacilityID = ChangedNames.Add(ColumnFacilityID, mFacilityID, value);
			}
		}
		private Int32 mFacilityID;

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(25)")]
		public String Code
		{
			get { return mCode; }
			set
			{
				value = NetString.InitString(value);
				mCode = ChangedNames.Add(ColumnCode, mCode, value);
			}
		}
		private String mCode;

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

		/// <summary>Gets or sets the Beds value.</summary>
		//[Required]
		//[Column("Beds", TypeName="smallint")]
		public Int16 Beds
		{
			get { return mBeds; }
			set
			{
				mBeds = ChangedNames.Add(ColumnBeds, mBeds, value);
			}
		}
		private Int16 mBeds;

		/// <summary>Gets or sets the Baths value.</summary>
		//[Required]
		//[Column("Baths", TypeName="smallint")]
		public Int16 Baths
		{
			get { return mBaths; }
			set
			{
				mBaths = ChangedNames.Add(ColumnBaths, mBaths, value);
			}
		}
		private Int16 mBaths;

		/// <summary>Gets or sets the Phone value.</summary>
		//[Column("Phone", TypeName="nvarchar(18)")]
		public String Phone
		{
			get { return mPhone; }
			set
			{
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
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the PersonName calculated value.</summary>
		[DataMember]
		public String PersonName
		{
			get { return mPersonName; }
			set
			{
				mPersonName = ChangedNames.Add(ColumnPersonName, mPersonName, value);
			}
		}
		private String mPersonName;

		/// <summary>Gets or sets the PersonID join value.</summary>
		[DataMember]
		public Int32 PersonID
		{
			get { return mPersonID; }
			set
			{
				mPersonID = ChangedNames.Add(PropertyPersonID, mPersonID, value);
			}
		}
		private Int32 mPersonID;

		/// <summary>Gets or sets the FirstName join value.</summary>
		[DataMember]
		public string FirstName
		{
			get { return mFirstName; }
			set
			{
				mFirstName = ChangedNames.Add(ColumnFirstName, mFirstName, value);
				CreatePersonName();
			}
		}
		private string mFirstName;

		/// <summary>Gets or sets the MiddleInitial join value.</summary>
		[DataMember]
		public string MiddleInitial
		{
			get { return mMiddleInitial; }
			set
			{
				mMiddleInitial = ChangedNames.Add(ColumnMiddleInitial, mMiddleInitial, value);
				CreatePersonName();
			}
		}
		private string mMiddleInitial;

		/// <summary>Gets or sets the LastName join value.</summary>
		[DataMember]
		public string LastName
		{
			get { return mLastName; }
			set
			{
				mLastName = ChangedNames.Add(ColumnLastName, mLastName, value);
				CreatePersonName();
			}
		}
		private string mLastName;

		/// <summary>Gets or sets the TypeDescription join value.</summary>
		[DataMember]
		public string TypeDescription
		{
			get { return mTypeDescription; }
			set
			{
				mTypeDescription = ChangedNames.Add(ColumnTypeDescription, mTypeDescription, value);
			}
		}
		private string mTypeDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Unit";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Facility ID column name.</summary>
		public static string ColumnFacilityID = "FacilityID";

		/// <summary>The Description column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code Type ID column name.</summary>
		public static string ColumnCodeTypeID = "CodeTypeID";

		/// <summary>The Beds column name.</summary>
		public static string ColumnBeds = "Beds";

		/// <summary>The Baths column name.</summary>
		public static string ColumnBaths = "Baths";

		/// <summary>The Phone column name.</summary>
		public static string ColumnPhone = "Phone";

		/// <summary>The Extension column name.</summary>
		public static string ColumnExtension = "Extension";

		#region Join Class Data

		/// <summary>The calculated Person Name column name.</summary>
		public static string ColumnPersonName = "PersonName";

		/// <summary>The join Person ID column name.</summary>
		public static string ColumnPersonID = "Person_Id";

		/// <summary>The join Person ID property name.</summary>
		public static string PropertyPersonID = "PersonID";

		/// <summary>The join First Name column name.</summary>
		public static string ColumnFirstName = "FirstName";

		/// <summary>The join Middle Initial column name.</summary>
		public static string ColumnMiddleInitial = "MiddleInitial";

		/// <summary>The join Last Name column name.</summary>
		public static string ColumnLastName = "LastName";

		/// <summary>The join Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";
		#endregion

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;

		/// <summary>The Phone maximum length.</summary>
		public static int LengthPhone = 18;

		/// <summary>The Extension maximum length.</summary>
		public static int LengthExtension = 4;
		#endregion
	}
}
