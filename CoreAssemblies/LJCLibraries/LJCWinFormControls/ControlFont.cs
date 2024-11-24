// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlFont.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides Font sizing.</summary>
  public class ControlFont
  {
    /// <summary>Initializes an object instance.</summary>
    /// <param name="control">The ContainerControl object.</param>
    // ********************
    public ControlFont(ContainerControl control)
    {
      Control = control;

      // Set event handlers.
      Control.MouseWheel += Control_MouseWheel;
    }

    // ******************************
    #region Methods
    // ******************************

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var rowHeight = RowHeight();
      var fontFamily = Control.Font.FontFamily;
      var style = Control.Font.Style;
      Control.Font = new Font(fontFamily, FontSize, style);

      SetLabelHeight(Control.Font);
    }

    // Sets the label height for the new font.
    // ********************
    private void SetLabelHeight(Font font)
    {
      foreach (Control control in Control.Controls)
      {
        if (control.GetType() == typeof(Label))
        {
          control.Font = font;
          var size = control.Size;
          control.Size = new Size(size.Width, font.Height + 1);
        }
      }
    }

    // Gets the row spacing.
    // ********************
    private int RowHeight()
    {
      var controls = Control.Controls;
      var controlsByIndex
        = controls.Cast<Control>().OrderBy(x => x.TabIndex);

      var retHeight = 0;
      var labelHeight = 0;
      foreach (Control control in controlsByIndex)
      {
        if (control.GetType() == typeof(Label))
        {
          // First row must contain a label.
          if (0 == retHeight)
          {
            retHeight = control.Top;
            labelHeight = control.Height;
          }

          // Second row must contain a label.
          // Make sure row has changed.
          if (control.Top > retHeight + labelHeight)
          {
            retHeight = control.Top - retHeight;
            break;
          }
        }
      }
      return retHeight;
    }
    #endregion

    // ******************************
    #region Control Event Handlers
    // ******************************

    /// <summary>Fires the font Change event.</summary>
    // ********************
    protected void OnFontChange()
    {
      FontChange?.Invoke(Control, new EventArgs());
    }

    // Handles the MouseWheel event.
    // ********************
    private void Control_MouseWheel(object sender, MouseEventArgs e)
    {
      FontSize = Control.Font.Size;
      if (e.Delta > 0)
      {
        FontSize++;
      }
      else
      {
        FontSize--;
      }
      SetFont();
    }
    #endregion

    // ******************************
    #region Properties
    // ******************************

    /// <summary>Gets or sets the font size value.</summary>
    public float FontSize
    {
      get { return mFontSize; }
      set
      {
        mFontSize = value;
        OnFontChange();
      }
    }
    private float mFontSize;

    // Gets or sets the Control reference.
    private ContainerControl Control { get; set; }
    #endregion

    /// <summary>The font Change event.</summary>
    public event EventHandler<EventArgs> FontChange;
  }
}
