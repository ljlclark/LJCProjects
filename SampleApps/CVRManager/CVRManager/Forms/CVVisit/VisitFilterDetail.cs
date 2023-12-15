// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// VisitFilterDetail.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataAccess;

namespace CVRManager
{
	/// <summary>The Filter detail dialog.</summary>
	internal partial class VisitFilterDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		internal VisitFilterDetail()
		{
			InitializeComponent();

			// Set default class data.
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void FilterDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			ConfigureControls();
			GetRecordValues();
			CenterToParent();
		}

		// Handles the form keys.
		private void FilterDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
						, LJCHelpPageName);
					break;
			}
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
				, EndColor);
		}
		#endregion

		#region Data Methods

		// Gets the record values and copies them to the controls.
		private void GetRecordValues()
		{
			if (BeginDate > DataCommon.GetMinDateTime())
			{
				BeginDateMask.LJCText = DataCommon.GetUIDateString(BeginDate);
			}
			if (EndDate > DataCommon.GetMinDateTime())
			{
				EndDateMask.LJCText = DataCommon.GetUIDateString(EndDate);
			}
			LastNameTextBox.Text = LastName;
			MiddleNameTextBox.Text = MiddleName;
			FirstNameTextBox.Text = FirstName;
		}

		// Creates and returns a record object with the data from the controls.
		private void SetRecordValues()
		{
			string dateString;

			BeginDate = DataCommon.GetMinDateTime();
			if (!BeginDateMask.LJCIsEmpty())
			{
				dateString = BeginDateMask.Text;
				BeginDate = DateTime.Parse(dateString);
			}

			EndDate = DataCommon.GetMinDateTime();
			if (!EndDateMask.LJCIsEmpty())
			{
				dateString = EndDateMask.Text;
				EndDate = DateTime.Parse(dateString);
			}

			LastName = LastNameTextBox.Text.Trim();
			MiddleName = MiddleNameTextBox.Text.Trim();
			FirstName = FirstNameTextBox.Text.Trim();
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesCVRManager.Instance;
			mSettings = values.StandardSettings;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Set control values.
			BeginDateLabel.BackColor = mSettings.BeginColor;
			EndDateLabel.BackColor = mSettings.BeginColor;
			LastNameLabel.BackColor = mSettings.BeginColor;
			MiddleNameLabel.BackColor = mSettings.BeginColor;
			FirstNameLabel.BackColor = mSettings.BeginColor;
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				BeginDateButton.Height = BeginDateMask.Height;
				BeginDateButton.Width = BeginDateButton.Height + 1;
				EndDateButton.Height = BeginDateButton.Height;
				EndDateButton.Width = BeginDateButton.Width;
				OKButton.Height = 24;
				FormCancelButton.Height = 24;
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the help page.
		private void DialogMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
				, LJCHelpPageName);
		}
		#endregion

		#region Control Event Handlers

		// Show the Calendar control.
		private void BeginDateButton_Click(object sender, EventArgs e)
		{
			BeginDateMask.LJCText = ControlCommon.GetSelectedDate(BeginDateMask.Text);
		}

		// Show the Calendar control.
		private void EndDateButton_Click(object sender, EventArgs e)
		{
			EndDateMask.LJCText = ControlCommon.GetSelectedDate(EndDateMask.Text);
		}

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			SetRecordValues();
			DialogResult = DialogResult.OK;
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Properties

		// Gets or sets the BeginDate value.
		internal DateTime BeginDate { get; set; }

		// Gets or sets the EndDate value.
		internal DateTime EndDate { get; set; }

		// Gets or sets the LastName value.
		internal string LastName { get; set; }

		// Gets or sets the MiddleName value.
		internal string MiddleName { get; set; }

		// Gets or sets the FirstName value.
		internal string FirstName { get; set; }

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

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		#endregion
	}
}
