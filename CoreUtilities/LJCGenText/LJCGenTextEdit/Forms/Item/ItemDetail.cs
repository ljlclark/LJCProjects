// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ItemDetail.cs
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  /// <summary>The Item detail dialog.</summary>
  public partial class ItemDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
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
    /// <include path='items/DataRetrieve/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Item Detail";
      if (NetString.HasValue(LJCParentName)
        && NetString.HasValue(LJCItemName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalName = LJCItemName;
        var dataRecord = LJCGenDataManager.RetrieveRepeatItem(LJCParentName
          , LJCItemName);
        GetRecordValues(dataRecord);
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
    /// <include path='items/GetRecordValues/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private void GetRecordValues(RepeatItem dataRecord)
    {
      if (dataRecord != null)
      {
        ParentNameTextbox.Text = LJCParentName;
        NameTextbox.Text = dataRecord.Name;
      }
    }

    // Creates and returns a record object with the data from
    // the controls.
    /// <include path='items/SetRecordValues/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private RepeatItem SetRecordValues()
    {
      RepeatItem retValue = new RepeatItem()
      {
        Name = FormCommon.SetString(NameTextbox.Text),
      };
      return retValue;
    }

    // Saves the data.
    /// <include path='items/DataSave/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private bool DataSave()
    {
      RepeatItem lookupRecord;
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      // Lookup record on unique key.
      lookupRecord = LJCGenDataManager.RetrieveRepeatItem(LJCParentName, LJCRecord.Name);
      if (IsDuplicate(lookupRecord, LJCRecord))
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
          lookupRecord = LJCGenDataManager.RetrieveRepeatItem(LJCParentName, mOriginalName);
          if (lookupRecord != null)
          {
            lookupRecord.Name = LJCRecord.Name;
            LJCGenDataManager.Save();
          }
        }
        else
        {
          // Add new record.
          LJCGenDataManager.AddRepeatItem(LJCParentName, LJCRecord);
          LJCGenDataManager.Save();
        }
      }
      Cursor = Cursors.Default;
      return retValue;
    }
    #endregion

    #region Private Methods

    // Check for duplicate unique key.
    //private bool IsDuplicate(RepeatItem lookupRecord, RepeatItem currentRecord)
    private bool IsDuplicate(RepeatItem lookupRecord, RepeatItem _)
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
    /// <include path='items/IsValid/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private bool IsValid()
    {
      StringBuilder builder;
      string title;
      string message;
      bool retValue = true;

      builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!NetString.HasValue(NameTextbox.Text))
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
    /// <include path='items/InitializeControls/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    private void InitializeControls()
    {
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;

      // Initialize Class Data.
      NameLabel.BackColor = BeginColor;

      NameTextbox.MaxLength = 60;

      // Load control data.

      // Set control layout.
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

    /// <summary>Fires the Change event.</summary>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Properties

    // Gets or sets the GenData Manager reference.
    internal GenDataManager LJCGenDataManager { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets or sets the primary ID value.
    internal string LJCItemName
    {
      get { return mName; }
      set { mName = NetString.InitString(value); }
    }
    private string mName;

    // The form position.
    internal Point LJCLocation { get; set; }

    // Gets or sets the Parent ID value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets a reference to the record object.
    internal RepeatItem LJCRecord { get; private set; }

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private string mOriginalName;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
