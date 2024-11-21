// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MenuFont.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides Menu Font sizing.</summary>
  public class MenuFont
  {
    /// <summary>Initializes an object instance.</summary>
    /// <param name="menu">The Menu object.</param>
    // ********************
    public MenuFont(ToolStripDropDownMenu menu)
    {
      Menu = menu;

      // Set event handlers.
      Menu.KeyUp += Menu_KeyUp;
      Menu.MouseEnter += Menu_MouseEnter;
      Menu.MouseWheel += new MouseEventHandler(Menu_MouseWheel);
    }

    #region Methods

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var fontFamily = Menu.Font.FontFamily;
      var style = Menu.Font.Style;
      Menu.Font = new Font(fontFamily, FontSize, style);
    }
    #endregion

    #region Control Event Handlers

    /// <summary>Fires the font Change event.</summary>
    // ********************
    protected void OnFontChange()
    {
      FontChange?.Invoke(Menu, new EventArgs());
    }

    // Handles the menu KeyUp event.
    // ********************
    private void Menu_KeyUp(object sender, KeyEventArgs e)
    {
      FontSize = Menu.Font.Size;
      if (e.Control
        && (Keys.Up == e.KeyCode
        || Keys.Down == e.KeyCode))
      {
        switch (e.KeyCode)
        {
          case Keys.Up:
            FontSize++;
            break;

          case Keys.Down:
            FontSize--;
            break;
        }
        SetFont();
        e.Handled = true;
      }
    }

    // Handles the MouseEnter event.
    // ********************
    private void Menu_MouseEnter(object sender, EventArgs e)
    {
      FontSize = Menu.Font.Size;
    }

    // Handles the menu MouseWheel event.
    // ********************
    private void Menu_MouseWheel(object sender, MouseEventArgs e)
    {
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

    // Gets or sets the Menu reference.
    private ToolStripDropDownMenu Menu { get; set; }
    #endregion

    /// <summary>The font Change event.</summary>
    public event EventHandler<EventArgs> FontChange;
  }
}
