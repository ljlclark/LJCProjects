// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOnDetail.cs
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
	// The ViewJoinOn detail dialog.
	internal partial class ViewJoinOnDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewJoinOnDetail()
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
		private void ViewJoinOnDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Join\JoinOnDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewJoinOnDetail_Load(object sender, EventArgs e)
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
			Text = "ViewJoinOn Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
        var manager = Managers.ViewJoinOnManager;
				var dataRecord = manager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewJoinOn();
				ParentTextbox.Text = LJCParentName;
				OperatorTextbox.Text = "=";
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewJoinOn dataRecord)
		{
			if (dataRecord != null)
			{
				ParentTextbox.Text = LJCParentName;

				FromColumnCombo.Text = dataRecord.FromColumnName;
				var dbColumn
					= mJoinTableColumns.LJCSearchPropertyName(dataRecord.FromColumnName);
				if (dbColumn != null)
				{
					FromColumnCombo.SelectedItem = dbColumn;
				}

				ToColumnCombo.Text = dataRecord.ToColumnName;
				dbColumn
					= mJoinOnTableColumns.LJCSearchPropertyName(dataRecord.ToColumnName);
				if (dbColumn != null)
				{
					ToColumnCombo.SelectedItem = dbColumn;
				}

				OperatorTextbox.Text = dataRecord.JoinOnOperator;

        // Reference key values.
        LJCParentID = dataRecord.ViewJoinID;
      }
    }

		// Creates and returns a record object with the data from
		private ViewJoinOn SetRecordValues()
		{
      ViewJoinOn retValue = new ViewJoinOn()
      {
        FromColumnName = ViewEditorCommon.TruncateAtHyphen(FromColumnCombo.Text),
        ToColumnName = ViewEditorCommon.TruncateAtHyphen(ToColumnCombo.Text),
        JoinOnOperator = OperatorTextbox.Text,

        // Get Reference key values.
        ID = LJCID,
        ViewJoinID = LJCParentID,
      };
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

      var manager = Managers.ViewJoinOnManager;
			var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ViewJoinID
        , LJCRecord.FromColumnName);
			if (lookupRecord != null
				&& (false == LJCIsUpdate
				|| (true == LJCIsUpdate && lookupRecord.ID != LJCRecord.ID)))
			{
				retValue = false;
				var title = "Data Entry Error";
				var message = "The record already exists.";
				MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
          var keyColumns = manager.GetIDKey(LJCRecord.ID);
					LJCRecord.ID = 0;
					manager.Update(LJCRecord, keyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					ViewJoinOn viewJoinOn = manager.Add(LJCRecord);
					if (viewJoinOn != null)
					{
						LJCRecord.ID = viewJoinOn.ID;
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
			StringBuilder builder;
			string title;
			string message;
			bool retVal = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (false == NetString.HasValue(FromColumnCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {FromColumnLabel.Text}");
			}
			if (false == NetString.HasValue(ToColumnCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {ToColumnLabel.Text}");
			}
			if (false == NetString.HasValue(OperatorTextbox.Text))
			{
				retVal = false;
				builder.AppendLine($"  {OperatorLabel.Text}");
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
      Managers = values.Managers;
			mSettings = values.StandardSettings;

      // Initialize Class Data.

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

      FromColumnCombo.MaxLength = ViewJoinOn.LengthFromColumnName;
			ToColumnCombo.MaxLength = ViewJoinOn.LengthToColumnName;
			OperatorTextbox.MaxLength = ViewJoinOn.LengthJoinOperator;

			// Load control data.
			// From Columns Combo
			DataHelper dataHelper = new DataHelper(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			mJoinTableColumns = dataHelper.GetJoinFromColumns(LJCParentID);
			foreach (DbColumn dbColumn in mJoinTableColumns)
			{
				FromColumnCombo.Items.Add(dbColumn);
			}

			// To Columns Combo
			mJoinOnTableColumns = dataHelper.GetJoinToColumns(LJCParentID);
			foreach (DbColumn dbColumn in mJoinOnTableColumns)
			{
				ToColumnCombo.Items.Add(dbColumn);
			}
		}
		#endregion

		#region Action Event Handlers

		// Shows the Help page.
		private void JoinOnHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Join\JoinOnDetail.html");
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
		private void FromColumnTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void ToColumnTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Does not allow spaces.
		private void OperatorTextbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void FromColumnTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = FromColumnCombo.SelectionStart;
      FromColumnCombo.Text = FormCommon.StripBlanks(FromColumnCombo.Text);
      FromColumnCombo.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void ToColumnTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = ToColumnCombo.SelectionStart;
      ToColumnCombo.Text = FormCommon.StripBlanks(ToColumnCombo.Text);
      ToColumnCombo.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void OperatorTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = OperatorTextbox.SelectionStart;
      OperatorTextbox.Text = FormCommon.StripBlanks(OperatorTextbox.Text);
      OperatorTextbox.SelectionStart = prevStart;
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
		internal ViewJoinOn LJCRecord { get; private set; }

    // Gets or sets the BeginColor value.
    private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }

    // The Managers object.
    private ManagersDbView Managers { get; set; }
    #endregion

    #region Class Data

    private DbColumns mJoinOnTableColumns;
    private DbColumns mJoinTableColumns;
		private StandardUISettings mSettings;

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
