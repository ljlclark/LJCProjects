// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocGenAssembly.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  // The DocGenAssembly table Data Record. 
  /// <include path='items/DocGenAssembly/*' file='Doc/DocGenAssembly.xml'/>
  public class DocGenAssembly : IComparable<DocGenAssembly>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenAssembly()
    {
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return mName;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenAssembly Clone()
    {
      DocGenAssembly retValue = MemberwiseClone() as DocGenAssembly;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocGenAssembly other)
    {
      int retValue;

      if (null == other)
      {
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
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the Description value.</summary>
    public String Description
    {
      get { return mDescription; }
      set
      {
        mDescription = NetString.InitString(value);
      }
    }
    private String mDescription;

    /// <summary>Gets or sets the FileSpec value.</summary>
    public String FileSpec
    {
      get { return mFileSpec; }
      set
      {
        mFileSpec = NetString.InitString(value);
      }
    }
    private String mFileSpec;

    /// <summary>Gets or sets the MainImage value.</summary>
    public String MainImage
    {
      get { return mMainImage; }
      set
      {
        mMainImage = NetString.InitString(value);
      }
    }
    private String mMainImage;

    /// <summary>Gets or sets the Name value.</summary>
    public String Name
    {
      get { return mName; }
      set
      {
        mName = NetString.InitString(value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Sequence value.</summary>
    public int Sequence { get; set; }
    #endregion

    #region Calculated and Join Data Properties

    ///// <summary>Gets or sets the join TypeName value.</summary>
    //public String TypeName
    //{
    //	get { return mTypeName; }
    //	set
    //	{
    //		// Change next line to "Property" constant if property was renamed.
    //		mTypeName = ChangedNames.Add("TypeName", mTypeName, value);
    //	}
    //}
    //private String mTypeName;
    #endregion

    #region Class Properties

    #endregion

    #region Class Data

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The FileSpec column name.</summary>
    public static string ColumnFileSpec = "FileSpec";

    /// <summary>The MainImage column name.</summary>
    public static string ColumnMainImage = "MainImage";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The table name.</summary>
    public static string TableName = "DocGenAssembly";

    #region Join Class Data

    ///// <summary>The join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;

    /// <summary>The FileSpec maximum length.</summary>
    public static int LengthFileSpec = 160;

    /// <summary>The MainImage maximum length.</summary>
    public static int LengthMainImage = 160;
    #endregion
  }

  /// <summary>Sort and search on Sequence.</summary>
  public class DocAssemblySequenceComparer : IComparer<DocGenAssembly>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocGenAssembly x, DocGenAssembly y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Case sensitive.
        retValue = x.Sequence.CompareTo(y.Sequence);

        // Not case sensitive.
        //retValue = string.Compare(x.PropertyName, y.PropertyName, true);
      }
      return retValue;
    }
  }
}
