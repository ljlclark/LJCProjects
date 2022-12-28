// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlCommon.cs
using System;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides common WinForm Controls methods.</summary>
  public class ControlCommon
  {
    // Display the Calendar control to select a date.
    /// <summary>
    /// Display the Calendar control to select a date.
    /// </summary>
    /// <param name="startDate">The initial date.</param>
    /// <returns>The selected date.</returns>
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
  }
}
