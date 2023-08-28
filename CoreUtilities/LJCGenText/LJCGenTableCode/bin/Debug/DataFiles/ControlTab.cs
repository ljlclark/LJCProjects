// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlTab.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ControlTab table Data Object.</summary>
  public class ControlTab : IComparable<ControlTab>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlTab()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlTab(ControlTab item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlTab Clone()
    {
      var retValue = MemberwiseClone() as ControlTab;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlTab other)
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
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the ControlDetailID value.</summary>
    //[Column("ControlDetailID", TypeName="_DBType_")]
    public Int64 ControlDetailID
    {
      get { return mControlDetailID; }
      set
      {
        mControlDetailID = ChangedNames.Add(ColumnControlDetailID, mControlDetailID, value);
      }
    }
    private Int64 mControlDetailID;

    /// <summary>Gets or sets the TabIndex value.</summary>
    //[Column("TabIndex", TypeName="_DBType_")]
    public Int32 TabIndex
    {
      get { return mTabIndex; }
      set
      {
        mTabIndex = ChangedNames.Add(ColumnTabIndex, mTabIndex, value);
      }
    }
    private Int32 mTabIndex;

    /// <summary>Gets or sets the Caption value.</summary>
    //[Column("Caption", TypeName="_DBType_(40")]
    public String Caption
    {
      get { return mCaption; }
      set
      {
        value = NetString.InitString(value);
        mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
      }
    }
    private String mCaption;

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
    public static string TableName = "ControlTab";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ControlDetailID column name.</summary>
    public static string ColumnControlDetailID = "ControlDetailID";

    /// <summary>The TabIndex column name.</summary>
    public static string ColumnTabIndex = "TabIndex";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Caption maximum length.</summary>
    public static int LengthCaption = 40;

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
  public class ControlTabUniqueComparer : IComparer<ControlTab>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlTab x, ControlTab y)
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
