// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DetailTemplate.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
// #SectionBegin Class
// #Value _NameSpace_
// #Value _ClassName_
// #Value _AppName_
using _FullAppName_DAL;

namespace _Namespace_
{
  /// <summary>The _ClassName_ detail dialog.</summary>
  /// <remarks>
  /// <para>-- Library Level Remarks</para>
  /// </remarks>
  internal partial class _ClassName_Detail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Detail.xml'/>
    internal _ClassName_Detail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;
      LJCIsUpdate = false;
      LJCHelpFileName = "_AppName_.chm";
      LJCHelpPageName = "_ClassName_Detail.htm";

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void _ClassName_Detail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Handles the form keys.
    private void _ClassName_Detail_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFileName, HelpNavigator.Topic, LJCHelpPageName);
          break;
      }
    }

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/Detail.xml'/>
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "_ClassName_ Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = GetWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        //ParentNameText.Text = LJCParentName;

        // Set Type combos.
        //ItemTypeCombo.LJCSetByItemID(LJCItemTypeID);

        // Set default values.
        LJCRecord = new _ClassName_();
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(_ClassName_ dataRecord)
    {
      //	_ClassName_ lookupRecord;

      if (dataRecord != null)
      {
        //LJCParentID = record.ParentId;
        //ParentNameText.Text = LJCParentName;

        // Set Type combos.
        //ItemTypeCombo.LJCSetByItemID(LJCItemTypeID);

        NameText.Text = dataRecord.Name;
        DescriptionText.Text = dataRecord.Description;

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
      var retValue = mOriginalRecord;
      if (null == retValue)
      {
        retValue = new DocMethod();
      }
      retValue.ID = LJCID;
      //retValue.ParentID = LJCParentID;
      //retValue.ItemTypeID = ItemTypeCombo.LJCSelectedItemID(),
      //ForeignKeyID = mForeignKeyID,
      retValue.Name = FormCommon.SetString(NameText.Text);
      retValue.Description = FormCommon.SetString(DescriptionText.Text);

      // Get control join display values.
      //TypeDescription = TypeCombo.Text.Trim()

      //int.TryParse(IntegerText.Text, out int value);
      //retValue.Value = value;
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(_ClassName_ dataRecord)
    {
      dataRecord.Description = FormCommon.SetString(dataRecord.Description);
    }

    // Saves the data.
    private bool DataSave()
    {
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      var manager = Managers._ClassName_Manager;
      var keyRecord = manager.GetNameKey(LJCRecord.Name);
      var lookupRecord = manager.Retrieve(keyRecord);
      if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
      {
        retValue = false;
        title = "Data Entry Error";
        message = "The record already exists.";
        Cursor = Cursors.Default;
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyRecord = manager.GetIDKey(LJCRecord.ID);
          manager.Update(LJCRecord, keyRecord);
          ResetRecordValues(LJCRecord);
          if (0 == manager.Manager.AffectedCount)
          {
            title = "Update Error";
            message = "The Record was not updated.";
            MessageBox.Show(message, title, MessageBoxButtons.OK
              , MessageBoxIcon.Information);
          }
        }
        else
        {
          var addedRecord = manager.Add(LJCRecord);
          ResetRecordValues(LJCRecord);
          if (null == addedRecord)
          {
            if (manager.AffectedCount < 1)
            {
              title = "Add Error";
              message = "The Record was not added.";
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

      // Get join display values.

      Cursor = Cursors.Default;
      return retValue;
    }

    // Validates the data.
    private bool IsValid()
    {
      StringBuilder builder;
      string title;
      string message;
      bool retValue = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (false == NetString.HasValue(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }

      if (retValue == false)
      {
        title = "Data Entry Error";
        message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Get Data Methods

    // Retrieves the Product with the ID value.
    private _ClassName_ GetWithID(long id)
    {
      _ClassName_ retValue = null;

      if (id > 0)
      {
        var manager = Managers._ClassName_Manager;
        var keyRecord = manager.GetIDKey(LJCID);
        retValue = manager.Retrieve(keyRecord);
      }
      return retValue;
    }
    #endregion

    #region Action Methods

    // Save and setup for a new record.
    private void DialogNew_Click(object sender, EventArgs e)
    {
      FormCancelButton.Select();
      if (IsValid() && DataSave())
      {
        LJCOnChange();

        // Initialize property values.
        LJCID = 0;
        NameText.Text = "";

        DataRetrieve();
      }
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = Values_AppName_.Instance;
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
      SetNoSpace(NameText);

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
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNumericOnly(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNumeric_KeyPress;
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }

    // Sets the NoSpace events.
    private void SetNoSpace(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region Action Event Handlers

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
      if (IsValid()
        && DataSave())
      {
        LJCOnChange();
        DialogResult = DialogResult.OK;
      }
    }
    #endregion

    #region KeyEdit Event Handlers

    // Only allows numbers or edit keys.
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }

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
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the LJCHelpFileName value.</summary>
    public string LJCHelpFileName
    {
      get { return mHelpFileName; }
      set { mHelpFileName = NetCommon.InitString(value); }
    }
    private string mHelpFileName;

    /// <summary>Gets or sets the LJCHelpPageName value.</summary>
    public string LJCHelpPageName
    {
      get { return mHelpPageName; }
      set { mHelpPageName = NetCommon.InitString(value); }
    }
    private string mHelpPageName;

    /// <summary>Gets or sets the primary ID value.</summary>
    public int LJCID { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    public bool LJCIsUpdate { get; private set; }

    ///// <summary>Gets or sets the ItemType ID value.</summary>
    //public int LJCItemTypeID { get; set; }

    /// <summary>Gets or sets the Parent ID value.</summary>
    public int LJCParentID { get; set; }

    /// <summary>Gets or sets the LJCParentName value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets a reference to the record object.</summary>
    public _ClassName_ LJCRecord { get; private set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // The Managers object.
    private _ClassName_Managers Managers { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;

    // Foreign Keys
    private int mForeignKeyId;

    // 
    private DataRecord mOriginalRecord;

    private StandardUISettings mSettings;
    //private ItemTypeComboCode mItemTypeComboCode;
    #endregion
  }
}
// #SectionEnd Class
