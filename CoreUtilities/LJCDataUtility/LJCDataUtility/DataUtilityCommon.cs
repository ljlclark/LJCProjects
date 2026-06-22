// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityCommon.cs
using LJCWinFormCommon;
using LJCWinFormControls;
using System.Drawing;

namespace LJCDataUtility
{
  // Common DataUtility Methods.
  internal class DataUtilityCommon
  {
    #region Functions

    // Show the info window.
    internal ControlValue ShowInfo(string contents, string text
      , ControlValue controlValue = null)
    {
      ControlValue retValue = controlValue;

      Point? location = null;
      if (retValue != null)
      {
        location = new Point(retValue.Left, retValue.Top);
      }
      var infoWindow = new InfoWindow(text, contents, location);
      if (retValue != null)
      {
        infoWindow.Height = retValue.Height;
        infoWindow.Width = retValue.Width;
      }
      // *** Begin *** Add
      infoWindow.ShowExecuteButton(true);
      infoWindow.LJCCloseEvent += InfoWindow_LJCCloseEvent;
      // *** End ***
      infoWindow.ShowDialog();
      retValue = new ControlValue()
      {
        ControlName = "AddProc",
        Height = infoWindow.Height,
        Left = infoWindow.Left,
        Top = infoWindow.Top,
        Width = infoWindow.Width
      };
      return retValue;
    }
    #endregion

    private void InfoWindow_LJCCloseEvent(object sender, System.EventArgs e)
    {
      var infoWindow = (InfoWindow)sender;
      if (infoWindow.LJCIsExecute)
      {
        IsExecute = true;
      }
    }

    public bool IsExecute { get; set; }
  }
}
