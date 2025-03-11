// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// BusinessDetail.cs
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
	// <summary>The Business detail dialog.</summary>
	/// <include path='items/BusinessDetail/*' file='Doc/BusinessDetail.xml'/>
	public partial class BusinessDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public BusinessDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "BusinessDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void BusinessDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void BusinessDetail_KeyDown(object sender, KeyEventArgs e)
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
			Business record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ Business.ColumnID, LJCID }
				};
				record = mBusinessManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Business();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Business dataRecord)
		{
			if (dataRecord != null)
			{
				NameTextbox.Text = dataRecord.Name;
				DescriptionTextbox.Text = dataRecord.Description;
				TypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);
				EffectiveMask.Text = DataCommon.GetUIDateString(dataRecord.EffectiveDate);
				ExpirationMask.Text = DataCommon.GetUIDateString(dataRecord.ExpirationDate);
				PhoneTextbox.Text = dataRecord.Phone;
				ExtensionTextbox.Text = dataRecord.Extension;
				FaxTextbox.Text = dataRecord.Fax;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Business SetRecordValues()
		{
			Business retValue = new Business()
			{
				ID = LJCID,
				Name = NameTextbox.Text,
				Description = FormCommon.SetString(DescriptionTextbox.Text),
				CodeTypeID = TypeCombo.LJCGetSelectedItemID(),
				EffectiveDate = DataCommon.GetDbDate(EffectiveMask.Text),
				ExpirationDate = DataCommon.GetDbDate(ExpirationMask.Text),
				Phone = FormCommon.SetString(PhoneTextbox.Text),
				Extension = FormCommon.SetString(ExtensionTextbox.Text),
				Fax = FormCommon.SetString(FaxTextbox.Text.Trim()),
				TypeDescription = TypeCombo.Text
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Business record)
		{
			record.Description = FormCommon.SetString(record.Description);
			record.Phone = FormCommon.SetString(record.Phone);
			record.Extension = FormCommon.SetString(record.Extension);
			record.Fax = FormCommon.SetString(record.Fax);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Business lookupRecord;
			string message;
			string title;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ Business.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mBusinessManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The record already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ Business.ColumnID, LJCRecord.ID }
					};
					mBusinessManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Business addedRecord = mBusinessManager.Add(LJCRecord);
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

			if (NetString.HasValue(NameTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {NameLabel.Text}");
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
			mBusinessManager = new BusinessManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			NameLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			TypeLabel.BackColor = mSettings.BeginColor;
			EffectiveLabel.BackColor = mSettings.BeginColor;
			ExpirationLabel.BackColor = mSettings.BeginColor;
			PhoneLabel.BackColor = mSettings.BeginColor;
			ExtensionLabel.BackColor = mSettings.BeginColor;
			FaxLabel.BackColor = mSettings.BeginColor;

			NameTextbox.MaxLength = 60;
			DescriptionTextbox.MaxLength = 60;
			PhoneTextbox.MaxLength = 18;
			ExtensionTextbox.MaxLength = 4;
			FaxTextbox.MaxLength = 18;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			TypeCombo.LJCInit();
			TypeCombo.LJCLoad(manager.GetCodeClassID("Business"));

			// Set control layout.
			if (NetString.HasValue(mParentName))
			{
				ParentNameTextbox.Text = mParentName;
			}
			else
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				Height -= 26;
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

		// Fires the Change event.
		/// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		protected void LJCOnChange()
		{
			LJCChange?.Invoke(this, new EventArgs());
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
		public Business LJCRecord { get; private set; }

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
		private BusinessManager mBusinessManager;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
