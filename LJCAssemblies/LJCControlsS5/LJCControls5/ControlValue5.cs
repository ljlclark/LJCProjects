// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlValue5.cs

namespace LJCControls5
{
  // Represents a controls position and size.
  /// <include file='Doc/ControlValue.xml'
  ///  path='items/ControlValue/*'/>
  public class ControlValue : IComparable<ControlValue>
  {
    // 
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      return $"{ControlName}";
    }

    #region Methods

    // Provides the default Sort functionality.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/CompareTo/*'/>
    public int CompareTo(ControlValue? other)
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

    // Gets or sets the controlName value.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/ControlName/*'/>
    public string ControlName { get; set; } = null!;

    // Gets or sets the Left value.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/Left/*'/>
    public int Left { get; set; }

    // Gets or sets the Top value.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/Top/*'/>
    public int Top { get; set; }

    // Gets or sets the Width value.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/Width/*'/>
    public int Width { get; set; }

    // Gets or sets the Height value.
    /// <include file='Doc/ControlValue.xml'
    ///  path='items/Height/*'/>
    public int Height { get; set; }
    #endregion
  }
}
