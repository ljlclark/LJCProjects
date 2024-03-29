// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItem.cs
using System;

namespace LJCGenTextLib
{
  /// <summary>Represents a TextGen repeate item.</summary>
  public class RepeatItem : IComparable<RepeatItem>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public RepeatItem()
    {
      Replacements = new Replacements();
    }
    #endregion

    #region Public Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public RepeatItem Clone()
    {
      RepeatItem retValue = MemberwiseClone() as RepeatItem;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(RepeatItem other)
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

    /// <summary>Gets or sets the Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the replacement values.</summary>
    public Replacements Replacements { get; set; }

    /// <summary>Gets or sets the sub section.</summary>
    public Section Subsection { get; set; }
  }
  #endregion
}
