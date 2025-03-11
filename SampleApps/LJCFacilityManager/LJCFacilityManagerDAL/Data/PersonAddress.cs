// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonAddress.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class PersonAddresses : List<PersonAddress> { }

	/// <summary>Represents a person Address data record.</summary>
	[DataContract]
	public class PersonAddress
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonAddress()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonAddress Clone()
		{
			PersonAddress retValue = MemberwiseClone() as PersonAddress;
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the PersonID value.</summary>
		//[Required]
		//[Column("PersonID", TypeName="int")]
		[DataMember]
		public Int32 PersonID
		{
			get { return mPersonID; }
			set
			{
				mPersonID = ChangedNames.Add(ColumnPersonID, mPersonID, value);
			}
		}
		private Int32 mPersonID;

		/// <summary>Gets or sets the AddressID value.</summary>
		//[Required]
		//[Column("AddressID", TypeName="int")]
		public Int32 AddressID
		{
			get { return mAddressID; }
			set
			{
				mAddressID = ChangedNames.Add(ColumnAddressID, mAddressID, value);
			}
		}
		private Int32 mAddressID;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The Person ID column name.</summary>
		public static string ColumnPersonID = "PersonID";

		/// <summary>The Address ID column name.</summary>
		public static string ColumnAddressID = "AddressID";
		#endregion
	}
}
