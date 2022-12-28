// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlValue.cs
using System;

namespace LJCWinFormCommon
{
  // Represents a controls position and size.
  /// <include path='items/ControlValue/*' file='Doc/ControlValue.xml'/>
  public class ControlValue : IComparable<ControlValue>
  {
    #region Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlValue other)
    {
      int retValue;

      if (null == other)
      {
        // This object is larger than the "other" object.
        retValue = 1;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(ControlName, other.ControlName, true);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the controlName value.</summary>
    public string ControlName { get; set; }

    /// <summary>Gets or sets the Left value.</summary>
    public int Left { get; set; }

    /// <summary>Gets or sets the Top value.</summary>
    public int Top { get; set; }

    /// <summary>Gets or sets the Width value.</summary>
    public int Width { get; set; }

    /// <summary>Gets or sets the Height value.</summary>
    public int Height { get; set; }
    #endregion
  }
}
