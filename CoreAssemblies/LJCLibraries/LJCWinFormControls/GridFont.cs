// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GridFont.cs
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides Grid Font sizing.</summary>
  public class GridFont
  {
    /// <summary>Initializes an object instance.</summary>
    /// <param name="parent"></param>
    /// <param name="grid"></param>
    /// <param name="autoSelect"></param>
    public GridFont(ContainerControl parent, LJCDataGrid grid
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

    #region Methods

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var fontFamily = Grid.Font.FontFamily;
      var style = Grid.Font.Style;
      Grid.Font = new Font(fontFamily, FontSize, style);
    }
    #endregion

    #region Control Event Handlers

    /// <summary>Fires the font Change event.</summary>
    // ********************
    protected void OnFontChange()
    {
      FontChange?.Invoke(Grid, new EventArgs());
    }

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
        if (Keys.Up == e.KeyCode
          || Keys.Down == e.KeyCode)
        {
          e.Handled = true;
        }
      }
    }

    // Handles the grid KeyUp event.
    private void Grid_KeyUp(object sender, KeyEventArgs e)
    {
      FontSize = Grid.Font.Size;
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
      FontSize = Grid.Font.Size;
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
    }
    #endregion

    #region Properties

    /// <summary>Select the grid on mouse enter.</summary>
    public bool AutoSelect { get; set; }

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

    // The LJCDataGrid control.
    private LJCDataGrid Grid { get; set; }

    // true if the grid received focus from a mouse click.
    private bool IsClicked { get; set; }

    // true if the Control key is held down; otherwise false.
    private bool IsControlKey { get; set; }

    // The parent form.
    private ContainerControl Parent { get; set; }

    // The previous focused control.
    private Control PrevControl { get; set; }
    #endregion

    /// <summary>The font Change event.</summary>
    public event EventHandler<EventArgs> FontChange;
  }
}
