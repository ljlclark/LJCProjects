﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlFont.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Font sizing.
  internal class ControlFont
  {
    // Initializes an object instance.
    // ********************
    internal ControlFont(ContainerControl control)
    {
      Control = control;

      // Set event handlers.
      Control.MouseWheel += Control_MouseWheel;
    }

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var fontFamily = Control.Font.FontFamily;
      var style = Control.Font.Style;
      Control.Font = new Font(fontFamily, FontSize, style);
    }

    #region Control Event Handlers

    // Fires the font Change event.
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

    #region Properties

    // Gets or sets the font size value.
    internal float FontSize
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

    // The font Change event.
    internal event EventHandler<EventArgs> FontChange;
  }
}
