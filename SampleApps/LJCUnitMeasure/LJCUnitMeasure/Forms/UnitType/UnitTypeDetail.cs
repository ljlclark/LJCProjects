﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// UnitTypeDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCUnitMeasureDAL;
using LJCWinFormCommon;

namespace LJCUnitMeasure
{
	/// <summary>The UnitTypeDetail detail dialog.</summary>
	public partial class UnitTypeDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public UnitTypeDetail(UnitMeasureManagers managers)
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;
			LJCHelpFileName = "UnitMeasureList.chm";
			LJCHelpPageName = "UnitTypeDetail.html";
			Managers = managers;

			// Set default class data.
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void UnitTypeDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			CenterToParent();
		}

		// Handles the form keys.
		private void UnitTypeDetail_KeyDown(object sender, KeyEventArgs e)
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
			FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
				, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			UnitType dataRecord;

			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ UnitType.ColumnID, LJCID }
				};
				dataRecord = Managers.UnitTypeManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
			}
			else
			{
				LJCIsUpdate = false;

				// Set default values.
				LJCRecord = new UnitType();
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(UnitType dataRecord)
		{
			if (dataRecord != null)
			{
				CodeText.Text = dataRecord.Code;
				NameText.Text = dataRecord.Name;
			}
		}

		// Creates and returns a record object with the data from
		private UnitType SetRecordValues()
		{
			UnitType retValue = new UnitType()
			{
				ID = LJCID,
				Code = CodeText.Text.Trim(),
				Name = NameText.Text.Trim(),
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			UnitType lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ UnitType.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = Managers.UnitTypeManager.Retrieve(keyColumns);
			if (Managers.UnitTypeManager.IsDuplicate(lookupRecord, LJCRecord
				, LJCIsUpdate))
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
						{ UnitType.ColumnID, LJCRecord.ID }
					};
					Managers.UnitTypeManager.Update(LJCRecord, keyColumns);
				}
				else
				{
					var addedRecord = Managers.UnitTypeManager.Add(LJCRecord);
					if (addedRecord != null)
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

			if (!NetString.HasValue(CodeText.Text))
			{
				retValue = false;
				builder.AppendLine($"  {CodeLabel.Text}");
			}
			if (!NetString.HasValue(NameText.Text))
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

			// Set control values.
			CodeLabel.BackColor = BeginColor;
			NameLabel.BackColor = BeginColor;
			CodeText.MaxLength = UnitCategory.LengthCode;
			NameText.MaxLength = UnitCategory.LengthName;

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
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void CodeText_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void CodeText_TextChanged(object sender, EventArgs e)
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

		/// <summary>Gets a reference to the record object.</summary>
		public UnitType LJCRecord { get; private set; }

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

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
