// Copyright (c) Lester J Clark 2017-2019 - All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCRegionDAL
{
  /// <summary>The CitySection table Data Record.</summary>
  public class CitySection : IComparable<CitySection>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CitySection()
    {
      ChangedNames = new ChangedNames();
    }
    #endregion

    #region Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CitySection Clone()
    {
      CitySection retValue = MemberwiseClone() as CitySection;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(CitySection other)
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
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return mName;
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

    /// <summary>Gets or sets the CityID value.</summary>
    //[Required]
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

    /// <summary>Gets or sets the ZoneType value.</summary>
    //[Column("ZoneType", TypeName="varchar(25)")]
    public String ZoneType
    {
      get { return mZoneType; }
      set
      {
        value = NetString.InitString(value);
        mZoneType = ChangedNames.Add(ColumnZoneType, mZoneType, value);
      }
    }
    private String mZoneType;

    /// <summary>Gets or sets the Contact value.</summary>
    //[Column("Contact", TypeName="varchar(60)")]
    public String Contact
    {
      get { return mContact; }
      set
      {
        value = NetString.InitString(value);
        mContact = ChangedNames.Add(ColumnContact, mContact, value);
      }
    }
    private String mContact;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name value.</summary>
    public static string TableName = "CitySection";

    /// <summary>The ID value.</summary>
    public static string ColumnID = "ID";

    /// <summary>The CityID value.</summary>
    public static string ColumnCityID = "CityID";

    /// <summary>The Name value.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description value.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The ZoneType column name.</summary>
    public static string ColumnZoneType = "ZoneType";

    /// <summary>The Contact column name.</summary>
    public static string ColumnContact = "Contact";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The ZoneType maximum length.</summary>
    public static int LengthZoneType = 25;

    /// <summary>The Contact maximum length.</summary>
    public static int LengthContact = 60;
    #endregion
  }
}
