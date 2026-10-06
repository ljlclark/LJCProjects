// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ItemDetail5.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCGenTextEdit5
{
  // The Item detail dialog.
  /// <include file='Doc/ItemDetail.xml'
  ///  path='items/ItemDetail/*'/>
  public partial class ItemDetail : Form
  {
    #region Properties

    // Gets or sets the Item Manager reference.
    internal RepeatItemManager ItemManager { get; set; } = null!;

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

    // Gets or sets the primary ID value.
    internal string? LJCItemName
    {
      get => mName;
      set
      {
        mName = value?.Trim();
      }
    }
    private string? mName;

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
    internal RepeatItem LJCRecord { get; private set; } = null!;

    // Gets or sets the GenData Manager reference.
    internal SectionManager SectionManager { get; set; } = null!;

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
    public ItemDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "GenTextEdit.chm";
      LJCIsUpdate = false;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ItemDetail_Load(object sender, EventArgs e)
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
      Text = "Item Detail";
      if (LJC.HasText(LJCParentName)
        && LJC.HasText(LJCItemName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalName = LJCItemName;
        var dataRecord = ItemManager.Retrieve(LJCParentName, LJCItemName);
        if (dataRecord != null)
        {
          GetRecordValues(dataRecord);
        }
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new RepeatItem();
        ParentNameTextbox.Text = LJCParentName;
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(RepeatItem dataRecord)
    {
      if (dataRecord != null)
      {
        ParentNameTextbox.Text = LJCParentName;
        NameText.Text = dataRecord.Name;
      }
    }

    // Creates and returns a record object with the data from
    // the controls.
    private RepeatItem SetRecordValues()
    {
      RepeatItem retValue = new()
      {
        Name = FormCommon.SetString(NameText.Text),
      };
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      RepeatItem? lookupRecord;
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      while (true)
      {
        if (!LJC.HasText(LJCParentName))
        {
          Cursor = Cursors.Default;
          break;
        }

        // Lookup record on unique key.
        lookupRecord = ItemManager.Retrieve(LJCParentName, LJCRecord.Name);
        if (IsDuplicate(lookupRecord))
        {
          retValue = false;
          title = "Data Entry Error";
          message = "The record already exists.";
          Cursor = Cursors.Default;
          MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          break;
        }

        if (LJCIsUpdate)
        {
          // Update record on primary key.
          //lookupRecord = ItemManager.Retrieve(LJCParentName, mOriginalName);
          lookupRecord = ItemManager.Retrieve(LJCParentName, LJCRecord.Name);
          if (lookupRecord != null)
          {
            lookupRecord.Name = LJCRecord.Name;

            // Sort if name is changed.
            if (!string.Equals(LJCRecord.Name, mOriginalName
              , StringComparison.OrdinalIgnoreCase))
            {
              var repeatItems = ItemManager.Load(LJCParentName);
              repeatItems?.Sort();
            }
          }
        }
        else
        {
          // Add new record.
          ItemManager.Add(LJCParentName, LJCRecord);
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
    //private bool IsDuplicate(RepeatItem lookupRecord, RepeatItem currentRecord)
    private bool IsDuplicate(RepeatItem? lookupRecord)
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
        builder.AppendLine("  {NameLabel.Text}");
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
      //EndColor = Color.LightSkyBlue;
      EndColor = Color.SkyBlue;

      // Initialize Class Data.
      //NameLabel.BackColor = BeginColor;

      // Set control values.
      SetNoSpace();
      NameText.MaxLength = 60;

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
        , @"Data\Item\ItemDetail.html");
    }
    #endregion

    #region Control Event Handlers

    // Handles the control keys.
    private void ItemDetail_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , @"Data\Item\ItemDetail.html");
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
