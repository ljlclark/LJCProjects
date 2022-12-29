// Copyright (c) Lester J. Clark 2017-2019- All Rights Reserved
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCRegionDAL
{
  /// <summary>The Region table Data Record.</summary>
  public class RegionData : IComparable<RegionData>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public RegionData()
    {
      ChangedNames = new ChangedNames();
    }
    #endregion

    #region Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public RegionData Clone()
    {
      RegionData retValue = MemberwiseClone() as RegionData;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(RegionData other)
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
      return $"{Number} - {Name}";
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the RegionID value.</summary>
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

    /// <summary>Gets or sets the Number value.</summary>
    //[Required]
    //[Column("Number", TypeName="nvarchar(5)")]
    public String Number
    {
      get { return mNumber; }
      set
      {
        value = NetString.InitString(value);
        mNumber = ChangedNames.Add(ColumnNumber, mNumber, value);
      }
    }
    private String mNumber;

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
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name value.</summary>
    public static string TableName = "Region";

    /// <summary>The RegionID value.</summary>
    public static string ColumnID = "ID";

    /// <summary>The Number value.</summary>
    public static string ColumnNumber = "Number";

    /// <summary>The Name value.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description value.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Number maximum length.</summary>
    public static int LengthNumber = 5;

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;
    #endregion
  }
}
