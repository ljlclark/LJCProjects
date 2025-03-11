// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Logon.cs
using System;
using System.Windows.Forms;
using System.Security.Principal;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;

namespace LJCAppManager
{
	/// <summary>The Logon dialog.</summary>
	public partial class Logon : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Logon()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle, mSettings.BeginColor
				, mSettings.EndColor);
		}

		// Configures the form and loads the initial control data.
		private void Logon_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();

			CenterToScreen();
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			bool success = false;
			string userID;
			string enteredPassword;

			userID = UserIDTextbox.Text.Trim();
			if (NetString.HasValue(userID))
			{
				enteredPassword = PasswordTextbox.Text.Trim();

				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				string[] values = identity.Name.Split('\\');
				string domain = values[0];
				WindowsUserID = $"{domain}\\{userID}";
				if (IsValidUser(userID, enteredPassword, domain))
				{
					success = true;
				}
			}

			if (success)
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else
			{
				MessageBox.Show("Invalid user or password.", "Logon Error", MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Private Methods

		// Retrieves the logged on user.
		/// <include path='items/LogonUser/*' file='Doc/Logon.xml'/>
		[System.Runtime.InteropServices.DllImport("advapi32.dll")]
		public static extern bool LogonUser(string userName, string domainName, string password, int LogonType, int LogonProvider, ref IntPtr phToken);

		// Verify the logged on user.
		private bool IsValidUser(string userName, string password, string domain)
		{
			IntPtr tokenHandler = IntPtr.Zero;
			bool isValid = LogonUser(userName, domain, password, 3, 0, ref tokenHandler);
			return isValid;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesAppManager.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			//mConnectionString = mSettings.ConnectionString;

			UserIDLabel.BackColor = mSettings.BeginColor;
			PasswordLabel.BackColor = mSettings.BeginColor;

			UserIDTextbox.MaxLength = 10;
			PasswordTextbox.MaxLength = 10;
		}
		#endregion

		#region Public Methods

		/// <summary>The windows user ID.</summary>
		public string WindowsUserID { get; private set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		//private string mConnectionString;
		#endregion
	}
}
