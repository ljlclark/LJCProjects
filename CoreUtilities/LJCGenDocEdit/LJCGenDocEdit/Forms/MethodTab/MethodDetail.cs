﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MethodDetail.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The DocMethod detail dialog.</summary>
  public partial class MethodDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Detail.xml'/>
    public MethodDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCMethodID = 0;
      LJCClassID = 0;
      LJCRecord = null;
      LJCIsUpdate = false;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.FromArgb(225, 233, 240);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void MethodDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
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

      // Get Parent values.
      GroupText.Text = MethodGroupHeadingName(LJCGroupID);
      ClassText.Text = DocClassName(LJCClassID);

      Text = "Method Detail";
      if (LJCMethodID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = DocMethodWithID(LJCMethodID);
        // *** Begin *** Add - 7/9/23
        if (mOriginalRecord.ChangedOverload)
        {
          mOriginalRecord.ChangedNames.Add(DocMethod.ColumnOverloadName);
        }
        // *** End   *** Add - 7/9/23
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;

        // Set default values.
        LJCRecord = new DocMethod();
        SequenceText.Text = "1";
        if (Sequence > 0)
        {
          SequenceText.Text = Sequence.ToString();
        }
        ActiveCheckbox.Checked = true;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(DocMethod dataRecord)
    {
      if (dataRecord != null)
      {
        NameText.Text = dataRecord.Name;
        DescriptionText.Text = dataRecord.Description;
        OverloadText.Text = dataRecord.OverloadName;
        if (false == NetString.HasValue(dataRecord.OverloadName))
        {
          OverloadText.Text = dataRecord.Name;
        }
        SequenceText.Text = dataRecord.Sequence.ToString();
        ActiveCheckbox.Checked = dataRecord.ActiveFlag;
      }
    }

    // Creates and returns a record object with the data from
    private DocMethod SetRecordValues()
    {
      DocMethod retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DocMethod();
      }
      retValue.ID = LJCMethodID;
      retValue.Name = NameText.Text.Trim();
      retValue.Description = DescriptionText.Text.Trim();
      retValue.OverloadName = OverloadText.Text.Trim();
      short.TryParse(SequenceText.Text, out short value);
      retValue.Sequence = value;
      retValue.ActiveFlag = ActiveCheckbox.Checked;

      // Get Parent key values.
      retValue.DocMethodGroupID = LJCGroupID;
      retValue.DocClassID = LJCClassID;
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      var manager = Managers.DocMethodManager;
      // *** Next Statement *** Change - 7/9/23
      var lookupRecord = manager.RetrieveWithUnique(LJCRecord.DocClassID
        , LJCRecord.OverloadName);
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
            if (manager.Manager.AffectedCount < 1)
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
      if (false == NetString.HasValue(SequenceText.Text))
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
    #endregion

    #region Get Data Methods

    // Retrieves the ClassItem name.
    private string DocClassName(short docClassID)
    {
      string retValue = null;

      var docClass = DocClassWithID(docClassID);
      if (docClass != null)
      {
        retValue = docClass.Name;
      }
      return retValue;
    }

    // Retrieves the DocClass with the ID value.
    private DocClass DocClassWithID(short docClassID)
    {
      DocClass retValue = null;

      if (docClassID > 0)
      {
        var manager = Managers.DocClassManager;
        retValue = manager.RetrieveWithID(docClassID);
      }
      return retValue;
    }

    // Retrieves the DocMethod with the ID value.
    private DocMethod DocMethodWithID(short docMethodID)
    {
      DocMethod retValue = null;

      if (docMethodID > 0)
      {
        var manager = Managers.DocMethodManager;
        retValue = manager.RetrieveWithID(LJCMethodID);
      }
      return retValue;
    }

    // Retrieves the MethodGroup heading name.
    private string MethodGroupHeadingName(short methodGroupID)
    {
      string retValue = null;

      var methodGroup = MethodGroupWithID(methodGroupID);
      if (methodGroup != null)
      {
        retValue = methodGroup.HeadingName;
      }
      return retValue;
    }

    // Retrieves the Product with the ID value.
    private DocMethodGroup MethodGroupWithID(short methodGroupID)
    {
      DocMethodGroup retValue = null;

      if (methodGroupID > 0)
      {
        var manager = Managers.DocMethodGroupManager;
        retValue = manager.RetrieveWithID(LJCGroupID);
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
        LJCMethodID = 0;
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
        , @"Method\MethodItemDetail.html");
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
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace(NameText);
      SetNumericOnly(SequenceText);

      NameText.MaxLength = DocMethod.LengthName;
      DescriptionText.MaxLength = DocMethod.LengthDescription;
      GroupText.MaxLength = DocMethod.LengthDescription;
      ConfigureControls();
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }

    // Sets the NoSpace events.
    private void SetNumericOnly(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNumeric_KeyPress;
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Select the method.
    private void NameButton_Click(object sender, EventArgs e)
    {
      var list = new MethodSelect()
      {
        LJCClassID = LJCClassID
      };
      if (DialogResult.OK == list.ShowDialog())
      {
        var dataMethod = list.LJCSelectedRecord;
        NameText.Text = dataMethod.Name;
        // *** Next Statement *** Add- 7/923
        OverloadText.Text = dataMethod.OverloadName;
        var description = NetString.RemoveTags(dataMethod.Summary);
        DescriptionText.Text = NetString.Truncate(description
          , DocMethod.LengthDescription);
      }
    }

    // Ensures OverloadText has a value.
    private void OverloadText_Leave(object sender, EventArgs e)
    {
      if (false == NetString.HasValue(OverloadText.Text))
      {
        OverloadText.Text = NameText.Text;
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
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }

    // Only allows numbers or edit keys.
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Class ID value.</summary>
    public short LJCClassID { get; set; }

    /// <summary>Gets or sets the foreign ID value.</summary>
    public short LJCGroupID { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    internal bool LJCIsUpdate { get; private set; }

    /// <summary>Gets or sets the primary ID value.</summary>
    internal short LJCMethodID { get; set; }

    /// <summary>Gets or sets the Next flag.</summary>
    internal bool LJCNext { get; set; }

    /// <summary>Gets or sets the Previous flag.</summary>
    internal bool LJCPrevious { get; set; }

    /// <summary>Gets a reference to the record object.</summary>
    internal DocMethod LJCRecord { get; private set; }

    /// <summary>The Managers object.</summary>
    internal ManagersDocGen Managers { get; set; }

    /// <summary>Gets or sets the next sequence value.</summary>
    internal int Sequence { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;

    // Record with the original values.
    private DocMethod mOriginalRecord;

    // The standard configuration settings.
    private StandardUISettings mSettings;
    #endregion
  }
}