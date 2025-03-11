// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AccountDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDataAccess;
using LJCFacilityManagerDAL;
using LJCWinFormControls;

namespace LJCFacilityManager
{
	// The Account detail dialog.
	/// <include path='items/AccountDetail/*' file='../Doc/AccountDetail.xml'/>
	public partial class AccountDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public AccountDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "AccountDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void AccountDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void AccountDetail_KeyDown(object sender, KeyEventArgs e)
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

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, mSettings.BeginColor, mSettings.EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			Account record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ Account.ColumnID, LJCID }
				};
				record = mAccountManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Account();
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager, LJCParentID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Account dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.PersonID;
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager, LJCParentID);
				mBusinessID = dataRecord.BusinessID;
				BusinessTextbox.Text = DataCommonFacility.GetBusinessName(mBusinessManager, dataRecord.BusinessID);
				DescriptionTextbox.Text = dataRecord.Description;
				IDNumberTextbox.Text = dataRecord.IDNumber;
				GroupTextbox.Text = dataRecord.GroupNumber;
				PlanTextbox.Text = dataRecord.PlanNumber;
				EffectiveMask.Text = DataCommon.GetUIDateString(dataRecord.EffectiveDate);
				ExpirationMask.Text = DataCommon.GetUIDateString(dataRecord.ExpirationDate);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Account SetRecordValues()
		{
			Account retValue = new Account()
			{
				ID = LJCID,
				PersonID = LJCParentID,
				BusinessID = mBusinessID,
				Description = DescriptionTextbox.Text,
				IDNumber = FormCommon.SetString(IDNumberTextbox.Text),
				GroupNumber = FormCommon.SetString(GroupTextbox.Text),
				PlanNumber = FormCommon.SetString(PlanTextbox.Text),
				EffectiveDate = DataCommon.GetDbDate(EffectiveMask.Text),
				ExpirationDate = DataCommon.GetDbDate(ExpirationMask.Text),

				// Set display values.
				Name = BusinessTextbox.Text.Trim()
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Account record)
		{
			record.IDNumber = FormCommon.SetString(record.IDNumber);
			record.GroupNumber = FormCommon.SetString(record.GroupNumber);
			record.PlanNumber = FormCommon.SetString(record.PlanNumber);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Account lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();
			var keyColumns = new DbColumns()
			{
				{ Account.ColumnPersonID, LJCRecord.PersonID },
				{ Account.ColumnDescription, (object)LJCRecord.Description }
			};
			lookupRecord = mAccountManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate account already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ Account.ColumnID, LJCRecord.ID }
					};
					mAccountManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Account addedRecord = mAccountManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.ID = addedRecord.ID;
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

			if (LJCParentID == 0)
			{
				retValue = false;
				builder.AppendLine($"  {ParentNameLabel.Text}");
			}
			if (mBusinessID == 0)
			{
				retValue = false;
				builder.AppendLine($"  {BusinessLabel.Text}");
			}
			if (!NetString.HasValue(DescriptionTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {DescriptionLabel.Text}");
			}

			if (!retValue)
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

			// Initialize Class Data.
			mAccountManager = new AccountManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mPersonManager = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mBusinessManager = new BusinessManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			BusinessLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			IDNumberLabel.BackColor = mSettings.BeginColor;
			GroupLabel.BackColor = mSettings.BeginColor;
			PlanLabel.BackColor = mSettings.BeginColor;
			EffectiveLabel.BackColor = mSettings.BeginColor;
			ExpirationLabel.BackColor = mSettings.BeginColor;

			DescriptionTextbox.MaxLength = 60;
			IDNumberTextbox.MaxLength = 25;
			GroupTextbox.MaxLength = 25;
			PlanTextbox.MaxLength = 25;
			Cursor = Cursors.Default;
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

		// Saves the data and closes the form.
		private void OKButton_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			if (IsValid()
				&& DataSave())
			{
				LJCOnChange();
				DialogResult = DialogResult.OK;
			}
			Cursor = Cursors.Default;
		}

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		// Retrieve date from displayed calendar control.
		private void EffectiveButton_Click(object sender, EventArgs e)
		{
			EffectiveMask.Text = ControlCommon.GetSelectedDate(EffectiveMask.Text);
		}

		// Retrieve date from displayed calendar control.
		private void ExpirationButton_Click(object sender, EventArgs e)
		{
			ExpirationMask.Text = ControlCommon.GetSelectedDate(ExpirationMask.Text);
		}

		// Displays the Business Select List.
		private void BusinessButton_Click(object sender, EventArgs e)
		{
			BusinessList businessList;
			Business businessRecord = null;

			// Get current record to seed the selection list.
			// #002 Begin - Add
			if (mBusinessID > 0)
			{
				var keyColumns = new DbColumns()
				{
					{ Business.ColumnID, mBusinessID }
				};
				businessRecord = mBusinessManager.Retrieve(keyColumns);
			}
			// #002 End - Add

			businessList = new BusinessList()
			{
				LJCIsSelect = true,
				// #002 Next Statement - Add
				LJCSelectedRecord = businessRecord
			};
			businessList.ShowDialog();
			if (businessList.DialogResult == DialogResult.OK
				&& businessList.LJCSelectedRecord != null)
			{
				businessRecord = businessList.LJCSelectedRecord;
				mBusinessID = businessRecord.ID;
				BusinessTextbox.Text = businessRecord.Name;
			}
		}

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
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
		public Account LJCRecord { get; private set; }

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
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private AccountManager mAccountManager;
		private PersonManager mPersonManager;
		private BusinessManager mBusinessManager;

		private int mBusinessID;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
