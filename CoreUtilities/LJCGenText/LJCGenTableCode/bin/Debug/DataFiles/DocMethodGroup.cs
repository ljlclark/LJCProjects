// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocMethodGroup.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DocMethodGroup table Data Object.</summary>
  public class DocMethodGroup : IComparable<DocMethodGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethodGroup()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethodGroup(DocMethodGroup item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethodGroup Clone()
    {
      var retValue = MemberwiseClone() as DocMethodGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocMethodGroup other)
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

    /// <summary>Gets or sets the DocMethodGroupHeadingID value.</summary>
    //[Column("DocMethodGroupHeadingID", TypeName="_DBType_")]
    public Int16 DocMethodGroupHeadingID
    {
      get { return mDocMethodGroupHeadingID; }
      set
      {
        mDocMethodGroupHeadingID = ChangedNames.Add(ColumnDocMethodGroupHeadingID, mDocMethodGroupHeadingID, value);
      }
    }
    private Int16 mDocMethodGroupHeadingID;

    /// <summary>Gets or sets the HeadingName value.</summary>
    //[Column("HeadingName", TypeName="_DBType_(60")]
    public String HeadingName
    {
      get { return mHeadingName; }
      set
      {
        value = NetString.InitString(value);
        mHeadingName = ChangedNames.Add(ColumnHeadingName, mHeadingName, value);
      }
    }
    private String mHeadingName;

    /// <summary>Gets or sets the HeadingTextCustom value.</summary>
    //[Column("HeadingTextCustom", TypeName="_DBType_(100")]
    public String HeadingTextCustom
    {
      get { return mHeadingTextCustom; }
      set
      {
        value = NetString.InitString(value);
        mHeadingTextCustom = ChangedNames.Add(ColumnHeadingTextCustom, mHeadingTextCustom, value);
      }
    }
    private String mHeadingTextCustom;

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
    public static string TableName = "DocMethodGroup";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocClassID column name.</summary>
    public static string ColumnDocClassID = "DocClassID";

    /// <summary>The DocMethodGroupHeadingID column name.</summary>
    public static string ColumnDocMethodGroupHeadingID = "DocMethodGroupHeadingID";

    /// <summary>The HeadingName column name.</summary>
    public static string ColumnHeadingName = "HeadingName";

    /// <summary>The HeadingTextCustom column name.</summary>
    public static string ColumnHeadingTextCustom = "HeadingTextCustom";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ActiveFlag column name.</summary>
    public static string ColumnActiveFlag = "ActiveFlag";

    /// <summary>The HeadingName maximum length.</summary>
    public static int LengthHeadingName = 60;

    /// <summary>The HeadingTextCustom maximum length.</summary>
    public static int LengthHeadingTextCustom = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocMethodGroupUniqueComparer : IComparer<DocMethodGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocMethodGroup x, DocMethodGroup y)
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
