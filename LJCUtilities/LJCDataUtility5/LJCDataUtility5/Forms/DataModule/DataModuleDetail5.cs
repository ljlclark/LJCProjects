// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleDetail.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCDataUtility5
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
      LJCDbId = 0;
      LJCId = 0;
      LJCIsUpdate = false;
      LJCRecord = null;
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
      CenterToParent();
      //Location = LJCLocation;
    }
    #endregion

    #region Data Methods

    // Resets the empty record values.
    private static void ResetValues(DataModule data)
    {
      // In control order.
      data.Description = FormCommon.SetString(data.Description);
    }

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Module Detail";
      if (LJCDbId > 0
        && LJCId > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataModuleManager;
        if (manager != null)
        {
          _OriginalRecord = manager.RetrieveWithId(LJCDbId, LJCId);
          if (_OriginalRecord != null)
          {
            GetValues(_OriginalRecord);
          }
        }
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
      // In control order.
      NameText.Text = data.Name;
      DescriptionText.Text = data.Description;

      // Reference key values.
      LJCDbId = data.DbId;
    }

    // Creates and returns a record object with the data from
    private DataModule SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = FormCommon.SetString(NameText.Text);
      retData.Description = FormCommon.SetString(DescriptionText.Text);

      // Get Reference key values.
      retData.DbId = LJCDbId;
      retData.Id = LJCId;
      return retData;
    }

    // Gets the original or new record.
    private DataModule Data()
    {
      var retData = new DataModule();

      if (_OriginalRecord != null)
      {
        var dataModule = _OriginalRecord.Clone();
        if (dataModule != null)
        {
          retData = dataModule;
        }
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
      if (manager != null)
      {
        var lookupRecord = manager.RetrieveUnique(LJCRecord.Name);
        if (lookupRecord != null
          && DataModuleManager.IsDuplicate(lookupRecord, LJCRecord
          , LJCIsUpdate))
        {
          retValue = false;
          FormCommon.DataError(this);
        }
      }

      if (manager != null
        && retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = DataModuleManager.IdKey(LJCDbId, LJCId);
          LJCRecord.Id = 0;
          manager.Update(LJCRecord, keyColumns);
          ResetValues(LJCRecord);
          LJCRecord.Id = LJCId;
          retValue = !FormCommon.UpdateError(this, manager.AffectedCount);
        }
        else
        {
          LJCRecord.DbId = LJCDbId;
          LJCRecord.Id = 0;
          var addedRecord = manager.Add(LJCRecord);
          ResetValues(LJCRecord);
          if (addedRecord != null)
          {
            LJCRecord.Id = addedRecord.Id;
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

      if (!LJC.HasText(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }

      if (!LJC.HasText(DescriptionText.Text))
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
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
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
    internal short LJCDbId { get; set; }

    // Gets or sets the primary ID value.
    internal long LJCId { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; } = null!;

    // Gets a reference to the record object.
    internal DataModule? LJCRecord { get; private set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange = null!;

    private DataModule? _OriginalRecord;
    #endregion
  }
}
