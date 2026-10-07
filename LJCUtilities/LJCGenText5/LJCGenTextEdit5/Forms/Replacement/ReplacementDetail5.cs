// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ReplacementDetail5.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCGenTextEdit5
{
  // The Replacement detail dialog.
  /// <include file='Doc/ReplacementDetail.xml'
  ///  path='items/ReplacementDetail/*'/>
  public partial class ReplacementDetail : Form
  {
    #region Properties

    // The help file name.
    internal string? LJCHelpFile
    {
      get => mHelpFile;
      set
      {
        mHelpFile = value?.Trim();
      }
    }
    private string? mHelpFile;

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // Gets or sets the Parent ID value.
    internal string? LJCParentName
    {
      get => mParentName;
      set
      {
        mParentName = value?.Trim();
      }
    }
    private string? mParentName;

    // Gets a reference to the record object.
    internal Replacement LJCRecord { get; private set; } = null!;

    // Gets or sets the Replacement Manager reference.
    internal ReplacementManager ReplacementManager { get; set; } = null!;

    // Gets or sets the primary ID value.
    internal string? LJCReplacementName
    {
      get => mName;
      set
      {
        mName = value?.Trim();
      }
    }
    private string? mName;

    // Gets or sets the Section ID value.
    internal string? LJCSectionName
    {
      get => mSectionName;
      set
      {
        mSectionName = value?.Trim();
      }
    }
    private string? mSectionName;

    // Gets or sets the Section Manager reference.
    internal SectionManager SectionManager
    {
      get => mSectionManager;
      set
      {
        mSectionManager = value;
        if (mSectionManager != null)
        {
          Sections sections = mSectionManager.Load();
          ReplacementManager = new ReplacementManager(sections);
        }
      }
    }
    private SectionManager mSectionManager = null!;

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private string mOriginalName = null!;

    // The Change event.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/LJCChange/*'/>
    public event EventHandler<EventArgs> LJCChange = null!;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public ReplacementDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenTextEdit.chm";
      LJCIsUpdate = false;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ReplacementDetail_Load(object sender, EventArgs e)
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

      //FormCommon.CreateGradient(e.Graphics, ClientRectangle
      //  , BeginColor, EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Replacement Detail";
      if (LJC.HasText(LJCSectionName)
        && LJC.HasText(LJCParentName)
        && LJC.HasText(LJCReplacementName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalName = LJCReplacementName;
        var dataRecord = ReplacementManager.Retrieve(LJCSectionName, LJCParentName
          , LJCReplacementName);
        if (dataRecord != null)
        {
          GetRecordValues(dataRecord);
        }
        ValueTextbox.Select();
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new Replacement();
        ParentNameText.Text = LJCParentName;
        NameText.Select();
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(Replacement dataRecord)
    {
      if (dataRecord != null)
      {
        ParentNameText.Text = LJCParentName;
        NameText.Text = dataRecord.Name;
        ValueTextbox.Text = dataRecord.Value;
      }
    }

    // Creates and returns a record object with the data from
    // the controls.
    private Replacement SetRecordValues()
    {
      Replacement retValue = new()
      {
        Name = NameText.Text.Trim(),
        Value = ValueTextbox.Text.Trim()
      };
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      Replacement? lookupRecord;
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      while (true)
      {
        if (!LJC.HasText(LJCSectionName)
          || !LJC.HasText(LJCParentName))
        {
          break;
        }

        // Lookup record on unique key.
        lookupRecord = ReplacementManager.Retrieve(LJCSectionName
          , LJCParentName, LJCRecord.Name);
        if (IsDuplicate(lookupRecord))
        {
          retValue = false;
          title = "Data Entry Error";
          message = "The record already exists.";
          MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          break;
        }

        if (LJCIsUpdate)
        {
          // Update record on primary key.
          lookupRecord = ReplacementManager.Retrieve(LJCSectionName
          , LJCParentName, mOriginalName);
          if (lookupRecord != null)
          {
            lookupRecord.Name = LJCRecord.Name;
            lookupRecord.Value = LJCRecord.Value;

            // Sort if name is changed.
            if (!LJC.IsEqual(LJCRecord.Name, mOriginalName))
            {
              var replacements
                = ReplacementManager.Load(LJCSectionName, LJCParentName);
              replacements?.Sort();
            }
          }
        }
        else
        {
          // Add new record.
          ReplacementManager.Add(LJCSectionName, LJCParentName, LJCRecord);
        }
        SectionManager.Save();
        break;
      }
      Cursor = Cursors.Default;
      return retValue;
    }
    #endregion

    #region Private Methods

    // Check for duplicate unique key.
    //private bool IsDuplicate(Replacement lookupRecord, Replacement currentRecord)
    private bool IsDuplicate(Replacement? lookupRecord)
    {
      bool retValue = false;

      if (lookupRecord != null)
      {
        if (!LJCIsUpdate)
        {
          // Duplicate for "New" record that already exists.
          retValue = true;
        }
        else
        {
          if (lookupRecord.Name != mOriginalName)
          {
            // Duplicate for "Update" where unique key is modified.
            retValue = true;
          }
        }
      }
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

      if (!LJC.HasText(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }
      if (!LJC.HasText(ValueTextbox.Text))
      {
        retValue = false;
        builder.AppendLine($"  {ValueLabel.Text}");
      }

      if (retValue == false)
      {
        title = "Data Entry Error";
        message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      BeginColor = Color.AliceBlue;
      EndColor = Color.SkyBlue;

      // Initialize Class Data.
      //FormCommon.SetLabelsBackColor(Controls, BeginColor);

      // Set control values.
      SetNoSpace();
      NameText.MaxLength = 60;
      ValueTextbox.MaxLength = 100;

      // Load control data.

      // Set control layout.
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
    }
    #endregion

    #region Action Event Handlers

    // Displays the context sensitive help.
    private void DetailMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Data\Replacement\ReplacementDetail.html");
    }
    #endregion

    #region Control Event Handlers

    // Handles the control keys.
    private void ReplacementDetail_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\Replacement\ReplacementDetail.html");
          break;
      }
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

    // Closes the form without saving the data.
    private void FormCancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    // Fires the Change event.
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }
    #endregion
  }
}
