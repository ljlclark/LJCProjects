// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitPerson.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class UnitPersons : List<UnitPerson> { }

	/// <summary>Represents a unit person data record.</summary>
	[DataContract]
	public class UnitPerson
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitPerson()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitPerson Clone()
		{
			UnitPerson retValue = MemberwiseClone() as UnitPerson;
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
			mPersonName = record.FullName;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the UnitID value.</summary>
		//[Required]
		//[Column("UnitID", TypeName="int")]
		public Int32 UnitID
		{
			get { return mUnitID; }
			set
			{
				mUnitID = ChangedNames.Add(ColumnUnitID, mUnitID, value);
			}
		}
		private Int32 mUnitID;

		/// <summary>Gets or sets the PersonID value.</summary>
		//[Required]
		//[Column("PersonID", TypeName="int")]
		public Int32 PersonID
		{
			get { return mPersonID; }
			set
			{
				mPersonID = ChangedNames.Add(ColumnPersonID, mPersonID, value);
			}
		}
		private Int32 mPersonID;

		/// <summary>Gets or sets the BeginDate value.</summary>
		//[Required]
		//[Column("BeginDate", TypeName="DateTime")]
		public DateTime BeginDate
		{
			get { return mBeginDate; }
			set
			{
				mBeginDate = ChangedNames.Add(ColumnBeginDate, mBeginDate, value);
			}
		}
		private DateTime mBeginDate;

		/// <summary>Gets or sets the EndDate value.</summary>
		//[Column("EndDate", TypeName="DateTime")]
		public DateTime EndDate
		{
			get { return mEndDate; }
			set
			{
				mEndDate = ChangedNames.Add(ColumnEndDate, mEndDate, value);
			}
		}
		private DateTime mEndDate;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the PersonName calculated value.</summary>
		[DataMember]
		public String PersonName
		{
			get { return mPersonName; }
			set
			{
				mPersonName = ChangedNames.Add(PropertyPersonName, mPersonName, value);
			}
		}
		private String mPersonName;

		/// <summary>Gets or sets the FirstName join value.</summary>
		[DataMember]
		public String FirstName
		{
			get { return mFirstName; }
			set
			{
				mFirstName = ChangedNames.Add(PropertyFirstName, mFirstName, value);
				CreatePersonName();
			}
		}
		private String mFirstName;

		/// <summary>Gets or sets the MiddleInitial join value.</summary>
		[DataMember]
		public String MiddleInitial
		{
			get { return mMiddleInitial; }
			set
			{
				mMiddleInitial = ChangedNames.Add(PropertyMiddleInitial, mMiddleInitial, value);
				CreatePersonName();
			}
		}
		private String mMiddleInitial;

		/// <summary>Gets or sets the join LastName value.</summary>
		[DataMember]
		public String LastName
		{
			get { return mLastName; }
			set
			{
				mLastName = ChangedNames.Add(PropertyLastName, mLastName, value);
				CreatePersonName();
			}
		}
		private String mLastName;

		/// <summary>Gets or sets the UnitDescription join value.</summary>
		[DataMember]
		public String UnitDescription
		{
			get { return mUnitDescription; }
			set
			{
				mUnitDescription = ChangedNames.Add(PropertyUnitDescription, mUnitDescription
					, value);
			}
		}
		private String mUnitDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "UnitPerson";

		/// <summary>The Unit ID column name.</summary>
		public static string ColumnUnitID = "UnitID";

		/// <summary>The Person ID column name.</summary>
		public static string ColumnPersonID = "PersonID";

		/// <summary>The Begin Date column name.</summary>
		public static string ColumnBeginDate = "BeginDate";

		/// <summary>The End Date column name.</summary>
		public static string ColumnEndDate = "EndDate";

		#region Calculated and Join Class Data
		
		/// <summary>The calculated Person Name column name.</summary>
		public static string PropertyPersonName = "PersonName";

		/// <summary>The join FirstName column name.</summary>
		public static string PropertyFirstName = "FirstName";

		/// <summary>The join Middle Initial column name.</summary>
		public static string PropertyMiddleInitial = "MiddleInitial";

		/// <summary>The join Last Name column name.</summary>
		public static string PropertyLastName = "LastName";

		/// <summary>The join Unit Description column name.</summary>
		public static string PropertyUnitDescription = "UnitDescription";
		#endregion
		#endregion
	}
}
