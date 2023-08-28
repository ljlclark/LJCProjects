// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TransformMatch.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The TransformMatch table Data Object.</summary>
  public class TransformMatch : IComparable<TransformMatch>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TransformMatch()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TransformMatch(TransformMatch item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TransformMatch Clone()
    {
      var retValue = MemberwiseClone() as TransformMatch;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(TransformMatch other)
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

    /// <summary>Gets or sets the TransformMatchID value.</summary>
    //[Column("TransformMatchID", TypeName="_DBType_")]
    public Int32 TransformMatchID
    {
      get { return mTransformMatchID; }
      set
      {
        mTransformMatchID = ChangedNames.Add(ColumnTransformMatchID, mTransformMatchID, value);
      }
    }
    private Int32 mTransformMatchID;

    /// <summary>Gets or sets the TransformID value.</summary>
    //[Column("TransformID", TypeName="_DBType_")]
    public Int32 TransformID
    {
      get { return mTransformID; }
      set
      {
        mTransformID = ChangedNames.Add(ColumnTransformID, mTransformID, value);
      }
    }
    private Int32 mTransformID;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Column("Sequence", TypeName="_DBType_")]
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int32 mSequence;

    /// <summary>Gets or sets the SourceColumnID value.</summary>
    //[Column("SourceColumnID", TypeName="_DBType_")]
    public Int16 SourceColumnID
    {
      get { return mSourceColumnID; }
      set
      {
        mSourceColumnID = ChangedNames.Add(ColumnSourceColumnID, mSourceColumnID, value);
      }
    }
    private Int16 mSourceColumnID;

    /// <summary>Gets or sets the TargetColumnID value.</summary>
    //[Column("TargetColumnID", TypeName="_DBType_")]
    public Int16 TargetColumnID
    {
      get { return mTargetColumnID; }
      set
      {
        mTargetColumnID = ChangedNames.Add(ColumnTargetColumnID, mTargetColumnID, value);
      }
    }
    private Int16 mTargetColumnID;
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
    public static string TableName = "TransformMatch";

    /// <summary>The TransformMatchID column name.</summary>
    public static string ColumnTransformMatchID = "TransformMatchID";

    /// <summary>The TransformID column name.</summary>
    public static string ColumnTransformID = "TransformID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The SourceColumnID column name.</summary>
    public static string ColumnSourceColumnID = "SourceColumnID";

    /// <summary>The TargetColumnID column name.</summary>
    public static string ColumnTargetColumnID = "TargetColumnID";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class TransformMatchUniqueComparer : IComparer<TransformMatch>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(TransformMatch x, TransformMatch y)
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
