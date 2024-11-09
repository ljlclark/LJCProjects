using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Menu MouseWheel Font sizing.
  internal class MenuFont
  {
    // Initializes an object instance.
    internal MenuFont(ToolStripDropDownMenu menu)
    {
      menu.MouseWheel += new MouseEventHandler(Menu_MouseWheel);
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
      if (size > 10)
      {
        //style = FontStyle.Bold;
      }
      menu.Font = new Font(fontFamily, size, style);
    }
  }
}
