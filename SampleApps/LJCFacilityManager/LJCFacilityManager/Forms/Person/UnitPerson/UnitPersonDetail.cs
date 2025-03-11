// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitPersonDetail.cs
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
	/// <summary>The UnitPerson detail dialog.</summary>
	public partial class UnitPersonDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitPersonDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCIsUpdate = false;
			LJCRecord = null;
			LJCHelpFileName = "Facilitymanager.chm";
			LJCHelpPageName = "PersonUnitDetail.htm";
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UnitPersonDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;

			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		//// Handles the form keys.
		private void UnitPersonDetail_KeyDown(object sender, KeyEventArgs e)
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
			UnitPerson record;

			Cursor = Cursors.WaitCursor;
			if (LJCUnitID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ UnitPerson.ColumnUnitID, LJCUnitID },
					{ UnitPerson.ColumnPersonID, LJCPersonID }
				};
				record = mUnitPersonManager.Retrieve(keyColumns);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new UnitPerson()
				{
					PersonID = LJCPersonID
				};
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager2, LJCPersonID);
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(UnitPerson dataRecord)
		{
			Unit unitRecord;

			if (dataRecord != null)
			{
				ParentNameTextbox.Text = DataCommonFacility.GetPersonName(mPersonManager2, dataRecord.PersonID);

				mOriginalUnitID = dataRecord.UnitID;
				var keyColumns = new DbColumns()
				{
					{ Unit.ColumnID, mOriginalUnitID }
				};
				unitRecord = mUnitManager.Retrieve(keyColumns);
				mOriginalFacilityID = unitRecord.FacilityID;

				FacilityCombo.LJCSetSelectedItem(mOriginalFacilityID);
				UnitCombo.LJCSetSelectedItem(mOriginalUnitID);
				BeginMask.Text = DataCommon.GetUIDateString(dataRecord.BeginDate);
				EndMask.Text = DataCommon.GetUIDateString(dataRecord.EndDate);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private UnitPerson SetRecordValues()
		{
			Unit unitRecord;
			UnitPerson retValue = new UnitPerson();

			unitRecord = UnitCombo.LJCGetSelectedItem() as Unit;
			retValue.UnitID = unitRecord.ID;
			retValue.PersonID = LJCPersonID;
			retValue.BeginDate = DataCommon.GetDbDate(BeginMask.Text);
			retValue.EndDate = DataCommon.GetDbDate(EndMask.Text);

			// Set display values.
			retValue.UnitDescription = UnitCombo.Text.Trim();
			return retValue;
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			UnitPerson lookupRecord;
			DbColumns keyColumns;
			string title;
			string message;
			bool retValue = true;

			LJCRecord = SetRecordValues();

			keyColumns = new DbColumns()
			{
				{ UnitPerson.ColumnUnitID, LJCRecord.UnitID },
				{ UnitPerson.ColumnPersonID, LJCRecord.PersonID }
			};
			lookupRecord = mUnitPersonManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && (lookupRecord.UnitID != LJCRecord.UnitID
				&& lookupRecord.PersonID != LJCRecord.PersonID))))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "A duplicate entry already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ UnitPerson.ColumnUnitID, mOriginalUnitID },
						{ UnitPerson.ColumnPersonID, LJCRecord.PersonID }
					};
					mUnitPersonManager.Update(LJCRecord, keyColumns);
				}
				else
				{
					mUnitPersonManager.Add(LJCRecord);
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

			if (!NetString.HasValue(ParentNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {ParentNameLabel.Text}");
			}
			if (UnitCombo.LJCGetSelectedItemID() == -1)
			{
				retValue = false;
				builder.AppendLine($"  {UnitLabel.Text}");
			}
			if (!BeginMask.MaskCompleted
				|| "/  /" == BeginMask.Text.Trim())
			{
				retValue = false;
				builder.AppendLine($"  {BeginLabel.Text}");
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
			mUnitPersonManager = new UnitPersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mUnitManager = new UnitManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mPersonManager2 = new PersonManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = mSettings.BeginColor;
			LocationGroup.BackColor = mSettings.BeginColor;
			FacilityLabel.BackColor = mSettings.BeginColor;
			UnitLabel.BackColor = mSettings.BeginColor;
			BeginLabel.BackColor = mSettings.BeginColor;
			EndLabel.BackColor = mSettings.BeginColor;

			// Load control data.
			FacilityCombo.LJCInit(mSettings.ConnectionString);
			FacilityCombo.LJCLoad();

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

		// 
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

		// 
		private void BeginButton_Click(object sender, EventArgs e)
		{
			BeginMask.Text = ControlCommon.GetSelectedDate(BeginMask.Text);
		}

		// 
		private void EndButton_Click(object sender, EventArgs e)
		{
			EndMask.Text = ControlCommon.GetSelectedDate(EndMask.Text);
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the UnitID value.</summary>
		public int LJCUnitID { get; set; }

		/// <summary>Gets or sets the PersonID value.</summary>
		public int LJCPersonID { get; set; }

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		/// <summary>Gets a reference to the record object.</summary>
		public UnitPerson LJCRecord { get; private set; }

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
		private UnitPersonManager mUnitPersonManager;
		private UnitManager mUnitManager;
		private PersonManager mPersonManager2;

		private int mOriginalFacilityID;
		private int mOriginalUnitID;

		/// <summary>The change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
