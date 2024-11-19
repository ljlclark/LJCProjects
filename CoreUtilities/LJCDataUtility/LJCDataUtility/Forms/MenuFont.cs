// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MenuFont.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LJCDataUtility
{
  // Provides Menu MouseWheel Font sizing.
  internal class MenuFont
  {
    // Initializes an object instance.
    internal MenuFont(ToolStripDropDownMenu menu)
    {
      menu.MouseEnter += Menu_MouseEnter;
      menu.MouseWheel += new MouseEventHandler(Menu_MouseWheel);
      menu.KeyUp += Menu_KeyUp;
    }

    // Shpw the menu font size.
    private void ShowFontSize(ToolStripDropDownMenu menu)
    {
      var size = menu.Font.Size;
      var text = menu.Items[0].Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = text.Substring(0, index - 1);
      }
      menu.Items[0].Text = $"{text} [{size}]";
    }

    // Handles the menu KeyUp event.
    // ********************
    private void Menu_KeyUp(object sender, KeyEventArgs e)
    {
      if (Keys.Up == e.KeyCode
        || Keys.Down == e.KeyCode)
      {
        if (e.Control)
        {
          var menu = sender as ToolStripDropDownMenu;
          var size = menu.Font.Size;
          switch (e.KeyCode)
          {
            case Keys.Up:
              size++;
              break;

            case Keys.Down:
              size--;
              break;
          }
          var fontFamily = menu.Font.FontFamily;
          var style = menu.Font.Style;
          menu.Font = new Font(fontFamily, size, style);
          ShowFontSize(menu);
          e.Handled = true;
        }
      }
    }

    // Handles the MouseEnter event.
    private void Menu_MouseEnter(object sender, EventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      ShowFontSize(menu);
    }

    // Handles the menu MouseWheel event.
    private void Menu_MouseWheel(object sender, MouseEventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      var size = menu.Font.Size;
      if (e.Delta > 0)
      {
        size++;
      }
      else
      {
        size--;
      }
      var fontFamily = menu.Font.FontFamily;
      var style = menu.Font.Style;
      menu.Font = new Font(fontFamily, size, style);
      ShowFontSize(menu);
    }
  }
}
