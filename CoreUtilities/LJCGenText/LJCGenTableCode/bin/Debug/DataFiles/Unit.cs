// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Unit.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Unit table Data Object.</summary>
  public class Unit : IComparable<Unit>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Unit()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Unit(Unit item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Unit Clone()
    {
      var retValue = MemberwiseClone() as Unit;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Unit other)
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

    /// <summary>Gets or sets the FacilityID value.</summary>
    //[Column("FacilityID", TypeName="_DBType_")]
    public Int32 FacilityID
    {
      get { return mFacilityID; }
      set
      {
        mFacilityID = ChangedNames.Add(ColumnFacilityID, mFacilityID, value);
      }
    }
    private Int32 mFacilityID;

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

    /// <summary>Gets or sets the Beds value.</summary>
    //[Column("Beds", TypeName="_DBType_")]
    public Int16 Beds
    {
      get { return mBeds; }
      set
      {
        mBeds = ChangedNames.Add(ColumnBeds, mBeds, value);
      }
    }
    private Int16 mBeds;

    /// <summary>Gets or sets the Baths value.</summary>
    //[Column("Baths", TypeName="_DBType_")]
    public Int16 Baths
    {
      get { return mBaths; }
      set
      {
        mBaths = ChangedNames.Add(ColumnBaths, mBaths, value);
      }
    }
    private Int16 mBaths;

    /// <summary>Gets or sets the Phone value.</summary>
    //[Column("Phone", TypeName="_DBType_(18")]
    public String Phone
    {
      get { return mPhone; }
      set
      {
        value = NetString.InitString(value);
        mPhone = ChangedNames.Add(ColumnPhone, mPhone, value);
      }
    }
    private String mPhone;

    /// <summary>Gets or sets the Extension value.</summary>
    //[Column("Extension", TypeName="_DBType_(4")]
    public String Extension
    {
      get { return mExtension; }
      set
      {
        value = NetString.InitString(value);
        mExtension = ChangedNames.Add(ColumnExtension, mExtension, value);
      }
    }
    private String mExtension;
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
    public static string TableName = "Unit";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The FacilityID column name.</summary>
    public static string ColumnFacilityID = "FacilityID";

    /// <summary>The Code column name.</summary>
    public static string ColumnCode = "Code";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CodeTypeID column name.</summary>
    public static string ColumnCodeTypeID = "CodeTypeID";

    /// <summary>The Beds column name.</summary>
    public static string ColumnBeds = "Beds";

    /// <summary>The Baths column name.</summary>
    public static string ColumnBaths = "Baths";

    /// <summary>The Phone column name.</summary>
    public static string ColumnPhone = "Phone";

    /// <summary>The Extension column name.</summary>
    public static string ColumnExtension = "Extension";

    /// <summary>The Code maximum length.</summary>
    public static int LengthCode = 25;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;

    /// <summary>The Phone maximum length.</summary>
    public static int LengthPhone = 18;

    /// <summary>The Extension maximum length.</summary>
    public static int LengthExtension = 4;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class UnitUniqueComparer : IComparer<Unit>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Unit x, Unit y)
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
