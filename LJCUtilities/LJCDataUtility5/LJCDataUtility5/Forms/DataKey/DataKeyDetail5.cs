// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyDetail5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCDataUtility5
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
      LJCDbId = 0;
      LJCId = 0;
      LJCTableDbId = 0;
      LJCTableId = 0;
      _TableName = "";
      LJCIsUpdate = false;
      LJCRecord = null;

      NameText.Leave += NameText_Leave;
      KeyTypeCombo.SelectedIndexChanged += KeyTypeCombo_SelectedIndexChanged;
      SourceColumnText.Leave += SourceColumnText_Leave;
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
      //CenterToParent();
      Location = LJCLocation;
    }
    #endregion

    #region Data Methods

    // Resets the empty record values.
    private static void ResetValues(DataKey dataRecord)
    {
      // In control order.
      dataRecord.SourceColumnName
        = FormCommon.SetString(dataRecord.SourceColumnName);
      dataRecord.TargetTableName
        = FormCommon.SetString(dataRecord.TargetTableName);
      dataRecord.TargetColumnName
        = FormCommon.SetString(dataRecord.TargetColumnName);
    }

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Key Detail";
      if (LJCDbId > 0
        && LJCId > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataKeyManager;
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
        LJCRecord = new DataKey();
        ParentNameText.Text = LJCTableName;
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataKey data)
    {
      if (data != null)
      {
        // In control order.
        ParentNameText.Text = LJCTableName;
        NameText.Text = data.Name;
        KeyTypeCombo.LJCSetByItemID(data.KeyType, 0);
        SourceColumnText.Text = data.SourceColumnName;
        TargetTableText.Text = data.TargetTableName;
        TargetColumnText.Text = data.TargetColumnName;
        ClusteredCheck.Checked = data.IsClustered;
        AscendingCheck.Checked = data.IsAscending;

        // Reference key values.
        LJCDbId = data.DbId;
        LJCTableId = data.DataTableId;
        LJCTableDbId = data.DataTableDbId;
      }
    }

    // Creates and returns a record object with the data from
    private DataKey SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = FormCommon.SetString(NameText.Text);
      var keyType = KeyTypeCombo.LJCSelectedItemID(out _);
      retData.KeyType = (short)keyType;
      retData.SourceColumnName
        = FormCommon.SetString(SourceColumnText.Text);
      retData.TargetTableName
        = FormCommon.SetString(TargetTableText.Text);
      retData.TargetColumnName
        = FormCommon.SetString(TargetColumnText.Text);
      retData.IsClustered = ClusteredCheck.Checked;
      retData.IsAscending = AscendingCheck.Checked;

      // Get Reference key values.
      retData.DbId = LJCDbId;
      retData.Id = LJCId;
      retData.DataTableDbId = LJCTableDbId;
      retData.DataTableId = LJCTableId;
      return retData;
    }

    // Gets the original or new record.
    private DataKey Data()
    {
      var retData = new DataKey();

      if (_OriginalRecord != null)
      {
        var dataKey = _OriginalRecord.Clone();
        if (dataKey != null)
        {
          retData = dataKey;
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
      var manager = LJCManagers.DataKeyManager;
      if (manager != null)
      {
        var lookupRecord = manager.RetrieveUnique(LJCRecord.DataTableDbId
          , LJCRecord.DataTableId, LJCRecord.Name);
        if (lookupRecord != null
          && manager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
          var keyColumns = DataKeyManager.IdKey(LJCDbId, LJCId);
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
      NameText.MaxLength = DataKey.LengthName;
      SourceColumnText.MaxLength = DataKey.LengthSourceColumnName;
      TargetTableText.MaxLength = DataKey.LengthTargetTableName;
      TargetColumnText.MaxLength = DataKey.LengthTargetColumnName;

      // Load control data.
      KeyTypeCombo.LJCAddItem(1, 1, "Primary");
      KeyTypeCombo.LJCAddItem(2, 1, "Unique");
      KeyTypeCombo.LJCAddItem(3, 1, "Foreign");
      KeyTypeCombo.LJCAddItem(4, 1, "Table");

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
      TargetTableText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      TargetTableText.TextChanged += FormCommon.TextNoSpaceChanged;
    }
    #endregion

    #region Control Event Methods

    // Sets the Clustered and Ascending check boxes.
    private void ClusteredChecked(bool enable)
    {
      ClusteredCheck.Checked = enable;
      AscendingCheck.Checked = enable;
      ClusteredCheck.Enabled = enable;
      AscendingCheck.Enabled = enable;
    }

    // Sets the default values base on the selected KeyType.
    private void SetKeyTypeValues()
    {
      var keyType = KeyTypeCombo.Text.Trim();
      switch (keyType)
      {
        case "Primary":
          SourceColumnText.Text = "ID";
          TargetTableText.Text = "";
          TargetColumnText.Text = "";
          ClusteredChecked(true);
          break;

        case "Foreign":
          SourceColumnText.Text = "ReferencingID";
          TargetTableText.Text = "Referencing";
          TargetColumnText.Text = "ID";
          ClusteredChecked(false);
          break;

        case "Unique":
          SourceColumnText.Text = "Name";
          TargetTableText.Text = "";
          TargetColumnText.Text = "";
          ClusteredChecked(false);
          break;
      }
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the KeyTypeCombo IndexChanged event.
    private void KeyTypeCombo_SelectedIndexChanged(object? sender, EventArgs e)
    {
      SetKeyTypeValues();
    }

    // Handles the NameText Leave event.
    private void NameText_Leave(object? sender, EventArgs e)
    {
      int index;
      var name = NameText.Text;
      if (name.ToLower().StartsWith("pk"))
      {
        index = KeyTypeCombo.FindString("Primary");
        KeyTypeCombo.SelectedIndex = index;
      }
      if (name.ToLower().StartsWith("fk"))
      {
        index = KeyTypeCombo.FindString("Foreign");
        KeyTypeCombo.SelectedIndex = index;
      }
      if (name.ToLower().StartsWith("uq")
        || name.ToLower().StartsWith("uk"))
      {
        index = KeyTypeCombo.FindString("Unique");
        KeyTypeCombo.SelectedIndex = index;
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

    // Handles the SourceColumnText Leave event.
    private void SourceColumnText_Leave(object? sender, EventArgs e)
    {
      var keyType = KeyTypeCombo.Text.Trim();
      switch (keyType)
      {
        case "Foreign":
          TargetTableText.Text = "";
          TargetColumnText.Text = "";

          var sourceColumnText = SourceColumnText.Text.Trim();
          if (!LJC.HasText(TargetTableText.Text)
            && sourceColumnText.ToLower() != "id"
            && sourceColumnText.EndsWith("ID"))
          {
            if (sourceColumnText.Contains(","))
            {
              var index = sourceColumnText.IndexOf(",");
              sourceColumnText = sourceColumnText.Substring(0, index);
            }
            var length = sourceColumnText.Length - 2;
            TargetTableText.Text = sourceColumnText.Substring(0, length);
            TargetColumnText.Text = "ID";
          }
          ClusteredChecked(false);
          break;
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

    // Gets or sets the ParentSite ID value.
    internal short LJCTableDbId { get; set; }

    // Gets or sets the Parent ID value.
    internal long LJCTableId { get; set; }

    // Gets or sets the LJCParentName value.
    internal string? LJCTableName
    {
      get => _TableName;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _TableName != newValue)
        {
          _TableName = newValue;
        }
      }
    }
    private string _TableName;

    // Gets a reference to the record object.
    internal DataKey? LJCRecord { get; private set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange = null!;

    private DataKey? _OriginalRecord;
    #endregion
  }
}
