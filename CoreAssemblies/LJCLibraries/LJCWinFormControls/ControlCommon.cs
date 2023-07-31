// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlCommon.cs
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides common WinForm Controls methods.</summary>
  public class ControlCommon
  {
    // Use a supplied text and control font.
    /// <include path='items/AverageCharWidth/*' file='Doc/ControlCommon.xml'/>
    public static int AverageCharWidth(Control control, string text)
    {
      int retValue;

      var textWidth = TextUnitWidth(control, text);
      retValue = textWidth / text.Length;
      return retValue;
    }

    // Use text consisting of possible characters (A-Z) or (a-z).
    /// <include path='items/AverageCharWidth1/*' file='Doc/ControlCommon.xml'/>
    public static int AverageCharWidth(Control control, int textLength
      , int upperCount = 0)
    {
      int retValue;

      retValue = TextUnitWidth(control, textLength, upperCount) / 26;
      return retValue;
    }

    // Display the Calendar control to select a date.
    /// <include path='items/GetSelectedDate/*' file='Doc/ControlCommon.xml'/>
    public static string GetSelectedDate(string startDate)
    {
      Calendar calendar;
      string retValue = startDate;

      calendar = new Calendar()
      {
        LJCStartDate = startDate
      };
      if (calendar.ShowDialog() == DialogResult.OK)
      {
        retValue = calendar.LJCSelectedDate.ToString("MM/dd/yyyy");
      }
      return retValue;
    }

    // The text length in page units.
    /// <include path='items/TextUnitWidth/*' file='Doc/ControlCommon.xml'/>
    public static int TextUnitWidth(Control control, string text)
    {
      int retValue;

      var canvas = control.CreateGraphics();
      SizeF textSize = canvas.MeasureString(text, control.Font);
      retValue = (int)textSize.Width;
      return retValue;
    }

    // Use text consisting of possible characters (A-Z) or (a-z).
    /// <include path='items/TextUnitWidth1/*' file='Doc/ControlCommon.xml'/>
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
