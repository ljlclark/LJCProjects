// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PersonDetail.cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDataAccess;
using LJCGridDataLib;
using LJCFacilityManagerDAL;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	/// <summary>The Person detail dialog.</summary>
	public partial class PersonDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public PersonDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "PersonDetail.htm";

			LJCReadOnly = false;
		}
		#endregion

		#region Form Event Handlers

		// Handles the FormClosing event.
		private void PersonDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			PersonPicture.ReleaseResources();
		}

		// Handles the form keys.
		private void PersonDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
						, LJCHelpPageName);
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void PersonDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
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

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			Person record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				record = mPersonManager.RetrieveWithID(LJCID);
				//SetPersonUpdateColumns(record);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Person();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Person dataRecord)
		{
			if (dataRecord != null)
			{
				FirstNameTextbox.Text = dataRecord.FirstName;
				MiddleTextbox.Text = dataRecord.MiddleInitial;
				LastNameTextbox.Text = dataRecord.LastName;
				if (dataRecord.Sex == "F")
				{
					SexCombo.SelectedIndex = 1;
				}
				TypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);
				PrincipleCheckbox.Checked = dataRecord.PrincipleFlag;
				AddressToTextbox.Text = dataRecord.PrincipleTitle;
				BirthDateMask.Text = DataCommon.GetUIDateString(dataRecord.BirthDate);
				PhoneTextbox.Text = dataRecord.Phone;
				ExtensionTextbox.Text = dataRecord.Extension;
				CellTextbox.Text = dataRecord.CellPhone;
				FaxTextbox.Text = dataRecord.Fax;

				PersonPicture.LoadFromFile(PersonImageName());
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Person SetRecordValues()
		{
			Person retValue = new Person()
			{
				ID = LJCID,
				FirstName = FirstNameTextbox.Text,
				MiddleInitial = FormCommon.SetString(MiddleTextbox.Text),
				LastName = LastNameTextbox.Text.Trim(),
				Sex = SexCombo.Text == "Male" ? "M" : "F",
				CodeTypeID = TypeCombo.LJCGetSelectedItemID(),
				PrincipleFlag = PrincipleCheckbox.Checked,
				PrincipleTitle = FormCommon.SetString(AddressToTextbox.Text),
				BirthDate = DataCommon.GetDbDate(BirthDateMask.Text),
				Phone = FormCommon.SetString(PhoneTextbox.Text),
				Extension = FormCommon.SetString(ExtensionTextbox.Text),
				CellPhone = FormCommon.SetString(CellTextbox.Text),
				Fax = FormCommon.SetString(FaxTextbox.Text),

				// Get join display values.
				TypeDescription = TypeCombo.Text.Trim()
			};

			// Get additional join display values.
			CodeType codeType = TypeCombo.SelectedItem as CodeType;
			retValue.Code = codeType.Code;
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Person record)
		{
			record.MiddleInitial = FormCommon.SetString(record.MiddleInitial);
			record.PrincipleTitle = FormCommon.SetString(record.PrincipleTitle);
			record.Phone = FormCommon.SetString(record.Phone);
			record.Extension = FormCommon.SetString(record.Extension);
			record.CellPhone = FormCommon.SetString(record.CellPhone);
			record.Fax = FormCommon.SetString(record.Fax);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Person lookupRecord;
			DbColumns keyColumns;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			keyColumns = new DbColumns()
			{
				{ Person.ColumnFirstName, (object)LJCRecord.FirstName },
				{ Person.ColumnMiddleInitial, (object)LJCRecord.MiddleInitial },
				{ Person.ColumnLastName, (object)LJCRecord.LastName }
			};
			lookupRecord = mPersonManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "An individual with that name already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = mPersonManager.GetIDKey(LJCRecord.ID);
					mPersonManager.Update(LJCRecord, keyColumns, GetPersonUpdateColumnNames());
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Person addedRecord = mPersonManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ID = addedRecord.ID;
						Crypto crypto = new Crypto()
						{
							Password = "Temp",
							PersonID = Convert.ToInt32(LJCRecord.ID),
							IsAdministrator = false
						};
						LJCRecord.Password = crypto.Encrypt();
						List<string> propertyNames = new List<string>()
						{
							Person.ColumnPassword,
							Person.ColumnUserID
						};

						keyColumns = mPersonManager.GetIDKey(addedRecord.ID);
						mPersonManager.Update(LJCRecord, keyColumns, propertyNames);
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

			if (!NetString.HasValue(FirstNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {FirstNameLabel.Text}");
			}
			if (!NetString.HasValue(LastNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {LastNameLabel.Text}");
			}
			if (TypeCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {TypeLabel.Text}");
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

		#region Image Methods

		// Returns the default image file name for the current individual.
		private string PersonImageName()
		{
			string retValueue;

			string dateText = BirthDateMask.Text.Trim().Replace("/", "");
			retValueue = $"{FirstNameTextbox.Text.Trim()}{MiddleTextbox.Text.Trim()}"
				+ $"{LastNameTextbox.Text.Trim()}{dateText}.jpg";
			return retValueue;
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

			// Initialize Class Data.
			mPersonManager = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			NameLabel.BackColor = mSettings.BeginColor;
			FirstNameLabel.BackColor = mSettings.BeginColor;
			MiddleLabel.BackColor = mSettings.BeginColor;
			PrincipleCheckbox.BackColor = mSettings.BeginColor;
			LastNameLabel.BackColor = mSettings.BeginColor;
			SexLabel.BackColor = mSettings.BeginColor;
			TypeLabel.BackColor = mSettings.BeginColor;
			AddressToLabel.BackColor = mSettings.BeginColor;
			BirthDateLabel.BackColor = mSettings.BeginColor;
			PhoneLabel.BackColor = mSettings.BeginColor;
			ExtensionLabel.BackColor = mSettings.BeginColor;
			CellLabel.BackColor = mSettings.BeginColor;
			FaxLabel.BackColor = mSettings.BeginColor;

			FirstNameTextbox.MaxLength = 45;
			MiddleTextbox.MaxLength = 1;
			LastNameTextbox.MaxLength = 45;
			PhoneTextbox.MaxLength = 18;
			ExtensionTextbox.MaxLength = 4;
			CellTextbox.MaxLength = 18;
			FaxTextbox.MaxLength = 18;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			TypeCombo.LJCInit();
			TypeCombo.LJCLoad(manager.GetCodeClassID("Person"));

			SexCombo.Items.Add("Male");
			SexCombo.Items.Add("Female");
			SexCombo.SelectedIndex = 0;

			// Set control layout.
			if (NetString.HasValue(mName))
			{
				NameLabel.Text = mLabelText;
				NameTextbox.Text = mName;
			}
			else
			{
				NameLabel.Visible = false;
				NameTextbox.Visible = false;
				Height -= 26;
			}
			SetReadOnly();
			Cursor = Cursors.Default;
		}

		// 
		private List<string> GetPersonUpdateColumnNames()
		{
			List<string> retValue = new List<string>()
			{
				Person.ColumnFirstName, Person.ColumnMiddleInitial,
				Person.ColumnLastName, Person.ColumnSex,
				Person.ColumnCodeTypeID,  Person.ColumnPrincipleFlag,
				Person.ColumnPrincipleTitle, Person.ColumnBirthDate,
				Person.ColumnPhone, Person.ColumnExtension,
				Person.ColumnCellPhone, Person.ColumnFax
			};
			return retValue;
		}

		//// 
		//private void SetPersonUpdateColumns(Person person = null)
		//{
		//	if (person != null)
		//	{
		//		ResultGridData resultGridData = new ResultGridData();
		//		var gridColumns = resultGridData.GetGridColumns(person
		//			, GetPersonUpdateColumnNames());
		//		//mPersonGridColumns = gridColumns;
		//	}
		//}

		// Sets all applicable controls to read-only or disabled.
		private void SetReadOnly()
		{
			if (LJCReadOnly)
			{
				FirstNameTextbox.ReadOnly = true;
				MiddleTextbox.ReadOnly = true;
				PrincipleCheckbox.Enabled = false;
				LastNameTextbox.ReadOnly = true;
				TypeCombo.Enabled = false;
				AddressToTextbox.ReadOnly = true;
				BirthDateMask.ReadOnly = true;
				BirthDateButton.Enabled = false;
				PhoneTextbox.ReadOnly = true;
				ExtensionTextbox.ReadOnly = true;
				CellTextbox.ReadOnly = true;
				FaxTextbox.ReadOnly = true;
				OKButton.Visible = false;
				FormCancelButton.Text = "&Close";
			}
		}
		#endregion

		#region Control Event Handlers

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

		///// <summary>
		///// 
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void Password_Click(object sender, EventArgs e)
		//{
		//	Password detail;

		//	detail = new Password()
		//	{
		//		LJCPersonID = mID
		//	};
		//	detail.ShowDialog();
		//}

		// Retrieve date from displayed calendar control.
		private void BirthDateButton_Click(object sender, EventArgs e)
		{
			BirthDateMask.Text = ControlCommon.GetSelectedDate(BirthDateMask.Text);
		}

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Action Event Handlers

		// Show the help page.
		private void DialogMenuHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
				, LJCHelpPageName);
		}

		// Displays a file selection dialog and loads the selected image.
		private void PictureMenuImport_Click(object sender, EventArgs e)
		{
			PersonPicture.SelectImageFile();
		}

		// Rotates the image 90 degrees left.
		private void PictureMenuRotateLeft_Click(object sender, EventArgs e)
		{
			PersonPicture.RotateLeft();
		}

		// Rotates the image 90 degrees right.
		private void PictureMenuRotateRight_Click(object sender, EventArgs e)
		{
			PersonPicture.RotateRight();
		}

		// Indicates that a crop action is allowed.
		private void PictureMenuCrop_Click(object sender, EventArgs e)
		{
			PersonPicture.AllowCrop = true;
		}

		// Saves the image to a file.
		private void PictureMenuSave_Click(object sender, EventArgs e)
		{
			PersonPicture.SaveImageFile(PersonImageName());
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets or sets the Parent ID value.</summary>
		public int LJCParentID { get; set; }

		/// <summary>Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		/// <summary>Gets a reference to the record object.</summary>
		public Person LJCRecord { get; private set; }

		/// <summary>Gets or sets the LJCHelpFileName value.</summary>
		public string LJCHelpFileName
		{
			get { return mHelpFileName; }
			set { mHelpFileName = NetString.InitString(value); }
		}
		private string mHelpFileName;

		/// <summary>Gets or sets the LJCHelpPageName value.</summary>
		public string LJCHelpPageName
		{
			get { return mHelpPageName; }
			set { mHelpPageName = NetString.InitString(value); }
		}
		private string mHelpPageName;

		/// <summary>Gets or sets the Name value.</summary>
		public string LJCName
		{
			get { return mName; }
			set { mName = NetString.InitString(value); }
		}
		private string mName;

		/// <summary>Gets or sets the LabelText value.</summary>
		public string LJCLabelText
		{
			get { return mLabelText; }
			set { mLabelText = NetString.InitString(value); }
		}
		private string mLabelText;

		/// <summary>Gets the LJCReadOnly value.</summary>
		public bool LJCReadOnly { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private PersonManager mPersonManager;
		//private DbColumns mPersonGridColumns;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
