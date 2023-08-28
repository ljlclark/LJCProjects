// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Step.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The Step table Data Object.</summary>
  public class Step : IComparable<Step>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Step()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Step(Step item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Step Clone()
    {
      var retValue = MemberwiseClone() as Step;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Step other)
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

    /// <summary>Gets or sets the StepID value.</summary>
    //[Column("StepID", TypeName="_DBType_")]
    public Int32 StepID
    {
      get { return mStepID; }
      set
      {
        mStepID = ChangedNames.Add(ColumnStepID, mStepID, value);
      }
    }
    private Int32 mStepID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Column("Name", TypeName="_DBType_(60")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName, mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="_DBType_(100")]
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

    /// <summary>Gets or sets the DataProcessID value.</summary>
    //[Column("DataProcessID", TypeName="_DBType_")]
    public Int32 DataProcessID
    {
      get { return mDataProcessID; }
      set
      {
        mDataProcessID = ChangedNames.Add(ColumnDataProcessID, mDataProcessID, value);
      }
    }
    private Int32 mDataProcessID;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Column("Sequence", TypeName="_DBType_")]
    public Int16 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int16 mSequence;

    /// <summary>Gets or sets the StatusID value.</summary>
    //[Column("StatusID", TypeName="_DBType_")]
    public Int16 StatusID
    {
      get { return mStatusID; }
      set
      {
        mStatusID = ChangedNames.Add(ColumnStatusID, mStatusID, value);
      }
    }
    private Int16 mStatusID;
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
    public static string TableName = "Step";

    /// <summary>The StepID column name.</summary>
    public static string ColumnStepID = "StepID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The DataProcessID column name.</summary>
    public static string ColumnDataProcessID = "DataProcessID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The StatusID column name.</summary>
    public static string ColumnStatusID = "StatusID";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class StepUniqueComparer : IComparer<Step>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(Step x, Step y)
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
