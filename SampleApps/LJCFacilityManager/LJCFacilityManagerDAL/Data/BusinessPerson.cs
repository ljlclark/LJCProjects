// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessPerson.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class BusinessPersons : List<BusinessPerson> { }

	/// <summary>Represents a business contact data record.</summary>
	[DataContract]
	public class BusinessPerson
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessPerson()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessPerson Clone()
		{
			BusinessPerson retValue = MemberwiseClone() as BusinessPerson;
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
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The Business ID column name.</summary>
		public static string ColumnBusinessID = "BusinessID";

		/// <summary>The Person ID column name.</summary>
		public static string ColumnPersonID = "PersonID";
		#endregion
	}
}
