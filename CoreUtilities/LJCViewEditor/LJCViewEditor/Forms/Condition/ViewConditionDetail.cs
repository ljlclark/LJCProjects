// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionDetail.cs
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
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
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"Filter\ConditionDetail.html";
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
          Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
            , LJCHelpPageName);
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
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
			{
				Text += " - New";
				LJCIsUpdate = false;
				LJCRecord = new ViewCondition();
				ParentTextbox.Text = LJCParentName;

        // Set default values.
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
      ViewCondition retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewCondition();
      }

      // In control order.
      retValue.FirstValue = ViewEditorCommon.TruncateAtHyphen(FirstValueCombo.Text);
      retValue.SecondValue = SecondValueTextbox.Text;
      retValue.ComparisonOperator = ComparisonTextbox.Text;

      // Get Reference key values.
      retValue.ID = LJCID;
      retValue.ViewConditionSetID = LJCParentID;
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
					var addedRecord = manager.Add(LJCRecord);
          if (addedRecord != null)
          {
            LJCRecord.ID = addedRecord.ID;
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
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Initialize Class Data.
      Managers = new ManagersDbView();
      Managers.SetDbProperties(mSettings.DbServiceRef
        , mSettings.DataConfigName);

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();
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

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      FirstValueCombo.KeyPress += TextBoxNoSpace_KeyPress;
      SecondValueTextbox.KeyPress += TextBoxNoSpace_KeyPress;
      ComparisonTextbox.KeyPress += TextBoxNoSpace_KeyPress;
      FirstValueCombo.TextChanged += TextBoxNoSpace_TextChanged;
      SecondValueTextbox.TextChanged += TextBoxNoSpace_TextChanged;
      ComparisonTextbox.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void ConditionHelp_Click(object sender, EventArgs e)
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
		private void TextBoxNoSpace_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = FormCommon.HandleSpace(e.KeyChar);
		}

		// Strips blanks from the text value.
		private void TextBoxNoSpace_TextChanged(object sender, EventArgs e)
		{
      if (sender is TextBox textbox)
      {
        var prevStart = textbox.SelectionStart;
        textbox.Text = FormCommon.StripBlanks(FirstValueCombo.Text);
        textbox.SelectionStart = prevStart;
      }
      if (sender is ComboBox combobox)
      {
        var prevStart = combobox.SelectionStart;
        combobox.Text = FormCommon.StripBlanks(FirstValueCombo.Text);
        combobox.SelectionStart = prevStart;
      }
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
		internal ViewCondition LJCRecord { get; private set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }

    // The Managers object.
    private ManagersDbView Managers { get; set; }
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
    private ViewCondition mOriginalRecord;
    private StandardUISettings mSettings;
		#endregion
	}
}

