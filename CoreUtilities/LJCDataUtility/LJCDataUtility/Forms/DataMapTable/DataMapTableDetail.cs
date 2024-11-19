// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMapTableDetail.cs
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
  internal partial class DataMapTableDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataMapTableDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCIsUpdate = false;
      LJCNewTableName = null;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;
    }
    #endregion

    #region Form Event Handlers

    // ********************
    private void DataMapTableDetail_Load(object sender, EventArgs e)
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
      Text = "DataUtilityColumn Detail";
      if (NetString.HasValue(LJCNewTableName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataMapTableManager;
        mOriginalRecord = manager.RetrieveWithParentID(LJCParentID);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataMapTable();
        ParentNameText.Text = LJCParentName;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    // ********************
    private void GetRecordValues(DataMapTable dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        var data = dataRecord;
        ParentNameText.Text = LJCParentName;
        NameText.Text = data.NewTableName;

        // Reference key values.
        //LJCParentID = data.DataTableID;
      }
    }

    // Creates and returns a record object with the data from
    // ********************
    private DataMapTable SetRecordValues()
    {
      DataMapTable retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new DataMapTable();
      }

      // In control order.
      retValue.NewTableName = NameText.Text;

      // Get Reference key values.
      retValue.DataTableID = LJCParentID;
      return retValue;
    }

    //// Resets the empty record values.
    // ********************
    //private void ResetRecordValues(DataMapTable dataRecord)
    //{
    //  // In control order.
    //  //dataRecord.NewTableName
    //  //  = FormCommon.SetString(dataRecord.NewTableName);
    //}

    // Saves the data.
    // ********************
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = LJCManagers.DataMapTableManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = manager.ParentKey(LJCParentID);
          manager.Update(LJCRecord, keyColumns);
          //ResetRecordValues(LJCRecord);
          LJCRecord.DataTableID = LJCParentID;
          retValue = !FormCommon.UpdateError(this
            , manager.AffectedCount);
        }
        else
        {
          manager.Add(LJCRecord);
          //ResetRecordValues(LJCRecord);
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
      NameText.MaxLength = DataMapTable.LengthNewTableName;

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    // ********************
    private void SetNoSpace()
    {
      NameText.KeyPress += TextBoxNoSpace_KeyPress;
      NameText.TextChanged += TextBoxNoSpace_TextChanged;
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

    // Gets a reference to the record object.
    internal DataMapTable LJCRecord { get; private set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCNewTableName
    {
      get { return mNewTableName; }
      set { mNewTableName = NetString.InitString(value); }
    }
    private string mNewTableName;
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataMapTable mOriginalRecord;
    //private StandardUISettings mSettings;
    #endregion
  }
}
