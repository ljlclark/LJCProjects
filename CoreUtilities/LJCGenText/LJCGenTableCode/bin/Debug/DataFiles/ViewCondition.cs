// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewCondition.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ViewCondition table Data Object.</summary>
  public class ViewCondition : IComparable<ViewCondition>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewCondition()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewCondition(ViewCondition item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewCondition Clone()
    {
      var retValue = MemberwiseClone() as ViewCondition;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ViewCondition other)
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

    /// <summary>Gets or sets the ViewConditionSetID value.</summary>
    //[Column("ViewConditionSetID", TypeName="_DBType_")]
    public Int32 ViewConditionSetID
    {
      get { return mViewConditionSetID; }
      set
      {
        mViewConditionSetID = ChangedNames.Add(ColumnViewConditionSetID, mViewConditionSetID, value);
      }
    }
    private Int32 mViewConditionSetID;

    /// <summary>Gets or sets the FirstValue value.</summary>
    //[Column("FirstValue", TypeName="_DBType_(60")]
    public String FirstValue
    {
      get { return mFirstValue; }
      set
      {
        value = NetString.InitString(value);
        mFirstValue = ChangedNames.Add(ColumnFirstValue, mFirstValue, value);
      }
    }
    private String mFirstValue;

    /// <summary>Gets or sets the SecondValue value.</summary>
    //[Column("SecondValue", TypeName="_DBType_(60")]
    public String SecondValue
    {
      get { return mSecondValue; }
      set
      {
        value = NetString.InitString(value);
        mSecondValue = ChangedNames.Add(ColumnSecondValue, mSecondValue, value);
      }
    }
    private String mSecondValue;

    /// <summary>Gets or sets the ComparisonOperator value.</summary>
    //[Column("ComparisonOperator", TypeName="_DBType_(10")]
    public String ComparisonOperator
    {
      get { return mComparisonOperator; }
      set
      {
        value = NetString.InitString(value);
        mComparisonOperator = ChangedNames.Add(ColumnComparisonOperator, mComparisonOperator, value);
      }
    }
    private String mComparisonOperator;
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
    public static string TableName = "ViewCondition";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ViewConditionSetID column name.</summary>
    public static string ColumnViewConditionSetID = "ViewConditionSetID";

    /// <summary>The FirstValue column name.</summary>
    public static string ColumnFirstValue = "FirstValue";

    /// <summary>The SecondValue column name.</summary>
    public static string ColumnSecondValue = "SecondValue";

    /// <summary>The ComparisonOperator column name.</summary>
    public static string ColumnComparisonOperator = "ComparisonOperator";

    /// <summary>The FirstValue maximum length.</summary>
    public static int LengthFirstValue = 60;

    /// <summary>The SecondValue maximum length.</summary>
    public static int LengthSecondValue = 60;

    /// <summary>The ComparisonOperator maximum length.</summary>
    public static int LengthComparisonOperator = 10;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ViewConditionUniqueComparer : IComparer<ViewCondition>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ViewCondition x, ViewCondition y)
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
