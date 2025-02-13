﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyDetail.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The DocAssembly detail dialog.</summary>
  internal partial class AssemblyDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    internal AssemblyDetail()
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
    private void AssemblyDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCGenDoc/Common/Detail.xml'/>
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
      GroupText.Text = GetGroupName(LJCGroupID);

      Text = "Assembly Detail";
      if (LJCAssemblyID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = GetAssemblyWithID(LJCAssemblyID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;

        // Set default values.
        LJCRecord = new DocAssembly();
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
    private void GetRecordValues(DocAssembly dataRecord)
    {
      if (dataRecord != null)
      {
        NameText.Text = dataRecord.Name;
        DescriptionText.Text = dataRecord.Description;
        FileText.Text = dataRecord.FileSpec;
        ImageText.Text = dataRecord.MainImage;
        SequenceText.Text = dataRecord.Sequence.ToString();
        ActiveCheckbox.Checked = dataRecord.ActiveFlag;
      }
    }

    // Creates and returns a record object with the data from
    private DocAssembly SetRecordValues()
    {
      DocAssembly retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DocAssembly();
      }
      retValue.ID = LJCAssemblyID;
      retValue.Name = NameText.Text;
      retValue.Description = DescriptionText.Text;
      retValue.FileSpec = FileText.Text;
      retValue.MainImage = FormCommon.SetString(ImageText.Text);
      retValue.Sequence = Convert.ToInt16(SequenceText.Text);
      retValue.ActiveFlag = ActiveCheckbox.Checked;

      // Get Parent key values.
      retValue.DocAssemblyGroupID = LJCGroupID;
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(DocAssembly dataRecord)
    {
      dataRecord.MainImage = FormCommon.SetString(dataRecord.MainImage);
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      string title;
      string message;
      LJCRecord = SetRecordValues();

      var manager = LJCManagers.DocAssemblyManager;
      var lookupRecord = manager.RetrieveWithUnique(LJCRecord.DocAssemblyGroupID
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
            ResetRecordValues(LJCRecord);
            if (0 == manager.Manager.AffectedCount)
            {
              title = "Update Error";
              message = "The Record was not updated.";
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
      if (!NetString.HasValue(FileText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {FileLabel.Text}");
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

    // Retrieves the AssemblyItem name.
    private string GetGroupName(short groupID)
    {
      string retValue = null;

      var assemblyGroup = GetGroupWithID(groupID);
      if (assemblyGroup != null)
      {
        retValue = assemblyGroup.Name;
      }
      return retValue;
    }

    // Retrieves the AssemblyItem with the ID value.
    private DocAssembly GetAssemblyWithID(short assemblyID)
    {
      DocAssembly retValue = null;

      if (assemblyID > 0)
      {
        var manager = LJCManagers.DocAssemblyManager;
        retValue = manager.RetrieveWithID(LJCAssemblyID);
      }
      return retValue;
    }

    // Retrieves the AssemblyGroup with the ID value.
    private DocAssemblyGroup GetGroupWithID(short assemblyGroupID)
    {
      DocAssemblyGroup retValue = null;

      if (assemblyGroupID > 0)
      {
        var manager = LJCManagers.DocAssemblyGroupManager;
        retValue = manager.RetrieveWithID(assemblyGroupID);
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
        LJCAssemblyID = 0;
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
        , @"Assembly\AssemblyItemDetail.html");
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
        FileButton.Top = FileText.Top;
        FileButton.Height = FileText.Height;
        ImageButton.Top = ImageText.Top;
        ImageButton.Height = ImageText.Height;
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

      //HeadingText.MaxLength = DocAssemblyGroup.LengthHeading;
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
      if (IsDataSaved())
      {
        LJCOnChange();
        DialogResult = DialogResult.OK;
      }
    }

    // Select the documentation XML file.
    private void FileButton_Click(object sender, EventArgs e)
    {
      // No Spaces in filter file spec.
      var filter = "XML files(*.xml)|*.xml";
      filter += "|All files(*.*)|*.*";
      var initialDirectory = Directory.GetCurrentDirectory();
      var fileSpec = FormCommon.SelectFile(filter, initialDirectory);
      if (fileSpec != null)
      {
        // ToDo: Fix relative path.
        var fromPath = initialDirectory;
        var toPath = Path.GetDirectoryName(fileSpec);
        var filePath = NetFile.GetRelativePath(fromPath, toPath);
        var fileName = Path.GetFileName(fileSpec);
        FileText.Text = Path.Combine(filePath, fileName);
      }
    }

    // Select the image file spec.
    private void ImageButton_Click(object sender, EventArgs e)
    {
      // No Spaces in filter file spec.
      var filter = "JPG files(*.jpg)|*.jpg";
      filter += "|All files(*.*)|*.*";
      var initialDirectory = Directory.GetCurrentDirectory();
      var fileSpec = FormCommon.SelectFile(filter, initialDirectory);
      if (fileSpec != null)
      {
        // ToDo: Fix relative path.
        var fromPath = initialDirectory;
        var toPath = Path.GetPathRoot(fileSpec);
        var filePath = NetFile.GetRelativePath(fromPath, toPath);
        var fileName = Path.GetFileName(fileSpec);
        ImageText.Text = Path.Combine(filePath, fileName);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Parent Group ID value.</summary>
    public short LJCGroupID { get; set; }

    // Gets or sets the primary ID value.
    internal short LJCAssemblyID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The Managers object.
    internal ManagersGenDoc LJCManagers { get; set; }

    // Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    // Gets a reference to the record object.
    internal DocAssembly LJCRecord { get; private set; }

    // Gets or sets the next sequence value.
    internal int LJCSequence { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;

    // Record with the original values.
    private DocAssembly mOriginalRecord;

    // The standard configuration settings.
    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    #endregion
  }
}
