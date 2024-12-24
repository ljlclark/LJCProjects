// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleDetail.cs
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
  // The DataModule detail dialog.
  internal partial class DataModuleDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataModuleDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCIsUpdate = false;
      LJCRecord = null;

      _ = new ControlFont(this);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataModuleDetail_Load(object sender, EventArgs e)
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
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Module Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataModuleManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataModule();
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataModule data)
    {
      if (data != null)
      {
        // In control order.
        NameText.Text = data.Name;
        DescriptionText.Text = data.Description;
      }
    }

    // Creates and returns a record object with the data from
    private DataModule SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = FormCommon.SetString(NameText.Text);
      retData.Description = FormCommon.SetString(DescriptionText.Text);

      // Get Reference key values.
      retData.ID = LJCID;
      return retData;
    }

    // Resets the empty record values.
    private void ResetValues(DataModule data)
    {
      // In control order.
      data.Description = FormCommon.SetString(data.Description);
    }

    // Gets the original or new record.
    private DataModule Data()
    {
      DataModule retData = null;

      if (mOriginalRecord != null)
      {
        retData = mOriginalRecord.Clone();
      }
      if (null == retData)
      {
        retData = new DataModule();
      }
      return retData;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetValues();
      var manager = LJCManagers.DataModuleManager;
      //var lookupRecord = manager.RetrieveWithUnique(LJCRecord.Name);
      //if (manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
      //{
      //  retValue = false;
      //  FormCommon.DataError(this);
      //}

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

      if (!NetString.HasValue(DescriptionText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {DescriptionLabel.Text}");
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

      // Set control values.
      SetNoSpace();
      NameText.MaxLength = DataModule.LengthName;
      DescriptionText.MaxLength = DataModule.LengthDescription;

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
        var saveStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = saveStart;
      }
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
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
        Close();
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

    // Gets a reference to the record object.
    internal DataModule LJCRecord { get; private set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataModule mOriginalRecord;
    #endregion
  }
}
