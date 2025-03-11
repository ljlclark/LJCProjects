// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Address.cs
using System;
using System.Text;
using LJCNetCommon;
using LJCDBClientLib;

namespace LJCFacilityManagerDAL
{
	/// <summary>The Address table Data Record.</summary>
	public class Address : IComparable<Address>
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Address()
		{
			ChangedNames = new ChangedNames();
		}
		#endregion

		#region Methods

		// Creates and returns a clone of this object.
		/// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Address Clone()
		{
			Address retValue = MemberwiseClone() as Address;
			return retValue;
		}

		// Provides the default Sort functionality.
		/// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public int CompareTo(Address other)
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

		/// <summary>
		/// Creates the calculated value.
		/// </summary>
		private void CreateCityStateZip()
		{
			StringBuilder builder;

			builder = new StringBuilder(64);
			if (NetString.HasValue(mCityName))
			{
				builder.Append(mCityName);
			}
			if (NetString.HasValue(mProvinceName))
			{
				if (builder.Length > 0)
				{
					if (NetString.HasValue(mCityName))
					{
						builder.Append(",");
					}
					builder.Append(" ");
				}
				builder.Append(mProvinceName);
			}
			if (NetString.HasValue(mPostalCode))
			{
				if (builder.Length > 0)
				{
					builder.Append(" ");
				}
				builder.Append(mPostalCode);
			}
			mCityStateZip = builder.ToString();
		}

		// The object string identifier.
		/// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public override string ToString()
		{
			return mStreet;
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

		/// <summary>Gets or sets the Region ID value.</summary>
		//[Column("RegionID", TypeName="int")]
		public Int32 RegionID
		{
			get { return mRegionID; }
			set
			{
				mRegionID = ChangedNames.Add(ColumnRegionID, mRegionID, value);
			}
		}
		private Int32 mRegionID;

		/// <summary>Gets or sets the Province ID value.</summary>
		//[Column("ProvinceID", TypeName="int")]
		public Int32 ProvinceID
		{
			get { return mProvinceID; }
			set
			{
				mProvinceID = ChangedNames.Add(ColumnProvinceID, mProvinceID, value);
			}
		}
		private Int32 mProvinceID;

		/// <summary>Gets or sets the City ID value.</summary>
		//[Column("CityID", TypeName="int")]
		public Int32 CityID
		{
			get { return mCityID; }
			set
			{
				mCityID = ChangedNames.Add(ColumnCityID, mCityID, value);
			}
		}
		private Int32 mCityID;

		/// <summary>Gets or sets the CitySection ID value.</summary>
		//[Column("CitySectionID", TypeName="int")]
		public Int32 CitySectionID
		{
			get { return mCitySectionID; }
			set
			{
				mCitySectionID = ChangedNames.Add(ColumnCitySectionID, mCitySectionID, value);
			}
		}
		private Int32 mCitySectionID;

		/// <summary>Gets or sets the Street value.</summary>
		//[Column("Street", TypeName="nvarchar(45)")]
		public String Street
		{
			get { return mStreet; }
			set
			{
				value = NetString.InitString(value);
				mStreet = ChangedNames.Add(ColumnStreet, mStreet, value);
			}
		}
		private String mStreet;

		/// <summary>Gets or sets the PostalCode value.</summary>
		//[Column("PostalCode", TypeName="nvarchar(10)")]
		public string PostalCode
		{
			get { return mPostalCode; }
			set
			{
				value = NetString.InitString(value);
				mPostalCode = ChangedNames.Add(ColumnPostalCode, mPostalCode, value);
				CreateCityStateZip();
			}
		}
		private string mPostalCode;

		/// <summary>Gets or sets the CodeTypeID value.</summary>
		//[Required]
		//[Column("CodeTypeID", TypeName="int")]
		public Int32 CodeTypeID
		{
			get { return mCodeTypeID; }
			set
			{
				mCodeTypeID = ChangedNames.Add(PropertyCodeTypeID, mCodeTypeID, value);
			}
		}
		private Int32 mCodeTypeID;
		#endregion

		#region Calculated and Data Join Properties.

		/// <summary>Gets the calculated CityStateZip calculated value.</summary>
		public string CityStateZip
		{
			get { return mCityStateZip; }
			set { mCityStateZip = NetString.InitString(value); }
		}
		private string mCityStateZip;

		/// <summary>Gets or sets the join RegionName value.</summary>
		public string RegionName
		{
			get { return mRegionName; }
			set
			{
				mRegionName = ChangedNames.Add(PropertyRegionName, mRegionName, value);
			}
		}
		private string mRegionName;

		/// <summary>Gets or sets the join ProvinceName value.</summary>
		public string ProvinceName
		{
			get { return mProvinceName; }
			set
			{
				mProvinceName = ChangedNames.Add(PropertyProvinceName, mProvinceName, value);
				CreateCityStateZip();
			}
		}
		private string mProvinceName;

		/// <summary>Gets or sets the join CityName value.</summary>
		public string CityName
		{
			get { return mCityName; }
			set
			{
				mCityName = ChangedNames.Add(PropertyCityName, mCityName, value);
				CreateCityStateZip();
			}
		}
		private string mCityName;

		/// <summary>Gets or sets the join CitySectionName value.</summary>
		public string CitySectionName
		{
			get { return mCitySectionName; }
			set
			{
				mCitySectionName = ChangedNames.Add(PropertyCitySectionName, mCitySectionName, value);
			}
		}
		private string mCitySectionName;

		/// <summary>Gets or sets the join TypeDescription value.</summary>
		public string TypeDescription
		{
			get { return mTypeDescription; }
			set
			{
				mTypeDescription = ChangedNames.Add("TypeDescription", mTypeDescription, value);
			}
		}
		private string mTypeDescription;
		#endregion

		#region Class Properties

		/// <summary>Gets a reference to the ChangedNames list.</summary>
		public ChangedNames ChangedNames { get; private set; }
		#endregion

		#region Class Data

		/// <summary>The table name value.</summary>
		public static string TableName = "Address";

		/// <summary>The ID value.</summary>
		public static string ColumnID = "ID";

		/// <summary>The RegionID value.</summary>
		public static string ColumnRegionID = "RegionID";

		/// <summary>The ProvinceID value.</summary>
		public static string ColumnProvinceID = "ProvinceID";

		/// <summary>The CityID value.</summary>
		public static string ColumnCityID = "CityID";

		/// <summary>The CitySectionID value.</summary>
		public static string ColumnCitySectionID = "CitySectionID";

		/// <summary>The Street value.</summary>
		public static string ColumnStreet = "Street";

		/// <summary>The PostalCode value.</summary>
		public static string ColumnPostalCode = "PostalCode";

		/// <summary>The CodeTypeID value.</summary>
		public static string ColumnCodeTypeID = "CodeType_Id";

		/// <summary>The Code Type ID property name.</summary>
		public static string PropertyCodeTypeID = "CodeTypeID";

		#region Join Class Data

		// These values were added to the base code.
		/// <summary>The join RegionName property value.</summary>
		public static string PropertyRegionName = "RegionName";

		/// <summary>The join ProvinceName property value.</summary>
		public static string PropertyProvinceName = "ProvinceName";

		/// <summary>The join CityName property value.</summary>
		public static string PropertyCityName = "CityName";

		/// <summary>The join CitySectionName property value.</summary>
		public static string PropertyCitySectionName = "CitySectionName";

		/// <summary>The Combined address calculated column name.</summary>
		public static string ColumnCityStateZip = "CityStateZip";
		#endregion

		/// <summary>The Street maximum length.</summary>
		public static int LengthStreet = 45;

		/// <summary>The City maximum length.</summary>
		public static int LengthCity = 45;

		/// <summary>The Province_Code maximum length.</summary>
		public static int LengthProvinceCode = 3;

		/// <summary>The PostalCode maximum length.</summary>
		public static int LengthPostalCode = 10;
		#endregion
	}
}
