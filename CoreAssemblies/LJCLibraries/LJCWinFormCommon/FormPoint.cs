// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FormPoint.cs
using System;
//using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormCommon
{
  /// <summary>Screen Point Functions</summary>
  public class FormPoint
  {
    /// <summary>Adjust location if form is outside screen.</summary>
    public static Point AdjustedLocation(Form form, Point location)
    {
      var retLocation = location;

      var screen = Screen.PrimaryScreen.WorkingArea;

      // Adjust Left
      var formRight = retLocation.X + form.Width;
      if (formRight > screen.Width)
      {
        var adjustX = formRight - screen.Width;
        if (retLocation.X - adjustX >= 0)
        {
          retLocation.X -= adjustX;
        }
      }

      // Adjust Top
      var formBottom = retLocation.Y + form.Height;
      if (formBottom > screen.Height)
      {
        var adjustY = formBottom - screen.Height;
        if (retLocation.Y - adjustY >= 0)
        {
          retLocation.Y -= adjustY;
        }
      }
      return retLocation;
    }

    // Gets the Grid target Dialog screen position.
    /// <include path='items/GetDialogScreenPoint/*' file='Doc/FormCommon.xml'/>
    // ********************
    public static Point DialogScreenPoint(DataGridView grid)
    {
      Rectangle rectangle = ScreenRectangle(grid);
      Point gridPoint = new Point((rectangle.X + rectangle.Width) / 8
        , (rectangle.Y + rectangle.Height) / 8);
      var retValue = grid.Parent.PointToScreen(gridPoint);
      return retValue;
    }

    // Converts the Control point to Screen point.
    /// <include path='items/GetScreenPoint/*' file='Doc/FormCommon.xml'/>
    // ********************
    public static Point ScreenPoint(Control control, int x, int y)
    {
      Control parent = control.Parent;
      Point controlPoint = new Point(x, y);
      Point retValue = parent.PointToScreen(controlPoint);
      return retValue;
    }

    // Gets the Control screen rectangle.
    /// <include path='items/GetScreenRectangle/*' file='Doc/FormCommon.xml'/>
    // ********************
    public static Rectangle ScreenRectangle(Control control)
    {
      Point topLeft = ScreenPoint(control, control.Left, control.Top);
      Point bottomRight = ScreenPoint(control, control.Left + control.Right
        , control.Top + control.Bottom);
      Rectangle retValue = new Rectangle(topLeft.X, topLeft.Y
        , bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y - control.Top);
      return retValue;
    }

    // Get the control target menu screen position.
    /// <include path='items/GetMenuScreenPoint/*' file='Doc/FormCommon.xml'/>
    // ********************
    public static Point MenuScreenPoint(Control control
      , Point mousePosition)
    {
      Point retValue = mousePosition;
      Rectangle rectangle = ScreenRectangle(control);
      if (!rectangle.Contains(mousePosition))
      {
        Point point = new Point((control.Left + control.Right) / 4
          , (control.Top + control.Bottom) / 4);
        retValue = ScreenPoint(control, point.X, point.Y);
      }
      return retValue;
    }
  }
}
