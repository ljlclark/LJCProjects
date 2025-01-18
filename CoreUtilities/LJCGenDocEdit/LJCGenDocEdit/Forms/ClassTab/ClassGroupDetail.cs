// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupDetail.cs
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
  // The DocAssembly detail dialog.
  internal partial class ClassGroupDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassGroupDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCAssemblyID = 0;
      LJCGroupID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.FromArgb(225, 235, 245);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ClassGroupDetail_Load(object sender, EventArgs e)
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

      // Get Parent values.
      AssemblyText.Text = DocAssemblyName(LJCAssemblyID);

      Text = "Class Group Detail";
      if (LJCGroupID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = ClassGroupWithID(LJCGroupID);
        GetRecordValues(mOriginalRecord);
        if ("Ungrouped" == mOriginalRecord.HeadingName)
        {
          SetUngrouped();
        }
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;

        // Set default values.
        LJCRecord = new DocClassGroup();
        SequenceText.Text = "1";
        if (LJCSequence > 0)
        {
          SequenceText.Text = LJCSequence.ToString();
        }
        ActiveCheckbox.Checked = true;
        NameText.Select();
        NameText.Select(0, 0);
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(DocClassGroup dataRecord)
    {
      if (dataRecord != null)
      {
        NameText.Text = dataRecord.HeadingName;
        CustomText.Text = dataRecord.HeadingTextCustom;
        SequenceText.Text = dataRecord.Sequence.ToString();
        ActiveCheckbox.Checked = dataRecord.ActiveFlag;

        // Get Parent values.
        AssemblyText.Text = DocAssemblyName(LJCAssemblyID);

        // Join values.
        HeadingText.Text = dataRecord.Heading;

        // Get foreign key values.
        mDocClassGroupHeadingID = dataRecord.DocClassGroupHeadingID;
        if (mDocClassGroupHeadingID > 0)
        {
          // Join values.
          //NameText.Text = dataRecord.GroupName;
          NameText.ReadOnly = true;
          HeadingText.Text = dataRecord.Heading;
          HeadingText.ReadOnly = true;

          CustomText.Select();
          CustomText.Select(0, 0);
        }
        else
        {
          NameText.Select();
          NameText.Select(0, 0);
        }
      }
    }

    // Creates and returns a record object with the data from
    private DocClassGroup SetRecordValues()
    {
      DocClassGroup retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DocClassGroup();
      }
      retValue.ID = LJCGroupID;
      retValue.HeadingName = NameText.Text.Trim();
      retValue.HeadingTextCustom = FormCommon.SetString(CustomText.Text);
      short.TryParse(SequenceText.Text, out short value);
      retValue.Sequence = value;
      retValue.ActiveFlag = ActiveCheckbox.Checked;

      // Get Reference key values.
      retValue.DocAssemblyID = LJCAssemblyID;
      retValue.DocClassGroupHeadingID = mDocClassGroupHeadingID;

      // Get control join display values.
      retValue.Heading = HeadingText.Text.Trim();
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(DocClassGroup dataRecord)
    {
      dataRecord.HeadingTextCustom
        = FormCommon.SetString(dataRecord.HeadingTextCustom);
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      string title;
      string message;
      LJCRecord = SetRecordValues();

      var manager = LJCManagers.DocClassGroupManager;
      var lookupRecord = manager.RetrieveWithUnique(LJCRecord.DocAssemblyID
        , LJCRecord.HeadingName);
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
          var isChanged = DbCommon.IsChanged(LJCRecord
            , out List<string> propertyNames);
          if (isChanged)
          {
            var keyRecord = manager.GetIDKey(LJCRecord.ID);
            manager.SourceSequence = mOriginalRecord.Sequence;
            manager.TargetSequence = LJCRecord.Sequence;
            manager.Update(LJCRecord, keyRecord, propertyNames);
            ResetRecordValues(LJCRecord);
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
          ResetRecordValues(LJCRecord);
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
      StringBuilder builder;
      string title;
      string message;
      bool retValue = true;

      builder = new StringBuilder(64);
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
        title = "Data Entry Error";
        message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }

    // Set the controls for Ungrouped classes.
    private void SetUngrouped()
    {
      NameText.ReadOnly = true;
      GroupButton.Enabled = false;
      HeadingText.ReadOnly = true;
      CustomText.ReadOnly = true;
      ActiveCheckbox.Enabled = false;
    }
    #endregion

    #region Get Data Methods

    // Retrieves the ClassGroup with the ID value.
    private DocClassGroup ClassGroupWithID(short groupID)
    {
      DocClassGroup retValue = null;

      if (groupID > 0)
      {
        var manager = LJCManagers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(groupID);
      }
      return retValue;
    }

    // Retrieves the DocAssembly name.
    private string DocAssemblyName(short assemblyID)
    {
      string retValue = null;

      var docAssembly = DocAssemblyWithID(assemblyID);
      if (docAssembly != null)
      {
        retValue = docAssembly.Name;
      }
      return retValue;
    }

    // Retrieves the Assembly with the ID value.
    private DocAssembly DocAssemblyWithID(short assemblyID)
    {
      DocAssembly retValue = null;

      if (assemblyID > 0)
      {
        var manager = LJCManagers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(assemblyID);
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
        LJCGroupID = 0;
        NameText.Text = "";

        DataRetrieve();
      }
    }

    // Save and move to the Previous record.
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

    // Save and move to the Next record.
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
        , @"Class\ClassGroupDetail.html");
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
        GroupButton.Top = NameText.Top;
        GroupButton.Height = NameText.Height;
        ActiveCheckbox.Top = SequenceText.Top + 2;
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
      // *** Begin *** Dummy Entries to eliminate notice.
      BeginColor = BeginColor;
      EndColor = EndColor;
      // *** End   ***
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
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Displays the ClassGroup selection dialog.
    private void GroupButton_Click(object sender, EventArgs e)
    {
      var list = new ClassHeadingSelect()
      {
        LJCIsSelect = true,
        LJCAssemblyID = LJCAssemblyID
      };
      list.LJCChange += List_LJCChange;
      if (DialogResult.OK == list.ShowDialog())
      {
        if (1 == list.ClassHeadingGrid.SelectedRows.Count)
        {
          SelectedChange(list);
        }
      }
    }

    // Event handler from selection list.
    private void List_LJCChange(object sender, EventArgs e)
    {
      // Save if more than one row is selected.
      if (sender is ClassHeadingSelect list
        && list.ClassHeadingGrid.SelectedRows.Count > 1)
      {
        SelectedChange(list);
        if (IsValid()
          && DataSave())
        {
          LJCOnChange();
          if (list.LastMultiSelect)
          {
            DialogResult = DialogResult.OK;
          }
        }
      }
    }

    // Set control values from selected item.
    private void SelectedChange(ClassHeadingSelect list)
    {
      var groupHeading = list.LJCSelectedRecord;
      if (groupHeading != null)
      {
        mDocClassGroupHeadingID = groupHeading.ID;
        NameText.Text = groupHeading.Name;
        NameText.ReadOnly = true;
        HeadingText.Text = groupHeading.Heading;
        HeadingText.ReadOnly = true;
      }
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

    #region Properties

    // Gets or sets the parent Assembly ID value.
    internal short LJCAssemblyID { get; set; }

    // Gets or sets the primary ID value.
    internal short LJCGroupID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // The Managers object.
    internal ManagersGenDoc LJCManagers { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    // Gets a reference to the record object.
    internal DocClassGroup LJCRecord { get; private set; }

    // Gets or sets the next sequence value.
    internal int LJCSequence { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    public event EventHandler<EventArgs> LJCChange;

    // Foreign Keys
    private short mDocClassGroupHeadingID;

    // Record with the original values.
    private DocClassGroup mOriginalRecord;

    // The standard configuration settings.
    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    #endregion
  }
}
