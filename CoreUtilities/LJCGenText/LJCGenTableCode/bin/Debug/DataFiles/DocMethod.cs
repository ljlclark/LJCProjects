// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethod.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DocMethod table Data Object.</summary>
  public class DocMethod : IComparable<DocMethod>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethod()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethod(DocMethod item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethod Clone()
    {
      var retValue = MemberwiseClone() as DocMethod;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocMethod other)
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
    public Int16 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int16 mID;

    /// <summary>Gets or sets the DocClassID value.</summary>
    //[Column("DocClassID", TypeName="_DBType_")]
    public Int16 DocClassID
    {
      get { return mDocClassID; }
      set
      {
        mDocClassID = ChangedNames.Add(ColumnDocClassID, mDocClassID, value);
      }
    }
    private Int16 mDocClassID;

    /// <summary>Gets or sets the DocMethodGroupID value.</summary>
    //[Column("DocMethodGroupID", TypeName="_DBType_")]
    public Int16 DocMethodGroupID
    {
      get { return mDocMethodGroupID; }
      set
      {
        mDocMethodGroupID = ChangedNames.Add(ColumnDocMethodGroupID, mDocMethodGroupID, value);
      }
    }
    private Int16 mDocMethodGroupID;

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

    /// <summary>Gets or sets the ActiveFlag value.</summary>
    //[Column("ActiveFlag", TypeName="_DBType_")]
    public Boolean ActiveFlag
    {
      get { return mActiveFlag; }
      set
      {
        mActiveFlag = ChangedNames.Add(ColumnActiveFlag, mActiveFlag, value);
      }
    }
    private Boolean mActiveFlag;

    /// <summary>Gets or sets the OverloadName value.</summary>
    //[Column("OverloadName", TypeName="_DBType_(60")]
    public String OverloadName
    {
      get { return mOverloadName; }
      set
      {
        value = NetString.InitString(value);
        mOverloadName = ChangedNames.Add(ColumnOverloadName, mOverloadName, value);
      }
    }
    private String mOverloadName;
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
    public static string TableName = "DocMethod";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocClassID column name.</summary>
    public static string ColumnDocClassID = "DocClassID";

    /// <summary>The DocMethodGroupID column name.</summary>
    public static string ColumnDocMethodGroupID = "DocMethodGroupID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ActiveFlag column name.</summary>
    public static string ColumnActiveFlag = "ActiveFlag";

    /// <summary>The OverloadName column name.</summary>
    public static string ColumnOverloadName = "OverloadName";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The OverloadName maximum length.</summary>
    public static int LengthOverloadName = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocMethodUniqueComparer : IComparer<DocMethod>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocMethod x, DocMethod y)
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
