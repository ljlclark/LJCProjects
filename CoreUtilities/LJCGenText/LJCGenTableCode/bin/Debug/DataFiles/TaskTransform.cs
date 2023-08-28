// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TaskTransform.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The TaskTransform table Data Object.</summary>
  public class TaskTransform : IComparable<TaskTransform>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskTransform()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskTransform(TaskTransform item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TaskTransform Clone()
    {
      var retValue = MemberwiseClone() as TaskTransform;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(TaskTransform other)
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

    /// <summary>Gets or sets the TargetID value.</summary>
    //[Column("TargetID", TypeName="_DBType_")]
    public Int32 TargetID
    {
      get { return mTargetID; }
      set
      {
        mTargetID = ChangedNames.Add(ColumnTargetID, mTargetID, value);
      }
    }
    private Int32 mTargetID;
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
    public static string TableName = "TaskTransform";

    /// <summary>The TransformID column name.</summary>
    public static string ColumnTransformID = "TransformID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The StepTaskID column name.</summary>
    public static string ColumnStepTaskID = "StepTaskID";

    /// <summary>The DataSourceID column name.</summary>
    public static string ColumnDataSourceID = "DataSourceID";

    /// <summary>The TargetID column name.</summary>
    public static string ColumnTargetID = "TargetID";

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
  public class TaskTransformUniqueComparer : IComparer<TaskTransform>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(TaskTransform x, TaskTransform y)
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
