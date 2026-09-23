// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Replacement.cs

namespace LJCGenTextXAL5
{
  // Represents a GenText replacement item.
  /// <include file='Doc/Replacement5.xml'
  ///  path='items/Replacement/*'/>
  public class Replacement : IComparable<Replacement>
  {
    #region Data Properties

    // Gets or sets the replacement name.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Name/*'/>
    public string Name { get; set; } = null!;

    // Gets or sets the replacement value.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Value/*'/>
    public string Value { get; set; } = null!;
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public Replacement()
    {
    }

    // Initializes the Replacement object with the supplied values.
    /// <include file='Doc/Replacement5.xml'
    ///  path='items/ReplacementC/*'/>
    public Replacement(string name, string value)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public Replacement? Clone()
    {
      Replacement? retValue = MemberwiseClone() as Replacement;
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
    public int CompareTo(Replacement? other)
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
  }
}
