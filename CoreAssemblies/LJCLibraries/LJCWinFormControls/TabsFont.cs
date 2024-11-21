// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TabsFont.cs
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides Tabs Font sizing.</summary>
  public class TabsFont
  {
    /// <summary>Initializes an object instance.</summary>
    /// <param name="tabControl">The LJCTabControl object.</param>
    // ********************
    public TabsFont(LJCTabControl tabControl)
    {
      Tabs = tabControl;

      // Set event handlers.
      Tabs.KeyUp += Tabs_KeyUp;
      Tabs.MouseEnter += Tabs_MouseEnter;
      Tabs.MouseLeave += Tabs_MouseLeave;
      Tabs.MouseWheel += new MouseEventHandler(Tabs_MouseWheel);
    }

    #region Methods

    // Sets the font size value.
    // ********************
    private void SetFont()
    {
      var fontFamily = Tabs.Font.FontFamily;
      var style = Tabs.Font.Style;
      Tabs.Font = new Font(fontFamily, FontSize, style);
    }
    #endregion

    #region Control Event Handlers

    /// <summary>Fires the font Change event.</summary>
    // ********************
    protected void OnFontChange()
    {
      FontChange?.Invoke(Tabs, new EventArgs());
    }

    // Handles the KeyUp event.
    // ********************
    private void Tabs_KeyUp(object sender, KeyEventArgs e)
    {
      FontSize = Tabs.Font.Size;
      if (IsTabs && e.Control
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
    private void Tabs_MouseEnter(object sender, EventArgs e)
    {
      FontSize = Tabs.Font.Size;
      IsTabs = true;
    }

    // Handles the Leave event.
    // ********************
    private void Tabs_MouseLeave(object sender, EventArgs e)
    {
      IsTabs = false;
    }

    // Handles the MouseWheel event.
    // ********************
    private void Tabs_MouseWheel(object sender
      , MouseEventArgs e)
    {
      if (IsTabs)
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

    // true if the mouse is in the tabs; otherwise false.
    private bool IsTabs { get; set; }

    // The LJCTabControl control.
    private LJCTabControl Tabs { get; set; }
    #endregion

    /// <summary>The font Change event.</summary>
    public event EventHandler<EventArgs> FontChange;
  }
}
