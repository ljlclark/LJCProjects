// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyDetail.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // The DataKey detail dialog.
  internal partial class DataKeyDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataKeyDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;

      _ = new ControlFont(this);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataKeyDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      Location = LJCLocation;
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "DataUtilityColumn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataKeyManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataKey();
        ParentNameText.Text = LJCParentName;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataKey dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        var data = dataRecord;
        ParentNameText.Text = LJCParentName;
        NameText.Text = data.Name;
        KeyTypeText.LJCSetByItemID(data.KeyType);
        SourceColumnText.Text = data.SourceColumnName;
        TargetTableText.Text = data.TargetTableName;
        TargetColumnText.Text = data.TargetColumnName;
        ClusteredCheck.Checked = data.IsClustered;
        AscendingCheck.Checked = data.IsAscending;

        // Reference key values.
        LJCParentID = dataRecord.DataTableID;
      }
    }

    // Creates and returns a record object with the data from
    private DataKey SetValues()
    {
      var retData = GetRecord();

      // In control order.
      retData.Name = FormCommon.SetString(NameText.Text);
      retData.KeyType = (short)KeyTypeText.LJCSelectedItemID();
      retData.SourceColumnName
        = FormCommon.SetString(SourceColumnText.Text);
      retData.TargetTableName
        = FormCommon.SetString(TargetTableText.Text);
      retData.TargetColumnName
        = FormCommon.SetString(TargetColumnText.Text);
      retData.IsClustered = ClusteredCheck.Checked;
      retData.IsAscending = AscendingCheck.Checked;

      // Get Reference key values.
      retData.ID = LJCID;
      retData.DataTableID = LJCParentID;
      return retData;
    }

    // Resets the empty record values.
    private void ResetValues(DataKey dataRecord)
    {
      // In control order.
      dataRecord.SourceColumnName
        = FormCommon.SetString(dataRecord.SourceColumnName);
      dataRecord.TargetTableName
        = FormCommon.SetString(dataRecord.TargetTableName);
      dataRecord.TargetColumnName
        = FormCommon.SetString(dataRecord.TargetColumnName);
    }

    // Gets the original or new record.
    private DataKey GetRecord()
    {
      DataKey retRecord = null;

      if (mOriginalRecord != null)
      {
        retRecord = mOriginalRecord.Clone();
      }
      if (null == retRecord)
      {
        retRecord = new DataKey();
      }
      return retRecord;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetValues();
      var manager = LJCManagers.DataKeyManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.IDKey(LJCID);
          LJCRecord.ID = 0;
          manager.Update(LJCRecord, keyColumns);
          ResetValues(LJCRecord);
          LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this, manager.AffectedCount);
        }
        else
        {
          LJCRecord.ID = 0;
          var addedRecord = manager.Add(LJCRecord);
          ResetValues(LJCRecord);
          if (addedRecord != null)
          {
            LJCRecord.ID = addedRecord.ID;
          }
          retValue = !FormCommon.AddError(this, manager.AffectedCount);
        }
      }
      Cursor = Cursors.Default;
      return retValue;
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
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesDataUtility.Instance;
      LJCManagers = values.Managers;
      //mSettings = values.StandardSettings;

      // Set control values.
      SetNoSpace();
      NameText.MaxLength = DataKey.LengthName;
      SourceColumnText.MaxLength = DataKey.LengthSourceColumnName;
      TargetTableText.MaxLength = DataKey.LengthTargetTableName;
      TargetColumnText.MaxLength = DataKey.LengthTargetColumnName;

      // Load control data.
      KeyTypeText.LJCAddItem(1, "Primary");
      KeyTypeText.LJCAddItem(2, "Unique");
      KeyTypeText.LJCAddItem(3, "Foreign");
      KeyTypeText.LJCAddItem(4, "Table");
      KeyTypeText.LJCSetByItemID(1);

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += TextBoxNoSpace_KeyPress;
      NameText.TextChanged += TextBoxNoSpace_TextChanged;
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
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
      }
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    //// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
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
    #endregion

    #region Properties

    // Gets or sets the primary ID value.
    internal int LJCID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }

    // Gets or sets the Parent ID value.
    internal int LJCParentID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets a reference to the record object.
    internal DataKey LJCRecord { get; private set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataKey mOriginalRecord;

    //private StandardUISettings mSettings;
    #endregion
  }
}
