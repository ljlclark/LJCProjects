// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlCommon5.cs

namespace LJCControls5
{
  // Provides common WinForm control methods.
  /// <include file='Doc/ControlCommon5.xml'
  ///  path='members/ControlCommon5/*'/>
  public class ControlCommon
  {
    // Use a supplied text and control font.
    /// <include file='Doc/ControlCommon5.xml'
    ///  path='members/AverageCharWidth/*'/>
    public static int AverageCharWidth(Control control, string text)
    {
      int retValue;

      var textWidth = TextUnitWidth(control, text);
      retValue = textWidth / text.Length;
      return retValue;
    }

    // Use text consisting of possible characters (A-Z) or (a-z).
    /// <include file='Doc/ControlCommon.xml'
    ///  path='members/AverageCharWidth1/*'/>
    public static int AverageCharWidth(Control control, int textLength
      , int upperCount = 0)
    {
      int retValue;

      retValue = TextUnitWidth(control, textLength, upperCount) / 26;
      return retValue;
    }

    // The text length in page units.
    /// <include file='Doc/ControlCommon5.xml'
    ///  path='members/TextUnitWidth/*'/>
    public static int TextUnitWidth(Control control, string text)
    {
      int retValue;

      var canvas = control.CreateGraphics();
      SizeF textSize = canvas.MeasureString(text, control.Font);
      retValue = (int)textSize.Width;
      return retValue;
    }

    // Use text consisting of possible characters (A-Z) or (a-z).
    /// <include file='Doc/ControlCommon5.xml'
    ///  path='members/TextUnitWidth1/*'/>
    public static int TextUnitWidth(Control control, int textLength
      , int capsCount = 0)
    {
      int retValue;

      var upperAverage = AverageCharWidth(control
        , "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
      var lowerAverage = AverageCharWidth(control
              , "abcdefghijklmnopqrstuvwxyz");

      retValue = upperAverage * capsCount;
      retValue += lowerAverage * (textLength - capsCount);
      return retValue;
    }
  }
}
