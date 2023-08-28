// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Equipment.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Equipment table Data Object.</summary>
  public class Equipment : IComparable<Equipment>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Equipment()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Equipment(Equipment item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Equipment Clone()
    {
      var retValue = MemberwiseClone() as Equipment;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Equipment other)
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

    /// <summary>Gets or sets the UnitID value.</summary>
    //[Column("UnitID", TypeName="_DBType_")]
    public Int32 UnitID
    {
      get { return mUnitID; }
      set
      {
        mUnitID = ChangedNames.Add(ColumnUnitID, mUnitID, value);
      }
    }
    private Int32 mUnitID;

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

    /// <summary>Gets or sets the Make value.</summary>
    //[Column("Make", TypeName="_DBType_(25")]
    public String Make
    {
      get { return mMake; }
      set
      {
        value = NetString.InitString(value);
        mMake = ChangedNames.Add(ColumnMake, mMake, value);
      }
    }
    private String mMake;

    /// <summary>Gets or sets the Model value.</summary>
    //[Column("Model", TypeName="_DBType_(25")]
    public String Model
    {
      get { return mModel; }
      set
      {
        value = NetString.InitString(value);
        mModel = ChangedNames.Add(ColumnModel, mModel, value);
      }
    }
    private String mModel;

    /// <summary>Gets or sets the SerialNumber value.</summary>
    //[Column("SerialNumber", TypeName="_DBType_(25")]
    public String SerialNumber
    {
      get { return mSerialNumber; }
      set
      {
        value = NetString.InitString(value);
        mSerialNumber = ChangedNames.Add(ColumnSerialNumber, mSerialNumber, value);
      }
    }
    private String mSerialNumber;
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
    public static string TableName = "Equipment";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The UnitID column name.</summary>
    public static string ColumnUnitID = "UnitID";

    /// <summary>The Code column name.</summary>
    public static string ColumnCode = "Code";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The CodeTypeID column name.</summary>
    public static string ColumnCodeTypeID = "CodeTypeID";

    /// <summary>The Make column name.</summary>
    public static string ColumnMake = "Make";

    /// <summary>The Model column name.</summary>
    public static string ColumnModel = "Model";

    /// <summary>The SerialNumber column name.</summary>
    public static string ColumnSerialNumber = "SerialNumber";

    /// <summary>The Code maximum length.</summary>
    public static int LengthCode = 25;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;

    /// <summary>The Make maximum length.</summary>
    public static int LengthMake = 25;

    /// <summary>The Model maximum length.</summary>
    public static int LengthModel = 25;

    /// <summary>The SerialNumber maximum length.</summary>
    public static int LengthSerialNumber = 25;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class EquipmentUniqueComparer : IComparer<Equipment>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Equipment x, Equipment y)
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
