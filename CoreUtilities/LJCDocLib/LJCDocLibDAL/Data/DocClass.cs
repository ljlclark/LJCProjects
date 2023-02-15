// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocClass.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocClass : IComparable<DocClass>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClass()
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
    public DocClass Clone()
    {
      var retValue = MemberwiseClone() as DocClass;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocClass other)
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

    #region Class Properties

    /// <summary>Gets or sets the DocMethods collection.</summary>
    public DocMethods DocMethods { get; set; }
    #endregion
  }

  /// <summary>Sort and search on Sequence.</summary>
  public class DocClassSequenceComparer : IComparer<DocClass>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocClass x, DocClass y)
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
