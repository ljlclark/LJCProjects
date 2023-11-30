// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionDetail.cs
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
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
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
		/// <include path='items/OnPaintBackground/*' file='../../LJCGenDoc/Common/Detail.xml'/>
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
        var manager = Managers.ViewConditionManager;
				var dataRecord = manager.RetrieveWithID(LJCID);
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
        // In control order.
				ParentTextbox.Text = LJCParentName;
				FirstValueCombo.Text = dataRecord.FirstValue;
				SecondValueTextbox.Text = dataRecord.SecondValue;
				ComparisonTextbox.Text = dataRecord.ComparisonOperator;

        // Reference key values.
        LJCParentID = dataRecord.ViewConditionSetID;
      }
    }

		// Creates and returns a record object with the data from
		private ViewCondition SetRecordValues()
		{
			ViewCondition retValue = new ViewCondition()
			{
				FirstValue = ViewEditorCommon.TruncateAtHyphen(FirstValueCombo.Text),
				SecondValue = SecondValueTextbox.Text,
				ComparisonOperator = ComparisonTextbox.Text,

        // Get Reference key values.
        ID = LJCID,
        ViewConditionSetID = LJCParentID
      };
			return retValue;
		}

		// Saves the data.
		private bool DataSave()
		{
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

      var manager = Managers.ViewConditionManager;
			var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ViewConditionSetID
        , LJCRecord.FirstValue);
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
					ViewCondition addedRecord = manager.Add(LJCRecord);
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
			bool retVal = true;

			var builder = new StringBuilder(64);
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

      // Initialize Class Data.
      Managers = new ManagersDbView();
      Managers.SetDbProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);
      //mDataDbView = new DataDbView(Managers);

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);

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
      var prevStart = FirstValueCombo.SelectionStart;
      FirstValueCombo.Text = FormCommon.StripBlanks(FirstValueCombo.Text);
      FirstValueCombo.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void SecondValueTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = SecondValueTextbox.SelectionStart;
      SecondValueTextbox.Text = FormCommon.StripBlanks(SecondValueTextbox.Text);
      SecondValueTextbox.SelectionStart = prevStart;
    }

    // Strips blanks from the text value.
    private void ComparisonTextbox_TextChanged(object sender, EventArgs e)
		{
      var prevStart = ComparisonTextbox.SelectionStart;
      ComparisonTextbox.Text = FormCommon.StripBlanks(ComparisonTextbox.Text);
      ComparisonTextbox.SelectionStart = prevStart;
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

    // The Managers object.
    internal ManagersDbView Managers { get; set; }

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

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    // Singleton values.
    private DbColumns mFirstValueColumns;
		private StandardUISettings mSettings;
		//private DataDbView mDataDbView;
		#endregion
	}
}

