// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// UnitConversion.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The UnitConversion table Data Object.</summary>
  public class UnitConversion : IComparable<UnitConversion>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UnitConversion()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UnitConversion(UnitConversion item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public UnitConversion Clone()
    {
      var retValue = MemberwiseClone() as UnitConversion;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(UnitConversion other)
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

    /// <summary>Gets or sets the FromUnitMeasureID value.</summary>
    //[Column("FromUnitMeasureID", TypeName="_DBType_")]
    public Int32 FromUnitMeasureID
    {
      get { return mFromUnitMeasureID; }
      set
      {
        mFromUnitMeasureID = ChangedNames.Add(ColumnFromUnitMeasureID, mFromUnitMeasureID, value);
      }
    }
    private Int32 mFromUnitMeasureID;

    /// <summary>Gets or sets the ToUnitMeasureID value.</summary>
    //[Column("ToUnitMeasureID", TypeName="_DBType_")]
    public Int32 ToUnitMeasureID
    {
      get { return mToUnitMeasureID; }
      set
      {
        mToUnitMeasureID = ChangedNames.Add(ColumnToUnitMeasureID, mToUnitMeasureID, value);
      }
    }
    private Int32 mToUnitMeasureID;

    /// <summary>Gets or sets the Expression value.</summary>
    //[Column("Expression", TypeName="_DBType_(25")]
    public String Expression
    {
      get { return mExpression; }
      set
      {
        value = NetString.InitString(value);
        mExpression = ChangedNames.Add(ColumnExpression, mExpression, value);
      }
    }
    private String mExpression;
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
    public static string TableName = "UnitConversion";

    /// <summary>The FromUnitMeasureID column name.</summary>
    public static string ColumnFromUnitMeasureID = "FromUnitMeasureID";

    /// <summary>The ToUnitMeasureID column name.</summary>
    public static string ColumnToUnitMeasureID = "ToUnitMeasureID";

    /// <summary>The Expression column name.</summary>
    public static string ColumnExpression = "Expression";

    /// <summary>The Expression maximum length.</summary>
    public static int LengthExpression = 25;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class UnitConversionUniqueComparer : IComparer<UnitConversion>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(UnitConversion x, UnitConversion y)
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
