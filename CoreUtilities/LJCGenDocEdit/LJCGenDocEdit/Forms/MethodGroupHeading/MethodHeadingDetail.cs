// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingDetail.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The DocMethodGroupHeading detail dialog.
  internal partial class MethodHeadingDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal MethodHeadingDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCHeadingID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.FromArgb(225, 235, 245);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void MethodHeadingDetail_Load(object sender, EventArgs e)
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
      //*** Next Statement*** Delete 1 / 17 / 25
      //FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
      //  , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Method Group Heading Detail";
      if (LJCHeadingID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = GetHeadingWithID(LJCHeadingID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;

        // Set default values.
        LJCRecord = new DocMethodGroupHeading();
        SequenceText.Text = "1";
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(DocMethodGroupHeading dataRecord)
    {
      if (dataRecord != null)
      {
        NameText.Text = dataRecord.Name;
        HeadingText.Text = dataRecord.Heading;
        SequenceText.Text = dataRecord.Sequence.ToString();
      }
    }

    // Creates and returns a record object with the data from
    private DocMethodGroupHeading SetRecordValues()
    {
      DocMethodGroupHeading retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DocMethodGroupHeading();
      }
      retValue.ID = LJCHeadingID;
      retValue.Name = NameText.Text.Trim();
      retValue.Heading = HeadingText.Text.Trim();
      short.TryParse(SequenceText.Text, out short value);
      retValue.Sequence = value;
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      string title;
      string message;
      LJCRecord = SetRecordValues();

      var manager = LJCManagers.DocMethodGroupHeadingManager;
      //var lookupRecord = manager.RetrieveWithUnique(LJCRecord.Name);
      //if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
      //{
      //  retValue = false;
      //  title = "Data Entry Error";
      //  message = "The record already exists.";
      //  Cursor = Cursors.Default;
      //  MessageBox.Show(message, title, MessageBoxButtons.OK
      //    , MessageBoxIcon.Exclamation);
      //}

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var isChanged = DbCommon.IsChanged(LJCRecord
            , out List<string> propertyNames);
          if (isChanged)
          {
            var keyRecord = manager.GetIDKey(LJCRecord.ID);
            manager.SourceSequence = mOriginalRecord.Sequence;
            manager.TargetSequence = LJCRecord.Sequence;
            manager.Update(LJCRecord, keyRecord, propertyNames);
            if (0 == manager.Manager.AffectedCount)
            {
              title = "Update Error";
              message = "The Record was not updated.";
              Cursor = Cursors.Default;
              MessageBox.Show(message, title, MessageBoxButtons.OK
                , MessageBoxIcon.Information);
            }
          }
        }
        else
        {
          manager.TargetSequence = LJCRecord.Sequence;
          var addedRecord = manager.Add(LJCRecord);
          if (null == addedRecord)
          {
            if (manager.Manager.AffectedCount < 1)
            {
              title = "Add Error";
              message = "The Record was not added.";
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
      if (!NetString.HasValue(SequenceText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {SequenceLabel.Text}");
      }

      if (retValue == false)
      {
        var title = "Data Entry Error";
        var message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Get Data Methods

    // Retrieves the item with the ID value.
    private DocMethodGroupHeading GetHeadingWithID(short id)
    {
      DocMethodGroupHeading retValue = null;

      if (id > 0)
      {
        var manager = LJCManagers.DocMethodGroupHeadingManager;
        retValue = manager.RetrieveWithID(id);
      }
      return retValue;
    }
    #endregion

    #region Action Event Handlers

    // Save and setup for a new record.
    private void DialogNew_Click(object sender, EventArgs e)
    {
      if (IsDataSaved())
      {
        LJCPrevious = false;
        LJCNext = false;
        LJCOnChange();

        // Initialize property values.
        LJCHeadingID = 0;
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
    private void DialogHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, "GenDocEdit.chm", HelpNavigator.Topic
        , @"Method\MethodHeadingDetail.html");
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
      var values = ValuesGenDocEdit.Instance;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Set control values.
      // *** Next Statement *** Delete 1/17/25
      //FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();
      SetNumeric();

      ConfigureControls();
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
    }

    // Sets the Numeric events.
    private void SetNumeric()
    {
      SequenceText.KeyPress += mSequence.KeyPress;
      SequenceText.TextChanged += mSequence.TextChanged;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCGenDoc/Common/Detail.xml'/>
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

    #region Properties

    // Gets or sets the primary ID value.
    internal short LJCHeadingID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The Managers object.
    internal ManagersGenDoc LJCManagers { get; set; }

    // Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    // Gets a reference to the record object.
    internal DocMethodGroupHeading LJCRecord { get; private set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    public event EventHandler<EventArgs> LJCChange;

    // Record with the original values.
    private DocMethodGroupHeading mOriginalRecord;

    // The standard configuration settings.
    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    #endregion
  }
}
