// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionDetail.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCViewEditorDAL;

namespace LJCViewEditor
{
	// The ViewCondition detail dialog.
	internal partial class ViewConditionDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewConditionDetail()
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
		private void ViewConditionDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Filter\ConditionDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewConditionDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			//CenterToParent();
			Location = LJCLocation;
		}

		// Paint the form background.
		/// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
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
			Text = "ViewCondition Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ ViewCondition.ColumnID, LJCID }
				};
				var dataRecord = mViewConditionManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewCondition();
				ParentTextbox.Text = LJCParentName;

				ComparisonTextbox.Text = "=";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewCondition dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.ViewConditionSetID;
				ParentTextbox.Text = LJCParentName;
				FirstValueCombo.Text = dataRecord.FirstValue;
				SecondValueTextbox.Text = dataRecord.SecondValue;
				ComparisonTextbox.Text = dataRecord.ComparisonOperator;
			}
		}

		// Creates and returns a record object with the data from
		private ViewCondition SetRecordValues()
		{
			ViewCondition retValue = new ViewCondition()
			{
				ID = LJCID,
				ViewConditionSetID = LJCParentID,
				FirstValue = ViewEditorCommon.TruncateAtHyphen(FirstValueCombo.Text),
				SecondValue = SecondValueTextbox.Text,
				ComparisonOperator = ComparisonTextbox.Text
			};
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewCondition lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewCondition.ColumnViewConditionSetID, LJCRecord.ViewConditionSetID },
				{ ViewCondition.ColumnFirstValue, (object)LJCRecord.FirstValue }
			};
			lookupRecord = mViewConditionManager.Retrieve(keyColumns);
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
						{ ViewCondition.ColumnID, LJCRecord.ID }
					};
					LJCRecord.ID = 0;
					mViewConditionManager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					ViewCondition addedRecord = mViewConditionManager.Add(LJCRecord);
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
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(FirstValueCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {FirstValueLabel.Text}");
			}
			if (false == NetString.HasValue(SecondValueTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {SecondValueLabel.Text}");
			}
			if (false == NetString.HasValue(ComparisonTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ComparisonLabel.Text}");
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

			// Initialize Class Data.
			mViewHelper = new ViewHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mViewConditionManager = mViewHelper.ViewConditionManager;

			// Set control values.
			ParentLabel.BackColor = mSettings.BeginColor;
			FirstValueLabel.BackColor = mSettings.BeginColor;
			SecondValueLabel.BackColor = mSettings.BeginColor;
			ComparisonLabel.BackColor = mSettings.BeginColor;

			FirstValueCombo.MaxLength = ViewCondition.LengthFirstValue;
			SecondValueTextbox.MaxLength = ViewCondition.LengthSecondValue;
			ComparisonTextbox.MaxLength = ViewCondition.LengthComparisonOperator;

			// Load control data.
			// First Value Columns Combo
			DataHelper dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mFirstValueColumns = dataHelper.GetTableColumns(LJCTableName);
			foreach (DbColumn dbColumn in mFirstValueColumns)
			{
				FirstValueCombo.Items.Add(dbColumn);
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void ConditionHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Filter\ConditionDetail.html");
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

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void FirstValueTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void SecondValueTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void ComparisonTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void FirstValueTextbox_TextChanged(object sender, EventArgs e)
		{
			FirstValueCombo.Text = FormCommon.StripBlanks(FirstValueCombo.Text);
		}

		// Strips blanks from the text value.
		private void SecondValueTextbox_TextChanged(object sender, EventArgs e)
		{
			SecondValueTextbox.Text = FormCommon.StripBlanks(SecondValueTextbox.Text);
		}

		// Strips blanks from the text value.
		private void ComparisonTextbox_TextChanged(object sender, EventArgs e)
		{
			ComparisonTextbox.Text = FormCommon.StripBlanks(ComparisonTextbox.Text);
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
		internal ViewCondition LJCRecord { get; private set; }

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }
		#endregion

		#region Custom Properties

		// Gets or sets the TableName value.
		internal string LJCTableName { get; set; }
		#endregion

		#region Class Data

		// Singleton values.
		private DbColumns mFirstValueColumns;
		private StandardSettings mSettings;
		private ViewHelper mViewHelper;
		private ViewConditionManager mViewConditionManager;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}

