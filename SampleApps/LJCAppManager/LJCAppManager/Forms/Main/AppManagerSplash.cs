// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AppManagerSplash.cs
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LJCAppManager
{
	// The AppManager Splash and About dialog.
	/// <include path='items/AppManagerSplash/*' file='Doc/AppManagerSplash.xml'/>
	public partial class AppManagerSplash : Form
	{
		// Displays the Splash/About dialog.
		/// <include path='items/AppManagerSplashC/*' file='Doc/AppManagerSplash.xml'/>
		public AppManagerSplash(bool isSplash = false)
		{
			InitializeComponent();

			Size = new Size(369, 166);

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
