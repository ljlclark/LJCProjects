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
	// The ViewColumn detail dialog.
	internal partial class ViewColumnDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewColumnDetail()
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
		private void ViewColumnDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Column\ColumnDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewColumnDetail_Load(object sender, EventArgs e)
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
			Text = "ViewColumn Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
				var dataRecord = mViewColumnManager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);

				// Do not allow column change on update.
				TemplateColumnCombo.Enabled = false;
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewColumn();
				ParentTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewColumn dataRecord)
		{
			// Also called from TemplateCombo_SelectedIndexChanged.
			if (dataRecord != null)
			{
				// Do not update parent values on TemplateCombo changed.
				if (dataRecord.ViewDataID > 0)
				{
					LJCParentID = dataRecord.ViewDataID;
				}
				ParentTextbox.Text = LJCParentName;

				// Only select a column if currently empty.
				// This is to allow on Edit but not TemplateCombo changed.
				if (null == TemplateColumnCombo.SelectedItem)
				{
					DbColumn dbColumn = mTableColumns.LJCSearchName(dataRecord.ColumnName);
					if (dbColumn != null)
					{
						mAllowTemplateGetValues = false;
						TemplateColumnCombo.SelectedItem = dbColumn;
					}
				}

				ColumnNameTextbox.Text = dataRecord.ColumnName;
				PropertyTextbox.Text = dataRecord.PropertyName;
				RenameTextbox.Text = dataRecord.RenameAs;
				CaptionTextbox.Text = dataRecord.Caption;
				DataTypeCombo.SelectedIndex = DataTypeCombo.FindStringExact(dataRecord.DataTypeName);
				ValueTextbox.Text = dataRecord.Value;
				PrimaryKeyCheckBox.Checked = dataRecord.IsPrimaryKey;
			}
		}

		// Creates and returns a record object with the data from
		private ViewColumn SetRecordValues()
		{
			ViewColumn retValue = new ViewColumn()
			{
				ID = LJCID,
				ViewDataID = LJCParentID,
				ColumnName = ColumnNameTextbox.Text.Trim(),
				PropertyName = FormCommon.SetString(PropertyTextbox.Text.Trim()),
				RenameAs = FormCommon.SetString(RenameTextbox.Text.Trim()),
				Caption = FormCommon.SetString(CaptionTextbox.Text.Trim()),
				DataTypeName = DataTypeCombo.Text.Trim(),
				Value = FormCommon.SetString(ValueTextbox.Text.Trim()),
				IsPrimaryKey = PrimaryKeyCheckBox.Checked
			};
			return retValue;
		}

		// Resets the empty record values.
		private void ResetRecordValues(ViewColumn record)
		{
			record.PropertyName = FormCommon.SetString(record.PropertyName);
			record.Caption = FormCommon.SetString(record.Caption);
			record.RenameAs = FormCommon.SetString(record.RenameAs);
			record.Value = FormCommon.SetString(record.Value);
		}

		// Saves the data.
		private bool DataSave()
		{
			ViewColumn lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ ViewColumn.ColumnViewDataID, LJCRecord.ViewDataID },
				{ ViewColumn.ColumnColumnName, (object)LJCRecord.ColumnName }
			};
			lookupRecord = mViewColumnManager.Retrieve(keyColumns);
			if (mViewColumnManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
					var updateKeyColumns = new DbColumns()
					{
						{ ViewColumn.ColumnID, LJCRecord.ID }
					};
					LJCRecord.ID = 0;
					mViewColumnManager.Update(LJCRecord, updateKeyColumns);
					LJCRecord.ID = LJCID;
					ResetRecordValues(LJCRecord);
				}
				else
				{
					LJCRecord.ID = 0;
					ViewColumn addedRecord = mViewColumnManager.AddWithFlags(LJCRecord);
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
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(ColumnNameTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ColumnNameLabel.Text}");
			}
			if (false == NetString.HasValue(DataTypeCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {DataTypeLabel.Text}");
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
			DataTypeManager dataTypeManager;

			// Get singleton values.
			ValuesViewEditor values = ValuesViewEditor.Instance;
			mSettings = values.StandardSettings;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			mViewHelper = new ViewHelper(LJCDbServiceRef, LJCDataConfigName);
			mViewColumnManager = new ViewColumnManager(LJCDbServiceRef
				, LJCDataConfigName);

			// Set control values.
			ParentLabel.BackColor = BeginColor;
			ColumnNameLabel.BackColor = BeginColor;
			PropertyLabel.BackColor = BeginColor;
			CaptionLabel.BackColor = BeginColor;
			DataTypeLabel.BackColor = BeginColor;
			ValueLabel.BackColor = BeginColor;
			RenameLabel.BackColor = BeginColor;

			ColumnNameTextbox.MaxLength = ViewColumn.LengthColumnName;
			PropertyTextbox.MaxLength = ViewColumn.LengthPropertyName;
			CaptionTextbox.MaxLength = ViewColumn.LengthCaption;
			ValueTextbox.MaxLength = ViewColumn.LengthValue;
			RenameTextbox.MaxLength = ViewColumn.LengthRenameAs;

			// Template Columns Combo
			DataHelper dataHelper = new DataHelper(LJCDbServiceRef, LJCDataConfigName);
			mTableColumns = dataHelper.GetTableColumns(LJCTableName);
			foreach (DbColumn dbColumn in mTableColumns)
			{
				TemplateColumnCombo.Items.Add(dbColumn);
			}

			// Data Types Combo
			try
			{
				dataTypeManager = new DataTypeManager(LJCDbServiceRef, LJCDataConfigName);
			}
			catch (SystemException e)
			{
				ViewEditorCommon.CreateTables(e, LJCDataConfigName);
				dataTypeManager = new DataTypeManager(LJCDbServiceRef, LJCDataConfigName);
			}
			LJCViewEditorDAL.DataTypes dataTypes = dataTypeManager.Load();
			foreach (DataType dataType in dataTypes)
			{
				DataTypeCombo.Items.Add(dataType);
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void ViewColumnHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Column\ColumnDetail.html");
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
				DbColumn dbColumn = TemplateColumnCombo.SelectedItem as DbColumn;
				ViewColumn viewColumn = mViewHelper.GetViewColumnFromDbColumn(dbColumn);
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
		internal ViewColumn LJCRecord { get; set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }
		#endregion

		#region Custom Properties

		// Gets or sets the LJCDataConfigName value.
		internal string LJCDataConfigName { get; set; }

		// Gets or sets the LJCDbServiceRef value.
		internal DbServiceRef LJCDbServiceRef { get; set; }

		// Gets or sets the TableName value.
		internal string LJCTableName { get; set; }
		#endregion

		#region Class Data

		private StandardSettings mSettings;
		private DbColumns mTableColumns;
		private ViewColumnManager mViewColumnManager;
		private ViewHelper mViewHelper;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
