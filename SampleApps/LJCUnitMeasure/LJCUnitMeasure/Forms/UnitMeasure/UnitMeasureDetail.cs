// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCUnitMeasureDAL;

namespace LJCUnitMeasure
{
	/// <summary>The UnitMeasure detail dialog.</summary>
	public partial class UnitMeasureDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
		public UnitMeasureDetail(UnitMeasureManagers managers)
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCUnitSystemID = 0;
			LJCUnitCategoryID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;
			LJCHelpFileName = "UnitMeasure.chm";
			LJCHelpPageName = "UnitMeasureDetail.html";
			Managers = managers;

			// Set default class data.
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UnitMeasureDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void UnitMeasureDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
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
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCDocLib/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			UnitMeasure dataRecord;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ UnitMeasure.ColumnID, LJCID }
				};
				dataRecord = Managers.UnitMeasureManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
			}
			else
			{
				LJCIsUpdate = false;

				// Set Type combos.
				SystemCombo.LJCSetByItemID(LJCUnitSystemID);
				CategoryCombo.LJCSetByItemID(LJCUnitCategoryID);

				// Set default values.
				LJCRecord = new UnitMeasure();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(UnitMeasure dataRecord)
		{
			if (dataRecord != null)
			{
				// Set Type combos.
				SystemCombo.LJCSetByItemID(dataRecord.UnitSystemID);
				CategoryCombo.LJCSetByItemID(dataRecord.UnitCategoryID);

				CodeText.Text = dataRecord.Code;
				NameText.Text = dataRecord.Name;
				AltNameText.Text = dataRecord.AltName;
				SequenceText.Text = dataRecord.Sequence.ToString();
				DescriptionText.Text = dataRecord.Description;
			}
		}

		// Creates and returns a record object with the data from
		private UnitMeasure SetRecordValues()
		{
			UnitMeasure retValue = new UnitMeasure()
			{
				ID = LJCID,
				UnitSystemID = SystemCombo.LJCSelectedItemID(),
				UnitCategoryID = CategoryCombo.LJCSelectedItemID(),
				Code = CodeText.Text.Trim(),
				Name = NameText.Text.Trim(),
				AltName = AltNameText.Text.Trim(),
				Description = FormCommon.SetString(DescriptionText.Text),
			};
			int.TryParse(SequenceText.Text, out int sequence);
			retValue.Sequence = sequence;
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			UnitMeasure lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ UnitMeasure.ColumnName, (object)LJCRecord.Name }
			};
			var measureManager = Managers.UnitMeasureManager;
			lookupRecord = measureManager.Retrieve(keyColumns);
			if (measureManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ UnitMeasure.ColumnID, LJCRecord.ID }
					};
					measureManager.Update(LJCRecord, keyColumns);
					if (0 == Managers.UnitMeasureManager.AffectedCount)
					{
						title = "Update Error";
						message = "The Record was not updated.";
						MessageBox.Show(message, title, MessageBoxButtons.OK
							, MessageBoxIcon.Information);
					}
				}
				else
				{
					var addedRecord = measureManager.Add(LJCRecord);
					if (null == addedRecord)
					{
						title = "Add Error";
						message = "The Record was not added.";
						MessageBox.Show(message, title, MessageBoxButtons.OK
							, MessageBoxIcon.Information);
					}
					else
					{
						LJCRecord.ID = addedRecord.ID;
					}
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

			if (false == NetString.HasValue(CodeText.Text))
			{
				retValue = false;
				builder.AppendLine($"  {CodeLabel.Text}");
			}
			if (false == NetString.HasValue(NameText.Text))
			{
				retValue = false;
				builder.AppendLine($"  {NameLabel.Text}");
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

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			Cursor = Cursors.WaitCursor;
			var values = ValuesUnitMeasure.Instance;
			mSettings = values.StandardSettings;

			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			mUnitSystemComboCode = new UnitSystemComboCode(Managers)
			{
				UnitSystemCombo = SystemCombo
			};
			mUnitCategoryComboCode = new UnitCategoryComboCode(Managers)
			{
				UnitCategoryCombo = CategoryCombo
			};

			// Set control values.
			SystemLabel.BackColor = BeginColor;
			CategoryLabel.BackColor = BeginColor;
			CodeLabel.BackColor = BeginColor;
			NameLabel.BackColor = BeginColor;
			AltNameLabel.BackColor = BeginColor;
			SequenceLabel.BackColor = BeginColor;
			DescriptionLabel.BackColor = BeginColor;

			CodeText.MaxLength = UnitMeasure.LengthName;
			NameText.MaxLength = UnitMeasure.LengthDescription;
			AltNameText.MaxLength = UnitMeasure.LengthAltName;
			DescriptionText.MaxLength = UnitMeasure.LengthDescription;

			// Load control data.
			mUnitSystemComboCode.LoadCombo();
			mUnitCategoryComboCode.LoadCombo();
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

		// Closes the form without saving the data.
		private void FormCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region KeyEdit Event Handlers

		// Only allows numbers or edit keys.
		private void SequenceText_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
		}

		// Does not allow spaces.
		private void CodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void CodeTextBox_TextChanged(object sender, EventArgs e)
		{
			if (sender is TextBox textBox)
			{
				textBox.Text = FormCommon.StripBlanks(textBox.Text);
				textBox.SelectionStart = textBox.Text.Trim().Length;
			}
		}
		#endregion

		#region Properties 

		/// <summary>Gets or sets the primary ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets or sets the Unit System ID value.</summary>
		public int LJCUnitSystemID { get; set; }

		/// <summary>Gets or sets the Unit Category ID value.</summary>
		public int LJCUnitCategoryID { get; set; }

		/// <summary>Gets a reference to the record object.</summary>
		public UnitMeasure LJCRecord { get; private set; }

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

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

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }

		// The Managers object.
		private UnitMeasureManagers Managers { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private UnitSystemComboCode mUnitSystemComboCode;
		private UnitCategoryComboCode mUnitCategoryComboCode;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
