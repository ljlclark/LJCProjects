// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSetDetail.cs
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCWinFormCommon;
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
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"Filter\ConditionSetDetail.html";
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
          Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
            , LJCHelpPageName);
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
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(mOriginalRecord);
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
        // In control order.
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
      ViewConditionSet retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewConditionSet();
      }

      // In control order.
      retValue.BooleanOperator = OperatorCombo.Text;

      // Get Reference key values.
      retValue.ID = LJCID;
      retValue.ViewFilterID = LJCParentID;

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
          var keyColumns = manager.IDKey(LJCID);
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

      if (!NetString.HasValue(OperatorCombo.Text))
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
      var values = ValuesViewEditor.Instance;
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

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
    internal ViewConditionSet LJCRecord { get; private set; }

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

    private ViewConditionSet mOriginalRecord;
    private StandardUISettings mSettings;
    #endregion
  }
}
