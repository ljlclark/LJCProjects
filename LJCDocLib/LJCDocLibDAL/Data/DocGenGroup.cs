// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocGenGroups.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>The DocGenGroup table Data Record.</summary>
  public class DocGenGroup : IComparable<DocGenGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenGroup()
    {
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return mDescription;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenGroup Clone()
    {
      DocGenGroup retValue = MemberwiseClone() as DocGenGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocGenGroup other)
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

    /// <summary>Gets or sets the DocGenAssembly collection.</summary>
    public DocGenAssemblies DocGenAssemblies { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The table name.</summary>
    public static string TableName = "DocGenGroup";

    #region Join Class Data

    ///// <summary>The join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;
    #endregion
  }

  /// <summary>Sort and search on Sequence.</summary>
  public class DocGenGroupSequenceComparer : IComparer<DocGenGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocGenGroup x, DocGenGroup y)
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
