// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableDetail5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using System.Text;

namespace LJCDataUtility5
{
  // The DataTable detail dialog.
  internal partial class DataTableDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataTableDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCDbId = 0;
      LJCId = 0;
      LJCModuleDbId = 0;
      LJCModuleId = 0;
      _ModuleName = "";
      LJCIsUpdate = false;
      LJCRecord = null;

      NameText.Leave += NameText_Leave;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataTableDetail_Load(object? sender, EventArgs e)
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
    private static void ResetValues(DataUtilTable data)
    {
      // In control order.
      data.Description = FormCommon.SetString(data.Description);
      data.NewName = FormCommon.SetString(data.NewName);
    }

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Table Detail";
      if (LJCDbId > 0
        && LJCId > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataTableManager;
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
        LJCRecord = new DataUtilTable();
        ParentNameText.Text = LJCModuleName;
        SequenceText.Text = FormCommon.DefaultZero();
        if (LJCSequence > 0)
        {
          SequenceText.Text = LJCSequence.ToString();
        }
        SchemaText.Text = "dbo";
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataUtilTable data)
    {
      // In control order.
      ParentNameText.Text = LJCModuleName;
      NameText.Text = data.Name;
      DescriptionText.Text = data.Description;
      SequenceText.Text = data.Sequence.ToString();
      SchemaText.Text = data.SchemaName;
      if (!LJC.HasText(data.SchemaName))
      {
        SchemaText.Text = "dbo";
      }
      NewNameText.Text = data.NewName;

      // Reference key values.
      LJCDbId = data.DbId;
      LJCModuleDbId = data.DataModuleDbId;
      LJCModuleId = data.DataModuleId;
    }

    // Creates and returns a record object with the data from
    private DataUtilTable SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = NameText.Text;
      retData.Description = FormCommon.SetString(DescriptionText.Text);
      retData.Sequence = LJC.ToInt32(SequenceText.Text);
      retData.SchemaName = SchemaText.Text;
      retData.NewName = FormCommon.SetString(NewNameText.Text);

      // Get Reference key values.
      retData.DbId = LJCDbId;
      retData.Id = LJCId;
      retData.DataModuleDbId = LJCModuleDbId;
      retData.DataModuleId = LJCModuleId;
      return retData;
    }

    // Gets the original or new record.
    private DataUtilTable Data()
    {
      var retData = new DataUtilTable();

      if (_OriginalRecord != null)
      {
        var dataUtilTable = _OriginalRecord.Clone();
        if (dataUtilTable != null)
        {
          retData = dataUtilTable;
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
      var manager = LJCManagers.DataTableManager;
      if (manager != null)
      {
        var lookupRecord = manager.RetrieveUnique(LJCRecord.DataModuleDbId
          , LJCRecord.DataModuleId, LJCRecord.Name);
        if (lookupRecord != null
          && DataTableManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
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
          var keyColumns = DataTableManager.IdKey(LJCDbId, LJCId);
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
      SetNumericOnly();
      NameText.MaxLength = DataUtilTable.LengthName;
      DescriptionText.MaxLength = DataUtilTable.LengthDescription;
      SequenceText.MaxLength = DataUtilTable.LengthSequence;
      NewNameText.MaxLength = DataUtilTable.LengthName;

      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NameText.TextChanged += FormCommon.TextNoSpaceChanged;
      NewNameText.KeyPress += FormCommon.TextNoSpaceKeyPress;
      NewNameText.TextChanged += FormCommon.TextNoSpaceChanged;
    }

    // Sets the Numeric events.
    private void SetNumericOnly()
    {
      SequenceText.KeyPress += _Sequence.KeyPress;
      SequenceText.TextChanged += _Sequence.TextChanged;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the Leave event.
    private void NameText_Leave(object? sender, EventArgs e)
    {
      if (!LJC.HasText(DescriptionText.Text))
      {
        DescriptionText.Text = NameText.Text;
      }
    }

    // Saves the data and closes the form.
    private void OKButton_Click(object? sender, EventArgs e)
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

    // Gets or sets the database ID.
    internal short LJCDbId { get; set; }

    // Gets or sets the table row ID.
    internal long LJCId { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; } = null!;

    // Gets or sets the parent database ID.
    internal short LJCModuleDbId { get; set; }

    // Gets or sets the parent table row ID value.
    internal long LJCModuleId { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCModuleName
    {
      get => _ModuleName;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _ModuleName != newValue)
        {
          _ModuleName = newValue;
        }
      }
    }
    private string _ModuleName;

    // Gets a reference to the record object.
    internal DataUtilTable? LJCRecord { get; private set; }

    // Gets or sets the Sequence value.
    internal int LJCSequence { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange = null!;

    private DataUtilTable? _OriginalRecord;

    private readonly LJCTextNumber _Sequence = new();
    #endregion
  }
}
