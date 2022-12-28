// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ColorSetting.cs
using System;
using System.Drawing;

namespace LJCWinFormControls
{
  /// <summary>The ColorSetting data class.</summary>
  public class ColorSetting
  {
    // Initializes an object instance.
    /// <include path='items/ColorSettingC/*' file='Doc/ColorSetting.xml'/>
    public ColorSetting(int lineIndex, int beginIndex, int textLength
      , Color color)
    {
      LineIndex = lineIndex;
      BeginIndex = beginIndex;
      TextLength = textLength;
      Color = color;
    }

    /// <summary>Gets or sets the line index value.</summary>
    public int LineIndex { get; set; }

    /// <summary>Gets or sets the text begin index.</summary>
    public int BeginIndex { get; set; }

    /// <summary>Gets or sets the text length.</summary>
    public int TextLength { get; set; }

    /// <summary>Gets or sets the text color.</summary>
    public Color Color { get; set; }
  }
}
