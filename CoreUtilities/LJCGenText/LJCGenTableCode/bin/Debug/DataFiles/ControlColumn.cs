// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ControlColumn table Data Object.</summary>
  public class ControlColumn : IComparable<ControlColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn(ControlColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlColumn Clone()
    {
      var retValue = MemberwiseClone() as ControlColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlColumn other)
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

    /// <summary>Gets or sets the ControlTabID value.</summary>
    //[Column("ControlTabID", TypeName="_DBType_")]
    public Int64 ControlTabID
    {
      get { return mControlTabID; }
      set
      {
        mControlTabID = ChangedNames.Add(ColumnControlTabID, mControlTabID, value);
      }
    }
    private Int64 mControlTabID;

    /// <summary>Gets or sets the ColumnIndex value.</summary>
    //[Column("ColumnIndex", TypeName="_DBType_")]
    public Int32 ColumnIndex
    {
      get { return mColumnIndex; }
      set
      {
        mColumnIndex = ChangedNames.Add(ColumnColumnIndex, mColumnIndex, value);
      }
    }
    private Int32 mColumnIndex;

    /// <summary>Gets or sets the LabelsWidth value.</summary>
    //[Column("LabelsWidth", TypeName="_DBType_")]
    public Int32 LabelsWidth
    {
      get { return mLabelsWidth; }
      set
      {
        mLabelsWidth = ChangedNames.Add(ColumnLabelsWidth, mLabelsWidth, value);
      }
    }
    private Int32 mLabelsWidth;

    /// <summary>Gets or sets the ControlsWidth value.</summary>
    //[Column("ControlsWidth", TypeName="_DBType_")]
    public Int32 ControlsWidth
    {
      get { return mControlsWidth; }
      set
      {
        mControlsWidth = ChangedNames.Add(ColumnControlsWidth, mControlsWidth, value);
      }
    }
    private Int32 mControlsWidth;
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
    public static string TableName = "ControlColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ControlTabID column name.</summary>
    public static string ColumnControlTabID = "ControlTabID";

    /// <summary>The ColumnIndex column name.</summary>
    public static string ColumnColumnIndex = "ColumnIndex";

    /// <summary>The LabelsWidth column name.</summary>
    public static string ColumnLabelsWidth = "LabelsWidth";

    /// <summary>The ControlsWidth column name.</summary>
    public static string ColumnControlsWidth = "ControlsWidth";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ControlColumnUniqueComparer : IComparer<ControlColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlColumn x, ControlColumn y)
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
