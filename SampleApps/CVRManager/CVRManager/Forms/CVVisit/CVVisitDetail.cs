// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CVVisitDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CVRDAL;
using LJCDataAccess;
using LJCDBClientLib;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;
using LJCWinFormControls;

namespace CVRManager
{
	// The CVVisit detail dialog.
	internal partial class CVVisitDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal CVVisitDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCRecord = null;
			LJCIsUpdate = false;

			// Set default class data.
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void CVVisitDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			ConfigureControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void CVVisitDetail_KeyDown(object sender, KeyEventArgs e)
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

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			CVVisit dataRecord;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				dataRecord = CVRManagers.CVVisitManager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				LJCIsUpdate = false;
				ParentNameText.Text = LJCParentName;

				// Set default values.
				LJCRecord = new CVVisit();
				mBaseTemperatureUnitID = GetCelsiusID();
				TemperatureCombo.LJCSetByItemID(mBaseTemperatureUnitID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(CVVisit dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.FacilityID;
				ParentNameText.Text = LJCParentName;

				// Get Foreign Key values.
				ContactNameText.Text = GetCVPersonName(dataRecord);
				mBaseTemperature = dataRecord.BaseTemperature;
				mBaseTemperatureUnitID = dataRecord.BaseTemperatureUnitID;
				if (mBaseTemperatureUnitID < 1)
				{
					mBaseTemperatureUnitID = GetCelsiusID();
				}

				ContactIndexText.Text = dataRecord.CVPersonID.ToString();
				//ContactIndexText.ReadOnly = true;
				//ContactIndexButton.Enabled = false;

				// Set missing Temperature values from Base Temperature.
				string temperature = dataRecord.Temperature;
				if (false == NetString.HasValue(dataRecord.Temperature))
				{
					temperature = mBaseTemperature;
				}
				if (dataRecord.TemperatureUnitID < 1)
				{
					dataRecord.TemperatureUnitID = mBaseTemperatureUnitID;
				}

				// Set Temperature.
				mOriginalTemperature = temperature;
				TemperatureText.Text = temperature;
				mAllowChange = false;
				TemperatureCombo.LJCSetByItemID(dataRecord.TemperatureUnitID);
				mAllowChange = true;

				GetDateTime(RegisterDateMask, RegisterTimeMask, dataRecord.RegisterTime);
				GetDateTime(EnterDateMask, EnterTimeMask, dataRecord.EnterTime);
				GetDateTime(ExitDateMask, ExitTimeMask, dataRecord.ExitTime);
			}
		}

		// Creates and returns a record object with the data from the controls.
		private CVVisit SetRecordValues()
		{
			CVVisit retValue = new CVVisit()
			{
				ID = LJCID,
				FacilityID = LJCParentID,
				CVPersonID = mCVPersonID,
				Temperature = TemperatureText.Text.Trim(),
				TemperatureUnitID = TemperatureCombo.LJCSelectedItemID(),
				BaseTemperature = mBaseTemperature,
				BaseTemperatureUnitID = mBaseTemperatureUnitID,
				RegisterTime = SetDateTime(RegisterDateMask, RegisterTimeMask),
				EnterTime = SetDateTime(EnterDateMask, EnterTimeMask),
				ExitTime = SetDateTime(ExitDateMask, ExitTimeMask)
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var cvVisitManager = CVRManagers.CVVisitManager;
			if (LJCIsUpdate)
			{
				var keyColumns = cvVisitManager.GetIDKey(LJCRecord.ID);
				cvVisitManager.Update(LJCRecord, keyColumns);
			}
			else
			{
				CVVisit addedRecord = cvVisitManager.Add(LJCRecord);
				if (addedRecord != null)
				{
					LJCRecord.ID = addedRecord.ID;
				}
			}

			// Get join display values.
			CVVisit lookupRecord = cvVisitManager.RetrieveWithID(LJCRecord.ID);
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

			if (mCVPersonID <= 0)
			{
				retValue = false;
				builder.AppendLine($"  {ContactNameLabel.Text}");
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

		// Get the CVPerson full name and store the foreign key.
		private string GetCVPersonName(CVVisit dataRecord)
		{
			string retValue = null;

			mCVPersonID = dataRecord.CVPersonID;
			if (mCVPersonID > 0)
			{
				var keyColumns = CVRManagers.CVPersonManager.GetIDKey(mCVPersonID);
				var cvPerson = CVRManagers.CVPersonManager.Retrieve(keyColumns);
				retValue = GetFullName(cvPerson);
			}
			return retValue;
		}

		// Get the temperature value and store the foreign key.
		private int GetCelsiusID()
		{
			int retValue;

			UnitMeasure unitMeasure;
			var unitMeasureManager = UnitMeasureManagers.UnitMeasureManager;
			var keyColumns = new DbColumns()
			{
				{ UnitMeasure.ColumnName, (object)"Celsius" }
			};
			unitMeasure = unitMeasureManager.Retrieve(keyColumns);
			retValue = unitMeasure.ID;
			return retValue;
		}

		// Gets the Full Name from the CVPerson record.
		private string GetFullName(CVPerson cvPerson)
		{
			string retValue = null;

			StringBuilder builder;
			if (cvPerson != null)
			{
				builder = new StringBuilder(64);
				builder.Append(cvPerson.FirstName.Trim());
				if (NetString.HasValue(cvPerson.MiddleName))
				{
					if (NetString.HasValue(cvPerson.FirstName))
					{
						builder.Append(" ");
					}
					builder.Append(cvPerson.MiddleName.Trim());
				}
				if (NetString.HasValue(cvPerson.LastName))
				{
					if (NetString.HasValue(cvPerson.FirstName)
						|| NetString.HasValue(cvPerson.MiddleName))
					{
						builder.Append(" ");
					}
					builder.Append(cvPerson.LastName.Trim());
				}
				retValue = builder.ToString();
			}
			return retValue;
		}

		// Gets the DateTime values and copies them into the specified conrols.
		private void GetDateTime(MaskedTextBox dateMaskTextbox
			, MaskedTextBox timeMaskTextbox, DateTime dateTime)
		{
			if (dateTime > DataCommon.GetMinDateTime())
			{
				dateMaskTextbox.Mask = LJCMaskBox.LJCDateMaskValue;
				timeMaskTextbox.Mask = LJCMaskBox.LJCTimeMaskValue;
				dateMaskTextbox.Text = DataCommon.GetUIDateString(dateTime);
				string timeString = DataCommon.GetUITimeString(dateTime);
				timeMaskTextbox.Text = timeString;
			}
		}

		// Creates and returns the DateTime from the controls. 
		private DateTime SetDateTime(MaskedTextBox dateMaskTextbox
			, MaskedTextBox timeMaskTextbox)
		{
			DateTime retValue = DataCommon.GetMinDateTime();

			if (NetString.HasValue(dateMaskTextbox.Text)
				&& NetString.HasValue(timeMaskTextbox.Text))
			{
				var dateTimeString = $"{dateMaskTextbox.Text} {timeMaskTextbox.Text}";
				retValue = DateTime.Parse(dateTimeString);
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
			mSettingTemperatureUnitCode = values.TemperatureUnitCode;
			mSettingTemperatureHighValue = values.TemperatureHighValue;
			mSettingTemperatureLowValue = values.TemperatureLowValue;

			// Initialize Class Data.
			CVRManagers = ValuesCVRDAL.Instance.Managers;
			UnitMeasureManagers = new UnitMeasureManagers();
			UnitMeasureManagers.SetDBProperties(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Set control values.
			ParentNameLabel.BackColor = BeginColor;
			ContactNameLabel.BackColor = BeginColor;
			ContactIndexLabel.BackColor = BeginColor;
			RegisterTimeLabel.BackColor = BeginColor;
			EnterTimeLabel.BackColor = BeginColor;
			ExitTimeLabel.BackColor = BeginColor;
			TemperatureLabel.BackColor = BeginColor;

			RegisterTimeMask.LJCMaskValue = LJCMaskBox.LJCTimeMaskValue;
			EnterTimeMask.LJCMaskValue = LJCMaskBox.LJCTimeMaskValue;
			ExitTimeMask.LJCMaskValue = LJCMaskBox.LJCTimeMaskValue;

			// Load control data.
			var unitMeasureManager = UnitMeasureManagers.UnitMeasureManager;
			var unitMeasures = unitMeasureManager.LoadWithCodes("temp");
			foreach (UnitMeasure unitMeasure in unitMeasures)
			{
				TemperatureCombo.LJCAddItem(unitMeasure.ID, unitMeasure.Name);
			}
			Cursor = Cursors.Default;
		}

		// Configure the initial control settings.
		private void ConfigureControls()
		{
			// Make sure lists scroll vertically and counter labels show.
			if (AutoScaleMode == AutoScaleMode.Font)
			{
				ContactIndexButton.Height = ContactNameText.Height;
				ContactIndexButton.Width = ContactIndexButton.Height + 1;
				ContactButton.Height = ContactNameText.Height;
				ContactButton.Width = ContactButton.Height + 1;
				RegisterDateButton.Height = RegisterDateMask.Height;
				RegisterDateButton.Width = RegisterDateButton.Height + 1;
				EnterDateButton.Height = EnterDateMask.Height;
				EnterDateButton.Width = EnterDateButton.Height + 1;
				ExitDateButton.Height = ExitDateMask.Height;
				ExitDateButton.Width = ExitDateButton.Height + 1;
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

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
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

		// Display the Contact Detail
		private void ContactIndexButton_Click(object sender, EventArgs e)
		{
			if (NetString.HasValue(ContactIndexText.Text))
			{
				int.TryParse(ContactIndexText.Text, out int id);
				if (id > 0)
				{
					var cvPerson = CVRManagers.CVPersonManager.RetrieveWithID(id);
					if (cvPerson != null)
					{
						CVPersonDetail dialog = new CVPersonDetail()
						{
							LJCID = cvPerson.ID
						};
						if (DialogResult.OK == dialog.ShowDialog())
						{
							if (dialog.LJCRecord != null
								&& dialog.LJCRecord.ID > 0)
							{
								mCVPersonID = dialog.LJCRecord.ID;
								ContactIndexText.Text = mCVPersonID.ToString();
								ContactNameText.Text = GetFullName(dialog.LJCRecord);
								RegisterDateMask.Text = DataCommon.GetUIDateString(DateTime.Now);
								RegisterTimeMask.Text = DataCommon.GetUITimeString(DateTime.Now);
								//ContactIndexText.ReadOnly = true;
								//ContactIndexButton.Enabled = false;
							}
						}
					}
				}
			}
		}

		// Displays the Contact List.
		private void ContactButton_Click(object sender, EventArgs e)
		{
			CVPersonList list = new CVPersonList()
			{
				LJCIsSelect = true
			};
			if (DialogResult.OK == list.ShowDialog())
			{
				mCVPersonID = list.LJCSelectedRecord.ID;
				ContactIndexText.Text = mCVPersonID.ToString();
				ContactNameText.Text = GetFullName(list.LJCSelectedRecord);
				RegisterDateMask.Text = DataCommon.GetUIDateString(DateTime.Now);
				RegisterTimeMask.Text = DataCommon.GetUITimeString(DateTime.Now);
			}
		}

		// Handles the Click event to show the Calendar control.
		private void RegisterDateButton_Click(object sender, EventArgs e)
		{
			RegisterDateMask.Text = ControlCommon.GetSelectedDate(RegisterDateMask.Text);
		}

		// Handles the Click event to show the Calendar control.
		private void EnterDateButton_Click(object sender, EventArgs e)
		{
			EnterDateMask.Text = ControlCommon.GetSelectedDate(EnterDateMask.Text);
		}

		// Handles the Click event to show the Calendar control.
		private void ExitDateButton_Click(object sender, EventArgs e)
		{
			ExitDateMask.Text = ControlCommon.GetSelectedDate(ExitDateMask.Text);
		}

		// Handles the Leave event.
		private void TemperatureText_Leave(object sender, EventArgs e)
		{
			string temperature = TemperatureText.Text.Trim();
			decimal.TryParse(temperature, out decimal temperatureValue);
			int temperatureUnitID = TemperatureCombo.LJCSelectedItemID();

			// Reset temperature values.
			if (temperature != mOriginalTemperature)
			{
				mOriginalTemperature = temperature;
				mBaseTemperature = temperature;
				if (temperatureUnitID != mBaseTemperatureUnitID)
				{
					mBaseTemperatureUnitID = temperatureUnitID;
				}
			}

			// Get setting temperature values.
			var unitMeasureManager = UnitMeasureManagers.UnitMeasureManager;
			var unitMeasure = unitMeasureManager.RetrieveWithID(temperatureUnitID);
			string temperatureCode = unitMeasure.Code;

			decimal settingHighValue = mSettingTemperatureHighValue;
			decimal settingLowValue = mSettingTemperatureLowValue;
			GetLimitSettings(unitMeasureManager, temperatureUnitID, ref settingHighValue
				, ref settingLowValue);
			if (temperatureValue >= settingHighValue)
			{
				MessageBox.Show($"Temperature {temperature}{temperatureCode} "
					+ "is too high.");
			}
			if (temperatureValue <= settingLowValue)
			{
				MessageBox.Show($"Temperature {temperature}{temperatureCode} "
					+ $"is too low.");
			}
		}

		// Handles the IndexChanged event.
		private void TemperatureCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mAllowChange)
			{
				string temperature = TemperatureText.Text.Trim();
				decimal.TryParse(temperature, out decimal temperatureValue);
				int temperatureUnitID = TemperatureCombo.LJCSelectedItemID();
				if (temperatureUnitID == mBaseTemperatureUnitID)
				{
					// Revert temperature to the base temperature.
					TemperatureText.Text = mBaseTemperature;
				}
				else
				{
					// Convert temperature to selected units.
					var unitMeasureManager = UnitMeasureManagers.UnitMeasureManager;
					//var unitMeasure = unitMeasureManager.RetrieveWithID(temperatureUnitID);
					var value = unitMeasureManager.ConvertUnit(mBaseTemperatureUnitID
						, temperatureUnitID, temperatureValue);
					TemperatureText.Text = value.ToString();
				}
			}
			mAllowChange = true;
		}

		// Gets the settings temperature limits.
		private void GetLimitSettings(UnitMeasureManager unitMeasureManager
			, int temperatureUnitID, ref decimal settingHighValue
			, ref decimal settingLowValue)
		{
			var settingUnitMeasure
				= unitMeasureManager.RetrieveWithCode(mSettingTemperatureUnitCode);
			if (settingUnitMeasure.ID != temperatureUnitID)
			{
				// Convert setting temperature values to temperature values.
				var value = unitMeasureManager.ConvertUnit(settingUnitMeasure.ID
					, temperatureUnitID, mSettingTemperatureHighValue);
				decimal.TryParse(value.ToString(), out settingHighValue);

				value = unitMeasureManager.ConvertUnit(settingUnitMeasure.ID
					, temperatureUnitID, mSettingTemperatureLowValue);
				decimal.TryParse(value.ToString(), out settingLowValue);
			}
		}
		#endregion

		#region KeyEdit Event Handlers

		// Only allows numbers or edit keys.
		private void TemperatureText_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
		}
		#endregion

		#region Properties

		// Gets or sets the primary ID value.
		internal long LJCID { get; set; }

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the LJCParentName value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets a reference to the record object.
		internal CVVisit LJCRecord { get; private set; }

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
		private CVRManagers CVRManagers { get; set; }

		// The Managers object.
		private UnitMeasureManagers UnitMeasureManagers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private string mSettingTemperatureUnitCode;
		private decimal mSettingTemperatureHighValue;
		private decimal mSettingTemperatureLowValue;

		// Foreign Keys
		private long mCVPersonID;
		private string mBaseTemperature;
		private int mBaseTemperatureUnitID;
		private string mOriginalTemperature;

		private bool mAllowChange;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}