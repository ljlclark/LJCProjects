// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVPerson.cs
using System;
using System.Collections.Generic;
using System.Text;
using LJCNetCommon;
using LJCDBClientLib;

namespace CVRDAL
{
	/// <summary>The CVPerson table Data Object.</summary>
	public class CVPerson : IComparable<CVPerson>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CVPerson()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Data Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public CVPerson Clone()
		{
			CVPerson retValue = MemberwiseClone() as CVPerson;
			return retValue;
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			string retValue;

			StringBuilder builder = new StringBuilder(64);
			builder.Append(mFirstName.Trim());
			if (NetString.HasValue(mLastName))
			{
				builder.Append($" {mLastName}");
			}
			retValue = builder.ToString();
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(CVPerson other)
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
		//[Column("ID", TypeName="int")]
		public long ID
		{
			get { return mID; }
			set
			{
				mID = ChangedNames.Add(ColumnID, mID, value);
			}
		}
		private long mID;

		/// <summary>Gets or sets the FirstName value.</summary>
		//[Required]
		//[Column("FirstName", TypeName="varchar(30)")]
		public string FirstName
		{
			get { return mFirstName; }
			set
			{
				value = NetString.InitString(value);
				mFirstName = ChangedNames.Add(ColumnFirstName, mFirstName, value);
			}
		}
		private string mFirstName;

		/// <summary>Gets or sets the MiddleName value.</summary>
		//[Required]
		//[Column("LastName", TypeName="varchar(30)")]
		public string MiddleName
		{
			get { return mMiddleName; }
			set
			{
				value = NetString.InitString(value);
				mMiddleName = ChangedNames.Add(ColumnFirstName, mMiddleName, value);
			}
		}
		private string mMiddleName;

		/// <summary>Gets or sets the LastName value.</summary>
		//[Required]
		//[Column("LastName", TypeName="varchar(30)")]
		public string LastName
		{
			get { return mLastName; }
			set
			{
				value = NetString.InitString(value);
				mLastName = ChangedNames.Add(ColumnFirstName, mLastName, value);
			}
		}
		private string mLastName;

		/// <summary>Gets or sets the CVSexID value.</summary>
		//[Required]
		//[Column("CVSexID", TypeName="int")]
		public long CVSexID
		{
			get { return mCVSexID; }
			set
			{
				// Change next line to "Property" constant if property was renamed.
				mCVSexID = ChangedNames.Add(ColumnCVSexID, mCVSexID, value);
			}
		}
		private long mCVSexID;

		/// <summary>Gets or sets the DeliveryAddressLine value.</summary>
		//[Column("DeliveryAddressLine", TypeName="varchar(45)")]
		public string DeliveryAddressLine
		{
			get { return mDeliveryAddressLine; }
			set
			{
				value = NetString.InitString(value);
				mDeliveryAddressLine = ChangedNames.Add(ColumnDeliveryAddressLine
					, mDeliveryAddressLine, value);
			}
		}
		private string mDeliveryAddressLine;

		/// <summary>Gets or sets the LastLine value.</summary>
		//[Column("LastLine", TypeName="varchar(45)")]
		public string LastLine
		{
			get { return mLastLine; }
			set
			{
				value = NetString.InitString(value);
				mLastLine = ChangedNames.Add(ColumnLastLine, mLastLine, value);
			}
		}
		private string mLastLine;

		/// <summary>Gets or sets the Phone value.</summary>
		//[Column("Phone", TypeName="varchar(15)")]
		public string Phone
		{
			get { return mPhone; }
			set
			{
				value = NetString.InitString(value);
				mPhone = ChangedNames.Add(ColumnPhone, mPhone, value);
			}
		}
		private string mPhone;

		/// <summary>Gets or sets the RegionID value.</summary>
		//[Column("RegionID", TypeName="int")]
		public int RegionID
		{
			get { return mRegionID; }
			set
			{
				mRegionID = ChangedNames.Add(ColumnRegionID, mRegionID, value);
			}
		}
		private int mRegionID;

		/// <summary>Gets or sets the ProvinceID value.</summary>
		//[Column("ProvinceID", TypeName="int")]
		public int ProvinceID
		{
			get { return mProvinceID; }
			set
			{
				mProvinceID = ChangedNames.Add(ColumnProvinceID, mProvinceID, value);
			}
		}
		private int mProvinceID;

		/// <summary>Gets or sets the CityID value.</summary>
		//[Column("CityID", TypeName="int")]
		public int CityID
		{
			get { return mCityID; }
			set
			{
				mCityID = ChangedNames.Add(ColumnCityID, mCityID, value);
			}
		}
		private int mCityID;

		/// <summary>Gets or sets the CitySectionID value.</summary>
		//[Column("CitySectionID", TypeName="int")]
		public int CitySectionID
		{
			get { return mCitySectionID; }
			set
			{
				mCitySectionID = ChangedNames.Add(ColumnCitySectionID, mCitySectionID, value);
			}
		}
		private int mCitySectionID;
		#endregion

		#region Calculated and Join Class Data Properties

		/// <summary>Gets the calculated FullName value.</summary>
		public string FullName
		{
			get
			{
				string name = "";
				if (NetString.HasValue(FirstName))
				{
					name += FirstName;
				}
				if (NetString.HasValue(LastName))
				{
					if (NetString.HasValue(name))
					{
						name += " ";
					}
					name += LastName;
				}
				return name;
			}
		}
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name.</summary>
		public static string TableName = "CVPerson";

		/// <summary>The ID column name.</summary>
		public static string ColumnID = "ID";

		/// <summary>The FirstName column name.</summary>
		public static string ColumnFirstName = "FirstName";

		/// <summary>The MiddleName column name.</summary>
		public static string ColumnMiddleName = "MiddleName";

		/// <summary>The LastName column name.</summary>
		public static string ColumnLastName = "LastName";

		/// <summary>The CVSexID column name.</summary>
		public static string ColumnCVSexID = "CVSexID";

		/// <summary>The DeliveryAddressLine column name.</summary>
		public static string ColumnDeliveryAddressLine = "DeliveryAddressLine";

		/// <summary>The LastLine column name.</summary>
		public static string ColumnLastLine = "LastLine";

		/// <summary>The Phone column name.</summary>
		public static string ColumnPhone = "Phone";

		/// <summary>The RegionID column name.</summary>
		public static string ColumnRegionID = "RegionID";

		/// <summary>The ProvinceID column name.</summary>
		public static string ColumnProvinceID = "ProvinceID";

		/// <summary>The CityID column name.</summary>
		public static string ColumnCityID = "CityID";

		/// <summary>The CitySectionID column name.</summary>
		public static string ColumnCitySectionID = "CitySectionID";

		/// <summary>The FirstName maximum length.</summary>
		public static int LengthFirstName = 30;

		/// <summary>The MiddleName maximum length.</summary>
		public static int LengthMiddleName = 30;

		/// <summary>The LastName maximum length.</summary>
		public static int LengthLastName = 30;

		/// <summary>The DeliveryAddressLine maximum length.</summary>
		public static int LengthDeliveryAddressLine = 45;

		/// <summary>The LastLine maximum length.</summary>
		public static int LengthLastLine = 45;

		/// <summary>The LastLine maximum length.</summary>
		public static int LengthPhone = 15;
		#endregion

		#region Calculated and Join Class Data

		/// <summary>The Full Name column name.</summary>
		public static string ColumnFullName = "FullName";

		/// <summary>The LastLine maximum length.</summary>
		public static int LengthFullName = 61;
		#endregion
	}

	#region Comparers

	/// <summary>Sort and search on Name value.</summary>
	public class CVPersonNameComparer : IComparer<CVPerson>
	{
		// Compares two objects.
		/// <include path='items/Compare/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int Compare(CVPerson x, CVPerson y)
		{
			int retValue;

			retValue = NetCommon.CompareNull(x, y);
			if (-2 == retValue)
			{
				// Case sensitive.
				//retValue = x.LastName.CompareTo(y.LastName);
				//if (0 == retValue)
				//{
				//	retValue = x.FirstName.CompareTo(y.FirstName);
				//	if (0 == retValue)
				//	{
				//		retValue = x.MiddleName.CompareTo(y.MiddleName);
				//	}
				//}

				// Not case sensitive.
				retValue = string.Compare(x.LastName, y.LastName, true);
				if (0 == retValue)
				{
					retValue = string.Compare(x.FirstName, y.FirstName, true);
					if (0 == retValue)
					{
						retValue = string.Compare(x.MiddleName, y.MiddleName, true);
					}
				}
			}
			return retValue;
		}
	}
	#endregion
}
