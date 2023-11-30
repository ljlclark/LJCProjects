// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinColumnDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCViewEditorDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
      LJCIsUpdate = false;
      LJCParentID = 0;
			LJCParentName = null;
      LJCRecord = null;

      // Set default class data.
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
        var manager = Managers.ViewJoinColumnManager;
				var dataRecord = manager.RetrieveWithID(LJCID);
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
					var dbColumn
						= mTableDbColumns.LJCSearchPropertyName(dataRecord.PropertyName);
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
				Caption = FormCommon.SetString(CaptionTextbox.Text.Trim()),
				ColumnName = ColumnNameTextbox.Text.Trim(),
				DataTypeName = DataTypeCombo.Text.Trim(),
				PropertyName = FormCommon.SetString(PropertyTextbox.Text.Trim()),
				RenameAs = FormCommon.SetString(RenameTextbox.Text.Trim()),

        // Get Reference key values.
        ID = LJCID,
        ViewJoinID = LJCParentID
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
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

      var manager = Managers.ViewJoinColumnManager;
			var lookupRecord = manager.RetrieveWithUnique(LJCRecord.ViewJoinID
        , LJCRecord.PropertyName, LJCRecord.RenameAs);
			if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				var title = "Data Entry Error";
				var message = "The record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
          var keyColumns = manager.IDKey(LJCRecord.ID);
					manager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					ViewJoinColumn addedRecord = manager.Add(LJCRecord);
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

    // Check for saved data.
    private bool IsDataSaved()
    {
      bool retValue = false;

      FormCancelButton.Select();
      if (IsValid() && DataSave())
      {
        retValue = true;
      }
      return retValue;
    }

    // Validates the data.
    private bool IsValid()
		{
			bool retValue = true;

			var builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(ColumnNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {ColumnNameLabel.Text}");
			}

			if (retValue == false)
			{
				var title = "Data Entry Error";
				var message = builder.ToString();
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
			ValuesViewEditor values = ValuesViewEditor.Instance;
      Managers = values.Managers;
			mSettings = values.StandardSettings;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

      // Initialize Class Data.

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

      ColumnNameTextbox.MaxLength = ViewColumn.LengthColumnName;
			PropertyTextbox.MaxLength = ViewColumn.LengthPropertyName;
			CaptionTextbox.MaxLength = ViewColumn.LengthCaption;
			RenameTextbox.MaxLength = ViewColumn.LengthRenameAs;

      // Template Columns Combo
      var dataHelper = new DataHelper(mSettings.DbServiceRef
        , mSettings.DataConfigName);
      mTableDbColumns = dataHelper.GetTableColumns(LJCParentName);
			foreach (DbColumn dbColumn in mTableDbColumns)
			{
				TemplateCombo.Items.Add(dbColumn);
			}

      // Data Types Combo
      var dataTypeManager = new DataTypeManager(mSettings.DbServiceRef
        , mSettings.DataConfigName);
      if (dataHelper != null
				&& dataTypeManager != null)
			{
				LJCViewEditorDAL.DataTypes dataTypes = dataTypeManager.Load();
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
			if (IsDataSaved())
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
      var prevStart = ColumnNameTextbox.SelectionStart;
      ColumnNameTextbox.Text = FormCommon.StripBlanks(ColumnNameTextbox.Text);
      ColumnNameTextbox.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void PropertyTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = PropertyTextbox.SelectionStart;
      PropertyTextbox.Text = FormCommon.StripBlanks(PropertyTextbox.Text);
      PropertyTextbox.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void RenameTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = RenameTextbox.SelectionStart;
      RenameTextbox.Text = FormCommon.StripBlanks(RenameTextbox.Text);
      RenameTextbox.SelectionStart = prevStart;
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

    // The Managers object.
    private ManagersDbView Managers { get; set; }
    #endregion

    #region Class Data

		private StandardUISettings mSettings;
		private DbColumns mTableDbColumns;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
