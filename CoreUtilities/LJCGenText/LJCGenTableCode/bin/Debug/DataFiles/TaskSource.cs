// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TaskSource.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The TaskSource table Data Object.</summary>
  public class TaskSource : IComparable<TaskSource>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskSource()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskSource(TaskSource item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskSource Clone()
    {
      var retValue = MemberwiseClone() as TaskSource;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(TaskSource other)
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

    /// <summary>Gets or sets the StepTaskID value.</summary>
    //[Column("StepTaskID", TypeName="_DBType_")]
    public Int32 StepTaskID
    {
      get { return mStepTaskID; }
      set
      {
        mStepTaskID = ChangedNames.Add(ColumnStepTaskID, mStepTaskID, value);
      }
    }
    private Int32 mStepTaskID;

    /// <summary>Gets or sets the DataSourceID value.</summary>
    //[Column("DataSourceID", TypeName="_DBType_")]
    public Int32 DataSourceID
    {
      get { return mDataSourceID; }
      set
      {
        mDataSourceID = ChangedNames.Add(ColumnDataSourceID, mDataSourceID, value);
      }
    }
    private Int32 mDataSourceID;
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
    public static string TableName = "TaskSource";

    /// <summary>The StepTaskID column name.</summary>
    public static string ColumnStepTaskID = "StepTaskID";

    /// <summary>The DataSourceID column name.</summary>
    public static string ColumnDataSourceID = "DataSourceID";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class TaskSourceUniqueComparer : IComparer<TaskSource>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(TaskSource x, TaskSource y)
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
