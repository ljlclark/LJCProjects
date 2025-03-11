// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UserDetail.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCAppManagerDAL;

namespace LJCAppManager
{
	/// <summary>The User detail dialog.</summary>
	public partial class UserDetail : Form
	{
		#region Constructors

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		public UserDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "AppManager.chm";
			LJCHelpPageName = "UserDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UserDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void UserDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
					break;
			}
		}

		// Handles the FormClosing message.
		private void UserDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			// LJCPictureBox
			PersonPicture.ReleaseResources();
		}

		// Fires the Change event.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, mSettings.BeginColor, mSettings.EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			AppUser record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = Managers.AppUserManager.GetIDKey(LJCID);
				record = Managers.AppUserManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new AppUser();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(AppUser dataRecord)
		{
			if (dataRecord != null)
			{
				UserNameTextbox.Text = dataRecord.Name;
				UserIDTextbox.Text = dataRecord.UserID;
				PersonPicture.LoadFromFile(UserImageName());
			}
		}

		// Creates and returns a record object with the data from
		private AppUser SetRecordValues()
		{
			AppUser retValue = new AppUser()
			{
				ID = LJCID,
				Name = UserNameTextbox.Text.Trim(),
				UserID = UserIDTextbox.Text.Trim(),
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			AppUser lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ AppUser.ColumnName, (object)LJCRecord.Name },
				{ AppUser.ColumnUserID, LJCRecord.UserID }
			};
			var userManager = Managers.AppUserManager;
			lookupRecord = userManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The User already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					var updateKeyColumns = userManager.GetIDKey(LJCRecord.ID);
					List<string> updateColumns = new List<string>()
					{
						AppUser.ColumnName,
						AppUser.ColumnUserID
					};
					userManager.Update(LJCRecord, updateKeyColumns, updateColumns);
				}
				else
				{
					AppUser addedRecord = userManager.Add(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ID = addedRecord.ID;
					}
				}
			}
			return retValue;
		}

		// Validates the data.
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (!NetString.HasValue(UserNameTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {UserNameLabel.Text}");
			}
			if (!NetString.HasValue(UserIDTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {UserIDLabel.Text}");
			}

			if (retVal == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return retVal;
		}
		#endregion

		#region Image Methods

		// Returns the default image file name for the current user.
		private string UserImageName()
		{
			string retValue;

			// LJCPictureBox
			string userIDText = UserIDTextbox.Text.Trim().Replace("\\", "");
			retValue = $"{UserNameTextbox.Text.Trim()}{userIDText}.jpg";
			return retValue;
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
			Managers = new AppManagers(mSettings.DbServiceRef
			, mSettings.DataConfigName);

			UserNameLabel.BackColor = mSettings.BeginColor;
			UserIDLabel.BackColor = mSettings.BeginColor;

			UserNameTextbox.MaxLength = AppUser.LengthName;
			UserIDTextbox.MaxLength = AppUser.LengthUserID;
			Cursor = Cursors.Default;
		}
		#endregion

		#region Action Event Handlers

		// Displays a file selection dialog and loads the selected image.
		private void PictureMenuImport_Click(object sender, EventArgs e)
		{
			// LJCPictureBox
			PersonPicture.SelectImageFile();
		}

		// Rotates the image 90 degrees left.
		private void PictureMenuRotateLeft_Click(object sender, EventArgs e)
		{
			// LJCPictureBox
			PersonPicture.RotateLeft();
		}

		// Rotates the image 90 degrees right.
		private void PictureMenuRotateRight_Click(object sender, EventArgs e)
		{
			// LJCPictureBox
			PersonPicture.RotateRight();
		}

		// Indicates that a crop action is allowed.
		private void PictureMenuCrop_Click(object sender, EventArgs e)
		{
			// LJCPictureBox
			PersonPicture.AllowCrop = true;
		}

		// Saves the image to a file.
		private void PictureMenuSave_Click(object sender, EventArgs e)
		{
			// LJCPictureBox
			PersonPicture.SaveImageFile(UserImageName());
		}
		#endregion

		#region Control Event Handlers

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid()
				&& DataSave())
			{
				OnChange();
				DialogResult = DialogResult.OK;
			}
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void OnChange()
		{
			Change?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Properties

		// Gets or sets the primary ID value.
		internal int LJCID { get; set; }

		// Gets the LJCIsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// Gets a reference to the record object.
		internal AppUser LJCRecord { get; private set; }

		// Gets or sets the LJCHelpFileName value.
		internal string LJCHelpFileName
		{
			get { return mHelpFileName; }
			set { mHelpFileName = NetString.InitString(value); }
		}
		private string mHelpFileName;

		// Gets or sets the LJCHelpPageName value.
		internal string LJCHelpPageName
		{
			get { return mHelpPageName; }
			set { mHelpPageName = NetString.InitString(value); }
		}
		private string mHelpPageName;

		// Gets or sets the Managers value.
		private AppManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> Change;
		#endregion
	}
}
