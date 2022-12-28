// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TextDisplay.cs
using System;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides a form to display messages that can be copied.</summary>
  public partial class TextDisplay : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public TextDisplay()
    {
      InitializeComponent();
    }
    #endregion

    #region Action Methods

    // Send the CopyAll key combination.
    private void MenuCopyAllItem_Click(object sender, EventArgs e)
    {
      SendKeys.Send("^a");
      SendKeys.Send("^C");
    }

    // Send the SelectAll key combination.
    private void MenuSelectAllItem_Click(object sender, EventArgs e)
    {
      SendKeys.Send("^a");
    }

    // Send the Copy key combination.
    private void MenuCopyItem_Click(object sender, EventArgs e)
    {
      SendKeys.Send("^C");
    }

    // Send the Paste key combination.
    private void MenuPasteItem_Click(object sender, EventArgs e)
    {
      SendKeys.Send("^V");
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the reference to the RTF textbox.</summary>
    public LJCRtControl LJCRichTextBox
    {
      get
      {
        return TextRichTextbox;
      }
    }
    #endregion
  }
}
