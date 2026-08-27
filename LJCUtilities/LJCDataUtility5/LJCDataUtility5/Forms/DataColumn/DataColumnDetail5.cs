// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnDetail5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCNetCommon5;
using System.Text;

namespace LJCDataUtility5
{
  // The DataColumn detail dialog.
  internal partial class DataColumnDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal DataColumnDetail()
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
      //TypeNameCombo.Leave += TypeNameCombo_Leave;
      TypeNameCombo.SelectedIndexChanged += TypeNameCombo_SelectedIndexChanged;
      IdentityStartText.TextChanged += IdentityStartText_TextChanged;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ColumnDetail_Load(object sender, EventArgs e)
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
    private static void ResetValues(DataUtilColumn dataRecord)
    {
      // In control order.
      dataRecord.Description = FormCommon.SetString(dataRecord.Description);
      dataRecord.DefaultValue = FormCommon.SetString(dataRecord.DefaultValue);
    }

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Column Detail";
      if (LJCDbId > 0
        && LJCId > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataColumnManager;
        if (manager != null)
        {
          mOriginalRecord = manager.RetrieveWithId(LJCDbId, LJCId);
          if (mOriginalRecord != null)
          {
            GetValues(mOriginalRecord);
          }
        }
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataUtilColumn();
        ParentNameText.Text = LJCTableName;
        SequenceText.Text = FormCommon.DefaultZero();
        if (LJCSequence > 0)
        {
          SequenceText.Text = LJCSequence.ToString();
        }
        MaxLengthText.Text = FormCommon.DefaultMinusOne();
        NewMaxLengthText.Text = FormCommon.DefaultMinusOne();
        IdentityStartText.Text = FormCommon.DefaultMinusOne();
        IdentityIncrementText.Text = FormCommon.DefaultMinusOne();
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetValues(DataUtilColumn data)
    {
      if (data != null)
      {
        // In control order.
        ParentNameText.Text = LJCTableName;
        NameText.Text = data.Name;
        NewNameText.Text = data.NewName;
        DescriptionText.Text = data.Description;
        SequenceText.Text = FormCommon.DefaultMinusOne((object)data.Sequence);
        int index = TypeNameCombo.FindString(data.TypeName);
        MaxLengthText.Text = FormCommon.DefaultMinusOne((object)data.MaxLength);
        TypeNameCombo.SelectedIndex = index;
        NewMaxLengthText.Text
          = FormCommon.DefaultMinusOne((object)data.NewMaxLength);
        DefaultText.Text = data.DefaultValue;
        IdentityStartText.Text
          = FormCommon.DefaultMinusOne((object)data.IdentityStart);
        IdentityIncrementText.Text
          = FormCommon.DefaultMinusOne((object)data.IdentityIncrement);
        AllowNullCheck.Checked = data.AllowNull;

        // Reference key values.
        LJCDbId = data.DbId;
        LJCTableDbId = data.DataTableDbId;
        LJCTableId = data.DataTableId;
      }
    }

    // Creates and returns a record object with the data from
    private DataUtilColumn SetValues()
    {
      var retData = Data();

      // In control order.
      retData.Name = NameText.Text;
      retData.NewName = FormCommon.SetString(NewNameText.Text);
      retData.Description = FormCommon.SetString(DescriptionText.Text);
      retData.Sequence = LJC.ToInt32(SequenceText.Text);
      retData.TypeName = TypeNameCombo.Text;
      retData.MaxLength = LJC.ToInt16(MaxLengthText.Text);
      retData.NewMaxLength = LJC.ToInt16(NewMaxLengthText.Text);
      retData.DefaultValue = FormCommon.SetString(DefaultText.Text);
      retData.IdentityStart = LJC.ToInt16(IdentityStartText.Text);
      retData.IdentityIncrement
        = LJC.ToInt16(IdentityIncrementText.Text);
      retData.AllowNull = AllowNullCheck.Checked;

      // Get Reference key values.
      retData.DbId = LJCDbId;
      retData.Id = LJCId;
      retData.DataTableDbId = LJCTableDbId;
      retData.DataTableId = LJCTableId;
      return retData;
    }

    // Gets the original or new record.
    private DataUtilColumn Data()
    {
      var retData = new DataUtilColumn();

      if (mOriginalRecord != null)
      {
        var dataUtilColumn = mOriginalRecord.Clone();
        if (dataUtilColumn != null)
        {
          retData = dataUtilColumn;
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
      var manager = LJCManagers.DataColumnManager;

      if (manager != null
        && retValue)
      {
        if (LJCIsUpdate)
        {
          var keyColumns = DataColumnManager.IdKey(LJCDbId, LJCId);
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
      SetNumeric();
      NameText.MaxLength = DataUtilColumn.LengthName;
      NewNameText.MaxLength = DataUtilColumn.LengthName;
      DescriptionText.MaxLength = DataUtilColumn.LengthDescription;
      SequenceText.MaxLength = DataUtilColumn.LengthSequence;
      MaxLengthText.MaxLength = DataUtilColumn.LengthMaxLength;
      NewMaxLengthText.MaxLength = DataUtilColumn.LengthMaxLength;
      DefaultText.MaxLength = DataUtilColumn.LengthDefaultValue;
      IdentityStartText.MaxLength = DataUtilColumn.LengthIdentityStart;
      IdentityIncrementText.MaxLength = DataUtilColumn.LengthIdentityIncrement;

      // Load control data.
      LoadTypeCombo();
      //TypeNameCombo.SelectedIndex = 0;

      Cursor = Cursors.Default;
    }

    // Loads the TypeName combo.
    private void LoadTypeCombo()
    {
      TypeNameCombo.LJCAddItem(15, 1, "nvarchar");
      TypeNameCombo.LJCAddItem(11, 1, "int");
      TypeNameCombo.LJCAddItem(18, 1, "smallint");
      TypeNameCombo.LJCAddItem(1, 1, "bigint");
      TypeNameCombo.LJCAddItem(2, 1, "binary");
      TypeNameCombo.LJCAddItem(3, 1, "bit");
      TypeNameCombo.LJCAddItem(4, 1, "char");
      TypeNameCombo.LJCAddItem(5, 1, "date");
      TypeNameCombo.LJCAddItem(6, 1, "datetime");
      TypeNameCombo.LJCAddItem(7, 1, "datetime2");
      TypeNameCombo.LJCAddItem(8, 1, "datetimeoffset");
      TypeNameCombo.LJCAddItem(9, 1, "decimal");
      TypeNameCombo.LJCAddItem(10, 1, "float");
      TypeNameCombo.LJCAddItem(12, 1, "money");
      TypeNameCombo.LJCAddItem(13, 1, "nchar");
      TypeNameCombo.LJCAddItem(14, 1, "ntext");
      TypeNameCombo.LJCAddItem(16, 1, "real");
      TypeNameCombo.LJCAddItem(17, 1, "smalldatetime");
      TypeNameCombo.LJCAddItem(19, 1, "smallmoney");
      TypeNameCombo.LJCAddItem(20, 1, "text");
      TypeNameCombo.LJCAddItem(21, 1, "time");
      TypeNameCombo.LJCAddItem(22, 1, "tinyint");
      TypeNameCombo.LJCAddItem(23, 1, "varbinary");
      TypeNameCombo.LJCAddItem(24, 1, "varchar");
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
    private void SetNumeric()
    {
      SequenceText.KeyPress += mSequence.KeyPress;
      SequenceText.TextChanged += mSequence.TextChanged;
      MaxLengthText.KeyPress += mMaxLength.KeyPress;
      MaxLengthText.TextChanged += mMaxLength.TextChanged;
      NewMaxLengthText.KeyPress += mNewMaxLength.KeyPress;
      NewMaxLengthText.TextChanged += mNewMaxLength.TextChanged;
      IdentityStartText.KeyPress += mIdentityStart.KeyPress;
      IdentityStartText.TextChanged += mIdentityStart.TextChanged;
      IdentityIncrementText.KeyPress += mIdentityIncrement.KeyPress;
      IdentityIncrementText.TextChanged += mIdentityIncrement.TextChanged;
    }
    #endregion

    #region Control Event Methods

    // Sets the Identity control default values.
    private void IdentityEnable()
    {
      AllowNullCheck.Enabled = false;
      if (false == IdentityStartText.Enabled
        || false == IdentityIncrementText.Enabled)
      {
        IdentityStartText.Text = "-1";
        IdentityIncrementText.Text = "-1";
        AllowNullCheck.Enabled = true;
      }
    }

    // Set the combo index by a text value.
    private void SetComboIndex(string text)
    {
      // Make sure SelectedIndexChanged fires.
      TypeNameCombo.SelectedIndex = -1;

      var index = TypeNameCombo.FindString(text);
      TypeNameCombo.SelectedIndex = index;
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    //// <include path='members/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the NameText Leave event.
    private void NameText_Leave(object? sender, EventArgs e)
    {
      // Set missing description the same as column name.
      var columnName = NameText.Text.Trim();
      if (!LJC.HasText(DescriptionText.Text))
      {
        DescriptionText.Text = columnName;
      }
      var isTypeSet = false;

      // Set TypeName = "bigint" and Identity values.
      if ("Id" == columnName)
      {
        isTypeSet = true;
        if (-1 == TypeNameCombo.SelectedIndex)
        {
          SetComboIndex("bigint");
        }
        IdentityStartText.Text = "1";
        IdentityIncrementText.Text = "1";
      }

      // Set TypeName = "bigint".
      if (columnName.Length > 2
        && columnName.EndsWith("Id"))
      {
        isTypeSet = true;
        if (-1 == TypeNameCombo.SelectedIndex)
        {
          SetComboIndex("bigint");
        }
        IdentityEnable();
      }

      if (!isTypeSet)
      {
        if (-1 == TypeNameCombo.SelectedIndex)
        {
          SetComboIndex("varchar");
        }
      }
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

    // Handles the TextChanged event.
    private void IdentityStartText_TextChanged(object? sender, EventArgs e)
    {
      var identityStart = IdentityStartText.Text.Trim();
      if (identityStart.StartsWith('-'))
      {
        IdentityEnable();
        AllowNullCheck.Checked = false;
        AllowNullCheck.Enabled = true;
      }
      else
      {
        IdentityEnable();
        IdentityIncrementText.Text = "1";
        AllowNullCheck.Checked = false;
        AllowNullCheck.Enabled = false;
      }
    }

    // Handles the SelectedIndexChanged event.
    private void TypeNameCombo_SelectedIndexChanged(object? sender, EventArgs e)
    {
      // Set MaxLength.
      var columnName = NameText.Text.Trim();
      var typeName = TypeNameCombo.Text.Trim();
      if ("char" == typeName
        || "nchar" == typeName
        || "nvarchar" == typeName
        || "varchar" == typeName)
      {
        IdentityEnable();
        var maxLength = MaxLengthText.Text.Trim();
        if ("-1" == maxLength
          || !LJC.HasText(maxLength))
        {
          switch (columnName)
          {
            case "Name":
              if (!LJC.HasText(MaxLengthText.Text))
              {
                MaxLengthText.Text = "60";
              }
              AllowNullCheck.Checked = false;
              break;

            case "Description":
              if (!LJC.HasText(MaxLengthText.Text))
              {
                MaxLengthText.Text = "80";
              }
              AllowNullCheck.Checked = true;
              break;

            default:
              if (!LJC.HasText(MaxLengthText.Text))
              {
                MaxLengthText.Text = "60";
              }
              break;
          }
        }
      }
      else
      {
        MaxLengthText.Text = "-1";
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

    // Gets or sets the Parent ID value.
    internal long LJCTableId { get; set; }

    // Gets or sets the ParentSite ID value.
    internal short LJCTableDbId { get; set; }

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
    private string? _TableName;

    // Gets a reference to the record object.
    internal DataUtilColumn? LJCRecord { get; private set; }

    // Gets or sets the Sequence value.
    internal int LJCSequence { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange = null!;

    private DataUtilColumn? mOriginalRecord;

    private readonly LJCTextNumber mSequence = new();
    private readonly LJCTextNumber mMaxLength = new();
    private readonly LJCTextNumber mNewMaxLength = new();
    private readonly LJCTextNumber mIdentityStart = new();
    private readonly LJCTextNumber mIdentityIncrement = new();
    #endregion
  }
}
