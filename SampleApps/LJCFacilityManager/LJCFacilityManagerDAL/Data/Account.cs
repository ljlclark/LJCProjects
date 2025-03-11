// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Account.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCFacilityManagerDAL
{
	// The Account table Data record.
	/// <include path='items/Account/*' file='Doc/ProjectFacilityManagerDAL.xml'/>
	public class Account
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Account()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Account Clone()
		{
			Account retValue = MemberwiseClone() as Account;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(Account other)
		{
			int retValue;

			if (null == other)
			{
				retValue = 1;
			}
			else
			{
				// Case sensitive.
				retValue = ID.CompareTo(other.ID);

				// Not case sensitive.
				//retValue = string.Compare(Name, other.Name, true);
			}
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
		public Int32 ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private Int32 mID;

		/// <summary>Gets or sets the Description value.</summary>
		//[Column("Description", TypeName="nvarchar(60)")]
		public String Description
		{
			get { return mDescription; }
			set
			{
				mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
			}
		}
		private String mDescription;

		/// <summary>Gets or sets the PersonID value.</summary>
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

		/// <summary>Gets or sets the BusinessID value.</summary>
		//[Column("BusinessID", TypeName="int")]
		public Int32 BusinessID
		{
			get { return mBusinessID; }
			set
			{
				mBusinessID = ChangedNames.Add(ColumnBusinessID, mBusinessID, value);
			}
		}
		private Int32 mBusinessID;

		/// <summary>Gets or sets the IDNumber value.</summary>
		//[Column("IDNumber", TypeName="nvarchar(25)")]
		public String IDNumber
		{
			get { return mIDNumber; }
			set
			{
				value = NetString.InitString(value);
				mIDNumber = ChangedNames.Add(ColumnIDNumber, mIDNumber, value);
			}
		}
		private String mIDNumber;

		/// <summary>Gets or sets the GroupNumber value.</summary>
		//[Column("GroupNumber", TypeName="nvarchar(25)")]
		public String GroupNumber
		{
			get { return mGroupNumber; }
			set
			{
				value = NetString.InitString(value);
				mGroupNumber = ChangedNames.Add(ColumnGroupNumber, mGroupNumber, value);
			}
		}
		private String mGroupNumber;

		/// <summary>Gets or sets the PlanNumber value.</summary>
		//[Column("PlanNumber", TypeName="nvarchar(25)")]
		public String PlanNumber
		{
			get { return mPlanNumber; }
			set
			{
				value = NetString.InitString(value);
				mPlanNumber = ChangedNames.Add(ColumnPlanNumber, mPlanNumber, value);
			}
		}
		private String mPlanNumber;

		/// <summary>Gets or sets the EffectiveDate value.</summary>
		//[Column("EffectiveDate", TypeName="DateTime")]
		public DateTime EffectiveDate
		{
			get { return mEffectiveDate; }
			set
			{
				mEffectiveDate = ChangedNames.Add(ColumnEffectiveDate, mEffectiveDate, value);
			}
		}
		private DateTime mEffectiveDate;

		/// <summary>Gets or sets the ExpirationDate value.</summary>
		//[Column("ExpirationDate", TypeName="DateTime")]
		public DateTime ExpirationDate
		{
			get { return mExpirationDate; }
			set
			{
				mExpirationDate = ChangedNames.Add(ColumnExpirationDate, mExpirationDate, value);
			}
		}
		private DateTime mExpirationDate;
		#endregion

		#region Calculated and Join Data Properties

		/// <summary>Gets or sets the Name join value.</summary>
		public String Name
		{
			get { return mBusinessName; }
			set
			{
				value = NetString.InitString(value);
				mBusinessName = ChangedNames.Add(ColumnBusinessName, mBusinessName, value);
			}
		}
		private String mBusinessName;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "Account";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The Description column name.</summary>
		public static string ColumnDescription = "Description";

		/// <summary>The Person ID column name.</summary>
		public static string ColumnPersonID = "PersonID";

		/// <summary>The Business ID column name.</summary>
		public static string ColumnBusinessID = "BusinessID";

		/// <summary>The ID Number column name.</summary>
		public static string ColumnIDNumber = "IDNumber";

		/// <summary>The Group Number column name.</summary>
		public static string ColumnGroupNumber = "GroupNumber";

		/// <summary>The Plan Number column name.</summary>
		public static string ColumnPlanNumber = "PlanNumber";

		/// <summary>The Effective Date column name.</summary>
		public static string ColumnEffectiveDate = "EffectiveDate";

		/// <summary>The Expiration Date column name.</summary>
		public static string ColumnExpirationDate = "ExpirationDate";

		#region Join Class Data

		/// <summary>The BusinessName join column name.</summary>
		public static string ColumnBusinessName = "Name";
		#endregion

		/// <summary>The Description maximum length.</summary>
		public static int LengthDescription = 60;

		/// <summary>The IDNumber maximum length.</summary>
		public static int LengthIDNumber = 25;

		/// <summary>The GroupNumber maximum length.</summary>
		public static int LengthGroupNumber = 25;

		/// <summary>The PlanNumber maximum length.</summary>
		public static int LengthPlanNumber = 25;
		#endregion
	}
}
