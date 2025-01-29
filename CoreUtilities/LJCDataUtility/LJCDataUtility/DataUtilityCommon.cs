// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityCommon.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace LJCDataUtility
{
  // Common DataUtility Methods.
  internal class DataUtilityCommon
  {
    #region Functions

    // Show the info data.
    internal static ControlValue ShowInfo(string contents, string text
      , ControlValue controlValue = null)
    {
      ControlValue retValue = controlValue;

      Point? location = null;
      if (retValue != null)
      {
        location = new Point(retValue.Left, retValue.Top);
      }
      var info = new InfoWindow(text, contents, location);
      if (retValue != null)
      {
        info.Height = retValue.Height;
        info.Width = retValue.Width;
      }
      info.ShowDialog();
      retValue = new ControlValue()
      {
        ControlName = "AddProc",
        Height = info.Height,
        Left = info.Left,
        Top = info.Top,
        Width = info.Width
      };
      return retValue;
    }
    #endregion
  }
}
