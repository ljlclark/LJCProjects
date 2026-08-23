// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FormPoint5.cs

using System.Drawing;

namespace LJCControls5
{
  // Screen Point Functions
  /// <include file='Doc/FormPoint5.xml'
  ///  path='items/FormPoint/*'/>
  public class FormPoint
  {
    // Adjust location if form is outside screen.
    /// <include file='Doc/FormPoint5.xml'
    ///  path='items/AdjustedLocation/*'/>
    public static Point AdjustedLocation(Form form, Point location)
    {
      var retLocation = location;

      if (Screen.PrimaryScreen != null)
      {
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
      }
      return retLocation;
    }

    // Gets the Grid target Dialog screen position.
    /// <include file='Doc/FormPoint5.xml'
    ///  path='items/DialogScreenPoint/*'/>
    public static Point DialogScreenPoint(DataGridView grid)
    {
      var retPoint = new Point(-1, -1);

      if (grid.Parent != null)
      {
        Rectangle rectangle = ScreenRectangle(grid);
        var gridPoint = new Point((rectangle.X + rectangle.Width) / 8
          , (rectangle.Y + rectangle.Height) / 8);
        retPoint = grid.Parent.PointToScreen(gridPoint);
      }
      return retPoint;
    }

    // Converts the Control point to Screen point.
    /// <include file='Doc/FormPoint5.xml'
    ///  path='items/ScreenPoint/*'/>
    public static Point ScreenPoint(Control control, int x, int y)
    {
      var retPoint = new Point(-1, -1);

      if (control.Parent != null)
      {
        Control parent = control.Parent;
        var controlPoint = new Point(x, y);
        retPoint = parent.PointToScreen(controlPoint);
      }
      return retPoint;
    }

    // Gets the Control screen rectangle.
    /// <include file='Doc/FormPoint5.xml'
    ///  path='items/ScreenRectangle/*'/>
    public static Rectangle ScreenRectangle(Control control)
    {
      var retRectangle = new Rectangle(-1, -1, -1, -1);

      Point topLeft = ScreenPoint(control, control.Left, control.Top);
      Point bottomRight = ScreenPoint(control, control.Left + control.Right
        , control.Top + control.Bottom);
      if (topLeft.X > 0 && topLeft.Y > 0
        && bottomRight.X > 0 && bottomRight.Y > 0)
      {
        retRectangle = new Rectangle(topLeft.X, topLeft.Y
          , bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y - control.Top);
      }
      return retRectangle;
    }

    // Get the control target menu screen position.
    /// <include file='Doc/FormPoint5.xml'
    ///  path='items/MenuScreenPoint/*'/>
    public static Point MenuScreenPoint(Control control
      , Point mousePosition)
    {
      Point retValue = mousePosition;
      Rectangle rectangle = ScreenRectangle(control);
      if (IsRectangle(rectangle)
        && !rectangle.Contains(mousePosition))
      {
        var point = new Point((control.Left + control.Right) / 4
          , (control.Top + control.Bottom) / 4);
        retValue = ScreenPoint(control, point.X, point.Y);
      }
      return retValue;
    }

    public static bool IsPoint(Point point)
    {
      var retValue = false;

      if (point.X > -1
        && point.Y > -10)
      {
        retValue = true;
      }
      return retValue;
    }

    public static bool IsRectangle(Rectangle rectangle)
    {
      var retValue = false;

      if (rectangle.X > -1 && rectangle.Y > -10
        && rectangle.Width > -1 && rectangle.Height > -1)
      {
        retValue = true;
      }
      return retValue;
    }
  }
}
