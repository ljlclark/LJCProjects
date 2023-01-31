// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenTextEditSplash.cs
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // The GenTextEdit Splash and About dialog.
  /// <include path='items/GenTextEditSplash/*' file='Doc/GenTextEditSplash.xml'/>
  public partial class GenTextEditSplash : Form
  {
    // Displays the Splash/About dialog.
    /// <include path='items/GenTextEditSplashC/*' file='Doc/GenTextEditSplash.xml'/>
    public GenTextEditSplash(bool isSplash = false)
    {
      InitializeComponent();

      Size = new Size(400, 166);

      Version versionInfo = Assembly.GetExecutingAssembly().GetName().Version;
      VersionLabel.Text = "Version " + versionInfo.ToString();

      if (isSplash)
      {
        OKButton.Visible = false;
        mTimer = new Timer();
        mTimer.Tick += MTimer_Tick;
        mTimer.Interval = 2000;
        mTimer.Start();
      }
    }

    // Closes the dialog.
    private void OKButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    // The Splash timer.
    private void MTimer_Tick(object sender, EventArgs e)
    {
      mTimer.Stop();
      Close();
    }

    private readonly Timer mTimer;
  }
}
