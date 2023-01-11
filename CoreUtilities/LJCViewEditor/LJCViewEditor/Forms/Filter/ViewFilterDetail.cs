// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewFilterDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBViewDAL;

namespace LJCViewEditor
{
	// The ViewFilter detail dialog.
	internal partial class ViewFilterDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewFilterDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCRecord = null;
			LJCIsUpdate = false;
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Handles the control keys.
		private void ViewFilterDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Filter\FilterDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewFilterDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			//CenterToParent();
			Location = LJCLocation;
		}

		// Paint the form background.
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			FormCommon.CreateGradient(e.Graphics, ClientRectangle
				, BeginColor, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			Cursor = Cursors.WaitCursor;
			Text = "ViewFilter Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ ViewFilter.ColumnID, LJCID }
				};
				var dataRecord = mViewFilterManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewFilter();
				ParentTextbox.Text = LJCParentName;

				NameTextbox.Text = LJCDefaultName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewFilter dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.ViewDataID;
				ParentTextbox.Text = LJCParentName;
				NameTextbox.Text = dataRecord.Name;

				if ("or" == dataRecord.BooleanOperator.ToLower())
				{
					OperatorCombo.SelectedIndex = 1;
				}
			}
		}

		// Creates and returns a record object with the data from
		private ViewFilter SetRecordValues()
		{
			ViewFilter retValue = new ViewFilter()
			{
				ID = LJCID,
				ViewDataID = LJCParentID,
				Name = NameTextbox.Text,
				BooleanOperator = OperatorCombo.Text
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewFilter lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewFilter.ColumnViewDataID, LJCRecord.ViewDataID },
				{ ViewFilter.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mViewFilterManager.Retrieve(keyColumns);
			if (lookupRecord != null
				&& (false == LJCIsUpdate
				|| (true == LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
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
					var updateKeyColumns = new DbColumns()
					{
						{ ViewFilter.ColumnID, LJCRecord.ID }
					};
					LJCRecord.ID = 0;
					mViewFilterManager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					ViewFilter viewFilter = mViewFilterManager.Add(LJCRecord);
					if (viewFilter != null)
					{
						LJCRecord.ID = viewFilter.ID;
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
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(NameTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {NameLabel.Text}");
			}

			if (retVal == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return retVal;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			ValuesViewEditor values = ValuesViewEditor.Instance;

			mSettings = values.StandardSettings;

			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			mViewFilterManager = new ViewFilterManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Load control data.
			OperatorCombo.Items.Add("And");
			OperatorCombo.Items.Add("Or");
			OperatorCombo.SelectedIndex = 0;
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void FilterHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Filter\FilterDetail.html");
		}
		#endregion

		#region Control Event Handlers

		// Fires the Change event.
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

		#region Properties

		// Gets or sets the ID value.
		internal int LJCID { get; set; }

		// Gets the IsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// The form position.
		internal Point LJCLocation { get; set; }

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the Parent name value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets a reference to the record object.
		internal ViewFilter LJCRecord { get; private set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }
		#endregion

		#region Custom Properties

		// Gets or sets the default FilterName value.
		internal string LJCDefaultName { get; set; }
		#endregion

		#region Class Data

		// Singleton values.
		private StandardUISettings mSettings;
		private ViewFilterManager mViewFilterManager;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
