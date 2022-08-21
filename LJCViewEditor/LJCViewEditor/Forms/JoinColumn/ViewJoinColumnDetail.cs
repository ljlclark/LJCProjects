// Copyright (c) Lester J. Clark 2021 - All Rights Reserved
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
	// The ViewJoinColumn detail dialog.
	internal partial class ViewJoinColumnDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewJoinColumnDetail()
		{
			InitializeComponent();

			// Initialize property values.
			LJCID = 0;
			LJCParentID = 0;
			LJCParentName = null;
			LJCRecord = null;
			LJCIsUpdate = false;
			BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;

			mAllowTemplateGetValues = true;
		}
		#endregion

		#region Form Event Handlers

		// Handles the control keys.
		private void ViewJoinColumnDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Join\JoinColumnDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewJoinColumnDetail_Load(object sender, EventArgs e)
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
			Text = "ViewJoinColumn Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var keyColumns = new DbColumns()
				{
					{ ViewJoinColumn.ColumnID, LJCID }
				};
				var dataRecord = mViewJoinColumnManager.Retrieve(keyColumns);
				GetRecordValues(dataRecord);

				// Do not allow column change on update.
				TemplateCombo.Enabled = false;
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewJoinColumn();
				ParentTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewJoinColumn dataRecord)
		{
			// Also called from TemplateCombo_SelectedIndexChanged.
			if (dataRecord != null)
			{
				// Do not update parent values on TemplateCombo changed.
				if (dataRecord.ViewJoinID > 0)
				{
					LJCParentID = dataRecord.ViewJoinID;
				}
				ParentTextbox.Text = LJCParentName;

				// Only select a column if currently empty.
				// This is to allow on Edit but not TemplateCombo changed.
				if (null == TemplateCombo.SelectedItem)
				{
					DbColumn dbColumn = mTableDbColumns.LJCSearchName(dataRecord.ColumnName);
					if (dbColumn != null)
					{
						mAllowTemplateGetValues = false;
						TemplateCombo.SelectedItem = dbColumn;
					}
				}
				ColumnNameTextbox.Text = dataRecord.ColumnName;
				PropertyTextbox.Text = dataRecord.PropertyName;
				RenameTextbox.Text = dataRecord.RenameAs;
				CaptionTextbox.Text = dataRecord.Caption;
				DataTypeCombo.SelectedIndex = DataTypeCombo.FindStringExact(dataRecord.DataTypeName);
			}
		}

		// Creates and returns a record object with the data from
		private ViewJoinColumn SetRecordValues()
		{
			ViewJoinColumn retValue = new ViewJoinColumn()
			{
				ID = LJCID,
				ViewJoinID = LJCParentID,
				Caption = FormCommon.SetString(CaptionTextbox.Text.Trim()),
				ColumnName = ColumnNameTextbox.Text.Trim(),
				DataTypeName = DataTypeCombo.Text.Trim(),
				PropertyName = FormCommon.SetString(PropertyTextbox.Text.Trim()),
				RenameAs = FormCommon.SetString(RenameTextbox.Text.Trim()),
			};

			// Get additional join display values.
			return retValue;
		}

		// Resets the empty record values.
		private void ResetRecordValues(ViewJoinColumn record)
		{
			record.PropertyName = FormCommon.SetString(record.PropertyName);
			record.Caption = FormCommon.SetString(record.Caption);
			record.RenameAs = FormCommon.SetString(record.RenameAs);
			record.Value = FormCommon.SetString(record.Value);
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewJoinColumn lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewJoinColumn.ColumnViewJoinID, LJCRecord.ViewJoinID },
				{ ViewJoinColumn.ColumnPropertyName, (object)LJCRecord.PropertyName },
				{ ViewJoinColumn.ColumnRenameAs, (object)LJCRecord.RenameAs }
			};
			lookupRecord = mViewJoinColumnManager.Retrieve(keyColumns);
			if (mViewJoinColumnManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
						{ ViewJoinColumn.ColumnID, LJCRecord.ID }
					};
					mViewJoinColumnManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					ViewJoinColumn addedRecord = mViewJoinColumnManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
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

			if (false == NetString.HasValue(ColumnNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {ColumnNameLabel.Text}");
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
			DataHelper dataHelper;

			// Get singleton values.
			ValuesViewEditor values = ValuesViewEditor.Instance;

			mSettings = values.StandardSettings;

			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			mViewJoinColumnManager
				= new ViewJoinColumnManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mDataTypeManager = new DataTypeManager(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			// Set control values.
			ParentLabel.BackColor = BeginColor;
			ColumnNameLabel.BackColor = BeginColor;
			PropertyLabel.BackColor = BeginColor;
			CaptionLabel.BackColor = BeginColor;
			DataTypeLabel.BackColor = BeginColor;
			RenameLabel.BackColor = BeginColor;

			ColumnNameTextbox.MaxLength = ViewColumn.LengthColumnName;
			PropertyTextbox.MaxLength = ViewColumn.LengthPropertyName;
			CaptionTextbox.MaxLength = ViewColumn.LengthCaption;
			RenameTextbox.MaxLength = ViewColumn.LengthRenameAs;

			// Template Columns Combo
			mTableDbColumns = dataHelper.GetTableColumns(LJCParentName);
			foreach (DbColumn dbColumn in mTableDbColumns)
			{
				TemplateCombo.Items.Add(dbColumn);
			}

			// Data Types Combo
			if (dataHelper != null
				&& mDataTypeManager != null)
			{
				LJCViewEditorDAL.DataTypes dataTypes = mDataTypeManager.Load();
				foreach (DataType dataType in dataTypes)
				{
					DataTypeCombo.Items.Add(dataType);
				}
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void JoinColumnHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Join\JoinColumnDetail.html");
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

		// Handles the SelectionIndexChanged event.
		private void TemplateCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mAllowTemplateGetValues)
			{
				DbColumn dbColumn = TemplateCombo.SelectedItem as DbColumn;
				ViewJoinColumn viewColumn = new ViewJoinColumn()
				{
					ColumnName = dbColumn.ColumnName,
					PropertyName = dbColumn.PropertyName,
					RenameAs = dbColumn.RenameAs,
					Caption = dbColumn.Caption,
					DataTypeName = dbColumn.DataTypeName
				};
				GetRecordValues(viewColumn);
			}
			mAllowTemplateGetValues = true;
		}
		private bool mAllowTemplateGetValues;
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void ColumnNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void PropertyTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void RenameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void ColumnNameTextbox_TextChanged(object sender, EventArgs e)
		{
			ColumnNameTextbox.Text = FormCommon.StripBlanks(ColumnNameTextbox.Text);
		}

		// Strips blanks from the text value.
		private void PropertyTextbox_TextChanged(object sender, EventArgs e)
		{
			PropertyTextbox.Text = FormCommon.StripBlanks(PropertyTextbox.Text);
		}

		// Strips blanks from the text value.
		private void RenameTextbox_TextChanged(object sender, EventArgs e)
		{
			RenameTextbox.Text = FormCommon.StripBlanks(RenameTextbox.Text);
		}
		#endregion

		#region Properties

		// Gets or sets the primary ID value.
		internal int LJCID { get; set; }

		// Gets the LJCIsUpdate value.
		internal bool LJCIsUpdate { get; private set; }

		// The form position.
		internal Point LJCLocation { get; set; }

		// Gets or sets the Parent ID value.
		internal int LJCParentID { get; set; }

		// Gets or sets the LJCParentName value.
		internal string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		// Gets a reference to the record object.
		internal ViewJoinColumn LJCRecord { get; private set; }

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }
		#endregion

		#region Class Data

		private DataTypeManager mDataTypeManager;
		private StandardSettings mSettings;
		private DbColumns mTableDbColumns;
		private ViewJoinColumnManager mViewJoinColumnManager;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
