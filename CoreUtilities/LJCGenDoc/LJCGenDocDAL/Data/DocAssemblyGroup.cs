// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyGroup.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCGenDocDAL
{
  /// <summary>The DocAssemblyGroup table Data Object.</summary>
  public class DocAssemblyGroup : IComparable<DocAssemblyGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblyGroup()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblyGroup(DocAssemblyGroup item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssemblyGroup Clone()
    {
      var retValue = MemberwiseClone() as DocAssemblyGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DocAssemblyGroup other)
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
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}"
      return $"{mSequence}){mHeading}";
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

    /// <summary>Gets or sets the Heading value.</summary>
    //[Required]
    //[Column("Name", TypeName="nvarchar(60")]
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

    /// <summary>Gets or sets the Heading value.</summary>
    //[Required]
    //[Column("Heading", TypeName="nvarchar(100")]
    public String Heading
    {
      get { return mHeading; }
      set
      {
        value = NetString.InitString(value);
        mHeading = ChangedNames.Add(ColumnHeading, mHeading, value);
      }
    }
    private String mHeading;

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

    /// <summary>Gets or sets the ActiveFlag value.</summary>
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

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DocAssemblyGroup";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Heading column name.</summary>
    public static string ColumnHeading = "Heading";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ActiveFlag column name.</summary>
    public static string ColumnActiveFlag = "ActiveFlag";

    /// <summary>The Heading maximum length.</summary>
    public static int LengthHeading = 100;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocAssemblyGroupUniqueComparer : IComparer<DocAssemblyGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DocAssemblyGroup x, DocAssemblyGroup y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (-2 == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(x.Name, y.Name, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
