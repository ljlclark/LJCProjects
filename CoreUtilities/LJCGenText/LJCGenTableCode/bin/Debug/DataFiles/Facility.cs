// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Facility.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Facility table Data Object.</summary>
  public class Facility : IComparable<Facility>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Facility()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Facility(Facility item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Facility Clone()
    {
      var retValue = MemberwiseClone() as Facility;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Facility other)
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

    /// <summary>Gets or sets the Code value.</summary>
    //[Column("Code", TypeName="_DBType_(25")]
    public String Code
    {
      get { return mCode; }
      set
      {
        value = NetString.InitString(value);
        mCode = ChangedNames.Add(ColumnCode, mCode, value);
      }
    }
    private String mCode;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="_DBType_(60")]
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
    public static string TableName = "Facility";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The Code column name.</summary>
    public static string ColumnCode = "Code";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CodeTypeID column name.</summary>
    public static string ColumnCodeTypeID = "CodeTypeID";

    /// <summary>The Code maximum length.</summary>
    public static int LengthCode = 25;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class FacilityUniqueComparer : IComparer<Facility>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Facility x, Facility y)
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
