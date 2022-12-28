// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using CVRDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;

namespace CVRManager
{
	/// <summary>The Facility detail dialog.</summary>
	internal partial class FacilityDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		internal FacilityDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;

			// Set default class data.
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void FacilityDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			ConfigureControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the Form keys.
		private void FacilityDetail_KeyDown(object sender, KeyEventArgs e)
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
		/// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
				, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			Facility dataRecord;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				dataRecord = Managers.FacilityManager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				LJCIsUpdate = false;

				// Set default values.
				LJCRecord = new Facility();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(Facility dataRecord)
		{
			if (dataRecord != null)
			{
				CodeTextbox.Text = dataRecord.Code;
				DescriptionTextbox.Text = dataRecord.Description;
				FacilityTypeCombo.LJCSetByItemID(dataRecord.CodeTypeID);
			}
		}

		// Creates and returns a record object with the data from the controls.
		private Facility SetRecordValues()
		{
			Facility retValue = new Facility()
			{
				ID = LJCID,
				Code = FormCommon.SetString(CodeTextbox.Text),
				Description = DescriptionTextbox.Text,
				CodeTypeID = FacilityTypeCombo.LJCSelectedItemID()
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var facilityManager = Managers.FacilityManager;
			if (LJCIsUpdate)
			{
				var keyColumns = facilityManager.GetIDKey(LJCRecord.ID);
				facilityManager.Update(LJCRecord, keyColumns);
			}
			else
			{
				Facility addedRecord = facilityManager.Add(LJCRecord);
				if (addedRecord != null)
				{
					LJCRecord.ID = addedRecord.ID;
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		// Validates the data.
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retValue = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			//if (mCVPersonID <= 0)
			//{
			//	retValue = false;
			//	builder.AppendLine($"  {CustomerNameLabel.Text}");
			//}

			if (retValue == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}
			return retValue;
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

			// Initialize Class Data.
			Managers = ValuesCVRDAL.Instance.Managers;

			// Set control values.
			CodeLabel.BackColor = BeginColor;
			DescriptionLabel.BackColor = BeginColor;
			FacilityTypeLabel.BackColor = BeginColor;

			CodeTextbox.MaxLength = Facility.LengthCode;
			DescriptionTextbox.MaxLength = Facility.LengthDescription;

			// Load control data.
			CodeTypes codeTypes = Managers.CodeTypeManager.Load();
			foreach (CodeType codeType in codeTypes)
			{
				FacilityTypeCombo.LJCAddItem((int)codeType.ID, codeType.Description);
			}
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
			}
		}
		#endregion

		#region Action Event Handlers

		// Show the help page.
		private void DialogMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
				, LJCHelpPageName);
		}
		#endregion

		#region Control Event Handlers

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			if (IsValid()
				&& DataSave())
			{
				LJCOnChange();
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

		// Gets or sets the primary ID value.
		internal int LJCID { get; set; }

		// Gets a reference to the record object.
		internal Facility LJCRecord { get; private set; }

		// Gets the LJCIsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

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

		// The Managers object.
		private CVRManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardSettings mSettings;

		/// <summary>The Change event.</summary>
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
