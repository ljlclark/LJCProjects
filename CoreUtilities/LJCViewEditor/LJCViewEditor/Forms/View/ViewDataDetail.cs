// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewDataDetail.cs
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
  // The ViewData detail dialog.
  internal partial class ViewDataDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ViewDataDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"View\ViewDetail.html";
      LJCID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.LightBlue;
      EndColor = Color.SkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Handles the control keys.
    private void ViewDataDetail_KeyDown(object sender, KeyEventArgs e)
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
    private void ViewDataDetail_Load(object sender, EventArgs e)
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
      //  , BeginColor, EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      var manager = Managers.ViewDataManager;
      Text = "ViewData Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new ViewData();
        ParentTextbox.Text = LJCParentName;

        // Set default values.
        var name = $"{LJCParentName}Standard";
        var keyRecord = new DbColumns()
        {
          { ViewData.ColumnName, (object)name }
        };

        var dataRecord = manager.Retrieve(keyRecord);
        if (null == dataRecord)
        {
          NameTextbox.Text = name;
          DescriptionTextbox.Text = $"{LJCParentName} Standard View";
        }
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(ViewData dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        ParentTextbox.Text = LJCParentName;
        NameTextbox.Text = dataRecord.Name;
        DescriptionTextbox.Text = dataRecord.Description;

        // Reference key values.
        LJCParentID = dataRecord.ViewTableID;
      }
    }

    // Creates and returns a record object with the data from
    private ViewData SetRecordValues()
    {
      ViewData retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewData();
      }

      // In control order.
      retValue.Name = NameTextbox.Text;
      retValue.Description = DescriptionTextbox.Text;

      // Get Reference key values.
      retValue.ID = LJCID;
      retValue.ViewTableID = LJCParentID;
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = Managers.ViewDataManager;
      var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ViewTableID
        , LJCRecord.Name);
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
          var updateKeyColumns = new DbColumns()
          {
            { ViewData.ColumnID, LJCRecord.ID }
          };
          LJCRecord.ID = 0;
          manager.Update(LJCRecord, updateKeyColumns);
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
      StringBuilder builder;
      string title;
      string message;
      bool retVal = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!NetString.HasValue(NameTextbox.Text))
      {
        retVal = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }
      if (!NetString.HasValue(DescriptionTextbox.Text))
      {
        retVal = false;
        builder.AppendLine($"  {DescriptionLabel.Text}");
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
      Cursor = Cursors.WaitCursor;
      var values = ValuesViewEditor.Instance;
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Initialize Class Data.
      //mViewDataManager = new ViewDataManager(mSettings.DbServiceRef
      //  , mSettings.DataConfigName);

      // Set control values.
      // *** Next Statement *** Delete
      //FormCommon.SetLabelsBackColor(Controls, BeginColor);
      NameTextbox.MaxLength = ViewData.LengthName;
      DescriptionTextbox.MaxLength = ViewData.LengthDescription;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void ViewDetailHelp_Click(object sender, EventArgs e)
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
    private void NameTextbox_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void NameTextbox_TextChanged(object sender, EventArgs e)
    {
      var prevStart = NameTextbox.SelectionStart;
      NameTextbox.Text = FormCommon.StripBlanks(NameTextbox.Text);
      NameTextbox.SelectionStart = prevStart;
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
    internal ViewData LJCRecord { get; private set; }

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
    private ViewData mOriginalRecord;
    private StandardUISettings mSettings;
    #endregion
  }
}
