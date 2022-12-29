// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CVRDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCRegionDAL;
using LJCRegionForm;
using LJCRegionManager;
using LJCWinFormCommon;

namespace CVRManager
{
	/// <summary>The CVPerson detail dialog.</summary>
	internal partial class CVPersonDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal CVPersonDetail()
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
		private void CVPersonDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			ConfigureControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void CVPersonDetail_KeyDown(object sender, KeyEventArgs e)
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
		/// <include path='items/OnPaintBackground/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
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
			CVPerson dataRecord;

			Cursor = Cursors.WaitCursor;
			Text = "Person Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				dataRecord = Managers.CVPersonManager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;

				// Set default values.
				LJCRecord = new CVPerson();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(CVPerson dataRecord)
		{
			if (dataRecord != null)
			{
				// Get Foreign Key values.
				mRegionID = dataRecord.RegionID;
				mProvinceID = dataRecord.ProvinceID;
				mCityID = dataRecord.CityID;
				mCitySectionID = dataRecord.CitySectionID;

				FirstNameTextbox.Text = dataRecord.FirstName;
				MiddleNameTextbox.Text = dataRecord.MiddleName;
				LastNameTextbox.Text = dataRecord.LastName;
				if (dataRecord.CVSexID > 0)
				{
					SexCombo.LJCSetByItemID((int)dataRecord.CVSexID);
				}
				AddressTextbox.Text = dataRecord.DeliveryAddressLine;
				RegionTextbox.Text = dataRecord.LastLine;
				PhoneTextbox.Text = dataRecord.Phone;
			}
		}

		// Creates and returns a record object with the data from the controls.
		private CVPerson SetRecordValues()
		{
			CVPerson retValue = new CVPerson()
			{
				ID = LJCID,
				RegionID = mRegionID,
				ProvinceID = mProvinceID,
				CityID = mCityID,
				CitySectionID = mCitySectionID,
				FirstName = FirstNameTextbox.Text.Trim(),
				MiddleName = MiddleNameTextbox.Text.Trim(),
				LastName = LastNameTextbox.Text.Trim(),
				CVSexID = SexCombo.LJCSelectedItemID(),
				DeliveryAddressLine = AddressTextbox.Text.Trim(),
				LastLine = RegionTextbox.Text.Trim(),
				Phone = PhoneTextbox.Text.Trim()
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var cvPersonManager = Managers.CVPersonManager;
			if (LJCIsUpdate)
			{
				var keyColumns = cvPersonManager.GetIDKey(LJCRecord.ID);
				cvPersonManager.Update(LJCRecord, keyColumns);
			}
			else
			{
				CVPerson addedRecord = cvPersonManager.Add(LJCRecord);
				if (addedRecord != null)
				{
					LJCRecord.ID = addedRecord.ID;
				}
			}

			// Get join display values.
			CVPerson lookupRecord = cvPersonManager.RetrieveWithID(LJCRecord.ID);
			if (lookupRecord != null)
			{
				LJCRecord.FirstName = lookupRecord.FirstName;
				LJCRecord.MiddleName = lookupRecord.MiddleName;
				LJCRecord.LastName = lookupRecord.LastName;
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

			if (false == NetString.HasValue(FirstNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {FirstNameLabel.Text}");
			}
			if (false == NetString.HasValue(MiddleNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {MiddleNameLabel.Text}");
			}
			if (false == NetString.HasValue(LastNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {LastNameLabel.Text}");
			}

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

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				RegionButton.Height = RegionTextbox.Height;
				RegionButton.Width = RegionButton.Height + 1;
				OKButton.Height = 24;
				FormCancelButton.Height = 24;
			}
		}

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
			FirstNameLabel.BackColor = mSettings.BeginColor;
			MiddleNameLabel.BackColor = mSettings.BeginColor;
			LastNameLabel.BackColor = mSettings.BeginColor;
			SexLabel.BackColor = mSettings.BeginColor;
			AddressLabel.BackColor = mSettings.BeginColor;
			RegionLabel.BackColor = mSettings.BeginColor;
			PhoneLabel.BackColor = mSettings.BeginColor;

			FirstNameTextbox.MaxLength = CVPerson.LengthFirstName;
			MiddleNameTextbox.MaxLength = CVPerson.LengthMiddleName;
			LastNameTextbox.MaxLength = CVPerson.LengthLastName;
			AddressTextbox.MaxLength = CVPerson.LengthDeliveryAddressLine;
			RegionTextbox.MaxLength = CVPerson.LengthLastLine;
			PhoneTextbox.MaxLength = CVPerson.LengthPhone;

			// Load control data.
			CVSexes cvSexes = Managers.CVSexManager.Load();
			foreach (CVSex cvSex in cvSexes)
			{
				SexCombo.LJCAddItem((int)cvSex.ID, cvSex.Name);
			}
			Cursor = Cursors.Default;
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

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
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

		// Displays the Region Selection List.
		private void RegionButton_Click(object sender, EventArgs e)
		{
			RegionTabForm regionForm = new RegionTabForm()
			{
				LJCRegionID = mRegionID,
				LJCProvinceID = mProvinceID,
				LJCCityID = mCityID,
				LJCCitySectionID = mCitySectionID,
				IsChildWindow = true
			};
			if (DialogResult.OK == regionForm.ShowDialog())
			{
				RegionModule regionModule = regionForm.RegionModule;
				mRegionID = 0;
				if (regionModule.LJCSelectedRegion != null)
				{
					mRegionID = regionModule.LJCSelectedRegion.ID;
				}
				mProvinceID = 0;
				if (regionModule.LJCSelectedProvince != null)
				{
					mProvinceID = regionModule.LJCSelectedProvince.ID;
				}
				mCityID = 0;
				if (regionModule.LJCSelectedCity != null)
				{
					mCityID = regionModule.LJCSelectedCity.ID;
				}
				mCitySectionID = 0;
				if (regionModule.LJCSelectedCitySection != null)
				{
					mCitySectionID = regionModule.LJCSelectedCitySection.ID;
				}
				RegionTextbox.Text = CreateLastLine(regionModule);
			}
		}

		// Create Region LastLine.
		private string CreateLastLine(RegionModule regionModule)
		{
			string retValue = null;

			if (regionModule != null)
			{
				Province province = regionModule.LJCSelectedProvince;
				City city = regionModule.LJCSelectedCity;
				CitySection citySection = regionModule.LJCSelectedCitySection;

				StringBuilder builder = new StringBuilder(64);

				if (citySection != null
					&& NetString.HasValue(citySection.Name))
				{
					builder.Append(citySection.Name);
				}

				if (city != null
					&& NetString.HasValue(city.Name))
				{
					if (builder.Length > 0)
					{
						builder.Append(" ");
					}
					builder.Append(city.Name);
				}

				if (province != null)
				{
					string text = null;
					if (NetString.HasValue(province.Name))
					{
						text = province.Name;
					}
					if (NetString.HasValue(province.Abbreviation))
					{
						text = province.Abbreviation;
					}
					if (builder.Length > 0)
					{
						builder.Append(", ");
					}

					// Trim last line to allow for zipcode.
					int fillLength = builder.Length;
					if (city != null && NetString.HasValue(city.ZipCode))
					{
						fillLength += city.ZipCode.Length + 1;
					}
					if (fillLength + text.Length > CVPerson.LengthLastLine)
					{
						text = text.Substring(0, CVPerson.LengthLastLine - fillLength);
					}
					builder.Append(text);
				}

				if (city != null
					&& NetString.HasValue(city.Name))
				{
					if (builder.Length > 0)
					{
						builder.Append(" ");
					}
					builder.Append(city.ZipCode);
				}
				retValue = builder.ToString();
			}
			return retValue;
		}
		#endregion

		#region Properties

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

		// Gets or sets the primary ID value.
		internal long LJCID { get; set; }

		// Gets the LJCIsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// Gets a reference to the record object.
		internal CVPerson LJCRecord { get; private set; }

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }

		// The Managers object.
		private CVRManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardSettings mSettings;

		// Foreign Keys
		private int mCityID;
		private int mCitySectionID;
		private int mProvinceID;
		private int mRegionID;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
