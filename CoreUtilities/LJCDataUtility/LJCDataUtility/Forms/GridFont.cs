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
    internal GridFont(ContainerControl parent, LJCDataGrid grid)
    {
      Parent = parent;
      Grid = grid;
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

    private void Grid_Click(object sender, EventArgs e)
    {
      IsClicked = true;
    }

    #region Control Event Handlers

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
      IsControlKey = false;
    }

    // Handles the grid Leave event.
    private void Grid_Leave(object sender, EventArgs e)
    {
      IsClicked = false;
      IsControlKey = false;
      PrevControl = null;
    }

    private void Grid_MouseEnter(object sender, EventArgs e)
    {
      if (null == PrevControl)
      {
        PrevControl = Parent.ActiveControl;
        if (PrevControl != null)
        {
          Grid.Focus();
        }
      }
    }

    // Handles the grid MouseLeave event.
    private void Grid_MouseLeave(object sender, EventArgs e)
    {
      if (!IsClicked)
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
        var fontFamily = "Microsoft Sans Serif";
        var style = FontStyle.Bold;
        grid.Font = new Font(fontFamily, size, style);
      }
    }
    #endregion

    #region Properties

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
