// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessAddress.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class BusinessAddresses : List<BusinessAddress> { }

	/// <summary>The BusinessAddress table Data Record.</summary>
	[DataContract]
	public class BusinessAddress
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessAddress()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessAddress Clone()
		{
			BusinessAddress retValue = MemberwiseClone() as BusinessAddress;
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the BusinessID value.</summary>
		//[Required]
		//[Column("BusinessID", TypeName="int")]
		[DataMember]
		public Int32 BusinessID
		{
			get { return mBusinessID; }
			set
			{
				mBusinessID = ChangedNames.Add(ColumnBusinessID, mBusinessID, value);
			}
		}
		private Int32 mBusinessID;

		/// <summary>Gets or sets the AddressID value.</summary>
		//[Required]
		//[Column("AddressID", TypeName="int")]
		[DataMember]
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

		/// <summary>The Business ID column name.</summary>
		public static string ColumnBusinessID = "BusinessID";

		/// <summary>The Address ID column name.</summary>
		public static string ColumnAddressID = "AddressID";
		#endregion
	}
}
