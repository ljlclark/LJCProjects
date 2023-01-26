// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColorSettings.cs
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LJCWinFormControls
{
  /// <summary>The Collection of ColorSetting items.</summary>
  public class ColorSettings : List<ColorSetting>
  {
    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/ColorSettings.xml'/>
    public ColorSetting Add(int lineIndex, int beginIndex, int textLength
      , Color color)
    {
      ColorSetting retValue;

      retValue = new ColorSetting(lineIndex, beginIndex, textLength, color);
      Add(retValue);
      return retValue;
    }
  }
}
