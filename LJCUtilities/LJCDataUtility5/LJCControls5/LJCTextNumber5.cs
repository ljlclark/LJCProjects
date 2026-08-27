// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextNumber5.cs
using System;
using System.Windows.Forms;

namespace LJCControls5
{
  /// <summary>Provides event handlers for text numbers.</summary>
  public class LJCTextNumber
  {
    #region Event Handlers

    /// <summary>
    /// Only allows numbers or edit keys
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The event arguments.</param>
    public void KeyPress(object? sender, KeyPressEventArgs e)
    {
      mPrevText = "";
      if (sender is TextBox textBox)
      {
        mPrevText = textBox.Text;
        e.Handled = FormCommon.HandleNumber(mPrevText, e.KeyChar);
      }
    }

    /// <summary>
    /// Resets text to previous value if not a number.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The event arguments.</param>
    public void TextChanged(object? sender, EventArgs e)
    {
      if (sender is TextBox textBox)
      {
        if (!FormCommon.IsNumber(textBox.Text))
        {
          int saveStart = textBox.SelectionStart;
          textBox.Text = mPrevText;
          textBox.SelectionStart = saveStart;
        }
      }
    }
    #endregion

    #region Class Data

    // The previous text value.
    private string mPrevText = null!;
    #endregion
  }
}
