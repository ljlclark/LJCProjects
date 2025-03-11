// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Password.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;
using LJCWinFormCommon;

namespace LJCFacilityManager
{
	/// <summary>The Password update dialog.</summary>
	public partial class Password : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public Password()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void Password_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();

			DataRetrieve();

			CenterToParent();
		}
		#endregion

		#region Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			Person personRecord;
			Crypto crypto = new Crypto();

			var keyColumns = new DbColumns()
			{
				{ Person.ColumnID, LJCPersonID }
			};
			personRecord = mPersonManager.Retrieve(keyColumns);

			if (personRecord != null)
			{
				UserIDTextbox.Text = personRecord.UserID;
				if (ValuesFacility.Instance.SignonIsAdministrator)
				{
					UserIDTextbox.ReadOnly = false;
					AdminCheckbox.Enabled = true;
				}
				crypto.Decrypt(personRecord.Password);
				AdminCheckbox.Checked = crypto.IsAdministrator;
				mPrevAdministrator = crypto.IsAdministrator;
				NewTextbox.Text = crypto.Password;
				ReEnterTextbox.Text = crypto.Password;

				// Password ID must match person ID.
				if (ValuesFacility.Instance.SignonIsAdministrator == false
					&& crypto.PersonID != LJCPersonID)
				{
					FacilityCommon.ShowCorruptPasswordMessage();
					Close();
				}
			}
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Person personRecord;
			Crypto crypto = new Crypto();
			string oldPassword;
			string newPassword;
			string reEnterPassword;
			bool success;
			bool retValue = true;

			var keyColumns = new DbColumns()
			{
				{ Person.ColumnID, LJCPersonID }
			};
			personRecord = mPersonManager.Retrieve(keyColumns);
			if (personRecord != null)
			{
				// Check rights.
				success = true;
				if (ValuesFacility.Instance.SignonIsAdministrator == false)
				{
					oldPassword = OldTextbox.Text.Trim();
					crypto.Decrypt(personRecord.Password);
					if (oldPassword != crypto.Password)
					{
						retValue = false;
						success = false;
						MessageBox.Show("Invalid user or password.", "Logon Error", MessageBoxButtons.OK
							, MessageBoxIcon.Exclamation);
					}
				}

				if (success)
				{
					newPassword = NewTextbox.Text.Trim();
					reEnterPassword = ReEnterTextbox.Text.Trim();
					if (newPassword != reEnterPassword)
					{
						retValue = false;
						MessageBox.Show("New password does not equal re-entered password", "Password Match Error"
							, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						// Update password.
						crypto.Decrypt(personRecord.Password);
						crypto.Password = newPassword;
						crypto.PersonID = personRecord.ID;
						personRecord.Password = crypto.Encrypt();
						personRecord.UserID = UserIDTextbox.Text.Trim();

						List<string> propertyNames = new List<string>()
						{
							Person.ColumnUserID,
							Person.ColumnPassword
						};
						mPersonManager.Update(personRecord, null, propertyNames);
					}
				}
			}
			return retValue;
		}

		// Validates the data.
		/// <include path='items/IsValid/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retValue = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (mPrevAdministrator
				&& AdminCheckbox.Checked == false)
			{
				if (FacilityCommon.GetAdministrator(LJCPersonID) == null)
				{
					retValue = false;
					AdminCheckbox.Checked = true;
					builder.AppendLine("  There must be at least one system administrator.");
				}
			}
			if (AdminCheckbox.Checked
				&& NetString.HasValue(UserIDTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {UserIDLabel.Text}");
			}

			if (retValue == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return retValue;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			ValuesFacility values = ValuesFacility.Instance;

			mSettings = values.StandardSettings;

			mPersonManager = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			UserIDLabel.BackColor = Color.AliceBlue;
			OldLabel.BackColor = Color.AliceBlue;
			NewLabel.BackColor = Color.AliceBlue;
			ReEnterLabel.BackColor = Color.AliceBlue;

			OldTextbox.MaxLength = 10;
			NewTextbox.MaxLength = 10;
			ReEnterTextbox.MaxLength = 10;
			Cursor = Cursors.Default;
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid()
				&& DataSave())
			{
				DialogResult = DialogResult.OK;
			}
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the person ID value.</summary>
		public int LJCPersonID { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private PersonManager mPersonManager;
		private bool mPrevAdministrator;
		#endregion
	}
}
