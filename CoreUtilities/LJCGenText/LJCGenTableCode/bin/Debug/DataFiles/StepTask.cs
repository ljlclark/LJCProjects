// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// StepTask.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The StepTask table Data Object.</summary>
  public class StepTask : IComparable<StepTask>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public StepTask()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public StepTask(StepTask item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public StepTask Clone()
    {
      var retValue = MemberwiseClone() as StepTask;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(StepTask other)
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

    /// <summary>Gets or sets the TaskTypeID value.</summary>
    //[Column("TaskTypeID", TypeName="_DBType_")]
    public Int16 TaskTypeID
    {
      get { return mTaskTypeID; }
      set
      {
        mTaskTypeID = ChangedNames.Add(ColumnTaskTypeID, mTaskTypeID, value);
      }
    }
    private Int16 mTaskTypeID;

    /// <summary>Gets or sets the ActionItemName value.</summary>
    //[Column("ActionItemName", TypeName="_DBType_(100")]
    public String ActionItemName
    {
      get { return mActionItemName; }
      set
      {
        value = NetString.InitString(value);
        mActionItemName = ChangedNames.Add(ColumnActionItemName, mActionItemName, value);
      }
    }
    private String mActionItemName;

    /// <summary>Gets or sets the TaskStatusID value.</summary>
    //[Column("TaskStatusID", TypeName="_DBType_")]
    public Int16 TaskStatusID
    {
      get { return mTaskStatusID; }
      set
      {
        mTaskStatusID = ChangedNames.Add(ColumnTaskStatusID, mTaskStatusID, value);
      }
    }
    private Int16 mTaskStatusID;
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
    public static string TableName = "StepTask";

    /// <summary>The StepTaskID column name.</summary>
    public static string ColumnStepTaskID = "StepTaskID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The StepID column name.</summary>
    public static string ColumnStepID = "StepID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The TaskTypeID column name.</summary>
    public static string ColumnTaskTypeID = "TaskTypeID";

    /// <summary>The ActionItemName column name.</summary>
    public static string ColumnActionItemName = "ActionItemName";

    /// <summary>The TaskStatusID column name.</summary>
    public static string ColumnTaskStatusID = "TaskStatusID";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The ActionItemName maximum length.</summary>
    public static int LengthActionItemName = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class StepTaskUniqueComparer : IComparer<StepTask>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(StepTask x, StepTask y)
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
