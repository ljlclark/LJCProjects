// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAssembly.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCGenDocDAL
{
  /// <summary>The DocAssembly table Data Object.</summary>
  public class DocAssembly : IComparable<DocAssembly>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssembly()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssembly(DocAssembly item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocAssembly Clone()
    {
      var retValue = MemberwiseClone() as DocAssembly;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DocAssembly other)
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
      return $"{mSequence}){mName}";
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

    /// <summary>Gets or sets the DocAssemblyGroupID value.</summary>
    //[Required]
    //[Column("DocAssemblyGroupID", TypeName="smallint")]
    public Int16 DocAssemblyGroupID
    {
      get { return mDocAssemblyGroupID; }
      set
      {
        mDocAssemblyGroupID = ChangedNames.Add(ColumnDocAssemblyGroupID, mDocAssemblyGroupID, value);
      }
    }
    private Int16 mDocAssemblyGroupID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="nvarchar(60)")]
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
    //[Required]
    //[Column("Description", TypeName="nvarchar(100)")]
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

    /// <summary>Gets or sets the FileSpec value.</summary>
    //[Column("FileSpec", TypeName="nvarchar(60)")]
    public String FileSpec
    {
      get { return mFileSpec; }
      set
      {
        value = NetString.InitString(value);
        mFileSpec = ChangedNames.Add(ColumnFileSpec, mFileSpec, value);
      }
    }
    private String mFileSpec;

    /// <summary>Gets or sets the MainImage value.</summary>
    //[Column("MainImage", TypeName="nvarchar(60)")]
    public String MainImage
    {
      get { return mMainImage; }
      set
      {
        value = NetString.InitString(value);
        mMainImage = ChangedNames.Add(ColumnMainImage, mMainImage, value);
      }
    }
    private String mMainImage;

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
    public static string TableName = "DocAssembly";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocAssemblyGroupID column name.</summary>
    public static string ColumnDocAssemblyGroupID = "DocAssemblyGroupID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The FileSpec column name.</summary>
    public static string ColumnFileSpec = "FileSpec";

    /// <summary>The MainImage column name.</summary>
    public static string ColumnMainImage = "MainImage";

    /// <summary>The ActiveFlag column name.</summary>
    public static string ColumnActiveFlag = "ActiveFlag";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The FileSpec maximum length.</summary>
    public static int LengthFileSpec = 120;

    /// <summary>The MainImage maximum length.</summary>
    public static int LengthMainImage = 60;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocAssemblyUniqueComparer : IComparer<DocAssembly>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DocAssembly x, DocAssembly y)
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
