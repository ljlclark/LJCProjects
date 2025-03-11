// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Facility.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class Facilities : List<Facility> { }

	// Represents a facility data record.
	/// <include path='items/Facility/*' file='Doc/Facility.xml'/>
	[DataContract]
	public class Facility
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Facility()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Facility Clone()
		{
			Facility retValue = MemberwiseClone() as Facility;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mDescription;
		}
		#endregion

		#region Properties

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

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(25)")]
		[DataMember]
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

		/// <summary>Gets or sets the CodeTypeID value.</summary>
		//[Required]
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
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the TypeDescription value.</summary>
		[DataMember]
		public String TypeDescription
		{
			get { return mTypeDescription; }
			set
			{
				mTypeDescription = ChangedNames.Add(ColumnTypeDescription, mTypeDescription, value);
			}
		}
		private String mTypeDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Equipment";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code Type ID column name.</summary>
		public static string ColumnCodeTypeID = "CodeTypeID";

		#region Join Class Data

		/// <summary>The join Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";
		#endregion

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;
		#endregion
	}
}
