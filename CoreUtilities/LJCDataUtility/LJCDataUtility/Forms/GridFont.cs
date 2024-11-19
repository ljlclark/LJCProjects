// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GridFont.cs
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides Grid MouseWheel Font sizing.
  internal class GridFont
  {
    // Initializes an object instance.
    internal GridFont(ContainerControl parent, LJCDataGrid grid
      , bool autoSelect = true)
    {
      Parent = parent;
      Grid = grid;
      AutoSelect = autoSelect;
      IsClicked = false;

      // Set event handlers.
      grid.Click += Grid_Click;
      grid.KeyDown += Grid_KeyDown;
      grid.KeyUp += Grid_KeyUp;
      grid.Leave += Grid_Leave;
      grid.MouseEnter += Grid_MouseEnter;
      grid.MouseLeave += Grid_MouseLeave;
      grid.MouseWheel += new MouseEventHandler(Grid_MouseWheel);
    }

    // Show the grid font size.
    private void ShowFontSize(DataGridView grid)
    {
      var size = grid.Font.Size;
      var text = Parent.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = Parent.Text.Substring(0, index - 1);
      }
      Parent.Text = $"{text} [{size}]";
    }

    #region Control Event Handlers

    // Handles the grid Click event.
    private void Grid_Click(object sender, EventArgs e)
    {
      IsClicked = true;
    }

    // Handles the grid KeyDown event.
    private void Grid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.Control)
      {
        IsControlKey = true;
      }
    }

    // Handles the grid KeyUp event.
    private void Grid_KeyUp(object sender, KeyEventArgs e)
    {
      if (Keys.Up == e.KeyCode
        || Keys.Down == e.KeyCode)
      {
        if (e.Control)
        {
          var grid = sender as LJCDataGrid;
          var size = grid.Font.Size;
          switch (e.KeyCode)
          {
            case Keys.Up:
              size++;
              break;

            case Keys.Down:
              size--;
              break;
          }
          var fontFamily = grid.Font.FontFamily;
          var style = grid.Font.Style;
          grid.Font = new Font(fontFamily, size, style);
          ShowFontSize(grid);
          e.Handled = true;
        }
      }
      IsControlKey = false;
    }

    // Handles the grid Leave event.
    private void Grid_Leave(object sender, EventArgs e)
    {
      IsClicked = false;
      IsControlKey = false;
      PrevControl = null;
    }

    // Handles the grid MouseEnter event.
    private void Grid_MouseEnter(object sender, EventArgs e)
    {
      var grid = sender as LJCDataGrid;
      ShowFontSize(grid);
      if (AutoSelect
        && null == PrevControl)
      {
        PrevControl = Parent.ActiveControl;
        if (PrevControl != null)
        {
          Grid.Select();
        }
      }
    }

    // Handles the grid MouseLeave event.
    private void Grid_MouseLeave(object sender, EventArgs e)
    {
      if (AutoSelect
        && !IsClicked)
      {
        PrevControl?.Focus();
      }
      PrevControl = null;
    }

    // Handles the grid MouseWheel event.
    private void Grid_MouseWheel(object sender
      , MouseEventArgs e)
    {
      if (!IsControlKey)
      {
        var grid = sender as LJCDataGrid;
        var size = grid.Font.Size;
        if (e.Delta > 0)
        {
          size++;
        }
        else
        {
          size--;
        }
        var fontFamily = grid.Font.FontFamily;
        var style = grid.Font.Style;
        grid.Font = new Font(fontFamily, size, style);
        ShowFontSize(grid);
      }
    }
    #endregion

    #region Properties

    // Select the grid on mouse enter.
    internal bool AutoSelect { get; set; }

    // The LJCDataGrid control.
    private LJCDataGrid Grid { get; set; }

    // true if the grid received focus from a mouse click.
    private bool IsClicked { get; set; }

    // true if the Control key is held down.
    private bool IsControlKey { get; set; }

    // The parent form.
    private ContainerControl Parent { get; set; }

    // The previous focused control.
    private Control PrevControl { get; set; }
    #endregion
  }
}
