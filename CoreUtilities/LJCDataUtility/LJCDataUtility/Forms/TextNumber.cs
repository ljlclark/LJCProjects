// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextNumber.cs
using LJCWinFormCommon;
using System;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides event handlers for text numbers.
  internal class TextNumber
  {
    #region Event Handlers

    // Only allows numbers or edit keys.
    public void KeyPress(object sender, KeyPressEventArgs e)
    {
      mPrevText = "";
      if (sender is TextBox textBox)
      {
        mPrevText = textBox.Text;
        e.Handled = FormCommon.HandleNumber(mPrevText, e.KeyChar);
      }
    }

    // Resets text to previous value if not a number.
    public void TextChanged(object sender, EventArgs e)
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

    private string mPrevText;
    #endregion
  }
}
