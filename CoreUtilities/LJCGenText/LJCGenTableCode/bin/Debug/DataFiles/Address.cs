// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Address.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Address table Data Object.</summary>
  public class Address : IComparable<Address>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Address()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Address(Address item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Address Clone()
    {
      var retValue = MemberwiseClone() as Address;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Address other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}";
      return mDescription;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Column("ID", TypeName="_DBType_")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the RegionID value.</summary>
    //[Column("RegionID", TypeName="_DBType_")]
    public Int32 RegionID
    {
      get { return mRegionID; }
      set
      {
        mRegionID = ChangedNames.Add(ColumnRegionID, mRegionID, value);
      }
    }
    private Int32 mRegionID;

    /// <summary>Gets or sets the ProvinceID value.</summary>
    //[Column("ProvinceID", TypeName="_DBType_")]
    public Int32 ProvinceID
    {
      get { return mProvinceID; }
      set
      {
        mProvinceID = ChangedNames.Add(ColumnProvinceID, mProvinceID, value);
      }
    }
    private Int32 mProvinceID;

    /// <summary>Gets or sets the CityID value.</summary>
    //[Column("CityID", TypeName="_DBType_")]
    public Int32 CityID
    {
      get { return mCityID; }
      set
      {
        mCityID = ChangedNames.Add(ColumnCityID, mCityID, value);
      }
    }
    private Int32 mCityID;

    /// <summary>Gets or sets the CitySectionID value.</summary>
    //[Column("CitySectionID", TypeName="_DBType_")]
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
    //[Column("Street", TypeName="_DBType_(45")]
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
    //[Column("PostalCode", TypeName="_DBType_(10")]
    public String PostalCode
    {
      get { return mPostalCode; }
      set
      {
        value = NetString.InitString(value);
        mPostalCode = ChangedNames.Add(ColumnPostalCode, mPostalCode, value);
      }
    }
    private String mPostalCode;

    /// <summary>Gets or sets the CodeTypeID value.</summary>
    //[Column("CodeTypeID", TypeName="_DBType_")]
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

    ///// <summary>Gets or sets the Join TypeName value.</summary>
    //public string TypeName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "Address";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The RegionID column name.</summary>
    public static string ColumnRegionID = "RegionID";

    /// <summary>The ProvinceID column name.</summary>
    public static string ColumnProvinceID = "ProvinceID";

    /// <summary>The CityID column name.</summary>
    public static string ColumnCityID = "CityID";

    /// <summary>The CitySectionID column name.</summary>
    public static string ColumnCitySectionID = "CitySectionID";

    /// <summary>The Street column name.</summary>
    public static string ColumnStreet = "Street";

    /// <summary>The PostalCode column name.</summary>
    public static string ColumnPostalCode = "PostalCode";

    /// <summary>The CodeTypeID column name.</summary>
    public static string ColumnCodeTypeID = "CodeTypeID";

    /// <summary>The Street maximum length.</summary>
    public static int LengthStreet = 45;

    /// <summary>The PostalCode maximum length.</summary>
    public static int LengthPostalCode = 10;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class AddressUniqueComparer : IComparer<Address>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Address x, Address y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x._ComparerName_, y._ComparerName_);
        if (-2 == retValue)
        {
          // Case sensitive.
          //retValue = x._ComparerName_.CompareTo(y._ComparerName_);

          // Not case sensitive.
          retValue = string.Compare(x._ComparerName_, y._ComparerName_, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
