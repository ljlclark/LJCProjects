﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassDetail.cs
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
  // The DocClass detail dialog.
  internal partial class ClassDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCAssemblyID = 0;
      LJCClassID = 0;
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
    private void ClassDetail_Load(object sender, EventArgs e)
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

      // Get Parent values.
      GroupText.Text = ClassGroupHeading(LJCGroupID);
      if (0 == LJCGroupID)
      {
        GroupText.Text = "Ungrouped Classes";
      }
      AssemblyText.Text = DocAssemblyName(LJCAssemblyID);

      Text = "Class Detail";
      if (LJCClassID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = DocClassWithID(LJCClassID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;

        // Set default values.
        LJCRecord = new DocClass();
        SequenceText.Text = "1";
        if (LJCSequence > 0)
        {
          SequenceText.Text = LJCSequence.ToString();
        }
        ActiveCheckbox.Checked = true;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(DocClass dataRecord)
    {
      if (dataRecord != null)
      {
        NameText.Text = dataRecord.Name;
        DescriptionText.Text = dataRecord.Description;
        SequenceText.Text = dataRecord.Sequence.ToString();
        ActiveCheckbox.Checked = dataRecord.ActiveFlag;
      }
    }

    // Creates and returns a record object with the data from
    private DocClass SetRecordValues()
    {
      DocClass retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DocClass();
      }
      retValue.ID = LJCClassID;
      retValue.Name = NameText.Text.Trim();
      retValue.Description = DescriptionText.Text.Trim();
      short.TryParse(SequenceText.Text, out short value);
      retValue.Sequence = value;
      retValue.ActiveFlag = ActiveCheckbox.Checked;

      // Get Reference key values.
      retValue.DocClassGroupID = LJCGroupID;
      retValue.DocAssemblyID = LJCAssemblyID;
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

      var manager = LJCManagers.DocClassManager;
      var lookupRecord = manager.RetrieveWithUnique(LJCRecord.DocAssemblyID
        , LJCRecord.Name);
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
      if (!NetString.HasValue(DescriptionText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {DescriptionLabel.Text}");
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

    // Retrieves the ClassGroup heading.
    private string ClassGroupHeading(short classGroupID)
    {
      string retValue = null;

      var classGroup = ClassGroupWithID(classGroupID);
      if (classGroup != null)
      {
        retValue = classGroup.Heading;
        // *** Begin *** Add 5/5/25
        if (!NetString.HasValue(retValue))
        {
          retValue = classGroup.HeadingTextCustom;
        }
        // *** End   *** Add
      }
      return retValue;
    }

    // Retrieves the ClassGroup with the ID value.
    private DocClassGroup ClassGroupWithID(short classGroupID)
    {
      DocClassGroup retValue = null;

      if (classGroupID > 0)
      {
        var manager = LJCManagers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(classGroupID);
      }
      return retValue;
    }

    // Retrieves the AssemblyItem name.
    private string DocAssemblyName(short docAssemblyID)
    {
      string retValue = null;

      var docAssembly = DocAssemblyWithID(docAssemblyID);
      if (docAssembly != null)
      {
        retValue = docAssembly.Name;
      }
      return retValue;
    }

    // Retrieves the AssemblyItem with the ID value.
    private DocAssembly DocAssemblyWithID(short docAssemblyID)
    {
      DocAssembly retValue = null;

      if (docAssemblyID > 0)
      {
        var manager = LJCManagers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(docAssemblyID);
      }
      return retValue;
    }

    // Retrieves the ClassItem with the ID value.
    private DocClass DocClassWithID(short docClassID)
    {
      DocClass retValue = null;

      if (docClassID > 0)
      {
        var manager = LJCManagers.DocClassManager;
        retValue = manager.RetrieveWithID(docClassID);
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
        LJCClassID = 0;
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
        , @"Class\ClassItemDetail.html");
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
        NameButton.Top = NameText.Top;
        NameButton.Height = NameText.Height;
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

    // Displays the Class selection dialog.
    private void NameButton_Click(object sender, EventArgs e)
    {
      var list = new ClassSelect()
      {
        LJCAssemblyID = LJCAssemblyID,
        LJCGroupID = LJCGroupID
      };
      list.LJCChange += ListLJCChange;
      if (DialogResult.OK == list.ShowDialog())
      {
        if (1 == list.ClassGrid.SelectedRows.Count)
        {
          SelectedChange(list);
        }
      }
    }

    // Event handler from selection list.
    private void ListLJCChange(object sender, EventArgs e)
    {
      // Save if more than one row is selected.
      if (sender is ClassSelect list
        && list.ClassGrid.SelectedRows.Count > 1)
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
    private void SelectedChange(ClassSelect list)
    {
      var dataType = list.LJCSelectedRecord;
      if (dataType != null)
      {
        NameText.Text = dataType.Name;
        var description = NetString.RemoveTags(dataType.Summary);
        if (NetString.HasValue(description))
        {
          DescriptionText.Text = NetString.Truncate(description
            , DocClass.LengthDescription);
        }
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

    // Gets or sets the foreign ID value.
    internal short LJCAssemblyID { get; set; }

    // Gets or sets the parent Group ID value.
    public short LJCGroupID { get; set; }

    // Gets or sets the primary ID value.
    internal short LJCClassID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The Managers object.
    internal ManagersGenDoc LJCManagers { get; set; }

    // Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    // Gets a reference to the record object.
    internal DocClass LJCRecord { get; private set; }

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

    // Record with the original values.
    private DocClass mOriginalRecord;

    // The standard configuration settings.
    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    #endregion
  }
}
