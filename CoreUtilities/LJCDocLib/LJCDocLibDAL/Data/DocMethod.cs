// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocMethod.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocMethod : IComparable<DocMethod>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethod()
    {
    }
    #endregion

    #region Data Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return mDescription;
    }

    // Creates and returns a clone of the object.
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
        retValue = 1;
      }
      else
      {
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
  }

  /// <summary>Sort and search on Sequence.</summary>
  public class DocMethodSequenceComparer : IComparer<DocMethod>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocMethod x, DocMethod y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        // Case sensitive.
        retValue = x.Sequence.CompareTo(y.Sequence);
      }
      return retValue;
    }
  }
}
