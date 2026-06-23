// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityCommon.cs
using LJCWinFormCommon;
using LJCWinFormControls;
using System.Drawing;

namespace LJCDataUtility
{
  /// <summary>Common DataUtility Methods.</summary>
  public class ShowInfoDialog
  {
    #region Constructor Methods

    /// <summary>Initializes an object instance.</summary>
    public ShowInfoDialog()
    {
      ShowExecuteButton = false;
    }

    /// <summary>Initializes an object instance.</summary>
    public ShowInfoDialog(bool showExecuteButton) : this()
    {
      ShowExecuteButton = showExecuteButton;
    }

    #endregion

    #region Functions

    /// <summary>Show the info window.</summary>
    public ControlValue ShowInfo(string contents, string text
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
      infoWindow.ShowExecuteButton(ShowExecuteButton);
      infoWindow.LJCCloseEvent += InfoWindow_LJCCloseEvent;
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

    /// <summary>Gets or sets the IsExecute flag.</summary>
    public bool IsExecute { get; set; }

    /// <summary>Gets or sets the show "Execute" button flag.</summary>
    public bool ShowExecuteButton { get; set; }
  }
}
