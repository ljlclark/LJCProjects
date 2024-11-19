// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMapColumnDetail.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The DataColumn detail dialog.
  internal partial class DataMapColumnDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataMapColumnDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      //LJCColumnName = null;
      LJCRecord = null;
    }
    #endregion

    #region Form Event Handlers

    // ********************
    private void DataMapColumnDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    // ********************
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "DataMapColumn Detail";
      if (NetString.HasValue(LJCColumnName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataMapColumnManager;
        mOriginalRecord = manager.RetrieveWithIDs(LJCParentID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataMapColumn();
        ParentNameText.Text = LJCParentName;
      }
      RenameText.Select();
      RenameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    // ********************
    private void GetRecordValues(DataMapColumn dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        var data = dataRecord;
        ParentNameText.Text = LJCParentName;
        RenameText.Text = data.ColumnName;
        SequenceText.Text = data.Sequence.ToString();
        MaxLengthText.Text = data.MaxLength.ToString();

        // Reference key values.
        LJCParentID = dataRecord.DataColumnID;
      }
    }

    // Creates and returns a record object with the data from
    // ********************
    private DataMapColumn SetRecordValues()
    {
      DataMapColumn retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DataMapColumn();
      }

      // In control order.
      retValue.ColumnName = RenameText.Text;
      short.TryParse(SequenceText.Text, out short shortValue);
      retValue.Sequence = shortValue;
      short.TryParse(MaxLengthText.Text, out shortValue);
      retValue.MaxLength = shortValue;

      // Get Reference key values.
      retValue.DataColumnID = LJCParentID;
      return retValue;
    }

    // Resets the empty record values.
    // ********************
    private void ResetRecordValues(DataMapColumn dataRecord)
    {
      // In control order.
      dataRecord.ColumnName
        = FormCommon.SetString(dataRecord.ColumnName);
    }

    // Saves the data.
    // ********************
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = LJCManagers.DataMapColumnManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.IDKeys(LJCParentID);
          manager.Update(LJCRecord, keyColumns);
          ResetRecordValues(LJCRecord);
          retValue = !FormCommon.UpdateError(this
            , manager.AffectedCount);
        }
        else
        {
          manager.Add(LJCRecord);
          ResetRecordValues(LJCRecord);
          retValue = !FormCommon.AddError(this, manager.AffectedCount);
        }
      }
      Cursor = Cursors.Default;
      return retValue;
    }

    // Check for saved data.
    // ********************
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

    // Validates the data.
    // ********************
    private bool IsValid()
    {
      bool retValue = true;

      var builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      //if (!NetString.HasValue(NameText.Text))
      //{
      //  retValue = false;
      //  builder.AppendLine($"  {NameLabel.Text}");
      //}

      if (!retValue)
      {
        var title = "Data Entry Error";
        var message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    // ********************
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesDataUtility.Instance;
      LJCManagers = values.Managers;
      //mSettings = values.StandardSettings;

      // Set control values.
      SetNoSpace();
      SetNumeric();
      RenameText.MaxLength = DataMapColumn.LengthColumnName;

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    // ********************
    private void SetNoSpace()
    {
      RenameText.KeyPress += TextBoxNoSpace_KeyPress;
      RenameText.TextChanged += TextBoxNoSpace_TextChanged;
    }
    // Sets the Numeric events.
    // ********************
    private void SetNumeric()
    {
      SequenceText.KeyPress += TextBoxNumeric_KeyPress;
      SequenceText.KeyPress += TextBoxNoSpace_KeyPress;
      SequenceText.TextChanged += TextBoxNoSpace_TextChanged;
      MaxLengthText.KeyPress += TextBoxNumeric_KeyPress;
      MaxLengthText.KeyPress += TextBoxNoSpace_KeyPress;
      MaxLengthText.TextChanged += TextBoxNoSpace_TextChanged;
    }
    #endregion

    #region KeyEdit Event Handlers

    // Does not allow spaces.
    // ********************
    private void TextBoxNoSpace_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    // ********************
    private void TextBoxNoSpace_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox)
      {
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
      }
    }

    // Only allows numbers or edit keys.
    // ********************
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    //// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    // ********************
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Saves the data and closes the form.
    // ********************
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

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets or sets the Parent ID value.
    internal int LJCParentID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets or sets the LJCRenameValue value.
    internal string LJCColumnName
    {
      get { return mColumnName; }
      set { mColumnName = NetString.InitString(value); }
    }
    private string mColumnName;

    // Gets a reference to the record object.
    internal DataMapColumn LJCRecord { get; private set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataMapColumn mOriginalRecord;
    //private StandardUISettings mSettings;
    #endregion
  }
}
