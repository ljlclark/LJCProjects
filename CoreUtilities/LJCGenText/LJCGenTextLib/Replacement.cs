// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Replacement.cs
using System;

namespace LJCGenTextLib
{
  // Represents a TextGen replacement item.
  /// <include path='items/Replacement/*' file='Doc/Replacement.xml'/>
  public class Replacement : IComparable<Replacement>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Replacement()
    {
    }

    // Initializes the Replacement object with the supplied values.
    /// <include path='items/ReplacementC/*' file='Doc/Replacement.xml'/>
    public Replacement(string name, string value)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Public Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Replacement Clone()
    {
      Replacement retValue = MemberwiseClone() as Replacement;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(Replacement other)
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
        retValue = Name.CompareTo(other.Name);
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the replacement name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the replacement value.</summary>
    public string Value { get; set; }
    #endregion
  }
}
