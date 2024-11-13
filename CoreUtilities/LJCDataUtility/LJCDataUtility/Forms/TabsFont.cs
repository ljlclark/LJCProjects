// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TabsFont.cs
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Tabs MouseWheel Font sizing.
  internal class TabsFont
  {
    // Initializes an object instance.
    internal TabsFont(LJCTabControl tabControl)
    {
      var tabs = tabControl;
      tabs.MouseEnter += Tabs_MouseEnter;
      tabs.MouseLeave += Tabs_MouseLeave;
      tabs.MouseWheel += new MouseEventHandler(Tabs_MouseWheel);
    }

    // Handles the MouseEnter event.
    private void Tabs_MouseEnter(object sender, EventArgs e)
    {
      IsTabs = true;
    }

    // Handles the Leave event.
    private void Tabs_MouseLeave(object sender, EventArgs e)
    {
      IsTabs = false;
    }

    // Handles the MouseWheel event.
    private void Tabs_MouseWheel(object sender
      , MouseEventArgs e)
    {
      if (IsTabs)
      {
        var tabs = sender as LJCTabControl;
        var size = tabs.Font.Size;
        if (e.Delta > 0)
        {
          size++;
        }
        else
        {
          size--;
        }
        var fontFamily = tabs.Font.FontFamily;
        var style = tabs.Font.Style;
        tabs.Font = new Font(fontFamily, size, style);
      }
    }

    private bool IsTabs { get; set; }
  }
}
