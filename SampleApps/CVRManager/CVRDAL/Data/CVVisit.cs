// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVVisit.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
	/// <summary>The CVVisit table Data Object.</summary>
	public class CVVisit : IComparable<CVVisit>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CVVisit()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CVVisit Clone()
		{
			CVVisit retValue = MemberwiseClone() as CVVisit;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue;

			retValue =	$"{mID}-{mRegisterTime}";
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(CVVisit other)
		{
			int retValue;

			if (null == other)
			{
				// This value is greater than null.
				retValue = 1;
			}
			else
			{
				retValue = ID.CompareTo(other.ID);
			}
			return retValue;
		}
		#endregion

		#region Data Properties

		// Update ChangedNames.Add() statements to "Property" constant
		// if property was renamed.

		/// <summary>Gets or sets the ID value.</summary>
		//[Required]
		//[Column("ID", TypeName="bigint")]
		public long ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private long mID;

		/// <summary>Gets or sets the CVPersonID value.</summary>
		//[Required]
		//[Column("FacilityID", TypeName="int")]
		public int FacilityID
		{
			get { return mFacilityID; }
			set
			{
				mFacilityID = ChangedNames.Add(ColumnFacilityID, mFacilityID, value);
			}
		}
		private int mFacilityID;

		/// <summary>Gets or sets the CVPersonID value.</summary>
		//[Required]
		//[Column("CVPersonID", TypeName="int")]
		public long CVPersonID
		{
			get { return mCVPersonID; }
			set
			{
				mCVPersonID = ChangedNames.Add(ColumnCVPersonID, mCVPersonID, value);
			}
		}
		private long mCVPersonID;

		/// <summary>Gets or sets the RegisterTime value.</summary>
		//[Required]
		//[Column("RegisterTime", TypeName="datetime")]
		public DateTime RegisterTime
		{
			get { return mRegisterTime; }
			set
			{
				mRegisterTime = ChangedNames.Add(ColumnRegisterTime, mRegisterTime, value);
			}
		}
		private DateTime mRegisterTime;

		/// <summary>Gets or sets the EnterTime value.</summary>
		//[Column("EnterTime", TypeName="datetime")]
		public DateTime EnterTime
		{
			get { return mEnterTime; }
			set
			{
				mEnterTime = ChangedNames.Add(ColumnEnterTime, mEnterTime, value);
			}
		}
		private DateTime mEnterTime;

		/// <summary>Gets or sets the ExitTime value.</summary>
		//[Column("ExitTime", TypeName="datetime")]
		public DateTime ExitTime
		{
			get { return mExitTime; }
			set
			{
				mExitTime = ChangedNames.Add(ColumnExitTime, mExitTime, value);
			}
		}
		private DateTime mExitTime;

		/// <summary>Gets or sets the Temperature value.</summary>
		//[Column("BaseTemperature", TypeName="varchar(5)")]
		public string BaseTemperature
		{
			get { return mBaseTemperature; }
			set
			{
				value = NetString.InitString(value);
				mBaseTemperature = ChangedNames.Add(ColumnBaseTemperature
					, mBaseTemperature, value);
			}
		}
		private string mBaseTemperature;

		/// <summary>Gets or sets the ExitTime value.</summary>
		//[Column("BaseTemperatureUnitID", TypeName="int")]
		public int BaseTemperatureUnitID
		{
			get { return mBaseTemperatureUnitID; }
			set
			{
				mBaseTemperatureUnitID = ChangedNames.Add(ColumnBaseTemperatureUnitID
					, mBaseTemperatureUnitID, value);
			}
		}
		private int mBaseTemperatureUnitID;

		/// <summary>Gets or sets the Temperature value.</summary>
		//[Column("Temperature", TypeName="varchar(5)")]
		public string Temperature
		{
			get { return mTemperature; }
			set
			{
				value = NetString.InitString(value);
				mTemperature = ChangedNames.Add(ColumnTemperature, mTemperature
					, value);
			}
		}
		private string mTemperature;

		/// <summary>Gets or sets the ExitTime value.</summary>
		//[Column("TemperatureUnitID", TypeName="int")]
		public int TemperatureUnitID
		{
			get { return mTemperatureUnitID; }
			set
			{
				mTemperatureUnitID = ChangedNames.Add(ColumnTemperatureUnitID
					, mTemperatureUnitID, value);
			}
		}
		private int mTemperatureUnitID;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the Join FirstName value.</summary>
		public string FirstName { get; set; }

		/// <summary>Gets or sets the Join MiddleName value.</summary>
		public string MiddleName { get; set; }

		/// <summary>Gets or sets the Join LastName value.</summary>
		public string LastName { get; set; }
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table value.</summary>
		public static string TableName = "CVVisit";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The FacilityID column name.</summary>
		public static string ColumnFacilityID = "FacilityID";

		/// <summary>The CVPersonID column name.</summary>
		public static string ColumnCVPersonID = "CVPersonID";

		/// <summary>The RegisterTime column name.</summary>
		public static string ColumnRegisterTime = "RegisterTime";

		/// <summary>The EnterTime column name.</summary>
		public static string ColumnEnterTime = "EnterTime";

		/// <summary>The ExitTime column name.</summary>
		public static string ColumnExitTime = "ExitTime";

		/// <summary>The Base Temperature column name.</summary>
		public static string ColumnBaseTemperature = "BaseTemperature";

		/// <summary>The Base Temerature UnitID column name.</summary>
		public static string ColumnBaseTemperatureUnitID = "BaseTemperatureUnitID";

		/// <summary>The Temperature column name.</summary>
		public static string ColumnTemperature = "Temperature";

		/// <summary>The Temerature UnitID column name.</summary>
		public static string ColumnTemperatureUnitID = "TemperatureUnitID";

		/// <summary>The Temerature maximum length.</summary>
		public static int LengthTemperature = 5;
		#endregion

		#region Calculated and Join Class Data

		/// <summary>The Join FirstName column name.</summary>
		public static string JoinFirstName = "FirstName";

		/// <summary>The Join MiddleName column name.</summary>
		public static string JoinMiddleName = "MiddleName";

		/// <summary>The Join LastName column name.</summary>
		public static string JoinLastName = "LastName";
		#endregion
	}
}
