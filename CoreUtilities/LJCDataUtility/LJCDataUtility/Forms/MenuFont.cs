// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MenuFont.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Menu Font sizing.
  internal class MenuFont
  {
    // Initializes an object instance.
    // ********************
    internal MenuFont(ToolStripDropDownMenu menu)
    {
      Menu = menu;
      Menu.MouseEnter += Menu_MouseEnter;
      Menu.MouseWheel += new MouseEventHandler(Menu_MouseWheel);
      Menu.KeyUp += Menu_KeyUp;
    }

    #region Methods

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var fontFamily = Menu.Font.FontFamily;
      var style = Menu.Font.Style;
      Menu.Font = new Font(fontFamily, FontSize, style);
      ShowFontSize();
    }

    // Show the menu font size.
    // ********************
    private void ShowFontSize()
    {
      var text = Menu.Items[0].Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = text.Substring(0, index - 1);
      }
      Menu.Items[0].Text = $"{text} [{FontSize}]";
    }
    #endregion

    #region Event Handlers

    // Handles the menu KeyUp event.
    // ********************
    private void Menu_KeyUp(object sender, KeyEventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      FontSize = menu.Font.Size;
      if (Keys.Up == e.KeyCode
        || Keys.Down == e.KeyCode)
      {
        if (e.Control)
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
    }

    // Handles the MouseEnter event.
    // ********************
    private void Menu_MouseEnter(object sender, EventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      FontSize = menu.Font.Size;
      ShowFontSize();
    }

    // Handles the menu MouseWheel event.
    // ********************
    private void Menu_MouseWheel(object sender, MouseEventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      FontSize = menu.Font.Size;
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
    internal float FontSize { get; set; }

    // Gets or sets the Menu reference.
    private ToolStripDropDownMenu Menu { get; set; }
    #endregion
  }
}
