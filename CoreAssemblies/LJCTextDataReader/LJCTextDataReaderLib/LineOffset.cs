// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LineOffset.cs
using System;

namespace LJCTextDataReaderLib
{
  // Represents the file character offset for a text line.
  /// <include path='items/LineOffset/*' file='Doc/ProjectTextDataReaderLib.xml'/>
  public class LineOffset : IComparable<LineOffset>
  {

    #region Data Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(LineOffset other)
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

    #region Data Properties

    /// <summary>Gets or sets the line number.</summary>
    public long Number { get; set; }

    /// <summary>Gets or sets the character offset value.</summary>
    public long Offset { get; set; }
    #endregion
  }
}
