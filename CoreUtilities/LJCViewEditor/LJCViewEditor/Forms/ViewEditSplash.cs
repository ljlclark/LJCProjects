// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewEditSplash.cs
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LJCViewEditor
{
	/// <summary>The ViewEditor Splash and About dialog.</summary>
	public partial class ViewEditSplash : Form
	{
		/// <summary>
		/// Displays the Splash/About dialog. 
		/// </summary>
		/// <param name="isSplash">Indicates if it is a Splash screen or About box.</param>
		public ViewEditSplash(bool isSplash = false)
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
