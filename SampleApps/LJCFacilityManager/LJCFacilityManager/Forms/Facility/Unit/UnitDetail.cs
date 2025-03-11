// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	/// <summary>The Unit detail dialog.</summary>
	public partial class UnitDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "UnitDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UnitDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void UnitDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
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
			Unit record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				record = mUnitManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new Unit();
				ParentNameTextbox.Text = DataCommonFacility.GetFacilityText(mFacilityManager, LJCParentID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Unit dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.FacilityID;
				ParentNameTextbox.Text = DataCommonFacility.GetFacilityText(mFacilityManager, LJCParentID);
				CodeTextbox.Text = dataRecord.Code;
				DescriptionTextbox.Text = dataRecord.Description;
				UnitTypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);
				if (dataRecord.Beds > 0)
				{
					BedsCombo.SelectedIndex = dataRecord.Beds;
				}
				if (dataRecord.Baths > 0)
				{
					BathsCombo.SelectedIndex = dataRecord.Baths;
				}
				PhoneTextbox.Text = dataRecord.Phone;
				ExtensionTextbox.Text = dataRecord.Extension;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Unit SetRecordValues()
		{
			Unit retValue = new Unit()
			{
				ID = LJCID,
				FacilityID = LJCParentID,
				Code = FormCommon.SetString(CodeTextbox.Text),
				Description = DescriptionTextbox.Text,
				CodeTypeID = UnitTypeCombo.LJCGetSelectedItemID(),
				Beds = Convert.ToInt16(BedsCombo.Text),
				Baths = Convert.ToInt16(BathsCombo.Text),
				Phone = FormCommon.SetString(PhoneTextbox.Text),
				Extension = FormCommon.SetString(ExtensionTextbox.Text),
				TypeDescription = UnitTypeCombo.Text
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Unit record)
		{
			record.Code = FormCommon.SetString(record.Code);
			record.Phone = FormCommon.SetString(record.Phone);
			record.Extension = FormCommon.SetString(record.Extension);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Unit lookupRecord;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			if (NetString.HasValue(LJCRecord.Code))
			{
				lookupRecord = mUnitManager.RetrieveWithCode(LJCRecord.Code);
				if (lookupRecord != null
					&& (LJCIsUpdate == false
					|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
				{
					retValue = false;
					title = "Data Entry Error";
					message = "A duplicate code already exists.";
					MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			lookupRecord = mUnitManager.RetrieveWithLookup(LJCRecord.Description);
			if (lookupRecord != null
				&& (LJCIsUpdate == false
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate description already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					var keyColumns = mUnitManager.GetIDKey(LJCRecord.ID);
					mUnitManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Unit addedRecord = mUnitManager.Add(LJCRecord);
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
			if (NetString.HasValue(DescriptionTextbox.Text) == false)
			{
				retValue = false;
				builder.AppendLine($"  {DescriptionLabel.Text}");
			}
			if (UnitTypeCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {UnitTypeLabel.Text}");
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
			mUnitManager = new UnitManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mFacilityManager = new FacilityDbManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			CodeLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			UnitTypeLabel.BackColor = mSettings.BeginColor;
			BedsLabel.BackColor = mSettings.BeginColor;
			BathsLabel.BackColor = mSettings.BeginColor;
			PhoneLabel.BackColor = mSettings.BeginColor;
			ExtensionLabel.BackColor = mSettings.BeginColor;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			UnitTypeCombo.LJCInit();
			UnitTypeCombo.LJCLoad(manager.GetCodeClassID("Unit"));

			for (int beds = 0; beds <= 15; beds++)
			{
				BedsCombo.Items.Add(beds.ToString());
			}
			BedsCombo.SelectedIndex = 0;
			for (int baths = 0; baths <= 10; baths++)
			{
				BathsCombo.Items.Add(baths.ToString());
				BathsCombo.SelectedIndex = 0;
			}
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
		public Unit LJCRecord { get; private set; }

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
		private UnitManager mUnitManager;
		private FacilityDbManager mFacilityManager;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
