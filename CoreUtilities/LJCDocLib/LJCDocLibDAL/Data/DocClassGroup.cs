// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocClassGroup.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>The DocClassGroup table Data Object.</summary>
  public class DocClassGroup : IComparable<DocClassGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClassGroup()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClassGroup(DocClassGroup item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClassGroup Clone()
    {
      var retValue = MemberwiseClone() as DocClassGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocClassGroup other)
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
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}"
      return $"{mSequence}){mDocAssemblyID}";
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="smallint")]
    public Int16 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int16 mID;

    /// <summary>Gets or sets the DocAssemblyID value.</summary>
    //[Required]
    //[Column("DocAssemblyID", TypeName="smallint")]
    public Int16 DocAssemblyID
    {
      get { return mDocAssemblyID; }
      set
      {
        mDocAssemblyID = ChangedNames.Add(ColumnDocAssemblyID, mDocAssemblyID, value);
      }
    }
    private Int16 mDocAssemblyID;

    /// <summary>Gets or sets the DocClassGroupHeadingID value.</summary>
    //[Column("DocClassGroupHeadingID", TypeName="smallint")]
    public Int16 DocClassGroupHeadingID
    {
      get { return mDocClassGroupHeadingID; }
      set
      {
        mDocClassGroupHeadingID = ChangedNames.Add(ColumnDocClassGroupHeadingID, mDocClassGroupHeadingID, value);
      }
    }
    private Int16 mDocClassGroupHeadingID;

    /// <summary>Gets or sets the HeadingName value.</summary>
    //[Required]
    //[Column("HeadingName", TypeName="nvarchar(60)")]
    public string HeadingName
    {
      get { return mHeadingName; }
      set
      {
        mHeadingName = ChangedNames.Add(ColumnHeadingName, mHeadingName, value);
      }
    }
    private string mHeadingName;

    /// <summary>Gets or sets the HeadingTextCustom value.</summary>
    //[Column("HeaddingTextCustom", TypeName="smallint")]
    public string HeadingTextCustom
    {
      get { return mHeadingTextCustom; }
      set
      {
        mHeadingTextCustom = ChangedNames.Add(ColumnHeadingTextCustom, mHeadingTextCustom, value);
      }
    }
    private string mHeadingTextCustom;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Required]
    //[Column("Sequence", TypeName="smallint")]
    public Int16 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int16 mSequence;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Required]
    //[Column("ActiveFlag", TypeName="bit")]
    public bool ActiveFlag
    {
      get { return mActiveFlag; }
      set
      {
        mActiveFlag = ChangedNames.Add(ColumnActiveFlag, mActiveFlag, value);
      }
    }
    private bool mActiveFlag;
    #endregion

    #region Calculated and Join Data Properties

    /// <summary>Gets or sets the Join Heading value.</summary>
    public string Heading { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DocClassGroup";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocAssemblyID column name.</summary>
    public static string ColumnDocAssemblyID = "DocAssemblyID";

    /// <summary>The DocClassGroupHeadingID column name.</summary>
    public static string ColumnDocClassGroupHeadingID = "DocClassGroupHeadingID";

    /// <summary>The HeadingName column name.</summary>
    public static string ColumnHeadingName = "HeadingName";

    /// <summary>The HeadingTextCustom column name.</summary>
    public static string ColumnHeadingTextCustom = "HeadingTextCustom";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ActiveFlag column name.</summary>
    public static string ColumnActiveFlag = "ActiveFlag";
    #endregion

    #region Calculated and Join Class Data

    /// <summary>The Join Heading column name.</summary>
    public static string ColumnJoinHeading = "Heading";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocClassGroupUniqueComparer : IComparer<DocClassGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocClassGroup x, DocClassGroup y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Case sensitive.
        retValue = x.DocAssemblyID.CompareTo(y.DocAssemblyID);
        if (0 == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(x.HeadingName, y.HeadingName, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
