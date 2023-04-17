// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupDetail.cs
using LJCDBClientLib;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The DocAssembly detail dialog.</summary>
  public partial class ClassGroupDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Detail.xml'/>
    public ClassGroupDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;
      LJCIsUpdate = false;

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
      Text = "Class Group Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        mOriginalRecord = GetClassGroupWithID(LJCID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        ParentText.Text = LJCParentName;

        // Set default values.
        LJCRecord = new DocClassGroup();
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
        LJCParentID = dataRecord.DocAssemblyID;
        ParentText.Text = LJCParentName;
        NameText.Text = dataRecord.HeadingName;
        CustomText.Text = dataRecord.HeadingTextCustom;
        SequenceText.Text = dataRecord.Sequence.ToString();
        ActiveCheckbox.Checked = dataRecord.ActiveFlag;

        // Join values.
        HeadingText.Text = dataRecord.Heading;

        // Get foreign key values.
        mDocClassGroupHeadingID = dataRecord.DocClassGroupHeadingID;
        if (0 == mDocClassGroupHeadingID)
        {
          NameText.ReadOnly = false;
          HeadingText.ReadOnly = false;
          NameText.Select();
          NameText.Select(0, 0);
        }
        else
        {
          CustomText.Select();
          CustomText.Select(0, 0);
        }
      }
    }

    // Creates and returns a record object with the data from
    private DocClassGroup SetRecordValues()
    {
      var retValue = mOriginalRecord.Clone();
      if (null == retValue)
      {
        retValue = new DocClassGroup();
      }
      retValue.ID = LJCID;
      retValue.DocAssemblyID = LJCParentID;
      retValue.DocClassGroupHeadingID = mDocClassGroupHeadingID;
      retValue.HeadingName = NameText.Text.Trim();
      retValue.HeadingTextCustom = FormCommon.SetString(CustomText.Text);
      retValue.Sequence = Convert.ToInt16(SequenceText.Text);
      retValue.ActiveFlag = ActiveCheckbox.Checked;
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
      string title;
      string message;
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();

      var manager = Managers.DocClassGroupManager;
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
          var keyRecord = manager.GetIDKey(LJCRecord.ID);
          manager.SourceSequence = mOriginalRecord.Sequence;
          manager.TargetSequence = LJCRecord.Sequence;
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

    // Retrieves the Product with the ID value.
    private DocClassGroup GetClassGroupWithID(short id)
    {
      DocClassGroup retValue = null;

      if (id > 0)
      {
        var manager = Managers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(LJCID);
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
        LJCID = 0;
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

      //HeadingText.MaxLength = DocAssemblyGroup.LengthHeading;
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

    // Displays the ClassGroup selection dialog.
    private void GroupButton_Click(object sender, EventArgs e)
    {
      var list = new ClassHeadingSelect()
      {
        LJCIsSelect = true
      };
      if (DialogResult.OK == list.ShowDialog())
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

    /// <summary>Gets or sets the primary ID value.</summary>
    internal short LJCID { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    internal bool LJCIsUpdate { get; private set; }

    // Gets or sets the Next flag.
    internal bool LJCNext { get; set; }

    // Gets or sets the Previous flag.
    internal bool LJCPrevious { get; set; }

    /// <summary>Gets or sets the Parent ID value.</summary>
    public short LJCParentID { get; set; }

    /// <summary>Gets or sets the LJCParentName value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets a reference to the record object.</summary>
    internal DocClassGroup LJCRecord { get; private set; }

    // The Managers object.
    internal ManagersDocGen Managers { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;

    // Foreign Keys
    private short mDocClassGroupHeadingID;

    // 
    private DocClassGroup mOriginalRecord;

    // 
    private StandardUISettings mSettings;
    #endregion
  }
}
