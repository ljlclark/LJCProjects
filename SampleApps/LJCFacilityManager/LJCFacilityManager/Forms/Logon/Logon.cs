// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Logon.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
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

		// Configures the form and loads the initial control data.
		private void Logon_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();

			CenterToScreen();
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, mSettings.BeginColor, mSettings.EndColor);
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			ValuesFacility values = ValuesFacility.Instance;

			mSettings = values.StandardSettings;

			// Initialize Class Data.
			mPersonManager = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			CreateDefaultAdministrator();

			UserIDLabel.BackColor = mSettings.BeginColor;
			PasswordLabel.BackColor = mSettings.BeginColor;

			UserIDTextbox.MaxLength = 10;
			PasswordTextbox.MaxLength = 10;
			Cursor = Cursors.Default;
		}

		// 
		private void CreateDefaultAdministrator()
		{
			Person record;
			Crypto crypto;

			if (FacilityCommon.GetAdministrator(0) == null)
			{
				var keyColumns = new DbColumns()
				{
					{ Person.ColumnFirstName, (object)"System" },
					{ Person.ColumnLastName, (object)"Administrator" }
				};
				record = mPersonManager.Retrieve(keyColumns);
				if (record == null)
				{
					record = new Person()
					{
						FirstName = "System",
						LastName = "Administrator",
						CodeTypeID = 1
					};
					mPersonManager.Add(record);

					crypto = new Crypto()
					{
						Password = "Admin",
						PersonID = record.ID,
						IsAdministrator = true
					};
					record.Password = crypto.Encrypt();
					record.UserID = "Admin";

					List<string> propertyNames = new List<string>()
					{
						Person.ColumnUserID,
						Person.ColumnPassword
					};
					mPersonManager.Update(record, null, propertyNames);
				}
			}
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			Person personRecord = null;
			Crypto crypto;
			string userID;
			string enteredPassword;
			int personID;
			bool success = false;

			userID = UserIDTextbox.Text.Trim();
			if (NetString.HasValue(userID))
			{
				enteredPassword = PasswordTextbox.Text.Trim();
				var keyColumns = new DbColumns()
				{
					{ Person.ColumnUserID, userID }
				};
				personRecord = mPersonManager.Retrieve(keyColumns);
				if (personRecord != null)
				{
					crypto = new Crypto();
					if (crypto.Decrypt(personRecord.Password) == false)
					{
						FacilityCommon.ShowCorruptPasswordMessage();
					}
					else
					{
						personID = crypto.PersonID;
						var userPassword = crypto.Password;
						if (personRecord.ID == personID
							&& enteredPassword == userPassword)
						{
							success = true;
						}
					}
				}
			}
			if (success)
			{
				ValuesFacility.Instance.SignonID = personRecord.ID;
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

		#region Class Data

		private StandardUISettings mSettings;
		private PersonManager mPersonManager;
		#endregion
	}
}
