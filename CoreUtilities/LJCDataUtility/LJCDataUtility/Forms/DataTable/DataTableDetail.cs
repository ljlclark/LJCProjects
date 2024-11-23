// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableDetail.cs
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
  #region Constructors

  // The DataTable detail dialog.
  internal partial class DataTableDetail : Form
  {
    // Initializes an object instance.
    // ********************
    internal DataTableDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;
    }
    #endregion

    #region Form Event Handlers

    // ********************

    private void DataTableDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      //CenterToParent();
      Location = LJCLocation;
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    // ********************
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "DataUtilityColumn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataTableManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataUtilTable();
        ParentNameText.Text = LJCParentName;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    // ********************
    private void GetValues(DataUtilTable dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        var data = dataRecord;
        ParentNameText.Text = LJCParentName;
        NameText.Text = data.Name;
        DescriptionText.Text = data.Description;
        NewNameText.Text = data.NewName;

        // Reference key values.
        LJCParentID = dataRecord.DataModuleID;
      }
    }

    // Creates and returns a record object with the data from
    // ********************
    private DataUtilTable SetValues()
    {
      var retData = GetRecord();

      // In control order.
      retData.Name = NameText.Text;
      retData.Description
        = FormCommon.SetString(DescriptionText.Text);
      retData.NewName
        = FormCommon.SetString(NewNameText.Text);

      // Get Reference key values.
      retData.ID = LJCID;
      retData.DataModuleID = LJCParentID;
      return retData;
    }

    // Resets the empty record values.
    // ********************
    private void ResetValues(DataUtilTable dataRecord)
    {
      // In control order.
      dataRecord.Description
        = FormCommon.SetString(dataRecord.Description);
      dataRecord.NewName
        = FormCommon.SetString(dataRecord.NewName);
    }

    // Gets the original or new record.
    // ********************
    private DataUtilTable GetRecord()
    {
      DataUtilTable retRecord = null;

      if (mOriginalRecord != null)
      {
        retRecord = mOriginalRecord.Clone();
      }
      if (null == retRecord)
      {
        retRecord = new DataUtilTable();
      }
      return retRecord;
    }

    // Saves the data.
    // ********************
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetValues();
      var manager = LJCManagers.DataTableManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.IDKey(LJCID);
          LJCRecord.ID = 0;
          manager.Update(LJCRecord, keyColumns);
          ResetValues(LJCRecord);
          LJCRecord.ID = LJCID;
          retValue = !FormCommon.UpdateError(this
            , manager.AffectedCount);
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
          retValue = !FormCommon.AddError(this
            , manager.AffectedCount);
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
      DescriptionText.MaxLength = DataUtilityColumn.LengthDescription;

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    // ********************
    private void SetNoSpace()
    {
      NameText.KeyPress += TextBoxNoSpace_KeyPress;
      NameText.TextChanged += TextBoxNoSpace_TextChanged;
      NewNameText.TextChanged += TextBoxNoSpace_TextChanged;
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

    // Gets or sets the primary ID value.
    internal int LJCID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

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
    internal DataUtilTable LJCRecord { get; private set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataUtilTable mOriginalRecord;
    //private StandardUISettings mSettings;
    #endregion
  }
}
