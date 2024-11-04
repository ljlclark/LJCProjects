// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinDetail.cs
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCSQLUtilLib;
using LJCSQLUtilLibDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCViewEditor
{
	// The ViewJoin detail dialog.
	internal partial class ViewJoinDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewJoinDetail()
		{
			InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"Join\JoinDetail.html";
      LJCID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
			EndColor = Color.LightSkyBlue;
		}
		#endregion

		#region Form Event Handlers

		// Handles the control keys.
		private void ViewJoinDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
          Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
            , LJCHelpPageName);
          break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewJoinDetail_Load(object sender, EventArgs e)
		{
			AcceptButton = OKButton;
			CancelButton = FormCancelButton;
			InitializeControls();
			DataRetrieve();
			Location = LJCLocation;
		}

		// Paint the form background.
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			//FormCommon.CreateGradient(e.Graphics, ClientRectangle
			//	, BeginColor, EndColor);
		}
		#endregion

		#region Data Methods

		// Retrieves the initial control data.
		private void DataRetrieve()
		{
			Cursor = Cursors.WaitCursor;
			Text = "ViewJoin Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
        var manager = Managers.ViewJoinManager;
				mOriginalRecord = manager.RetrieveWithID(LJCID);
				GetRecordValues(mOriginalRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewJoin();
				ParentTextbox.Text = LJCParentName;

        // Set default values.
        JoinTypeTextbox.Text = "Left";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewJoin dataRecord)
		{
			if (dataRecord != null)
			{
        // In control order.
        ParentTextbox.Text = LJCParentName;

        if (NetString.HasValue(dataRecord.JoinTableName))
				{
					foreach (LJCItem item in JoinTableCombo.Items)
					{
						string tableName = GetJoinTableName(item.Text);
						if (tableName == dataRecord.JoinTableName)
						{
							JoinTableCombo.LJCSetByItemID(item.ID);
							break;
						}
					}

					// Add user entered join table name.
					if (-1 == JoinTableCombo.SelectedIndex)
					{
						JoinTableCombo.Items.Add(dataRecord.JoinTableName);
						JoinTableCombo.SelectedIndex = JoinTableCombo.Items.Count - 1;
					}
				}

				JoinTypeTextbox.Text = dataRecord.JoinType;
				AliasTextbox.Text = dataRecord.TableAlias;

        // Reference key values.
        LJCParentID = dataRecord.ViewDataID;
      }
    }

		// Creates and returns a record object with the data from
		private ViewJoin SetRecordValues()
		{
      ViewJoin retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewJoin();
      }

      // In control order.
      retValue.ViewDataID = LJCParentID;
      retValue.JoinTableName = GetJoinTableName(JoinTableCombo.Text);
      retValue.JoinType = JoinTypeTextbox.Text;
      retValue.TableAlias = AliasTextbox.Text;

      // Get Reference key values.
      retValue.ID = LJCID;
      return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();
      var manager = Managers.ViewJoinManager;
			var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ViewDataID
        , LJCRecord.JoinTableName);
			if (lookupRecord != null
				&& (!LJCIsUpdate
				|| (LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
        FormCommon.DataError(this);
      }

      if (retValue)
			{
				if (LJCIsUpdate)
				{
          var keyColumns = manager.IDKey(LJCRecord.ID);
					LJCRecord.ID = 0;
					manager.Update(LJCRecord, keyColumns);
					LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this, manager.AffectedCount);
        }
        else
				{
					LJCRecord.ID = 0;
					var addedRecor = manager.Add(LJCRecord);
					if (addedRecor != null)
					{
						LJCRecord.ID = addedRecor.ID;
					}
          retValue = !FormCommon.AddError(this, manager.AffectedCount);
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
			bool retVal = true;

			var builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (!NetString.HasValue(JoinTableCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {JoinTableLabel.Text}");
			}
			if (!NetString.HasValue(JoinTypeTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {JoinTypeLabel.Text}");
			}

			if (retVal == false)
			{
				var title = "Data Entry Error";
				var message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return retVal;
		}

		// Extract the join table name from the Combobox item text.
		private string GetJoinTableName(string itemText)
		{
			string retValue = itemText;

			if (retValue.StartsWith("<"))
			{
				retValue = itemText.Substring(1);
			}
			int index = retValue.IndexOf('-');
			if (index > -1)
			{
				retValue = retValue.Substring(0, index);
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
      var values = ValuesViewEditor.Instance;
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Set control values.
      //FormCommon.SetLabelsBackColor(Controls, BeginColor);
      JoinTableCombo.MaxLength = ViewColumn.LengthColumnName;
      JoinTypeTextbox.MaxLength = ViewColumn.LengthDataTypeName;

      // Load control data.
      var dataManager = Managers.ViewDataManager;
      var viewData = dataManager.RetrieveWithID(LJCParentID);
      var tableManager = Managers.ViewTableManager;
      var viewTable = tableManager.RetrieveWithID(viewData.ViewTableID);
			if (viewTable != null)
			{
				LJCMetadata metaData = new LJCMetadata(mSettings.DbServiceRef
					, mSettings.DataConfigName);
				var foreignKeys = metaData.LoadSchemaForeignKeys(viewTable.Name);

				// Join Table Combo
				PopulateTableCombo(foreignKeys, viewTable.Name);
			}
      Cursor = Cursors.Default;
    }

    // Populates the join table combo.
    private void PopulateTableCombo(ForeignKeys foreignKeys, string targetTableName)
		{
			if (NetCommon.HasItems(foreignKeys))
			{
				int index = 0;
				foreach (ForeignKey foreignKey in foreignKeys)
				{
					index++;
					string itemText = foreignKey.TargetTable;
					if (targetTableName == foreignKey.SourceTable)
					{
						itemText = $"{itemText}-{foreignKey.SourceColumn}";
						JoinTableCombo.LJCAddItem(index, itemText);
					}
					//else
					//{
					//	itemText = $"<{foreignKey.SourceTable}-{foreignKey.SourceColumn}";
					//}
				}
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void JoinHelp_Click(object sender, EventArgs e)
		{
      Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
        , LJCHelpPageName);
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
		#endregion

		#region KeyEdit Event Handlers

		// Does not allow spaces.
		private void TableNameTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void TableNameTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = JoinTableCombo.SelectionStart;
      JoinTableCombo.Text = FormCommon.StripBlanks(JoinTableCombo.Text);
      JoinTableCombo.SelectionStart = prevStart;
    }
    #endregion

    #region Properties

    // Gets or sets the LJCHelpFileName value.
    internal string LJCHelpFileName
    {
      get { return mHelpFileName; }
      set { mHelpFileName = NetString.InitString(value); }
    }
    private string mHelpFileName;

    // Gets or sets the LJCHelpPageName value.
    internal string LJCHelpPageName
    {
      get { return mHelpPageName; }
      set { mHelpPageName = NetString.InitString(value); }
    }
    private string mHelpPageName;

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
		internal ViewJoin LJCRecord { get; private set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }

    // The Managers object.
    private ManagersDbView Managers { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    // Singleton values.
    private ViewJoin mOriginalRecord;
    private StandardUISettings mSettings;
		#endregion
	}
}
