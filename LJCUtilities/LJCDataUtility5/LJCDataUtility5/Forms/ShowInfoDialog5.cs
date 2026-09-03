// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ShowInfoDialog5.cs
using LJCControls5;
using LJCDataAccessConfig5;

namespace LJCDataUtility5
{
  // The ShowInfo Dialog.
  /// <include file='Doc/ShowInfoDialog'
  ///  path='members/ShowInfoDialog/*'/>
  public class ShowInfoDialog
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='ShowInfoDialog'
    ///  path='members/Constructor/*'/>
    public ShowInfoDialog()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/ShowInfoDialog'
    ///  path='members/ParamConstructor/*'/>
    public ShowInfoDialog(LJCDataConfig? dataConfig) : this()
    {
      if (dataConfig != null)
      {
        DataConfig = dataConfig;
      }
    }
    #endregion

    #region Methods

    // Show the info dialog.
    /// <include file='Doc/ShowInfoDialog'
    ///  path='members/ShowInfo/*'/>
    public ControlValue ShowInfo(string contents, string text
      , ControlValue? controlValue = null)
    {
      ControlValue? retValue = controlValue;

      Point? location = null;
      if (retValue != null)
      {
        location = new Point(retValue.Left, retValue.Top);
      }
      var infoWindow = new InfoWindow(text, contents, location);
      if (DataConfig != null)
      {
        infoWindow.DataConfig = DataConfig;
      }

      if (retValue != null)
      {
        infoWindow.Height = retValue.Height;
        infoWindow.Width = retValue.Width;
      }
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

    #region Properties

    // Gets or sets the data config object.
    /// <include file='Doc/ShowInfoDialog'
    ///  path='members/DataConfig/*'/>
    public LJCDataConfig DataConfig { get; set; } = null!;
    #endregion
  }
}
