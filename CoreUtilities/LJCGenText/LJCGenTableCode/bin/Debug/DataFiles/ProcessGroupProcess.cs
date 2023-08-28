// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProcessGroupProcess.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ProcessGroupProcess table Data Object.</summary>
  public class ProcessGroupProcess : IComparable<ProcessGroupProcess>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProcessGroupProcess()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProcessGroupProcess(ProcessGroupProcess item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ProcessGroupProcess Clone()
    {
      var retValue = MemberwiseClone() as ProcessGroupProcess;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ProcessGroupProcess other)
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

    /// <summary>Gets or sets the ProcessGroupID value.</summary>
    //[Column("ProcessGroupID", TypeName="_DBType_")]
    public Int32 ProcessGroupID
    {
      get { return mProcessGroupID; }
      set
      {
        mProcessGroupID = ChangedNames.Add(ColumnProcessGroupID, mProcessGroupID, value);
      }
    }
    private Int32 mProcessGroupID;

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
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int32 mSequence;
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
    public static string TableName = "ProcessGroupProcess";

    /// <summary>The ProcessGroupID column name.</summary>
    public static string ColumnProcessGroupID = "ProcessGroupID";

    /// <summary>The DataProcessID column name.</summary>
    public static string ColumnDataProcessID = "DataProcessID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ProcessGroupProcessUniqueComparer : IComparer<ProcessGroupProcess>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ProcessGroupProcess x, ProcessGroupProcess y)
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
