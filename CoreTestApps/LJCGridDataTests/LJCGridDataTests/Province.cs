// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Province.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCGridDataTests
{
  /// <summary>The Province table Data Record.</summary>
  public class Province : IComparable<Province>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public Province()
    {
      ChangedNames = new ChangedNames();
    }
    #endregion

    #region Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public Province Clone()
    {
      Province retValue = MemberwiseClone() as Province;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Province other)
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

    /// <summary>Gets or sets the ProvinceID value.</summary>
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

    /// <summary>Gets or sets the RegionID value.</summary>
    //[Required]
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

    /// <summary>Gets or sets the Abbreviation value.</summary>
    //[Column("Abbreviation", TypeName="char(3)")]
    public String Abbreviation
    {
      get { return mAbbreviation; }
      set
      {
        value = NetString.InitString(value);
        mAbbreviation = ChangedNames.Add(ColumnAbbreviation, mAbbreviation, value);
      }
    }
    private String mAbbreviation;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name value.</summary>
    public static string TableName = "Province";

    /// <summary>The ProvinceID value.</summary>
    public static string ColumnID = "ID";

    /// <summary>The RegionID value.</summary>
    public static string ColumnRegionID = "RegionID";

    /// <summary>The Name value.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description value.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Abbreviation value.</summary>
    public static string ColumnAbbreviation = "Abbreviation";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The Abbreviation maximum length.</summary>
    public static int LengthAbbreviation = 3;
    #endregion
  }
}
