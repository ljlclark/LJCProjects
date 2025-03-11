// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FixtureDetail.cs
using System;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCFacilityManagerDAL;

namespace LJCFacilityManager
{
	/// <summary>The Fixture detail dialog.</summary>
	public partial class FixtureDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public FixtureDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "FacilityManager.chm";
			LJCHelpPageName = "FixtureDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void FixtureDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void FixtureDetail_KeyDown(object sender, KeyEventArgs e)
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
			Fixture record;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ Fixture.ColumnID, LJCID }
				};
				record = mFixtureManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				ParentNameTextbox.Text = DataCommonFacility.GetUnitText(mUnitManager, LJCParentID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(Fixture dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.UnitID;
				ParentNameTextbox.Text = DataCommonFacility.GetUnitText(mUnitManager, LJCParentID);
				CodeTextbox.Text = dataRecord.Code;
				DescriptionTextbox.Text = dataRecord.Description;
				FixtureTypeCombo.LJCSetSelectedItem(dataRecord.CodeTypeID);
				MakeTextbox.Text = dataRecord.Make;
				ModelTextbox.Text = dataRecord.Model;
				SerialNumberTextbox.Text = dataRecord.SerialNumber;
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private Fixture SetRecordValues()
		{
			Fixture retValue = new Fixture()
			{
				ID = LJCID,
				UnitID = LJCParentID,
				Code = FormCommon.SetString(CodeTextbox.Text),
				Description = DescriptionTextbox.Text,
				CodeTypeID = FixtureTypeCombo.LJCGetSelectedItemID(),
				TypeDescription = FixtureTypeCombo.Text,
				Make = FormCommon.SetString(MakeTextbox.Text),
				Model = FormCommon.SetString(ModelTextbox.Text),
				SerialNumber = FormCommon.SetString(SerialNumberTextbox.Text)
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(Fixture record)
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
			Fixture lookupRecord;
			DbColumns keyColumns;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			if (NetString.HasValue(LJCRecord.Code))
			{
				keyColumns = new DbColumns()
				{
					{ Fixture.ColumnCode, (object)LJCRecord.Code }
				};
				lookupRecord = mFixtureManager.Retrieve(keyColumns);
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
				{ Fixture.ColumnDescription, (object)LJCRecord.Description }
			};
			lookupRecord = mFixtureManager.Retrieve(keyColumns);
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
						{ Fixture.ColumnID, (object)LJCRecord.ID }
					};
					mFixtureManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					Fixture addedRecord = mFixtureManager.Add(LJCRecord);
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
			if (FixtureTypeCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {FixtureTypeLabel.Text}");
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
			mFixtureManager = new FixtureManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mUnitManager = new UnitManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			CodeLabel.BackColor = mSettings.BeginColor;
			DescriptionLabel.BackColor = mSettings.BeginColor;
			FixtureTypeLabel.BackColor = mSettings.BeginColor;
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
			FixtureTypeCombo.LJCInit();
			FixtureTypeCombo.LJCLoad(manager.GetCodeClassID("Fixture"));
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
		public Fixture LJCRecord { get; private set; }

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
		private FixtureManager mFixtureManager;
		private UnitManager mUnitManager;

		/// <summary>The change event.</summary>
		/// <remarks><para>Syntax: public event EventHandler&lt;EventArgs&gt; Change
		/// </para></remarks>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
