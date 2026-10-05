// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SectionDetail5.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCGenTextEdit5
{
  /// <summary>The Section detail dialog.</summary>
  public partial class SectionDetail : Form
  {
    #region Properties

    // Gets or sets the GenData Manager reference.
    internal SectionManager SectionManager { get; set; } = null!;

    // The help file name.
    internal string? LJCHelpFile
    {
      get => mHelpFile;
      set
      {
        mHelpFile = value?.Trim();
      }
    }
    private string? mHelpFile = null!;

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // Gets a reference to the record object.
    internal Section LJCRecord { get; private set; } = null!;

    // Gets or sets the primary ID value.
    internal string? LJCSectionName
    {
      get => mName;
      set
      {
        mName = value?.Trim();
      }
    }
    private string? mName = null!;

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private string mOriginalName = null!;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange = null!;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public SectionDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenTextEdit.chm";
      LJCIsUpdate = false;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void SectionDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;

      InitializeControls();
      DataRetrieve();
      //CenterToParent();
      Location = LJCLocation;
    }

    // Paint the form background.
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/OnPaintBackground/*'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);

      //FormCommon.CreateGradient(e.Graphics, ClientRectangle
      //  , BeginColor, EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/DataRetrieve/*'/>
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Section Detail";
      if (LJC.HasText(LJCSectionName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalName = LJCSectionName;
        var dataRecord = SectionManager.Retrieve(LJCSectionName);
        if (dataRecord != null)
        {
          GetRecordValues(dataRecord);
        }
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new Section();
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/GetRecordValues/*'/>
    private void GetRecordValues(Section dataRecord)
    {
      if (dataRecord != null)
      {
        NameTextbox.Text = dataRecord.Name;
      }
    }

    // Creates and returns a record object with the data from
    // the controls.
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/SetRecordValues/*'/>
    private Section SetRecordValues()
    {
      Section retValue = new()
      {
        Name = FormCommon.SetString(NameTextbox.Text),
      };
      return retValue;
    }

    // Saves the data.
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/DataSave/*'/>
    private bool DataSave()
    {
      Section? lookupRecord;
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      // Lookup record on unique key.
      lookupRecord = SectionManager.Retrieve(LJCRecord.Name);
      //if (IsDuplicate(lookupRecord, LJCRecord))
      if (IsDuplicate(lookupRecord))
      {
        retValue = false;
        title = "Data Entry Error";
        message = "The record already exists.";
        Cursor = Cursors.Default;
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          // Update record on primary key.
          lookupRecord = SectionManager.Retrieve(mOriginalName);
          if (lookupRecord != null)
          {
            lookupRecord.Name = LJCRecord.Name;

            // *** Begin *** Add 9/13/26
            // Sort if name is changed.
            if (!string.Equals(LJCRecord.Name, mOriginalName
              , StringComparison.OrdinalIgnoreCase))
            {
              var sections = SectionManager.Load();
              sections.Sort();
            }
            // *** End ***
          }
        }
        else
        {
          // Add new record.
          SectionManager.Add(LJCRecord);
        }
        SectionManager.Save();
      }
      Cursor = Cursors.Default;
      return retValue;
    }
    #endregion

    #region Private Methods

    // Check for duplicate unique key.
    //private bool IsDuplicate(Section lookupRecord, Section currentRecord)
    private bool IsDuplicate(Section? lookupRecord)
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
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/IsValid/*'/>
    private bool IsValid()
    {
      StringBuilder builder;
      string title;
      string message;
      bool retValue = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!LJC.HasText(NameTextbox.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
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
    /// <include file='../../LJCGenDoc/Common/Detail.xml'
    ///  path='items/InitializeControls/*'/>
    private void InitializeControls()
    {
      BeginColor = Color.AliceBlue;
      //EndColor = Color.LightSkyBlue;
      EndColor = Color.SkyBlue;

      // Initialize Class Data.
      //NameLabel.BackColor = BeginColor;

      // Set control values.
      SetNoSpace();
      NameTextbox.MaxLength = 60;

      // Load control data.

      // Set control layout.
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameTextbox.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameTextbox.TextChanged += FormCommon.TextNoSpaceChanged;
    }
    #endregion

    #region Action Event Handlers

    // Displays the context sensitive help.
    private void DetailMenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , @"Data\Section\SectionDetail.html");
    }
    #endregion

    #region Control Event Handlers

    // Handles the control keys.
    private void SectionDetail_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\Section\SectionDetail.html");
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

    /// <summary>Fires the Change event.</summary>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }
    #endregion
  }
}
