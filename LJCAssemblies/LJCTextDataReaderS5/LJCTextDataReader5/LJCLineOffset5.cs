// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LineOffset5.cs

namespace LJCTextDataReader5
{
  // Represents the file character offset for a text line.
  /// <include file='Doc/ProjectLineOffset.xml'
  ///  path='items/LJCLineOffset/*'/>
  public class LJCLineOffset : IComparable<LJCLineOffset>
  {
    #region Data Properties

    // Gets or sets the line number.
    /// <include file='Doc/ProjectLineOffset.xml'
    ///  path='items/Number/*'/>
    public long Number { get; set; }

    // Gets or sets the character offset value.
    /// <include file='Doc/ProjectLineOffset.xml'
    ///  path='items/Offset/*'/>
    public long Offset { get; set; }
    #endregion

    #region Data Methods

    // Provides the default Sort functionality.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
    public int CompareTo(LJCLineOffset? other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = Number.CompareTo(other.Number);

        // Not case sensitive.
        //retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }
    #endregion
  }
}
