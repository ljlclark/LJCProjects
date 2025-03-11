// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// EquipmentDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	// The Equipment detail dialog.
	/// <include path='items/EquipmentDetail/*' file='../Doc/EquipmentDetail.xml'/>
	public partial class EquipmentDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public EquipmentDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "EquipmentDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void EquipmentDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			base.CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void EquipmentDetail_KeyDown(object sender, KeyEventArgs e)
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
			Equipment record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ Equipment.ColumnID, LJCID }
				};
				record = mEquipmentManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Equipment dataRecord)
		{
			Unit unitRecord;

			if (dataRecord != null)
			{
				CodeTextbox.Text = dataRecord.Code;
				DescriptionTextbox.Text = dataRecord.Description;
				EquipmentTypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);

				unitRecord = mUnitManager.RetrieveWithID(dataRecord.UnitID);
				if (unitRecord != null)
				{
					FacilityCombo.LJCSetSelectedItem(unitRecord.FacilityID);
				}
				UnitCombo.LJCSetSelectedItem(dataRecord.UnitID);

				MakeTextbox.Text = dataRecord.Make;
				ModelTextbox.Text = dataRecord.Model;
				SerialNumberTextbox.Text = dataRecord.SerialNumber;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Equipment SetRecordValues()
		{
			Equipment retValue = new Equipment()
			{
				ID = LJCID,
				Code = FormCommon.SetString(CodeTextbox.Text),
				Description = DescriptionTextbox.Text,
				CodeTypeID = EquipmentTypeCombo.LJCGetSelectedItemID(),
				UnitID = UnitCombo.LJCGetSelectedItemID(),
				Make = FormCommon.SetString(MakeTextbox.Text),
				Model = FormCommon.SetString(ModelTextbox.Text),
				SerialNumber = FormCommon.SetString(SerialNumberTextbox.Text),

				// Get join display values.
				TypeDescription = EquipmentTypeCombo.Text,
				UnitDescription = UnitCombo.Text
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Equipment record)
		{
			record.Code = FormCommon.SetString(record.Code);
			record.Make = FormCommon.SetString(record.Make);
			record.Model = FormCommon.SetString(record.Model);
			record.SerialNumber = FormCommon.SetString(record.SerialNumber);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			Equipment lookupRecord;
			DbColumns keyColumns;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			if (NetString.HasValue(LJCRecord.Code))
			{
				keyColumns = new DbColumns()
				{
					{ Equipment.ColumnCode, (object)LJCRecord.Code }
				};
				lookupRecord = mEquipmentManager.Retrieve(keyColumns);
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

			keyColumns = new DbColumns()
			{
				{ Equipment.ColumnDescription, (object)LJCRecord.Description }
			};
			lookupRecord = mEquipmentManager.Retrieve(keyColumns);
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
					keyColumns = new DbColumns()
					{
						{ Equipment.ColumnID, LJCRecord.ID }
					};
					mEquipmentManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					//Equipment addedRecord = mEquipmentManager.Add(LJCRecord);
          mEquipmentManager.Add(LJCRecord);
          ResetRecordValues(LJCRecord);
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

			if (!NetString.HasValue(DescriptionTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {DescriptionLabel.Text}");
			}
			if (UnitCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {UnitLabel.Text}");
			}
			if (EquipmentTypeCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {EquipmentTypeLabel.Text}");
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
			mEquipmentManager = new EquipmentManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mUnitManager = new UnitManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			CodeLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			LocationGroup.BackColor = mSettings.BeginColor;
			FacilityLabel.BackColor = mSettings.BeginColor;
			UnitLabel.BackColor = mSettings.BeginColor;
			MakeLabel.BackColor = mSettings.BeginColor;
			ModelLabel.BackColor = mSettings.BeginColor;
			SerialNumberLabel.BackColor = mSettings.BeginColor;

			CodeTextbox.MaxLength = 25;
			DescriptionTextbox.MaxLength = 60;
			MakeTextbox.MaxLength = 25;
			ModelTextbox.MaxLength = 25;
			SerialNumberTextbox.MaxLength = 25;

			// Load control data.
			CodeTypeClassManager manager
				= new CodeTypeClassManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			EquipmentTypeCombo.LJCInit();
			EquipmentTypeCombo.LJCLoad(manager.GetCodeClassID("Equipment"));

			FacilityCombo.LJCInit(mSettings.ConnectionString);
			FacilityCombo.LJCLoad();
			FacilityCombo.Items.Insert(0, "");

			UnitCombo.LJCInit(mSettings.ConnectionString);
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

		// Handles the Combo SelectedIndex changed.
		private void FacilityCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			Facility record;

			record = FacilityCombo.LJCGetSelectedItem();
			if (record == null)
			{
				UnitCombo.Items.Clear();
			}
			else
			{
				UnitCombo.LJCLoad(record.ID);
				UnitCombo.Items.Insert(0, "");
			}
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
		public Equipment LJCRecord { get; private set; }

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
		private EquipmentManager mEquipmentManager;
		private UnitManager mUnitManager;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
