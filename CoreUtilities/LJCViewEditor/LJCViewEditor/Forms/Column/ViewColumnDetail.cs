// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewColumnDetail.cs
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
  // The ViewColumn detail dialog.
  internal partial class ViewColumnDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ViewColumnDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "ViewEditor.chm";
      LJCHelpPageName = @"Column\ColumnDetail.html";
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
    private void ViewColumnDetail_KeyDown(object sender, KeyEventArgs e)
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
    private void ViewColumnDetail_Load(object sender, EventArgs e)
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
      Text = "ViewColumn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = Managers.ViewColumnManager;
        var dataRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(dataRecord);

        // Do not allow column change on update.
        TemplateColumnCombo.Enabled = false;
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new ViewColumn();
        ParentTextbox.Text = LJCParentName;
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(ViewColumn dataRecord)
    {
      // Also called from TemplateCombo_SelectedIndexChanged.
      if (dataRecord != null)
      {
        // In control order.
        ParentTextbox.Text = LJCParentName;

        // Only select a column if currently empty.
        // This is to allow on Edit but not TemplateCombo changed.
        if (null == TemplateColumnCombo.SelectedItem)
        {
          var dbColumn
            = mTableColumns.LJCSearchPropertyName(dataRecord.PropertyName);
          if (dbColumn != null)
          {
            mAllowTemplateGetValues = false;
            TemplateColumnCombo.SelectedItem = dbColumn;
          }
        }

        ColumnNameTextbox.Text = dataRecord.ColumnName;
        PropertyTextbox.Text = dataRecord.PropertyName;
        RenameTextbox.Text = dataRecord.RenameAs;
        CaptionTextbox.Text = dataRecord.Caption;
        DataTypeCombo.SelectedIndex = DataTypeCombo.FindStringExact(dataRecord.DataTypeName);
        ValueTextbox.Text = dataRecord.Value;
        PrimaryKeyCheckBox.Checked = dataRecord.IsPrimaryKey;

        // Reference key values.
        // Do not update parent values on TemplateCombo changed.
        if (dataRecord.ViewDataID > 0)
        {
          LJCParentID = dataRecord.ViewDataID;
        }
      }
    }

    // Creates and returns a record object with the data from
    private ViewColumn SetRecordValues()
    {
      ViewColumn retValue = new ViewColumn()
      {
        // In control order.
        ColumnName = ColumnNameTextbox.Text.Trim(),
        PropertyName = FormCommon.SetString(PropertyTextbox.Text.Trim()),
        RenameAs = FormCommon.SetString(RenameTextbox.Text.Trim()),
        Caption = FormCommon.SetString(CaptionTextbox.Text.Trim()),
        DataTypeName = DataTypeCombo.Text.Trim(),
        Value = FormCommon.SetString(ValueTextbox.Text.Trim()),
        IsPrimaryKey = PrimaryKeyCheckBox.Checked,

        // Get Reference key values.
        ID = LJCID,
        ViewDataID = LJCParentID
      };
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(ViewColumn record)
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
      var manager = Managers.ViewColumnManager;
      var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.ViewDataID
        , LJCRecord.ColumnName);
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
          var keyColumns = manager.IDKey(LJCID);
          LJCRecord.ID = 0;
          manager.Update(LJCRecord, keyColumns);
          LJCRecord.ID = LJCID;
          ResetRecordValues(LJCRecord);
          if (0 == manager.AffectedCount)
          {
            retValue = false;
            var title = "Update Error";
            var message = "The Record was not updated.";
            Cursor = Cursors.Default;
            MessageBox.Show(message, title, MessageBoxButtons.OK
              , MessageBoxIcon.Information);
          }
        }
        else
        {
          LJCRecord.ID = 0;
          ViewColumn addedRecord = manager.AddWithFlags(LJCRecord);
          ResetRecordValues(LJCRecord);
          if (null == addedRecord)
          {
            if (manager.AffectedCount < 1)
            {
              var title = "Add Error";
              var message = "The Record was not added.";
              Cursor = Cursors.Default;
              MessageBox.Show(message, title, MessageBoxButtons.OK
                , MessageBoxIcon.Information);
            }
          }
          else
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

      if (false == NetString.HasValue(ColumnNameTextbox.Text))
      {
        retVal = false;
        builder.AppendLine($"  {ColumnNameLabel.Text}");
      }
      if (false == NetString.HasValue(DataTypeCombo.Text))
      {
        retVal = false;
        builder.AppendLine($"  {DataTypeLabel.Text}");
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
      mDbServiceRef = mSettings.DbServiceRef;
      mDataConfigName = mSettings.DataConfigName;

      // Initialize Class Data.
      mDataDbView = new DataDbView(Managers);

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();

      ColumnNameTextbox.MaxLength = ViewColumn.LengthColumnName;
      PropertyTextbox.MaxLength = ViewColumn.LengthPropertyName;
      CaptionTextbox.MaxLength = ViewColumn.LengthCaption;
      ValueTextbox.MaxLength = ViewColumn.LengthValue;
      RenameTextbox.MaxLength = ViewColumn.LengthRenameAs;

      // Load control data.
      // Template Columns Combo
      DataHelper dataHelper = new DataHelper(mDbServiceRef, mDataConfigName);
      mTableColumns = dataHelper.GetTableColumns(LJCTableName);
      foreach (DbColumn dbColumn in mTableColumns)
      {
        TemplateColumnCombo.Items.Add(dbColumn);
      }

      // Data Types Combo
      DataTypeManager dataTypeManager;
      try
      {
        dataTypeManager = new DataTypeManager(mDbServiceRef, mDataConfigName);
      }
      catch (SystemException e)
      {
        ViewEditorCommon.CreateTables(e, mDataConfigName);
        dataTypeManager = new DataTypeManager(mDbServiceRef, mDataConfigName);
      }
      LJCViewEditorDAL.DataTypes dataTypes = dataTypeManager.Load();
      foreach (DataType dataType in dataTypes)
      {
        DataTypeCombo.Items.Add(dataType);
      }
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      ColumnNameTextbox.KeyPress += TextboxNoSpace_KeyPress;
      PropertyTextbox.KeyPress += TextboxNoSpace_KeyPress;
      RenameTextbox.KeyPress += TextboxNoSpace_KeyPress;
      ColumnNameTextbox.TextChanged += TextboxNoSpace_TextChanged;
      PropertyTextbox.TextChanged += TextboxNoSpace_TextChanged;
      RenameTextbox.TextChanged += TextboxNoSpace_TextChanged;
    }
    #endregion

    #region Action Event Handlers

    // Shows the Help page.
    private void ViewColumnHelp_Click(object sender, EventArgs e)
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

    // Handles the SelectionIndexChanged event.
    private void TemplateCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (mAllowTemplateGetValues)
      {
        DbColumn dbColumn = TemplateColumnCombo.SelectedItem as DbColumn;
        ViewColumn viewColumn = mDataDbView.GetViewColumnFromDbColumn(dbColumn);
        GetRecordValues(viewColumn);
      }
      mAllowTemplateGetValues = true;
    }
    private bool mAllowTemplateGetValues;
    #endregion

    #region KeyEdit Event Handlers

    // Does not allow spaces.
    private void TextboxNoSpace_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void TextboxNoSpace_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textbox)
      {
        var prevStart = textbox.SelectionStart;
        textbox.Text = FormCommon.StripBlanks(ColumnNameTextbox.Text);
        textbox.SelectionStart = prevStart;
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
    internal ViewColumn LJCRecord { get; set; }

    internal ManagersDbView Managers { get; set; }

    // Gets or sets the BeginColor value.
    private Color BeginColor { get; set; }

    // Gets or sets the Parent ID value.
    private Color EndColor { get; set; }
    #endregion

    #region Custom Properties

    //// Gets or sets the LJCDataConfigName value.
    //internal string LJCDataConfigName { get; set; }

    //// Gets or sets the LJCDbServiceRef value.
    //internal DbServiceRef LJCDbServiceRef { get; set; }

    // Gets or sets the TableName value.
    internal string LJCTableName { get; set; }
    #endregion

    #region Class Data

    private StandardUISettings mSettings;
    private DbServiceRef mDbServiceRef;
    private string mDataConfigName;
    private DbColumns mTableColumns;
    private DataDbView mDataDbView;

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
