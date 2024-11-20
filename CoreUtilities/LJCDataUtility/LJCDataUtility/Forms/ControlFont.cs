using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Font sizing.
  internal class ControlFont
  {
    // Initializes an object instance.
    // ********************
    internal ControlFont(Control control)
    {
      Control = control;
      Control.KeyUp += Control_KeyUp;
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

    #region Event Handlers

    // Handles the menu KeyUp event.
    // ********************
    private void Control_KeyUp(object sender, KeyEventArgs e)
    {
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

    // Handles the MouseWheel event.
    // ********************
    private void Control_MouseWheel(object sender, MouseEventArgs e)
    {
      var control = sender as Control;
      FontSize = control.Font.Size;
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

    // Gets or sets the Control reference.
    private Control Control { get; set; }
    #endregion
  }
}
