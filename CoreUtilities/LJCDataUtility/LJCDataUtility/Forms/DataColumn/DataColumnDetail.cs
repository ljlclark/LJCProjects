﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnDetail.cs
using LJCDataUtility;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LJCDataUtility
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
      LJCID = 0;
      LJCIsUpdate = false;
      LJCParentID = 0;
      LJCParentName = null;
      LJCRecord = null;

      _ = new ControlFont(this);

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

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "DataUtilityColumn Detail";
      if (LJCID > 0)
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = LJCManagers.DataColumnManager;
        mOriginalRecord = manager.RetrieveWithID(LJCID);
        GetValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new DataUtilColumn();
        ParentNameText.Text = LJCParentName;
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
        ParentNameText.Text = LJCParentName;
        NameText.Text = data.Name;
        NewNameText.Text = data.NewName;
        DescriptionText.Text = data.Description;
        SequenceText.Text
          = FormCommon.DefaultMinusOne((object)data.Sequence);
        int index = TypeNameCombo.FindString(data.TypeName);
        MaxLengthText.Text
          = FormCommon.DefaultMinusOne((object)data.MaxLength);
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
        LJCSiteID = data.DataSiteID;
        LJCParentID = data.DataTableID;
        LJCParentSiteID = data.DataTableSiteID;
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
      retData.Sequence = NetCommon.ToInt32(SequenceText.Text);
      retData.TypeName = TypeNameCombo.Text;
      retData.MaxLength = NetCommon.ToInt16(MaxLengthText.Text);
      retData.NewMaxLength = NetCommon.ToInt16(NewMaxLengthText.Text);
      retData.DefaultValue = FormCommon.SetString(DefaultText.Text);
      retData.IdentityStart = NetCommon.ToInt16(IdentityStartText.Text);
      retData.IdentityIncrement
        = NetCommon.ToInt16(IdentityIncrementText.Text);
      retData.AllowNull = AllowNullCheck.Checked;

      // Get Reference key values.
      retData.ID = LJCID;
      retData.DataSiteID = LJCSiteID;
      retData.DataTableID = LJCParentID;
      retData.DataTableSiteID = LJCParentSiteID;
      return retData;
    }

    // Resets the empty record values.
    private void ResetValues(DataUtilColumn dataRecord)
    {
      // In control order.
      dataRecord.Description
        = FormCommon.SetString(dataRecord.Description);
      dataRecord.DefaultValue
        = FormCommon.SetString(dataRecord.DefaultValue);
    }

    // Gets the original or new record.
    private DataUtilColumn Data()
    {
      DataUtilColumn retRecord = null;

      if (mOriginalRecord != null)
      {
        retRecord = mOriginalRecord.Clone();
      }
      if (null == retRecord)
      {
        retRecord = new DataUtilColumn();
      }
      return retRecord;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetValues();
      var manager = LJCManagers.DataColumnManager;

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
          LJCRecord.DataSiteID = mSettings.SiteID;
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
      mSettings = values.StandardSettings;

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
      TypeNameCombo.LJCAddItem(15, "nvarchar");
      TypeNameCombo.LJCAddItem(11, "int");
      TypeNameCombo.LJCAddItem(18, "smallint");
      TypeNameCombo.LJCAddItem(1, "bigint");
      TypeNameCombo.LJCAddItem(2, "binary");
      TypeNameCombo.LJCAddItem(3, "bit");
      TypeNameCombo.LJCAddItem(4, "char");
      TypeNameCombo.LJCAddItem(5, "date");
      TypeNameCombo.LJCAddItem(6, "datetime");
      TypeNameCombo.LJCAddItem(7, "datetime2");
      TypeNameCombo.LJCAddItem(8, "datetimeoffset");
      TypeNameCombo.LJCAddItem(9, "decimal");
      TypeNameCombo.LJCAddItem(10, "float");
      TypeNameCombo.LJCAddItem(12, "money");
      TypeNameCombo.LJCAddItem(13, "nchar");
      TypeNameCombo.LJCAddItem(14, "ntext");
      TypeNameCombo.LJCAddItem(16, "real");
      TypeNameCombo.LJCAddItem(17, "smalldatetime");
      TypeNameCombo.LJCAddItem(19, "smallmoney");
      TypeNameCombo.LJCAddItem(20, "text");
      TypeNameCombo.LJCAddItem(21, "time");
      TypeNameCombo.LJCAddItem(22, "tinyint");
      TypeNameCombo.LJCAddItem(23, "varbinary");
      TypeNameCombo.LJCAddItem(24, "varchar");
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
    private void IdentityEnable(bool enable)
    {
      //IdentityStartText.Enabled = enable;
      //IdentityIncrementText.Enabled = enable;
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
    //// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the NameText Leave event.
    private void NameText_Leave(object sender, EventArgs e)
    {
      // Set missing description the same as column name.
      var columnName = NameText.Text.Trim();
      if (!NetString.HasValue(DescriptionText.Text))
      {
        DescriptionText.Text = columnName;
      }
      var isTypeSet = false;

      // Set TypeName = "bigint" and Identity values.
      if ("ID" == columnName)
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
        && columnName.EndsWith("ID"))
      {
        isTypeSet = true;
        if (-1 == TypeNameCombo.SelectedIndex)
        {
          SetComboIndex("bigint");
        }
        IdentityEnable(false);
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
      }
    }

    // Handles the TextChanged event.
    private void IdentityStartText_TextChanged(object sender, EventArgs e)
    {
      var identityStart = IdentityStartText.Text.Trim();
      if (identityStart.StartsWith("-"))
      {
        IdentityEnable(false);
        AllowNullCheck.Checked = false;
        AllowNullCheck.Enabled = true;
      }
      else
      {
        IdentityEnable(true);
        IdentityIncrementText.Text = "1";
        AllowNullCheck.Checked = false;
        AllowNullCheck.Enabled = false;
      }
    }

    // Handles the SelectedIndexChanged event.
    private void TypeNameCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      // Set MaxLength.
      var columnName = NameText.Text.Trim();
      var typeName = TypeNameCombo.Text.Trim();
      if ("char" == typeName
        || "nchar" == typeName
        || "nvarchar" == typeName
        || "varchar" == typeName)
      {
        IdentityEnable(false);
        var maxLength = MaxLengthText.Text.Trim();
        if ("-1" == maxLength
          || !NetString.HasValue(maxLength))
        {
          switch (columnName)
          {
            case "Name":
              if (!NetString.HasValue(MaxLengthText.Text))
              {
                MaxLengthText.Text = "60";
              }
              AllowNullCheck.Checked = false;
              break;

            case "Description":
              if (!NetString.HasValue(MaxLengthText.Text))
              {
                MaxLengthText.Text = "80";
              }
              AllowNullCheck.Checked = true;
              break;

            default:
              if (!NetString.HasValue(MaxLengthText.Text))
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
    internal long LJCID { get; set; }

    // Gets or sets the primary ID value.
    internal long LJCSiteID { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // The form position.
    internal Point LJCLocation { get; set; }

    // The Managers object.
    internal ManagersDataUtility LJCManagers { get; set; }

    // Gets or sets the Parent ID value.
    internal long LJCParentID { get; set; }

    // Gets or sets the ParentSite ID value.
    internal long LJCParentSiteID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets a reference to the record object.
    internal DataUtilColumn LJCRecord { get; private set; }

    // Gets or sets the Sequence value.
    internal int LJCSequence { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private DataUtilColumn mOriginalRecord;
    private StandardUISettings mSettings;

    private readonly TextNumber mSequence = new TextNumber();
    private readonly TextNumber mMaxLength = new TextNumber();
    private readonly TextNumber mNewMaxLength = new TextNumber();
    private readonly TextNumber mIdentityStart = new TextNumber();
    private readonly TextNumber mIdentityIncrement = new TextNumber();
    #endregion
  }
}
