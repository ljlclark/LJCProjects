// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCRegionDAL
{
  /// <summary>The City table Data Record.</summary>
  public class City : IComparable<City>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public City()
    {
      ChangedNames = new ChangedNames();
    }
    #endregion

    #region Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public City Clone()
    {
      City retValue = MemberwiseClone() as City;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(City other)
    {
      int retValue;

      if (null == other)
      {
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
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return mName;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the CityID value.</summary>
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

    /// <summary>Gets or sets the ProvinceID value.</summary>
    //[Required]
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

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="varchar(60)")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName, mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="varchar(100)")]
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

    /// <summary>Gets or sets the CityFlag value.</summary>
    //[Required]
    //[Column("CityFlag", TypeName="bit")]
    public Boolean? CityFlag
    {
      get { return mCityFlag; }
      set
      {
        mCityFlag = ChangedNames.Add(ColumnCityFlag, mCityFlag, value);
      }
    }
    private Boolean? mCityFlag;

    /// <summary>Gets or sets the ZipCode value.</summary>
    //[Column("ZipCode", TypeName="char(4)")]
    public String ZipCode
    {
      get { return mZipCode; }
      set
      {
        value = NetString.InitString(value);
        mZipCode = ChangedNames.Add(ColumnZipCode, mZipCode, value);
      }
    }
    private String mZipCode;

    /// <summary>Gets or sets the District value.</summary>
    //[Column("District", TypeName="smallint")]
    public Int16 District
    {
      get { return mDistrict; }
      set
      {
        mDistrict = ChangedNames.Add(ColumnDistrict, mDistrict, value);
      }
    }
    private Int16 mDistrict;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name value.</summary>
    public static string TableName = "City";

    /// <summary>The CityID value.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ProvinceID value.</summary>
    public static string ColumnProvinceID = "ProvinceID";

    /// <summary>The Name value.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description value.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CityFlag value.</summary>
    public static string ColumnCityFlag = "CityFlag";

    /// <summary>The ZipCode value.</summary>
    public static string ColumnZipCode = "ZipCode";

    /// <summary>The District value.</summary>
    public static string ColumnDistrict = "District";

    /// <summary>The ParentCityID value.</summary>
    public static string ColumnParentCityID = "ParentCityID";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The ZipCode maximum length.</summary>
    public static int LengthZipCode = 4;
    #endregion
  }
}
