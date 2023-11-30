// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCSQLUtilLib;
using LJCSQLUtilLibDAL;
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
			LJCID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;
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
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Join\JoinDetail.html");
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
			Text = "ViewJoin Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
        var manager = Managers.ViewJoinManager;
				var dataRecord = manager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewJoin();
				ParentTextbox.Text = LJCParentName;
				JoinTypeTextbox.Text = "Left";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewJoin dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.ViewDataID;
				ParentTextbox.Text = LJCParentName;

				//JoinTableCombo.Text = record.JoinTableName;
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
			}
		}

		// Creates and returns a record object with the data from
		private ViewJoin SetRecordValues()
		{
			ViewJoin retValue = new ViewJoin()
			{
				ID = LJCID,
				ViewDataID = LJCParentID,
				JoinTableName = GetJoinTableName(JoinTableCombo.Text),
				JoinType = JoinTypeTextbox.Text,
				TableAlias = AliasTextbox.Text
			};
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
				&& (false == LJCIsUpdate
				|| (true == LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				var title = "Data Entry Error";
				var message = "The record already exists.";
        Cursor = Cursors.Default;
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
          var keyColumns = manager.IDKey(LJCRecord.ID);
					LJCRecord.ID = 0;
					manager.Update(LJCRecord, keyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					var viewJoin = manager.Add(LJCRecord);
					if (viewJoin != null)
					{
						LJCRecord.ID = viewJoin.ID;
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
			bool retVal = true;

			var builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(JoinTableCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {JoinTableLabel.Text}");
			}
			if (false == NetString.HasValue(JoinTypeTextbox.Text))
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

      // Initialize Class Data.
      var dataManager = Managers.ViewDataManager;
			var viewData = dataManager.RetrieveWithID(LJCParentID);
      var tableManager = Managers.ViewTableManager;
			var viewTable = tableManager.RetrieveWithID(viewData.ViewTableID);
			if (viewTable != null)
			{
				LJCMetadata metaData = new LJCMetadata(mSettings.DbServiceRef
					, mSettings.DataConfigName);
				var foreignKeys = metaData.LoadSchemaForeignKeys(viewTable.Name);

				// Load control data.
				// Join Table Combo
				PopulateTableCombo(foreignKeys, viewTable.Name);
			}

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

      JoinTableCombo.MaxLength = ViewColumn.LengthColumnName;
			JoinTypeTextbox.MaxLength = ViewColumn.LengthDataTypeName;
		}

		// Populates the join table combo.
		private void PopulateTableCombo(ForeignKeys foreignKeys, string targetTableName)
		{
			if (foreignKeys != null && foreignKeys.Count > 0)
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
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Join\JoinDetail.html");
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

    // Singleton values.
    private StandardUISettings mSettings;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
