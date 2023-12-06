// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOnDetail.cs
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
  // The ViewJoinOn detail dialog.
  internal partial class ViewJoinOnDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ViewJoinOnDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"Join\JoinOnDetail.html";
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
    private void ViewJoinOnDetail_KeyDown(object sender, KeyEventArgs e)
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
    private void ViewJoinOnDetail_Load(object sender, EventArgs e)
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
      Text = "ViewJoinOn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = Managers.ViewJoinOnManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new ViewJoinOn();
        ParentTextbox.Text = LJCParentName;

        // Set default values.
        OperatorTextbox.Text = "=";
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(ViewJoinOn dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
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
      ViewJoinOn retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ViewJoinOn();
      }

      // In control order.
      retValue.FromColumnName = ViewEditorCommon.TruncateAtHyphen(FromColumnCombo.Text);
      retValue.ToColumnName = ViewEditorCommon.TruncateAtHyphen(ToColumnCombo.Text);
      retValue.JoinOnOperator = OperatorTextbox.Text;

      // Get Reference key values.
      retValue.ID = LJCID;
      retValue.ViewJoinID = LJCParentID;
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
        FormCommon.DataError(this);
      }

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.GetIDKey(LJCRecord.ID);
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
      Cursor = Cursors.WaitCursor;
      var values = ValuesViewEditor.Instance;
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();
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
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      FromColumnCombo.KeyPress += TextBoxNoSpace_KeyPress;
      ToColumnCombo.KeyPress += TextBoxNoSpace_KeyPress;
      OperatorTextbox.KeyPress += TextBoxNoSpace_KeyPress;
      FromColumnCombo.TextChanged += TextBoxNoSpace_TextChanged;
      ToColumnCombo.TextChanged += TextBoxNoSpace_TextChanged;
      OperatorTextbox.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void JoinOnHelp_Click(object sender, EventArgs e)
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
        textbox.Text = FormCommon.StripBlanks(textbox.Text);
        textbox.SelectionStart = prevStart;
      }
      if (sender is ComboBox combobox)
      {
        var prevStart = combobox.SelectionStart;
        combobox.Text = FormCommon.StripBlanks(combobox.Text);
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
    internal ViewJoinOn LJCRecord { get; private set; }

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

    private DbColumns mJoinOnTableColumns;
    private DbColumns mJoinTableColumns;
    private ViewJoinOn mOriginalRecord;
    private StandardUISettings mSettings;
    #endregion
  }
}
