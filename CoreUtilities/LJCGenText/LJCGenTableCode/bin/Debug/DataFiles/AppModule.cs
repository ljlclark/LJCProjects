// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// AppModule.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The AppModule table Data Object.</summary>
  public class AppModule : IComparable<AppModule>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public AppModule()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public AppModule(AppModule item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public AppModule Clone()
    {
      var retValue = MemberwiseClone() as AppModule;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(AppModule other)
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

    /// <summary>Gets or sets the Id value.</summary>
    //[Column("Id", TypeName="_DBType_")]
    public Int32 Id
    {
      get { return mId; }
      set
      {
        mId = ChangedNames.Add(ColumnId, mId, value);
      }
    }
    private Int32 mId;

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

    /// <summary>Gets or sets the TypeName value.</summary>
    //[Column("TypeName", TypeName="_DBType_(60")]
    public String TypeName
    {
      get { return mTypeName; }
      set
      {
        value = NetString.InitString(value);
        mTypeName = ChangedNames.Add(ColumnTypeName, mTypeName, value);
      }
    }
    private String mTypeName;

    /// <summary>Gets or sets the Title value.</summary>
    //[Column("Title", TypeName="_DBType_(60")]
    public String Title
    {
      get { return mTitle; }
      set
      {
        value = NetString.InitString(value);
        mTitle = ChangedNames.Add(ColumnTitle, mTitle, value);
      }
    }
    private String mTitle;
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
    public static string TableName = "AppModule";

    /// <summary>The Id column name.</summary>
    public static string ColumnId = "Id";

    /// <summary>The AppProgram_Id column name.</summary>
    public static string ColumnAppProgram_Id = "AppProgram_Id";

    /// <summary>The TypeName column name.</summary>
    public static string ColumnTypeName = "TypeName";

    /// <summary>The Title column name.</summary>
    public static string ColumnTitle = "Title";

    /// <summary>The TypeName maximum length.</summary>
    public static int LengthTypeName = 60;

    /// <summary>The Title maximum length.</summary>
    public static int LengthTitle = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class AppModuleUniqueComparer : IComparer<AppModule>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(AppModule x, AppModule y)
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
