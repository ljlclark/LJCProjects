// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Fixture.cs
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	/// <summary>Represents a collection of record objects.</summary>
	[CollectionDataContract]
	public class Fixtures : List<Fixture> { }

	// The Fixture table Data Record.
	/// <include path='items/Fixture/*' file='Doc/Fixture.xml'/>
	public class Fixture
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Fixture()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Fixture Clone()
		{
			Fixture retValue = MemberwiseClone() as Fixture;
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

		/// <summary>Gets or sets the UnitID value.</summary>
		//[Required]
		//[Column("UnitID", TypeName="int")]
		[DataMember]
		public Int32 UnitID
		{
			get { return mUnitID; }
			set
			{
				mUnitID = ChangedNames.Add(ColumnUnitID, mUnitID, value);
			}
		}
		private Int32 mUnitID;

		/// <summary>Gets or sets the Code value.</summary>
		//[Required]
		//[Column("Code", TypeName="nvarchar(25)")]
		[DataMember]
		public string Code
		{
			get { return mCode; }
			set
			{
				value = NetString.InitString(value);
				mCode = ChangedNames.Add(ColumnCode, mCode, value);
			}
		}
		private string mCode;

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

		/// <summary>Gets or sets the Make value.</summary>
		//[Column("Make", TypeName="nvarchar(25)")]
		public String Make
		{
			get { return mMake; }
			set
			{
				value = NetString.InitString(value);
				mMake = ChangedNames.Add(ColumnMake, mMake, value);
			}
		}
		private String mMake;

		/// <summary>Gets or sets the Model value.</summary>
		//[Column("Model", TypeName="nvarchar(25)")]
		public String Model
		{
			get { return mModel; }
			set
			{
				value = NetString.InitString(value);
				mModel = ChangedNames.Add(ColumnModel, mModel, value);
			}
		}
		private String mModel;

		/// <summary>Gets or sets the SerialNumber value.</summary>
		//[Column("SerialNumber", TypeName="nvarchar(25)")]
		public String SerialNumber
		{
			get { return mSerialNumber; }
			set
			{
				value = NetString.InitString(value);
				mSerialNumber = ChangedNames.Add(ColumnSerialNumber, mSerialNumber, value);
			}
		}
		private String mSerialNumber;
		#endregion

		#region Calculated and Join Data Properties

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
		public static string TableName = "Fixture";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Unit ID column name.</summary>
		public static string ColumnUnitID = "UnitID";

		/// <summary>The Code column name.</summary>
		public static string ColumnCode = "Code";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Code Type ID column name.</summary>
		public static string ColumnCodeTypeID = "CodeTypeID";

		/// <summary>The Make column name.</summary>
		public static string ColumnMake = "Make";

		/// <summary>The Model column name.</summary>
		public static string ColumnModel = "Model";

		/// <summary>The Serial Number column name.</summary>
		public static string ColumnSerialNumber = "SerialNumber";

		#region Join Class Data

		/// <summary>The join Type Description column name.</summary>
		public static string ColumnTypeDescription = "TypeDescription";

		/// <summary>The join Unit Description column name.</summary>
		public static string ColumnUnitDescription = "UnitDescription";
		#endregion

		/// <summary>The Code maximum length.</summary>
		public static int LengthCode = 25;

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;

		/// <summary>The Make maximum length.</summary>
		public static int LengthMake = 25;

		/// <summary>The Model maximum length.</summary>
		public static int LengthModel = 25;

		/// <summary>The SerialNumber maximum length.</summary>
		public static int LengthSerialNumber = 25;
		#endregion
	}
}
