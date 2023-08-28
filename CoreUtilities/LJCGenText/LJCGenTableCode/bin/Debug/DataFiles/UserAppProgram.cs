// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// UserAppProgram.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The UserAppProgram table Data Object.</summary>
  public class UserAppProgram : IComparable<UserAppProgram>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UserAppProgram()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UserAppProgram(UserAppProgram item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UserAppProgram Clone()
    {
      var retValue = MemberwiseClone() as UserAppProgram;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(UserAppProgram other)
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

    /// <summary>Gets or sets the AppManagerUser_Id value.</summary>
    //[Column("AppManagerUser_Id", TypeName="_DBType_")]
    public Int32 AppManagerUser_Id
    {
      get { return mAppManagerUser_Id; }
      set
      {
        mAppManagerUser_Id = ChangedNames.Add(ColumnAppManagerUser_Id, mAppManagerUser_Id, value);
      }
    }
    private Int32 mAppManagerUser_Id;

    /// <summary>Gets or sets the AppProgram_Id value.</summary>
    //[Column("AppProgram_Id", TypeName="_DBType_")]
    public Int32 AppProgram_Id
    {
      get { return mAppProgram_Id; }
      set
      {
        mAppProgram_Id = ChangedNames.Add(ColumnAppProgram_Id, mAppProgram_Id, value);
      }
    }
    private Int32 mAppProgram_Id;

    /// <summary>Gets or sets the Active value.</summary>
    //[Column("Active", TypeName="_DBType_")]
    public Boolean Active
    {
      get { return mActive; }
      set
      {
        mActive = ChangedNames.Add(ColumnActive, mActive, value);
      }
    }
    private Boolean mActive;
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
    public static string TableName = "UserAppProgram";

    /// <summary>The AppManagerUser_Id column name.</summary>
    public static string ColumnAppManagerUser_Id = "AppManagerUser_Id";

    /// <summary>The AppProgram_Id column name.</summary>
    public static string ColumnAppProgram_Id = "AppProgram_Id";

    /// <summary>The Active column name.</summary>
    public static string ColumnActive = "Active";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class UserAppProgramUniqueComparer : IComparer<UserAppProgram>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(UserAppProgram x, UserAppProgram y)
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
