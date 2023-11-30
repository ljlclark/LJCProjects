// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSetDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDBViewDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCViewEditor
{
	// The ViewFilter detail dialog.
	internal partial class ViewConditionSetDetail : Form
	{
		#region Constructors

		// Initializes an object instance.
		internal ViewConditionSetDetail()
		{
			InitializeComponent();

			// Initialize property values.
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
		private void ViewConditionSetDetail_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
					Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
						, @"Filter\ConditionSetDetail.html");
					break;
			}
		}

		// Configures the form and loads the initial control data.
		private void ViewColumnSetDetail_Load(object sender, EventArgs e)
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
			Text = "ViewConditionSet Detail";
			if (LJCID > 0)
			{
				Text += " - Edit";
				LJCIsUpdate = true;
        var manager = Managers.ViewConditionSetManager;
				var dataRecord = manager.RetrieveWithID(LJCID);
				GetRecordValues(dataRecord);
			}
			else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewConditionSet();
				ParentTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		private void GetRecordValues(ViewConditionSet dataRecord)
		{
			if (dataRecord != null)
			{
				ParentTextbox.Text = LJCParentName;
				if ("or" == dataRecord.BooleanOperator.ToLower())
				{
					OperatorCombo.SelectedIndex = 1;
				}

        // Reference key values.
        LJCParentID = dataRecord.ViewFilterID;
      }
    }

		// Creates and returns a record object with the data from
		private ViewConditionSet SetRecordValues()
		{
			ViewConditionSet retValue = new ViewConditionSet()
			{
				ID = LJCID,
				BooleanOperator = OperatorCombo.Text,

        // Get Reference key values.
        ViewFilterID = LJCParentID
      };
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

      var manager = Managers.ViewConditionSetManager;
      // Only one ViewConditionSet per ViewFilter?
      var lookupRecord = manager.RetrieveWithParentID(LJCRecord.ViewFilterID);
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
          var keyColumns = manager.IDKey(LJCID);
					LJCRecord.ID = 0;
					manager.Update(LJCRecord, keyColumns);
					LJCRecord.ID = LJCID;
				}
				else
				{
					LJCRecord.ID = 0;
					var viewConditionSet = manager.Add(LJCRecord);
					if (viewConditionSet != null)
					{
						LJCRecord.ID = viewConditionSet.ID;
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

			if (false == NetString.HasValue(OperatorCombo.Text))
			{
				retVal = false;
				builder.AppendLine($"  {OperatorLabel.Text}");
			}

			if (retVal == false)
			{
				var title = "Data Entry Error";
				var message = builder.ToString();
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
      Cursor = Cursors.WaitCursor;
      ValuesViewEditor values = ValuesViewEditor.Instance;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;


      // Initialize Class Data.

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

      OperatorCombo.MaxLength = ViewConditionSet.LengthBooleanOperator;

			// Load control data.
			OperatorCombo.Items.Add("And");
			OperatorCombo.Items.Add("Or");
			OperatorCombo.SelectedIndex = 0;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void ConditionSetHelp_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, "ViewEditor.chm", HelpNavigator.Topic
				, @"Filter\ConditionSetDetail.html");
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
		internal ViewConditionSet LJCRecord { get; private set; }

		// Gets or sets the BeginColor value.
		private Color BeginColor { get; set; }

		// Gets or sets the Parent ID value.
		private Color EndColor { get; set; }

    // The Managers object.
    private ManagersDbView Managers { get; set; }
    #endregion

    #region Class Data

    // Singleton values.

		// The Change event.
		internal event EventHandler<EventArgs> LJCChange;

    private StandardUISettings mSettings;
    //private ItemTypeComboCode mItemTypeComboCode;
    #endregion
  }
}
