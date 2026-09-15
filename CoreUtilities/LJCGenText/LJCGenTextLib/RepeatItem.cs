// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItem.cs
using System;

namespace LJCGenTextLib
{
  // Represents a TextGen repeate item.
  /// <include file='../../LJCGenDoc/Common/Data.xml'
  ///  path='items/RepeatItem/*'/>
  public class RepeatItem : IComparable<RepeatItem>
  {
    #region Constructors

    //Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public RepeatItem()
    {
      Replacements = new Replacements();
    }
    #endregion

    #region Public Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public RepeatItem Clone()
    {
      RepeatItem retValue = MemberwiseClone() as RepeatItem;
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return Name;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
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

    // Gets or sets the Name value.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Name/*'/>
    public string Name { get; set; }

    // Gets or sets the replacement values.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Replacements/*'/>
    public Replacements Replacements { get; set; }

    // Gets or sets the sub section.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Subsection/*'/>
    public Section Subsection { get; set; }
  }
  #endregion
}
