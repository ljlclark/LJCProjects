// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #SectionBegin Class
// #Value _NameSpace_
// #Value _ClassName_
// #Value _AppName_
// _ClassName_Detail.cs
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using _FullAppName_DAL;

namespace _Namespace_
{
  // The _ClassName_ detail dialog.
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  internal partial class _ClassName_Detail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal _ClassName_Detail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFileName = "_AppName_.chm";
      LJCHelpPageName = "_ClassName_Detail.html";
      LJCID = 0;
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Handles the form keys.
    private void _ClassName_Detail_KeyDown(object sender, KeyEventArgs e)
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
    private void _ClassName_Detail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Paint the form background.
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "_ClassName_ Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = Managers._ClassName_Manager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new _ClassName_();
        //ParentNameText.Text = LJCParentName;

        // Set Type combos.
        //ItemTypeCombo.LJCSetByItemID(LJCItemTypeID);

        // Set default values.
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(_ClassName_ dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        //ParentNameText.Text = LJCParentName;
        NameText.Text = dataRecord.Name;
        DescriptionText.Text = dataRecord.Description;
        //ItemTypeCombo.LJCSetByItemID(LJCItemTypeID);

        // Reference key values.
        //LJCParentID = record.ParentId;

        // Get foreign key values.
        var item = GetItemWithID(dataRecord.ForeignKeyID);
        if (item != null)
        {
          mForeignKeyID = item.ID;
          NameText.Text = item.Name;
        }
      }
    }

    // Creates and returns a record object with the data from
    private _ClassName_ SetRecordValues()
    {
      _ClassName_ retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new _ClassName_();
      }

      // In control order.
      //retValue.ItemTypeID = ItemTypeCombo.LJCSelectedItemID(),
      retValue.Name = FormCommon.SetString(NameText.Text);
      retValue.Description = FormCommon.SetString(DescriptionText.Text);
      //int.TryParse(IntegerText.Text, out int value);
      //retValue.Value = value;

      // Get Reference key values.
      retValue.ID = LJCID;
      //retValue.ParentID = LJCParentID;
      //ForeignKeyID = mForeignKeyID,

      // Get control join display values.
      //TypeDescription = TypeCombo.Text.Trim()
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(_ClassName_ dataRecord)
    {
      // In control order.
      dataRecord.Description = FormCommon.SetString(dataRecord.Description);
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = Managers._ClassName_Manager;
      var lookupRecord = manager.RetrieveWithUniqueKey(LJCRecord.Name);
      if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
          ResetRecordValues(LJCRecord);
          LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this, manager.AffectedCount);
        }
        else
        {
          LJCRecord.ID = 0;
          var addedRecord = manager.Add(LJCRecord);
          ResetRecordValues(LJCRecord);
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
      bool retValue = true;

      var builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!NetString.HasValue(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }

      if (!retValue)
      {
        var title = "Data Entry Error";
        var message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = Values_AppName_.Instance;
      Managers = values.Managers;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Initialize Class Data.
      //mItemTypeComboCode = new ItemTypeComboCode(Managers)
      //{
      //	UnitSystemCombo = SystemCombo
      //};

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();
      DescriptionText.MaxLength = _ClassName_.LengthDescription;

      // Load control data.
      //ItemTypeComboCode.LoadCombo();

      // Set control layout.
      //if (0 == LJCParentID)
      //{
      //	ParentNameLabel.Visible = false;
      //	ParentNameText.Visible = false;
      //	Height -= ParentNameText.Height;
      //}

      ConfigureControls();
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }

    // Sets the NoSpace events.
    private void SetNumericOnly()
    {
      textBox.KeyPress += TextBoxNumeric_KeyPress;
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region Action Event Handlers

    // Save and setup for a new record.
    private void DialogNew_Click(object sender, EventArgs e)
    {
      FormCancelButton.Select();
      if (IsDataSaved())
      {
        LJCPrevious = false;
        LJCNext = false;
        LJCOnChange();

        // Initialize property values.
        LJCID = 0;
        NameText.Text = "";

        DataRetrieve();
      }
    }

    // Save and move to the Next record.
    private void DialogNext_Click(object sender, EventArgs e)
    {
      if (IsDataSaved())
      {
        LJCNext = true;
        LJCOnChange();
        if (LJCNext)
        {
          LJCNext = false;
          DataRetrieve();
        }
        else
        {
          Close();
        }
      }
    }

    // Save and move to the Previous record.
    private void DialogPrevious_Click(object sender, EventArgs e)
    {
      if (IsDataSaved())
      {
        LJCPrevious = true;
        LJCOnChange();
        if (LJCPrevious)
        {
          LJCPrevious = false;
          DataRetrieve();
        }
        else
        {
          Close();
        }
      }
    }

    // Shows the help page.
    private void DialogMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic
        , LJCHelpPageName);
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
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
      if (sender is TextBox textBox)
      {
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
      }
    }

    // Only allows numbers or edit keys.
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
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

    // Gets or sets the primary ID value.
    internal int LJCID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    //// Gets or sets the ItemType ID value.
    //internal int LJCItemTypeID { get; set; }

    /// <summary>Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    // Gets or sets the Parent ID value.
    internal int LJCParentID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets a reference to the record object.
    internal _ClassName_ LJCRecord { get; private set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // The Managers object.
    private Managers_AppName_ Managers { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private int mForeignKeyId;
    //private ItemTypeComboCode mItemTypeComboCode;
    private _ClassName_ mOriginalRecord;
    private StandardUISettings mSettings;
    #endregion
  }
}
// #SectionEnd Class
