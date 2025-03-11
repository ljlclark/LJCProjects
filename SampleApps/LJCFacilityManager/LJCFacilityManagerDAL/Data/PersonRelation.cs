// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonRelation.cs
using System;
using System.Collections.Generic;
using LJCDBClientLib;
using System.Runtime.Serialization;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class PersonRelations : List<PersonRelation> { }

	/// <summary>Represents a relationship data record.</summary>
	[DataContract]
	public class PersonRelation
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonRelation()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonRelation Clone()
		{
			PersonRelation retValue = MemberwiseClone() as PersonRelation;
			return retValue;
		}

		/// <summary>
		/// Creates the full combined person name.
		/// </summary>
		private void CreateFullName()
		{
			Person record;

			record = new Person()
			{
				FirstName = mFirstName,
				MiddleInitial = mMiddleInitial,
				LastName = mLastName
			};
			mFullName = record.FullName;
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

		/// <summary>Gets or sets the RelationID value.</summary>
		//[Required]
		//[Column("RelationID", TypeName="int")]
		public Int32 RelationID
		{
			get { return mRelationID; }
			set
			{
				mRelationID = ChangedNames.Add(ColumnRelationID, mRelationID, value);
			}
		}
		private Int32 mRelationID;

		/// <summary>Gets or sets the RelationCodeTypeID value.</summary>
		//[Required]
		//[Column("RelationCodeTypeID", TypeName="int")]
		public Int32 RelationCodeTypeID
		{
			get { return mRelationCodeTypeID; }
			set
			{
				mRelationCodeTypeID = ChangedNames.Add(ColumnRelationCodeTypeID, mRelationCodeTypeID, value);
			}
		}
		private Int32 mRelationCodeTypeID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the FullName calculated value.</summary>
		[DataMember]
		public String FullName
		{
			get { return mFullName; }
			set
			{
				mFullName = ChangedNames.Add(ColumnFullName, mFullName, value);
			}
		}
		private String mFullName;

		/// <summary>Gets or sets the FirstName join value.</summary>
		[DataMember]
		public String FirstName
		{
			get { return mFirstName; }
			set
			{
				mFirstName = ChangedNames.Add(ColumnFirstName, mFirstName, value);
				CreateFullName();
			}
		}
		private String mFirstName;

		/// <summary>Gets or sets the MiddleInitial join value.</summary>
		[DataMember]
		public string MiddleInitial
		{
			get { return mMiddleInitial; }
			set
			{
				mMiddleInitial = ChangedNames.Add(ColumnMiddleInitial, mMiddleInitial, value);
				CreateFullName();
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
				CreateFullName();
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
				mTypeDescription = ChangedNames.Add(ColumnTypeDescription, mTypeDescription
					, value);
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
		public static string TableName = "PersonRelation";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Person ID column name.</summary>
		public static string ColumnPersonID = "PersonID";

		/// <summary>The Relation ID column name.</summary>
		public static string ColumnRelationID = "RelationID";

		/// <summary>The Relation Code Type ID column name.</summary>
		public static string ColumnRelationCodeTypeID = "RelationCodeTypeID";

		#region Join Class Data

		/// <summary>The Full Name column name.</summary>
		public static string ColumnFullName = "FullName";

		/// <summary>The First Name column name.</summary>
		public static string ColumnFirstName = "FirstName";

		/// <summary>The Middle Initial column name.</summary>
		public static string ColumnMiddleInitial = "MiddleInitial";

		/// <summary>The Last Name column name.</summary>
		public static string ColumnLastName = "LastName";

		/// <summary>The Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";
		#endregion
		#endregion
	}
}
